Imports System.Drawing.Bitmap
'Imports Microsoft.Office.Interop.Outlook
Imports System.Globalization
Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Imports X = Microsoft.Office.Interop.Excel
Public Class frmDisburshment
    Dim aD As Integer = 0
    Private Sub frmDisburshment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        newRow()
        callLast()
        'Me.DataGridView1.Rows(iRow).Cells("coPayOff").Value.ToString = 0

        'DataGridView1.d.Cells("coLD_ID").ColumnIndex
    End Sub
    Private Sub DataGridView1_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim iCol = DataGridView1.CurrentCell.ColumnIndex
            'Dim iRow = DataGridView1.CurrentCell.RowIndex
            Dim staffName As String = ""
            If iCol = DataGridView1.Columns.Count - 1 Then
                If iRow < DataGridView1.Rows.Count - 1 Then
                    DataGridView1.CurrentCell = DataGridView1(0, iRow + 1)
                End If
            Else
                If iRow < DataGridView1.Rows.Count - 1 Then
                    SendKeys.Send("{up}")
                End If
                If Me.DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("coLD_ID").ColumnIndex Then
                    If DataGridView1.CurrentCell.Value Is Nothing Then
                        Return
                    Else
                        Dim LD_ID As String = getData("select top 1 LD_ID from BK_Loan where LD_ID=" & DataGridView1.CurrentCell.Value.ToString & " and LD_BrId=" & frmMain.lblCode.Text)
                        If LD_ID = "" Then
                            clear()
                            DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("coEM_ID").ColumnIndex, iRow)
                        Else
                            showLoan()
                            newRow()
                        End If
                    End If
                    Int()
                ElseIf DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("PID").ColumnIndex Then
                    If DataGridView1.CurrentCell.Value Is Nothing Then
                        Return
                    Else
                        Dim PID As String = getData("select top 1 Kh_Name from BK_Product where PID='" & DataGridView1.CurrentCell.Value.ToString & "'")
                        If PID = "" Then
                     MessageBox.Show("This product have no in database, please add first!","No Product!",MessageBoxButtons.OK,MessageBoxIcon.Information)
                            DataGridView1.CurrentCell.Value=""
                            DataGridView1.Rows(iRow).Cells("PName").Value = ""
                        Else
                            Dim KhName As String = getData("select top 1 Kh_Name from BK_Product where PID='" & DataGridView1.CurrentCell.Value.ToString & "'")
                            DataGridView1.Rows(iRow).Cells("PName").Value = KhName.ToString
                            'DataGridView1.Rows(iRow).Cells("PName").Value = KhName.ToString
                            'DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coCM_ID").ColumnIndex, iRow)
                        End If
                    End If
                ElseIf DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("coEM_ID").ColumnIndex Then
                    If DataGridView1.CurrentCell.Value Is Nothing Then
                        Return
                    Else
                        Dim EM_ID As String = getData("select top 1 EM_ID from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "' and EM_ID='" & DataGridView1.CurrentCell.Value.ToString & "'")
                        If EM_ID = "" Then
                            frmEmployee.Text = "FromDisbursh"
                            frmEmployee.MdiParent = frmMain
                            frmEmployee.WindowState = FormWindowState.Maximized
                            frmEmployee.Show()
                        Else
                            Dim em_name As String = getData("select top 1 EM_Name from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "' and EM_ID='" & DataGridView1.CurrentCell.Value.ToString & "'")
                            DataGridView1.Rows(iRow).Cells("coEM_Name").Value = em_name.ToString
                            DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coCM_ID").ColumnIndex, iRow)
                        End If
                    End If
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coCM_ID").ColumnIndex Then
                    If DataGridView1.CurrentCell.Value.ToString = "" Then
                        Return
                    Else
                        Dim status As String = getData("select LD_Status from BK_Loan where LD_ID=(select top 1 LD_ID from BK_Loan where CM_ID='" & DataGridView1.CurrentCell.Value.ToString & "' and LD_BrId='" & frmMain.lblCode.Text & "' order by LD_Date_Create desc) and CM_ID='" & DataGridView1.CurrentCell.Value.ToString & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                        If status = "Active" Then
                            Dim a As String = getData("Select top 1 LD_ID from BK_Loan where CM_ID='" & Me.DataGridView1.CurrentCell.Value & "' and LD_Status='Active'")
                            resultError = frmMessageError.ShowBoxError("អតិថិជននេះមិនអាចធ្វើការខី្ចថ្មីបានទេ ព្រោះមិនទាន់ផ្តាច់ឥណទានចាស់លេខ " & a & " ។", "មិនទាន់ផ្តាច់")
                            Me.DataGridView1.CurrentCell.Value = ""
                            Return
                        Else
                            Dim CM_Name As String = getData("select CM_KhName from BK_Customer where CM_BrId='" & frmMain.lblCode.Text & "' and CM_ID='" & DataGridView1.CurrentCell.Value.ToString & "' and Status='Active'")
                            Dim CM_Phone As String = getData("select CM_Phone from BK_Customer where CM_BrId='" & frmMain.lblCode.Text & "' and CM_ID='" & DataGridView1.CurrentCell.Value.ToString & "' and Status='Active'")
                            Dim CM_Address As String = getData("select b.VL_ID+','+b.CN_ID+','+b.DT_ID+','+b.PV_ID from BK_Customer a inner join BK_Location b on a.CM_BrId=b.LO_BrID and a.LO_ID=b.LO_ID where CM_BrId='" & frmMain.lblCode.Text & "' and CM_ID='" & DataGridView1.CurrentCell.Value.ToString & "' and Status='Active'")
                            If CM_Name = "" Then
                                'FrmCustomer.Show()
                                FrmCustomer.Text = "FromDisbursh"
                                FrmCustomer.MdiParent = frmMain
                                FrmCustomer.WindowState = FormWindowState.Maximized
                                FrmCustomer.Show()
                            Else
                                DataGridView1.Rows(iRow).Cells("coCM_Name").Value = CM_Name.ToString
                                DataGridView1.Rows(iRow).Cells("coCM_Phone").Value = CM_Phone.ToString
                                DataGridView1.Rows(iRow).Cells("coAddress").Value = CM_Address.ToString
                                DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("coLD_DisAmt").ColumnIndex, iRow)
                            End If
                        End If
                    End If
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coLD_DisAmt").ColumnIndex Then
                    Try
                        If DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value = "" Then
                            Return
                        ElseIf (DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value) / 1 = DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value Then
                            Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value
                            Me.DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value = Format(as1, "###,###.##")
                            DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("coCurrency").ColumnIndex, iRow)
                            Me.DataGridView1.Rows(iRow).Cells("coCharge_Rate").Value = ""
                            Me.DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value = ""
                        Else
                            resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទឹកប្រាក់ខ្ចីមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        End If
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទឹកប្រាក់ខ្ចីមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Return
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coCurrency").ColumnIndex Then
                    Try
                        Dim Curr As String = getData("select top 1 CU_Name from BK_Currency where CU_ID=" & DataGridView1.CurrentCell.Value)
                        If Curr = "" Then
                            resultError = frmMessageError.ShowBoxError("កូដ 1 សំរាប់រៀល និង កូដ 2 សំរាប់ដុល្លារ។", "ការបញ្ចូលទិន្ន័យខុស")
                        ElseIf Me.DataGridView1.CurrentCell.Value.ToString = "រៀល" Or Me.DataGridView1.CurrentCell.Value.ToString = "ដុល្លារ" Then
                            Return
                        Else
                            DataGridView1.CurrentCell.Value = Curr.ToString
                            DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("coUnit").ColumnIndex, iRow)
                        End If
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("កូដ 1 សំរាប់រៀល និង កូដ 2 សំរាប់ដុល្លារ។", "ការបញ្ចូលទិន្ន័យខុស")
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coUnit").ColumnIndex Then
                    Try
                        Dim Unit As String = getData("select top 1 LU_Name from BK_LoanUnit where LU_ID=" & DataGridView1.CurrentCell.Value)
                        If Unit = "" Then
                            resultError = frmMessageError.ShowBoxError("កូដ 1 ថ្ងៃ,កូដ 2 សំរាប់ សប្តាហ៍,កូដ 3 សំរាប់ 2 សប្តាហ៍ និង កូដ 4 សំរាប់ខែ។", "ការបញ្ចូលទិន្ន័យខុស")
                            DataGridView1.Rows(iRow).Cells("coUnit").Value = ""
                        Else
                            DataGridView1.CurrentCell.Value = Unit.ToString
                            DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coTerm").ColumnIndex, iRow)
                        End If
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("កូដ 1 ថ្ងៃ,កូដ 2 សំរាប់ សប្តាហ៍,កូដ 3 សំរាប់ 2 សប្តាហ៍ និង កូដ 4 សំរាប់ខែ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("coUnit").Value = ""
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coTerm").ColumnIndex Then
                    Try
                        If DataGridView1.Rows(iRow).Cells("coTerm").Value = "" Then
                            'Return
                        ElseIf (DataGridView1.Rows(iRow).Cells("coTerm").Value) / 1 = DataGridView1.Rows(iRow).Cells("coTerm").Value Then
                            DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coIntRate").ColumnIndex, iRow)
                        Else
                            resultError = frmMessageError.ShowBoxError("ការបញ្ចូលកាលចំនួនកាលវិភាគមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                            DataGridView1.Rows(iRow).Cells("coTerm").Value = ""
                        End If
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលកាលចំនួនកាលវិភាគមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("coIntRate").Value = ""
                        Return
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coIntRate").ColumnIndex Then
                    Try
                        If DataGridView1.Rows(iRow).Cells("coIntRate").Value = "" Then
                            'Return
                        ElseIf (DataGridView1.Rows(iRow).Cells("coIntRate").Value) / 1 = DataGridView1.Rows(iRow).Cells("coIntRate").Value Then
                            DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coType").ColumnIndex, iRow)
                        Else
                            resultError = frmMessageError.ShowBoxError("ការបញ្ចូលអត្រាការប្រាក់មិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                            DataGridView1.Rows(iRow).Cells("coIntRate").Value = ""
                        End If
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលអត្រាការប្រាក់មិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("coIntRate").Value = ""
                        Return
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coType").ColumnIndex Then
                    Try
                        Dim Unit As String = getData("select LT_Name from BK_LoanType where LT_ID=" & DataGridView1.CurrentCell.Value)
                        If Unit = "" Then
                            resultError = frmMessageError.ShowBoxError("កូដ 1 ថេរ​ និង កូដ 2 សំរាប់ចុះ។", "ការបញ្ចូលទិន្ន័យខុស")
                            DataGridView1.Rows(iRow).Cells("coType").Value = ""
                        Else
                            DataGridView1.CurrentCell.Value = Unit.ToString
                            DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                        End If
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("កូដ 1 ថេរ​ និង កូដ 2 សំរាប់ចុះ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("coType").Value = ""
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coCharge_Rate").ColumnIndex Then
                    Try
                        If DataGridView1.Rows(iRow).Cells("coCharge_Rate").Value Is Nothing Then
                            Return
                        ElseIf (DataGridView1.Rows(iRow).Cells("coCharge_Rate").Value) / 1 = DataGridView1.Rows(iRow).Cells("coCharge_Rate").Value Then
                            If DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value = "" Then
                                resultError = frmMessageError.ShowBoxError("បញ្ចូលទឹកប្រាក់បញ្ចេញជាមុនសិន។", "ការបញ្ចូលទិន្ន័យខុស")
                                DataGridView1.CurrentCell = DataGridView1(iCol - 6, iRow)
                                DataGridView1.Rows(iRow).Cells("coCharge_Rate").Value = ""
                            Else
                                Dim dis As Double = DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value
                                Dim rate As Double = DataGridView1.Rows(iRow).Cells("coCharge_Rate").Value / 100
                                DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value = dis * rate
                                DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coInsurance").ColumnIndex, iRow)
                            End If
                        Else
                            'resultError = frmMessageError.ShowBoxError("ការបញ្ចូលអត្រាសេវាមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        End If
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលអត្រាសេវាមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("coInsurance").Value = ""
                        Return
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coInsurance").ColumnIndex Then
                    Try
                        If DataGridView1.Rows(iRow).Cells("coInsurance").Value Is Nothing Then
                            Return
                        ElseIf (DataGridView1.Rows(iRow).Cells("coInsurance").Value) / 1 = DataGridView1.Rows(iRow).Cells("coInsurance").Value Then
                            If DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value = "" Then
                                resultError = frmMessageError.ShowBoxError("បញ្ចូលទឹកប្រាក់បញ្ចេញជាមុនសិន។", "ការបញ្ចូលទិន្ន័យខុស")
                                DataGridView1.CurrentCell = DataGridView1(iCol - 6, iRow)
                                DataGridView1.Rows(iRow).Cells("coInsurance").Value = ""
                            Else
                                Dim dis As Double = DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value
                                Dim rate As Double = DataGridView1.Rows(iRow).Cells("coInsurance").Value / 100
                                If DataGridView1.Rows(iRow).Cells("coCurrency").Value = "រៀល" Then
                                    DataGridView1.Rows(iRow).Cells("coInsuranceTotal").Value = ReturnRound(dis * rate)
                                Else
                                    DataGridView1.Rows(iRow).Cells("coInsuranceTotal").Value = dis * rate
                                End If
                                DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coLD_Service").ColumnIndex, iRow)
                            End If
                        Else
                            'resultError = frmMessageError.ShowBoxError("ការបញ្ចូលអត្រាសេវាមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        End If
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលអត្រាសេវាមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("coInsurance").Value = ""
                        Return
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coLD_Service").ColumnIndex Then
                    If Me.DataGridView1.Rows(iRow).Cells("coLD_Service").Value Is Nothing Then
                        MessageBox.Show("1 មាន និង 2 មិនមាន។", "ការបញ្ចូលខុស", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'MessageBox.Show("Can not be null for this column, please check again!","Error!",MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.DataGridView1.Rows(iRow).Cells("coLD_Service").Value = ""
                        Return
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coLD_Service").Value.ToString = "1" Then
                        Me.DataGridView1.Rows(iRow).Cells("coLD_Service").Value = "មាន"
                        Me.DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coDisDate").ColumnIndex, iRow)
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coLD_Service").Value.ToString = "2" Then
                        Me.DataGridView1.Rows(iRow).Cells("coLD_Service").Value = "មិនមាន"
                        Me.DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coDisDate").ColumnIndex, iRow)
                    Else
                        MessageBox.Show("1 មាន និង 2 មិនមាន។", "ការបញ្ចូលខុស", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.DataGridView1.Rows(iRow).Cells("coLD_Service").Value = ""
                        Return
                    End If
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coDisDate").ColumnIndex Then
                    Try
                        Dim a As String = DataGridView1.Rows(iRow).Cells(iCol).Value.ToString
                        Dim dateCheck As Boolean
                        dateCheck = IsDate(a)
                        If dateCheck = True Then
                            DataGridView1.Rows(iRow).Cells(iCol).Value = a
                        Else
                            Dim now As Date
                            Dim day As Integer = DateTime.Now.Day
                            a = a - day
                            now = DateTime.Now.AddDays(a)
                            DataGridView1.Rows(iRow).Cells(iCol).Value = FormatDateTime(now, DateFormat.ShortDate)
                        End If
                        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coDisDatePay").ColumnIndex, iRow)
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលថ្ងៃខ្ចីមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("coDisDate").Value = ""
                        Return
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coDisDatePay").ColumnIndex Then
                    Try
                        Dim a As String = DataGridView1.Rows(iRow).Cells(iCol).Value.ToString
                        Dim dateCheck As Boolean
                        dateCheck = IsDate(a)
                        If dateCheck = True Then
                            'MessageBox.Show("OK")
                        Else
                            Dim now As Date
                            Dim day As Integer = DateTime.Now.Day
                            a = a - day
                            now = DateTime.Now.AddDays(a)
                            DataGridView1.Rows(iRow).Cells(iCol).Value = FormatDateTime(now, DateFormat.ShortDate)
                        End If
                        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("coPayOff").ColumnIndex, iRow)
                        Me.DataGridView1.Rows(iRow).Cells("coPayOff").Value = 0
                        Me.DataGridView1.Rows(iRow).Cells("coRef").Value = 0
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលថ្ងៃខ្ចីមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("coDisDatePay").Value = ""
                        Return
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coPayOff").ColumnIndex Then
                    Try
                        If DataGridView1.Rows(iRow).Cells("coPayOff").Value = "" Then
                            Me.DataGridView1.Rows(iRow).Cells("coRef").ReadOnly = True
                            DataGridView1.Rows(iRow).Cells("coPayOff").Value = 0
                            DataGridView1.Rows(iRow).Cells("coRef").Value = 0
                        ElseIf (DataGridView1.Rows(iRow).Cells("coPayOff").Value) / 1 = DataGridView1.Rows(iRow).Cells("coPayOff").Value Then
                            Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells("coPayOff").Value
                            Me.DataGridView1.Rows(iRow).Cells("coPayOff").Value = Format(as1, "###,###.##")
                       Else
                            resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទឹកប្រាក់ផ្តាច់មិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                            Me.DataGridView1.Rows(iRow).Cells("coRef").ReadOnly = True
                            DataGridView1.Rows(iRow).Cells("coPayOff").Value = 0
                            DataGridView1.Rows(iRow).Cells("coRef").Value = 0
                        End If
                        DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("coRef").ColumnIndex, iRow)
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទឹកប្រាក់ផ្តាច់មិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Me.DataGridView1.Rows(iRow).Cells("coRef").ReadOnly = True
                        DataGridView1.Rows(iRow).Cells("coPayOff").Value = 0
                        DataGridView1.Rows(iRow).Cells("coRef").Value = 0
                        Return
                    End Try
                ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("coRef").ColumnIndex Then
                    Try
                        If DataGridView1.Rows(iRow).Cells("coRef").Value = "" Then
                            DataGridView1.Rows(iRow).Cells("coRef").Value = 0
                        ElseIf (DataGridView1.Rows(iRow).Cells("coRef").Value) / 1 = DataGridView1.Rows(iRow).Cells("coRef").Value Then
                            Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells("coRef").Value
                            Me.DataGridView1.Rows(iRow).Cells("coRef").Value = Format(as1, "###,###.##")
                        Else
                            resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទឹកប្រាក់ផ្តាច់មិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                            DataGridView1.Rows(iRow).Cells("coRef").Value = 0
                        End If
                        'DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("coRef").ColumnIndex, iRow)
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទឹកប្រាក់ខ្ចីមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("coRef").Value = 0
                        Return
                    End Try
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Need IT Now", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        Dim curr As Integer = 0
        If e.KeyCode = Keys.F12 Then
            If Me.DataGridView1.Rows.Count = 0 Then
                newRow()
            End If
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If Format(Me.DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value, "") = "" Then
                Dim a As Integer = checkNull()
                If a = 1 Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្នន័យខុសមិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                    Return
                Else
                    '---------------------------------------------------------------- SH_Service
                    Dim SH_Service As Double
                    If Me.DataGridView1.Rows(iRow).Cells("coLD_Service").Value.ToString.Trim = "មាន" Then
                        If Me.DataGridView1.Rows(iRow).Cells("coCurrency").Value.ToString = "រៀល" Then
                            If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value.ToString = "ថ្ងៃ" Then
                                SH_Service = 1000
                            ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value.ToString = "សប្តាហ៍" Then
                                SH_Service = 2000
                            ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value.ToString = "២សប្តាហ៍" Or Me.DataGridView1.Rows(iRow).Cells("coUnit").Value.ToString = "២សប្តាហ៍ធ្វើការ" Then
                                SH_Service = 3000
                            Else
                                SH_Service = 4000
                            End If
                        Else
                            SH_Service = 1
                        End If
                    Else
                        SH_Service = 0
                    End If
                    Dim int As Double = DataGridView1.Rows(iRow).Cells("coIntRate").Value
                    Dim dis_amt As Integer = DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value
                    Dim first_Date As Date = DataGridView1.Rows(iRow).Cells("coDisDatePay").Value
                    Dim term As Integer = DataGridView1.Rows(iRow).Cells("coTerm").Value
                    Dim Cycle As Integer = Val(getData("select top 1 LD_Cycle from BK_Customer where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells("coCM_ID").Value & "' and Status='Active' and CM_BrId='" & frmMain.lblCode.Text & "'"))
                    Dim CM_ID1 As Integer = Val(getData("select top 1 ID from BK_Customer where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells("coCM_ID").Value & "' and Status='Active' and CM_BrId='" & frmMain.lblCode.Text & "'"))
                    Dim LD_Cycle As String = getData("select COUNT(LD_ID)LD_ID from BK_Loan where CM_ID1='" & CM_ID1 & "' and LD_BrId='" & frmMain.lblCode.Text & "' group by CM_ID1")
                    With Me.DataGridView1.Rows(iRow)
                        If .Cells("coType").Value = "ថេរ" Then
                            If .Cells("coUnit").Value = "សប្តាហ៍" Or .Cells("coUnit").Value = "ខែ" Then
                                CalculateLoan2(dis_amt, int, term, first_Date, SH_Service)
                                PayOff(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                                '------------------------------------------------------ Get Data
                                If LD_Cycle = "" Then
                                    Cycle = 1
                                Else
                                    Cycle = Val(LD_Cycle) + 1
                                End If
                                '----------------------------------------------------- Add loan
                                With Me.DataGridView1.Rows(iRow)
                                    If .Cells("coCurrency").Value.ToString() = "រៀល" Then
                                        curr = 1
                                    Else
                                        curr = 2
                                    End If
                                    addLoan(.Cells("coLD_ID").Value, frmMain.lblCode.Text, .Cells("coCM_ID").Value, .Cells("coDisDate").Value, .Cells("coDisDatePay").Value, .Cells("coDisDateEnd").Value, .Cells("coLD_DisAmt").Value, curr, int, int, Me.DataGridView1.Rows(iRow).Cells("coEM_ID").Value, .Cells("coUnit").Value.ToString(), .Cells("coType").Value.ToString(), term, "Active", 1, frmMain.users.ToString, DateTime.Now(), 0, 0, .Cells("coCharge_Rate").Value, .Cells("coCharge_Amt").Value, Cycle, .Cells("coInsurance").Value, .Cells("coInsuranceTotal").Value)
                                End With
                                FrmCustomer.AddTrace_Customer("UPDATE OLD", CM_ID1)
                                addIn("Update BK_Customer set LD_Cycle='" & Cycle & "' where ID='" & CM_ID1 & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                                FrmCustomer.AddTrace_Customer("UPDATE NEW", CM_ID1)
                                showLoan()
                                newRow()
                            Else
                                '----------- insert to schedule
                                CalculateLoan(dis_amt, int, term, first_Date, SH_Service)
                                '------------ Get payoff
                                PayOff(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                                '------------------------------------------------------ Get Data
                                If LD_Cycle = "" Then
                                    Cycle = 1
                                Else
                                    Cycle = Val(LD_Cycle) + 1
                                End If
                                '----------------------------------------------------- Add loan
                                With Me.DataGridView1.Rows(iRow)
                                    If .Cells("coCurrency").Value.ToString() = "រៀល" Then
                                        curr = 1
                                    Else
                                        curr = 2
                                    End If
                                    addLoan(.Cells("coLD_ID").Value, frmMain.lblCode.Text, .Cells("coCM_ID").Value, .Cells("coDisDate").Value, .Cells("coDisDatePay").Value, .Cells("coDisDateEnd").Value, .Cells("coLD_DisAmt").Value, curr, int, int, Me.DataGridView1.Rows(iRow).Cells("coEM_ID").Value, .Cells("coUnit").Value.ToString(), .Cells("coType").Value.ToString(), term, "Active", 1, frmMain.users.ToString, DateTime.Now(), 0, 0, .Cells("coCharge_Rate").Value, .Cells("coCharge_Amt").Value, Cycle, .Cells("coInsurance").Value, .Cells("coInsuranceTotal").Value)
                                End With
                                FrmCustomer.AddTrace_Customer("UPDATE OLD", CM_ID1)
                                addIn("Update BK_Customer set LD_Cycle='" & Cycle & "' where ID='" & CM_ID1 & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                                FrmCustomer.AddTrace_Customer("UPDATE NEW", CM_ID1)
                                showLoan()
                                newRow()
                            End If
                        ElseIf .Cells("coType").Value = "ចុះ" Then
                            CalculateLoan1(dis_amt, int, term, first_Date, SH_Service)
                            PayOff(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                            '------------------------------------------------------ Get Data
                            If LD_Cycle = "" Then
                                Cycle = 1
                            Else
                                Cycle = Val(LD_Cycle) + 1
                            End If
                            '----------------------------------------------------- Add 
                            With Me.DataGridView1.Rows(iRow)
                                If .Cells("coCurrency").Value.ToString() = "រៀល" Then
                                    curr = 1
                                Else
                                    curr = 2
                                End If
                                addLoan(.Cells("coLD_ID").Value, frmMain.lblCode.Text, .Cells("coCM_ID").Value, .Cells("coDisDate").Value, .Cells("coDisDatePay").Value, .Cells("coDisDateEnd").Value, .Cells("coLD_DisAmt").Value, curr, int, int, Me.DataGridView1.Rows(iRow).Cells("coEM_ID").Value, .Cells("coUnit").Value.ToString(), .Cells("coType").Value.ToString(), term, "Active", 1, frmMain.users.ToString, DateTime.Now(), 0, 0, .Cells("coCharge_Rate").Value, .Cells("coCharge_Amt").Value, Cycle, .Cells("coInsurance").Value, .Cells("coInsuranceTotal").Value)
                            End With
                            FrmCustomer.AddTrace_Customer("UPDATE OLD", CM_ID1)
                            addIn("Update BK_Customer set LD_Cycle='" & Cycle & "' where ID='" & CM_ID1 & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                            FrmCustomer.AddTrace_Customer("UPDATE NEW", CM_ID1)
                            showLoan()
                            newRow()
                        Else
                            CalculateLoan3(dis_amt, int, term, first_Date, SH_Service)
                            PayOff3(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                            '------------------------------------------------------ Get Data
                            If LD_Cycle = "" Then
                                Cycle = 1
                            Else
                                Cycle = Val(LD_Cycle) + 1
                            End If
                            '----------------------------------------------------- Add 
                            With Me.DataGridView1.Rows(iRow)
                                If .Cells("coCurrency").Value.ToString() = "រៀល" Then
                                    curr = 1
                                Else
                                    curr = 2
                                End If
                                addLoan(.Cells("coLD_ID").Value, frmMain.lblCode.Text, .Cells("coCM_ID").Value, .Cells("coDisDate").Value, .Cells("coDisDatePay").Value, .Cells("coDisDateEnd").Value, .Cells("coLD_DisAmt").Value, curr, int, int, Me.DataGridView1.Rows(iRow).Cells("coEM_ID").Value, .Cells("coUnit").Value.ToString(), .Cells("coType").Value.ToString(), term, "Active", 1, frmMain.users.ToString, DateTime.Now(), 0, 0, .Cells("coCharge_Rate").Value, .Cells("coCharge_Amt").Value, Cycle, .Cells("coInsurance").Value, .Cells("coInsuranceTotal").Value)
                            End With
                            FrmCustomer.AddTrace_Customer("UPDATE OLD", CM_ID1)
                            addIn("Update BK_Customer set LD_Cycle='" & Cycle & "' where ID='" & CM_ID1 & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                            FrmCustomer.AddTrace_Customer("UPDATE NEW", CM_ID1)
                            showLoan()
                            newRow()
                        End If
                    End With
                End If
                callLast()
            Else
                If checkNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្នន័យខុសមិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                    Return
                Else
                    If NoRecordChange() = 1 Then
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យគ្មានការកែប្រែ។", "គ្មានការកែប្រែ")
                        Return
                    Else
                        'MessageBox.Show(Me.DataGridView1.Rows(iRow).Cells("coEM_ID").Value & " " & Me.DataGridView1.Rows(iRow).Cells("coEM_ID").Value.ToString)
                        'Return
                        Dim EM_ID As String = Me.DataGridView1.Rows(iRow).Cells("coEM_ID").Value
                        '------------------------------------------------------------------------- Start update record
                        AddTrace_Debursh("UPDATE OLD", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                        updateLoan(Me.DataGridView1.Rows(iRow).Cells("coCM_ID").Value, EM_ID, Me.DataGridView1.Rows(iRow).Cells("coCharge_Rate").Value, Me.DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value, Me.DataGridView1.Rows(iRow).Cells("coInsurance").Value, Me.DataGridView1.Rows(iRow).Cells("coInsuranceTotal").Value, Me.DataGridView1.Rows(iRow).Cells("coPayOff").Value, Me.DataGridView1.Rows(iRow).Cells("coRef").Value, Me.DataGridView1.Rows(iRow).Cells("PID").Value)
                        AddTrace_Debursh("UPDATE NEW", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                        MessageBox.Show("Your record has been updated!!!", "Update completed", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        'resultError = frmMessageError.ShowBoxError("ការកែប្រែបានសម្រេច។", "ជោគជ័យ")
                        showLoan()
                    End If
                End If
            End If
            callLast()
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            newRow()
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.F11 Then
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If Me.DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value Is Nothing Then
                resultError = frmMessageError.ShowBoxError("ឥណទានមិនទាន់រក្សាទុកផងបោះទៅតារាងម្តេចហ្នឹងកើត សូមពិនិត្យឡើងវិញ។", "មិនទាន់រក្សាទុក")
                Return
            Else
                Me.toExcel(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value, Me.DataGridView1.CurrentCell.RowIndex)
            End If
        ElseIf e.KeyCode = Keys.F10 Then
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If Me.DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value Is Nothing Then
                resultError = frmMessageError.ShowBoxError("ឥណទានមិនទាន់រក្សាទុកផងបោះទៅតារាងម្តេចហ្នឹងកើត សូមពិនិត្យឡើងវិញ។", "មិនទាន់រក្សាទុក")
                Return
            Else
                Me.toExcel1(Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value, Me.DataGridView1.CurrentCell.RowIndex)
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            Dim a As Integer = Me.DataGridView1.Rows.Count()
            If a = 0 Then
                resultError = frmMessageError.ShowBoxError("គ្មានទិន្នន័យត្រូវលប់ សូមពិនិត្យឡើងវិញ។", "គ្មានទិន្នន័យ")
            Else
                Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
                '--------------------------------------------------- Check Loan in repay or not
                Dim LR_ID As String = getData("select top 1 LD_ID from BK_LoanRepay where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'")
                If LR_ID <> "" Then
                    resultError = frmMessageError.ShowBoxError("មិនអាចលុបបានទេ ពីព្រោះលេខកិច្ចសន្យានេះមានប្រតិបត្តការហើយ។", "មិនអាចលុបបានទេ")
                    Return
                Else
                    result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                    If result = "1" Then
                        Dim Cycle As Integer = Val(getData("select top 1 LD_Cycle from BK_Customer where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells("coCM_ID").Value & "' and Status='Active' and CM_BrId='" & frmMain.lblCode.Text & "'"))
                        Dim CM_ID1 As Integer = Val(getData("select top 1 ID from BK_Customer where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells("coCM_ID").Value & "' and Status='Active' and CM_BrId='" & frmMain.lblCode.Text & "'"))
                        '-------------------------------------------------------------- Delete Loan and Schedule
                        AddTrace_Debursh("DELETE", Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value)
                        FrmCustomer.AddTrace_Customer("UPDATE OLD", CM_ID1)
                        addIn("Update BK_Customer set LD_Cycle='" & Cycle - 1 & "' where ID='" & CM_ID1 & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                        addIn("delete from BK_Loan where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                        addIn("delete from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
                        FrmCustomer.AddTrace_Customer("UPDATE NEW", CM_ID1)
                        Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុបទិន្នន័យ")
                    End If
                End If
            End If
            callLast()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
        Dim Null As String = getData("IF COL_LENGTH('BK_Loan', 'PID') IS NOT NULL BEGIN select top 1 LD_ID from BK_Loan END")
        If Null <> "" Then
            AddToGridLoan(DataGridView1, 26, "exec spListLoan '" & date1 & "','" & frmMain.lblCode.Text & "'")
            'MessageBox.Show("Exist!")
        Else
            AddToGridLoan(DataGridView1, 26, "exec spListLoanMorokot '" & date1 & "','" & frmMain.lblCode.Text & "'")
            'MessageBox.Show("No Exist!")
        End If

    End Sub
    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        Try
            Dim total As Double = DataGridView1.SelectedCells.Cast(Of DataGridViewCell)().Sum(Function(cell) CDec(cell.Value))
            'MessageBox.Show(DataGridView1.SelectedCells.
            lblResultSum.Text = total.ToString("##,###.00")
            'MessageBox.Show(total)
            'Dim count As Integer = DataGridView1.SelectedRows.Count.ToString
            '--- for count
            lblResultCount.Text = DataGridView1.SelectedCells.Count.ToString
        Catch ex As Exception
        End Try

    End Sub
    '------------------------------------------------------------ Function and Method
    Private Sub CalculateLoan2(ByVal Disbursh As Integer, ByVal interestRate As Double, ByVal term As Integer, ByVal datefirstpay As DateTime, ByVal LD_Service As Double)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim LD_ID As Integer = DataGridView1.Rows(iRow).Cells("coLD_ID").Value.ToString
        Dim CM_ID As Integer = DataGridView1.Rows(iRow).Cells("coCM_ID").Value.ToString
        Dim SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt As Double
        datefirstpay = Check_date(datefirstpay, 1)
        Dim payDate As DateTime = datefirstpay
        Dim interestRate1 As Double = 0
        Dim SH_Int_Org As Double = 0
        Dim loanAmount As Double
        Dim amortizationTerm As Integer = 0
        Dim no As Integer = 0
        Dim SH_Balance_Org As Double
        Dim cummulativeInterest As Double = 0
        Dim monthlyPrincipal As Double = 0
        Dim cummulativePrincipal As Double = 0
        Dim payoff As Double = 0
        Dim amountday As Integer = 0
        loanAmount = Disbursh
        SH_Balance_Org = Disbursh
        interestRate1 = interestRate * 0.01
        amortizationTerm = term
        ' Calculate the monthly payment and round it to 2 decimal places         
        Dim SH_Prn_Org = loanAmount / amortizationTerm
        SH_Prn_Org = Math.Round(SH_Prn_Org, 2)
        ' Loop for amortization term (number of monthly payments)
        For j As Integer = 0 To amortizationTerm - 1
            ' Calculate monthly cycle
            SH_Int_Org = Disbursh * interestRate1
            monthlyPrincipal = SH_Prn_Org - SH_Int_Org
            SH_Balance_Org = SH_Balance_Org - SH_Prn_Org
            If j = amortizationTerm - 1 AndAlso SH_Balance_Org <> SH_Prn_Org Then
                ' Adjust the last payment to make sure the final balance is 0
                SH_Prn_Org += SH_Balance_Org
                SH_Balance_Org = 0
            End If
            ' Reset Date
            Dim day As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' ORDER BY SH_ID DESC")
            If day = "" Then
                payDate = payDate
            Else
                If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                    payDate = payDate.AddDays(7)
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                    If day <> "" Then
                        Dim secondDay As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' and SH_Date <'" & day & "' ORDER BY SH_ID desc")
                        If secondDay <> "" Then
                            aD = DateDiff(DateInterval.Day, Convert.ToDateTime(secondDay), Convert.ToDateTime(day))
                            If aD > 14 Then
                                payDate = payDate.AddDays(14 - (aD - 14))
                            Else
                                payDate = payDate.AddDays(14)
                            End If
                        Else
                            payDate = payDate.AddDays(14)
                        End If
                        'MessageBox.Show(secondDay.ToString() & "" & aD)
                    End If
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                    payDate = payDate.AddDays(30)
                Else
                    payDate = payDate.AddDays(1)
                End If
            End If
            payDate = Check_date1(payDate, 1)
            'If day = "" Then
            '    Dim Name_Day As String = payDate.DayOfWeek
            'End If
            ''---------------------------------------------------------------------- Loan type week
            cummulativeInterest += SH_Int_Org
            cummulativePrincipal += monthlyPrincipal
            no = no + 1
            '---------------------------------------------------------------- សំរាប់ការបង់ចុះ
            If Me.DataGridView1.Rows(iRow).Cells("coType").Value = "ចុះ" Then
                Dim ID As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID ")
                If ID = "" Then
                    amountday = DateDiff(DateInterval.Day, Me.DataGridView1.Rows(iRow).Cells("coDisDate").Value, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        Dim IntDay As Double = SH_Int_Org / 7
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                        Dim IntDay As Double = SH_Int_Org / 14
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        Dim IntDay As Double = SH_Int_Org / 30
                        SH_Int_Org = IntDay * amountday
                    Else
                        Dim IntDay As Double = SH_Int_Org / 1
                        SH_Int_Org = IntDay * amountday
                    End If
                Else
                    Dim ID1 As Date = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc ")
                    Dim Balance As Double = Val(getData("select top 1 SH_Balance_Org from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc "))
                    Dim int As Double = Balance * (interestRate * 0.01)
                    amountday = DateDiff(DateInterval.Day, ID1, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        SH_Int_Org = SH_Int_Org * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                        SH_Int_Org = SH_Int_Org * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        SH_Int_Org = SH_Int_Org * amountday
                    Else
                        SH_Int_Org = SH_Int_Org * amountday
                    End If
                End If
            Else
                Dim IntDay As Double
                If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                    IntDay = SH_Int_Org / 7
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                    IntDay = SH_Int_Org / 30
                End If
                Dim ID As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID ")
                If ID = "" Then
                    amountday = DateDiff(DateInterval.Day, Me.DataGridView1.Rows(iRow).Cells("coDisDate").Value, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        SH_Int_Org = IntDay * amountday
                    End If
                Else
                    Dim ID1 As Date = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc ")
                    Dim Balance As Double = Val(getData("select top 1 SH_Balance_Org from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc "))
                    amountday = DateDiff(DateInterval.Day, ID1, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        SH_Int_Org = IntDay * amountday
                    Else
                        SH_Int_Org = IntDay * amountday
                    End If
                End If
            End If
            '---------------------------------------------------------------- Round up if Riel
            If Me.DataGridView1.Rows(iRow).Cells("coCurrency").Value = "រៀល" Then
                SH_Prn_Amt = ReturnRound(SH_Prn_Org)
                SH_Int_Amt = ReturnRound(SH_Int_Org)
                SH_Ballance_Amt = ReturnRound(SH_Balance_Org)
            Else
                SH_Prn_Amt = SH_Prn_Org
                SH_Int_Amt = SH_Int_Org
                SH_Ballance_Amt = SH_Balance_Org
            End If
            '---------------------------------------------------------------------------------
            addLoanSchedule1(LD_ID, CM_ID, frmMain.lblCode.Text, payDate, SH_Prn_Org, SH_Int_Org, SH_Balance_Org, SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt, 1, frmMain.users.ToString, DateTime.Now(), LD_Service)
        Next
        '-------------------------------------- Show after saved loan
        Dim lastDate As Date = getData("select max(SH_Date) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
        Me.DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value = lastDate
        Me.DataGridView1.Rows(iRow).Cells(0).Value = "Saved"
    End Sub
    Private Sub CalculateLoan3(ByVal Disbursh As Integer, ByVal interestRate As Double, ByVal term As Integer, ByVal datefirstpay As DateTime, ByVal LD_Service As Double)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim LD_ID As Integer = DataGridView1.Rows(iRow).Cells("coLD_ID").Value.ToString
        Dim CM_ID As Integer = DataGridView1.Rows(iRow).Cells("coCM_ID").Value.ToString
        Dim SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt As Double
        datefirstpay = Check_date(datefirstpay, 1)
        Dim payDate As DateTime = datefirstpay
        Dim interestRate1 As Double = 0
        Dim SH_Int_Org As Double = 0
        Dim loanAmount As Double
        Dim amortizationTerm As Integer = 0
        Dim no As Integer = 0
        Dim SH_Balance_Org As Double
        Dim cummulativeInterest As Double = 0
        Dim monthlyPrincipal As Double = 0
        Dim cummulativePrincipal As Double = 0
        Dim payoff As Double = 0
        Dim amountday As Integer = 0
        'int i = 0;
        loanAmount = Disbursh
        SH_Balance_Org = Disbursh
        interestRate1 = interestRate * 0.01
        amortizationTerm = term
        ' Calculate the monthly payment and round it to 2 decimal places         
        Dim SH_Prn_Org = 0
        'SH_Prn_Org = Math.Round(SH_Prn_Org, 2)
        ' Loop for amortization term (number of monthly payments)
        For j As Integer = 0 To amortizationTerm - 1
            ' Calculate monthly cycle
            SH_Int_Org = Disbursh * interestRate1
            monthlyPrincipal = SH_Prn_Org - SH_Int_Org
            SH_Balance_Org = Disbursh
            If j = amortizationTerm - 1 AndAlso SH_Balance_Org <> SH_Prn_Org Then
                ' Adjust the last payment to make sure the final balance is 0
                SH_Prn_Org = Disbursh
                SH_Balance_Org = 0
            End If
            ' Reset Date
            Dim day As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' ORDER BY SH_ID DESC")
            If day = "" Then
                payDate = payDate
            Else
                If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                    payDate = payDate.AddDays(7)
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                    If day <> "" Then
                        Dim secondDay As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' and SH_Date <'" & day & "' ORDER BY SH_ID desc")
                        If secondDay <> "" Then
                            aD = DateDiff(DateInterval.Day, Convert.ToDateTime(secondDay), Convert.ToDateTime(day))
                            If aD > 14 Then
                                payDate = payDate.AddDays(14 - (aD - 14))
                            Else
                                payDate = payDate.AddDays(14)
                            End If
                        Else
                            payDate = payDate.AddDays(14)
                        End If
                        'MessageBox.Show(secondDay.ToString() & "" & aD)
                    End If
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                    payDate = payDate.AddDays(30)
                Else
                    payDate = payDate.AddDays(1)
                End If
            End If
            If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                payDate = Check_date1(payDate, 1)
            Else
                payDate = Check_date(payDate, 1)
            End If
            payDate = Check_date(payDate, 1)
            cummulativeInterest += SH_Int_Org
            cummulativePrincipal += monthlyPrincipal
            no = no + 1
            '---------------------------------------------------------------- សំរាប់ការបង់ចុះ
            If Me.DataGridView1.Rows(iRow).Cells("coType").Value = "ចុះ" Then
                Dim ID As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID ")
                If ID = "" Then
                    amountday = DateDiff(DateInterval.Day, Me.DataGridView1.Rows(iRow).Cells("coDisDate").Value, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        Dim IntDay As Double = SH_Int_Org / 7
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                        Dim IntDay As Double = SH_Int_Org / 14
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        Dim IntDay As Double = SH_Int_Org / 30
                        SH_Int_Org = IntDay * amountday
                    Else
                        Dim IntDay As Double = SH_Int_Org / 1
                        SH_Int_Org = IntDay * amountday
                    End If
                Else
                    Dim ID1 As Date = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc ")
                    Dim Balance As Double = Val(getData("select top 1 SH_Balance_Org from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc "))
                    Dim int As Double = Balance * (interestRate * 0.01)
                    amountday = DateDiff(DateInterval.Day, ID1, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        SH_Int_Org = (int / 7) * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                        SH_Int_Org = (int / 14) * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        SH_Int_Org = (int / 30) * amountday
                    Else
                        SH_Int_Org = (int / 1) * amountday
                    End If
                End If
            End If
            '---------------------------------------------------------------- Round up if Riel
            If Me.DataGridView1.Rows(iRow).Cells("coCurrency").Value = "រៀល" Then
                SH_Prn_Amt = ReturnRound(SH_Prn_Org)
                SH_Int_Amt = ReturnRound(SH_Int_Org)
                SH_Ballance_Amt = ReturnRound(SH_Balance_Org)
            Else
                SH_Prn_Amt = SH_Prn_Org
                SH_Int_Amt = SH_Int_Org
                SH_Ballance_Amt = SH_Balance_Org
            End If

            'Dim inter1 As Double = 0
            ''Dim f As Double = 0
            'If Me.DataGridView1.Rows(iRow).Cells("coCurrency").Value = "រៀល" Then
            '    inter1 = ReturnRound(SH_Balance_Org * interestRate1 * term)
            '    'inter1 = f * term
            'Else
            '    inter1 = (SH_Balance_Org * interestRate1) * term
            'End If
            '---------------------------------------------------------------------------------
            'payoff = SH_Prn_Amt + SH_Int_Amt + SH_Ballance_Amt + inter1
            addLoanSchedule1(LD_ID, CM_ID, frmMain.lblCode.Text, payDate, SH_Prn_Org, SH_Int_Org, SH_Balance_Org, SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt, 1, frmMain.users.ToString, DateTime.Now(), LD_Service)
        Next
        Dim lastDate As Date = getData("select max(SH_Date) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
        Me.DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value = lastDate
        Me.DataGridView1.Rows(iRow).Cells(0).Value = "Saved"
    End Sub
    Private Sub CalculateLoan1(ByVal Disbursh As Integer, ByVal interestRate As Double, ByVal term As Integer, ByVal datefirstpay As DateTime, ByVal LD_Service As Double)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim LD_ID As Integer = DataGridView1.Rows(iRow).Cells("coLD_ID").Value.ToString
        Dim CM_ID As Integer = DataGridView1.Rows(iRow).Cells("coCM_ID").Value.ToString
        Dim SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt As Double
        datefirstpay = Check_date(datefirstpay, 1)
        Dim payDate As DateTime = datefirstpay
        Dim interestRate1 As Double = 0
        Dim SH_Int_Org As Double = 0
        Dim loanAmount As Double
        Dim amortizationTerm As Integer = 0
        Dim no As Integer = 0
        Dim SH_Balance_Org As Double
        Dim cummulativeInterest As Double = 0
        Dim monthlyPrincipal As Double = 0
        Dim cummulativePrincipal As Double = 0
        Dim payoff As Double = 0
        Dim amountday As Integer = 0
        'int i = 0;
        loanAmount = Disbursh
        SH_Balance_Org = Disbursh
        interestRate1 = interestRate * 0.01
        amortizationTerm = term
        ' Calculate the monthly payment and round it to 2 decimal places         
        Dim SH_Prn_Org = loanAmount / amortizationTerm
        SH_Prn_Org = Math.Round(SH_Prn_Org, 2)
        ' Loop for amortization term (number of monthly payments)
        For j As Integer = 0 To amortizationTerm - 1
            ' Calculate monthly cycle
            Dim dayOfInt As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' ORDER BY SH_ID DESC")
            If dayOfInt = "" Then
                SH_Int_Org = Disbursh * interestRate1
            Else
                SH_Int_Org = Val(getData("select top 1 SH_Balance from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' ORDER BY SH_ID DESC")) * interestRate1
            End If
            'SH_Int_Org = Disbursh * interestRate1
            monthlyPrincipal = SH_Prn_Org - SH_Int_Org
            SH_Balance_Org = SH_Balance_Org - SH_Prn_Org
            If j = amortizationTerm - 1 AndAlso SH_Balance_Org <> SH_Prn_Org Then
                ' Adjust the last payment to make sure the final balance is 0
                SH_Prn_Org += SH_Balance_Org
                SH_Balance_Org = 0
            End If
            ' Reset Date
            Dim day As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' ORDER BY SH_ID DESC")
            If day = "" Then
                payDate = payDate
            Else
                If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                    payDate = payDate.AddDays(7)
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                    If day <> "" Then
                        Dim secondDay As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' and SH_Date <'" & day & "' ORDER BY SH_ID desc")
                        If secondDay <> "" Then
                            aD = DateDiff(DateInterval.Day, Convert.ToDateTime(secondDay), Convert.ToDateTime(day))
                            If aD > 14 Then
                                payDate = payDate.AddDays(14 - (aD - 14))
                            Else
                                payDate = payDate.AddDays(14)
                            End If
                        Else
                            payDate = payDate.AddDays(14)
                        End If

                        'MessageBox.Show(secondDay.ToString() & "" & aD)
                    End If
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍ធ្វើការ" Then
                    If day <> "" Then
                        Dim secondDay As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' and SH_Date <'" & day & "' ORDER BY SH_ID desc")
                        If secondDay <> "" Then
                            aD = DateDiff(DateInterval.Day, Convert.ToDateTime(secondDay), Convert.ToDateTime(day))
                            If aD > 15 Then
                                payDate = payDate.AddDays(15 - (aD - 15))
                            Else
                                payDate = payDate.AddDays(15)
                            End If
                        Else
                            payDate = payDate.AddDays(15)
                        End If
                        'MessageBox.Show(secondDay.ToString() & "" & aD)
                    End If
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                    payDate = payDate.AddDays(30)
                Else
                    payDate = payDate.AddDays(1)
                End If
            End If
            If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                payDate = Check_date1(payDate, 1)
            Else
                payDate = Check_date(payDate, 1)
            End If
            payDate = Check_date(payDate, 1)
            cummulativeInterest += SH_Int_Org
            cummulativePrincipal += monthlyPrincipal
            no = no + 1
            '---------------------------------------------------------------- សំរាប់ការបង់ចុះ
            If Me.DataGridView1.Rows(iRow).Cells("coType").Value = "ចុះ" Then
                Dim ID As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID ")
                If ID = "" Then
                    amountday = DateDiff(DateInterval.Day, Me.DataGridView1.Rows(iRow).Cells("coDisDate").Value, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        Dim IntDay As Double = SH_Int_Org / 7
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                        Dim IntDay As Double = SH_Int_Org / 14
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍ធ្វើការ" Then
                        Dim IntDay As Double = SH_Int_Org / 15
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        Dim IntDay As Double = SH_Int_Org / 30
                        SH_Int_Org = IntDay * amountday
                    Else
                        Dim IntDay As Double = SH_Int_Org / 1
                        SH_Int_Org = IntDay * amountday
                    End If
                Else
                    Dim ID1 As Date = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc ")
                    Dim Balance As Double = Val(getData("select top 1 SH_Balance_Org from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc "))
                    Dim int As Double = Balance * (interestRate * 0.01)
                    amountday = DateDiff(DateInterval.Day, ID1, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        SH_Int_Org = (int / 7) * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                        SH_Int_Org = (int / 14) * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍ធ្វើការ" Then
                        Dim IntDay As Double = SH_Int_Org / 15
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        SH_Int_Org = (int / 30) * amountday
                    Else
                        SH_Int_Org = (int / 1) * amountday
                    End If
                End If
            End If
            '---------------------------------------------------------------- Round up if Riel
            If Me.DataGridView1.Rows(iRow).Cells("coCurrency").Value = "រៀល" Then
                SH_Prn_Amt = ReturnRound(SH_Prn_Org)
                SH_Int_Amt = ReturnRound(SH_Int_Org)
                SH_Ballance_Amt = ReturnRound(SH_Balance_Org)
            Else
                SH_Prn_Amt = SH_Prn_Org
                SH_Int_Amt = SH_Int_Org
                SH_Ballance_Amt = SH_Balance_Org
            End If

            'Dim inter1 As Double = 0
            ''Dim f As Double = 0
            'If Me.DataGridView1.Rows(iRow).Cells("coCurrency").Value = "រៀល" Then
            '    inter1 = ReturnRound(SH_Balance_Org * interestRate1 * term)
            '    'inter1 = f * term
            'Else
            '    inter1 = (SH_Balance_Org * interestRate1) * term
            'End If
            '---------------------------------------------------------------------------------
            'payoff = SH_Prn_Amt + SH_Int_Amt + SH_Ballance_Amt + inter1
            addLoanSchedule1(LD_ID, CM_ID, frmMain.lblCode.Text, payDate, SH_Prn_Org, SH_Int_Org, SH_Balance_Org, SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt, 1, frmMain.users.ToString, DateTime.Now(), LD_Service)
        Next
        Dim lastDate As Date = getData("select max(SH_Date) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
        Me.DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value = lastDate
        Me.DataGridView1.Rows(iRow).Cells(0).Value = "Saved"
    End Sub

    Private Sub PayOff3(ByVal LD_ID As Integer)
        Dim i As Integer = 0
        Dim DisInt As Double = 0
        'Dim DisRate As Double = 0
        'If Me.DataGridView1.Rows(Me.DataGridView1.CurrentCell.RowIndex).Cells("coUnit").Value <> "ថ្ងៃ" Then
        '    DisRate = 0.2
        'End If

        Dim sh As Integer = Val(getData("select count(LD_ID) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'"))
        While i < sh
            Dim SH_Date As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' and isnull(SH_PayoffAmt,0)= 0 order by SH_ID")
            Dim TotalInt As Double = Val(getData("select sum(isnull(SH_Int_Amt,0)) from  BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_Date >'" & SH_Date & "'"))
            Dim int As Double = Val(getData("select isnull(SH_Int_Amt,0) from  BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_Date ='" & SH_Date & "'"))
            Dim TotalService As Double = Val(getData("select sum(isnull(SH_Service,0)) from  BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_Date >'" & SH_Date & "'"))
            Dim SH_Prn_Amt As Double = Val(getData("select top 1 SH_Prn_Amt from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' and isnull(SH_PayoffAmt,0)= 0 order by SH_ID"))
            Dim SH_Balance_Amt As Double = Val(getData("select top 1 SH_Balance from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' and isnull(SH_PayoffAmt,0)= 0 order by SH_ID"))
            Dim LD_Service As Double = Val(getData("select isnull(SH_Service,0) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_Date ='" & SH_Date & "' and SH_BrId='" & frmMain.lblCode.Text & "'"))
            Dim a, b As Double
            a = 0
            b = 0
            Dim PayoffAmt As Double
            '= TotalInt + int + SH_Balance_Amt + LD_Service + SH_Prn_Amt
            a = TotalInt
            b = a / 2
            If Me.DataGridView1.Rows(Me.DataGridView1.CurrentRow.Index).Cells("coCurrency").Value = "ដុល្លារ" Then
                PayoffAmt = (SH_Prn_Amt + SH_Balance_Amt + LD_Service + int) + b
            Else
                PayoffAmt = (SH_Prn_Amt + SH_Balance_Amt + LD_Service + int) + ReturnRound(b)
            End If
            'MessageBox.Show(LD_Service.ToString)
            addIn("Update BK_LoanSchedule set SH_PayoffAmt='" & PayoffAmt & "' where SH_Date='" & SH_Date & "' and LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
            i = i + 1
        End While
    End Sub
    Private Sub PayOff(ByVal LD_ID As Integer)
        Dim i As Integer = 0
        Dim DisInt As Double = 0
        Dim DisRate As Double = 0
        If Me.DataGridView1.Rows(Me.DataGridView1.CurrentCell.RowIndex).Cells("coUnit").Value = "សប្តាហ៍" Or Me.DataGridView1.Rows(Me.DataGridView1.CurrentCell.RowIndex).Cells("coUnit").Value = "២សប្តាហ៍" Then
            DisRate = 0.2
        ElseIf Me.DataGridView1.Rows(Me.DataGridView1.CurrentCell.RowIndex).Cells("coUnit").Value = "ខែ" Then
            DisRate = 0.5
        End If

        Dim sh As Integer = Val(getData("select count(LD_ID) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'"))
        While i < sh
            Dim SH_Date As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' and isnull(SH_PayoffAmt,0)= 0 order by SH_ID")
            Dim TotalInt As Double = Val(getData("select sum(isnull(SH_Int_Amt,0)) from  BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_Date >'" & SH_Date & "'"))
            Dim int As Double = Val(getData("select isnull(SH_Int_Amt,0) from  BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_Date ='" & SH_Date & "'"))
            Dim TotalService As Double = Val(getData("select sum(isnull(SH_Service,0)) from  BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_Date >'" & SH_Date & "'"))
            Dim SH_Prn_Amt As Double = Val(getData("select top 1 SH_Prn_Amt from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' and isnull(SH_PayoffAmt,0)= 0 order by SH_ID"))
            Dim SH_Balance_Amt As Double = Val(getData("select top 1 SH_Balance from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' and isnull(SH_PayoffAmt,0)= 0 order by SH_ID"))
            Dim LD_Service As Double = Val(getData("select top 1 isnull(SH_Service,0) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_Date >='" & SH_Date & "'"))
            Dim a, b As Double
            a = 0
            b = 0
            Dim PayoffAmt As Double
            '= TotalInt + int + SH_Balance_Amt + LD_Service + SH_Prn_Amt
            a = TotalInt
            b = (a - (a * DisRate))
            TotalService = TotalService - (TotalService * DisRate)
            If Me.DataGridView1.Rows(Me.DataGridView1.CurrentRow.Index).Cells("coCurrency").Value = "ដុល្លារ" Then
                PayoffAmt = (SH_Prn_Amt + SH_Balance_Amt + LD_Service + TotalService + int) + b
            Else
                PayoffAmt = (SH_Prn_Amt + SH_Balance_Amt + LD_Service + TotalService + int) + ReturnRound(b)
            End If
            'MessageBox.Show(LD_Service.ToString)
            addIn("Update BK_LoanSchedule set SH_PayoffAmt='" & PayoffAmt & "' where SH_Date='" & SH_Date & "' and LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
            i = i + 1
        End While
    End Sub
    Private Sub newRow()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        With DataGridView1.Rows(iRow)
            .Cells(0).Style.BackColor = Color.Yellow
            .Cells(0).Value = "Editing"
            .Cells("coEM_Name").Style.BackColor = Color.Yellow
            .Cells("coEM_Name").ReadOnly = True
            .Cells("coCM_Name").Style.BackColor = Color.Yellow
            .Cells("coCM_Name").ReadOnly = True
            .Cells("coCM_Phone").Style.BackColor = Color.Yellow
            .Cells("coCM_Phone").ReadOnly = True
            .Cells("coAddress").Style.BackColor = Color.Yellow
            .Cells("coAddress").ReadOnly = True
            .Cells("coCharge_Amt").Style.BackColor = Color.Yellow
            .Cells("coCharge_Amt").ReadOnly = True
            .Cells("coDisDateEnd").Style.BackColor = Color.Yellow
            .Cells("coDisDateEnd").ReadOnly = True
            .Cells("coInsuranceTotal").ReadOnly = True
            .Cells("coInsuranceTotal").Style.BackColor = Color.Yellow
            .Cells("PName").ReadOnly = True
            .Cells("PName").Style.BackColor = Color.Yellow
            DataGridView1.CurrentCell = DataGridView1(1, iRow)
        End With
    End Sub
    Private Sub clear()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        DataGridView1.Rows(iRow).Cells("coEM_ID").Value = ""
        DataGridView1.Rows(iRow).Cells("coEM_Name").Value = ""
        DataGridView1.Rows(iRow).Cells("coCM_ID").Value = ""
        DataGridView1.Rows(iRow).Cells("coCM_Name").Value = ""
        DataGridView1.Rows(iRow).Cells("coCM_Phone").Value = ""
        DataGridView1.Rows(iRow).Cells("coAddress").Value = ""
        DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value = ""
        DataGridView1.Rows(iRow).Cells("coCurrency").Value = ""
        DataGridView1.Rows(iRow).Cells("coUnit").Value = ""
        DataGridView1.Rows(iRow).Cells("coTerm").Value = ""
        DataGridView1.Rows(iRow).Cells("coIntRate").Value = ""
        DataGridView1.Rows(iRow).Cells("coType").Value = ""
        DataGridView1.Rows(iRow).Cells("coCharge_Rate").Value = ""
        DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value = ""
        DataGridView1.Rows(iRow).Cells("coDisDate").Value = ""
        DataGridView1.Rows(iRow).Cells("coDisDatePay").Value = ""
        DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value = ""
        DataGridView1.Rows(iRow).Cells("coLD_Service").Value = ""
    End Sub
    Private Sub Int()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim a As String = DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value
        If a = "" Then
            DataGridView1.Rows(iRow).Cells("coIntRate").ReadOnly = False
            DataGridView1.Columns("coIntRate").DefaultCellStyle.BackColor = Color.White
        Else
            DataGridView1.Rows(iRow).Cells("coIntRate").ReadOnly = True
            DataGridView1.Columns("coIntRate").DefaultCellStyle.BackColor = Color.Yellow
        End If
    End Sub
    Private Function checkNull()
        Dim a As Integer
        Dim dg As DataGridView = DataGridView1
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If dg.Rows(iRow).Cells("coLD_ID").Value Is Nothing Or dg.Rows(iRow).Cells("coEM_ID").Value Is Nothing Or dg.Rows(iRow).Cells("coEM_Name").Value Is Nothing Or dg.Rows(iRow).Cells("coCM_ID").Value Is Nothing _
            Or dg.Rows(iRow).Cells("coCM_Name").Value Is Nothing Or dg.Rows(iRow).Cells("coCM_Phone").Value Is Nothing Or dg.Rows(iRow).Cells("coAddress").Value Is Nothing _
        Or dg.Rows(iRow).Cells("coLD_DisAmt").Value Is Nothing Or
         dg.Rows(iRow).Cells("coCurrency").Value Is Nothing Or dg.Rows(iRow).Cells("coUnit").Value Is Nothing Or dg.Rows(iRow).Cells("coTerm").Value Is Nothing Or dg.Rows(iRow).Cells("coIntRate").Value Is Nothing Or
                   dg.Rows(iRow).Cells("coType").Value Is Nothing Or dg.Rows(iRow).Cells("coCharge_Rate").Value Is Nothing Or dg.Rows(iRow).Cells("coCharge_Amt").Value Is Nothing Or dg.Rows(iRow).Cells("coDisDate").Value Is Nothing Or
                        dg.Rows(iRow).Cells("coDisDatePay").Value Is Nothing Or dg.Rows(iRow).Cells("coLD_Service").Value Is Nothing Then
            a = 1
        Else
            a = 2
        End If
        Return a
    End Function
    Sub datagrid2()
        SetFontDatagrid1(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 19
        DataGridView1.Columns(0).Name = "...."
        DataGridView1.Columns(1).Name = "កិច្ចសន្យា"
        DataGridView1.Columns(2).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(3).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(4).Name = "កូដអតិថិជន"
        DataGridView1.Columns(5).Name = "ឈ្មោះអតិថិជន"
        DataGridView1.Columns(6).Name = "លេខទូរស័ព្ទ"
        DataGridView1.Columns(7).Name = "អសយដ្ឋាន"
        DataGridView1.Columns(8).Name = "ទឹកប្រាក់ខ្ចី"
        DataGridView1.Columns(9).Name = "រូបបិយវត្ថុ"
        DataGridView1.Columns(10).Name = "ឯកត្តា"
        DataGridView1.Columns(11).Name = "កាលវិភាគ"
        DataGridView1.Columns(12).Name = "អត្រាការប្រាក់"
        DataGridView1.Columns(13).Name = "ប្រភេទ"
        DataGridView1.Columns(14).Name = "អត្រាថ្លៃសេវា"
        DataGridView1.Columns(15).Name = "ថ្លៃសេវាសរុប"
        DataGridView1.Columns(16).Name = "ថ្ងៃខ្ចី"
        DataGridView1.Columns(17).Name = "ថ្ងៃចាប់ផ្តើម"
        DataGridView1.Columns(18).Name = "រហូតដល់"
    End Sub
    Private Sub CalculateLoan(ByVal Disbursh As Integer, ByVal interestRate As Double, ByVal term As Integer, ByVal datefirstpay As DateTime, ByVal LD_Service As Double)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim LD_ID As Integer = DataGridView1.Rows(iRow).Cells("coLD_ID").Value.ToString
        Dim CM_ID As Integer = DataGridView1.Rows(iRow).Cells("coCM_ID").Value.ToString
        Dim SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt As Double
        datefirstpay = Check_date(datefirstpay, 1)
        Dim payDate As DateTime = datefirstpay
        Dim interestRate1 As Double = 0
        Dim SH_Int_Org As Double = 0
        Dim loanAmount As Double
        Dim amortizationTerm As Integer = 0
        Dim no As Integer = 0
        Dim SH_Balance_Org As Double
        Dim cummulativeInterest As Double = 0
        Dim monthlyPrincipal As Double = 0
        Dim cummulativePrincipal As Double = 0
        Dim payoff As Double = 0
        'int i = 0;
        loanAmount = Disbursh
        SH_Balance_Org = Disbursh
        interestRate1 = interestRate * 0.01
        amortizationTerm = term
        ' Calculate the monthly payment and round it to 2 decimal places         
        Dim SH_Prn_Org = loanAmount / amortizationTerm
        SH_Prn_Org = Math.Round(SH_Prn_Org, 2)
        ' Loop for amortization term (number of monthly payments)
        For j As Integer = 0 To amortizationTerm - 1
            ' Calculate monthly cycle
            SH_Int_Org = Disbursh * interestRate1
            monthlyPrincipal = SH_Prn_Org - SH_Int_Org
            SH_Balance_Org = SH_Balance_Org - SH_Prn_Org
            If j = amortizationTerm - 1 AndAlso SH_Balance_Org <> SH_Prn_Org Then
                ' Adjust the last payment to make sure the final balance is 0
                SH_Prn_Org += SH_Balance_Org
                SH_Balance_Org = 0
            End If
            ' Reset Date
            Dim day2 As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' ORDER BY SH_ID DESC")
            'Dim secondDay As String = getData("select top 1 LD_ID from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' and SH_Date <'" & day & "' ")
            If day2 = "" Then
                payDate = payDate
            Else
                If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                    payDate = payDate.AddDays(7)
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                    If day2 <> "" Then
                        Dim secondDay As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' and SH_Date <'" & day2 & "' ORDER BY SH_ID desc")
                        If secondDay <> "" Then
                            aD = DateDiff(DateInterval.Day, Convert.ToDateTime(secondDay), Convert.ToDateTime(day2))
                            If aD > 14 Then
                                payDate = payDate.AddDays(14 - (aD - 14))
                            Else
                                payDate = payDate.AddDays(14)
                            End If
                        Else
                            payDate = payDate.AddDays(14)
                        End If
                        'MessageBox.Show(secondDay.ToString() & "" & aD)
                    End If
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍ធ្វើការ" Then
                    If day2 <> "" Then
                        Dim secondDay As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "' and SH_Date <'" & day2 & "' ORDER BY SH_ID desc")
                        If secondDay <> "" Then
                            aD = DateDiff(DateInterval.Day, Convert.ToDateTime(secondDay), Convert.ToDateTime(day2))
                            If aD > 15 Then
                                payDate = payDate.AddDays(15 - (aD - 15))
                            Else
                                payDate = payDate.AddDays(15)
                            End If
                        Else
                            payDate = payDate.AddDays(15)
                        End If
                        'MessageBox.Show(secondDay.ToString() & "" & aD)
                    End If
                    'MessageBox.Show(secondDay)
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                    payDate = payDate.AddDays(30)
                Else
                    payDate = payDate.AddDays(1)
                End If
            End If
            payDate = Check_date(payDate, 1)
            cummulativeInterest += SH_Int_Org
            cummulativePrincipal += monthlyPrincipal
            no = no + 1
            If Me.DataGridView1.Rows(iRow).Cells("coCurrency").Value = "រៀល" Then
                SH_Prn_Amt = ReturnRound(SH_Prn_Org)
                SH_Int_Amt = ReturnRound(SH_Int_Org)
                SH_Ballance_Amt = ReturnRound(SH_Balance_Org)
            Else
                SH_Prn_Amt = SH_Prn_Org
                SH_Int_Amt = SH_Int_Org
                SH_Ballance_Amt = SH_Balance_Org
            End If
            Dim inter1 As Double = 0
            'Dim f As Double = 0
            'If Me.DataGridView1.Rows(iRow).Cells("coCurrency").Value = "រៀល" Then
            '    'inter1 = ReturnRound(SH_Balance_Org * interestRate1 * term)
            '    inter1 = SH_Int_Amt * (term - j)
            'Else
            '    inter1 = (SH_Balance_Org * interestRate1) * term
            'End If
            '---------------------------------------------------------------------------------
            payoff = 0
            addLoanSchedule(LD_ID, CM_ID, frmMain.lblCode.Text, payDate, SH_Prn_Org, SH_Int_Org, SH_Balance_Org, SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt, payoff, 1, frmMain.users.ToString, DateTime.Now(), LD_Service)
        Next
        Dim lastDate As Date = getData("select max(SH_Date) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
        Me.DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value = lastDate
        Me.DataGridView1.Rows(iRow).Cells(0).Value = "Saved"
    End Sub
    Private Sub addLoan(ByVal LD_ID As Integer, ByVal LD_BrId As String, ByVal CM_ID As Integer, ByVal LD_Dis_Date As Date, ByVal LD_First_Date As Date, ByVal LD_Mat_Date As Date, ByVal LD_Dis_Amt As Double, ByVal CU_ID As String, ByVal LD_ExRate As Double, ByVal LD_IntRate As Double, ByVal EM_ID As String, ByVal LD_Unit As String, ByVal LD_Type As String, ByVal LD_Term As Integer, ByVal LD_Status As String, ByVal LD_Rec_Status As Integer, ByVal LD_User_Create As String, ByVal LD_Date_Create As Date, ByVal IsExport As Integer, ByVal IsWriteoff As Integer, ByVal LD_ChargeRate As Double, ByVal LD_ChargeAmt As Double, ByVal Cycle As Integer, ByVal LD_InRate As Double, ByVal LD_InAmt As Double)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim LD_Service As Integer
       dim PayOff as Double=0
        Dim Ref as Double=0
        If Me.DataGridView1.Rows(iRow).Cells("coLD_Service").Value.ToString = "មាន" Then
            LD_Service = 1
        Else
            LD_Service = 0
        End If
        If Me.DataGridView1.Rows(iRow).Cells("coPayOff").Value.ToString = "" Then
            PayOff = 0
        Else
            PayOff = Me.DataGridView1.Rows(iRow).Cells("coPayOff").Value
        End If
        If Me.DataGridView1.Rows(iRow).Cells("coRef").Value.ToString = "" Then
            Ref = 0
        Else
            Ref = Me.DataGridView1.Rows(iRow).Cells("coRef").Value
        End If
 
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LD_ID
                .Add("@d1", SqlDbType.NVarChar).Value = LD_BrId
                .Add("@d2", SqlDbType.Int).Value = CM_ID
                .Add("@d3", SqlDbType.Date).Value = LD_Dis_Date
                .Add("@d4", SqlDbType.Date).Value = LD_First_Date
                .Add("@d5", SqlDbType.Date).Value = LD_Mat_Date
                .Add("@d6", SqlDbType.Float).Value = LD_Dis_Amt
                .Add("@d7", SqlDbType.NVarChar).Value = CU_ID
                .Add("@d8", SqlDbType.Float).Value = LD_ExRate
                .Add("@d9", SqlDbType.Float).Value = LD_IntRate
                .Add("@d10", SqlDbType.NVarChar).Value = EM_ID
                .Add("@d11", SqlDbType.NVarChar).Value = LD_Unit
                .Add("@d12", SqlDbType.NVarChar).Value = LD_Type
                .Add("@d13", SqlDbType.Int).Value = LD_Term
                .Add("@d14", SqlDbType.NVarChar).Value = LD_Status
                .Add("@d15", SqlDbType.Int).Value = LD_Rec_Status
                .Add("@d16", SqlDbType.NVarChar).Value = LD_User_Create
                .Add("@d17", SqlDbType.DateTime).Value = LD_Date_Create
                .Add("@d18", SqlDbType.Int).Value = IsExport
                .Add("@d19", SqlDbType.Int).Value = IsWriteoff
                .Add("@d20", SqlDbType.Float).Value = LD_ChargeRate
                .Add("@d21", SqlDbType.Float).Value = LD_ChargeAmt
                .Add("@d22", SqlDbType.DateTime).Value = DateTime.MaxValue.Date
                .Add("@d23", SqlDbType.Int).Value = Val(getData("select ID from BK_Customer where CM_ID='" & CM_ID & "' and Status='Active' and CM_BrId='" & frmMain.lblCode.Text & "'"))
                .Add("@d24", SqlDbType.Int).Value = Cycle
                .Add("@d25", SqlDbType.Int).Value = LD_Service
                .Add("@d26", SqlDbType.Float).Value = LD_InRate
                .Add("@d27", SqlDbType.Float).Value = LD_InAmt
                .Add("@d28", SqlDbType.Float).Value = PayOff
                .Add("@d29", SqlDbType.Float).Value = Ref
                If Me.DataGridView1.Rows(iRow).Cells("PID").Value.ToString = "" Then
                    .Add("@d30", SqlDbType.NVarChar).Value = ""
                Else
                    .Add("@d30", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells("PID").Value
                End If
            End With
            sql = "insert BK_Loan(LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_First_Date,LD_Mat_Date,LD_Dis_Amt,CU_ID,LD_ExRate,LD_IntRate,EM_ID,LD_Unit,LD_Type,LD_Term,LD_Status,LD_Rec_Status,LD_User_Create,LD_Date_Create,IsExport,IsWriteoff,LD_ChargeRate,LD_ChargeAmt,Date_Payoff,CM_ID1,LD_Cycle,LD_Service,LD_InRate,LD_InAmt,PayOff,Ref,PID) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23,@d24,@d25,@d26,@d27,@d28,@d29,@d30)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")

        End Try
    End Sub
    Private Sub addLoanSchedule(ByVal LD_ID As Integer, ByVal CM_ID As Integer, ByVal BrID As String, ByVal SH_Date As Date, ByVal SH_Prn_Org As Double, ByVal SH_Int_Org As Double, ByVal SH_Balance_Org As Double,
                                ByVal SH_Prn_Amt As Double, ByVal SH_Int_Amt As Double, ByVal SH_Balance As Double, ByVal SH_Payoff As Double, ByVal Rec_status As Integer, ByVal User_create As String, ByVal Date_Create As Date, ByVal LD_Service As Double)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LD_ID
                .Add("@d1", SqlDbType.Int).Value = CM_ID
                .Add("@d2", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d3", SqlDbType.Date).Value = FormatDateTime(SH_Date, DateFormat.ShortDate)
                .Add("@d4", SqlDbType.Float).Value = SH_Prn_Org
                .Add("@d5", SqlDbType.Float).Value = SH_Int_Org
                .Add("@d6", SqlDbType.Float).Value = SH_Balance_Org
                .Add("@d7", SqlDbType.Float).Value = SH_Prn_Amt
                .Add("@d8", SqlDbType.Float).Value = SH_Int_Amt
                .Add("@d9", SqlDbType.Float).Value = SH_Balance
                .Add("@d10", SqlDbType.Float).Value = SH_Payoff
                .Add("@d11", SqlDbType.Int).Value = Rec_status
                .Add("@d12", SqlDbType.NVarChar).Value = User_create
                .Add("@d13", SqlDbType.DateTime).Value = Date_Create
            End With
            sql = "insert into BK_LoanSchedule(LD_ID,CM_ID,SH_BrId,SH_Date,SH_Prn_Org,SH_Int_Org,SH_Balance_Org,SH_Prn_Amt,SH_Int_Amt,SH_Balance,SH_PayoffAmt,Rec_Status,User_Create,Date_Create,SH_Service) values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,'" & LD_Service & "')"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub addLoanSchedule1(ByVal LD_ID As Integer, ByVal CM_ID As Integer, ByVal BrID As String, ByVal SH_Date As Date, ByVal SH_Prn_Org As Double, ByVal SH_Int_Org As Double, ByVal SH_Balance_Org As Double,
                               ByVal SH_Prn_Amt As Double, ByVal SH_Int_Amt As Double, ByVal SH_Balance As Double, ByVal Rec_status As Integer, ByVal User_create As String, ByVal Date_Create As Date, ByVal LD_Service As Double)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LD_ID
                .Add("@d1", SqlDbType.Int).Value = CM_ID
                .Add("@d2", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d3", SqlDbType.Date).Value = FormatDateTime(SH_Date, DateFormat.ShortDate)
                .Add("@d4", SqlDbType.Float).Value = SH_Prn_Org
                .Add("@d5", SqlDbType.Float).Value = SH_Int_Org
                .Add("@d6", SqlDbType.Float).Value = SH_Balance_Org
                .Add("@d7", SqlDbType.Float).Value = SH_Prn_Amt
                .Add("@d8", SqlDbType.Float).Value = SH_Int_Amt
                .Add("@d9", SqlDbType.Float).Value = SH_Balance
                .Add("@d11", SqlDbType.Int).Value = Rec_status
                .Add("@d12", SqlDbType.NVarChar).Value = User_create
                .Add("@d13", SqlDbType.DateTime).Value = Date_Create
            End With
            sql = "insert into BK_LoanSchedule(LD_ID,CM_ID,SH_BrId,SH_Date,SH_Prn_Org,SH_Int_Org,SH_Balance_Org,SH_Prn_Amt,SH_Int_Amt,SH_Balance,Rec_Status,User_Create,Date_Create,SH_Service) values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d11,@d12,@d13,'" & LD_Service & "')"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub AddTrace_Debursh(ByVal RecordAction As String, ByVal LD_ID As Integer)
        Dim LD_ID1, CU_ID, LD_Unit, LD_Type, LD_Status, LD_User_Create, LD_User_Modify, LD_User_Delete, LD_BrId, EM_ID As String
        Dim CM_ID, LD_Term, IsExport, IsWriteoff, LD_Cycle, CM_ID1 As Integer
        Dim LD_Dis_Date, LD_First_Date, LD_Mat_Date, Date_Payoff As Date
        Dim LD_Dis_Amt, LD_Out_Amt, LD_ExRate, LD_IntRate, LD_Saving, LD_SavingAmt, LD_SavingRate, LD_ChargeRate, LD_ChargeAmt, LD_InRate, LD_InAmt, PayOff, Ref As Double
        Dim LD_Rec_Status, LD_Service As Boolean
        Dim DateAction, LD_Date_Create, LD_Date_Modify, LD_Date_Delete As DateTime
        Try
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select top 1 * from BK_Loan where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'"
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DateAction = DateTime.Now
            '--- RecordAction
            LD_ID1 = oDt.Rows(0).Item(0).ToString
            LD_BrId = oDt.Rows(0).Item(1).ToString
            CM_ID = oDt.Rows(0).Item(2).ToString
            LD_Dis_Date = oDt.Rows(0).Item(3).ToString
            LD_First_Date = oDt.Rows(0).Item(4).ToString
            LD_Mat_Date = oDt.Rows(0).Item(5).ToString
            LD_Dis_Amt = oDt.Rows(0).Item(6).ToString
            LD_Out_Amt = Val(oDt.Rows(0).Item(7).ToString)
            CU_ID = oDt.Rows(0).Item(8).ToString
            LD_ExRate = Val(oDt.Rows(0).Item(9).ToString)
            LD_IntRate = oDt.Rows(0).Item(10).ToString
            EM_ID = oDt.Rows(0).Item(11).ToString
            LD_Unit = oDt.Rows(0).Item(12).ToString
            LD_Type = oDt.Rows(0).Item(13).ToString
            LD_Term = oDt.Rows(0).Item(14).ToString
            LD_Status = oDt.Rows(0).Item(15).ToString
            LD_Rec_Status = oDt.Rows(0).Item(16).ToString
            LD_User_Create = oDt.Rows(0).Item(17).ToString
            LD_Date_Create = oDt.Rows(0).Item(18).ToString
            LD_User_Modify = oDt.Rows(0).Item(19).ToString
            If Format(oDt.Rows(0).Item(20).ToString, "") = "" Then
                LD_Date_Modify = DateTime.MaxValue.ToString
            Else
                LD_Date_Modify = oDt.Rows(0).Item(20).ToString
            End If
            'LD_Date_Modify = oDt.Rows(0).Item(20).ToString
            LD_User_Delete = frmMain.users
            LD_Date_Delete = DateTime.Now
            IsExport = oDt.Rows(0).Item(23).ToString
            IsWriteoff = oDt.Rows(0).Item(24).ToString
            LD_Cycle = Val(oDt.Rows(0).Item(25).ToString)
            LD_Saving = Val(oDt.Rows(0).Item(26).ToString)
            LD_SavingAmt = Val(oDt.Rows(0).Item(27).ToString)
            LD_SavingRate = Val(oDt.Rows(0).Item(28).ToString)
            LD_ChargeRate = Val(oDt.Rows(0).Item(29).ToString)
            LD_ChargeAmt = Val(oDt.Rows(0).Item(30).ToString)
            Date_Payoff = oDt.Rows(0).Item(31).ToString
            CM_ID1 = oDt.Rows(0).Item(32).ToString
            LD_Service = oDt.Rows(0).Item(33).ToString
            If oDt.Rows(0).Item(34).ToString = "" Then
                LD_InRate = 0
            Else
                LD_InRate = oDt.Rows(0).Item(34).ToString
            End If
            If oDt.Rows(0).Item(35).ToString = "" Then
                LD_InAmt = 0
            Else
                LD_InAmt = oDt.Rows(0).Item(35).ToString
            End If
            PayOff = oDt.Rows(0).Item(36).ToString
            Ref = oDt.Rows(0).Item(37).ToString
            If RecordAction = "DELETE" Then
                If LD_Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Loan(DateAction,RecordAction,LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_First_Date,LD_Mat_Date,LD_Dis_Amt,LD_Out_Amt,CU_ID,LD_ExRate,LD_IntRate,EM_ID,LD_Unit,LD_Type,LD_Term,LD_Status,LD_Rec_Status,LD_User_Create,LD_Date_Create,LD_User_Delete,LD_Date_Delete,IsExport,IsWriteoff,LD_Cycle,LD_Saving,LD_SavingAmt,LD_SavingRate,LD_ChargeRate,LD_ChargeAmt,Date_Payoff,CM_ID1,LD_Service,LD_InRate,LD_InAmt,PayOff,Ref) Values('" & DateAction & "','" & RecordAction & "','" & LD_ID & "','" & LD_BrId & "','" & CM_ID & "','" & LD_Dis_Date & "','" & LD_First_Date & "',N'" & LD_Mat_Date & "',N'" & LD_Dis_Amt & "','" & LD_Out_Amt & "',N'" & CU_ID & "','" & LD_ExRate & "','" & LD_IntRate & "','" & EM_ID & "',N'" & LD_Unit & "',N'" & LD_Type & "' ,'" & LD_Term & "','" & LD_Status & "','" & LD_Rec_Status & "','" & LD_User_Create & "','" & LD_Date_Create & "','" & LD_User_Delete & "','" & LD_Date_Delete & "','" & IsExport & "','" & IsWriteoff & "','" & LD_Cycle & "','" & LD_Saving & "','" & LD_SavingAmt & "','" & LD_SavingRate & "','" & LD_ChargeRate & "','" & LD_ChargeAmt & "','" & Date_Payoff & "','" & CM_ID1 & "','" & LD_Service & "','" & LD_InRate & "','" & LD_InAmt & "','" & PayOff & "','" & Ref & "')")
                Else
                    addIn("insert TRACE_Loan(DateAction,RecordAction,LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_First_Date,LD_Mat_Date,LD_Dis_Amt,LD_Out_Amt,CU_ID,LD_ExRate,LD_IntRate,EM_ID,LD_Unit,LD_Type,LD_Term,LD_Status,LD_Rec_Status,LD_User_Create,LD_Date_Create,LD_User_Modify,LD_Date_Modify,LD_User_Delete,LD_Date_Delete,IsExport,IsWriteoff,LD_Cycle,LD_Saving,LD_SavingAmt,LD_SavingRate,LD_ChargeRate,LD_ChargeAmt,Date_Payoff,CM_ID1,LD_Service,LD_InRate,LD_InAmt,PayOff,Ref) Values('" & DateAction & "','" & RecordAction & "','" & LD_ID & "','" & LD_BrId & "','" & CM_ID & "','" & LD_Dis_Date & "','" & LD_First_Date & "',N'" & LD_Mat_Date & "',N'" & LD_Dis_Amt & "','" & LD_Out_Amt & "',N'" & CU_ID & "','" & LD_ExRate & "','" & LD_IntRate & "','" & EM_ID & "',N'" & LD_Unit & "',N'" & LD_Type & "' ,'" & LD_Term & "','" & LD_Status & "','" & LD_Rec_Status & "','" & LD_User_Create & "','" & LD_Date_Create & "','" & LD_User_Modify & "','" & LD_Date_Modify & "','" & LD_User_Delete & "','" & LD_Date_Delete & "','" & IsExport & "','" & IsWriteoff & "','" & LD_Cycle & "','" & LD_Saving & "','" & LD_SavingAmt & "','" & LD_SavingRate & "','" & LD_ChargeRate & "','" & LD_ChargeAmt & "','" & Date_Payoff & "','" & CM_ID1 & "','" & LD_Service & "','" & LD_InRate & "','" & LD_InAmt & "','" & PayOff & "','" & Ref & "')")
                End If
            Else
                If LD_Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Loan(DateAction,RecordAction,LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_First_Date,LD_Mat_Date,LD_Dis_Amt,LD_Out_Amt,CU_ID,LD_ExRate,LD_IntRate,EM_ID,LD_Unit,LD_Type,LD_Term,LD_Status,LD_Rec_Status,LD_User_Create,LD_Date_Create,IsExport,IsWriteoff,LD_Cycle,LD_Saving,LD_SavingAmt,LD_SavingRate,LD_ChargeRate,LD_ChargeAmt,Date_Payoff,CM_ID1,LD_Service,LD_InRate,LD_InAmt,PayOff,Ref) Values('" & DateAction & "','" & RecordAction & "','" & LD_ID & "','" & LD_BrId & "','" & CM_ID & "','" & LD_Dis_Date & "','" & LD_First_Date & "',N'" & LD_Mat_Date & "',N'" & LD_Dis_Amt & "','" & LD_Out_Amt & "',N'" & CU_ID & "','" & LD_ExRate & "','" & LD_IntRate & "','" & EM_ID & "',N'" & LD_Unit & "',N'" & LD_Type & "' ,'" & LD_Term & "','" & LD_Status & "','" & LD_Rec_Status & "','" & LD_User_Create & "','" & LD_Date_Create & "','" & IsExport & "','" & IsWriteoff & "','" & LD_Cycle & "','" & LD_Saving & "','" & LD_SavingAmt & "','" & LD_SavingRate & "','" & LD_ChargeRate & "','" & LD_ChargeAmt & "','" & Date_Payoff & "','" & CM_ID1 & "','" & LD_Service & "','" & LD_InRate & "','" & LD_InAmt & "','" & PayOff & "','" & Ref & "')")
                Else
                    addIn("insert TRACE_Loan(DateAction,RecordAction,LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_First_Date,LD_Mat_Date,LD_Dis_Amt,LD_Out_Amt,CU_ID,LD_ExRate,LD_IntRate,EM_ID,LD_Unit,LD_Type,LD_Term,LD_Status,LD_Rec_Status,LD_User_Create,LD_Date_Create,LD_User_Modify,LD_Date_Modify,IsExport,IsWriteoff,LD_Cycle,LD_Saving,LD_SavingAmt,LD_SavingRate,LD_ChargeRate,LD_ChargeAmt,Date_Payoff,CM_ID1,LD_Service,LD_InRate,LD_InAmt,PayOff,Ref) Values('" & DateAction & "','" & RecordAction & "','" & LD_ID & "','" & LD_BrId & "','" & CM_ID & "','" & LD_Dis_Date & "','" & LD_First_Date & "',N'" & LD_Mat_Date & "',N'" & LD_Dis_Amt & "','" & LD_Out_Amt & "',N'" & CU_ID & "','" & LD_ExRate & "','" & LD_IntRate & "','" & EM_ID & "',N'" & LD_Unit & "',N'" & LD_Type & "' ,'" & LD_Term & "','" & LD_Status & "','" & LD_Rec_Status & "','" & LD_User_Create & "','" & LD_Date_Create & "','" & LD_User_Modify & "','" & LD_Date_Modify & "','" & IsExport & "','" & IsWriteoff & "','" & LD_Cycle & "','" & LD_Saving & "','" & LD_SavingAmt & "','" & LD_SavingRate & "','" & LD_ChargeRate & "','" & LD_ChargeAmt & "','" & Date_Payoff & "','" & CM_ID1 & "','" & LD_Service & "','" & LD_InRate & "','" & LD_InAmt & "','" & PayOff & "','" & Ref & "')")
                End If
            End If
            oDa.Dispose()
            oDt.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select top 1 CM_ID,EM_ID,isnull(LD_ChargeRate,0)LD_ChargeRate,isnull(LD_ChargeAmt,0)LD_ChargeAmt,isnull(LD_InRate,0)LD_InRate,isnull(LD_InAmt,0)LD_InAmt,PayOff,Ref,PID from BK_Loan where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim CM_ID As Integer = oDt.Rows(0).Item(0).ToString
        Dim EM_ID As Integer = oDt.Rows(0).Item(1).ToString
        Dim LD_ChargeRate As Double = oDt.Rows(0).Item(2).ToString
        Dim LD_ChargeAmt As Double = oDt.Rows(0).Item(3).ToString
        Dim LD_InRate As Double = oDt.Rows(0).Item(4).ToString
        Dim LD_InAmt As Double = oDt.Rows(0).Item(5).ToString
        Dim payoff As Double = oDt.Rows(0).Item(6).ToString
        Dim Ref As Double = oDt.Rows(0).Item(7).ToString
        dim PID as String=oDt.Rows(0).Item(8).ToString
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            If CM_ID = .Cells("coCM_ID").Value And EM_ID = .Cells("coEM_ID").Value And LD_ChargeRate = .Cells("coCharge_Rate").Value And LD_ChargeAmt = .Cells("coCharge_Amt").Value And .Cells("coInsurance").Value = LD_InRate And .Cells("coInsuranceTotal").Value = LD_InAmt And .Cells("coPayOff").Value = payoff And .Cells("coRef").Value = Ref And .Cells("PID").Value = PID Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Public Sub callLast()
        Dim a As String = ""
        a = getData("select max(cast(isnull(LD_ID,0) as int)) from BK_Loan where LD_BrId='" & frmMain.lblCode.Text & "'")
        Dim b As String = ""
        b = getData("select max(cast(isnull(CM_ID,0) as int)) from BK_Customer where CM_BrId='" & frmMain.lblCode.Text & "'")
        If a = "" Then
            a = "0"
        End If
        If b = "" Then
            b = "0"
        End If
        lblCustomerID.Text = b
        lblLoanID.Text = a
    End Sub
    Public Sub toExcel1(ByVal LD_ID As String, ByVal index As Integer)
        Dim iRow = index
        'Dim cnn As SqlConnection
        Dim connectionString As String = Nothing
        Dim sql As String = Nothing
        Dim data As String = Nothing
        Dim i As Integer = 0
        Dim j As Integer = 0
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        'Dim xlWorkBook As Excel.Workbook
        'Dim xlWorkSheet As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        xlApp = New Excel.Application()
        'xlWorkBook = xlApp.Workbooks.Add(misValue)
        ''-----------------------------------------------------------------------------
        Dim Str As String = "select a.LD_ID,a.CM_ID,b.CM_KhName,c.VL_ID+','+c.CN_ID+','+c.DT_ID+','+c.PV_ID 'CM_Address',a.LD_Unit,LD_Type,convert( varchar(12),LD_Dis_Date,101)LD_Dis_Date,convert( varchar(12),LD_First_Date,101)LD_First_Date,LD_Term,LD_Dis_Amt,LD_IntRate,case when a.CU_ID=1 then N'រៀល' else N'ដុល្លារ' end CU_ID from BK_Loan a inner join BK_Customer b on a.CM_ID1=b.ID and LD_BrId=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrID where LD_ID ='" & LD_ID & "' and LD_BrId ='" & frmMain.lblCode.Text & "' "
        Dim oDt As New System.Data.DataTable
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim CM_ID As String = oDt.Rows(0).Item(1).ToString
        Dim CM_Name As String = oDt.Rows(0).Item(2).ToString
        Dim CM_Address As String = oDt.Rows(0).Item(3).ToString
        Dim LD_Unit As String = oDt.Rows(0).Item(4).ToString
        Dim LD_Type As String = oDt.Rows(0).Item(5).ToString
        Dim val6 As String = oDt.Rows(0).Item(6).ToString
        Dim val7 As String = oDt.Rows(0).Item(7).ToString
        Dim val8 As String = oDt.Rows(0).Item(8).ToString
        Dim val9 As String = oDt.Rows(0).Item(9).ToString
        Dim val10 As String = oDt.Rows(0).Item(10).ToString
        Dim val11 As String = oDt.Rows(0).Item(11).ToString
        oDa.Dispose()
        oDt.Dispose()
        '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
        Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\sample\Schedule-for-Tax.xlsx", False, True)
        Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
        xlApp.Visible = True
        'cnn = New SqlConnection(connectionString1)
        'cnn.Open()
        sql = "select convert( varchar(12),SH_Date,101) SH_Date,case  when DATENAME(WEEKDAY,SH_Date)=N'Monday'then 1 when DATENAME(WEEKDAY,SH_Date)=N'Tuesday' then 2 when DATENAME(WEEKDAY,SH_Date)=N'Wednesday' then 3 when DATENAME(WEEKDAY,SH_Date)=N'Thursday' then 4 when DATENAME(WEEKDAY,SH_Date)=N'Friday' then 5 else 0 end'Day''Day',SH_Prn_Amt+SH_Int_Amt+isnull(SH_Service,0),SH_Prn_Amt,SH_Int_Amt,SH_Balance from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'"
        Dim count As Integer = getData("select COUNT(LD_ID) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
        Dim dscmd As New SqlDataAdapter(sql, g_cnn)
        Dim ds As New DataSet()
        dscmd.Fill(ds)
        With excelWorksheet
            .Range("A8:A" & count + 4).EntireRow.Insert()
            '.Range("C2").Value = LD_ID
            '.Range("C3").Value = val1
            .Range("B3").Value = CM_Name
            .Range("B4").Value = CM_Address
            '.Range("E2").Value = val4
            '.Range("E3").Value = val5
            .Range("G5").Value = val6
            .Range("G4").Value = val7
            '.Range("G2").Value = val8
            .Range("G3").Value = val9
            '.Range("G4").Value = val10
            .Range("H3").Value = val11
            For i = 0 To ds.Tables(0).Rows.Count - 1
                For j = 0 To ds.Tables(0).Columns.Count - 1
                    data = ds.Tables(0).Rows(i).ItemArray(j).ToString()
                    .Cells(i + 7, j + 2) = data
                    .Cells(i + 7, 1) = i + 1
                Next
                If .Cells(i + 7, 3).value = 1 Then
                    .Cells(i + 7, 3) = "ច័ន្ទ"
                ElseIf .Cells(i + 7, 3).value = 2 Then
                    .Cells(i + 7, 3) = "អង្គារ"
                ElseIf .Cells(i + 7, 3).value = 3 Then
                    .Cells(i + 7, 3) = "ពុធ"
                ElseIf .Cells(i + 7, 3).value = 4 Then
                    .Cells(i + 7, 3) = "ព្រហស្បតិ៍"
                ElseIf .Cells(i + 7, 3).value = 5 Then
                    .Cells(i + 7, 3) = "សុក្រ"
                End If
            Next
        End With
    End Sub
    Public Sub toExcel(ByVal LD_ID As String, ByVal index As Integer)
        Dim iRow = index
        'Dim cnn As SqlConnection
        Dim connectionString As String = Nothing
        Dim sql As String = Nothing
        Dim data As String = Nothing
        Dim i As Integer = 0
        Dim j As Integer = 0
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        'Dim xlWorkBook As Excel.Workbook
        'Dim xlWorkSheet As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        xlApp = New Excel.Application()
        'xlWorkBook = xlApp.Workbooks.Add(misValue)
        ''-----------------------------------------------------------------------------
        Dim Str As String = "select a.LD_ID,a.CM_ID,b.CM_KhName,c.VL_ID+','+c.CN_ID+','+c.DT_ID+','+c.PV_ID 'CM_Address',a.LD_Unit,LD_Type,convert( varchar(12),LD_Dis_Date,101)LD_Dis_Date,convert( varchar(12),LD_First_Date,101)LD_First_Date,LD_Term,LD_Dis_Amt,LD_IntRate,case when a.CU_ID=1 then N'រៀល' else N'ដុល្លារ' end CU_ID,b.LD_Cycle,isnull(d.Kh_Name,0)Kh_Name from BK_Loan a inner join BK_Customer b on a.CM_ID1=b.ID and LD_BrId=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrID left join BK_Product d on a.PID=d.PID where LD_ID ='" & LD_ID & "' and LD_BrId ='" & frmMain.lblCode.Text & "' "
        Dim oDt As New System.Data.DataTable
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim val1 As String = oDt.Rows(0).Item(1).ToString
        Dim val2 As String = oDt.Rows(0).Item(2).ToString
        Dim val3 As String = oDt.Rows(0).Item(3).ToString
        Dim val4 As String = oDt.Rows(0).Item(4).ToString
        Dim val5 As String = oDt.Rows(0).Item(5).ToString
        Dim val6 As String = oDt.Rows(0).Item(6).ToString
        Dim val7 As String = oDt.Rows(0).Item(7).ToString
        Dim val8 As String = oDt.Rows(0).Item(8).ToString
        Dim val9 As String = oDt.Rows(0).Item(9).ToString
        Dim val10 As String = oDt.Rows(0).Item(10).ToString
        Dim val11 As String = oDt.Rows(0).Item(11).ToString
        Dim LD_Cycle As String = oDt.Rows(0).Item(12).ToString
        Dim P_Name As String = oDt.Rows(0).Item(13).ToString
        oDa.Dispose()
        oDt.Dispose()
        '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
        Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\sample\Schedule.xls", False, True)
        Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
        xlApp.Visible = True
        'cnn = New SqlConnection(connectionString1)
        'cnn.Open()
        sql = "select convert( varchar(12),SH_Date,101) SH_Date,case  when DATENAME(WEEKDAY,SH_Date)=N'Monday'then 1 when DATENAME(WEEKDAY,SH_Date)=N'Tuesday' then 2 when DATENAME(WEEKDAY,SH_Date)=N'Wednesday' then 3 when DATENAME(WEEKDAY,SH_Date)=N'Thursday' then 4 when DATENAME(WEEKDAY,SH_Date)=N'Friday' then 5 else 0 end'Day''Day',SH_Prn_Amt+SH_Int_Amt+isnull(SH_Service,0),SH_Prn_Amt,SH_Int_Amt,isnull(SH_Service,0),SH_Balance from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'"
        Dim count As Integer = getData("select COUNT(LD_ID) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
        Dim dscmd As New SqlDataAdapter(sql, g_cnn)
        Dim ds As New DataSet()
        dscmd.Fill(ds)
        With excelWorksheet
            .Range("A8:A" & count + 4).EntireRow.Insert()
            .Range("C2").Value = LD_ID
            .Range("C3").Value = val1
            .Range("B4").Value = val2
            .Range("B5").Value = val3
            .Range("E2").Value = val4
            .Range("E3").Value = val5
            .Range("E4").Value = val6
            .Range("E5").Value = val7
            .Range("G2").Value = val8
            .Range("G3").Value = val9
            .Range("G4").Value = val10
            .Range("G5").Value = val11
            .Range("H2").Value = "ជំហ៊ានទី: " & LD_Cycle
            If P_Name=""
                Else 
                    .Range("H3").Value = P_Name
            End If

            For i = 0 To ds.Tables(0).Rows.Count - 1
                For j = 0 To ds.Tables(0).Columns.Count - 1
                    data = ds.Tables(0).Rows(i).ItemArray(j).ToString()
                    .Cells(i + 7, j + 2) = data
                    .Cells(i + 7, 1) = i + 1
                Next
                If .Cells(i + 7, 3).value = 1 Then
                    .Cells(i + 7, 3) = "ច័ន្ទ"
                ElseIf .Cells(i + 7, 3).value = 2 Then
                    .Cells(i + 7, 3) = "អង្គារ"
                ElseIf .Cells(i + 7, 3).value = 3 Then
                    .Cells(i + 7, 3) = "ពុធ"
                ElseIf .Cells(i + 7, 3).value = 4 Then
                    .Cells(i + 7, 3) = "ព្រហស្បតិ៍"
                ElseIf .Cells(i + 7, 3).value = 5 Then
                    .Cells(i + 7, 3) = "សុក្រ"
                End If
            Next
        End With
    End Sub
    Private Sub updateLoan(ByVal CM_ID As Integer, ByVal EM_ID As String, ByVal LD_ChargeRate As Double, ByVal LD_ChargeAmt As Double, ByVal LD_InRate As Double, ByVal LD_InAmt As Double, ByVal PayOff As Double, ByVal Ref As Double,ByVal PID As String)
        If Me.DataGridView1.Rows.Count = 0 Then
            resultError = frmMessageError.ShowBoxError("គ្មានទិន្នន័យត្រូវកែរប្រែ។", "គ្មានទិន្នន័យ")
            Return
        End If
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = CM_ID
                .Add("@d1", SqlDbType.NVarChar).Value = EM_ID
                .Add("@d2", SqlDbType.Float).Value = LD_ChargeRate
                .Add("@d3", SqlDbType.Float).Value = LD_ChargeAmt
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d5", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d6", SqlDbType.Int).Value = getData("select top 1 ID from BK_Customer where CM_ID='" & CM_ID & "' and CM_BrId='" & frmMain.lblCode.Text & "' and Status='Active'")
                .Add("@d7", SqlDbType.Float).Value = LD_InRate
                .Add("@d8", SqlDbType.Float).Value = LD_InAmt
                .Add("@d9", SqlDbType.Float).Value = PayOff
                .Add("@d10", SqlDbType.Float).Value = Ref
                .Add("@d11", SqlDbType.NVarChar).Value = PID
            End With
            sql = "update BK_Loan set CM_ID1=@d6,CM_ID=@d0,EM_ID=@d1,LD_ChargeRate=@d2,LD_ChargeAmt=@d3,LD_User_Modify=@d4,LD_Date_Modify=@d5,LD_InRate=@d7,LD_InAmt=@d8,PayOff=@d9,Ref=@d10,PID=@d11 where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As SystemException
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub CalculateLoanType3(ByVal Disbursh As Integer, ByVal interestRate As Double, ByVal term As Integer, ByVal datefirstpay As DateTime, ByVal LD_Service As Double)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim LD_ID As Integer = DataGridView1.Rows(iRow).Cells("coLD_ID").Value.ToString
        Dim CM_ID As Integer = DataGridView1.Rows(iRow).Cells("coCM_ID").Value.ToString
        Dim SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt As Double
        datefirstpay = Check_date(datefirstpay, 1)
        Dim payDate As DateTime = datefirstpay
        Dim interestRate1 As Double = 0
        Dim SH_Int_Org As Double = 0
        Dim loanAmount As Double
        Dim amortizationTerm As Integer = 0
        Dim no As Integer = 0
        Dim SH_Balance_Org As Double
        Dim cummulativeInterest As Double = 0
        Dim monthlyPrincipal As Double = 0
        Dim cummulativePrincipal As Double = 0
        Dim payoff As Double = 0
        Dim amountday As Integer = 0
        'int i = 0;
        loanAmount = Disbursh
        SH_Balance_Org = Disbursh
        interestRate1 = interestRate * 0.01
        amortizationTerm = term
        ' Calculate the monthly payment and round it to 2 decimal places         
        Dim SH_Prn_Org = loanAmount / amortizationTerm
        SH_Prn_Org = Math.Round(SH_Prn_Org, 2)
        ' Loop for amortization term (number of monthly payments)
        For j As Integer = 0 To amortizationTerm - 1
            ' Calculate monthly cycle
            SH_Int_Org = Disbursh * interestRate1
            monthlyPrincipal = SH_Prn_Org - SH_Int_Org
            SH_Balance_Org = SH_Balance_Org - SH_Prn_Org
            If j = amortizationTerm - 1 AndAlso SH_Balance_Org <> SH_Prn_Org Then
                ' Adjust the last payment to make sure the final balance is 0
                SH_Prn_Org += SH_Balance_Org
                SH_Balance_Org = 0
            End If
            ' Reset Date
            Dim day As String = getData("select top 1 LD_ID from BK_LoanSchedule where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
            If day = "" Then
                payDate = payDate
            Else
                If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                    payDate = payDate.AddDays(7)
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                    payDate = payDate.AddDays(14)
                ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                    payDate = payDate.AddDays(30)
                Else
                    payDate = payDate.AddDays(1)
                End If
            End If
            If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                payDate = Check_date1(payDate, 1)
            Else
                payDate = Check_date(payDate, 1)
            End If
            payDate = Check_date(payDate, 1)
            cummulativeInterest += SH_Int_Org
            cummulativePrincipal += monthlyPrincipal
            no = no + 1
            '---------------------------------------------------------------- សំរាប់ការបង់ចុះ
            If Me.DataGridView1.Rows(iRow).Cells("coType").Value = "ចុះ" Then
                Dim ID As String = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID ")
                If ID = "" Then
                    amountday = DateDiff(DateInterval.Day, Me.DataGridView1.Rows(iRow).Cells("coDisDate").Value, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        Dim IntDay As Double = SH_Int_Org / 7
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                        Dim IntDay As Double = SH_Int_Org / 14
                        SH_Int_Org = IntDay * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        Dim IntDay As Double = SH_Int_Org / 30
                        SH_Int_Org = IntDay * amountday
                    Else
                        Dim IntDay As Double = SH_Int_Org / 1
                        SH_Int_Org = IntDay * amountday
                    End If
                Else
                    Dim ID1 As Date = getData("select top 1 SH_Date from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc ")
                    Dim Balance As Double = Val(getData("select top 1 SH_Balance_Org from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "' order by SH_ID desc "))
                    Dim int As Double = Balance * (interestRate * 0.01)
                    amountday = DateDiff(DateInterval.Day, ID1, payDate)
                    If Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "សប្តាហ៍" Then
                        SH_Int_Org = (int / 7) * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "២សប្តាហ៍" Then
                        SH_Int_Org = (int / 14) * amountday
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("coUnit").Value = "ខែ" Then
                        SH_Int_Org = (int / 30) * amountday
                    Else
                        SH_Int_Org = (int / 1) * amountday
                    End If
                End If
            End If
            '---------------------------------------------------------------- Round up if Riel
            If Me.DataGridView1.Rows(iRow).Cells("coCurrency").Value = "រៀល" Then
                SH_Prn_Amt = ReturnRound(SH_Prn_Org)
                SH_Int_Amt = ReturnRound(SH_Int_Org)
                SH_Ballance_Amt = ReturnRound(SH_Balance_Org)
            Else
                SH_Prn_Amt = SH_Prn_Org
                SH_Int_Amt = SH_Int_Org
                SH_Ballance_Amt = SH_Balance_Org
            End If
            addLoanSchedule1(LD_ID, CM_ID, frmMain.lblCode.Text, payDate, SH_Prn_Org, SH_Int_Org, SH_Balance_Org, SH_Prn_Amt, SH_Int_Amt, SH_Ballance_Amt, 1, frmMain.users.ToString, DateTime.Now(), LD_Service)
        Next
        Dim lastDate As Date = getData("select max(SH_Date) from BK_LoanSchedule where LD_ID='" & LD_ID & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
        Me.DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value = lastDate
        Me.DataGridView1.Rows(iRow).Cells(0).Value = "Saved"
    End Sub
End Class