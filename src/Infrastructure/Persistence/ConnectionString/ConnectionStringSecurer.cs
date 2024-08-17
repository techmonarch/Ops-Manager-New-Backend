using Microsoft.Extensions.Options;
using OpsManagerAPI.Application.Common.Persistence;
using System.Data.SqlClient;

namespace OpsManagerAPI.Infrastructure.Persistence.ConnectionString;
public class ConnectionStringSecurer : IConnectionStringSecurer
{
    private const string HiddenValueDefault = "*******";

    public string? MakeSecure(string? connectionString)
    {
        if (connectionString == null || string.IsNullOrEmpty(connectionString))
            return connectionString;

        return MakeSecureSqlConnectionString(connectionString);
    }

    private static string MakeSecureSqlConnectionString(string connectionString)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);

        if (!string.IsNullOrEmpty(builder.Password) || !builder.IntegratedSecurity)
        {
            builder.Password = HiddenValueDefault;
        }

        if (!string.IsNullOrEmpty(builder.UserID) || !builder.IntegratedSecurity)
        {
            builder.UserID = HiddenValueDefault;
        }

        return builder.ToString();
    }
}