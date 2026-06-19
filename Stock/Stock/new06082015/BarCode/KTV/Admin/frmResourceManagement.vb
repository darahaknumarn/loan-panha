Imports Microsoft.Office.Interop

Public Class frmResourceManagement
    Private Sub BtnColleteral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnColleteral.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        FrmCollateral.MdiParent = frmMain
        FrmCollateral.WindowState = FormWindowState.Maximized
        FrmCollateral.Show()
    End Sub

    Private Sub BtnStaff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStaff.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        FrmStaff.MdiParent = frmMain
        FrmStaff.WindowState = FormWindowState.Maximized
        FrmStaff.Show()
    End Sub

    Private Sub BtnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCustomer.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        FrmCustomer.MdiParent = frmMain
        FrmCustomer.WindowState = FormWindowState.Maximized
        FrmCustomer.Show()
    End Sub

    Private Sub BtnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReport.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmCustomerReport.MdiParent = frmMain
        frmCustomerReport.WindowState = FormWindowState.Maximized
        frmCustomerReport.Show()
    End Sub

    Sub addresource()
        Dim sql As String
        Dim id As Integer
        Dim a As String = getData("select top 1 id from tblResource where BrID='" & frmMain.lblCode.Text & "' order by id desc")
        If a = "" Then
            id = 1
        Else
            id = Convert.ToDecimal(a) + 1
        End If
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d7", SqlDbType.Int).Value = id
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtStaffid.Text
                .Add("@d1", SqlDbType.Int).Value = Me.txtCollateralID.Text
                .Add("@d2", SqlDbType.Int).Value = Me.txtCustomerID.Text
                .Add("@d3", SqlDbType.Int).Value = 0
                .Add("@d4", SqlDbType.Date).Value = FormatDateTime(Me.DateBorrow.Value)
                'FormatDateTime(Me.DateBorrow.Value)
                .Add("@d5", SqlDbType.Date).Value = FormatDateTime(Me.DateReturn.Value)
                'FormatDateTime(Me.DateReturn.Value)
                .Add("@d6", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d8", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d9", SqlDbType.DateTime).Value = DateTime.Now()
            End With
            '------------------------------------- 
            sql = "insert into tblresource(staffid,collateralid,customid,checking,borrowdate,returndate,BrID,id,User_Create,Date_Create) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
            txtCollateralID.Text = ""
            txtCollateralName.Text = ""
            txtStaffid.Text = ""
            txtStaffName.Text = ""
            txtCustomerID.Text = ""
            txtStaffid.Focus()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub

    'Sub addTraceResouce()
    '    Dim sql As String
    '    Try
    '        Dim com As New SqlClient.SqlCommand
    '        Dim con As New SqlClient.SqlConnection
    '        con.ConnectionString = connectionString1
    '        con.Open()
    '        com.Connection = con
    '        With com.Parameters
    '            .Add("@d0", SqlDbType.Int).Value = getData("select id from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d1", SqlDbType.NVarChar).Value = getData("select staffid from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d2", SqlDbType.NVarChar).Value = getData("select collateralid from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d3", SqlDbType.Int).Value = getData("select customid from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d4", SqlDbType.VarChar).Value = getData("select BrID from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d5", SqlDbType.NVarChar).Value = getData("select User_Create from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d6", SqlDbType.NVarChar).Value = frmMain.users
    '            Dim a As String = getData("select Date_Create from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            If a = "" Then
    '                .Add("@d7", SqlDbType.DateTime).Value = DateTime.Now()
    '            Else
    '                .Add("@d7", SqlDbType.DateTime).Value = getData("select Date_Create from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            End If
    '            .Add("@d8", SqlDbType.DateTime).Value = DateTime.Now
    '            .Add("@d9", SqlDbType.NVarChar).Value = "Old"
    '            .Add("@d10", SqlDbType.NVarChar).Value = getData("select borrowdate from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d11", SqlDbType.NVarChar).Value = getData("select returndate from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d12", SqlDbType.NVarChar).Value = getData("select checking from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")

    '        End With
    '        sql = "insert into tblTraceResource (staffid,collateralid,customid,BrID,User_Create,User_Modify,Date_Create,Date_Modify,Status,borrowdate,returndate,checking) values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)"
    '        com.CommandText = sql
    '        com.ExecuteNonQuery()
    '        com.Parameters.Clear()
    '        com.Dispose()
    '        con.Close()
    '        con.Dispose()
    '    Catch ex As Exception
    '        MessageBox.Show(Err.Description, "IT Solution")
    '    End Try
    'End Sub
    'Sub addTraceResouce1()
    '    Dim sql As String
    '    Try
    '        Dim com As New SqlClient.SqlCommand
    '        Dim con As New SqlClient.SqlConnection
    '        con.ConnectionString = connectionString1
    '        con.Open()
    '        com.Connection = con
    '        With com.Parameters
    '            .Add("@d0", SqlDbType.Int).Value = getData("select id from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d1", SqlDbType.NVarChar).Value = getData("select staffid from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d2", SqlDbType.NVarChar).Value = getData("select collateralid from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d3", SqlDbType.Int).Value = getData("select customid from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d4", SqlDbType.VarChar).Value = getData("select BrID from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d5", SqlDbType.NVarChar).Value = getData("select User_Create from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d6", SqlDbType.NVarChar).Value = frmMain.users
    '            Dim a As String = getData("select Date_Create from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            If a = "" Then
    '                .Add("@d7", SqlDbType.DateTime).Value = DateTime.Now()
    '            Else
    '                .Add("@d7", SqlDbType.DateTime).Value = getData("select Date_Create from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            End If
    '            .Add("@d8", SqlDbType.DateTime).Value = DateTime.Now
    '            .Add("@d9", SqlDbType.NVarChar).Value = "New"
    '            .Add("@d10", SqlDbType.NVarChar).Value = getData("select borrowdate from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d11", SqlDbType.NVarChar).Value = getData("select returndate from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d12", SqlDbType.NVarChar).Value = getData("select checking from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")

    '        End With
    '        sql = "insert into tblTraceResource (staffid,collateralid,customid,BrID,User_Create,User_Modify,Date_Create,Date_Modify,Status,borrowdate,returndate,checking) values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)"
    '        com.CommandText = sql
    '        com.ExecuteNonQuery()
    '        com.Parameters.Clear()
    '        com.Dispose()
    '        con.Close()
    '        con.Dispose()
    '    Catch ex As Exception
    '        MessageBox.Show(Err.Description, "IT Solution")
    '    End Try
    'End Sub

    'Sub addTraceResouce2()
    '    Dim sql As String
    '    Try
    '        Dim com As New SqlClient.SqlCommand
    '        Dim con As New SqlClient.SqlConnection
    '        con.ConnectionString = connectionString1
    '        con.Open()
    '        com.Connection = con
    '        With com.Parameters
    '            .Add("@d0", SqlDbType.Int).Value = getData("select id from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d1", SqlDbType.NVarChar).Value = getData("select staffid from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d2", SqlDbType.NVarChar).Value = getData("select collateralid from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d3", SqlDbType.Int).Value = getData("select customid from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d4", SqlDbType.VarChar).Value = getData("select BrID from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d5", SqlDbType.NVarChar).Value = getData("select User_Create from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d6", SqlDbType.NVarChar).Value = frmMain.users
    '            Dim a As String = getData("select Date_Create from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            If a = "" Then
    '                .Add("@d7", SqlDbType.DateTime).Value = DateTime.Now()
    '            Else
    '                .Add("@d7", SqlDbType.DateTime).Value = getData("select Date_Create from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            End If
    '            .Add("@d8", SqlDbType.DateTime).Value = DateTime.Now
    '            .Add("@d9", SqlDbType.NVarChar).Value = "Deleted"
    '            .Add("@d10", SqlDbType.NVarChar).Value = getData("select borrowdate from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d11", SqlDbType.NVarChar).Value = getData("select returndate from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d12", SqlDbType.NVarChar).Value = getData("select checking from tblResource where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
    '            .Add("@d13", SqlDbType.NVarChar).Value = frmMain.users
    '            .Add("@d14", SqlDbType.DateTime).Value = DateTime.Now

    '        End With
    '        sql = "insert into tblTraceResource (staffid,collateralid,customid,BrID,User_Create,User_Modify,Date_Create,Date_Modify,Status,borrowdate,returndate,checking,User_Delete,Date_Delete) values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14)"
    '        com.CommandText = sql
    '        com.ExecuteNonQuery()
    '        com.Parameters.Clear()
    '        com.Dispose()
    '        con.Close()
    '        con.Dispose()
    '    Catch ex As Exception
    '        MessageBox.Show(Err.Description, "IT Solution")
    '    End Try
    'End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "ខ្ចី" Then
            BtnNew.Text = "រក្សាទុកថ្មី"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
            Me.DateBorrow.Value = Now()
            Me.DateReturn.Value = Now()
            Me.txtCustomerAddress.Text = ""
            Me.txtCollateralID.Text = ""
            Me.txtCustomerID.Text = ""
        ElseIf BtnNew.Text = "រក្សាទុកថ្មី" Then
            BtnNew.Text = "ខ្ចី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            If txtStaffName.Text = "" Or txtCollateralName.Text = "" Or txtCustomerName.Text = "" Then
                resultError = frmMessageError.ShowBoxError("សូមបញ្ចូលទិន្នន័យអោយបានគ្រប់គ្រាន់។", "ទិន្នន័យមិនគ្រប់គ្រន់")
            Else
                Dim a As String = getData("exec sp_CheckPro '" & txtCollateralID.Text & "','" & txtCustomerID.Text & "','" & frmMain.lblCode.Text & "'")
                If a = "" Then
                    a = "0"
                End If

                If a = 0 Then
                    resultError = ""
                    resultError = frmMessageError.ShowBoxError("អតិថិជនម្នាក់នេះគ្មានទ្រព្យតម្កល់ '" & txtCollateralName.Text & "' ឡើយ។ សូមពិនិត្យមើលម្តងទៀត!!!", "គ្មានទ្រព្យតម្កល់")
                    Return
                Else
                    Dim b As String = getData("select count(customid)customid from tblResource where customid='" & txtCustomerID.Text & "' and collateralid='" & txtCollateralID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0")

                    If b = "" Then

                        Dim result As String = MyMessageBox.ShowBox("តើអ្នកចង់រក្សាទុកទិន្ន័យមែនទេ?", "រក្សាទុកទិន្ន័យ")
                        If result.Equals("1") Then
                            addresource()
                            showActive()
                        End If
                    Else
                        If Val(b) < a Then
                            Dim result As String = MyMessageBox.ShowBox("តើអ្នកចង់រក្សាទុកទិន្ន័យមែនទេ?", "រក្សាទុកទិន្ន័យ")
                            If result.Equals("1") Then
                                addresource()
                                showActive()
                            End If
                        Else
                            resultError = frmMessageError.ShowBoxError("ទ្រព្យធានា '" & txtCollateralName.Text & "' បានខ្ចីរួចហើយ។ សងជាមុនសិន ទើបអាចខ្ចីម្តងទៀតបាន។", "ទ្រព្យបានខ្ចីហើយ")
                            Return
                        End If
                    End If
                End If
            End If
        Else
            updateResource()
            BtnNew.Text = "ខ្ចី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            showActive()
        End If
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "បោះបង់" Then
            txtCollateralID.ReadOnly = False
            txtCustomerID.ReadOnly = False
            BtnNew.Text = "ខ្ចី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            Me.Close()
        End If
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Me.txtCollateralID.ReadOnly = True
        Me.txtCustomerID.ReadOnly = True
        If BtnEdit.Text = "កែប្រែ" Then
            BtnEdit.Enabled = False
            BtnNew.Text = "រក្សាទុក"
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
        End If
    End Sub

    Private Sub frmResourceManagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmMain.users = "sh" Then
            txtCustomerID.ReadOnly = True
            BtnDelete.Enabled = False
            BtnNew.Enabled = False
            BtnReturn.Enabled = False
        End If
        addIn("update tblresource set returndate=getdate() where checking =0")
        BtnNew.Focus()
        CboSearch.SelectedIndex = 0
        '--------------------------------------------set header font
        With DataGridView1.ColumnHeadersDefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .BackColor = Color.DarkRed
            .ForeColor = Color.Gold
            .Font = New Font("Khmer OS Battambang", 11, FontStyle.Regular, GraphicsUnit.Point)
        End With
        '---------------------------------- Set font datagridview
        Dim cs As New DataGridViewCellStyle
        cs.ForeColor = Color.Black
        cs.Font = New Font("Khmer OS Battambang", 11, FontStyle.Regular)
        DataGridView1.RowsDefaultCellStyle = cs
        '---------------------------------------------------------
        DateBorrow.Value = DateTime.Now
        DateReturn.Value = DateTime.Now.AddDays(3)
        showActive()
    End Sub

    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                txtCustomerAddress.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value.ToString
                Me.txtStaffName.Text = Me.DataGridView1.SelectedRows(0).Cells(2).Value
                Me.txtCollateralID.Text = Me.DataGridView1.SelectedRows(0).Cells(3).Value
                Me.txtCollateralName.Text = Me.DataGridView1.SelectedRows(0).Cells(4).Value
                Me.txtCustomerID.Text = Me.DataGridView1.SelectedRows(0).Cells(5).Value
                Me.txtCustomerName.Text = Me.DataGridView1.SelectedRows(0).Cells(6).Value
                Me.txtStaffid.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value
                Me.DateBorrow.Value = Me.DataGridView1.SelectedRows(0).Cells(8).Value
                Me.DateReturn.Value = Me.DataGridView1.SelectedRows(0).Cells(9).Value
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub

    Private Sub txtCollateralID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCollateralID.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCollateralName.Text = "" Then
                Return
            Else
                txtCustomerID.Focus()
            End If
        End If
    End Sub

    Private Sub txtCollateralID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCollateralID.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtCollateralID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCollateralID.TextChanged
        Dim a As String = getData("select collateralname from tblcollateral where  id='" & txtCollateralID.Text & "' ")
        txtCollateralName.Text = a
    End Sub

    Private Sub txtCustomerID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCustomerID.KeyDown
        Dim brid As String = frmMain.lblCode.Text
        If e.KeyCode = Keys.Enter Then
            If txtStaffName.Text = "" Or txtStaffid.Text = "" Or txtCollateralID.Text = "" Or txtCollateralName.Text = "" Or txtCustomerAddress.Text = "" Or txtCustomerID.Text = "" Then
                Return
            Else
                Dim a As String = getData("exec sp_CheckPro '" & txtCollateralID.Text & "','" & txtCustomerID.Text & "','" & brid & "'")
                If a = "" Then
                    a = "0"
                End If

                If a = 0 Then
                    resultError = ""
                    resultError = frmMessageError.ShowBoxError("អតិថិជនម្នាក់នេះគ្មានទ្រព្យតម្កល់ '" & txtCollateralName.Text & "' ឡើយ។ សូមពិនិត្យមើលម្តងទៀត!!!", "គ្មានទ្រព្យតម្កល់")
                    Return
                Else
                    Dim b As String = getData("select count(customid)customid from tblResource where customid='" & txtCustomerID.Text & "' and collateralid='" & txtCollateralID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0")

                    If b = "" Then
                        Dim result As String = MyMessageBox.ShowBox("តើអ្នកចង់រក្សាទុកទិន្ន័យមែនទេ?", "រក្សាទុកទិន្ន័យ")
                        If result.Equals("1") Then
                            addresource()
                            showActive()
                        End If
                    Else
                        If Val(b) < a Then
                            Dim result As String = MyMessageBox.ShowBox("តើអ្នកចង់រក្សាទុកទិន្ន័យមែនទេ?", "រក្សាទុកទិន្ន័យ")
                            If result.Equals("1") Then
                                addresource()
                                showActive()
                            End If
                        Else
                            resultError = frmMessageError.ShowBoxError("ទ្រព្យធានា '" & txtCollateralName.Text & "' បានខ្ចីរួចហើយ។ សងជាមុនសិន ទើបអាចខ្ចីម្តងទៀតបាន។", "ទ្រព្យបានខ្ចីហើយ")
                            Return
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub txtCustomerID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCustomerID.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtCustomerID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomerID.TextChanged
        txtCustomerName.Text = getData(" select CM_Name from BK_Customer where CM_ID='" & txtCustomerID.Text & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
        txtCustomerAddress.Text = getData("select b.VL_ID+','+b.CN_ID from BK_Customer a inner join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrId where a.CM_BrId='" & frmMain.lblCode.Text & "' and a.CM_ID='" & txtCustomerID.Text & "'")
    End Sub

    Private Sub BtnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReturn.Click
        If Me.txtCustomerID.Text = "" Or Me.txtCollateralID.Text = "" Or Me.txtCustomerAddress.Text = "" Then
            resultError = ""
            resultError = frmMessageError.ShowBoxError("មិនអាចសងវិញបានទេ ដោយសារមិនមានទិន្នន័យគ្រប់គ្រាន់", "គ្មានទិន្នន័យ")
        Else
            result = ""
            result = MyMessageBox.ShowBox("ចង់សងទ្រព្យតម្កល់ '" & Me.txtCollateralName.Text & "' អតិថិជនឈ្មោះ '" & Me.txtCustomerName.Text & "' មែនទេ?", "សងទ្រព្យតម្កល់")
            If result = "1" Then
                addIn("update tblresource set returndate='" & FormatDateTime(Me.DateReturn.Value, DateFormat.ShortDate) & "',checking=1 where id=" & Me.DataGridView1.SelectedRows(0).Cells(0).Value)
                showActive()
            End If
        End If
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If txtCollateralID.Text = "" Or txtCollateralName.Text = "" Or txtCustomerAddress.Text = "" Or txtCustomerID.Text = "" Or txtCustomerName.Text = "" Or txtStaffid.Text = "" Or txtStaffName.Text = "" Then
            resultError = frmMessageError.ShowBoxError("ជ្រើសរើសទិន្នន័យជាមុនសិនមុននឹងធ្វើការលុប", "លុបទិន្នន័យ")
        Else
            result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទុកទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
            'addTraceResouce2()
            If result = "1" Then
                addIn("delete from tblresource where id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                showActive()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        showActive()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        excelexportNormal1(DataGridView1, frmMain.strPath & "\simple Excel\Resource.xlsx", 6, "M")
    End Sub

    Private Sub excelexportNormal1(ByVal DataGridView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer, ByVal endcolumnBorder As String)
        Dim rowsTotal, colsTotal As Short
        Dim I, j As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        ''-----------------------------------------------------------------------------
        Try
            '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(SampleLocation, False, True)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
            xlApp.Visible = True
            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                .Cells(2, 1).value = frmMain.lblName.Text
                If All.Checked Then
                    .Cells(3, 1).value = "របាយការណ៏អ្នកខ្ចីទ្រព្យតម្កល់ទាំងអស់"
                ElseIf NotReturn.Checked Then
                    .Cells(3, 1).value = "របាយការណ៍អតិថិជនខ្ចីទ្រព្យតម្កល់មិនទាន់សង"
                ElseIf Returned.Checked Then
                    .Cells(3, 1).value = "របាយការណ៍អ្នកដែលបានសងទ្រព្យតម្កល់"
                ElseIf radThen15.Checked Then
                    .Cells(3, 1).value = "របាយការណ៍អ្នកខ្ចីទ្រព្យតម្កល់លើស " & txtDay.Text & " ថ្ងៃ"
                End If

                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                    Next (j)
                Next I
                Dim selection As Excel.Range
                selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & I + startRow)
                selection.Borders.Weight = Excel.XlBorderWeight.xlThin
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub

    Private Sub txtStaffid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStaffid.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtStaffName.Text = "" Then
                Return
            Else
                txtCollateralID.Focus()
            End If
        End If
    End Sub

    Private Sub txtStaffid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStaffid.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtStaffid_TextChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStaffid.TextChanged
        Me.txtStaffName.Text = getData("select staffname from tblstaff where BrID='" & frmMain.lblCode.Text & "' and staffid='" & Me.txtStaffid.Text & "'")
    End Sub

    Private Sub txtInsert_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInsert.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtDay_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDay.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtInsert_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsert.TextChanged
        Try
            If CboSearch.Text = "" Then
                Return
            ElseIf CboSearch.Text = "កូដទ្រព្យ" Then
                If txtInsert.Text = "all" Or txtInsert.Text = "All" Then
                    If All.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','All','500','0','0'")
                    ElseIf Returned.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','" & Me.txtInsert.Text & "','All','500','1','0'")
                    ElseIf NotReturn.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','" & Me.txtInsert.Text & "','All','All','0','0'")
                    End If
                Else
                    If All.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','All','500','0','0'")
                    ElseIf Returned.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','" & Me.txtInsert.Text & "','All','500','1','0'")
                    ElseIf NotReturn.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','" & Me.txtInsert.Text & "','All','All','0','0'")
                    End If
                End If
            ElseIf CboSearch.Text = "កូដអតិថិជន" Then
                If txtInsert.Text = "all" Or txtInsert.Text = "All" Then
                    If All.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','All','500','0','0'")
                    ElseIf Returned.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','" & Me.txtInsert.Text & "','500','1','0'")
                    ElseIf NotReturn.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','" & Me.txtInsert.Text & "','All','0','0'")
                    End If
                Else
                    If All.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','All','500','0','0'")
                    ElseIf Returned.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','" & Me.txtInsert.Text & "','500','1','0'")
                    ElseIf NotReturn.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','" & Me.txtInsert.Text & "','All','0','0'")
                    End If
                End If
            ElseIf CboSearch.Text = "កូដបុគ្គលិក" Then
                If txtInsert.Text = "all" Or txtInsert.Text = "All" Then
                    If All.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','All','500','0','0'")
                    ElseIf Returned.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','" & Me.txtInsert.Text & "','All','All','500','1','0'")
                    ElseIf NotReturn.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','" & Me.txtInsert.Text & "','All','All','All','0','0'")
                    End If
                Else
                    If All.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','All','500','0','0'")
                    ElseIf Returned.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','" & Me.txtInsert.Text & "','All','All','500','1','0'")
                    ElseIf NotReturn.Checked Then
                        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','" & Me.txtInsert.Text & "','All','All','All','0','0'")
                    End If
                End If
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub

    Public Sub showActive()
        AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','All','All','50','0','0'")
    End Sub

    Sub updateResource()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtStaffid.Text
                .Add("@d2", SqlDbType.Int).Value = Me.txtCustomerID.Text
                .Add("@d4", SqlDbType.Date).Value = FormatDateTime(Me.DateBorrow.Value, DateFormat.ShortDate)
            End With
            'addTraceResouce()
            sql = "Update tblresource set staffid=@d0,borrowdate=@d4 where id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
            txtCollateralID.Text = ""
            txtCollateralName.Text = ""
            txtStaffid.Text = ""
            txtStaffName.Text = ""
            txtCustomerID.Text = ""
            txtStaffid.Focus()
            'addTraceResouce1()
            resultError = ""
            resultError = frmMessageError.ShowBoxError("ទិន្នន័យបានធ្វើការកែប្រែរួចរាល់", "កែប្រែទិន្នន័យ")

        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub txtDay_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDay.TextChanged
        Try
            AddToGrid(DataGridView1, 13, "exec sp_VResource '" & frmMain.lblCode.Text & "','All','" & Me.txtInsert.Text & "','All','All','0','" & Me.txtDay.Text & "'")
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
End Class