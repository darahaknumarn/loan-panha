Public Class frmSetup

    Private Sub frmSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If frmMain.users = "sh" Then
                Button1.Enabled = False
                Button2.Enabled = False
            End If
            If frmMain.users = "acc" Then
                Button1.Enabled = False
                Button2.Enabled = False
            End If
            If frmMain.users = "e01" Then
                Button1.Enabled = False
                Button2.Enabled = False
            End If
            If frmMain.users = "admin" Then
                Button1.Enabled = True
                Button2.Enabled = True
            End If
            Dim photo As Image
            'Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            com.CommandText = "select * from tblcompany"
            dr = com.ExecuteReader()
            If dr.Read = True Then
                'Me.logo.Image = toImage(dr(1))
                photo = Me.logo.Image
                If (photo IsNot Nothing) Then
                    photo.Save(Application.StartupPath & "\101010101010101photo.JPG")
                    Me.logo.ImageLocation = Application.StartupPath & "\101010101010101photo.JPG"
                End If

            End If
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Timer1.Enabled = True
        Timer1.Start()
        Timer2.Enabled = False
        Timer3.Enabled = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label1.ForeColor = Color.CadetBlue
        Me.BackColor = Color.LightCyan
        Timer1.Stop()
        Timer2.Start()
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Label1.ForeColor = Color.Yellow
        Me.BackColor = Color.LightGreen
        Timer2.Stop()
        Timer3.Start()
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Label1.ForeColor = Color.Red
        Me.BackColor = Color.LightSlateGray
        Timer3.Stop()
        Timer1.Start()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
        OpenFileDialog1.Filter = "JPEG File (*.jpeg)|*.jpeg|JPG File (*.jpg)|*.jpg|All files (*.*)|*.*"
        Me.logo.ImageLocation = OpenFileDialog1.FileName.ToString
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            Dim imageData As Byte()
            Dim sFileName As String
            imageData = ReadFile(Me.logo.ImageLocation)
            sFileName = System.IO.Path.GetFileName(Me.logo.ImageLocation)
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .AddWithValue("@d2", DirectCast(imageData, Object))
            End With
            com.CommandText = "Update tblCompany set companyLogo=@d2"
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
            MessageBox.Show("Your logo has been updated.", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
    End Sub
End Class