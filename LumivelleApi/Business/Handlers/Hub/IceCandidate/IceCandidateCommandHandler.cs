using System.Threading;
using System.Threading.Tasks;
using Business.Helpers;
using Core.Extensions;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Hub.IceCandidate;

public class IceCandidateCommandRequest : IRequest<IResult>
{
    public string TargetUsername { get; set; }
    public string Candidate { get; set; }
}

public class IceCandidateCommandHandler(
    ISignalRClientHelper signalRClientHelper)
    : IRequestHandler<IceCandidateCommandRequest, IResult>
{
    public async Task<IResult> Handle(IceCandidateCommandRequest request, CancellationToken cancellationToken)
    {
        var currentUsername = UserInfoExtensions.GetUsername();

        await signalRClientHelper.SendToUserAsync(request.TargetUsername, "ice_candidate",
            new { callerUsername = currentUsername, candidate = request.Candidate });

        return new SuccessResult();
    }
}