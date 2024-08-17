using Microsoft.Extensions.Logging;
using OpsManagerAPI.Domain.Aggregates.TariffsAggregate;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using OpsManagerAPI.Infrastructure.Persistence.Initialization;

namespace OpsManagerAPI.Infrastructure.Persistence.CustomSeeders;

public class TariffSeeder : ICustomSeeder
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<TariffSeeder> _logger;

    public TariffSeeder(ILogger<TariffSeeder> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_db.Tariffs.Any())
        {
            _logger.LogInformation("Started to Seed Tariffs.");

            // Seed Tariffs
            foreach (var tariff in GetTariffs())
            {
                await _db.Tariffs.AddAsync(tariff, cancellationToken);
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Tariffs.");
        }
    }

    private static List<Tariff> GetTariffs()
    {
        var tariffs = new List<Tariff>();

        // Service Band A (Minimum of 20hrs a day)
        AddTariffsForServiceBand(
            tariffs,
            "A",
            20,
            new Dictionary<string, decimal>
        {
            { "NMD-A", 206.80m },
            { "MD1-A", 206.80m },
            { "MD2-A", 206.80m }
        },
            tariffCodes: new List<string>
        {
            "R25", "R3", "R4", "C2", "C3", "D2", "D3", "A1S", "A1T", "A2", "A3", "E1", "E2", "E3", "RST", "S1", "D1S", "D1T"
        });

        // Service Band B (Minimum of 16hrs a day)
        AddTariffsForServiceBand(
            tariffs,
            "B",
            16,
            new Dictionary<string, decimal>
        {
            { "NMD-B", 61.00m },
            { "MD1-B", 64.07m },
            { "MD2-B", 64.07m }
        },
            tariffCodes: new List<string> { "R4", "C3", "D3", "A3", "E3" });

        // Service Band C (Minimum of 12hrs a day)
        AddTariffsForServiceBand(
            tariffs,
            "C",
            12,
            new Dictionary<string, decimal>
        {
            { "NMD-C", 48.53m },
            { "MD1-C", 52.05m },
            { "MD2-C", 52.05m }
        },
            tariffCodes: new List<string> { "R25", "R3", "R4", "C2", "C3", "D2", "D3", "A1S", "A1T", "A2", "A3", "E1", "E2", "E3", "RST", "S1", "D1S", "D1T" });

        // Service Band D (Minimum of 8hrs a day)
        AddTariffsForServiceBand(
            tariffs,
            "D",
            8,
            new Dictionary<string, decimal>
        {
            { "NMD-D", 32.48m },
            { "MD1-D", 43.27m },
            { "MD2-D", 43.27m }
        },
            tariffCodes: new List<string> { "R4", "C3", "D3", "A3", "E3" });

        // Service Band E (Minimum of 4hrs a day) - Currently suspended
        AddTariffsForServiceBand(
            tariffs,
            "E",
            4,
            new Dictionary<string, decimal>
        {
            { "NMD-E", 32.44m },
            { "MD1-E", 43.27m },
            { "MD2-E", 43.27m }
        },
            tariffCodes: new List<string> { "R4", "C3", "D3", "A3", "E3" });

        return tariffs;
    }

    private static void AddTariffsForServiceBand(List<Tariff> tariffs, string serviceBandName, int minimumHours, Dictionary<string, decimal> rateCategories, List<string> tariffCodes)
    {
        foreach (var rateCategory in rateCategories)
        {
            tariffs.AddRange(from code in tariffCodes
                             let tariff = new Tariff(code, rateCategory.Key, rateCategory.Value, serviceBandName, minimumHours)
                             select tariff);
        }
    }
}
