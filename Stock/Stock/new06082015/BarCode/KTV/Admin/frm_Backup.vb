Imports System.Data.SqlClient
Public Class frm_Backup
    Dim dbcon As SqlConnection
    Dim dbcmd As SqlCommand
    Private Sub txtBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBackup.Click
        Try
            dbcon = New SqlConnection("Data Source=.;Integrated Security=SSPI;Initial Catalog=master")
            Dim cmd As String = "BACKUP DATABASE " & cbDatabseDatabase.Text.ToUpper & " TO  DISK = N'" & txtdestination.Text & "' WITH NOFORMAT, INIT,  NAME = N'" & cbDatabseDatabase.Text.ToUpper & "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10"
            dbcmd = New SqlCommand(cmd, dbcon)
            dbcon.Open()
            dbcmd.ExecuteNonQuery()
            dbcon.Close()
            MsgBox("Backup completed successfully!", MsgBoxStyle.Information)
            Me.Close()
        Catch ex As Exception
            MsgBox("For Server only!", MsgBoxStyle.Information, "NiTA Solution")
        End Try
    End Sub

    Private Sub txtBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBrowse.Click
        Backup_SaveFileDialog.ShowDialog()
        txtdestination.Text = Backup_SaveFileDialog.FileName
    End Sub
    Private Sub txtdestination_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtdestination.TextChanged

    End Sub

    Private Sub frm_Backup_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.cbDatabseDatabase.SelectedIndex = 0
    End Sub
End Class