Public Class frmsignin

    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        Try
            'If openConnection(connectionString1) = True Then
            If IsExisted("Select * from tbluser where employeeid=" & Amstr(Me.txtstaff.Text) & " and password=" & Amstr(Me.txtpass.Text)) = True Then
                uid = Me.txtstaff.Text
                Me.Hide()
                Security()
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
        Frm_Connection.Close()
        Application.Exit()
    End Sub

    Private Sub frmsignin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '----------------------log to the database 
        Try
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            connectionString1 = GetSetting("CamITSo", "Connection", "String")
            If connectionString1 = "" Then
                Frm_Connection.Show()
                Me.Close()
                Exit Sub
            End If
            'If openConnection(connectionString1) = True Then
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = "select * from tblcompany"
            dr = com.ExecuteReader()
            If dr.Read = True Then
                Me.piclogo.Image = toImage(dr(1))
            Else
                Me.piclogo.Image = Nothing
            End If
            'End If
            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Frm_Connection.Show()
        End Try
    End Sub

    Private Sub txtpass_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpass.KeyPress
        If e.KeyChar = Chr(13) Then
            btnlogin_Click(sender, e)
        End If
    End Sub

    Private Sub txtstaff_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtstaff.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtstaff.Text = "" Then
                Return
            Else
                txtpass.Focus()
            End If
        End If
    End Sub

    Private Sub piclogo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles piclogo.Click

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class