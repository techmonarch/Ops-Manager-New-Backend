namespace OpsManagerAPI.Application.Features.Teams.Dtos;
public class TeamDetailsDto
{
    public string? Id { get; set; }
    public string? Address { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<TeamMemberDetailsDto> StaffMembers { get; set; } = new();
}