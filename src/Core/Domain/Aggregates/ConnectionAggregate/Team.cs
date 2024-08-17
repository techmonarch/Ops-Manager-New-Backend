using OpsManagerAPI.Domain.Common;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;
using System.Linq;
using OpsManagerAPI.Domain.Aggregates.OfficeAggregate;

namespace OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;

public class Team : AuditableEntity, IAggregateRoot
{
    private readonly HashSet<StaffTeam> _staffTeams = new();
    private readonly List<Guid> _reconnectionTaskIds = new();
    private readonly List<Guid> _disconnectionTaskIds = new();

    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string OfficeName { get; private set; } = default!;
    public DefaultIdType TeamLeadId { get; private set; }
    public DefaultIdType OfficeId { get; private set; }
    public IReadOnlyCollection<StaffTeam> StaffTeams => _staffTeams;
    public IReadOnlyCollection<Guid> ReconnectionTaskIds => _reconnectionTaskIds.AsReadOnly();
    public IReadOnlyCollection<Guid> DisconnectionTaskIds => _disconnectionTaskIds.AsReadOnly();
    public bool IsActive { get; private set; }

    private Team()
    {
    }

    public Team(string name, string description, DefaultIdType officeId, string officeName, DefaultIdType teamLeadId)
    {
        Name = name;
        Description = description;
        OfficeId = officeId;
        OfficeName = officeName;
        TeamLeadId = teamLeadId;
        IsActive = true;
    }

    public Office? Office { get; private set; }

    public void AddMembers(IEnumerable<Staff> staffMembers)
    {
        // Use a HashSet to track existing staff IDs for quick lookups
        var existingStaffIds = new HashSet<DefaultIdType>(_staffTeams.Select(st => st.StaffId));

        foreach (var staff in staffMembers)
        {
            if (existingStaffIds.Contains(staff.Id))
            {
                throw new InvalidOperationException($"Member with ID {staff.Id} is already in the team.");
            }

            var staffTeam = new StaffTeam(staff.Id, this.Id);
            _staffTeams.Add(staffTeam);
            existingStaffIds.Add(staff.Id); // Add to the HashSet for future checks
        }
    }

    public void RemoveMembers(IEnumerable<Staff> staffMembers)
    {
        // Use a HashSet for quick lookups
        var staffIdsToRemove = new HashSet<DefaultIdType>(staffMembers.Select(staff => staff.Id));

        // Keep track of which members were removed
        foreach (var staffTeam in _staffTeams.Where(st => staffIdsToRemove.Contains(st.StaffId)).ToList())
        {
            _staffTeams.Remove(staffTeam);
        }
    }

    public void ChangeStatus()
    {
        IsActive = !IsActive;
    }

    public void AssignReconnectionTasks(IEnumerable<Guid> taskIds)
    {
        _reconnectionTaskIds.AddRange(taskIds);
    }

    public void AssignDisconnectionTasks(IEnumerable<Guid> taskIds)
    {
        _disconnectionTaskIds.AddRange(taskIds);
    }
}
