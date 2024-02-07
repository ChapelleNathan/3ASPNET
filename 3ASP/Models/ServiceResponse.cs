namespace _3ASP.Models;

public class ServiceResponse<T>
{
    public T? Data { get; set; }

    public bool Success { get; private set; } = true;

    public string Message { get; private set; } = string.Empty;
}