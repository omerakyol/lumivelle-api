using QRCoder;

namespace Business.Helpers;

public static class QrCodeHelper
{
    public static string GenerateQrCodeBase64(string otpAuthUrl)
    {
        using var qrGenerator = new QRCodeGenerator();
        using var qrData = qrGenerator.CreateQrCode(otpAuthUrl, QRCodeGenerator.ECCLevel.Q);
        using var qrCode = new Base64QRCode(qrData);
        var qrCodeImage = qrCode.GetGraphic(20);
        return $"data:image/png;base64,{qrCodeImage}";
    }
}