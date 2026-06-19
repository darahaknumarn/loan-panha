Imports System.Data.SqlClient

Public Class frmWiteOff

    Private Sub frmWiteOff_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        'DataGridView1.Rows.Add()
        newRow()
        'Me.MaximumSize = True
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ToExcel(DataGridView1)
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
            ElseIf DataGridView1.CurrentCell.ColumnIndex = 0 Then

                if DataGridView1.CurrentCell.Value.ToString="" Then
                    Return
                End If
                dim a as String = getdata("select LD_Status from BK_Loan where LD_ID='"& DataGridView1.CurrentCell.Value.ToString &"'")
                if a ="Payoff" or a="Mature"
                    MessageBox.Show("This loan is already payoff, so can't do this operation!","Error!",MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                    ElseIf a=""
                        MessageBox.Show("No this loan id!","Error!",MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                End If
                dim b as String=getData("select LD_ID from Writeoff where LD_ID='"& DataGridView1.CurrentCell.Value.ToString &"'")
                if b <>""
                    MessageBox.Show("This loan is already writeoff!","Try other loan")
                    return
                End If
                If DataGridView1.CurrentCell.Value Is Nothing Then
                    Return
                Else
                    Dim oDt As New System.Data.DataTable
                    Dim WF As String = "exec sp_GetLoanToWriteoff '" & DataGridView1.CurrentCell.Value.ToString & "','" & frmMain.lblCode.Text & "'"
                    oDt.Clear()
                    oDa = New SqlDataAdapter(WF, g_cnn)
                    oDa.Fill(oDt)
                    Me.DataGridView1.Rows(iRow).Cells(1).Value = dateTime.now.ToShortDateString()
                    Me.DataGridView1.Rows(iRow).Cells(2).Value = oDt.Rows(0).Item(4).ToString
                    Me.DataGridView1.Rows(iRow).Cells(3).Value = oDt.Rows(0).Item(5).ToString
                    Me.DataGridView1.Rows(iRow).Cells(4).Value = oDt.Rows(0).Item(6).ToString
                    Me.DataGridView1.Rows(iRow).Cells(5).Value = oDt.Rows(0).Item(7).ToString
                    Me.DataGridView1.Rows(iRow).Cells(6).Value = oDt.Rows(0).Item(10).ToString
                    Me.DataGridView1.Rows(iRow).Cells(7).Value = oDt.Rows(0).Item(11).ToString
                    oDa.Dispose()
                    oDt.Dispose()
            
                   
                    addIn("insert into [dbo].[Writeoff] (LD_ID,BR_ID,WOF_Date,LD_OS,IsExport,User_Create,Date_Create) values('"& DataGridView1.CurrentCell.Value.ToString &"','"& frmMain.lblCode.Text &"','"& dateTime.now.ToShortDateString() &"','"& Me.DataGridView1.Rows(iRow).Cells(7).Value &"',0,'"& frmMain.users &"','"& dateTime.now() &"')")
                    showWF()
                    newRow()
                End If
        End If
            end if 

    End Sub
     Private Sub addWF()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            'Dim num As Integer = 0
            'Dim autoCode As String = getData("select max(OPAutoCode) from ExpenseOperation where ASID='" & Me.DataGridView1.Rows(iRow).Cells(4).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
            'If autoCode = "" Then
            '    num = 1
            'Else
            '    num = Val(autoCode) + 1
            'End If
            Dim OPCode As String = ""
            Dim dateEx As Date = Me.DataGridView1.Rows(iRow).Cells(1).Value
            Dim year As String = dateEx.Year
            Dim month As String = dateEx.Month
            'OPCode = frmMain.lblCode.Text & "-" & year & month & "-" & Me.DataGridView1.Rows(iRow).Cells(4).Value & "-" & num
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
                '.Add("@d9", SqlDbType.Int).Value = num
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddToGridWF(DataGridView1,8,"exec sp_GetListLoanWriteoff '"& me.DateTimePicker1.Value.ToShortDateString()&"','"& me.DateTimePicker2.Value.ToShortDateString() &"','"& frmMain.lblCode.Text &"'")
    End Sub
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        'DataGridView1.Columns("LDID").DefaultCellStyle.BackColor = Color.
        DataGridView1.Columns("LDID").ReadOnly = False
        DataGridView1.Columns("cusID").DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Columns("cusID").ReadOnly = True
        DataGridView1.Columns("cusName").DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Columns("cusName").ReadOnly = True
        DataGridView1.Columns("cusAddress").DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Columns("cusAddress").ReadOnly = True
        DataGridView1.Columns("dateDis").DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Columns("dateDis").ReadOnly = True
        DataGridView1.Columns("disAmt").DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Columns("disAmt").ReadOnly = True
        DataGridView1.Columns("LDOS").DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Columns("LDOS").ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("LDID").ColumnIndex, iRow)
        'lblAutoSum.Text = 0
    End Sub

    Private  Sub AddToGridWF(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                DataGridView1.Rows(i - 1).Cells("disAmt").Value = Format(Val(DataGridView1.Rows(i - 1).Cells("disAmt").Value), "###,###.##")
                DataGridView1.Rows(i - 1).Cells("LDOS").Value = Format(Val(DataGridView1.Rows(i - 1).Cells("LDOS").Value), "###,###.##")
                DataGridView1.Columns("LDID").DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns("LDID").ReadOnly = True
                DataGridView1.Columns("cusID").DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns("cusID").ReadOnly = True
                DataGridView1.Columns("cusName").DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns("cusName").ReadOnly = True
                DataGridView1.Columns("cusAddress").DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns("cusAddress").ReadOnly = True
                DataGridView1.Columns("dateDis").DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns("dateDis").ReadOnly = True
                DataGridView1.Columns("disAmt").DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns("disAmt").ReadOnly = True
                DataGridView1.Columns("LDOS").DefaultCellStyle.BackColor = Color.Yellow
                DataGridView1.Columns("LDOS").ReadOnly = True
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
        Private Sub showWF()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "Select a.LD_ID,e.WOF_Date,a.CM_ID,c.CM_KhName,d.VL_ID + ',' + d.CN_ID +','+ d.DT_ID +','+d.PV_ID as 'CM_Address',a.LD_Dis_Date,a.LD_Dis_Amt,e.LD_OS From BK_Loan a inner join BK_Employee b on a.EM_ID = b.EM_ID and a.LD_BrId = b.EM_BrID   inner join BK_Customer c on a.CM_ID = c.CM_ID and a.LD_BrId = c.CM_BrId and a.CM_ID1=c.ID  inner join BK_Location d on d.LO_ID = c.LO_ID and d.LO_BrID = c.CM_BrId   inner join Writeoff e on a.LD_ID= e.LD_ID and a.LD_BrId = e.BR_ID Where a.LD_BrId= '" & frmMain.lblCode.Text &"' and e.LD_ID='"& Me.DataGridView1.Rows(iRow).Cells(1).Value &"'"
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DataGridView1.Rows(iRow).Cells("LDID").Value = oDt.Rows(0).Item(0).ToString
        DataGridView1.Rows(iRow).Cells("dateWF").Value = oDt.Rows(0).Item(1).ToString
        DataGridView1.Rows(iRow).Cells("cusID").Value = oDt.Rows(0).Item(2).ToString
        DataGridView1.Rows(iRow).Cells("cusName").Value = oDt.Rows(0).Item(3).ToString
        DataGridView1.Rows(iRow).Cells("cusAddress").Value = oDt.Rows(0).Item(4).ToString
        DataGridView1.Rows(iRow).Cells("dateDis").Value = oDt.Rows(0).Item(5).ToString
        DataGridView1.Rows(iRow).Cells("disAmt").Value = Format(Val(oDt.Rows(0).Item(6).ToString), "###,###.##")
        DataGridView1.Rows(iRow).Cells("LDOS").Value = Format(Val(oDt.Rows(0).Item(7).ToString), "###,###.##")

        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
        'Dim iRow As Integer = Me.DataGridView1.Rows.Count
        'DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("LDID").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("LDID").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("cusID").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("cusID").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("cusName").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("cusName").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("cusAddress").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("cusAddress").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("dateDis").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("dateDis").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("disAmt").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("disAmt").ReadOnly = True
            DataGridView1.Rows(iRow).Cells("LDOS").Style.BackColor = Color.Yellow
            DataGridView1.Rows(iRow).Cells("LDOS").ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("LDID").ColumnIndex, iRow)
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim LD_ID1 as String= Me.DataGridView1.Rows(iRow).Cells("LDID").Value
     
        If (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
         newRow()
        ElseIf e.KeyCode = Keys.F12 Then
            try
                if Me.DataGridView1.Rows(iRow).Cells("LDID").Value="" and Me.DataGridView1.Rows(iRow).Cells("cusID").Value="" then
                    MessageBox.Show("No record to update, please check again!","No record!",MessageBoxButtons.OK, MessageBoxIcon.Error )
                    Return
                Else 
                    addIn("update Writeoff set LD_ID='"& Me.DataGridView1.Rows(iRow).Cells("LDID").Value &"',WOF_Date='"&Me.DataGridView1.Rows(iRow).Cells("dateWF").Value &"',User_Modify='"& frmMain.users &"',Date_Modify='"& datetime.now() &"' where LD_ID='"& LD_ID1 &"' and BR_ID='"& frmMain.lblCode.text &"'")
                    showWF()  
                    MessageBox.Show("Record has been updated!","Updated",MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
            End Try
   
  

       ElseIf e.KeyCode = Keys.Delete Then
            Dim LD_ID As String = getData("select LD_ID from  Writeoff where LD_ID='" & DataGridView1.Rows(iRow).Cells("LDID").Value & "' and BR_ID='" & frmMain.lblCode.Text & "'")
           if LD_ID=""
               Return
           End If
            If LD_ID = Me.DataGridView1.Rows(iRow).Cells("LDID").Value Then
                result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                If result = "1" Then
                    If LD_ID<>"" Then
                        addIn("delete from Writeoff where LD_ID='"& Me.DataGridView1.Rows(iRow).Cells("LDID").Value &"' and BR_ID ='"& frmMain.lblCode.text &"'")
                        MessageBox.Show("Record has been deleted!","deleted",MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                    Else 
                        Return
                    End If
         
                Else
                    Return

                End If
            End If
           End If
    End Sub



    Private Sub addTraceWF(ByVal LD_ID As Integer, ByVal DateAction As DateTime, ByVal RecordAction As String, ByVal CM_ID As Integer, _
                ByVal BrID As String, ByVal WF_Date As Date, ByVal LD_OD As Double, ByVal User_Create As String, _
                ByVal Date_Create As DateTime, ByVal User_Modify As String, ByVal Date_Modify As DateTime, ByVal User_Delete As String, ByVal Date_Delete As DateTime)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = LD_ID
                .Add("@d1", SqlDbType.DateTime).Value = DateAction
                .Add("@d2", SqlDbType.NVarChar).Value = RecordAction
                .Add("@d3", SqlDbType.Int).Value = LD_ID
                .Add("@d4", SqlDbType.Int).Value = CM_ID
                .Add("@d5", SqlDbType.NVarChar).Value = BrID
                '.Add("@d6", SqlDbType.Date).Value = SH_Date
                '.Add("@d7", SqlDbType.Int).Value = EM_ID
                '.Add("@d8", SqlDbType.NVarChar).Value = LR_Description
                '.Add("@d9", SqlDbType.Float).Value = SH_Total
                '.Add("@d10", SqlDbType.Date).Value = LR_Date
                '.Add("@d11", SqlDbType.Float).Value = LR_Amount
                '.Add("@d12", SqlDbType.Float).Value = LR_Charge
                '.Add("@d13", SqlDbType.Int).Value = LR_Rec_Status
                .Add("@d14", SqlDbType.NVarChar).Value = User_Create
                .Add("@d15", SqlDbType.DateTime).Value = Date_Create
                .Add("@d16", SqlDbType.NVarChar).Value = User_Modify
                .Add("@d17", SqlDbType.DateTime).Value = Date_Modify
                .Add("@d18", SqlDbType.NVarChar).Value = User_Delete
                .Add("@d19", SqlDbType.DateTime).Value = Date_Delete
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
End Class