using System.Data;
using Dapper;
using MediatR;
using MonthlyRevenue.Domain;
using MonthlyRevenue.Infrastructure;
using MonthlyRevenue.Models; // PagedResponse<T>

namespace MonthlyRevenue.Application.Queries
{
    // 查詢參數：不限公司、可選期間 + 分頁
    public sealed record SearchRevenuesQuery(
        string? CompanyCode,
        string? FromYM,          // "YYYYMM"
        string? ToYM,            // "YYYYMM"
        int PageIndex = 1,       // 1-based
        int PageSize = 100
    ) : IRequest<PagedResponse<MonthlyRevenueFromCsv>>;

    public sealed class SearchRevenuesHandler
        : IRequestHandler<SearchRevenuesQuery, PagedResponse<MonthlyRevenueFromCsv>>
    {
        private readonly DapperContext _ctx;
        public SearchRevenuesHandler(DapperContext ctx) => _ctx = ctx;

        public async Task<PagedResponse<MonthlyRevenueFromCsv>> Handle(
            SearchRevenuesQuery r, CancellationToken ct)
        {
            using var conn = _ctx.CreateConnection();

            // 參數正規化
            int? code = ParseNullableInt(r.CompanyCode);
            short? fromYm = ParseNullableShort(r.FromYM);
            short? toYm = ParseNullableShort(r.ToYM);
            int pageIdx = r.PageIndex > 0 ? r.PageIndex : 1;
            int pageSz = r.PageSize > 0 ? r.PageSize : 100;

            var cmd = new CommandDefinition(
                "dbo.usp_MonthlyRevenue_Search",
                new
                {
                    CompanyCode = code,
                    FromYM = fromYm,
                    ToYM = toYm,
                    PageIndex = pageIdx,
                    PageSize = pageSz
                },
                commandType: CommandType.StoredProcedure,
                cancellationToken: ct
            );

            using var grid = await conn.QueryMultipleAsync(cmd);
            var items = (await grid.ReadAsync<MonthlyRevenueFromCsv>()).AsList();
            var total = await grid.ReadSingleAsync<int>();

            return new PagedResponse<MonthlyRevenueFromCsv>
            {
                Items = items,
                PageIndex = pageIdx,
                PageSize = pageSz,
                TotalCount = total
            };
        }

        private static int? ParseNullableInt(string? s)
            => string.IsNullOrWhiteSpace(s) ? null : (int.TryParse(s, out var v) ? v : null);

        private static short? ParseNullableShort(string? s)
            => string.IsNullOrWhiteSpace(s) ? null : (short.TryParse(s, out var v) ? v : null);
    }
}
