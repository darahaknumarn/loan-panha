-- SP_DELETE1 reads the export date and branch from <staging>.dbo.SYS_IMPORT_EXPORT, but that
-- table was never created on any client, so every head-office import failed at that step.
-- Run this once per STAGING database (Temppheap, TempPanha, ...). SP_EXPORT fills the two rows
-- on every export and the restored .bak carries them to head office.
-- usage: sqlcmd -S <server> -E -C -v STAGING=Temppheap -i CREATE-SYS_IMPORT_EXPORT.sql
USE [$(STAGING)];
GO
IF OBJECT_ID('dbo.SYS_IMPORT_EXPORT') IS NULL
BEGIN
  CREATE TABLE dbo.SYS_IMPORT_EXPORT (ID varchar(50) NOT NULL PRIMARY KEY, [Value] nvarchar(50) NULL);
  INSERT INTO dbo.SYS_IMPORT_EXPORT (ID, [Value]) VALUES ('EXPORT_DATE', NULL), ('BRANCH', NULL);
  PRINT 'SYS_IMPORT_EXPORT created in ' + DB_NAME();
END
ELSE PRINT 'SYS_IMPORT_EXPORT already exists in ' + DB_NAME();
GO
