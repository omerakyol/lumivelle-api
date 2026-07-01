namespace Business.Handlers.Accounts.Commands.SetupTwoFactor;

public class SetupTwoFactorCommandResult
{
    public string QrCode { get; set; }
    public string? SecretKey { get; set; }
}