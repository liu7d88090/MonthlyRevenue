using Microsoft.Data.SqlClient;
using System.Data;

namespace MonthlyRevenue.Infrastructure
{
    public class DapperContext
    {
        private readonly IConfiguration _cfg;
        public DapperContext(IConfiguration cfg) => _cfg = cfg;
        public IDbConnection CreateConnection()
            => new SqlConnection(_cfg.GetConnectionString("Default"));
    }
}
