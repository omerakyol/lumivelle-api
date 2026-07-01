using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.ChangeLanguage;

public class ChangeLanguageCommandRequest : IRequest<IResult>
{
    public string Language { get; set; }
}