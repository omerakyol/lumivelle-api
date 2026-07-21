using System;
using System.Threading.Tasks; 
using Business.Handlers.Hub.Connect;
using Business.Handlers.Hub.Disconnect; 
using Business.Helpers;
using Core.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Business.Hub;

[Authorize]
public class TulparHub(IMediator mediator, ISignalRClientHelper signalRClientHelper) : Microsoft.AspNetCore.SignalR.Hub
{
    public override async Task OnConnectedAsync()
    {
        var email = UserInfoExtensions.GetAccountEmail();

        if (!string.IsNullOrWhiteSpace(email))
            AccountConnectionHelper.AddAccount(Context.ConnectionId, email);

        await mediator.Send(new ConnectCommandRequest());
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await mediator.Send(new DisconnectCommandRequest());
        AccountConnectionHelper.RemoveAccount(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}