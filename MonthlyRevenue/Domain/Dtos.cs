namespace MonthlyRevenue.Domain
{
    public record RevenueUpsertRequest(
        DateTime? ReportDate,
        string DataYearMonth,
        string CompanyCode,
        string CompanyName,
        string? Industry,
        long? Rev_CurrentMonth,
        long? Rev_PreviousMonth,
        long? Rev_SameMonthLastYear,
        decimal? MoM_ChangePct,
        decimal? YoY_ChangePct,
        long? Rev_Accu_CurrentYear,
        long? Rev_Accu_LastYear,
        decimal? Accu_YoY_ChangePct,
        string? Notes
    );

    public record RevenueResponse(
        string DataYearMonth,
        string CompanyCode,
        string CompanyName,
        string? Industry,
        long? Rev_CurrentMonth,
        long? Rev_PreviousMonth,
        long? Rev_SameMonthLastYear,
        decimal? MoM_ChangePct,
        decimal? YoY_ChangePct,
        long? Rev_Accu_CurrentYear,
        long? Rev_Accu_LastYear,
        decimal? Accu_YoY_ChangePct
    );

    public record RevenueRow(
        DateTime? ReportDate,
        string DataYearMonth,
        string CompanyCode,
        string CompanyName,
        string? Industry,
        long? Rev_CurrentMonth,
        long? Rev_PreviousMonth,
        long? Rev_SameMonthLastYear,
        decimal? MoM_ChangePct,
        decimal? YoY_ChangePct,
        long? Rev_Accu_CurrentYear,
        long? Rev_Accu_LastYear,
        decimal? Accu_YoY_ChangePct,
        string? Notes
    );
}
