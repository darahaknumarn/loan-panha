-- Migration: 0001_add_service_rule.sql
-- Purpose: Create table BK_ServiceRule to store dynamic rules for loan service fees (sh_service)
-- Notes:
--  - FeeType: 'Fixed' means fixed amount in currency; 'Percent' means percentage (use decimal like 0.015 for 1.5%)
--  - LoanUnit: optional filter for frequency (daily, weekly, biweekly, monthly). NULL means applies to any unit.
--  - BranchID: optional filter for branch-specific rule. NULL means global rule.
--  - Priority: higher priority rules take precedence when multiple match.

IF OBJECT_ID('dbo.BK_ServiceRule', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.BK_ServiceRule (
		RuleID INT IDENTITY(1,1) PRIMARY KEY,
		BranchID NVARCHAR(50) NULL,
		MinLoanAmt DECIMAL(18,2) NULL,
		MaxLoanAmt DECIMAL(18,2) NULL,
		LoanUnit NVARCHAR(20) NULL,
		FeeType NVARCHAR(20) NOT NULL, -- 'Fixed' or 'Percent'
		FeeValue DECIMAL(18,6) NOT NULL,
		MinFee DECIMAL(18,2) NULL,
		MaxFee DECIMAL(18,2) NULL,
		EffectiveFrom DATE NULL,
		IsActive BIT NOT NULL DEFAULT(1),
		Priority INT NOT NULL DEFAULT(1),
		CreatedOn DATETIME NOT NULL DEFAULT(GETDATE()),
		CreatedBy NVARCHAR(100) NULL
	);
END

-- Seed sample rules based on description provided by user:
-- 1) For any branch, any loan unit, loan amount <= 1,000,000 Riel -> fixed fee 1,000 Riel
-- 2) For any branch, any loan unit, loan amount > 1,000,000 Riel -> percent fee 1.5% (0.015)

INSERT INTO dbo.BK_ServiceRule(BranchID, MinLoanAmt, MaxLoanAmt, LoanUnit, FeeType, FeeValue, EffectiveFrom, IsActive, Priority, CreatedBy)
VALUES (NULL, 0.00, 1000000.00, NULL, 'Fixed', 1000.00, GETDATE(), 1, 100, 'migration');

INSERT INTO dbo.BK_ServiceRule(BranchID, MinLoanAmt, MaxLoanAmt, LoanUnit, FeeType, FeeValue, EffectiveFrom, IsActive, Priority, CreatedBy)
VALUES (NULL, 1000000.01, NULL, NULL, 'Percent', 0.015000, GETDATE(), 1, 90, 'migration');

-- Example: if you need different rules by loan unit (daily/weekly/monthly) you can add rows like:
-- INSERT INTO dbo.BK_ServiceRule(BranchID, MinLoanAmt, MaxLoanAmt, LoanUnit, FeeType, FeeValue, EffectiveFrom, IsActive, Priority, CreatedBy)
-- VALUES (NULL, 0.00, NULL, 'daily', 'Fixed', 100.00, GETDATE(), 1, 110, 'migration');

PRINT 'BK_ServiceRule created and sample rules inserted.';
