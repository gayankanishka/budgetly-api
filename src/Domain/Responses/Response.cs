namespace Budgetly.Domain.Responses;

public class Response<T> where T : class
{
    public Response(IEnumerable<T> data, string message = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
    }

    public Response(string message)
    {
        Succeeded = false;
        Message = message;
    }
    
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public  List<string>? Errors { get; set; }
    public IEnumerable<T>? Data { get; private set; }
}