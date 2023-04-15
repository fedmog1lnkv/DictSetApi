using MySql.Data.MySqlClient;

namespace Database;

public class TokenDatabase : DBClass
{
    public bool AddToken(string email, string token)
    {
        if (DBUtils.CheckTokenExists(token))
        {
            // A token exists
            return false;
        }

        int user_id = UsersDatabase.GetUserIdByEmail(email);
        string sql = $"INSERT INTO tokens (user_id, token) values ({user_id}, '{token}');";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();

        // Token created
        return true;
    }

    public string GetTokenByEmail(string email)
    {
        int user_id = UsersDatabase.GetUserIdByEmail(email);
        string sql = $"SELECT token FROM tokens WHERE user_id = {user_id}";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        var tokenFromDb = cmd.ExecuteScalar();
        connection.Close();

        if (tokenFromDb != null)
        {
            string token = tokenFromDb.ToString();

            return token;
        }

        return "";
    }

    public string GetEmailByToken(string token)
    {
        string sql = $"SELECT email FROM users WHERE user_id=(SELECT user_id FROM tokens WHERE token='{token}')";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        var emailFromDb = cmd.ExecuteScalar();
        connection.Close();

        if (emailFromDb != null)
        {
            var email = emailFromDb.ToString();

            Console.WriteLine(email);
            return email;
        }

        return "";
    }

    public void DeleteToken(string email)
    {
        int user_id = UsersDatabase.GetUserIdByEmail(email);
        string sql = $"DELETE FROM tokens WHERE user_id = {user_id}";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();
    }
}