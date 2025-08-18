namespace MonthlyRevenue.Models
{
    public sealed class RevenueSearchRequest
    {
        public string? CompanyCode { get; set; }
        public string? FromYM { get; set; }     // "YYYMM"
        public string? ToYM { get; set; }       // "YYYMM"
        public int PageIndex { get; set; } = 1; // 1-based
        public int PageSize { get; set; } = 100;
    }
}
