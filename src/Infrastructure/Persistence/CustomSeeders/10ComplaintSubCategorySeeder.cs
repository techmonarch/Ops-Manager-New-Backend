using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class ComplaintSubCategorySeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ComplaintSubCategorySeeder> _logger;

    public ComplaintSubCategorySeeder(ILogger<ComplaintSubCategorySeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.ComplaintSubCategories.Any())
        {
            _logger.LogInformation("Started to Seed ComplaintSubCategories.");

            var categories = _db.ComplaintCategories.ToList();

            var subCategoryNames = new Dictionary<string, string[]>
            {
                { "Electrical", new[] { "Power Outage", "Voltage Fluctuation" } },
                { "Bulb", new[] { "Pipe" } },
                { "Sanitation", new[] { "Sewage Backup" } }
            };

            var complaintSubCategoryFaker = new Faker<ComplaintSubCategory>()
                .CustomInstantiator(f => new ComplaintSubCategory(
                    name: f.PickRandom(subCategoryNames.Values.SelectMany(x => x)),
                    categoryId: f.PickRandom(categories).Id));

            var subCategories = new List<ComplaintSubCategory>();
            foreach (var category in categories)
            {
                if (subCategoryNames.TryGetValue(category.Name, out string[]? value))
                {
                    subCategories.AddRange(value.Select(name => new ComplaintSubCategory(name, category.Id)));
                }
            }

            await _db.ComplaintSubCategories.AddRangeAsync(subCategories, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded ComplaintSubCategories.");
        }
    }
}
