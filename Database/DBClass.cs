namespace Database;

using MySql.Data.MySqlClient;

public class DBClass
{
    public static MySqlConnection GetDBConnection()
    {
        string host = "localhost"; // TODO : To make it possible to get from appsettings.json (use dict-set-db for deployment)
        int port = 3306;
        string database = "dict_set";
        string username = "DictSetUserDB";
        string password = "1234";

        return DBUtils.GetDBConnection(host, port, database, username, password);
    }
}