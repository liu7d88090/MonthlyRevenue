/*============================================================
 檔名：01_create_table_MonthlyRevenue.sql
 建立日期：2025-08-15
 修改紀錄：
   - 2025-08-15 首版：建立資料表、主鍵、索引
============================================================*/

CREATE TABLE [dbo].[MonthlyRevenueFromCsv] (
    [ReportDate]            INT             NULL,
    [DataYearMonth]         SMALLINT        NOT NULL,
    [CompanyCode]           INT             NOT NULL,
    [CompanyName]           NVARCHAR(50)    NULL,
    [Industry]              NVARCHAR(50)    NULL,
    [Rev_CurrentMonth]      BIGINT          NULL,
    [Rev_PreviousMonth]     BIGINT          NULL,
    [Rev_SameMonthLastYear] BIGINT          NULL,
    [MoM_ChangePct]         DECIMAL(9,2)    NULL,
    [YoY_ChangePct]         DECIMAL(9,2)    NULL,
    [Rev_Accu_CurrentYear]  BIGINT          NULL,
    [Rev_Accu_LastYear]     BIGINT          NULL,
    [Accu_YoY_ChangePct]    DECIMAL(9,2)    NULL,
    [Notes]                 NVARCHAR(MAX)   NOT NULL,
    CONSTRAINT [PK_MonthlyRevenueFromCsv] 
        PRIMARY KEY CLUSTERED ([CompanyCode], [DataYearMonth])
);