namespace MonthlyRevenue.Domain
{
    public record RevenueUpsertRequest(
        int? ReportDate,
        int DataYearMonth,
        int CompanyCode,
        string? CompanyName,
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
        int? ReportDate,
        int DataYearMonth,
        int CompanyCode,
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
