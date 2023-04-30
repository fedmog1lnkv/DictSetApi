using MySql.Data.MySqlClient;

namespace Database;

public class Words
{
    public int Id { get; set; }
    public int SetId { get; set; }
    public string Word { get; set; }
    public string Translate { get; set; }
}

public class WordsDatabase : DBClass
{
    public void AddWord(int setId, string word, string translate)
    {
        string sql = $"INSERT INTO words (set_id, word, translate) values ({setId}, '{word}', '{translate}');";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();
    }
    
    public void DeleteWord(int setId, string word)
    {
        string sql = $"DELETE FROM words WHERE set_id = {setId} AND word = '{word}'";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
        connection.Close();
    }
    
    public static List<Words> GetSetWords(int setId)
    {
        string sql = $"SELECT * FROM words WHERE set_id = {setId};";

        MySqlConnection connection = GetDBConnection();
        connection.Open();
        MySqlCommand cmd = connection.CreateCommand();
        cmd.CommandText = sql;
        MySqlDataReader reader = cmd.ExecuteReader();

        List<Words> result = new List<Words>();
        while (reader.Read())
        {
            Console.WriteLine(reader);
            Words tempWord = new Words();
            tempWord.Id = Convert.ToInt32(reader["word_id"]);
            tempWord.SetId = Convert.ToInt32(reader["set_id"]);
            tempWord.Word = reader["word"].ToString() ?? string.Empty;
            tempWord.Translate = reader["translate"].ToString() ?? string.Empty;
            result.Add(tempWord);
        }
        connection.Close();

        return result;
    }
}