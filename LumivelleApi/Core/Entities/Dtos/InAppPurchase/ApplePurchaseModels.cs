using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Entities.Dtos.InAppPurchase;

public class AppleVerifyResponse
{
    [JsonProperty("status")] public int Status { get; set; }

    [JsonProperty("receipt")] public AppleReceipt Receipt { get; set; }
}

public class AppleReceipt
{
    [JsonProperty("bundle_id")] public string BundleId { get; set; }

    [JsonProperty("in_app")] public List<AppleInAppPurchase> InApp { get; set; } = [];
}

public class AppleInAppPurchase
{
    [JsonProperty("product_id")] public string ProductId { get; set; }

    [JsonProperty("transaction_id")] public string TransactionId { get; set; }

    [JsonProperty("original_transaction_id")]
    public string OriginalTransactionId { get; set; }

    [JsonProperty("purchase_date_ms")] public string PurchaseDateMs { get; set; }

    [JsonProperty("expires_date_ms")] public string ExpiresDateMs { get; set; }
}