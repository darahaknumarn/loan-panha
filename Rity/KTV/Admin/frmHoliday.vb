Imports System.Data.SqlClient

Public Class frmHoliday

    Private Sub frmHoliday_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        newRow()
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        If DataGridView1.CurrentCell.ColumnIndex = 1 Then
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
                DataGridView1.Rows(iRow).Cells(2).Value = ""
                Return
            End Try
        End If


    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If e.KeyCode = Keys.F12 Then
            If Me.DataGridView1.Rows(iRow).Cells(3).Value = "" Then
                If CheckNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("មិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "ខ្វះទិន្នន័យ")
                    Return
                Else
                    addHoliday()
                    showHoliday()
                    newRow()
                End If
            Else
                If CheckNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("មិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "ខ្វះទិន្នន័យ")
                    Return
                Else
                    If NoRecordChange() = 1 Then
                        resultError = frmMessageError.ShowBoxError("គ្មានទិន្នន័យដូរ សូមពិនិត្យឡើងវិញ។", "គ្មានការកែរប្រែ")
                        Return
                    Else
                        AddTrace_Holiday("UPDATE OLD")
                        UpdateHoliday()
                        AddTrace_Holiday("UPDATE NEW")
                        showHoliday()
                    End If
                End If
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            If Me.DataGridView1.Rows(iRow).Cells(3).Value = "" Then
                resultError = frmMessageError.ShowBoxError("មិនអាចលុបបានទេ សូមពិនិត្យឡើងវិញ។", "គ្មានទិន្និន័យលុប")
                Return
            Else
                result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                If result = "1" Then
                    AddTrace_Holiday("DELETE")
                    addIn("Delete from BK_Holiday where ID='" & Me.DataGridView1.Rows(iRow).Cells(3).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                    Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប សូមពិនិត្យឡើងវិញ។", "គ្មានទិន្និន័យលុប")
                End If
            End If
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            newRow()
        End If
    End Sub
    '----------------------------- Function and Method
    Private Sub AddTrace_Holiday(ByVal RecordAction As String)
        Dim BrID, User_Create, User_Modify, User_Delete, Description As String
        Dim ID As Integer
        Dim StartDate As Date
        Dim Date_Create, Date_Modify, DateAction, Date_Delete As DateTime
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select * from BK_Holiday where ID='" & Me.DataGridView1.Rows(iRow).Cells(3).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
            'On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DateAction = DateTime.Now
            '--- RecordAction
            '-----------------													
            ID = oDt.Rows(0).Item(0).ToString
            StartDate = oDt.Rows(0).Item(1).ToString
            Description = oDt.Rows(0).Item(2).ToString
            User_Create = oDt.Rows(0).Item(3).ToString
            Date_Create = oDt.Rows(0).Item(4).ToString
            User_Modify = oDt.Rows(0).Item(5).ToString
            If Format(oDt.Rows(0).Item(6).ToString, "") = "" Then
                Date_Modify = DateTime.MaxValue.ToString
            Else
                Date_Modify = oDt.Rows(0).Item(6).ToString
            End If
            User_Delete = frmMain.users
            Date_Delete = DateTime.Now
            BrID = oDt.Rows(0).Item(9).ToString
            If RecordAction = "DELETE" Then
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Holiday(DateAction,RecordAction,ID,StartDate,Description,User_Create,Date_Create,User_Delete,Date_Delete,BrID) Values('" & DateAction & "','" & RecordAction & "','" & ID & "','" & StartDate & "',N'" & Description & "','" & User_Create & "','" & Date_Create & "','" & User_Delete & "','" & Date_Delete & "','" & BrID & "')")
                Else
                    addIn("insert TRACE_Holiday(DateAction,RecordAction,ID,StartDate,Description,User_Create,Date_Create,User_Modify,Date_Modify,User_Delete,Date_Delete,BrID) Values('" & DateAction & "','" & RecordAction & "','" & ID & "','" & StartDate & "',N'" & Description & "','" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & User_Delete & "','" & Date_Delete & "','" & BrID & "')")
                End If
            Else
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Holiday(DateAction,RecordAction,ID,StartDate,Description,User_Create,Date_Create,BrID) Values('" & DateAction & "','" & RecordAction & "','" & ID & "','" & StartDate & "',N'" & Description & "','" & User_Create & "','" & Date_Create & "','" & BrID & "')")
                Else
                    addIn("insert TRACE_Holiday(DateAction,RecordAction,ID,StartDate,Description,User_Create,Date_Create,User_Modify,Date_Modify,BrID) Values('" & DateAction & "','" & RecordAction & "','" & ID & "','" & StartDate & "',N'" & Description & "','" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & BrID & "')")
                End If
            End If
            oDa.Dispose()
            oDt.Dispose()
        Catch ex As System.Exception
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
                DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(0).ReadOnly = True
                DataGridView1.Columns(5).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(5).ReadOnly = True
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = False
        DataGridView1.Rows(iRow).Cells(3).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(3).ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
        DataGridView1.Rows(iRow).Cells(0).Value = "Editing"
    End Sub
    Private Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        'Dim sql As String
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select * from BK_Holiday where ID='" & DataGridView1.Rows(iRow).Cells(3).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
        'On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim StartDate As Date = oDt.Rows(0).Item(1).ToString
        Dim Description As String = oDt.Rows(0).Item(2).ToString
        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            If StartDate = .Cells(1).Value And Description = .Cells(1).Value Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Private Function CheckNull()
        If Me.DataGridView1.Rows.Count = 0 Then
            newRow()
        End If
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        With DataGridView1.Rows(iRow)
            If .Cells(1).Value Is Nothing Or .Cells(2).Value Is Nothing Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Sub datagrid2()
        SetFontDatagrid1(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 6
        DataGridView1.Columns(0).Name = "...."
        DataGridView1.Columns(1).Name = "កាលបរិច្ឆេទ"
        DataGridView1.Columns(2).Name = "បរិយាយ"
        DataGridView1.Columns(3).Name = "ចំនួនដុល្លារ"
        DataGridView1.Columns(4).Name = "ចំនួនរៀល"
        DataGridView1.Columns(5).Name = "OPID"
    End Sub
    Private Sub showHoliday()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        If Me.DataGridView1.Rows(iRow).Cells(3).Value Is Nothing Then
            Str = "select top 1 'Saved',Convert(Varchar(12),StartDate,101) as StartDate,Description,ID from BK_Holiday where BrID='" & frmMain.lblCode.Text & "' order by Date_Create desc"
        Else
            Str = "select top 1 'Saved',Convert(Varchar(12),StartDate,101) as StartDate,Description,ID from BK_Holiday where ID='" & Me.DataGridView1.Rows(iRow).Cells(3).Value & "' and BrID='" & frmMain.lblCode.Text & "' order by Date_Create desc"
        End If
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DataGridView1.Rows(iRow).Cells(0).Value = oDt.Rows(0).Item(0).ToString
        DataGridView1.Rows(iRow).Cells(1).Value = oDt.Rows(0).Item(1).ToString
        DataGridView1.Rows(iRow).Cells(2).Value = oDt.Rows(0).Item(2).ToString
        DataGridView1.Rows(iRow).Cells(3).Value = oDt.Rows(0).Item(3).ToString
        oDa.Dispose()
        oDt.Dispose()
        'Dim iRow As Integer = Me.DataGridView1.Rows.Count
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(6).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(6).ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
    End Sub
    Public Sub UpdateHoliday()
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
                .Add("@d0", SqlDbType.Date).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
                .Add("@d1", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(2).Value
                .Add("@d2", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d3", SqlDbType.DateTime).Value = DateTime.Now
            End With
            sql = "update BK_Holiday set StartDate=@d0,Description=@d1,User_Modify=@d2,Date_Modify=@d3 where ID='" & Me.DataGridView1.Rows(iRow).Cells(3).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
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
    Private Sub addHoliday()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Date).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
                .Add("@d1", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(2).Value
                .Add("@d2", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d3", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
            End With
            sql = "insert BK_Holiday (StartDate,Description,User_Create,Date_Create,BrID) values (@d0,@d1,@d2,@d3,@d4)"
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
    Sub AddToGriHoliday(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                DataGridView1.Columns(0).ReadOnly = True
                DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(3).ReadOnly = True
                DataGridView1.Columns(3).DefaultCellStyle.BackColor = Color.Yellow
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddToGriHoliday(Me.DataGridView1, 4, "Select top 50 'Saved',StartDate,Description,ID from BK_Holiday order by Date_Create desc")
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddToGriHoliday(Me.DataGridView1, 4, "Select top 100 'Saved',StartDate,Description,ID from BK_Holiday order by Date_Create desc")
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        AddToGriHoliday(Me.DataGridView1, 4, "Select 'Saved',StartDate,Description,ID from BK_Holiday order by Date_Create desc")
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ToExcel(DataGridView1)
    End Sub

End Class