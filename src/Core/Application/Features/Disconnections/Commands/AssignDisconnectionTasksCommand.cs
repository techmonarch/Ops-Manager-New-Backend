using OpsManagerAPI.Application.Features.Disconnections.Queries;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Reconnections.Commands;

public class AssignDisconnectionTasksCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public Guid TeamId { get; set; }
    public List<Guid> DisconnectionTaskIds { get; set; } = new List<Guid>();
}

public class AssignDisconnectionTasksCommandHandler : IRequestHandler<AssignDisconnectionTasksCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepository<Team> _teamRepository;
    private readonly IDisconnectionRepository _disconnectionRepository;

    public AssignDisconnectionTasksCommandHandler(IRepository<Team> teamRepository, IDisconnectionRepository disconnection)
    {
        _teamRepository = teamRepository;
        _disconnectionRepository = disconnection;
    }

    public async Task<ApiResponse<DefaultIdType>> Handle(AssignDisconnectionTasksCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetByIdAsync(request.TeamId, cancellationToken)
                   ?? throw new NotFoundException("Team not found");

        // Get disconnection results
        var assignmentResults = await UpdateDisconnectionTasksToAssignedAsync(request.DisconnectionTaskIds, cancellationToken);

        // Filter successful assignments
        var successfullyAssignedIds = assignmentResults
            .Where(result => result.Success)
            .Select(result => result.DisconnectionId)
            .ToList();

        // Assign only successfully assigned disconnections to the team
        team.AssignDisconnectionTasks(successfullyAssignedIds);
        await _teamRepository.UpdateAsync(team, cancellationToken);

        if (assignmentResults.Any(result => !result.Success))
        {
            return new ApiResponse<DefaultIdType>(
                true,
                "Some disconnections could not be assigned. Check the errors for details.",
                default,
                string.Join(",", assignmentResults.Select(x => x.ErrorMessage)));
        }

        return new ApiResponse<DefaultIdType>(false, "Disconnections successfully assigned to Team", request.TeamId);
    }

    private async Task<List<AssignmentResult>> UpdateDisconnectionTasksToAssignedAsync(List<Guid> disconnectionTaskIds, CancellationToken cancellationToken)
    {
        var assignmentResults = new List<AssignmentResult>();
        var disconnections = await _disconnectionRepository.GetDisconnectionsByIdsAsync(disconnectionTaskIds, cancellationToken);

        foreach (var disconnection in disconnections)
        {
            try
            {
                disconnection.Assign();
                assignmentResults.Add(new AssignmentResult
                {
                    Success = true,
                    DisconnectionId = disconnection.Id
                });
            }
            catch (Exception ex)
            {
                assignmentResults.Add(new AssignmentResult { Success = false, ErrorMessage = ex.Message });
            }
        }

        return assignmentResults;
    }

    private class AssignmentResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public Guid DisconnectionId { get; set; }
    }
}