

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
From BK_Loan a left join BK_Employee b on a.EM_ID = b.EM_ID and a.LD_BrId = b.EM_BrID
			   left join BK_Customer c on a.CM_ID = c.CM_ID and a.LD_BrId = c.CM_BrId
			   left join BK_Location d on d.LO_ID = c.LO_ID and d.LO_BrID = c.CM_BrId
Where a.LD_ID= @LDID
	  and a.LD_BrId= @BrID
	  and a.LD_Status='Active'


Select a.LD_ID,
a.LR_BrID,
Sum(a.Prn) as PrnPaid
Into #LD2
From dbo.BK_LoanRepay a
--Inner Join dbo.BK_LoanSchedule b On a.LD_ID=b.LD_ID and a.LR_BrID=b.SH_BrId and a.SH_Date=b.SH_Date
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
