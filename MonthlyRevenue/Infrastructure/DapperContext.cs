using System.Data;
using Microsoft.Data.SqlClient;

namespace MonthlyRevenue.Infrastructure;

public sealed class DapperContext
{
    private readonly string _connString;

    public DapperContext(IConfiguration cfg)
    {
        var cs = cfg.GetConnectionString("Default");
        if (string.IsNullOrWhiteSpace(cs))
            throw new InvalidOperationException(
                "Connection string 'Default' not found or empty. " +
                "Check appsettings*.json / environment variables.");

        _connString = cs;
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connString);
}
