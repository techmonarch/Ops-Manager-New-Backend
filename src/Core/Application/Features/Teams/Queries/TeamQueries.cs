using OpsManagerAPI.Application.Features.Staffs;
using OpsManagerAPI.Application.Features.Teams.Dtos;
using OpsManagerAPI.Application.Identity.Users;

namespace OpsManagerAPI.Application.Features.Teams.Queries;
public class TeamQueries : ITeamQueries
{
    #region Constructor
    private readonly ITeamRepository _repository;
    private readonly IStaffRepository _staffRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;

    public TeamQueries(ITeamRepository repository, IStaffRepository staffRepository, ICurrentUser currentUser, IUserService userService)
    {
        _repository = repository;
        _staffRepository = staffRepository;
        _currentUser = currentUser;
        _userService = userService;
    }

    #endregion

    public async Task<ApiResponse<PaginationResponse<TeamDetailsDto>>> GetAll(TeamFilterRequest request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUser.GetUserId();
        var currentStaff = await _staffRepository.GetByUserIdAsync(currentUserId, CancellationToken.None);

        var (teams, totalCount) = await _repository.GetAll(request, currentStaff, cancellationToken);

        var response = teams.ConvertAll(team =>
        {
            return new TeamDetailsDto
            {
                Id = team.Id.ToString(),
                Name = team.Name,
                Description = team.Description,
                Address = team.OfficeName,
                StaffMembers = _userService.GetTeamMemberDetails(team)
            };
        });

        var paginatedResponse = new PaginationResponse<TeamDetailsDto>(response, totalCount, request.PageNumber, request.PageSize);

        return new ApiResponse<PaginationResponse<TeamDetailsDto>>(true, "Teams successfully retrievd", paginatedResponse);
    }
}
