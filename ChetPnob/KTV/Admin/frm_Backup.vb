Imports System.Data.SqlClient
Public Class frm_Backup
    Dim dbcon As SqlConnection
    Dim dbcmd As SqlCommand
    Private Sub txtBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBackup.Click
        Timer1.Start()
    End Sub
    Private Sub frm_Backup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Visible = False
        Label1.Visible = False
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Visible = True
        Label1.Visible = True
        ProgressBar1.Increment(10)
        If ProgressBar1.Value = 100 Then
            Timer1.Stop()
            Try
                ''--------Start Validate Permission ------                 
                'If Not CheckPermission(Me.Tag, uid, 2) Then 'Check if menu(FormTag is menuID) and user(loggedin user) and privilege (2=New,3=update,4=delete,5=after export)
                '    MessageBox.Show("អ្នកមិនមានសិទ្ធិ Backup ទេ, សូមទាក់ទង IT", "Privileges")
                '    Return
                'End If
                '--------End Validate--------------------
                Dim appPath As String = Application.StartupPath()
                dbcon = New SqlConnection("Data Source=.;Integrated Security=SSPI;Initial Catalog=master")
                Dim days As Integer = DateTime.Now.Day
                'Dim times As time
                'Dim cmd As String = "BACKUP DATABASE " & cbDatabseDatabase.Text.ToUpper & " TO  DISK = N'" & txtdestination.Text & "' WITH NOFORMAT, INIT,  NAME = N'" & cbDatabseDatabase.Text.ToUpper & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
                Dim cmd As String = "BACKUP DATABASE " & DB & " TO  DISK = N'" & appPath & "\BackUp\" & frmMain.lblCode.Text & " " & DB & Year(DateTime.Now) & "" & Month(DateTime.Now) & "" & days & " " & Hour(DateTime.Now) & "" & Minute(DateTime.Now) & ".bak' WITH NOFORMAT, INIT,  NAME = N'" & DB & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
                dbcmd = New SqlCommand(cmd, g_cnn)
                dbcon.Open()
                dbcmd.ExecuteNonQuery()
                dbcon.Close()
                MsgBox("Backup completed successfully!", MsgBoxStyle.Information)
                ProgressBar1.Visible = False
                Label1.Visible = False
                Me.Close()
            Catch ex As Exception
                MsgBox("For Server only, contact IT for more detail!", MsgBoxStyle.Information, "IT Solution")
                ProgressBar1.Visible = False
                Label1.Visible = False
            End Try
        End If
    End Sub
End Class