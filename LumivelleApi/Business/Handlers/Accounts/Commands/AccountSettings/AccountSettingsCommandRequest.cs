using Core.Entities.Dtos.Account;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.AccountSettings;

public class AccountSettingsCommandRequest : IRequest<IDataResult<AccountDto>>
{
    public bool VoiceTransformer { get; set; }
    public bool EnableScreenShots { get; set; }
    public bool EnableTor { get; set; }
    public bool EnablePushNotifications { get; set; }
    public bool EnableReadReceipt { get; set; }
    public int AutoDeleteMessagesHours { get; set; }
    public int AskSpacePassword { get; set; }
    public bool EnableOnlineStatus { get; set; }
    public bool EnablePrivateNotes { get; set; }
    public bool DeleteButton { get; set; }
}