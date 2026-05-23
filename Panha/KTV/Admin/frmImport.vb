Imports System.Data.SqlClient

Public Class frmImport

    Private Sub frmImport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.ReadOnly = True
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.OpenFileDialog1.ShowDialog()
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            restore()
            g_cnn.Open()
            'Dim comID As String = getData("use TempMorokot select CompanyID from BK_Company")
            g_cnn.Close()
            g_cnn.Open()
            'addIn("exec spDeleteData '" & comID & "'")
            'addIn("exec spImportData")
            addIn("Exec SP_MAIN_IMPORT")
            MessageBox.Show("Import file successful!", "Import", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub restore()
        Dim dbcmd As SqlCommand
        Dim db As String = "TempPanha"
        Try
            'dbcon = New SqlConnection("Data Source=.;Integrated Security=SSPI;Initial Catalog=master")
            Dim CMD As String = "USE master" & _
                vbCrLf & "Alter Database " & db & " Set SINGLE_USER with Rollback Immediate" & _
                vbCrLf & "RESTORE DATABASE " & db & " FROM  DISK = N'" & TextBox1.Text & "' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10" & _
                vbCrLf & "Alter Database " & db & " Set MULTI_USER"
            dbcmd = New SqlCommand(CMD, g_cnn)
            'dbcon.Open()
            dbcmd.ExecuteNonQuery()
            g_cnn.Close()
            'MsgBox("Imonport successfully!", MsgBoxStyle.Information)
            Me.Close()
        Catch ex As Exception
            MsgBox("Can't Import", MsgBoxStyle.Information)
        End Try
    End Sub
End Class