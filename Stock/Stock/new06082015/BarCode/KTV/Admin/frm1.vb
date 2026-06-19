Imports Microsoft.Office.Interop
Imports X = Microsoft.Office.Interop.Excel
Public Class frm1
    'Private Sub frm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    Me.TabPage1.Text = "របាយការណ៍អតិថិជន និង ទ្រព្យតម្តល់"
    '    Me.TabPage2.Text = "របាយការណ៍សាំង"
    '    Me.TabPage3.Text = "របាយការណ៍ស្តុក"
    '    CheckBox1.Checked = True
    '    SetFontDatagrid(DataGridView2)
    '    SetFontDatagrid(DgdSP)
    '    ComboBox2.SelectedIndex = 0
    'End Sub
    'Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Me.ComboBox1.Text = "" Then
    '        resultError = frmMessageError.ShowBoxError("សូមជ្រើសរើសការស្វែងរកជាមុនសិន។", "សូមជ្រើសរើស")
    '        'MessageBox.Show("Please select option first before search again!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return
    '    Else
    '        If ComboBox1.SelectedIndex = 0 Then
    '            SetFontDatagrid(DataGridView1)
    '            DataGridView1.ColumnCount = 3
    '            DataGridView1.Columns(0).Name = "កូដ"
    '            DataGridView1.Columns(1).Name = "ឈ្មោះអតិថិជន"
    '            DataGridView1.Columns(2).Name = "អាសយដ្ឋាន"
    '            AddToGrid(DataGridView1, 3, "select l.CM_ID,c.CM_Name,la.VL_ID from BK_Loan l inner join (select MAX(convert(decimal,LD_ID))  as ld_id ,LD_BrId,CM_ID from BK_Loan group by CM_ID, LD_BrId,CM_ID) ls on l.LD_BrId=ls.LD_BrId and l.CM_ID=ls.CM_ID and l.LD_ID=ls.ld_id inner join BK_Customer c on l.CM_ID=c.CM_ID and l.LD_BrId=c.CM_BrId inner join BK_Location la on c.LO_ID=la.LO_ID and c.CM_BrId=la.LO_BrID where  l.LD_Status ='Active' and l.LD_BrId='" & frmMain.lblCode.Text & "'  order by CONVERT(decimal,l.CM_ID)")
    '        ElseIf ComboBox1.SelectedIndex = 1 Then
    '            SetFontDatagrid(DataGridView1)
    '            DataGridView1.ColumnCount = 3
    '            DataGridView1.Columns(0).Name = "កូដ"
    '            DataGridView1.Columns(1).Name = "ឈ្មោះអតិថិជន"
    '            DataGridView1.Columns(2).Name = "អាសយដ្ឋាន"
    '            AddToGrid(DataGridView1, 3, "select l.CM_ID,c.CM_Name,la.VL_ID from BK_Loan l inner join (select MAX(convert(decimal,LD_ID))  as ld_id ,LD_BrId,CM_ID from BK_Loan group by CM_ID, LD_BrId,CM_ID) ls on l.LD_BrId=ls.LD_BrId and l.CM_ID=ls.CM_ID and l.LD_ID=ls.ld_id inner join BK_Customer c on l.CM_ID=c.CM_ID and l.LD_BrId=c.CM_BrId inner join BK_Location la on c.LO_ID=la.LO_ID and c.CM_BrId=la.LO_BrID where not l.LD_Status ='Active' and l.LD_BrId='" & frmMain.lblCode.Text & "'  order by CONVERT(decimal,l.CM_ID)")
    '        ElseIf ComboBox1.SelectedIndex = 2 Then
    '            SetFontDatagrid(DataGridView1)
    '            DataGridView1.ColumnCount = 11
    '            DataGridView1.Columns(0).Name = "កូដ"
    '            DataGridView1.Columns(1).Name = "ឈ្មោះអតិថិជន"
    '            DataGridView1.Columns(2).Name = "អាសយដ្ឋាន"
    '            DataGridView1.Columns(3).Name = "សៀវភៅគ្រួសារ"
    '            DataGridView1.Columns(4).Name = "សៀវភៅស្នាក់នៅ"
    '            DataGridView1.Columns(5).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ម៉ូតូ)"
    '            DataGridView1.Columns(6).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ឡាន)"
    '            DataGridView1.Columns(7).Name = "បង្កាន់ដៃពន្ធនាំចូល"
    '            DataGridView1.Columns(8).Name = "ប្លង់រឹង"
    '            DataGridView1.Columns(9).Name = "ប្លង់ទន់"
    '            DataGridView1.Columns(10).Name = "File"
    '            AddToGrid(DataGridView1, 11, "select a.CU_ID,b.CM_Name,c.VL_ID,FamilyBook,LiveBook,MotoCard,CarCard,BongKanDai,plong_reng,Plong_tun,Files from tblCU_Pro a inner join BK_Customer b on a.CU_ID=b.CM_ID and a.BrID=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrId where isnull(FamilyBook+LiveBook+MotoCard+CarCard+BongKanDai+plong_reng+Plong_tun+Files,0)>0  and a.BrID='" & frmMain.lblCode.Text & "' order by CM_ID")
    '            'ElseIf ComboBox1.SelectedIndex = 3 Then
    '            '    SetFontDatagrid(DataGridView3)
    '            '    DataGridView3.ColumnCount = 3
    '            '    DataGridView3.Columns(0).Name = "កូដ"
    '            '    DataGridView3.Columns(1).Name = "ឈ្មោះអតិថិជន"
    '            '    DataGridView3.Columns(2).Name = "អាសយដ្ឋាន"
    '            '    AddToGrid(DataGridView3, 3, "select Convert(decimal,b.CM_ID) CM_ID,b.CM_Name,c.VL_ID from tblCU_Pro a right join BK_Customer b on a.CU_ID=b.CM_ID and a.BrID=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrId where isnull(isnull(FamilyBook,0)+isnull(LiveBook,0)+isnull(MotoCard,0)+isnull(CarCard,0)+isnull(BongKanDai,0)+isnull(plong_reng,0)+isnull(Plong_tun+Files,0),0)=0  and b.CM_BrId='" & frmMain.lblCode.Text & "' order by CM_ID")
    '        ElseIf ComboBox1.SelectedIndex = 3 Then
    '            SetFontDatagrid(DataGridView1)
    '            DataGridView1.ColumnCount = 3
    '            DataGridView1.Columns(0).Name = "កូដ"
    '            DataGridView1.Columns(1).Name = "ឈ្មោះអតិថិជន"
    '            DataGridView1.Columns(2).Name = "អាសយដ្ឋាន"
    '            AddToGrid(DataGridView1, 3, "select l.CM_ID,c.CM_Name,la.VL_ID from BK_Loan l inner join (select MAX(convert(decimal,LD_ID))  as ld_id ,LD_BrId,CM_ID from BK_Loan group by CM_ID, LD_BrId,CM_ID) ls on l.LD_BrId=ls.LD_BrId and l.CM_ID=ls.CM_ID and l.LD_ID=ls.ld_id inner join BK_Customer c on l.CM_ID=c.CM_ID and l.LD_BrId=c.CM_BrId inner join BK_Location la on c.LO_ID=la.LO_ID and c.CM_BrId=la.LO_BrID where  l.LD_BrId='" & frmMain.lblCode.Text & "'  order by CONVERT(decimal,l.CM_ID)")
    '        ElseIf ComboBox1.SelectedIndex = 4 Then
    '            SetFontDatagrid(DataGridView1)
    '            DataGridView1.ColumnCount = 9
    '            DataGridView1.Columns(0).Name = "កូដសាខា"
    '            DataGridView1.Columns(1).Name = "សៀវភៅគ្រួសារ"
    '            DataGridView1.Columns(2).Name = "សៀវភៅស្នាក់នៅ"
    '            DataGridView1.Columns(3).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ម៉ូតូ)"
    '            DataGridView1.Columns(4).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ឡាន)"
    '            DataGridView1.Columns(5).Name = "បង្កាន់ដៃពន្ធនាំចូល"
    '            DataGridView1.Columns(6).Name = "ប្លង់រឹង"
    '            DataGridView1.Columns(7).Name = "ប្លង់ទន់"
    '            DataGridView1.Columns(8).Name = "File"
    '            AddToGrid(DataGridView1, 9, " select BrID,SUM(ISNULL(FamilyBook,0)) Family ,SUM(ISNULL(LiveBook,0)) LiveBook ,SUM(ISNULL(MotoCard,0)) MotoCard, SUM(ISNULL(CarCard,0)) CarCard, SUM(ISNULL(BongKanDai,0)) BongKanDai, SUM(ISNULL(plong_reng,0)) plong_reng, SUM(ISNULL(Plong_tun,0)) Plong_tun,SUM(ISNULL(Files,0)) Files from tblCU_Pro where BrID='" & frmMain.lblCode.Text & "' group by BrID")
    '        ElseIf ComboBox1.SelectedIndex = 5 Then
    '            SetFontDatagrid(DataGridView1)
    '            DataGridView1.ColumnCount = 25
    '            DataGridView1.Columns(0).Name = "កូដសាខា"
    '            DataGridView1.Columns(1).Name = "ប័ណ្ណ(ម៉ូតូ)"
    '            DataGridView1.Columns(2).Name = "ប័ណ្ណ(ឡាន)"
    '            DataGridView1.Columns(3).Name = "ស.ស្នាក់នៅ"
    '            DataGridView1.Columns(4).Name = "ស.គ្រួសារ"
    '            DataGridView1.Columns(5).Name = "ប្លង់រឹង"
    '            DataGridView1.Columns(6).Name = "ប្លង់ទន់"
    '            DataGridView1.Columns(7).Name = "ប.ពន្ធនាំចូល"
    '            DataGridView1.Columns(8).Name = "File"
    '            DataGridView1.Columns(9).Name = "ប័ណ្ណ(ម៉ូតូ)"
    '            DataGridView1.Columns(10).Name = "ប័ណ្ណ(ឡាន)"
    '            DataGridView1.Columns(11).Name = "ស.ស្នាក់នៅ"
    '            DataGridView1.Columns(12).Name = "ស.គ្រួសារ"
    '            DataGridView1.Columns(13).Name = "ប្លង់រឹង"
    '            DataGridView1.Columns(14).Name = "ប្លង់ទន់"
    '            DataGridView1.Columns(15).Name = "ប.ពន្ធនាំចូល"
    '            DataGridView1.Columns(16).Name = "File"
    '            DataGridView1.Columns(17).Name = "ប័ណ្ណ(ម៉ូតូ)"
    '            DataGridView1.Columns(18).Name = "ប័ណ្ណ(ឡាន)"
    '            DataGridView1.Columns(19).Name = "ស.ស្នាក់នៅ"
    '            DataGridView1.Columns(20).Name = "ស.គ្រួសារ"
    '            DataGridView1.Columns(21).Name = "ប្លង់រឹង"
    '            DataGridView1.Columns(22).Name = "ប្លង់ទន់"
    '            DataGridView1.Columns(23).Name = "ប.ពន្ធនាំចូល"
    '            DataGridView1.Columns(24).Name = "File"
    '            AddToGrid(DataGridView1, 25, "exec sp_SummaryCollateral '" & frmMain.lblCode.Text & "'")
    '        End If
    '    End If
    'End Sub
    'Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ToExcel(DataGridView1)
    'End Sub
    'Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If ComboBox1.SelectedIndex = 0 Then
    '            excelexportCount(DataGridView1, "D:\new\simple Excel\simple Excel\Active customer.xls", 6, "C", "a", "b", "c", "c")
    '        ElseIf ComboBox1.SelectedIndex = 1 Then
    '            excelexportCount(DataGridView1, "D:\new\simple Excel\simple Excel\Inactive customer.xls", 6, "C", "a", "b", "c", "c")
    '        ElseIf ComboBox1.SelectedIndex = 2 Then
    '            excelexport(DataGridView1, "D:\new\simple Excel\simple Excel\DepositSchedule.xls", 6, "k", "a", "c", "d", "k")
    '        ElseIf ComboBox1.SelectedIndex = 3 Then
    '            excelexportCount(DataGridView1, "D:\new\simple Excel\simple Excel\Inactive customer.xls", 6, "C", "a", "b", "c", "c")
    '        ElseIf ComboBox1.SelectedIndex = 4 Then
    '            excelexportNormal(DataGridView1, "D:\new\simple Excel\simple Excel\All collateral.xls", 6, "i")
    '        ElseIf ComboBox1.SelectedIndex = 5 Then
    '            'Summary(Collateral)
    '            excelexportNormal(DataGridView1, "D:\new\simple Excel\simple Excel\Summary Collateral.xls", 7, "y")
    '        End If
    '    Catch ex As Exception
    '        resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
    '    End Try
    'End Sub
    'Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    'End Sub
    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
    '    Dim date2 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
    '    Dim result As Integer = DateTime.Compare(date1, date2)
    '    Dim BrID As String = frmMain.lblCode.Text
    '    '-----------------------------.te--------------------------------------------------------------------------
    '    If BrID = "" Then
    '        resultError = frmMessageError.ShowBoxError("កូដសាខាមិនអាចគ្មានបានទេ", "គ្មានកូដសាខា")
    '        'MessageBox.Show("Please input correct brand ID before search.")
    '    Else
    '        If RadioButton1.Checked Then
    '            If CheckBox1.Checked Then
    '                If result > 0 Then
    '                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
    '                    'MessageBox.Show("You've selected date not correct, please check again before reload record!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Return
    '                Else
    '                    DataGridView2.Columns.Clear()
    '                    DataGridView2.ColumnCount = 7
    '                    DataGridView2.Columns(0).Name = "កូដបុគ្គលិក"
    '                    DataGridView2.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
    '                    DataGridView2.Columns(2).Name = "លេខម៉ូតូ"
    '                    DataGridView2.Columns(3).Name = "ចំនួនលីត្រ"
    '                    DataGridView2.Columns(4).Name = "តំលៃសរុប"
    '                    DataGridView2.Columns(5).Name = "ចំនួនគីឡូម៉ែត្រ"
    '                    AddToGrid(DataGridView2, 6, "select b.staffid,b.StaffName ,b.MotoNo,sum(amount)amount,sum(amount*unitPrice) as Total,sum(a.Km )Km from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & BrID & "' group by b.StaffID ,b.StaffName ,b.MotoNo order by StaffID ")
    '                End If
    '            Else
    '                DataGridView2.Columns.Clear()
    '                DataGridView2.ColumnCount = 7
    '                DataGridView2.Columns(0).Name = "កូដបុគ្គលិក"
    '                DataGridView2.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
    '                DataGridView2.Columns(2).Name = "លេខម៉ូតូ"
    '                DataGridView2.Columns(3).Name = "ចំនួនលីត្រ"
    '                DataGridView2.Columns(4).Name = "តំលៃសរុប"
    '                DataGridView2.Columns(5).Name = "ចំនួនគីឡូម៉ែត្រ"
    '                AddToGrid(DataGridView2, 6, "select b.staffid,b.StaffName ,b.MotoNo,sum(amount)amount,sum(amount*unitPrice) as Total,sum(a.Km )Km from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.BrID='" & BrID & "' group by b.StaffID ,b.StaffName ,b.MotoNo order by StaffID ")
    '            End If
    '        Else
    '            If CheckBox1.Checked Then
    '                If result > 0 Then
    '                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
    '                    'MessageBox.Show("You've selected date not correct, please check again before reload record!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Return
    '                Else
    '                    DataGridView2.Columns.Clear()
    '                    DataGridView2.ColumnCount = 7
    '                    DataGridView2.Columns(0).Name = "កូដបុគ្គលិក"
    '                    DataGridView2.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
    '                    DataGridView2.Columns(2).Name = "លេខម៉ូតូ"
    '                    DataGridView2.Columns(3).Name = "ចំនួនលីត្រ"
    '                    DataGridView2.Columns(4).Name = "តំលៃសរុប"
    '                    DataGridView2.Columns(5).Name = "ចំនួនគីឡូម៉ែត្រ"
    '                    AddToGrid(DataGridView2, 6, "select b.staffid,b.StaffName ,b.MotoNo,sum(amount)amount,sum(amount*unitPrice) as Total,sum(a.Km )Km from tblSang a left join tblStaff b on a.StaffID =b.StaffID and a.BrID=b.BrID where b.staffID='" & txtStaffID.Text & "' and a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & BrID & "'group by b.StaffID ,b.StaffName ,b.MotoNo order by StaffID ")
    '                End If
    '            Else
    '                DataGridView2.Columns.Clear()
    '                DataGridView2.ColumnCount = 7
    '                DataGridView2.Columns(0).Name = "កូដបុគ្គលិក"
    '                DataGridView2.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
    '                DataGridView2.Columns(2).Name = "លេខម៉ូតូ"
    '                DataGridView2.Columns(3).Name = "ចំនួនលីត្រ"
    '                DataGridView2.Columns(4).Name = "តំលៃសរុប"
    '                DataGridView2.Columns(5).Name = "ចំនួនគីឡូម៉ែត្រ"
    '                AddToGrid(DataGridView2, 6, "select b.staffid,b.StaffName ,b.MotoNo,sum(amount)amount,sum(amount*unitPrice) as Total,sum(a.Km )Km from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.BrID = '" & BrID & "' and b.staffID='" & txtStaffID.Text & "'  group by b.StaffID ,b.StaffName ,b.MotoNo order by StaffID ")
    '            End If
    '        End If

    '    End If
    'End Sub
    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ToExcel(DataGridView2)
    'End Sub
    'Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    'End Sub
    'Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    'End Sub


    'Private Sub CheckDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If CheckDate.Checked = True Then
    '        If ComboBox2.SelectedIndex = 0 And RadAll.Checked Then
    '            result = frmMessageError.ShowBoxError("ការជ្រើសរើសនេះមិនត្រឹមត្រូវទេ។", "មិនត្រឹមត្រូវ")
    '            'MessageBox.Show("Not allows this option, please try again!!!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            CheckDate.Checked = False
    '        Else
    '            lblFrom.Visible = True
    '            lblTo.Visible = True
    '            DateStart.Visible = True
    '            DateEnd.Visible = True
    '        End If
    '    Else
    '        lblFrom.Visible = False
    '        lblTo.Visible = False
    '        DateStart.Visible = False
    '        DateEnd.Visible = False
    '    End If
    'End Sub

    'Private Sub RadCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    lblcode.Visible = True
    '    TextBox1.Focus()
    '    TextBox1.Visible = True
    'End Sub

    'Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim date1 As Date = FormatDateTime(DateStart.Value, DateFormat.ShortDate)
    '    Dim date2 As Date = FormatDateTime(DateEnd.Value, DateFormat.ShortDate)
    '    Dim result As Integer = DateTime.Compare(date1, date2)
    '    Dim BrID As String = frmMain.lblCode.Text
    '    If BrID = "" Then
    '        'MessageBox.Show("Please insert your brand code first before search again.")
    '        result = frmMessageError.ShowBoxError("កូដសាខាមិនអាចគ្មានបានទេ សូមពិនិត្យម្តងទៀត។", "គ្មានកូដសាខា")
    '        Return
    '    Else
    '        If ComboBox2.SelectedIndex = 0 Then
    '            DgdSP.ColumnCount = 4
    '            DgdSP.Columns(0).Name = "កូដទ្រព្យ"
    '            DgdSP.Columns(1).Name = "ឈ្មោះទ្រព្យ"
    '            DgdSP.Columns(2).Name = "រង្វាស់"
    '            DgdSP.Columns(3).Name = "ចំនួនសរុប"
    '            If RadAll.Checked Then
    '                AddToGrid(DgdSP, 4, "Select assetID,assetName,oumName,totalAmount from tblasset where BrID='" & BrID & "' ")
    '            ElseIf RadCode.Checked Then
    '                AddToGrid(DgdSP, 4, "Select assetID,assetName,oumName,totalAmount from tblasset where assetID='" & TextBox1.Text & "' and BrID='" & BrID & "'")
    '            Else
    '                'MessageBox.Show("Not allow this optoin!!!")
    '                resultError = frmMessageError.ShowBoxError("ការជ្រ់សរើសក្នុងការស្វែងរកមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ជ្រើសរើសខុស")
    '                RadAll.Checked = True
    '            End If
    '        ElseIf ComboBox2.SelectedIndex = 1 Then
    '            If RadAll.Checked = True And CheckDate.Checked = False Then
    '                DgdSP.Columns.Clear()
    '                DgdSP.ColumnCount = 7
    '                DgdSP.Columns(0).Name = "កូដមន្ត្រីឥណទាន"
    '                DgdSP.Columns(1).Name = "ឈ្មោះមន្ត្រីឥណទាន"
    '                DgdSP.Columns(2).Name = "កូដទ្រព្យ"
    '                DgdSP.Columns(3).Name = "ឈ្មោះទ្រព្យ"
    '                DgdSP.Columns(4).Name = "ចំនួនសរុប"
    '                DgdSP.Columns(5).Name = "គិតជារៀល"
    '                DgdSP.Columns(6).Name = "គិតជាដុល្លារ"
    '                AddToGrid(DgdSP, 7, "select c.staffid,c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel,SUM(dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where c.BrID='" & BrID & "' group by c.StaffName ,a.assetID ,b.assetName,c.StaffID order by c.staffID ")
    '            ElseIf RadAll.Checked = True And CheckDate.Checked = True Then
    '                DgdSP.Columns.Clear()
    '                If result > 0 Then
    '                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
    '                    'MessageBox.Show("You've selected date not correct, please check again before reload record!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Return
    '                Else
    '                    DgdSP.Columns.Clear()
    '                    DgdSP.ColumnCount = 7
    '                    DgdSP.Columns(0).Name = "កូដមន្ត្រីឥណទាន"
    '                    DgdSP.Columns(1).Name = "ឈ្មោះមន្ត្រីឥណទាន"
    '                    DgdSP.Columns(2).Name = "កូដទ្រព្យ"
    '                    DgdSP.Columns(3).Name = "ឈ្មោះទ្រព្យ"
    '                    DgdSP.Columns(4).Name = "ចំនួនសរុប"
    '                    DgdSP.Columns(5).Name = "គិតជារៀល"
    '                    DgdSP.Columns(6).Name = "គិតជាដុល្លារ"
    '                    AddToGrid(DgdSP, 7, "select  c.staffid,c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel,SUM(dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & BrID & "' group by c.StaffName ,a.assetID ,b.assetName,c.staffID order by c.staffID")
    '                End If
    '            ElseIf RadCode.Checked = True And CheckDate.Checked = False Then
    '                DgdSP.Columns.Clear()
    '                DgdSP.ColumnCount = 7
    '                DgdSP.Columns(0).Name = "កូដមន្ត្រីឥណទាន"
    '                DgdSP.Columns(1).Name = "ឈ្មោះមន្ត្រីឥណទាន"
    '                DgdSP.Columns(2).Name = "កូដទ្រព្យ"
    '                DgdSP.Columns(3).Name = "ឈ្មោះទ្រព្យ"
    '                DgdSP.Columns(4).Name = "ចំនួនសរុប"
    '                DgdSP.Columns(5).Name = "គិតជារៀល"
    '                DgdSP.Columns(6).Name = "គិតជាដុល្លារ"
    '                AddToGrid(DgdSP, 7, "select  c.staffid,c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel, sum(Dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where a.staffID ='" & TextBox1.Text & "' and c.BrID=BrID='" & BrID & "' group by c.StaffName ,a.assetID,b.assetName,c.staffID order by c.staffID")
    '            ElseIf RadCode.Checked = True And CheckDate.Checked = True Then
    '                If result > 0 Then
    '                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
    '                    'MessageBox.Show("You've selected date not correct, please check again before reload record!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Return
    '                Else
    '                    DgdSP.Columns.Clear()
    '                    DgdSP.ColumnCount = 6
    '                    DgdSP.Columns(0).Name = "ឈ្មោះមន្ត្រីឥណទាន"
    '                    DgdSP.Columns(1).Name = "កូដទ្រព្យ"
    '                    DgdSP.Columns(2).Name = "ឈ្មោះទ្រព្យ"
    '                    DgdSP.Columns(3).Name = "ចំនួនសរុប"
    '                    DgdSP.Columns(4).Name = "គិតជារៀល"
    '                    DgdSP.Columns(5).Name = "គិតជាដុល្លារ"
    '                    AddToGrid(DgdSP, 6, "select c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel, sum(Dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where a.staffID ='" & TextBox1.Text & "' and cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & BrID & "' group by c.StaffName ,a.assetID,b.assetName ")
    '                End If

    '            End If
    '        Else
    '            If RadAll.Checked = True And CheckDate.Checked = False Then
    '                DgdSP.Columns.Clear()
    '                DgdSP.ColumnCount = 5
    '                DgdSP.Columns(0).Name = "កូដទ្រព្យ"
    '                DgdSP.Columns(1).Name = "ឈ្មោះទ្រព្យ"
    '                DgdSP.Columns(2).Name = "ចំនួនសរុប"
    '                DgdSP.Columns(3).Name = "គិតជារៀល"
    '                DgdSP.Columns(4).Name = "គិតជាដុល្លារ"
    '                AddToGrid(DgdSP, 5, "select tblassetadd.assetID ,tblAsset .assetName,SUM(amount) Amount,  SUM(Riel)   Riel ,SUM(Dollar) Dollar from tblAssetAdd inner join tblAsset on tblAsset .assetID =tblAssetAdd .assetID where tblAsset.BrID='" & BrID & "' group by tblAssetAdd .assetID ,tblAsset .assetName  order by tblAssetAdd.assetID")
    '            ElseIf RadAll.Checked = True And CheckDate.Checked = True Then
    '                DgdSP.Columns.Clear()
    '                If result > 0 Then
    '                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
    '                    'MessageBox.Show("You've selected date not correct, please check again before reload record!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Return
    '                Else
    '                    DgdSP.ColumnCount = 5
    '                    DgdSP.Columns(0).Name = "កូដទ្រព្យ"
    '                    DgdSP.Columns(1).Name = "ឈ្មោះទ្រព្យ"
    '                    DgdSP.Columns(2).Name = "ចំនួនសរុប"
    '                    DgdSP.Columns(3).Name = "គិតជារៀល"
    '                    DgdSP.Columns(4).Name = "គិតជាដុល្លារ"
    '                    AddToGrid(DgdSP, 5, "select tblassetadd.assetID ,tblAsset .assetName,SUM(amount) Amount,  SUM(Riel)   Riel ,SUM(Dollar) Dollar from tblAssetAdd inner join tblAsset on tblAsset .assetID =tblAssetAdd .assetID where cast(tblAssetAdd.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and tblAssetAdd.BrID='" & BrID & "' group by tblAssetAdd .assetID ,tblAsset .assetName  order by tblAssetAdd.assetID")
    '                End If
    '            ElseIf RadCode.Checked = True And CheckDate.Checked = False Then
    '                DgdSP.Columns.Clear()
    '                DgdSP.ColumnCount = 5
    '                DgdSP.Columns(0).Name = "កូដទ្រព្យ"
    '                DgdSP.Columns(1).Name = "ឈ្មោះទ្រព្យ"
    '                DgdSP.Columns(2).Name = "ចំនួនសរុប"
    '                DgdSP.Columns(3).Name = "គិតជារៀល"
    '                DgdSP.Columns(4).Name = "គិតជាដុល្លារ"
    '                AddToGrid(DgdSP, 5, "select tblassetadd.assetID ,tblAsset .assetName,SUM(amount) Amount,  SUM(Riel)   Riel ,SUM(Dollar) Dollar from tblAssetAdd inner join tblAsset on tblAsset .assetID =tblAssetAdd .assetID where tblAssetAdd.assetID='" & TextBox1.Text & "' and tblAssetAdd.BrID='" & BrID & "' group by tblAssetAdd .assetID ,tblAsset .assetName order by tblAssetAdd.assetID")
    '            ElseIf RadCode.Checked = True And CheckDate.Checked = True Then
    '                DgdSP.Columns.Clear()
    '                If result > 0 Then
    '                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
    '                    'MessageBox.Show("You've selected date not correct, please check again before reload record!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '                    Return
    '                Else
    '                    DgdSP.ColumnCount = 5
    '                    DgdSP.Columns(0).Name = "កូដទ្រព្យ"
    '                    DgdSP.Columns(1).Name = "ឈ្មោះទ្រព្យ"
    '                    DgdSP.Columns(2).Name = "ចំនួនសរុប"
    '                    DgdSP.Columns(3).Name = "គិតជារៀល"
    '                    DgdSP.Columns(4).Name = "គិតជាដុល្លារ"
    '                    AddToGrid(DgdSP, 5, "select tblassetadd.assetID,tblAsset .assetName,SUM(amount) Amount,  SUM(Riel)   Riel ,SUM(Dollar) Dollar from tblAssetAdd inner join tblAsset on tblAsset .assetID =tblAssetAdd .assetID where cast(tblAssetAdd.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and tblAssetAdd.assetID='" & TextBox1.Text & "' and tblAssetAdd.BrID='" & BrID & "'group by tblAssetAdd .assetID ,tblAsset .assetName  order by tblAssetAdd.assetID")
    '                End If
    '            End If
    '        End If
    '    End If

    'End Sub
    'Sub toExcelAsset()
    '    Dim rowsTotal, colsTotal As Short
    '    Dim I, j, iC As Short
    '    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '    Dim xlApp As New X.Application
    '    Try
    '        Dim excelBook As X.Workbook = xlApp.Workbooks.Add
    '        Dim excelWorksheet As X.Worksheet = CType(excelBook.Worksheets(1), X.Worksheet)
    '        xlApp.Visible = True
    '        rowsTotal = DgdSP.RowCount - 1
    '        colsTotal = DgdSP.Columns.Count - 1
    '        With excelWorksheet
    '            .Cells.Select()
    '            .Cells.Delete()
    '            .Cells(1, iC + 1).Value = "របាយការណ៍ស្តីអំពីទ្រព្យ"
    '            For iC = 0 To colsTotal
    '                .Cells(3, iC + 1).Value = DgdSP.Columns(iC).HeaderText
    '            Next
    '            For I = 0 To rowsTotal
    '                For j = 0 To colsTotal
    '                    .Cells(I + 4, j + 1).value = DgdSP.Rows(I).Cells(j).Value
    '                Next j
    '            Next I
    '            .Rows("1:1").Font.FontStyle = "Regular"
    '            .Rows("1:1").Font.Size = 10
    '            '.Cells.Columns.AutoFit()
    '            .Cells.Select()
    '            '.Cells.EntireColumn.AutoFit()
    '            .Cells(1, 1).Select()
    '            .Cells.Font.Name = "Khmer os battambang"
    '            .Cells.Font.Size = 10
    '        End With
    '    Catch ex As Exception
    '        MsgBox("Export Excel Error " & ex.Message)
    '    Finally
    '        'RELEASE ALLOACTED RESOURCES
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '        xlApp = Nothing
    '    End Try
    'End Sub
    'Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If ComboBox2.SelectedIndex = 0 Then
    '        toExcelAsset()
    '    ElseIf ComboBox2.SelectedIndex = 1 Then
    '        Dim rowsTotal, colsTotal As Short
    '        Dim I, j, iC As Short
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '        Dim xlApp As New X.Application
    '        Try
    '            Dim excelBook As X.Workbook = xlApp.Workbooks.Add
    '            Dim excelWorksheet As X.Worksheet = CType(excelBook.Worksheets(1), X.Worksheet)
    '            xlApp.Visible = True
    '            rowsTotal = DgdSP.RowCount - 1
    '            colsTotal = DgdSP.Columns.Count - 1
    '            With excelWorksheet
    '                .Cells.Select()
    '                .Cells.Delete()
    '                For iC = 0 To colsTotal
    '                    .Cells(1, iC + 1).Value = DgdSP.Columns(iC).HeaderText
    '                Next
    '                For I = 0 To rowsTotal
    '                    For j = 0 To colsTotal
    '                        .Cells(I + 2, j + 1).value = DgdSP.Rows(I).Cells(j).Value
    '                    Next j
    '                Next I
    '                .Rows("1:1").Font.FontStyle = "Regular"
    '                .Rows("1:1").Font.Size = 10
    '                .Cells.Columns.AutoFit()
    '                .Cells.Select()
    '                .Cells.EntireColumn.AutoFit()
    '                .Cells(1, 1).Select()
    '                .Cells.Font.Name = "Khmer os battambang"
    '                .Cells.Font.Size = 10
    '            End With
    '        Catch ex As Exception
    '            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
    '        Finally
    '            'RELEASE ALLOACTED RESOURCES
    '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '            xlApp = Nothing
    '        End Try
    '    Else
    '        Dim rowsTotal, colsTotal As Short
    '        Dim I, j, iC As Short
    '        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    '        Dim xlApp As New X.Application
    '        Try
    '            Dim excelBook As X.Workbook = xlApp.Workbooks.Add
    '            Dim excelWorksheet As X.Worksheet = CType(excelBook.Worksheets(1), X.Worksheet)
    '            xlApp.Visible = True
    '            rowsTotal = DgdSP.RowCount - 1
    '            colsTotal = DgdSP.Columns.Count - 1
    '            With excelWorksheet
    '                .Cells.Select()
    '                .Cells.Delete()
    '                For iC = 0 To colsTotal
    '                    .Cells(1, iC + 1).Value = DgdSP.Columns(iC).HeaderText
    '                Next
    '                For I = 0 To rowsTotal
    '                    For j = 0 To colsTotal
    '                        .Cells(I + 2, j + 1).value = DgdSP.Rows(I).Cells(j).Value
    '                    Next j
    '                Next I
    '                .Rows("1:1").Font.FontStyle = "Regular"
    '                .Rows("1:1").Font.Size = 10
    '                .Cells.Columns.AutoFit()
    '                .Cells.Select()
    '                .Cells.EntireColumn.AutoFit()
    '                .Cells(1, 1).Select()
    '                .Cells.Font.Name = "Khmer os battambang"
    '                .Cells.Font.Size = 10
    '            End With
    '        Catch ex As Exception
    '            MsgBox("Export Excel Error " & ex.Message)
    '        Finally
    '            'RELEASE ALLOACTED RESOURCES
    '            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    '            xlApp = Nothing
    '        End Try
    '    End If
    'End Sub

    'Private Sub Button5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.Close()
    'End Sub

    'Private Sub btnSearch_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

    'End Sub
End Class