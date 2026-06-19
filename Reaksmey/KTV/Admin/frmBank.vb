Imports System.Data.SqlClient
Public Class frmBank
    Private Sub frmBank_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        newRow()
        CallBalance()
    End Sub
    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        Try
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
                    DataGridView1.Rows(iRow).Cells(16).Value = ""
                    Return
                End Try
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 2 Then
                Try
                    Dim Unit As String = getData("select T_Des from Bank_T where T_ID=" & DataGridView1.CurrentCell.Value)
                    If Unit = "" Then
                        resultError = frmMessageError.ShowBoxError("កូដ 1 ដាក់ចូល​ និង កូដ 2 ដកចេញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells(3).Value = ""
                    Else
                        DataGridView1.Rows(iRow).Cells(iCol + 1).Value = Unit.ToString
                        DataGridView1.CurrentCell = DataGridView1(iCol + 2, iRow)
                    End If
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("កូដ 1 ដាក់ចូល​ និង កូដ 2 ដកចេញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    DataGridView1.Rows(iRow).Cells(3).Value = ""
                End Try
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 4 Then
                DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 5 Then
                Try
                    If DataGridView1.Rows(iRow).Cells(5).Value = "" Or DataGridView1.Rows(iRow).Cells(5).Value = "0" Then
                        DataGridView1.Rows(iRow).Cells(5).Value = 0
                        DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                    ElseIf (DataGridView1.Rows(iRow).Cells(5).Value) / 1 = DataGridView1.Rows(iRow).Cells(5).Value Then
                        DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                        Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells(5).Value
                        Me.DataGridView1.Rows(iRow).Cells(5).Value = Format(as1, "###,###.##")
                    Else
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនដុល្លារមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells(5).Value = ""
                        Return
                    End If
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនដុល្លារមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    DataGridView1.Rows(iRow).Cells(5).Value = ""
                    Return
                End Try

            ElseIf DataGridView1.CurrentCell.ColumnIndex = 6 Then
                Try
                    If DataGridView1.Rows(iRow).Cells(6).Value = "" Or DataGridView1.Rows(iRow).Cells(6).Value = "0" Then
                        DataGridView1.Rows(iRow).Cells(6).Value = 0
                        DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                    ElseIf (DataGridView1.Rows(iRow).Cells(6).Value) / 1 = DataGridView1.Rows(iRow).Cells(6).Value Then
                        DataGridView1.CurrentCell = DataGridView1(iCol + 1, iRow)
                    Else
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនដុល្លារមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells(6).Value = ""
                        Return
                    End If
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលចំនួនដុល្លារមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    DataGridView1.Rows(iRow).Cells(6).Value = ""
                    Return
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
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
                            addBank(.Cells(1).Value, .Cells(2).Value, .Cells(5).Value, .Cells(6).Value, .Cells(4).Value, DateTime.Now, frmMain.users, frmMain.lblCode.Text, 0)
                            showBank()
                            newRow()
                            CallBalance()
                        End With
                    End If
                Else
                    If checkNull() = 1 Then
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្ន័យខុសមិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Return
                    Else
                        If NoRecordChange() = 1 Then
                            resultError = frmMessageError.ShowBoxError("ទិន្នន័យគ្មានការកែប្រែ។", "គ្មានការកែប្រែ")
                            Return
                        Else
                            'updateLoan(Me.DataGridView1.Rows(iRow).Cells(4).Value, Me.DataGridView1.Rows(iRow).Cells(2).Value, Me.DataGridView1.Rows(iRow).Cells(14).Value, Me.DataGridView1.Rows(iRow).Cells(15).Value)
                            AddTrace_Bank("UPDATE OLD")
                            With DataGridView1.Rows(iRow)
                                UpdateBank(.Cells(1).Value, .Cells(2).Value, .Cells(5).Value, .Cells(6).Value, .Cells(4).Value)
                            End With
                            AddTrace_Bank("UPDATE NEW")
                            resultError = frmMessageError.ShowBoxError("ការកែប្រែបានសម្រេច។", "ជោគជ័យ")
                            showBank()
                            CallBalance()
                        End If
                    End If
                End If
            ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
                newRow()
            ElseIf e.KeyCode = Keys.Delete Then
                Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
                Dim a As Integer = Me.DataGridView1.Rows.Count()
                If a = 0 Or Me.DataGridView1.Rows(iRow).Cells(7).Value = "" Then
                    resultError = frmMessageError.ShowBoxError("គ្មានទិន្ន័យត្រូវលប់ សូមពិនិត្យឡើងវិញ។", "គ្មានទិន្ន័យ")
                    Return
                Else
                    '--------------------------------------------------- Check Loan in repay or not
                    result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                    If result = "1" Then
                        '-------------------------------------------------------------- Delete Loan and Schedule
                        AddTrace_Bank("DELETE")
                        addIn("delete from Bank_Transaction where ID='" & Me.DataGridView1.Rows(iRow).Cells(7).Value & "' and BrId='" & frmMain.lblCode.Text & "'")
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុបទិន្នន័យ")
                        CallBalance()
                        Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
        AddToGrid1(DataGridView1, 8, "select 'Saved',Convert(Varchar(12),Date_Operation,101) as Date_Operation,a.T_ID,b.T_Des,Descriptions,USD,KHR,ID from Bank_Transaction a inner join Bank_T b on a.T_ID=b.T_ID where BrId='" & frmMain.lblCode.Text & "' and Date_Operation='" & date1 & "' order by Date_Create desc")
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.AddToGrid1(DataGridView1, 8, "select top 50 'Saved',Date_Operation,a.T_ID,b.T_Des,a.Descriptions,a.USD,a.KHR,a.ID from Bank_Transaction a inner join Bank_T b on a.T_ID=b.T_ID where BrId='" & frmMain.lblCode.Text & "' order by Date_Create desc")
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.AddToGrid1(DataGridView1, 8, "select top 100 'Saved',Date_Operation,a.T_ID,b.T_Des,a.Descriptions,a.USD,a.KHR,a.ID from Bank_Transaction a inner join Bank_T b on a.T_ID=b.T_ID where BrId='" & frmMain.lblCode.Text & "' order by Date_Create desc")
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.AddToGrid1(DataGridView1, 8, "select 'Saved',Date_Operation,a.T_ID,b.T_Des,a.Descriptions,a.USD,a.KHR,a.ID from Bank_Transaction a inner join Bank_T b on a.T_ID=b.T_ID where BrId='" & frmMain.lblCode.Text & "' order by Date_Create desc")
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ToExcel(Me.DataGridView1)
    End Sub
    '----------------------------------------------------------------------------------------- Function and Method
    Private Sub AddTrace_Bank(ByVal RecordAction As String)
        Dim BrId, User_Create, User_Modify, User_Delete, Descriptions As String
        Dim ID, IsExport, T_ID As Integer
        Dim Date_Operation As Date
        Dim USD, KHR As Double
        'Dim Rec_Status As Boolean
        Dim Date_Create, Date_Modify, DateAction, Date_Delete As DateTime
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select * from Bank_Transaction where ID='" & Me.DataGridView1.Rows(iRow).Cells(7).Value & "' and BrId='" & frmMain.lblCode.Text & "'"
            'On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DateAction = DateTime.Now
            '--- RecordAction
            ID = oDt.Rows(0).Item(0).ToString
            Date_Operation = oDt.Rows(0).Item(1).ToString
            T_ID = oDt.Rows(0).Item(2).ToString
            USD = oDt.Rows(0).Item(3).ToString
            KHR = oDt.Rows(0).Item(4).ToString
            Descriptions = oDt.Rows(0).Item(5).ToString
            User_Create = oDt.Rows(0).Item(6).ToString
            Date_Create = oDt.Rows(0).Item(7).ToString
            User_Modify = oDt.Rows(0).Item(8).ToString
            If Format(oDt.Rows(0).Item(9).ToString, "") = "" Then
                Date_Modify = DateTime.MaxValue.ToString
            Else
                Date_Modify = oDt.Rows(0).Item(9).ToString
            End If
            'Date_Modify = oDt.Rows(0).Item(9).ToString
            User_Delete = frmMain.users
            Date_Delete = DateTime.Now
            BrId = oDt.Rows(0).Item(12).ToString
            IsExport = oDt.Rows(0).Item(13).ToString
    
            If RecordAction = "DELETE" Then
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Bank(DateAction,RecordAction,ID,Date_Operation,T_ID,USD,KHR,Descriptions,User_Create,Date_Create,User_Delete,Date_Delete,BrId,IsExport) Values('" & DateAction & "','" & RecordAction & "','" & ID & "','" & Date_Operation & "','" & T_ID & "','" & USD & "','" & KHR & "',N'" & Descriptions & "',N'" & User_Create & "','" & Date_Create & "','" & User_Delete & "','" & Date_Delete & "','" & BrId & "','" & IsExport & "')")
                Else
                    addIn("insert TRACE_Bank(DateAction,RecordAction,ID,Date_Operation,T_ID,USD,KHR,Descriptions,User_Create,Date_Create,User_Modify,Date_Modify,User_Delete,Date_Delete,BrId,IsExport) Values('" & DateAction & "','" & RecordAction & "','" & ID & "','" & Date_Operation & "','" & T_ID & "','" & USD & "','" & KHR & "',N'" & Descriptions & "',N'" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & User_Delete & "','" & Date_Delete & "','" & BrId & "','" & IsExport & "')")
                End If
            Else
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Bank(DateAction,RecordAction,ID,Date_Operation,T_ID,USD,KHR,Descriptions,User_Create,Date_Create,BrId,IsExport) Values('" & DateAction & "','" & RecordAction & "','" & ID & "','" & Date_Operation & "','" & T_ID & "','" & USD & "','" & KHR & "',N'" & Descriptions & "',N'" & User_Create & "','" & Date_Create & "','" & BrId & "','" & IsExport & "')")
                Else
                    addIn("insert TRACE_Bank(DateAction,RecordAction,ID,Date_Operation,T_ID,USD,KHR,Descriptions,User_Create,Date_Create,User_Modify,Date_Modify,BrId,IsExport) Values('" & DateAction & "','" & RecordAction & "','" & ID & "','" & Date_Operation & "','" & T_ID & "','" & USD & "','" & KHR & "',N'" & Descriptions & "',N'" & User_Create & "','" & Date_Create & "','" & User_Modify & "','" & Date_Modify & "','" & BrId & "','" & IsExport & "')")
                End If
            End If
            oDa.Dispose()
            oDt.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub newRow()
        SetFontDatagrid(DataGridView1)
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        With DataGridView1.Rows(iRow)
            .Cells(0).Style.BackColor = Color.Yellow
            .Cells(0).Value = "Editing"
            .Cells(3).Style.BackColor = Color.Yellow
            .Cells(3).ReadOnly = True
            .Cells(7).Style.BackColor = Color.Yellow
            .Cells(7).ReadOnly = True
            DataGridView1.CurrentCell = DataGridView1(1, iRow)
        End With
    End Sub
    Private Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select top 1 ID,Convert(Varchar(12),Date_Operation,101) as Date_Operation,a.T_ID,b.T_Des,Descriptions,USD,KHR from Bank_Transaction a inner join Bank_T b on a.T_ID=b.T_ID where BrId='" & frmMain.lblCode.Text & "' and ID='" & Me.DataGridView1.Rows(iRow).Cells(7).Value & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        With DataGridView1.Rows(iRow)
            If FormatDateTime(.Cells(1).Value, DateFormat.ShortDate) = FormatDateTime(oDt.Rows(0).Item(1).ToString, DateFormat.ShortDate) = True And _
              .Cells(2).Value = oDt.Rows(0).Item(2).ToString And _
               .Cells(3).Value = oDt.Rows(0).Item(3).ToString And _
              .Cells(4).Value = oDt.Rows(0).Item(4).ToString And _
       .Cells(5).Value = oDt.Rows(0).Item(5).ToString And _
   .Cells(6).Value = oDt.Rows(0).Item(6).ToString Then
                Return 1
            Else
                Return 2
            End If
        End With
        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
    End Function
    Private Sub showBank()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        If Me.DataGridView1.Rows(iRow).Cells(7).Value = "" Then
            Str = "select top 1 ID,Convert(Varchar(12),Date_Operation,101) as Date_Operation,a.T_ID,b.T_Des,USD,KHR,Descriptions from Bank_Transaction a inner join Bank_T b on a.T_ID=b.T_ID where BrId='" & frmMain.lblCode.Text & "' order by Date_Create desc"
        Else
            Str = "select top 1 ID,Convert(Varchar(12),Date_Operation,101) as Date_Operation,a.T_ID,b.T_Des,USD,KHR,Descriptions from Bank_Transaction a inner join Bank_T b on a.T_ID=b.T_ID where BrId='" & frmMain.lblCode.Text & "' and ID='" & Me.DataGridView1.Rows(iRow).Cells(7).Value & "' order by Date_Create desc"
        End If

        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DataGridView1.Rows(iRow).Cells(0).Value = "Saved"
        DataGridView1.Rows(iRow).Cells(1).Value = oDt.Rows(0).Item(1).ToString
        DataGridView1.Rows(iRow).Cells(2).Value = oDt.Rows(0).Item(2).ToString
        DataGridView1.Rows(iRow).Cells(3).Value = oDt.Rows(0).Item(3).ToString
        DataGridView1.Rows(iRow).Cells(4).Value = oDt.Rows(0).Item(6).ToString
        DataGridView1.Rows(iRow).Cells(5).Value = oDt.Rows(0).Item(4).ToString
        DataGridView1.Rows(iRow).Cells(6).Value = oDt.Rows(0).Item(5).ToString
        DataGridView1.Rows(iRow).Cells(7).Value = oDt.Rows(0).Item(0).ToString
        oDa.Dispose()
        oDt.Dispose()
        Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells(5).Value
        Me.DataGridView1.Rows(iRow).Cells(5).Value = Format(as1, "###,###.##")
    End Sub
    Private Function checkNull()
        Dim a As Integer
        Dim dg As DataGridView = DataGridView1
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If dg.Rows(iRow).Cells(1).Value Is Nothing Or dg.Rows(iRow).Cells(2).Value Is Nothing Then
            a = 1
        Else
            a = 2
        End If
        Return a
    End Function
    Private Sub addBank(ByVal Date_Operation As Date, ByVal T_ID As Integer, ByVal USD As Double, ByVal KHR As Double, ByVal Descriptions As String, ByVal Date_Create As DateTime, ByVal User_Create As String, ByVal BrId As String, ByVal IsExport As Integer)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Date).Value = Date_Operation
                .Add("@d1", SqlDbType.Int).Value = T_ID
                .Add("@d2", SqlDbType.Float).Value = USD
                .Add("@d3", SqlDbType.Float).Value = KHR
                .Add("@d4", SqlDbType.NVarChar).Value = Descriptions
                .Add("@d5", SqlDbType.DateTime).Value = Date_Create
                .Add("@d6", SqlDbType.NVarChar).Value = User_Create
                .Add("@d7", SqlDbType.NVarChar).Value = BrId
                .Add("@d8", SqlDbType.Int).Value = IsExport
            End With
            sql = "insert Bank_Transaction (Date_Operation,T_ID,USD,KHR,Descriptions,Date_Create,User_Create,BrId,IsExport) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)"
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
                Dim iRow = Me.DataGridView1.Rows.Count - 1
                Dim d As Double = Val(Me.DataGridView1.Rows(iRow).Cells(5).Value)
                If d = 0 Or d.ToString = "" Then
                    Me.DataGridView1.Rows(iRow).Cells(5).Value = 0
                Else
                    Me.DataGridView1.Rows(iRow).Cells(5).Value = Format(d, "###,###.##")
                End If
                Dim as2 As Double = Val(Me.DataGridView1.Rows(iRow).Cells(6).Value)
                If as2 = 0 Or as2.ToString = "" Then
                    Me.DataGridView1.Rows(iRow).Cells(6).Value = 0
                Else
                    Me.DataGridView1.Rows(iRow).Cells(6).Value = Format(as2, "###,###.##")
                End If

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
    Public Sub UpdateBank(ByVal Date_Operation As Date, ByVal T_ID As Integer, ByVal USD As Double, ByVal KHR As Double, ByVal Descriptions As String)
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
                .Add("@d0", SqlDbType.Date).Value = Date_Operation
                .Add("@d1", SqlDbType.Int).Value = T_ID
                .Add("@d2", SqlDbType.Float).Value = USD
                .Add("@d3", SqlDbType.Float).Value = KHR
                .Add("@d4", SqlDbType.NVarChar).Value = Descriptions
                .Add("@d5", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d6", SqlDbType.NVarChar).Value = frmMain.users
            End With
            sql = "update Bank_Transaction set Date_Operation =@d0,T_ID=@d1,USD=@d2,KHR=@d3,Descriptions=@d4,Date_Modify=@d5,User_Modify=@d6 where ID='" & Me.DataGridView1.Rows(iRow).Cells(7).Value & "' and BrId='" & frmMain.lblCode.Text & "'"
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
    Public Sub CallBalance()
        Dim date1 As Date = FormatDateTime(DateTime.Now, DateFormat.ShortDate)
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        Str = "select isnull(a.USD,0)-isnull(b.USD,0) USD,isnull(a.KHR,0)-isnull(b.KHR,0) KHR from (select BrId,sum(isnull(USD,0))USD,Sum(isnull(KHR,0))KHR from Bank_Transaction where T_ID in(1,3) and BrId= '" & frmMain.lblCode.Text & "' group by BrId)a left join (select BrId,sum(isnull(USD,0))USD,Sum(isnull(KHR,0))KHR from Bank_Transaction where T_ID in(2,4) and BrId='" & frmMain.lblCode.Text & "' group by BrId) b on a.BrId=b.BrId"
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim a As Double = Val(oDt.Rows(0).Item(0).ToString)
        Dim b As Double = Val(oDt.Rows(0).Item(1).ToString)
        lblUSD.Text = Format(a, "##,###.##")
        lblKHR.Text = Format(b, "##,###.##")
        oDa.Dispose()
        oDt.Dispose()
    End Sub

End Class