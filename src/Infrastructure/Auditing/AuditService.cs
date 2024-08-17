using Mapster;
using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Auditing;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Auditing;
public class AuditService : IAuditService
{
    private readonly ApplicationDbContext _context;

    public AuditService(ApplicationDbContext context) => _context = context;

    public async Task<ApiResponse<List<AuditDto>>> GetUserTrailsAsync(Guid userId)
    {
        var trails = await _context.AuditTrails
            .Where(a => a.UserId == userId)
        .OrderByDescending(a => a.DateTime)
        .Take(250)
        .ToListAsync();

        return new ApiResponse<List<AuditDto>>(true, "User Audits retrievd Successfully", trails.Adapt<List<AuditDto>>());
    }
}