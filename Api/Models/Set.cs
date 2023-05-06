namespace Api.Models;

public class SetDto
{
    public int? SetId { get; set; }
    public string AccessToken { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}