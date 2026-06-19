USE [Barcode]
GO

/****** Object:  StoredProcedure [dbo].[sp_SummaryCollateral]    Script Date: 06/23/2017 10:52:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_SummaryCollateral] @BrID nvarchar(3)
AS
BEGIN
------------------------------------
select COUNT(collateralid) Num5,b.CollateralName,a.BrID
into #5 FROM	tblResource a inner join tblCollateral b on a.collateralid=b.id and a.BrID=b.BrID 
where collateralid='5' and a.BrID=@BrID and checking=0 group by b.CollateralName,a.BrID
----------------------------------------
select COUNT(collateralid) Num6,b.CollateralName,a.BrID 
into #6
FROM	tblResource a inner join tblCollateral b on a.collateralid=b.id and a.BrID=b.BrID 
where collateralid='6' and a.BrID=@BrID and checking=0 group by b.CollateralName,a.BrID
----------------------------------------
select COUNT(collateralid) Num7,b.CollateralName,a.BrID 
into #7
FROM	tblResource a inner join tblCollateral b on a.collateralid=b.id and a.BrID=b.BrID 
where collateralid='7' and a.BrID=@BrID and checking=0 group by b.CollateralName,a.BrID
----------------------------------------
select COUNT(collateralid) Num8,b.CollateralName,a.BrID 
into #8
FROM	tblResource a inner join tblCollateral b on a.collateralid=b.id and a.BrID=b.BrID 
where collateralid='8' and a.BrID=@BrID and checking=0 group by b.CollateralName,a.BrID
----------------------------------------
select COUNT(collateralid) Num9,b.CollateralName ,a.BrID 
into #9
FROM	tblResource a inner join tblCollateral b on a.collateralid=b.id and a.BrID=b.BrID 
where collateralid='9' and a.BrID=@BrID and checking=0 group by b.CollateralName,a.BrID 
----------------------------------------
select COUNT(collateralid) Num10,b.CollateralName,a.BrID 
into #10
FROM	tblResource a inner join tblCollateral b on a.collateralid=b.id and a.BrID=b.BrID 
where collateralid='10' and a.BrID=@BrID and checking=0 group by b.CollateralName,a.BrID
----------------------------------------
select COUNT(collateralid) Num11,b.CollateralName,a.BrID 
into #11
FROM	tblResource a inner join tblCollateral b on a.collateralid=b.id and a.BrID=b.BrID 
where collateralid='11' and a.BrID=@BrID and checking=0 group by b.CollateralName,a.BrID
----------------------------------------
select COUNT(collateralid) Num12,b.CollateralName,a.BrID 
into #12
FROM	tblResource a inner join tblCollateral b on a.collateralid=b.id and a.BrID=b.BrID 
where collateralid='12' and a.BrID=@BrID and checking=0 group by b.CollateralName,a.BrID
----------------------------------------
select a.BrID,b.Num5,c.Num6,d.Num7,e.Num8,f.Num9,g.Num10,h.Num11,j.Num12
into #all
from tblCollateral a 
Left join #5 b on a.BrID=b.BrID and a.CollateralName=b.CollateralName
Left join #6 c on a.BrID=c.BrID and a.CollateralName=c.CollateralName
Left join #7 d on a.BrID=d.BrID and a.CollateralName=d.CollateralName
Left join #8 e on a.BrID=e.BrID and a.CollateralName=e.CollateralName
Left join #9 f on a.BrID=f.BrID and a.CollateralName=f.CollateralName
Left join #10 g on a.BrID=g.BrID and a.CollateralName=g.CollateralName
Left join #11 h on a.BrID=h.BrID and a.CollateralName=h.CollateralName
Left join #12 j on a.BrID=j.BrID and a.CollateralName=j.CollateralName
where a.BrID=@BrID
group by a.BrID,b.Num5,c.Num6,d.Num7,e.Num8,f.Num9,g.Num10,h.Num11,j.Num12
-------------------
select BrID,isnull(SUM(Num5),0)MotoCard
,isnull(SUM(Num6),0)CarCard
,isnull(SUM(Num7),0)LiveBook
,isnull(SUM(Num8),0)Family
,isnull(SUM(Num9),0)plong_reng
,isnull(SUM(Num10),0)Plong_tun
,isnull(SUM(Num11),0)BongKanDai
,isnull(SUM(Num12),0)Files
into #inborrow
from #all group by BrID
--------------------
select BrID,SUM(ISNULL(FamilyBook,0)) Family ,SUM(ISNULL(LiveBook,0)) LiveBook 
,SUM(ISNULL(MotoCard,0)) MotoCard, SUM(ISNULL(CarCard,0)) CarCard,
 SUM(ISNULL(BongKanDai,0)) BongKanDai, SUM(ISNULL(plong_reng,0)) plong_reng, 
 SUM(ISNULL(Plong_tun,0)) Plong_tun,SUM(ISNULL(Files,0)) Files
 into #allPro
 from tblCU_Pro where BrID=@BrID group by BrID
---------------------------------------
select 
a.BrID,
------------ all
a.MotoCard,a.CarCard,a.LiveBook,a.Family,a.plong_reng,a.Plong_tun,a.BongKanDai,a.Files, 
------------ borrowed
b.MotoCard,b.CarCard,b.LiveBook,b.Family,b.plong_reng,b.Plong_tun,b.BongKanDai,b.Files,
-------------- in cabinet
a.MotoCard-b.MotoCard 'MotoCard',a.CarCard-b.CarCard 'CarCard'
,a.LiveBook-b.LiveBook 'LiveBook',a.Family-b.Family 'Family'
,a.plong_reng-b.plong_reng 'plong_reng',a.Plong_tun-b.Plong_tun 'Plong_tun'
,a.BongKanDai-b.BongKanDai'BongKanDai',a.Files-b.Files'Files'
from #allPro a left join #inborrow b on a.BrID=b.BrID

END


GO

