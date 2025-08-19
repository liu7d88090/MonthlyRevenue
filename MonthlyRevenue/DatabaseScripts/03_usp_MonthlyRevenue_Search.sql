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
    @FromYM       SMALLINT  = NULL,   -- 民國 YYYYMM
    @ToYM         SMALLINT  = NULL,   -- 民國 YYYYMM
    @PageIndex    INT       = 1,      -- 1-based
    @PageSize     INT       = 100     -- 保護上限（下方再強制 <=1000）
AS
BEGIN
    SET NOCOUNT ON;

    IF @PageIndex < 1 SET @PageIndex = 1;
    IF @PageSize  < 1 SET @PageSize  = 100;
    IF @PageSize  > 1000 SET @PageSize = 1000;

    -- 結果 1：分頁資料
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
        Notes
    FROM dbo.MonthlyRevenueFromCsv
    WHERE (@CompanyCode IS NULL OR CompanyCode = @CompanyCode)
      AND (@FromYM     IS NULL OR DataYearMonth >= @FromYM)
      AND (@ToYM       IS NULL OR DataYearMonth <= @ToYM)
    ORDER BY CompanyCode ASC, DataYearMonth DESC
    OFFSET (@PageIndex - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- 結果 2：總筆數
    SELECT COUNT(*) AS TotalCount
    FROM dbo.MonthlyRevenueFromCsv
    WHERE (@CompanyCode IS NULL OR CompanyCode = @CompanyCode)
      AND (@FromYM     IS NULL OR DataYearMonth >= @FromYM)
      AND (@ToYM       IS NULL OR DataYearMonth <= @ToYM);
END
GO