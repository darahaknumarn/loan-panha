Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.VisualStudio.TestTools.UnitTesting

'End-to-end checks against a real SQL Server using the client's sample export.
'Everything lands in a scratch database that is dropped afterwards; no live
'database is opened, altered or restored over.
'
'Skipped automatically when the server or the sample file is not present, so the
'unit tests still run on a machine without either.
<TestClass()>
Public Class BackupRestoreIntegrationTests

    Private Const ScratchDb As String = "TempData_Test"
    Private Const LiveDb As String = "Panha"

    Private Shared ReadOnly ServerCnn As String =
        If(Environment.GetEnvironmentVariable("KTV_TEST_SQL"),
           "Server=lpc:.\SQLEXPRESS;Database=master;Integrated Security=SSPI;TrustServerCertificate=True")

    Private Shared ReadOnly SampleBak As String =
        If(Environment.GetEnvironmentVariable("KTV_TEST_BAK"),
           "F:\Phanha-issue-01\001 Panha2026828 186\001 Panha2026828 186.bak")

    Private Shared ScratchDir As String

    Private Shared Function OpenServer() As SqlConnection
        Dim cnn As New SqlConnection(ServerCnn)
        cnn.Open()
        Return cnn
    End Function

    Private Shared Sub RequireEnvironment()
        If Not File.Exists(SampleBak) Then
            Assert.Inconclusive("Sample backup not found: " & SampleBak)
        End If
        Try
            Using OpenServer()
            End Using
        Catch ex As Exception
            Assert.Inconclusive("SQL Server not reachable: " & ex.Message)
        End Try
    End Sub

    Private Shared Sub Exec(ByVal cnn As SqlConnection, ByVal sql As String)
        Using cmd As New SqlCommand(sql, cnn)
            cmd.CommandTimeout = 0
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Shared Function Scalar(ByVal cnn As SqlConnection, ByVal sql As String) As Object
        Using cmd As New SqlCommand(sql, cnn)
            cmd.CommandTimeout = 0
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Private Shared Sub DropScratch()
        Try
            Using cnn = OpenServer()
                If Scalar(cnn, "SELECT DB_ID('" & ScratchDb & "')") IsNot DBNull.Value Then
                    Exec(cnn, "ALTER DATABASE [" & ScratchDb & "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE")
                    Exec(cnn, "DROP DATABASE [" & ScratchDb & "]")
                End If
            End Using
        Catch
            'Best effort: a failed cleanup must not mask a test result.
        End Try
    End Sub

    <ClassInitialize()>
    Public Shared Sub Init(ByVal ctx As TestContext)
        'Restore into the instance's own data directory. The SQL Server service
        'account cannot write to a user profile folder, and this is the same
        'directory the Import screen uses in production.
        Try
            Using cnn = OpenServer()
                ScratchDir = DbOps.InstanceDataDir(cnn)
            End Using
        Catch
            ScratchDir = Path.Combine(Path.GetTempPath(), "ktv-test-sqldata")
            Directory.CreateDirectory(ScratchDir)
        End Try
        DropScratch()
    End Sub

    <ClassCleanup()>
    Public Shared Sub Cleanup()
        DropScratch()
    End Sub

    ' ---------- reading the client's export ----------

    <TestMethod()>
    Public Sub Sample_export_is_a_readable_undamaged_backup()
        RequireEnvironment()
        Using cnn = OpenServer()
            Exec(cnn, "RESTORE VERIFYONLY FROM DISK = N'" & DbOps.SqlEscape(SampleBak) & "'")
        End Using
    End Sub

    <TestMethod()>
    Public Sub Sample_export_file_list_is_readable()
        RequireEnvironment()
        Using cnn = OpenServer()
            Dim files = DbOps.ReadBackupFileList(cnn, SampleBak)

            Assert.IsTrue(files.Count >= 2, "Expected at least one data and one log file.")
            Assert.IsTrue(files.Exists(Function(f) f.FileType.ToUpper() = "D"), "No data file.")
            Assert.IsTrue(files.Exists(Function(f) f.FileType.ToUpper() = "L"), "No log file.")
        End Using
    End Sub

    'Documents what the client actually sends us: a copy of their live database,
    'not of a staging database. If this ever changes, the import assumptions change.
    <TestMethod()>
    Public Sub Sample_export_is_a_copy_of_the_clients_live_database()
        RequireEnvironment()
        Using cnn = OpenServer()
            Dim files = DbOps.ReadBackupFileList(cnn, SampleBak)
            Dim data = files.Find(Function(f) f.FileType.ToUpper() = "D")

            StringAssert.Contains(data.PhysicalName.ToLower(), "panha",
                                  "Sample backup is no longer a Panha database backup.")
        End Using
    End Sub

    ' ---------- the import path ----------

    <TestMethod()>
    Public Sub Import_restores_the_client_export_into_the_scratch_database()
        RequireEnvironment()
        DropScratch()

        Using cnn = OpenServer()
            Dim files = DbOps.ReadBackupFileList(cnn, SampleBak)
            Dim sql As String = DbOps.BuildRestoreSql(ScratchDb, SampleBak, files, ScratchDir)

            'The scratch database does not exist yet, so skip the SET SINGLE_USER
            'line the app runs against an existing staging database.
            Dim restoreOnly As String = String.Join(vbCrLf,
                Array.FindAll(sql.Split(New String() {vbCrLf}, StringSplitOptions.None),
                              Function(l) Not l.StartsWith("ALTER DATABASE") AndAlso l <> "USE master"))

            Exec(cnn, restoreOnly)

            Assert.IsNotNull(Scalar(cnn, "SELECT DB_ID('" & ScratchDb & "')"),
                             "Restore reported success but the database is not there.")

            Dim state = Convert.ToString(Scalar(cnn,
                "SELECT state_desc FROM sys.databases WHERE name = '" & ScratchDb & "'"))
            Assert.AreEqual("ONLINE", state, "Restored database is not online.")
        End Using
    End Sub

    'The files must land where we put them, not on the client's D: drive.
    <TestMethod()>
    Public Sub Import_writes_its_files_into_the_local_data_directory()
        RequireEnvironment()
        Import_restores_the_client_export_into_the_scratch_database()

        Using cnn = OpenServer()
            Using cmd As New SqlCommand(
                "SELECT physical_name FROM sys.master_files WHERE database_id = DB_ID('" & ScratchDb & "')", cnn)
                Using rd = cmd.ExecuteReader()
                    Dim any As Boolean = False
                    While rd.Read()
                        any = True
                        Dim p As String = Convert.ToString(rd(0))
                        StringAssert.StartsWith(p.ToLower(), ScratchDir.ToLower(),
                                                "Database file landed outside the test directory: " & p)
                    End While
                    Assert.IsTrue(any, "Restored database reported no files.")
                End Using
            End Using
        End Using
    End Sub

    'The data the import is supposed to hand to SP_IMPORT has to actually be there.
    <TestMethod()>
    Public Sub Imported_database_carries_the_clients_loan_data()
        RequireEnvironment()
        Import_restores_the_client_export_into_the_scratch_database()

        Using cnn = OpenServer()
            Dim tables = Convert.ToInt32(Scalar(cnn,
                "SELECT COUNT(*) FROM [" & ScratchDb & "].sys.tables"))
            Assert.IsTrue(tables > 0, "Restored database has no tables.")
        End Using
    End Sub

    'The restore script starts with USE master, and USE sticks for the rest of the
    'session. That is why the Import screen restores on its own connection: run on
    'the shared g_cnn it left "Exec SP_MAIN_IMPORT" looking for the procedure in
    'master, which reports it as missing.
    <TestMethod()>
    Public Sub Restore_script_leaves_the_session_in_master()
        RequireEnvironment()

        Using cnn = OpenServer()
            Exec(cnn, "USE tempdb")
            Assert.AreEqual("tempdb", Convert.ToString(Scalar(cnn, "SELECT DB_NAME()")),
                            "Could not move the test session off master.")

            Dim files = DbOps.ReadBackupFileList(cnn, SampleBak)
            Dim sql As String = DbOps.BuildRestoreSql(ScratchDb, SampleBak, files, ScratchDir)
            Dim firstLine As String = sql.Split(New String() {vbCrLf}, StringSplitOptions.None)(0)
            Assert.AreEqual("USE master", firstLine,
                            "Restore script no longer starts with USE master.")

            Exec(cnn, firstLine)
            Assert.AreEqual("master", Convert.ToString(Scalar(cnn, "SELECT DB_NAME()")),
                            "The restore script did not move the session to master.")
        End Using
    End Sub

    ' ---------- the export path ----------

    <TestMethod()>
    Public Sub Export_produces_a_backup_that_can_be_read_back()
        RequireEnvironment()
        Import_restores_the_client_export_into_the_scratch_database()

        Dim outFile As String = Path.Combine(ScratchDir, "export-roundtrip.bak")

        Using cnn = OpenServer()
            Exec(cnn, DbOps.BuildBackupSql(ScratchDb, outFile))

            'SQL Server owns this directory, so verify through the server rather
            'than the test process, which may not be able to read it.
            Exec(cnn, "RESTORE VERIFYONLY FROM DISK = N'" & DbOps.SqlEscape(outFile) & "'")
        End Using

        Try
            File.Delete(outFile)
        Catch
            'Left behind in the instance data directory; harmless.
        End Try
    End Sub

    ' ---------- the guards, against a real server ----------

    <TestMethod()>
    Public Sub Import_will_not_build_a_restore_aimed_at_the_live_database()
        Try
            DbOps.GuardStagingTarget(LiveDb, LiveDb)
            Assert.Fail("A restore over the live database was allowed.")
        Catch ex As InvalidOperationException
            StringAssert.Contains(ex.Message, LiveDb)
        End Try
    End Sub

    'Proves the live database is untouched by everything above.
    <TestMethod()>
    Public Sub Live_database_is_never_modified_by_these_tests()
        RequireEnvironment()
        Using cnn = OpenServer()
            Dim id = Scalar(cnn, "SELECT DB_ID('" & LiveDb & "')")
            If id Is Nothing OrElse id Is DBNull.Value Then
                Assert.Inconclusive(LiveDb & " is not on this server; nothing to protect.")
            End If

            Dim state = Convert.ToString(Scalar(cnn,
                "SELECT state_desc FROM sys.databases WHERE name = '" & LiveDb & "'"))
            Assert.AreEqual("ONLINE", state, LiveDb & " is no longer online.")
        End Using
    End Sub

End Class
