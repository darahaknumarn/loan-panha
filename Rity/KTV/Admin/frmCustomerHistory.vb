Imports Microsoft.Office.Interop
Imports System.Data.SqlClient

Public Class frmCustomerHistory

    Private Sub frmCustomerHistory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid3(DataGridView1)
        SetFontDatagrid3(DataGridView2)
        SetFontDatagrid3(DataGridView3)
    End Sub
    Private Sub txtCustomer_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomer.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim CM_ID1 As String = getData("Select * from BK_Customer where CM_ID='" & txtCustomer.Text & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
            If CM_ID1 = "" Then
                MessageBox.Show("No this customer please check again.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Else
                ShowDataGrid(Me.DataGridView1, "select CM_ID,CM_KhName,CM_Phone,a.LO_ID,LD_Cycle,VL_ID+','+CN_ID+','+b.DT_ID+','+PV_ID 'Address',ID 'RealID' from BK_Customer a left join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID where CM_ID='" & Me.txtCustomer.Text & "' and CM_BrId ='" & frmMain.lblCode.Text & "'")
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If DataGridView2.Rows.Count = 0 Then
            Return
        Else
            DataGridView2.DataSource.Rows.Clear()
            If Me.DataGridView3.Rows.Count = 0 Then
                Return
            Else
                DataGridView3.DataSource.Rows.Clear()
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Try
            Dim cm As String = Me.DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(6).Value.ToString
            If cm = "" Then
                Return
            Else
                ShowDataGrid(DataGridView2, "select LD_ID,LD_BrId,CM_ID,convert(nvarchar(12),LD_Dis_Date,101)Dis_Date,convert(nvarchar(12),LD_First_Date,101)FirstDate,convert(nvarchar(12),LD_Mat_Date,101)EndDate,LD_Dis_Amt Dis_Amt,CU_ID,LD_IntRate,a.EM_ID,b.EM_Name,LD_Unit,LD_Type,LD_Term,LD_ChargeRate ChargeRate,LD_ChargeAmt ChargeAmt,case when LD_Service=1 then 'Yes' else 'No' end 'OP.Fee',LD_Status from BK_Loan a left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID where CM_ID1='" & cm & "' and LD_BrId='" & frmMain.lblCode.Text & "' order by Cast(a.LD_ID as int) desc")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        If DataGridView3.Rows.Count = 0 Then
            Return
        Else
            DataGridView3.DataSource.Rows.Clear()
        End If
    End Sub
    Private Sub DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        Try
            Dim LD_ID As String = Me.DataGridView2.Rows(DataGridView2.CurrentRow.Index).Cells(0).Value.ToString
            If LD_ID = "" Then
                Return
            Else
                ShowDataGrid(DataGridView3, "exec spGetLoanRepayDetailAudit '" & LD_ID & "','" & frmMain.lblCode.Text & "'")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    'Private Sub DataGridView1_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView1.MouseClick
    '    Dim rowClicked As Integer
    '    If e.Button = Windows.Forms.MouseButtons.Right Then
    '        rowClicked = DataGridView1.HitTest(e.Location.X, e.Location.Y).RowIndex
    '        ContextMenuStrip1.Show(DataGridView1, e.Location)
    '    End If
    'End Sub
    'Private Sub DataGridView2_MouseClick(sender As Object, e As MouseEventArgs) Handles DataGridView2.MouseClick
    '    Dim rowClicked As Integer
    '    If e.Button = Windows.Forms.MouseButtons.Right Then
    '        rowClicked = DataGridView2.HitTest(e.Location.X, e.Location.Y).RowIndex
    '        ContextMenuStrip1.Show(DataGridView2, e.Location)
    '    End If
    'End Sub


    Private Sub toExcelFormat()
        Dim iRow1 = Me.DataGridView1.CurrentCell.RowIndex
        Dim iRow2 = Me.DataGridView2.CurrentCell.RowIndex
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
        Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\sample\LoanAudit.xls", False, True)
        Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet2"), Excel.Worksheet)
        xlApp.Visible = True
        'cnn = New SqlConnection(connectionString1)
        'cnn.Open()
        sql = "Exec spGetLoanRepayDetailAudit '" & Me.DataGridView2.Rows(DataGridView2.CurrentRow.Index).Cells(0).Value & "','" & frmMain.lblCode.Text & "'"
        Dim count As Integer = getData("select COUNT(LD_ID) from BK_LoanSchedule where LD_ID='" & Me.DataGridView2.Rows(DataGridView2.CurrentRow.Index).Cells(0).Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
        Dim dscmd As New SqlDataAdapter(sql, g_cnn)
        Dim ds As New DataSet()
        dscmd.Fill(ds)
        With excelWorksheet
            .Range("A8:A" & count + 4).EntireRow.Insert()
            .Range("B2").Value = Me.DataGridView2.Rows(iRow2).Cells(0).Value
            .Range("B3").Value = Me.DataGridView2.Rows(iRow2).Cells(2).Value
            .Range("B4").Value = Me.DataGridView1.Rows(iRow1).Cells(1).Value
            .Range("B5").Value = Me.DataGridView1.Rows(iRow1).Cells(5).Value
            .Range("I2").Value = Me.DataGridView2.Rows(iRow2).Cells(11).Value
            .Range("I3").Value = Me.DataGridView2.Rows(iRow2).Cells(12).Value
            .Range("I4").Value = Me.DataGridView2.Rows(iRow2).Cells(3).Value
            .Range("M2").Value = Me.DataGridView2.Rows(iRow2).Cells(13).Value
            .Range("M3").Value = Me.DataGridView2.Rows(iRow2).Cells(6).Value
            .Range("M4").Value = Me.DataGridView2.Rows(iRow2).Cells(8).Value
            .Range("M5").Value = Me.DataGridView2.Rows(iRow2).Cells(7).Value
            For i = 0 To ds.Tables(0).Rows.Count - 1
                For j = 0 To ds.Tables(0).Columns.Count - 1
                    data = ds.Tables(0).Rows(i).ItemArray(j).ToString()
                    .Cells(i + 7, j + 2) = data
                    .Cells(i + 7, 1) = i + 1
                Next
            Next
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ToExcel(DataGridView1)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ToExcel(DataGridView2)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.DataGridView3.Rows.Count < 3 Then
            MessageBox.Show("Nothing to export to excel file", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            toExcelFormat()
        End If

    End Sub

    Private Sub RadCus_CheckedChanged(sender As Object, e As EventArgs) Handles RadCus.CheckedChanged
        If RadCus.Checked Then
            txtCustomer.Enabled = True
            txtLoan.Enabled = False
        Else
            txtCustomer.Enabled = False
            txtLoan.Enabled = True
        End If
    End Sub

    Private Sub txtLoan_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLoan.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim CM_ID1 As String = getData("Select * from BK_Loan where LD_ID='" & txtLoan.Text & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                If CM_ID1 = "" Then
                    MessageBox.Show("No this Loan please check again.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Else
                    ShowDataGrid(Me.DataGridView1, "select a.CM_ID,CM_KhName,CM_Phone,a.LO_ID,a.LD_Cycle,VL_ID+','+CN_ID+','+b.DT_ID+','+PV_ID 'Address',ID 'RealID' from BK_Customer a left join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID inner join BK_Loan c on a.CM_ID=c.CM_ID and a.CM_BrId=c.LD_BrId and a.ID=c.CM_ID1 where LD_ID='" & Me.txtLoan.Text & "' and LD_BrId ='" & frmMain.lblCode.Text & "'")
                    ShowDataGrid(DataGridView2, "select LD_ID,LD_BrId,CM_ID,convert(nvarchar(12),LD_Dis_Date,101)Dis_Date,convert(nvarchar(12),LD_First_Date,101)FirstDate,convert(nvarchar(12),LD_Mat_Date,101)EndDate,LD_Dis_Amt Dis_Amt,CU_ID,LD_IntRate,a.EM_ID,b.EM_Name,LD_Unit,LD_Type,LD_Term,LD_ChargeRate ChargeRate,LD_ChargeAmt ChargeAmt,case when LD_Service=1 then 'Yes' else 'No' end 'OP.Fee',LD_Status from BK_Loan a left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID where a.LD_ID='" & txtLoan.Text & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                    ShowDataGrid(DataGridView3, "exec spGetLoanRepayDetailAudit '" & txtLoan.Text & "','" & frmMain.lblCode.Text & "'")
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub toExcelFormat(ByVal LD_ID As Integer)
        Try
            Dim iRow1 = Me.DataGridView1.CurrentCell.RowIndex
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
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\LoanAudit.xls", False, True)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet2"), Excel.Worksheet)
            xlApp.Visible = True
            sql = "Exec spGetLoanRepayDetailAudit '" & LD_ID & "','" & frmMain.lblCode.Text & "'"
            Dim count As Integer = getData("select COUNT(LD_ID) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
            Dim dscmd As New SqlDataAdapter(sql, g_cnn)
            Dim ds As New DataSet()
            dscmd.Fill(ds)
            With excelWorksheet
                .Range("A8:A" & count + 4).EntireRow.Insert()
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    For j = 0 To ds.Tables(0).Columns.Count - 1
                        data = ds.Tables(0).Rows(i).ItemArray(j).ToString()
                        .Cells(i + 7, j + 2) = data
                        .Cells(i + 7, 1) = i + 1
                    Next
                Next
                Dim oDt As New System.Data.DataTable
                Dim Str As String = "select top 1 a.LD_ID,a.CM_ID,b.CM_KhName,c.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID [Address],a.LD_Unit,a.LD_Type,convert(nvarchar(12),a.LD_Dis_Date,101)Dis_Date,LD_Term,LD_Dis_Amt,LD_IntRate,CU_ID from BK_Loan a left join BK_Customer b on a.CM_ID=b.CM_ID and a.LD_BrId=b.CM_BrId left join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=LO_BrID left join BK_Employee d on a.EM_ID=d.EM_ID and a.LD_BrId=d.EM_BrID where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'"
                oDt.Clear()
                oDa = New SqlDataAdapter(Str, g_cnn)
                oDa.Fill(oDt)
                .Range("B2").Value = oDt.Rows(0).Item(0).ToString
                .Range("B3").Value = oDt.Rows(0).Item(1).ToString
                .Range("B4").Value = oDt.Rows(0).Item(2).ToString
                .Range("B5").Value = oDt.Rows(0).Item(3).ToString
                .Range("I2").Value = oDt.Rows(0).Item(4).ToString
                .Range("I3").Value = oDt.Rows(0).Item(5).ToString
                .Range("I4").Value = oDt.Rows(0).Item(6).ToString
                .Range("M2").Value = oDt.Rows(0).Item(7).ToString
                .Range("M3").Value = oDt.Rows(0).Item(8).ToString
                .Range("M4").Value = oDt.Rows(0).Item(9).ToString
                .Range("M5").Value = oDt.Rows(0).Item(10).ToString
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class