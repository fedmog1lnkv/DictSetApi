namespace Database;

using MySql.Data.MySqlClient;

public class DBClass
{
    public static MySqlConnection GetDBConnection()
    {
        string host = "localhost";
        int port = 3306;
        string database = "time_tracker";
        string username = "TimeTrackerUser";
        string password = "1234";

        return DBUtils.GetDBConnection(host, port, database, username, password);
    }
}