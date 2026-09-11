Imports System.Data.SqlClient
Imports System.IO

Public Class frmImport

    Private Sub frmImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.ReadOnly = True
        Me.WindowState = FormWindowState.Maximized
        lblStatus.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.OpenFileDialog1.ShowDialog()
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub

    'Shows what the import is doing. The work runs on the UI thread, so paint the
    'label before the call that blocks or the user never sees it.
    Private Sub SetBusy(ByVal message As String)
        lblStatus.Text = message
        lblStatus.Visible = True
        Button1.Enabled = False
        Button2.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        lblStatus.Refresh()
    End Sub

    Private Sub SetIdle()
        lblStatus.Visible = False
        Button1.Enabled = True
        Button2.Enabled = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Please choose a backup file to import.", "Import",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        If Not File.Exists(TextBox1.Text) Then
            MessageBox.Show("Backup file not found:" & vbCrLf & TextBox1.Text, "Import",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim imported As Boolean = False
        Try
            SetBusy("Restoring backup file, please wait...")
            restore()

            SetBusy("Importing data into " & DB & ", please wait...")
            If g_cnn.State <> ConnectionState.Open Then g_cnn.Open()
            'Not addIn(): it swallows errors and keeps the default 30 second timeout,
            'which SP_MAIN_IMPORT will always exceed.
            Using dbcmd As New SqlCommand("Exec SP_MAIN_IMPORT", g_cnn)
                dbcmd.CommandTimeout = 0
                dbcmd.ExecuteNonQuery()
            End Using
            imported = True
        Catch ex As Exception
            MessageBox.Show("Import failed:" & vbCrLf & ex.Message, "Import",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetIdle()
        End Try

        'Only after the whole sequence succeeded, and only once the form is idle again -
        'closing disposes the controls the code above touches.
        If imported Then
            MessageBox.Show("Import file successful!", "Import",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
    End Sub

    'Restores the chosen backup into the staging database. Throws on failure so the
    'caller can stop before SP_MAIN_IMPORT deletes live rows it cannot replace.
    'Runs on its own connection to master, the way the Backup / Restore / Export
    'screens do: the script starts with USE master, and a USE on the shared g_cnn
    'leaves every later statement - SP_MAIN_IMPORT included - resolving against
    'master instead of the signed-in database.
    Private Sub restore()
        'Called stagingDb rather than db on purpose: VB identifiers are case-insensitive,
        'so a local named "db" shadows the module-level DB holding the signed-in database.
        Dim stagingDb As String = ImportStagingDB()
        GuardStagingTarget(stagingDb, DB)
        Using dbcon As New SqlConnection(MasterCnnString())
            dbcon.Open()
            Dim files = ReadBackupFileList(dbcon, TextBox1.Text)
            Dim dataDir As String = InstanceDataDir(dbcon)
            Dim CMD As String = BuildRestoreSql(stagingDb, TextBox1.Text, files, dataDir)
            Using dbcmd As New SqlCommand(CMD, dbcon)
                dbcmd.CommandTimeout = 0
                dbcmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class
