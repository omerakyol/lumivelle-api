using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Wardrobe.Queries.GetOutfitSuggestions;

public class GetOutfitSuggestionsQueryRequest : IRequest<IDataResult<OutfitSuggestionsResult>>
{
}
