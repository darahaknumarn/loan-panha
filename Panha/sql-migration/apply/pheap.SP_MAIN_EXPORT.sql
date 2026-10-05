ALTER PROCEDURE [dbo].[SP_MAIN_EXPORT](@Date as DateTime,@Branch as varchar(12))
AS
BEGIN
DECLARE @strDate as Varchar(12);
SET @strDate = Convert(Varchar(12),@Date,101);
PRINT '---------------EXPORT DATA TO TEMP DATABASE-----------------------';
EXEC dbo.SP_EXPORT @strDate,@Branch;
PRINT '---------------UPDATE COLUMN ISEXPORT-----------------------';
EXEC [dbo].[SP_UPDATE_COLUMN_ISEXPORT] @strDate,@Branch;
PRINT '------------------SHRINK TEMP DATABASE...-------------------------';
EXEC dbo.SP_SHRINK_TEMPDATA
END



