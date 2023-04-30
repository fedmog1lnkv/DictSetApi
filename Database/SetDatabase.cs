using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Database;

public class Set
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class SetDatabase : DBClass
{
    public bool AddSet(int userId, string name, string description)
    {
        if (DBUtils.CheckSetExists(userId, name))
        {
            // A set exists
            return false;
        }

        string sql = $"INSERT INTO sets (user_id, name, description) values ({userId}, '{name}', '{description}');";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();

        // Set created
        return true;
    }

    public static Set GetSet(int userId, int setId)
    {
        string sql = $"SELECT * FROM sets WHERE user_id = {userId} AND set_id = {setId} LIMIT 1;";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        MySqlDataReader reader = cmd.ExecuteReader();
        Set resultingSet = new Set();
        while (reader.Read())
        {
            resultingSet.Id = Convert.ToInt32(reader["set_id"]);
            resultingSet.UserId = Convert.ToInt32(reader["user_id"]);
            resultingSet.Name = reader["name"].ToString() ?? string.Empty;
            resultingSet.Description = reader["description"].ToString() ?? string.Empty;
        }

        connection.Close();

        return resultingSet;
    }

    public static List<Set> GetAllSets(int userId)
    {
        string sql = $"SELECT * FROM sets WHERE user_id = {userId};";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        MySqlDataReader reader = cmd.ExecuteReader();
        
        List<Set> allSets = new List<Set>();
        while (reader.Read())
        {
            Set tempSet = new Set();
            tempSet.Id = Convert.ToInt32(reader["set_id"]);
            tempSet.UserId = Convert.ToInt32(reader["user_id"]);
            tempSet.Name = reader["name"].ToString()!;
            tempSet.Description = reader["description"].ToString()!;
            allSets.Add(tempSet);
        }

        connection.Close();

        return allSets;
    }

    public void DeleteSet(int userId, string name)
    {
        string sql = $"DELETE FROM sets WHERE user_id = {userId} AND name = '{name}'";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();
    }
}