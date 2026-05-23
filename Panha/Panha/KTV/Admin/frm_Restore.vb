Imports System.Data.SqlClient
Public Class frm_Restore
    Dim dbcon As SqlConnection
    Dim dbcmd As SqlCommand
    Private Sub txtBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        RestoreDatabaseOFD.ShowDialog()
        txtdestination.Text = RestoreDatabaseOFD.FileName
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Visible = True
        Label1.Visible = True
        ProgressBar1.Increment(10)
        If ProgressBar1.Value = 100 Then
            Timer1.Stop()
            Try
                dbcon = New SqlConnection("Data Source=.;Integrated Security=SSPI;Initial Catalog=master")
                Dim CMD As String = "USE master" & _
                                    vbCrLf & "Alter Database " & cbDatabseDatabase.Text.ToUpper & " Set SINGLE_USER with Rollback Immediate" & _
                                    vbCrLf & "RESTORE DATABASE " & cbDatabseDatabase.Text.ToUpper & " FROM  DISK = N'" & txtdestination.Text & "' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10" & _
                                    vbCrLf & "Alter Database " & cbDatabseDatabase.Text.ToUpper & " Set MULTI_USER"
                'Dim CMD As String = "RESTORE DATABASE " & cbDatabseDatabase.Text.ToUpper & " FROM DISK = N'" & txtdestination.Text & "' WITH REPLACE"
                dbcmd = New SqlCommand(CMD, dbcon)
                dbcon.Open()
                dbcmd.ExecuteNonQuery()
                dbcon.Close()
                MsgBox("Restore completed successfully!", MsgBoxStyle.Information)
                ProgressBar1.Visible = False
                Label1.Visible = False
                Me.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information)
                ProgressBar1.Visible = False
                Label1.Visible = False
            End Try
        End If
    End Sub
    Private Sub txtRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestore.Click
        If cbDatabseDatabase.Text = "" Or txtdestination.Text = "" Then
            MessageBox.Show("Database name or location database can't blank, please check again!", "Error!")
            Return
        End If
        If MessageBox.Show("Are you sure to restore this database, so everything can't undo?", "Restore Database", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            Timer1.Start()

        Else
            Return
        End If
    End Sub

    Private Sub ComLoad()
        cbDatabseDatabase.Items.Clear()
        'g_cnn.Open()
        Dim comm As New SqlCommand("select * from sysdatabases", g_cnn)
        Dim myreader As SqlDataReader = comm.ExecuteReader
        While myreader.Read
            cbDatabseDatabase.Items.Add(myreader(0))
        End While
        'g_cnn.Close()
    End Sub

    Private Sub frm_Restore_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Visible = False
        Label1.Visible = False
        ComLoad()
    End Sub
End Class