﻿namespace Database;

using MySql.Data.MySqlClient;

class DBUtils
{
    public static MySqlConnection
        GetDBConnection(string host, int port, string database, string username, string password)
    {
        // Connection String.
        String connString = "Server=" + host + ";Database=" + database
                            + ";port=" + port + ";User Id=" + username + ";password=" + password;

        MySqlConnection conn = new MySqlConnection(connString);

        return conn;
    }

    public static bool CheckUserEmailExists(string email)
    {
        MySqlConnection connection = DBClass.GetDBConnection();
        connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM users WHERE email = @email", connection);
        command.Parameters.AddWithValue("@email", email);
        long count = (long)command.ExecuteScalar();
        return count != 0;
    }
}