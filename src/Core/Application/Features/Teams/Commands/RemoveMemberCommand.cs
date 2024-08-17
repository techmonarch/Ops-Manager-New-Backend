using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Teams.Commands;
public class RemoveMemberCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType TeamId { get; set; }
    public List<DefaultIdType> StaffMemberIds { get; set; } = new();
}

public class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<Team> _repository;
    private readonly IStaffRepository _staffRepository;

    public RemoveMemberCommandHandler(IRepositoryWithEvents<Team> repository, IStaffRepository staffRepository)
    {
        _repository = repository;
        _staffRepository = staffRepository;
    }

    public async Task<ApiResponse<DefaultIdType>> Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
    {
        var team = await _repository.GetByIdAsync(request.TeamId, cancellationToken);
        var staffMembers = await _staffRepository.GetByUserIdsAsync(request.StaffMemberIds, cancellationToken) ?? throw new NotFoundException("Staff not found.");

        team.RemoveMembers(staffMembers);

        await _repository.UpdateAsync(team, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Members successfully removed from team.", team.Id);
    }
}