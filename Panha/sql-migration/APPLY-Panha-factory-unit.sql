-- Panha: add the factory loan unit (LU_ID 5, "រោងចក្រ") used by the factory-payday schedule
-- (installments on the 10th and 25th; see KTV/Admin/FactorySchedule.vb).
-- Run with: sqlcmd -S lpc:.\SQLEXPRESS -E -C -f 65001 -i APPLY-Panha-factory-unit.sql
-- Rollback: DELETE FROM BK_LoanUnit WHERE LU_ID = 5 AND LU_Name = N'រោងចក្រ';
--           (only once no BK_Loan row has LD_Unit = N'រោងចក្រ')
USE [Panha];
GO
IF NOT EXISTS (SELECT 1 FROM dbo.BK_LoanUnit WHERE LU_ID = 5)
    INSERT INTO dbo.BK_LoanUnit (LU_ID, LU_Name) VALUES (5, N'រោងចក្រ');
ELSE IF NOT EXISTS (SELECT 1 FROM dbo.BK_LoanUnit WHERE LU_ID = 5 AND LU_Name = N'រោងចក្រ')
    RAISERROR ('BK_LoanUnit already has LU_ID 5 with a different name; the app expects it to be the factory unit.', 16, 1);
GO
SELECT LU_ID, LU_Name FROM dbo.BK_LoanUnit ORDER BY LU_ID;
GO
