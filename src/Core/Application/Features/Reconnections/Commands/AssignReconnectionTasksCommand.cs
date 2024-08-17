using OpsManagerAPI.Application.Features.Downloads.Queries;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Reconnections.Commands;

public class AssignReconnectionTasksCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public Guid TeamId { get; set; }
    public List<Guid> ReconnectionTaskIds { get; set; } = new List<Guid>();
}

public class AssignReconnectionTasksCommandHandler : IRequestHandler<AssignReconnectionTasksCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepository<Team> _teamRepository;
    private readonly IReconnectionRepository _reconnectionRepository;

    public AssignReconnectionTasksCommandHandler(IRepository<Team> teamRepository, IReconnectionRepository Reconnection)
    {
        _teamRepository = teamRepository;
        _reconnectionRepository = Reconnection;
    }

    public async Task<ApiResponse<DefaultIdType>> Handle(AssignReconnectionTasksCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetByIdAsync(request.TeamId, cancellationToken)
                   ?? throw new NotFoundException("Team not found");

        // Get Reconnection results
        var assignmentResults = await UpdateReconnectionTasksToAssignedAsync(request.ReconnectionTaskIds, cancellationToken);

        // Filter successful assignments
        var successfullyAssignedIds = assignmentResults
            .Where(result => result.Success)
            .Select(result => result.ReconnectionId)
            .ToList();

        // Assign only successfully assigned Reconnections to the team
        team.AssignReconnectionTasks(successfullyAssignedIds);
        await _teamRepository.UpdateAsync(team, cancellationToken);

        if (assignmentResults.Any(result => !result.Success))
        {
            return new ApiResponse<DefaultIdType>(
                true,
                "Some Reconnections could not be assigned. Check the errors for details.",
                default);
        }

        return new ApiResponse<DefaultIdType>(false, "Reconnections successfully assigned to Team", request.TeamId);
    }

    private async Task<List<AssignmentResult>> UpdateReconnectionTasksToAssignedAsync(List<Guid> ReconnectionTaskIds, CancellationToken cancellationToken)
    {
        var assignmentResults = new List<AssignmentResult>();
        var reconnections = await _reconnectionRepository.GetReconnectionsByIdsAsync(ReconnectionTaskIds, cancellationToken);

        foreach (var reconnection in reconnections)
        {
            try
            {
                reconnection.Assign();
                assignmentResults.Add(new AssignmentResult
                {
                    Success = true,
                    ReconnectionId = reconnection.Id
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
        public Guid ReconnectionId { get; set; }
    }
}