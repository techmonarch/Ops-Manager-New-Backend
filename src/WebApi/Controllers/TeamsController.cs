using OpsManagerAPI.Application.Common.Models;
using OpsManagerAPI.Application.Features.Reconnections.Commands;
using OpsManagerAPI.Application.Features.Teams.Commands;
using OpsManagerAPI.Application.Features.Teams.Dtos;
using OpsManagerAPI.Application.Features.Teams.Queries;
using OpsManagerAPI.Infrastructure.Authorization;
using OpsManagerAPI.WebApi.Controllers.Conventions;

namespace OpsManagerAPI.WebApi.Controllers;

public class TeamsController : VersionNeutralApiController
{
    private readonly ITeamQueries _teamService;
    public TeamsController(ITeamQueries TeamService)
       => _teamService = TeamService;

    /// <summary>
    /// Post a new Team.
    /// </summary>
    /// <param name="request">The Team details.</param>
    /// <returns>The ID of the created Team.</returns>
    [HttpPost]
    [MustHavePermission(OPSAction.Create, OPSResource.Teams)]
    [OpenApiOperation("Post a new Team.", "Creates a new Team with the provided details.")]
    [ApiConventionMethod(typeof(OPSApiConventions), nameof(OPSApiConventions.Register))]
    public Task<ApiResponse<DefaultIdType>> CreateAsync(CreateTeamCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// ChangeTeamStatus a Team by ID.
    /// </summary>
    /// <param name="teamId">The ID of the Team.</param>
    /// <returns>The ID of the Team.</returns>
    [HttpPut("{teamId}/change-team-status")]
    [MustHavePermission(OPSAction.Update, OPSResource.Teams)]
    [OpenApiOperation("Change Team Status Team by ID.", "Change Team Status using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> ChangeTeamStatusAsync(DefaultIdType teamId)
    {
        return Mediator.Send(new ChangeStatusCommand { TeamId = teamId });
    }

    /// <summary>
    /// Add Member to a Team by ID.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The ID of the Team.</returns>
    [HttpPut("add-member")]
    [MustHavePermission(OPSAction.Update, OPSResource.Teams)]
    [OpenApiOperation("Add Member to Team by ID.", "Add Member to a Team using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> AddMemberAsync(AddMemberCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Remove Member to a Team by ID.
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The ID of the Team.</returns>
    [HttpPut("remove-member")]
    [MustHavePermission(OPSAction.Update, OPSResource.Teams)]
    [OpenApiOperation("Remove Member to Team by ID.", "Remove Member to a Team using their unique identifier.")]
    public Task<ApiResponse<DefaultIdType>> RemoveMemberAsync(RemoveMemberCommand request)
    {
        return Mediator.Send(request);
    }

    /// <summary>
    /// Get all Team.
    /// </summary>
    /// <param name="request">The pagination filter request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A paginated list of all Team.</returns>
    [HttpGet]
    [MustHavePermission(OPSAction.View, OPSResource.Teams)]
    [OpenApiOperation("Get all Team.", "Retrieve a paginated list of Team based on filter criteria.")]
    public Task<ApiResponse<PaginationResponse<TeamDetailsDto>>> GetTeamsAsync([FromQuery] TeamFilterRequest request, CancellationToken cancellationToken)
    {
        return _teamService.GetAll(request, cancellationToken);
    }

    [HttpPost("assign-reconnection-tasks")]
    [MustHavePermission(OPSAction.Create, OPSResource.DownloadManagers)]
    [OpenApiOperation("Assign reconnection tasks to a team.", "Assigns a list of reconnection task IDs to a specified team.")]
    public Task<ApiResponse<DefaultIdType>> AssignReconnectionTasks(AssignReconnectionTasksCommand request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("assign-disconnection-tasks")]
    [MustHavePermission(OPSAction.Create, OPSResource.DownloadManagers)]
    [OpenApiOperation("Assign disconnection tasks to a team.", "Assigns a list of disconnection task IDs to a specified team.")]
    public Task<ApiResponse<DefaultIdType>> AssignDisconnectionTasks(AssignDisconnectionTasksCommand request)
    {
        return Mediator.Send(request);
    }
}