using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.ConnectionAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;
using OpsManagerAPI.Domain.Aggregates.StaffAggregate;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class TeamSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<TeamSeeder> _logger;

    public TeamSeeder(ILogger<TeamSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Teams.Any())
        {
            _logger.LogInformation("Started to Seed Teams.");

            var offices = _db.Offices.ToList();
            var staffMembers = _db.Staffs.ToList(); // Get all staff members to pick a team lead

            var teamFaker = new Faker<Team>()
                .CustomInstantiator(f =>
                {
                    var office = f.PickRandom(offices);
                    var teamLead = f.PickRandom(staffMembers);

                    return new Team(
                        name: f.Company.CompanyName(),
                        description: f.Lorem.Sentence(),
                        officeId: office?.Id ?? default,
                        officeName: office?.Name ?? string.Empty,
                        teamLeadId: teamLead.Id);
                });

            var teams = teamFaker.Generate(5);

            await _db.Teams.AddRangeAsync(teams, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Teams.");
        }
    }
}
