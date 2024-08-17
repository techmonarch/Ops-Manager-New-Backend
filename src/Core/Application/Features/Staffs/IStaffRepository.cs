using OpsManagerAPI.Domain.Aggregates.StaffAggregate;

namespace OpsManagerAPI.Application.Features.Staffs;
public interface IStaffRepository : ITransientService
{
    Task<List<Staff>> GetByUserIdsAsync(List<DefaultIdType> userIds, CancellationToken cancellationToken);
    Task<Staff> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
