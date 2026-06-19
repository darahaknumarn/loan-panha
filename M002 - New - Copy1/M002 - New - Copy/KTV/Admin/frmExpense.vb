Imports System.Data.SqlClient
Imports Microsoft.Office.Interop

Public Class frmExpense
    Private Sub frmExpense_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        addHeadGrid()
        addNewRow()
        'CalExpense(55, 36, "5/19/2017")
    End Sub
    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        If iCol = DataGridView1.Columns.Count - 1 Then
            If iRow < DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1(0, iRow + 1)
            End If
        Else
            If iRow < DataGridView1.Rows.Count - 1 Then
                SendKeys.Send("{up}")
            End If
            If DataGridView1.CurrentCell.ColumnIndex = 1 Then
                If DataGridView1.CurrentCell.Value Is Nothing Then
                    Return
                Else
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
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលថ្ងៃខែមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells(1).Value = ""
                        Return
                    End Try
                End If
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 2 Then
                If DataGridView1.CurrentCell.Value Is Nothing Then
                    Return
                Else
                    Dim EM_ID As String = getData("select EM_ID from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "' and EM_ID='" & DataGridView1.CurrentCell.Value.ToString & "'")
                    If EM_ID = "" Then
                        FrmStaff.Show()
                    Else
                        Dim em_name As String = getData("select EM_Name from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "' and EM_ID='" & DataGridView1.CurrentCell.Value.ToString & "'")
                        DataGridView1.Rows(iRow).Cells(3).Value = em_name.ToString
                        DataGridView1.CurrentCell = DataGridView1(iCol + 2, iRow)
                    End If
                End If
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 4 Then
                If DataGridView1.CurrentCell.Value Is Nothing Then
                    Return
                Else
                    Dim EX_ID As String = getData("select ASID from Asset where ASID='" & DataGridView1.CurrentCell.Value.ToString & "' and BrID='" & frmMain.lblCode.Text & "'")
                    If EX_ID = "" Then
                        frmAsset.Show()
                    Else
                        Dim oDt As New System.Data.DataTable
                        Dim em_name As String = "select Name,Term from Asset where ASID='" & DataGridView1.CurrentCell.Value.ToString & "' and BrID='" & frmMain.lblCode.Text & "'"
                        'On Error Resume Next
                        oDt.Clear()
                        oDa = New SqlDataAdapter(em_name, g_cnn)
                        oDa.Fill(oDt)
                        Me.DataGridView1.Rows(iRow).Cells(5).Value = oDt.Rows(0).Item(0).ToString
                        Me.DataGridView1.Rows(iRow).Cells(9).Value = oDt.Rows(0).Item(1).ToString
                        oDa.Dispose()
                        oDt.Dispose()
                        DataGridView1.CurrentCell = DataGridView1(iCol + 2, iRow)
                    End If
                End If
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 6 Then
                Try
                    If DataGridView1.CurrentCell.Value Is Nothing Then
                        Return
                    Else
                        DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                    End If
                Catch ex As Exception
                    Return
                End Try
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 7 Then
                If DataGridView1.CurrentCell.Value Is Nothing Then
                    Return
                Else
                    Try
                        Dim Curr As String = getData("select CU_Name from BK_Currency where CU_ID=" & DataGridView1.CurrentCell.Value)
                        If Curr = "" Then
                            resultError = frmMessageError.ShowBoxError("កូដ 1 សំរាប់រៀល និង កូដ 2 សំរាប់ដុល្លារ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Else
                            DataGridView1.CurrentCell.Value = Curr.ToString
                            DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                        End If
                    Catch ex As Exception
                        resultError = frmMessageError.ShowBoxError("កូដ 1 សំរាប់រៀល និង កូដ 2 សំរាប់ដុល្លារ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Return
                    End Try
                End If
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 8 Then
                If Me.DataGridView1.CurrentCell.Value Is Nothing Then
                    Return
                Else
                    DataGridView1.CurrentCell = DataGridView1(iCol + 3, iRow)
                End If
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 11 Then
                If Me.DataGridView1.CurrentCell.Value Is Nothing Then
                    Return
                Else
                    DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                End If
            End If
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
            Dim date2 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
            addHeadGrid()
            AddToGridExpense(DataGridView1, 13, "exec spListExpenseOperation '" & date1 & "','" & date2 & "','" & frmMain.lblCode.Text & "'")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Need IT now")
        End Try
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.F12 Then
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            'MessageBox.Show(Me.DataGridView1.Rows(iRow).Cells(1).Value.ToString())
            'Return
            If Me.DataGridView1.Rows(iRow).Cells(10).Value = "" Then
                Dim a As Integer = checkNull()
                If a = 1 Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្ន័យខុសមិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    Return
                Else
                    With Me.DataGridView1.Rows(iRow)
                        CalExpense(.Cells(8).Value, .Cells(9).Value, .Cells(1).Value)
                        showExpense()
                        addNewRow()
                    End With
                End If
            Else
                If checkNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនគ្រប់គ្រាន់មិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "មិនរក្សាទុក")
                    Return
                Else
                    If Me.NoRecordChange = 1 Then
                        MessageBox.Show("No record change!", "No change")
                        Return
                    Else
                        AddTrace_Expense("UPDATE OLD")
                        UpdateExpense()
                        AddTrace_Expense("UPDATE NEW")
                        MessageBox.Show("Saved record change!", "Saved change")
                        showExpense()
                    End If
                End If
            End If
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            addNewRow()
        ElseIf e.KeyCode = Keys.Delete Then
            If Me.DataGridView1.Rows.Count = 0 Then
                MessageBox.Show("No record to delete!", "Can't Delete")
                Return
            End If
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If Me.DataGridView1.Rows(iRow).Cells(10).Value = "" Then
                MessageBox.Show("No record to delete!", "Can't Delete")
                Return
            Else
                If checkNull() = 1 Then
                    MessageBox.Show("No record to delete!", "Can't Delete")
                    Return
                Else
                    result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                    If result = "1" Then
                        AddTrace_Expense("DELETE")
                        addIn("Delete from ExpenseOperation where OPCode='" & Me.DataGridView1.Rows(iRow).Cells(10).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                        addIn("Delete from ExpenseSchedule where OPCode='" & Me.DataGridView1.Rows(iRow).Cells(10).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                        Me.DataGridView1.Rows.Remove(DataGridView1.Rows(iRow))
                        MessageBox.Show("Record has been deleted!", "Deleted")
                    Else
                        Return
                    End If
                End If
            End If
        ElseIf e.KeyCode = Keys.F11 Then
            Me.toExcel1()
        End If
    End Sub
 
    '----------------------------------------------- Function and 
    Private Sub AddTrace_Expense(ByVal RecordAction As String)
        Dim BrID, User_Create, User_Modify, User_Delete, ASID, OPCurrency, OPDescription, OPCode, InNo, Suppliers As String
        Dim OPTerm, OPID, EM_ID, OPAutoCode As Integer
        Dim OPDate, OPMatDate As Date
        Dim OPCost As Double
        Dim Rec_Status As Boolean
        Dim Date_Create, Date_Modify, DateAction, Date_Delete As DateTime
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select * from ExpenseOperation where OPCode='" & Me.DataGridView1.Rows(iRow).Cells(10).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
            'On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DateAction = DateTime.Now
            '--- RecordAction
            OPID = oDt.Rows(0).Item(0).ToString
            BrID = oDt.Rows(0).Item(1).ToString
            OPDate = oDt.Rows(0).Item(2).ToString
            EM_ID = oDt.Rows(0).Item(3).ToString
            ASID = oDt.Rows(0).Item(4).ToString
            OPDescription = oDt.Rows(0).Item(5).ToString
            OPCurrency = oDt.Rows(0).Item(6).ToString
            OPCost = oDt.Rows(0).Item(7).ToString
            OPTerm = oDt.Rows(0).Item(8).ToString
            OPCode = oDt.Rows(0).Item(9).ToString
            OPAutoCode = oDt.Rows(0).Item(10).ToString
            OPMatDate = oDt.Rows(0).Item(11).ToString
            Rec_Status = 1
            User_Create = oDt.Rows(0).Item(13).ToString
            Date_Create = oDt.Rows(0).Item(14).ToString
            User_Modify = oDt.Rows(0).Item(15).ToString
            If Format(oDt.Rows(0).Item(16).ToString, "") = "" Then
                Date_Modify = DateTime.MaxValue.ToString
            Else
                Date_Modify = oDt.Rows(0).Item(16).ToString
            End If
            User_Delete = frmMain.users
            Date_Delete = DateTime.Now
            InNo = oDt.Rows(0).Item(19).ToString
            Suppliers = oDt.Rows(0).Item(20).ToString
            If RecordAction = "DELETE" Then
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_ExpenseOperation(DateAction,RecordAction,OPID,BrID,OPDate,EM_ID,ASID,OPDescription,OPCurrency,OPCost,OPTerm,OPCode,OPAutoCode,OPMatDate,Rec_Status,User_Create,Date_Create,User_Delete,Date_Delete,InNo,Supplier) Values('" & DateAction & "','" & RecordAction & "','" & OPID & "','" & BrID & "','" & OPDate & "','" & EM_ID & "','" & ASID & "',N'" & OPDescription & "',N'" & OPCurrency & "','" & OPCost & "','" & OPTerm & "','" & OPCode & "','" & OPAutoCode & "','" & OPMatDate & "','" & Rec_Status & "','" & User_Create & "' ,'" & Date_Create & "','" & User_Delete & "','" & Date_Delete & "','" & InNo & "','" & Suppliers & "')")
                Else
                    addIn("insert TRACE_ExpenseOperation(DateAction,RecordAction,OPID,BrID,OPDate,EM_ID,ASID,OPDescription,OPCurrency,OPCost,OPTerm,OPCode,OPAutoCode,OPMatDate,Rec_Status,User_Create,Date_Create,User_Modify,Date_Modify,User_Delete,Date_Delete,InNo,Supplier) Values('" & DateAction & "','" & RecordAction & "','" & OPID & "','" & BrID & "','" & OPDate & "','" & EM_ID & "','" & ASID & "',N'" & OPDescription & "',N'" & OPCurrency & "','" & OPCost & "','" & OPTerm & "','" & OPCode & "','" & OPAutoCode & "','" & OPMatDate & "','" & Rec_Status & "', ,'" & User_Create & "' ,'" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & User_Delete & "','" & Date_Delete & "','" & InNo & "','" & Suppliers & "')")
                End If
            Else
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_ExpenseOperation(DateAction,RecordAction,OPID,BrID,OPDate,EM_ID,ASID,OPDescription,OPCurrency,OPCost,OPTerm,OPCode,OPAutoCode,OPMatDate,Rec_Status,User_Create,Date_Create,InNo,Supplier) Values('" & DateAction & "','" & RecordAction & "','" & OPID & "','" & BrID & "','" & OPDate & "','" & EM_ID & "','" & ASID & "',N'" & OPDescription & "',N'" & OPCurrency & "','" & OPCost & "','" & OPTerm & "','" & OPCode & "','" & OPAutoCode & "','" & OPMatDate & "','" & Rec_Status & "','" & User_Create & "' ,'" & Date_Create & "','" & InNo & "','" & Suppliers & "')")
                Else
                    addIn("insert TRACE_ExpenseOperation(DateAction,RecordAction,OPID,BrID,OPDate,EM_ID,ASID,OPDescription,OPCurrency,OPCost,OPTerm,OPCode,OPAutoCode,OPMatDate,Rec_Status,User_Create,Date_Create,User_Modify,Date_Modify,InNo,Supplier) Values('" & DateAction & "','" & RecordAction & "','" & OPID & "','" & BrID & "','" & OPDate & "','" & EM_ID & "','" & ASID & "',N'" & OPDescription & "',N'" & OPCurrency & "','" & OPCost & "','" & OPTerm & "','" & OPCode & "','" & OPAutoCode & "','" & OPMatDate & "','" & Rec_Status & "','" & User_Create & "' ,'" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & InNo & "','" & Suppliers & "')")
                End If
            End If
            oDa.Dispose()
            oDt.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub showExpense()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        If Me.DataGridView1.Rows(iRow).Cells(10).Value = "" Then
            Str = "Select top 1 'Saved',CONVERT(Varchar(12), a.OPDate,101) as OPDate ,a.EM_ID,c.EM_Name,b.ASID,b.Name,a.OPDescription,a.OPCurrency,a.OPCost,a.OPTerm,a.OPCode,InNo,Supplier from dbo.ExpenseOperation a Inner Join dbo.Asset b on a.ASID= b.ASID and a.BrID = b.BrID inner join BK_Employee c on a.EM_ID=c.EM_ID and a.BrID=c.EM_BrID Where a.BrID='" & frmMain.lblCode.Text & "' order by a.Date_Create desc"
        Else
            Str = "Select top 1 'Saved',CONVERT(Varchar(12), a.OPDate,101) as OPDate ,a.EM_ID,c.EM_Name,b.ASID,b.Name,a.OPDescription,a.OPCurrency,a.OPCost,a.OPTerm,a.OPCode,InNo,Supplier from dbo.ExpenseOperation a Inner Join dbo.Asset b on a.ASID= b.ASID and a.BrID = b.BrID inner join BK_Employee c on a.EM_ID=c.EM_ID and a.BrID=c.EM_BrID Where a.BrID='" & frmMain.lblCode.Text & "' and a.OPCode='" & Me.DataGridView1.Rows(iRow).Cells(10).Value & "'"
        End If
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DataGridView1.Rows(iRow).Cells(0).Value = oDt.Rows(0).Item(0).ToString
        DataGridView1.Rows(iRow).Cells(1).Value = oDt.Rows(0).Item(1).ToString
        DataGridView1.Rows(iRow).Cells(2).Value = oDt.Rows(0).Item(2).ToString
        DataGridView1.Rows(iRow).Cells(3).Value = oDt.Rows(0).Item(3).ToString
        DataGridView1.Rows(iRow).Cells(4).Value = oDt.Rows(0).Item(4).ToString
        DataGridView1.Rows(iRow).Cells(5).Value = oDt.Rows(0).Item(5).ToString
        DataGridView1.Rows(iRow).Cells(6).Value = oDt.Rows(0).Item(6).ToString
        DataGridView1.Rows(iRow).Cells(7).Value = oDt.Rows(0).Item(7).ToString
        DataGridView1.Rows(iRow).Cells(8).Value = oDt.Rows(0).Item(8).ToString
        DataGridView1.Rows(iRow).Cells(9).Value = oDt.Rows(0).Item(9).ToString
        DataGridView1.Rows(iRow).Cells(10).Value = oDt.Rows(0).Item(10).ToString
        oDa.Dispose()
        oDt.Dispose()
        Dim iRow1 As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow1).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow1).Cells(0).ReadOnly = True
        DataGridView1.Rows(iRow1).Cells(3).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow1).Cells(3).ReadOnly = True
        DataGridView1.Rows(iRow1).Cells(5).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow1).Cells(5).ReadOnly = True
        DataGridView1.Rows(iRow1).Cells(9).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow1).Cells(9).ReadOnly = True
        DataGridView1.Rows(iRow1).Cells(10).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow1).Cells(10).ReadOnly = True
    End Sub
    Private Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select OPDescription,EM_ID,InNo,Supplier from ExpenseOperation where OPCode='" & Me.DataGridView1.Rows(iRow).Cells(10).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim Des As String = oDt.Rows(0).Item(0).ToString
        Dim EM_ID As String = oDt.Rows(0).Item(1).ToString
        Dim InNo As String = oDt.Rows(0).Item(2).ToString
        Dim Suppliers As String = oDt.Rows(0).Item(3).ToString
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            If Des = .Cells(6).Value And EM_ID = .Cells(2).Value And .Cells(11).Value = InNo And .Cells(12).Value = Suppliers Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Private Sub addExpense()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim num As Integer = 0
            Dim autoCode As String = getData("select max(OPAutoCode) from ExpenseOperation where ASID='" & Me.DataGridView1.Rows(iRow).Cells(4).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
            If autoCode = "" Then
                num = 1
            Else
                num = Val(autoCode) + 1
            End If
            Dim OPCode As String = ""
            Dim dateEx As Date = Me.DataGridView1.Rows(iRow).Cells(1).Value
            Dim year As String = dateEx.Year
            Dim month As String = dateEx.Month
            OPCode = frmMain.lblCode.Text & "-" & year & month & "-" & Me.DataGridView1.Rows(iRow).Cells(4).Value & "-" & num
            Dim lastDate As Date = getData("select MAX(ExDate) from ExpenseSchedule where OPCode='" & OPCode & "' and BrID='" & frmMain.lblCode.Text & "'")
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d1", SqlDbType.Date).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
                .Add("@d2", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(2).Value
                .Add("@d3", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(4).Value
                .Add("@d4", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(6).Value
                .Add("@d5", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(7).Value
                .Add("@d6", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(8).Value
                .Add("@d7", SqlDbType.Int).Value = Me.DataGridView1.Rows(iRow).Cells(9).Value
                .Add("@d8", SqlDbType.NVarChar).Value = OPCode
                .Add("@d9", SqlDbType.Int).Value = num
                .Add("@d10", SqlDbType.Date).Value = lastDate
                .Add("@d11", SqlDbType.Bit).Value = 1
                .Add("@d12", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d13", SqlDbType.DateTime).Value = DateTime.Now
                If Me.DataGridView1.Rows(iRow).Cells(11).Value Is Nothing Then
                    .Add("@d14", SqlDbType.NVarChar).Value = ""
                Else
                    .Add("@d14", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(11).Value
                End If
                If Me.DataGridView1.Rows(iRow).Cells(12).Value Is Nothing Then
                    .Add("@d15", SqlDbType.NVarChar).Value = ""
                Else
                    .Add("@d15", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(12).Value
                End If


            End With
            sql = "insert into ExpenseOperation(BrID,OPDate,EM_ID,ASID,OPDescription,OPCurrency,OPCost,OPTerm,OPCode,OPAutoCode,OPMatDate,Rec_Status,User_Create,Date_Create,InNo,Supplier) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15)"
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
    Sub AddToGridExpense(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
        ''Dim con As New SqlClient.SqlConnection
        Dim com As New SqlClient.SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As String = ""
        Try
            ''con.ConnectionString = connectionString1
            ''con.Open()
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
                DataGridView1.Columns(0).ReadOnly = True
                DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(1).ReadOnly = True
                DataGridView1.Columns(1).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(3).ReadOnly = True
                DataGridView1.Columns(3).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(5).ReadOnly = True
                DataGridView1.Columns(5).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(9).ReadOnly = True
                DataGridView1.Columns(9).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(10).ReadOnly = True
                DataGridView1.Columns(10).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(8).ReadOnly = True
                DataGridView1.Columns(8).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(7).ReadOnly = True
                DataGridView1.Columns(7).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(4).ReadOnly = True
                DataGridView1.Columns(4).DefaultCellStyle.BackColor = Color.Yellow
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub addNewRow()
        Me.DataGridView1.Rows.Add()
        Dim iRow = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(1).ReadOnly = False
        DataGridView1.Rows(iRow).Cells(1).Style.BackColor = Color.White
        DataGridView1.Rows(iRow).Cells(3).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(3).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(5).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(5).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(9).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(9).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(10).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(10).Style.BackColor = Color.Yellow
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
        DataGridView1.Rows(iRow).Cells(0).Value = "Edit"
    End Sub
    Private Sub addHeadGrid()
        SetFontDatagrid1(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 13
        DataGridView1.Columns(0).Name = "...."
        DataGridView1.Columns(1).Name = "កាលបរិច្ឆេត"
        DataGridView1.Columns(2).Name = "កូដបុគ្គលិក"
        DataGridView1.Columns(3).Name = "ឈ្មោះបុគ្គលិក"
        DataGridView1.Columns(4).Name = "កូដចំណាយ"
        DataGridView1.Columns(5).Name = "ឈ្មោះចំណាយ"
        DataGridView1.Columns(6).Name = "បរិយាយ"
        DataGridView1.Columns(7).Name = "រូបបិយវត្ថុ"
        DataGridView1.Columns(8).Name = "តំលៃ"
        DataGridView1.Columns(9).Name = "រយះពេលខែ(រំលោះ)"
        DataGridView1.Columns(10).Name = "កូដចំណាយ"
        DataGridView1.Columns(11).Name = "លេខវិក័យប័ត្រ"
        DataGridView1.Columns(12).Name = "អ្នកផ្គត់ផ្គង់"

    End Sub
    Private Function checkNull()
        Dim a As Integer
        Dim dg As DataGridView = DataGridView1
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If dg.Rows(iRow).Cells(1).Value Is Nothing Or dg.Rows(iRow).Cells(2).Value Is Nothing Or dg.Rows(iRow).Cells(3).Value Is Nothing Or dg.Rows(iRow).Cells(4).Value Is Nothing _
            Or dg.Rows(iRow).Cells(5).Value Is Nothing Or dg.Rows(iRow).Cells(6).Value Is Nothing Or dg.Rows(iRow).Cells(7).Value Is Nothing _
        Or dg.Rows(iRow).Cells(8).Value Is Nothing Or _
         dg.Rows(iRow).Cells(9).Value Is Nothing Then
            a = 1
        Else
            a = 2
        End If
        Return a
    End Function
    Private Sub CalExpense(ByVal Disbursh As Integer, ByVal term As Integer, ByVal dateEx As DateTime)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim num As Integer = 0
        Dim autoCode As String = getData("select max(OPAutoCode) from ExpenseOperation where ASID='" & Me.DataGridView1.Rows(iRow).Cells(4).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
        If autoCode = "" Then
            num = 1
        Else
            num = Val(autoCode) + 1
        End If
        Dim OPCode As String = ""
        Dim year As String = dateEx.Year
        Dim month As String = dateEx.Month
        OPCode = frmMain.lblCode.Text & "-" & year & month & "-" & Me.DataGridView1.Rows(iRow).Cells(4).Value & "-" & num
        Dim payDate As DateTime = dateEx
        Dim interestRate1 As Double = 0
        Dim dailyInterest As Double = 0
        Dim loanAmount As Double
        Dim amortizationTerm As Integer = 0
        Dim no As Integer = 0
        Dim currentBalance As Double
        Dim cummulativeInterest As Double = 0
        Dim monthlyPrincipal As Double = 0
        Dim cummulativePrincipal As Double = 0
        Dim payoff As Double = 0
        'int i = 0;
        loanAmount = Disbursh
        currentBalance = Disbursh
        amortizationTerm = term
        ' Calculate the monthly payment and round it to 2 decimal places         
        Dim dailypayment = loanAmount / amortizationTerm
        dailypayment = Math.Round(dailypayment, 2)
        ' Loop for amortization term (number of monthly payments)
        For j As Integer = 0 To amortizationTerm - 1
            ' Calculate monthly cycle
            dailyInterest = Disbursh * interestRate1
            Dim inter1 As Double = ((currentBalance * interestRate1) * term) - dailyInterest
            monthlyPrincipal = dailypayment - dailyInterest
            currentBalance = currentBalance - dailypayment
            'dailyInterest = dailypayment + dailyInterest
            If j = amortizationTerm - 1 AndAlso currentBalance <> dailypayment Then
                ' Adjust the last payment to make sure the final balance is 0
                dailypayment += currentBalance
                currentBalance = 0
            End If
            dailyInterest = dailyInterest + dailypayment
            ' Reset Date
            Dim date1 As String = getData("select top 1 ExDate from ExpenseSchedule where OPCode='" & OPCode & "'")
            If date1 = "" Then
                payDate = payDate
            Else
                payDate = payDate.AddMonths(1)
            End If
            cummulativeInterest += dailyInterest
            cummulativePrincipal += monthlyPrincipal
            no = no + 1
            payoff = payoff + dailypayment
            addExpenseSchedule(OPCode, frmMain.lblCode.Text, payDate, dailypayment, payoff, currentBalance, DateTime.Now)
        Next
        With Me.DataGridView1.Rows(iRow)
            addExpense()
        End With
    End Sub
    Private Sub addExpenseSchedule(ByVal OPCode As String, ByVal BrID As String, ByVal ExDate As Date, ByVal ExCost As Double, ByVal ExAccumulate As Double, ByVal ASBalance As Double, ByVal Date_Create As DateTime)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = OPCode
                .Add("@d1", SqlDbType.NVarChar).Value = BrID
                .Add("@d2", SqlDbType.Date).Value = ExDate
                .Add("@d3", SqlDbType.Float).Value = ExCost
                .Add("@d4", SqlDbType.Float).Value = ExAccumulate
                .Add("@d5", SqlDbType.Float).Value = ASBalance
                .Add("@d6", SqlDbType.DateTime).Value = Date_Create
            End With
            sql = "insert into ExpenseSchedule(OPCode,BrID,ExDate,ExCost,ExAccumulate,ASBalance,Date_Create) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub toExcel1()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
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
        Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\sample\DepreciationSchedule.xls", False, True)
        Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Report1"), Excel.Worksheet)
        xlApp.Visible = True
        sql = "select ExDate,ExCost,ASBalance from ExpenseSchedule where OPCode ='" & Me.DataGridView1.Rows(iRow).Cells(10).Value & "' order by ExDate"
        Dim count As Integer = getData("select count(OPCode) from ExpenseSchedule where OPCode ='" & Me.DataGridView1.Rows(iRow).Cells(10).Value & "'")
        Dim dscmd As New SqlDataAdapter(sql, g_cnn)
        Dim ds As New DataSet()
        dscmd.Fill(ds)
        With excelWorksheet
            .Range("A9:A" & count + 5).EntireRow.Insert()
            .Range("B2").Value = Me.DataGridView1.Rows(iRow).Cells(4).Value
            .Range("B3").Value = Me.DataGridView1.Rows(iRow).Cells(5).Value
            .Range("B4").Value = Me.DataGridView1.Rows(iRow).Cells(6).Value
            .Range("B5").Value = frmMain.lblCode.Text
            .Range("E2").Value = Me.DataGridView1.Rows(iRow).Cells(9).Value
            .Range("E3").Value = Me.DataGridView1.Rows(iRow).Cells(8).Value
            .Range("E4").Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
            .Range("E5").Value = Me.DataGridView1.Rows(iRow).Cells(7).Value
            For i = 0 To ds.Tables(0).Rows.Count - 1
                For j = 0 To ds.Tables(0).Columns.Count - 1
                    data = ds.Tables(0).Rows(i).ItemArray(j).ToString()
                    .Cells(i + 8, j + 2) = data
                    .Cells(i + 8, 1) = i + 1
                Next
            Next
        End With
    End Sub
    Private Sub UpdateExpense()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(2).Value.ToString
                .Add("@d1", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(6).Value
                .Add("@d2", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d3", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d4", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(11).Value
                .Add("@d5", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(12).Value

            End With
            sql = "update ExpenseOperation set EM_ID=@d0,OPDescription =@d1,User_Modify=@d2,Date_Modify=@d3,InNo=@d4,Supplier=@d5 where OPCode='" & Me.DataGridView1.Rows(iRow).Cells(10).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub

End Class