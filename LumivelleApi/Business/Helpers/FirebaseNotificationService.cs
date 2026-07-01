using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Business.Helpers;

public interface IFirebaseNotificationService
{
    Task<BatchResponse> SendNotificationAsync(FirebasePushRequest request);
    Task CancelNotification(List<string> tokens, string collapseId);
}

public class FirebaseNotificationService : IFirebaseNotificationService
{
    private static bool _initialized;

    public FirebaseNotificationService(IWebHostEnvironment env)
    {
        if (_initialized) return;

        var credential = GoogleCredential
            .FromFile(Path.Combine(env.WebRootPath, "firebase-config.json"));

        FirebaseApp.Create(new AppOptions
        {
            Credential = credential
        });

        _initialized = true;
    }

    public async Task<BatchResponse> SendNotificationAsync(FirebasePushRequest request)
    {
        if (request.Tokens == null || request.Tokens.Count == 0)
            return null;

        var messages = request.Tokens.Select(token =>
            BuildMessage(token, request)
        ).ToList();

        var result = await FirebaseMessaging.DefaultInstance.SendEachAsync(messages);
        return result;
    }

    public async Task CancelNotification(List<string> tokens, string collapseId)
    {
        if (tokens == null || tokens.Count == 0)
            return;

        var messages = tokens.Select(token =>
            BuildCancelMessage(token, collapseId)
        ).ToList();

        await FirebaseMessaging.DefaultInstance.SendEachAsync(messages);
    }

    private static Message BuildMessage(string token, FirebasePushRequest r)
    {
        return new Message
        {
            Token = token,

            Notification = !r.DataOnly ? new Notification
            {
                Title = r.Title,
                Body = r.Body,
            } : null,
            
            Data = r.Data != null
                ? new Dictionary<string, string>
                {
                    ["data"] = JsonConvert.SerializeObject(r.Data)
                }
                : null,

            Android = new AndroidConfig
            {
                Priority = r.Priority == "high"
                    ? Priority.High
                    : Priority.Normal,
                CollapseKey = r.CollapseId,
            },

            Apns = new ApnsConfig
            {
                Headers = r.CollapseId != null
                    ? new Dictionary<string, string>
                    {
                        ["apns-collapse-id"] = r.CollapseId
                    }
                    : null,

                Aps = new Aps()
                {
                    Badge = r.Badge,
                    Category = r.CategoryId,
                    Sound = r.Sound,
                    ContentAvailable = r.ContentAvailable,
                    CustomData = new Dictionary<string, object>
                    {
                        ["channelId"] = r.ChannelId,
                        ["interruption-level"] = r.InterruptionLevel
                    }
                }
            }
        };
    }

    private static Message BuildCancelMessage(string token, string collapseId)
    {
        return new Message
        {
            Token = token,
            Apns = new ApnsConfig
            {
                Headers = new Dictionary<string, string>
                {
                    ["apns-collapse-id"] = collapseId
                },
                Aps = new Aps
                {
                    ContentAvailable = true
                }
            },
            Android = new AndroidConfig
            {
                CollapseKey = collapseId,
                Priority = Priority.High
            }
        };
    }
}

public class FirebasePushRequest
{
    public List<string> Tokens { get; set; } = [];
    public string Title { get; set; }
    public string Body { get; set; }
    public object? Data { get; set; }
    public string Priority { get; set; } = "high";
    public int? Badge { get; set; } = 1;
    public string? Sound { get; set; } = "default";
    public string? CategoryId { get; set; }
    public string? ChannelId { get; set; } = "default";
    public string? InterruptionLevel { get; set; } // passive | active | time-sensitive
    public string? CollapseId { get; set; } 
    public bool DataOnly { get; set; }
    public bool ContentAvailable  { get; set; }
}