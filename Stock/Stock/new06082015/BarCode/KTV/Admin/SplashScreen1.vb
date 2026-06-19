Public NotInheritable Class frmSplash
    Dim sapi

    Private Sub frmSplash_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DoubleClick
        Me.Close()
        frmMain.Show()
    End Sub

    Private Sub frmSplash_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Me.Close()
    End Sub

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).

    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        frmMain.Hide()
    End Sub
    'Private Sub ShowInfo(ByVal input As String)
    '    Try
    '        Dim con As New System.Data.SqlClient.SqlConnection
    '        Dim com As New System.Data.SqlClient.SqlCommand
    '        Dim dr As System.Data.SqlClient.SqlDataReader
    '        con.ConnectionString = connectionString1
    '        con.Open()
    '        com.Connection = con
    '        com.CommandText = "Select fullname,phone,email,photo,address from tbldelegate where barcode='" & input & "'"
    '        dr = com.ExecuteReader
    '        If dr.Read = True Then
    '            frmDashboard5.Show()
    '            frmDashboard5.WindowState = FormWindowState.Maximized
    '        Else
    '            sapi = CreateObject("sapi.spvoice")
    '            sapi.speak("Can not find the information, please check again")
    '            Return
    '            'MessageBox.Show("Can not find the information, please check again")
    '        End If
    '        con.Close()
    '        con.Dispose()
    '    Catch ex As Exception
    '        MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    'Private Sub txtBarcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBarcode.TextChanged
    '    Try
    '        ShowInfo(Me.txtBarcode.Text)
    '        Me.txtBarcode.SelectionStart = 0
    '        Me.txtBarcode.SelectionLength = 10
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

End Class
