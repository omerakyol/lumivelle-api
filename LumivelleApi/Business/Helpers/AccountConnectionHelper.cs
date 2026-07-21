using System.Collections.Generic;
using System.Linq;
using Core.Entities.Dtos.Hub;

namespace Business.Helpers;

public static class AccountConnectionHelper
{
    private static readonly List<ConnectedAccountDto> Accounts = [];

    public static void AddAccount(string connectionId, string email)
    {
        var oldAccounts = Accounts.Where(x => x.Email == email).ToList();
        if (oldAccounts.Count > 0)
            foreach (var oldAccount in oldAccounts)
                Accounts.Remove(oldAccount);

        Accounts.Add(new ConnectedAccountDto
        {
            ConnectionId = connectionId,
            Email = email
        });
    }

    public static void RemoveAccount(string connectionId)
    {
        var account = Accounts.FirstOrDefault(u => u.ConnectionId == connectionId);
        if (account != null)
            Accounts.Remove(account);
    }

    public static ConnectedAccountDto GetAccountByConnectionId(string connectionId)
    {
        return Accounts.FirstOrDefault(u => u.ConnectionId == connectionId);
    }

    public static ConnectedAccountDto GetAccountByUsername(string email)
    {
        return Accounts.FirstOrDefault(u => u.Email == email);
    }

    public static List<ConnectedAccountDto> GetAllAccounts()
    {
        return Accounts.ToList();
    }

    public static int GetAccountsCount()
    {
        return Accounts.Count;
    }
}