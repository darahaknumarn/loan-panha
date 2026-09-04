-- Migration: 0003_update_sp_repay1.sql
-- Purpose: Example ALTER PROC to integrate dynamic LD_Service calculation into sp_repay1 (and/or sp_repay)
-- IMPORTANT: This is a template. You must adapt parameter names and placement to match the real sp_repay1 definition in your database.

/*
Typical approach to integrate:

1) Find where sp_repay1 currently computes/prices schedule values and declare a variable for LD_Service.
2) Replace any hard-coded service amount logic with a call to dbo.ufn_GetLoanService(@LoanAmt, @LoanUnit, @BranchID)
   or use values returned by sp_repay1 (if it already returns loan amount and unit) to compute service.
3) Ensure the final SELECT returned by sp_repay1 includes LD_Service (column name matching application expectations).

Below is a safe template. DO NOT execute blindly — inspect your current sp_repay1 and merge the highlighted parts.
*/

-- Example: get current definition (for review)
-- EXEC sp_helptext 'sp_repay1'

-- Template to ALTER existing procedure: replace the body where appropriate
-- (This script will not drop your real sp_repay1; instead it shows the pattern to apply.)

PRINT '--- BEGIN: sp_repay1 integration template ---';

/*
ALTER PROCEDURE dbo.sp_repay1
	@LD_ID INT,
	@BranchID NVARCHAR(50),
	@DateToPay DATE,
	@SomeOtherParam INT -- adjust per your actual signature
AS
BEGIN
	SET NOCOUNT ON;

	-- existing local variables
	DECLARE @LoanAmt DECIMAL(18,2) = 0; -- set from your BK_Loan column (e.g. LD_Amt or LD_LoanAmount)
	DECLARE @LoanUnit NVARCHAR(20) = NULL; -- set from your BK_Loan column (e.g. LD_Unit or frequency)
	DECLARE @LD_Service DECIMAL(18,2) = 0;

	-- populate @LoanAmt and @LoanUnit from BK_Loan table (adjust column names as needed):
	SELECT TOP 1 @LoanAmt = ISNULL(LD_Amt, LD_Amount, 0), @LoanUnit = ISNULL(LD_Unit, '')
	FROM BK_Loan
	WHERE LD_ID = @LD_ID AND LD_BrId = @BranchID;

	-- Compute dynamic service using the new function
	SET @LD_Service = dbo.ufn_GetLoanService(@LoanAmt, @LoanUnit, @BranchID);

	-- Existing logic that computes prn/int and other values should continue. Replace any hard-coded service
	-- assignments with @LD_Service so final resultset includes the dynamic fee.

	-- Example final select (adjust columns/order to match original sp_repay1 return schema):
	SELECT
		@LD_ID AS LD_ID,
		-- ... other fields ...
		@LoanAmt AS LoanAmount,
		@LD_Service AS LD_Service
		-- ... rest of fields sp_repay1 returns ...
END
*/

PRINT '--- END: sp_repay1 integration template. Review and merge into your DB. ---';
