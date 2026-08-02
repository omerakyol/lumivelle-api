using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace Business.Helpers;

public interface IApplePurchaseValidator
{
    Task<AppleInAppPurchase?> ValidateAsync(string jwtToken);
}

public class ApplePurchaseValidator(IConfiguration configuration) : IApplePurchaseValidator
{
     
    private readonly HttpClient _httpClient = new();
    private readonly string _issuerId = configuration["InAppPurchaseSettings:AppleIssuerId"];
    private readonly string _keyId = configuration["InAppPurchaseSettings:AppleKeyId"];
    private readonly string _bundleId = configuration["InAppPurchaseSettings:AppleBundleId"];
    private readonly string _privateKey = configuration["InAppPurchaseSettings:ApplePrivateKey"]?.Replace("\\n", "\n");
    private readonly string _sandBoxUrl = configuration["InAppPurchaseSettings:AppleSandboxUrl"];
    private readonly string _productionUrl = configuration["InAppPurchaseSettings:AppleProductionUrl"];

    public async Task<AppleInAppPurchase?> ValidateAsync(string jwtToken)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwtToken) as JwtSecurityToken;

            var payload = jsonToken?.Payload;
            var transactionId = payload?["transactionId"]?.ToString();
            var environment = payload?["environment"]?.ToString();

            var isValid = await VerifyWithAppStoreAsync(transactionId, environment);

            if (!isValid)
                return null;

            var purchase = new AppleInAppPurchase
            {
                TransactionId = transactionId,
                OriginalTransactionId = payload["originalTransactionId"]?.ToString(),
                ProductId = payload["productId"]?.ToString(),
                PurchaseDate = payload["purchaseDate"] != null
                    ? DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(payload["purchaseDate"].ToString())).DateTime
                    : DateTime.MinValue,
                ExpiresAt = payload["expiresDate"] != null
                    ? DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(payload["expiresDate"].ToString())).DateTime
                    : null,
                Quantity = payload["quantity"] != null ? int.Parse(payload["quantity"].ToString()) : 0,
                Type = payload["type"]?.ToString(),
                Environment = environment,
                BundleId = payload["bundleId"]?.ToString(),
                TransactionReason = payload["transactionReason"]?.ToString()
            };

            return purchase;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JWT validation error: {ex.Message}");
            throw;
        }
    }

    private async Task<bool> VerifyWithAppStoreAsync(string transactionId, string environment)
    {
        try
        {
            // JWT token oluştur (App Store API için)
            var authToken = GenerateAppStoreAuthToken();

            // API URL (sandbox veya production)
            var baseUrl = environment?.ToLower() == "sandbox"
                ? _sandBoxUrl
                : _productionUrl;

            var url = $"{baseUrl}/inApps/v1/transactions/{transactionId}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"App Store API error: {response.StatusCode} - {errorContent}");
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AppStoreTransactionResponse>(content);

            return result?.SignedTransactionInfo != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"App Store verification error: {ex.Message}");
            return false;
        }
    }

    private string GenerateAppStoreAuthToken()
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var payload = new JwtPayload
        {
            { "iss", _issuerId },
            { "iat", now },
            { "exp", now + 3600 }, 
            { "aud", "appstoreconnect-v1" },
            { "bid", _bundleId }
        };

        var securityKey = GetEcDsaSecurityKey();
 
        var signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.EcdsaSha256
        );
 
        var header = new JwtHeader(signingCredentials);
 
        if (!header.ContainsKey("kid"))
            header.Add("kid", _keyId);

        var token = new JwtSecurityToken(header, payload);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private ECDsaSecurityKey GetEcDsaSecurityKey()
    {
        try
        {
            using var reader = new StringReader(_privateKey);
            var pemReader = new PemReader(reader);
            var keyObject = pemReader.ReadObject();

            ECPrivateKeyParameters privateKey;

            if (keyObject is AsymmetricCipherKeyPair keyPair)
            {
                privateKey = keyPair.Private as ECPrivateKeyParameters;
            }
            else if (keyObject is ECPrivateKeyParameters ecKey)
            {
                privateKey = ecKey;
            }
            else
            {
                throw new Exception($"Unexpected key type: {keyObject?.GetType().Name}");
            }

            if (privateKey == null)
            {
                throw new Exception("Could not extract EC private key");
            }

            var q = privateKey.Parameters.G.Multiply(privateKey.D).Normalize();

            var ecParams = new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,
                D = privateKey.D.ToByteArrayUnsigned(),
                Q = new ECPoint
                {
                    X = q.XCoord.GetEncoded(),
                    Y = q.YCoord.GetEncoded()
                }
            };

            var ecdsa = ECDsa.Create(ecParams);
            return new ECDsaSecurityKey(ecdsa);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading private key: {ex.Message}");
            throw;
        }
    }
}

public class AppStoreTransactionResponse
{
    [JsonProperty("signedTransactionInfo")] public string SignedTransactionInfo { get; set; }
}

public class AppleInAppPurchase
{
    public string TransactionId { get; set; }
    public string OriginalTransactionId { get; set; }
    public string ProductId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public int Quantity { get; set; }
    public string Type { get; set; }
    public string Environment { get; set; }
    public string BundleId { get; set; }
    public string TransactionReason { get; set; }
}