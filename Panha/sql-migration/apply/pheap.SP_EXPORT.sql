
ALTER PROCEDURE [dbo].[SP_EXPORT](@Date DateTime,@Branch varchar(12))
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @StrDate Varchar(12) ;
    SET @StrDate= CONVERT(VARCHAR(12),@Date,101);
	UPDATE Temppheap.dbo.SYS_IMPORT_EXPORT SET [Value]=@StrDate WHERE ID='EXPORT_DATE';
	UPDATE Temppheap.dbo.SYS_IMPORT_EXPORT SET [Value]=@Branch WHERE ID='BRANCH';
truncate table Temppheap.dbo.BK_Customer 
truncate table Temppheap.dbo.BK_ChangeCustomer 
truncate table Temppheap.dbo.BK_CustomerOther 
truncate table Temppheap.dbo.BK_Employee 
truncate table Temppheap.dbo.BK_Exchange 
truncate table Temppheap.dbo.BK_Loan 
truncate table Temppheap.dbo.BK_LoanRepay 
truncate table Temppheap.dbo.BK_LoanSchedule 
truncate table Temppheap.dbo.BK_Location 
truncate table Temppheap.dbo.BK_LocationOther 
truncate table Temppheap.dbo.BK_SavingRepay 
truncate table Temppheap.dbo.Bank_Transaction 
truncate table Temppheap.dbo.ExpenseOperation 
truncate table Temppheap.dbo.ExpenseSchedule 
truncate table Temppheap.dbo.OtherDeposit 
truncate table Temppheap.dbo.OtherDepositRepay 
truncate table Temppheap.dbo.OtherIncome 
truncate table Temppheap.dbo.OwnerTransaction 
truncate table Temppheap.dbo.ProfitCutout
truncate table Temppheap.dbo.TRACE_Asset 
truncate table Temppheap.dbo.TRACE_Bank 
truncate table Temppheap.dbo.TRACE_Customer 
truncate table Temppheap.dbo.TRACE_CustomerOther 
truncate table Temppheap.dbo.TRACE_Employee 
truncate table Temppheap.dbo.TRACE_Exchange
truncate table Temppheap.dbo.TRACE_ExpenseOperation 
truncate table Temppheap.dbo.TRACE_ExpenseSchedule 
truncate table Temppheap.dbo.TRACE_Holiday 
truncate table Temppheap.dbo.TRACE_Loan 
truncate table Temppheap.dbo.TRACE_LoanRepay 
truncate table Temppheap.dbo.TRACE_LoanSchedule 
truncate table Temppheap.dbo.TRACE_Location 
truncate table Temppheap.dbo.TRACE_OtherDeposit 
truncate table Temppheap.dbo.TRACE_OtherDepositRepay 
truncate table Temppheap.dbo.TRACE_OtherIncome 
truncate table Temppheap.dbo.TRACE_OwnerTransaction 
truncate table Temppheap.dbo.TRACE_ProfitCutout 
truncate table Temppheap.dbo.TRACE_SavingRepay 
truncate table Temppheap.dbo.TRACE_Writeoff 
truncate table Temppheap.dbo.Writeoff 

	PRINT 'Delete and insert temporary table Location.';
	DELETE FROM Temppheap.dbo.BK_Location;
	INSERT INTO Temppheap.dbo.BK_Location ([LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete])
	SELECT [LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete]
	FROM pheap.dbo.BK_Location 
	WHERE CONVERT(VARCHAR(12),LO_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LO_Date_Modify,101)=@StrDate;

	--return
    PRINT 'Delete and export temporary table customer.';
	DELETE FROM Temppheap.dbo.BK_Customer;
	INSERT INTO Temppheap.dbo.BK_Customer ([CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change])
	SELECT [CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change]
	FROM pheap.dbo.BK_Customer 
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;

	PRINT 'Delete and export temporary table customer other.';
	DELETE FROM Temppheap.[dbo].[BK_CustomerOther]
	INSERT INTO Temppheap.[dbo].[BK_CustomerOther] ([CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change])
	SELECT [CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change]
	FROM pheap.[dbo].[BK_CustomerOther]
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;

	PRINT 'Delete and export temporary table trace customer other.';
	DELETE FROM Temppheap.[dbo].TRACE_CustomerOther
	SET IDENTITY_INSERT Temppheap.[dbo].TRACE_CustomerOther ON;
	INSERT INTO Temppheap.[dbo].TRACE_CustomerOther ([AutoID],[DateAction],[RecordAction],[CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change])
	SELECT [AutoID],[DateAction],[RecordAction],[CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change]
	FROM pheap.[dbo].TRACE_CustomerOther
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].TRACE_CustomerOther OFF;


	PRINT 'Delete and export temporary table Exchange.';
	DELETE FROM Temppheap.[dbo].[BK_Exchange]
	SET IDENTITY_INSERT Temppheap.[dbo].[BK_Exchange] ON;
	INSERT INTO Temppheap.[dbo].[BK_Exchange] ([ID],[Date_Operation],[Ex_Rate],[USD],[KHR],[Descriptions],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Type],[BrId])
	SELECT [ID],[Date_Operation],[Ex_Rate],[USD],[KHR],[Descriptions],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Type],[BrId]
	FROM pheap.[dbo].[BK_Exchange]
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[BK_Exchange] OFF;

	PRINT 'Delete and export temporary table Trace Exchange.';
	DELETE FROM Temppheap.[dbo].TRACE_Exchange
	SET IDENTITY_INSERT Temppheap.[dbo].TRACE_Exchange ON;
	INSERT INTO Temppheap.[dbo].TRACE_Exchange ([AutoID],[DateAction],[RecordAction],[ID],[Date_Operation],[Ex_Rate],[USD],[KHR],[Descriptions],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Type],[BrId])
	SELECT [AutoID],[DateAction],[RecordAction],[ID],[Date_Operation],[Ex_Rate],[USD],[KHR],[Descriptions],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Type],[BrId]
	FROM pheap.[dbo].TRACE_Exchange
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].TRACE_Exchange OFF;

	PRINT 'Delete and export temporary table Bank Transaction';
	DELETE FROM Temppheap.[dbo].Bank_Transaction
	SET IDENTITY_INSERT Temppheap.[dbo].Bank_Transaction ON;
	INSERT INTO Temppheap.[dbo].Bank_Transaction ([ID],[Date_Operation],[T_ID],[USD],[KHR],[Descriptions],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[BrId],[IsExport])
	SELECT [ID],[Date_Operation],[T_ID],[USD],[KHR],[Descriptions],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[BrId],[IsExport]
	FROM pheap.[dbo].Bank_Transaction
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].Bank_Transaction OFF;

	PRINT 'Delete and export temporary table Trace Bank Transaction';
	DELETE FROM Temppheap.[dbo].TRACE_Bank
	INSERT INTO Temppheap.[dbo].TRACE_Bank ([DateAction],[RecordAction],[ID],[Date_Operation],[T_ID],[USD],[KHR],[Descriptions],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[BrId],[IsExport])
	SELECT [DateAction],[RecordAction],[ID],[Date_Operation],[T_ID],[USD],[KHR],[Descriptions],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[BrId],[IsExport]
	FROM pheap.[dbo].TRACE_Bank
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;



    PRINT 'Delete and export temporary table position.';
	DELETE FROM Temppheap.dbo.BK_Position;
	INSERT INTO Temppheap.dbo.BK_Position ([ID],[BrID],[Position],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [ID],[BrID],[Position],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.BK_Position 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;

	PRINT 'Delete and export temporary table Change customer.';
	DELETE FROM Temppheap.[dbo].[BK_ChangeCustomer];
	SET IDENTITY_INSERT Temppheap.[dbo].[BK_ChangeCustomer] ON;
	INSERT INTO Temppheap.[dbo].[BK_ChangeCustomer] ([ID],[CM_ID],[Name_Old],[Phone_Old],[LO_Old],[Name_New],[Phone_New],[LO_New],[Date],[User_Create],[Date_Create],[BrId],[ID_Old],[ID_New])
	SELECT [ID],[CM_ID],[Name_Old],[Phone_Old],[LO_Old],[Name_New],[Phone_New],[LO_New],[Date],[User_Create],[Date_Create],[BrId],[ID_Old],[ID_New]
	FROM pheap.[dbo].[BK_ChangeCustomer]
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[BK_ChangeCustomer] OFF;

	--select * from BK_ChangeCustomer
	PRINT 'Delete and export temporary table employee.';
	DELETE FROM Temppheap.dbo.BK_Employee;
	INSERT INTO Temppheap.dbo.BK_Employee ([EM_ID],[EM_Name],[EM_BrID],[Position],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [EM_ID],[EM_Name],[EM_BrID],[Position],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.BK_Employee 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;

    PRINT 'Delete and export temporary table pheap.';
	DELETE FROM Temppheap.dbo.BK_Loan;
	INSERT INTO Temppheap.dbo.BK_Loan ([LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate],[LD_ChargeRate],[LD_ChargeAmt],[Date_Payoff],[CM_ID1],[LD_Service],[LD_InRate],[LD_InAmt],[PayOff],[Ref])
	SELECT [LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate],[LD_ChargeRate],[LD_ChargeAmt],[Date_Payoff],[CM_ID1],[LD_Service],[LD_InRate],[LD_InAmt],[PayOff],[Ref]
	FROM pheap.dbo.BK_Loan 
	WHERE CONVERT(VARCHAR(12),LD_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LD_Date_Modify,101)=@StrDate;

	PRINT 'Delete and export temporary table pheap repay.';
	DELETE FROM Temppheap.dbo.BK_LoanRepay;
	SET IDENTITY_INSERT Temppheap.dbo.BK_LoanRepay ON;
	INSERT INTO Temppheap.dbo.BK_LoanRepay ([LR_ID],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport],[Prn],[Int],[CM_ID1],[LR_Service],[Mark])
	SELECT [LR_ID],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport],[Prn],[Int],[CM_ID1],[LR_Service],[Mark]
	FROM pheap.[dbo].BK_LoanRepay 
	WHERE CONVERT(VARCHAR(12),LR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LR_Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.dbo.BK_LoanRepay OFF;

	PRINT 'Delete and export temporary table pheap schedule.';
	DELETE FROM Temppheap.dbo.BK_LoanSchedule;
	SET IDENTITY_INSERT Temppheap.dbo.BK_LoanSchedule ON;
	INSERT INTO Temppheap.dbo.BK_LoanSchedule ([SH_ID],[LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt],[SH_Service])
	SELECT [SH_ID],[LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt],[SH_Service]
	FROM pheap.[dbo].[BK_LoanSchedule] 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.dbo.BK_LoanSchedule OFF;

	Print 'Delete and export temporary table trace customer.';
	Delete From Temppheap.dbo.TRACE_Customer;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_Customer] ON;
	INSERT INTO Temppheap.[dbo].[TRACE_Customer] ([AutoID],[DateAction],[RecordAction],[CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change])
	SELECT [AutoID],[DateAction],[RecordAction],[CM_ID],[CM_KhName],[LO_ID],[CM_Address],[CM_Phone],[CM_BrId],[CM_Rec_Status],[CM_User_Create],[CM_Date_Create],[CM_User_Modify],[CM_Date_Modify],[CM_User_Delete],[CM_Date_Delete],[LD_Cycle],[ID],[Status],[Date_Change]
	FROM pheap.[dbo].[TRACE_Customer] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_Customer] OFF;

	Print 'Delete and export temporary table trace pheap.';
	Delete From Temppheap.dbo.TRACE_Loan;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_Loan] ON;
	INSERT INTO Temppheap.[dbo].[TRACE_Loan] ([AutoID],[DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate],[LD_ChargeRate],[LD_ChargeAmt],[Date_Payoff],[CM_ID1],[LD_Service],[LD_InRate],[LD_InAmt],[PayOff],[Ref])
	SELECT [AutoID],[DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff],[LD_Cycle],[LD_Saving],[LD_SavingAmt],[LD_SavingRate],[LD_ChargeRate],[LD_ChargeAmt],[Date_Payoff],[CM_ID1],[LD_Service],[LD_InRate],[LD_InAmt],[PayOff],[Ref]
	FROM pheap.[dbo].[TRACE_Loan] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_Loan] OFF;

	Print 'Delete and export temporary table trace pheap repay.';
	Delete From Temppheap.dbo.TRACE_LoanRepay;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_LoanRepay] ON;
	INSERT INTO Temppheap.[dbo].[TRACE_LoanRepay] ([AutoID],[LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport],[Mark])
	SELECT [AutoID],[LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport],[Mark]
	FROM pheap.[dbo].[TRACE_LoanRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_LoanRepay] OFF;

	Print 'Delete and export temporary table trace location.';
	Delete From Temppheap.dbo.TRACE_Location;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_Location] ON;
	INSERT INTO Temppheap.[dbo].[TRACE_Location] ([AutoID],[DateAction],[RecordAction],[LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete])
	SELECT [AutoID],[DateAction],[RecordAction],[LO_ID],[VL_ID],[CN_ID],[DT_ID],[PV_ID],[LO_BrID],[LO_Rec_Status],[LO_User_Create],[LO_Date_Create],[LO_User_Modify],[LO_Date_Modify],[LO_User_Delete],[LO_Date_Delete]
	FROM pheap.[dbo].[TRACE_Location] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_Location] OFF;

	Print 'Delete and Export temporary table user.';
	Delete From Temppheap.dbo.sys_User;
	INSERT INTO Temppheap.[dbo].[sys_User] ([User_Name],[BrID],[Full_Name],[Lock],[Lock_Date],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[PassWords],[IsExport])
	SELECT [User_Name],[BrID],[Full_Name],[Lock],[Lock_Date],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[PassWords],[IsExport]
	FROM pheap.[dbo].[sys_User] WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export asset table';
	Delete From Temppheap.dbo.Asset;
	INSERT INTO Temppheap.dbo.Asset ([ASID],[BrID],[Name],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Term])
	SELECT [ASID],[BrID],[Name],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Term]
	FROM pheap.dbo.Asset A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export Expense Operation';
	Delete From Temppheap.dbo.ExpenseOperation;
	SET IDENTITY_INSERT Temppheap.[dbo].[ExpenseOperation] ON;
	INSERT INTO Temppheap.[dbo].[ExpenseOperation] ([OPID],[BrID],[OPDate],[EM_ID],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[InNo],[Supplier])
	SELECT [OPID],[BrID],[OPDate],[EM_ID],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[InNo],[Supplier]
	FROM pheap.[dbo].[ExpenseOperation] WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[ExpenseOperation] OFF; 

	Print 'Delete and Export Expense Schedule';
	Delete From Temppheap.dbo.ExpenseSchedule;
	INSERT INTO Temppheap.dbo.ExpenseSchedule ([OPCode],[BrID],[ExDate],[ExCost],[ExAccumulate],[ASBalance],[Date_Create])
	SELECT [OPCode],[BrID],[ExDate],[ExCost],[ExAccumulate],[ASBalance],[Date_Create]
	FROM pheap.dbo.ExpenseSchedule WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate;

	Print 'Delete and Export trace asset table';
	Delete From Temppheap.dbo.TRACE_Asset;
	INSERT INTO Temppheap.dbo.TRACE_Asset ([DateAction],[RecordAction],[ASID],[BrID],[Name],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Term])
	SELECT [DateAction],[RecordAction],[ASID],[BrID],[Name],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[Term]
	FROM pheap.dbo.TRACE_Asset A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	Print 'Delete and Export trace Expense Operation';
	Delete From Temppheap.dbo.TRACE_ExpenseOperation;
	INSERT INTO Temppheap.[dbo].[TRACE_ExpenseOperation] ([DateAction],[RecordAction],[OPID],[BrID],[OPDate],[EM_ID],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[InNo],[Supplier])
	SELECT [DateAction],[RecordAction],[OPID],[BrID],[OPDate],[EM_ID],[ASID],[OPDescription],[OPCurrency],[OPCost],[OPTerm],[OPCode],[OPAutoCode],[OPMatDate],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[InNo],[Supplier]
	FROM pheap.[dbo].[TRACE_ExpenseOperation] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	Print 'Delete and Export trace expense schedule';
	Delete From Temppheap.dbo.TRACE_ExpenseSchedule;
	INSERT INTO Temppheap.[dbo].[TRACE_ExpenseSchedule] ([DateAction],[RecordAction],[OPCode],[BrID],[ExDate],[ExCost],[ExAccumulate],[ASBalance],[Date_Create])
	SELECT [DateAction],[RecordAction],[OPCode],[BrID],[ExDate],[ExCost],[ExAccumulate],[ASBalance],[Date_Create]
	FROM pheap.[dbo].[TRACE_ExpenseSchedule] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;;

	Print 'Delete and Export trace pheap schedule';
	Delete From Temppheap.dbo.TRACE_LoanSchedule;
	INSERT INTO Temppheap.[dbo].TRACE_LoanSchedule ([DateAction],[RecordAction],[SH_ID],[LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt])
	SELECT [DateAction],[RecordAction],[SH_ID],[LD_ID],[CM_ID],[SH_BrId],[SH_Date],[SH_Prn_Org],[SH_Int_Org],[SH_Balance_Org],[SH_Prn_Amt],[SH_Int_Amt],[SH_Balance],[SH_PayoffAmt],[Status],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[SH_SavingAmt]
	FROM pheap.[dbo].TRACE_LoanSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	Print 'Delete and Export OwnerTransaction table';
	Delete From Temppheap.dbo.OwnerTransaction;
	SET IDENTITY_INSERT Temppheap.dbo.OwnerTransaction ON;
	INSERT INTO Temppheap.dbo.OwnerTransaction ([OPID],[BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [OPID],[BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.OwnerTransaction A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.dbo.OwnerTransaction OFF; 

	Print 'Delete and Export trace OwnerTransaction table';
	Delete From Temppheap.dbo.TRACE_OwnerTransaction;
	INSERT INTO Temppheap.dbo.TRACE_OwnerTransaction ([DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[USD],[KHR],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.TRACE_OwnerTransaction A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and Export writeoff table';
	Delete From Temppheap.dbo.Writeoff;
	INSERT INTO Temppheap.dbo.Writeoff ([LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.Writeoff A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and Export trace writeoff table';
	Delete From Temppheap.dbo.TRACE_Writeoff;
	INSERT INTO Temppheap.dbo.TRACE_Writeoff ([DateAction],[RecordAction],[LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [DateAction],[RecordAction],[LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.TRACE_Writeoff A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and Export Profit Cutout table';
	Delete From Temppheap.dbo.ProfitCutout;
	INSERT INTO Temppheap.dbo.ProfitCutout ([OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.ProfitCutout A
	 WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate 
	 OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace Profit Cutout table';
	Delete From Temppheap.dbo.TRACE_ProfitCutout;
	INSERT INTO Temppheap.dbo.TRACE_ProfitCutout ([DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.TRACE_ProfitCutout A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	PRINT 'Delete and export temporary table other deposit.';
	DELETE FROM Temppheap.dbo.OtherDeposit;
	INSERT INTO Temppheap.dbo.OtherDeposit ([LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_Dis_Amt],[CU_ID],[LD_IntRate],[EM_ID],[LD_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[IsExport],[Date_Payoff],[CM_ID1])
	SELECT [LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_Dis_Amt],[CU_ID],[LD_IntRate],[EM_ID],[LD_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[IsExport],[Date_Payoff],[CM_ID1]
	FROM pheap.dbo.OtherDeposit WHERE CONVERT(VARCHAR(12),LD_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LD_Date_Modify,101)=@StrDate;

	PRINT 'Delete and export temporary table pheap repay.';
	DELETE FROM Temppheap.dbo.OtherDepositRepay;
	INSERT INTO Temppheap.dbo.OtherDepositRepay ([LR_ID],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [LR_ID],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport]
	FROM pheap.[dbo].OtherDepositRepay WHERE CONVERT(VARCHAR(12),LR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LR_Date_Modify,101)=@StrDate;


	Print 'Delete and export temporary table trace pheap.';
	Delete From Temppheap.dbo.TRACE_OtherDeposit;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_OtherDeposit] ON;
	INSERT INTO Temppheap.[dbo].[TRACE_OtherDeposit] ([AutoID],[DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff])
	SELECT [AutoID],[DateAction],[RecordAction],[LD_ID],[LD_BrId],[CM_ID],[LD_Dis_Date],[LD_First_Date],[LD_Mat_Date],[LD_Dis_Amt],[LD_Out_Amt],[CU_ID],[LD_ExRate],[LD_IntRate],[EM_ID],[LD_Unit],[LD_Type],[LD_Term],[LD_Status],[LD_Rec_Status],[LD_User_Create],[LD_Date_Create],[LD_User_Modify],[LD_Date_Modify],[LD_User_Delete],[LD_Date_Delete],[IsExport],[IsWriteoff]
	FROM pheap.[dbo].[TRACE_OtherDeposit] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_OtherDeposit] OFF;

	Print 'Delete and export temporary table trace pheap repay.';
	Delete From Temppheap.dbo.TRACE_OtherDepositRepay;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_OtherDepositRepay] ON;
	INSERT INTO Temppheap.[dbo].[TRACE_OtherDepositRepay] ([AutoID],[LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport])
	SELECT [AutoID],[LR_ID],[DateAction],[RecordAction],[LD_ID],[CM_ID],[LR_BrID],[SH_Date],[EM_ID],[LR_Description],[SH_Total],[LR_Date],[LR_Amount],[LR_Charge],[LR_ExRate],[LR_Rec_Status],[LR_User_Create],[LR_Date_Create],[LR_User_Modify],[LR_Date_Modify],[LR_User_Delete],[LR_Date_Delete],[IsExport]
	FROM pheap.[dbo].[TRACE_OtherDepositRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_OtherDepositRepay] OFF;


	Print 'Delete and Export OtherIncome table';
	Delete From Temppheap.dbo.OtherIncome;
	INSERT INTO Temppheap.dbo.OtherIncome ([OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.OtherIncome A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace OtherIncome table';
	Delete From Temppheap.dbo.TRACE_OtherIncome;
	INSERT INTO Temppheap.dbo.TRACE_OtherIncome ([DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [DateAction],[RecordAction],[OPID],[BrID],[OPDate],[OPDescription],[LAmount],[FAmount],[FCurrency],[ExRate],[OPType],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.dbo.TRACE_OtherIncome A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;


	PRINT 'Delete and export table saving repay.';
	DELETE FROM Temppheap.dbo.BK_SavingRepay;
	SET IDENTITY_INSERT Temppheap.dbo.BK_SavingRepay ON;
	INSERT INTO Temppheap.dbo.BK_SavingRepay ([SR_ID],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport])
	SELECT [SR_ID],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport]
	FROM pheap.[dbo].BK_SavingRepay WHERE CONVERT(VARCHAR(12),SR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),SR_Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.dbo.BK_SavingRepay OFF;

	Print 'Delete and export TRACE_SavingRepay.';
	Delete From Temppheap.dbo.TRACE_SavingRepay;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_SavingRepay] ON;
	INSERT INTO Temppheap.[dbo].[TRACE_SavingRepay] ([AutoID],[SR_ID],[DateAction],[RecordAction],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport])
	SELECT [AutoID],[SR_ID],[DateAction],[RecordAction],[LD_ID],[SR_BrID],[SR_Des],[SR_Date],[SR_Amount],[SR_TotalToRepay],[SR_Rec_Status],[SR_User_Create],[SR_Date_Create],[SR_User_Modify],[SR_Date_Modify],[SR_User_Delete],[SR_Date_Delete],[IsExport]
	FROM pheap.[dbo].[TRACE_SavingRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.[dbo].[TRACE_SavingRepay] OFF;


	Print 'Delete and export Holiday';
	--Print 'Delete and Export writeoff table';
	Delete From Temppheap.dbo.BK_Holiday;
	SET IDENTITY_INSERT Temppheap.dbo.BK_Holiday ON;
	INSERT INTO Temppheap.dbo.BK_Holiday ([ID],[StartDate],[Description],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[BrID])
	SELECT [ID],[StartDate],[Description],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[BrID]
	FROM pheap.dbo.BK_Holiday A 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.dbo.BK_Holiday OFF;  

	Print 'Delete and export TRACE_Holiday';
	Delete From Temppheap.dbo.TRACE_Holiday;
	INSERT INTO Temppheap.[dbo].TRACE_Holiday ([DateAction],[RecordAction],[ID],[StartDate],[Description],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[BrID])
	SELECT [DateAction],[RecordAction],[ID],[StartDate],[Description],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete],[BrID]
	FROM pheap.[dbo].TRACE_Holiday WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	-------
	Print 'Delete and export WriteOff';
	Delete From Temppheap.dbo.Writeoff;
	INSERT INTO Temppheap.[dbo].Writeoff ([LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete])
	SELECT [LD_ID],[BR_ID],[WOF_Date],[LD_OS],[IsExport],[Rec_Status],[User_Create],[Date_Create],[User_Modify],[Date_Modify],[User_Delete],[Date_Delete]
	FROM pheap.[dbo].Writeoff WHERE CONVERT(VARCHAR(12),Date_Create,101)=@StrDate;

	---
		Print 'Delete and export OtherIncome';
	--Print 'Delete and Export writeoff table';
	Delete From Temppheap.dbo.BK_OtherIncome;
	SET IDENTITY_INSERT Temppheap.dbo.BK_OtherIncome ON;
	INSERT INTO Temppheap.dbo.BK_OtherIncome ([BrId],[Date_Operation],[Amount],[CU_ID],[Date_Create],[User_Create],[Date_Modify],[User_Modify],[Date_Delete],[User_Delete],[ID],[Descriptions])
	SELECT [BrId],[Date_Operation],[Amount],[CU_ID],[Date_Create],[User_Create],[Date_Modify],[User_Modify],[Date_Delete],[User_Delete],[ID],[Descriptions]
	FROM pheap.dbo.BK_OtherIncome A 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	SET IDENTITY_INSERT Temppheap.dbo.BK_OtherIncome OFF;  

	Print 'Delete and export TRACE_OtherIncome';
	Delete From Temppheap.dbo.Trace_OtherIncome;
	INSERT INTO Temppheap.[dbo].Trace_OtherIncome ([DateAction],[RecordAction],[BrId],[Date_Operation],[Amount],[CU_ID],[Date_Create],[User_Create],[Date_Modify],[User_Modify],[Date_Delete],[User_Delete],[ID],[Descriptions])
	SELECT [DateAction],[RecordAction],[BrId],[Date_Operation],[Amount],[CU_ID],[Date_Create],[User_Create],[Date_Modify],[User_Modify],[Date_Delete],[User_Delete],[ID],[Descriptions]
	FROM pheap.[dbo].Trace_OtherIncome WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

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
