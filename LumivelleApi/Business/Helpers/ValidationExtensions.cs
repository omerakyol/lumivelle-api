using System.Text.RegularExpressions;

namespace Business.Helpers;

public static class ValidationExtensions
{
    public static bool IsPhoneValid(this string mobilePhone)
    {
        if (string.IsNullOrWhiteSpace(mobilePhone)) return false;

        mobilePhone = Regex.Replace(mobilePhone, "[^0-9]", string.Empty);
        return mobilePhone.StartsWith("05") && mobilePhone.Length == 11;
    }
}