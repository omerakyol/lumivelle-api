using System;

namespace Core.Helpers;

public static class GenerateQrCodeHelper
{
    private const string BaseUrl = "https://api.qrserver.com/v1/create-qr-code/";
    private const string Size = "300x300";

    public static string Generate(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
            throw new ArgumentException("Data cannot be null or empty.", nameof(data));

        var encodedEmail = Uri.EscapeDataString(data);
        var qrCodeUrl = $"{BaseUrl}?size={Size}&data={encodedEmail}";

        return qrCodeUrl;
    }
}