namespace OpsManagerAPI.Application.Common.Models;
public record ApiResponse<T>(bool Status, string Message, T Data, string ErrorMessage = default!);