using Http_Server.models;
using System.Data.SqlClient;

namespace Http_Server.ORM;

public class AccountDAO : IAccountDAO
{
    private readonly string connectionString;

    public AccountDAO(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public IEnumerable<Account> GetAll()
    {
        var result = new List<Account>();

        string sqlExpression = $"SELECT * FROM Accounts";
        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var command = new SqlCommand(sqlExpression, connection);
        using var reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                result.Add(new Account(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2)));
            }
        }

        return result;
    }

    public Account? GetById(int id)
    {
        string sqlExpression = $"SELECT * FROM Accounts WHERE Id = {id}";
        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var command = new SqlCommand(sqlExpression, connection);
        using var reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                if (reader.GetInt32(0) == id)
                    return new Account(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2));
            }
        }

        return null;
    }

    public void Insert(string login, string password)
    {
        string sqlExpression =
            $"INSERT INTO Accounts " +
            $"VALUES('{login}', '{password}')";

        using var connection = new SqlConnection(connectionString);

        connection.Open();

        var command = new SqlCommand(sqlExpression, connection);
        command.ExecuteNonQuery();
    }
}