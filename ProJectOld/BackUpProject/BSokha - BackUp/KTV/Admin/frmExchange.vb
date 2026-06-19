Imports System.Data.SqlClient

Public Class frmExchange
    Private Sub frmExchange_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.Text = "USDTOKHR" Then
            Me.Label2.Text = "ដកប្រាក់ដុល្លារទិញរៀល"
            SetFontDatagrid(DataGridView1)
            DataGridView1.Rows.Add()
            Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
            With DataGridView1.Rows(iRow)
                .Cells(0).Style.BackColor = Color.Yellow
                .Cells(0).Value = "Editing"
                .Cells(0).ReadOnly = True
                .Cells(7).Style.BackColor = Color.Yellow
                .Cells(7).ReadOnly = True
                .Cells(6).Style.BackColor = Color.Yellow
                .Cells(6).ReadOnly = True
                .Cells(6).Value = "USDTOKHR"
                .Cells(5).Style.BackColor = Color.Yellow
                .Cells(5).ReadOnly = True
                DataGridView1.CurrentCell = DataGridView1(1, iRow)
            End With
        Else
            Me.Label2.Text = "ដកប្រាក់រៀលទិញដុល្លារ"
            SetFontDatagrid(DataGridView1)
            DataGridView1.Rows.Add()
            Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
            With DataGridView1.Rows(iRow)
                .Cells(0).Style.BackColor = Color.Yellow
                .Cells(0).Value = "Editing"
                .Cells(0).ReadOnly = True
                .Cells(4).Style.BackColor = Color.Yellow
                .Cells(4).ReadOnly = True
                .Cells(7).Style.BackColor = Color.Yellow
                .Cells(7).ReadOnly = True
                .Cells(6).Value = "KHRTOUSD"
                .Cells(6).Style.BackColor = Color.Yellow
                .Cells(6).ReadOnly = True
                DataGridView1.CurrentCell = DataGridView1(1, iRow)
            End With
        End If
        CallBalance()
    End Sub
    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        If DataGridView1.CurrentCell.ColumnIndex = 2 Then
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
                DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError("ការបញ្ចូលថ្ងៃខែមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                DataGridView1.Rows(iRow).Cells(2).Value = ""
                Return
            End Try
        ElseIf DataGridView1.CurrentCell.ColumnIndex = 1 Then
            Try
                DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
            Catch ex As Exception
                Return
            End Try

        ElseIf DataGridView1.CurrentCell.ColumnIndex = 3 Then
            Try
                If DataGridView1.Rows(iRow).Cells(3).Value = "" Or DataGridView1.Rows(iRow).Cells(3).Value = "0" Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលមិនអាចសូន្យ រឺ អក្សរបានឡើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                    Me.DataGridView1.Rows(iRow).Cells(3).Value = ""
                    Return
                ElseIf (DataGridView1.Rows(iRow).Cells(3).Value) / 1 = DataGridView1.Rows(iRow).Cells(3).Value Then
                    If Me.Text = "USDTOKHR" Then
                        DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                        Me.DataGridView1.Rows(iRow).Cells(5).Value = Val(Me.DataGridView1.Rows(iRow).Cells(4).Value) * Val(Me.DataGridView1.Rows(iRow).Cells(3).Value)
                    Else
                        DataGridView1.CurrentCell = DataGridView1(iCol + 2, iRow)
                        Me.DataGridView1.Rows(iRow).Cells(4).Value = Val(Me.DataGridView1.Rows(iRow).Cells(5).Value) / Val(Me.DataGridView1.Rows(iRow).Cells(3).Value)
                    End If
                    Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells(3).Value
                    Me.DataGridView1.Rows(iRow).Cells(3).Value = Format(as1, "###,###.##")
                Else
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនអត្រាប្តូរប្រាក់មិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                    DataGridView1.Rows(iRow).Cells(3).Value = ""
                    Return
                End If
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនអត្រាប្តូរប្រាក់មិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                DataGridView1.Rows(iRow).Cells(3).Value = ""
                Return
            End Try
        ElseIf DataGridView1.CurrentCell.ColumnIndex = 4 Then
            Try
                If DataGridView1.Rows(iRow).Cells(4).Value = "" Or DataGridView1.Rows(iRow).Cells(4).Value = "0" Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលមិនអាចសូន្យ រឺ អក្សរបានឡើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                    Me.DataGridView1.Rows(iRow).Cells(4).Value = ""
                    Return
                ElseIf (DataGridView1.Rows(iRow).Cells(4).Value) / 1 = DataGridView1.Rows(iRow).Cells(4).Value Then
                    If Me.DataGridView1.Rows(iRow).Cells(3).Value Is Nothing Then
                        resultError = frmMessageError.ShowBoxError("សូមបញ្ចូលអត្រាប្តូរប្រាក់ជាមុនសិន សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                        Me.DataGridView1.Rows(iRow).Cells(4).Value = ""
                        DataGridView1.CurrentCell = DataGridView1(iCol - 1, iRow)
                    Else
                        Dim as11 As Integer = Convert.ToDecimal(Me.DataGridView1.Rows(iRow).Cells(4).Value) - Convert.ToDecimal(Label3.Text)
                        If as11 > 0 Then
                            resultError = frmMessageError.ShowBoxError("ការដកប្រាក់មិនអាចលើសពីប្រាក់នៅសល់បានឡើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                            Me.DataGridView1.Rows(iRow).Cells(4).Value = 0
                            Me.DataGridView1.Rows(iRow).Cells(5).Value = 0
                            Return
                        End If
                        DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                        Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells(4).Value
                        Me.DataGridView1.Rows(iRow).Cells(4).Value = Format(as1, "###,###.##")
                        Me.DataGridView1.Rows(iRow).Cells(5).Value = as1 * Me.DataGridView1.Rows(iRow).Cells(3).Value
                        Dim as2 As Double = Me.DataGridView1.Rows(iRow).Cells(3).Value
                        Me.DataGridView1.Rows(iRow).Cells(3).Value = Format(as2, "###,###.##")
                        Dim as21 As Double = Me.DataGridView1.Rows(iRow).Cells(5).Value
                        Me.DataGridView1.Rows(iRow).Cells(5).Value = Format(as21, "###,###.##")
                    End If
                Else
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលមិនអាចសូន្យ រឺ អក្សរបានឡើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                    DataGridView1.Rows(iRow).Cells(4).Value = ""
                    Return
                End If
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError("ការបញ្ចូលមិនអាចសូន្យ រឺ អក្សរបានឡើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលខុស")
                DataGridView1.Rows(iRow).Cells(4).Value = ""
                Return
            End Try
        ElseIf DataGridView1.CurrentCell.ColumnIndex = 5 Then
            Try
                If DataGridView1.Rows(iRow).Cells(5).Value = "" Or DataGridView1.Rows(iRow).Cells(5).Value = "0" Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលមិនអាចសូន្យ រឺ អក្សរបានឡើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលខុស")
                    Return
                ElseIf (DataGridView1.Rows(iRow).Cells(5).Value) / 1 = DataGridView1.Rows(iRow).Cells(5).Value Then
                    If Me.DataGridView1.Rows(iRow).Cells(3).Value Is Nothing Then
                        resultError = frmMessageError.ShowBoxError("សូមបញ្ចូលអត្រាប្តូរប្រាក់ជាមុនសិន សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលខុស")
                        Me.DataGridView1.Rows(iRow).Cells(5).Value = ""
                        DataGridView1.CurrentCell = DataGridView1(iCol - 2, iRow)
                    Else
                        Dim as11 As Integer = Convert.ToDecimal(Me.DataGridView1.Rows(iRow).Cells(5).Value) - Convert.ToDecimal(Label4.Text)
                        If as11 > 0 Then
                            resultError = frmMessageError.ShowBoxError("ការដកប្រាក់មិនអាចលើសពីប្រាក់នៅសល់បានឡើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលខុស")
                            Me.DataGridView1.Rows(iRow).Cells(5).Value = 0
                            Me.DataGridView1.Rows(iRow).Cells(4).Value = 0
                            Return
                        End If
                        DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                        Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells(5).Value
                        Me.DataGridView1.Rows(iRow).Cells(5).Value = Format(as1, "###,###.##")
                        Me.DataGridView1.Rows(iRow).Cells(4).Value = as1 / Me.DataGridView1.Rows(iRow).Cells(3).Value
                        Dim as2 As Double = Me.DataGridView1.Rows(iRow).Cells(3).Value
                        Me.DataGridView1.Rows(iRow).Cells(3).Value = Format(as2, "###,###.##")
                        Dim as21 As Double = Me.DataGridView1.Rows(iRow).Cells(4).Value
                        Me.DataGridView1.Rows(iRow).Cells(4).Value = Format(as21, "###,###.##")
                    End If  
                Else
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលមិនអាចសូន្យ រឺ អក្សរបានឡើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលខុស")
                    DataGridView1.Rows(iRow).Cells(5).Value = ""
                    Return
                End If
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError("ការបញ្ចូលមិនអាចសូន្យ រឺ អក្សរបានឡើយ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលខុស")
                DataGridView1.Rows(iRow).Cells(5).Value = ""
                Return
            End Try
        End If
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Try
            If e.KeyCode = Keys.F12 Then
                If Me.DataGridView1.Rows.Count = 0 Then
                    newRow()
                End If
                Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
                If Me.DataGridView1.Rows(iRow).Cells(7).Value = "" Then
                    Dim a As Integer = checkNull()
                    If a = 1 Then
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្នន័យខុសមិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                        Return
                    Else
                        With DataGridView1.Rows(iRow)
                            addEx()
                            showOwn()
                            newRow()
                            CallBalance()
                        End With
                    End If
                Else
                    If checkNull() = 1 Then
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្ន័យខុសមិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                        Return
                    Else
                        If NoRecordChange() = 1 Then
                            resultError = frmMessageError.ShowBoxError("ទិន្នន័យគ្មានការកែប្រែ។", "គ្មានការកែប្រែ")
                            Return
                        Else
                            With DataGridView1.Rows(iRow)
                                If .Cells(3).Value = "" Or .Cells(3).Value = 0 Or .Cells(4).Value = 0 Or .Cells(4).Value = "" Or .Cells(5).Value = "" Or .Cells(5).Value = 0 Then
                                    resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការផ្លាស់ប្តូរបានទេ សូមពិនិត្យឡើងវិញ។", "ខុសទិន្នន័យ")
                                    Return
                                Else
                                    addTrace_Exchange("UPDATE OLD")
                                    UpdateEX()
                                    addTrace_Exchange("UPDATE NEW")
                                    resultError = frmMessageError.ShowBoxError("ការកែប្រែបានសម្រេច។", "ជោគជ័យ")
                                    showOwn()
                                End If
                            End With
                        End If
                    End If
                End If
            ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
                newRow()
            ElseIf e.KeyCode = Keys.Delete Then
                Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
                Dim a As Integer = Me.DataGridView1.Rows.Count()
                If a = 0 Or Me.DataGridView1.Rows(iRow).Cells(6).Value = "" Then
                    resultError = frmMessageError.ShowBoxError("គ្មានទិន្ន័យត្រូវលប់ សូមពិនិត្យឡើងវិញ។", "គ្មានទិន្ន័យ")
                    Return
                Else
                    '--------------------------------------------------- Check Loan in repay or not
                    result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                    If result = "1" Then
                        '-------------------------------------------------------------- Delete
                        addTrace_Exchange("DELETE")
                        addIn("delete from BK_Exchange where ID='" & Me.DataGridView1.Rows(iRow).Cells(7).Value & "' and BrId='" & frmMain.lblCode.Text & "'")
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុបទិន្នន័យ")
                        'Me.DataGridView1.CurrentCell
                        'Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
                        Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                        'AddToGrid1(DataGridView1, 8, "select 'Saved',Convert(Varchar(12),Date_Operation,101) as Date_Operation,a.T_ID,b.T_Des,Descriptions,USD,KHR,ID from Bank_Transaction a inner join Bank_T b on a.T_ID=b.T_ID where BrId='" & frmMain.lblCode.Text & "' and Date_Operation='" & date1 & "' order by Date_Create desc")
                        CallBalance()
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    '---------------------------------------- Function and Method
    Private Sub addTrace_Exchange(ByVal RecordAction As String)
        Dim Descriptions, User_Create, User_Modify, User_Delete, Type, BrId As String
        Dim Ex_Rate, USD, KHR As Double
        Dim DateAction, Date_Create, Date_Modify, Date_Delete As DateTime
        Dim Date_Operation As Date
        Dim iRow, ID As Integer
        If Me.Text = "FromCustomer" Then
            iRow = FrmCustomer.DataGridView1.CurrentCell.RowIndex
        Else
            iRow = Me.DataGridView1.CurrentCell.RowIndex
        End If
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select * from BK_Exchange where ID='" & DataGridView1.Rows(iRow).Cells(7).Value & "' and BrId='" & frmMain.lblCode.Text & "'"
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DateAction = DateTime.Now
        '--- Record
        ID = oDt.Rows(0).Item(0).ToString
        Date_Operation = oDt.Rows(0).Item(1).ToString
        Ex_Rate = oDt.Rows(0).Item(2).ToString
        USD = oDt.Rows(0).Item(3).ToString
        KHR = oDt.Rows(0).Item(4).ToString
        Descriptions = oDt.Rows(0).Item(5).ToString
        User_Create = oDt.Rows(0).Item(6).ToString
        Date_Create = oDt.Rows(0).Item(7).ToString
        User_Modify = oDt.Rows(0).Item(8).ToString
        'Date_Modify = oDt.Rows(0).Item(9).ToString
        If Format(oDt.Rows(0).Item(9).ToString, "") = "" Then
            Date_Modify = DateTime.MaxValue.ToString
        Else
            Date_Modify = oDt.Rows(0).Item(9).ToString
        End If
        User_Delete = frmMain.users
        Date_Delete = DateTime.Now
        Type = oDt.Rows(0).Item(12).ToString
        BrId = oDt.Rows(0).Item(13).ToString
        If RecordAction = "DELETE" Then
            If Date_Modify = DateTime.MaxValue.ToString Then
                addIn("insert TRACE_Exchange(DateAction,RecordAction,ID,Date_Operation,Ex_Rate,USD,KHR,Descriptions,User_Create,Date_Create,User_Delete,Date_Delete,Type,BrId) values ('" & DateAction & "','" & RecordAction & "','" & ID & "','" & Date_Operation & "','" & Ex_Rate & "','" & USD & "','" & KHR & "',N'" & Descriptions & "','" & User_Create & "','" & Date_Create & "','" & User_Delete & "','" & Date_Delete & "','" & Type & "','" & BrId & "')")
            Else
                addIn("insert TRACE_Exchange(DateAction,RecordAction,ID,Date_Operation,Ex_Rate,USD,KHR,Descriptions,User_Create,Date_Create,User_Modify,Date_Modify,User_Delete,Date_Delete,Type,BrId) values ('" & DateAction & "','" & RecordAction & "','" & ID & "','" & Date_Operation & "','" & Ex_Rate & "','" & USD & "','" & KHR & "',N'" & Descriptions & "','" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & User_Delete & "','" & Date_Delete & "','" & Type & "','" & BrId & "')")
            End If
        Else
            If Date_Modify = DateTime.MaxValue.ToString Then
                addIn("insert TRACE_Exchange(DateAction,RecordAction,ID,Date_Operation,Ex_Rate,USD,KHR,Descriptions,User_Create,Date_Create,Type,BrId) values ('" & DateAction & "','" & RecordAction & "','" & ID & "','" & Date_Operation & "','" & Ex_Rate & "','" & USD & "','" & KHR & "',N'" & Descriptions & "','" & User_Create & "','" & Date_Create & "','" & Type & "','" & BrId & "')")
            Else
                addIn("insert TRACE_Exchange(DateAction,RecordAction,ID,Date_Operation,Ex_Rate,USD,KHR,Descriptions,User_Create,Date_Create,User_Modify,Date_Modify,Type,BrId) values ('" & DateAction & "','" & RecordAction & "','" & ID & "','" & Date_Operation & "','" & Ex_Rate & "','" & USD & "','" & KHR & "',N'" & Descriptions & "','" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & Type & "','" & BrId & "')")
            End If
        End If
        oDa.Dispose()
        oDt.Dispose()
    End Sub
    Public Sub CallBalance()
        Dim date1 As Date = FormatDateTime(DateTime.Now, DateFormat.ShortDate)
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        Str = "exec sp_rptEndBalSumByEndDay '" & date1 & "','" & date1 & "','" & frmMain.lblCode.Text & "'"
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim a As Double = Val(oDt.Rows(0).Item(6).ToString) + Val(oDt.Rows(1).Item(4).ToString)
        Dim b As Double = Val(oDt.Rows(0).Item(5).ToString) + Val(oDt.Rows(1).Item(3).ToString)
        'MessageBox.Show(a.ToString, b.ToString)
        Label3.Text = Format(a, "##,###.##")
        Label4.Text = Format(b, "##,###.##")
        oDa.Dispose()
        oDt.Dispose()
    End Sub
    Private Sub newRow()
        If Me.Text = "KHRTOUSD" Then
            SetFontDatagrid(DataGridView1)
            DataGridView1.Rows.Add()
            Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
            With DataGridView1.Rows(iRow)
                .Cells(0).Style.BackColor = Color.Yellow
                .Cells(0).Value = "Editing"
                .Cells(0).ReadOnly = True
                .Cells(4).Style.BackColor = Color.Yellow
                .Cells(4).ReadOnly = True
                .Cells(6).Style.BackColor = Color.Yellow
                .Cells(6).ReadOnly = True
                .Cells(6).Value = "KHRTOUSD"
                .Cells(7).Style.BackColor = Color.Yellow
                .Cells(7).ReadOnly = True
                DataGridView1.CurrentCell = DataGridView1(1, iRow)
            End With
        Else
            SetFontDatagrid(DataGridView1)
            DataGridView1.Rows.Add()
            Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
            With DataGridView1.Rows(iRow)
                .Cells(0).Style.BackColor = Color.Yellow
                .Cells(0).Value = "Editing"
                .Cells(0).ReadOnly = True
                .Cells(5).Style.BackColor = Color.Yellow
                .Cells(5).ReadOnly = True
                .Cells(6).Style.BackColor = Color.Yellow
                .Cells(6).ReadOnly = True
                .Cells(6).Value = "USDTOKHR"
                .Cells(7).Style.BackColor = Color.Yellow
                .Cells(7).ReadOnly = True
                DataGridView1.CurrentCell = DataGridView1(1, iRow)
            End With
        End If
    End Sub
    Private Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select Descriptions, Date_Operation,Ex_Rate,USD,KHR,Type,ID from BK_Exchange where ID='" & Me.DataGridView1.Rows(iRow).Cells(7).Value & "' and BrId='" & frmMain.lblCode.Text & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        With DataGridView1.Rows(iRow)
            If FormatDateTime(.Cells(2).Value, DateFormat.ShortDate) = FormatDateTime(oDt.Rows(0).Item(1).ToString, DateFormat.ShortDate) = True And _
              .Cells(1).Value = oDt.Rows(0).Item(0).ToString And _
               .Cells(3).Value = oDt.Rows(0).Item(2).ToString And _
              .Cells(4).Value = oDt.Rows(0).Item(3).ToString And _
       .Cells(5).Value = oDt.Rows(0).Item(4).ToString And _
   .Cells(6).Value = oDt.Rows(0).Item(5).ToString Then
                Return 1
            Else
                Return 2
            End If
        End With
        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
    End Function
    Private Sub showOwn()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        If Me.DataGridView1.Rows(iRow).Cells(7).Value = "" Then
            Str = "select top 1 'Saved',Descriptions,Convert(Varchar(12),Date_Operation,101) Date_Operation,Ex_Rate,USD,KHR,Type,ID from BK_Exchange  where BrId='" & frmMain.lblCode.Text & "' order by Date_Create desc"
        Else
            Str = "select top 1 'Saved',Descriptions,Convert(Varchar(12),Date_Operation,101) Date_Operation,Ex_Rate,USD,KHR,Type,ID from BK_Exchange  where BrId='" & frmMain.lblCode.Text & "' and ID='" & Me.DataGridView1.Rows(iRow).Cells(7).Value & "'"
        End If
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DataGridView1.Rows(iRow).Cells(0).Value = "Saved"
        DataGridView1.Rows(iRow).Cells(1).Value = oDt.Rows(0).Item(1).ToString
        DataGridView1.Rows(iRow).Cells(2).Value = oDt.Rows(0).Item(2).ToString
        DataGridView1.Rows(iRow).Cells(3).Value = oDt.Rows(0).Item(3).ToString
        DataGridView1.Rows(iRow).Cells(4).Value = oDt.Rows(0).Item(4).ToString
        DataGridView1.Rows(iRow).Cells(5).Value = oDt.Rows(0).Item(5).ToString
        DataGridView1.Rows(iRow).Cells(6).Value = oDt.Rows(0).Item(6).ToString
        DataGridView1.Rows(iRow).Cells(7).Value = oDt.Rows(0).Item(7).ToString
        oDa.Dispose()
        oDt.Dispose()
        Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells(5).Value
        Me.DataGridView1.Rows(iRow).Cells(5).Value = Format(as1, "###,###.##")
    End Sub
    Private Function checkNull()
        Dim a As Integer
        Dim dg As DataGridView = DataGridView1
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If dg.Rows(iRow).Cells(1).Value Is Nothing Or dg.Rows(iRow).Cells(2).Value Is Nothing Or dg.Rows(iRow).Cells(3).Value Is Nothing Or dg.Rows(iRow).Cells(4).Value Is Nothing _
            Or dg.Rows(iRow).Cells(5).Value Is Nothing Or dg.Rows(iRow).Cells(6).Value Is Nothing Then
            a = 1
        Else
            a = 2
        End If
        Return a
    End Function
    Private Sub addEx()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Date).Value = Me.DataGridView1.Rows(iRow).Cells(2).Value
                .Add("@d1", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(3).Value
                .Add("@d2", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(4).Value
                .Add("@d3", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(5).Value
                .Add("@d4", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
                .Add("@d5", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(6).Value
                .Add("@d6", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d7", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d8", SqlDbType.DateTime).Value = DateTime.Now()
            End With
            sql = "insert BK_Exchange(Date_Operation,Ex_Rate,USD,KHR,Descriptions,Type,BrId,User_Create,Date_Create) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)"
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
    Public Sub UpdateEX()
        'Dim SH_Total As Double
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Date).Value = Me.DataGridView1.Rows(iRow).Cells(2).Value
                .Add("@d1", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(3).Value
                .Add("@d2", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(4).Value
                .Add("@d3", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(5).Value
                .Add("@d4", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
                .Add("@d6", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d5", SqlDbType.NVarChar).Value = frmMain.users
            End With
            sql = "update BK_Exchange set Date_Operation=@d0,Ex_Rate=@d1,USD=@d2,KHR=@d3,Descriptions=@d4,User_Modify=@d5,Date_Modify=@d6 where ID='" & Me.DataGridView1.Rows(iRow).Cells(7).Value & "' and BrId='" & frmMain.lblCode.Text & "'"
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
    Private Sub AddToGrid1(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                Dim iRow = Me.DataGridView1.CurrentRow.Index
                Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells(5).Value
                Me.DataGridView1.Rows(iRow).Cells(5).Value = Format(as1, "###,###.##")
                DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(0).ReadOnly = True
                DataGridView1.Columns(3).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(3).ReadOnly = True
                DataGridView1.Columns(7).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(7).ReadOnly = True
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
            AddToGridExchange(DataGridView1, 8, "select 'Saved',Descriptions,Date_Operation,Ex_Rate,USD,KHR,Type,ID from BK_Exchange where Date_Operation='" & date1 & "' and BrId='" & frmMain.lblCode.Text & "'")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Need IT now")
        End Try
    End Sub
    Sub AddToGridExchange(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
                DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
                DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
                DataGridView1.Rows(iRow).Cells(7).ReadOnly = True
                DataGridView1.Rows(iRow).Cells(7).Style.BackColor = Color.Yellow
                DataGridView1.Rows(iRow).Cells(6).ReadOnly = True
                DataGridView1.Rows(iRow).Cells(6).Style.BackColor = Color.Yellow
                'If 
                If DataGridView1.Rows(iRow).Cells(6).Value = "KHRTOUSD" Then
                    DataGridView1.Rows(iRow).Cells(4).ReadOnly = True
                    DataGridView1.Rows(iRow).Cells(4).Style.BackColor = Color.Yellow
                Else
                    DataGridView1.Rows(iRow).Cells(5).ReadOnly = True
                    DataGridView1.Rows(iRow).Cells(5).Style.BackColor = Color.Yellow
                End If
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class