using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Utilities.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace WebAPI.OpenApi;

/// <summary>
/// Sets the document metadata and registers the JWT bearer security scheme,
/// applying it as a global requirement (mirrors the previous Swagger setup).
/// </summary>
internal sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        document.Info.Title = SwaggerMessages.Title;
        document.Info.Version = SwaggerMessages.Version;
        document.Info.TermsOfService = new Uri(SwaggerMessages.TermsOfService);
        document.Info.Contact = new OpenApiContact
        {
            Name = SwaggerMessages.ContactName,
            Url = new Uri(SwaggerMessages.ContactUrl)
        };
        document.Info.License = new OpenApiLicense
        {
            Name = "LICX",
            Url = new Uri(SwaggerMessages.LicenceUrl)
        };

        var bearerScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT access token only — without the 'Bearer ' prefix."
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes["Bearer"] = bearerScheme;

        document.Security =
        [
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()
            }
        ];

        return Task.CompletedTask;
    }
}

/// <summary>
/// Adds 401/403 responses to secured operations and clears the global bearer
/// requirement from operations marked with [AllowAnonymous].
/// </summary>
internal sealed class AuthorizationOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        var metadata = context.Description.ActionDescriptor.EndpointMetadata;
        var allowAnonymous = metadata.OfType<IAllowAnonymous>().Any();

        if (allowAnonymous)
        {
            // Override the document-level requirement: this endpoint needs no auth.
            operation.Security = new List<OpenApiSecurityRequirement>();
            return Task.CompletedTask;
        }

        var requiresAuthorization = metadata.OfType<IAuthorizeData>().Any();
        if (!requiresAuthorization) return Task.CompletedTask;

        operation.Responses ??= new OpenApiResponses();
        operation.Responses["401"] = new OpenApiResponse { Description = "Unauthorized" };
        operation.Responses["403"] = new OpenApiResponse { Description = "Forbidden" };

        return Task.CompletedTask;
    }
}