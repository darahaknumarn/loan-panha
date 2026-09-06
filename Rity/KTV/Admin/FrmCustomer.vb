Imports System.Data.SqlClient
Public Class FrmCustomer
    Private Sub FrmCustomer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        frmDisburshment.callLast()
    End Sub
    Private Sub FrmCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid1(DataGridView1)
        DataGridView1.Rows.Add()
        DataGridView1.Rows(0).Cells(0).ReadOnly = True
        DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Rows(0).Cells(5).ReadOnly = True
        DataGridView1.Columns(5).DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Rows(0).Cells(6).ReadOnly = True
        DataGridView1.Columns(6).DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Rows(0).Cells(0).Value = "Editing"
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If Me.Text = "CustomerOther" Then
            lblCustomerID.Text = getLastCM_ID().ToString
            lblLoID.Text = getLastLO_ID().ToString
        Else
            lblCustomerID.Text = getLastCM_ID().ToString
            lblLoID.Text = getLastLO_ID().ToString
        End If

        'Me.DataGridView1.SelectedRows.Item = Nothing
        If Me.Text = "FromDisbursh" Then
            Dim iRows = frmDisburshment.DataGridView1.CurrentCell.RowIndex
            DataGridView1.ClearSelection()
            DataGridView1.Rows(iRow).Cells(1).Value = frmDisburshment.DataGridView1.Rows(iRow).Cells(4).Value
            Me.DataGridView1.CurrentCell = DataGridView1.Rows(iRow).Cells(2)
        Else
            Dim iRows = Me.DataGridView1.CurrentCell.RowIndex
            DataGridView1.ClearSelection()
            Me.DataGridView1.Rows(0).Cells(1).Selected = True
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
                        Dim cuid As String = getData("select 'Saved',CM_ID,CM_KhName,CM_Phone,a.LO_ID,b.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID 'Address',LD_Cycle from BK_Customer a inner join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID where a.CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and Status='Active' and a.CM_BrId ='" & frmMain.lblCode.Text & "'order by CM_Date_Create desc")
                        If cuid <> "" Then
                            showCustomer()
                            Return
                        Else
                            If .Rows(iRow).Cells(1).Value Is Nothing Then
                                Return
                            Else
                                If Val(.Rows(iRow).Cells(1).Value) / Val(.Rows(iRow).Cells(1).Value) = 1 Then
                                    .Rows(iRow).Cells(0).Value = "Editing"
                                    .Rows(iRow).Cells(2).Value = ""
                                    .Rows(iRow).Cells(3).Value = ""
                                    .Rows(iRow).Cells(4).Value = ""
                                    .Rows(iRow).Cells(5).Value = ""
                                    .Rows(iRow).Cells(6).Value = ""
                                    .CurrentCell = DataGridView1(iCol + 1, iRow)
                                Else
                                    resultError = frmMessageError.ShowBoxError("កូដអតិថិជនសូមបញ្ចូលជាលេខ មិនមែនជាអក្សរទេ។", "បញ្ចូលមិនត្រឹមត្រូវ")
                                    Return
                                End If
                            End If
                        End If
                    ElseIf .CurrentCell.ColumnIndex = 2 Then
                        If .Rows(iRow).Cells(2).Value Is Nothing Then
                            Return
                        Else
                            .CurrentCell = DataGridView1(iCol + 1, iRow)
                        End If
                    ElseIf .CurrentCell.ColumnIndex = 3 Then
                        If .Rows(iRow).Cells(3).Value Is Nothing Then
                            Return
                        Else
                            .CurrentCell = DataGridView1(iCol + 1, iRow)
                        End If
                    ElseIf .CurrentCell.ColumnIndex = 4 Then
                        If .Rows(iRow).Cells(4).Value Is Nothing Then
                            Return
                        Else
                            Dim address As String = ""
                            If Me.Text = "CustomerOther" Then
                                address = getData("select VL_ID+','+CN_ID+','+DT_ID+','+PV_ID from BK_LocationOther where LO_ID='" & .Rows(iRow).Cells(4).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'")
                            Else
                                address = getData("select VL_ID+','+CN_ID+','+DT_ID+','+PV_ID from BK_Location where LO_ID='" & .Rows(iRow).Cells(4).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'")
                            End If
                            If address = "" Then
                                If Me.Text = "CustomerOther" Then
                                    frmLocation.Text = "LocationOther"
                                Else
                                    frmLocation.Text = "FromCustomer"
                                End If

                                frmLocation.MdiParent = frmMain
                                frmLocation.WindowState = FormWindowState.Maximized
                                frmLocation.Show()
                            Else
                                .Rows(iRow).Cells(5).Value = address
                            End If
                            .CurrentCell = DataGridView1(iCol + 2, iRow)
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError("មិនអាចបញ្ចូលទិន្នន័យបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលមិនត្រឹមត្រូវ")
        End Try
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.F12 Then
            '--------------------------------------------------------------- Save new customer
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If Me.DataGridView1.Rows(iRow).Cells(6).Value = "" Then
                Dim a As Integer = checkNull()
                If a = 1 Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្ន័យខុសមិនអាចរក្សាទុកបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    Return
                Else
                    If Me.Text = "CustomerOther" Then
                        Dim ID As String = getData("Select CM_ID from BK_CustomerOther where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                        If ID = "" Then
                            addCustomer(Me.DataGridView1.Rows(iRow).Cells(1).Value, Me.DataGridView1.Rows(iRow).Cells(2).Value, Me.DataGridView1.Rows(iRow).Cells(4).Value, Me.DataGridView1.Rows(iRow).Cells(3).Value, frmMain.lblCode.Text, 1, frmMain.users.ToString, DateTime.Now())
                            lblCustomerID.Text = getData("select top 1 CM_ID from BK_CustomerOther where CM_BrId='" & frmMain.lblCode.Text & "' order by Cast(CM_ID as int) desc")
                            lblLoID.Text = getData("select top 1 LO_ID from BK_LocationOther where LO_BrID='" & frmMain.lblCode.Text & "' order by Cast(LO_ID as int) desc")
                            showCustomer()
                            DataGridView1.Rows.Add()
                            Dim last As Integer = Me.DataGridView1.Rows.Count
                            iRow = Me.DataGridView1.CurrentCell.RowIndex
                            Dim iCol = DataGridView1.CurrentCell.ColumnIndex
                            DataGridView1.CurrentCell = DataGridView1(1, last - 1)
                            DataGridView1(1, last - 1).Selected = True
                            DataGridView1(0, last - 1).Value = "Editing"
                        Else
                            resultError = frmMessageError.ShowBoxError("លេខកូដនេះបានប្រើរូចហើយ បើសិនជាចង់យកលេខចាស់មកប្រើសូមចូលទៅកាន់ 'យកលេខអតិថិជនចាស់មកប្រើ'។", "ការបញ្ចូលទិន្នន័យខុស")
                            Return
                        End If
                    Else
                        Dim ID As String = getData("Select CM_ID from BK_Customer where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                        If ID = "" Then
                            addCustomer(Me.DataGridView1.Rows(iRow).Cells(1).Value, Me.DataGridView1.Rows(iRow).Cells(2).Value, Me.DataGridView1.Rows(iRow).Cells(4).Value, Me.DataGridView1.Rows(iRow).Cells(3).Value, frmMain.lblCode.Text, 1, frmMain.users.ToString, DateTime.Now())
                            getLastCM_ID()
                            getLastLO_ID()
                            showCustomer()
                            DataGridView1.Rows.Add()
                            Dim last As Integer = Me.DataGridView1.Rows.Count
                            iRow = Me.DataGridView1.CurrentCell.RowIndex
                            Dim iCol = DataGridView1.CurrentCell.ColumnIndex
                            DataGridView1.CurrentCell = DataGridView1(1, last - 1)
                            DataGridView1(1, last - 1).Selected = True
                            DataGridView1(0, last - 1).Value = "Editing"
                        Else
                            resultError = frmMessageError.ShowBoxError("លេខកូដនេះបានប្រើរូចហើយ បើសិនជាចង់យកលេខចាស់មកប្រើសូមចូលទៅកាន់ 'យកលេខអតិថិជនចាស់មកប្រើ'។", "ការបញ្ចូលទិន្នន័យខុស")
                            Return
                        End If
                    End If
                    lblCustomerID.Text = getLastCM_ID().ToString
                    lblLoID.Text = getLastLO_ID().ToString
                End If
            Else
                '--------------------- Update customer
                If checkNull() = 1 Then
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្ន័យខ្វះមិនអាចកែរប្រែបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខ្វះ")
                    Return
                Else
                    If NoRecordChange() = 1 Then
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យដូចដើមគ្មានអ្វីកែរប្រែ។", "គ្មានការកែរប្រែ")
                        Return
                    Else
                        Dim ID As Integer = Val(getData("Select ID from BK_Customer where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and CM_BrId='" & frmMain.lblCode.Text & "' and Status='Active'"))
                        AddTrace_Customer("UPDATE OLD", ID)
                        UpdateCustomer(DataGridView1.Rows(iRow).Cells(2).Value, DataGridView1.Rows(iRow).Cells(4).Value, DataGridView1.Rows(iRow).Cells(3).Value, frmMain.users.ToString, DateTime.Now())
                        AddTrace_Customer("UPDATE NEW", ID)
                        showCustomer()
                        newRow()
                        resultError = frmMessageError.ShowBoxError("អតិថិជនបានធ្វើការកែរប្រែរួចរាល់។", "កែរប្រែ")
                    End If
                End If
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            DataGridView1.Rows.Add()
            Dim last As Integer = Me.DataGridView1.Rows.Count
            iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim iCol = DataGridView1.CurrentCell.ColumnIndex
            DataGridView1.CurrentCell = DataGridView1(1, last - 1)
            DataGridView1(1, last - 1).Selected = True
            DataGridView1(0, last - 1).Value = "Editing"
        ElseIf e.KeyCode = Keys.F11 Then
            ToExcel(DataGridView1)
        ElseIf e.KeyCode = Keys.Delete Then
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If Me.DataGridView1.Rows(iRow).Cells(6).Value = "" Then
                Return
            Else
                Dim ID As Integer = 0
                Dim status As String = ""
                If Me.Text = "CustomerOther" Then
                    ID = Val(getData("Select ID from BK_CustomerOther where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and Status ='Active' and CM_BrId='" & frmMain.lblCode.Text & "'"))
                    status = getData("select top 1 LD_ID from OtherDeposit where CM_ID1='" & ID & "' and LD_BrId='" & frmMain.lblCode.Text & "' order by LD_Date_Create desc")
                Else
                    ID = Val(getData("Select ID from BK_Customer where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and Status ='Active' and CM_BrId='" & frmMain.lblCode.Text & "'"))
                    status = getData("select top 1 LD_ID from BK_Loan where CM_ID1='" & ID & "' and LD_BrId='" & frmMain.lblCode.Text & "' order by LD_Date_Create desc")
                End If
                If status = "" Then
                    result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                    If result = "1" Then
                        AddTrace_Customer("DELETE", ID)
                        If Me.Text = "CustomerOther" Then
                            addIn("Delete from BK_CustomerOther where ID='" & ID & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                        Else
                            addIn("Delete from BK_Customer where ID='" & ID & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                        End If
                        resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុប")
                        DataGridView1.Rows.Remove(DataGridView1.Rows(iRow))
                    End If
                Else
                    resultError = frmMessageError.ShowBoxError("លុបមិនបានទេព្រោះអតិថិជនមាននៅក្នុងលេខកិច្ចសន្យាលេខ " & status & "។", "លុប")
                    Return
                End If
            End If
        End If
    End Sub
    '----------------------------------------------- Function and Method
    Public Sub AddTrace_Customer(ByVal RecordAction As String, ByVal ID As Integer)
        Dim CM_KhName, CM_Address, CM_Phone, CM_BrId, CM_User_Create, CM_User_Modify, CM_User_Delete, Status As String
        Dim CM_ID, LO_ID, LD_Cycle, ID1 As Integer
        Dim CM_Rec_Status As Boolean
        Dim DateAction, CM_Date_Create, CM_Date_Modify, CM_Date_Delete, Date_Change As DateTime
        Try
            Dim oDt As New System.Data.DataTable
            Dim Str As String = ""
            If Me.Text = "CustomerOther" Then
                Str = "select top 1 * from BK_CustomerOther where ID='" & ID & "' and CM_BrId='" & frmMain.lblCode.Text & "'"
            Else
                Str = "select top 1 * from BK_Customer where ID='" & ID & "' and CM_BrId='" & frmMain.lblCode.Text & "'"
            End If
            'On Error Resume Next
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DateAction = DateTime.Now
            '--- RecordAction
            CM_ID = oDt.Rows(0).Item(0).ToString
            CM_KhName = oDt.Rows(0).Item(1).ToString
            LO_ID = oDt.Rows(0).Item(2).ToString
            CM_Address = oDt.Rows(0).Item(3).ToString
            CM_Phone = oDt.Rows(0).Item(4).ToString
            CM_BrId = oDt.Rows(0).Item(5).ToString
            CM_Rec_Status = oDt.Rows(0).Item(6).ToString
            CM_User_Create = oDt.Rows(0).Item(7).ToString
            CM_Date_Create = oDt.Rows(0).Item(8).ToString
            CM_User_Modify = oDt.Rows(0).Item(9).ToString
            If Format(oDt.Rows(0).Item(10).ToString, "") = "" Then
                CM_Date_Modify = DateTime.MaxValue.ToString
            Else
                CM_Date_Modify = oDt.Rows(0).Item(10).ToString
            End If
            CM_User_Delete = frmMain.lblCode.Text
            CM_Date_Delete = DateTime.Now
            LD_Cycle = oDt.Rows(0).Item(13).ToString
            ID1 = oDt.Rows(0).Item(14).ToString
            Status = oDt.Rows(0).Item(15).ToString
            If Format(oDt.Rows(0).Item(16).ToString, "") = "" Then
                Date_Change = DateTime.MaxValue.ToString
            Else
                Date_Change = oDt.Rows(0).Item(16).ToString
            End If
            'Date_Change = oDt.Rows(0).Item(16).ToString
            If Me.Text = "CustomerOther" Then
                If RecordAction = "DELETE" Then
                    If CM_Date_Modify = DateTime.MaxValue.ToString Then
                        addIn("insert TRACE_CustomerOther(DateAction,RecordAction,CM_ID,CM_KhName,LO_ID,CM_Address,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,CM_User_Delete,CM_Date_Delete,LD_Cycle,ID,Status,Date_Change) Values('" & DateAction & "','" & RecordAction & "','" & CM_ID & "','" & CM_KhName & "','" & LO_ID & "','" & CM_Address & "','" & CM_Phone & "',N'" & CM_BrId & "',N'" & CM_Rec_Status & "','" & CM_User_Create & "','" & CM_Date_Create & "','" & CM_User_Delete & "','" & CM_Date_Delete & "','" & LD_Cycle & "' ,'" & ID1 & "','" & Status & "','" & Date_Change & "')")
                    Else
                        addIn("insert TRACE_CustomerOther(DateAction,RecordAction,CM_ID,CM_KhName,LO_ID,CM_Address,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,CM_User_Modify,CM_Date_Modify,CM_User_Delete,CM_Date_Delete,LD_Cycle,ID,Status,Date_Change) Values('" & DateAction & "','" & RecordAction & "','" & CM_ID & "','" & CM_KhName & "','" & LO_ID & "','" & CM_Address & "','" & CM_Phone & "',N'" & CM_BrId & "',N'" & CM_Rec_Status & "','" & CM_User_Create & "','" & CM_Date_Create & "','" & CM_User_Modify & "','" & CM_Date_Modify & "','" & CM_User_Delete & "','" & CM_Date_Delete & "','" & LD_Cycle & "' ,'" & ID1 & "','" & Status & "','" & Date_Change & "')")
                    End If
                Else
                    If CM_Date_Modify = DateTime.MaxValue.ToString Then
                        addIn("insert TRACE_CustomerOther(DateAction,RecordAction,CM_ID,CM_KhName,LO_ID,CM_Address,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,LD_Cycle,ID,Status,Date_Change) Values('" & DateAction & "','" & RecordAction & "','" & CM_ID & "','" & CM_KhName & "','" & LO_ID & "','" & CM_Address & "','" & CM_Phone & "',N'" & CM_BrId & "',N'" & CM_Rec_Status & "','" & CM_User_Create & "','" & CM_Date_Create & "','" & LD_Cycle & "' ,'" & ID1 & "','" & Status & "','" & Date_Change & "')")
                    Else
                        addIn("insert TRACE_CustomerOther(DateAction,RecordAction,CM_ID,CM_KhName,LO_ID,CM_Address,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,CM_User_Modify,CM_Date_Modify,LD_Cycle,ID,Status,Date_Change) Values('" & DateAction & "','" & RecordAction & "','" & CM_ID & "','" & CM_KhName & "','" & LO_ID & "','" & CM_Address & "','" & CM_Phone & "',N'" & CM_BrId & "',N'" & CM_Rec_Status & "','" & CM_User_Create & "','" & CM_Date_Create & "','" & CM_User_Modify & "','" & CM_Date_Modify & "','" & LD_Cycle & "' ,'" & ID1 & "','" & Status & "','" & Date_Change & "')")
                    End If
                End If
            Else
                If RecordAction = "DELETE" Then
                    If CM_Date_Modify = DateTime.MaxValue.ToString Then
                        addIn("insert TRACE_Customer(DateAction,RecordAction,CM_ID,CM_KhName,LO_ID,CM_Address,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,CM_User_Delete,CM_Date_Delete,LD_Cycle,ID,Status,Date_Change) Values('" & DateAction & "','" & RecordAction & "','" & CM_ID & "','" & CM_KhName & "','" & LO_ID & "','" & CM_Address & "','" & CM_Phone & "',N'" & CM_BrId & "',N'" & CM_Rec_Status & "','" & CM_User_Create & "','" & CM_Date_Create & "','" & CM_User_Delete & "','" & CM_Date_Delete & "','" & LD_Cycle & "' ,'" & ID1 & "','" & Status & "','" & Date_Change & "')")
                    Else
                        addIn("insert TRACE_Customer(DateAction,RecordAction,CM_ID,CM_KhName,LO_ID,CM_Address,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,CM_User_Modify,CM_Date_Modify,CM_User_Delete,CM_Date_Delete,LD_Cycle,ID,Status,Date_Change) Values('" & DateAction & "','" & RecordAction & "','" & CM_ID & "','" & CM_KhName & "','" & LO_ID & "','" & CM_Address & "','" & CM_Phone & "',N'" & CM_BrId & "',N'" & CM_Rec_Status & "','" & CM_User_Create & "','" & CM_Date_Create & "','" & CM_User_Modify & "','" & CM_Date_Modify & "','" & CM_User_Delete & "','" & CM_Date_Delete & "','" & LD_Cycle & "' ,'" & ID1 & "','" & Status & "','" & Date_Change & "')")
                    End If
                Else
                    If CM_Date_Modify = DateTime.MaxValue.ToString Then
                        addIn("insert TRACE_Customer(DateAction,RecordAction,CM_ID,CM_KhName,LO_ID,CM_Address,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,LD_Cycle,ID,Status,Date_Change) Values('" & DateAction & "','" & RecordAction & "','" & CM_ID & "','" & CM_KhName & "','" & LO_ID & "','" & CM_Address & "','" & CM_Phone & "',N'" & CM_BrId & "',N'" & CM_Rec_Status & "','" & CM_User_Create & "','" & CM_Date_Create & "','" & LD_Cycle & "' ,'" & ID1 & "','" & Status & "','" & Date_Change & "')")
                    Else
                        addIn("insert TRACE_Customer(DateAction,RecordAction,CM_ID,CM_KhName,LO_ID,CM_Address,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,CM_User_Modify,CM_Date_Modify,LD_Cycle,ID,Status,Date_Change) Values('" & DateAction & "','" & RecordAction & "','" & CM_ID & "','" & CM_KhName & "','" & LO_ID & "','" & CM_Address & "','" & CM_Phone & "',N'" & CM_BrId & "',N'" & CM_Rec_Status & "','" & CM_User_Create & "','" & CM_Date_Create & "','" & CM_User_Modify & "','" & CM_Date_Modify & "','" & LD_Cycle & "' ,'" & ID1 & "','" & Status & "','" & Date_Change & "')")
                    End If
                End If
            End If
            oDa.Dispose()
            oDt.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Sub AddCustom()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = ""
                .Add("@d1", SqlDbType.NVarChar).Value = ""
                .Add("@d2", SqlDbType.NVarChar).Value = ""
            End With
            sql = "insert tblcustom(customid,customname,customadd) values (@d0,@d1,@d2)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Function NoRecordChange()
        Dim ID As Integer = 0
        Dim Str As String = ""
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If Me.Text = "FrmCustomer" Then
            ID = Val(getData("Select ID from BK_Customer where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and Status ='Active' and CM_BrId='" & frmMain.lblCode.Text & "'"))
        Else
            ID = Val(getData("Select ID from BK_CustomerOther where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and Status ='Active' and CM_BrId='" & frmMain.lblCode.Text & "'"))
        End If

        'Dim sql As String
        Dim oDt As New System.Data.DataTable
        If Me.Text = "FrmCustomer" Then
            Str = "select CM_KhName,LO_ID,CM_Phone from BK_Customer where ID='" & ID & "' and CM_BrId='" & frmMain.lblCode.Text & "'"
        Else
            Str = "select CM_KhName,LO_ID,CM_Phone from BK_CustomerOther where ID='" & ID & "' and CM_BrId='" & frmMain.lblCode.Text & "'"
        End If

        'On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim CM_Name As String = oDt.Rows(0).Item(0).ToString
        Dim LO_ID As Integer = oDt.Rows(0).Item(1).ToString
        Dim CM_Phone As String = oDt.Rows(0).Item(2).ToString
        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            If CM_Name = .Cells(2).Value And CM_Phone = .Cells(3).Value And LO_ID = .Cells(4).Value Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(1).Style.BackColor = Color.WhiteSmoke
        DataGridView1.Rows(iRow).Cells(1).ReadOnly = False
        DataGridView1.Rows(iRow).Cells(5).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(5).ReadOnly = True
        DataGridView1.Rows(iRow).Cells(6).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(6).ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
    End Sub
    Private Function checkNull()
        Dim a As Integer
        Dim dg As DataGridView = DataGridView1
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If dg.Rows(iRow).Cells(1).Value Is Nothing Or dg.Rows(iRow).Cells(2).Value Is Nothing Or dg.Rows(iRow).Cells(3).Value Is Nothing Or dg.Rows(iRow).Cells(4).Value Is Nothing _
            Or dg.Rows(iRow).Cells(5).Value Is Nothing Then
            a = 1
        Else
            a = 2
        End If
        Return a
    End Function
    Private Sub showCustomer()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        If Me.Text = "CustomerOther" Then
            Str = "select 'Saved',CM_ID,CM_KhName,CM_Phone,a.LO_ID,b.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID 'Address',LD_Cycle from BK_CustomerOther a inner join BK_LocationOther b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID where a.CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and Status='Active' and a.CM_BrId ='" & frmMain.lblCode.Text & "'order by CM_Date_Create desc"
        Else
            Str = "select 'Saved',CM_ID,CM_KhName,CM_Phone,a.LO_ID,b.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID 'Address',LD_Cycle from BK_Customer a inner join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID where a.CM_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and Status='Active' and a.CM_BrId ='" & frmMain.lblCode.Text & "'order by CM_Date_Create desc"
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
        'Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
        Dim iRow1 As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow1).Cells(1).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow1).Cells(1).ReadOnly = True
    End Sub
End Class