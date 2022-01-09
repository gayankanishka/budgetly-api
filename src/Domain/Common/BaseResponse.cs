namespace Budgetly.Domain.Common;

public class BaseResponse<T> where T : class
{
    public BaseResponse(IEnumerable<T> data)
    {
        Data = data;
        Success = true;
    }

    public BaseResponse(string error)
    {
        Error = error;
        Success = false;
    }
    
    public bool Success { get; set; }
    public IEnumerable<T>? Data { get; private set; }
    public string? Error { get; set; }
}