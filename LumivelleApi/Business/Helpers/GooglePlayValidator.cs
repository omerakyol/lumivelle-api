using System.IO;
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
}