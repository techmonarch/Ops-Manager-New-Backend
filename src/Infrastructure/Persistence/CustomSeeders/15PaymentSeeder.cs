using Bogus;
using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.PaymentAggregate;
using OpsManagerAPI.Domain.Enums;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class PaymentSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<PaymentSeeder> _logger;

    public PaymentSeeder(ILogger<PaymentSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Payments.Any())
        {
            _logger.LogInformation("Started to Seed Payments.");

            var customers = _db.Customers.ToList();
            var bills = _db.Billings.ToList();

            var paymentFaker = new Faker<Payment>()
                .CustomInstantiator(f => new Payment(
                    customerId: f.PickRandom(customers)?.Id ?? default,
                    paymentDate: f.Date.Past(1),
                    billId: f.PickRandom(bills)?.Id ?? default,
                    paymentSource: f.PickRandom(new[] { "Bank Transfer", "Credit Card", "Cash", "Mobile Payment" }),
                    amount: f.Finance.Amount(100, 5000),
                    accountType: f.PickRandom<AccountType>(),
                    customerType: f.PickRandom<CustomerType>()));

            var payments = paymentFaker.Generate(10);

            await _db.Payments.AddRangeAsync(payments, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Payments.");
        }
    }
}
