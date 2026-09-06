Imports System.Data.SqlClient
Public Class frmAsset
    Sub datagrid2()
        SetFontDatagrid1(DataGridView1)
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 4
        DataGridView1.Columns(0).Name = "...."
        DataGridView1.Columns(1).Name = "កូដចំណាយ"
        DataGridView1.Columns(2).Name = "ឈ្មោះចំណាយ"
        DataGridView1.Columns(3).Name = "ខែរំលោះ"
    End Sub
    Private Sub frmAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        datagrid2()
        AddToGrid1(DataGridView1, 4, "select 'Saved',ASID,Name,Term from Asset where BrID='" & frmMain.lblCode.Text & "' order by ASID ")
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
                DataGridView1.Columns(1).DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns(1).ReadOnly = True
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
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
        DataGridView1.Rows(iRow).Cells(0).Value = "Edit"
    End Sub
    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If Me.DataGridView1.Rows.Count = 0 Then
            Me.DataGridView1.Rows.Add()
        End If
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If e.KeyCode = Keys.F12 Then
            '-------------------------------------------------------------------- Add new record
            If Me.DataGridView1.Rows(iRow).Cells(0).Value = "Edit" Then
                If checkNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនគ្រប់គ្រាន់សំរាប់រក្សាទុក សូមពិនិត្យឡើងវិញ។", "ខ្វះទិន្នន័យ")
                    Return
                Else
                    addAsset()
                    showAsset()
                    addNewRow()
                End If
            Else
                '----------------------------------------------------------------- Update old record
                If checkNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនគ្រប់គ្រាន់សំរាប់រក្សាទុក សូមពិនិត្យឡើងវិញ។", "ខ្វះទិន្នន័យ")
                    Return
                Else
                    If NoRecordChange() = 1 Then
                        resultError = frmMessageError.ShowBoxError("No reocord change", "No change")
                        Return
                    Else
                        AddTrace_Asset("UPDATE OLD")
                        UpdateAsset()
                        AddTrace_Asset("UPDATE NEW")
                        showAsset()
                        resultError = frmMessageError.ShowBoxError("Reocord change saved!", "Saved change")
                    End If
                End If
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            Dim Status As String = getData("select * from ExpenseOperation where ASID='" & DataGridView1.Rows(iRow).Cells(1).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
            If Status = "" Then
                result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                If result = "1" Then
                    AddTrace_Asset("DELETE")
                    '-------------------------------------------------------------- Check Status Loan
                    addIn("Delete from Asset where ASID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                    'showListRepay()
                    Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុបទិន្នន័យ")
                Else
                    Return
                End If
            Else
                resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនអាចលុបបានទេ ព្រោះលេខកូដនេះមាននៅក្នុងចំណាយរួចហើយ។", "លុបទិន្នន័យ")
                Return
            End If
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            Me.addNewRow()
        ElseIf e.KeyCode = Keys.F11 Then
            ToExcel(DataGridView1)
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Try
            With Me.DataGridView1
                Dim iRow = .CurrentCell.RowIndex
                Dim iCol = .CurrentCell.ColumnIndex
                If iCol = .Columns.Count - 1 Then
                    If iRow < .Rows.Count - 1 Then
                        .CurrentCell = DataGridView1(0, iRow + 1)
                    End If
                Else
                    If iRow < .Rows.Count - 1 Then
                        SendKeys.Send("{up}")
                    End If
                    If .CurrentCell.ColumnIndex = 1 Then
                        Dim cuid As String = getData("Select 'Saved',ASID,Name,Term from Asset where ASID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                        If .Rows(iRow).Cells(1).Value = "" Then
                            Return
                        Else
                            If cuid <> "" Then
                                showAsset()
                                Return
                            Else
                                .CurrentCell = DataGridView1(iCol + 1, iRow)
                            End If
                        End If
                    ElseIf .CurrentCell.ColumnIndex = 2 Then
                        If .Rows(iRow).Cells(2).Value Is Nothing Then
                            Return
                        Else
                            .CurrentCell = DataGridView1(iCol + 1, iRow)
                        End If
                    ElseIf .CurrentCell.ColumnIndex = 3 Then
                        If .Rows(iRow).Cells(3).Value Is Nothing Or .Rows(iRow).Cells(3).Value Then
                            MessageBox.Show("Can't be blank")
                            Return
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError("មិនអាចបញ្ចូលទិន្នន័យបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលមិនត្រឹមត្រូវ")
        End Try
    End Sub
    Private Sub addAsset()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
                .Add("@d1", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d2", SqlDbType.NVarChar).Value = DataGridView1.Rows(iRow).Cells(2).Value
                .Add("@d3", SqlDbType.Int).Value = 1
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d5", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d6", SqlDbType.Int).Value = Me.DataGridView1.Rows(iRow).Cells(3).Value
            End With
            sql = "insert Asset(ASID,BrID,Name,Rec_Status,User_Create,Date_Create,Term) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6)"
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
    Private Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "Select ASID,Name,Term from Asset where ASID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim ASID As String = oDt.Rows(0).Item(0).ToString
        Dim Name As String = oDt.Rows(0).Item(1).ToString
        Dim Term As String = oDt.Rows(0).Item(2).ToString
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            If ASID = .Cells(1).Value And Name = .Cells(2).Value And Term = .Cells(3).Value Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Private Sub showAsset()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "Select 'Saved',ASID,Name,Term from Asset where ASID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
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
    Private Sub UpdateAsset()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells(1).Value
                .Add("@d1", SqlDbType.NVarChar).Value = DataGridView1.Rows(iRow).Cells(2).Value
                .Add("@d2", SqlDbType.Int).Value = DataGridView1.Rows(iRow).Cells(3).Value
                .Add("@d3", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d5", SqlDbType.DateTime).Value = DateTime.Now
            End With
            sql = "update Asset set Name=@d1,Term=@d2,User_Modify=@d4,Date_Modify=@d5 where ASID=@d0 and BrID=@d3"
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
    Public Sub AddTrace_Asset(ByVal RecordAction As String)
        'Dim RecordAction As String
        Dim BrID, ASID, User_Create, User_Modify, User_Delete As String
        Dim Term As Integer
        Dim a As String
        Dim Date_Create, Date_Modify, DateAction, Date_Delete As DateTime
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            'Dim sql As String
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select ASID,BrID,Name,Rec_Status,User_Create,Date_Create,User_Modify,Date_Modify,Term from Asset where ASID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and BrID='" & frmMain.lblCode.Text & "'"
            'On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            ASID = oDt.Rows(0).Item(0).ToString
            BrID = oDt.Rows(0).Item(1).ToString
            a = oDt.Rows(0).Item(2).ToString
            'MessageBox.Show(a)
            Dim Rec_Status As Boolean = 1
            User_Create = oDt.Rows(0).Item(4).ToString
            Date_Create = oDt.Rows(0).Item(5).ToString
            User_Modify = oDt.Rows(0).Item(6).ToString
            User_Delete = frmMain.users
            Date_Delete = DateTime.Now
            If Format(oDt.Rows(0).Item(7).ToString, "") = "" Then
                Date_Modify = DateTime.MaxValue.ToString
            Else
                Date_Modify = oDt.Rows(0).Item(7).ToString
            End If
            Term = oDt.Rows(0).Item(8).ToString
            DateAction = DateTime.Now
            If RecordAction = "DELETE" Then
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Asset(DateAction,RecordAction,ASID,BrID,Name,Rec_Status,User_Create,Date_Create,Term,User_Delete,Date_Delete) values ('" & DateAction & "','" & RecordAction & "','" & ASID & "','" & BrID & "',N'" & a & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "','" & Term & "','" & User_Delete & "','" & Date_Delete & "')")
                Else
                    addIn("insert TRACE_Asset(DateAction,RecordAction,ASID,BrID,Name,Rec_Status,User_Create,Date_Create,Term,User_Delete,Date_Delete,User_Modify,Date_Modify) values ('" & DateAction & "','" & RecordAction & "','" & ASID & "','" & BrID & "',N'" & a & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "','" & Term & "','" & User_Delete & "','" & Date_Delete & "','" & User_Modify & "','" & Date_Modify & "')")
                End If
            ElseIf RecordAction = "UPDATE OLD" Then
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Asset(DateAction,RecordAction,ASID,BrID,Name,Rec_Status,User_Create,Date_Create,Term) values ('" & DateAction & "','" & RecordAction & "','" & ASID & "','" & BrID & "',N'" & a & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "','" & Term & "')")
                Else
                    addIn("insert TRACE_Asset(DateAction,RecordAction,ASID,BrID,Name,Rec_Status,User_Create,Date_Create,Term,User_Modify,Date_Modify) values ('" & DateAction & "','" & RecordAction & "','" & ASID & "','" & BrID & "',N'" & a & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "','" & Term & "','" & User_Modify & "','" & Date_Modify & "')")
                End If
            Else
                If Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Asset(DateAction,RecordAction,ASID,BrID,Name,Rec_Status,User_Create,Date_Create,Term) values ('" & DateAction & "','" & RecordAction & "','" & ASID & "','" & BrID & "',N'" & a & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "','" & Term & "')")
                Else
                    addIn("insert TRACE_Asset(DateAction,RecordAction,ASID,BrID,Name,Rec_Status,User_Create,Date_Create,Term,User_Modify,Date_Modify) values ('" & DateAction & "','" & RecordAction & "','" & ASID & "','" & BrID & "',N'" & a & "','" & Rec_Status & "','" & User_Create & "','" & Date_Create & "','" & Term & "','" & User_Modify & "','" & Date_Modify & "')")
                End If
            End If

            oDa.Dispose()
            oDt.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
End Class