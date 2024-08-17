using Microsoft.EntityFrameworkCore;
using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class StaffRepository : IStaffRepository
{
    private readonly ApplicationDbContext _context;

    public StaffRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Staff> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        Staff? staff = await _context.Staffs
                                    .AsNoTracking()
                                    .Include(x => x.Office)
                                    .FirstOrDefaultAsync(st => st.ApplicationUserId == userId, cancellationToken);
        return staff!;
    }

    public async Task<List<Staff>> GetByUserIdsAsync(List<Guid> userIds, CancellationToken cancellationToken)
     => await _context.Staffs
                            .Where(staff => userIds.Contains(staff.ApplicationUserId))
                            .ToListAsync(cancellationToken);

    public async Task<List<Staff>> GetStaffsByIdsAsync(List<DefaultIdType> staffIds, CancellationToken cancellationToken)
      => await _context.Staffs
          .Where(r => staffIds.Contains(r.Id))
          .ToListAsync(cancellationToken);
}
