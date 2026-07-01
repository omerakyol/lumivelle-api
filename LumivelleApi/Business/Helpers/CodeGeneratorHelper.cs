using System.Security.Cryptography;

namespace Business.Helpers;

public static class CodeGeneratorHelper
{
    public static string GenerateCode(int length = 16)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var data = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(data);

        var result = new char[length];
        for (var i = 0; i < length; i++) result[i] = chars[data[i] % chars.Length];

        return new string(result);
    }
}