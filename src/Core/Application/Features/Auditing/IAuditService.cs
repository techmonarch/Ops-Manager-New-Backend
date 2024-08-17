namespace OpsManagerAPI.Application.Features.Auditing;

public interface IAuditService : ITransientService
{
    Task<ApiResponse<List<AuditDto>>> GetUserTrailsAsync(DefaultIdType userId);
}