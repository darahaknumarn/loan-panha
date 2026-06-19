Imports System.IO
Public Class Frm_Connection
    Dim PM As New Cls_qbPayroll
    Private Sub btnsave_Click(ByVal sender As System.Object, _
    ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If PM.MustExist(txtserver, "Server is mandatory!") = True Then Exit Sub
            If PM.MustExist(txtdatabase, "Database is mandatory!") = True Then Exit Sub
            If PM.MustExist(txtuserid, "User id is mandatory!") = True Then Exit Sub
            If PM.MustExist(txtpassword, "Password is mandatory!") = True Then Exit Sub
            SaveSetting("CamITSo", "Connection", "String", "Data Source=" & txtserver.Text & ";Database=" & txtdatabase.Text & ";user id=" & txtuserid.Text & ";password=" & txtpassword.Text)
            MessageBox.Show("Save completed!", "Connection string configuration", MessageBoxButtons.OK, MessageBoxIcon.Information)
            connectionString1 = GetSetting("CamITSo", "Connection", "String")
            Me.Close()
            frmsignin.Show()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
End Class
