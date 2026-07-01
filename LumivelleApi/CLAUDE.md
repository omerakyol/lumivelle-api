# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build the whole solution
dotnet build

# Run the API with hot reload (only entry point — there is no separate host project)
dotnet dev-certs https --trust            # one-time, trust the local HTTPS cert
dotnet watch run --project ./WebAPI/WebAPI.csproj

# Run a single project / publish
dotnet run --project ./WebAPI/WebAPI.csproj
dotnet publish WebAPI/WebAPI.csproj -c Release -o ./publish

# Docker (multi-stage build, runs WebAPI.dll on port 80)
docker build -t starter . && docker run -p 8080:80 starter
```

- **No test project exists** in the solution. Do not invent a `dotnet test` workflow unless one is added.
- Targets **.NET 10** (`net10.0`).
- API docs use the built-in `Microsoft.AspNetCore.OpenApi` generator (no Swashbuckle). The OpenAPI document is at `/openapi/v1.json` (`MapOpenApi()`); the interactive **Scalar** UI is at `/scalar` (`MapScalarApiReference()`). Keep these at their defaults: a custom Scalar path with a trailing-slash redirect (e.g. `/doc` → `/doc/`) breaks Scalar's relative resolution of the document URL. The Hangfire dashboard is at `/jobs` (basic auth, credentials in `TaskSchedulerOptions`).
- API version is selected via the `x-lumivelle-api-version` request header (defaults to `1.0`).

## Architecture

This is a layered CQRS template (derived from the DevArchitecture pattern). Five projects with a strict reference chain: **WebAPI → Business → DataAccess → Core**, with **Entities** referenced across layers.

- **Core** — framework-agnostic infrastructure: aspects, cross-cutting concerns (caching/logging/validation), the repository base classes, results pattern, JWT/security helpers, IoC utilities, Hangfire/ElasticSearch/RabbitMQ/Mail integrations.
- **Entities** + **Core/Entities** — domain entities and DTOs. Mongo documents derive from `DocumentDbEntity`.
- **DataAccess** — repository interfaces (`Abstract/`) and implementations (`Concrete/`).
- **Business** — all application logic as MediatR handlers, plus DI wiring, middlewares, SignalR hub logic, and scheduled jobs.
- **WebAPI** — thin host: controllers that only dispatch to MediatR, plus startup/pipeline config.

### CQRS + MediatR (the core pattern)
Every operation is a MediatR request. Controllers do nothing but `Mediator.Send(request)` and translate the result. Feature handlers live under `Business/Handlers/<Feature>/Commands|Queries/<Action>/` and conventionally come as a trio:

- `<Action>CommandRequest.cs` — implements `IRequest<IDataResult<T>>` (or `IRequest<IResult>`)
- `<Action>CommandHandler.cs` — implements `IRequestHandler<TRequest, TResult>`
- `<Action>CommandResult.cs` — the payload (when applicable)
- FluentValidation rules live alongside in `Business/Handlers/<Feature>/ValidationRules/`

Handlers and validators are **auto-registered by assembly scan** in `Business/DependencyResolvers/AutofacBusinessModule.cs` (`IRequestHandler<,>`, `IValidator<>`). Adding a new handler/validator requires no manual DI registration.

### Aspect-Oriented Programming (how cross-cutting concerns are applied)
Behavior is attached declaratively via attributes on the handler's `Handle` method, intercepted by Castle DynamicProxy. `AutofacBusinessModule` enables interface interceptors with `AspectInterceptorSelector`; aspects extend `MethodInterception` (`Core/Utilities/Interceptors`). Available aspects:

- `[ValidationAspect(typeof(XValidator), Priority = n)]` — runs FluentValidation before the method
- `[CacheAspect]` / `[CacheRemoveAspect]` — read-through cache / invalidation (`Core/Aspects/Autofac/Caching`)
- `[PerformanceAspect]`, `[TransactionScopeAspect]` / `[TransactionScopeAspectAsync]`
- `[SecuredOperation]` / `[AdminOperation]` (`Business/BusinessAspects`) — authorization; `SecuredOperation` checks the **handler class name** against the user's operation claims cached under `UserIdForClaim={accountId}`. Authorization is claim-per-operation, not role-based middleware.

Aspects resolve dependencies through the static service locator `ServiceTool.ServiceProvider` (set once in `WebAPI/Startup.cs` `Configure`). This is intentional — do not try to constructor-inject into aspects.

### Results pattern
Handlers never return raw data. They return `IResult` / `IDataResult<T>` (`Core/Utilities/Results`): `SuccessResult`, `SuccessDataResult<T>`, `ErrorResult`, `ErrorDataResult<T>`. Controllers convert with `result.Success ? Ok(result) : BadRequest(result.Messages)`. `BaseApiController` exposes the shared `Mediator` accessor and HTTP-status helpers.

### Data access — MongoDB is the live store
Despite the `Concrete/EntityFramework/` folder name, the active repositories extend `MongoDbRepositoryBase<T>` and implement `IDocumentDbRepository<T>` (`Core/DataAccess`). Mongo connection comes from `MongoDbSettings`; `MongoDbContext` is a singleton. Repositories are registered explicitly in `Business/Startup.cs` (`AddScoped<IAccountRepository, AccountRepository>` etc.) — add new ones there.

The EF base class (`EfEntityRepositoryBase`), `DataAccess/Migrations/HowTo.md`, and the SQL config sections are **leftover scaffolding from the base template and are not wired up**. Only reach for EF/relational migrations if you are intentionally adding a relational store; otherwise model new data as Mongo documents.

### Startup split
Service registration is layered across two classes: `Business/Startup.cs` (`BusinessStartup`) holds core service registrations (MediatR, FluentValidation, Mongo, Hangfire, helpers); `WebAPI/Startup.cs : BusinessStartup` adds web concerns (auth, CORS, versioning, SignalR, OpenAPI/Scalar) and calls `base.ConfigureServices`. OpenAPI customizations (JWT bearer scheme, per-operation auth metadata) live in `WebAPI/OpenApi/OpenApiTransformers.cs` as `IOpenApiDocumentTransformer`/`IOpenApiOperationTransformer` implementations. `Program.cs` uses Autofac via `AutofacServiceProviderFactory`; `ConfigureContainer` registers `AutofacBusinessModule`.

### Other integrations
- **Auth**: JWT bearer (`TokenOptions`); `ITokenHelper`/`JwtHelper`. SignalR connections authenticate via `access_token` query string on `/hub/lumivelle-live`.
- **Scheduled jobs**: Hangfire, storage backend selectable in `TaskSchedulerOptions.StorageType` (`mongodb`/`postgresql`/`mssql`/`inmemory`). Recurring jobs registered in `WebAPI/Startup.cs` `AddRecurringJobs`.
- **Pipeline middlewares** (order matters, see `Configure`): `UseForwardedHeaders` → (`UseDeveloperExceptionPage` in Development only) → custom exception middleware → `SecurityBlockMiddleware` → CORS → routing → rate limiter → auth. `SecurityBlockMiddleware` blocks `curl`/`wget`/etc. user-agents (use a browser UA when probing the API). The old `Fail2BanMiddleware` was removed — rate limiting is now handled by the built-in limiter below.
- **Forwarded headers**: `UseForwardedHeaders` runs first so client IP/scheme are correct behind a proxy. Only proxies listed in `ForwardedHeaders:KnownProxies` are trusted (empty by default = forwarded headers ignored, direct connection IP used) — prevents `X-Forwarded-For` spoofing of the rate limiter.
- **Rate limiting**: built-in `Microsoft.AspNetCore.RateLimiting` (`AddRateLimiter`/`UseRateLimiter` in `WebAPI/Startup.cs`). Global limiter = 100 req/min per IP; the `"auth"` policy (`RateLimitPolicies.Auth`, 10 req/min per IP) is applied via `[EnableRateLimiting(...)]` to credential/OTP endpoints on `AccountController`. Rejections return 429 with `Retry-After: 60`.
- **CORS**: policy `AllowOrigin`. Configure `Cors:AllowedOrigins` to enable credentialed cross-origin requests for specific origins; with no allow-list it falls back to any-origin **without** credentials (never combine wildcard origin with `AllowCredentials`).
- **Uploads**: `UploadFileValidator` allows image/video/audio MIME types (SVG excluded — stored-XSS risk). Static files (incl. `/media`) are served with `X-Content-Type-Options: nosniff` and a restrictive CSP. Login returns a single generic `InvalidCredentials` message to avoid username enumeration.
- **i18n**: `ILanguageHelper`, translate handlers under `Business/Handlers/Translates`, and `Messages` constants in `Core/Constants`.
- **App mode**: `ConfigurationManager.Mode` (`ApplicationMode` from `AppSettings:Mode`) gates behavior in both `Startup` and `AutofacBusinessModule` (e.g. `Business.Adapters` are only registered in Production).
- Other adapters present: Firebase/Expo push notifications, Apple/Google in-app purchase validation, ElasticSearch, RabbitMQ, SMTP mail, SMS service.

## Conventions

- Add a new feature: create the request/handler/(result)/validation-rules folder under `Business/Handlers/...`, then a thin controller action that sends the request — no DI changes needed for the handler itself.
- Apply cross-cutting behavior by attribute on `Handle`, not by editing the pipeline.
- Config secrets in `appsettings.json` are committed placeholders (Mongo URI, JWT key, dashboard creds) — treat them as non-production and override via environment/user-secrets (`UserSecretsId` is set on WebAPI).
- All async repository methods exist alongside sync ones; prefer the `...Async` variants in handlers.
</content>
</invoke>
