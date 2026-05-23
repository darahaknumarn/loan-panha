Imports System.Data.SqlClient
Public Class frmEmployee
    Private Sub frmEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        newRow()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        'Dim iRows = frmDisburshment.DataGridView1.Rows.Count
        If Me.Text = "FromDisbursh" Then
            Dim iRows = frmDisburshment.DataGridView1.CurrentCell.RowIndex
            DataGridView1.ClearSelection()
            DataGridView1.Rows(iRow).Cells(1).Value = frmDisburshment.DataGridView1.Rows(iRows).Cells(2).Value
            Me.DataGridView1.CurrentCell = DataGridView1.Rows(iRow).Cells(2)
        Else
            Dim iRows = Me.DataGridView1.CurrentCell.RowIndex
            DataGridView1.ClearSelection()
            Me.DataGridView1.Rows(0).Cells(1).Selected = True
        End If
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            newRow()
        ElseIf e.KeyCode = Keys.Delete Then
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If Me.DataGridView1.Rows(iRow).Cells(0).Value = "Saved" Then
                If checkNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("មិនអាចលុបបានទេ ព្រោះគ្មានទិន្នន័យនេះ។", "ខ្វះទិន្នន័យ")
                    Return
                Else
                    Dim EM_Expense As String = getData("select EM_ID from ExpenseOperation where EM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                    Dim EM_Dis As String = getData("Select EM_ID from BK_Loan where EM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                    If EM_Expense = "" And EM_Dis = "" Then
                        result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                        If result = "1" Then
                            AddTrace_Employee("DELETE")
                            addIn("Delete from BK_Employee where EM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and EM_BrID='" & frmMain.lblCode.Text & "'")
                            resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុបទិន្នន័យ")
                            Me.DataGridView1.Rows.Remove(DataGridView1.Rows(iRow))
                        End If
                    Else
                        resultError = frmMessageError.ShowBoxError("កូដបុគ្គលិកម្នាក់នេះមាននៅក្នុងប្រតិបត្តិការហើយ មិនអាចលុបបានទេ។", "លុបទិន្នន័យ")
                        Return
                    End If
                End If
            Else
                resultError = frmMessageError.ShowBoxError("មិនអាចលុបបានទេ ព្រោះគ្មានទិន្នន័យនេះ។", "ខ្វះទិន្នន័យ")
                Return
            End If
        ElseIf e.KeyCode = Keys.F12 Then
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If Me.DataGridView1.Rows(iRow).Cells(0).Value = "Saved" Then
                If checkNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("មិនអាចរក្សាទុកបានទេ ព្រោះទិន្នន័យមិនគ្រប់គ្រាន់។", "ខ្វះទិន្នន័យ")
                    Return
                Else
                    If NoRecordChange() = 1 Then
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យគ្មានអ្វីប្រែប្រួល។", "គ្មានការកែរប្រែ")
                        Return
                    Else
                        '------------------------------ Update Employee
                        AddTrace_Employee("UPDATE OLD")
                        UpdateEmployee()
                        AddTrace_Employee("UPDATE NEW")
                        showEmployee()
                    End If
                End If
            Else
                If checkNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("មិនអាចរក្សាទុកបានទេ ព្រោះទិន្នន័យមិនគ្រប់គ្រាន់។", "ខ្វះទិន្នន័យ")
                    Return
                Else
                    Dim EmId As String = getData("select EM_ID from BK_Employee where EM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and EM_BrID='" & frmMain.lblCode.Text & "'")
                    If EmId = "" Then
                        '--------------------- Add New Employee
                        addEmployee()
                        showEmployee()
                        newRow()
                    Else
                        resultError = frmMessageError.ShowBoxError("កូដបុគ្គលិកនេះមាននៅក្នុងប្រព័ន្ធរួចហើយ សូមពិនិត្យឡើងវិញ។", "មានរួចហើយ")
                        Return
                    End If
                End If

            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddToGrid1(DataGridView1, 4, "select top 50 'Saved',EM_ID,EM_Name,Position from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "'")
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        AddToGrid1(DataGridView1, 4, "select top 100 'Saved',EM_ID,EM_Name,Position from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "'")
    End Sub

    '--------------------------------------------- Function and Method
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
                DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(0).ReadOnly = True
                DataGridView1.Columns(1).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(1).ReadOnly = True
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub AddTrace_Employee(ByVal RecordAction As String)
        Dim EM_BrID, EM_Name, Position, EM_ID, User_Create, User_Modify, User_Delete As String
        Dim Date_Create, Date_Modify, DateAction, Date_Delete As DateTime
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select * from BK_Employee where EM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and EM_BrID='" & frmMain.lblCode.Text & "'"
            'On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DateAction = DateTime.Now
            '--- RecordAction
            EM_ID = oDt.Rows(0).Item(0).ToString
            EM_Name = oDt.Rows(0).Item(1).ToString
            EM_BrID = oDt.Rows(0).Item(2).ToString
            Position = oDt.Rows(0).Item(3).ToString
            User_Create = oDt.Rows(0).Item(4).ToString
            Date_Create = oDt.Rows(0).Item(5).ToString
            User_Modify = oDt.Rows(0).Item(6).ToString
            'Date_Modify = oDt.Rows(0).Item(7).ToString
            If Format(oDt.Rows(0).Item(7).ToString, "") = "" Then
                Date_Modify = DateTime.MaxValue.ToString
            Else
                Date_Modify = oDt.Rows(0).Item(7).ToString
            End If
            User_Delete = frmMain.users
            Date_Delete = DateTime.Now
            If RecordAction = "DELETE" Then
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Employee(DateAction,RecordAction,EM_ID,EM_Name,EM_BrID,Position,User_Create,Date_Create,User_Delete,Date_Delete) Values('" & DateAction & "','" & RecordAction & "','" & EM_ID & "',N'" & EM_Name & "','" & EM_BrID & "',N'" & Position & "','" & User_Create & "','" & Date_Create & "','" & User_Delete & "','" & Date_Delete & "')")
                Else
                    addIn("insert TRACE_Employee(DateAction,RecordAction,EM_ID,EM_Name,EM_BrID,Position,User_Create,Date_Create,User_Modify,Date_Modify,User_Delete,Date_Delete) Values('" & DateAction & "','" & RecordAction & "','" & EM_ID & "',N'" & EM_Name & "','" & EM_BrID & "',N'" & Position & "','" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & User_Delete & "','" & Date_Delete & "')")
                End If
            Else
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Employee(DateAction,RecordAction,EM_ID,EM_Name,EM_BrID,Position,User_Create,Date_Create) Values('" & DateAction & "','" & RecordAction & "','" & EM_ID & "',N'" & EM_Name & "','" & EM_BrID & "',N'" & Position & "','" & User_Create & "','" & Date_Create & "')")
                Else
                    addIn("insert TRACE_Employee(DateAction,RecordAction,EM_ID,EM_Name,EM_BrID,Position,User_Create,Date_Create,User_Modify,Date_Modify) Values('" & DateAction & "','" & RecordAction & "','" & EM_ID & "',N'" & EM_Name & "','" & EM_BrID & "',N'" & Position & "','" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "')")
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
        Dim Str As String = "Select EM_ID,EM_Name,Position from BK_Employee where EM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and EM_BrID='" & frmMain.lblCode.Text & "'"
        'On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim EM_ID As String = oDt.Rows(0).Item(0).ToString
        Dim EM_Name As String = oDt.Rows(0).Item(1).ToString
        Dim Position As String = oDt.Rows(0).Item(2).ToString
        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            If EM_ID = .Cells(1).Value And EM_Name = .Cells(2).Value And Position = .Cells(3).Value Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Private Sub showEmployee()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select 'Saved',EM_ID,EM_Name,Position from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "' and EM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "'"
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DataGridView1.Rows(iRow).Cells(0).Value = oDt.Rows(0).Item(0).ToString
        DataGridView1.Rows(iRow).Cells(1).Value = oDt.Rows(0).Item(1).ToString
        DataGridView1.Rows(iRow).Cells(2).Value = oDt.Rows(0).Item(2).ToString
        DataGridView1.Rows(iRow).Cells(3).Value = oDt.Rows(0).Item(3).ToString
        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
        Dim iRow1 As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow1).Cells(1).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow1).Cells(1).ReadOnly = True
    End Sub
    Private Sub addEmployee()
        Dim sql As String
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
                .Add("@d1", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(2).Value
                .Add("@d2", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d3", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(3).Value
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d5", SqlDbType.DateTime).Value = DateTime.Now
            End With
            sql = "insert BK_Employee(EM_ID,EM_Name,EM_BrID,Position,User_Create,Date_Create) values (@d0,@d1,@d2,@d3,@d4,@d5)"
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
    Private Sub UpdateEmployee()
        Dim sql As String
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(2).Value
                .Add("@d1", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(3).Value
                .Add("@d2", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d3", SqlDbType.DateTime).Value = DateTime.Now
            End With
            sql = "Update BK_Employee set EM_Name=@d0,Position=@d1,User_Modify=@d2,Date_Modify=@d3 where EM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and EM_BrID='" & frmMain.lblCode.Text & "'"
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
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(1).Style.BackColor = Color.White
        DataGridView1.Rows(iRow).Cells(1).ReadOnly = False
        DataGridView1.Rows(iRow).Cells(0).Value = "Editing"
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
    End Sub
    Private Function checkNull()
        Dim a As Integer
        Dim dg As DataGridView = DataGridView1
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If dg.Rows(iRow).Cells(1).Value Is Nothing Or dg.Rows(iRow).Cells(2).Value Is Nothing Or dg.Rows(iRow).Cells(3).Value Is Nothing Then
            a = 1
        Else
            a = 2
        End If
        Return a
    End Function
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        AddToGrid1(DataGridView1, 4, "select 'Saved',EM_ID,EM_Name,Position from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "'")
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ToExcel(DataGridView1)
    End Sub
End Class