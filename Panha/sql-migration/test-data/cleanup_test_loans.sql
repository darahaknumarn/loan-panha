-- Removes the factory-schedule test loans inserted by insert_test_loans.sql
USE [Panha];
DELETE FROM BK_LoanSchedule WHERE LD_ID BETWEEN '900001' AND '900011' AND SH_BrId = '001' AND User_Create = N'claude-test';
DELETE FROM BK_Loan WHERE LD_ID BETWEEN '900001' AND '900011' AND LD_BrId = '001' AND LD_User_Create = N'claude-test';
SELECT @@ROWCOUNT AS loans_removed;
