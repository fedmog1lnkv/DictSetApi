namespace Database;

using MySql.Data.MySqlClient;

public class DBClass
{
    public static MySqlConnection GetDBConnection()
    {
        string host = "localhost";
        int port = 3306;
        string database = "time_tracker";
        string username = "root";
        string password = "FEDYAFEDOR9";

        return DBUtils.GetDBConnection(host, port, database, username, password);
    }

    public bool Register(string username, string email, string password)
    {
        if (DBUtils.CheckUserEmailExists(email))
        {
            // A user with this name exists
            return false;
        }

        string sql = $"INSERT INTO users (username, email, password) values ('{username}', '{email}', '{password}');";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();

        // User created
        return true;
    }

    public bool DeleteUser(string email)
    {
        string sql = $"DELETE FROM users WHERE email = '{email}' LIMIT 1;";

        if (!DBUtils.CheckUserEmailExists(email))
        {
            // A user with this name not exists
            return false;
        }

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();

        // User deleted
        return true;
    }

    public bool Authorization(string email, string password)
    {
        string sql = $"SELECT COUNT(*) FROM users WHERE email = '{email}' AND password = '{password}'";
        
        MySqlConnection connection = DBClass.GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        long count = (long)cmd.ExecuteScalar();
        Console.WriteLine(count);
        return count == 1;
    }
}