
CREATE PROCEDURE [dbo].[SP_EXPORT](@Date DateTime,@Branch varchar(12))
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @StrDate Varchar(12) ;
    SET @StrDate= CONVERT(VARCHAR(12),@Date,101);
	--UPDATE TempPanha.dbo.SYS_IMPORT_EXPORT SET [Value]=@StrDate WHERE ID='EXPORT_DATE';
	--UPDATE TempPanha.dbo.SYS_IMPORT_EXPORT SET [Value]=@Branch WHERE ID='BRANCH';
truncate table TempPanha.dbo.BK_Customer 
truncate table TempPanha.dbo.BK_ChangeCustomer 
truncate table TempPanha.dbo.BK_CustomerOther 
truncate table TempPanha.dbo.BK_Employee 
truncate table TempPanha.dbo.BK_Exchange 
truncate table TempPanha.dbo.BK_Loan 
truncate table TempPanha.dbo.BK_LoanRepay 
truncate table TempPanha.dbo.BK_LoanSchedule 
truncate table TempPanha.dbo.BK_Location 
truncate table TempPanha.dbo.BK_LocationOther 
truncate table TempPanha.dbo.BK_SavingRepay 
truncate table TempPanha.dbo.Bank_Transaction 
truncate table TempPanha.dbo.ExpenseOperation 
truncate table TempPanha.dbo.ExpenseSchedule 
truncate table TempPanha.dbo.OtherDeposit 
truncate table TempPanha.dbo.OtherDepositRepay 
truncate table TempPanha.dbo.OtherIncome 
truncate table TempPanha.dbo.OwnerTransaction 
truncate table TempPanha.dbo.ProfitCutout
truncate table TempPanha.dbo.TRACE_Asset 
truncate table TempPanha.dbo.TRACE_Bank 
truncate table TempPanha.dbo.TRACE_Customer 
truncate table TempPanha.dbo.TRACE_CustomerOther 
truncate table TempPanha.dbo.TRACE_Employee 
truncate table TempPanha.dbo.TRACE_Exchange
truncate table TempPanha.dbo.TRACE_ExpenseOperation 
truncate table TempPanha.dbo.TRACE_ExpenseSchedule 
truncate table TempPanha.dbo.TRACE_Holiday 
truncate table TempPanha.dbo.TRACE_Loan 
truncate table TempPanha.dbo.TRACE_LoanRepay 
truncate table TempPanha.dbo.TRACE_LoanSchedule 
truncate table TempPanha.dbo.TRACE_Location 
truncate table TempPanha.dbo.TRACE_OtherDeposit 
truncate table TempPanha.dbo.TRACE_OtherDepositRepay 
truncate table TempPanha.dbo.TRACE_OtherIncome 
truncate table TempPanha.dbo.TRACE_OwnerTransaction 
truncate table TempPanha.dbo.TRACE_ProfitCutout 
truncate table TempPanha.dbo.TRACE_SavingRepay 
truncate table TempPanha.dbo.TRACE_Writeoff 
truncate table TempPanha.dbo.Writeoff 
	
	PRINT 'Delete and insert temporary table Location.';
	DELETE FROM TempPanha.dbo.BK_Location;
	INSERT INTO TempPanha.dbo.BK_Location 
	SELECT * FROM Panha.dbo.BK_Location 
	WHERE CONVERT(VARCHAR(12),LO_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LO_Date_Modify,101)=@StrDate;
    
	--return
    PRINT 'Delete and export temporary table customer.';
	DELETE FROM TempPanha.dbo.BK_Customer;
	INSERT INTO TempPanha.dbo.BK_Customer 
	SELECT * FROM Panha.dbo.BK_Customer 
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table customer other.';
	DELETE FROM TempPanha.[dbo].[BK_CustomerOther]
	INSERT INTO TempPanha.[dbo].[BK_CustomerOther]
	SELECT * FROM Panha.[dbo].[BK_CustomerOther]
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table trace customer other.';
	DELETE FROM TempPanha.[dbo].TRACE_CustomerOther
	INSERT INTO TempPanha.[dbo].TRACE_CustomerOther
	SELECT * FROM Panha.[dbo].TRACE_CustomerOther
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
    

	PRINT 'Delete and export temporary table Exchange.';
	DELETE FROM TempPanha.[dbo].[BK_Exchange]
	INSERT INTO TempPanha.[dbo].[BK_Exchange]
	SELECT * FROM Panha.[dbo].[BK_Exchange]
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table Trace Exchange.';
	DELETE FROM TempPanha.[dbo].TRACE_Exchange
	INSERT INTO TempPanha.[dbo].TRACE_Exchange
	SELECT * FROM Panha.[dbo].TRACE_Exchange
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table Bank Transaction';
	DELETE FROM TempPanha.[dbo].Bank_Transaction
	INSERT INTO TempPanha.[dbo].Bank_Transaction
	SELECT * FROM Panha.[dbo].Bank_Transaction
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table Trace Bank Transaction';
	DELETE FROM TempPanha.[dbo].TRACE_Bank
	INSERT INTO TempPanha.[dbo].TRACE_Bank
	SELECT * FROM Panha.[dbo].TRACE_Bank
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    


    PRINT 'Delete and export temporary table position.';
	DELETE FROM TempPanha.dbo.BK_Position;
	INSERT INTO TempPanha.dbo.BK_Position 
	SELECT * FROM Panha.dbo.BK_Position 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;

	PRINT 'Delete and export temporary table Change customer.';
	DELETE FROM TempPanha.[dbo].[BK_ChangeCustomer];
	INSERT INTO TempPanha.[dbo].[BK_ChangeCustomer]
	SELECT * FROM Panha.[dbo].[BK_ChangeCustomer]
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate ;

	--select * from BK_ChangeCustomer
	PRINT 'Delete and export temporary table employee.';
	DELETE FROM TempPanha.dbo.BK_Employee;
	INSERT INTO TempPanha.dbo.BK_Employee 
	SELECT * FROM Panha.dbo.BK_Employee 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
    PRINT 'Delete and export temporary table Panha.';
	DELETE FROM TempPanha.dbo.BK_Loan;
	INSERT INTO TempPanha.dbo.BK_Loan 
	SELECT * FROM Panha.dbo.BK_Loan 
	WHERE CONVERT(VARCHAR(12),LD_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LD_Date_Modify,101)=@StrDate;
	
	PRINT 'Delete and export temporary table Panha repay.';
	DELETE FROM TempPanha.dbo.BK_LoanRepay;
	INSERT INTO TempPanha.dbo.BK_LoanRepay
	SELECT 
	*
	FROM Panha.[dbo].BK_LoanRepay 
	WHERE CONVERT(VARCHAR(12),LR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LR_Date_Modify,101)=@StrDate;
	
	PRINT 'Delete and export temporary table Panha schedule.';
	DELETE FROM TempPanha.dbo.BK_LoanSchedule;
	INSERT INTO TempPanha.dbo.BK_LoanSchedule
	SELECT *
	FROM Panha.[dbo].[BK_LoanSchedule] 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	
	Print 'Delete and export temporary table trace customer.';
	Delete From TempPanha.dbo.TRACE_Customer;
	INSERT INTO TempPanha.[dbo].[TRACE_Customer]
	SELECT *
	FROM Panha.[dbo].[TRACE_Customer] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	
	Print 'Delete and export temporary table trace Panha.';
	Delete From TempPanha.dbo.TRACE_Loan;
	INSERT INTO TempPanha.[dbo].[TRACE_Loan]
	SELECT *
	FROM Panha.[dbo].[TRACE_Loan] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and export temporary table trace Panha repay.';
	Delete From TempPanha.dbo.TRACE_LoanRepay;
	INSERT INTO TempPanha.[dbo].[TRACE_LoanRepay]
	SELECT *
	FROM Panha.[dbo].[TRACE_LoanRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and export temporary table trace location.';
	Delete From TempPanha.dbo.TRACE_Location;
	INSERT INTO TempPanha.[dbo].[TRACE_Location]
	SELECT *
	FROM Panha.[dbo].[TRACE_Location] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	
	Print 'Delete and Export temporary table user.';
	Delete From TempPanha.dbo.sys_User;
	INSERT INTO TempPanha.[dbo].[sys_User]	
	SELECT * FROM Panha.[dbo].[sys_User] WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 
	
	Print 'Delete and Export asset table';
	Delete From TempPanha.dbo.Asset;
	INSERT INTO TempPanha.dbo.Asset 
	SELECT * FROM Panha.dbo.Asset A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 
	
	Print 'Delete and Export Expense Operation';
	Delete From TempPanha.dbo.ExpenseOperation;
	INSERT INTO TempPanha.[dbo].[ExpenseOperation]
	SELECT *
	FROM Panha.[dbo].[ExpenseOperation] WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 
	
	Print 'Delete and Export Expense Schedule';
	Delete From TempPanha.dbo.ExpenseSchedule;
	Insert Into TempPanha.dbo.ExpenseSchedule 
	Select * from Panha.dbo.ExpenseSchedule WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate;
	
	Print 'Delete and Export trace asset table';
	Delete From TempPanha.dbo.TRACE_Asset;
	INSERT INTO TempPanha.dbo.TRACE_Asset 
	SELECT * FROM Panha.dbo.TRACE_Asset A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 
	
	Print 'Delete and Export trace Expense Operation';
	Delete From TempPanha.dbo.TRACE_ExpenseOperation;
	INSERT INTO TempPanha.[dbo].[TRACE_ExpenseOperation] 
	SELECT * FROM Panha.[dbo].[TRACE_ExpenseOperation] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	Print 'Delete and Export trace expense schedule';
	Delete From TempPanha.dbo.TRACE_ExpenseSchedule;
	INSERT INTO TempPanha.[dbo].[TRACE_ExpenseSchedule] 
	SELECT * FROM Panha.[dbo].[TRACE_ExpenseSchedule] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;;
	
	Print 'Delete and Export trace Panha schedule';
	Delete From TempPanha.dbo.TRACE_LoanSchedule;
	INSERT INTO TempPanha.[dbo].TRACE_LoanSchedule	
	SELECT * FROM Panha.[dbo].TRACE_LoanSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	Print 'Delete and Export OwnerTransaction table';
	Delete From TempPanha.dbo.OwnerTransaction;
	INSERT INTO TempPanha.dbo.OwnerTransaction 
	SELECT * FROM Panha.dbo.OwnerTransaction A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace OwnerTransaction table';
	Delete From TempPanha.dbo.TRACE_OwnerTransaction;
	INSERT INTO TempPanha.dbo.TRACE_OwnerTransaction 
	SELECT * FROM Panha.dbo.TRACE_OwnerTransaction A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and Export writeoff table';
	Delete From TempPanha.dbo.Writeoff;
	INSERT INTO TempPanha.dbo.Writeoff SELECT * FROM Panha.dbo.Writeoff A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and Export trace writeoff table';
	Delete From TempPanha.dbo.TRACE_Writeoff;
	INSERT INTO TempPanha.dbo.TRACE_Writeoff SELECT * FROM Panha.dbo.TRACE_Writeoff A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and Export Profit Cutout table';
	Delete From TempPanha.dbo.ProfitCutout;
	INSERT INTO TempPanha.dbo.ProfitCutout 
	SELECT * FROM Panha.dbo.ProfitCutout A
	 WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate 
	 OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace Profit Cutout table';
	Delete From TempPanha.dbo.TRACE_ProfitCutout;
	INSERT INTO TempPanha.dbo.TRACE_ProfitCutout SELECT * FROM Panha.dbo.TRACE_ProfitCutout A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 
		
	PRINT 'Delete and export temporary table other deposit.';
	DELETE FROM TempPanha.dbo.OtherDeposit;
	INSERT INTO TempPanha.dbo.OtherDeposit SELECT * FROM Panha.dbo.OtherDeposit WHERE CONVERT(VARCHAR(12),LD_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LD_Date_Modify,101)=@StrDate;
	
	PRINT 'Delete and export temporary table Panha repay.';
	DELETE FROM TempPanha.dbo.OtherDepositRepay;
	INSERT INTO TempPanha.dbo.OtherDepositRepay SELECT * FROM Panha.[dbo].OtherDepositRepay WHERE CONVERT(VARCHAR(12),LR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LR_Date_Modify,101)=@StrDate;
	
	
	Print 'Delete and export temporary table trace Panha.';
	Delete From TempPanha.dbo.TRACE_OtherDeposit;
	INSERT INTO TempPanha.[dbo].[TRACE_OtherDeposit]
	SELECT *
	FROM Panha.[dbo].[TRACE_OtherDeposit] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and export temporary table trace Panha repay.';
	Delete From TempPanha.dbo.TRACE_OtherDepositRepay;
	INSERT INTO TempPanha.[dbo].[TRACE_OtherDepositRepay]
	SELECT *
	FROM Panha.[dbo].[TRACE_OtherDepositRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	

	Print 'Delete and Export OtherIncome table';
	Delete From TempPanha.dbo.OtherIncome;
	INSERT INTO TempPanha.dbo.OtherIncome 
	SELECT * FROM Panha.dbo.OtherIncome A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace OtherIncome table';
	Delete From TempPanha.dbo.TRACE_OtherIncome;
	INSERT INTO TempPanha.dbo.TRACE_OtherIncome 
	SELECT * FROM Panha.dbo.TRACE_OtherIncome A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;


	PRINT 'Delete and export table saving repay.';
	DELETE FROM TempPanha.dbo.BK_SavingRepay;
	INSERT INTO TempPanha.dbo.BK_SavingRepay
	SELECT *
	FROM Panha.[dbo].BK_SavingRepay WHERE CONVERT(VARCHAR(12),SR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),SR_Date_Modify,101)=@StrDate;
	
	Print 'Delete and export TRACE_SavingRepay.';
	Delete From TempPanha.dbo.TRACE_SavingRepay;
	INSERT INTO TempPanha.[dbo].[TRACE_SavingRepay]
	SELECT *
	FROM Panha.[dbo].[TRACE_SavingRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;


	Print 'Delete and export Holiday';
	--Print 'Delete and Export writeoff table';
	Delete From TempPanha.dbo.BK_Holiday;
	INSERT INTO TempPanha.dbo.BK_Holiday 
	SELECT * FROM Panha.dbo.BK_Holiday A 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and export TRACE_Holiday';
	Delete From TempPanha.dbo.TRACE_Holiday;
	INSERT INTO TempPanha.[dbo].TRACE_Holiday
	SELECT *
	FROM Panha.[dbo].TRACE_Holiday WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	-------
	Print 'Delete and export WriteOff';
	Delete From TempPanha.dbo.Writeoff;
	INSERT INTO TempPanha.[dbo].Writeoff
	SELECT *
	FROM Panha.[dbo].Writeoff WHERE CONVERT(VARCHAR(12),Date_Create,101)=@StrDate;

	---
		Print 'Delete and export OtherIncome';
	--Print 'Delete and Export writeoff table';
	Delete From TempPanha.dbo.BK_OtherIncome;
	INSERT INTO TempPanha.dbo.BK_OtherIncome 
	SELECT * FROM Panha.dbo.BK_OtherIncome A 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and export TRACE_OtherIncome';
	Delete From TempPanha.dbo.Trace_OtherIncome;
	INSERT INTO TempPanha.[dbo].Trace_OtherIncome
	SELECT *
	FROM Panha.[dbo].Trace_OtherIncome WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
		
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




