using OpsManagerAPI.Application.Features.Teams.Dtos;

namespace OpsManagerAPI.Application.Features.Teams.Queries;
public interface ITeamQueries : ITransientService
{
    Task<ApiResponse<PaginationResponse<TeamDetailsDto>>> GetAll(TeamFilterRequest request, CancellationToken cancellationToken);
}
