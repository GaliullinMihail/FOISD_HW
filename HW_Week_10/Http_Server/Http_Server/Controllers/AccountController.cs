using Http_Server.Attributes;
using Http_Server.models;
using Http_Server.ORM;
using Microsoft.AspNetCore.Mvc;

namespace Http_Server.Controllers;

[HttpController("/accounts$")]
public class AccountController  
{
    [HttpGet("/accounts$")]
    public List<Account> GetAccounts()
    {
        var repository = new AccountRepository(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True");
        return repository.GetAll().ToList();
    }

    [HttpGet("/accounts/[1-9][0-9]*$")]
    public Account? GetAccountById(int id)
    {
        var repository = new AccountRepository(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True");
        return repository.GetById(id);
    }

    [HttpPost("/accounts$")]
    public void SaveAccount(string query)
    {
        var queryParams = query.Split('&')
            .Select(pair => pair.Split('='))
            .Select(pair => pair[1])
            .ToArray();

        var repository = new AccountRepository(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True");
        repository.Insert(queryParams[0], queryParams[1]);
    }
}