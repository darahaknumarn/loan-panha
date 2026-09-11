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





