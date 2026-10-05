ALTER PROCEDURE [dbo].[SP_IMPORT]
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY

	PRINT 'Import Location.';
	INSERT INTO pheap.dbo.BK_Location ([LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete])
	SELECT [LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete]
	FROM Temppheap.dbo.BK_Location;
    
    PRINT 'Import customer.';
	INSERT INTO pheap.dbo.BK_Customer ([CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change])
	SELECT [CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change]
	FROM Temppheap.dbo.BK_Customer;
    
    PRINT 'Import position.';
	INSERT INTO pheap.dbo.BK_Position ([ID],[BrID],[Position],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [ID],[BrID],[Position],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.BK_Position;

	PRINT 'Import employee.';
	INSERT INTO pheap.dbo.BK_Employee ([EM_ID],[EM_Name],[EM_BrID],[Position],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [EM_ID],[EM_Name],[EM_BrID],[Position],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.BK_Employee;
    
    PRINT 'Import loan.';
	INSERT INTO pheap.dbo.BK_Loan ([LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate],[LD_ChargeRate],[LD_ChargeAmt],[Date_Payoff],[CM_ID1],[LD_Service],[LD_InRate],[LD_InAmt],[PayOff],[Ref])
	SELECT [LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate],[LD_ChargeRate],[LD_ChargeAmt],[Date_Payoff],[CM_ID1],[LD_Service],[LD_InRate],[LD_InAmt],[PayOff],[Ref]
	FROM Temppheap.dbo.BK_Loan;
	
	PRINT 'Import loan repay.';
	INSERT INTO pheap.dbo.BK_LoanRepay ([LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport],[Prn],[Int],[CM_ID1],[LR_Service],[Mark])
	SELECT [LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport],[Prn],[Int],[CM_ID1],[LR_Service],[Mark]
	FROM [Temppheap].[dbo].[BK_LoanRepay];
	
	PRINT 'Import loan schedule.';
	INSERT INTO pheap.dbo.BK_LoanSchedule ([LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt],[SH_Service])
	SELECT [LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt],[SH_Service]
	FROM [Temppheap].[dbo].[BK_LoanSchedule];
	
	PRINT 'Import trace customer';
    INSERT INTO [pheap].[dbo].[TRACE_Customer] ([DateAction],[RecordAction],[CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change])
    SELECT [DateAction],[RecordAction],[CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change]
    FROM [Temppheap].[dbo].[TRACE_Customer];
	
	Print 'Import trace loan.';
	INSERT INTO [pheap].[dbo].[TRACE_Loan] ([DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate],[LD_ChargeRate],[LD_ChargeAmt],[Date_Payoff],[CM_ID1],[LD_Service],[LD_InRate],[LD_InAmt],[PayOff],[Ref])
	SELECT [DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate],[LD_ChargeRate],[LD_ChargeAmt],[Date_Payoff],[CM_ID1],[LD_Service],[LD_InRate],[LD_InAmt],[PayOff],[Ref]
	FROM [Temppheap].[dbo].[TRACE_Loan];

	Print 'Import trace loan repay.';
	INSERT INTO [pheap].[dbo].[TRACE_LoanRepay] ([LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport],[Mark])
	SELECT [LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport],[Mark]
	FROM [Temppheap].[dbo].[TRACE_LoanRepay];

	Print 'Import trace location.';
	INSERT INTO [pheap].[dbo].[TRACE_Location] ([DateAction],[RecordAction],[LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete])
	SELECT [DateAction],[RecordAction],[LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete]
	FROM [Temppheap].[dbo].[TRACE_Location];
	
	Print 'Import table user.';
	INSERT INTO [pheap].[dbo].[sys_User] ([User_Name],[BrID],[Full_Name],[Lock],[Lock_Date],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[PassWords],[IsExport])
	SELECT [User_Name],[BrID],[Full_Name],[Lock],[Lock_Date],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[PassWords],[IsExport]
	FROM [Temppheap].[dbo].[sys_User]; 
	
	Print 'Import asset table.';
	INSERT INTO pheap.dbo.Asset ([ASID],[BrID],[Name],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Term])
	SELECT [ASID],[BrID],[Name],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Term]
	FROM Temppheap.dbo.Asset;
	
	Print 'Import expense operation.';
	INSERT INTO [pheap].[dbo].[ExpenseOperation] ([BrID],[OPDate],[EM_ID],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[InNo],[Supplier])
	SELECT [BrID],[OPDate],[EM_ID],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[InNo],[Supplier]
	FROM [Temppheap].[dbo].[ExpenseOperation];

	Print 'Import expense schedule.';
	INSERT INTO pheap.dbo.ExpenseSchedule ([OPCode],[BrID],[ExDate],[ExCost],[ExAccumulate],[ASBalance],[Date_Create])
	SELECT [OPCode],[BrID],[ExDate],[ExCost],[ExAccumulate],[ASBalance],[Date_Create]
	FROM Temppheap.dbo.ExpenseSchedule;
	
	Print 'Import trace asset table.';
	INSERT INTO pheap.dbo.TRACE_Asset ([DateAction],[RecordAction],[ASID],[BrID],[Name],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Term])
	SELECT [DateAction],[RecordAction],[ASID],[BrID],[Name],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Term]
	FROM Temppheap.dbo.TRACE_Asset;
	
	Print 'Import trace expense operation.';
	INSERT INTO [pheap].[dbo].[TRACE_ExpenseOperation] ([DateAction],[RecordAction],[OPID],[BrID],[OPDate],[EM_ID],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[InNo],[Supplier])
	SELECT [DateAction],[RecordAction],[OPID],[BrID],[OPDate],[EM_ID],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[InNo],[Supplier]
	FROM [Temppheap].[dbo].[TRACE_ExpenseOperation];
	
	Print 'Import trace loan schedule table.';
	INSERT INTO pheap.dbo.TRACE_LoanSchedule ([DateAction],[RecordAction],[SH_ID],[LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt])
	SELECT [DateAction],[RecordAction],[SH_ID],[LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt]
	FROM Temppheap.dbo.TRACE_LoanSchedule;
	
	Print 'Import trace expense schedule.';
	INSERT INTO [pheap].[dbo].[TRACE_ExpenseSchedule] ([DateAction],[RecordAction],[OPCode],[BrID],[ExDate],[ExCost],[ExAccumulate],[ASBalance],[Date_Create])
	SELECT [DateAction],[RecordAction],[OPCode],[BrID],[ExDate],[ExCost],[ExAccumulate],[ASBalance],[Date_Create]
	FROM [Temppheap].[dbo].[TRACE_ExpenseSchedule];
	
	Print 'Import owner transaction';
	INSERT INTO pheap.dbo.OwnerTransaction ([BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.OwnerTransaction;
	
	Print 'Import trace owner transaction';
	INSERT INTO pheap.dbo.TRACE_OwnerTransaction ([DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.TRACE_OwnerTransaction;
	
	Print 'Import Writeoff';
	INSERT INTO pheap.dbo.Writeoff ([LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.Writeoff;
	
	Print 'Import trace Writeoff';
	INSERT INTO pheap.dbo.TRACE_Writeoff ([DateAction],[RecordAction],[LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [DateAction],[RecordAction],[LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.TRACE_Writeoff;

	Print 'Import Profit Cutout';
	INSERT INTO pheap.dbo.ProfitCutout ([OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.ProfitCutout;
	
	Print 'Import trace Profit Cutout';
	INSERT INTO pheap.dbo.TRACE_ProfitCutout ([DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.TRACE_ProfitCutout;

	PRINT 'Import Other Deposit.';
	INSERT INTO pheap.dbo.OtherDeposit ([LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_Dis_Amt],[CU_ID],[LD_IntRate],[EM_ID],[LD_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[IsExport],[Date_Payoff],[CM_ID1])
	SELECT [LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_Dis_Amt],[CU_ID],[LD_IntRate],[EM_ID],[LD_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[IsExport],[Date_Payoff],[CM_ID1]
	FROM Temppheap.dbo.OtherDeposit;
	
	PRINT 'Import Other Deposit Repay.';
	INSERT INTO pheap.dbo.OtherDepositRepay ([LR_ID],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [LR_ID],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport]
	FROM [Temppheap].[dbo].[OtherDepositRepay];
	
	Print 'Import trace other deposit.';
	INSERT INTO [pheap].[dbo].[TRACE_OtherDeposit] ([DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff])
	SELECT [DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff]
	FROM [Temppheap].[dbo].[TRACE_OtherDeposit];

	Print 'Import trace other deposit repay.';
	INSERT INTO [pheap].[dbo].[TRACE_OtherDepositRepay] ([LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport]
	FROM [Temppheap].[dbo].[TRACE_OtherDepositRepay];

	Print 'Import OtherIncome';
	INSERT INTO pheap.dbo.OtherIncome ([OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.OtherIncome;
	
	Print 'Import trace OtherIncome';
	INSERT INTO pheap.dbo.TRACE_OtherIncome ([DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM Temppheap.dbo.TRACE_OtherIncome;
	
	
	PRINT 'Import BK_SavingRepay.';
	INSERT INTO pheap.dbo.BK_SavingRepay ([LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport])
	SELECT [LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport]
	FROM [Temppheap].[dbo].[BK_SavingRepay];

	Print 'Import TRACE_SavingRepay.';
	INSERT INTO [pheap].[dbo].[TRACE_SavingRepay] ([SR_ID],[DateAction],[RecordAction],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport])
	SELECT [SR_ID],[DateAction],[RecordAction],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport]
	FROM [Temppheap].[dbo].[TRACE_SavingRepay];

	

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





