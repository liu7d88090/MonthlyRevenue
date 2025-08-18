/*============================================================
 檔名：03_usp_MonthlyRevenue_Search.sql
 建立日期：2025-08-15
 修改紀錄：
   - 2025-08-15 首版：依公司代號 + 可選期間查詢
============================================================*/

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.usp_MonthlyRevenue_Search', N'P') IS NOT NULL
    DROP PROC dbo.usp_MonthlyRevenue_Search;
GO

CREATE PROC dbo.usp_MonthlyRevenue_Search
    @CompanyCode  INT       = NULL,   -- 不限公司可 NULL
    @FromYM       SMALLINT  = NULL,   -- YYYYMM
    @ToYM         SMALLINT  = NULL,   -- YYYYMM
    @PageIndex    INT       = 1,      -- 1-based
    @PageSize     INT       = 100    -- 保護上限
AS
BEGIN
    SET NOCOUNT ON;

    IF @PageIndex < 1 SET @PageIndex = 1;
    IF @PageSize  < 1 SET @PageSize  = 100;

    ;WITH F AS (
        SELECT
            ReportDate,
            DataYearMonth,
            CompanyCode,
            CompanyName,
            Industry,
            Rev_CurrentMonth,
            Rev_PreviousMonth,
            Rev_SameMonthLastYear,
            MoM_ChangePct,
            YoY_ChangePct,
            Rev_Accu_CurrentYear,
            Rev_Accu_LastYear,
            Accu_YoY_ChangePct,
            Notes,
            TotalCount = COUNT(*) OVER()
        FROM dbo.MonthlyRevenueFromCsv WITH (NOLOCK)
        WHERE (@CompanyCode IS NULL OR CompanyCode = @CompanyCode)
          AND (@FromYM     IS NULL OR DataYearMonth >= @FromYM)
          AND (@ToYM       IS NULL OR DataYearMonth <= @ToYM)
    )
    SELECT
        ReportDate,
        DataYearMonth,
        CompanyCode,
        CompanyName,
        Industry,
        Rev_CurrentMonth,
        Rev_PreviousMonth,
        Rev_SameMonthLastYear,
        MoM_ChangePct,
        YoY_ChangePct,
        Rev_Accu_CurrentYear,
        Rev_Accu_LastYear,
        Accu_YoY_ChangePct,
        Notes,
        TotalCount
    FROM F
    ORDER BY CompanyCode ASC, DataYearMonth DESC
    OFFSET (@PageIndex - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO