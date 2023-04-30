namespace Api.Classes;

public class Response
{
    public bool Success { get; set; }
    public object? Data { get; set; }
    public string? ErrorMessage { get; set; }

    public Response(bool success, object? data, string? errorMessage)
    {
        Success = success;
        Data = data;
        ErrorMessage = errorMessage;
    }
}