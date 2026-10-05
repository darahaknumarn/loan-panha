



CREATE PROCEDURE [dbo].[SP_IMPORT]
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





