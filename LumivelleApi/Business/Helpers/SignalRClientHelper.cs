using System;
using System.Threading.Tasks;
using Business.Hub;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Microsoft.AspNetCore.SignalR; 

namespace Business.Helpers;

public interface ISignalRClientHelper
{
    Task SendToAllAsync(string method, object message);
    Task SendToUserAsync(string email, string method, object message);
    Task SendToGroupAsync(string groupName, string method, object message);
    Task AddToGroupAsync(string connectionId, string groupName);
    Task RemoveFromGroupAsync(string connectionId, string groupName);
}

public class SignalRClientHelper(IHubContext<TulparHub> hubContext, MongoDbLogger logger)
    : ISignalRClientHelper
{
    public async Task SendToAllAsync(string method, object message)
    {
        try
        {
            await hubContext.Clients.All.SendAsync(method, message);
        }
        catch (Exception ex)
        {
            logger.Error($"An error occurred while sending message to all clients using method '{method}'", ex);
        }
    }

    public async Task SendToUserAsync(string email, string method, object message)
    {
        try
        {
            
            if (string.IsNullOrWhiteSpace(email))
                return;

            var user = AccountConnectionHelper.GetAccountByUsername(email); 
            if (user == null)
                return;

            if (string.IsNullOrWhiteSpace(user.ConnectionId))
                return;

            await hubContext.Clients.Client(user.ConnectionId).SendAsync(method, message);
        }
        catch (Exception ex)
        {
            logger.Error($"An error occurred while sending message to user '{email}' using method '{method}'", ex);
        }
    }

    public async Task SendToGroupAsync(string groupName, string method, object message)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(groupName))
                return;

            await hubContext.Clients.Group(groupName).SendAsync(method, message);
        }
        catch (Exception ex)
        {
            logger.Error($"An error occurred while sending message to group '{groupName}' using method '{method}'", ex);
        }
    }

    public async Task AddToGroupAsync(string connectionId, string groupName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(connectionId) || string.IsNullOrWhiteSpace(groupName))
                return;

            await hubContext.Groups.AddToGroupAsync(connectionId, groupName); 
        }
        catch (Exception ex)
        {
            logger.Error($"An error occurred while adding connection '{connectionId}' to group '{groupName}'", ex);
        }
    }

    public async Task RemoveFromGroupAsync(string connectionId, string groupName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(connectionId) || string.IsNullOrWhiteSpace(groupName))
                return;

            await hubContext.Groups.RemoveFromGroupAsync(connectionId, groupName); 
        }
        catch (Exception ex)
        {
            logger.Error($"An error occurred while removing connection '{connectionId}' from group '{groupName}'", ex);
        }
    }
}