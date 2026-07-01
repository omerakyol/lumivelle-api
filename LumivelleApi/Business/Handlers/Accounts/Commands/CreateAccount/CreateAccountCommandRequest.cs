using Core.Enums;
using Core.Utilities.Results;
using MediatR;

namespace Business.Handlers.Accounts.Commands.CreateAccount;

public class CreateAccountCommandRequest : IRequest<IDataResult<CreateAccountCommandResult>>
{
    public string? LicenseCode { get; set; }
    public AccountType? AccountType { get; set; }
    public string? TransactionId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}