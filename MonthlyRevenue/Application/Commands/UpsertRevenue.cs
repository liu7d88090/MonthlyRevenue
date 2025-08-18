using Dapper;
using MediatR;
using MonthlyRevenue.Infrastructure;
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
        var affected = await conn.ExecuteAsync("dbo.usp_MonthlyRevenue_Upsert", 
            new { c.ReportDate, c.DataYearMonth, c.CompanyCode, c.CompanyName, c.Industry, 
                c.Rev_CurrentMonth, c.Rev_PreviousMonth, c.Rev_SameMonthLastYear, c.MoM_ChangePct, 
                c.YoY_ChangePct, c.Rev_Accu_CurrentYear, c.Rev_Accu_LastYear, c.Accu_YoY_ChangePct, c.Notes 
            }, commandType: System.Data.CommandType.StoredProcedure); return affected; 
    } 
}