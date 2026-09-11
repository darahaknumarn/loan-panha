


CREATE PROCEDURE [dbo].[SP_DELETE1]
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




