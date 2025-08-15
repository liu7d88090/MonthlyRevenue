/*============================================================
 檔名：02_proc_upsert_MonthlyRevenue.sql
 建立日期：2025-08-15
 修改紀錄：
   - 2025-08-15 首版：Upsert（存在則更新，不在則新增）
============================================================*/

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.usp_MonthlyRevenue_Upsert', N'P') IS NOT NULL
    DROP PROC dbo.usp_MonthlyRevenue_Upsert;
GO

CREATE PROC dbo.usp_MonthlyRevenue_Upsert
    @ReportDate            date,
    @DataYearMonth         char(6),
    @CompanyCode           varchar(10),
    @CompanyName           nvarchar(120),
    @Industry              nvarchar(60) = NULL,
    @Rev_CurrentMonth      bigint = NULL,
    @Rev_PreviousMonth     bigint = NULL,
    @Rev_SameMonthLastYear bigint = NULL,
    @MoM_ChangePct         decimal(9,2) = NULL,
    @YoY_ChangePct         decimal(9,2) = NULL,
    @Rev_Accu_CurrentYear  bigint = NULL,
    @Rev_Accu_LastYear     bigint = NULL,
    @Accu_YoY_ChangePct    decimal(9,2) = NULL,
    @Notes                 nvarchar(400) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    MERGE dbo.MonthlyRevenue AS T
    USING (SELECT @DataYearMonth AS DataYearMonth, @CompanyCode AS CompanyCode) AS S
    ON (T.DataYearMonth = S.DataYearMonth AND T.CompanyCode = S.CompanyCode)
    WHEN MATCHED THEN
        UPDATE SET
            ReportDate            = @ReportDate,
            CompanyName           = @CompanyName,
            Industry              = @Industry,
            Rev_CurrentMonth      = @Rev_CurrentMonth,
            Rev_PreviousMonth     = @Rev_PreviousMonth,
            Rev_SameMonthLastYear = @Rev_SameMonthLastYear,
            MoM_ChangePct         = @MoM_ChangePct,
            YoY_ChangePct         = @YoY_ChangePct,
            Rev_Accu_CurrentYear  = @Rev_Accu_CurrentYear,
            Rev_Accu_LastYear     = @Rev_Accu_LastYear,
            Accu_YoY_ChangePct    = @Accu_YoY_ChangePct,
            Notes                 = @Notes
    WHEN NOT MATCHED THEN
        INSERT (ReportDate, DataYearMonth, CompanyCode, CompanyName, Industry,
                Rev_CurrentMonth, Rev_PreviousMonth, Rev_SameMonthLastYear,
                MoM_ChangePct, YoY_ChangePct, Rev_Accu_CurrentYear, Rev_Accu_LastYear,
                Accu_YoY_ChangePct, Notes)
        VALUES (@ReportDate, @DataYearMonth, @CompanyCode, @CompanyName, @Industry,
                @Rev_CurrentMonth, @Rev_PreviousMonth, @Rev_SameMonthLastYear,
                @MoM_ChangePct, @YoY_ChangePct, @Rev_Accu_CurrentYear, @Rev_Accu_LastYear,
                @Accu_YoY_ChangePct, @Notes);
END
GO
