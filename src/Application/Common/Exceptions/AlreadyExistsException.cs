namespace Budgetly.Application.Common.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException()
    {
    }

    public AlreadyExistsException(string message)
        : base(message)
    {
    }

    public AlreadyExistsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public AlreadyExistsException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was already exists.")
    {
    }
}