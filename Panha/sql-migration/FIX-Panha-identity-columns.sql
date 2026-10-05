-- Dev Panha: restore IDENTITY on six BK_ tables. The client's databases (pheap, TempPanha) have it;
-- this copy lost it, so every INSERT from the app that omits the id ("Cannot insert the value NULL into
-- column 'SH_ID'") and every head-office import fails.
-- Each table is rebuilt in its own transaction: copy with IDENTITY, rows copied keeping their ids
-- (duplicates included - head office holds rows imported from several branches), row count verified,
-- old table dropped, copy renamed, default constraints and triggers recreated, identity reseeded.
-- Run (nobody else using the database):
--   sqlcmd -S lpc:.\SQLEXPRESS -E -C -i FIX-Panha-identity-columns.sql
-- Written 2026-10-03.
USE [Panha];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO
DECLARE @work TABLE (seq int identity, tbl sysname, idcol sysname);
INSERT @work (tbl, idcol) VALUES
    ('BK_LoanSchedule', 'SH_ID'), ('BK_LoanRepay', 'LR_ID'), ('BK_SavingRepay', 'SR_ID'),
    ('BK_Exchange', 'ID'), ('BK_OtherIncome', 'ID'), ('BK_ChangeCustomer', 'ID');

DECLARE @seq int = 1, @tbl sysname, @idcol sysname, @sql nvarchar(max), @cols nvarchar(max), @defs nvarchar(max),
        @n bigint, @n2 bigint, @trg nvarchar(max), @msg nvarchar(400);
DECLARE @triggers TABLE (def nvarchar(max));
WHILE @seq <= (SELECT MAX(seq) FROM @work)
BEGIN
    SELECT @tbl = tbl, @idcol = idcol FROM @work WHERE seq = @seq;
    SET @seq += 1;

    IF OBJECT_ID('dbo.' + @tbl) IS NULL
    BEGIN PRINT @tbl + ': table not found, skipped'; CONTINUE; END
    IF COLUMNPROPERTY(OBJECT_ID('dbo.' + @tbl), @idcol, 'IsIdentity') = 1
    BEGIN PRINT @tbl + ': ' + @idcol + ' is already IDENTITY, skipped'; CONTINUE; END
    IF OBJECT_ID('dbo.' + @tbl + '_new') IS NOT NULL
    BEGIN RAISERROR('%s_new already exists - clean up from a previous run first', 16, 1, @tbl); RETURN; END

    -- column definitions, in column order, from the live catalog
    SET @defs = N''; SET @cols = N'';
    SELECT @defs += CASE WHEN @defs = N'' THEN N'' ELSE N',' + CHAR(10) END + N'    ' + QUOTENAME(c.name) + N' '
            + CASE
                WHEN t.name IN ('varchar','char','varbinary','binary') THEN t.name + N'(' + CASE WHEN c.max_length = -1 THEN N'max' ELSE CAST(c.max_length AS nvarchar(10)) END + N')'
                WHEN t.name IN ('nvarchar','nchar') THEN t.name + N'(' + CASE WHEN c.max_length = -1 THEN N'max' ELSE CAST(c.max_length / 2 AS nvarchar(10)) END + N')'
                WHEN t.name IN ('numeric','decimal') THEN t.name + N'(' + CAST(c.precision AS nvarchar(10)) + N',' + CAST(c.scale AS nvarchar(10)) + N')'
                WHEN t.name IN ('datetime2','time','datetimeoffset') THEN t.name + N'(' + CAST(c.scale AS nvarchar(10)) + N')'
                ELSE t.name END
            + CASE WHEN c.collation_name IS NOT NULL THEN N' COLLATE ' + c.collation_name ELSE N'' END
            + CASE WHEN c.name = @idcol THEN N' IDENTITY(1,1)' ELSE N'' END
            + CASE WHEN c.is_nullable = 1 THEN N' NULL' ELSE N' NOT NULL' END,
           @cols += CASE WHEN @cols = N'' THEN N'' ELSE N', ' END + QUOTENAME(c.name)
    FROM sys.columns c JOIN sys.types t ON t.user_type_id = c.user_type_id
    WHERE c.object_id = OBJECT_ID('dbo.' + @tbl)
    ORDER BY c.column_id;

    BEGIN TRANSACTION;
    SET @sql = N'SELECT @n = COUNT_BIG(*) FROM dbo.' + QUOTENAME(@tbl);
    EXEC sp_executesql @sql, N'@n bigint OUTPUT', @n OUTPUT;

    SET @sql = N'CREATE TABLE dbo.' + QUOTENAME(@tbl + '_new') + N' (' + CHAR(10) + @defs + CHAR(10) + N');';
    EXEC sp_executesql @sql;

    SET @sql = N'SET IDENTITY_INSERT dbo.' + QUOTENAME(@tbl + '_new') + N' ON; '
             + N'INSERT INTO dbo.' + QUOTENAME(@tbl + '_new') + N' (' + @cols + N') SELECT ' + @cols + N' FROM dbo.' + QUOTENAME(@tbl) + N'; '
             + N'SET IDENTITY_INSERT dbo.' + QUOTENAME(@tbl + '_new') + N' OFF;';
    EXEC sp_executesql @sql;

    SET @sql = N'SELECT @n = COUNT_BIG(*) FROM dbo.' + QUOTENAME(@tbl + '_new');
    EXEC sp_executesql @sql, N'@n bigint OUTPUT', @n2 OUTPUT;
    IF @n2 <> @n
    BEGIN ROLLBACK; RAISERROR('%s: row count mismatch after copy, rolled back', 16, 1, @tbl); RETURN; END

    -- default constraints and triggers of the old table, recreated on the new one after the rename
    SET @sql = N'';
    SELECT @sql += N'ALTER TABLE dbo.' + QUOTENAME(@tbl) + N' ADD CONSTRAINT ' + QUOTENAME(dc.name) + N' DEFAULT ' + dc.definition + N' FOR ' + QUOTENAME(c.name) + N'; '
    FROM sys.default_constraints dc JOIN sys.columns c ON c.object_id = dc.parent_object_id AND c.column_id = dc.parent_column_id
    WHERE dc.parent_object_id = OBJECT_ID('dbo.' + @tbl);

    DELETE @triggers;
    INSERT @triggers SELECT OBJECT_DEFINITION(object_id) FROM sys.triggers WHERE parent_id = OBJECT_ID('dbo.' + @tbl);

    EXEC('DROP TABLE dbo.' + @tbl);
    SET @msg = N'dbo.' + @tbl + N'_new';
    EXEC sp_rename @objname = @msg, @newname = @tbl;

    IF @sql <> N'' EXEC sp_executesql @sql;
    WHILE EXISTS (SELECT 1 FROM @triggers)
    BEGIN
        SELECT TOP 1 @trg = def FROM @triggers;
        EXEC sp_executesql @trg;
        DELETE TOP (1) FROM @triggers;
    END

    DBCC CHECKIDENT (@tbl, RESEED) WITH NO_INFOMSGS;
    COMMIT;
    PRINT @tbl + ': rebuilt, ' + CAST(@n AS varchar(20)) + ' rows kept, ' + @idcol + ' is now IDENTITY';
END
GO
SELECT OBJECT_NAME(object_id) AS tbl, name AS col, IDENT_CURRENT(OBJECT_NAME(object_id)) AS current_seed
FROM sys.identity_columns WHERE OBJECT_NAME(object_id) LIKE 'BK_%' ORDER BY 1;
GO
