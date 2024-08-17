using Microsoft.Extensions.Logging;
using OpsManagerAPI.Application.Common.Persistence;
using System.Data.SqlClient;

namespace OpsManagerAPI.Infrastructure.Persistence.ConnectionString;
internal class ConnectionStringValidator : IConnectionStringValidator
{
    private readonly ILogger<ConnectionStringValidator> _logger;

    public ConnectionStringValidator(ILogger<ConnectionStringValidator> logger) => _logger = logger;

    public bool TryValidate(string connectionString)
    {
        try
        {
            _ = new SqlConnectionStringBuilder(connectionString);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Connection String Validation Exception : {ex.Message}");
            return false;
        }
    }
}