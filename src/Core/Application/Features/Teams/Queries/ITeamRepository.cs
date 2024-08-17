using OpsManagerAPI.Application.Features.Teams.Dtos;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;

namespace OpsManagerAPI.Application.Features.Teams.Queries;
public interface ITeamRepository : ITransientService
{
    Task<(List<Team> Teams, int TotalCount)> GetAll(TeamFilterRequest request, Staff currentStaff, CancellationToken cancellationToken);
    Task<Team?> GetTeamByUserIdAsync(DefaultIdType staffId, CancellationToken cancellationToken);
}
