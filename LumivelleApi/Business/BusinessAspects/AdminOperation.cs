using System.Security;
using Castle.DynamicProxy;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Interceptors;

namespace Business.BusinessAspects;

/// <summary>
/// This Aspect control the user's roles in HttpContext by inject the IHttpContextAccessor.
/// It is checked by writing as [AdminOperation] on the handler.
/// If a valid authorization cannot be found in aspect, it throws an exception.
/// </summary>
public class AdminOperation : MethodInterception
{
    protected override void OnBefore(IInvocation invocation)
    {
        var accountType = UserInfoExtensions.GetAccountType();
        if (accountType != AccountType.Admin) throw new SecurityException(Messages.AuthorizationsDenied);
    }
}