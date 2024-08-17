namespace OpsManagerAPI.Infrastructure.Middleware;

public class ErrorResult
{
    public List<string> Messages { get; set; } = new();

    public string? Source { get; set; }
    public string? Exception { get; set; }
    public string? ErrorId { get; set; }
    public string? SupportMessage { get; set; }
    public int StatusCode { get; set; }
}

public class ApiErrorResponse
{
    public string? Message { get; set; }
    public object? Data { get; set; }
    public bool Status { get; set; } = false;
}