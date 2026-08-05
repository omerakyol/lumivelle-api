namespace Core.Constants;

public static partial class Messages
{
    public static string PasswordError => "PasswordError";
    public static string SuccessfulLogin => "SuccessfulLogin";

    // Generic credential failure used for both "no such account" and "wrong
    // password" so login responses cannot be used to enumerate valid usernames.
    public static string InvalidCredentials => "InvalidCredentials";

    public static string SocialTokenEmpty => "SocialTokenEmpty";
    public static string SocialTokenInvalid => "SocialTokenInvalid";
}