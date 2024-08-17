namespace OpsManagerAPI.Application.Features.Auditing;

public record GetMyAuditLogsRequest : IRequest<ApiResponse<List<AuditDto>>>;

public class GetMyAuditLogsRequestHandler : IRequestHandler<GetMyAuditLogsRequest, ApiResponse<List<AuditDto>>>
{
    private readonly ICurrentUser _currentUser;
    private readonly IAuditService _auditService;

    public GetMyAuditLogsRequestHandler(ICurrentUser currentUser, IAuditService auditService) =>
        (_currentUser, _auditService) = (currentUser, auditService);

    public async Task<ApiResponse<List<AuditDto>>> Handle(GetMyAuditLogsRequest request, CancellationToken cancellationToken) =>
       await _auditService.GetUserTrailsAsync(_currentUser.GetUserId());
}