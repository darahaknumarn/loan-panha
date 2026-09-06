Imports Microsoft.Office.Interop
Imports System.IO

Public Class frmrptSang
    ' Public Function ChangeVolumeLabel(ByVal DriveLetter As String, _
    'ByVal NewDriveVolume As String) As Boolean

    '     'Example:
    '     'ChangeVolumeLabel("C:\", "CDrive")


    '     Dim lAns As Long

    '     On Error Resume Next
    '     lAns = SetVolumeLabel(DriveLetter, NewDriveVolume)
    '     ChangeVolumeLabel = (lAns <> 0)

    ' End Function

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        'Try
        '    For Each dir As String In Directory.GetDirectories(sDir)
        '        For Each file In Directory.GetFiles(dir, "yourfilename.exe")
        '            lstFilesFound.Items.Add(file)
        '        Next
        '        DirSearch(dir)
        '    Next
        'Catch ex As Exception
        '    Debug.WriteLine(ex.Message)
        'End Try


        Me.Close()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        txtStaffID.ReadOnly = False
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        txtStaffID.ReadOnly = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
        Dim result1 As Integer = DateTime.Compare(date1, date2)
        Dim BrID As String = frmMain.lblCode.Text
        '-----------------------------te--------------------------------------------------------------------------
        If BrID = "" Then
            resultError = frmMessageError.ShowBoxError("កូដសាខាមិនអាចគ្មានបានទេ", "គ្មានកូដសាខា")
            'MessageBox.Show("Please input correct brand ID before search.")
        Else
            If RadioButton1.Checked And radMonth.Checked Then
                If result1 > 0 Then
                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
                    Return
                Else
                    datagrid2()
                    AddToGrid(DataGridView1, 10, "select a.No,SUM(amount) TotalAmount,a.unitPrice,SUM(a.unitprice*amount) totalKH,priceUSD, SUM(amount*PriceUSD)as TotalUSD,d.KmOut, c.KmIn,sum(a.Km) Km,case when SUM (Amount)=0 then sum(Km)/1 else sum(Km)/sum(Amount) end 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID inner join (select MIN( kmout)kmout,No,BrID from tblSang where Date between '" & date1 & "' and '" & date2 & "' group by No,BrID )d on a.BrID=d.BrID and a.No=d.No inner join ( select max(KmIn)KmIn,No,BrID from tblSang where Date between '" & date1 & "' and '" & date2 & "'group by No,BrID ) c on a.BrID=c.BrID  and a.No=c.No where a.Date between '" & date1 & "' and '" & date2 & "'and a.BrID='" & frmMain.lblCode.Text & "'group by unitPrice,PriceUSD,c.KmIn,d.KmOut,a.BrID,a.No")
                    'AddToGrid(DataGridView1, 13, "select b.staffid,b.StaffName ,b.Position,a.No,SUM(amount) TotalAmount,a.unitPrice,SUM(a.unitprice*amount) totalKH,priceUSD, SUM(amount*PriceUSD) as TotalUSD,d.KmOut,c.KmIn,sum(a.Km) Km,case when SUM (Amount)=0 then sum(Km)/1 else sum(Km)/sum(Amount) end 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID inner join (select MIN( kmout)kmout,StaffID,BrID from tblSang where Date between '" & date1 & "' and '" & date2 & "'  group by StaffID,BrID )d on a.BrID=d.BrID and a.StaffID=d.StaffID inner join ( select max(KmIn)KmIn,StaffID,BrID from tblSang where Date between '" & date1 & "' and '" & date2 & "'  group by StaffID,BrID ) c on a.BrID=c.BrID  and a.StaffID=c.StaffID where a.Date between '" & date1 & "' and '" & date2 & "'and a.BrID='" & frmMain.lblCode.Text & "' group by b.StaffID ,b.StaffName ,a.No,b.Position, unitPrice,PriceUSD,c.KmIn,d.KmOut,a.BrID")
                    'AddToGrid(DataGridView1, 13, "select b.staffid,b.StaffName ,b.Position,b.MotoNo,SUM(amount) TotalAmount,a.unitPrice,SUM(a.unitprice*amount) totalKH,priceUSD,SUM(amount*PriceUSD) as TotalUSD,d.KmOut,c.KmIn,sum(a.Km )Km,sum(Km)/SUM(amount) 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID inner join  (select  kmout,StaffID,BrID from tblSang where DATE='" & date1 & "' ) d on a.BrID=d.BrID and a.StaffID=d.StaffID inner join (select KmIn,StaffID,BrID from tblSang where DATE='" & date2 & "' ) c on a.BrID=c.BrID and a.StaffID=c.StaffID where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "' group by b.StaffID ,b.StaffName ,b.MotoNo,b.Position,unitPrice,PriceUSD,c.KmIn,d.KmOut order by StaffID ")
                    'AddToGrid(DataGridView1, 13, "select b.staffid,b.StaffName ,b.Position,b.MotoNo,SUM(amount) TotalAmount,a.unitPrice,SUM(a.unitprice*amount) totalKH,priceUSD,SUM(amount*PriceUSD) as TotalUSD,(select top 1 kmout from tblSang where DATE='" & date1 & "' order by id) KmOut,(select top 1 KmIn from tblSang where DATE='" & date2 & "' order by id desc) KmIn,sum(a.Km )Km,sum(Km)/SUM(amount) 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "'  group by b.StaffID ,b.StaffName ,b.MotoNo,b.Position,unitPrice,PriceUSD order by StaffID ")
                End If
            ElseIf RadioButton1.Checked And radDay.Checked Then
                If result1 > 0 Then
                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
                    Return
                Else
                    datagrid1()
                    AddToGrid(DataGridView1, 13, "select  b.staffid,b.StaffName,a.Position,a.No ,amount,isnull(unitprice,0) PriceKH,isnull(amount*unitPrice,0) TotalKH,isnull(PriceUSD,0)PriceUSD,isnull(PriceUSD*Amount,0) TotalUSD,a.KmOut,a.KmIn,a.Km,date from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID    where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "'  order by StaffID")
                    'AddToGrid(DataGridView1, 13, "select b.staffid,b.StaffName ,b.Position,b.MotoNo,SUM(amount) TotalAmount,a.unitPrice,SUM(a.unitprice) totalKH,priceUSD,SUM(amount*PriceUSD) as TotalUSD,(select top 1 kmout from tblSang where DATE='" & date1 & "' order by id) KmOut,(select top 1 KmIn from tblSang where DATE='" & date2 & "' order by id desc) KmIn,sum(a.Km )Km,sum(Km)/SUM(amount) 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "'  group by b.StaffID ,b.StaffName ,b.MotoNo,b.Position,unitPrice,PriceUSD order by StaffID ")
                End If
            ElseIf RadioButton2.Checked And radDay.Checked Then
                If result1 > 0 Then
                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
                    Return
                Else
                    datagrid1()
                    AddToGrid(DataGridView1, 13, "select  b.staffid,b.StaffName,a.Position,a.No,amount,isnull(unitprice,0) PriceKH,isnull(amount*unitPrice,0) TotalKH,isnull(PriceUSD,0)PriceUSD,isnull(PriceUSD*Amount,0) TotalUSD,a.KmOut,a.KmIn,a.Km,date from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID    where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "' and b.staffID='" & txtStaffID.Text & "'  order by StaffID")
                End If
            ElseIf RadioButton2.Checked And radMonth.Checked Then

                If result1 > 0 Then
                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
                    Return
                Else
                    datagrid()
                    AddToGrid(DataGridView1, 13, "select b.staffid,b.StaffName,b.Position,a.No,SUM(amount) TotalAmount,a.unitPrice,SUM(a.unitprice*amount) totalKH,priceUSD ,SUM(amount*PriceUSD) as TotalUSD,d.KmOut,c.KmIn, sum(a.Km) Km,case when SUM (Amount)=0 then sum(Km)/1 else sum(Km)/sum(Amount) end 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID inner join(select MIN(kmout)kmout,StaffID,BrID from tblSang where Date between '" & date1 & "' and '" & date2 & "' group by StaffID,BrID )d on a.BrID=d.BrID and a.StaffID=d.StaffID  inner join ( select max(KmIn)KmIn,StaffID,BrID  from tblSang where Date between '" & date1 & "' and '" & date2 & "' group by StaffID,BrID ) c on a.BrID=c.BrID and a.StaffID=c.StaffID where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "' and b.staffID='" & txtStaffID.Text & "'  group by b.StaffID ,b.StaffName ,a.No,b.Position,unitPrice,PriceUSD,c.KmIn,d.KmOut,a.BrID")
                    'AddToGrid(DataGridView1, 13, "select b.staffid,b.StaffName ,b.Position,b.MotoNo,SUM(amount) TotalAmount,a.unitPrice,SUM(a.unitprice*amount) totalKH,priceUSD,SUM(amount*PriceUSD) as TotalUSD,d.KmOut,c.KmIn,sum(a.Km )Km,sum(Km)/SUM(amount) 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID inner join  (select MIN( kmout)kmout,StaffID,BrID from tblSang where DATE>='" & date1 & "' group by StaffID,BrID ) d on a.BrID=d.BrID and a.StaffID=d.StaffID inner join ( select max(KmIn)KmIn,StaffID,BrID from tblSang where DATE<='" & date2 & "' group by StaffID,BrID ) c on a.BrID=c.BrID and a.StaffID=c.StaffID where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "' and a.staffID='" & txtStaffID.Text & "' group by b.StaffID ,b.StaffName ,b.MotoNo,b.Position,unitPrice,PriceUSD,c.KmIn,d.KmOut order by StaffID ")
                    'AddToGrid(DataGridView1, 13, "select b.staffid,b.StaffName ,b.Position,b.MotoNo,SUM(amount) TotalAmount,a.unitPrice,SUM(amount*a.unitprice) totalKH,priceUSD,SUM(amount*PriceUSD) as TotalUSD,(select top 1 kmout from tblSang where DATE='" & date1 & "' order by id) KmOut,(select top 1 KmIn from tblSang where DATE='" & date2 & "' order by id desc) KmIn,sum(a.Km )Km,sum(Km)/SUM(amount) 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "' and b.Staffid='" & txtStaffID.Text & "' group by b.StaffID ,b.StaffName ,b.MotoNo,b.Position,unitPrice,PriceUSD order by StaffID ")
                End If
            Else
                If result1 > 0 Then
                    resultError = frmMessageError.ShowBoxError("ការជ្រើសរើសថ្ងៃខែរបស់អ្នកខុសហើយ សូមពិនិត្យឡើងវិញ។", "ថ្ងៃខែខុស")
                    Return
                Else
                    datagrid1()
                    AddToGrid(DataGridView1, 15, "select b.staffid,b.StaffName,b.Position,b.MotoNo,b.MotoNo2,b.MotoNo3,SUM(amount) TotalAmount,a.unitPrice,SUM(a.unitprice*amount) totalKH,priceUSD,SUM(amount*PriceUSD) as TotalUSD,d.KmOut,c.KmIn,sum(a.Km )Km,sum(Km)/SUM(amount) 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID inner join  (select MIN( kmout)kmout,StaffID,BrID from tblSang where  Date between '" & date1 & "' and '" & date2 & "' group by StaffID,BrID ) d on a.BrID=d.BrID and a.StaffID=d.StaffID inner join ( select max(KmIn)KmIn,StaffID,BrID from tblSang where Date between '" & date1 & "' and '" & date2 & "'group by StaffID,BrID ) c on a.BrID=c.BrID and a.StaffID=c.StaffID where a.Date between() '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "' and  a.staffID='" & txtStaffID.Text & "' group by b.StaffID ,b.StaffName ,b.MotoNo,b.Position, unitPrice,PriceUSD,c.KmIn,d.KmOut order by StaffID ")
                    'AddToGrid(DataGridView1, 13, "select b.staffid,b.StaffName ,b.Position,b.MotoNo,SUM(amount) TotalAmount,a.unitPrice,SUM(a.unitprice) totalKH,priceUSD,SUM(amount*PriceUSD) as TotalUSD,(select top 1 kmout from tblSang where DATE='" & date1 & "' order by id) KmOut,(select top 1 KmIn from tblSang where DATE='" & date2 & "' order by id desc) KmIn,sum(a.Km )Km,sum(Km)/SUM(amount) 'Km/L' from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & frmMain.lblCode.Text & "' and b.Staffid='" & txtStaffID.Text & "' group by b.StaffID ,b.StaffName ,b.MotoNo,b.Position,unitPrice,PriceUSD order by StaffID ")
                End If
            End If
        End If
    End Sub

    Private Sub rptSang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'CheckBox1.Checked = True
        'TODO: This line of code loads data into the 'BarCodeDataSet2.tblcompany1' table. You can move, or remove it, as needed.
        'Me.Tblcompany1TableAdapter.Fill(Me.BarCodeDataSet2.tblcompany1)
        SetFontDatagrid(DataGridView1)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ToExcel(DataGridView1)
    End Sub

    Private Function Tblcompany1TableAdapter() As Object
        Throw New NotImplementedException
    End Function
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        'MessageBox.Show(strPath.ToString)
        If radMonth.Checked = True Then
            'excelexport(DataGridView1, "D:\new\simple Excel\simple Excel\របាយការណ៍ប្រើប្រាស់សាំងប្រចាំខែ.xls", 6, "O", "A", "E", "F", "O")
            'excelexportMonth(DataGridView1, "path\simple Excel\របាយការណ៍ប្រើប្រាស់សាំងប្រចាំខែ.xls", 7, "K", "A", "D", "F", "K")
            excelexportMonth(DataGridView1, frmMain.strPath & "\simple Excel\របាយការណ៍ប្រើប្រាស់សាំងប្រចាំខែ.xls", 7, "K", "A", "D", "F", "K")
        Else
            excelexportMonth(DataGridView1, frmMain.strPath & "\simple Excel\របាយការណ៍ប្រើប្រាស់សាំងប្រចាំថ្ងៃ.xls", 7, "N", "A", "D", "F", "N")
        End If
    End Sub
    Sub datagrid()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 13
        DataGridView1.Columns(0).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(2).Name = "តួនាទី"
        DataGridView1.Columns(3).Name = "ស្លាកលេខ"
        DataGridView1.Columns(4).Name = "ចំនួនលីត្រ"
        DataGridView1.Columns(5).Name = "តំលៃរាយរៀល"
        DataGridView1.Columns(6).Name = "តំលៃសរុបរៀល"
        DataGridView1.Columns(7).Name = "តំលៃរាយដុល្លា"
        DataGridView1.Columns(8).Name = "តំលៃសរុបដុល្លា"
        DataGridView1.Columns(9).Name = "គីឡូចេញ"
        DataGridView1.Columns(10).Name = "គីឡូចូល"
        DataGridView1.Columns(11).Name = "គីឡូសរុប"
        DataGridView1.Columns(12).Name = "ចំនួនគីឡូក្នុងមួយលីត្រ"
    End Sub
    Sub datagrid1()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 13
        DataGridView1.Columns(0).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(2).Name = "តួនាទី"
        DataGridView1.Columns(3).Name = "ស្លាកលេខ"
        DataGridView1.Columns(4).Name = "ចំនួនលីត្រ"
        DataGridView1.Columns(5).Name = "តំលៃរាយរៀល"
        DataGridView1.Columns(6).Name = "តំលៃសរុបរៀល"
        DataGridView1.Columns(7).Name = "តំលៃរាយដុល្លា"
        DataGridView1.Columns(8).Name = "តំលៃសរុបដុល្លា"
        DataGridView1.Columns(9).Name = "គីឡូចេញ"
        DataGridView1.Columns(10).Name = "គីឡូចូល"
        DataGridView1.Columns(11).Name = "គីឡូសរុប"
        DataGridView1.Columns(12).Name = "កាលបរិច្ឆេត"
    End Sub
    Sub datagrid2()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 10
        DataGridView1.Columns(0).Name = "ស្លាកលេខ"
        DataGridView1.Columns(1).Name = "ចំនួនលីត្រ"
        DataGridView1.Columns(2).Name = "តំលៃរាយរៀល"
        DataGridView1.Columns(3).Name = "តំលៃសរុបរៀល"
        DataGridView1.Columns(4).Name = "តំលៃរាយដុល្លា"
        DataGridView1.Columns(5).Name = "តំលៃសរុបដុល្លា"
        DataGridView1.Columns(6).Name = "គីឡូចេញ"
        DataGridView1.Columns(7).Name = "គីឡូចូល"
        DataGridView1.Columns(8).Name = "គីឡូសរុប"
        DataGridView1.Columns(9).Name = "ចំនួនគីឡូក្នុងមួយលីត្រ"
    End Sub
    Public Sub excelexportMonth(ByVal DataGridView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer, ByVal endcolumnBorder As String, ByVal StartMergeCell As String, ByVal EndMergeCell As String, ByVal startColumnSum As String, ByVal EndColumnSum As String)
        'MessageBox.Show(SampleLocation)
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
                'Nexta

                .Cells(3, 1).value = frmMain.lblName.Text

                .Cells(4, 1).value = "កាលបរិច្ឆេទចាប់ពី " & DateTimePicker1.Text & " ដល់ " & DateTimePicker2.Text

                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                        'MessageBox.Show(I & " " & j)
                    Next (j)
                Next I
                '.Rows("1:1").Font.FontStyle = "Bold"
                '.Rows("1:1").Font.Size = 12
                ''.Rows("1:1").font.size =
                '.Cells.Columns.AutoFit()
                '.Cells.Select()
                '-----------------------------------------
                Dim selection As Excel.Range
                selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & I + startRow)
                selection.Borders.Weight = Excel.XlBorderWeight.xlThin
                '------------------------------------------
                'MessageBox.Show(I)
                excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).MergeCells = True
                excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).Value = "សរុប"
                '----excelWorksheet.Range(startColumnSum & j + 2 & ":" & EndColumnSum & j + 2).Value = "=SUM(" & startColumnSum & startRow & ":" & startColumnSum & j + 1 & ")"
                excelWorksheet.Range(startColumnSum & I + startRow & ":" & "I" & I + startRow).Value = "=Sum(" & startColumnSum & startRow & ":" & startColumnSum & I + startRow - 1 & ")"
                'excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                'excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & I + startRow).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
                'excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & j + 2).Font.Bold = True
                .Cells(1, 1).Select()
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
End Class