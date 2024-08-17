using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Domain.Common;

namespace OpsManagerAPI.Domain.Aggregates.StaffAggregate;

public class StaffTeam : AuditableEntity
{
    public DefaultIdType StaffId { get; private set; }
    public DefaultIdType TeamId { get; private set; }

    public Staff Staff { get; private set; } = default!;
    public Team Team { get; private set; } = default!;

    #region Consructors
    private StaffTeam()
    {
    }

    public StaffTeam(DefaultIdType staffId, DefaultIdType teamId)
    {
        StaffId = staffId;
        TeamId = teamId;
    }
    #endregion
}
