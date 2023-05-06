namespace Api.Models;

public class WordsDto
{
    public string AccessToken { get; set; }
    public int SetId { get; set; }
    public List<Words> WordsForAdd { get; set; }
}

public class Words
{
    public string Word { get; set; }
    public string Translate { get; set; }
    public string? Description { get; set; }
}