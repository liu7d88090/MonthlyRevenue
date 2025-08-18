/*視查詢情況加入*/

CREATE INDEX IX_MonthlyRevenueFromCsv_YM ON dbo.MonthlyRevenueFromCsv(DataYearMonth, CompanyCode);