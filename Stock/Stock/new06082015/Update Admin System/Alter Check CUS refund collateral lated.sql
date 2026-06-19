USE [Barcode]
GO

/****** Object:  StoredProcedure [dbo].[sp_CheckArrear]    Script Date: 12/11/2017 10:49:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[sp_CheckArrear]
 @BrID Varchar(120)
,@EmpCode varchar(5)
,@Collateral varchar(5)
,@CustomerID varchar(5)
,@Amt varchar(5)
,@Checking varchar(1)
,@StartDate Varchar(20)
,@EndDate Varchar(20)
--,@amtDay int
AS
BEGIN
SET NOCOUNT ON;
Declare @SDate as DateTime;
Declare @EDate as DateTime;
IF @BrID = 'All' Set @BrID ='%';
IF @EmpCode='All' set @EmpCode='%';
IF @Collateral='All' set @Collateral='%';
IF @CustomerID='All' set @CustomerID='%';
set @SDate=@StartDate;
set @EDate=@EndDate;

SELECT  a.id, a.staffid, e.StaffName, a.collateralid, c.CollateralName, a.customid, b.CM_Name, d.VL_ID + ',' + d.CN_ID AS CustomerAdd, a.Borrowdate, DATEADD(day, 2, a.borrowdate) AS ReturnDate, 
                      --CASE WHEN checking = 1 THEN returndate ELSE NULL END AS RealReturn
                      CASE WHEN checking = 1 THEN '-' WHEN checking = 0 AND DATEDIFF(day, a.borrowdate, a.returndate) 
                      > 2 THEN CAST(DATEDIFF(day, a.borrowdate, a.returndate) - 2 AS Varchar) WHEN checking = 0 AND (DATEDIFF(day, a.borrowdate, a.returndate) - 2) < 2 THEN CAST('0' AS Varchar) END AS MoreDate, 
                      CASE WHEN checking = 0 THEN 'No' ELSE 'OK' END AS remark
into #a
FROM         tblResource   a 
			 INNER JOIN
             BK_Customer   b ON a.customid = b.CM_ID AND a.BrID = b.CM_BrId 
             inner JOIN
			 BK_Location   d ON b.LO_ID = d.LO_ID AND b.CM_BrId = d.LO_BrId 
			 LEFT JOIN
             tblCollateral c ON a.collateralid = c.id  and a.BrID= c.BrID
             LEFT JOIN
             tblStaff      e ON a.staffid = e.StaffID AND a.BrID = e.BrID 
      where a.BrID like @BrID and a.staffid like @EmpCode and a.collateralid like @Collateral 
			and a.customid like @CustomerID and a.checking like @Checking 
			and a.borrowdate>=@StartDate and a.borrowdate<=@EndDate
      
IF @Amt='All'
	select * from #a order by id desc
--else 
--begin
----	if @Amt ='All' and @amtDay>0
----		select * from #a a 
----		where a. moreDate >=@amtDay
----    else
----		select top 50 * from #a order by id desc 
end
GO

