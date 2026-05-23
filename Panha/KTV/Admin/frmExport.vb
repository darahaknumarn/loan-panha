Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.IO.Compression

Public Class frmExport

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'MessageBox.Show(DateTimePicker1.Text.ToString)
        'Return
        'Return
        Try
            addIn("exec SP_EXPORT N'" & DateTimePicker1.Text.ToString & "',N'" & frmMain.lblCode.Text & "'")

            ''--------Start Validate Permission ------                 
            'If Not CheckPermission(Me.Tag, uid, 2) Then 'Check if menu(FormTag is menuID) and user(loggedin user) and privilege (2=New,3=update,4=delete,5=after export)
            '    MessageBox.Show("អ្នកមិនមានសិទ្ធិ Backup ទេ, សូមទាក់ទង IT", "Privileges")
            '    Return
            'End If
            '--------End Validate--------------------
            Dim appPath As String = Application.StartupPath()
            Dim dbcmd As SqlCommand
            Dim dbcon As SqlConnection
            dbcon = New SqlConnection("Data Source=.;Integrated Security=SSPI;Initial Catalog=master")
            Dim days As Integer = DateTime.Now.Day
            'Dim times As time
            'Dim cmd As String = "BACKUP DATABASE " & cbDatabseDatabase.Text.ToUpper & " TO  DISK = N'" & txtdestination.Text & "' WITH NOFORMAT, INIT,  NAME = N'" & cbDatabseDatabase.Text.ToUpper & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
            Dim days1 As Integer = DateTimePicker1.Value.Day
            Dim cmd As String = "Use TempPanha BACKUP DATABASE TempPanha TO  DISK = N'" & appPath & "\Export\Export " & frmMain.lblCode.Text & " " & Year(DateTimePicker1.Value) & "-" & Month(DateTimePicker1.Value) & "-" & days1 & ".bak' WITH NOFORMAT, INIT,  NAME = N'TempPanha-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
            dbcmd = New SqlCommand(cmd, dbcon)
            dbcon.Open()
            dbcmd.ExecuteNonQuery()
            dbcon.Close()
            ''MsgBox("Backup completed successfully!", MsgBoxStyle.Information)
            'ProgressBar1.Visible = False
            'Label1.Visible = False
            'Me.Close()
            MessageBox.Show("Export is completed!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'If MessageBox.Show("Do you want to send file export?", "Send Export", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
            '    'MessageBox.Show(appPath)
            '    Dim startPart As String = "D:\Work\New Project\Morokot\KTV\bin\Debug\Export\Export 002 2018-7-2.bak"
            '    'appPath & "\Export\Export " & frmMain.lblCode.Text & " " & Year(DateTimePicker1.Value) & "-" & Month(DateTimePicker1.Value) & "-" & days1 & ".bak"
            '    Dim zipName As String = "Export " & frmMain.lblCode.Text & " " & Year(DateTimePicker1.Value) & "-" & Month(DateTimePicker1.Value) & "-" & days1 & ".zip"
            '    ZipFile.CreateFromDirectory(startPart, zipName, CompressionLevel.Optimal, False)
            '    'Compress("a", "")
            'Else
            '    Return
            'End If

            'Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub frmExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        ''DateTimePicker1.Value = Now
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "MM/dd/yyyy"
    End Sub

End Class