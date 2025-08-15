using Dapper;
using MediatR;
using MonthlyRevenue.Domain;
using MonthlyRevenue.Infrastructure;

namespace MonthlyRevenue.Application.Queries;
public record GetRevenuesByCompanyQuery(string CompanyCode, string? FromYM, string? ToYM)
    : IRequest<IReadOnlyList<RevenueRow>>;

public class GetRevenuesByCompanyHandler : IRequestHandler<GetRevenuesByCompanyQuery, IReadOnlyList<RevenueRow>>
{
    private readonly DapperContext _ctx;
    public GetRevenuesByCompanyHandler(DapperContext ctx) => _ctx = ctx;

    public async Task<IReadOnlyList<RevenueRow>> Handle(GetRevenuesByCompanyQuery r, CancellationToken ct)
    {
        using var conn = _ctx.CreateConnection();
        var rows = await conn.QueryAsync<RevenueRow>(
            "dbo.usp_MonthlyRevenue_SelectByCompany",
            new { CompanyCode = r.CompanyCode, FromYM = r.FromYM, ToYM = r.ToYM },
            commandType: System.Data.CommandType.StoredProcedure);
        return rows.ToList();
    }
}