using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Constants;
using Microsoft.AspNetCore.Hosting;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;

namespace Business.Helpers;

public interface IGooglePlayValidator
{
    Task<ProductPurchase?> ValidatePurchaseAsync(string packageName, string productId, string purchaseToken);
    Task<GoogleSubscriptionPurchase?> ValidateSubscriptionAsync(string packageName, string purchaseToken);
}

public class GoogleSubscriptionPurchase
{
    public string ProductId { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public bool AutoRenewing { get; set; }
    public bool IsActive { get; set; }
}

public class GooglePlayValidator : IGooglePlayValidator
{
    private readonly AndroidPublisherService _service;

    public GooglePlayValidator(IWebHostEnvironment env)
    {
        var credential = GoogleCredential
            .FromFile(Path.Combine(env.WebRootPath, "google-services.json"))
            .CreateScoped(AndroidPublisherService.Scope.Androidpublisher);

        _service = new AndroidPublisherService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = GlobalConfig.ApplicationName
        });
    }

    public async Task<ProductPurchase?> ValidatePurchaseAsync(string packageName, string productId,
        string purchaseToken)
    {
        var request = _service.Purchases.Products.Get(packageName, productId, purchaseToken);
        var response = await request.ExecuteAsync();
        return response.PurchaseState == 0 && response.ConsumptionState == 0 ? response : null;
    }

    public async Task<GoogleSubscriptionPurchase?> ValidateSubscriptionAsync(string packageName, string purchaseToken)
    {
        var request = _service.Purchases.Subscriptionsv2.Get(packageName, purchaseToken);
        var response = await request.ExecuteAsync();

        var lineItem = response.LineItems?.FirstOrDefault();
        if (lineItem == null)
            return null;

        return new GoogleSubscriptionPurchase
        {
            ProductId = lineItem.ProductId,
            ExpiresAt = lineItem.ExpiryTimeDateTimeOffset?.UtcDateTime,
            AutoRenewing = lineItem.AutoRenewingPlan?.AutoRenewEnabled ?? false,
            IsActive = response.SubscriptionState == "SUBSCRIPTION_STATE_ACTIVE"
        };
    }
}