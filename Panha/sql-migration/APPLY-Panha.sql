-- Cross-database reference fixes for Panha
-- Applied 2026-09-11. Rollback scripts live in sql-migration/rollback/
USE [Panha];
GO

-- ===== SP_DELETE =====
ALTER PROCEDURE [dbo].[SP_DELETE]
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
----------------------------------------DELETE EXISTING RECORD BEFORE INSERT------------------------------------------
	DECLARE @ID NVARCHAR(50)
	DECLARE @BrID VARCHAR(50)
	DECLARE @SH_Date DateTime
--Location
	DECLARE Curr CURSOR FOR
	SELECT LO_ID,LO_BrID FROM TempPanha.dbo.BK_Location WHERE NOT LO_Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Panha.dbo.BK_Location WHERE LO_ID=@ID AND LO_BrID=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Customer
	DECLARE Curr CURSOR FOR
	SELECT CM_ID,CM_BrId FROM TempPanha.dbo.BK_Customer WHERE NOT CM_Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Panha.dbo.BK_Customer WHERE CM_ID=@ID AND CM_BrId=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Position
	DECLARE Curr CURSOR FOR
	SELECT ID,BrID FROM TempPanha.dbo.BK_Position WHERE NOT Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Panha.dbo.BK_Position WHERE ID=@ID AND BrID=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Employee
	DECLARE Curr CURSOR FOR
	SELECT EM_ID,EM_BrID FROM TempPanha.dbo.BK_Employee WHERE NOT Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Panha.dbo.BK_Employee WHERE EM_ID=@ID AND EM_BrID=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Loan
	DECLARE Curr CURSOR FOR
	SELECT LD_ID,LD_BrId FROM TempPanha.dbo.BK_Loan WHERE NOT LD_Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Panha.dbo.BK_Loan WHERE LD_ID=@ID AND LD_BrId=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Loan Repay
	DECLARE Curr CURSOR FOR
	SELECT LD_ID,LR_BrID,SH_Date FROM TempPanha.dbo.BK_LoanRepay WHERE NOT LR_Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID,@SH_Date
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Panha.dbo.BK_LoanRepay WHERE LD_ID=@ID AND LR_BrID=@BrID AND SH_Date = @SH_Date
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID,@SH_Date
	END
	CLOSE Curr
	DEALLOCATE Curr
--Loan Schedule
	DECLARE Curr CURSOR FOR
	SELECT LD_ID,SH_BrId,SH_Date FROM TempPanha.dbo.BK_LoanSchedule WHERE NOT Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID,@SH_Date
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Panha.dbo.BK_LoanSchedule WHERE LD_ID=@ID AND SH_BrId=@BrID AND SH_Date = @SH_Date
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID,@SH_Date
	END
	CLOSE Curr
	DEALLOCATE Curr
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







GO

-- ===== SP_DELETE1 =====
ALTER PROCEDURE [dbo].[SP_DELETE1]
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
----------------------------------------DELETE EXISTING RECORD BEFORE INSERT------------------------------------------
DELETE Panha.dbo.BK_Location FROM Panha.dbo.BK_Location A INNER JOIN TempPanha.dbo.BK_Location B ON A.LO_ID = B.LO_ID AND A.LO_BrID = B.LO_BrID;
DELETE Panha.dbo.BK_Customer FROM Panha.dbo.BK_Customer A INNER JOIN TempPanha.dbo.BK_Customer B ON A.CM_ID = B.CM_ID AND A.CM_BrId = B.CM_BrId; 
DELETE Panha.dbo.BK_Position FROM Panha.dbo.BK_Position A INNER JOIN TempPanha.dbo.BK_Position B ON A.ID = B.ID AND A.BrID = B.BrID;
DELETE Panha.dbo.BK_Employee FROM Panha.dbo.BK_Employee A INNER JOIN TempPanha.dbo.BK_Employee B ON A.EM_ID = B.EM_ID AND A.EM_BrID = B.EM_BrID;
DELETE Panha.dbo.BK_Loan FROM Panha.dbo.BK_Loan A INNER JOIN TempPanha.dbo.BK_Loan B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId;
DELETE Panha.dbo.BK_LoanRepay FROM Panha.dbo.BK_LoanRepay A INNER JOIN TempPanha.dbo.BK_LoanRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.SH_Date = B.SH_Date AND A.LR_Date_Create = B.LR_Date_Create;
DELETE Panha.dbo.BK_LoanSchedule FROM Panha.dbo.BK_LoanSchedule A INNER JOIN TempPanha.dbo.BK_LoanSchedule B ON A.LD_ID = B.LD_ID AND A.SH_BrId = B.SH_BrId AND A.SH_Date =B.SH_Date;
DELETE Panha.dbo.Asset From Panha.dbo.Asset A INNER JOIN TempPanha.dbo.Asset B on A.ASID = B.ASID AND A.BrID = B.BrID;
DELETE Panha.dbo.ExpenseOperation From Panha.dbo.ExpenseOperation A INNER JOIN TempPanha.dbo.ExpenseOperation B ON A.OPCode = B.OPCode AND A.BrID = B.BrID;
DELETE Panha.dbo.ExpenseSchedule From Panha.dbo.ExpenseSchedule A INNER JOIN TempPanha.dbo.ExpenseSchedule B ON A.OPCode = B.OPCode AND A.ExDate = B.ExDate AND A.BrID = B.BrID;
DELETE Panha.dbo.OwnerTransaction From Panha.dbo.OwnerTransaction a INNER JOIN TempPanha.dbo.OwnerTransaction b on a.OPID = b.OPID and a.BrID = b.BrID;
DELETE Panha.dbo.Writeoff From Panha.dbo.Writeoff a INNER JOIN TempPanha.dbo.Writeoff b on a.LD_ID = b.LD_ID and a.BR_ID = b.BR_ID;
DELETE Panha.dbo.ProfitCutout From Panha.dbo.ProfitCutout a INNER JOIN TempPanha.dbo.ProfitCutout b on a.OPID = b.OPID and a.BrID = b.BrID;
DELETE Panha.dbo.OtherDeposit FROM Panha.dbo.OtherDeposit A INNER JOIN TempPanha.dbo.OtherDeposit B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId;
DELETE Panha.dbo.OtherDepositRepay FROM Panha.dbo.OtherDepositRepay A INNER JOIN TempPanha.dbo.OtherDepositRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.LR_ID = B.LR_ID;
DELETE Panha.dbo.OtherIncome From Panha.dbo.OtherIncome a INNER JOIN TempPanha.dbo.OtherIncome b on a.OPID = b.OPID and a.BrID = b.BrID;
DELETE Panha.dbo.BK_SavingRepay From Panha.dbo.BK_SavingRepay a INNER JOIN TempPanha.dbo.BK_SavingRepay b on a.LD_ID= b.LD_ID and a.SR_BrID=b.SR_BrID and a.SR_Date_Create=b.SR_Date_Create;


DECLARE @STRDATE AS VARCHAR(12);
DECLARE @BRACNH AS VARCHAR(12);
SELECT @STRDATE=[Value] FROM TempPanha.dbo.SYS_IMPORT_EXPORT WHERE ID='EXPORT_DATE';
SELECT @BRACNH=[Value] FROM TempPanha.dbo.SYS_IMPORT_EXPORT WHERE ID='BRANCH';
DELETE FROM Panha.dbo.TRACE_Customer WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND CM_BrId=@BRACNH;
DELETE FROM Panha.dbo.TRACE_Loan WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND LD_BrId = @BRACNH ;
DELETE FROM Panha.dbo.TRACE_LoanRepay WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND LR_BrID=@BRACNH;
DELETE FROM Panha.dbo.TRACE_Location WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND LO_BrID=@BRACNH;
DELETE FROM Panha.dbo.TRACE_Asset WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Panha.dbo.TRACE_ExpenseOperation WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Panha.dbo.TRACE_ExpenseSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Panha.dbo.TRACE_LoanSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND SH_BrId=@BRACNH;
DELETE FROM Panha.dbo.TRACE_OwnerTransaction Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Panha.dbo.TRACE_Writeoff Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BR_ID=@BRACNH;
DELETE FROM Panha.dbo.TRACE_ProfitCutout Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Panha.dbo.TRACE_OtherDeposit WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND LD_BrId = @BRACNH ;
DELETE FROM Panha.dbo.TRACE_OtherDepositRepay WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND LR_BrID=@BRACNH;
DELETE Panha.dbo.sys_User FROM Panha.dbo.sys_User A INNER JOIN TempPanha.dbo.sys_User B ON A.[User_Name] = B.[User_Name] AND A.BrID = B.BrID; 
DELETE FROM Panha.dbo.TRACE_OtherIncome Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Panha.dbo.TRACE_SavingRepay WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND SR_BrID=@BRACNH;

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





GO

-- ===== SP_DELETE2 =====
ALTER PROCEDURE [dbo].[SP_DELETE2]
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
---------------------------------------Syn Record which deleted by user at Export Machine------------------------------------------
DELETE Panha.dbo.BK_Location FROM Panha.dbo.BK_Location A INNER JOIN TempPanha.dbo.TRACE_Location B ON A.LO_ID = B.LO_ID AND A.LO_BrID = B.LO_BrID AND B.RecordAction='DELETE' AND ((A.LO_Date_Modify IS NULL AND B.DateAction > A.LO_Date_Create) OR (NOT A.LO_Date_Modify IS NULL AND B.DateAction > A.LO_Date_Modify));
DELETE Panha.dbo.BK_Customer FROM Panha.dbo.BK_Customer A INNER JOIN TempPanha.dbo.TRACE_Customer B ON A.CM_ID = B.CM_ID AND A.CM_BrId = B.CM_BrId AND B.RecordAction='DELETE' AND ((A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Create) OR (NOT A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Modify));
DELETE Panha.dbo.BK_Loan FROM Panha.dbo.BK_Loan A INNER JOIN TempPanha.dbo.TRACE_Loan B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId AND B.RecordAction='DELETE' AND ((A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Create)OR (NOT A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Modify));
DELETE Panha.dbo.BK_LoanRepay FROM Panha.dbo.BK_LoanRepay A INNER JOIN TempPanha.dbo.TRACE_LoanRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.SH_Date = B.SH_Date AND A.LR_Date_Create = B.LR_Date_Create AND B.RecordAction='DELETE' AND ((A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Create) OR (NOT A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Modify));
DELETE Panha.dbo.Asset FROM Panha.dbo.Asset A INNER JOIN TempPanha.dbo.TRACE_Asset B ON A.ASID = B.ASID AND A.BrID = B.BrID AND B.RecordAction='DELETE' AND ((A.Date_Modify IS NULL AND B.DateAction > A.Date_Create) OR (NOT A.Date_Modify IS NULL AND B.DateAction > A.Date_Modify));
DELETE Panha.dbo.ExpenseOperation FROM Panha.dbo.ExpenseOperation A INNER JOIN TempPanha.dbo.TRACE_ExpenseOperation B ON A.ASID = B.ASID AND A.BrID = B.BrID AND A.OPCode = B.OPCode AND B.RecordAction='DELETE' AND ((A.Date_Modify IS NULL AND B.DateAction > A.Date_Create) OR (NOT A.Date_Modify IS NULL AND B.DateAction > A.Date_Modify));
DELETE Panha.dbo.ExpenseSchedule FROM Panha.dbo.ExpenseSchedule A INNER JOIN TempPanha.dbo.TRACE_ExpenseSchedule B ON A.OPCode = B.OPCode AND A.BrID = B.BrID AND A.Date_Create=B.Date_Create AND B.RecordAction='DELETE';
DELETE Panha.dbo.BK_LoanSchedule FROM Panha.dbo.BK_LoanSchedule A INNER JOIN TempPanha.dbo.TRACE_LoanSchedule B ON A.LD_ID = B.LD_ID AND A.SH_BrId = B.SH_BrId AND A.SH_Date=B.SH_Date AND A.Date_Create=B.Date_Create AND B.RecordAction='DELETE';
DELETE Panha.dbo.OwnerTransaction From Panha.dbo.OwnerTransaction a Inner Join TempPanha.dbo.TRACE_OwnerTransaction b on a.OPID = b.OPID and a.BrID = b.BrID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Panha.dbo.Writeoff From Panha.dbo.Writeoff a Inner Join TempPanha.dbo.TRACE_Writeoff b on a.LD_ID = b.LD_ID and a.BR_ID = b.BR_ID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Panha.dbo.ProfitCutout From Panha.dbo.ProfitCutout a Inner Join TempPanha.dbo.TRACE_ProfitCutout b on a.OPID = b.OPID and a.BrID = b.BrID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Panha.dbo.OtherDeposit FROM Panha.dbo.OtherDeposit A INNER JOIN TempPanha.dbo.TRACE_OtherDeposit B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId AND B.RecordAction='DELETE' AND ((A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Create)OR (NOT A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Modify));
DELETE Panha.dbo.OtherDepositRepay FROM Panha.dbo.OtherDepositRepay A INNER JOIN TempPanha.dbo.TRACE_OtherDepositRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.SH_Date = B.SH_Date AND B.RecordAction='DELETE' AND ((A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Create) OR (NOT A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Modify));
DELETE Panha.dbo.OtherIncome From Panha.dbo.OtherIncome a Inner Join TempPanha.dbo.TRACE_OtherIncome b on a.OPID = b.OPID and a.BrID = b.BrID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Panha.dbo.BK_SavingRepay FROM Panha.dbo.BK_SavingRepay A INNER JOIN TempPanha.dbo.TRACE_SavingRepay B ON A.LD_ID = B.LD_ID AND A.SR_BrID = B.SR_BrID AND A.SR_Date_Create = B.SR_Date_Create AND B.RecordAction='DELETE' AND ((A.SR_Date_Modify IS NULL AND B.DateAction > A.SR_Date_Create) OR (NOT A.SR_Date_Modify IS NULL AND B.DateAction > A.SR_Date_Modify));
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







GO

-- ===== SP_IMPORT =====
ALTER PROCEDURE [dbo].[SP_IMPORT]
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY

	PRINT 'Import Location.';
	INSERT INTO Panha.dbo.BK_Location SELECT * FROM TempPanha.dbo.BK_Location;
    
    PRINT 'Import customer.';
	INSERT INTO Panha.dbo.BK_Customer SELECT * FROM TempPanha.dbo.BK_Customer;
    
    PRINT 'Import position.';
	INSERT INTO Panha.dbo.BK_Position SELECT * FROM TempPanha.dbo.BK_Position;

	PRINT 'Import employee.';
	INSERT INTO Panha.dbo.BK_Employee SELECT * FROM TempPanha.dbo.BK_Employee;
    
    PRINT 'Import loan.';
	INSERT INTO Panha.dbo.BK_Loan SELECT * FROM TempPanha.dbo.BK_Loan;
	
	PRINT 'Import loan repay.';
	INSERT INTO Panha.dbo.BK_LoanRepay([LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport] FROM [TempPanha].[dbo].[BK_LoanRepay];
	
	PRINT 'Import loan schedule.';
	INSERT INTO Panha.dbo.BK_LoanSchedule([LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt]) 
	SELECT [LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt] FROM [TempPanha].[dbo].[BK_LoanSchedule];
	
	PRINT 'Import trace customer';
    INSERT INTO [Panha].[dbo].[TRACE_Customer]([DateAction],[RecordAction],[CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle])
	SELECT [DateAction],[RecordAction],[CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle] FROM [TempPanha].[dbo].[TRACE_Customer];
	
	Print 'Import trace loan.';
	INSERT INTO [Panha].[dbo].[TRACE_Loan]([DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate])
	SELECT [DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate] FROM [TempPanha].[dbo].[TRACE_Loan];

	Print 'Import trace loan repay.';
	INSERT INTO [Panha].[dbo].[TRACE_LoanRepay]([LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport] FROM [TempPanha].[dbo].[TRACE_LoanRepay];

	Print 'Import trace location.';
	INSERT INTO [Panha].[dbo].[TRACE_Location]([DateAction],[RecordAction],[LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete])
	SELECT [DateAction],[RecordAction],[LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete] FROM [TempPanha].[dbo].[TRACE_Location];
	
	Print 'Import table user.';
	INSERT INTO [Panha].[dbo].[sys_User]	SELECT * FROM [TempPanha].[dbo].[sys_User] ; 
	
	Print 'Import asset table.';
	INSERT INTO Panha.dbo.Asset SELECT * FROM TempPanha.dbo.Asset;
	
	Print 'Import expense operation.';
	INSERT INTO [Panha].[dbo].[ExpenseOperation]([BrID],[OPDate],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [BrID],[OPDate],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete] FROM [TempPanha].[dbo].[ExpenseOperation] ;

	Print 'Import expense schedule.';
	INSERT INTO Panha.dbo.ExpenseSchedule SELECT * FROM TempPanha.dbo.ExpenseSchedule;
	
	Print 'Import trace asset table.';
	INSERT INTO Panha.dbo.TRACE_Asset SELECT * FROM TempPanha.dbo.TRACE_Asset;
	
	Print 'Import trace expense operation.';
	INSERT INTO [Panha].[dbo].[TRACE_ExpenseOperation] SELECT * FROM [TempPanha].[dbo].[TRACE_ExpenseOperation] ;
	
	Print 'Import trace loan schedule table.';
	INSERT INTO Panha.dbo.TRACE_LoanSchedule SELECT * FROM TempPanha.dbo.TRACE_LoanSchedule;
	
	Print 'Import trace expense schedule.';
	INSERT INTO [Panha].[dbo].[TRACE_ExpenseSchedule] SELECT * FROM [TempPanha].[dbo].[TRACE_ExpenseSchedule] ;
	
	Print 'Import owner transaction';
	Insert Into Panha.dbo.OwnerTransaction([BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]) Select [BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete] From TempPanha.dbo.OwnerTransaction;
	
	Print 'Import trace owner transaction';
	Insert Into Panha.dbo.TRACE_OwnerTransaction select * From TempPanha.dbo.TRACE_OwnerTransaction;
	
	Print 'Import Writeoff';
	Insert Into Panha.dbo.Writeoff Select * From TempPanha.dbo.Writeoff;
	
	Print 'Import trace Writeoff';
	Insert Into Panha.dbo.TRACE_Writeoff select * From TempPanha.dbo.TRACE_Writeoff;

	Print 'Import Profit Cutout';
	Insert Into Panha.dbo.ProfitCutout Select * From TempPanha.dbo.ProfitCutout;
	
	Print 'Import trace Profit Cutout';
	Insert Into Panha.dbo.TRACE_ProfitCutout select * From TempPanha.dbo.TRACE_ProfitCutout;

	PRINT 'Import Other Deposit.';
	INSERT INTO Panha.dbo.OtherDeposit SELECT * FROM TempPanha.dbo.OtherDeposit;
	
	PRINT 'Import Other Deposit Repay.';
	INSERT INTO Panha.dbo.OtherDepositRepay SELECT * FROM [TempPanha].[dbo].[OtherDepositRepay];
	
	Print 'Import trace other deposit.';
	INSERT INTO [Panha].[dbo].[TRACE_OtherDeposit]([DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport])
	SELECT [DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport] FROM [TempPanha].[dbo].[TRACE_OtherDeposit];

	Print 'Import trace other deposit repay.';
	INSERT INTO [Panha].[dbo].[TRACE_OtherDepositRepay]([LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport] FROM [TempPanha].[dbo].[TRACE_OtherDepositRepay];

	Print 'Import OtherIncome';
	Insert Into Panha.dbo.OtherIncome Select * From TempPanha.dbo.OtherIncome;
	
	Print 'Import trace OtherIncome';
	Insert Into Panha.dbo.TRACE_OtherIncome select * From TempPanha.dbo.TRACE_OtherIncome;
	
	
	PRINT 'Import BK_SavingRepay.';
	INSERT INTO Panha.dbo.BK_SavingRepay([LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport])
	SELECT [LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport] FROM [TempPanha].[dbo].[BK_SavingRepay];

	Print 'Import TRACE_SavingRepay.';
	INSERT INTO [Panha].[dbo].[TRACE_SavingRepay]([SR_ID],[DateAction],[RecordAction],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport])
	SELECT [SR_ID],[DateAction],[RecordAction],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport] FROM [TempPanha].[dbo].[TRACE_SavingRepay];

	

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
	PRINT '??????????IMPORT ERROR | IMPORT ERROR | IMPORT ERROR | IMPORT ERROR??????????';
	PRINT '';
END CATCH;

IF @@TRANCOUNT > 0
    COMMIT TRANSACTION;
END






GO

-- ===== SP_MAIN_EXPORT =====
ALTER PROCEDURE [dbo].[SP_MAIN_EXPORT](@Date as DateTime,@Branch as varchar(12))
AS
BEGIN
DECLARE @strDate as Varchar(12);
SET @strDate = Convert(Varchar(12),@Date,101);
PRINT '---------------EXPORT DATA TO TEMP DATABASE-----------------------';
EXEC dbo.SP_EXPORT @strDate,@Branch;
PRINT '---------------UPDATE COLUMN ISEXPORT-----------------------';
EXEC [dbo].[SP_UPDATE_COLUMN_ISEXPORT] @strDate,@Branch;
PRINT '------------------SHRINK TEMP DATABASE...-------------------------';
EXEC dbo.SP_SHRINK_TEMPDATA
END




GO

-- ===== SP_SHRINK_TEMPDATA =====
ALTER PROCEDURE [dbo].[SP_SHRINK_TEMPDATA]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DBCC SHRINKDATABASE(N'TempPanha');
END



GO

