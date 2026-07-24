using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Posts;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Comments.Queries.GetComments;

public class GetCommentsQueryHandler(
    ICommentRepository commentRepository,
    IAccountRepository accountRepository,
    IFollowRepository followRepository)
    : IRequestHandler<GetCommentsQueryRequest, IDataResult<CommentPageResult>>
{
    public async Task<IDataResult<CommentPageResult>> Handle(
        GetCommentsQueryRequest request,
        CancellationToken cancellationToken)
    {
        var viewerAccountId = UserInfoExtensions.GetAccountId();
        var postId = ObjectId.Parse(request.PostId);
        var cursor = PostResultBuilder.ParseCursor(request.Cursor);

        var comments = await commentRepository.GetByPostIdPageAsync(postId, cursor, PostResultBuilder.PageSize);

        if (comments.Count == 0)
            return new SuccessDataResult<CommentPageResult>(
                new CommentPageResult { Comments = [], NextCursor = null });

        var authors = await AuthorLookup.GetAuthorsAsync(
            accountRepository, followRepository, viewerAccountId, comments.Select(c => c.AccountId));

        var results = comments
            .Select(c => CommentResult.FromDocument(c, authors.GetValueOrDefault(c.AccountId.ToString())))
            .ToList();

        var page = new CommentPageResult
        {
            Comments = results,
            NextCursor = comments.Count < PostResultBuilder.PageSize
                ? null
                : comments[^1].CreatedAt.ToString("o")
        };

        return new SuccessDataResult<CommentPageResult>(page);
    }
}
