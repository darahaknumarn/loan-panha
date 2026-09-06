Imports Microsoft.Office.Interop
Imports X = Microsoft.Office.Interop.Excel
Public Class frmReportResource
    Private Sub frmReportResource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetFontDatagrid(DgdSP)
        ComboBox2.SelectedIndex = 0
        'lblFrom.Visible = False
        'lblTo.Visible = False
        'DateStart.Visible = False
        'DateEnd.Visible = False
        lblcode.Visible = False
        TextBox1.Visible = False
    End Sub
    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadCode.CheckedChanged
        lblcode.Visible = True
        TextBox1.Focus()
        TextBox1.Visible = True
    End Sub
    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadAll.CheckedChanged
        lblcode.Visible = False
        TextBox1.Visible = False
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim date1 As Date = FormatDateTime(DateStart.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(DateEnd.Value, DateFormat.ShortDate)
        Dim result As Integer = DateTime.Compare(date1, date2)
        Dim BrID As String = frmMain.lblCode.Text
        If BrID = "" Then
            'MessageBox.Show("Please insert your brand code first before search again.")
            result = frmMessageError.ShowBoxError("កូដសាខាមិនអាចគ្មានបានទេ សូមពិនិត្យម្តងទៀត។", "គ្មានកូដសាខា")
            Return
        Elseif ComboBox2.SelectedIndex = 0 Then
                DgdSP.ColumnCount = 4
                DgdSP.Columns(0).Name = "កូដទ្រព្យ"
                DgdSP.Columns(1).Name = "ឈ្មោះទ្រព្យ"
                DgdSP.Columns(2).Name = "រង្វាស់"
                DgdSP.Columns(3).Name = "ចំនួនសរុប"
                If RadAll.Checked Then
                    AddToGrid(DgdSP, 4, "Select assetID,assetName,oumName,totalAmount from tblasset where BrID='" & BrID & "' ")
                ElseIf RadCode.Checked Then
                    AddToGrid(DgdSP, 4, "Select assetID,assetName,oumName,totalAmount from tblasset where assetID='" & TextBox1.Text & "' and BrID='" & BrID & "'")
                Else
                    'MessageBox.Show("Not allow this optoin!!!")
                    resultError = frmMessageError.ShowBoxError("ការជ្រ់សរើសក្នុងការស្វែងរកមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ជ្រើសរើសខុស")
                    RadAll.Checked = True
                End If
        ElseIf ComboBox2.SelectedIndex = 1 Then
            DgdSP.Columns.Clear()
            DgdSP.ColumnCount = 11
            DgdSP.Columns(0).Name = "កូដមន្ត្រីឥណទាន"
            DgdSP.Columns(1).Name = "ឈ្មោះមន្ត្រីឥណទាន"
            DgdSP.Columns(2).Name = "តួនាទី"
            DgdSP.Columns(3).Name = "កូដទ្រព្យ"
            DgdSP.Columns(4).Name = "ឈ្មោះទ្រព្យ"
            DgdSP.Columns(5).Name = "រង្វស់"
            DgdSP.Columns(6).Name = "ចំនួនដកសរុប"
            DgdSP.Columns(7).Name = "តម្លៃរាយ(៛)"
            DgdSP.Columns(8).Name = "តម្លៃសរុប(៛)"
            DgdSP.Columns(9).Name = "តម្លៃរាយ($)"
            DgdSP.Columns(10).Name = "តម្លៃសរុប($)"
            'AddToGrid(DgdSP, 7, "select c.staffid,c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel,SUM(dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where c.BrID='" & BrID & "' group by c.StaffName ,a.assetID ,b.assetName,c.StaffID order by c.staffID ")
            If RadAll.Checked Then
                AddToGrid(DgdSP, 11, "select  c.staffid,c.StaffName,c.Position ,a. assetID,b.assetName ,b.oumName,sum(amount)  Amount,Riel,SUM(Amount*Riel) TotalRiel,Dollar,SUM(Amount*dollar) TotalDollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID and a.BrID=b.BrID inner join tblStaff c on a.staffID =c.StaffID where cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & BrID & "' group by c.StaffName ,a.assetID ,b.assetName,c.staffID,oumName,Riel,Dollar,c.Position order by c.staffID")
                'DgdSP.Columns.Clear()
            ElseIf RadCode.Checked Then
                AddToGrid(DgdSP, 11, "select  c.staffid,c.StaffName,c.Position ,a. assetID,b.assetName ,b.oumName,sum(amount)  Amount,Riel,SUM(Amount*Riel) TotalRiel,Dollar,SUM(Amount*dollar) TotalDollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID and a.BrID=b.BrID inner join tblStaff c on a.staffID =c.StaffID where cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & BrID & "' and a.staffID='" & Me.TextBox1.Text & "' group by c.StaffName ,a.assetID ,b.assetName,c.staffID,oumName,Riel,Dollar,c.Position order by c.staffID")
                If result > 0 Then
                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
                    'MessageBox.Show("You've selected date not correct, please check again before reload record!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                    'Else
                    '    DgdSP.Columns.Clear()
                    '    DgdSP.ColumnCount = 11
                    '    DgdSP.Columns(0).Name = "កូដមន្ត្រីឥណទាន"
                    '    DgdSP.Columns(1).Name = "ឈ្មោះមន្ត្រីឥណទាន"
                    '    DgdSP.Columns(2).Name = "តួនាទី"
                    '    DgdSP.Columns(3).Name = "កូដទ្រព្យ"
                    '    DgdSP.Columns(4).Name = "ឈ្មោះទ្រព្យ"
                    '    DgdSP.Columns(5).Name = "រង្វស់"
                    '    DgdSP.Columns(6).Name = "ចំនួនដកសរុប"
                    '    DgdSP.Columns(7).Name = "តម្លៃរាយ(៛)"
                    '    DgdSP.Columns(8).Name = "តម្លៃសរុប(៛)"
                    '    DgdSP.Columns(9).Name = "តម្លៃរាយ($)"
                    '    DgdSP.Columns(10).Name = "តម្លៃសរុប($)"

                    'AddToGrid(DgdSP, 7, "select  c.staffid,c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel,SUM(dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & BrID & "' group by c.StaffName ,a.assetID ,b.assetNamee,c.staffID order by c.staffID")
                End If
            End If       
            ElseIf ComboBox2.SelectedIndex = 2 Then
                DgdSP.Columns.Clear()
                DgdSP.ColumnCount = 9
                DgdSP.Columns(0).Name = "កូដទ្រព្យ"
                DgdSP.Columns(1).Name = "ឈ្មោះទ្រព្យ"
                DgdSP.Columns(2).Name = "រង្វាស់"
                DgdSP.Columns(3).Name = "ចំនួនសរុប"
                DgdSP.Columns(4).Name = "តម្លៃរាយ(៛)"
                DgdSP.Columns(5).Name = "តម្លៃសរុប(៛)"
                DgdSP.Columns(6).Name = "តម្លៃរាយ($)"
                DgdSP.Columns(7).Name = "តម្លៃសរុប($)"
                DgdSP.Columns(8).Name = "ថ្ងៃបញ្ចូល"
            If RadAll.Checked Then
                AddToGrid(DgdSP, 9, "select a.assetID,b.assetName,b.oumName,a.amount,case when currency='Riel      ' then unitprice else 0 end Riel,  case when currency='Riel      ' then unitprice*amount else 0 end TotalRiel,case when currency='Dollar    ' then unitprice else 0 end Dollar,case when currency='Dollar    ' then unitprice*amount else 0 end  TatalDollar,a.dates  from tblAssetAdd a inner join tblAsset b on a.assetID=b.assetID and a.BrID=b.BrID where cast(a.dates as date) between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "'  order by a.assetID")
            ElseIf RadCode.Checked Then
                AddToGrid(DgdSP, 9, "select a.assetID,b.assetName,b.oumName,a.amount,case when currency='Riel      ' then unitprice else 0 end Riel,  case when currency='Riel      ' then unitprice*amount else 0 end TotalRiel,case when currency='Dollar    ' then unitprice else 0 end Dollar,case when currency='Dollar    ' then unitprice*amount else 0 end  TatalDollar ,a.dates  from tblAssetAdd a inner join tblAsset b on a.assetID=b.assetID and a.BrID=b.BrID where cast(a.dates as date) between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "' and   a.assetID='" & Me.TextBox1.Text & "' order by a.assetID")
            If result > 0 Then
                resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
                'MessageBox.Show("You've selected date not correct, please check again before reload record!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            End If
            'ElseIf ComboBox2.SelectedIndex = 2 Then
            '    DgdSP.ColumnCount = 9
            '    DgdSP.Columns(0).Name = "កូដទ្រព្យ"
            '    DgdSP.Columns(1).Name = "ឈ្មោះទ្រព្យ"
            '    DgdSP.Columns(2).Name = "រង្វាស់"
            '    DgdSP.Columns(3).Name = "ចំនួនសរុប"
            '    DgdSP.Columns(4).Name = "តម្លៃរាយ(៛)"
            '    DgdSP.Columns(5).Name = "តម្លៃសរុប(៛)"
            '    DgdSP.Columns(6).Name = "តម្លៃរាយ($)"
            '    DgdSP.Columns(7).Name = "តម្លៃសរុប($)"
            '    DgdSP.Columns(8).Name = "ថ្ងៃបញ្ចូល"

            'AddToGrid(DgdSP, 7, "select c.staffid,c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel,SUM(dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where c.BrID='" & BrID & "' group by c.StaffName ,a.assetID ,b.assetName,c.StaffID order by c.staffID ")
        ElseIf ComboBox2.SelectedIndex = 3 Then
            DgdSP.Columns.Clear()
            DgdSP.ColumnCount = 12
            DgdSP.Columns(0).Name = "កូដមន្ត្រីឥណទាន"
            DgdSP.Columns(1).Name = "ឈ្មោះមន្ត្រីឥណទាន"
            DgdSP.Columns(2).Name = "តួនាទី"
            DgdSP.Columns(3).Name = "កូដទ្រព្យ"
            DgdSP.Columns(4).Name = "ឈ្មោះទ្រព្យ"
            DgdSP.Columns(5).Name = "រង្វស់"
            DgdSP.Columns(6).Name = "ចំនួនដក"
            DgdSP.Columns(7).Name = "តម្លៃរាយ(៛)"
            DgdSP.Columns(8).Name = "តម្លៃសរុប(៛)"
            DgdSP.Columns(9).Name = "តម្លៃរាយ($)"
            DgdSP.Columns(10).Name = "តម្លៃសរុប($)"
            DgdSP.Columns(11).Name = "ថ្ងៃដក"
            If RadAll.Checked Then
                AddToGrid(DgdSP, 12, "select  c.staffid,c.StaffName,c.Position ,a. assetID,b.assetName ,b.oumName,Amount,Riel,(Amount*Riel)TotalRiel,Dollar,(Amount*dollar) TotalDollar, a.dates from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID and a.BrID=b.BrID inner join tblStaff c on a.staffID =c.StaffID  where cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & frmMain.lblCode.Text & "' order by c.staffID")
            ElseIf RadCode.Checked Then
                AddToGrid(DgdSP, 12, "select  c.staffid,c.StaffName,c.Position ,a. assetID,b.assetName ,b.oumName,(amount)  Amount,Riel,(Amount*Riel) TotalRiel,Dollar,(Amount*dollar) TotalDollar, a.dates from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID and a.BrID=b.BrID inner join tblStaff c on a.staffID =c.StaffID where cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & BrID & "' and a.staffID='" & Me.TextBox1.Text & "'  order by c.staffID")
                If result > 0 Then
                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
                    'MessageBox.Show("You've selected date not correct, please check again before reload record!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                    'Else
                    '    DgdSP.Columns.Clear()
                    '    DgdSP.ColumnCount = 12
                    '    DgdSP.Columns(0).Name = "កូដមន្ត្រីឥណទាន"
                    '    DgdSP.Columns(1).Name = "ឈ្មោះមន្ត្រីឥណទាន"
                    '    DgdSP.Columns(2).Name = "តួនាទី"
                    '    DgdSP.Columns(3).Name = "កូដទ្រព្យ"
                    '    DgdSP.Columns(4).Name = "ឈ្មោះទ្រព្យ"
                    '    DgdSP.Columns(5).Name = "រង្វស់"
                    '    DgdSP.Columns(6).Name = "ចំនួនដកសរុប"
                    '    DgdSP.Columns(7).Name = "តម្លៃរាយ(៛)"
                    '    DgdSP.Columns(8).Name = "តម្លៃសរុប(៛)"
                    '    DgdSP.Columns(9).Name = "តម្លៃរាយ($)"
                    '    DgdSP.Columns(10).Name = "តម្លៃសរុប($)"
                    '    DgdSP.Columns(11).Name = "ថ្ងៃដក"

                    'AddToGrid(DgdSP, 7, "select  c.staffid,c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel,SUM(dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & BrID & "' group by c.StaffName ,a.assetID ,b.assetNamee,c.staffID order by c.staffID")
                End If
            End If
        ElseIf ComboBox2.SelectedIndex = 4 Then
            DgdSP.Columns.Clear()
            DgdSP.ColumnCount = 4
            DgdSP.Columns(0).Name = "កូដទ្រព្យ"
            DgdSP.Columns(1).Name = "ឈ្មោះទ្រព្យ"
            DgdSP.Columns(2).Name = "រង្វាស់"
            DgdSP.Columns(3).Name = "ចំនួនដកសរុប"
            'AddToGrid(DgdSP, 7, "select c.staffid,c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel,SUM(dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where c.BrID='" & BrID & "' group by c.StaffName ,a.assetID ,b.assetName,c.StaffID order by c.staffID ")
            If RadAll.Checked Then
                AddToGrid(DgdSP, 4, "select a.assetID,a.assetName,a.oumName,isnull(b.Amount,0) Amt from tblasset a left join(select b.assetID,b.BrID,sum(isnull(amount,0)) Amount from  tblAssetDetail b  inner join tblStaff c on b.staffID=c.StaffID and b.BrID=c.BrID where cast(b.dates as date)between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "'and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "'and b.BrID='" & BrID & "' group by b.BrID, b.assetID ) b on a.BrID=b.BrID and a.assetID=b.assetID where a.BrID='" & BrID & "' order by assetID")
                'DgdSP.Columns.Clear()
                'ElseIf RadCode.Checked Then
                '    AddToGrid(DgdSP, 11, "select  c.staffid,c.StaffName,c.Position ,a. assetID,b.assetName ,b.oumName,sum(amount)  Amount,Riel,SUM(Amount*Riel) TotalRiel,Dollar,SUM(Amount*dollar) TotalDollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID and a.BrID=b.BrID inner join tblStaff c on a.staffID =c.StaffID where cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & BrID & "' and a.staffID='" & Me.TextBox1.Text & "' group by c.StaffName ,a.assetID ,b.assetName,c.staffID,oumName,Riel,Dollar,c.Position order by c.staffID")
                If result > 0 Then
                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
                    'MessageBox.Show("You've selected date not correct, please check again before reload record!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                    'Else
                    '    DgdSP.Columns.Clear()
                    '    DgdSP.ColumnCount = 11
                    '    DgdSP.Columns(0).Name = "កូដមន្ត្រីឥណទាន"
                    '    DgdSP.Columns(1).Name = "ឈ្មោះមន្ត្រីឥណទាន"
                    '    DgdSP.Columns(2).Name = "តួនាទី"
                    '    DgdSP.Columns(3).Name = "កូដទ្រព្យ"
                    '    DgdSP.Columns(4).Name = "ឈ្មោះទ្រព្យ"
                    '    DgdSP.Columns(5).Name = "រង្វស់"
                    '    DgdSP.Columns(6).Name = "ចំនួនដកសរុប"
                    '    DgdSP.Columns(7).Name = "តម្លៃរាយ(៛)"
                    '    DgdSP.Columns(8).Name = "តម្លៃសរុប(៛)"
                    '    DgdSP.Columns(9).Name = "តម្លៃរាយ($)"
                    '    DgdSP.Columns(10).Name = "តម្លៃសរុប($)"

                    'AddToGrid(DgdSP, 7, "select  c.staffid,c.StaffName ,a. assetID,b.assetName ,sum(amount)  Amount,SUM(Riel) Riel,SUM(dollar) Dollar from tblAssetDetail a inner join tblAsset b on a.assetID =b.assetID inner join tblStaff c on a.staffID =c.StaffID where cast(a.dates as date) between '" & FormatDateTime(DateStart.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(DateEnd.Value, DateFormat.ShortDate) & "' and c.BrID='" & BrID & "' group by c.StaffName ,a.assetID ,b.assetNamee,c.staffID order by c.staffID")
                End If
            End If
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ComboBox2.SelectedIndex = 0 Then
            toExcelAsset()
        ElseIf ComboBox2.SelectedIndex = 1 Then
            Dim rowsTotal, colsTotal As Short
            Dim I, j, iC As Short
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim xlApp As New X.Application
            Try
                Dim excelBook As X.Workbook = xlApp.Workbooks.Add
                Dim excelWorksheet As X.Worksheet = CType(excelBook.Worksheets(1), X.Worksheet)
                xlApp.Visible = True
                rowsTotal = DgdSP.RowCount - 1
                colsTotal = DgdSP.Columns.Count - 1
                With excelWorksheet
                    .Cells.Select()
                    .Cells.Delete()
                    For iC = 0 To colsTotal
                        .Cells(1, iC + 1).Value = DgdSP.Columns(iC).HeaderText
                    Next
                    For I = 0 To rowsTotal
                        For j = 0 To colsTotal
                            .Cells(I + 2, j + 1).value = DgdSP.Rows(I).Cells(j).Value
                        Next j
                    Next I
                    .Rows("1:1").Font.FontStyle = "Regular"
                    .Rows("1:1").Font.Size = 10
                    .Cells.Columns.AutoFit()
                    .Cells.Select()
                    .Cells.EntireColumn.AutoFit()
                    .Cells(1, 1).Select()
                    .Cells.Font.Name = "Khmer os battambang"
                    .Cells.Font.Size = 10
                End With
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
            Finally
                'RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                xlApp = Nothing
            End Try
        Else
            Dim rowsTotal, colsTotal As Short
            Dim I, j, iC As Short
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim xlApp As New X.Application
            Try
                Dim excelBook As X.Workbook = xlApp.Workbooks.Add
                Dim excelWorksheet As X.Worksheet = CType(excelBook.Worksheets(1), X.Worksheet)
                xlApp.Visible = True
                rowsTotal = DgdSP.RowCount - 1
                colsTotal = DgdSP.Columns.Count - 1
                With excelWorksheet
                    .Cells.Select()
                    .Cells.Delete()
                    For iC = 0 To colsTotal
                        .Cells(1, iC + 1).Value = DgdSP.Columns(iC).HeaderText
                    Next
                    For I = 0 To rowsTotal
                        For j = 0 To colsTotal
                            .Cells(I + 2, j + 1).value = DgdSP.Rows(I).Cells(j).Value
                        Next j
                    Next I
                    .Rows("1:1").Font.FontStyle = "Regular"
                    .Rows("1:1").Font.Size = 10
                    .Cells.Columns.AutoFit()
                    .Cells.Select()
                    .Cells.EntireColumn.AutoFit()
                    .Cells(1, 1).Select()
                    .Cells.Font.Name = "Khmer os battambang"
                    .Cells.Font.Size = 10
                End With
            Catch ex As Exception
                MsgBox("Export Excel Error " & ex.Message)
            Finally
                'RELEASE ALLOACTED RESOURCES
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
                xlApp = Nothing
            End Try
        End If
    End Sub
    Sub toExcelAsset()
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New X.Application
        Try
            Dim excelBook As X.Workbook = xlApp.Workbooks.Add
            Dim excelWorksheet As X.Worksheet = CType(excelBook.Worksheets(1), X.Worksheet)
            xlApp.Visible = True
            rowsTotal = DgdSP.RowCount - 1
            colsTotal = DgdSP.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                .Cells(1, iC + 1).Value = "របាយការណ៍ស្តីអំពីទ្រព្យ"
                For iC = 0 To colsTotal
                    .Cells(3, iC + 1).Value = DgdSP.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal
                    For j = 0 To colsTotal
                        .Cells(I + 4, j + 1).value = DgdSP.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Regular"
                .Rows("1:1").Font.Size = 10
                '.Cells.Columns.AutoFit()
                .Cells.Select()
                '.Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
                .Cells.Font.Name = "Khmer os battambang"
                .Cells.Font.Size = 10
            End With
        Catch ex As Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
    'Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
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

    Private Sub cmbBrand_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim ID As String = cmbBrand.SelectedValue
        'lblName.Text = getData("select Name from tblcompany1 where ID='" & ID & "'")
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Tblcompany1BindingSource_CurrentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tblcompany1BindingSource.CurrentChanged

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub lblFrom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblFrom.Click

    End Sub

    Private Sub lblTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblTo.Click

    End Sub

    Private Sub DateStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateStart.ValueChanged

    End Sub

    Private Sub DateEnd_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateEnd.ValueChanged

    End Sub

    Private Sub lblcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblcode.Click

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel3_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint

    End Sub


    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ComboBox2.SelectedIndex = 0 Then
            excelexportNormal1(DgdSP, frmMain.strPath & "\simple Excel\InStock.xlsx", 6, "E")
        ElseIf ComboBox2.SelectedIndex = 1 Then
            excelexformat(DgdSP, frmMain.strPath & "\simple Excel\User Stock.xlsx", 7, "k", "A", "f", "G", "K")
        ElseIf ComboBox2.SelectedIndex = 2 Then
            excelexformat(DgdSP, frmMain.strPath & "\simple Excel\intoStock.xlsx", 7, "I", "A", "D", "E", "I")
        ElseIf ComboBox2.SelectedIndex = 3 Then
            excelexformat(DgdSP, frmMain.strPath & "\simple Excel\User Stock dialy.xlsx", 7, "L", "A", "f", "G", "L")
        ElseIf ComboBox2.SelectedIndex = 4 Then
            excelexformat(DgdSP, frmMain.strPath & "\simple Excel\Summary Stock using by selected.xls", 6, "D", "A", "B", "C", "D")
        End If

    End Sub
    Sub excelexformat(ByVal DataGridView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer, ByVal endcolumnBorder As String, ByVal StartMergeCell As String, ByVal EndMergeCell As String, ByVal startColumnSum As String, ByVal EndColumnSum As String)
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
                .Cells(3, 1).value = frmMain.lblName.Text
                'If radDay.Checked Then
                .Cells(4, 1).value = "កាលបរិច្ឆេទចាប់ពី " & DateStart.Text & " ដល់ " & DateEnd.Text
                'End If
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                        'MessageBox.Show(I & " " & j)
                    Next (j)
                Next I
                '-----------------------------------------
                Dim selection As Excel.Range
                selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & I + startRow)
                selection.Borders.Weight = Excel.XlBorderWeight.xlThin
                '------------------------------------------
                'MessageBox.Show(I)
                excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).MergeCells = True
                excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).Value = "សរុប"
                '----excelWorksheet.Range(startColumnSum & j + 2 & ":" & EndColumnSum & j + 2).Value = "=SUM(" & startColumnSum & startRow & ":" & startColumnSum & j + 1 & ")"
                excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).Value = "=Sum(" & startColumnSum & startRow & ":" & startColumnSum & I + startRow - 1 & ")"
                .Cells(1, 1).Select()
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
    Sub excelexformat1(ByVal DataGridView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer, ByVal endcolumnBorder As String, ByVal StartMergeCell As String, ByVal EndMergeCell As String, ByVal startColumnSum As String, ByVal EndColumnSum As String)
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
                'If radDay.Checked Then
                .Cells(4, 1).value = "កាលបរិច្ឆេទចាប់ពី " & DateStart.Text & " ដល់ " & DateEnd.Text
                'End If
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                        'MessageBox.Show(I & " " & j)
                    Next (j)
                Next I
                '-----------------------------------------
                Dim selection As Excel.Range
                selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & I + startRow)
                selection.Borders.Weight = Excel.XlBorderWeight.xlThin
                '------------------------------------------
                'MessageBox.Show(I)
                excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).MergeCells = True
                excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).Value = "សរុប"
                '----excelWorksheet.Range(startColumnSum & j + 2 & ":" & EndColumnSum & j + 2).Value = "=SUM(" & startColumnSum & startRow & ":" & startColumnSum & j + 1 & ")"
                excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).Value = "=Sum(" & startColumnSum & startRow & ":" & startColumnSum & I + startRow - 1 & ")"
                .Cells(1, 1).Select()
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
    Public Sub excelexportNormal1(ByVal DataGridView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer, ByVal endcolumnBorder As String)
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
                '.Cells.Select()
                '.Cells.Delete()
                'For iC = 0 To colsTotal
                '    .Cells(5, iC + 1).Value = DataGridView1.Columns(iC).HeaderText
                'Next
                .Cells(2, 1).value = frmMain.lblName.Text
                .Cells(3, 1).value = "របាយការណ៍សម្ភារះនៅសល់ក្នុងស្តុកប្រចាំខែ " & Month(Now) & " ឆ្នាំ " & Year(Now)
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                        'MessageBox.Show(I & " " & j)
                    Next (j)
                Next I
                '.Rows("1:1").Font.FontStyle = "Bold"
                '.Rows("1:1").Font.Size = 12
                '.Rows("1:1").font.size =
                '.Cells.Columns.AutoFit()
                '.Cells.Select()
                '-----------------------------------------
                Dim selection As Excel.Range
                selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & I + startRow)
                selection.Borders.Weight = Excel.XlBorderWeight.xlThin
                '------------------------------------------
                ''MessageBox.Show(I)
                'excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).MergeCells = True
                'excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).Value = "Total:"
                ''----excelWorksheet.Range(startColumnSum & j + 2 & ":" & EndColumnSum & j + 2).Value = "=SUM(" & startColumnSum & startRow & ":" & startColumnSum & j + 1 & ")"
                'excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).Value = "=Sum(" & startColumnSum & startRow & ":" & startColumnSum & I + startRow - 1 & ")"
                ''excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                ''excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & I + startRow).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
                'excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & j + 2).Font.Bold = True
                '.Cells(1, 1).Select()
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
End Class