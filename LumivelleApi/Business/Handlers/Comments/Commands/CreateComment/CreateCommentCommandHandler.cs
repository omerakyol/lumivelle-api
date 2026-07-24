using System;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Business.Handlers.Comments.ValidationRules;
using Business.Handlers.Posts;
using Core.Aspects.Autofac.Validation;
using Core.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using MongoDB.Bson;

namespace Business.Handlers.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler(
    IPostRepository postRepository,
    ICommentRepository commentRepository,
    IAccountRepository accountRepository)
    : IRequestHandler<CreateCommentCommandRequest, IDataResult<CommentResult>>
{
    [ValidationAspect(typeof(CreateCommentValidator), Priority = 2)]
    public async Task<IDataResult<CommentResult>> Handle(
        CreateCommentCommandRequest request,
        CancellationToken cancellationToken)
    {
        var accountId = UserInfoExtensions.GetAccountId();
        var postId = ObjectId.Parse(request.PostId);
        var post = await postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new ApplicationException(Messages.PostNotFound);

        var document = new CommentDocument
        {
            PostId = postId,
            AccountId = accountId,
            Text = request.Text
        };

        await commentRepository.AddAsync(document);

        post.CommentCount += 1;
        await postRepository.UpdateAsync(post);

        var account = await accountRepository.GetByIdAsync(accountId);
        var author = AuthorLookup.ToAuthorResult(account);

        return new SuccessDataResult<CommentResult>(CommentResult.FromDocument(document, author));
    }
}
