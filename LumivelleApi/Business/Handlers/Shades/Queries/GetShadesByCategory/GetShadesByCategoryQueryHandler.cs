using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;

namespace Business.Handlers.Shades.Queries.GetShadesByCategory;

public class GetShadesByCategoryQueryHandler(IShadeRepository shadeRepository, IAccountRepository accountRepository)
    : IRequestHandler<GetShadesByCategoryQueryRequest, IDataResult<List<ShadeResult>>>
{
    private const string DefaultLanguage = "en";

    public async Task<IDataResult<List<ShadeResult>>> Handle(
        GetShadesByCategoryQueryRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        var language = string.IsNullOrWhiteSpace(account?.Language) ? DefaultLanguage : account.Language;

        var documents = await shadeRepository.GetByCategoryAsync(request.Category);

        var results = documents
            .Select(d => new ShadeResult { Id = d.Id.ToString(), Name = Localize(d.Name, language), Hex = d.Hex })
            .ToList();

        return new SuccessDataResult<List<ShadeResult>>(results);
    }

    private static string Localize(Dictionary<string, string> values, string language)
    {
        if (values == null || values.Count == 0) return string.Empty;
        if (values.TryGetValue(language, out var localized) && !string.IsNullOrEmpty(localized))
            return localized;
        if (values.TryGetValue(DefaultLanguage, out var fallback) && !string.IsNullOrEmpty(fallback))
            return fallback;
        return values.Values.First();
    }
}