Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Public Class FrmStaff
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "ថ្មី" Then
            Me.txtStaffID.Text = ""
            Me.txtName.Text = ""
            Me.txtStaffID.ReadOnly = False
            BtnNew.Text = "រក្សាទុកថ្មី"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
        ElseIf BtnNew.Text = "រក្សាទុកថ្មី" Then
            Me.txtStaffID.ReadOnly = True
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            If txtStaffID.Text = "" Or txtName.Text = "" Or ComboBox1.Text = "" Then
                resultError = frmMessageError.ShowBoxError("សូមបំពេញលេខកូដ ឈ្មោះ និង តួនាទីបុគ្គលិក។", "មិនអាចបន្តទៅមុខ")
                ' MessageBox.Show("Please fill the Staff ID and Staff Name", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Else
                addstaff()
                ShowStaff()
            End If
        ElseIf BtnNew.Text = "រក្សាទុក" Then
            updatestaff()
            ShowStaff()
            BtnNew.Text = "ថ្មី"
            BtnExit.Text = "ចាកចេញ"
            BtnEdit.Text = "កែប្រែ"
            BtnDelete.Enabled = True
            BtnEdit.Enabled = True
        Else
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        End If
    End Sub
    Sub updatestaff()
        If txtName.Text = "" Then
            resultError = frmMessageError.ShowBoxError("សូមបំពេញឈ្មោះអោយបានត្រឹមត្រូវមុននិងធ្វើការរក្សាទុក", "ពិនិត្យឡើងវិញ")
            Return
        End If
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = Me.txtName.Text
                .Add("@d2", SqlDbType.NVarChar).Value = txtMoto.Text
                .Add("@d3", SqlDbType.NVarChar).Value = ComboBox1.Text
                .Add("@d4", SqlDbType.NVarChar).Value = txtMoto2.Text
                .Add("@d5", SqlDbType.NVarChar).Value = txtMoto3.Text
            End With
            sql = "update tblStaff set StaffName=@d1,motoNo=@d2,Position=@d3,motoNo2=@d4,motoNo3=@d5 where StaffID='" & txtStaffID.Text & "' and BrID='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Sub addstaff()
        Dim id1 As String = getData("select staffid from tblStaff where staffID='" & txtStaffID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
        If id1 = "" Then
            Dim ID As String = getData("Select top 1 ID from tblstaff where BrID='" & frmMain.lblCode.Text & "' order by ID desc")
            If ID = "" Then
                ID = "1"
            Else
                ID = Convert.ToDecimal(ID) + 1
            End If
            Dim sql As String
            Try
                Dim com As New SqlClient.SqlCommand
                Dim con As New SqlClient.SqlConnection
                con.ConnectionString = connectionString1
                con.Open()
                com.Connection = con
                With com.Parameters
                    .Add("@d0", SqlDbType.NVarChar).Value = Me.txtStaffID.Text
                    .Add("@d1", SqlDbType.NVarChar).Value = Me.txtName.Text
                    .Add("@d2", SqlDbType.NVarChar).Value = txtMoto.Text
                    .Add("@d3", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                    .Add("@d4", SqlDbType.NVarChar).Value = ComboBox1.Text.Trim
                    .Add("@d5", SqlDbType.Int).Value = ID
                    .Add("@d6", SqlDbType.NVarChar).Value = txtMoto2.Text
                    .Add("@d7", SqlDbType.NVarChar).Value = txtMoto3.Text
                End With
                sql = "insert into tblstaff(id,staffid,staffname,motoNo,BrID,Position,motoNo2,motoNo3) values (@d5,@d0,@d1,@d2,@d3,@d4,@d6,@d7)"
                com.CommandText = sql
                com.ExecuteNonQuery()
                com.Parameters.Clear()
                com.Dispose()
                con.Close()
                con.Dispose()
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
            End Try
        Else
            resultError = frmMessageError.ShowBoxError("មិនអាចបញ្ចូលបានទេព្រោះបុគ្គលិកនេះមានរួចហើយ", "មើលឡើងវិញ")
            Return
        End If
    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "បោះបង់" Then
            Me.txtStaffID.ReadOnly = True
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            Me.Close()
        End If
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        If BtnEdit.Text = "កែប្រែ" Then
            If Me.txtStaffID.Text = "" Or Me.txtName.Text = "" Then
                resultError = frmMessageError.ShowBoxError("សូមជ្រើសរើសលេខកូដនឹងឈ្មោះបុគ្គលិកមុននឹងធ្វើការកែប្រែ", "ពិនិត្យម្តងទៀត")
                ' MessageBox.Show("Please select any item to edit information.", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                BtnEdit.Enabled = False
                BtnNew.Text = "រក្សាទុក"
                BtnDelete.Enabled = False
                BtnExit.Text = "បោះបង់"
                Me.txtStaffID.ReadOnly = True
                Me.txtMoto.ReadOnly = False
                Me.txtMoto2.ReadOnly = False
                Me.txtMoto3.ReadOnly = False
            End If
        End If
    End Sub
    Sub ShowStaff()
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 5
        DataGridView1.Columns(0).Name = "No"
        DataGridView1.Columns(1).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(2).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(3).Name = "លេខម៉ូតូ"
        DataGridView1.Columns(4).Name = "តួនាទី"
        'DataGridView1.Columns(5).Name = "លេខម៉ូតូ"
        AddToGrid(DataGridView1, 5, "select id,StaffID,StaffName,MotoNo,Position from tblStaff where BrID='" & frmMain.lblCode.Text & "' order by StaffID")
    End Sub
    Private Sub FrmStaff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmMain.users = "sh" Then
            BtnDelete.Enabled = False
            BtnEdit.Enabled = False
            BtnNew.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            txtStaffID.ReadOnly = True
        End If
        ComboBox1.Text = ""
        Dim p As String = getData("select Position from tblPosition where BrID='" & frmMain.lblCode.Text & "'")
        If p = "" Then
            'ComboBox1.SelectedIndex = 0
        Else
            AddCombo(ComboBox1, "select Position from tblPosition where BrID='" & frmMain.lblCode.Text & "'")
            ComboBox1.SelectedIndex = 0
        End If
        
        '--------------------------------------------set header font
        With DataGridView1.ColumnHeadersDefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .BackColor = Color.DarkRed
            .ForeColor = Color.Gold
            .Font = New Font("Khmer OS Battambang", 10, FontStyle.Regular, GraphicsUnit.Point)
        End With
        '---------------------------------- Set font datagridview
        Dim cs As New DataGridViewCellStyle
        cs.ForeColor = Color.Black
        cs.Font = New Font("Khmer OS Battambang", 10, FontStyle.Regular)
        DataGridView1.RowsDefaultCellStyle = cs
        '---------------------------------------------------------
        Me.txtStaffID.ReadOnly = True
        ShowStaff()
    End Sub
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                Me.txtStaffID.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value.ToString
                Me.txtName.Text = Me.DataGridView1.SelectedRows(0).Cells(2).Value
                txtMoto.Text = Me.DataGridView1.SelectedRows(0).Cells(3).Value.ToString
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        'MessageBox.Show(frmMain.lblCode.Text)
        If txtStaffID.Text = "" Or txtName.Text = "" Then
            resultError = frmMessageError.ShowBoxError("មិនអាចលុបបានទេត្រូវជ្រើសរើសបុគ្គលិកជាមុនសិន", "លុបទិន្នន័យ")
        Else
            Dim a As String = getData("select StaffID from tblAssetDetail where StaffID='" & Me.txtStaffID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
            Dim b As String = getData("select StaffID from tblResource where StaffID='" & Me.txtStaffID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
            Dim c As String = getData("select StaffID from tblSang where StaffID='" & Me.txtStaffID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
            If a <> "" Or b <> "" Or c <> "" Then
                resultError = frmMessageError.ShowBoxError("បុគ្គលិកម្នាក់នេះមិនអាចលុបបានទេព្រោះនៅមានប្រតិបត្តិការណ៏ក្នុងប្រព័ន្ធ!!! សូមពិនិត្យមើលម្តងទៀត!!!")
            Else
                result = MyMessageBox.ShowBox("តើអ្នកចង់លុបបុគ្គលិកលេខ'" & Me.txtStaffID.Text & "'មែនទេ?", "លុបទិន្នន័យ")
                If result = "1" Then
                    addIn("delete from tblStaff where StaffID='" & txtStaffID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
                End If
            End If
            ShowStaff()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ShowStaff()
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmChangeStaff.MdiParent = frmMain
        frmChangeStaff.WindowState = FormWindowState.Maximized
        frmChangeStaff.Show()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmPosition.MdiParent = frmMain
        frmPosition.WindowState = FormWindowState.Maximized
        frmPosition.Show()
    End Sub

    Private Sub txtMoto_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtMoto.KeyDown
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
        End If
    End Sub
End Class