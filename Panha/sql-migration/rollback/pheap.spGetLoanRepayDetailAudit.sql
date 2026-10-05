
CREATE PROCEDURE [dbo].[spGetLoanRepayDetailAudit] (@LDID Varchar(12),@LDBrID Varchar(12))
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
