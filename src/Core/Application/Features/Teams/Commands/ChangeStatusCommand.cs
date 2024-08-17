using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Teams.Commands;
public class ChangeStatusCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public Guid TeamId { get; set; }
}

public class ChangeStatusCommandHandler : IRequestHandler<ChangeStatusCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<Team> _repository;

    public ChangeStatusCommandHandler(IRepositoryWithEvents<Team> repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<DefaultIdType>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
    {
        var team = await _repository.GetByIdAsync(request.TeamId, cancellationToken);

        team.ChangeStatus();

        await _repository.UpdateAsync(team, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, $"Team status successfully changed to {(team.IsActive ? "Active." : "Inactive.")}", team.Id);
    }
}