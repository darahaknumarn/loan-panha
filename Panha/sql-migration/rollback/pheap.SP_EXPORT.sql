
CREATE PROCEDURE [dbo].[SP_EXPORT](@Date DateTime,@Branch varchar(12))
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
