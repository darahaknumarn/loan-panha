



CREATE PROCEDURE [dbo].[SP_DELETE1]
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
----------------------------------------DELETE EXISTING RECORD BEFORE INSERT------------------------------------------
DELETE Loan.dbo.BK_Location FROM Loan.dbo.BK_Location A INNER JOIN TempLoan.dbo.BK_Location B ON A.LO_ID = B.LO_ID AND A.LO_BrID = B.LO_BrID;
DELETE Loan.dbo.BK_Customer FROM Loan.dbo.BK_Customer A INNER JOIN TempLoan.dbo.BK_Customer B ON A.CM_ID = B.CM_ID AND A.CM_BrId = B.CM_BrId; 
DELETE Loan.dbo.BK_Position FROM Loan.dbo.BK_Position A INNER JOIN TempLoan.dbo.BK_Position B ON A.ID = B.ID AND A.BrID = B.BrID;
DELETE Loan.dbo.BK_Employee FROM Loan.dbo.BK_Employee A INNER JOIN TempLoan.dbo.BK_Employee B ON A.EM_ID = B.EM_ID AND A.EM_BrID = B.EM_BrID;
DELETE Loan.dbo.BK_Loan FROM Loan.dbo.BK_Loan A INNER JOIN TempLoan.dbo.BK_Loan B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId;
DELETE Loan.dbo.BK_LoanRepay FROM Loan.dbo.BK_LoanRepay A INNER JOIN TempLoan.dbo.BK_LoanRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.SH_Date = B.SH_Date AND A.LR_Date_Create = B.LR_Date_Create;
DELETE Loan.dbo.BK_LoanSchedule FROM Loan.dbo.BK_LoanSchedule A INNER JOIN TempLoan.dbo.BK_LoanSchedule B ON A.LD_ID = B.LD_ID AND A.SH_BrId = B.SH_BrId AND A.SH_Date =B.SH_Date;
DELETE Loan.dbo.Asset From Loan.dbo.Asset A INNER JOIN TempLoan.dbo.Asset B on A.ASID = B.ASID AND A.BrID = B.BrID;
DELETE Loan.dbo.ExpenseOperation From Loan.dbo.ExpenseOperation A INNER JOIN TempLoan.dbo.ExpenseOperation B ON A.OPCode = B.OPCode AND A.BrID = B.BrID;
DELETE Loan.dbo.ExpenseSchedule From Loan.dbo.ExpenseSchedule A INNER JOIN TempLoan.dbo.ExpenseSchedule B ON A.OPCode = B.OPCode AND A.ExDate = B.ExDate AND A.BrID = B.BrID;
DELETE Loan.dbo.OwnerTransaction From Loan.dbo.OwnerTransaction a INNER JOIN TempLoan.dbo.OwnerTransaction b on a.OPID = b.OPID and a.BrID = b.BrID;
DELETE Loan.dbo.Writeoff From Loan.dbo.Writeoff a INNER JOIN TempLoan.dbo.Writeoff b on a.LD_ID = b.LD_ID and a.BR_ID = b.BR_ID;
DELETE Loan.dbo.ProfitCutout From Loan.dbo.ProfitCutout a INNER JOIN TempLoan.dbo.ProfitCutout b on a.OPID = b.OPID and a.BrID = b.BrID;
DELETE Loan.dbo.OtherDeposit FROM Loan.dbo.OtherDeposit A INNER JOIN TempLoan.dbo.OtherDeposit B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId;
DELETE Loan.dbo.OtherDepositRepay FROM Loan.dbo.OtherDepositRepay A INNER JOIN TempLoan.dbo.OtherDepositRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.LR_ID = B.LR_ID;
DELETE Loan.dbo.OtherIncome From Loan.dbo.OtherIncome a INNER JOIN TempLoan.dbo.OtherIncome b on a.OPID = b.OPID and a.BrID = b.BrID;
DELETE Loan.dbo.BK_SavingRepay From Loan.dbo.BK_SavingRepay a INNER JOIN TempLoan.dbo.BK_SavingRepay b on a.LD_ID= b.LD_ID and a.SR_BrID=b.SR_BrID and a.SR_Date_Create=b.SR_Date_Create;
------- New table
--- Exchange
DELETE Loan.dbo.BK_Exchange
FROM Loan.dbo.BK_Exchange A 
INNER JOIN TempLoan.dbo.BK_Exchange B ON A.ID = B.ID AND A.BrId = B.BrId; 
-- Customer Other
DELETE Loan.dbo.BK_CustomerOther 
FROM Loan.dbo.BK_CustomerOther A 
INNER JOIN TempLoan.dbo.BK_CustomerOther B ON A.CM_ID = B.CM_ID AND A.CM_BrId = B.CM_BrId; 
--- Change Customer
DELETE Loan.dbo.BK_ChangeCustomer
FROM Loan.dbo.BK_ChangeCustomer A 
INNER JOIN TempLoan.dbo.BK_ChangeCustomer B ON A.CM_ID = B.CM_ID AND A.BrId = B.BrId; 
-- Location Other
DELETE Loan.dbo.BK_LocationOther
FROM Loan.dbo.BK_LocationOther A 
INNER JOIN TempLoan.dbo.BK_LocationOther B ON A.LO_ID = B.LO_ID AND A.LO_BrID = B.LO_BrID; 
-- Bank Transaction
DELETE Loan.dbo.Bank_Transaction
FROM Loan.dbo.Bank_Transaction A 
INNER JOIN TempLoan.dbo.Bank_Transaction B ON A.ID = B.ID AND A.BrId = B.BrId; 
-- Bank BK_OtherIncome
DELETE Loan.dbo.BK_OtherIncome
FROM Loan.dbo.BK_OtherIncome A 
INNER JOIN TempLoan.dbo.BK_OtherIncome B ON A.ID = B.ID AND A.BrId = B.BrId; 
DECLARE @STRDATE AS VARCHAR(12);
DECLARE @BRACNH AS VARCHAR(12);
--SELECT @STRDATE=[Value] FROM TempLoan.dbo.SYS_IMPORT_EXPORT WHERE ID='EXPORT_DATE';
--SELECT @BRACNH=[Value] FROM TempLoan.dbo.SYS_IMPORT_EXPORT WHERE ID='BRANCH';
-- New table
DELETE FROM Loan.dbo.TRACE_Exchange 
WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND BrId=@BRACNH;
DELETE FROM Loan.dbo.TRACE_CustomerOther 
WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND CM_BrId=@BRACNH;
DELETE FROM Loan.dbo.TRACE_Bank
WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND BrId=@BRACNH;
--DELETE FROM Loan.dbo.TRACE
--WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND BrId=@BRACNH;

DELETE FROM Loan.dbo.TRACE_Customer WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND CM_BrId=@BRACNH;
DELETE FROM Loan.dbo.TRACE_Loan WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND LD_BrId = @BRACNH ;
DELETE FROM Loan.dbo.TRACE_LoanRepay WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND LR_BrID=@BRACNH;
DELETE FROM Loan.dbo.TRACE_Location WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND LO_BrID=@BRACNH;
DELETE FROM Loan.dbo.TRACE_Asset WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Loan.dbo.TRACE_ExpenseOperation WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Loan.dbo.TRACE_ExpenseSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Loan.dbo.TRACE_LoanSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND SH_BrId=@BRACNH;
DELETE FROM Loan.dbo.TRACE_OwnerTransaction Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Loan.dbo.TRACE_Writeoff Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BR_ID=@BRACNH;
DELETE FROM Loan.dbo.TRACE_ProfitCutout Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Loan.dbo.TRACE_OtherDeposit WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND LD_BrId = @BRACNH ;
DELETE FROM Loan.dbo.TRACE_OtherDepositRepay WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND LR_BrID=@BRACNH;
DELETE Loan.dbo.sys_User FROM Loan.dbo.sys_User A INNER JOIN TempLoan.dbo.sys_User B ON A.[User_Name] = B.[User_Name] AND A.BrID = B.BrID; 
DELETE FROM Loan.dbo.TRACE_OtherIncome Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Loan.dbo.TRACE_SavingRepay 
WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND SR_BrID=@BRACNH;

DELETE FROM Loan.dbo.TRACE_OtherIncome 
WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND BrID=@BRACNH;

-----------------------------------------------------------
END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0
	ROLLBACK TRANSACTION;
	DECLARE @errmsg NVARCHAR(4000);
	DECLARE @errseverity INT;
	DECLARE @errstate INT;
	SELECT @errmsg = ERROR_MESSAGE(), @errseverity = ERROR_SEVERITY(), @errstate = ERROR_STATE();
	RAISERROR(@errmsg, @errseverity, @errstate);
	PRINT '';
	PRINT '??????????DELETE ERROR | DELETE ERROR | DELETE ERROR | DELETE ERROR??????????';
	PRINT '';
END CATCH;

IF @@TRANCOUNT > 0
    COMMIT TRANSACTION;
END





