using Dapper;
using MediatR;
using MonthlyRevenue.Infrastructure;
using System.Data;
namespace MonthlyRevenue.Application.Commands; 
public record RevenueUpsertCommand(int? ReportDate, int DataYearMonth, string CompanyCode, string? CompanyName, 
    string? Industry, long? Rev_CurrentMonth, long? Rev_PreviousMonth, long? Rev_SameMonthLastYear, decimal? MoM_ChangePct, 
    decimal? YoY_ChangePct, long? Rev_Accu_CurrentYear, long? Rev_Accu_LastYear, decimal? Accu_YoY_ChangePct, string? Notes) 
    : IRequest<int>; public class RevenueUpsertHandler : IRequestHandler<RevenueUpsertCommand, int> 
{ 
    private readonly DapperContext _ctx; 
    public RevenueUpsertHandler(DapperContext ctx) => _ctx = ctx; 
    public async Task<int> Handle(RevenueUpsertCommand c, CancellationToken ct) 
    {
        using var conn = _ctx.CreateConnection();
        if (!int.TryParse(c.CompanyCode, out var companyCode))
            throw new ArgumentException($"CompanyCode 不是有效數字：{c.CompanyCode}");
        var p = new DynamicParameters();
        p.Add("@ReportDate", c.ReportDate, DbType.Int32);
        p.Add("@DataYearMonth", c.DataYearMonth, DbType.Int16);
        p.Add("@CompanyCode", companyCode, DbType.Int32);
        p.Add("@CompanyName", c.CompanyName, DbType.String, size: 50);
        p.Add("@Industry", c.Industry, DbType.String, size: 50);
        p.Add("@Rev_CurrentMonth", c.Rev_CurrentMonth, DbType.Int64);
        p.Add("@Rev_PreviousMonth", c.Rev_PreviousMonth, DbType.Int64);
        p.Add("@Rev_SameMonthLastYear", c.Rev_SameMonthLastYear, DbType.Int64);
        p.Add("@MoM_ChangePct", c.MoM_ChangePct, DbType.Decimal);
        p.Add("@YoY_ChangePct", c.YoY_ChangePct, DbType.Decimal);
        p.Add("@Rev_Accu_CurrentYear", c.Rev_Accu_CurrentYear, DbType.Int64);
        p.Add("@Rev_Accu_LastYear", c.Rev_Accu_LastYear, DbType.Int64);
        p.Add("@Accu_YoY_ChangePct", c.Accu_YoY_ChangePct, DbType.Decimal);
        p.Add("@Notes", c.Notes, DbType.String);
        p.Add("@Affected", dbType: DbType.Int32, direction: ParameterDirection.Output);

        await conn.ExecuteAsync(
            "dbo.usp_MonthlyRevenue_Upsert",
            p,
            commandType: CommandType.StoredProcedure
        );

        return p.Get<int>("@Affected"); // 1=插入, 0=已存在
    } 
}