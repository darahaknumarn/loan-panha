USE [Morokot]
GO

/****** Object:  StoredProcedure [dbo].[spListLoan]    Script Date: 1/27/2020 3:59:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







ALTER PROCEDURE [dbo].[spListLoan] (@DisDate Datetime,@LDBrID Varchar(12))
AS
BEGIN
	SET NOCOUNT ON;
SELECT     'Saved' Saved,a.LD_ID, 
--dbo.BK_Loan.LD_BrId, 
dbo.BK_Employee.EM_ID, 
dbo.BK_Employee.EM_Name, 
b.CM_ID, 
b.CM_KhName, 
b.CM_Phone, 
dbo.BK_Location.VL_ID +','+dbo.BK_Location.CN_ID +','+ dbo.BK_Location.DT_ID +','+ dbo.BK_Location.PV_ID AS 'Address', 
a.LD_Dis_Amt, 
case when a.CU_ID=1 then N'រៀល' else N'ដុល្លារ' end CU_ID , 
a.LD_Unit,
a.LD_Term,
a.LD_IntRate, 
a.LD_Type, 
a.LD_ChargeRate, 
a.LD_ChargeAmt, 
a.LD_InRate,a.LD_InAmt,case when LD_Service=0 then N'មិនមាន' else N'មាន' end LD_Service,
convert( varchar(12),a.LD_Dis_Date,101) LD_Dis_Date, 
convert( varchar(12),a.LD_First_Date,101) LD_First_Date, 
a.PayOff,a.Ref,
convert( varchar(12),a.LD_Mat_Date,101) LD_Mat_Date
FROM         dbo.BK_Loan a INNER JOIN
             dbo.BK_Customer b ON a.CM_ID =b.CM_ID AND a.LD_BrId = b.CM_BrId and a.CM_ID1=b.ID INNER JOIN
             dbo.BK_Employee ON a.EM_ID = dbo.BK_Employee.EM_ID AND a.LD_BrId = dbo.BK_Employee.EM_BrID INNER JOIN
             dbo.BK_Location ON b.LO_ID = dbo.BK_Location.LO_ID AND b.CM_BrId = dbo.BK_Location.LO_BrID 
             --INNER JOIN
             --dbo.BK_Setting on dbo.BK_Loan.LD_Saving=dbo.BK_Setting.Value and dbo.BK_Setting.Type='LDSaving'
Where a.LD_Dis_Date=@DisDate and a.LD_BrId=@LDBrID
Order By Convert(int,a.LD_ID)
END





GO

