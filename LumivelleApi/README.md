# Build Project with Any Terminal

`> dotnet build`

`Build succeeded.
0 Warning(s)
0 Error(s)`

# Run Api Project with Any Terminal

`> dotnet dev-certs https --trust`
`> dotnet watch run --project ./WebAPI/WebAPI.csproj`

# Use as a `dotnet new` Template

This repository is also a `dotnet new` template, so you can scaffold a fresh
Web API solution from it.

## Install

From a local clone (the template tracks the repo, so edits here update it):

```bash
dotnet new install .
```

Or install a packaged version (see *Packaging* below):

```bash
dotnet new install ./nupkg/Tulpar.AspCore.Templates.1.0.0.nupkg
```

## Create a project

```bash
dotnet new tulpar-aspcore -n ShopApp        # creates ./ShopApp with ShopApp.sln
dotnet new tulpar-aspcore -n ShopApp --ApiTitle "Shop API" --CompanyName "Globex" --CompanyDomain "globex.example" --ApiVersionHeader "x-shop-api-version" --HubPath "shop-live" # creates ./ShopApp with ShopApp.sln fully branded
```

`-n` (the project name) renames the solution and assigns a fresh
`SolutionGuid` and per-project `UserSecretsId` so each generated app has its
own user-secrets store. The layer projects (`Core`, `Entities`, `DataAccess`,
`Business`, `WebAPI`) keep their names by design. Packages are restored
automatically after creation.

Short names: `tulpar-aspcore` or `TulparAspCoreStarter`.

### Branding options

The default branding can be overridden at creation time (all optional; defaults
keep the Tulpar branding):

| Option | Default | Replaces |
| --- | --- | --- |
| `--ApiTitle` | `Lumivelle API` | OpenAPI/Scalar title and `GlobalConfig.ApplicationName` |
| `--CompanyName` | `Lumivelle` | OpenAPI contact name and Hangfire dashboard title |
| `--CompanyDomain` | `lumivelle.app` | JWT issuer/audience and contact/terms/licence URLs |
| `--ApiVersionHeader` | `x-lumivelle-api-version` | API-version request header name |
| `--HubPath` | `lumivelle-live` | SignalR endpoint segment (`/hub/<HubPath>`) |

```bash
dotnet new tulpar-aspcore -n Acme \
  --ApiTitle "Acme Commerce API" \
  --CompanyName "Acme Inc" \
  --CompanyDomain "acme.io" \
  --ApiVersionHeader "x-acme-api-version" \
  --HubPath "acme-realtime"
```

(The internal `TulparHub` SignalR class name is not parameterized; rename it
manually if desired — only the public `/hub/<HubPath>` route is templated.)

## Uninstall / update

```bash
dotnet new uninstall <path-or-package-id>   # e.g. the repo path, or Tulpar.AspCore.Templates
```

## Packaging (for sharing / a NuGet feed)

```bash
dotnet pack template-pack/Tulpar.AspCore.Templates.csproj -o ./nupkg
# then `dotnet nuget push` the .nupkg to your feed, or install it locally
```

The template definition lives in `.template.config/template.json`; the
packaging project is `template-pack/Tulpar.AspCore.Templates.csproj` (it is
deliberately not part of `LumivelleApi.sln`).
