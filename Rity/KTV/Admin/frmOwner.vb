Imports System.Data.SqlClient
Public Class frmOwner
    Private Sub frmOwner_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        datagrid2()
        newRow()
        CallBalance1()
        DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Columns(0).ReadOnly = True
        If Me.Text = "Deposit" Then
            Label2.Text = "ដាក់ទុន"
        Else
            Label2.Text = "ដកទុន"
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
        datagrid2()
        If Me.Text = "Deposit" Then
            AddToGrid1(DataGridView1, 6, "select 'Saved',OPDate,OPDescription,USD,KHR,OPID from OwnerTransaction where OPType='Deposit' and OPDate='" & date1 & "' and BrID='" & frmMain.lblCode.Text & "'")
        Else
            AddToGrid1(DataGridView1, 6, "select 'Saved',OPDate,OPDescription,USD,KHR,OPID from OwnerTransaction where OPType<>'Deposit' and OPDate='" & date1 & "' and BrID='" & frmMain.lblCode.Text & "'")
        End If

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
                    'MessageBox.Show("OK")
                    DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                Else
                    Dim now As Date
                    Dim day As Integer = DateTime.Now.Day
                    a = a - day
                    now = DateTime.Now.AddDays(a)
                    DataGridView1.Rows(iRow).Cells(iCol).Value = FormatDateTime(now, DateFormat.ShortDate)
                    DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                End If
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError("ការបញ្ចូលថ្ងៃខែមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                DataGridView1.Rows(iRow).Cells(1).Value = ""
                Return
            End Try
        ElseIf DataGridView1.CurrentCell.ColumnIndex = 2 Then
            DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
        ElseIf DataGridView1.CurrentCell.ColumnIndex = 3 Then
            Try
                If DataGridView1.Rows(iRow).Cells(3).Value = "" Or DataGridView1.Rows(iRow).Cells(3).Value = "0" Then
                    DataGridView1.Rows(iRow).Cells(3).Value = 0
                    DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                ElseIf (DataGridView1.Rows(iRow).Cells(3).Value) / 1 = DataGridView1.Rows(iRow).Cells(3).Value Then
                    DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                Else
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនដុល្លារមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    DataGridView1.Rows(iRow).Cells(3).Value = ""
                    Return
                End If
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនដុល្លារមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                DataGridView1.Rows(iRow).Cells(3).Value = ""
                Return
            End Try
        ElseIf DataGridView1.CurrentCell.ColumnIndex = 4 Then
            Try
                If DataGridView1.Rows(iRow).Cells(4).Value = "" Or DataGridView1.Rows(iRow).Cells(4).Value = "0" Then
                    DataGridView1.Rows(iRow).Cells(4).Value = 0
                    DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                ElseIf (DataGridView1.Rows(iRow).Cells(4).Value) / 1 = DataGridView1.Rows(iRow).Cells(4).Value Then
                    DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                Else
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនរៀលមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    DataGridView1.Rows(iRow).Cells(4).Value = ""
                    Return
                End If
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនរៀលមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                DataGridView1.Rows(iRow).Cells(4).Value = ""
                Return
            End Try
        End If
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If Me.DataGridView1.Rows.Count = 0 Then
            newRow()
        End If
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If e.KeyCode = Keys.F12 Then
            If Me.DataGridView1.Rows(iRow).Cells(5).Value Is Nothing Then
                If CheckNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្នន័យមិនគ្រប់គ្រាន់ សូមធ្វើការពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                    Return
                ElseIf CheckNull() = 2 Then
                    addOwn(frmMain.lblCode.Text, Me.DataGridView1.Rows(iRow).Cells(1).Value, DataGridView1.Rows(iRow).Cells(2).Value, DataGridView1.Rows(iRow).Cells(3).Value, DataGridView1.Rows(iRow).Cells(4).Value, Me.Text)
                    showOwn()
                    newRow()
                End If
            Else
                If CheckNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្នន័យមិនគ្រប់គ្រាន់ សូមធ្វើការពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្នន័យខុស")
                    Return
                ElseIf CheckNull() = 2 Then
                    AddTrace_Owner("UPDATE OLD")
                    UpdateOwn()
                    AddTrace_Owner("UPDATE NEW")
                    'addOwn(frmMain.lblCode.Text, Me.DataGridView1.Rows(iRow).Cells(1).Value, DataGridView1.Rows(iRow).Cells(2).Value, DataGridView1.Rows(iRow).Cells(3).Value, DataGridView1.Rows(iRow).Cells(4).Value, Me.Text)
                    CallBalance1()
                    showOwn()
                    newRow()
                End If
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            'Dim LR_ID As Integer = Val(getData("select Max(LR_ID)LR_ID from BK_LoanRepay where LD_ID='" & DataGridView1.Rows(iRow).Cells(1).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"))
            If Me.DataGridView1.Rows(iRow).Cells(5).Value Is Nothing Then
                resultError = frmMessageError.ShowBoxError("មិនអាចលុបបានទេ។", "គ្មានទិន្នន័យ")
                Return
            Else
                result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                If result = "1" Then
                    AddTrace_Owner("DELETE")
                    addIn("Delete from OwnerTransaction where OPID='" & Me.DataGridView1.Rows(iRow).Cells(5).Value & "'")
                    CallBalance1()
                    Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
                    datagrid2()
                    If Me.Text = "Deposit" Then
                        AddToGrid1(DataGridView1, 6, "select 'Saved',OPDate,OPDescription,USD,KHR,OPID from OwnerTransaction where OPType='Deposit' and OPDate='" & date1 & "' and BrID='" & frmMain.lblCode.Text & "'")
                    Else
                        AddToGrid1(DataGridView1, 6, "select 'Saved',OPDate,OPDescription,USD,KHR,OPID from OwnerTransaction where OPType<>'Deposit' and OPDate='" & date1 & "' and BrID='" & frmMain.lblCode.Text & "'")
                    End If
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុបទិន្នន័យ")
                Else
                    Return
                End If
            End If
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            newRow()
        End If
    End Sub

    '--------------------------------------------------------------------- Function and Method
    Public Sub CallBalance1()
        Dim date1 As Date = FormatDateTime(DateTime.Now, DateFormat.ShortDate)
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        Str = "exec sp_rptEndBalSumByEndDay1 '" & date1 & "','" & frmMain.lblCode.Text & "'"
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim a As Double = Val(oDt.Rows(0).Item(3).ToString)
        Dim b As Double = Val(oDt.Rows(0).Item(2).ToString)
        lblUSD.Text = Format(a, "##,###.##")
        lblKHR.Text = Format(b, "##,###.##")
        oDa.Dispose()
        oDt.Dispose()
    End Sub
    Private Sub AddTrace_Owner(ByVal RecordAction As String)
        Dim BrID, User_Create, User_Modify, User_Delete, OPDescription, OPType As String
        Dim OPID As Integer
        Dim OPDate As Date
        Dim USD, KHR As Double
        Dim Rec_Status As Boolean
        Dim Date_Create, Date_Modify, DateAction, Date_Delete As DateTime
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select * from OwnerTransaction where OPID='" & Me.DataGridView1.Rows(iRow).Cells(5).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
            'On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DateAction = DateTime.Now
            '--- RecordAction
            '-----------------													
            OPID = oDt.Rows(0).Item(0).ToString
            BrID = oDt.Rows(0).Item(1).ToString
            OPDate = oDt.Rows(0).Item(2).ToString
            OPDescription = oDt.Rows(0).Item(3).ToString
            USD = oDt.Rows(0).Item(4).ToString
            KHR = oDt.Rows(0).Item(5).ToString
            OPType = oDt.Rows(0).Item(6).ToString
            Rec_Status = oDt.Rows(0).Item(7).ToString
            User_Create = oDt.Rows(0).Item(8).ToString
            Date_Create = oDt.Rows(0).Item(9).ToString
            User_Modify = oDt.Rows(0).Item(10).ToString
            If Format(oDt.Rows(0).Item(11).ToString, "") = "" Then
                Date_Modify = DateTime.MaxValue.ToString
            Else
                Date_Modify = oDt.Rows(0).Item(11).ToString
            End If
            User_Delete = frmMain.users
            Date_Delete = DateTime.Now

            If RecordAction = "DELETE" Then
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_OwnerTransaction(DateAction,RecordAction,OPID,BrID,OPDate,OPDescription,USD,KHR,OPType,Rec_Status,User_Create,Date_Create,User_Delete,Date_Delete) Values('" & DateAction & "','" & RecordAction & "','" & OPID & "','" & BrID & "','" & OPDate & "','" & OPDescription & "','" & USD & "',N'" & KHR & "',N'" & OPType & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "','" & User_Delete & "','" & Date_Delete & "')")
                Else
                    addIn("insert TRACE_OwnerTransaction(DateAction,RecordAction,OPID,BrID,OPDate,OPDescription,USD,KHR,OPType,Rec_Status,User_Create,Date_Create,User_Modify,Date_Modify,User_Delete,Date_Delete) Values('" & DateAction & "','" & RecordAction & "','" & OPID & "','" & BrID & "','" & OPDate & "','" & OPDescription & "','" & USD & "',N'" & KHR & "',N'" & OPType & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & User_Delete & "','" & Date_Delete & "')")
                End If
            Else
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_OwnerTransaction(DateAction,RecordAction,OPID,BrID,OPDate,OPDescription,USD,KHR,OPType,Rec_Status,User_Create,Date_Create) Values('" & DateAction & "','" & RecordAction & "','" & OPID & "','" & BrID & "','" & OPDate & "','" & OPDescription & "','" & USD & "',N'" & KHR & "',N'" & OPType & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "')")
                Else
                    addIn("insert TRACE_OwnerTransaction(DateAction,RecordAction,OPID,BrID,OPDate,OPDescription,USD,KHR,OPType,Rec_Status,User_Create,Date_Create,User_Modify,Date_Modify) Values('" & DateAction & "','" & RecordAction & "','" & OPID & "','" & BrID & "','" & OPDate & "','" & OPDescription & "','" & USD & "',N'" & KHR & "',N'" & OPType & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "')")
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
            'con.Open()   con.ConnectionString = connectionString1

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
            'com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = False
        DataGridView1.Rows(iRow).Cells(5).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(5).ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
        DataGridView1.Rows(iRow).Cells(0).Value = "Editing"
    End Sub
    Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        'Dim sql As String
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select * from BK_LoanRepay where LR_ID='" & DataGridView1.Rows(iRow).Cells(0).Value & "' and LR_BrID='" & frmMain.lblCode.Text & "'"
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
    Private Function CheckNull()
        If Me.DataGridView1.Rows.Count = 0 Then
            newRow()
        End If
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        With DataGridView1.Rows(iRow)
            If .Cells(1).Value Is Nothing Or .Cells(2).Value Is Nothing Or .Cells(3).Value Is Nothing Or .Cells(4).Value Is Nothing Then
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
    Private Sub showOwn()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        If Me.DataGridView1.Rows(iRow).Cells(5).Value Is Nothing Then
            Str = "select top 1 Convert(Varchar(12),OPDate,101) as OPDate,OPDescription,USD,KHR,OPID from OwnerTransaction where BrID='" & frmMain.lblCode.Text & "' order by Date_Create desc"
        Else
            Str = "select Convert(Varchar(12),OPDate,101) as OPDate,OPDescription,USD,KHR,OPID from OwnerTransaction where OPID='" & Me.DataGridView1.Rows(iRow).Cells(5).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
        End If
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DataGridView1.Rows(iRow).Cells(0).Value = "Saved"
        DataGridView1.Rows(iRow).Cells(1).Value = oDt.Rows(0).Item(0).ToString
        DataGridView1.Rows(iRow).Cells(2).Value = oDt.Rows(0).Item(1).ToString
        DataGridView1.Rows(iRow).Cells(3).Value = oDt.Rows(0).Item(2).ToString
        DataGridView1.Rows(iRow).Cells(4).Value = oDt.Rows(0).Item(3).ToString
        DataGridView1.Rows(iRow).Cells(5).Value = oDt.Rows(0).Item(4).ToString
        oDa.Dispose()
        oDt.Dispose()
        'Dim iRow As Integer = Me.DataGridView1.Rows.Count
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(6).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(6).ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
    End Sub
    Public Sub UpdateOwn()
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
                .Add("@d2", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(3).Value
                .Add("@d3", SqlDbType.Float).Value = Me.DataGridView1.Rows(iRow).Cells(4).Value
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d5", SqlDbType.DateTime).Value = DateTime.Now
            End With
            sql = "update OwnerTransaction set OPDate=@d0,OPDescription=@d1,USD=@d2,KHR=@d3,User_Modify=@d4,Date_Modify=@d5 where OPID='" & Me.DataGridView1.Rows(iRow).Cells(5).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
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
    Private Sub addOwn(ByVal BrID As String, ByVal OPDate As Date, ByVal OPDescription As String, ByVal USD As Double, _
                        ByVal KHR As Double, ByVal OPType As String)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = BrID
                .Add("@d1", SqlDbType.Date).Value = OPDate
                .Add("@d2", SqlDbType.NVarChar).Value = OPDescription
                .Add("@d3", SqlDbType.Float).Value = USD
                .Add("@d4", SqlDbType.Float).Value = KHR
                .Add("@d5", SqlDbType.NVarChar).Value = OPType
                .Add("@d6", SqlDbType.Int).Value = 1
                .Add("@d7", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d8", SqlDbType.DateTime).Value = DateTime.Now
            End With
            sql = "insert OwnerTransaction (BrID,OPDate,OPDescription,USD,KHR,OPType,Rec_Status,User_Create,Date_Create) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)"
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
End Class