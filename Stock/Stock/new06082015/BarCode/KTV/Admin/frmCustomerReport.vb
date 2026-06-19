Imports System.IO
Imports System.Data
Imports System.Reflection
Imports X = Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Excel

Public Class frmCustomerReport
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub Hided()
        Label10.Hide()
        Label9.Hide()
        DateBorrow.Hide()
        DateReturn.Hide()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim date1 As Date = FormatDateTime(Me.DateBorrow.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(Me.DateReturn.Value, DateFormat.ShortDate)
        'date1.ToString("MM-dd-yyyy")
        'date2.ToString("MM-dd-yyyy")
        'MessageBox.Show(date1 & " -" & date2)
        'Return
        Dim BrID As String = frmMain.lblCode.Text
        If ComboBox1.SelectedIndex = 0 Then
            SetFontDatagrid(DataGridView1)
            DataGridView1.ColumnCount = 3
            DataGridView1.Columns(0).Name = "កូដ"
            DataGridView1.Columns(1).Name = "ឈ្មោះអតិថិជន"
            DataGridView1.Columns(2).Name = "អាសយដ្ឋាន"
            AddToGrid(DataGridView1, 3, "select l.CM_ID,c.CM_Name,la.VL_ID from BK_Loan l inner join (select MAX(convert(decimal,LD_ID))  as ld_id ,LD_BrId,CM_ID from BK_Loan group by CM_ID, LD_BrId,CM_ID) ls on l.LD_BrId=ls.LD_BrId and l.CM_ID=ls.CM_ID and l.LD_ID=ls.ld_id inner join BK_Customer c on l.CM_ID=c.CM_ID and l.LD_BrId=c.CM_BrId inner join BK_Location la on c.LO_ID=la.LO_ID and c.CM_BrId=la.LO_BrID where  l.LD_Status ='Active' and l.LD_BrId='" & frmMain.lblCode.Text & "'  order by CONVERT(decimal,l.CM_ID)")

        ElseIf ComboBox1.SelectedIndex = 1 Then
            SetFontDatagrid(DataGridView1)
            DataGridView1.ColumnCount = 3
            DataGridView1.Columns(0).Name = "កូដ"
            DataGridView1.Columns(1).Name = "ឈ្មោះអតិថិជន"
            DataGridView1.Columns(2).Name = "អាសយដ្ឋាន"
            AddToGrid(DataGridView1, 3, "select l.CM_ID,c.CM_Name,la.VL_ID from BK_Loan l inner join (select MAX(convert(decimal,LD_ID))  as ld_id ,LD_BrId,CM_ID from BK_Loan group by CM_ID, LD_BrId,CM_ID) ls on l.LD_BrId=ls.LD_BrId and l.CM_ID=ls.CM_ID and l.LD_ID=ls.ld_id inner join BK_Customer c on l.CM_ID=c.CM_ID and l.LD_BrId=c.CM_BrId inner join BK_Location la on c.LO_ID=la.LO_ID and c.CM_BrId=la.LO_BrID where not l.LD_Status ='Active' and l.LD_BrId='" & frmMain.lblCode.Text & "'  order by CONVERT(decimal,l.CM_ID)")

        ElseIf ComboBox1.SelectedIndex = 2 Then
            SetFontDatagrid(DataGridView1)
            DataGridView1.ColumnCount = 12
            DataGridView1.Columns(0).Name = "កូដ"
            DataGridView1.Columns(1).Name = "ឈ្មោះអតិថិជន"
            DataGridView1.Columns(2).Name = "អាសយដ្ឋាន"
            DataGridView1.Columns(3).Name = "សៀវភៅគ្រួសារ Copy"
            DataGridView1.Columns(4).Name = "សៀវភៅស្នាក់នៅ Copy"
            DataGridView1.Columns(5).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ម៉ូតូ)"
            DataGridView1.Columns(6).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ឡាន)"
            DataGridView1.Columns(7).Name = "បង្កាន់ដៃពន្ធនាំចូល"
            DataGridView1.Columns(8).Name = "ប្លង់រឹង"
            DataGridView1.Columns(9).Name = "ប្លង់ទន់"
            DataGridView1.Columns(10).Name = "File"
            DataGridView1.Columns(11).Name = "ការបរិយាយអំពីទ្រព្យ"
            AddToGrid(DataGridView1, 12, "select a.CU_ID,b.CM_Name,c.VL_ID,FamilyBook,LiveBook,MotoCard,CarCard,BongKanDai,plong_reng,Plong_tun,Files,Des from tblCU_Pro a inner join BK_Customer b on a.CU_ID=b.CM_ID and a.BrID=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrId where isnull(FamilyBook+LiveBook+MotoCard+CarCard+BongKanDai+plong_reng+Plong_tun+Files,0)>0 and a.BrID='" & frmMain.lblCode.Text & "' order by CM_ID desc")

        ElseIf ComboBox1.SelectedIndex = 3 Then
            SetFontDatagrid(DataGridView1)
            DataGridView1.ColumnCount = 3
            DataGridView1.Columns(0).Name = "កូដ"
            DataGridView1.Columns(1).Name = "ឈ្មោះអតិថិជន"
            DataGridView1.Columns(2).Name = "អាសយដ្ឋាន"
            AddToGrid(DataGridView1, 3, "select l.CM_ID,c.CM_Name,la.VL_ID from BK_Loan l inner join (select MAX(convert(decimal,LD_ID))  as ld_id ,LD_BrId,CM_ID from BK_Loan group by CM_ID, LD_BrId,CM_ID) ls on l.LD_BrId=ls.LD_BrId and l.CM_ID=ls.CM_ID and l.LD_ID=ls.ld_id inner join BK_Customer c on l.CM_ID=c.CM_ID and l.LD_BrId=c.CM_BrId inner join BK_Location la on c.LO_ID=la.LO_ID and c.CM_BrId=la.LO_BrID where  l.LD_BrId='" & frmMain.lblCode.Text & "'  order by l.CM_ID")

        ElseIf ComboBox1.SelectedIndex = 4 Then
            SetFontDatagrid(DataGridView1)
            DataGridView1.ColumnCount = 9
            DataGridView1.Columns(0).Name = "កូដសាខា"
            DataGridView1.Columns(1).Name = "សៀវភៅគ្រួសារ Copy"
            DataGridView1.Columns(2).Name = "សៀវភៅស្នាក់នៅ Copy"
            DataGridView1.Columns(3).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ម៉ូតូ)"
            DataGridView1.Columns(4).Name = "ប័ណ្ណសម្គាល់យានយន្ត(ឡាន)"
            DataGridView1.Columns(5).Name = "បង្កាន់ដៃពន្ធនាំចូល"
            DataGridView1.Columns(6).Name = "ប្លង់រឹង"
            DataGridView1.Columns(7).Name = "ប្លង់ទន់"
            DataGridView1.Columns(8).Name = "File"
            AddToGrid(DataGridView1, 9, " select BrID,SUM(ISNULL(FamilyBook,0)) Family ,SUM(ISNULL(LiveBook,0)) LiveBook ,SUM(ISNULL(MotoCard,0)) MotoCard, SUM(ISNULL(CarCard,0)) CarCard, SUM(ISNULL(BongKanDai,0)) BongKanDai, SUM(ISNULL(plong_reng,0)) plong_reng, SUM(ISNULL(Plong_tun,0)) Plong_tun,SUM(ISNULL(Files,0)) Files from tblCU_Pro where BrID='" & frmMain.lblCode.Text & "' group by BrID")

        ElseIf ComboBox1.SelectedIndex = 5 Then
            SetFontDatagrid(DataGridView1)
            DataGridView1.ColumnCount = 9
            DataGridView1.Columns(0).Name = "កូដសាខា"
            DataGridView1.Columns(1).Name = "ប័ណ្ណ(ម៉ូតូ)"
            DataGridView1.Columns(2).Name = "ប័ណ្ណ(ឡាន)"
            DataGridView1.Columns(3).Name = "ស.ស្នាក់នៅCopy"
            DataGridView1.Columns(4).Name = "ស.គ្រួសារ Copy"
            DataGridView1.Columns(5).Name = "ប្លង់រឹង"
            DataGridView1.Columns(6).Name = "ប្លង់ទន់"
            DataGridView1.Columns(7).Name = "ប.ពន្ធនាំចូល"
            DataGridView1.Columns(8).Name = "File"
            AddToGrid(DataGridView1, 9, "exec sp_SummaryCollateral '" & frmMain.lblCode.Text & "'")

        ElseIf ComboBox1.SelectedIndex = 6 Then
            SetFontDatagrid(DataGridView1)
            DataGridView1.ColumnCount = 12
            DataGridView1.Columns(0).Name = "លេខរៀង"
            DataGridView1.Columns(1).Name = "កូដបុគ្គលិក"
            DataGridView1.Columns(2).Name = "ឈ្មោះបុគ្គលិក"
            DataGridView1.Columns(3).Name = "កូដទ្រព្យ"
            DataGridView1.Columns(4).Name = "ឈ្មោះទ្រព្យតម្កល់"
            DataGridView1.Columns(5).Name = "កូដអតិថិជន"
            DataGridView1.Columns(6).Name = "ឈ្មោះអតិថិជន"
            DataGridView1.Columns(7).Name = "អាសយដ្ជាន"
            DataGridView1.Columns(8).Name = "ថ្ងៃខ្ចី"
            DataGridView1.Columns(9).Name = "ថ្ងៃត្រូវសង"
            DataGridView1.Columns(10).Name = "ថ្ងៃលើស"
            DataGridView1.Columns(11).Name = "បានទទួល"
            AddToGrid(DataGridView1, 12, "sp_CheckArrear '" & frmMain.lblCode.Text & "' ,'All', 'All', 'All', 'All', '0' , '" & date1 & "', '" & date2 & "'")

        ElseIf ComboBox1.SelectedIndex = 7 Then
            SetFontDatagrid(DataGridView1)
            DataGridView1.ColumnCount = 8
            DataGridView1.Columns(0).Name = "កូដបុគ្គលិក"
            DataGridView1.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
            DataGridView1.Columns(2).Name = "កូដអតិថិជន"
            DataGridView1.Columns(3).Name = "ឈ្មោះអតិថិជន"
            DataGridView1.Columns(4).Name = "កូដទ្រព្យ"
            DataGridView1.Columns(5).Name = "ឈ្មោះទ្រព្យតម្កល់"
            DataGridView1.Columns(6).Name = "ថ្ងៃខ្ចី"
            DataGridView1.Columns(7).Name = "ថ្ងៃសង"
            AddToGrid(DataGridView1, 8, "select a.staffid,b.staffName,d.CM_ID,d.CM_Name,c.id,c.CollateralName,a.borrowdate,a.returndate,CASE WHEN checking = 1 THEN '-' WHEN checking = 0 And DateDiff(Day, a.borrowdate, a.returndate) > 2 THEN CAST(DATEDIFF(day, a.borrowdate, a.returndate) - 2 AS Varchar) WHEN checking = 0 AND (DATEDIFF(day, a.borrowdate, a.returndate) - 2) < 2  THEN CAST('0' AS Varchar)END AS moreDate from tblResource a inner join tblStaff b on a.staffid=b.StaffID and a.BrID=b.BrID inner join tblCollateral c on a.collateralid=c.id and a.BrID=c.BrID inner join BK_Customer d on a.customid=d.CM_ID and a.BrID=d.CM_BrId where a.borrowdate between '" & date1 & "' and '" & date2 & "' and checking='1' and a.BrID='" & frmMain.lblCode.Text & "' order by StaffID desc")
            'select a.staffid,b.StaffName,CM_ID,CM_Name,CollateralName,a.borrowdate,a.returndate, CASE WHEN checking = 1 THEN '-' WHEN checking = 0 And DateDiff(Day, a.borrowdate, a.returndate) > 3 THEN CAST(DATEDIFF(day, a.borrowdate, a.returndate) - 3 AS Varchar) WHEN checking = 0 AND (DATEDIFF(day, a.borrowdate, a.returndate) - 3) < 3  THEN CAST('0' AS Varchar)END AS moreDate from tblResource a inner join tblStaff b on a.staffid = b.StaffID and a.BrID=b.BrID inner join tblCollateral c on a.collateralid= c.id inner join BK_Customer d on a.customid= d.CM_ID and a.BrID = d.CM_BrId where a.returndate between '" & date1 & "' and '" & date2 & "' and checking='1' and a.BrID='" & frmMain.lblCode.Text & "'")
        End If
    End Sub
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        ToExcel(DataGridView1)
    End Sub

    Private Sub frmCustomerReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Hided()
        DateBorrow.Value = DateTime.Now
        DateReturn.Value = DateTime.Now
    End Sub

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        Try
            If ComboBox1.SelectedIndex = 0 Then
                excelexportCount(DataGridView1, frmMain.strPath & "\simple Excel\Active customer.xls", 6, "C", "a", "b", "c", "c")
            ElseIf ComboBox1.SelectedIndex = 1 Then
                excelexportCount(DataGridView1, frmMain.strPath & "\simple Excel\Inactive customer.xls", 6, "C", "a", "b", "c", "c")
            ElseIf ComboBox1.SelectedIndex = 2 Then
                excelexport(DataGridView1, frmMain.strPath & "\simple Excel\DepositSchedule.xls", 6, "k", "a", "c", "d", "k")
            ElseIf ComboBox1.SelectedIndex = 3 Then
                excelexportCount(DataGridView1, frmMain.strPath & "\simple Excel\Inactive customer1.xls", 6, "C", "a", "b", "c", "c")
            ElseIf ComboBox1.SelectedIndex = 4 Then
                excelexportNormal(DataGridView1, frmMain.strPath & "\simple Excel\All collateral.xls", 6, "i")
            ElseIf ComboBox1.SelectedIndex = 5 Then
                excelexportNormal(DataGridView1, frmMain.strPath & "\simple Excel\Summary Collateral.xls", 7, "y")
            ElseIf ComboBox1.SelectedIndex = 6 Then
                excelexportNormalNew(DataGridView1, frmMain.strPath & "\simple Excel\របាយការណ៍អ្នកដកទ្រព្យតម្កល់ទាំងអស់.xls", 6, "h")
            ElseIf ComboBox1.SelectedIndex = 7 Then
                excelexportNormalNew(DataGridView1, frmMain.strPath & "\simple Excel\របាយការណ៍អ្នកសងទ្រព្យតម្កល់ទាំងអស់.xls", 6, "g")
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            Hided()
        ElseIf ComboBox1.SelectedIndex = 1 Then
            Hided()
        ElseIf ComboBox1.SelectedIndex = 2 Then
            Hided()
        ElseIf ComboBox1.SelectedIndex = 3 Then
            Hided()
        ElseIf ComboBox1.SelectedIndex = 4 Then
            Hided()
        ElseIf ComboBox1.SelectedIndex = 5 Then
            Hided()
        ElseIf ComboBox1.SelectedIndex = 6 Then
            Label10.Show()
            Label9.Show()
            DateBorrow.Show()
            DateReturn.Show()
        ElseIf ComboBox1.SelectedIndex = 7 Then
            Label10.Show()
            Label9.Show()
            DateBorrow.Show()
            DateReturn.Show()
        End If
    End Sub
End Class