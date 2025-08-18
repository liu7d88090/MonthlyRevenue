/*============================================================
 檔名：02_proc_upsert_MonthlyRevenue.sql
 說明：只 INSERT；若鍵已存在則不處理
============================================================*/
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.usp_MonthlyRevenue_Upsert', N'P') IS NOT NULL
    DROP PROC dbo.usp_MonthlyRevenue_Upsert;
GO

CREATE PROC dbo.usp_MonthlyRevenue_Upsert
    @ReportDate            INT               = NULL,
    @DataYearMonth         SMALLINT,
    @CompanyCode           INT,
    @CompanyName           NVARCHAR(50)      = NULL,
    @Industry              NVARCHAR(50)      = NULL,
    @Rev_CurrentMonth      BIGINT            = NULL,
    @Rev_PreviousMonth     BIGINT            = NULL,
    @Rev_SameMonthLastYear BIGINT            = NULL,
    @MoM_ChangePct         DECIMAL(9,2)      = NULL,
    @YoY_ChangePct         DECIMAL(9,2)      = NULL,
    @Rev_Accu_CurrentYear  BIGINT            = NULL,
    @Rev_Accu_LastYear     BIGINT            = NULL,
    @Accu_YoY_ChangePct    DECIMAL(9,2)      = NULL,
    @Notes                 NVARCHAR(MAX)     = NULL
AS
BEGIN

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.MonthlyRevenueFromCsv WITH (UPDLOCK, HOLDLOCK)
        WHERE DataYearMonth = @DataYearMonth
          AND CompanyCode   = @CompanyCode
    )
    BEGIN
        INSERT dbo.MonthlyRevenueFromCsv (
            ReportDate, DataYearMonth, CompanyCode, CompanyName, Industry,
            Rev_CurrentMonth, Rev_PreviousMonth, Rev_SameMonthLastYear,
            MoM_ChangePct, YoY_ChangePct, Rev_Accu_CurrentYear, Rev_Accu_LastYear,
            Accu_YoY_ChangePct, Notes
        )
        VALUES (
            @ReportDate, @DataYearMonth, @CompanyCode, @CompanyName, @Industry,
            @Rev_CurrentMonth, @Rev_PreviousMonth, @Rev_SameMonthLastYear,
            @MoM_ChangePct, @YoY_ChangePct, @Rev_Accu_CurrentYear, @Rev_Accu_LastYear,
            @Accu_YoY_ChangePct, ISNULL(@Notes, N'')
        );
    END

    SELECT @@ROWCOUNT AS RowsInserted;
END
GO
