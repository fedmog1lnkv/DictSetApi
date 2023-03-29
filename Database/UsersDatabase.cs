using MySql.Data.MySqlClient;

namespace Database;

public class UsersDatabase : DBClass
{
    
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

    public int GetUserId(string email)
    {
        string sql = $"SELECT user_id FROM users WHERE email = '{email}'";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        int user_id = Int32.Parse(cmd.ExecuteScalar().ToString());
        connection.Close();

        return user_id;
    }
}