Imports System.Collections.Generic
Imports Microsoft.VisualStudio.TestTools.UnitTesting

'Unit tests for the SQL the Backup / Restore / Import / Export screens send.
'No database is touched here.
<TestClass()>
Public Class DbOpsTests

    'The client sample: a backup of their live Panha database, taken on SQL Server
    '2012, whose files live on a drive that does not exist on our machines.
    Private Shared Function ClientBackupFiles() As List(Of DbOps.BackupFile)
        Return New List(Of DbOps.BackupFile) From {
            New DbOps.BackupFile() With {
                .LogicalName = "HPT_Data",
                .PhysicalName = "D:\IT\MSSQL11.MSSQLSERVER\MSSQL\DATA\panha.mdf",
                .FileType = "D"},
            New DbOps.BackupFile() With {
                .LogicalName = "HPT_Data_log",
                .PhysicalName = "D:\IT\MSSQL11.MSSQLSERVER\MSSQL\DATA\panha_0.ldf",
                .FileType = "L"}
        }
    End Function

    ' ---------- escaping ----------

    <TestMethod()>
    Public Sub SqlEscape_doubles_single_quotes()
        Assert.AreEqual("O''Brien", DbOps.SqlEscape("O'Brien"))
    End Sub

    <TestMethod()>
    Public Sub SqlEscape_leaves_ordinary_text_alone()
        Assert.AreEqual("C:\Export\jan.bak", DbOps.SqlEscape("C:\Export\jan.bak"))
    End Sub

    'A backup chosen from the file dialog can sit in a folder with an apostrophe.
    'Unescaped it would close the string literal and change the statement.
    <TestMethod()>
    Public Sub Backup_path_with_apostrophe_cannot_break_out_of_the_literal()
        Dim evil As String = "C:\tmp\a'; DROP DATABASE [Panha]; --\x.bak"
        Dim sql As String = DbOps.BuildBackupSql("TempLoan", evil)

        'The path must appear only in its escaped form.
        StringAssert.Contains(sql, DbOps.SqlEscape(evil))
        Assert.IsFalse(sql.Contains(evil),
                       "The raw apostrophe survived; the path closed its own literal.")

        'And the statement's string literals must still be balanced.
        Dim quotes As Integer = sql.Length - sql.Replace("'", "").Length
        Assert.AreEqual(0, quotes Mod 2,
                        "Unbalanced quotes: the path broke out of its literal.")
    End Sub

    ' ---------- identifier safety ----------

    <TestMethod()>
    Public Sub QuoteDbName_brackets_a_plain_name()
        Assert.AreEqual("[TempLoan]", DbOps.QuoteDbName("TempLoan"))
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub QuoteDbName_rejects_a_name_carrying_sql()
        DbOps.QuoteDbName("TempLoan]; DROP DATABASE [Panha")
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub QuoteDbName_rejects_empty()
        DbOps.QuoteDbName("")
    End Sub

    ' ---------- restore target guard ----------

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub Restore_refuses_to_overwrite_the_live_database()
        DbOps.GuardStagingTarget("Panha", "Panha")
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub Restore_refuses_a_target_that_is_not_staging()
        DbOps.GuardStagingTarget("loan", "Panha")
    End Sub

    <TestMethod()>
    Public Sub Restore_allows_the_staging_database()
        DbOps.GuardStagingTarget("TempData", "Panha")
    End Sub

    ' ---------- MOVE: the reason imports failed on a different machine ----------

    <TestMethod()>
    Public Sub Restore_redirects_every_file_into_the_local_data_directory()
        Dim sql As String = DbOps.BuildRestoreSql(
            "TempData", "F:\in\client.bak", ClientBackupFiles(), "F:\SqlData")

        StringAssert.Contains(sql, "MOVE N'HPT_Data' TO N'F:\SqlData\TempData.mdf'")
        StringAssert.Contains(sql, "MOVE N'HPT_Data_log' TO N'F:\SqlData\TempData_log.ldf'")
    End Sub

    'The whole point of MOVE: never write to the path baked into the client's backup.
    <TestMethod()>
    Public Sub Restore_never_targets_the_path_inside_the_backup()
        Dim sql As String = DbOps.BuildRestoreSql(
            "TempData", "F:\in\client.bak", ClientBackupFiles(), "F:\SqlData")

        Assert.IsFalse(sql.Contains("D:\IT\MSSQL11.MSSQLSERVER"),
                       "Restore still points at the client's own drive path.")
    End Sub

    <TestMethod()>
    Public Sub Restore_appends_a_missing_trailing_slash_to_the_data_directory()
        Dim withSlash As String = DbOps.BuildRestoreSql("TempData", "a.bak", ClientBackupFiles(), "F:\SqlData\")
        Dim without As String = DbOps.BuildRestoreSql("TempData", "a.bak", ClientBackupFiles(), "F:\SqlData")
        Assert.AreEqual(withSlash, without)
    End Sub

    <TestMethod()>
    Public Sub Restore_names_extra_data_and_log_files_uniquely()
        Dim many As New List(Of DbOps.BackupFile) From {
            New DbOps.BackupFile() With {.LogicalName = "d1", .FileType = "D"},
            New DbOps.BackupFile() With {.LogicalName = "d2", .FileType = "D"},
            New DbOps.BackupFile() With {.LogicalName = "l1", .FileType = "L"},
            New DbOps.BackupFile() With {.LogicalName = "l2", .FileType = "L"}
        }
        Dim sql As String = DbOps.BuildRestoreSql("TempData", "a.bak", many, "F:\d")

        StringAssert.Contains(sql, "TO N'F:\d\TempData.mdf'")
        StringAssert.Contains(sql, "TO N'F:\d\TempData_2.mdf'")
        StringAssert.Contains(sql, "TO N'F:\d\TempData_log.ldf'")
        StringAssert.Contains(sql, "TO N'F:\d\TempData_log2.ldf'")
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub Restore_rejects_a_backup_with_no_files()
        DbOps.BuildRestoreSql("TempData", "a.bak", New List(Of DbOps.BackupFile)(), "F:\d")
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub Restore_rejects_an_empty_backup_path()
        DbOps.BuildRestoreSql("TempData", "", ClientBackupFiles(), "F:\d")
    End Sub

    ' ---------- restore statement shape ----------

    <TestMethod()>
    Public Sub Restore_takes_the_database_offline_and_puts_it_back()
        Dim sql As String = DbOps.BuildRestoreSql("TempData", "a.bak", ClientBackupFiles(), "F:\d")

        StringAssert.Contains(sql, "ALTER DATABASE [TempData] SET SINGLE_USER WITH ROLLBACK IMMEDIATE")
        StringAssert.Contains(sql, "ALTER DATABASE [TempData] SET MULTI_USER")
        Assert.IsTrue(sql.IndexOf("SINGLE_USER") < sql.IndexOf("RESTORE DATABASE"),
                      "Database must go single-user before the restore.")
        Assert.IsTrue(sql.IndexOf("MULTI_USER") > sql.IndexOf("RESTORE DATABASE"),
                      "Database must return to multi-user after the restore.")
    End Sub

    <TestMethod()>
    Public Sub Restore_runs_against_master()
        Dim sql As String = DbOps.BuildRestoreSql("TempData", "a.bak", ClientBackupFiles(), "F:\d")
        Assert.IsTrue(sql.TrimStart().StartsWith("USE master"),
                      "A restore cannot run from inside the database being replaced.")
    End Sub

    ' ---------- backup ----------

    <TestMethod()>
    Public Sub Backup_targets_the_named_database_and_destination()
        Dim sql As String = DbOps.BuildBackupSql("TempLoan", "F:\out\jan.bak")

        StringAssert.Contains(sql, "BACKUP DATABASE [TempLoan]")
        StringAssert.Contains(sql, "TO DISK = N'F:\out\jan.bak'")
        StringAssert.Contains(sql, "NAME = N'TempLoan-Full Database Backup'")
    End Sub

    <TestMethod()>
    <ExpectedException(GetType(ArgumentException))>
    Public Sub Backup_rejects_an_empty_destination()
        DbOps.BuildBackupSql("TempLoan", "")
    End Sub

    ' ---------- export ----------

    'Export backs up the staging database that SP_EXPORT fills. Pointing it at a
    'live database would ship real data to another site.
    <TestMethod()>
    <ExpectedException(GetType(InvalidOperationException))>
    Public Sub Export_refuses_to_export_a_live_database()
        DbOps.BuildExportSql("Panha", "F:\out\x.bak")
    End Sub

    <TestMethod()>
    Public Sub Export_backs_up_the_staging_database()
        Dim sql As String = DbOps.BuildExportSql("TempLoan", "F:\out\x.bak")
        StringAssert.Contains(sql, "BACKUP DATABASE [TempLoan]")
    End Sub

End Class
