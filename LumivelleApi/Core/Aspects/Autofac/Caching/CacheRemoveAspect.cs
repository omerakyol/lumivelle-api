using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching;

/// <summary>
/// CacheRemoveAspect
/// </summary>
public class CacheRemoveAspect(string pattern = "") : MethodInterception
{
    private string _pattern = pattern;
    private readonly ICacheManager _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
    private const string CommandHandler = "CommandHandler";
    private const string Create = "Create";
    private const string Update = "Update";
    private const string Delete = "Delete";
    private const string Get = "Get";

    protected override void OnSuccess(IInvocation invocation)
    {
        if (string.IsNullOrEmpty(_pattern))
        {
            var targetTypeName = invocation?.TargetType?.Name;
            if (!string.IsNullOrEmpty(targetTypeName))
            {
                targetTypeName = targetTypeName.Replace(CommandHandler, string.Empty);
                targetTypeName = targetTypeName.Replace(Create, string.Empty);
                targetTypeName = targetTypeName.Replace(Update, string.Empty);
                targetTypeName = targetTypeName.Replace(Delete, string.Empty);
                _pattern = Get + targetTypeName;
            }
        }

        _cacheManager.RemoveByPattern(_pattern);
    }
}