using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Posts.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Posts.Commands.CreatePost;

public class CreatePostCommandHandler(
    IPostRepository postRepository,
    IWardrobeItemRepository wardrobeItemRepository,
    IOutfitRepository outfitRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<CreatePostCommandRequest, IDataResult<PostResult>>
{
    [SecuredOperation(Priority = 1)]
    [ValidationAspect(typeof(CreatePostValidator), Priority = 2)]
    public async Task<IDataResult<PostResult>> Handle(
        CreatePostCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();

        ObjectId? wardrobeItemId = null;
        if (!string.IsNullOrEmpty(request.WardrobeItemId))
        {
            var itemId = ObjectId.Parse(request.WardrobeItemId);
            var item = await wardrobeItemRepository.GetByIdAsync(itemId);
            if (item == null || item.AccountId != accountId)
                return new ErrorDataResult<PostResult>(
                    new ResultMessage { Code = "NOT_FOUND", Description = "Wardrobe item not found" });

            wardrobeItemId = itemId;
        }

        ObjectId? outfitId = null;
        if (!string.IsNullOrEmpty(request.OutfitId))
        {
            var oId = ObjectId.Parse(request.OutfitId);
            var outfit = await outfitRepository.GetByIdAsync(oId);
            if (outfit == null || outfit.AccountId != accountId)
                return new ErrorDataResult<PostResult>(
                    new ResultMessage { Code = "NOT_FOUND", Description = "Outfit not found" });

            outfitId = oId;
        }

        var document = new PostDocument
        {
            AccountId = accountId,
            ImageUrls = request.ImageUrls,
            Caption = request.Caption ?? string.Empty,
            WardrobeItemId = wardrobeItemId,
            OutfitId = outfitId,
            LikeCount = 0,
            CommentCount = 0
        };

        await postRepository.AddAsync(document);

        var account = await accountRepository.GetByIdAsync(accountId);
        var author = AuthorLookup.ToAuthorResult(account);

        return new SuccessDataResult<PostResult>(PostResult.FromDocument(document, author, false, false));
    }
}
