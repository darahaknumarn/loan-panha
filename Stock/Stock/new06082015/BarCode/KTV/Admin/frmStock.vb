Public Class frmStock
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        FrmStaff.MdiParent = frmMain
        FrmStaff.WindowState = FormWindowState.Maximized
        FrmStaff.Show()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmProduct.MdiParent = frmMain
        frmProduct.WindowState = FormWindowState.Maximized
        frmProduct.Show()
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ToExcel(DataGridView1)
    End Sub
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "ដក" Then
            BtnNew.Text = "រក្សាទុកថ្មី"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
            txtPID.ReadOnly = False
            txtAmount.ReadOnly = False
        ElseIf BtnNew.Text = "រក្សាទុកថ្មី" Then
            BtnNew.Text = "ដក"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            BtnNew.Text = "ដក"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        End If
    End Sub
    Sub addasset()
        '---------------------------------------
        Dim ReAmount As Integer = Me.txtAmount.Text
        Dim ID As String = getData("select top 1 isnull(ID,0) from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' order by ID desc")
        If ID = "" Then
            ID = 1
        Else
            ID = Convert.ToInt16(ID) + 1
        End If
        '-------------------------------
        Dim amount As Integer = getData("select totalAmount from tblAsset where assetID='" & txtPID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
        If amount < 1 Then
            result = frmMessageError.ShowBoxError("ចំនួនបានអស់ពីស្តុក មិនអាចធ្វើការដកបានទេ។", "អស់ពីស្តុក")
        ElseIf amount - Me.txtAmount.Text < 0 Then
            result = frmMessageError.ShowBoxError("មិនអាចធ្វើការដកលើសចំនួនក្នុងស្តុកបានទេ។", "អស់ពីស្តុក")
        Else
            Dim FirstAmount As Integer = Val(getData("select top 1 amount from tblAssetAdd where BrID='" & frmMain.lblCode.Text & "' and assetID='" & txtPID.Text & "' order by dates desc"))
            If FirstAmount - ReAmount < 0 Then
                result = frmMessageError.ShowBoxError("ចំនួនបានអស់ពីស្តុក មិនអាចធ្វើការដកបានទេ។", "អស់ពីស្តុក")
            Else
                addAssetDetail(ID, ReAmount)
                addIn("update tblAsset set totalAmount = totalAmount -" & ReAmount & " where assetID ='" & txtPID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
            End If
        End If
        showsAsset()
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If txtPID.Text = "" Or Me.DataGridView1.SelectedRows.Count = 0 Or txtStaffName.Text = "" Then
            resultError = frmMessageError.ShowBoxError("គ្មានទិន្នន័យលុប ជ្រើសរើសទិន្នន័យជាមុនសិន។", "គ្មានទិន្នន័យ")
        Else
            Dim addID = getData("select assetAddID  from tblAssetDetail where ID='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
            result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
            If result = "1" Then
                addIn("update tblAsset set totalAmount =totalAmount +'" & Val(Me.DataGridView1.SelectedRows(0).Cells(7).Value) & "' where assetID ='" & txtPID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
                addIn("delete from tblAssetDetail where ID='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                showsAsset()
                Return
            End If
        End If
    End Sub
    Sub addTraceAssetDetail()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = getData("select id from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d1", SqlDbType.NVarChar).Value = getData("select assetID from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d2", SqlDbType.NChar).Value = getData("select staffID from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d3", SqlDbType.Float).Value = getData("select amount from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d4", SqlDbType.DateTime).Value = getData("select dates from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d5", SqlDbType.Float).Value = getData("select Riel from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d6", SqlDbType.Float).Value = getData("select Dollar from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d7", SqlDbType.NChar).Value = getData("select currency from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                .Add("@d8", SqlDbType.NVarChar).Value = frmMain.users
               Dim a As String = getData("select Date_Create from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                If a = "" Then
                    .Add("@d9", SqlDbType.DateTime).Value = DateTime.Now()
                Else
                    .Add("@d9", SqlDbType.DateTime).Value = getData("select Date_Create from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
                End If
                .Add("@d10", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d11", SqlDbType.DateTime).Value = DateTime.Now()
                .Add("@d12", SqlDbType.NVarChar).Value = "Deleted"
                .Add("@d13", SqlDbType.NChar).Value = getData("select BrID from tblAssetDetail where BrID='" & frmMain.lblCode.Text & "' and id='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'")
            End With
            sql = "insert into tblTraceAssetDetail(assetID,staffID,amount,dates,Riel,Dollar,currency,User_Create,Date_Create,User_Delete,Date_Delete,Status,BrID) values (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13)"
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

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "បោះបង់" Then
            BtnNew.Text = "ដក"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            Me.Close()
        End If
    End Sub
    Sub addAssetDetail(ByVal ID, ByVal amount)
        Dim BrID As String = frmMain.lblCode.Text
        Dim sql As String
        Dim currency As String
        Dim unitprice As Double
        Dim R As Integer = getData("select top 1 Riel from tblAssetAdd where BrID='" & frmMain.lblCode.Text & "' and assetID='" & Me.txtPID.Text & "' order by ID desc")
        If R = 0 Then
            currency = "Dollar"
        Else
            currency = "Riel"
        End If
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtPID.Text
                .Add("@d1", SqlDbType.NVarChar).Value = Me.txtStaffID.Text
                .Add("@d2", SqlDbType.Float).Value = Me.txtAmount.Text
                .Add("@d3", SqlDbType.DateTime).Value = Me.DateTimePicker1.Value
                .Add("@d4", SqlDbType.Float).Value = Me.txtUnitprice.Text
                .Add("@d5", SqlDbType.NChar).Value = Me.txtType.Text
                .Add("@d8", SqlDbType.Int).Value = Val(getData("select top 1 ID from tblAssetAdd where assetID='" & txtPID.Text & "'"))
                .Add("@d10", SqlDbType.NVarChar).Value = BrID
                .Add("@d11", SqlDbType.Int).Value = ID
                .Add("@d12", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d13", SqlDbType.DateTime).Value = DateTime.Now()
            End With
            If currency = "Riel" Then
                sql = "insert into tblAssetDetail (ID,assetID ,staffID ,amount ,dates ,Riel,Dollar,currency,assetAddID,BrID,User_Create,Date_Create) values(@d11,@d0,@d1,@d2,@d3,@d4,0,@d5,@d8,@d10,@d12,@d13)"
            Else
                sql = "insert into tblAssetDetail (ID,assetID ,staffID ,amount ,dates ,Riel,Dollar,currency,assetAddID,BrID,User_Create,Date_Create) values(@d11,@d0,@d1,@d2,@d3,0,@d4,@d5,@d8,@d10,@d12,@d13)"
            End If
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
            showsAsset()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Sub showsAsset()
        SetFontDatagrid(DataGridView1)
        datagrid()
        AddToGrid(DataGridView1, 14, "select top 50 a .ID, a .staffID as 'កូដមន្ត្រីឥណទាន',c.StaffName as 'ឈ្មោះមន្ត្រីឥណទាន',c.Position, a.assetID as 'Asset ID', b.assetName as 'Asset Name',b.oumName,a.amount,a .dates as 'កាលបរិច្ឆេត' ,a .Riel , a.Riel*a.amount TotalRiel,a.Dollar,a.Dollar*a.amount TotalUSD ,a .currency as 'រូបបិយវត្ថុ' from tblAssetDetail a inner join tblAsset b  on a.assetID = b.assetID and a.BrID = b.BrID inner join tblstaff c on a.staffID = c.StaffId  and a.BrID =c.BrID where c.BrID='" & frmMain.lblCode.Text & "' order by ID desc")
    End Sub
    Private Sub frmStock_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmMain.users = "sh" Then
            BtnDelete.Enabled = False
            BtnNew.Enabled = False
            BtnEdit.Enabled = False
            txtStaffID.ReadOnly = True
            txtAmount.ReadOnly = True
        End If
        txtStaffID.Focus()
        txtPName.ReadOnly = True
        CboSearch.Text = "សូមជ្រើសរើស"
        txtStaffName.ReadOnly = True
        showsAsset()
    End Sub
    Private Sub txtPID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPID.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtPName.Text = "" Then
                Return
            Else
                txtAmount.Focus()
            End If
        End If
    End Sub
    Private Sub txtPID_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPID.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub txtPID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPID.TextChanged
        Try
            txtPName.Text = getData("Select assetName from tblAsset where assetID='" & txtPID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
            txtOum.Text = getData("Select oumName from tblAsset where BrID='" & frmMain.lblCode.Text & "' and assetID='" & txtPID.Text & "'")
            txtUnitprice.Text = getData("select top 1 unitprice  from tblAssetAdd where BrID='" & frmMain.lblCode.Text & "' and assetID ='" & txtPID.Text & "' order by dates desc")
            txtType.Text = getData("select top 1 currency  from tblAssetAdd where BrID='" & frmMain.lblCode.Text & "' and assetID ='" & txtPID.Text & "' order by dates desc")
            Dim a As String = getData("select totalAmount  from tblAsset where BrID='" & frmMain.lblCode.Text & "' and assetID ='" & txtPID.Text & "'")
            If txtPName.Text = "" Then
                lblAmount.Text = ""
            Else
                lblAmount.Text = "ក្នុងស្តុក= " & a.ToString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub txtStaffID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStaffID.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtStaffName.Text = "" Then
                Return
            Else
                txtPID.Focus()
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
    Private Sub txtStaffID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStaffID.TextChanged
        Try
            txtStaffName.Text = getData("Select StaffName from tblStaff where StaffID='" & txtStaffID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
            txtPosition.Text = getData("Select Position from tblStaff where StaffID='" & txtStaffID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        showsAsset()
    End Sub
    Private Sub txtAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
        If e.KeyCode = Keys.Enter Then
            result = ""
            result = MyMessageBox.ShowBox("តើអ្នកចង់រក្សាទុកទិន្នន័យមែនទេ?", "រក្សាទុកទិន្នន័យ")
            If txtStaffID.Text = "" Or txtStaffName.Text = "" Or txtPID.Text = "" Or txtPName.Text = "" Or txtUnitprice.Text = "" Then
                resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនគ្រប់គ្រាន់ សូមពិនិត្យឡើងវិញ។", "គ្មានទិន្នន័យ")
            Else
                If result = "1" Then
                    addasset()
                    showsAsset()
                    txtStaffID.Text = ""
                    txtStaffName.Text = ""
                    txtAmount.Text = ""
                    txtPosition.Text = ""
                    txtPID.Text = ""
                    txtPName.Text = ""
                    txtUnitprice.Text = ""
                    txtOum.Text = ""
                    txtStaffID.Focus()
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
    Private Sub txtInsert_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInsert.TextChanged
        Dim date1 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(DateTimePicker3.Value, DateFormat.ShortDate)
        Dim result1 As Integer = DateTime.Compare(date2, date1)
        Dim BrID As String = frmMain.lblCode.Text
        datagrid()
        AddToGrid(DataGridView1, 14, "select  a .ID, a .staffID as 'កូដមន្ត្រីឥណទាន',tblStaff.StaffName as 'ឈ្មោះមន្ត្រីឥណទាន',tblStaff.Position, a .assetID as 'Asset ID',b .assetName as 'Asset Name',b.oumName,a.amount,a .dates as 'កាលបរិច្ឆេត' , a .Riel ,a.Riel*a.amount TotalRiel,a .Dollar,a.Dollar*a.amount TotalUSD ,a .currency as 'រូបបិយវត្ថុ'  from tblAssetDetail a  inner join tblAsset b  on a .assetID  =b .assetID  and a.BrID = b.BrID inner join tblstaff on a .staffID =tblStaff .StaffId  where convert(date,a.dates) between '" & date1 & "' and '" & date2 & "' and tblStaff.BrID='" & BrID & "' and b.BrID='" & BrID & "' and a.staffID='" & txtInsert.Text & "' order by a.ID desc")
    End Sub
    Private Sub txtPName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPName.TextChanged
        If txtPName.Text = "" Then
            lblAmount.Text = ""
        End If
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReportResource.MdiParent = frmMain
        frmReportResource.WindowState = FormWindowState.Maximized
        frmReportResource.Show()
    End Sub
    Private Sub DataGridView1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                Me.txtStaffID.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value.ToString
                Me.txtStaffName.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString
                Me.txtPosition.Text = Me.DataGridView1.SelectedRows(0).Cells(3).Value
                Me.txtPID.Text = Me.DataGridView1.SelectedRows(0).Cells(4).Value
                Me.txtPName.Text = Me.DataGridView1.SelectedRows(0).Cells(5).Value
                Me.txtOum.Text = Me.DataGridView1.SelectedRows(0).Cells(6).Value
                Me.txtAmount.Text = Me.DataGridView1.SelectedRows(0).Cells(7).Value
                Me.DateTimePicker1.Value = Me.DataGridView1.SelectedRows(0).Cells(8).Value
                Me.txtType.Text = Me.DataGridView1.SelectedRows(0).Cells(13).Value
                If Me.DataGridView1.SelectedRows(0).Cells(9).Value = 0 Then
                    txtUnitprice.Text = Me.DataGridView1.SelectedRows(0).Cells(11).Value
                Else
                    txtUnitprice.Text = Me.DataGridView1.SelectedRows(0).Cells(9).Value
                End If
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        showsAsset()
    End Sub
    Sub datagrid()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 14
        DataGridView1.Columns(0).Name = "No"
        DataGridView1.Columns(1).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(2).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(3).Name = "តួនាទី"
        DataGridView1.Columns(4).Name = "កូដសម្ភារៈ"
        DataGridView1.Columns(5).Name = "ឈ្មោះសម្ភារៈ"
        DataGridView1.Columns(6).Name = "ឯកត្តា"
        DataGridView1.Columns(7).Name = "ចំនួនដកសរុប"
        DataGridView1.Columns(8).Name = "កាលបរិច្ឆេត"
        DataGridView1.Columns(9).Name = "តំលៃរាយរៀល"
        DataGridView1.Columns(10).Name = "តំលៃសរុបរៀល"
        DataGridView1.Columns(11).Name = "តំលៃរាយដុល្លារ"
        DataGridView1.Columns(12).Name = "តំលៃសរុបដុល្លារ"
        DataGridView1.Columns(13).Name = "រូបបិយវត្ថុ"
    End Sub
    Private Sub CboSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboSearch.SelectedIndexChanged
        Dim date1 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(DateTimePicker3.Value, DateFormat.ShortDate)
        Dim result1 As Integer = DateTime.Compare(date2, date1)
        Dim BrID As String = frmMain.lblCode.Text
        If CboSearch.Text = "សូមជ្រើសរើស" Then
            Return
        End If
        If CboSearch.SelectedIndex = 0 Then
            txtInsert.Text = ""
            txtInsert.ReadOnly = True
            If result1 >= 0 Then
                datagrid()
                AddToGrid(DataGridView1, 14, "select  a .ID, a .staffID as 'កូដមន្ត្រីឥណទាន',tblStaff .StaffName as 'ឈ្មោះមន្ត្រីឥណទាន',tblStaff.Position, a .assetID as 'Asset ID',b .assetName as 'Asset Name',b.oumName,a.amount,a .dates as 'កាលបរិច្ឆេត' ,a .Riel ,a.Riel*a.amount TotalRiel,a .Dollar,a.Dollar*a.amount TotalUSD ,a .currency as 'រូបបិយវត្ថុ' from tblAssetDetail a inner join tblAsset b  on a .assetID  =b .assetID inner join tblstaff  on a .staffID =tblStaff .StaffId  where convert(date,a.dates) between '" & date1 & "' and '" & date2 & "' and tblStaff.BrID='" & BrID & "' and b.BrID='" & BrID & "' order by a.ID desc")
            Else
                result = frmMessageError.ShowBoxError("អ្នកបានជ្រើសរើសថ្ងៃខែខុស សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
            End If
        Else
            If result1 < 0 Then
                result = frmMessageError.ShowBoxError("អ្នកបានជ្រើសរើសថ្ងៃខែខុស សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
            Else
                txtInsert.ReadOnly = False
                txtInsert.Focus()
            End If
        End If
    End Sub
End Class