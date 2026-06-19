Imports System.Data.SqlClient
'Public Class frm_Restore
'    Dim dbcon As SqlConnection
'    Dim dbcmd As SqlCommand
'    Private Sub txtBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
'        RestoreDatabaseOFD.ShowDialog()
'        txtdestination.Text = RestoreDatabaseOFD.FileName
'    End Sub

'    Private Sub txtRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestore.Click
'        Try
'            dbcon = New SqlConnection("Data Source=.;Integrated Security=SSPI;Initial Catalog=master")
'            Dim CMD As String = "USE master" & _
'                vbCrLf & "Alter Database " & cbDatabseDatabase.Text.ToUpper & " Set SINGLE_USER with Rollback Immediate" & _
'                vbCrLf & "RESTORE DATABASE " & cbDatabseDatabase.Text.ToUpper & " FROM  DISK = N'" & txtdestination.Text & "' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10" & _
'                vbCrLf & "Alter Database " & cbDatabseDatabase.Text.ToUpper & " Set MULTI_USER"
'            dbcmd = New SqlCommand(CMD, dbcon)
'            dbcon.Open()
'            dbcmd.ExecuteNonQuery()
'            dbcon.Close()
'            MsgBox("Restore completed successfully!", MsgBoxStyle.Information)
'            Me.Close()
'        Catch ex As Exception
'            MsgBox("For Server only!", MsgBoxStyle.Information)
'        End Try
'    End Sub

'    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

'    End Sub
'End Class