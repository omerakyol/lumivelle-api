using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Core.Entities.Dtos.Hub;

namespace Business.Helpers;

public static class AccountConnectionHelper
{
    private static readonly List<ConnectedAccountDto> Accounts = [];
     
    public static void AddAccount(string connectionId, string username)
    {
        var oldAccounts = Accounts.Where(x => x.Username == username).ToList();
        if (oldAccounts.Count > 0)
            foreach (var oldAccount in oldAccounts)
                Accounts.Remove(oldAccount);

        Accounts.Add(new ConnectedAccountDto
        {
            ConnectionId = connectionId,
            Username = username
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

    public static ConnectedAccountDto GetAccountByUsername(string username)
    {
        return Accounts.FirstOrDefault(u => u.Username == username);
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