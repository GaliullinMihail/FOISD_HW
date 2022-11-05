using Http_Server.models;

namespace Http_Server.ORM;

public interface IAccountRepository
{
    public IEnumerable<Account> GetAll();
    public Account? GetById(int id);
    public void Insert(string login, string password);
}