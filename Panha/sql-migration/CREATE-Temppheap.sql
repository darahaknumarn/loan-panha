-- Creates the staging database [Temppheap] that the Panha build expects for the
-- SoPheap client (ExportStagingDB() = "Temp" & DB  ->  pheap -> Temppheap).
-- The staging database is a full schema copy of the live one (TempPanha has the
-- same 70 tables and procedures as Panha), so the simplest way to make it is
-- backup + restore under the new name. Run as sysadmin. Skips if it already exists.
USE [master];
GO
IF DB_ID(N'Temppheap') IS NOT NULL
BEGIN
    PRINT 'Temppheap already exists - nothing to do.';
    RETURN;
END
DECLARE @dataDir nvarchar(260) = CONVERT(nvarchar(260), SERVERPROPERTY('InstanceDefaultDataPath'));
DECLARE @bak     nvarchar(260) = @dataDir + N'pheap_for_staging.bak';
DECLARE @sql     nvarchar(max);

SET @sql = N'BACKUP DATABASE [pheap] TO DISK = ' + QUOTENAME(@bak, '''') + N' WITH COPY_ONLY, INIT, STATS = 10;';
EXEC (@sql);

SET @sql = N'RESTORE DATABASE [Temppheap] FROM DISK = ' + QUOTENAME(@bak, '''') + N' WITH '
         + N'MOVE N''HPT_Data'' TO '     + QUOTENAME(@dataDir + N'Temppheap.mdf', '''') + N', '
         + N'MOVE N''HPT_Data_log'' TO ' + QUOTENAME(@dataDir + N'Temppheap_log.ldf', '''') + N', '
         + N'STATS = 10;';
EXEC (@sql);

-- No need to empty it: SP_EXPORT (APPLY-pheap.sql) truncates every staging table
-- before each export, and SP_MAIN_IMPORT only reads from it.
PRINT 'Temppheap created from pheap. Delete ' + @bak + ' when done.';
GO
-- SP_DELETE1 reads the export date and branch from this table; SP_EXPORT fills it.
IF OBJECT_ID('Temppheap.dbo.SYS_IMPORT_EXPORT') IS NULL
BEGIN
  CREATE TABLE Temppheap.dbo.SYS_IMPORT_EXPORT (ID varchar(50) NOT NULL PRIMARY KEY, [Value] nvarchar(50) NULL);
  INSERT INTO Temppheap.dbo.SYS_IMPORT_EXPORT (ID, [Value]) VALUES ('EXPORT_DATE', NULL), ('BRANCH', NULL);
END
GO
