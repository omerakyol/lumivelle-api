using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text.Json;
using System.Threading.Tasks;
using Business.Helpers;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Context;

namespace Business.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    private static readonly ITranslateRepository TranslateRepository =
        ServiceTool.ServiceProvider.GetService<ITranslateRepository>();

    private static readonly ILanguageHelper LanguageHelper =
        ServiceTool.ServiceProvider.GetService<ILanguageHelper>();

    private static readonly MongoDbLogger Logger = ServiceTool.ServiceProvider.GetService<MongoDbLogger>();

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            PerformanceEnricher.SetRequestStartTime(httpContext);
            await next(httpContext);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        _ = e.Message;

        var result = new Result(false, []);

        var acceptLanguage = LanguageHelper.GetAcceptLanguageWithDefaultRules();

        if (e.GetType() == typeof(ValidationException))
        {
            var validationTranslates = await TranslateRepository.GetTranslates(acceptLanguage);
            var messages = ((ValidationException)e).Errors.Select(x => new { x.ErrorMessage, x.PropertyName })
                .Distinct()
                .Select(x =>
                {
                    var match = validationTranslates.FirstOrDefault(y => y.Code == x.ErrorMessage);
                    return new ResultMessage
                        { Code = x.ErrorMessage, Description = match != null ? match.Value : x.ErrorMessage };
                })
                .ToList();
            result.Messages.AddRange(messages);
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
 
            Logger.Warn("ValidationException {@Errors}", result.Messages, e);
        }
        else if (e.GetType() == typeof(ApplicationException))
        {
            var applicationTranslates = await TranslateRepository.GetTranslates(acceptLanguage);
            var exceptionCode = e.Message;
            var exceptionMessage = applicationTranslates.FirstOrDefault(y => y.Code == exceptionCode);
            result.Messages.Add(new ResultMessage { Code = exceptionCode, Description = exceptionMessage?.Value });
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            Logger.Warn(exceptionMessage?.Value + " {@Errors}", result.Messages, e); 
        }
        else if (e.InnerException?.GetType() == typeof(ApplicationException))
        {
            var applicationTranslates = await TranslateRepository.GetTranslates(acceptLanguage);
            var exceptionCode = e.InnerException?.Message;
            var exceptionMessage = applicationTranslates.FirstOrDefault(y => y.Code == exceptionCode);
            result.Messages.Add(new ResultMessage { Code = exceptionCode, Description = exceptionMessage?.Value });
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            Logger.Warn(exceptionMessage?.Value + " {@Errors}", result.Messages, e); 
        }
        else if (e.InnerException?.InnerException?.GetType() == typeof(ApplicationException))
        {
            var applicationTranslates = await TranslateRepository.GetTranslates(acceptLanguage);
            var exceptionCode = e.InnerException?.InnerException?.Message;
            var exceptionMessage = applicationTranslates.FirstOrDefault(y => y.Code == exceptionCode);
            result.Messages.Add(new ResultMessage { Code = exceptionCode, Description = exceptionMessage?.Value });
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            
            Logger.Warn(exceptionMessage?.Value + " {@Errors}", result.Messages, e); 
        }
        else if (e.GetType() == typeof(UnauthorizedAccessException) || e.GetType() == typeof(SecurityException))
        {
            result.Messages.AddRange(new ResultMessage
                { Code = StatusCodes.Status401Unauthorized.ToString(), Description = e.Message });
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            Logger.Warn("UnauthorizedAccessException {@Errors}", result.Messages, e); 
        }
        else if (e.GetType() == typeof(NotSupportedException))
        {
            result.Messages.AddRange(new ResultMessage { Code = "400", Description = e.Message });
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            Logger.Warn("NotSupportedException {@Errors}", result.Messages, e);  
        }
        else
        {
            // Do not leak internal exception details to the client; log the full
            // exception server-side and return a generic message.
            result.Messages.AddRange(new ResultMessage
                { Code = "500", Description = "An unexpected error occurred." });

            Logger.Error(e.Message + " {@Errors}", result.Messages, e);
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result, options));
    }
}