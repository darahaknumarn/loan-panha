Public Class frmCountry
    Dim Ccode As Integer
    Private Sub B4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B4.Click
        Me.Close()
    End Sub

    Private Sub B1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B1.Click
        If Me.B1.Text.Equals("&New") Then
            Me.B1.Text = "&Save"
            Me.B2.Enabled = False
            Me.B3.Text = "&Cancel"
            Me.B4.Enabled = False
            Me.txtCCode.Text = ""
            Me.txtCName.Text = ""
            Me.txtCNameKH.Text = ""
            '  EID = "E" & Microsoft.VisualBasic.Format(Val(Microsoft.VisualBasic.Right(getLastRow(), 2)) + 1, "00")
        ElseIf Me.B1.Text.Equals("&Save") Then

            '------------------------Add User
            If Me.txtCCode.Text <> "" And Me.txtCName.Text <> "" And Me.txtCNameKH.Text <> "" Then
                AddCountry()
            Else
                MessageBox.Show("Please input the full information", "NiTA POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Me.B1.Text = "&New"
            Me.B2.Text = "&Edit"
            Me.B3.Text = "&Delete"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            AddToGrid(Me.dgdetail, 4, "select * from tblcountry")
        ElseIf Me.B1.Text.Equals("&Update") Then

            If Me.txtCCode.Text <> "" And Me.txtCName.Text <> "" And Me.txtCNameKH.Text <> "" And Ccode > 0 Then
                UpdateCountry(Ccode)
            Else
                MessageBox.Show("សូមបញ្ជូលទិន្ន័យអោយគ្រប់គ្រាន់ Please input the full information", "NiTA POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Me.B1.Text = "&New"
            Me.B2.Text = "&Edit"
            Me.B3.Text = "&Delete"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            AddToGrid(Me.dgdetail, 4, "select * from tblcountry")
        End If
    End Sub

    Private Sub B2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B2.Click
        Me.B1.Text = "&Update"
        Me.B2.Enabled = False
        Me.B3.Text = "&Cancel"
        Me.B4.Enabled = False
    End Sub

    Private Sub B3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B3.Click
        If Me.B3.Text.Equals("&Cancel") Then
            Me.B2.Enabled = True
            Me.B2.Text = "&Edit"
            Me.B4.Enabled = True
            Me.B1.Text = "&New"
            Me.B3.Text = "&Delete"
        ElseIf Me.B3.Text = "&Delete" Then
            If Ccode > 0 Then
                If MessageBox.Show("Are you sure you want to delete this user account?" & Microsoft.VisualBasic.Chr(13) & "តើអ្នកពិតជាចង់លុបមែនទេ?", "NiTA Solution", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    addIn("Delete from TBLCOUNTRY where CID =" & Ccode)
                    AddToGrid(Me.dgdetail, 4, "select * from tblcountry")
                End If
            End If
        End If
    End Sub

    Private Sub dgdetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdetail.Click
        Try
            If Me.dgdetail.SelectedRows.Count > 0 Then
                Ccode = Val(Me.dgdetail.SelectedRows(0).Cells(0).Value.ToString)
                Me.txtCCode.Text = Me.dgdetail.SelectedRows(0).Cells(1).Value
                Me.txtCName.Text = Me.dgdetail.SelectedRows(0).Cells(2).Value
                Me.txtCNameKH.Text = Me.dgdetail.SelectedRows(0).Cells(3).Value
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
    End Sub

    Sub AddCountry()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = Me.txtCCode.Text
                .Add("@d2", SqlDbType.NVarChar).Value = Me.txtCName.Text
                .Add("@d3", SqlDbType.NVarChar).Value = Me.txtCNameKH.Text
                .Add("@d4", SqlDbType.Date).Value = Now.Date
            End With
            sql = "Insert into tblCountry(CountryCode,CountryName,CountryNameKH,InputDate) values(@d1,@d2,@d3,@d4)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
    End Sub
    Sub UpdateCountry(ByVal Code As Integer)
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.Int).Value = Code
                .Add("@d2", SqlDbType.NVarChar).Value = Me.txtCCode.Text
                .Add("@d3", SqlDbType.NVarChar).Value = Me.txtCName.Text
                .Add("@d4", SqlDbType.NVarChar).Value = Me.txtCNameKH.Text

            End With
            sql = "Update tblCountry set CountryCode=@d2,CountryName=@d3,CountryNameKH=@d4 where CID=@d1"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try

    End Sub

    Private Sub frmCountry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddToGrid(Me.dgdetail, 4, "select * from tblcountry")
    End Sub
End Class