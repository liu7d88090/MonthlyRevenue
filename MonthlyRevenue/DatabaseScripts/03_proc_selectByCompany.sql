/*============================================================
 檔名：03_proc_selectByCompany.sql
 建立日期：2025-08-15
 修改紀錄：
   - 2025-08-15 首版：依公司代號 + 可選期間查詢
============================================================*/

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.usp_MonthlyRevenue_SelectByCompany', N'P') IS NOT NULL
    DROP PROC dbo.usp_MonthlyRevenue_SelectByCompany;
GO

CREATE PROC dbo.usp_MonthlyRevenue_SelectByCompany
    @CompanyCode  varchar(10),
    @FromYM       char(6) = NULL,
    @ToYM         char(6) = NULL
AS
BEGIN
    SET NOCOUNT ON;

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
    FROM dbo.MonthlyRevenue WITH (NOLOCK)
    WHERE CompanyCode = @CompanyCode
      AND (@FromYM IS NULL OR DataYearMonth >= @FromYM)
      AND (@ToYM   IS NULL OR DataYearMonth <= @ToYM)
    ORDER BY DataYearMonth DESC, CompanyCode;
END
GO
