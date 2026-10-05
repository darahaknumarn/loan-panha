

ALTER PROCEDURE [dbo].[sp_rptGetLoanPaid] (
@StartDate Varchar(12),
@EndDate Varchar(12),
@BrID Varchar(12),
@EMCode as Varchar(12),
@Curr as Nvarchar(12),
@Remark as Nvarchar(12))
AS
BEGIN
SET NOCOUNT ON;
Declare @SDate as DateTime;
Declare @EDate as DateTime;
Set @SDate = @StartDate;
Set @EDate = @EndDate;

IF @BrID = 'All' set @BrID ='%';
if @EMCode ='All' set @EMCode ='%';
IF @Curr ='All' set @Curr ='%';
IF @Curr =N'រៀល' set @Curr =1
IF @Curr =N'ដុល្លារ' set @Curr =2
----------------------------------------------------------------------------


if @Remark='0'
SELECT a.LD_ID as 'Loan ID', 
a.LR_BrID as 'Branch Code',
c.EM_ID as 'Employee Code', 
c.EM_Name as 'Employee Name' , 
b.CM_ID  as 'Customer Code', 
b.CM_KhName  as 'Customer Name', 
b.CM_Phone as 'Customer Phone', 
d.VL_ID +','+d.CN_ID +','+ d.DT_ID +','+ d.PV_ID AS 'Customer Address',
a.LR_Description as 'Description',
Convert(Varchar(12),a.LR_Date,101) as 'Paid Date',
a.LR_Amount+isnull(a.LR_Charge,0) as 'Total Amount',
--a.LR_Charge as 'Charge Amount',
--e.CU_ID as 'Currency',
case when e.CU_ID=1 then N'រៀល' else N'ដុល្លារ' end 'Currency', case when isnull(Mark,0)=2 then 'Yes' else 'No' end 'WF'
FROM  dbo.BK_LoanRepay a INNER JOIN 
             dbo.BK_Customer b ON a.CM_ID1 = b.ID AND a.LR_BrID = b.CM_BrId INNER JOIN
             dbo.BK_Employee c ON a.EM_ID = c.EM_ID AND a.LR_BrID = c.EM_BrID INNER JOIN
             dbo.BK_Location d ON b.LO_ID = d.LO_ID AND b.CM_BrId = d.LO_BrID Inner Join 
             dbo.BK_Loan e on a.LD_ID = e.LD_ID and a.LR_BrID = e.LD_BrId
Where a.LR_BrID like @BrID
and a.EM_ID like @EMCode
and a.LR_Date >=@SDate and a.LR_Date <= @EDate
and e.CU_ID like @Curr;

else if @Remark =1
SELECT a.LD_ID as 'Loan ID', 
a.LR_BrID as 'Branch Code',
c.EM_ID as 'Employee Code', 
c.EM_Name as 'Employee Name' , 
b.CM_ID  as 'Customer Code', 
b.CM_KhName  as 'Customer Name', 
b.CM_Phone as 'Customer Phone', 
d.VL_ID +','+d.CN_ID +','+ d.DT_ID +','+ d.PV_ID AS 'Customer Address',
a.LR_Description as 'Description',
Convert(Varchar(12),a.LR_Date,101) as 'Paid Date',
a.LR_Amount+isnull(a.LR_Charge,0) as 'Total Amount',
--a.LR_Charge as 'Charge Amount',
case when e.CU_ID=1 then N'រៀល' else N'ដុល្លារ' end 'Currency', case when isnull(Mark,0)=2 then 'Yes' else 'No' end 'WF'
FROM  dbo.BK_LoanRepay a INNER JOIN 
             dbo.BK_Customer b ON a.CM_ID1 = b.ID AND a.LR_BrID = b.CM_BrId INNER JOIN
             dbo.BK_Employee c ON a.EM_ID = c.EM_ID AND a.LR_BrID = c.EM_BrID INNER JOIN
             dbo.BK_Location d ON b.LO_ID = d.LO_ID AND b.CM_BrId = d.LO_BrID Inner Join 
             dbo.BK_Loan e on a.LD_ID = e.LD_ID and a.LR_BrID = e.LD_BrId
Where a.LR_BrID like @BrID
and a.EM_ID like @EMCode
and a.LR_Date >=@SDate and a.LR_Date <= @EDate
and e.CU_ID like @Curr and Mark=2;

else

SELECT a.LD_ID as 'Loan ID', 
a.LR_BrID as 'Branch Code',
c.EM_ID as 'Employee Code', 
c.EM_Name as 'Employee Name' , 
b.CM_ID  as 'Customer Code', 
b.CM_KhName  as 'Customer Name', 
b.CM_Phone as 'Customer Phone', 
d.VL_ID +','+d.CN_ID +','+ d.DT_ID +','+ d.PV_ID AS 'Customer Address',
a.LR_Description as 'Description',
Convert(Varchar(12),a.LR_Date,101) as 'Paid Date',
a.LR_Amount+isnull(a.LR_Charge,0) as 'Total Amount',
--a.LR_Charge as 'Charge Amount',
case when e.CU_ID=1 then N'រៀល' else N'ដុល្លារ' end 'Currency', case when isnull(Mark,0)=2 then 'Yes' else 'No' end 'WF'
FROM  dbo.BK_LoanRepay a INNER JOIN 
             dbo.BK_Customer b ON a.CM_ID1 = b.ID AND a.LR_BrID = b.CM_BrId INNER JOIN
             dbo.BK_Employee c ON a.EM_ID = c.EM_ID AND a.LR_BrID = c.EM_BrID INNER JOIN
             dbo.BK_Location d ON b.LO_ID = d.LO_ID AND b.CM_BrId = d.LO_BrID Inner Join 
             dbo.BK_Loan e on a.LD_ID = e.LD_ID and a.LR_BrID = e.LD_BrId
Where a.LR_BrID like @BrID
and a.EM_ID like @EMCode
and a.LR_Date >=@SDate and a.LR_Date <= @EDate
and e.CU_ID like @Curr and isnull(Mark,0)=0;



END
