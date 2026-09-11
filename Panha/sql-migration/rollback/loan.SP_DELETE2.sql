

CREATE PROCEDURE [dbo].[SP_DELETE2]
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
---------------------------------------Syn Record which deleted by user at Export Machine------------------------------------------
-- New Table
DELETE Loan.dbo.BK_Exchange
FROM Loan.dbo.BK_Exchange A 
INNER JOIN TempLoan.dbo.TRACE_Exchange B 
ON A.ID = B.ID AND A.BrId = B.BrId 
AND B.RecordAction='DELETE' AND ((A.Date_Modify IS NULL AND B.DateAction > A.Date_Create) 
OR (NOT A.Date_Modify IS NULL AND B.DateAction > A.Date_Modify));

DELETE Loan.dbo.BK_CustomerOther
FROM Loan.dbo.BK_CustomerOther A 
INNER JOIN TempLoan.dbo. TRACE_CustomerOther B 
ON A.ID = B.ID AND A.CM_BrId = B.CM_BrId 
AND B.RecordAction='DELETE' AND ((A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Create) 
OR (NOT A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Modify));

----DELETE Loan.dbo.BK_LocationOther
----FROM Loan.dbo.BK_LocationOther A 
----INNER JOIN TempLoan.dbo. Trace_locationOther B 
----ON A.ID = B.ID AND A.CM_BrId = B.CM_BrId 
----AND B.RecordAction='DELETE' AND ((A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Create) 
----OR (NOT A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Modify));

DELETE Loan.dbo.Bank_Transaction
FROM Loan.dbo.Bank_Transaction A 
INNER JOIN TempLoan.dbo. TRACE_Bank B 
ON A.ID = B.ID AND A.BrId = B.BrId 
AND B.RecordAction='DELETE' AND ((A.Date_Modify IS NULL AND B.DateAction > A.Date_Create) 
OR (NOT A.Date_Modify IS NULL AND B.DateAction > A.Date_Modify));

DELETE Loan.dbo.BK_Location 
FROM Loan.dbo.BK_Location A 
INNER JOIN TempLoan.dbo.TRACE_Location B 
ON A.LO_ID = B.LO_ID AND A.LO_BrID = B.LO_BrID 
AND B.RecordAction='DELETE' AND ((A.LO_Date_Modify IS NULL AND B.DateAction > A.LO_Date_Create) 
OR (NOT A.LO_Date_Modify IS NULL AND B.DateAction > A.LO_Date_Modify));
DELETE Loan.dbo.BK_Customer FROM Loan.dbo.BK_Customer A INNER JOIN TempLoan.dbo.TRACE_Customer B ON A.CM_ID = B.CM_ID AND A.CM_BrId = B.CM_BrId AND B.RecordAction='DELETE' AND ((A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Create) OR (NOT A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Modify));
DELETE Loan.dbo.BK_Loan FROM Loan.dbo.BK_Loan A INNER JOIN TempLoan.dbo.TRACE_Loan B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId AND B.RecordAction='DELETE' AND ((A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Create)OR (NOT A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Modify));
DELETE Loan.dbo.BK_LoanRepay 
FROM Loan.dbo.BK_LoanRepay A 
INNER JOIN TempLoan.dbo.TRACE_LoanRepay B 
ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.SH_Date = B.SH_Date 
AND format (A.LR_Date_Create,'mm/dd/yy, hh:mm:ss') = format (B.LR_Date_Create ,'mm/dd/yy, hh:mm:ss')
AND B.RecordAction='DELETE' 
AND ((A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Create) OR (NOT A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Modify));
DELETE Loan.dbo.Asset FROM Loan.dbo.Asset A INNER JOIN TempLoan.dbo.TRACE_Asset B ON A.ASID = B.ASID AND A.BrID = B.BrID AND B.RecordAction='DELETE' AND ((A.Date_Modify IS NULL AND B.DateAction > A.Date_Create) OR (NOT A.Date_Modify IS NULL AND B.DateAction > A.Date_Modify));
DELETE Loan.dbo.ExpenseOperation FROM Loan.dbo.ExpenseOperation A INNER JOIN TempLoan.dbo.TRACE_ExpenseOperation B ON A.ASID = B.ASID AND A.BrID = B.BrID AND A.OPCode = B.OPCode AND B.RecordAction='DELETE' AND ((A.Date_Modify IS NULL AND B.DateAction > A.Date_Create) OR (NOT A.Date_Modify IS NULL AND B.DateAction > A.Date_Modify));
DELETE Loan.dbo.ExpenseSchedule FROM Loan.dbo.ExpenseSchedule A INNER JOIN TempLoan.dbo.TRACE_ExpenseSchedule B ON A.OPCode = B.OPCode AND A.BrID = B.BrID AND A.Date_Create=B.Date_Create AND B.RecordAction='DELETE';
DELETE Loan.dbo.BK_LoanSchedule FROM Loan.dbo.BK_LoanSchedule A INNER JOIN TempLoan.dbo.TRACE_LoanSchedule B ON A.LD_ID = B.LD_ID AND A.SH_BrId = B.SH_BrId AND A.SH_Date=B.SH_Date AND A.Date_Create=B.Date_Create AND B.RecordAction='DELETE';
DELETE Loan.dbo.OwnerTransaction From Loan.dbo.OwnerTransaction a Inner Join TempLoan.dbo.TRACE_OwnerTransaction b on a.OPID = b.OPID and a.BrID = b.BrID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Loan.dbo.Writeoff From Loan.dbo.Writeoff a Inner Join TempLoan.dbo.TRACE_Writeoff b on a.LD_ID = b.LD_ID and a.BR_ID = b.BR_ID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Loan.dbo.ProfitCutout From Loan.dbo.ProfitCutout a Inner Join TempLoan.dbo.TRACE_ProfitCutout b on a.OPID = b.OPID and a.BrID = b.BrID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Loan.dbo.OtherDeposit FROM Loan.dbo.OtherDeposit A INNER JOIN TempLoan.dbo.TRACE_OtherDeposit B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId AND B.RecordAction='DELETE' AND ((A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Create)OR (NOT A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Modify));
DELETE Loan.dbo.OtherDepositRepay FROM Loan.dbo.OtherDepositRepay A INNER JOIN TempLoan.dbo.TRACE_OtherDepositRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.SH_Date = B.SH_Date AND B.RecordAction='DELETE' AND ((A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Create) OR (NOT A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Modify));
DELETE Loan.dbo.OtherIncome From Loan.dbo.OtherIncome a Inner Join TempLoan.dbo.TRACE_OtherIncome b on a.OPID = b.OPID and a.BrID = b.BrID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Loan.dbo.BK_SavingRepay FROM Loan.dbo.BK_SavingRepay A INNER JOIN TempLoan.dbo.TRACE_SavingRepay B ON A.LD_ID = B.LD_ID AND A.SR_BrID = B.SR_BrID AND A.SR_Date_Create = B.SR_Date_Create AND B.RecordAction='DELETE' AND ((A.SR_Date_Modify IS NULL AND B.DateAction > A.SR_Date_Create) OR (NOT A.SR_Date_Modify IS NULL AND B.DateAction > A.SR_Date_Modify));
DELETE Loan.dbo.BK_OtherIncome FROM Loan.dbo.BK_OtherIncome A INNER JOIN TempLoan.dbo.Trace_OtherIncome B ON A.BrId = B.BrId AND A.ID = B.ID AND A.Date_Create = B.Date_Create AND B.RecordAction='DELETE' AND ((A.Date_Modify IS NULL AND B.DateAction > A.Date_Create) OR (NOT A.Date_Modify IS NULL AND B.DateAction > A.Date_Modify));
----------------------------------------------------------
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







