using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.StylePreferences.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;

namespace Business.Handlers.StylePreferences.Commands.SavePreferences;

public class SavePreferencesCommandHandler(IStylePreferenceRepository stylePreferenceRepository)
    : IRequestHandler<SavePreferencesCommandRequest, IResult>
{
    [SecuredOperation(Priority = 1)]
    [ValidationAspect(typeof(SavePreferencesValidator), Priority = 2)]
    public async Task<IResult> Handle(
        SavePreferencesCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
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
