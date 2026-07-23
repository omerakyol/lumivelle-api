using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Collections.Commands.DeleteCollection;

public class DeleteCollectionCommandRequest : IRequest<IResult>
{
    public string Id { get; set; }
}
