using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Business.Helpers;

public interface IExpoNotificationService
{
    Task<PushTicketResponse> SendNotificationAsync(PushTicketRequest request);

    Task<TicketDeliveryStatus> GetNotificationReceiptsAsync(string ticketId);
}

public class ExpoNotificationService : IExpoNotificationService
{
    // Environment Configuration
    private const string ExpoBackendHost = "https://exp.host";
    private const string PushSendPath = "/--/api/v2/push/send";
    private const string PushGetReceiptsPath = "/--/api/v2/push/getReceipts";

    public async Task<PushTicketResponse> SendNotificationAsync(PushTicketRequest request)
    {
        request.PushChannelId ??= "default";
        request.PushSound ??= "default";
        request.PushPriority ??= "high";
        request.PushBadgeCount ??= 1;
        request.PushTo ??= [];

        var response = await PostAsync<PushTicketRequest, PushTicketResponse>(request, PushSendPath);

        return response;
    }

    public async Task<TicketDeliveryStatus> GetNotificationReceiptsAsync(string ticketId)
    {
        var receiptResponse = await PostAsync<PushReceiptRequest, PushReceiptResponse>(
            new PushReceiptRequest { PushTicketIds = [ticketId] }, PushGetReceiptsPath);

        if (receiptResponse?.PushTicketReceipts == null || receiptResponse.PushTicketReceipts.Count == 0)
            return TicketDeliveryStatus.NotDelivered;

        var firstReceiptData = receiptResponse.PushTicketReceipts.First().Value;

        if (
            firstReceiptData is not { DeliveryStatus: "ok" } ||
            firstReceiptData.DeliveryDetails is Newtonsoft.Json.Linq.JObject details && details["error"] != null
        )
            return TicketDeliveryStatus.NotDelivered;

        return TicketDeliveryStatus.Delivered;
    }

    private async Task<U> PostAsync<T, U>(T requestObj, string path) where T : new()
    {
        try
        {
            var serializedRequestObj = JsonConvert.SerializeObject(requestObj, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            var requestBody = new StringContent(serializedRequestObj, System.Text.Encoding.UTF8, "application/json");
            var responseBody = default(U);

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ExpoBackendHost);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.PostAsync(path, requestBody);

            if (!response.IsSuccessStatusCode) return responseBody;

            var rawResponseBody = await response.Content.ReadAsStringAsync();
            responseBody = JsonConvert.DeserializeObject<U>(rawResponseBody);

            return responseBody;
        }
        catch
        {
            return default;
        }
    }
}

public enum TicketDeliveryStatus
{
    NotDelivered = 0,
    Delivered = 1,
}

[JsonObject(MemberSerialization.OptIn)]
public class PushTicketRequest
{
    [JsonProperty(PropertyName = "to")] public List<string> PushTo { get; set; }

    [JsonProperty(PropertyName = "data")] public object PushData { get; set; }

    [JsonProperty(PropertyName = "title")] public string PushTitle { get; set; }

    [JsonProperty(PropertyName = "body")] public string PushBody { get; set; }

    [JsonProperty(PropertyName = "ttl")] public int? PushTTL { get; set; }

    [JsonProperty(PropertyName = "expiration")]
    public int? PushExpiration { get; set; }

    [JsonProperty(PropertyName = "priority")] // 'default' | 'normal' | 'high'
    public string PushPriority { get; set; }

    [JsonProperty(PropertyName = "subtitle")]
    public string PushSubTitle { get; set; }

    [JsonProperty(PropertyName = "sound")] // 'default' | null	
    public string PushSound { get; set; }

    [JsonProperty(PropertyName = "badge")] public int? PushBadgeCount { get; set; }

    [JsonProperty(PropertyName = "channelId")]
    public string PushChannelId { get; set; }

    [JsonProperty(PropertyName = "categoryId")]
    public string PushCategoryId { get; set; }
    [JsonProperty(PropertyName = "interruptionLevel")]
    public string PushInterruptionLevel { get; set; } // 'passive' | 'active' | 'time-sensitive'	
}

[JsonObject(MemberSerialization.OptIn)]
public class PushTicketResponse
{
    [JsonProperty(PropertyName = "data")] public List<PushTicketStatus> PushTicketStatuses { get; set; }

    [JsonProperty(PropertyName = "errors")]
    public List<PushTicketErrors> PushTicketErrors { get; set; }
}

[JsonObject(MemberSerialization.OptIn)]
public class PushTicketStatus
{
    [JsonProperty(PropertyName = "status")] // "error" | "ok",
    public string TicketStatus { get; set; }

    [JsonProperty(PropertyName = "id")] public string TicketId { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string TicketMessage { get; set; }

    [JsonProperty(PropertyName = "details")]
    public object TicketDetails { get; set; }
}

[JsonObject(MemberSerialization.OptIn)]
public class PushTicketErrors
{
    [JsonProperty(PropertyName = "code")] public string ErrorCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string ErrorMessage { get; set; }
}

[JsonObject(MemberSerialization.OptIn)]
public class PushReceiptRequest
{
    [JsonProperty(PropertyName = "ids")] public List<string> PushTicketIds { get; set; }
}

[JsonObject(MemberSerialization.OptIn)]
public class PushReceiptResponse
{
    [JsonProperty(PropertyName = "data")]
    public Dictionary<string, PushTicketDeliveryStatus> PushTicketReceipts { get; set; }

    [JsonProperty(PropertyName = "errors")]
    public List<PushReceiptErrorInformation> ErrorInformations { get; set; }
}

[JsonObject(MemberSerialization.OptIn)]
public class PushTicketDeliveryStatus
{
    [JsonProperty(PropertyName = "status")]
    public string DeliveryStatus { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string DeliveryMessage { get; set; }

    [JsonProperty(PropertyName = "details")]
    public object DeliveryDetails { get; set; }
}

[JsonObject(MemberSerialization.OptIn)]
public class PushReceiptErrorInformation
{
    [JsonProperty(PropertyName = "code")] public string ErrorCode { get; set; }

    [JsonProperty(PropertyName = "message")]
    public string ErrorMessage { get; set; }
}