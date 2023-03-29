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

        UsersDatabase UDB = new UsersDatabase();
        int user_id = UDB.GetUserId(email);
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
        UsersDatabase UDB = new UsersDatabase();
        int user_id = UDB.GetUserId(email);
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

    public void DeleteToken(string email)
    {
        UsersDatabase UDB = new UsersDatabase();
        int user_id = UDB.GetUserId(email);
        string sql = $"DELETE FROM tokens WHERE user_id = {user_id}";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();
    }
}