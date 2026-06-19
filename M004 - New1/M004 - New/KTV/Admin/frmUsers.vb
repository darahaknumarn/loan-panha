Imports System.Data.SqlClient

Public Class frmUsers
    Private Sub frmUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BindUserGrid()
        Combobox_Datasource(cboBranch, "BK_Company", "CompanyKhmerName", "CompanyID", "", g_cnn)
    End Sub
    Private Sub BindUserGrid()
        SetFontDatagrid(dgvUsers)
        dgvUsers.DataSource = Nothing
        Dim DT As DataTable
        Dim str As String = "SELECT SU.User_Name,SU.PassWords,SU.BrID,BR.CompanyKhmerName BrName,SU.Full_Name,SU.Lock FROM sys_User SU  LEFT JOIN BK_Company BR ON SU.BrID = BR.CompanyID "
        DT = ExecuteDatatable(str, g_cnn)
        MakeColumns(dgvUsers)
        dgvUsers.DataSource = DT
    End Sub
    Private Sub MakeColumns(objDGV As DataGridView)
        'objDGV.Columns.Add(MakeTColumn("No", "Nº", 0, True))
        objDGV.Columns.Add(MakeTColumn("User_Name", "User Name", 200, True))
        objDGV.Columns.Add(MakeTColumn("PassWords", "PassWords", 0, False))
        objDGV.Columns.Add(MakeTColumn("BrID", "BrID", 300, False))
        objDGV.Columns.Add(MakeTColumn("BrName", "Branch Name", 300, True))
        objDGV.Columns.Add(MakeTColumn("Full_Name", "Full Name", 300, True))
        objDGV.Columns.Add(MakeCheckColumn("Lock", "Locked", 150, True))
    End Sub
    Private Sub dgvUsers_SelectionChanged(sender As Object, e As EventArgs) Handles dgvUsers.SelectionChanged
        If dgvUsers.SelectedRows.Count > 0 Then
            txtUserName.Text = dgvUsers.SelectedRows(0).Cells("User_Name").Value
            txtPassword.Text = If(IsDBNull(dgvUsers.SelectedRows(0).Cells("PassWords").Value), "", dgvUsers.SelectedRows(0).Cells("PassWords").Value)
            txtFullName.Text = If(IsDBNull(dgvUsers.SelectedRows(0).Cells("Full_Name").Value), "", dgvUsers.SelectedRows(0).Cells("Full_Name").Value)
            chkLock.Checked = dgvUsers.SelectedRows(0).Cells("Lock").Value
            cboBranch.SelectedValue = dgvUsers.SelectedRows(0).Cells("BrID").Value
            DisableController()
            ResetButton()
        End If
    End Sub
    Private Sub ResetButton()
        BtnNew.Text = "ថ្មី"
        BtnEdit.Text = "កែប្រែ"
        BtnNew.Enabled = True
        BtnEdit.Enabled = True
        BtnDelete.Enabled = True
    End Sub
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "ថ្មី" Then
            ClearController()
            EnableController()
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnNew.Text = "រក្សាទុក"
        Else
            SaveUser("New", g_cnn)
            BtnNew.Text = "ថ្មី"
            DisableController()
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BindUserGrid()
        End If
    End Sub
    Private Sub SaveUser(flage As String, sqlCon As SqlClient.SqlConnection)
        Dim com As SqlCommand = sqlCon.CreateCommand()
        If flage = "New" Then
            com.CommandText = "INSERT INTO sys_User(User_Name,BrID,Full_Name,Lock,Lock_Date,Rec_Status,User_Create,Date_Create,PassWords,IsExport)      VALUES(@User_Name,@BrID,@Full_Name,@Lock,@Lock_Date,@Rec_Status,@User_Create,@Date_Create,@PassWords,0)"
            com.Parameters.Add("@User_Name", SqlDbType.NVarChar).Value = txtUserName.Text
            com.Parameters.Add("@BrID", SqlDbType.NVarChar).Value = cboBranch.SelectedValue
            com.Parameters.Add("@Full_Name", SqlDbType.NVarChar).Value = txtFullName.Text
            com.Parameters.Add("@Lock", SqlDbType.Bit).Value = chkLock.Checked
            com.Parameters.Add("@Lock_Date", SqlDbType.DateTime).Value = If(chkLock.Checked = True, Now, DBNull.Value)
            com.Parameters.Add("@Rec_Status", SqlDbType.Bit).Value = 0
            com.Parameters.Add("@User_Create", SqlDbType.NVarChar).Value = uid
            com.Parameters.Add("@Date_Create", SqlDbType.DateTime).Value = Now()
            com.Parameters.Add("@PassWords", SqlDbType.NVarChar).Value = txtPassword.Text
        ElseIf flage = "Update" Then
            com.CommandText = "UPDATE sys_User SET User_Name=@User_Name,BrID=@BrID,Full_Name=@Full_Name,Lock=@Lock,Lock_Date=@Lock_Date,User_Modify=@User_Modify,Date_Modify=@Date_Modify,PassWords=@PassWords,IsExport=0 WHERE User_Name=@Old_User_Name"
            com.Parameters.Add("@User_Name", SqlDbType.NVarChar).Value = txtUserName.Text
            com.Parameters.Add("@BrID", SqlDbType.NVarChar).Value = cboBranch.SelectedValue
            com.Parameters.Add("@Full_Name", SqlDbType.NVarChar).Value = txtFullName.Text
            com.Parameters.Add("@Lock", SqlDbType.Bit).Value = chkLock.Checked
            com.Parameters.Add("@Lock_Date", SqlDbType.DateTime).Value = If(chkLock.Checked = True, Now, DBNull.Value)
            com.Parameters.Add("@User_Modify", SqlDbType.NVarChar).Value = uid
            com.Parameters.Add("@Date_Modify", SqlDbType.DateTime).Value = Now()
            com.Parameters.Add("@PassWords", SqlDbType.NVarChar).Value = txtPassword.Text
            com.Parameters.Add("@Old_User_Name", SqlDbType.NVarChar).Value = dgvUsers.SelectedRows(0).Cells(0).Value.ToString()
        ElseIf flage = "Delete" Then 'Delete
            'com.CommandText = "DELETE FROM sys_User WHERE User_Name=@User_Name"
            com.CommandText = "UPDATE sys_User SET User_Delete=@User_Delete,Date_Delete=@Date_Delete,Rec_Status=@Rec_Status,IsExport=0 WHERE User_Name=@Old_User_Name"
            com.Parameters.Add("@Rec_Status", SqlDbType.Bit).Value = 1
            com.Parameters.Add("@User_Delete", SqlDbType.NVarChar).Value = uid
            com.Parameters.Add("@Date_Delete", SqlDbType.DateTime).Value = Now()
            com.Parameters.Add("@Old_User_Name", SqlDbType.NVarChar).Value = dgvUsers.SelectedRows(0).Cells(0).Value.ToString()
        End If

        com.ExecuteNonQuery()
    End Sub
    Private Sub EnableController()
        txtUserName.Enabled = True
        txtPassword.Enabled = True
        txtFullName.Enabled = True
        cboBranch.Enabled = True
        chkLock.Enabled = True
    End Sub
    Private Sub DisableController()
        txtUserName.Enabled = False
        txtPassword.Enabled = False
        txtFullName.Enabled = False
        cboBranch.Enabled = False
        chkLock.Enabled = False
    End Sub
    Private Sub ClearController()
        txtUserName.Text = ""
        txtPassword.Text = ""
        txtFullName.Text = ""
        cboBranch.SelectedIndex = 0
        chkLock.Checked = False
    End Sub
    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        If BtnEdit.Text = "កែប្រែ" Then
            EnableController()
            BtnNew.Enabled = False
            BtnDelete.Enabled = False
            BtnEdit.Text = "រក្សាទុក"
        Else
            SaveUser("Update", g_cnn)
            BtnEdit.Text = "កែប្រែ"
            DisableController()
            BtnNew.Enabled = True
            BtnDelete.Enabled = True
            BindUserGrid()
        End If
    End Sub
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If dgvUsers.SelectedRows.Count > 0 Then
            Dim Result As Integer = MessageBox.Show("តើអ្នកពិតជាចង់លុបអ្នកប្រើប្រាស់" + dgvUsers.SelectedRows(0).Cells(0).Value.ToString() + " ពិតម៉ែន?", "", MessageBoxButtons.YesNo)
            If Result = DialogResult.Yes Then
                SaveUser("Delete", g_cnn)
                BindUserGrid()
            End If
        Else
            MessageBox.Show("សូមជ្រើសរើសអ្នកប្រើប្រាស់ម្នាក់ដើម្បីធ្វើការលុប!!!", "លុបអ្នកប្រើប្រាស់")
        End If
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub
End Class