using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Handlers.StylePreferences.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Enums;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.StylePreferences.Commands.SavePreferences;

public class SavePreferencesCommandHandler(
    IStylePreferenceRepository stylePreferenceRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<SavePreferencesCommandRequest, IResult>
{
    [ValidationAspect(typeof(SavePreferencesValidator), Priority = 1)]
    public async Task<IResult> Handle(
        SavePreferencesCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var account =
            await accountRepository.GetAsync(x => x.Id == accountId && x.AccountStatus == AccountStatus.Active);
        if (account == null)
            throw new ApplicationException(Messages.AccountNotFound);

        var existing = await stylePreferenceRepository.GetByAccountIdAsync(accountId);

        if (existing == null)
        {
            await stylePreferenceRepository.AddAsync(new StylePreferenceDocument
            {
                AccountId = accountId,
                Styles = request.Styles,
                Goals = request.Goals
            });
        }
        else
        {
            existing.Styles = request.Styles;
            existing.Goals = request.Goals;
            existing.UpdatedAt = DateTime.UtcNow;
            await stylePreferenceRepository.UpdateAsync(existing.Id, existing);
        }

        return new SuccessResult(new ResultMessage { Code = Messages.Updated, Description = Messages.Updated });
    }
}