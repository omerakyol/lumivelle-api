using System.Threading;
using System.Threading.Tasks;
using Business.Helpers;
using Core.Extensions;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Hub.IceCandidate;

public class IceCandidateCommandRequest : IRequest<IResult>
{
    public string TargetEmail { get; set; }
    public string Candidate { get; set; }
}

public class IceCandidateCommandHandler(
    ISignalRClientHelper signalRClientHelper)
    : IRequestHandler<IceCandidateCommandRequest, IResult>
{
    public async Task<IResult> Handle(IceCandidateCommandRequest request, CancellationToken cancellationToken)
    {
        var email = UserInfoExtensions.GetAccountEmail();

        await signalRClientHelper.SendToUserAsync(request.TargetEmail, "ice_candidate",
            new { callerEmail = email, candidate = request.Candidate });

        return new SuccessResult();
    }
}