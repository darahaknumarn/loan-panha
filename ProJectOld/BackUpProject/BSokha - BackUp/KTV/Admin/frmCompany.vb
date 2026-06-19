Public Class frmCompany

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click, Label5.Click

    End Sub
    Private Sub frmCompany_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnNew.Text = "New"
        btnDelete.Text = "Delete"
        btnExit.Text = "Exit"
        btnEdit.Text = "Edit"
        SetFontDatagrid(DataGridView1)
        showCompany()
    End Sub
    Private Sub showCompany()
        ShowDataGrid(DataGridView1, "select CompanyID Company_Code,CompanyKhmerName Khmer_Name,CompanyEnglishName English_Name,Telephone Telephone,Email,Address from BK_Company")
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        If btnNew.Text = "New" Then
            btnNew.Text = "Save"
            btnEdit.Enabled = False
            btnDelete.Enabled = False
            btnExit.Text = "Cancel"
            txtCompanyCode.ReadOnly = False
            txtCompanyCode.Text = ""
            txtEnglishName.Text = ""
            txtKhmerName.Text = ""
            txtEmail.Text = ""
            txtPhone.Text = ""
            txtAddress.Text = ""
        ElseIf Me.btnNew.Text = "Update" Then
            updateCompany()
            showCompany()
            btnNew.Text = "New"
            btnExit.Text = "Exit"
            btnEdit.Enabled = True
            btnDelete.Enabled = True
        Else
            btnNew.Text = "New"
            btnExit.Text = "Exit"
            btnEdit.Enabled = True
            btnDelete.Enabled = True
            AddCompany()
            showCompany()
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If btnExit.Text = "Cancel" Then
            btnNew.Text = "New"
            btnEdit.Enabled = True
            btnDelete.Enabled = True
            btnExit.Text = "Exit"
            txtCompanyCode.ReadOnly = True
        Else
            Me.Close()
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If btnEdit.Text = "Edit" Then
            btnNew.Text = "Update"
            btnDelete.Enabled = False
            btnEdit.Enabled = False
            btnExit.Text = "Cancel"
        End If
    End Sub
    Private Sub updateCompany()
        Dim id As String = getData("select CompanyID from BK_Company where CompanyID ='" & txtCompanyCode.Text & "'")
        If id = "" Then
            MessageBox.Show("No company to update or invalid this company code, please check again!!!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim sql As String
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = txtKhmerName.Text
                .Add("@d1", SqlDbType.NVarChar).Value = txtEnglishName.Text
                .Add("@d2", SqlDbType.NVarChar).Value = txtPhone.Text
                .Add("@d3", SqlDbType.NVarChar).Value = txtEmail.Text
                .Add("@d4", SqlDbType.NVarChar).Value = txtAddress.Text
                .Add("@d5", SqlDbType.NVarChar).Value = uid
                .Add("@d6", SqlDbType.DateTime).Value = DateTime.Now
            End With
            sql = "update BK_Company set CompanyKhmerName=@d0,CompanyEnglishName=@d1,Telephone=@d2,Email=@d3,Address=@d4,User_Modify=@d5,Date_Modify=@d6 where CompanyID='" & txtCompanyCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            MessageBox.Show("Record has been updated!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub AddCompany()
        Dim id As String = getData("select CompanyID from BK_Company where CompanyID ='" & txtCompanyCode.Text & "'")
        'If id = "" Then
        '    MessageBox.Show("No company to update or invalid this company code, please check again!!!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Return
        'End If
        Dim sql As String
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = txtKhmerName.Text
                .Add("@d1", SqlDbType.NVarChar).Value = txtEnglishName.Text
                .Add("@d2", SqlDbType.NVarChar).Value = txtPhone.Text
                .Add("@d3", SqlDbType.NVarChar).Value = txtEmail.Text
                .Add("@d4", SqlDbType.NVarChar).Value = txtAddress.Text
                .Add("@d5", SqlDbType.NVarChar).Value = uid
                .Add("@d6", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d7", SqlDbType.NVarChar).Value = me.txtCompanyCode.text
            End With
            sql = "insert into BK_Company(CompanyID,CompanyKhmerName,CompanyEnglishName,User_Create,Date_Create,Telephone) values(@d7,@d0,@d1,@d5,@d6,@d2)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            MessageBox.Show("Record has been updated!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        If Me.DataGridView1.Rows.Count = 0 Then
            Return
        Else
            With Me.DataGridView1.Rows(Me.DataGridView1.CurrentCell.RowIndex)
                txtCompanyCode.Text = .Cells(0).Value
                txtKhmerName.Text = .Cells(1).Value
                txtEnglishName.Text = .Cells(2).Value
                txtPhone.Text = .Cells(3).Value
                'txtEmail.Text = .Cells(4).Value
                'txtAddress.Text = .Cells(5).Value
            End With
        End If
    End Sub
End Class