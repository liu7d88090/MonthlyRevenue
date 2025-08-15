/*============================================================
 檔名：01_create_table_MonthlyRevenue.sql
 建立日期：2025-08-15
 修改紀錄：
   - 2025-08-15 首版：建立資料表、主鍵、索引
============================================================*/

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID(N'dbo.MonthlyRevenue', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.MonthlyRevenue (
        ReportDate               date            NULL,          -- 出表日期
        DataYearMonth            char(6)         NOT NULL,      -- 資料年月 (YYYYMM)
        CompanyCode              varchar(10)     NOT NULL,      -- 公司代號
        CompanyName              nvarchar(120)   NOT NULL,      -- 公司名稱
        Industry                 nvarchar(60)    NULL,          -- 產業別

        Rev_CurrentMonth         bigint          NULL,          -- 當月營收
        Rev_PreviousMonth        bigint          NULL,          -- 上月營收
        Rev_SameMonthLastYear    bigint          NULL,          -- 去年同月營收
        MoM_ChangePct            decimal(9,2)    NULL,          -- 月增率(%)
        YoY_ChangePct            decimal(9,2)    NULL,          -- 年增率(%)

        Rev_Accu_CurrentYear     bigint          NULL,          -- 本年累計營收
        Rev_Accu_LastYear        bigint          NULL,          -- 上年累計營收
        Accu_YoY_ChangePct       decimal(9,2)    NULL,          -- 累計年增率(%)

        Notes                    nvarchar(400)   NULL,          -- 備註
        CONSTRAINT PK_MonthlyRevenue PRIMARY KEY (DataYearMonth, CompanyCode)
    );
END
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_MonthlyRevenue_Code_YM' AND object_id = OBJECT_ID(N'dbo.MonthlyRevenue')
)
BEGIN
    CREATE INDEX IX_MonthlyRevenue_Code_YM
        ON dbo.MonthlyRevenue (CompanyCode, DataYearMonth);
END
GO
