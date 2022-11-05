using Http_Server.Attributes;
using Http_Server.models;
using Microsoft.AspNetCore.Mvc;

namespace Http_Server.Controllers;

[HttpController("accounts")]
public class AccountController
{
    [HttpGet("/accounts")]
    public List<Account> GetAccounts()
    {
        return null;
    }

    [HttpGet("/accounts/`")]
    public Account? GetAccountById(int id)
    {
        return null;
    }

    [HttpPost("/account")]
    public void SaveAccount(string query)
    {
    }
}
