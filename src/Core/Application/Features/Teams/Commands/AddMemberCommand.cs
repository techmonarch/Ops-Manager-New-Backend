using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

namespace OpsManagerAPI.Application.Features.Teams.Commands;

public class AddMemberCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public DefaultIdType TeamId { get; set; }
    public List<DefaultIdType> StaffMemberIds { get; set; } = new();
}

public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, ApiResponse<DefaultIdType>>
{
    private readonly IRepositoryWithEvents<Team> _repository;
    private readonly IStaffRepository _staffRepository;

    public AddMemberCommandHandler(IRepositoryWithEvents<Team> repository, IStaffRepository staffRepository)
    {
        _repository = repository;
        _staffRepository = staffRepository;
    }

    public async Task<ApiResponse<DefaultIdType>> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        var team = await _repository.GetByIdAsync(request.TeamId, cancellationToken) ?? throw new InvalidOperationException("team does not exist.");
        var staffMember = await _staffRepository.GetByUserIdsAsync(request.StaffMemberIds, cancellationToken) ?? throw new InvalidOperationException("Staff does not exist.");

        team.AddMembers(staffMember);

        await _repository.UpdateAsync(team, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, "Members successfully added to team.", team.Id);
    }
}