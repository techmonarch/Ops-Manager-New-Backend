using Microsoft.EntityFrameworkCore;
using Ardalis.Specification.EntityFrameworkCore;
using OpsManagerAPI.Application.Features.Teams.Dtos;
using OpsManagerAPI.Application.Common.Specification;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using OpsManagerAPI.Application.Features.Teams.Queries;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Application.Features.Teams.Queries.QueryBuilders;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository.EfCoreRepositories;
public class TeamRepository : ITeamRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TeamRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<(List<Team> Teams, int TotalCount)> GetAll(TeamFilterRequest request, Staff currentStaff, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<Team>(request);
        var filter = TeamQueryBuilder.BuildFilter(request, currentStaff);

        int totalCount = await _dbContext.Teams
                                    .AsNoTracking().Where(filter).CountAsync(cancellationToken);

        var teams = await _dbContext.Teams
                                    .AsNoTracking()
                                    .Include(t => t.StaffTeams)
                                    .ThenInclude(st => st.Staff)
                                    .WithSpecification(spec)
                                    .Where(filter)
                                    .ToListAsync(cancellationToken);

        return (teams, totalCount);
    }

    public async Task<Team?> GetTeamByUserIdAsync(DefaultIdType userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Team>()
            .Include(t => t.StaffTeams)
            .ThenInclude(st => st.Staff)
            .FirstOrDefaultAsync(t => t.StaffTeams.Any(m => m.Staff.ApplicationUserId == userId), cancellationToken);
    }
}