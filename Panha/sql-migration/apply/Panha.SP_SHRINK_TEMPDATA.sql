ALTER PROCEDURE [dbo].[SP_SHRINK_TEMPDATA]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DBCC SHRINKDATABASE(N'TempPanha');
END


