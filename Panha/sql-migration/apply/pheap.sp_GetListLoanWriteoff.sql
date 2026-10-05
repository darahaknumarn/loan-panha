


ALTER PROCEDURE [dbo].[sp_GetListLoanWriteoff](
@StartDate Varchar(12),
@EndDate Varchar(12),
@BrID Varchar(12)
)
AS
BEGIN
	SET NOCOUNT ON;
	Declare @SDate as Date;
	Declare @EDate as Date;
	Set @SDate = @StartDate;
	Set @EDate = @EndDate;


Select a.LD_ID
,e.WOF_Date
		--,a.LD_BrId
		--,a.EM_ID
		--,b.EM_Name
		,a.CM_ID
		,c.CM_KhName
		--,c.CM_Phone
		,d.VL_ID + ',' + d.CN_ID +','+ d.DT_ID +','+d.PV_ID as 'CM_Address'
		,a.LD_Dis_Date
		--,a.LD_Mat_Date

		,a.LD_Dis_Amt
				,e.LD_OS
		--,a.Note
From BK_Loan a inner join BK_Employee b on a.EM_ID = b.EM_ID and a.LD_BrId = b.EM_BrID
			   left join BK_Customer c on a.CM_ID = c.CM_ID and a.LD_BrId = c.CM_BrId and a.CM_ID1=c.ID
			   left join BK_Location d on d.LO_ID = c.LO_ID and d.LO_BrID = c.CM_BrId
			   inner join Writeoff e on a.LD_ID= e.LD_ID and a.LD_BrId = e.BR_ID
Where a.LD_BrId= @BrID
	  and e.WOF_Date between @SDate and @EDate

END


--select * from Writeoff
