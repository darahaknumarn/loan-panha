-- Rollback of APPLY-pheap.sql: restores the procedure bodies pheap had on 2026-10-03
USE [pheap];
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
	SELECT LO_ID,LO_BrID FROM TempData.dbo.BK_Location WHERE NOT LO_Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Data.dbo.BK_Location WHERE LO_ID=@ID AND LO_BrID=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Customer
	DECLARE Curr CURSOR FOR
	SELECT CM_ID,CM_BrId FROM TempData.dbo.BK_Customer WHERE NOT CM_Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Data.dbo.BK_Customer WHERE CM_ID=@ID AND CM_BrId=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Position
	DECLARE Curr CURSOR FOR
	SELECT ID,BrID FROM TempData.dbo.BK_Position WHERE NOT Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Data.dbo.BK_Position WHERE ID=@ID AND BrID=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Employee
	DECLARE Curr CURSOR FOR
	SELECT EM_ID,EM_BrID FROM TempData.dbo.BK_Employee WHERE NOT Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Data.dbo.BK_Employee WHERE EM_ID=@ID AND EM_BrID=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Loan
	DECLARE Curr CURSOR FOR
	SELECT LD_ID,LD_BrId FROM TempData.dbo.BK_Loan WHERE NOT LD_Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Data.dbo.BK_Loan WHERE LD_ID=@ID AND LD_BrId=@BrID
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID
	END
	CLOSE Curr
	DEALLOCATE Curr
--Loan Repay
	DECLARE Curr CURSOR FOR
	SELECT LD_ID,LR_BrID,SH_Date FROM TempData.dbo.BK_LoanRepay WHERE NOT LR_Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID,@SH_Date
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Data.dbo.BK_LoanRepay WHERE LD_ID=@ID AND LR_BrID=@BrID AND SH_Date = @SH_Date
	  FETCH NEXT FROM Curr
	    INTO @ID,@BrID,@SH_Date
	END
	CLOSE Curr
	DEALLOCATE Curr
--Loan Schedule
	DECLARE Curr CURSOR FOR
	SELECT LD_ID,SH_BrId,SH_Date FROM TempData.dbo.BK_LoanSchedule WHERE NOT Date_Modify IS NULL
	OPEN Curr
	FETCH NEXT FROM Curr
	INTO @ID,@BrID,@SH_Date
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  DELETE FROM Data.dbo.BK_LoanSchedule WHERE LD_ID=@ID AND SH_BrId=@BrID AND SH_Date = @SH_Date
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
DELETE Data.dbo.BK_Location FROM Data.dbo.BK_Location A INNER JOIN TempData.dbo.BK_Location B ON A.LO_ID = B.LO_ID AND A.LO_BrID = B.LO_BrID;
DELETE Data.dbo.BK_Customer FROM Data.dbo.BK_Customer A INNER JOIN TempData.dbo.BK_Customer B ON A.CM_ID = B.CM_ID AND A.CM_BrId = B.CM_BrId; 
DELETE Data.dbo.BK_Position FROM Data.dbo.BK_Position A INNER JOIN TempData.dbo.BK_Position B ON A.ID = B.ID AND A.BrID = B.BrID;
DELETE Data.dbo.BK_Employee FROM Data.dbo.BK_Employee A INNER JOIN TempData.dbo.BK_Employee B ON A.EM_ID = B.EM_ID AND A.EM_BrID = B.EM_BrID;
DELETE Data.dbo.BK_Loan FROM Data.dbo.BK_Loan A INNER JOIN TempData.dbo.BK_Loan B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId;
DELETE Data.dbo.BK_LoanRepay FROM Data.dbo.BK_LoanRepay A INNER JOIN TempData.dbo.BK_LoanRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.SH_Date = B.SH_Date AND A.LR_Date_Create = B.LR_Date_Create;
DELETE Data.dbo.BK_LoanSchedule FROM Data.dbo.BK_LoanSchedule A INNER JOIN TempData.dbo.BK_LoanSchedule B ON A.LD_ID = B.LD_ID AND A.SH_BrId = B.SH_BrId AND A.SH_Date =B.SH_Date;
DELETE Data.dbo.Asset From Data.dbo.Asset A INNER JOIN TempData.dbo.Asset B on A.ASID = B.ASID AND A.BrID = B.BrID;
DELETE Data.dbo.ExpenseOperation From Data.dbo.ExpenseOperation A INNER JOIN TempData.dbo.ExpenseOperation B ON A.OPCode = B.OPCode AND A.BrID = B.BrID;
DELETE Data.dbo.ExpenseSchedule From Data.dbo.ExpenseSchedule A INNER JOIN TempData.dbo.ExpenseSchedule B ON A.OPCode = B.OPCode AND A.ExDate = B.ExDate AND A.BrID = B.BrID;
DELETE Data.dbo.OwnerTransaction From Data.dbo.OwnerTransaction a INNER JOIN TempData.dbo.OwnerTransaction b on a.OPID = b.OPID and a.BrID = b.BrID;
DELETE Data.dbo.Writeoff From Data.dbo.Writeoff a INNER JOIN TempData.dbo.Writeoff b on a.LD_ID = b.LD_ID and a.BR_ID = b.BR_ID;
DELETE Data.dbo.ProfitCutout From Data.dbo.ProfitCutout a INNER JOIN TempData.dbo.ProfitCutout b on a.OPID = b.OPID and a.BrID = b.BrID;
DELETE Data.dbo.OtherDeposit FROM Data.dbo.OtherDeposit A INNER JOIN TempData.dbo.OtherDeposit B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId;
DELETE Data.dbo.OtherDepositRepay FROM Data.dbo.OtherDepositRepay A INNER JOIN TempData.dbo.OtherDepositRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.LR_ID = B.LR_ID;
DELETE Data.dbo.OtherIncome From Data.dbo.OtherIncome a INNER JOIN TempData.dbo.OtherIncome b on a.OPID = b.OPID and a.BrID = b.BrID;
DELETE Data.dbo.BK_SavingRepay From Data.dbo.BK_SavingRepay a INNER JOIN TempData.dbo.BK_SavingRepay b on a.LD_ID= b.LD_ID and a.SR_BrID=b.SR_BrID and a.SR_Date_Create=b.SR_Date_Create;


DECLARE @STRDATE AS VARCHAR(12);
DECLARE @BRACNH AS VARCHAR(12);
SELECT @STRDATE=[Value] FROM TempData.dbo.SYS_IMPORT_EXPORT WHERE ID='EXPORT_DATE';
SELECT @BRACNH=[Value] FROM TempData.dbo.SYS_IMPORT_EXPORT WHERE ID='BRANCH';
DELETE FROM Data.dbo.TRACE_Customer WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND CM_BrId=@BRACNH;
DELETE FROM Data.dbo.TRACE_Loan WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND LD_BrId = @BRACNH ;
DELETE FROM Data.dbo.TRACE_LoanRepay WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND LR_BrID=@BRACNH;
DELETE FROM Data.dbo.TRACE_Location WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND LO_BrID=@BRACNH;
DELETE FROM Data.dbo.TRACE_Asset WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Data.dbo.TRACE_ExpenseOperation WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Data.dbo.TRACE_ExpenseSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Data.dbo.TRACE_LoanSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND SH_BrId=@BRACNH;
DELETE FROM Data.dbo.TRACE_OwnerTransaction Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Data.dbo.TRACE_Writeoff Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BR_ID=@BRACNH;
DELETE FROM Data.dbo.TRACE_ProfitCutout Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Data.dbo.TRACE_OtherDeposit WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND LD_BrId = @BRACNH ;
DELETE FROM Data.dbo.TRACE_OtherDepositRepay WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND LR_BrID=@BRACNH;
DELETE Data.dbo.sys_User FROM Data.dbo.sys_User A INNER JOIN TempData.dbo.sys_User B ON A.[User_Name] = B.[User_Name] AND A.BrID = B.BrID; 
DELETE FROM Data.dbo.TRACE_OtherIncome Where CONVERT(VARCHAR(12),DateAction,101)=@STRDATE AND BrID=@BRACNH;
DELETE FROM Data.dbo.TRACE_SavingRepay WHERE CONVERT(VARCHAR(12),DateAction,101)=@STRDATE  AND SR_BrID=@BRACNH;

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
DELETE Data.dbo.BK_Location FROM Data.dbo.BK_Location A INNER JOIN TempData.dbo.TRACE_Location B ON A.LO_ID = B.LO_ID AND A.LO_BrID = B.LO_BrID AND B.RecordAction='DELETE' AND ((A.LO_Date_Modify IS NULL AND B.DateAction > A.LO_Date_Create) OR (NOT A.LO_Date_Modify IS NULL AND B.DateAction > A.LO_Date_Modify));
DELETE Data.dbo.BK_Customer FROM Data.dbo.BK_Customer A INNER JOIN TempData.dbo.TRACE_Customer B ON A.CM_ID = B.CM_ID AND A.CM_BrId = B.CM_BrId AND B.RecordAction='DELETE' AND ((A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Create) OR (NOT A.CM_Date_Modify IS NULL AND B.DateAction > A.CM_Date_Modify));
DELETE Data.dbo.BK_Loan FROM Data.dbo.BK_Loan A INNER JOIN TempData.dbo.TRACE_Loan B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId AND B.RecordAction='DELETE' AND ((A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Create)OR (NOT A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Modify));
DELETE Data.dbo.BK_LoanRepay FROM Data.dbo.BK_LoanRepay A INNER JOIN TempData.dbo.TRACE_LoanRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.SH_Date = B.SH_Date AND A.LR_Date_Create = B.LR_Date_Create AND B.RecordAction='DELETE' AND ((A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Create) OR (NOT A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Modify));
DELETE Data.dbo.Asset FROM Data.dbo.Asset A INNER JOIN TempData.dbo.TRACE_Asset B ON A.ASID = B.ASID AND A.BrID = B.BrID AND B.RecordAction='DELETE' AND ((A.Date_Modify IS NULL AND B.DateAction > A.Date_Create) OR (NOT A.Date_Modify IS NULL AND B.DateAction > A.Date_Modify));
DELETE Data.dbo.ExpenseOperation FROM Data.dbo.ExpenseOperation A INNER JOIN TempData.dbo.TRACE_ExpenseOperation B ON A.ASID = B.ASID AND A.BrID = B.BrID AND A.OPCode = B.OPCode AND B.RecordAction='DELETE' AND ((A.Date_Modify IS NULL AND B.DateAction > A.Date_Create) OR (NOT A.Date_Modify IS NULL AND B.DateAction > A.Date_Modify));
DELETE Data.dbo.ExpenseSchedule FROM Data.dbo.ExpenseSchedule A INNER JOIN TempData.dbo.TRACE_ExpenseSchedule B ON A.OPCode = B.OPCode AND A.BrID = B.BrID AND A.Date_Create=B.Date_Create AND B.RecordAction='DELETE';
DELETE Data.dbo.BK_LoanSchedule FROM Data.dbo.BK_LoanSchedule A INNER JOIN TempData.dbo.TRACE_LoanSchedule B ON A.LD_ID = B.LD_ID AND A.SH_BrId = B.SH_BrId AND A.SH_Date=B.SH_Date AND A.Date_Create=B.Date_Create AND B.RecordAction='DELETE';
DELETE Data.dbo.OwnerTransaction From Data.dbo.OwnerTransaction a Inner Join TempData.dbo.TRACE_OwnerTransaction b on a.OPID = b.OPID and a.BrID = b.BrID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Data.dbo.Writeoff From Data.dbo.Writeoff a Inner Join TempData.dbo.TRACE_Writeoff b on a.LD_ID = b.LD_ID and a.BR_ID = b.BR_ID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Data.dbo.ProfitCutout From Data.dbo.ProfitCutout a Inner Join TempData.dbo.TRACE_ProfitCutout b on a.OPID = b.OPID and a.BrID = b.BrID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Data.dbo.OtherDeposit FROM Data.dbo.OtherDeposit A INNER JOIN TempData.dbo.TRACE_OtherDeposit B ON A.LD_ID = B.LD_ID AND A.LD_BrId = B.LD_BrId AND B.RecordAction='DELETE' AND ((A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Create)OR (NOT A.LD_Date_Modify IS NULL AND B.DateAction > A.LD_Date_Modify));
DELETE Data.dbo.OtherDepositRepay FROM Data.dbo.OtherDepositRepay A INNER JOIN TempData.dbo.TRACE_OtherDepositRepay B ON A.LD_ID = B.LD_ID AND A.LR_BrID = B.LR_BrID AND A.SH_Date = B.SH_Date AND B.RecordAction='DELETE' AND ((A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Create) OR (NOT A.LR_Date_Modify IS NULL AND B.DateAction > A.LR_Date_Modify));
DELETE Data.dbo.OtherIncome From Data.dbo.OtherIncome a Inner Join TempData.dbo.TRACE_OtherIncome b on a.OPID = b.OPID and a.BrID = b.BrID and b.RecordAction='DELETE' AND ((a.Date_Modify IS NULL AND b.DateAction > a.Date_Create) OR (NOT a.Date_Modify IS NULL AND b.DateAction > a.Date_Modify));
DELETE Data.dbo.BK_SavingRepay FROM Data.dbo.BK_SavingRepay A INNER JOIN TempData.dbo.TRACE_SavingRepay B ON A.LD_ID = B.LD_ID AND A.SR_BrID = B.SR_BrID AND A.SR_Date_Create = B.SR_Date_Create AND B.RecordAction='DELETE' AND ((A.SR_Date_Modify IS NULL AND B.DateAction > A.SR_Date_Create) OR (NOT A.SR_Date_Modify IS NULL AND B.DateAction > A.SR_Date_Modify));
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
	INSERT INTO Data.dbo.BK_Location SELECT * FROM TempData.dbo.BK_Location;
    
    PRINT 'Import customer.';
	INSERT INTO Data.dbo.BK_Customer SELECT * FROM TempData.dbo.BK_Customer;
    
    PRINT 'Import position.';
	INSERT INTO Data.dbo.BK_Position SELECT * FROM TempData.dbo.BK_Position;

	PRINT 'Import employee.';
	INSERT INTO Data.dbo.BK_Employee SELECT * FROM TempData.dbo.BK_Employee;
    
    PRINT 'Import loan.';
	INSERT INTO Data.dbo.BK_Loan SELECT * FROM TempData.dbo.BK_Loan;
	
	PRINT 'Import loan repay.';
	INSERT INTO Data.dbo.BK_LoanRepay([LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport] FROM [TempData].[dbo].[BK_LoanRepay];
	
	PRINT 'Import loan schedule.';
	INSERT INTO Data.dbo.BK_LoanSchedule([LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt]) 
	SELECT [LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt] FROM [TempData].[dbo].[BK_LoanSchedule];
	
	PRINT 'Import trace customer';
    INSERT INTO [Data].[dbo].[TRACE_Customer]([DateAction],[RecordAction],[CM_ID],[CM_EnName],[CM_KhName],[CM_Contact_Name],[CM_Contact_Title],[CM_Contact_Position],[CM_Contact_No],[CM_Contact_Email],[LO_ID],[CM_Address],[CM_Phone],[CM_Email],[CM_Fax],[CM_Website],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle])
	SELECT [DateAction],[RecordAction],[CM_ID],[CM_EnName],[CM_KhName],[CM_Contact_Name],[CM_Contact_Title],[CM_Contact_Position],[CM_Contact_No],[CM_Contact_Email],[LO_ID],[CM_Address],[CM_Phone],[CM_Email],[CM_Fax],[CM_Website],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle] FROM [TempData].[dbo].[TRACE_Customer];
	
	Print 'Import trace loan.';
	INSERT INTO [Data].[dbo].[TRACE_Loan]([DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate])
	SELECT [DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate] FROM [TempData].[dbo].[TRACE_Loan];

	Print 'Import trace loan repay.';
	INSERT INTO [Data].[dbo].[TRACE_LoanRepay]([LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport] FROM [TempData].[dbo].[TRACE_LoanRepay];

	Print 'Import trace location.';
	INSERT INTO [Data].[dbo].[TRACE_Location]([DateAction],[RecordAction],[LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete])
	SELECT [DateAction],[RecordAction],[LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete] FROM [TempData].[dbo].[TRACE_Location];
	
	Print 'Import table user.';
	INSERT INTO [Data].[dbo].[sys_User]	SELECT * FROM [TempData].[dbo].[sys_User] ; 
	
	Print 'Import asset table.';
	INSERT INTO Data.dbo.Asset SELECT * FROM TempData.dbo.Asset;
	
	Print 'Import expense operation.';
	INSERT INTO [Data].[dbo].[ExpenseOperation]([BrID],[OPDate],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [BrID],[OPDate],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete] FROM [TempData].[dbo].[ExpenseOperation] ;

	Print 'Import expense schedule.';
	INSERT INTO Data.dbo.ExpenseSchedule SELECT * FROM TempData.dbo.ExpenseSchedule;
	
	Print 'Import trace asset table.';
	INSERT INTO Data.dbo.TRACE_Asset SELECT * FROM TempData.dbo.TRACE_Asset;
	
	Print 'Import trace expense operation.';
	INSERT INTO [Data].[dbo].[TRACE_ExpenseOperation] SELECT * FROM [TempData].[dbo].[TRACE_ExpenseOperation] ;
	
	Print 'Import trace loan schedule table.';
	INSERT INTO Data.dbo.TRACE_LoanSchedule SELECT * FROM TempData.dbo.TRACE_LoanSchedule;
	
	Print 'Import trace expense schedule.';
	INSERT INTO [Data].[dbo].[TRACE_ExpenseSchedule] SELECT * FROM [TempData].[dbo].[TRACE_ExpenseSchedule] ;
	
	Print 'Import owner transaction';
	Insert Into Data.dbo.OwnerTransaction Select * From TempData.dbo.OwnerTransaction;
	
	Print 'Import trace owner transaction';
	Insert Into Data.dbo.TRACE_OwnerTransaction select * From TempData.dbo.TRACE_OwnerTransaction;
	
	Print 'Import Writeoff';
	Insert Into Data.dbo.Writeoff Select * From TempData.dbo.Writeoff;
	
	Print 'Import trace Writeoff';
	Insert Into Data.dbo.TRACE_Writeoff select * From TempData.dbo.TRACE_Writeoff;

	Print 'Import Profit Cutout';
	Insert Into Data.dbo.ProfitCutout Select * From TempData.dbo.ProfitCutout;
	
	Print 'Import trace Profit Cutout';
	Insert Into Data.dbo.TRACE_ProfitCutout select * From TempData.dbo.TRACE_ProfitCutout;

	PRINT 'Import Other Deposit.';
	INSERT INTO Data.dbo.OtherDeposit SELECT * FROM TempData.dbo.OtherDeposit;
	
	PRINT 'Import Other Deposit Repay.';
	INSERT INTO Data.dbo.OtherDepositRepay SELECT * FROM [TempData].[dbo].[OtherDepositRepay];
	
	Print 'Import trace other deposit.';
	INSERT INTO [Data].[dbo].[TRACE_OtherDeposit]([DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport])
	SELECT [DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport] FROM [TempData].[dbo].[TRACE_OtherDeposit];

	Print 'Import trace other deposit repay.';
	INSERT INTO [Data].[dbo].[TRACE_OtherDepositRepay]([LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport] FROM [TempData].[dbo].[TRACE_OtherDepositRepay];

	Print 'Import OtherIncome';
	Insert Into Data.dbo.OtherIncome Select * From TempData.dbo.OtherIncome;
	
	Print 'Import trace OtherIncome';
	Insert Into Data.dbo.TRACE_OtherIncome select * From TempData.dbo.TRACE_OtherIncome;
	
	
	PRINT 'Import BK_SavingRepay.';
	INSERT INTO Data.dbo.BK_SavingRepay([LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport])
	SELECT [LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport] FROM [TempData].[dbo].[BK_SavingRepay];

	Print 'Import TRACE_SavingRepay.';
	INSERT INTO [Data].[dbo].[TRACE_SavingRepay]([SR_ID],[DateAction],[RecordAction],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport])
	SELECT [SR_ID],[DateAction],[RecordAction],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport] FROM [TempData].[dbo].[TRACE_SavingRepay];

	

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
EXEC TempData.dbo.SP_SHRINK_TEMPDATA
END




GO

-- ===== SP_SHRINK_TEMPDATA =====

ALTER PROCEDURE [dbo].[SP_SHRINK_TEMPDATA]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DBCC SHRINKDATABASE(N'TempData' );
END



GO

-- ===== SP_EXPORT =====

ALTER PROCEDURE [dbo].[SP_EXPORT](@Date DateTime,@Branch varchar(12))
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @StrDate Varchar(12) ;
    SET @StrDate= CONVERT(VARCHAR(12),@Date,101);
	--UPDATE TempMorokot.dbo.SYS_IMPORT_EXPORT SET [Value]=@StrDate WHERE ID='EXPORT_DATE';
	--UPDATE TempMorokot.dbo.SYS_IMPORT_EXPORT SET [Value]=@Branch WHERE ID='BRANCH';
	
	PRINT 'Delete and insert temporary table Location.';
	DELETE FROM TempMorokot.dbo.BK_Location;
	INSERT INTO TempMorokot.dbo.BK_Location 
	SELECT * FROM Morokot.dbo.BK_Location 
	WHERE CONVERT(VARCHAR(12),LO_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LO_Date_Modify,101)=@StrDate;
    
	--return
    PRINT 'Delete and export temporary table customer.';
	DELETE FROM TempMorokot.dbo.BK_Customer;
	INSERT INTO TempMorokot.dbo.BK_Customer 
	SELECT * FROM Morokot.dbo.BK_Customer 
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table customer other.';
	DELETE FROM TempMorokot.[dbo].[BK_CustomerOther]
	INSERT INTO TempMorokot.[dbo].[BK_CustomerOther]
	SELECT * FROM Morokot.[dbo].[BK_CustomerOther]
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table trace customer other.';
	DELETE FROM TempMorokot.[dbo].TRACE_CustomerOther
	INSERT INTO TempMorokot.[dbo].TRACE_CustomerOther
	SELECT * FROM Morokot.[dbo].TRACE_CustomerOther
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
    

	PRINT 'Delete and export temporary table Exchange.';
	DELETE FROM TempMorokot.[dbo].[BK_Exchange]
	INSERT INTO TempMorokot.[dbo].[BK_Exchange]
	SELECT * FROM Morokot.[dbo].[BK_Exchange]
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table Trace Exchange.';
	DELETE FROM TempMorokot.[dbo].TRACE_Exchange
	INSERT INTO TempMorokot.[dbo].TRACE_Exchange
	SELECT * FROM Morokot.[dbo].TRACE_Exchange
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table Bank Transaction';
	DELETE FROM TempMorokot.[dbo].Bank_Transaction
	INSERT INTO TempMorokot.[dbo].Bank_Transaction
	SELECT * FROM Morokot.[dbo].Bank_Transaction
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table Trace Bank Transaction';
	DELETE FROM TempMorokot.[dbo].TRACE_Bank
	INSERT INTO TempMorokot.[dbo].TRACE_Bank
	SELECT * FROM Morokot.[dbo].TRACE_Bank
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    


    PRINT 'Delete and export temporary table position.';
	DELETE FROM TempMorokot.dbo.BK_Position;
	INSERT INTO TempMorokot.dbo.BK_Position 
	SELECT * FROM Morokot.dbo.BK_Position 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;

	PRINT 'Delete and export temporary table Change customer.';
	DELETE FROM TempMorokot.[dbo].[BK_ChangeCustomer];
	INSERT INTO TempMorokot.[dbo].[BK_ChangeCustomer]
	SELECT * FROM Morokot.[dbo].[BK_ChangeCustomer]
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate ;

	--select * from BK_ChangeCustomer
	PRINT 'Delete and export temporary table employee.';
	DELETE FROM TempMorokot.dbo.BK_Employee;
	INSERT INTO TempMorokot.dbo.BK_Employee 
	SELECT * FROM Morokot.dbo.BK_Employee 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
    PRINT 'Delete and export temporary table loan.';
	DELETE FROM TempMorokot.dbo.BK_Loan;
	INSERT INTO TempMorokot.dbo.BK_Loan 
	SELECT * FROM Morokot.dbo.BK_Loan 
	WHERE CONVERT(VARCHAR(12),LD_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LD_Date_Modify,101)=@StrDate;
	
	PRINT 'Delete and export temporary table loan repay.';
	DELETE FROM TempMorokot.dbo.BK_LoanRepay;
	INSERT INTO TempMorokot.dbo.BK_LoanRepay
	SELECT 
	*
	FROM Morokot.[dbo].[BK_LoanRepay] 
	WHERE CONVERT(VARCHAR(12),LR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LR_Date_Modify,101)=@StrDate;
	
	PRINT 'Delete and export temporary table loan schedule.';
	DELETE FROM TempMorokot.dbo.BK_LoanSchedule;
	INSERT INTO TempMorokot.dbo.BK_LoanSchedule 
	SELECT *
	FROM Morokot.[dbo].[BK_LoanSchedule] 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	
	Print 'Delete and export temporary table trace customer.';
	Delete From TempMorokot.dbo.TRACE_Customer;
	INSERT INTO TempMorokot.[dbo].[TRACE_Customer]
	SELECT *
	FROM Morokot.[dbo].[TRACE_Customer] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	
	Print 'Delete and export temporary table trace loan.';
	Delete From TempMorokot.dbo.TRACE_Loan;
	INSERT INTO TempMorokot.[dbo].[TRACE_Loan]
	SELECT *
	FROM Morokot.[dbo].[TRACE_Loan] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and export temporary table trace loan repay.';
	Delete From TempMorokot.dbo.TRACE_LoanRepay;
	INSERT INTO TempMorokot.[dbo].[TRACE_LoanRepay]
	SELECT *
	FROM Morokot.[dbo].[TRACE_LoanRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and export temporary table trace location.';
	Delete From TempMorokot.dbo.TRACE_Location;
	INSERT INTO TempMorokot.[dbo].[TRACE_Location]
	SELECT *
	FROM Morokot.[dbo].[TRACE_Location] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	
	Print 'Delete and Export temporary table user.';
	Delete From TempMorokot.dbo.sys_User;
	INSERT INTO TempMorokot.[dbo].[sys_User]	
	SELECT * FROM Morokot.[dbo].[sys_User] WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 
	
	Print 'Delete and Export asset table';
	Delete From TempMorokot.dbo.Asset;
	INSERT INTO TempMorokot.dbo.Asset 
	SELECT * FROM Morokot.dbo.Asset A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 
	
	Print 'Delete and Export Expense Operation';
	Delete From TempMorokot.dbo.ExpenseOperation;
	INSERT INTO TempMorokot.[dbo].[ExpenseOperation]
	SELECT *
	FROM Morokot.[dbo].[ExpenseOperation] WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 
	
	Print 'Delete and Export Expense Schedule';
	Delete From TempMorokot.dbo.ExpenseSchedule;
	Insert Into TempMorokot.dbo.ExpenseSchedule 
	Select * from Morokot.dbo.ExpenseSchedule WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate;
	
	Print 'Delete and Export trace asset table';
	Delete From TempMorokot.dbo.TRACE_Asset;
	INSERT INTO TempMorokot.dbo.TRACE_Asset 
	SELECT * FROM Morokot.dbo.TRACE_Asset A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 
	
	Print 'Delete and Export trace Expense Operation';
	Delete From TempMorokot.dbo.TRACE_ExpenseOperation;
	INSERT INTO TempMorokot.[dbo].[TRACE_ExpenseOperation] 
	SELECT * FROM Morokot.[dbo].[TRACE_ExpenseOperation] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	Print 'Delete and Export trace expense schedule';
	Delete From TempMorokot.dbo.TRACE_ExpenseSchedule;
	INSERT INTO TempMorokot.[dbo].[TRACE_ExpenseSchedule] 
	SELECT * FROM Morokot.[dbo].[TRACE_ExpenseSchedule] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;;
	
	Print 'Delete and Export trace loan schedule';
	Delete From TempMorokot.dbo.TRACE_LoanSchedule;
	INSERT INTO TempMorokot.[dbo].TRACE_LoanSchedule	
	SELECT * FROM Morokot.[dbo].TRACE_LoanSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	Print 'Delete and Export OwnerTransaction table';
	Delete From TempMorokot.dbo.OwnerTransaction;
	INSERT INTO TempMorokot.dbo.OwnerTransaction 
	SELECT * FROM Morokot.dbo.OwnerTransaction A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace OwnerTransaction table';
	Delete From TempMorokot.dbo.TRACE_OwnerTransaction;
	INSERT INTO TempMorokot.dbo.TRACE_OwnerTransaction 
	SELECT * FROM Morokot.dbo.TRACE_OwnerTransaction A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and Export writeoff table';
	Delete From TempMorokot.dbo.Writeoff;
	INSERT INTO TempMorokot.dbo.Writeoff SELECT * FROM Morokot.dbo.Writeoff A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and Export trace writeoff table';
	Delete From TempMorokot.dbo.TRACE_Writeoff;
	INSERT INTO TempMorokot.dbo.TRACE_Writeoff SELECT * FROM Morokot.dbo.TRACE_Writeoff A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and Export Profit Cutout table';
	Delete From TempMorokot.dbo.ProfitCutout;
	INSERT INTO TempMorokot.dbo.ProfitCutout 
	SELECT * FROM Morokot.dbo.ProfitCutout A
	 WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate 
	 OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace Profit Cutout table';
	Delete From TempMorokot.dbo.TRACE_ProfitCutout;
	INSERT INTO TempMorokot.dbo.TRACE_ProfitCutout SELECT * FROM Morokot.dbo.TRACE_ProfitCutout A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 
		
	PRINT 'Delete and export temporary table other deposit.';
	DELETE FROM TempMorokot.dbo.OtherDeposit;
	INSERT INTO TempMorokot.dbo.OtherDeposit SELECT * FROM Morokot.dbo.OtherDeposit WHERE CONVERT(VARCHAR(12),LD_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LD_Date_Modify,101)=@StrDate;
	
	PRINT 'Delete and export temporary table loan repay.';
	DELETE FROM TempMorokot.dbo.OtherDepositRepay;
	INSERT INTO TempMorokot.dbo.OtherDepositRepay SELECT * FROM Morokot.[dbo].OtherDepositRepay WHERE CONVERT(VARCHAR(12),LR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LR_Date_Modify,101)=@StrDate;
	
	
	Print 'Delete and export temporary table trace loan.';
	Delete From TempMorokot.dbo.TRACE_OtherDeposit;
	INSERT INTO TempMorokot.[dbo].[TRACE_OtherDeposit]
	SELECT *
	FROM Morokot.[dbo].[TRACE_OtherDeposit] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and export temporary table trace loan repay.';
	Delete From TempMorokot.dbo.TRACE_OtherDepositRepay;
	INSERT INTO TempMorokot.[dbo].[TRACE_OtherDepositRepay]
	SELECT *
	FROM Morokot.[dbo].[TRACE_OtherDepositRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	

	Print 'Delete and Export OtherIncome table';
	Delete From TempMorokot.dbo.OtherIncome;
	INSERT INTO TempMorokot.dbo.OtherIncome 
	SELECT * FROM Morokot.dbo.OtherIncome A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace OtherIncome table';
	Delete From TempMorokot.dbo.TRACE_OtherIncome;
	INSERT INTO TempMorokot.dbo.TRACE_OtherIncome 
	SELECT * FROM Morokot.dbo.TRACE_OtherIncome A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;


	PRINT 'Delete and export table saving repay.';
	DELETE FROM TempMorokot.dbo.BK_SavingRepay;
	INSERT INTO TempMorokot.dbo.BK_SavingRepay
	SELECT *
	FROM Morokot.[dbo].BK_SavingRepay WHERE CONVERT(VARCHAR(12),SR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),SR_Date_Modify,101)=@StrDate;
	
	Print 'Delete and export TRACE_SavingRepay.';
	Delete From TempMorokot.dbo.TRACE_SavingRepay;
	INSERT INTO TempMorokot.[dbo].[TRACE_SavingRepay]
	SELECT *
	FROM Morokot.[dbo].[TRACE_SavingRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;


	Print 'Delete and export Holiday';
	--Print 'Delete and Export writeoff table';
	Delete From TempMorokot.dbo.BK_Holiday;
	INSERT INTO TempMorokot.dbo.BK_Holiday 
	SELECT * FROM Morokot.dbo.BK_Holiday A 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and export TRACE_Holiday';
	Delete From TempMorokot.dbo.TRACE_Holiday;
	INSERT INTO TempMorokot.[dbo].TRACE_Holiday
	SELECT *
	FROM Morokot.[dbo].TRACE_Holiday WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	---
		Print 'Delete and export OtherIncome';
	--Print 'Delete and Export writeoff table';
	Delete From TempMorokot.dbo.BK_OtherIncome;
	INSERT INTO TempMorokot.dbo.BK_OtherIncome 
	SELECT * FROM Morokot.dbo.BK_OtherIncome A 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and export TRACE_OtherIncome';
	Delete From TempMorokot.dbo.Trace_OtherIncome;
	INSERT INTO TempMorokot.[dbo].Trace_OtherIncome
	SELECT *
	FROM Morokot.[dbo].Trace_OtherIncome WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
		
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
PRINT '??????????EXPORT ERROR | EXPORT ERROR | EXPORT ERROR | EXPORT ERROR??????????';
PRINT '';
END CATCH;

IF @@TRANCOUNT > 0
    COMMIT TRANSACTION;
END

GO

-- ===== sp_rptGetLoanPaid =====


ALTER PROCEDURE [dbo].[sp_rptGetLoanPaid] (
@StartDate Varchar(12),
@EndDate Varchar(12),
@BrID Varchar(12),
@EMCode as Varchar(12),
@Curr as Nvarchar(12))
AS
BEGIN
SET NOCOUNT ON;
Declare @SDate as DateTime;
Declare @EDate as DateTime;
Set @SDate = @StartDate;
Set @EDate = @EndDate;

IF @BrID = 'All' set @BrID ='%';
if @EMCode ='All' set @EMCode ='%';
IF @Curr ='All' set @Curr ='%';
IF @Curr =N'រៀល' set @Curr =1
IF @Curr =N'ដុល្លារ' set @Curr =2
----------------------------------------------------------------------------

SELECT a.LD_ID as 'Loan ID', 
a.LR_BrID as 'Branch Code',
c.EM_ID as 'Employee Code', 
c.EM_Name as 'Employee Name' , 
b.CM_ID  as 'Customer Code', 
b.CM_KhName  as 'Customer Name', 
b.CM_Phone as 'Customer Phone', 
d.VL_ID +','+d.CN_ID +','+ d.DT_ID +','+ d.PV_ID AS 'Customer Address',
a.LR_Description as 'Description',
Convert(Varchar(12),a.LR_Date,101) as 'Paid Date',
a.LR_Amount+isnull(a.LR_Charge,0) as 'Total Amount',
--a.LR_Charge as 'Charge Amount',
e.CU_ID as 'Currency'
FROM  dbo.BK_LoanRepay a INNER JOIN 
             dbo.BK_Customer b ON a.CM_ID1 = b.ID AND a.LR_BrID = b.CM_BrId INNER JOIN
             dbo.BK_Employee c ON a.EM_ID = c.EM_ID AND a.LR_BrID = c.EM_BrID INNER JOIN
             dbo.BK_Location d ON b.LO_ID = d.LO_ID AND b.CM_BrId = d.LO_BrID Inner Join 
             dbo.BK_Loan e on a.LD_ID = e.LD_ID and a.LR_BrID = e.LD_BrId
Where a.LR_BrID like @BrID
and a.EM_ID like @EMCode
and a.LR_Date >=@SDate and a.LR_Date <= @EDate
and e.CU_ID like @Curr;

END

GO

-- ===== sp_rptEndBalSumByEndDay =====



ALTER PROCEDURE [dbo].[sp_rptEndBalSumByEndDay] (
@StartDate Varchar(12),
@EndDate Varchar(12),
@BrID Varchar(12))
AS
BEGIN
SET NOCOUNT ON;
Declare @SDate as DateTime;
Declare @EDate as DateTime;
Declare @KHR as Nvarchar(100);
Declare @USD as Nvarchar(100);
Set @EDate = @EndDate;
Set @SDate = @StartDate;
Set @KHR = N'រៀល';
Set @USD = N'ដុល្លារ';

Declare @EKHR as numeric (18,2)=0;
Declare @EUSD as numeric(18,2)=0;

IF @BrID = 'All' set @BrID ='%';
----Loan Repay
Select a.LD_ID,a.LR_Date,
       c.CU_ID,
       a.LR_BrID
        ,case when c.CU_ID=2 then sum(a.LR_Amount) else 0 end USD
       ,case when c.CU_ID=1 then sum(a.LR_Amount) else 0 end KHR
       ,case when c.CU_ID=2 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_USD
       ,case when c.CU_ID=1 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_KHR
Into #LD_Repay
From dbo.BK_LoanRepay a Inner Join dbo.BK_LoanSchedule b On a.LD_ID = b.LD_ID and a.SH_Date = b.SH_Date and a.LR_BrID = b.SH_BrId
     Inner Join dbo.BK_Loan c On c.LD_ID = a.LD_ID and c.LD_BrId = a.LR_BrID
Where a.LR_Date between @SDate and @EDate
      and a.LR_BrID = @BrID
      group by a.LD_ID,c.CU_ID,a.LR_BrID,a.LR_Date
      --and c.CU_ID = @CUID;

Select a.LR_BrID,a.LR_Date,
SUM( a.KHR) as KHR,
 sum(a.USD) as USD
 , sum(a.Charge_KHR) Charge_KHR,
 sum(a.Charge_USD) Charge_USD
   Into #TotalRepay
From #LD_Repay a
Group By a.LR_BrID,a.LR_Date;
--select * from #TotalRepay
--return
---------------------------------------------- Admin fee
Select a.LD_Dis_Date,
       a.LD_ID, 
	   case when a.CU_ID=1 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end KHR,
	   case when a.CU_ID=2 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end USD,
	     case when a.CU_ID=1 then sum(isnuLL(a.Ref,0)) else 0 end RefKHR,
	   case when a.CU_ID=2 then sum(isnuLL(a.Ref,0)) else 0 end RefUSD,
	           a.LD_BrId,       a.CU_ID
Into #LD_Fee
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date between @SDate and @EDate
  group by a.LD_Dis_Date, a.LD_ID,a.LD_BrId,a.CU_ID
  
Select a.LD_BrId,a.LD_Dis_Date,
    sum(a.KHR) KHR,sum(a.USD)USD,
	sum(a.RefKHR) RefKHR,sum(a.RefUSD)RefUSD
Into #Total_Fee
From #LD_Fee a 
Group By a.LD_BrId,a.LD_Dis_Date
--select * from #Total_Fee return
----------------------------------------------- Insurance
Select a.LD_Dis_Date,
       a.LD_ID, case when a.CU_ID=1 then sum(isnuLL(a.LD_InAmt,0)) else 0 end KHR,case when a.CU_ID=2 then sum(isnuLL(a.LD_InAmt,0)) else 0 end USD
       ,        a.LD_BrId,       a.CU_ID
Into #LD_Insurance
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date between @SDate and @EDate
  group by a.LD_ID,a.LD_BrId,a.CU_ID,a.LD_Dis_Date
  
Select a.LD_BrId,a.LD_Dis_Date,
    sum(a.KHR) KHR,sum(a.USD)USD
Into #Total_Insurance
From #LD_Insurance a 
Group By a.LD_BrId,a.LD_Dis_Date
--select * from #Total_Insurance return
--select * from #LD_Fee
--return
----Loan Disbursment
Select a.LD_Dis_Date,
       a.LD_ID, case when a.CU_ID=1 then sum(a.LD_Dis_Amt) else 0 end KHR,case when a.CU_ID=2 then sum(a.LD_Dis_Amt) else 0 end USD
       ,        a.LD_BrId,       a.CU_ID
Into #LD_Dis
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date between @SDate and @EDate
  group by a.LD_ID,a.LD_BrId,a.CU_ID,a.LD_Dis_Date
  
Select a.LD_BrId,a.LD_Dis_Date,
    sum(a.KHR) KHR,sum(a.USD)USD
Into #Total_Dis
From #LD_Dis a 
Group By a.LD_BrId,a.LD_Dis_Date

--Total Expense
Select a.OPDate,
       a.BrID,
     case when OPCurrency=N'រៀល' then sum(OPCost) else 0 end KHR,
        case when OPCurrency=N'ដុល្លារ' then sum(OPCost) else 0 end USD,
       a.OPCurrency
Into #EX
From ExpenseOperation a 
Where a.BrID = @BrID 
and a.OPDate between @SDate and @EDate 
group by a.BrID,OPCurrency,a.OPDate


Select a.BrID,a.OPDate,
   SUM( a.KHR) KHR,sum(a.USD) USD
Into #Total_Ex
From #EX a
Group by a.BrID,a.OPDate

--select *from #Total_Ex return
--Owner Withdrawal and Deposit
Select a.BrID,a.OPDate,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Deposit
From OwnerTransaction a 
where a.BrID = @BrID
     and a.OPDate between @SDate and @EDate 
	 and a.OPType='Deposit' 
	 group by a.BrID,a.OPDate;

Select a.BrID,a.OPDate,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Withdrawal
From OwnerTransaction a 
where a.BrID = @BrID
     and a.OPDate between @SDate and @EDate 
	 and a.OPType='Withdrawal' group by a.BrID,a.OPDate;

select BrId,Date_Operation,SUM( USD)USD,SUM( KHR)KHR 
into #Total_USDTOKHR
from BK_Exchange 
where Date_Operation between @SDate and @EDate and Type='USDTOKHR'
group by BrId,Date_Operation

select BrId,Date_Operation,SUM( USD)USD,SUM( KHR)KHR 
into #Total_KHRTOUSD
from BK_Exchange where Date_Operation between @SDate and @EDate and Type='KHRTOUSD'
group by BrId,Date_Operation
------------------------
select BrId,Date_Operation
,case when CU_ID=1 then sum(isnull(Amount,0)) else 0 end KHR 
,case when CU_ID=2 then sum(isnull(Amount,0)) else 0 end USD
into #OtherTem
from BK_OtherIncome 
where BrId like @BrID 
and Date_Operation between @SDate and @EndDate
group by BrId,CU_ID,Date_Operation
--
select BrId,Date_Operation,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD 
into #OtherTotal from #OtherTem Group by BrId,Date_Operation

------------------------------------------First Ending Balance

Select a.LD_ID,
       c.CU_ID,
       a.LR_BrID
        ,case when c.CU_ID=2 then sum(a.LR_Amount) else 0 end USD
       ,case when c.CU_ID=1 then sum(a.LR_Amount) else 0 end KHR
       ,case when c.CU_ID=2 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_USD
       ,case when c.CU_ID=1 then sum(isnull(a.LR_Charge,0)) else 0 end Charge_KHR
Into #LD_Repay1
From dbo.BK_LoanRepay a 
	 Inner Join dbo.BK_LoanSchedule b On a.LD_ID = b.LD_ID and a.SH_Date = b.SH_Date and a.LR_BrID = b.SH_BrId
     Inner Join dbo.BK_Loan c On c.LD_ID = a.LD_ID and c.LD_BrId = a.LR_BrID
Where  a.LR_Date <= @SDate-1
      and a.LR_BrID = @BrID
      group by a.LD_ID,c.CU_ID,a.LR_BrID
      --and c.CU_ID = @CUID;

Select a.LR_BrID,
SUM( a.KHR) as KHR,
 sum(a.USD) as USD
 , sum(a.Charge_KHR) Charge_KHR,
 sum(a.Charge_USD) Charge_USD
Into #TotalRepay1
From #LD_Repay1 a
Group By a.LR_BrID;
--select * from #TotalRepay
--return
---------------------------------------------- Admin fee
Select 
       a.LD_ID, 
	   case when a.CU_ID=1 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end KHR,
	   case when a.CU_ID=2 then sum(isnuLL(a.LD_ChargeAmt,0)) else 0 end USD,
	     case when a.CU_ID=1 then sum(isnuLL(a.Ref,0)) else 0 end RefKHR,
	   case when a.CU_ID=2 then sum(isnuLL(a.Ref,0)) else 0 end RefUSD,
	           a.LD_BrId,       a.CU_ID
Into #LD_Fee1
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date <= @SDate-1
  group by a.LD_ID,a.LD_BrId,a.CU_ID
  
Select a.LD_BrId,
    sum(a.KHR) KHR,sum(a.USD)USD,
	sum(a.RefKHR) RefKHR,sum(a.RefUSD)RefUSD
Into #Total_Fee1
From #LD_Fee1 a 
Group By a.LD_BrId

----------------------------------------------- Insurance
Select 
       a.LD_ID, case when a.CU_ID=1 then sum(isnuLL(a.LD_InAmt,0)) else 0 end KHR,case when a.CU_ID=2 then sum(isnuLL(a.LD_InAmt,0)) else 0 end USD
       ,        a.LD_BrId,       a.CU_ID
Into #LD_Insurance1
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date <= @SDate-1
  group by a.LD_ID,a.LD_BrId,a.CU_ID
  
Select a.LD_BrId,
    sum(a.KHR) KHR,sum(a.USD)USD
Into #Total_Insurance1
From #LD_Insurance1 a 
Group By a.LD_BrId
--select * from #Total_Insurance return
--select * from #LD_Fee
--return
----Loan Disbursment
Select 
       a.LD_ID, case when a.CU_ID=1 then sum(a.LD_Dis_Amt) else 0 end KHR,case when a.CU_ID=2 then sum(a.LD_Dis_Amt) else 0 end USD
       ,        a.LD_BrId,       a.CU_ID
Into #LD_Dis1
From BK_Loan a
Where a.LD_BrId = @BrID
      and a.LD_Dis_Date <= @SDate-1
  group by a.LD_ID,a.LD_BrId,a.CU_ID
  
Select a.LD_BrId,
    sum(a.KHR) KHR,sum(a.USD)USD
Into #Total_Dis1
From #LD_Dis1 a 
Group By a.LD_BrId

--Total Expense
Select 
       a.BrID,
     case when OPCurrency=N'រៀល' then sum(OPCost) else 0 end KHR,
        case when OPCurrency=N'ដុល្លារ' then sum(OPCost) else 0 end USD,
       a.OPCurrency
Into #EX1
From ExpenseOperation a 
Where a.BrID = @BrID and a.OPDate <= @SDate-1 group by a.BrID,OPCurrency
Select a.BrID,
   SUM( a.KHR) KHR,sum(a.USD) USD
Into #Total_Ex1
From #EX1 a
Group by a.BrID
--Owner Withdrawal and Deposit
Select a.BrID,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Deposit1
From OwnerTransaction a 
where a.BrID = @BrID
     and a.OPDate <= @SDate-1 and a.OPType='Deposit'
	  group by a.BrID;

Select a.BrID,
       sum(a.USD) USD,
       sum(a.KHR) KHR
Into #Total_Withdrawal1
From OwnerTransaction a 
where a.BrID = @BrID
     and a.OPDate <= @SDate-1 and a.OPType='Withdrawal' 
	 group by a.BrID;

select BrId,SUM( USD)USD,SUM( KHR)KHR 
into #Total_USDTOKHR1
from BK_Exchange 
where Date_Operation<=@SDate-1 and Type='USDTOKHR'
group by BrId

select BrId,SUM( USD)USD,SUM( KHR)KHR 
into #Total_KHRTOUSD1
from BK_Exchange 
where Date_Operation<=@SDate-1 and Type='KHRTOUSD'
group by BrId
------------------------
select BrId
,case when CU_ID=1 then sum(isnull(Amount,0)) else 0 end KHR 
,case when CU_ID=2 then sum(isnull(Amount,0)) else 0 end USD
into #OtherTem1
from BK_OtherIncome 
where BrId like @BrID and Date_Operation <=@SDate-1 group by BrId,CU_ID
--
select BrId,sum(isnull(KHR,0))KHR,sum(isnull(USD,0))USD 
into #OtherTotal1 from #OtherTem1 Group by BrId
-----
select a.CompanyID,a.CompanyKhmerName,@SDate-1 dates,
 (isnull(b.KHR,0)+isnull(c.KHR,0)+isnull(i.KHR,0)+isnull(j.KHR,0)+isnull(j.RefKHR,0)+isnull(c.Charge_KHR,0)+isnull(l.KHR,0)+isnull(s.KHR,0))
 - (isnull(d.KHR,0)+isnull(e.KHR,0)+isnull(f.KHR,0)+isnull(h.KHR,0)) KHR 
,(isnull(b.USD,0)+isnull(c.USD,0)+isnull(h.USD,0)+isnull(j.USD,0)+isnull(j.RefUSD,0)+isnull(c.Charge_USD,0)+isnull(l.USD,0)+isnull(s.USD,0))
- (isnull(d.USD,0)+isnull(e.USD,0)+isnull(f.USD,0)+isnull(i.USD,0))  USD 
into #firstBalance
from BK_Company a
------------------------ Income
left join #Total_Deposit1 b on a.CompanyID=b.BrID
left join #Total_Fee1 j on a.CompanyID=j.LD_BrId
left join #TotalRepay1 c on a.CompanyID=c.LR_BrID
left join #OtherTotal1 s on a.CompanyID=s.BrId
------------------------ Expense
left join #Total_Withdrawal1 d on a.CompanyID=d.BrID
left join #Total_Dis1 e on a.CompanyID=e.LD_BrId
left join #Total_Ex1 f on a.CompanyID=f.BrID
left join #Total_KHRTOUSD1 h on a.CompanyID=h.BrId
left join #Total_USDTOKHR1 i on a.CompanyID=i.BrId
left join #Total_Insurance1 l on a.CompanyID=j.LD_BrId

where a.CompanyID=@BrID

--select * from #firstBalance return
-----
--select a.CompanyID,a.CompanyKhmerName,
-- (isnull(b.KHR,0)+isnull(c.KHR,0)+isnull(i.KHR,0)+isnull(j.KHR,0)+isnull(j.RefKHR,0)+c.Charge_KHR+l.KHR+isnull(s.KHR,0))
-- - (isnull(d.KHR,0)+isnull(e.KHR,0)+isnull(f.KHR,0)+isnull(h.KHR,0)) KHR 
--,(isnull(b.USD,0)+isnull(c.USD,0)+isnull(h.USD,0)+isnull(j.USD,0)+isnull(j.RefUSD,0)+c.Charge_USD+l.USD+isnull(s.USD,0))
--- (isnull(d.USD,0)+isnull(e.USD,0)+isnull(f.USD,0)+isnull(i.USD,0))  USD 

--from BK_Company a
-------------------------- Income
--left join #Total_Deposit b on a.CompanyID=b.BrID
--left join #Total_Fee j on a.CompanyID=j.LD_BrId
--left join #TotalRepay c on a.CompanyID=c.LR_BrID
--left join #OtherTotal s on a.CompanyID=s.BrId
-------------------------- Expense
--left join #Total_Withdrawal d on a.CompanyID=d.BrID
--left join #Total_Dis e on a.CompanyID=e.LD_BrId
--left join #Total_Ex f on a.CompanyID=f.BrID
--left join #Total_KHRTOUSD h on a.CompanyID=h.BrId
--left join #Total_USDTOKHR i on a.CompanyID=i.BrId
--left join #Total_Insurance l on a.CompanyID=j.LD_BrId

--where a.CompanyID=@BrID


--print  return
--------------------------------------------------------- Create table
Create Table #LD(
TxnOrder Int,
TxnDate Date,
BrID Varchar(12),
BrName NVarchar(50),
Total_Deposit_KH Numeric(18,2),
Total_Deposit_USD Numeric(18,2),
Total_AdminFee_KHR Numeric(18,2),
Total_AdminFee_USD Numeric(18,2),
Total_RefFee_KHR Numeric(18,2),
Total_RefFee_USD Numeric(18,2),
TotalRepay_KHR Numeric(18,2),
TotalRepay_USD Numeric(18,2),
TotalCharge_KHR Numeric(18,2),
TotalCharge_USD Numeric(18,2),
OtherTotal_KHR Numeric(18,2),
OtherTotal_USD Numeric(18,2),
Total_Insurance_KHR Numeric(18,2),
Total_Insurance_USD Numeric(18,2),


Total_Withdrawal_KH Numeric(18,2),
Total_Withdrawal_USD Numeric(18,2),
Total_Dis_KHR Numeric(18,2),
Total_Dis_USD Numeric(18,2),
Total_Ex_KHR Numeric(18,2),
Total_Ex_USD Numeric(18,2),
Total_KHRTOUSD_KHR Numeric(18,2),
Total_KHRTOUSD_USD Numeric(18,2),
Total_USDTOKHR_KHR Numeric(18,2),
Total_USDTOKHR_USD Numeric(18,2),

EndBalKHR Numeric(18,2),
EndBalUSD Numeric(18,2)
);
--Insert Into #LD(TxnOrder,EndBalKHR,EndBalUSD) Values(1,@BBalKHR,@BBalUSD);
--start balance
Insert Into #LD(TxnOrder,BrID,EndBalKHR,EndBalUSD)
Select 1,a.CompanyID,a.KHR,a.USD From #firstBalance a;
-- Deposit
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Deposit_KH,Total_Deposit_USD)
Select 2,a.OPDate,a.BrID,a.KHR,a.USD From #Total_Deposit a;
--AdminFee
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_AdminFee_KHR,Total_AdminFee_USD)
Select 2,a.LD_Dis_Date,a.LD_BrId,a.KHR,a.USD From #Total_Fee a;
------ RefFee
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_RefFee_KHR,Total_RefFee_USD)
Select 2,a.LD_Dis_Date,a.LD_BrId,a.RefKHR,a.RefUSD From #Total_Fee a;
--Repay
Insert Into #LD(TxnOrder,TxnDate,BrID,TotalRepay_KHR,TotalRepay_USD)
Select 2,a.LR_Date,a.LR_BrID,a.KHR,a.USD From #TotalRepay a;
-- Charge
Insert Into #LD(TxnOrder,TxnDate,BrID,TotalCharge_KHR,TotalCharge_USD)
Select 2,a.LR_Date,a.LR_BrID,a.Charge_KHR,a.Charge_USD From #TotalRepay a;
--Other Income
Insert Into #LD(TxnOrder,TxnDate,BrID,OtherTotal_KHR,OtherTotal_USD)
Select 2,a.Date_Operation,a.BrId,a.KHR,a.USD From #OtherTotal a;
--Insurense
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Insurance_KHR,Total_Insurance_USD)
Select 2,a.LD_Dis_Date,a.LD_BrId,a.KHR,a.USD From #Total_Insurance a;


------------------------------Expense
--Withdrawal
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Withdrawal_KH,Total_Withdrawal_USD)
Select 2,a.OPDate,a.BrID,a.KHR,a.USD From #Total_Withdrawal a;

--Dis
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Dis_KHR,Total_Dis_USD)
Select 2,a.LD_Dis_Date,a.LD_BrId,a.KHR,a.USD From #Total_Dis a;

--Ex
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_Ex_KHR,Total_Ex_USD)
Select 2,a.OPDate,a.BrID,a.KHR,a.USD From #Total_Ex a;

--KHRTOUSD
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_KHRTOUSD_KHR,Total_KHRTOUSD_USD)
Select 2,a.Date_Operation,a.BrId,a.KHR,a.USD From #Total_KHRTOUSD a;

---USDTOKHR
Insert Into #LD(TxnOrder,TxnDate,BrID,Total_USDTOKHR_KHR,Total_USDTOKHR_USD)
Select 2,a.Date_Operation,a.BrId,a.KHR,a.USD From #Total_USDTOKHR a;
--select * from #Total_Deposit
--select * from #Total_Fee

Select a.TxnOrder,
       a.TxnDate,
       a.BrID,
       b.CompanyKhmerName as 'BrName',
       Sum(Total_Deposit_KH) as Total_Deposit_KH ,
       Sum(Total_Deposit_USD) as Total_Deposit_USD ,
       Sum(Total_AdminFee_KHR) as Total_AdminFee_KHR ,
       Sum(Total_AdminFee_USD) as Total_AdminFee_USD ,
	    Sum(Total_RefFee_KHR) as Total_RefFee_KHR ,
       Sum(Total_RefFee_USD) as Total_RefFee_USD ,
       Sum(TotalRepay_KHR) as TotalRepay_KHR,
       Sum(TotalRepay_USD) as TotalRepay_USD ,
	      Sum(TotalCharge_KHR) as TotalCharge_KHR,
       Sum(TotalCharge_KHR) as TotalCharge_USD ,
       Sum(OtherTotal_KHR) as OtherTotal_KHR ,
       Sum(OtherTotal_USD) as OtherTotal_USD ,
       Sum(Total_Insurance_KHR) as Total_Insurance_KHR ,
       Sum(Total_Insurance_USD) as Total_Insurance_USD ,

	   Sum(Total_Withdrawal_KH) as Total_Withdrawal_KH ,
       Sum(Total_Withdrawal_USD) as Total_Withdrawal_USD ,
       Sum(Total_Dis_KHR) as Total_Dis_KHR ,
       Sum(Total_Dis_USD) as Total_Dis_USD ,
       Sum(Total_Ex_KHR) as Total_Ex_KHR,
       Sum(Total_Ex_USD) as Total_Ex_USD ,
       Sum(Total_KHRTOUSD_KHR) as Total_KHRTOUSD_KHR ,
       Sum(Total_KHRTOUSD_USD) as Total_KHRTOUSD_USD ,
       Sum(Total_USDTOKHR_KHR) as Total_USDTOKHR_KHR ,
       Sum(Total_USDTOKHR_USD) as Total_USDTOKHR_USD ,
       Sum(EndBalKHR) as EndBalKHR ,
       Sum(EndBalUSD) as EndBalUSD
Into #LDLD1
From #LD a Left join BK_Company b on a.BrID COLLATE DATABASE_DEFAULT = b.CompanyID COLLATE DATABASE_DEFAULT
Group By a.TxnOrder,a.TxnDate,a.BrID,b.CompanyKhmerName;

--select* from #LDLD1 return
select 
@EKHR= a.EndBalKHR,@EUSD=a.EndBalUSD
from #LDLD1 a
Select 
--a.TxnOrder,
       Convert(Varchar(12),a.TxnDate,101) as 'TxnDate',
       a.BrID,
       a.BrName,
       (ISNULL(a.Total_Deposit_KH,0) + ISNULL(a.Total_AdminFee_KHR,0)+ ISNULL(a.Total_RefFee_KHR,0)+ ISNULL(a.TotalCharge_KHR,0) + ISNULL(a.TotalRepay_KHR,0) + ISNULL(a.OtherTotal_KHR,0) + ISNULL(a.Total_Insurance_KHR,0)
	   + ISNULL(a.Total_USDTOKHR_KHR,0))
	   -  (ISNULL(a.Total_Withdrawal_KH,0) + ISNULL(a.Total_Dis_KHR,0) + ISNULL(a.Total_Ex_KHR,0) + ISNULL(a.Total_KHRTOUSD_KHR,0) ) as 'TotalKHR',
	     (ISNULL(a.Total_Deposit_USD,0) + ISNULL(a.Total_AdminFee_USD,0) + ISNULL(a.TotalRepay_USD,0) + ISNULL(a.OtherTotal_USD,0) + ISNULL(a.Total_Insurance_USD,0)
	   + ISNULL(a.Total_KHRTOUSD_USD,0))
	   -  (ISNULL(a.Total_Withdrawal_USD,0) + ISNULL(a.Total_Dis_USD,0) + ISNULL(a.Total_Ex_USD,0) + ISNULL(a.Total_KHRTOUSD_USD,0) ) as 'TotalUSD',
       --ISNULL(a.DepositUSD,0) - ISNULL(a.WithdrawalUSD,0) + ISNULL(a.LRAmtUSD,0) - ISNULL(a.LDAmtUSD,0) - ISNULL(a.TotalExpUSD,0) as 'TotalUSD',
       a.EndBalKHR,
       a.EndBalUSD
	   --, (ISNULL(a.Total_Deposit_KH,0) + ISNULL(a.Total_AdminFee_KHR,0)+ ISNULL(a.Total_RefFee_KHR,0)+ ISNULL(a.TotalCharge_KHR,0) + ISNULL(a.TotalRepay_KHR,0) + ISNULL(a.OtherTotal_KHR,0) + ISNULL(a.Total_Insurance_KHR,0)
	   --+ ISNULL(a.Total_USDTOKHR_KHR,0)) income
	   --,ISNULL(a.Total_Deposit_KH,0)Total_Deposit_KH
	   --, ISNULL(a.Total_AdminFee_KHR,0)Total_AdminFee_KHR
	   --, ISNULL(a.Total_RefFee_KHR,0)Total_RefFee_KHR
	   --, ISNULL(a.TotalCharge_KHR,0)TotalCharge_KHR
	   --, ISNULL(a.TotalRepay_KHR,0)TotalRepay_KHR
	   --,ISNULL(a.OtherTotal_KHR,0)OtherTotal_KHR
	   --, ISNULL(a.Total_Insurance_KHR,0)Total_Insurance_KHR
	   --, ISNULL(a.Total_USDTOKHR_KHR,0)Total_USDTOKHR_KHR


into #Final
From #LDLD1 a




select * from #Final
End



--select * from OwnerTransaction

GO

-- ===== sp_GetLoanToWriteoff =====



ALTER PROCEDURE [dbo].[sp_GetLoanToWriteoff](
@LDID Varchar(12),
@BrID Varchar(12)
)
AS
BEGIN
	SET NOCOUNT ON;
	
	


Select a.LD_ID
		,a.LD_BrId
		,a.EM_ID
		,b.EM_Name
		,a.CM_ID
		,c.CM_KhName
		,c.CM_Phone
		,d.VL_ID + ',' + d.CN_ID +','+ d.DT_ID +','+d.PV_ID as 'CM_Address'
		,a.LD_Dis_Date
		,a.LD_Mat_Date
		
		,a.LD_Dis_Amt
		,a.CU_ID
Into #LD1
From BK_Loan a inner join BK_Employee b on a.EM_ID = b.EM_ID and a.LD_BrId = b.EM_BrID
			   inner join BK_Customer c on a.CM_ID = c.CM_ID and a.LD_BrId = c.CM_BrId
			   inner join BK_Location d on d.LO_ID = c.LO_ID and d.LO_BrID = c.CM_BrId
Where a.LD_ID= @LDID
	  and a.LD_BrId= @BrID
	  and a.LD_Status='Active'


Select a.LD_ID,
a.LR_BrID,
Sum(b.SH_Prn_Amt) as PrnPaid
Into #LD2
From dbo.BK_LoanRepay a Inner Join dbo.BK_LoanSchedule b On a.LD_ID=b.LD_ID and a.LR_BrID=b.SH_BrId and a.SH_Date=b.SH_Date
Where a.LD_ID = @LDID
and a.LR_BrID = @BrID
Group By a.LD_ID,a.LR_BrID;


select a.LD_ID 
,a.LD_BrId
,a.EM_ID
,a.EM_Name
,a.CM_ID
,a.CM_KhName
,a.CM_Phone
,a.CM_Address
,a.LD_Dis_Date
,a.LD_Mat_Date
,a.LD_Dis_Amt
,a.LD_Dis_Amt - ISNULL(b.PrnPaid,0) as 'Loan_OS',
a.CU_ID as 'Currency'
From #LD1 a Left Join #LD2 b On a.LD_ID=b.LD_ID and a.LD_BrId = b.LR_BrID


END

GO

-- ===== sp_GetListLoanWriteoff =====



ALTER PROCEDURE [dbo].[sp_GetListLoanWriteoff](
@StartDate Varchar(12),
@EndDate Varchar(12),
@BrID Varchar(12)
)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SDate as Date;
	Declare @EDate as Date;
	Set @SDate = @StartDate;
	Set @EDate = @EndDate;


Select a.LD_ID
,e.WOF_Date
		--,a.LD_BrId
		--,a.EM_ID
		--,b.EM_Name
		,a.CM_ID
		,c.CM_KhName
		--,c.CM_Phone
		,d.VL_ID + ',' + d.CN_ID +','+ d.DT_ID +','+d.PV_ID as 'CM_Address'
		,a.LD_Dis_Date
		--,a.LD_Mat_Date

		,a.LD_Dis_Amt
				,e.LD_OS
		--,a.Note
From BK_Loan a inner join BK_Employee b on a.EM_ID = b.EM_ID and a.LD_BrId = b.EM_BrID
			   inner join BK_Customer c on a.CM_ID = c.CM_ID and a.LD_BrId = c.CM_BrId
			   inner join BK_Location d on d.LO_ID = c.LO_ID and d.LO_BrID = c.CM_BrId
			   inner join Writeoff e on a.LD_ID= e.LD_ID and a.LD_BrId = e.BR_ID
Where a.LD_BrId= @BrID
	  and e.WOF_Date between @SDate and @EDate

END


--select * from Writeoff

GO

-- ===== spGetLoanRepayDetailAudit =====

ALTER PROCEDURE [dbo].[spGetLoanRepayDetailAudit] (@LDID Varchar(12),@LDBrID Varchar(12))
AS
BEGIN
SET NOCOUNT ON;

Declare @Payoff as Nvarchar(100);
Declare @Repay as Nvarchar(100);
Set @Payoff =N'បង់ផ្ដាច់';
Set @Repay =N'យកប្រាក់ពីអតិថិជន';

SELECT a.LD_ID,
       a.LR_BrID,
       a.LR_Description,
       a.LR_Amount as LR_Amount, 
	   a.LR_Charge as LR_Charge,
	   a.LR_Date as LR_Date,
	   a.SH_Date,a.Prn,isnull(a.Int,0)IntPaid,isnull(a.LR_Service,0)ServicePaid
Into #LR
FROM dbo.BK_LoanRepay a 
Where a.LD_ID=@LDID AND a.LR_BrID=@LDBrID
--Group By a.LD_ID,
--       a.LR_BrID,
--	   a.SH_Date,
--	   a.LR_Description
----------------------------------------------------------
SELECT Convert(Varchar(12),b.SH_Date,101) as DateToPay,
	b.SH_Prn_Amt + b.SH_Int_Amt as 'Amt_ToPay',
	b.SH_Prn_Amt 'Prn_Amt', 
	b.SH_Int_Amt'Int_Amt',
	b.SH_Service'Service_Amt',case when a.LR_Description=@Repay then 'Repay' else case when a.LR_Description=@Payoff   then'PayOff' else '' end end LR_Description,
		Case When a.LR_Date is null then '' Else Convert(Varchar(12),ISNULL(a.LR_Date,''),101) End as PaidDate,
		datediff(day,LR_Date,a.SH_Date) [Day],a.Prn PrnPaid,a.IntPaid,a.ServicePaid,
	a.LR_Amount'Amt_Paid', 
	a.LR_Charge'Amt_Charge',
		b.SH_Balance'Balance'
FROM #LR a RIGHT JOIN dbo.BK_LoanSchedule b ON a.LD_ID = b.LD_ID AND a.LR_BrID = b.SH_BrId AND a.SH_Date = b.SH_Date
Where b.LD_ID=@LDID AND b.SH_BrId=@LDBrID
order by b.SH_Date 

END

GO

