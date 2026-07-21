using System;
using Core.Enums;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace Core.Extensions;

public static class UserInfoExtensions
{
    private static readonly IHttpContextAccessor HttpContextAccessor =
        ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

    public static ObjectId GetAccountId()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        var accountId = httpContext?.User.FindFirst(ClaimExtensions.NameIdentifier)?.Value;
        return accountId != null ? ObjectId.Parse(accountId) : ObjectId.Empty;
    }

    public static string GetAccountEmail()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        var email = httpContext?.User.FindFirst(ClaimExtensions.NameUniqueIdentifier)?.Value;
        return email;
    }

    public static AccountType? GetAccountType()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        var accountType = httpContext?.User.FindFirst(ClaimExtensions.AccountTypeIdentifier)?.Value;
        return accountType != null ? Enum.Parse<AccountType>(accountType) : null;
    }
}