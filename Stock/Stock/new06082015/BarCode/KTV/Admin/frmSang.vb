Public Class frmSang
    Dim newDate As Date = DateTime.Now.AddMonths(-2)
    Private Sub frmSang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmMain.users = "sh" Then
            BtnDelete.Enabled = False
            BtnEdit.Enabled = False
            BtnNew.Enabled = False
            Change.Enabled = False
            Button7.Enabled = False
            txtStaffID.ReadOnly = True
            txtAmount.ReadOnly = True
            txtPrice.ReadOnly = True
        End If
        If frmMain.users = "admin" Then
            txtKmOut.ReadOnly = False
            txtKmIn.Enabled = True
        End If
        radDate.Checked = True
        txtStaffID.Focus()
        txtKm.Text = "0 Km"
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        '-----------------
        txtPrice.Text = GetSetting("CamITSo", "textBox", "Integer".Trim)
        '----------------------------
        With DataGridView1.ColumnHeadersDefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .BackColor = Color.DarkRed
            .ForeColor = Color.Gold
            .Font = New Font("Khmer OS Battambang", 9, FontStyle.Regular, GraphicsUnit.Point)
        End With
        '---------------------------------- Set font datagridview
        Dim cs As New DataGridViewCellStyle
        cs.ForeColor = Color.Black
        cs.Font = New Font("Khmer OS Battambang", 9, FontStyle.Regular)
        DataGridView1.RowsDefaultCellStyle = cs
        ShowSang()
    End Sub
    Sub ShowSang()
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 14
        DataGridView1.Columns(0).Name = "No"
        DataGridView1.Columns(1).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(2).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(3).Name = "តួនាទី"
        DataGridView1.Columns(4).Name = "ស្លាកលេខម៉ូតូ"
        DataGridView1.Columns(5).Name = "ចំនួនលីត្រ"
        DataGridView1.Columns(6).Name = "តំលៃរាយរៀល"
        DataGridView1.Columns(7).Name = "តំលៃសរុបរៀល"
        DataGridView1.Columns(8).Name = "តំលៃរាយដុល្លា"
        DataGridView1.Columns(9).Name = "តំលៃសរុបដុល្លា"
        DataGridView1.Columns(10).Name = "គីឡូចេញ"
        DataGridView1.Columns(11).Name = "គីឡូចូល"
        DataGridView1.Columns(12).Name = "គីឡូសរុប"
        DataGridView1.Columns(13).Name = "កាលបរិច្ឆេត"
        AddToGrid(DataGridView1, 14, "select top 50 a.id,b.staffid,b.StaffName ,a.Position,a.No,amount,isnull(unitprice,0) PriceKH,isnull(amount*unitPrice,0) TotalKH,isnull(PriceUSD,0)PriceUSD,isnull(PriceUSD*Amount,0) TotalUSD,a.KmOut,a.KmIn,a.Km,date from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where  b.BrID='" & frmMain.lblCode.Text & "' order by a.id desc")
    End Sub
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "បញ្ចូលថ្មី" Then
            BtnNew.Text = "រក្សាទុកថ្មី"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
            txtPrice.ReadOnly = False
        ElseIf BtnNew.Text = "រក្សាទុកថ្មី" Then
            BtnNew.Text = "បញ្ចូលថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            addSang()
            ShowSang()
        Else
            BtnNew.Text = "បញ្ចូលថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            updateSang()
            ShowSang()
        End If
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If BtnEdit.Text = "កែប្រែ" Then
                If txtStaffID.Text = "" Or txtStaffName.Text = "" Then
                    resultError = frmMessageError.ShowBoxError("សូមធ្វើការជ្រើសរើសទិន្នន័យជាមុនសិនមុននិងធ្វើកែប្រែ", "កែប្រែទិន្នន័យ")
                    Return
                Else
                    BtnNew.Text = "រក្សាទុក"
                    BtnDelete.Enabled = False
                    BtnEdit.Enabled = False
                    BtnExit.Text = "បោះបង់"
                End If
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click

        If txtStaffID.Text = "" Or Me.DataGridView1.SelectedRows.Count = 0 Or txtStaffName.Text = "" Or txtAmount.Text = "" Then
            resultError = frmMessageError.ShowBoxError("គ្មានទិន្នន័យត្រូវលុប", "លុបទិន្នន័យ")
        Else
            result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
            addTracesang2()
            If result = "1" Then
                addIn("delete from tblsang where id=" & Me.DataGridView1.SelectedRows(0).Cells(0).Value)
                resultError = frmMessageError.ShowBoxError("លុបបានជោគជ័យ", "លុបទិន្នន័យ")
                ShowSang()
                Return
            Else
            End If
        End If
    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "បោះបង់" Then
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            Me.Close()
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ToExcel(DataGridView1)
    End Sub
    Sub addSang()
        Dim Price As Integer = txtPrice.Text
        Dim id As Integer
        Dim a As String = getData("select top 1 id from tblSang where BrID='" & frmMain.lblCode.Text & "' order by id desc")
        If a = "" Then
            id = 1
        Else
            id = Convert.ToDecimal(a) + 1
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
                .Add("@d1", SqlDbType.Int).Value = Me.txtAmount.Text
                .Add("@d2", SqlDbType.Float).Value = txtPrice.Text
                .Add("@d3", SqlDbType.Date).Value = Me.DateTimePicker1.Value
                .Add("@d4", SqlDbType.Int).Value = txtKmIn.Text - txtKmOut.Text
                .Add("@d5", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d6", SqlDbType.Int).Value = txtKmOut.Text
                .Add("@d7", SqlDbType.Int).Value = txtKmIn.Text
                .Add("@d8", SqlDbType.Int).Value = id
                If CheckBox1.Checked = True Then
                    If ComboBox1.Text = "" Then
                        resultError = frmMessageError.ShowBoxError("គ្មានស្លាកលេខម៉ូតូ", "ជ្រើសរើស")
                    Else
                        .Add("@d9", SqlDbType.NVarChar).Value = ComboBox1.Text
                    End If
                Else
                    .Add("@d9", SqlDbType.NVarChar).Value = cboNo.Text
                End If

                .Add("@d10", SqlDbType.NVarChar).Value = txtPosition.Text
                .Add("@d11", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d12", SqlDbType.DateTime).Value = DateTime.Now()

            End With
            If Price > 100 Then
                sql = "insert into tblSang(StaffID ,Amount,unitPrice ,Date,Km,BrID,KmOut,KmIn,id,No,Position,PriceUSD,User_Create,Date_Create) values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,0,@d11,@d12)"
            Else
                sql = "insert into tblSang(StaffID ,Amount,unitPrice ,Date,Km,BrID,KmOut,KmIn,id,No,Position,PriceUSD,User_Create,Date_Create) values(@d0,@d1,0,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d2,@d11,@d12)"
            End If
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
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        FrmStaff.MdiParent = frmMain
        FrmStaff.WindowState = FormWindowState.Maximized
        FrmStaff.Show()
    End Sub
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                Me.txtStaffID.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value.ToString
                Me.txtStaffName.Text = Me.DataGridView1.SelectedRows(0).Cells(2).Value
                txtPosition.Text = Me.DataGridView1.SelectedRows(0).Cells(3).Value.ToString
                cboNo.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString
                txtAmount.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString
                DateTimePicker1.Value = DataGridView1.SelectedRows(0).Cells(13).Value
                txtKm.Text = DataGridView1.SelectedRows(0).Cells(12).Value.ToString & " Km"
                txtKmOut.Text = DataGridView1.SelectedRows(0).Cells(10).Value.ToString
                txtKmIn.Text = DataGridView1.SelectedRows(0).Cells(11).Value.ToString
                If DataGridView1.SelectedRows(0).Cells(8).Value.ToString = 0 Then
                    txtPrice.Text = DataGridView1.SelectedRows(0).Cells(6).Value.ToString
                Else
                    txtPrice.Text = DataGridView1.SelectedRows(0).Cells(8).Value.ToString
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Sub updateSang()
        addTracesang()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = txtStaffID.Text
                .Add("@d2", SqlDbType.Int).Value = txtAmount.Text
                .Add("@d3", SqlDbType.Float).Value = txtPrice.Text
                .Add("@d4", SqlDbType.Date).Value = DateTimePicker1.Value
                .Add("@d5", SqlDbType.Int).Value = txtKmIn.Text - txtKmOut.Text
                .Add("@d6", SqlDbType.Int).Value = txtKmOut.Text
                .Add("@d7", SqlDbType.Int).Value = txtKmIn.Text
                .Add("@d8", SqlDbType.NVarChar).Value = txtPosition.Text
            End With
            addIn("update tblSang set unitPrice=0,PriceUSD=0 where id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "' and BrID='" & frmMain.lblCode.Text & "'")
            If txtPrice.Text > 100 Then
                sql = "update tblSang set StaffID=@d1,Amount =@d2,unitPrice =@d3,date=@d4,Km=@d5,KmOut=@d6,KmIn=@d7,Position=@d8 where id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "' and BrID='" & frmMain.lblCode.Text & "'"
            Else
                sql = "update tblSang set StaffID=@d1,Amount =@d2,PriceUSD =@d3,date=@d4,Km=@d5,KmOut=@d6,KmIn=@d7,Position=@d8 where id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "' and BrID='" & frmMain.lblCode.Text & "'"
            End If
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
            addTraceSang1()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Sub addTracesang()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = getData("select id from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d1", SqlDbType.NVarChar).Value = getData("select staffid from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d2", SqlDbType.Int).Value = getData("select Amount from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d3", SqlDbType.Int).Value = getData("select unitPrice from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d4", SqlDbType.Date).Value = getData("select Date from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d5", SqlDbType.Int).Value = getData("select Km from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d6", SqlDbType.NVarChar).Value = getData("select BrID from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d7", SqlDbType.Int).Value = getData("select KmIn from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d8", SqlDbType.Int).Value = getData("select KmOut from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d9", SqlDbType.NVarChar).Value = getData("select No from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d10", SqlDbType.Float).Value = getData("select PriceUSD from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d11", SqlDbType.NChar).Value = getData("select Position from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d12", SqlDbType.NVarChar).Value = getData("select User_Create from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d13", SqlDbType.NVarChar).Value = frmMain.users
                Dim a As String = getData("select Date_Create from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                If a = "" Then
                    .Add("@d14", SqlDbType.DateTime).Value = DateTime.Now()
                Else
                    .Add("@d14", SqlDbType.DateTime).Value = getData("select Date_Create from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                End If
                .Add("@d15", SqlDbType.DateTime).Value = DateTime.Now()
                .Add("@d16", SqlDbType.NVarChar).Value = "Old"
            End With
            sql = "insert into tblTracesang(StaffID,Amount,unitPrice,Date,Km,BrID,KmIn,KmOut,No,PriceUSD,Position,User_Create,User_Modify,Date_Create,Date_Modify,Status) values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Sub addTracesang1()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = getData("select id from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d1", SqlDbType.NVarChar).Value = getData("select staffid from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d2", SqlDbType.Int).Value = getData("select Amount from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d3", SqlDbType.Int).Value = getData("select unitPrice from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d4", SqlDbType.Date).Value = getData("select Date from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d5", SqlDbType.Int).Value = getData("select Km from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d6", SqlDbType.NVarChar).Value = getData("select BrID from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d7", SqlDbType.Int).Value = getData("select KmIn from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d8", SqlDbType.Int).Value = getData("select KmOut from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d9", SqlDbType.NVarChar).Value = getData("select No from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d10", SqlDbType.Float).Value = getData("select PriceUSD from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d11", SqlDbType.NChar).Value = getData("select Position from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d12", SqlDbType.NVarChar).Value = getData("select User_Create from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d13", SqlDbType.NVarChar).Value = frmMain.users
                Dim a As String = getData("select Date_Create from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                If a = "" Then
                    .Add("@d14", SqlDbType.DateTime).Value = DateTime.Now()
                Else
                    .Add("@d14", SqlDbType.DateTime).Value = getData("select Date_Create from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                End If
                .Add("@d15", SqlDbType.DateTime).Value = DateTime.Now()
                .Add("@d16", SqlDbType.NVarChar).Value = "New"
            End With
            sql = "insert into tblTracesang(StaffID,Amount,unitPrice,Date,Km,BrID,KmIn,KmOut,No,PriceUSD,Position,User_Create,User_Modify,Date_Create,Date_Modify,Status) values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Sub addTracesang2()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = getData("select id from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d1", SqlDbType.NVarChar).Value = getData("select staffid from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d2", SqlDbType.Int).Value = getData("select Amount from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d3", SqlDbType.Int).Value = getData("select unitPrice from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d4", SqlDbType.Date).Value = getData("select Date from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d5", SqlDbType.Int).Value = getData("select Km from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d6", SqlDbType.NVarChar).Value = getData("select BrID from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d7", SqlDbType.Int).Value = getData("select KmIn from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d8", SqlDbType.Int).Value = getData("select KmOut from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d9", SqlDbType.NVarChar).Value = getData("select No from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d10", SqlDbType.Float).Value = getData("select PriceUSD from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d11", SqlDbType.NChar).Value = getData("select Position from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d12", SqlDbType.NVarChar).Value = getData("select User_Create from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d13", SqlDbType.NVarChar).Value = frmMain.users
                Dim a As String = getData("select Date_Create from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                If a = "" Then
                    .Add("@d14", SqlDbType.DateTime).Value = DateTime.Now()
                Else
                    .Add("@d14", SqlDbType.DateTime).Value = getData("select Date_Create from tblSang where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                End If
                .Add("@d15", SqlDbType.DateTime).Value = DateTime.Now()
                .Add("@d16", SqlDbType.NVarChar).Value = "Deleted"
                .Add("@d17", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d18", SqlDbType.NVarChar).Value = DateTime.Now()
            End With
            sql = "insert into tblTracesang(StaffID,Amount,unitPrice,Date,Km,BrID,KmIn,KmOut,No,PriceUSD,Position,User_Create,User_Modify,Date_Create,Date_Modify,Status,User_Delete,Date_Delete) values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Private Sub txtStaffID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStaffID.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtStaffName.Text = "" Then
                Return
            Else
                Dim a As String = getData("select top 1 isnull(KmIn,0) from tblSang where No=N'" & cboNo.Text & "' and BrID = '" & frmMain.lblCode.Text & "' order by KmIn desc")
                If a = "0" Or a = "" Then
                    txtKmOut.Focus()
                    txtKmOut.ReadOnly = False
                Else
                    txtKmOut.Text = a
                    txtKmIn.Focus()
                    txtKmOut.ReadOnly = True
                End If
            End If
        End If
    End Sub
    Private Sub txtStaffID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtStaffID.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
        If e.KeyCode = Keys.Enter Then

            If txtAmount.Text = "" Or txtKmIn.Text = "" Or txtKmOut.Text = "" Or cboNo.Text = "" Or txtPrice.Text = "" Or txtStaffID.Text = "" Or txtStaffName.Text = "" Then
                resultError = frmMessageError.ShowBoxError("មិនអាចរក្សាទុកបានព្រោះទិន្នន័យមិនគ្រប់គ្រាន់", "រក្សាទុកទិន្នន័យ")
            Else
                result = MyMessageBox.ShowBox("តើអ្នកចង់រក្សាទុកទិន្នន័យមែនទេ?", "រក្សាទុកទិន្នន័យ")
                If result = "1" Then
                    addSang()
                    txtStaffID.Text = ""
                    txtAmount.Text = ""
                    txtKmIn.Text = ""
                    txtStaffID.Focus()
                    ShowSang()
                    Return
                End If
            End If
        End If
    End Sub
    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtPrice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPrice.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtAmount.Text = "" Or txtKmIn.Text = "" Or txtKmOut.Text = "" Or cboNo.Text = "" Or txtPrice.Text = "" Or txtStaffID.Text = "" Or txtStaffName.Text = "" Then
                resultError = frmMessageError.ShowBoxError("មិនអាចរក្សាទុកបានព្រោះទិន្នន័យមិនគ្រប់គ្រាន់", "រក្សាទុកទិន្នន័យ")
            Else
                result = MyMessageBox.ShowBox("តើអ្នកចង់រក្សាទុកទិន្នន័យមែនទេ?", "រក្សាទុកទិន្នន័យ")
                If result = "1" Then
                    addSang()
                    txtStaffID.Text = ""
                    txtAmount.Text = ""
                    txtKmIn.Text = ""
                    txtStaffID.Focus()
                    ShowSang()
                    Return

                End If
            End If
        End If
    End Sub
    Private Sub txtKmIn_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKmIn.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim a As Integer = Val(txtKmIn.Text) - Val(txtKmOut.Text)
            If a < 0 Then
                resultError = frmMessageError.ShowBoxError("ចំនួនគីឡូចូលមិនអាចតូចជាងចំនួនគីឡូចេញបានទេ។", "ខុសចំនួនគីឡូ")
                Return
            Else
                txtKm.Text = a & " Km"
                txtAmount.Focus()
            End If
        End If
    End Sub
    Private Sub txtKmIn_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKmIn.KeyPress, txtKmIn.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtStaffID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStaffID.TextChanged
        Try
            txtStaffName.Text = getData("Select StaffName from tblStaff where BrID='" & frmMain.lblCode.Text & "' and StaffID= '" & txtStaffID.Text & "'")
            Dim no1 As String = getData("select MotoNo from tblStaff where  BrID='" & frmMain.lblCode.Text & "' and StaffID ='" & txtStaffID.Text & "'")
            Dim no2 As String = getData("select MotoNo2 from tblStaff where  BrID='" & frmMain.lblCode.Text & "' and StaffID ='" & txtStaffID.Text & "'")
            Dim no3 As String = getData("select MotoNo3 from tblStaff where  BrID='" & frmMain.lblCode.Text & "' and StaffID ='" & txtStaffID.Text & "'")
            cboNo.Items.Clear()
            cboNo.Items.Add(no1)
            cboNo.Items.Add(no2)
            cboNo.Items.Add(no3)
            cboNo.SelectedIndex = 0
            txtPosition.Text = getData("select Position  from tblStaff where  BrID='" & frmMain.lblCode.Text & "' and StaffID ='" & txtStaffID.Text & "'")
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try

        Return
    End Sub
    Sub datagrid()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 14
        DataGridView1.Columns(0).Name = "No"
        DataGridView1.Columns(1).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(2).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(3).Name = "តួនាទី"
        DataGridView1.Columns(4).Name = "ស្លាកលេខម៉ូតូ"
        DataGridView1.Columns(5).Name = "ចំនួនលីត្រ"
        DataGridView1.Columns(6).Name = "តំលៃរាយរៀល"
        DataGridView1.Columns(7).Name = "តំលៃសរុបរៀល"
        DataGridView1.Columns(8).Name = "តំលៃរាយដុល្លា"
        DataGridView1.Columns(9).Name = "តំលៃសរុបដុល្លា"
        DataGridView1.Columns(10).Name = "គីឡូចេញ"
        DataGridView1.Columns(11).Name = "គីឡូចូល"
        DataGridView1.Columns(12).Name = "គីឡូសរុប"
        DataGridView1.Columns(13).Name = "កាលបរិច្ឆេត"
    End Sub
    Private Sub txtInsert_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsert.TextChanged
        Dim date1 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(DateTimePicker3.Value, DateFormat.ShortDate)
        Dim result1 As Integer = DateTime.Compare(date1, date2)
        Try
            If radDate.Checked Then
                If result1 > 0 Then
                    result = frmMessageError.ShowBoxError("អ្នកបានជ្រើសរើសកាលបរិច្ឆេតខុសហើយ សូមត្រួតពិនិត្យឡើងវិញ។", "កាលបរិច្ឆេតខុស")
                    Return
                Else
                    If CboSearch.SelectedIndex = 0 Then
                        datagrid()
                        AddToGrid(DataGridView1, 14, "select  a.id,b.staffid,b.StaffName ,a.Position,a.No,amount,isnull(unitprice,0) PriceKH,isnull(amount*unitPrice,0) TotalKH,isnull(PriceUSD,0)PriceUSD,isnull(PriceUSD*Amount,0) TotalUSD,a.KmOut,a.KmIn,a.Km,date from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where   a.Date between '" & FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateTimePicker3.Value, DateFormat.ShortDate) & "' and b.BrID='" & frmMain.lblCode.Text & "'")
                    Else
                        If txtInsert.Text <> "" Then
                            datagrid()
                            AddToGrid(DataGridView1, 14, "select  a.id,b.staffid,b.StaffName ,a.Position,a.No,amount,isnull(unitprice,0) PriceKH,isnull(amount*unitPrice,0) TotalKH,isnull(PriceUSD,0)PriceUSD,isnull(PriceUSD*Amount,0) TotalUSD,a.KmOut,a.KmIn,a.Km,date from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where   a.Date between '" & FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateTimePicker3.Value, DateFormat.ShortDate) & "' and b.BrID='" & frmMain.lblCode.Text & "' and a.StaffID='" & txtInsert.Text & "'")
                        Else
                            Return
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmrptSang.MdiParent = frmMain
        frmrptSang.WindowState = FormWindowState.Maximized
        frmrptSang.Show()
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        ShowSang()
    End Sub
    Private Sub txtKmOut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKmOut.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtKmIn.Focus()
        End If
    End Sub
    Private Sub Change_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Change.Click
        If txtPrice.Text.Trim.Length > 0 Then
            SaveSetting("CamITSo", "textBox", "Integer", txtPrice.Text)
        Else
            resultError = frmMessageError.ShowBoxError("ជ្រើសរើសជាមុនសិន", "ជ្រើសរើស")
        End If
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmUpdatePrice.MdiParent = frmMain
        frmUpdatePrice.WindowState = FormWindowState.Maximized
        frmUpdatePrice.Show()
    End Sub

    Private Sub CboSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboSearch.SelectedIndexChanged
        Dim date1 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(DateTimePicker3.Value, DateFormat.ShortDate)
        Dim result1 As Integer = DateTime.Compare(date1, date2)
        If radDate.Checked Then
            If result1 > 0 Then
                result = frmMessageError.ShowBoxError("អ្នកបានជ្រើសរើសកាលបរិច្ឆេតខុសហើយ សូមត្រួតពិនិត្យឡើងវិញ។", "កាលបរិច្ឆេតខុស")
                Return
            Else
                If CboSearch.SelectedIndex = 0 Then
                    datagrid()
                    AddToGrid(DataGridView1, 14, "select top 50 a.id,b.staffid,b.StaffName ,a.Position,a.No,amount,isnull(unitprice,0) PriceKH,isnull(amount*unitPrice,0) TotalKH,isnull(PriceUSD,0)PriceUSD,isnull(PriceUSD*Amount,0) TotalUSD,a.KmOut,a.KmIn,a.Km,date from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where   a.Date between '" & FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateTimePicker3.Value, DateFormat.ShortDate) & "' and b.BrID='" & frmMain.lblCode.Text & "'")
                Else
                    txtInsert.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub DataGridView1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                Me.txtStaffID.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value.ToString
                Me.txtStaffName.Text = Me.DataGridView1.SelectedRows(0).Cells(2).Value
                txtPosition.Text = Me.DataGridView1.SelectedRows(0).Cells(3).Value.ToString
                cboNo.Text = DataGridView1.SelectedRows(0).Cells(4).Value.ToString
                txtAmount.Text = DataGridView1.SelectedRows(0).Cells(5).Value.ToString
                DateTimePicker1.Value = DataGridView1.SelectedRows(0).Cells(13).Value
                txtKm.Text = DataGridView1.SelectedRows(0).Cells(12).Value.ToString & " Km"
                txtKmOut.Text = DataGridView1.SelectedRows(0).Cells(10).Value.ToString
                txtKmIn.Text = DataGridView1.SelectedRows(0).Cells(11).Value.ToString
                If DataGridView1.SelectedRows(0).Cells(8).Value.ToString = 0 Then
                    txtPrice.Text = DataGridView1.SelectedRows(0).Cells(6).Value.ToString
                Else
                    txtPrice.Text = DataGridView1.SelectedRows(0).Cells(8).Value.ToString
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            Dim a As String = getData("select top 1 isnull(KmIn,0) from tblSang where No=N'" & cboNo.Text & "' and BrID = '" & frmMain.lblCode.Text & "' order by id desc")
            If a = "0" Or a = "" Then
                txtKmOut.Focus()
            Else
                txtKmOut.Text = a
                txtKmIn.Focus()
            End If
        Else
            AddCombo(Me.ComboBox1, " exec a '" & frmMain.lblCode.Text & "'")
        End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If Me.CheckBox1.Checked = True Then
            Dim a As String = getData("select top 1 isnull(KmIn,0) from tblSang where No=N'" & Me.ComboBox1.Text & "' and BrID = '" & frmMain.lblCode.Text & "' order by id desc")
            If a = "0" Or a = "" Then
                txtKmOut.Focus()
            Else
                txtKmOut.Text = a
                txtKmIn.Focus()
            End If
        Else
            Dim a As String = getData("select top 1 isnull(KmIn,0) from tblSang where No=N'" & cboNo.Text & "' and BrID = '" & frmMain.lblCode.Text & "' order by id desc")
            If a = "0" Or a = "" Then
                txtKmOut.Focus()
            Else
                txtKmOut.Text = a
                txtKmIn.Focus()
            End If
            Return
        End If
    End Sub
    Private Sub cboNo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboNo.SelectedIndexChanged
        If txtStaffID.Text = "" Or txtStaffName.Text = "" Then
            Return
        Else
            Dim a As String = getData("select top 1 isnull(KmIn,0) from tblSang where No=N'" & cboNo.Text & "' and BrID = '" & frmMain.lblCode.Text & "' order by id desc")
            If a = "0" Or a = "" Then
                txtKmOut.Focus()
                txtKmOut.ReadOnly = False
            Else
                txtKmOut.Text = a
                txtKmIn.Focus()
                txtKmOut.ReadOnly = True
            End If
        End If

    End Sub
End Class