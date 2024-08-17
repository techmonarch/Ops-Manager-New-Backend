using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;

namespace OpsManagerAPI.Application.Features.Teams.Commands;
public class CreateTeamCommand : IRequest<ApiResponse<DefaultIdType>>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, ApiResponse<DefaultIdType>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Team> _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;

    public CreateTeamCommandHandler(IRepositoryWithEvents<Team> repository, ICurrentUser currentUser, IStaffRepository staffRepository) => (_repository, _currentUser, _staffRepository) = (repository, currentUser, staffRepository);

    public async Task<ApiResponse<DefaultIdType>> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, cancellationToken) ?? throw new InvalidOperationException("Can't retrieve the current user's detail");

        var team = new Team(request.Name, request.Description, currentStaff.OfficeId, currentStaff.Office.Name, currentStaff.Id);
        team.AddMembers(new List<Staff>
        {
            currentStaff
        });

        await _repository.AddAsync(team, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ApiResponse<DefaultIdType>(true, $"{request.Name} Team Successfully created.", team.Id);
    }
}