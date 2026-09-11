Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Text
Imports System.Text.RegularExpressions

'Builds the SQL for the Backup / Restore / Import / Export screens.
'Everything here is free of UI and global state so it can be unit tested;
'the forms supply the values and run the statements.
Module DbOps

    'One data or log file inside a backup, as reported by RESTORE FILELISTONLY.
    Public Class BackupFile
        Public Property LogicalName As String
        Public Property PhysicalName As String
        Public Property FileType As String     '"D" = data, "L" = log
    End Class

    'Doubles single quotes so a value is safe inside a SQL string literal.
    'Backup paths come from a file dialog and can legitimately contain an
    'apostrophe, which would otherwise close the literal early.
    Public Function SqlEscape(ByVal value As String) As String
        If value Is Nothing Then Return ""
        Return value.Replace("'", "''")
    End Function

    'Validates a database name and returns it bracket quoted. Anything that is not
    'a plain identifier is rejected, so a name can never smuggle extra statements
    'into a BACKUP / RESTORE / ALTER DATABASE command.
    Public Function QuoteDbName(ByVal name As String) As String
        If String.IsNullOrEmpty(name) Then
            Throw New ArgumentException("Database name is required.")
        End If
        If Not Regex.IsMatch(name, "^[A-Za-z_][A-Za-z0-9_$#]*$") Then
            Throw New ArgumentException("Unsafe database name: " & name)
        End If
        Return "[" & name & "]"
    End Function

    'An import must land in a staging database and never on top of a live one.
    'The client backup is a full copy of their production database, so restoring
    'it over ours would replace real data with theirs.
    Public Sub GuardStagingTarget(ByVal targetDb As String, ByVal liveDb As String)
        If String.IsNullOrEmpty(targetDb) Then
            Throw New ArgumentException("Restore target is required.")
        End If
        If String.Equals(targetDb, liveDb, StringComparison.OrdinalIgnoreCase) Then
            Throw New InvalidOperationException(
                "Refusing to restore over the live database '" & liveDb & "'.")
        End If
        If Not targetDb.StartsWith("Temp", StringComparison.OrdinalIgnoreCase) Then
            Throw New InvalidOperationException(
                "Restore target '" & targetDb & "' is not a staging database.")
        End If
    End Sub

    'Redirects every file in the backup into dataDir, named after the target
    'database. Without this a restore writes to the paths recorded in the backup,
    'which are the client's own drive letters and will not exist here.
    Public Function BuildMoveClauses(ByVal targetDb As String,
                                     ByVal files As IEnumerable(Of BackupFile),
                                     ByVal dataDir As String) As String
        If files Is Nothing Then Throw New ArgumentException("Backup file list is required.")
        If String.IsNullOrEmpty(dataDir) Then Throw New ArgumentException("Data directory is required.")
        If Not dataDir.EndsWith("\") Then dataDir &= "\"

        Dim sb As New StringBuilder()
        Dim dataSeq As Integer = 0
        Dim logSeq As Integer = 0
        For Each f As BackupFile In files
            Dim ext As String
            Dim suffix As String
            If String.Equals(f.FileType, "L", StringComparison.OrdinalIgnoreCase) Then
                logSeq += 1
                ext = ".ldf"
                suffix = If(logSeq = 1, "_log", "_log" & logSeq)
            Else
                dataSeq += 1
                ext = ".mdf"
                suffix = If(dataSeq = 1, "", "_" & dataSeq)
            End If
            sb.Append("  MOVE N'").Append(SqlEscape(f.LogicalName)).Append("' TO N'")
            sb.Append(SqlEscape(dataDir & targetDb & suffix & ext)).Append("',").Append(vbCrLf)
        Next
        If sb.Length = 0 Then Throw New ArgumentException("Backup contains no files to move.")
        Return sb.ToString()
    End Function

    'RESTORE for the Import screen. Takes the database offline, restores the backup
    'into dataDir, then puts it back online.
    Public Function BuildRestoreSql(ByVal targetDb As String,
                                    ByVal bakPath As String,
                                    ByVal files As IEnumerable(Of BackupFile),
                                    ByVal dataDir As String) As String
        If String.IsNullOrEmpty(bakPath) Then
            Throw New ArgumentException("Backup file is required.")
        End If
        Dim q As String = QuoteDbName(targetDb)
        Dim sb As New StringBuilder()
        sb.Append("USE master").Append(vbCrLf)
        sb.Append("ALTER DATABASE ").Append(q).Append(" SET SINGLE_USER WITH ROLLBACK IMMEDIATE").Append(vbCrLf)
        sb.Append("RESTORE DATABASE ").Append(q).Append(" FROM DISK = N'").Append(SqlEscape(bakPath)).Append("' WITH FILE = 1,").Append(vbCrLf)
        sb.Append(BuildMoveClauses(targetDb, files, dataDir))
        sb.Append("  NOUNLOAD, REPLACE, STATS = 10").Append(vbCrLf)
        sb.Append("ALTER DATABASE ").Append(q).Append(" SET MULTI_USER")
        Return sb.ToString()
    End Function

    'BACKUP for the Backup screen.
    Public Function BuildBackupSql(ByVal dbName As String, ByVal destPath As String) As String
        If String.IsNullOrEmpty(destPath) Then
            Throw New ArgumentException("Backup destination is required.")
        End If
        Dim q As String = QuoteDbName(dbName)
        Return "BACKUP DATABASE " & q &
               " TO DISK = N'" & SqlEscape(destPath) & "'" &
               " WITH NOFORMAT, INIT, NAME = N'" & SqlEscape(dbName) & "-Full Database Backup'," &
               " SKIP, NOREWIND, NOUNLOAD, STATS = 10"
    End Function

    'BACKUP of the staging database for the Export screen. Same shape as a backup;
    'kept separate because the Export screen always targets staging, never live.
    Public Function BuildExportSql(ByVal stagingDb As String, ByVal destPath As String) As String
        If Not stagingDb.StartsWith("Temp", StringComparison.OrdinalIgnoreCase) Then
            Throw New InvalidOperationException(
                "Export source '" & stagingDb & "' is not a staging database.")
        End If
        Return BuildBackupSql(stagingDb, destPath)
    End Function

    'Reads the file list out of a backup without restoring anything.
    Public Function ReadBackupFileList(ByVal cnn As SqlConnection, ByVal bakPath As String) As List(Of BackupFile)
        Dim result As New List(Of BackupFile)()
        Using cmd As New SqlCommand("RESTORE FILELISTONLY FROM DISK = N'" & SqlEscape(bakPath) & "'", cnn)
            cmd.CommandTimeout = 0
            Using rd As SqlDataReader = cmd.ExecuteReader()
                While rd.Read()
                    result.Add(New BackupFile() With {
                        .LogicalName = Convert.ToString(rd("LogicalName")),
                        .PhysicalName = Convert.ToString(rd("PhysicalName")),
                        .FileType = Convert.ToString(rd("Type"))
                    })
                End While
            End Using
        End Using
        Return result
    End Function

    'Where this instance puts new database files.
    Public Function InstanceDataDir(ByVal cnn As SqlConnection) As String
        Using cmd As New SqlCommand("SELECT CONVERT(nvarchar(400), SERVERPROPERTY('InstanceDefaultDataPath'))", cnn)
            Return Convert.ToString(cmd.ExecuteScalar())
        End Using
    End Function

End Module
