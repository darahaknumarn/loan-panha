Imports System.Data.SqlClient
Imports System.Net.Mime.MediaTypeNames
Imports System.IO
Imports Microsoft.Office.Interop
Imports X = Microsoft.Office.Interop.Excel
Imports System.Data
Public Class frmResultReport
    Private Sub frmResultReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblEndDate.Visible = True
        If frmReport.DateTimePicker2.Visible = False Then
            lblEndDate.Text = ""
        Else
            lblEndDate.Text = frmReport.DateTimePicker2.Value.ToShortDateString
        End If
        lblStartDate.Text = frmReport.DateTimePicker1.Value.ToShortDateString
        lblNameReport.Text = frmReport.Text
        SetFontDatagrid(DataGridView1)
        If lblNameReport.Text = "ត្រូវយកប្រាក់ពីអតិថិជន" Then '----- ------------------     Loan To Repay --------------------------'
            ForGrid(Me.DataGridView1, "exec sp_rptGetLoanToRepay '" & lblStartDate.Text & "','" & lblEndDate.Text & "','All','" & frmReport.cboBranch.Text & "','All','" & frmReport.cboEmployee.Text & "'")

        ElseIf lblNameReport.Text = "LoanToRepayByBranch" Then
            ForGrid(Me.DataGridView1, "exec sp_rptGetLoanToRepayByBrand '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "Bank Transaction" Then '----------------- Bank Transaction ------------------'
            ShowDataGrid(Me.DataGridView1, "exec sp_rptBank '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "បានយកប្រាក់ពីអតិថិជន" Then '------------------------ Laon Paid Detail ------------------'
            ShowDataGrid(DataGridView1, "exec sp_rptGetLoanPaid '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "','" & frmReport.cboEmployee.Text & "',N'" & frmReport.cboCurrency.Text & "'")
        ElseIf lblNameReport.Text = "SpecialRepay" Then '------------------------ Laon Paid Detail ------------------'
            ShowDataGrid(DataGridView1, "exec sp_rptGetLoanPaidSpecial '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "','" & frmReport.cboEmployee.Text & "',N'" & frmReport.cboCurrency.Text & "'")



        ElseIf lblNameReport.Text = "Loan Summary By CO" Then '----------------Summary By CO ---------------------'
            ShowDataGrid(DataGridView1, "exec sp_rptLDSummaryByCOWithoutWriteoff '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "',N'" & frmReport.cboCurrency.Text & "'")

        ElseIf lblNameReport.Text = "LoanSummaryByBranch" Then '----------------Summary By CO ---------------------'
            ShowDataGrid(DataGridView1, "exec sp_rptLDSummaryByBranchWithoutWriteoff '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "',N'All'")

        ElseIf lblNameReport.Text = "ចំណាយគិតរំលស់" Then
            SP_ExWithout()
            AddToGrid(DataGridView1, 8, "exec sp_rptTotalAsset '" & lblStartDate.Text & "','" & lblEndDate.Text & "','All','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "ចំណាយសរុប" Then
            ForGrid(DataGridView1, "exec sp_rptAssetWithoutDepreciation '" & lblStartDate.Text & "','" & lblEndDate.Text & "','All','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "Loan Arrears Detail" Then
            ForGrid(DataGridView1, "exec sp_rptGetLoanArrearsAsofDateWithoutWriteoff '" & lblStartDate.Text & "','" & frmReport.cboBranch.Text & "',N'" & frmReport.cboCurrency.Text & "','" & frmReport.cboEmployee.Text & "'")

        ElseIf lblNameReport.Text = "បញ្ចេញឥណទាន" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_rptLoanDisbursment '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "','" & frmReport.cboEmployee.Text & "','" & frmReport.cboCurrency.Text & "'")

        ElseIf lblNameReport.Text = "ការដាក់ និងដកទុន" Then
            SP_Own()
            AddToGrid(DataGridView1, 5, "exec sp_rptOwner '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "ស្តីពីប្រាក់សង្ខេប" Then
            SP_EndBalance()
            AddToGrid(DataGridView1, 4, "exec sp_rptEndBalSumByEndDay '" & lblStartDate.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "Profit" Then
            ShowDataGrid(DataGridView1, "exec sp_rptProfit '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "'")
        ElseIf lblNameReport.Text = "ProfitByBrand" Then
            ShowDataGrid(DataGridView1, "exec sp_rptProfitByBrand '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "'")
        ElseIf lblNameReport.Text = "Loan Outstanding Detail" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_rptGetLoanOSWithoutWriteoff '" & lblStartDate.Text & "','" & frmReport.cboBranch.Text & "',N'" & frmReport.cboCurrency.Text & "','" & frmReport.cboEmployee.Text & "'")

        ElseIf lblNameReport.Text = "ចំណាយរំលស់ប្រចាំខែ" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_rptAssetWithDepreciation '" & lblStartDate.Text & "','" & lblEndDate.Text & "','All','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "សង្ខេបចំណាយមិនគិតរំលស់" Then
            ShowDataGrid(DataGridView1, "exec sp_rptSummaryExpenseWithoutDep '" & lblStartDate.Text & "','" & lblEndDate.Text & "','All','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "សមតុល្យនៃទ្រព្យ" Then
            ShowDataGrid(DataGridView1, "exec sp_rptBalanceAsset '" & lblEndDate.Text & "','All','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "អតិថិជនអសកម្មក្នុងគ្រា" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_rptCMInActive '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "','" & frmReport.cboEmployee.Text & "'")

        ElseIf lblNameReport.Text = "Interest Detail" Then '----------------------- Interest Detail ---------------'
            ShowDataGrid(Me.DataGridView1, "exec sp_rptLoanPaidDetail '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboEmployee.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "Interest Summary" Then '--------------------- Interest Summary ----------------'
            ShowDataGrid(Me.DataGridView1, "exec sp_rptLoanPaidSummary '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboEmployee.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "Loan Arrears Summary By CO" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_rptGetLoanArrearsAsofDateWithoutWriteoff1 '" & lblStartDate.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "LoanArrearsSummaryByBranch" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_rptGetLoanArrearsAsofDateWithoutWriteoffByBranch '" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "Loan Outstanding Summary by CO" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_rptGetLoanOSWithoutWriteoff1 '" & lblStartDate.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "Inactive Loan Income" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_rptInactiveLoanIncome '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "','All','" & frmReport.cboEmployee.Text & "'")

        ElseIf lblNameReport.Text = "Summary loan paid" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_75_rptLoanPaidSummary '" & lblStartDate.Text & "','" & lblEndDate.Text & "','All','" & frmReport.cboBranch.Text & "'")
            'sp_75_rptLoanPaidSummary()
        ElseIf lblNameReport.Text = "SummaryLoanPaidByBranch" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_75_rptLoanPaidSummaryByBranch '" & lblStartDate.Text & "','" & lblEndDate.Text & "','All','" & frmReport.cboBranch.Text & "'")
            'SummaryLoanPaidByBranch()
        ElseIf lblNameReport.Text = "LoanOutstandingSummaryByBrand" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_rptGetLoanOSWithoutWriteoff1 '" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "Summary loan to pay" Then
            ShowDataGrid(Me.DataGridView1, "exec sp_76_rptLoanToPaySummary '" & lblStartDate.Text & "','" & lblEndDate.Text & "','All','" & frmReport.cboBranch.Text & "'")

        ElseIf lblNameReport.Text = "Incorrect Repay" Then
            ShowDataGrid(Me.DataGridView1, "Exec sp_rptIncorrectRepay '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "'")
        End If
    End Sub
    '------"exec sp_rptEndBalSumByEndDay '" & date1 & "','" & frmMain.lblCode.Text & "'"
    Private Sub SP_ExWithDep()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 7
        DataGridView1.Columns(0).Name = "ថ្ងៃត្រូវចំណាយ"
        DataGridView1.Columns(1).Name = "កូដចំណាយ"
        DataGridView1.Columns(2).Name = "កូដទ្រព្យ"
        DataGridView1.Columns(3).Name = "ឈ្មោះទ្រព្យ"
        DataGridView1.Columns(4).Name = "បរិយាយ"
        DataGridView1.Columns(5).Name = "ចំនួនខ្មែរ"
        DataGridView1.Columns(6).Name = "ចំនួនដុល្លារ"
    End Sub
    Private Sub SP_SumEx()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 4
        DataGridView1.Columns(0).Name = "កូដចំណាយ"
        DataGridView1.Columns(1).Name = "ឈ្មោះចំណាយ"
        DataGridView1.Columns(2).Name = "ចំនួនរៀល"
        DataGridView1.Columns(3).Name = "ចំនួនដុល្លារ"
    End Sub
    Private Sub SP_Profit()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 5
        DataGridView1.Columns(0).Name = "No"
        DataGridView1.Columns(1).Name = "សាខា"
        DataGridView1.Columns(2).Name = "បរិយាយ"
        DataGridView1.Columns(3).Name = "ចំនួនរៀល"
        DataGridView1.Columns(4).Name = "ចំនួនដុល្លារ"
    End Sub
    Private Sub SP_LDOS()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 15
        DataGridView1.Columns(0).Name = "កិច្ចសន្យា"
        DataGridView1.Columns(1).Name = "សាខា"
        DataGridView1.Columns(2).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(3).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(4).Name = "កូដអតិថិជន"
        DataGridView1.Columns(5).Name = "ឈ្មោះអតិថិជន"
        DataGridView1.Columns(6).Name = "ទូរស័ព្ទ"
        DataGridView1.Columns(7).Name = "អសយដ្ឋាន"
        DataGridView1.Columns(8).Name = "ថ្ងៃបញ្ចេញ"
        DataGridView1.Columns(9).Name = "ថ្ងៃចុងក្រោយ"
        DataGridView1.Columns(10).Name = "ទឹកប្រាក់បញ្ចេញ"
        DataGridView1.Columns(11).Name = "ទឹកប្រាក់សកម្ម"
        DataGridView1.Columns(12).Name = "ការប្រាក់នៅសល់"
        DataGridView1.Columns(13).Name = "ប្រភេទ"
        DataGridView1.Columns(14).Name = "រូបបិយវត្ថុ"
    End Sub
    Private Sub SP_EndBalance()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 4
        DataGridView1.Columns(0).Name = "កូដសាខា"
        DataGridView1.Columns(1).Name = "ឈ្មោះសាខា"
        DataGridView1.Columns(2).Name = "លុយរៀល"
        DataGridView1.Columns(3).Name = "លុយដុល្លារ"
    End Sub
    Private Sub SP_Own()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 5
        DataGridView1.Columns(0).Name = "ថ្ងៃប្រតិបត្តិការ"
        DataGridView1.Columns(1).Name = "ប្រភេទ"
        DataGridView1.Columns(2).Name = "លុយដុល្លារ"
        DataGridView1.Columns(3).Name = "លុយរៀល"
        DataGridView1.Columns(4).Name = "បរិយាយ"
    End Sub
    Private Sub SP_ExWithout()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 8
        DataGridView1.Columns(0).Name = "ថ្ងៃចំណាយ"
        DataGridView1.Columns(1).Name = "កូដអូតូ"
        DataGridView1.Columns(2).Name = "កូដចំណាយ"
        DataGridView1.Columns(3).Name = "ឈ្មោះចំណាយ"
        DataGridView1.Columns(4).Name = "បរិយាយ"
        DataGridView1.Columns(5).Name = "ប្រាក់រៀល"
        DataGridView1.Columns(6).Name = "ប្រាក់ដុល្លារ"
        DataGridView1.Columns(7).Name = "សាខា"
    End Sub
    Private Sub SP_Debursh()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 19
        DataGridView1.Columns(0).Name = "លេខកិច្ចសន្យា"
        DataGridView1.Columns(1).Name = "កូដសាខា"
        DataGridView1.Columns(2).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(3).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(4).Name = "កូដអតិថិជន"
        DataGridView1.Columns(5).Name = "ឈ្មោះអតិថិជន"
        DataGridView1.Columns(6).Name = "លេខទូរស័ព្ទ"
        DataGridView1.Columns(7).Name = "អសយដ្ឋាន"
        DataGridView1.Columns(8).Name = "ទឹកប្រាក់បញ្ចេញ"
        DataGridView1.Columns(9).Name = "អត្រាសេវា"
        DataGridView1.Columns(10).Name = "ថ្លៃសេវាសរុប"
        DataGridView1.Columns(11).Name = "រូបបិយបណ្ណ"
        DataGridView1.Columns(12).Name = "ប្រភេទ"
        DataGridView1.Columns(13).Name = "ចំនួនកាលវិភាគ"
        DataGridView1.Columns(14).Name = "ការប្រាក់"
        DataGridView1.Columns(15).Name = "របៀបបង់"
        DataGridView1.Columns(16).Name = "ថ្ងៃបញ្ចេញ"
        DataGridView1.Columns(17).Name = "ថ្ងៃបង់"
        DataGridView1.Columns(18).Name = "រហូតដល់"
        'End Sub MySub()

    End Sub
    Private Sub SP_Summary()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 16
        DataGridView1.Columns(0).Name = "កូដសាខា"
        DataGridView1.Columns(1).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(2).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(3).Name = "ចំនួនបញ្ចេញ"
        DataGridView1.Columns(4).Name = "ទឹកប្រាក់បញ្ចេញ"
        DataGridView1.Columns(5).Name = "អតិថិជនថ្មី"
        DataGridView1.Columns(6).Name = "បង់ផ្តាច់"
        DataGridView1.Columns(7).Name = "អសកម្ម"
        DataGridView1.Columns(8).Name = "ចំនួនយឺត"
        DataGridView1.Columns(9).Name = "កាលវិភាគយឺត"
        DataGridView1.Columns(10).Name = "ទឹកប្រាក់យឺត"
        DataGridView1.Columns(11).Name = "ឥណទានសកម្ម"
        DataGridView1.Columns(12).Name = "ទឹកប្រាក់សកម្ម"
        DataGridView1.Columns(13).Name = "ប្រាក់ដើមយឺតតាមកាលវិភាគ"
        DataGridView1.Columns(14).Name = "ប្រាក់ដើមយឺតនៅអតិថិជន"
        DataGridView1.Columns(15).Name = "ភាគរយ"
    End Sub
    Private Sub SP_Arrea()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 18
        DataGridView1.Columns(0).Name = "លេខកិច្ចសន្យា"
        DataGridView1.Columns(1).Name = "កូដសាខា"
        DataGridView1.Columns(2).Name = "កូដអតិថិជន"
        DataGridView1.Columns(3).Name = "ឈ្មោះអតិថិជន"
        DataGridView1.Columns(4).Name = "លេខទូរស័ព្ទ"
        DataGridView1.Columns(5).Name = "អសយដ្ឋាន"
        DataGridView1.Columns(6).Name = "បុគ្គលិកចាស់"
        DataGridView1.Columns(7).Name = "បុគ្គលិកថ្មី"
        DataGridView1.Columns(8).Name = "ទឹកប្រាក់បញ្ចេញ"
        DataGridView1.Columns(9).Name = "ទឹកប្រាក់សកម្ម"
        DataGridView1.Columns(10).Name = "%ទឹកប្រាក់សកម្ម"
        DataGridView1.Columns(11).Name = "ថ្ងៃត្រូវបង់"
        DataGridView1.Columns(12).Name = "ចំនួនកាលវិភាគ"
        DataGridView1.Columns(13).Name = "ចំនួនថ្ងៃ"
        DataGridView1.Columns(14).Name = "ទឹកប្រាក់យឺត"
        DataGridView1.Columns(15).Name = "ប្រាក់ដើមយឺត"
        DataGridView1.Columns(16).Name = "រូបបិយប័ណ្ណ"
        DataGridView1.Columns(17).Name = "ប្រភេទ"
    End Sub
    Private Sub SP_RptGetLoanPaid()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 13
        DataGridView1.Columns(0).Name = "លេខកិច្ចសន្យា"
        DataGridView1.Columns(1).Name = "កូដសាខា"
        DataGridView1.Columns(2).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(3).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(4).Name = "កូដអតិថិជន"
        DataGridView1.Columns(5).Name = "ឈ្មោះអតិថិជន"
        DataGridView1.Columns(6).Name = "លេខទូរស័ព្ទ"
        DataGridView1.Columns(7).Name = "អសយដ្ឋាន"
        DataGridView1.Columns(8).Name = "បរិយាយ"
        DataGridView1.Columns(9).Name = "ថ្ងៃបានបង់"
        DataGridView1.Columns(10).Name = "បា្រក់បានបង់"
        DataGridView1.Columns(11).Name = "ប្រាក់ពិន័យ"
        DataGridView1.Columns(12).Name = "រូបបិយប័ណ្ណ"
    End Sub
    Private Sub SP_RptGetLoanToRepay()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 11
        DataGridView1.Columns(0).Name = "លេខកិច្ចសន្យា"
        DataGridView1.Columns(1).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(2).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(3).Name = "កូដអតិថិជន"
        DataGridView1.Columns(4).Name = "ឈ្មោះអតិថិជន"
        DataGridView1.Columns(5).Name = "លេខទូរស័ព្ទ"
        DataGridView1.Columns(6).Name = "អសយដ្ឋាន"
        DataGridView1.Columns(7).Name = "ថ្ងៃត្រូវបង់"
        DataGridView1.Columns(8).Name = "ចំនួនត្រូវបង់"
        DataGridView1.Columns(9).Name = "ចំនួនបង់ផ្តាច់"
        DataGridView1.Columns(10).Name = "កាលវិភាគបានបង់"
    End Sub
    Private Sub SP_RptInActive()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 12
        DataGridView1.Columns(0).Name = "កូដសាខា"
        DataGridView1.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(2).Name = "លេខកិច្ចសន្យា"
        DataGridView1.Columns(3).Name = "កូដអតិថិជន"
        DataGridView1.Columns(4).Name = "ឈ្មោះអតិថិជន"
        DataGridView1.Columns(5).Name = "លេខទូរស័ព្ទ"
        DataGridView1.Columns(6).Name = "អស័យដ្ឋាន"
        DataGridView1.Columns(7).Name = "ទំហំទឹកប្រាក់ខ្ចី"
        DataGridView1.Columns(8).Name = "ចំនួនជំហ៊ាន"
        DataGridView1.Columns(9).Name = "ចំនួនកាលវិភាគយឺត"
        DataGridView1.Columns(10).Name = "ចំនួនថ្ងៃយឺត"
        DataGridView1.Columns(11).Name = "ថែ្ងបង់ផ្តាច់"
    End Sub
    'Private Sub loadFirst()

    '    Dim dgList As DataGridView = Me.Datagridview1
    '    Utility.GridCellStyle(dgList)
    '    Me.dgList = DirectCast(dgList, DataGridViewEx)
    '    Me.dgList.MainKey = Me.MainKey
    '    Me.Icon = MyProject.Forms.FrmMain.Icon
    '    Me.lblNumerofRecord.Text = Conversions.ToString(Me.dgList.RowCount)
    '    Me.WindowState = FormWindowState.Maximized
    '    dgList = Me.dgList
    '    Me.dgList.LoadDGList(dgList, Me.Name, Me.MainKey)
    '    Me.dgList = DirectCast(dgList, DataGridViewEx)
    '    Me.GeneratateHeader()
    '    Me.GetReportInfor()
    '    Me.btnToFormatExcel.Visible = (((((Me.MainKey = "2") OrElse (Me.MainKey = "13")) OrElse ((Me.MainKey = "14") OrElse (Me.MainKey = "15"))) OrElse (((Me.MainKey = "16") OrElse (Me.MainKey = "17")) OrElse ((Me.MainKey = "18") OrElse (Me.MainKey = "19")))) OrElse ((((Me.MainKey = "20") OrElse (Me.MainKey = "21")) OrElse ((Me.MainKey = "22") OrElse (Me.MainKey = "23"))) OrElse (((Me.MainKey = "24") OrElse (Me.MainKey = "25")) OrElse ((Me.MainKey = "26") OrElse (Me.MainKey = "27"))))) OrElse ((((Me.MainKey = "28") OrElse (Me.MainKey = "29")) OrElse ((Me.MainKey = "30") OrElse (Me.MainKey = "31"))) OrElse (((Me.MainKey = "32") OrElse (Me.MainKey = "33")) OrElse ((Me.MainKey = "34") OrElse (Me.ReportType = "TEMPLATE1"))))

    '    '=======================================================
    '    'Service provided by Telerik (www.telerik.com)
    '    'Conversion powered by NRefactory.
    '    'Twitter: @telerik
    '    'Facebook: facebook.com/telerik
    '    '=======================================================

    'End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ExportDatagridViewToExcel(DataGridView1, "D:\" & lblNameReport.Text & ".xls")
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Me.lblNameReport.Text = "Profit" Then
            toExcelFormat()
        ElseIf Me.lblNameReport.Text = "Loan Summary By CO" Then
            Me.toExcelFormatSummary()
        ElseIf Me.lblNameReport.Text = "Interest Summary" Then
            Me.toExcelFormatSummaryInt()
        ElseIf Me.lblNameReport.Text = "Loan Arrears Summary By CO" Then
            ToFormatExcel("LoanArreare-Summery-by-CO.xlsx", "Sheet1", "B10:B", 6, "A6", 9)
        ElseIf Me.lblNameReport.Text = "Loan Outstanding Summary by CO" Then
            ToFormatExcel("Loan-Outstanding-Summery-by-CO.xlsx", "Sheet1", "B10:B", 6, "A6", 9)
        ElseIf Me.lblNameReport.Text = "Summary loan paid" Then
            ToFormatExcel("LoanToPaid-Summery-by-CO.xlsx", "Sheet1", "B10:B", 6, "A6", 9)
        ElseIf Me.lblNameReport.Text = "Summary loan to pay" Then
            ToFormatExcel("LoanToPay-Summery-by-CO.xlsx", "Sheet1", "B10:B", 6, "A6", 9)
        ElseIf Me.lblNameReport.Text = "Incorrect Repay" Then
            ToFormatExcel("Incorrect-Repay.xlsx", "Incorrect Repay", "B10:B", 6, "A6", 8)
        ElseIf Me.lblNameReport.Text = "LoanToRepayByBranch" Then
            ToFormatExcel("GetNumberAndAmountRepayByBrand.xlsx", "Sheet1", "B10:B", 6, "A5", 10)
        ElseIf Me.lblNameReport.Text = "LoanSummaryByBranch" Then
            ToFormatExcel("68_LDSummaryByBranchWithoutWriteoff1.xlsx", "Sheet2", "B10:B", 6, "A4", 8)
        ElseIf Me.lblNameReport.Text = "LoanOutstandingSummaryByBrand" Then
            ToFormatExcel("Loan-Outstanding-Summery-by-Branch.xlsx", "Sheet1", "B10:B", 6, "A6", 9)
        ElseIf Me.lblNameReport.Text = "LoanArrearsSummaryByBranch" Then
            ToFormatExcel("LoanArreare-Summery-by-CO-Branch.xlsx", "Sheet1", "B10:B", 6, "A6", 9)
        ElseIf Me.lblNameReport.Text = "SummaryLoanPaidByBranch" Then
            ToFormatExcel("LoanToPaid-Summery-by-Branch.xlsx", "Sheet1", "B10:B", 6, "A6", 9)
        ElseIf Me.lblNameReport.Text = "ProfitByBrand" Then
            ToFormatExcel("Summery-Profit-By-Branch.xlsx", "Sheet1", "B10:B", 6, "A6", 10)
        Else
            MessageBox.Show("Sorry no format excel for this report, try standard excel please.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub FillData(ByVal dgv As DataGridView)
        Dim dt As New System.Data.DataTable()
        Dim xlApp As New X.Application
        For Each col As DataGridViewColumn In dgv.Columns
            dt.Columns.Add(col.HeaderText)
        Next

        For Each row As DataGridViewRow In dgv.Rows
            Dim dRow As DataRow = dt.NewRow()
            For Each cell As DataGridViewCell In row.Cells
                dRow(cell.ColumnIndex) = cell.Value
            Next
            dt.Rows.Add(dRow)

            'Dim wb = New xlApp
            'X.Workbooks.Add()
            'X.Worksheets.Add(dt)

        Next
        'X.Workbooks.Add(dt)
        'MessageBox.Show(dt.TableName)
        Dim excelBook As X.Workbook = xlApp.Workbooks.Add(dt)
        xlApp.SaveAs("myExcelFile.xlsx")
    End Sub
    Private Sub toExcelFormat()
        Try
            Dim connectionString As String = Nothing
            Dim sql As String = Nothing
            Dim data As String = Nothing
            Dim i As Integer = 0
            Dim j As Integer = 0
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim xlApp As New Excel.Application
            Dim misValue As Object = System.Reflection.Missing.Value
            xlApp = New Excel.Application()
            ''-----------------------------------------------------------------------------
            '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\sample\Income-Statement.xlsx", False, True)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
            xlApp.Visible = True
            sql = "Exec sp_rptProfit '" & lblStartDate.Text & "','" & lblEndDate.Text & "','" & frmReport.cboBranch.Text & "'"
            Dim count As Integer = getData("select count(ASID) Num from (select ASID from ExpenseOperation where OPDate between '" & lblStartDate.Text & "' and '" & lblEndDate.Text & "' and BrID like '" & frmReport.cboBranch.Text & "' and OPTerm=1 group by ASID) a")
            Dim dscmd As New SqlDataAdapter(sql, g_cnn)
            Dim ds As New DataSet()
            dscmd.Fill(ds)
            With excelWorksheet
                If count >= 3 Then
                    .Range("B30:B" & count + 27).EntireRow.Insert()
                End If
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    For j = 0 To ds.Tables(0).Columns.Count - 1
                        data = ds.Tables(0).Rows(i).ItemArray(j).ToString()
                        .Cells(i + 10, j + 2) = data
                    Next
                Next
                .Range("A3").Value = "កាលបរិច្ឆេទៈ " & lblStartDate.Text & " ដល់ " & lblEndDate.Text
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ForGrid(ByVal Dg As DataGridView, ByVal StorePro As String)
        Dim query As String = StorePro
        Dim dataSet As New DataSet()
        Dim sqlDataAdapter As New SqlDataAdapter(query, g_cnn)
        sqlDataAdapter.Fill(dataSet)
        Dg.DataSource = dataSet.Tables(0)
    End Sub
    Private Sub toExcelFormatSummary()
        Try
            Dim connectionString As String = Nothing
            Dim sql As String = Nothing
            Dim data As String = Nothing
            Dim i As Integer = 0
            Dim j As Integer = 0
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim xlApp As New Excel.Application
            Dim misValue As Object = System.Reflection.Missing.Value
            xlApp = New Excel.Application()
            ''-----------------------------------------------------------------------------
            '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\sample\Loan-Summery-by-CO.xlsx", False, True)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
            xlApp.Visible = True
            Dim count As Integer = DataGridView1.Rows.Count
            Dim rowsTotal, colsTotal As Short
            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                If count > 3 Then
                    .Range("B10:B" & count + 6).EntireRow.Insert()
                End If
                For i = 0 To rowsTotal
                    For j = 0 To colsTotal
                        .Cells(i + 9, j + 1).value = DataGridView1.Rows(i).Cells(j).Value
                    Next j
                Next i
                .Range("A6").Value = "កាលបរិច្ឆេទៈ " & lblStartDate.Text & " ដល់ " & lblEndDate.Text
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub toExcelFormatSummaryInt()
        Try
            Dim connectionString As String = Nothing
            Dim sql As String = Nothing
            Dim data As String = Nothing
            Dim i As Integer = 0
            Dim j As Integer = 0
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim xlApp As New Excel.Application
            Dim misValue As Object = System.Reflection.Missing.Value
            xlApp = New Excel.Application()
            ''-----------------------------------------------------------------------------
            '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\sample\Summery-Profit-By-CO.xlsx", False, True)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
            xlApp.Visible = True
            Dim count As Integer = DataGridView1.Rows.Count
            Dim rowsTotal, colsTotal As Short
            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                If count > 3 Then
                    .Range("B9:B" & count + 5).EntireRow.Insert()
                End If
                For i = 0 To rowsTotal
                    For j = 0 To colsTotal
                        .Cells(i + 8, j + 1).value = DataGridView1.Rows(i).Cells(j).Value
                    Next j
                Next i
                .Range("A6").Value = "កាលបរិច្ឆេទៈ " & lblStartDate.Text & " ដល់ " & lblEndDate.Text
            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ToFormatExcel(ByVal ExcelName As String, ByVal SheetName As String, ByVal RangeInsert As String, ByVal RowNum As Integer, ByVal Date1 As String, ByVal RowFill As Integer)
        Try
            Dim connectionString As String = Nothing
            Dim sql As String = Nothing
            Dim data As String = Nothing
            Dim i As Integer = 0
            Dim j As Integer = 0
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim xlApp As New Excel.Application
            Dim misValue As Object = System.Reflection.Missing.Value
            xlApp = New Excel.Application()
            ''-----------------------------------------------------------------------------
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\sample\" & ExcelName, False, True)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets(SheetName), Excel.Worksheet)
            xlApp.Visible = True
            Dim count As Integer = DataGridView1.Rows.Count
            Dim rowsTotal, colsTotal As Short
            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                If count > 3 Then
                    .Range(RangeInsert & count + RowNum).EntireRow.Insert()
                End If
                For i = 0 To rowsTotal
                    For j = 0 To colsTotal
                        .Cells(i + RowFill, j + 1).value = DataGridView1.Rows(i).Cells(j).Value
                    Next j
                Next i
                If lblEndDate.Text = "" Then
                    .Range(Date1).Value = "Date at: " & lblStartDate.Text
                Else
                    .Range(Date1).Value = "From date: " & lblStartDate.Text & " TO " & lblEndDate.Text
                End If

            End With

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class