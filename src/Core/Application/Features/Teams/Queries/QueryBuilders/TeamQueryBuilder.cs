using OpsManagerAPI.Application.Features.Teams.Dtos;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using System.Linq.Expressions;

namespace OpsManagerAPI.Application.Features.Teams.Queries.QueryBuilders;

public class TeamQueryBuilder
{
    public static Expression<Func<Team, bool>> BuildFilter(TeamFilterRequest request, Staff currentStaff)
    {
        return team =>
            (!request.Id.HasValue || team.Id == request.Id) &&
            team.IsActive &&
            (team.Office.Id == currentStaff.OfficeId && team.Office.OfficeLevelId == currentStaff.Office.OfficeLevelId) &&
            (request.Name == null || (team.Name != null && team.Name.Contains(request.Name)));
    }
}
