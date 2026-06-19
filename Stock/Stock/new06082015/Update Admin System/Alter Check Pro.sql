USE [Barcode]
GO

/****** Object:  StoredProcedure [dbo].[sp_CheckPro]    Script Date: 06/23/2017 10:52:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_CheckPro]
@CoID nvarchar(3),@CuID nvarchar(6),@BrID nvarchar (3)
AS
BEGIN
select a.id,a.CollateralName,a.BrID,b.CU_ID,b.FamilyBook,b.LiveBook,b.MotoCard,b.CarCard,b.BongKanDai,b.plong_reng,b.Plong_tun,b.Files
into #al2
from tblCollateral a
inner join tblCU_Pro b on a.BrID=b.BrID
where id=@CoID and b.CU_ID=@CuID and a.BrID=@BrID and b.BrID=@BrID
---------------------------------------
select case id 
when '6' then CarCard
when '7' then LiveBook
when '5' then MotoCard
when '8' then FamilyBook
when '9' then plong_reng
when '10' then Plong_tun
when '11' then BongKanDai
when '12' then Files

end
from #al2
END

GO

