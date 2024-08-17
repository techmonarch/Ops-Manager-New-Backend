using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.MeterAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class DistributionTransformerSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<DistributionTransformerSeeder> _logger;

    public DistributionTransformerSeeder(ILogger<DistributionTransformerSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.DistributionTransformers.Any())
        {
            _logger.LogInformation("Started to Seed Distribution Transformers.");

            var offices = _db.Offices.ToList();
            var transformerFaker = new Faker<DistributionTransformer>()
                .CustomInstantiator(f => new DistributionTransformer(
                    officeId: f.PickRandom(offices).Id,
                    name: f.Commerce.ProductName(),
                    longitude: Convert.ToDecimal(f.Address.Longitude()),
                    latitude: Convert.ToDecimal(f.Address.Latitude()),
                    installationDate: f.Date.Past(10),
                    capacity: f.Random.Int(50, 200),
                    status: f.PickRandom(new[] { "Active", "Inactive", "Maintenance" }),
                    rating: f.Random.Int(1, 5),
                    maker: f.Company.CompanyName(),
                    feederPillarType: f.Commerce.ProductAdjective()));

            var transformers = transformerFaker.Generate(10);
            await _db.DistributionTransformers.AddRangeAsync(transformers, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Distribution Transformers.");
        }
    }
}
