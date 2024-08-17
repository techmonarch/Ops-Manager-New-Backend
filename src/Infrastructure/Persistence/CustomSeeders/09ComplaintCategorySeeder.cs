using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class ComplaintCategorySeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ComplaintCategorySeeder> _logger;

    public ComplaintCategorySeeder(ILogger<ComplaintCategorySeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.ComplaintCategories.Any())
        {
            _logger.LogInformation("Started to Seed ComplaintCategories.");

            string[] categoryNames = new[] { "Electrical", "Bulb", "Sanitation" };

            var complaintCategoryFaker = new Faker<ComplaintCategory>()
                .CustomInstantiator(f => new ComplaintCategory(f.PickRandom(categoryNames)));

            var categories = categoryNames.Select(name => new ComplaintCategory(name)).ToList();

            await _db.ComplaintCategories.AddRangeAsync(categories, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded ComplaintCategories.");
        }
    }
}
