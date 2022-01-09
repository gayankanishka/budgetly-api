namespace Budgetly.Domain.Common;

public class BaseResponse<T> where T : class
{
    public BaseResponse(IEnumerable<T> data, string message = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
    }

    public BaseResponse(string message)
    {
        Succeeded = false;
        Message = message;
    }
    
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public  List<string>? Errors { get; set; }
    public IEnumerable<T>? Data { get; private set; }
}