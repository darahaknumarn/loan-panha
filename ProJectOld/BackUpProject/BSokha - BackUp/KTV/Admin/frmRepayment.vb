Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class frmRepayment
    Private Sub frmRepayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DataGridView1.Columns("coNo").Visible = False
        Me.dgWF.Columns("coNoWF").Visible = False
        newRowWF()
        newRow()
        AddCombo(ComboBox1, "select EM_ID from BK_Employee where EM_BrID ='" & frmMain.lblCode.Text & "'")
        ComboBox1.Text = "All"
        AddCombo(ComboBox2, "select EM_ID from BK_Employee where EM_BrID ='" & frmMain.lblCode.Text & "'")
        ComboBox2.Text = "All"
        radCuId.Checked = True
        SetFont()
    End Sub
    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        'Dim iRow = DataGridView1.CurrentCell.RowIndex
        Dim LDCheck1 As String = getData("select LD_ID from Writeoff where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and BR_ID='" & frmMain.lblCode.Text & "'")
        If LDCheck1 <> "" Then
            MessageBox.Show("This loan is writeoff, please check again!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim staffName As String = ""
        If iCol = DataGridView1.Columns.Count - 1 Then
            If iRow < DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1(0, iRow + 1)
            End If
        Else
            If iRow < DataGridView1.Rows.Count - 1 Then
                SendKeys.Send("{up}")
            End If

            '-------------------------------------------------------- Cell Loan ID
            If DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coLD_ID").ColumnIndex Then
                Dim a As String = DataGridView1.CurrentCell.Value
                If a = "" Then
                    Return
                Else
                    '---------------------------------------------- check loan status
                    Dim LD_Status As String = getData("select top 1 LD_Status from BK_Loan where LD_ID=" & DataGridView1.CurrentCell.Value.ToString & " and LD_BrId=" & frmMain.lblCode.Text)
                    If LD_Status = "" Then
                        resultError = frmMessageError.ShowBoxError("គ្មានលេខកិច្ចសន្យានេះទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Return
                    ElseIf LD_Status = "Active" Then
                        insertRepay(DataGridView1.Rows(iRow).Cells("coLD_ID").Value, 1)
                    Else
                        resultError = frmMessageError.ShowBoxError("លេខកិច្ចសន្យានេះបានបង់ផ្តាច់រួចរាល់ហើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Return
                    End If
                End If
                'AutoSum()
                '-------------------------------------------------------- Cell DatePaid
            ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coDatePaid").ColumnIndex Then
                Try
                    Dim a As String = DataGridView1.Rows(iRow).Cells("coDatePaid").Value.ToString
                    Dim dateCheck As Boolean
                    dateCheck = IsDate(a)
                    If dateCheck = True Then
                        DataGridView1.Rows(iRow).Cells("coDatePaid").Value = a
                    Else
                        Dim now As Date
                        Dim day As Integer = DateTime.Now.Day
                        a = a - day
                        now = DateTime.Now.AddDays(a)
                        DataGridView1.Rows(iRow).Cells(iCol).Value = FormatDateTime(now, DateFormat.ShortDate)
                    End If
                    'DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលថ្ងៃ​បង់ត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    DataGridView1.Rows(iRow).Cells("coDatePaid").Value = ""
                    Return
                End Try
                '------------------------------------------------------------ Cell Description
            ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coDes").ColumnIndex Then
                Try
                    If DataGridView1.CurrentCell.Value = 1 Or DataGridView1.CurrentCell.Value = 2 Then
                        Dim Unit As String = getData("select TD_Name from BK_TransactionDes where TD_ID=" & DataGridView1.CurrentCell.Value)
                        If DataGridView1.CurrentCell.Value = 1 Then '-------------------- Normal repay
                            DataGridView1.CurrentCell.Value = Unit.ToString
                            Dim iRow1 = Me.DataGridView1.CurrentCell.RowIndex
                            Dim oDt As New System.Data.DataTable
                            If Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value = "" Then
                                resultError = frmMessageError.ShowBoxError("បញ្ចូលលេខកិច្ចសន្យាជាមុនសិន។", "ការបញ្ចូលទិន្ន័យខុស")
                                Return
                            End If
                            Dim Str As String = "exec sp_repay1 '" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "','" & frmMain.lblCode.Text & "','" & Me.DataGridView1.Rows(iRow).Cells("coDateTopay").Value & "','" & DataGridView1.Rows(iRow).Cells(0).Value & "'"
                            'On Error Resume Next
                            oDt.Clear()
                            oDa = New SqlDataAdapter(Str, g_cnn)
                            oDa.Fill(oDt)
                            DataGridView1.Rows(iRow1).Cells(6).Value = oDt.Rows(0).Item(1).ToString
                            DataGridView1.Rows(iRow1).Cells(9).Value = oDt.Rows(0).Item(1).ToString
                            oDa.Dispose()
                            oDt.Dispose()
                        ElseIf DataGridView1.CurrentCell.Value = 2 Then '--------------------- when payoff
                            '------------------------------ Check loan schedule
                            Dim count As String = getData("select count(LD_ID) from BK_LoanRepay where SH_Date = '" & Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value & "' and LR_BrID ='" & frmMain.lblCode.Text & "' and LD_ID ='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' group by SH_Date,LR_BrID")
                            If count <> "1" Then
                                MessageBox.Show("This loan cannot be payoff because last repay not enough amount, please check again or contact IT for more detail.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return
                            End If
                            DataGridView1.CurrentCell.Value = Unit.ToString
                            Dim iRow1 = Me.DataGridView1.CurrentCell.RowIndex
                            Dim oDt As New System.Data.DataTable
                            If Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value = "" Then
                                resultError = frmMessageError.ShowBoxError("បញ្ចូលលេខកិច្ចសន្យាជាមុនសិន។", "ការបញ្ចូលទិន្ន័យខុស")
                                Return
                            End If
                            Dim Str As String = "exec sp_repay1 '" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "','" & frmMain.lblCode.Text & "','" & Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value & "','" & DataGridView1.Rows(iRow).Cells(0).Value & "'"
                            oDt.Clear()
                            oDa = New SqlDataAdapter(Str, g_cnn)
                            oDa.Fill(oDt)
                            DataGridView1.Rows(iRow1).Cells(6).Value = oDt.Rows(0).Item(2).ToString
                            DataGridView1.Rows(iRow1).Cells(9).Value = oDt.Rows(0).Item(2).ToString
                            oDa.Dispose()
                            oDt.Dispose()
                        Else
                            Return
                        End If
                    Else
                        Return
                    End If
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្ន័យខុស។", "ការបញ្ចូលទិន្ន័យខុស")
                End Try
            End If
        End If
    End Sub
    Private Sub dgWF_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgWF.CellEndEdit

        Dim iRow = Me.dgWF.CurrentCell.RowIndex
        Dim iCol = dgWF.CurrentCell.ColumnIndex
        'Dim iRow = DataGridView1.CurrentCell.RowIndex
        Dim staffName As String = ""


        Dim LDCheck As String = getData("select LD_ID from Writeoff where LD_ID='" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and BR_ID='" & frmMain.lblCode.Text & "'")
        If LDCheck = "" Then
            MessageBox.Show("This loan is not writeoff, please check again!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If iCol = dgWF.Columns.Count - 1 Then
            If iRow < dgWF.Rows.Count - 1 Then
                dgWF.CurrentCell = dgWF(0, iRow + 1)
            End If
        Else
            If iRow < dgWF.Rows.Count - 1 Then
                SendKeys.Send("{up}")
            End If
            '-------------------------------------------------------- Cell Loan ID
            If dgWF.CurrentCell.ColumnIndex = Me.dgWF.Rows(iRow).Cells("coLD_IDWF").ColumnIndex Then
                Dim a As String = dgWF.CurrentCell.Value
                If a = "" Then
                    Return
                Else
                    '---------------------------------------------- check loan status
                    Dim LD_Status As String = getData("select top 1 LD_Status from BK_Loan where LD_ID=" & dgWF.CurrentCell.Value.ToString & " and LD_BrId=" & frmMain.lblCode.Text)
                    If LD_Status = "" Then
                        resultError = frmMessageError.ShowBoxError("គ្មានលេខកិច្ចសន្យានេះទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Return
                    ElseIf LD_Status = "Active" Then
                        insertRepay(dgWF.Rows(iRow).Cells("coLD_IDWF").Value, 2)
                    Else
                        resultError = frmMessageError.ShowBoxError("លេខកិច្ចសន្យានេះបានបង់ផ្តាច់រួចរាល់ហើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Return
                    End If
                End If
                'AutoSum()
                '-------------------------------------------------------- Cell DatePaidcoEMID
            ElseIf dgWF.CurrentCell.ColumnIndex = Me.dgWF.Rows(iRow).Cells("coEMID").ColumnIndex Then
                Dim EmName As String = getData("select top 1 EM_ID from BK_Loan where LD_ID=" & Me.dgWF.Rows(iRow).Cells("coEMID").Value & " and LD_BrId=" & frmMain.lblCode.Text)
                If EmName <> "" Then
                    Me.dgWF.Rows(iRow).Cells("coEMName").Value = Getdata("select EM_Name from BK_Employee where EM_ID='" & Me.dgWF.Rows(iRow).Cells("coEMID").Value & "' and EM_BrID='" & frmMain.lblCode.Text & "'")
                Else
                    MessageBox.Show("This Employee ID have no in system, please check again!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            ElseIf dgWF.CurrentCell.ColumnIndex = Me.dgWF.Rows(iRow).Cells("coDatePaidWF").ColumnIndex Then
                Try
                    Dim a As String = dgWF.Rows(iRow).Cells("coDatePaidWF").Value.ToString
                    Dim dateCheck As Boolean
                    dateCheck = IsDate(a)
                    If dateCheck = True Then
                        dgWF.Rows(iRow).Cells("coDatePaidWF").Value = a
                    Else
                        Dim now As Date
                        Dim day As Integer = DateTime.Now.Day
                        a = a - day
                        now = DateTime.Now.AddDays(a)
                        dgWF.Rows(iRow).Cells(iCol).Value = FormatDateTime(now, DateFormat.ShortDate)
                    End If
                    'DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលថ្ងៃ​បង់ត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    dgWF.Rows(iRow).Cells("coDatePaidWF").Value = ""
                    Return
                End Try
                '------------------------------------------------------------ Cell Description
            ElseIf dgWF.CurrentCell.ColumnIndex = Me.dgWF.Rows(iRow).Cells("coDesWF").ColumnIndex Then
                Try
                    If dgWF.CurrentCell.Value = 1 Or dgWF.CurrentCell.Value = 2 Then
                        Dim Unit As String = getData("select TD_Name from BK_TransactionDes where TD_ID=" & dgWF.CurrentCell.Value)
                        If dgWF.CurrentCell.Value = 1 Then '-------------------- Normal repay
                            dgWF.CurrentCell.Value = Unit.ToString
                            Dim iRow1 = Me.DataGridView1.CurrentCell.RowIndex
                            Dim oDt As New System.Data.DataTable
                            If Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value = "" Then
                                resultError = frmMessageError.ShowBoxError("បញ្ចូលលេខកិច្ចសន្យាជាមុនសិន។", "ការបញ្ចូលទិន្ន័យខុស")
                                Return
                            End If
                            Dim Str As String = "exec sp_repay1 '" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "','" & frmMain.lblCode.Text & "','" & Me.dgWF.Rows(iRow).Cells("coDateTopayWF").Value & "','" & dgWF.Rows(iRow).Cells(0).Value & "'"
                            'On Error Resume Next
                            oDt.Clear()
                            oDa = New SqlDataAdapter(Str, g_cnn)
                            oDa.Fill(oDt)
                            dgWF.Rows(iRow1).Cells("coPaidWF").Value = oDt.Rows(0).Item(1).ToString
                            dgWF.Rows(iRow1).Cells("coAmtToPayWF").Value = oDt.Rows(0).Item(1).ToString
                            oDa.Dispose()
                            oDt.Dispose()
                        ElseIf dgWF.CurrentCell.Value = 2 Then '--------------------- when payoff
                            '------------------------------ Check loan schedule
                            Dim count As String = getData("select count(LD_ID) from BK_LoanRepay where SH_Date = '" & Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value & "' and LR_BrID ='" & frmMain.lblCode.Text & "' and LD_ID ='" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' group by SH_Date,LR_BrID")
                            If count <> "1" Then
                                MessageBox.Show("This loan cannot be payoff because last repay not enough amount, please check again or contact IT for more detail.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Return
                            End If
                            dgWF.CurrentCell.Value = Unit.ToString
                            Dim iRow1 = Me.dgWF.CurrentCell.RowIndex
                            Dim oDt As New System.Data.DataTable
                            If Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value = "" Then
                                resultError = frmMessageError.ShowBoxError("បញ្ចូលលេខកិច្ចសន្យាជាមុនសិន។", "ការបញ្ចូលទិន្ន័យខុស")
                                Return
                            End If
                            Dim Str As String = "exec sp_repay1 '" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "','" & frmMain.lblCode.Text & "','" & Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value & "','" & dgWF.Rows(iRow).Cells(0).Value & "'"
                            oDt.Clear()
                            oDa = New SqlDataAdapter(Str, g_cnn)
                            oDa.Fill(oDt)
                            dgWF.Rows(iRow1).Cells("coPaidWF").Value = oDt.Rows(0).Item(2).ToString
                            dgWF.Rows(iRow1).Cells("coAmtToPayWF").Value = oDt.Rows(0).Item(2).ToString
                            oDa.Dispose()
                            oDt.Dispose()
                        Else
                            Return
                        End If
                    Else
                        Return
                    End If
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្ន័យខុស។", "ការបញ្ចូលទិន្ន័យខុស")
                End Try
            End If
        End If
    End Sub
    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If Me.DataGridView1.Rows.Count = 0 Then
            newRow()
            Return
        End If
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If e.KeyCode = Keys.F12 Then
            '------------------------- declare prn and int
            Dim prn As Double = 0
            Dim int As Double = 0
            Dim LD_Service As Double = 0
            Dim ToPay As Double = 0
            '----------------------------------------- Check record change or not?
            If NoRecordChange() = 1 Then
                MessageBox.Show("No record change!", "No change", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            Else
                '--------------------------------------------------------------- check have record have or not
                If Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value Is Nothing Then
                    Return
                Else
                    '--------------------------------------------------------------- Start update data
                    Dim LR_ID As Integer = getData("select top 1 MAX(LR_ID) from BK_LoanRepay where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "' group by SH_Date order by SH_Date desc")
                    Dim Max_SH As Date = FormatDateTime(getData("select  max(SH_Date) SH_Date from BK_LoanSchedule where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)


                    ' -------------------------------------------------------------------------------------------         Change new code   ---------------------------------------------------
                    'If LR_ID = DataGridView1.Rows(iRow).Cells(0).Value Then
                    '----------------------------------------------------- Add to trace loan repay
                    Dim Repay As Integer = getData("select isnull(sum(case when Mark=0 and SH_Date <>'' then 1 else 0 end),0)  from BK_LoanRepay where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and  SH_Date >'" & DataGridView1.Rows(iRow).Cells("coDateTopay").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'")
                    If Repay > 0 Then
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនអាចកែរប្រែបានទេ ព្រោះមិនមែនជាទិន្នន័យចុងក្រោយ។", "កែរប្រែ")
                        Return
                    Else
                        AddTrace_Repay1("UPDATE OLD", 1)
                        '---------------------------------------------------------------------------- Update when description payoff
                        If Me.DataGridView1.Rows(iRow).Cells("coDes").Value.ToString.Trim = "បង់ផ្ដាច់" Then
                            '-------------------------- declare prn and int
                            prn = ReturnPrn(1, DataGridView1.Rows(iRow).Cells("coLD_ID").Value, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
                            int = ReturnInt(1, DataGridView1.Rows(iRow).Cells("coLD_ID").Value, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
                            LD_Service = ReturnService(1, DataGridView1.Rows(iRow).Cells("coLD_ID").Value, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
                            ToPay = prn + int + LD_Service
                            Dim LR_Amount As Double = Me.DataGridView1.Rows(iRow).Cells("coAmtPaid").Value
                            '---------------------------------------------------------------------- Calculate prn and int
                            If LR_Amount <= prn Then
                                prn = Val(LR_Amount)
                                int = 0
                                LD_Service = 0
                            ElseIf LR_Amount > prn And LR_Amount <= prn + int Then
                                int = LR_Amount - prn
                                LD_Service = 0
                            ElseIf LR_Amount > prn + int And LR_Amount <= prn + int + LD_Service Then
                                LD_Service = LR_Amount - (prn + int)
                            Else
                                prn = prn
                                int = int
                                LD_Service = LR_Amount - (prn + int)
                            End If
                            '--------------------------------------------------------------------------------------------- update status loan to payoff
                            '--- Add trace loan
                            frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                            '---- Update laon status
                            addIn("Update BK_Loan set LD_Status='Payoff',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "',Date_Payoff='" & DataGridView1.Rows(iRow).Cells("coDatePaid").Value & "' where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                            '---- add trace again
                            frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                            '----------------------------------------------------------------------------------------- Start update loan repay
                            UpdateRepay(DataGridView1.Rows(iRow).Cells("coDes").Value, DataGridView1.Rows(iRow).Cells("coDatePaid").Value, DataGridView1.Rows(iRow).Cells("coAmtPaid").Value, DataGridView1.Rows(iRow).Cells("coCharge").Value, frmMain.users, DateTime.Now, prn, int, LD_Service)
                            '------------------------------------ add trace repay loan again
                            AddTrace_Repay1("UPDATE NEW", 1)
                            '------------------------------------------- Auto to excel
                            toExcelFormat(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                        ElseIf DataGridView1.Rows(iRow).Cells("coDes").Value.ToString.Trim = "យកប្រាក់ពីអតិថិជន" Then '--------- Update when normal repay
                            '------------------------------ calculate prn and int
                            prn = ReturnPrn(0, DataGridView1.Rows(iRow).Cells("coLD_ID").Value, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
                            int = ReturnInt(0, DataGridView1.Rows(iRow).Cells("coLD_ID").Value, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
                            LD_Service = ReturnService(0, DataGridView1.Rows(iRow).Cells("coLD_ID").Value, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
                            ToPay = prn + int + LD_Service
                            Dim LR_Amt As Double = Me.DataGridView1.Rows(iRow).Cells("coAmtPaid").Value
                            If prn >= LR_Amt Then
                                prn = LR_Amt
                                int = 0
                                LD_Service = 0
                            ElseIf LR_Amt > prn And LR_Amt <= prn + int Then
                                int = LR_Amt - prn
                                LD_Service = 0
                            ElseIf LR_Amt > prn + int And LR_Amt <= prn + int + LD_Service Then
                                int = int
                                LD_Service = LR_Amt - (prn + int)
                            End If
                            '-----------------------------------------check status of repay
                            Dim status As String = getData("select top 1 LR_Description from BK_LoanRepay where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LR_ID='" & Me.DataGridView1.Rows(iRow).Cells(0).Value & "'")
                            '-------------------------------------------------------------------------------------------- update if it's last schedule repay
                            If Max_SH = FormatDateTime(DataGridView1.Rows(iRow).Cells("coDateTopay").Value, DateFormat.ShortDate) Then
                                frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                                addIn("Update BK_Loan set LD_Status='Active',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "',Date_Payoff='" & DateTime.MaxValue.Date & "' where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                                frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                            End If
                            '---------------------------
                            If Max_SH = FormatDateTime(DataGridView1.Rows(iRow).Cells("coDateTopay").Value, DateFormat.ShortDate) _
                                And Val(DataGridView1.Rows(iRow).Cells("coAmtPaid").Value) = Convert.ToDouble(DataGridView1.Rows(iRow).Cells("coAmtToPay").Value) Then
                                '---------------------- Update repay
                                UpdateRepay(DataGridView1.Rows(iRow).Cells("coDes").Value, DataGridView1.Rows(iRow).Cells("coDatePaid").Value, DataGridView1.Rows(iRow).Cells("coAmtPaid").Value, DataGridView1.Rows(iRow).Cells("coCharge").Value, frmMain.users, DateTime.Now, prn, int, LD_Service)
                                '-------------------------- Update Loan
                                frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                                addIn("Update BK_Loan set LD_Status='Mature',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "',Date_Payoff='" & DataGridView1.Rows(iRow).Cells(7).Value & "' where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                                frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                                AddTrace_Repay1("UPDATE NEW", 1)
                                '----------------------------------- To Excel
                                toExcelFormat(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                            ElseIf status = "បង់ផ្ដាច់" Then
                                '------------------------------------- If payoff update to Active
                                UpdateRepay(DataGridView1.Rows(iRow).Cells("coDes").Value, DataGridView1.Rows(iRow).Cells("coDatePaid").Value, DataGridView1.Rows(iRow).Cells("coAmtPaid").Value, DataGridView1.Rows(iRow).Cells("coCharge").Value, frmMain.users, DateTime.Now, prn, int, LD_Service)
                                frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                                addIn("Update BK_Loan set Date_Payoff='" & DateTime.MaxValue.Date & "',LD_Status='Active',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "' where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                                frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                                AddTrace_Repay1("UPDATE NEW", 1)
                            Else
                                '------------------------------------ Normal Update
                                If ToPay < LR_Amt Then
                                    'MessageBox.Show("Bigger")
                                    bigger()
                                Else
                                    UpdateRepay(DataGridView1.Rows(iRow).Cells("coDes").Value, DataGridView1.Rows(iRow).Cells("coDatePaid").Value, DataGridView1.Rows(iRow).Cells("coAmtPaid").Value, DataGridView1.Rows(iRow).Cells("coCharge").Value, frmMain.users, DateTime.Now, prn, int, LD_Service)
                                    AddTrace_Repay1("UPDATE NEW", 1)
                                End If
                            End If
                        Else
                            Return
                        End If
                        showRepay(1)
                        newRow()
                        '    Else
                        '    '-------------------------- If not last record
                        '    resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនអាចកែរប្រែបានទេ ព្រោះមិនមែនជាទិន្នន័យចុងក្រោយ។", "កែរប្រែ")
                        '    Return
                        'End If
                    End If
                End If

                'Dim SRepay As Integer = getData("select isnull(sum(case when Mark=1 and SH_Date <>'' then 1 else 0 end),0)  from BK_LoanRepay where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and  SH_Date >'" & DataGridView1.Rows(iRow).Cells("coDateTopay").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'")

                'MessageBox.Show(Repay.ToString)
                'Return
            End If
            'AutoSum()
        ElseIf e.KeyCode = Keys.F11 Then
            ToExcel(DataGridView1)
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Delete Then
            Dim LR_ID As String = getData("select Max(LR_ID)LR_ID from BK_LoanRepay where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'")
            If LR_ID = Me.DataGridView1.Rows(iRow).Cells("coNo").Value Then
                result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                If result = "1" Then
                    If LR_ID = "" Then
                        Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                    Else
                        AddTrace_Repay1("DELETE", 1)
                        '-------------------------------------------------------------- Check Status Loan
                        Dim Status As String = getData("Select top 1 LD_Status from BK_Loan where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                        If Status = "Payoff" Or Status = "Mature" Then
                            frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                            addIn("Update BK_Loan set LD_Status='Active',Date_Payoff='" & DateTime.MaxValue.Date & "',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "' where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                            frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                        End If
                        '----------------------------- Delete from repay
                        addIn("delete from BK_LoanRepay where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "' and LR_ID='" & Me.DataGridView1.Rows(iRow).Cells("coNo").Value & "'")
                        Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុបទិន្នន័យ")
                    End If
                Else
                    Return

                End If
            Else
                Dim LR_Date As Date = FormatDateTime(getData("Select Max(LR_Date) LR_Date from BK_LoanRepay where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
                resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនអាចលុបបានទេ ព្រោះមិនមែនជាការបង់ប្រាក់ចុងក្រោយ។ ការបង់ចុងក្រោយនៅថ្ងៃទី " & LR_Date & " ។", "លុបទិន្នន័យ")
                Return
            End If
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            newRow()
        End If
    End Sub
    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim a As String = getData("Select top 1 LD_ID from BK_Loan where LD_ID='" & TextBox1.Text.Trim & "' and LD_BrId=" & frmMain.lblCode.Text)
                If a.ToString = "" Then
                    resultError = frmMessageError.ShowBoxError("លេខកិច្ចសន្យានេះគ្មានទេ សូមពិនិត្យឡើងវិញ។", "លេខកូដខុស")
                    Return
                Else
                    'datagrid3()
                    Dim CM_ID1 As Integer = Val(getData("Select top 1 CM_ID1 from BK_Loan where LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID='" & TextBox1.Text & "'"))
                    Dim cm_Id As String = getData("Select top 1 CM_ID from BK_Loan where LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID='" & TextBox1.Text & "'")
                    Dim Name As String = getData("select top 1 CM_KhName from BK_Customer where ID='" & CM_ID1 & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                    Dim Address As String = getData("select c.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID addres from BK_Loan a inner join BK_Customer b on a.CM_ID=b.CM_ID and a.LD_BrId=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrID where LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID='" & TextBox1.Text & "' and b.ID='" & CM_ID1 & "'")
                    Label1.Text = cm_Id & ":" & " " & Name & ", អសយដ្ឋាន: " & Address
                    AddToGridLDPaid(DataGridView2, 11, "exec spGetLoanRepayDetail '" & TextBox1.Text.Trim & "','" & frmMain.lblCode.Text & "'")
                End If
            End If
            Return
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
            Return
        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        showListRepay(0)
    End Sub
    Private Sub DataGridView2_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView2.KeyDown
        If e.KeyCode = Keys.F11 Then
            ToExcel(DataGridView2)
        End If
    End Sub
    '--------------------------------------------- Function and Method
    Private Function ReturnService(ByRef a As Boolean, ByVal LD_ID As String, ByVal DateToPay As Date, ByVal LR_ID As Integer)
        Dim Int As Double = 0
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "exec sp_repay1 '" & LD_ID & "','" & frmMain.lblCode.Text & "','" & DateToPay & "','" & LD_ID & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        If a = False Then
            '--------------------------------- return prn when repay
            Int = Val(oDt.Rows(0).Item(7).ToString)
        Else
            '--------------------------------- Return prn when payoff
            Int = Val(oDt.Rows(0).Item(8).ToString)
        End If
        oDa.Dispose()
        oDt.Dispose()
        Return Int
    End Function
    Private Function ReturnInt(ByRef a As Boolean, ByVal LD_ID As String, ByVal DateToPay As Date, ByVal LR_ID As Integer)
        Dim Int As Double = 0
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "exec sp_repay1 '" & LD_ID & "','" & frmMain.lblCode.Text & "','" & DateToPay & "','" & LD_ID & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        If a = False Then
            '--------------------------------- return int when repay
            Int = Val(oDt.Rows(0).Item(6).ToString)
        Else
            '--------------------------------- Return int when payoff
            Int = Val(oDt.Rows(0).Item(4).ToString)
        End If
        oDa.Dispose()
        oDt.Dispose()
        Return Int
    End Function
    Private Function ReturnPrn(ByRef a As Boolean, ByVal LD_ID As String, ByVal DateToPay As Date, ByVal LR_ID As Integer)
        Dim prn As Double = 0
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "exec sp_repay1 '" & LD_ID & "','" & frmMain.lblCode.Text & "','" & DateToPay & "','" & LR_ID & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        If a = False Then
            '--------------------------------- return prn when repay
            prn = Val(oDt.Rows(0).Item(5).ToString)
        Else
            '--------------------------------- Return prn when payoff
            prn = Val(oDt.Rows(0).Item(3).ToString)
        End If
        oDa.Dispose()
        oDt.Dispose()
        Return prn
    End Function
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("coLD_ID").Style.BackColor = Color.WhiteSmoke
        DataGridView1.Rows(iRow).Cells("coLD_ID").ReadOnly = False
        DataGridView1.Rows(iRow).Cells("coCM_ID").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("coCM_ID").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("coCM_Name").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("coCM_Name").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("coCM_Address").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("coCM_Address").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("coAmtToPay").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("coAmtToPay").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("coDateTopay").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("coDateTopay").ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").ColumnIndex, iRow)
        lblAutoSum.Text = 0
    End Sub
    Private Sub newRowWF()
        dgWF.Rows.Add()
        Dim iRow As Integer = Me.dgWF.Rows.Count - 1
        dgWF.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        dgWF.Rows(iRow).Cells("coLD_IDWF").Style.BackColor = Color.WhiteSmoke
        dgWF.Rows(iRow).Cells("coLD_IDWF").ReadOnly = False
        dgWF.Rows(iRow).Cells("coCM_IDWF").Style.BackColor = Color.Yellow
        dgWF.Rows(iRow).Cells("coCM_IDWF").ReadOnly = True
        dgWF.Rows(iRow).Cells("coCM_NameWF").Style.BackColor = Color.Yellow
        dgWF.Rows(iRow).Cells("coCM_NameWF").ReadOnly = True
        dgWF.Rows(iRow).Cells("coCM_AddressWF").Style.BackColor = Color.Yellow
        dgWF.Rows(iRow).Cells("coCM_AddressWF").ReadOnly = True
        dgWF.Rows(iRow).Cells("coAmtToPayWF").Style.BackColor = Color.Yellow
        dgWF.Rows(iRow).Cells("coAmtToPayWF").ReadOnly = True
        dgWF.Rows(iRow).Cells("coDateTopayWF").Style.BackColor = Color.Yellow
        dgWF.Rows(iRow).Cells("coDateTopayWF").ReadOnly = True
        dgWF.Rows(iRow).Cells("coEMName").Style.BackColor = Color.Yellow
        dgWF.Rows(iRow).Cells("coEMName").ReadOnly = True
        dgWF.CurrentCell = dgWF(Me.dgWF.Rows(iRow).Cells("coLD_IDWF").ColumnIndex, iRow)
        lblAutoSum.Text = 0
    End Sub
    Sub AddToGrid1(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String, ByVal check As Integer)
        'Dim con As New SqlClient.SqlConnection
        Dim com As New SqlClient.SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As String = ""
        Try
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            com.CommandText = st
            dr = com.ExecuteReader
            dg.Rows.Clear()

            Do While dr.Read = True
                dg.Rows.Add()
                For j = 0 To n - 1
                    If IsDate(dr(j)) = True Then
                        dg.Rows(i).Cells(j).Value = FormatDateTime(dr(j), DateFormat.ShortDate)
                    Else
                        dg.Rows(i).Cells(j).Value = dr(j).ToString
                    End If
                Next j
                i += 1
                If check = 0 Then
                    dg.Rows(i - 1).Cells("coAmtPaid").Value = Format(Val(DataGridView1.Rows(i - 1).Cells("coAmtPaid").Value), "###,###.##")
                    DataGridView1.Rows(i - 1).Cells("coAmtToPay").Value = Format(Val(DataGridView1.Rows(i - 1).Cells("coAmtToPay").Value), "###,###.##")
                    DataGridView1.Columns("coLD_ID").DefaultCellStyle.BackColor = Color.Yellow
                    DataGridView1.Columns("coLD_ID").ReadOnly = True
                    DataGridView1.Columns("coCM_ID").DefaultCellStyle.BackColor = Color.Yellow
                    DataGridView1.Columns("coCM_ID").ReadOnly = True
                    DataGridView1.Columns("coCM_Name").DefaultCellStyle.BackColor = Color.Yellow
                    DataGridView1.Columns("coCM_Name").ReadOnly = True
                    DataGridView1.Columns("coCM_Address").DefaultCellStyle.BackColor = Color.Yellow
                    DataGridView1.Columns("coCM_Address").ReadOnly = True
                    DataGridView1.Columns("coAmtToPay").DefaultCellStyle.BackColor = Color.Yellow
                    DataGridView1.Columns("coAmtToPay").ReadOnly = True
                    DataGridView1.Columns("coDateToPay").DefaultCellStyle.BackColor = Color.Yellow
                    DataGridView1.Columns("coDateToPay").ReadOnly = True
                    DataGridView1.Columns("coLD_ID").DefaultCellStyle.BackColor = Color.Yellow
                    DataGridView1.Columns("coLD_ID").ReadOnly = True
                Else
                    dg.Rows(i - 1).Cells("coPaidWF").Value = Format(Val(dg.Rows(i - 1).Cells("coPaidWF").Value), "###,###.##")
                    dg.Rows(i - 1).Cells("coAmtToPayWF").Value = Format(Val(dg.Rows(i - 1).Cells("coAmtToPayWF").Value), "###,###.##")
                    dg.Columns("coLD_IDWF").DefaultCellStyle.BackColor = Color.Yellow
                    dg.Columns("coLD_IDWF").ReadOnly = True
                    dg.Columns("coCM_IDWF").DefaultCellStyle.BackColor = Color.Yellow
                    dg.Columns("coCM_IDWF").ReadOnly = True
                    dg.Columns("coCM_NameWF").DefaultCellStyle.BackColor = Color.Yellow
                    dg.Columns("coCM_NameWF").ReadOnly = True
                    dg.Columns("coCM_AddressWF").DefaultCellStyle.BackColor = Color.Yellow
                    dg.Columns("coCM_AddressWF").ReadOnly = True
                    dg.Columns("coAmtToPayWF").DefaultCellStyle.BackColor = Color.Yellow
                    dg.Columns("coAmtToPayWF").ReadOnly = True
                    dg.Columns("coDateToPayWF").DefaultCellStyle.BackColor = Color.Yellow
                    dg.Columns("coDateToPayWF").ReadOnly = True
                    dg.Columns("coLD_IDWF").DefaultCellStyle.BackColor = Color.Yellow
                    dg.Columns("coLD_IDWF").ReadOnly = True
                End If

            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        'Dim sql As String
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select top 1 * from BK_LoanRepay where LR_ID='" & DataGridView1.Rows(iRow).Cells(0).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"
        'On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim Des As String = oDt.Rows(0).Item(6).ToString
        Dim LR_Date As Date = oDt.Rows(0).Item(8).ToString
        Dim LR_Amount As Double = oDt.Rows(0).Item(9).ToString
        Dim LR_Charge As Double = oDt.Rows(0).Item(10).ToString
        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            If Des = .Cells(5).Value And LR_Date = .Cells(7).Value And LR_Amount = .Cells(6).Value And LR_Charge = .Cells(8).Value Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Private Function NoRecordChangeWF()
        Dim iRow = Me.dgWF.CurrentCell.RowIndex
        'Dim sql As String
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select top 1 * from BK_LoanRepay where LR_ID='" & dgWF.Rows(iRow).Cells(0).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"
        'On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim Des As String = oDt.Rows(0).Item(6).ToString
        Dim LR_Date As Date = oDt.Rows(0).Item(8).ToString
        Dim LR_Amount As Double = oDt.Rows(0).Item(9).ToString
        Dim LR_Charge As Double = oDt.Rows(0).Item(10).ToString
        Dim EM_ID As String = oDt.Rows(0).Item(5).ToString
        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
        With dgWF.Rows(iRow)
            If Des = .Cells("coDesWF").Value And LR_Date = .Cells("coDatePaidWF").Value And LR_Amount = .Cells("coPaidWF").Value And LR_Charge = .Cells("coChargeWF").Value And EM_ID = .Cells("coEMID").Value Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Sub datagrid2()
        SetFontDatagrid1(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 11
        DataGridView1.Columns(0).Name = "...."
        DataGridView1.Columns(1).Name = "កិច្ចសន្យា"
        DataGridView1.Columns(2).Name = "កូដអតិថិជន"
        DataGridView1.Columns(3).Name = "ឈ្មោះអតិថិជន"
        DataGridView1.Columns(4).Name = "អស័យដ្ឋាន"
        DataGridView1.Columns(5).Name = "អធិប្បាយ"
        DataGridView1.Columns(6).Name = "ប្រាក់បានបង់"
        DataGridView1.Columns(7).Name = "ថ្ងៃបានបង់"
        DataGridView1.Columns(8).Name = "ពិន័យ"
        DataGridView1.Columns(9).Name = "ប្រាក់ត្រូវបង់"
        DataGridView1.Columns(10).Name = "ថ្ងៃត្រូវបង់"
    End Sub
    Private Sub showRepay(ByVal check As Integer)
        If check = 1 Then
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select top 1 LR_ID,LD_ID,b.CM_ID,b.CM_KhName,c.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID 'Addresss' ,a.LR_Description,a.LR_Amount,Convert(Varchar(12),a.LR_Date,101),a.LR_Charge,a.SH_Total,Convert(Varchar(12),a.SH_Date,101) from BK_LoanRepay a inner join BK_Customer b on a.CM_ID=b.CM_ID and a.LR_BrID=b.CM_BrId and a.CM_ID1=b.ID inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrID where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "' order by LR_Date_Create desc"
            On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DataGridView1.Rows(iRow).Cells(0).Value = oDt.Rows(0).Item(0).ToString
            DataGridView1.Rows(iRow).Cells("coLD_ID").Value = oDt.Rows(0).Item(1).ToString
            DataGridView1.Rows(iRow).Cells("coCM_ID").Value = oDt.Rows(0).Item(2).ToString
            DataGridView1.Rows(iRow).Cells("coCM_Name").Value = oDt.Rows(0).Item(3).ToString
            DataGridView1.Rows(iRow).Cells("coCM_Address").Value = oDt.Rows(0).Item(4).ToString
            DataGridView1.Rows(iRow).Cells("coDes").Value = oDt.Rows(0).Item(5).ToString
            DataGridView1.Rows(iRow).Cells("coAmtPaid").Value = Format(Val(oDt.Rows(0).Item(6).ToString), "###,###.##")
            DataGridView1.Rows(iRow).Cells(7).Value = oDt.Rows(0).Item(7).ToString
            DataGridView1.Rows(iRow).Cells(8).Value = oDt.Rows(0).Item(8).ToString
            DataGridView1.Rows(iRow).Cells("coAmtToPay").Value = Format(Val(oDt.Rows(0).Item(9).ToString), "###,###.##")
            DataGridView1.Rows(iRow).Cells(10).Value = oDt.Rows(0).Item(10).ToString
            'Ctrl.DataSource = oDt
            oDa.Dispose()
            oDt.Dispose()
            'Dim iRow As Integer = Me.DataGridView1.Rows.Count
            DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
            DataGridView1.Rows(iRow).Cells("coLD_ID").Style.BackColor = Color.Yellow
            DataGridView1.Rows(iRow).Cells("coLD_ID").ReadOnly = True
            DataGridView1.Rows(iRow).Cells("coCM_ID").Style.BackColor = Color.Yellow
            DataGridView1.Rows(iRow).Cells("coCM_ID").ReadOnly = True
            DataGridView1.Rows(iRow).Cells("coCM_Name").Style.BackColor = Color.Yellow
            DataGridView1.Rows(iRow).Cells("coCM_Name").ReadOnly = True
            DataGridView1.Rows(iRow).Cells("coCM_Address").Style.BackColor = Color.Yellow
            DataGridView1.Rows(iRow).Cells("coCM_Address").ReadOnly = True
            DataGridView1.Rows(iRow).Cells("coAmtToPay").Style.BackColor = Color.Yellow
            DataGridView1.Rows(iRow).Cells("coAmtToPay").ReadOnly = True
            DataGridView1.Rows(iRow).Cells("coDateTopay").Style.BackColor = Color.Yellow
            DataGridView1.Rows(iRow).Cells("coDateTopay").ReadOnly = True
            DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").ColumnIndex, iRow)

        Else

            Dim iRow1 = Me.dgWF.CurrentCell.RowIndex
            Dim oDt1 As New System.Data.DataTable
            Dim Str1 As String = "select top 1 LR_ID,LD_ID,b.CM_ID,b.CM_KhName,c.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID 'Addresss' ,a.LR_Description,a.LR_Amount,Convert(Varchar(12),a.LR_Date,101),a.LR_Charge,a.SH_Total,Convert(Varchar(12),a.SH_Date,101),a.EM_ID,e.EM_Name from BK_LoanRepay a inner join BK_Customer b on a.CM_ID=b.CM_ID and a.LR_BrID=b.CM_BrId and a.CM_ID1=b.ID inner join BK_Employee e on a.EM_ID=e.EM_ID and a.LR_BrID=e.EM_BrID inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrID where LD_ID='" & Me.dgWF.Rows(iRow1).Cells(1).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "' order by LR_Date_Create desc"
            On Error Resume Next
            oDt1.Clear()
            Dim oDa1 As SqlDataAdapter
            oDa1 = New SqlDataAdapter(Str1, g_cnn)
            oDa1.Fill(oDt1)
            dgWF.Rows(iRow1).Cells(0).Value = oDt1.Rows(0).Item(0).ToString
            dgWF.Rows(iRow1).Cells("coLD_IDWF").Value = oDt1.Rows(0).Item(1).ToString
            dgWF.Rows(iRow1).Cells("coCM_IDWF").Value = oDt1.Rows(0).Item(2).ToString
            dgWF.Rows(iRow1).Cells("coCM_NameWF").Value = oDt1.Rows(0).Item(3).ToString
            dgWF.Rows(iRow1).Cells("coCM_AddressWF").Value = oDt1.Rows(0).Item(4).ToString
            dgWF.Rows(iRow1).Cells("coDesWF").Value = oDt1.Rows(0).Item(5).ToString
            dgWF.Rows(iRow1).Cells("coAmtPaidWF").Value = Format(Val(oDt1.Rows(0).Item(6).ToString), "###,###.##")
            dgWF.Rows(iRow1).Cells("coDatePaidWF").Value = oDt1.Rows(0).Item(7).ToString
            dgWF.Rows(iRow1).Cells("coChargeWF").Value = oDt1.Rows(0).Item(8).ToString
            dgWF.Rows(iRow1).Cells("coAmtToPayWF").Value = Format(Val(oDt1.Rows(0).Item(9).ToString), "###,###.##")
            dgWF.Rows(iRow1).Cells("coDateTopayWF").Value = oDt1.Rows(0).Item(10).ToString
            dgWF.Rows(iRow1).Cells("coEMID").Value = oDt1.Rows(0).Item(11).ToString
            dgWF.Rows(iRow1).Cells("coEMName").Value = oDt1.Rows(0).Item(12).ToString

            'Ctrl.DataSource = oDt
            oDa1.Dispose()
            oDt1.Dispose()
            'Dim iRow As Integer = Me.DataGridView1.Rows.Count
            dgWF.Rows(iRow1).Cells(0).Style.BackColor = Color.Yellow
            dgWF.Rows(iRow1).Cells("coLD_IDWF").Style.BackColor = Color.Yellow
            dgWF.Rows(iRow1).Cells("coLD_IDWF").ReadOnly = True
            dgWF.Rows(iRow1).Cells("coCM_IDWF").Style.BackColor = Color.Yellow
            dgWF.Rows(iRow1).Cells("coCM_IDWF").ReadOnly = True
            dgWF.Rows(iRow1).Cells("coCM_NameWF").Style.BackColor = Color.Yellow
            dgWF.Rows(iRow1).Cells("coCM_NameWF").ReadOnly = True
            dgWF.Rows(iRow1).Cells("coCM_AddressWF").Style.BackColor = Color.Yellow
            dgWF.Rows(iRow1).Cells("coCM_AddressWF").ReadOnly = True
            dgWF.Rows(iRow1).Cells("coAmtToPayWF").Style.BackColor = Color.Yellow
            dgWF.Rows(iRow1).Cells("coAmtToPayWF").ReadOnly = True
            dgWF.Rows(iRow1).Cells("coDateTopayWF").Style.BackColor = Color.Yellow
            dgWF.Rows(iRow1).Cells("coDateTopayWF").ReadOnly = True
            dgWF.CurrentCell = dgWF(Me.dgWF.Rows(iRow1).Cells("coLD_IDWF").ColumnIndex, iRow1)

        End If

    End Sub
    Private Sub showListRepay(ByVal check As Integer)
        Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
        Dim EM_ID As String
        If lblEmployee.Text = "All" Then
            EM_ID = "All"
        Else
            EM_ID = ComboBox1.Text
        End If
        If check = 0 Then
            AddToGrid1(DataGridView1, 11, "exec spListLoanRepay '" & date1 & "','" & frmMain.lblCode.Text & "','" & EM_ID & "','0'", 0)
        Else
            AddToGrid1(dgWF, 13, "exec spListLoanRepay '" & date1 & "','" & frmMain.lblCode.Text & "','" & EM_ID & "','1'", 1)
        End If

    End Sub
    Sub datagrid3()
        SetFontDatagrid1(DataGridView2)
        DataGridView2.Columns.Clear()
        DataGridView2.ColumnCount = 10
        DataGridView2.Columns(0).Name = "ថ្ងៃត្រូវបង់"
        DataGridView2.Columns(1).Name = "ប្រាក់ត្រូវបង់"
        DataGridView2.Columns(2).Name = "ប្រាក់ដើម"
        DataGridView2.Columns(3).Name = "ការប្រាក់"
        DataGridView2.Columns(4).Name = "សម្យតុល"
        DataGridView2.Columns(5).Name = "បង់ផ្តាច់"
        DataGridView2.Columns(6).Name = "ប្រាក់បានបង់"
        DataGridView2.Columns(7).Name = "ពិន័យ"
        DataGridView2.Columns(8).Name = "អធិប្បាយ"
        DataGridView2.Columns(9).Name = "ថ្ងៃបានបង់"
    End Sub
    Public Sub UpdateRepay(ByVal LR_Description As String, ByVal LR_Date As Date, ByVal LR_Amount As Double, ByVal LR_Charge As Double, ByVal LR_User_Modify As String, ByVal LR_Date_Modify As DateTime, ByVal prn As Double, ByVal int As Double, ByVal LR_Service As Double)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = LR_Description
                .Add("@d1", SqlDbType.Date).Value = LR_Date
                .Add("@d2", SqlDbType.Float).Value = LR_Amount
                .Add("@d3", SqlDbType.Float).Value = LR_Charge
                .Add("@d4", SqlDbType.NVarChar).Value = LR_User_Modify
                .Add("@d5", SqlDbType.DateTime).Value = LR_Date_Modify
                .Add("@d6", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells("coAmtToPay").Value
                .Add("@d7", SqlDbType.Float).Value = prn
                .Add("@d8", SqlDbType.Float).Value = int
                .Add("@d9", SqlDbType.Float).Value = LR_Service
            End With
            sql = "update BK_LoanRepay set LR_Description=@d0, LR_Date=@d1,LR_Amount=@d2,LR_Charge=@d3,LR_User_Modify=@d4,LR_Date_Modify=@d5,SH_Total=@d6,Prn=@d7,Int=@d8,LR_Service=@d9 where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and LR_ID='" & Me.DataGridView1.Rows(iRow).Cells(0).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As SystemException
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub UpdateRepayWF(ByVal LR_Description As String, ByVal LR_Date As Date, ByVal LR_Amount As Double, ByVal LR_Charge As Double, ByVal LR_User_Modify As String, ByVal LR_Date_Modify As DateTime, ByVal prn As Double, ByVal int As Double, ByVal LR_Service As Double,
                             ByVal EM_ID As String)
        Dim iRow = Me.dgWF.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = LR_Description
                .Add("@d1", SqlDbType.Date).Value = LR_Date
                .Add("@d2", SqlDbType.Float).Value = LR_Amount
                .Add("@d3", SqlDbType.Float).Value = LR_Charge
                .Add("@d4", SqlDbType.NVarChar).Value = LR_User_Modify
                .Add("@d5", SqlDbType.DateTime).Value = LR_Date_Modify
                .Add("@d6", SqlDbType.Float).Value = Me.dgWF.Rows(iRow).Cells("coAmtToPayWF").Value
                .Add("@d7", SqlDbType.Float).Value = prn
                .Add("@d8", SqlDbType.Float).Value = int
                .Add("@d9", SqlDbType.Float).Value = LR_Service
                .Add("@d10", SqlDbType.NVarChar).Value = EM_ID
            End With
            sql = "update BK_LoanRepay set LR_Description=@d0, LR_Date=@d1,LR_Amount=@d2,LR_Charge=@d3,LR_User_Modify=@d4,LR_Date_Modify=@d5,SH_Total=@d6,Prn=@d7,Int=@d8,LR_Service=@d9,EM_ID=@d10 where LD_ID='" & Me.dgWF.Rows(iRow).Cells(1).Value & "' and LR_ID='" & Me.dgWF.Rows(iRow).Cells(0).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As SystemException
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub addSpecialRepay(ByVal LD_ID As Integer, ByVal CM_ID As Integer, ByVal LR_BrID As String, ByVal SH_Date As Date, ByVal EM_ID As String, _
                         ByVal LR_Description As String, ByVal SH_Total As Double, ByVal LR_Date As Date, ByVal LR_Amount As Double, ByVal LR_Charge As Double, _
                         ByVal LR_Rec_Status As Integer, ByVal LR_User_Create As String, ByVal LR_Date_Create As DateTime, ByVal IsExport As Integer, ByVal CM_ID1 As Integer, ByVal prn As Double, ByVal int As Double, ByVal LD_Service As Double, ByVal Special As Integer)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LD_ID
                .Add("@d1", SqlDbType.Int).Value = CM_ID
                .Add("@d2", SqlDbType.NVarChar).Value = LR_BrID
                .Add("@d3", SqlDbType.Date).Value = SH_Date
                .Add("@d4", SqlDbType.NVarChar).Value = EM_ID
                .Add("@d5", SqlDbType.NVarChar).Value = LR_Description
                .Add("@d6", SqlDbType.Float).Value = SH_Total
                .Add("@d7", SqlDbType.Date).Value = LR_Date
                .Add("@d8", SqlDbType.Float).Value = LR_Amount
                .Add("@d9", SqlDbType.Float).Value = LR_Charge
                .Add("@d10", SqlDbType.Int).Value = LR_Rec_Status
                .Add("@d11", SqlDbType.NVarChar).Value = LR_User_Create
                .Add("@d12", SqlDbType.DateTime).Value = LR_Date_Create
                .Add("@d13", SqlDbType.Int).Value = IsExport
                .Add("@d14", SqlDbType.Int).Value = CM_ID1
                .Add("@d15", SqlDbType.Float).Value = prn
                .Add("@d16", SqlDbType.Float).Value = int
                .Add("@d17", SqlDbType.Float).Value = LD_Service
            End With
            sql = "insert BK_LoanRepay(LD_ID,CM_ID,LR_BrID,SH_Date,EM_ID,LR_Description,SH_Total,LR_Date,LR_Amount,LR_Charge,LR_Rec_Status,LR_User_Create,LR_Date_Create,IsExport,CM_ID1,Prn,Int,LR_Service,Mark) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,1)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub addRepay(ByVal LD_ID As Integer, ByVal CM_ID As Integer, ByVal LR_BrID As String, ByVal SH_Date As Date, ByVal EM_ID As String, _
                         ByVal LR_Description As String, ByVal SH_Total As Double, ByVal LR_Date As Date, ByVal LR_Amount As Double, ByVal LR_Charge As Double, _
                         ByVal LR_Rec_Status As Integer, ByVal LR_User_Create As String, ByVal LR_Date_Create As DateTime, ByVal IsExport As Integer, ByVal CM_ID1 As Integer, ByVal prn As Double, ByVal int As Double, ByVal LD_Service As Double, ByVal remark As Integer)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LD_ID
                .Add("@d1", SqlDbType.Int).Value = CM_ID
                .Add("@d2", SqlDbType.NVarChar).Value = LR_BrID
                .Add("@d3", SqlDbType.Date).Value = SH_Date
                .Add("@d4", SqlDbType.NVarChar).Value = EM_ID
                .Add("@d5", SqlDbType.NVarChar).Value = LR_Description
                .Add("@d6", SqlDbType.Float).Value = SH_Total
                .Add("@d7", SqlDbType.Date).Value = LR_Date
                .Add("@d8", SqlDbType.Float).Value = LR_Amount
                .Add("@d9", SqlDbType.Float).Value = LR_Charge
                .Add("@d10", SqlDbType.Int).Value = LR_Rec_Status
                .Add("@d11", SqlDbType.NVarChar).Value = LR_User_Create
                .Add("@d12", SqlDbType.DateTime).Value = LR_Date_Create
                .Add("@d13", SqlDbType.Int).Value = IsExport
                .Add("@d14", SqlDbType.Int).Value = CM_ID1
                .Add("@d15", SqlDbType.Float).Value = prn
                .Add("@d16", SqlDbType.Float).Value = int
                .Add("@d17", SqlDbType.Float).Value = LD_Service
                .Add("@d20", SqlDbType.int).Value = remark
                'Messagebox.Show(remark)
            End With
            sql = "insert BK_LoanRepay(LD_ID,CM_ID,LR_BrID,SH_Date,EM_ID,LR_Description,SH_Total,LR_Date,LR_Amount,LR_Charge,LR_Rec_Status,LR_User_Create,LR_Date_Create,IsExport,CM_ID1,Prn,Int,LR_Service,Mark) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d20)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub AddTrace_Repay1(ByVal RecordAction As String, ByVal mark As Integer)
        'Dim RecordAction As String+
        Dim LR_BrID, LR_Description, LR_User_Create, LR_User_Modify As String
        Dim SH_Total, LR_Charge, LR_Amount As Double
        Dim SH_Date, LR_Date As Date
        Dim LR_ID, LD_ID, CM_ID, EM_ID, LR_Rec_Status As Integer
        Dim LR_Date_Create, LR_Date_Modify, DateAction As DateTime
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            'Dim sql As String
            Dim oDt As New System.Data.DataTable
            Dim Str As String
            If mark = 1 Then
                Str = "select * from BK_LoanRepay where LR_ID='" & DataGridView1.Rows(iRow).Cells(0).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"
            Else
                Str = "select * from BK_LoanRepay where LR_ID='" & dgWF.Rows(Me.dgWF.CurrentCell.RowIndex).Cells(0).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"
            End If

            'On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            LR_ID = oDt.Rows(0).Item(0).ToString
            LD_ID = oDt.Rows(0).Item(1).ToString
            CM_ID = oDt.Rows(0).Item(2).ToString
            LR_BrID = oDt.Rows(0).Item(3).ToString
            SH_Date = oDt.Rows(0).Item(4).ToString
            EM_ID = oDt.Rows(0).Item(5).ToString
            LR_Description = oDt.Rows(0).Item(6).ToString
            SH_Total = oDt.Rows(0).Item(7).ToString
            LR_Date = oDt.Rows(0).Item(8).ToString
            LR_Amount = oDt.Rows(0).Item(9).ToString
            LR_Charge = oDt.Rows(0).Item(10).ToString
            If oDt.Rows(0).Item(12).ToString = True Then
                LR_Rec_Status = 1
            Else
                LR_Rec_Status = 0
            End If
            LR_User_Create = oDt.Rows(0).Item(13).ToString
            LR_Date_Create = oDt.Rows(0).Item(14).ToString
            DateAction = DateTime.Now
            If RecordAction = "DELETE" Then
                If oDt.Rows(0).Item(15).ToString = "" Then
                    addTraceDelete1(LR_ID, DateAction, RecordAction, LD_ID, CM_ID, LR_BrID, SH_Date, EM_ID, LR_Description, SH_Total, LR_Date, LR_Amount, LR_Charge, LR_Rec_Status, LR_User_Create, LR_Date_Create, frmMain.users, DateTime.Now)
                Else
                    LR_User_Modify = oDt.Rows(0).Item(15).ToString
                    LR_Date_Modify = oDt.Rows(0).Item(16).ToString
                    addTraceDelete2(LR_ID, DateAction, RecordAction, LD_ID, CM_ID, LR_BrID, SH_Date, EM_ID, LR_Description, SH_Total, LR_Date, LR_Amount, LR_Charge, LR_Rec_Status, LR_User_Create, LR_Date_Create, LR_User_Modify, LR_Date_Modify, frmMain.users, DateTime.Now)
                End If
            Else
                If oDt.Rows(0).Item(15).ToString = "" Then
                    addTraceFirst(LR_ID, DateAction, RecordAction, LD_ID, CM_ID, LR_BrID, SH_Date, EM_ID, LR_Description, SH_Total, LR_Date, LR_Amount, LR_Charge, LR_Rec_Status, LR_User_Create, LR_Date_Create)
                Else
                    LR_User_Modify = oDt.Rows(0).Item(15).ToString
                    LR_Date_Modify = oDt.Rows(0).Item(16).ToString
                    addTraceSecond(LR_ID, DateAction, RecordAction, LD_ID, CM_ID, LR_BrID, SH_Date, EM_ID, LR_Description, SH_Total, LR_Date, LR_Amount, LR_Charge, LR_Rec_Status, LR_User_Create, LR_Date_Create, LR_User_Modify, LR_Date_Modify)
                End If
            End If

            oDa.Dispose()
            oDt.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub addTraceFirst(ByVal LR_ID As Integer, ByVal DateAction As DateTime, ByVal RecordAction As String, ByVal LD_ID As Integer, ByVal CM_ID As Integer, _
                     ByVal LR_BrID As String, ByVal SH_Date As Date, ByVal EM_ID As Integer, ByVal LR_Description As String, ByVal SH_Total As Double, _
                     ByVal LR_Date As Date, ByVal LR_Amount As Double, ByVal LR_Charge As Double, ByVal LR_Rec_Status As Integer, ByVal LR_User_Create As String, ByVal LR_Date_Create As DateTime)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LR_ID
                .Add("@d1", SqlDbType.DateTime).Value = DateAction
                .Add("@d2", SqlDbType.NVarChar).Value = RecordAction
                .Add("@d3", SqlDbType.Int).Value = LD_ID
                .Add("@d4", SqlDbType.Int).Value = CM_ID
                .Add("@d5", SqlDbType.NVarChar).Value = LR_BrID
                .Add("@d6", SqlDbType.Date).Value = SH_Date
                .Add("@d7", SqlDbType.Int).Value = EM_ID
                .Add("@d8", SqlDbType.NVarChar).Value = LR_Description
                .Add("@d9", SqlDbType.Float).Value = SH_Total
                .Add("@d10", SqlDbType.Date).Value = LR_Date
                .Add("@d11", SqlDbType.Float).Value = LR_Amount
                .Add("@d12", SqlDbType.Float).Value = LR_Charge
                .Add("@d13", SqlDbType.Int).Value = LR_Rec_Status
                .Add("@d14", SqlDbType.NVarChar).Value = LR_User_Create
                .Add("@d15", SqlDbType.DateTime).Value = LR_Date_Create
            End With
            sql = "insert TRACE_LoanRepay(LR_ID,DateAction,RecordAction,LD_ID,CM_ID,LR_BrID,SH_Date,EM_ID,LR_Description,SH_Total,LR_Date,LR_Amount,LR_Charge,LR_Rec_Status,LR_User_Create,LR_Date_Create) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub addTraceSecond(ByVal LR_ID As Integer, ByVal DateAction As DateTime, ByVal RecordAction As String, ByVal LD_ID As Integer, ByVal CM_ID As Integer, _
                 ByVal LR_BrID As String, ByVal SH_Date As Date, ByVal EM_ID As Integer, ByVal LR_Description As String, ByVal SH_Total As Double, _
                 ByVal LR_Date As Date, ByVal LR_Amount As Double, ByVal LR_Charge As Double, ByVal LR_Rec_Status As Integer, ByVal LR_User_Create As String, _
                 ByVal LR_Date_Create As DateTime, ByVal LR_User_Modify As String, ByVal LR_Date_Modify As DateTime)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LR_ID
                .Add("@d1", SqlDbType.DateTime).Value = DateAction
                .Add("@d2", SqlDbType.NVarChar).Value = RecordAction
                .Add("@d3", SqlDbType.Int).Value = LD_ID
                .Add("@d4", SqlDbType.Int).Value = CM_ID
                .Add("@d5", SqlDbType.NVarChar).Value = LR_BrID
                .Add("@d6", SqlDbType.Date).Value = SH_Date
                .Add("@d7", SqlDbType.Int).Value = EM_ID
                .Add("@d8", SqlDbType.NVarChar).Value = LR_Description
                .Add("@d9", SqlDbType.Float).Value = SH_Total
                .Add("@d10", SqlDbType.Date).Value = LR_Date
                .Add("@d11", SqlDbType.Float).Value = LR_Amount
                .Add("@d12", SqlDbType.Float).Value = LR_Charge
                .Add("@d13", SqlDbType.Int).Value = LR_Rec_Status
                .Add("@d14", SqlDbType.NVarChar).Value = LR_User_Create
                .Add("@d15", SqlDbType.DateTime).Value = LR_Date_Create
                .Add("@d16", SqlDbType.NVarChar).Value = LR_User_Modify
                .Add("@d17", SqlDbType.DateTime).Value = LR_Date_Modify
            End With
            sql = "insert TRACE_LoanRepay(LR_ID,DateAction,RecordAction,LD_ID,CM_ID,LR_BrID,SH_Date,EM_ID,LR_Description,SH_Total,LR_Date,LR_Amount,LR_Charge,LR_Rec_Status,LR_User_Create,LR_Date_Create,LR_User_Modify,LR_Date_Modify) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub addTraceDelete2(ByVal LR_ID As Integer, ByVal DateAction As DateTime, ByVal RecordAction As String, ByVal LD_ID As Integer, ByVal CM_ID As Integer, _
                ByVal LR_BrID As String, ByVal SH_Date As Date, ByVal EM_ID As Integer, ByVal LR_Description As String, ByVal SH_Total As Double, _
                ByVal LR_Date As Date, ByVal LR_Amount As Double, ByVal LR_Charge As Double, ByVal LR_Rec_Status As Integer, ByVal LR_User_Create As String, _
                ByVal LR_Date_Create As DateTime, ByVal LR_User_Modify As String, ByVal LR_Date_Modify As DateTime, ByVal LR_User_Delete As String, ByVal LR_Date_Delete As DateTime)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LR_ID
                .Add("@d1", SqlDbType.DateTime).Value = DateAction
                .Add("@d2", SqlDbType.NVarChar).Value = RecordAction
                .Add("@d3", SqlDbType.Int).Value = LD_ID
                .Add("@d4", SqlDbType.Int).Value = CM_ID
                .Add("@d5", SqlDbType.NVarChar).Value = LR_BrID
                .Add("@d6", SqlDbType.Date).Value = SH_Date
                .Add("@d7", SqlDbType.Int).Value = EM_ID
                .Add("@d8", SqlDbType.NVarChar).Value = LR_Description
                .Add("@d9", SqlDbType.Float).Value = SH_Total
                .Add("@d10", SqlDbType.Date).Value = LR_Date
                .Add("@d11", SqlDbType.Float).Value = LR_Amount
                .Add("@d12", SqlDbType.Float).Value = LR_Charge
                .Add("@d13", SqlDbType.Int).Value = LR_Rec_Status
                .Add("@d14", SqlDbType.NVarChar).Value = LR_User_Create
                .Add("@d15", SqlDbType.DateTime).Value = LR_Date_Create
                .Add("@d16", SqlDbType.NVarChar).Value = LR_User_Modify
                .Add("@d17", SqlDbType.DateTime).Value = LR_Date_Modify
                .Add("@d18", SqlDbType.NVarChar).Value = LR_User_Delete
                .Add("@d19", SqlDbType.DateTime).Value = LR_Date_Delete
            End With
            sql = "insert TRACE_LoanRepay(LR_ID,DateAction,RecordAction,LD_ID,CM_ID,LR_BrID,SH_Date,EM_ID,LR_Description,SH_Total,LR_Date,LR_Amount,LR_Charge,LR_Rec_Status,LR_User_Create,LR_Date_Create,LR_User_Modify,LR_Date_Modify,LR_User_Delete,LR_Date_Delete) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub addTraceDelete1(ByVal LR_ID As Integer, ByVal DateAction As DateTime, ByVal RecordAction As String, ByVal LD_ID As Integer, ByVal CM_ID As Integer, _
               ByVal LR_BrID As String, ByVal SH_Date As Date, ByVal EM_ID As Integer, ByVal LR_Description As String, ByVal SH_Total As Double, _
               ByVal LR_Date As Date, ByVal LR_Amount As Double, ByVal LR_Charge As Double, ByVal LR_Rec_Status As Integer, ByVal LR_User_Create As String, _
               ByVal LR_Date_Create As DateTime, ByVal LR_User_Delete As String, ByVal LR_Date_Delete As DateTime)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LR_ID
                .Add("@d1", SqlDbType.DateTime).Value = DateAction
                .Add("@d2", SqlDbType.NVarChar).Value = RecordAction
                .Add("@d3", SqlDbType.Int).Value = LD_ID
                .Add("@d4", SqlDbType.Int).Value = CM_ID
                .Add("@d5", SqlDbType.NVarChar).Value = LR_BrID
                .Add("@d6", SqlDbType.Date).Value = SH_Date
                .Add("@d7", SqlDbType.Int).Value = EM_ID
                .Add("@d8", SqlDbType.NVarChar).Value = LR_Description
                .Add("@d9", SqlDbType.Float).Value = SH_Total
                .Add("@d10", SqlDbType.Date).Value = LR_Date
                .Add("@d11", SqlDbType.Float).Value = LR_Amount
                .Add("@d12", SqlDbType.Float).Value = LR_Charge
                .Add("@d13", SqlDbType.Int).Value = LR_Rec_Status
                .Add("@d14", SqlDbType.NVarChar).Value = LR_User_Create
                .Add("@d15", SqlDbType.DateTime).Value = LR_Date_Create
                .Add("@d16", SqlDbType.NVarChar).Value = LR_User_Delete
                .Add("@d17", SqlDbType.DateTime).Value = LR_Date_Delete
            End With
            sql = "insert TRACE_LoanRepay(LR_ID,DateAction,RecordAction,LD_ID,CM_ID,LR_BrID,SH_Date,EM_ID,LR_Description,SH_Total,LR_Date,LR_Amount,LR_Charge,LR_Rec_Status,LR_User_Create,LR_Date_Create,LR_User_Delete,LR_Date_Delete) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        'MessageBox.Show()
        Try
            '---------------------------------------------------- sum amount paid
            If 6 = DataGridView1.Columns("coAmtPaid").Index Then
                Dim total As Double = DataGridView1.SelectedCells.Cast(Of DataGridViewCell)().Sum(Function(cell) CDec(cell.Value))
                lblResultSum.Text = total.ToString("##,###.00")
                '--- for count column selected
                lblResultCount.Text = DataGridView1.SelectedCells.Count.ToString
            Else
                lblResultCount.Text = 0
                lblResultSum.Text = 0.0
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub DataGridView2_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView2.SelectionChanged
        Try
            '---------------------------------------------------- sum amount paid
            Dim total As Double = DataGridView2.SelectedCells.Cast(Of DataGridViewCell)().Sum(Function(cell) CDec(cell.Value))
            lblResultSum.Text = total.ToString("##,###.00")
            lblResultCount.Text = DataGridView2.SelectedCells.Count.ToString
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        newRow()
    End Sub
    Private Sub AutoSum()
        Dim total As String = 0

        For i As Integer = 0 To DataGridView1.RowCount - 1
            Dim as1 As Integer = CInt(DataGridView1.Rows(i).Cells("coAmtPaid").Value) + CInt(DataGridView1.Rows(i).Cells("coCharge").Value)
            total += as1
        Next
        lblAutoSum.Text = Format(Val(total), "###,###.##")
    End Sub
    Private Sub AutoSumWF()
        Dim total As String = 0

        For i As Integer = 0 To dgWF.RowCount - 1
            Dim as1 As Integer = CInt(dgWF.Rows(i).Cells("coPaidWF").Value) + CInt(dgWF.Rows(i).Cells("coChargeWF").Value)
            total += as1
        Next
        lblAutoSum.Text = Format(Val(total), "###,###.##")
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        AutoSum()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim name As String = getData("select top 1 EM_Name from BK_Employee where EM_BrID ='" & frmMain.lblCode.Text & "' and EM_ID='" & ComboBox1.Text & "'")
        If name = "" Then
            lblEmployee.Text = "All"
        Else
            lblEmployee.Text = name
        End If
    End Sub
    Private Sub ComboBox1_TextChanged(sender As Object, e As EventArgs) Handles ComboBox1.TextChanged
        Dim name As String = getData("select top 1 EM_Name from BK_Employee where EM_BrID ='" & frmMain.lblCode.Text & "' and EM_ID='" & ComboBox1.Text & "'")
        If name = "" Then
            lblEmployee.Text = "All"
        Else
            lblEmployee.Text = name
        End If
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
            ' ''-----------------------------------------------------------------------------
            'MessageBox.Show(frmMain.strPath.ToString)
            'Return
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\Sample\LoanAudit.xls", False, True)
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
                Dim Str As String = "select top 1 a.LD_ID,a.CM_ID,b.CM_KhName,c.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID [Address],a.LD_Unit,a.LD_Type,convert(nvarchar(12),a.LD_Dis_Date,101)Dis_Date,LD_Term,LD_Dis_Amt,LD_IntRate,CU_ID from BK_Loan a left join BK_Customer b on a.CM_ID=b.CM_ID and a.CM_ID1=b.ID and a.LD_BrId=b.CM_BrId left join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=LO_BrID left join BK_Employee d on a.EM_ID=d.EM_ID and a.LD_BrId=d.EM_BrID where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'"
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
    Private Sub radCuId_CheckedChanged(sender As Object, e As EventArgs) Handles radCuId.CheckedChanged
        txtCmName.Text = ""
        txtCmName.Enabled = False
        txtLoanid.Text = ""
        txtLoanid.Enabled = False
        txtCmId.Text = ""
        txtCmId.Enabled = True
        txtCmId.Focus()
    End Sub
    Private Sub radCuName_CheckedChanged(sender As Object, e As EventArgs) Handles radCuName.CheckedChanged
        txtCmName.Text = ""
        txtCmName.Enabled = True
        txtCmName.Focus()
        txtLoanid.Text = ""
        txtLoanid.Enabled = False
        txtCmId.Text = ""
        txtCmId.Enabled = False
    End Sub
    Private Sub radLoanId_CheckedChanged(sender As Object, e As EventArgs) Handles radLoanId.CheckedChanged
        txtCmName.Text = ""
        txtCmName.Enabled = False
        txtLoanid.Text = ""
        txtLoanid.Enabled = True
        txtLoanid.Focus()
        txtCmId.Text = ""
        txtCmId.Enabled = False
    End Sub
    Private Sub DataGridView3_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellDoubleClick
        Try
            Dim cm As String = Me.DataGridView3.Rows(DataGridView3.CurrentRow.Index).Cells(6).Value.ToString
            If cm = "" Then
                Return
            Else
                ShowDataGrid(DataGridView4, "select LD_ID,LD_BrId,CM_ID,convert(nvarchar(12),LD_Dis_Date,101)Dis_Date,convert(nvarchar(12),LD_First_Date,101)FirstDate,convert(nvarchar(12),LD_Mat_Date,101)EndDate,LD_Dis_Amt Dis_Amt,CU_ID,LD_IntRate,a.EM_ID,b.EM_Name,LD_Unit,LD_Type,LD_Term,LD_ChargeRate ChargeRate,LD_ChargeAmt ChargeAmt,case when LD_Service=1 then 'Yes' else 'No' end 'OP.Fee',LD_Status from BK_Loan a left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID where CM_ID1='" & cm & "' and LD_BrId='" & frmMain.lblCode.Text & "' order by Cast(a.LD_ID as int) desc")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub txtCmId_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCmId.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.txtCmId.Text = "" Then
                Return
            Else
                Dim CM_ID1 As String = getData("Select * from BK_Customer where CM_ID='" & txtCmId.Text & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                If CM_ID1 = "" Then
                    MessageBox.Show("No this customer please check again.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Else
                    ShowDataGrid(Me.DataGridView3, "select CM_ID,CM_KhName,CM_Phone,a.LO_ID,VL_ID+','+CN_ID+','+b.DT_ID+','+PV_ID 'Address',LD_Cycle,ID 'RealID' from BK_Customer a left join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID where CM_ID='" & Me.txtCmId.Text & "' and CM_BrId ='" & frmMain.lblCode.Text & "'")
                End If
            End If
        End If
    End Sub
    Private Sub bigger()
        '----------------------------------------------------------------------- Declare paramet
        Dim i = 0
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        Dim LR_ID As Integer = getData("select top 1 MAX(LR_ID) from BK_LoanRepay where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "' group by SH_Date order by SH_Date desc")
        Dim Max_SH As Date = FormatDateTime(getData("select  max(SH_Date) SH_Date from BK_LoanSchedule where LD_ID='" & DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
        Dim LR_Amt As Double = Me.DataGridView1.Rows(iRow).Cells("coAmtPaid").Value
        Dim prn As Double = 0
        Dim int As Double = 0
        Dim LD_Service As Double = 0
        Dim ToPay As Double = 0
        Dim LD_ID As String = DataGridView1.Rows(iRow).Cells("coLD_ID").Value
        Dim LD_Status As String = getData("select top 1 LD_Status from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
        Dim a As Date = getData("select top 1 SH_Date from BK_LoanRepay where LR_ID='" & LR_ID & "'")
        'Dim b As Date
        'Dim sh_date_in_repay As Date
        '-------------------------------------------------------------------------
        prn = ReturnPrn(0, LD_ID, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
        int = ReturnInt(0, LD_ID, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
        LD_Service = ReturnService(0, LD_ID, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
        ToPay = prn + int + LD_Service
        While LR_Amt > ToPay And LD_Status = "Active"
            prn = ReturnPrn(0, LD_ID, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
            int = ReturnInt(0, LD_ID, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
            LD_Service = ReturnService(0, LD_ID, Me.DataGridView1.Rows(iRow).Cells("coDateToPay").Value, Me.DataGridView1.Rows(iRow).Cells("coNo").Value)
            ToPay = prn + int + LD_Service
            If i = 0 Then
                If Max_SH = a Then
                    frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                    addIn("Update BK_Loan set LD_Status='Mature',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "',Date_Payoff='" & DataGridView1.Rows(iRow).Cells(7).Value & "' where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                    frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                    int = LR_Amt - (prn + LD_Service)
                    UpdateRepay(DataGridView1.Rows(iRow).Cells("coDes").Value, DataGridView1.Rows(iRow).Cells("coDatePaid").Value, prn + int + LD_Service, DataGridView1.Rows(iRow).Cells("coCharge").Value, frmMain.users, DateTime.Now, prn, int, LD_Service)
                    AddTrace_Repay1("UPDATE NEW", 1)
                    toExcelFormat(LD_ID)
                ElseIf Max_SH <> a Then
                    UpdateRepay(DataGridView1.Rows(iRow).Cells("coDes").Value, DataGridView1.Rows(iRow).Cells("coDatePaid").Value, ToPay, DataGridView1.Rows(iRow).Cells("coCharge").Value, frmMain.users, DateTime.Now, prn, int, LD_Service)
                    AddTrace_Repay1("UPDATE NEW", 1)
                End If
                LD_Status = getData("select top 1 LD_Status from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                i = i + 1
                LR_Amt = LR_Amt - ToPay
                showRepay(1)
                newRow()
            Else
                '--------------------------------------------------
                insertRepay1(LD_ID, LR_Amt)
                LD_Status = getData("select top 1 LD_Status from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                Dim topay1 As Double = getData("select top 1 LR_Amount from BK_LoanRepay where LD_ID='" & LD_ID & "' and LR_BrID='" & frmMain.lblCode.Text & "' order by LR_ID desc")
                LR_Amt = LR_Amt - topay1
            End If
        End While
        LD_Status = getData("select top 1 LD_Status from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
        If LR_Amt > 0 And LD_Status = "Active" Then
            If prn >= LR_Amt Then
                prn = LR_Amt
                int = 0
                LD_Service = 0
            ElseIf LR_Amt > prn And LR_Amt <= prn + int Then
                int = LR_Amt - prn
                LD_Service = 0
            ElseIf LR_Amt > prn + int And LR_Amt <= prn + int + LD_Service Then
                int = int
                LD_Service = LR_Amt - (prn + int)
            End If
            insertRepay1(LD_ID, LR_Amt)
            'insertRepay1(DataGridView1.Rows(iRow).Cells("coLD_ID").Value,prn,
        End If

        Return
    End Sub

    Private Sub biggerWF()
        '----------------------------------------------------------------------- Declare paramet
        Dim i = 0
        Dim iRow = Me.dgWF.CurrentCell.RowIndex
        Dim iCol = dgWF.CurrentCell.ColumnIndex
        Dim LR_ID As Integer = getData("select top 1 MAX(LR_ID) from BK_LoanRepay where LD_ID='" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "' group by SH_Date order by SH_Date desc")
        Dim Max_SH As Date = FormatDateTime(getData("select  max(SH_Date) SH_Date from BK_LoanSchedule where LD_ID='" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
        Dim LR_Amt As Double = Me.dgWF.Rows(iRow).Cells("coPaidWF").Value
        Dim prn As Double = 0
        Dim int As Double = 0
        Dim LD_Service As Double = 0
        Dim ToPay As Double = 0
        Dim LD_ID As String = dgWF.Rows(iRow).Cells("coLD_IDWF").Value
        Dim LD_Status As String = getData("select top 1 LD_Status from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
        Dim a As Date = getData("select top 1 SH_Date from BK_LoanRepay where LR_ID='" & LR_ID & "'")
        'Dim b As Date
        'Dim sh_date_in_repay As Date
        '-------------------------------------------------------------------------
        prn = ReturnPrn(0, LD_ID, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
        int = ReturnInt(0, LD_ID, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
        LD_Service = ReturnService(0, LD_ID, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
        ToPay = prn + int + LD_Service
        While LR_Amt > ToPay And LD_Status = "Active"
            prn = ReturnPrn(0, LD_ID, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
            int = ReturnInt(0, LD_ID, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
            LD_Service = ReturnService(0, LD_ID, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
            ToPay = prn + int + LD_Service
            If i = 0 Then
                If Max_SH = a Then
                    frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                    addIn("Update BK_Loan set LD_Status='Mature',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "',Date_Payoff='" & dgWF.Rows(iRow).Cells("coDatePaidWF").Value & "' where LD_ID='" & Me.dgWF.Rows(iRow).Cells(1).Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                    frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                    int = LR_Amt - (prn + LD_Service)
                    UpdateRepayWF(dgWF.Rows(iRow).Cells("coDesWF").Value, dgWF.Rows(iRow).Cells("coDatePaidWF").Value, prn + int + LD_Service, dgWF.Rows(iRow).Cells("coChargeWF").Value, frmMain.users, DateTime.Now, prn, int, LD_Service, dgWF.Rows(iRow).Cells("coEMID").Value.ToString())
                    AddTrace_Repay1("UPDATE NEW", 2)
                    toExcelFormat(LD_ID)
                ElseIf Max_SH <> a Then
                    UpdateRepayWF(dgWF.Rows(iRow).Cells("coDesWF").Value, dgWF.Rows(iRow).Cells("coDatePaidWF").Value, ToPay, dgWF.Rows(iRow).Cells("coChargeWF").Value, frmMain.users, DateTime.Now, prn, int, LD_Service, dgWF.Rows(iRow).Cells("coEMID").Value.ToString())
                    AddTrace_Repay1("UPDATE NEW", 2)
                End If
                LD_Status = getData("select top 1 LD_Status from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                i = i + 1
                LR_Amt = LR_Amt - ToPay
                showRepay(2)
                newRowWF()
            Else
                '--------------------------------------------------
                insertRepay1WF(LD_ID, LR_Amt)
                LD_Status = getData("select top 1 LD_Status from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                Dim topay1 As Double = getData("select top 1 LR_Amount from BK_LoanRepay where LD_ID='" & LD_ID & "' and LR_BrID='" & frmMain.lblCode.Text & "' order by LR_ID desc")
                LR_Amt = LR_Amt - topay1
            End If
        End While
        LD_Status = getData("select top 1 LD_Status from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
        If LR_Amt > 0 And LD_Status = "Active" Then
            If prn >= LR_Amt Then
                prn = LR_Amt
                int = 0
                LD_Service = 0
            ElseIf LR_Amt > prn And LR_Amt <= prn + int Then
                int = LR_Amt - prn
                LD_Service = 0
            ElseIf LR_Amt > prn + int And LR_Amt <= prn + int + LD_Service Then
                int = int
                LD_Service = LR_Amt - (prn + int)
            End If
            insertRepay1WF(LD_ID, LR_Amt)
            'insertRepay1(DataGridView1.Rows(iRow).Cells("coLD_ID").Value,prn,
        End If

        Return
    End Sub

    Private Sub DataGridView4_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView4.KeyDown
        If e.KeyCode = Keys.F11 Then
            If Me.DataGridView4.Rows.Count = 0 Then
                Return
            End If
            frmDisburshment.toExcel(Me.DataGridView4.Rows(DataGridView4.CurrentRow.Index).Cells(0).Value.ToString, Me.DataGridView4.CurrentCell.RowIndex)
        Else
            Return
        End If
    End Sub
    Private Sub insertRepay(ByVal LD_ID As String, ByVal Check As Integer)
        Dim iRow As Integer
        Dim iCol As Integer
        Dim iRow1 As Integer
        Dim prn As Double = 0
        Dim int As Double = 0
        Dim LD_Service As Double = 0
        Dim EM_ID As String
        Dim CM_ID1 As Integer
        Dim max_sh As Date
        Dim Count As Integer
        If Check = 1 Then
            iRow = Me.DataGridView1.CurrentCell.RowIndex
            iCol = DataGridView1.CurrentCell.ColumnIndex
            iRow1 = Me.DataGridView1.CurrentCell.RowIndex
        ElseIf Check = 2 Then
            iRow = Me.dgWF.CurrentCell.RowIndex
            iCol = dgWF.CurrentCell.ColumnIndex
            iRow1 = Me.dgWF.CurrentCell.RowIndex
        End If

        Dim oDt As New System.Data.DataTable


        '--------------------------------------------------------------- Start insert to loan repay
        Dim Str As String = "exec sp_repay '" & LD_ID & "','" & frmMain.lblCode.Text & "'"
        'On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        If Check = 1 Then
            DataGridView1.Rows(iRow1).Cells("coLD_ID").Value = oDt.Rows(0).Item(0).ToString
            DataGridView1.Rows(iRow1).Cells("coCM_ID").Value = oDt.Rows(0).Item(1).ToString
            DataGridView1.Rows(iRow1).Cells("coCM_Name").Value = oDt.Rows(0).Item(2).ToString
            DataGridView1.Rows(iRow1).Cells("coCM_Address").Value = oDt.Rows(0).Item(3).ToString
            DataGridView1.Rows(iRow1).Cells("coDes").Value = "យកប្រាក់ពីអតិថិជន"
            DataGridView1.Rows(iRow1).Cells("coAmtPaid").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
            DataGridView1.Rows(iRow1).Cells("coDatePaid").Value = FormatDateTime(DateTime.Now(), DateFormat.ShortDate)
            DataGridView1.Rows(iRow1).Cells("coCharge").Value = 0
            'Me.DataGridView1.Rows(iRow1).Cells("coLD_Service").Value = 0
            DataGridView1.Rows(iRow1).Cells("coAmtToPay").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
            DataGridView1.Rows(iRow1).Cells("coDateToPay").Value = FormatDateTime(oDt.Rows(0).Item(5).ToString, DateFormat.ShortDate)
            prn = oDt.Rows(0).Item(9).ToString
            int = oDt.Rows(0).Item(10).ToString
            LD_Service = oDt.Rows(0).Item(11).ToString
        Else
            dgWF.Rows(iRow1).Cells("coLD_IDWF").Value = oDt.Rows(0).Item(0).ToString
            dgWF.Rows(iRow1).Cells("coCM_IDWF").Value = oDt.Rows(0).Item(1).ToString
            dgWF.Rows(iRow1).Cells("coCM_NameWF").Value = oDt.Rows(0).Item(2).ToString
            dgWF.Rows(iRow1).Cells("coCM_AddressWF").Value = oDt.Rows(0).Item(3).ToString
            dgWF.Rows(iRow1).Cells("coDesWF").Value = "យកប្រាក់ពីអតិថិជន"
            dgWF.Rows(iRow1).Cells("coPaidWF").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
            dgWF.Rows(iRow1).Cells("coDatePaidWF").Value = FormatDateTime(DateTime.Now(), DateFormat.ShortDate)
            dgWF.Rows(iRow1).Cells("coChargeWF").Value = 0
            'Me.DataGridView1.Rows(iRow1).Cells("coLD_Service").Value = 0
            dgWF.Rows(iRow1).Cells("coAmtToPayWF").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
            dgWF.Rows(iRow1).Cells("coDateToPayWF").Value = FormatDateTime(oDt.Rows(0).Item(5).ToString, DateFormat.ShortDate)
            prn = oDt.Rows(0).Item(9).ToString
            int = oDt.Rows(0).Item(10).ToString
            LD_Service = oDt.Rows(0).Item(11).ToString
        End If

        oDa.Dispose()
        oDt.Dispose()
        'DataGridView1.Rows.Add()
        'Dim LR_Amt As Double=if
        If Check = 1 Then
            With DataGridView1.Rows(iRow)
                Count = getData("select count(isnull(b.LD_ID,1)) from BK_LoanSchedule a left join BK_LoanRepay b on a.LD_ID=b.LD_ID and a.SH_BrId=b.LR_BrID and a.SH_Date=b.SH_Date where a.LD_ID='" & .Cells("coLD_ID").Value & "' and b.LD_ID is null")
                EM_ID = getData("select top 1 EM_ID from BK_Loan where LD_ID='" & .Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                CM_ID1 = Val(getData("select top 1 CM_ID1 from BK_Loan where LD_ID='" & .Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'"))
                max_sh = FormatDateTime(getData("select Max(SH_Date)Max_SH_Date from BK_LoanSchedule where LD_ID='" & .Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
                If FormatDateTime(.Cells("coDateToPay").Value, DateFormat.ShortDate) = max_sh Then
                    frmDisburshment.AddTrace_Debursh("UPDATE OLD", .Cells("coLD_ID").Value)
                    addIn("Update BK_Loan set Date_Payoff='" & FormatDateTime(DateTime.Now, DateFormat.ShortDate) & "',LD_Status='Mature',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "' where LD_ID='" & .Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                    frmDisburshment.AddTrace_Debursh("UPDATE NEW", .Cells("coLD_ID").Value)
                End If
                addRepay(.Cells("coLD_ID").Value, .Cells("coCM_ID").Value, frmMain.lblCode.Text, .Cells("coDateToPay").Value, EM_ID, .Cells("coDes").Value, .Cells("coAmtPaid").Value, .Cells("coDatePaid").Value, .Cells("coAmtPaid").Value, .Cells("coCharge").Value, 1, frmMain.users, DateTime.Now, 0, CM_ID1, prn, int, LD_Service, 0)
                If FormatDateTime(.Cells("coDateToPay").Value, DateFormat.ShortDate) = max_sh Then
                    toExcelFormat(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                End If
                showRepay(1)
            End With
            newRow()
        Else
            With dgWF.Rows(iRow)
                EM_ID = getData("select top 1 EM_ID from BK_Loan where LD_ID='" & .Cells("coLD_IDWF").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                CM_ID1 = Val(getData("select top 1 CM_ID1 from BK_Loan where LD_ID='" & .Cells("coLD_IDWF").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'"))
                max_sh = FormatDateTime(getData("select Max(SH_Date)Max_SH_Date from BK_LoanSchedule where LD_ID='" & .Cells("coLD_IDWF").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
                If FormatDateTime(.Cells("coDateToPayWF").Value, DateFormat.ShortDate) = max_sh Then
                    frmDisburshment.AddTrace_Debursh("UPDATE OLD", .Cells("coLD_IDWF").Value)
                    addIn("Update BK_Loan set Date_Payoff='" & FormatDateTime(DateTime.Now, DateFormat.ShortDate) & "',LD_Status='Mature',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "' where LD_ID='" & .Cells("coLD_IDWF").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                    frmDisburshment.AddTrace_Debursh("UPDATE NEW", .Cells("coLD_IDWF").Value)
                End If
                addRepay(.Cells("coLD_IDWF").Value, .Cells("coCM_IDWF").Value, frmMain.lblCode.Text, .Cells("coDateToPayWF").Value, EM_ID, .Cells("coDesWF").Value, .Cells("coPaidWF").Value, .Cells("coDatePaidWF").Value, .Cells("coPaidWF").Value, .Cells("coChargeWF").Value, 1, frmMain.users, DateTime.Now, 0, CM_ID1, prn, int, LD_Service, 2)
                If FormatDateTime(.Cells("coDateToPayWF").Value, DateFormat.ShortDate) = max_sh Then
                    toExcelFormat(Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                End If
                showRepay(2)
            End With
            newRowWF()
        End If

    End Sub
    'Private Sub insertRepay1(ByVal LD_ID As String, ByVal LR_Amt As Double)
    '    Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
    '    Dim iCol = DataGridView1.CurrentCell.ColumnIndex
    '    Dim iRow1 = Me.DataGridView1.CurrentCell.RowIndex
    '    Dim oDt As New System.Data.DataTable
    '    '--------------------------------------------------------------- Start insert to loan repay
    '    Dim Str As String = "exec sp_repay '" & LD_ID & "','" & frmMain.lblCode.Text & "'"
    '    'On Error Resume Next
    '    oDt.Clear()
    '    oDa = New SqlDataAdapter(Str, g_cnn)
    '    oDa.Fill(oDt)
    '    DataGridView1.Rows(iRow1).Cells("coLD_ID").Value = oDt.Rows(0).Item(0).ToString
    '    DataGridView1.Rows(iRow1).Cells("coCM_ID").Value = oDt.Rows(0).Item(1).ToString
    '    DataGridView1.Rows(iRow1).Cells("coCM_Name").Value = oDt.Rows(0).Item(2).ToString
    '    DataGridView1.Rows(iRow1).Cells("coCM_Address").Value = oDt.Rows(0).Item(3).ToString
    '    DataGridView1.Rows(iRow1).Cells("coDes").Value = "យកប្រាក់ពីអតិថិជន"
    '    DataGridView1.Rows(iRow1).Cells("coAmtPaid").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
    '    DataGridView1.Rows(iRow1).Cells("coDatePaid").Value = FormatDateTime(DateTime.Now(), DateFormat.ShortDate)
    '    DataGridView1.Rows(iRow1).Cells("coCharge").Value = 0
    '    'Me.DataGridView1.Rows(iRow1).Cells("coLD_Service").Value = 0
    '    DataGridView1.Rows(iRow1).Cells("coAmtToPay").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
    '    DataGridView1.Rows(iRow1).Cells("coDateToPay").Value = FormatDateTime(oDt.Rows(0).Item(5).ToString, DateFormat.ShortDate)
    '    Dim prn As Double = oDt.Rows(0).Item(9).ToString
    '    Dim int As Double = oDt.Rows(0).Item(10).ToString
    '    Dim LD_Service As Double = oDt.Rows(0).Item(11).ToString
    '    oDa.Dispose()
    '    oDt.Dispose()
    '    'DataGridView1.Rows.Add()
    '    'Dim LR_Amt As Double=
    '    With DataGridView1.Rows(iRow)
    '        Dim EM_ID As String = getData("select top 1 EM_ID from BK_Loan where LD_ID='" & .Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
    '        Dim CM_ID1 As Integer = Val(getData("select top 1 CM_ID1 from BK_Loan where LD_ID='" & .Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'"))
    '        Dim max_sh As Date = FormatDateTime(getData("select Max(SH_Date)Max_SH_Date from BK_LoanSchedule where LD_ID='" & .Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
    '        If FormatDateTime(.Cells("coDateToPay").Value, DateFormat.ShortDate) = max_sh And LR_Amt >= prn + int + LD_Service Then
    '            int = LR_Amt - (prn + LD_Service)
    '            frmDisburshment.AddTrace_Debursh("UPDATE OLD", .Cells("coLD_ID").Value)
    '            addIn("Update BK_Loan set Date_Payoff='" & FormatDateTime(DateTime.Now, DateFormat.ShortDate) & "',LD_Status='Mature',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "' where LD_ID='" & .Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
    '            frmDisburshment.AddTrace_Debursh("UPDATE NEW", .Cells("coLD_ID").Value)
    '        End If
    '        addRepay(.Cells("coLD_ID").Value, .Cells("coCM_ID").Value, frmMain.lblCode.Text, .Cells("coDateToPay").Value, EM_ID, .Cells("coDes").Value, .Cells("coAmtPaid").Value, .Cells("coDatePaid").Value, .Cells("coAmtPaid").Value, .Cells("coCharge").Value, 1, frmMain.users, DateTime.Now, 0, CM_ID1, prn, int, LD_Service)
    '        If FormatDateTime(.Cells("coDateToPay").Value, DateFormat.ShortDate) = max_sh And LR_Amt >= prn + int + LD_Service Then
    '            toExcelFormat(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
    '        End If
    '        showRepay()
    '    End With
    '    newRow()
    'End Sub
    Private Function shdate(ByVal LD_ID As String)
        Dim shdate1 As Date
        Dim oDt As New System.Data.DataTable
        '--------------------------------------------------------------- Start insert to loan repay
        Dim Str As String = "exec sp_repay '" & LD_ID & "','" & frmMain.lblCode.Text & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        shdate1 = FormatDateTime(oDt.Rows(0).Item(5).ToString, DateFormat.ShortDate)
        oDa.Dispose()
        oDt.Dispose()
        Return shdate1
    End Function
    Private Sub insertRepay1(ByVal LD_ID As String, ByVal LR_Amt1 As Double)
        'Dim prn1, int1, LD_Service1 As Double
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        Dim iRow1 = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        '--------------------------------------------------------------- Start insert to loan repay
        Dim Str As String = "exec sp_repay '" & LD_ID & "','" & frmMain.lblCode.Text & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DataGridView1.Rows(iRow1).Cells("coLD_ID").Value = oDt.Rows(0).Item(0).ToString
        DataGridView1.Rows(iRow1).Cells("coCM_ID").Value = oDt.Rows(0).Item(1).ToString
        DataGridView1.Rows(iRow1).Cells("coCM_Name").Value = oDt.Rows(0).Item(2).ToString
        DataGridView1.Rows(iRow1).Cells("coCM_Address").Value = oDt.Rows(0).Item(3).ToString
        DataGridView1.Rows(iRow1).Cells("coDes").Value = "យកប្រាក់ពីអតិថិជន"
        DataGridView1.Rows(iRow1).Cells("coAmtPaid").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
        DataGridView1.Rows(iRow1).Cells("coDatePaid").Value = FormatDateTime(DateTime.Now(), DateFormat.ShortDate)
        DataGridView1.Rows(iRow1).Cells("coCharge").Value = 0
        DataGridView1.Rows(iRow1).Cells("coAmtToPay").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
        DataGridView1.Rows(iRow1).Cells("coDateToPay").Value = FormatDateTime(oDt.Rows(0).Item(5).ToString, DateFormat.ShortDate)
        Dim prn1 As Double = oDt.Rows(0).Item(9).ToString
        Dim int1 As Double = oDt.Rows(0).Item(10).ToString
        Dim LD_Service1 As Double = oDt.Rows(0).Item(11).ToString
        Dim d As Date = FormatDateTime(oDt.Rows(0).Item(5).ToString, DateFormat.ShortDate)
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            Dim EM_ID As String = getData("select top 1 EM_ID from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
            Dim CM_ID1 As Integer = Val(getData("select top 1 CM_ID1 from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'"))
            Dim max_sh As Date = FormatDateTime(getData("select Max(SH_Date)Max_SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
            If d = max_sh And LR_Amt1 >= prn1 + int1 + LD_Service1 Then
                If LR_Amt1 > prn1 + int1 + LD_Service1 Then
                    int1 = LR_Amt1 - (prn1 + LD_Service1)
                End If
                frmDisburshment.AddTrace_Debursh("UPDATE OLD", LD_ID)
                addIn("Update BK_Loan set Date_Payoff='" & FormatDateTime(DateTime.Now, DateFormat.ShortDate) & "',LD_Status='Mature',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "' where LD_ID='" & .Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                frmDisburshment.AddTrace_Debursh("UPDATE NEW", LD_ID)
                addRepay(LD_ID, .Cells("coCM_ID").Value, frmMain.lblCode.Text, .Cells("coDateToPay").Value, EM_ID, .Cells("coDes").Value, .Cells("coAmtPaid").Value, .Cells("coDatePaid").Value, LR_Amt1, .Cells("coCharge").Value, 1, frmMain.users, DateTime.Now, 0, CM_ID1, prn1, int1, LD_Service1, 0)
            Else
                If LR_Amt1 > 0 Then
                    If prn1 >= LR_Amt1 Then
                        prn1 = LR_Amt1
                        int1 = 0
                        LD_Service1 = 0
                    ElseIf LR_Amt1 > prn1 And LR_Amt1 <= prn1 + int1 Then
                        int1 = LR_Amt1 - prn1
                        LD_Service1 = 0
                    ElseIf LR_Amt1 > prn1 + int1 And LR_Amt1 <= prn1 + int1 + LD_Service1 Then
                        'Int = Int()
                        LD_Service1 = LR_Amt1 - (prn1 + int1)
                    End If
                Else
                    Return
                End If
                addRepay(.Cells("coLD_ID").Value, .Cells("coCM_ID").Value, frmMain.lblCode.Text, .Cells("coDateToPay").Value, EM_ID, .Cells("coDes").Value, .Cells("coAmtPaid").Value, .Cells("coDatePaid").Value, prn1 + int1 + LD_Service1, .Cells("coCharge").Value, 1, frmMain.users, DateTime.Now, 0, CM_ID1, prn1, int1, LD_Service1, 0)
            End If

            If FormatDateTime(.Cells("coDateToPay").Value, DateFormat.ShortDate) = max_sh And prn1 + int1 + LD_Service1 Then
                toExcelFormat(LD_ID)
            End If
            showRepay(1)
        End With
        newRow()
    End Sub
    Private Sub insertRepay1WF(ByVal LD_ID As String, ByVal LR_Amt1 As Double)
        'Dim prn1, int1, LD_Service1 As Double
        Dim iRow = Me.dgWF.CurrentCell.RowIndex
        Dim iCol = dgWF.CurrentCell.ColumnIndex
        Dim iRow1 = Me.dgWF.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        '--------------------------------------------------------------- Start insert to loan repay
        Dim Str As String = "exec sp_repay '" & LD_ID & "','" & frmMain.lblCode.Text & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        dgWF.Rows(iRow1).Cells("coLD_IDWF").Value = oDt.Rows(0).Item(0).ToString
        dgWF.Rows(iRow1).Cells("coCM_IDWF").Value = oDt.Rows(0).Item(1).ToString
        dgWF.Rows(iRow1).Cells("coCM_NameWF").Value = oDt.Rows(0).Item(2).ToString
        dgWF.Rows(iRow1).Cells("coCM_AddressWF").Value = oDt.Rows(0).Item(3).ToString
        dgWF.Rows(iRow1).Cells("coDesWF").Value = "យកប្រាក់ពីអតិថិជន"
        dgWF.Rows(iRow1).Cells("coPaidWF").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
        dgWF.Rows(iRow1).Cells("coDatePaidWF").Value = FormatDateTime(DateTime.Now(), DateFormat.ShortDate)
        dgWF.Rows(iRow1).Cells("coChargeWF").Value = 0
        dgWF.Rows(iRow1).Cells("coAmtToPayWF").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")
        dgWF.Rows(iRow1).Cells("coDateTopayWF").Value = FormatDateTime(oDt.Rows(0).Item(5).ToString, DateFormat.ShortDate)
        Dim prn1 As Double = oDt.Rows(0).Item(9).ToString
        Dim int1 As Double = oDt.Rows(0).Item(10).ToString
        Dim LD_Service1 As Double = oDt.Rows(0).Item(11).ToString
        Dim d As Date = FormatDateTime(oDt.Rows(0).Item(5).ToString, DateFormat.ShortDate)
        oDa.Dispose()
        oDt.Dispose()
        With dgWF.Rows(iRow)
            Dim EM_ID As String = getData("select top 1 EM_ID from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
            Dim CM_ID1 As Integer = Val(getData("select top 1 CM_ID1 from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'"))
            Dim max_sh As Date = FormatDateTime(getData("select Max(SH_Date)Max_SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
            If d = max_sh And LR_Amt1 >= prn1 + int1 + LD_Service1 Then
                If LR_Amt1 > prn1 + int1 + LD_Service1 Then
                    int1 = LR_Amt1 - (prn1 + LD_Service1)
                End If
                frmDisburshment.AddTrace_Debursh("UPDATE OLD", LD_ID)
                addIn("Update BK_Loan set Date_Payoff='" & FormatDateTime(DateTime.Now, DateFormat.ShortDate) & "',LD_Status='Mature',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "' where LD_ID='" & .Cells("coLD_IDWF").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                frmDisburshment.AddTrace_Debursh("UPDATE NEW", LD_ID)
                addRepay(LD_ID, .Cells("coCM_IDWF").Value, frmMain.lblCode.Text, .Cells("coDateTopayWF").Value, EM_ID, .Cells("coDesWF").Value, .Cells("coPaidWF").Value, .Cells("coDatePaidWF").Value, LR_Amt1, .Cells("coChargeWF").Value, 1, frmMain.users, DateTime.Now, 0, CM_ID1, prn1, int1, LD_Service1, 2)
            Else
                If LR_Amt1 > 0 Then
                    If prn1 >= LR_Amt1 Then
                        prn1 = LR_Amt1
                        int1 = 0
                        LD_Service1 = 0
                    ElseIf LR_Amt1 > prn1 And LR_Amt1 <= prn1 + int1 Then
                        int1 = LR_Amt1 - prn1
                        LD_Service1 = 0
                    ElseIf LR_Amt1 > prn1 + int1 And LR_Amt1 <= prn1 + int1 + LD_Service1 Then
                        'Int = Int()
                        LD_Service1 = LR_Amt1 - (prn1 + int1)
                    End If
                Else
                    Return
                End If
                addRepay(.Cells("coLD_IDWF").Value, .Cells("coCM_IDWF").Value, frmMain.lblCode.Text, .Cells("coDateTopayWF").Value, EM_ID, .Cells("coDesWF").Value, .Cells("coPaidWF").Value, .Cells("coDatePaidWF").Value, prn1 + int1 + LD_Service1, .Cells("coChargeWF").Value, 1, frmMain.users, DateTime.Now, 0, CM_ID1, prn1, int1, LD_Service1, 2)
            End If

            If FormatDateTime(.Cells("coDateTopayWF").Value, DateFormat.ShortDate) = max_sh And prn1 + int1 + LD_Service1 Then
                toExcelFormat(LD_ID)
            End If
            showRepay(2)
        End With
        newRowWF()
    End Sub
    Private Sub txtCmName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCmName.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.txtCmName.Text = "" Then
                Return
            Else
                Dim CM_ID1 As String = getData("Select * from BK_Customer where CM_KhName like N'%" & txtCmName.Text & "%' and CM_BrId='" & frmMain.lblCode.Text & "'")
                If CM_ID1 = "" Then
                    MessageBox.Show("No this customer please check again.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Else
                    ShowDataGrid(Me.DataGridView3, "select CM_ID,CM_KhName,CM_Phone,a.LO_ID,VL_ID+','+CN_ID+','+b.DT_ID+','+PV_ID 'Address',LD_Cycle,ID 'RealID' from BK_Customer a left join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID where CM_KhName like N'%" & Me.txtCmName.Text & "%' and CM_BrId ='" & frmMain.lblCode.Text & "'")
                End If
            End If
        End If
    End Sub
    Private Sub SetFont()
        SetFontDatagrid(Me.DataGridView1)
        SetFontDatagrid(Me.DataGridView2)
        SetFontDatagrid3(Me.DataGridView3)
        SetFontDatagrid3(Me.DataGridView4)
        SetFontDatagrid(Me.DataGridView5)
        SetFontDatagrid(dgWF)
        SetFontDatagrid3(Me.dgCMCode)
    End Sub
    Private Sub txtLoanid_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLoanid.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.txtLoanid.Text = "" Then
                Return
            Else
                Dim CM_ID1 As String = getData("Select top 1 LD_ID from BK_Loan where LD_ID='" & txtLoanid.Text & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                If CM_ID1 = "" Then
                    MessageBox.Show("No this customer please check again.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Else
                    ShowDataGrid(DataGridView4, "select LD_ID,LD_BrId,CM_ID,convert(nvarchar(12),LD_Dis_Date,101)Dis_Date,convert(nvarchar(12),LD_First_Date,101)FirstDate,convert(nvarchar(12),LD_Mat_Date,101)EndDate,LD_Dis_Amt Dis_Amt,CU_ID,LD_IntRate,a.EM_ID,b.EM_Name,LD_Unit,LD_Type,LD_Term,LD_ChargeRate ChargeRate,LD_ChargeAmt ChargeAmt,case when LD_Service=1 then 'Yes' else 'No' end 'OP.Fee',LD_Status from BK_Loan a left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID where LD_ID='" & CM_ID1 & "' and LD_BrId='" & frmMain.lblCode.Text & "' order by Cast(a.LD_ID as int) desc")
                End If
            End If
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'MessageBox.Show("Hi")
        ShowDataGrid(dgCMCode, "exec sp_InactiveCM '" & FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate) & "'")
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim st As String = "Inactive Customer"
        ExportDatagridViewToExcel1(dgCMCode, "D:\" & st & ".xls", st)
    End Sub
    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim a As String = getData("Select top 1 LD_ID from BK_Loan where LD_ID='" & TextBox2.Text.Trim & "' and LD_BrId=" & frmMain.lblCode.Text)
                If a.ToString = "" Then
                    resultError = frmMessageError.ShowBoxError("លេខកិច្ចសន្យានេះគ្មានទេ សូមពិនិត្យឡើងវិញ។", "លេខកូដខុស")
                    Return
                Else
                    'datagrid3()
                    Dim CM_ID1 As Integer = Val(getData("Select top 1 CM_ID1 from BK_Loan where LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID='" & TextBox2.Text & "'"))
                    Dim cm_Id As String = getData("Select top 1 CM_ID from BK_Loan where LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID='" & TextBox2.Text & "'")
                    Dim Name As String = getData("select top 1 CM_KhName from BK_Customer where ID='" & CM_ID1 & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                    Dim Address As String = getData("select c.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID addres from BK_Loan a inner join BK_Customer b on a.CM_ID=b.CM_ID and a.LD_BrId=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrID where LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID='" & TextBox2.Text & "' and b.ID='" & CM_ID1 & "'")
                    Label10.Text = cm_Id & ":" & " " & Name & ", អសយដ្ឋាន: " & Address
                    AddToGridLDPaid1(DataGridView5, 11, "exec spGetLoanRepayDetail '" & TextBox2.Text.Trim & "','" & frmMain.lblCode.Text & "'")
                End If
            End If
            Return
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
            Return
        End Try
    End Sub
    Private Sub DataGridView5_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView5.DoubleClick
        Dim iRow = Me.DataGridView5.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        'MessageBox.Show(DataGridView5.Rows(iRow).Cells("co_DateToPay1").Value)
        Try
            frmSpecialRepay.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        showListRepay(1)
    End Sub

    Private Sub dgWF_KeyDown(sender As Object, e As KeyEventArgs) Handles dgWF.KeyDown
        If Me.dgWF.Rows.Count = 0 Then
            newRowWF()
            Return
        End If
        Dim iRow = Me.dgWF.CurrentCell.RowIndex
        If e.KeyCode = Keys.F12 Then
            '------------------------- declare prn and int
            Dim prn As Double = 0
            Dim int As Double = 0
            Dim LD_Service As Double = 0
            Dim ToPay As Double = 0
            '----------------------------------------- Check record change or not?
            If NoRecordChangeWF() = 1 Then
                MessageBox.Show("No record change!", "No change", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            Else
                '--------------------------------------------------------------- check have record have or not
                If Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value Is Nothing Then
                    Return
                Else
                    '--------------------------------------------------------------- Start update data
                    Dim LR_ID As Integer = getData("select top 1 MAX(LR_ID) from BK_LoanRepay where LD_ID='" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "' group by SH_Date order by SH_Date desc")
                    Dim Max_SH As Date = FormatDateTime(getData("select  max(SH_Date) SH_Date from BK_LoanSchedule where LD_ID='" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
                    If LR_ID = dgWF.Rows(iRow).Cells(0).Value Then
                        '----------------------------------------------------- Add to trace loan repay
                        AddTrace_Repay1("UPDATE OLD", 2)
                        '---------------------------------------------------------------------------- Update when description payoff
                        If Me.dgWF.Rows(iRow).Cells("coDesWF").Value.ToString.Trim = "បង់ផ្ដាច់" Then
                            '-------------------------- declare prn and int
                            prn = ReturnPrn(1, dgWF.Rows(iRow).Cells("coLD_IDWF").Value, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
                            int = ReturnInt(1, dgWF.Rows(iRow).Cells("coLD_IDWF").Value, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
                            LD_Service = ReturnService(1, dgWF.Rows(iRow).Cells("coLD_IDWF").Value, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
                            ToPay = prn + int + LD_Service
                            Dim LR_Amount As Double = Me.dgWF.Rows(iRow).Cells("coPaidWF").Value
                            '---------------------------------------------------------------------- Calculate prn and int
                            If LR_Amount <= prn Then
                                prn = Val(LR_Amount)
                                int = 0
                                LD_Service = 0
                            ElseIf LR_Amount > prn And LR_Amount <= prn + int Then
                                int = LR_Amount - prn
                                LD_Service = 0
                            ElseIf LR_Amount > prn + int And LR_Amount <= prn + int + LD_Service Then
                                LD_Service = LR_Amount - (prn + int)
                            Else
                                prn = prn
                                int = int
                                LD_Service = LR_Amount - (prn + int)
                            End If
                            '--------------------------------------------------------------------------------------------- update status loan to payoff
                            '--- Add trace loan
                            frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                            '--- Update laon status
                            addIn("Update BK_Loan set LD_Status='Payoff',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "',Date_Payoff='" & dgWF.Rows(iRow).Cells("coDatePaidWF").Value & "' where LD_ID='" & Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                            '---- add trace again
                            frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                            '----------------------------------------------------------------------------------------- Start update loan repay
                            UpdateRepayWF(dgWF.Rows(iRow).Cells("coDesWF").Value, dgWF.Rows(iRow).Cells("coDatePaidWF").Value, dgWF.Rows(iRow).Cells("coPaidWF").Value, dgWF.Rows(iRow).Cells("coChargeWF").Value, frmMain.users, DateTime.Now, prn, int, LD_Service, dgWF.Rows(iRow).Cells("coEMID").Value.ToString())
                            '------------------------------------ add trace repay loan again
                            AddTrace_Repay1("UPDATE NEW", 2)
                            '------------------------------------------- Auto to excel
                            toExcelFormat(Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                        ElseIf dgWF.Rows(iRow).Cells("coDesWF").Value.ToString.Trim = "យកប្រាក់ពីអតិថិជន" Then '--------- Update when normal repay
                            '------------------------------ calculate prn and int
                            prn = ReturnPrn(0, dgWF.Rows(iRow).Cells("coLD_IDWF").Value, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
                            int = ReturnInt(0, dgWF.Rows(iRow).Cells("coLD_IDWF").Value, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
                            LD_Service = ReturnService(0, dgWF.Rows(iRow).Cells("coLD_IDWF").Value, Me.dgWF.Rows(iRow).Cells("coDateToPayWF").Value, Me.dgWF.Rows(iRow).Cells("coNoWF").Value)
                            ToPay = prn + int + LD_Service
                            Dim LR_Amt As Double = Me.dgWF.Rows(iRow).Cells("coPaidWF").Value
                            If prn >= LR_Amt Then
                                prn = LR_Amt
                                int = 0
                                LD_Service = 0
                            ElseIf LR_Amt > prn And LR_Amt <= prn + int Then
                                int = LR_Amt - prn
                                LD_Service = 0
                            ElseIf LR_Amt > prn + int And LR_Amt <= prn + int + LD_Service Then
                                int = int
                                LD_Service = LR_Amt - (prn + int)
                            End If
                            '-----------------------------------------check status of repay
                            Dim status As String = getData("select top 1 LR_Description from BK_LoanRepay where LD_ID='" & Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LR_ID='" & Me.dgWF.Rows(iRow).Cells(0).Value & "'")
                            '-------------------------------------------------------------------------------------------- update if it's last schedule repay
                            If Max_SH = FormatDateTime(dgWF.Rows(iRow).Cells("coDateTopayWF").Value, DateFormat.ShortDate) Then
                                frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                                addIn("Update BK_Loan set LD_Status='Active',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "',Date_Payoff='" & dgWF.Rows(iRow).Cells("coDatePaidWF").Value & "' where LD_ID='" & Me.dgWF.Rows(iRow).Cells(1).Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                                frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                            End If
                            '---------------------------
                            If Max_SH = FormatDateTime(dgWF.Rows(iRow).Cells("coDateTopayWF").Value, DateFormat.ShortDate) _
                                And Val(dgWF.Rows(iRow).Cells("coPaidWF").Value) = Convert.ToDouble(dgWF.Rows(iRow).Cells("coAmtToPayWF").Value) Then
                                '---------------------- Update repay
                                UpdateRepayWF(dgWF.Rows(iRow).Cells("coDesWF").Value, dgWF.Rows(iRow).Cells("coDatePaidWF").Value, dgWF.Rows(iRow).Cells("coPaidWF").Value, dgWF.Rows(iRow).Cells("coChargeWF").Value, frmMain.users, DateTime.Now, prn, int, LD_Service, dgWF.Rows(iRow).Cells("coEMID").Value.ToString())
                                '-------------------------- Update Loan
                                frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                                addIn("Update BK_Loan set LD_Status='Mature',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "',Date_Payoff='" & dgWF.Rows(iRow).Cells("coDatePaidWF").Value & "' where LD_ID='" & Me.dgWF.Rows(iRow).Cells(1).Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                                frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                                AddTrace_Repay1("UPDATE NEW", 2)
                                '----------------------------------- To Excel
                                toExcelFormat(Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                            ElseIf status = "បង់ផ្ដាច់" Then
                                '------------------------------------- If payoff update to Active
                                UpdateRepayWF(dgWF.Rows(iRow).Cells("coDesWF").Value, dgWF.Rows(iRow).Cells("coDatePaidWF").Value, dgWF.Rows(iRow).Cells("coPaidWF").Value, dgWF.Rows(iRow).Cells("coChargeWF").Value, frmMain.users, DateTime.Now, prn, int, LD_Service, dgWF.Rows(iRow).Cells("coEMID").Value.ToString())
                                frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                                addIn("Update BK_Loan set Date_Payoff='" & DateTime.MaxValue.Date & "',LD_Status='Active',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "' where LD_ID='" & Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                                frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                                AddTrace_Repay1("UPDATE NEW", 2)
                            Else
                                '------------------------------------ Normal Update
                                If ToPay < LR_Amt Then
                                    'MessageBox.Show("Bigger")

                                    biggerWF()

                                Else
                                    UpdateRepayWF(dgWF.Rows(iRow).Cells("coDesWF").Value, dgWF.Rows(iRow).Cells("coDatePaidWF").Value, dgWF.Rows(iRow).Cells("coPaidWF").Value, dgWF.Rows(iRow).Cells("coChargeWF").Value, frmMain.users, DateTime.Now, prn, int, LD_Service, dgWF.Rows(iRow).Cells("coEMID").Value.ToString())
                                    AddTrace_Repay1("UPDATE NEW", 2)
                                End If
                            End If
                        Else
                            Return
                        End If
                        showRepay(2)
                        newRowWF()
                    Else
                        '-------------------------- If not last record
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនអាចកែរប្រែបានទេ ព្រោះមិនមែនជាទិន្នន័យចុងក្រោយ។", "កែរប្រែ")
                        Return
                    End If
                End If
            End If
            'AutoSum()
        ElseIf e.KeyCode = Keys.F11 Then
            ToExcel(dgWF)
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Delete Then
            Dim LR_ID As String = getData("select Max(LR_ID)LR_ID from BK_LoanRepay where LD_ID='" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'")
            If LR_ID = Me.dgWF.Rows(iRow).Cells("coNoWF").Value Then
                result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                If result = "1" Then
                    If LR_ID = "" Then
                        Me.dgWF.Rows.Remove(Me.dgWF.Rows(iRow))
                    Else
                        AddTrace_Repay1("DELETE", 2)
                        '-------------------------------------------------------------- Check Status Loan
                        Dim Status As String = getData("Select top 1 LD_Status from BK_Loan where LD_ID='" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                        If Status = "Payoff" Or Status = "Mature" Then
                            frmDisburshment.AddTrace_Debursh("UPDATE OLD", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                            addIn("Update BK_Loan set LD_Status='Active',Date_Payoff='" & DateTime.MaxValue.Date & "',LD_User_Modify='" & frmMain.users & "',LD_Date_Modify='" & DateTime.Now & "' where LD_ID='" & Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                            frmDisburshment.AddTrace_Debursh("UPDATE NEW", Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value)
                        End If
                        '----------------------------- Delete from repay
                        addIn("delete from BK_LoanRepay where LD_ID='" & Me.dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "' and LR_ID='" & Me.dgWF.Rows(iRow).Cells("coNoWF").Value & "'")
                        Me.dgWF.Rows.Remove(Me.dgWF.Rows(iRow))
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុបទិន្នន័យ")
                    End If
                Else
                    Return

                End If
            Else
                Dim LR_Date As Date = FormatDateTime(getData("Select Max(LR_Date) LR_Date from BK_LoanRepay where LD_ID='" & dgWF.Rows(iRow).Cells("coLD_IDWF").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"), DateFormat.ShortDate)
                resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនអាចលុបបានទេ ព្រោះមិនមែនជាការបង់ប្រាក់ចុងក្រោយ។ ការបង់ចុងក្រោយនៅថ្ងៃទី " & LR_Date & " ។", "លុបទិន្នន័យ")
                Return
            End If
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            newRowWF()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        AutoSumWF()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        dgWF.Rows.Clear()
        newRowWF()
    End Sub

    Private Sub dgWF_SelectionChanged(sender As Object, e As EventArgs) Handles dgWF.SelectionChanged
        Try
            '---------------------------------------------------- sum amount paid
            If 8 = dgWF.Columns("coPaidWF").Index Then
                Dim total As Double = dgWF.SelectedCells.Cast(Of DataGridViewCell)().Sum(Function(cell) CDec(cell.Value))
                lblResultSum.Text = total.ToString("##,###.00")
                '--- for count column selected
                lblResultCount.Text = dgWF.SelectedCells.Count.ToString
            Else
                lblResultCount.Text = 0
                lblResultSum.Text = 0.0
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class