Imports System.Data.SqlClient

Public Class frmOtherIncome

    Private Sub frmOtherIncome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SetFontDatagrid(DataGridView1)
        Me.addHeadGrid()
        Me.addNewRow()
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
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 3 Then
                Try
                    If DataGridView1.CurrentCell.Value Is Nothing Then
                        Return
                    Else
                        DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                    End If
                Catch ex As Exception
                    Return
                End Try
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 2 Then
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

            End If
        End If
    End Sub
    Private Sub addOtherIncome()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Dim CU_ID As Integer = 0
        Try
            Dim num As Integer = 0
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d1", SqlDbType.Date).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
                If Me.DataGridView1.Rows(iRow).Cells(2).Value = "រៀល" Then
                    CU_ID = 1
                Else
                    CU_ID = 2
                End If
                .Add("@d2", SqlDbType.NVarChar).Value = CU_ID
                .Add("@d3", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(3).Value
                .Add("@d4", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(4).Value
                .Add("@d5", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d6", SqlDbType.NVarChar).Value = frmMain.users
            End With
            sql = "insert into BK_OtherIncome(BrId,Date_Operation,CU_ID,Amount,Descriptions,Date_Create,User_Create) Values (@d0,@d1,@d2,@d3,@d4,@d5,@d6)"
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
    Private Sub addHeadGrid()
        SetFontDatagrid1(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 6
        DataGridView1.Columns(0).Name = "...."
        DataGridView1.Columns(1).Name = "កាលបរិច្ឆេត"
        DataGridView1.Columns(2).Name = "រូបបិយវត្ថុ"
        DataGridView1.Columns(3).Name = "ចំនួន"
        DataGridView1.Columns(4).Name = "បរិយាយ"
        DataGridView1.Columns(5).Name = "អូតូកូដ"
    End Sub
    Private Sub addNewRow()
        Me.DataGridView1.Rows.Add()
        Dim iRow = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(5).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(5).Style.BackColor = Color.Yellow
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
        DataGridView1.Rows(iRow).Cells(0).Value = "Edit"
    End Sub
    Private Function checkNull(ByVal b As Integer)
        Dim a As Integer
        Dim dg As DataGridView = DataGridView1
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If b = 1 Then
            If dg.Rows(iRow).Cells(1).Value Is Nothing Or dg.Rows(iRow).Cells(2).Value Is Nothing Or dg.Rows(iRow).Cells(3).Value Is Nothing Or dg.Rows(iRow).Cells(4).Value Is Nothing _
    Or dg.Rows(iRow).Cells(5).Value Is Nothing Then
                a = 1
            Else
                a = 2
            End If
        Else
            If dg.Rows(iRow).Cells(1).Value Is Nothing Or dg.Rows(iRow).Cells(2).Value Is Nothing Or dg.Rows(iRow).Cells(3).Value Is Nothing Or dg.Rows(iRow).Cells(4).Value Is Nothing Then
                a = 1
            Else
                a = 2
            End If
        End If

        Return a
    End Function
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.F12 Then
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If Me.DataGridView1.Rows(iRow).Cells(5).Value = "" Then
                Dim a As Integer = checkNull(2)
                If a = 1 Then
                    resultError = frmMessageError.ShowBoxError("Please fill your data all cells which allow to enter!", "Not enough information!")
                    Return
                Else
                    With Me.DataGridView1.Rows(iRow)
                        addOtherIncome()
                        showOtherIcome()
                        addNewRow()
                    End With
                End If
            Else
                If checkNull(1) = 1 Then
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនគ្រប់គ្រាន់មិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "មិនរក្សាទុក")
                    Return
                Else
                    'If Me.NoRecordChange = 1 Then
                    '    MessageBox.Show("No record change!", "No change")
                    '    Return
                    'Else
                    AddTrace_OtherIncome("UPDATE OLD")
                    'UpdateExpense()
                    UpdateOtherIncome()
                    AddTrace_OtherIncome("UPDATE NEW")
                    MessageBox.Show("Saved record change!", "Saved change")
                    showOtherIcome()
                    'End If
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
            If Me.DataGridView1.Rows(iRow).Cells(5).Value = "" Then
                MessageBox.Show("No record to delete!", "Can't Delete")
                Return
            Else
                If checkNull(1) = 1 Then
                    MessageBox.Show("No record to delete!", "Can't Delete")
                    Return
                Else
                    result = MyMessageBox.ShowBox("Are you sure, delete record?", "Delete data!")
                    If result = "1" Then
                        AddTrace_OtherIncome("DELETE")
                        addIn("Delete from BK_OtherIncome where ID='" & Me.DataGridView1.Rows(iRow).Cells(5).Value & "' and BrId='" & frmMain.lblCode.Text & "'")
                        Me.DataGridView1.Rows.Remove(DataGridView1.Rows(iRow))
                        MessageBox.Show("Record has been deleted!", "Deleted")
                    Else
                        Return
                    End If
                End If
            End If
        ElseIf e.KeyCode = Keys.F11 Then
            'Me.toExcel1()
        End If
    End Sub
    Private Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select top 1 Date_Operation,Amount,CU_ID,Descriptions from BK_OtherIcome where BrId like '" & frmMain.lblCode.Text & "' and ID='" & Me.DataGridView1.Rows(iRow).Cells(5).Value & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim Des As String = oDt.Rows(0).Item(0).ToString
        Dim EM_ID As String = oDt.Rows(0).Item(1).ToString
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            If Des = .Cells(6).Value And EM_ID = .Cells(2).Value Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
            Dim date2 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
            addHeadGrid()
            AddToGridExpense(DataGridView1, 6, "exec spOtherIncome '" & date1 & "','" & date2 & "','" & frmMain.lblCode.Text & "'")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Need IT now")
        End Try
    End Sub
    Private Sub AddToGridExpense(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                DataGridView1.Columns(5).ReadOnly = True
                DataGridView1.Columns(5).DefaultCellStyle.BackColor = Color.Yellow
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub showOtherIcome()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        If Me.DataGridView1.Rows(iRow).Cells(5).Value = "" Then
            Str = "Select top 1 'Saved',CONVERT(Varchar(12), a.Date_Operation,101) as OPDate,b.CU_Name,Amount,Descriptions,a.ID from BK_OtherIncome a Inner Join dbo.BK_Currency b on a.CU_ID= b.CU_ID  Where a.BrId='" & frmMain.lblCode.Text & "' order by a.Date_Create desc"
        Else
            'and a.ID=''
            Str = "Select top 1 'Saved',CONVERT(Varchar(12), a.Date_Operation,101) as OPDate,b.CU_Name,Amount,Descriptions,a.ID from BK_OtherIncome a Inner Join dbo.BK_Currency b on a.CU_ID= b.CU_ID  Where a.BrId='" & frmMain.lblCode.Text & "' and a.ID='" & DataGridView1.Rows(iRow).Cells(5).Value & "' order by a.Date_Create desc"
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
        oDa.Dispose()
        oDt.Dispose()
        Dim iRow1 As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow1).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow1).Cells(0).ReadOnly = True
        DataGridView1.Rows(iRow1).Cells(5).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow1).Cells(5).ReadOnly = True
    End Sub
    Private Sub UpdateOtherIncome()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Dim cu_id As Integer = 0
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Date).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value.ToString
                If Me.DataGridView1.Rows(iRow).Cells(2).Value.ToString = "រៀល" Then
                    cu_id = 1
                Else
                    cu_id = 2
                End If
                .Add("@d1", SqlDbType.Int).Value = cu_id
                .Add("@d2", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(3).Value.ToString
                .Add("@d3", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(4).Value.ToString
                .Add("@d4", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d5", SqlDbType.NVarChar).Value = frmMain.users
            End With
            sql = "update BK_OtherIncome set Date_Operation=@d0,Amount=@d2,CU_ID=@d1,Descriptions=@d3,Date_Modify=@d4,User_Modify=@d5 where ID='" & Me.DataGridView1.Rows(iRow).Cells(5).Value & "' and BrId='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub AddTrace_OtherIncome(ByVal RecordAction As String)
        Dim BrID, User_Create, User_Modify, User_Delete, Descriptions As String
        Dim ID, CU_ID As Integer
        Dim DateOperation As Date
        Dim Amt As Double
        'Dim Rec_Status As Boolean
        Dim Date_Create, Date_Modify, DateAction, Date_Delete As DateTime
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select * from BK_OtherIncome where ID='" & Me.DataGridView1.Rows(iRow).Cells(5).Value & "' and BrId='" & frmMain.lblCode.Text & "'"
            'On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DateAction = DateTime.Now
            '--- RecordAction
            BrID = oDt.Rows(0).Item(0).ToString
            DateOperation = oDt.Rows(0).Item(1).ToString
            Amt = oDt.Rows(0).Item(2).ToString
            CU_ID = oDt.Rows(0).Item(3).ToString
            User_Create = oDt.Rows(0).Item(5).ToString
            Date_Create = oDt.Rows(0).Item(4).ToString
            User_Modify = oDt.Rows(0).Item(7).ToString
            If Format(oDt.Rows(0).Item(6).ToString, "") = "" Then
                Date_Modify = DateTime.MaxValue.ToString
            Else
                Date_Modify = oDt.Rows(0).Item(6).ToString
            End If
            User_Delete = frmMain.users
            Date_Delete = DateTime.Now
            Descriptions = oDt.Rows(0).Item(11).ToString
            ID = oDt.Rows(0).Item(10).ToString

            If RecordAction = "DELETE" Then
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert into Trace_OtherIncome(DateAction,RecordAction,BrId,Date_Operation,Amount,CU_ID,Date_Create,User_Create,Date_Delete,User_Delete,ID,Descriptions) Values ('" & DateAction & "','" & RecordAction & "','" & BrID & "','" & DateOperation & "','" & Amt & "','" & CU_ID & "','" & Date_Create & "','" & User_Create & "','" & Date_Delete & "','" & User_Delete & "','" & ID & "','" & Descriptions & "')")
                Else
                    addIn("insert into Trace_OtherIncome(DateAction,RecordAction,BrId,Date_Operation,Amount,CU_ID,Date_Create,User_Create,Date_Modify,User_Modify,Date_Delete,User_Delete,ID,Descriptions) Values ('" & DateAction & "','" & RecordAction & "','" & BrID & "','" & DateOperation & "','" & Amt & "','" & CU_ID & "','" & Date_Create & "','" & User_Create & "','" & Date_Modify & "','" & User_Modify & "','" & Date_Delete & "','" & User_Delete & "','" & ID & "','" & Descriptions & "')")
                End If
            Else
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert into Trace_OtherIncome(DateAction,RecordAction,BrId,Date_Operation,Amount,CU_ID,Date_Create,User_Create,ID,Descriptions) Values ('" & DateAction & "','" & RecordAction & "','" & BrID & "','" & DateOperation & "','" & Amt & "','" & CU_ID & "','" & Date_Create & "','" & User_Create & "','" & ID & "','" & Descriptions & "')")
                Else
                    addIn("insert into Trace_OtherIncome(DateAction,RecordAction,BrId,Date_Operation,Amount,CU_ID,Date_Create,User_Create,Date_Modify,User_Modify,ID,Descriptions) Values ('" & DateAction & "','" & RecordAction & "','" & BrID & "','" & DateOperation & "','" & Amt & "','" & CU_ID & "','" & Date_Create & "','" & User_Create & "','" & Date_Modify & "','" & User_Modify & "','" & ID & "','" & Descriptions & "')")
                End If
            End If
            oDa.Dispose()
            oDt.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
End Class