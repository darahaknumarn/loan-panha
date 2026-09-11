
CREATE PROCEDURE [dbo].[SP_EXPORT](@Date DateTime,@Branch varchar(12))
AS
BEGIN
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @StrDate Varchar(12) ;
    SET @StrDate= CONVERT(VARCHAR(12),@Date,101);
	--UPDATE TempLoan.dbo.SYS_IMPORT_EXPORT SET [Value]=@StrDate WHERE ID='EXPORT_DATE';
	--UPDATE TempLoan.dbo.SYS_IMPORT_EXPORT SET [Value]=@Branch WHERE ID='BRANCH';
truncate table TempLoan.dbo.BK_Customer 
truncate table TempLoan.dbo.BK_ChangeCustomer 
truncate table TempLoan.dbo.BK_CustomerOther 
truncate table TempLoan.dbo.BK_Employee 
truncate table TempLoan.dbo.BK_Exchange 
truncate table TempLoan.dbo.BK_Loan 
truncate table TempLoan.dbo.BK_LoanRepay 
truncate table TempLoan.dbo.BK_LoanSchedule 
truncate table TempLoan.dbo.BK_Location 
truncate table TempLoan.dbo.BK_LocationOther 
truncate table TempLoan.dbo.BK_SavingRepay 
truncate table TempLoan.dbo.Bank_Transaction 
truncate table TempLoan.dbo.ExpenseOperation 
truncate table TempLoan.dbo.ExpenseSchedule 
truncate table TempLoan.dbo.OtherDeposit 
truncate table TempLoan.dbo.OtherDepositRepay 
truncate table TempLoan.dbo.OtherIncome 
truncate table TempLoan.dbo.OwnerTransaction 
truncate table TempLoan.dbo.ProfitCutout
truncate table TempLoan.dbo.TRACE_Asset 
truncate table TempLoan.dbo.TRACE_Bank 
truncate table TempLoan.dbo.TRACE_Customer 
truncate table TempLoan.dbo.TRACE_CustomerOther 
truncate table TempLoan.dbo.TRACE_Employee 
truncate table TempLoan.dbo.TRACE_Exchange
truncate table TempLoan.dbo.TRACE_ExpenseOperation 
truncate table TempLoan.dbo.TRACE_ExpenseSchedule 
truncate table TempLoan.dbo.TRACE_Holiday 
truncate table TempLoan.dbo.TRACE_Loan 
truncate table TempLoan.dbo.TRACE_LoanRepay 
truncate table TempLoan.dbo.TRACE_LoanSchedule 
truncate table TempLoan.dbo.TRACE_Location 
truncate table TempLoan.dbo.TRACE_OtherDeposit 
truncate table TempLoan.dbo.TRACE_OtherDepositRepay 
truncate table TempLoan.dbo.TRACE_OtherIncome 
truncate table TempLoan.dbo.TRACE_OwnerTransaction 
truncate table TempLoan.dbo.TRACE_ProfitCutout 
truncate table TempLoan.dbo.TRACE_SavingRepay 
truncate table TempLoan.dbo.TRACE_Writeoff 
truncate table TempLoan.dbo.Writeoff 
	
	PRINT 'Delete and insert temporary table Location.';
	DELETE FROM TempLoan.dbo.BK_Location;
	INSERT INTO TempLoan.dbo.BK_Location 
	SELECT * FROM Loan.dbo.BK_Location 
	WHERE CONVERT(VARCHAR(12),LO_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LO_Date_Modify,101)=@StrDate;
    
	--return
    PRINT 'Delete and export temporary table customer.';
	DELETE FROM TempLoan.dbo.BK_Customer;
	INSERT INTO TempLoan.dbo.BK_Customer 
	SELECT * FROM Loan.dbo.BK_Customer 
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table customer other.';
	DELETE FROM TempLoan.[dbo].[BK_CustomerOther]
	INSERT INTO TempLoan.[dbo].[BK_CustomerOther]
	SELECT * FROM Loan.[dbo].[BK_CustomerOther]
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table trace customer other.';
	DELETE FROM TempLoan.[dbo].TRACE_CustomerOther
	INSERT INTO TempLoan.[dbo].TRACE_CustomerOther
	SELECT * FROM Loan.[dbo].TRACE_CustomerOther
	WHERE CONVERT(VARCHAR(12),CM_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),CM_Date_Modify,101)=@StrDate;
    

	PRINT 'Delete and export temporary table Exchange.';
	DELETE FROM TempLoan.[dbo].[BK_Exchange]
	INSERT INTO TempLoan.[dbo].[BK_Exchange]
	SELECT * FROM Loan.[dbo].[BK_Exchange]
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table Trace Exchange.';
	DELETE FROM TempLoan.[dbo].TRACE_Exchange
	INSERT INTO TempLoan.[dbo].TRACE_Exchange
	SELECT * FROM Loan.[dbo].TRACE_Exchange
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table Bank Transaction';
	DELETE FROM TempLoan.[dbo].Bank_Transaction
	INSERT INTO TempLoan.[dbo].Bank_Transaction
	SELECT * FROM Loan.[dbo].Bank_Transaction
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
	PRINT 'Delete and export temporary table Trace Bank Transaction';
	DELETE FROM TempLoan.[dbo].TRACE_Bank
	INSERT INTO TempLoan.[dbo].TRACE_Bank
	SELECT * FROM Loan.[dbo].TRACE_Bank
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    


    PRINT 'Delete and export temporary table position.';
	DELETE FROM TempLoan.dbo.BK_Position;
	INSERT INTO TempLoan.dbo.BK_Position 
	SELECT * FROM Loan.dbo.BK_Position 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;

	PRINT 'Delete and export temporary table Change customer.';
	DELETE FROM TempLoan.[dbo].[BK_ChangeCustomer];
	INSERT INTO TempLoan.[dbo].[BK_ChangeCustomer]
	SELECT * FROM Loan.[dbo].[BK_ChangeCustomer]
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate ;

	--select * from BK_ChangeCustomer
	PRINT 'Delete and export temporary table employee.';
	DELETE FROM TempLoan.dbo.BK_Employee;
	INSERT INTO TempLoan.dbo.BK_Employee 
	SELECT * FROM Loan.dbo.BK_Employee 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
    
    PRINT 'Delete and export temporary table loan.';
	DELETE FROM TempLoan.dbo.BK_Loan;
	INSERT INTO TempLoan.dbo.BK_Loan 
	SELECT * FROM Loan.dbo.BK_Loan 
	WHERE CONVERT(VARCHAR(12),LD_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LD_Date_Modify,101)=@StrDate;
	
	PRINT 'Delete and export temporary table loan repay.';
	DELETE FROM TempLoan.dbo.BK_LoanRepay;
	INSERT INTO TempLoan.dbo.BK_LoanRepay
	SELECT 
	*
	FROM Loan.[dbo].[BK_LoanRepay] 
	WHERE CONVERT(VARCHAR(12),LR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LR_Date_Modify,101)=@StrDate;
	
	PRINT 'Delete and export temporary table loan schedule.';
	DELETE FROM TempLoan.dbo.BK_LoanSchedule;
	INSERT INTO TempLoan.dbo.BK_LoanSchedule 
	SELECT *
	FROM Loan.[dbo].[BK_LoanSchedule] 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;
	
	Print 'Delete and export temporary table trace customer.';
	Delete From TempLoan.dbo.TRACE_Customer;
	INSERT INTO TempLoan.[dbo].[TRACE_Customer]
	SELECT *
	FROM Loan.[dbo].[TRACE_Customer] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	
	Print 'Delete and export temporary table trace loan.';
	Delete From TempLoan.dbo.TRACE_Loan;
	INSERT INTO TempLoan.[dbo].[TRACE_Loan]
	SELECT *
	FROM Loan.[dbo].[TRACE_Loan] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and export temporary table trace loan repay.';
	Delete From TempLoan.dbo.TRACE_LoanRepay;
	INSERT INTO TempLoan.[dbo].[TRACE_LoanRepay]
	SELECT *
	FROM Loan.[dbo].[TRACE_LoanRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and export temporary table trace location.';
	Delete From TempLoan.dbo.TRACE_Location;
	INSERT INTO TempLoan.[dbo].[TRACE_Location]
	SELECT *
	FROM Loan.[dbo].[TRACE_Location] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	
	Print 'Delete and Export temporary table user.';
	Delete From TempLoan.dbo.sys_User;
	INSERT INTO TempLoan.[dbo].[sys_User]	
	SELECT * FROM Loan.[dbo].[sys_User] WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 
	
	Print 'Delete and Export asset table';
	Delete From TempLoan.dbo.Asset;
	INSERT INTO TempLoan.dbo.Asset 
	SELECT * FROM Loan.dbo.Asset A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 
	
	Print 'Delete and Export Expense Operation';
	Delete From TempLoan.dbo.ExpenseOperation;
	INSERT INTO TempLoan.[dbo].[ExpenseOperation]
	SELECT *
	FROM Loan.[dbo].[ExpenseOperation] WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 
	
	Print 'Delete and Export Expense Schedule';
	Delete From TempLoan.dbo.ExpenseSchedule;
	Insert Into TempLoan.dbo.ExpenseSchedule 
	Select * from Loan.dbo.ExpenseSchedule WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate;
	
	Print 'Delete and Export trace asset table';
	Delete From TempLoan.dbo.TRACE_Asset;
	INSERT INTO TempLoan.dbo.TRACE_Asset 
	SELECT * FROM Loan.dbo.TRACE_Asset A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 
	
	Print 'Delete and Export trace Expense Operation';
	Delete From TempLoan.dbo.TRACE_ExpenseOperation;
	INSERT INTO TempLoan.[dbo].[TRACE_ExpenseOperation] 
	SELECT * FROM Loan.[dbo].[TRACE_ExpenseOperation] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	Print 'Delete and Export trace expense schedule';
	Delete From TempLoan.dbo.TRACE_ExpenseSchedule;
	INSERT INTO TempLoan.[dbo].[TRACE_ExpenseSchedule] 
	SELECT * FROM Loan.[dbo].[TRACE_ExpenseSchedule] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;;
	
	Print 'Delete and Export trace loan schedule';
	Delete From TempLoan.dbo.TRACE_LoanSchedule;
	INSERT INTO TempLoan.[dbo].TRACE_LoanSchedule	
	SELECT * FROM Loan.[dbo].TRACE_LoanSchedule WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 

	Print 'Delete and Export OwnerTransaction table';
	Delete From TempLoan.dbo.OwnerTransaction;
	INSERT INTO TempLoan.dbo.OwnerTransaction 
	SELECT * FROM Loan.dbo.OwnerTransaction A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace OwnerTransaction table';
	Delete From TempLoan.dbo.TRACE_OwnerTransaction;
	INSERT INTO TempLoan.dbo.TRACE_OwnerTransaction 
	SELECT * FROM Loan.dbo.TRACE_OwnerTransaction A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and Export writeoff table';
	Delete From TempLoan.dbo.Writeoff;
	INSERT INTO TempLoan.dbo.Writeoff SELECT * FROM Loan.dbo.Writeoff A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and Export trace writeoff table';
	Delete From TempLoan.dbo.TRACE_Writeoff;
	INSERT INTO TempLoan.dbo.TRACE_Writeoff SELECT * FROM Loan.dbo.TRACE_Writeoff A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and Export Profit Cutout table';
	Delete From TempLoan.dbo.ProfitCutout;
	INSERT INTO TempLoan.dbo.ProfitCutout 
	SELECT * FROM Loan.dbo.ProfitCutout A
	 WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate 
	 OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace Profit Cutout table';
	Delete From TempLoan.dbo.TRACE_ProfitCutout;
	INSERT INTO TempLoan.dbo.TRACE_ProfitCutout SELECT * FROM Loan.dbo.TRACE_ProfitCutout A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;; 
		
	PRINT 'Delete and export temporary table other deposit.';
	DELETE FROM TempLoan.dbo.OtherDeposit;
	INSERT INTO TempLoan.dbo.OtherDeposit SELECT * FROM Loan.dbo.OtherDeposit WHERE CONVERT(VARCHAR(12),LD_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LD_Date_Modify,101)=@StrDate;
	
	PRINT 'Delete and export temporary table loan repay.';
	DELETE FROM TempLoan.dbo.OtherDepositRepay;
	INSERT INTO TempLoan.dbo.OtherDepositRepay SELECT * FROM Loan.[dbo].OtherDepositRepay WHERE CONVERT(VARCHAR(12),LR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),LR_Date_Modify,101)=@StrDate;
	
	
	Print 'Delete and export temporary table trace loan.';
	Delete From TempLoan.dbo.TRACE_OtherDeposit;
	INSERT INTO TempLoan.[dbo].[TRACE_OtherDeposit]
	SELECT *
	FROM Loan.[dbo].[TRACE_OtherDeposit] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;

	Print 'Delete and export temporary table trace loan repay.';
	Delete From TempLoan.dbo.TRACE_OtherDepositRepay;
	INSERT INTO TempLoan.[dbo].[TRACE_OtherDepositRepay]
	SELECT *
	FROM Loan.[dbo].[TRACE_OtherDepositRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	

	Print 'Delete and Export OtherIncome table';
	Delete From TempLoan.dbo.OtherIncome;
	INSERT INTO TempLoan.dbo.OtherIncome 
	SELECT * FROM Loan.dbo.OtherIncome A WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate; 

	Print 'Delete and Export trace OtherIncome table';
	Delete From TempLoan.dbo.TRACE_OtherIncome;
	INSERT INTO TempLoan.dbo.TRACE_OtherIncome 
	SELECT * FROM Loan.dbo.TRACE_OtherIncome A WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;


	PRINT 'Delete and export table saving repay.';
	DELETE FROM TempLoan.dbo.BK_SavingRepay;
	INSERT INTO TempLoan.dbo.BK_SavingRepay
	SELECT *
	FROM Loan.[dbo].BK_SavingRepay WHERE CONVERT(VARCHAR(12),SR_Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),SR_Date_Modify,101)=@StrDate;
	
	Print 'Delete and export TRACE_SavingRepay.';
	Delete From TempLoan.dbo.TRACE_SavingRepay;
	INSERT INTO TempLoan.[dbo].[TRACE_SavingRepay]
	SELECT *
	FROM Loan.[dbo].[TRACE_SavingRepay] WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;


	Print 'Delete and export Holiday';
	--Print 'Delete and Export writeoff table';
	Delete From TempLoan.dbo.BK_Holiday;
	INSERT INTO TempLoan.dbo.BK_Holiday 
	SELECT * FROM Loan.dbo.BK_Holiday A 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and export TRACE_Holiday';
	Delete From TempLoan.dbo.TRACE_Holiday;
	INSERT INTO TempLoan.[dbo].TRACE_Holiday
	SELECT *
	FROM Loan.[dbo].TRACE_Holiday WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
	-------
	Print 'Delete and export WriteOff';
	Delete From TempLoan.dbo.Writeoff;
	INSERT INTO TempLoan.[dbo].Writeoff
	SELECT *
	FROM Loan.[dbo].Writeoff WHERE CONVERT(VARCHAR(12),Date_Create,101)=@StrDate;

	---
		Print 'Delete and export OtherIncome';
	--Print 'Delete and Export writeoff table';
	Delete From TempLoan.dbo.BK_OtherIncome;
	INSERT INTO TempLoan.dbo.BK_OtherIncome 
	SELECT * FROM Loan.dbo.BK_OtherIncome A 
	WHERE CONVERT(VARCHAR(12),Date_Create,101) = @StrDate OR CONVERT(VARCHAR(12),Date_Modify,101)=@StrDate;  

	Print 'Delete and export TRACE_OtherIncome';
	Delete From TempLoan.dbo.TRACE_OtherIncome;
	INSERT INTO TempLoan.[dbo].TRACE_OtherIncome
	SELECT *
	FROM Loan.[dbo].TRACE_OtherIncome WHERE CONVERT(VARCHAR(12),DateAction,101)=@StrDate;
		
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




