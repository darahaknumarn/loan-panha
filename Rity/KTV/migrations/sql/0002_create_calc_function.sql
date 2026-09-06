-- Migration: 0002_create_calc_function.sql
-- Purpose: Create a function to compute loan service fee (sh_service) dynamically based on BK_ServiceRule
-- This function computes a fee given a loan amount and loan unit. It does not assume specific BK_Loan column names.

IF OBJECT_ID('dbo.ufn_GetLoanService', 'FN') IS NOT NULL
	DROP FUNCTION dbo.ufn_GetLoanService;
GO
CREATE FUNCTION dbo.ufn_GetLoanService(
	@LoanAmt DECIMAL(18,2),
	@LoanUnit NVARCHAR(20), -- e.g. 'daily','weekly','biweekly','monthly' or NULL
	@BranchID NVARCHAR(50) = NULL
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	DECLARE @Result DECIMAL(18,6) = 0.00;
	DECLARE @FeeType NVARCHAR(20);
	DECLARE @FeeValue DECIMAL(18,6);
	DECLARE @MinFee DECIMAL(18,2);
	DECLARE @MaxFee DECIMAL(18,2);

	-- Find the best matching rule: branch-specific preferred, then global; LoanUnit-specific preferred, then global
	SELECT TOP 1
		@FeeType = FeeType,
		@FeeValue = FeeValue,
		@MinFee = MinFee,
		@MaxFee = MaxFee
	FROM dbo.BK_ServiceRule r
	WHERE r.IsActive = 1
	  AND (@BranchID IS NULL OR r.BranchID IS NULL OR r.BranchID = @BranchID)
	  AND (r.LoanUnit IS NULL OR @LoanUnit IS NULL OR r.LoanUnit = @LoanUnit)
	  AND (@LoanAmt >= ISNULL(r.MinLoanAmt, 0))
	  AND (@LoanAmt <= ISNULL(r.MaxLoanAmt, 999999999999.00))
	ORDER BY
		CASE WHEN r.BranchID IS NOT NULL THEN 1 ELSE 0 END DESC, -- prefer branch-specific
		CASE WHEN r.LoanUnit IS NOT NULL THEN 1 ELSE 0 END DESC, -- prefer unit-specific
		r.Priority DESC;

	IF @FeeType IS NULL
	BEGIN
		-- No matching rule found: default to 0
		RETURN 0.00;
	END

	IF UPPER(@FeeType) = 'FIXED'
	BEGIN
		SET @Result = ISNULL(@FeeValue, 0);
	END
	ELSE IF UPPER(@FeeType) = 'PERCENT'
	BEGIN
		SET @Result = ROUND(ISNULL(@LoanAmt, 0) * ISNULL(@FeeValue, 0), 2);
	END
	ELSE
	BEGIN
		-- Unknown FeeType: treat as zero
		SET @Result = 0.00;
	END

	-- Apply optional MinFee/MaxFee caps
	IF @MinFee IS NOT NULL AND @Result < @MinFee
		SET @Result = @MinFee;
	IF @MaxFee IS NOT NULL AND @Result > @MaxFee
		SET @Result = @MaxFee;

	RETURN @Result;
END
GO

-- Example usage:
-- SELECT dbo.ufn_GetLoanService(900000.00, 'monthly', NULL); -- returns 1000 if sample seed exists
-- SELECT dbo.ufn_GetLoanService(2000000.00, 'monthly', NULL); -- returns loanAmt * 0.015
