using Dapper;
using Finbuckle.MultiTenant.EntityFrameworkCore;
using OpsManagerAPI.Application.Common.Persistence;
using OpsManagerAPI.Infrastructure.Persistence.Configuration;
using OpsManagerAPI.Infrastructure.Persistence.Context;
using System.Data;

namespace OpsManagerAPI.Infrastructure.Persistence.Repository;
public class DapperRepository : IDapperRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DapperRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class =>
        (await _dbContext.Connection.QueryAsync<T>(AddSchema(sql), param, transaction))
            .AsList();

    public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class
    {
        if (_dbContext.Model.GetMultiTenantEntityTypes().Any(t => t.ClrType == typeof(T)))
        {
            sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        }

        string query = AddSchema(sql);

        return await _dbContext.Connection.QueryFirstOrDefaultAsync<T>(query, param, transaction);
    }

    public Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
    where T : class
    {
        if (_dbContext.Model.GetMultiTenantEntityTypes().Any(t => t.ClrType == typeof(T)))
        {
            sql = sql.Replace("@tenant", _dbContext.TenantInfo.Id);
        }

        return _dbContext.Connection.QuerySingleAsync<T>(AddSchema(sql), param, transaction);
    }

    private static string AddSchema(string sql)
    {
        // Ensure the schema is included in the SQL query
        return sql.Replace(" FROM ", $" FROM {SchemaNames.Dbo}.")
                  .Replace(" from ", $" from {SchemaNames.Dbo}.");
    }
}