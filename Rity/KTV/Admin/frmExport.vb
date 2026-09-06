Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.IO.Compression

Public Class frmExport

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            addIn("exec SP_EXPORT N'" & FormatDateTime(Me.DateTimePicker1.Value, DateFormat.ShortDate) & "',N'" & frmMain.lblCode.Text & "'")

            ''--------Start Validate Permission ------                 
            'If Not CheckPermission(Me.Tag, uid, 2) Then 'Check if menu(FormTag is menuID) and user(loggedin user) and privilege (2=New,3=update,4=delete,5=after export)
            '    MessageBox.Show("អ្នកមិនមានសិទ្ធិ Backup ទេ, សូមទាក់ទង IT", "Privileges")
            '    Return
            'End If
            '--------End Validate--------------------
            Dim appPath As String = Application.StartupPath()
            Dim dbcmd As SqlCommand
            Dim dbcon As SqlConnection
            dbcon = New SqlConnection("Data Source=.\SQLEXPRESS;User Id=sa;Password=123456;Initial Catalog=master")
            Dim days As Integer = DateTime.Now.Day
            'Dim times As time
            'Dim cmd As String = "BACKUP DATABASE " & cbDatabseDatabase.Text.ToUpper & " TO  DISK = N'" & txtdestination.Text & "' WITH NOFORMAT, INIT,  NAME = N'" & cbDatabseDatabase.Text.ToUpper & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
            Dim days1 As Integer = DateTimePicker1.Value.Day
            Dim cmd As String = "Use TempLoan BACKUP DATABASE TempLoan TO  DISK = N'" & appPath & "\Export\Export " & frmMain.lblCode.Text & " " & Year(DateTimePicker1.Value) & "-" & Month(DateTimePicker1.Value) & "-" & days1 & ".bak' WITH NOFORMAT, INIT,  NAME = N'TempMorokot-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
            dbcmd = New SqlCommand(cmd, dbcon)
            dbcon.Open()
            dbcmd.ExecuteNonQuery()
            dbcon.Close()

            MessageBox.Show("Export is completed!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub frmExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub

End Class