namespace Budgetly.Domain.Common;

public class BaseResponse
{
    public bool Success { get; set; }
    // public T Data<T>  { get; set; }
    public string Error { get; set; }
    public string Message { get; set; }
}