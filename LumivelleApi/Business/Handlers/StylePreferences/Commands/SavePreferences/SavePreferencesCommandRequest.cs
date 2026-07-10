using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.StylePreferences.Commands.SavePreferences;

public class SavePreferencesCommandRequest : IRequest<IResult>
{
    public string[] Styles { get; set; } = [];
    public string[] Goals { get; set; } = [];
}
