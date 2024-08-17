using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.ComplaintsAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class ComplaintSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ComplaintSeeder> _logger;

    public ComplaintSeeder(ILogger<ComplaintSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Complaints.Any())
        {
            _logger.LogInformation("Started to Seed Complaints.");

            var customers = _db.Customers.ToList();
            var staffs = _db.Staffs.ToList();
            var transformers = _db.DistributionTransformers.ToList();
            var categories = _db.ComplaintCategories.ToList();
            var subCategories = _db.ComplaintSubCategories.ToList();

            var complaintFaker = new Faker<Complaint>()
                .CustomInstantiator(f => new Complaint(
                    categoryId: f.PickRandom(categories).Id,
                    subCategoryId: f.PickRandom(subCategories).Id,
                    comment: f.Lorem.Paragraph(),
                    imagePath: null,
                    customerId: f.PickRandom(customers).Id,
                    distributionTransformerId: f.PickRandom(transformers).Id));

            var complaints = complaintFaker.Generate(10);

            await _db.Complaints.AddRangeAsync(complaints, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Complaints.");
        }
    }
}
