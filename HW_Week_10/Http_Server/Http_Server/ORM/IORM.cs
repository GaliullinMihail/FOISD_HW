using System.Data.SqlClient;

namespace Http_Server.ORM;

public interface IORM
{
    public List<T> Select<T>();
    public T? Select<T>(int id);
    public void Insert<T>(params string[] args);
    public void Delete<T>();
    public void Delete<T>(int id);
    public void Update<T>(int id, string pastValue, string newValue);
    
}