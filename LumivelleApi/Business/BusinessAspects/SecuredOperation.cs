using System.Collections.Generic;
using System.Linq;
using System.Security;
using Castle.DynamicProxy;
using Core.Constants;
using Core.CrossCuttingConcerns.Caching;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects;

/// <summary>
/// This Aspect control the user's roles in HttpContext by inject the IHttpContextAccessor.
/// It is checked by writing as [SecuredOperation] on the handler.
/// If a valid authorization cannot be found in aspect, it throws an exception.
/// </summary>
public class SecuredOperation : MethodInterception
{
    private readonly ICacheManager _cacheManager;

    public SecuredOperation()
    {
        _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var oprClaims = _cacheManager.Get<IEnumerable<string>>($"{CacheKeys.UserIdForClaim}={accountId}");
        var operationName = invocation.TargetType?.ReflectedType?.Name;
        if (oprClaims != null && oprClaims.Contains(operationName)) return;

        throw new SecurityException(Messages.AuthorizationsDenied);
    }
}