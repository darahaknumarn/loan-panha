Imports System.Data.SqlClient
Public Class frmLocation
    Private Sub frmLocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        Me.DataGridView1.Rows.Add()
        If Me.Text = "LocationOther" Or Me.Text = "LocationOtherEmployee" Then
            Me.lblLocationID.Text = getData("select top 1 cast(LO_ID as int) LO_ID from BK_LocationOther where LO_BrID='" & frmMain.lblCode.Text & "' order by  cast (LO_ID as Int) desc")
        Else
            Me.lblLocationID.Text = getLastLO_ID().ToString
        End If
        DataGridView1.Rows(0).Cells(0).ReadOnly = True
        DataGridView1.Columns(0).DefaultCellStyle.BackColor = Color.Yellow
        DataGridView1.Rows(0).Cells(0).Value = "Editing"
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If Me.Text = "FromCustomer" Then
            Dim iRows = FrmCustomer.DataGridView1.CurrentCell.RowIndex
            DataGridView1.ClearSelection()
            DataGridView1.Rows(iRow).Cells(1).Value = FrmCustomer.DataGridView1.Rows(iRow).Cells(4).Value
            Me.DataGridView1.CurrentCell = DataGridView1.Rows(iRow).Cells(2)
        Else
            DataGridView1.ClearSelection()
            Me.DataGridView1.CurrentCell = DataGridView1.Rows(iRow).Cells(1)
        End If
    End Sub
    Private Function checkNull()
        Dim a As Boolean
        Dim dg As DataGridView = DataGridView1
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If dg.Rows(iRow).Cells(1).Value Is Nothing Or dg.Rows(iRow).Cells(2).Value Is Nothing Or dg.Rows(iRow).Cells(3).Value Is Nothing Or dg.Rows(iRow).Cells(4).Value Is Nothing _
            Or dg.Rows(iRow).Cells(5).Value Is Nothing Then
            a = False
        Else
            a = True
        End If
        Return a
    End Function
    Private Sub New_Row()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        DataGridView1.Rows.Add()
        Dim last As Integer = Me.DataGridView1.Rows.Count
        iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        DataGridView1.CurrentCell = DataGridView1(1, last - 1)
        DataGridView1(1, last - 1).Selected = True
        DataGridView1(0, last - 1).Value = "Editing"
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
                        Dim cuid As String = ""
                        If Me.Text = "frmLocation" Then
                            cuid = getData("select LO_ID from BK_Location where LO_ID='" & .Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'")
                        Else
                            cuid = getData("select LO_ID from BK_LocationOther where LO_ID='" & .Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'")
                        End If

                        If cuid <> "" Then
                            showLocation()
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
                                    .CurrentCell = DataGridView1(iCol + 1, iRow)
                                Else
                                    resultError = frmMessageError.ShowBoxError("កូដទីកន្លែង សូមបញ្ចូលជាលេខ មិនមែនជាអក្សរទេ។", "បញ្ចូលមិនត្រឹមត្រូវ")
                                    .Rows(iRow).Cells(1).Value = ""
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
                            .CurrentCell = DataGridView1(iCol + 1, iRow)
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError("មិនអាចបញ្ចូលទិន្នន័យបានទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលមិនត្រឹមត្រូវ")
        End Try
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        With Me.DataGridView1.Rows(iRow)
            Dim Check As String = ""
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            ElseIf e.KeyCode = Keys.Delete Then
                If Me.DataGridView1.Rows(iRow).Cells(0).Value = "Saved" Then
                    result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                    If result = "1" Then
                        '-------------------------- Check from BK_Loan
                        If Me.Text = "LocationOther" Or Me.Text = "LocationOtherEmployee" Then
                            Check = getData("Select top 1 LO_ID from BK_CustomerOther where LO_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                        Else
                            Check = getData("Select top 1 LO_ID from BK_Customer where LO_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                        End If
                        '--------------------------------------------------------------- Start and start delete
                        If Check = "" Then
                            addTrace_Location_Main("DELETE")
                            If Me.Text = "LocationOther" Or Me.Text = "LocationOtherEmployee" Then
                                addIn("Delete from BK_LocationOther where LO_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'")
                            Else
                                addIn("Delete from BK_Location where LO_ID='" & Me.DataGridView1.Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'")
                            End If

                            Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                            MessageBox.Show("Delete successfully!")
                        Else
                            resultError = frmMessageError.ShowBoxError("មិនអាចលុបបានទេ ព្រោះកូដតំបន់ត្រូវប្រើបា្រស់រួចហើយ។", "មិនអាចលុប")
                            Return
                        End If
                    Else
                        Return
                    End If
                Else
                    result = frmMessageError.ShowBoxError("មិនអាចលុបបានទេ សូមពិនិត្យឡើងវិញ។", "លុបទិន្និន័យ")
                    Return
                End If
            ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
                newRow()
            ElseIf e.KeyCode = Keys.F12 Then
                If checkNull() = False Then
                    result = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្នន័យខុស សូមពិនត្យឡើងវិញ។", "ការបញ្ចូលខុស")
                    Return
                Else
                    Dim LO_ID As String = ""
                    If Me.Text = "LocationOther" Then
                        LO_ID = getData("select MAX(cast( isnull(LO_ID,0) as int)) from BK_LocationOther where LO_ID='" & .Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'")
                    Else
                        LO_ID = getData("select MAX(cast( isnull(LO_ID,0) as int)) from BK_Location where LO_ID='" & .Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'")
                    End If

                    If LO_ID = "" Then
                        'Save new record
                        addLocation(.Cells(1).Value, .Cells(2).Value, .Cells(3).Value, .Cells(4).Value, .Cells(5).Value, frmMain.lblCode.Text, 1, frmMain.users, DateTime.Now())
                        showLocation()
                        If Me.Text = "LocationOther" Or Me.Text = "LocationOtherEmployee" Then
                            lblLocationID.Text = getData("select top 1 LO_ID from BK_LocationOther order by cast(LO_ID as Int) desc")
                        Else
                            lblLocationID.Text = getLastLO_ID()
                        End If
                        New_Row()
                    Else
                        'Update record
                        addTrace_Location_Main("UPDATE OLD")
                        UpdateLocation(.Cells(2).Value, .Cells(3).Value, .Cells(4).Value, .Cells(5).Value)
                        addTrace_Location_Main("UPDATE NEW")
                        MessageBox.Show("The record has been updated!!!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        New_Row()
                    End If
                End If
            End If
        End With
    End Sub
    Private Sub addTrace_Location_Main(ByVal RecordAction As String)
        Dim LO_ID, LO_Rec_Status As Integer
        Dim VL_ID, CN_ID, DT_ID, PV_ID, LO_BrID, LO_User_Create, LO_User_Modify, LO_User_Delete As String
        Dim DateAction, LO_Date_Create, LO_Date_Modify, LO_Date_Delete As DateTime
        Dim iRow As Integer
        If Me.Text = "FromCustomer" Then
            iRow = FrmCustomer.DataGridView1.CurrentCell.RowIndex
        Else
            iRow = Me.DataGridView1.CurrentCell.RowIndex
        End If
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        If Me.Text = "LocationOther" Or Me.Text = "LocationOtherEmployee" Then
            Str = "select * from BK_Location where LO_ID='" & DataGridView1.Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'"
        Else
            Str = "select * from BK_Location where LO_ID='" & DataGridView1.Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'"
        End If

        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        LO_ID = oDt.Rows(0).Item(0).ToString
        VL_ID = oDt.Rows(0).Item(1).ToString
        CN_ID = oDt.Rows(0).Item(2).ToString
        DT_ID = oDt.Rows(0).Item(3).ToString
        PV_ID = oDt.Rows(0).Item(4).ToString
        LO_BrID = oDt.Rows(0).Item(5).ToString
        LO_Rec_Status = oDt.Rows(0).Item(6).ToString
        LO_User_Create = oDt.Rows(0).Item(7).ToString
        LO_Date_Create = oDt.Rows(0).Item(8).ToString
        LO_User_Modify = oDt.Rows(0).Item(9).ToString
        If Format(oDt.Rows(0).Item(10).ToString, "") = "" Then
            LO_Date_Modify = DateTime.MaxValue.ToString
        Else
            LO_Date_Modify = oDt.Rows(0).Item(10).ToString
        End If
        LO_User_Delete = frmMain.users
        LO_Date_Delete = DateTime.Now
        DateAction = DateTime.Now
        If Me.Text = "LocationOther" Or Me.Text = "LocationOtherEmployee" Then
            If RecordAction = "DELETE" Then
                If LO_Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_LocationOther(DateAction,RecordAction,LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create,LO_User_Delete,LO_Date_Delete) values ('" & DateAction & "','" & RecordAction & "','" & LO_ID & "',N'" & VL_ID & "',N'" & CN_ID & "',N'" & DT_ID & "',N'" & PV_ID & "','" & LO_BrID & "','" & LO_Rec_Status & "','" & LO_User_Create & "','" & LO_Date_Create & "','" & LO_User_Delete & "','" & LO_Date_Delete & "')")
                Else
                    addIn("insert TRACE_LocationOther(DateAction,RecordAction,LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create,LO_User_Modify,LO_Date_Modify,LO_User_Delete,LO_Date_Delete) values ('" & DateAction & "','" & RecordAction & "','" & LO_ID & "',N'" & VL_ID & "',N'" & CN_ID & "',N'" & DT_ID & "',N'" & PV_ID & "','" & LO_BrID & "','" & LO_Rec_Status & "','" & LO_User_Create & "','" & LO_Date_Create & "','" & LO_User_Modify & "','" & LO_Date_Modify & "','" & LO_User_Delete & "','" & LO_Date_Delete & "')")
                End If
            Else
                If LO_Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_LocationOther(DateAction,RecordAction,LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create) values ('" & DateAction & "','" & RecordAction & "','" & LO_ID & "',N'" & VL_ID & "',N'" & CN_ID & "',N'" & DT_ID & "',N'" & PV_ID & "','" & LO_BrID & "','" & LO_Rec_Status & "','" & LO_User_Create & "','" & LO_Date_Create & "')")
                Else
                    addIn("insert TRACE_LocationOther(DateAction,RecordAction,LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create,LO_User_Modify,LO_Date_Modify) values ('" & DateAction & "','" & RecordAction & "','" & LO_ID & "',N'" & VL_ID & "',N'" & CN_ID & "',N'" & DT_ID & "',N'" & PV_ID & "','" & LO_BrID & "','" & LO_Rec_Status & "','" & LO_User_Create & "','" & LO_Date_Create & "','" & LO_User_Modify & "','" & LO_Date_Modify & "')")
                End If
            End If
        Else
            If RecordAction = "DELETE" Then
                If LO_Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Location(DateAction,RecordAction,LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create,LO_User_Delete,LO_Date_Delete) values ('" & DateAction & "','" & RecordAction & "','" & LO_ID & "',N'" & VL_ID & "',N'" & CN_ID & "',N'" & DT_ID & "',N'" & PV_ID & "','" & LO_BrID & "','" & LO_Rec_Status & "','" & LO_User_Create & "','" & LO_Date_Create & "','" & LO_User_Delete & "','" & LO_Date_Delete & "')")
                Else
                    addIn("insert TRACE_Location(DateAction,RecordAction,LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create,LO_User_Modify,LO_Date_Modify,LO_User_Delete,LO_Date_Delete) values ('" & DateAction & "','" & RecordAction & "','" & LO_ID & "',N'" & VL_ID & "',N'" & CN_ID & "',N'" & DT_ID & "',N'" & PV_ID & "','" & LO_BrID & "','" & LO_Rec_Status & "','" & LO_User_Create & "','" & LO_Date_Create & "','" & LO_User_Modify & "','" & LO_Date_Modify & "','" & LO_User_Delete & "','" & LO_Date_Delete & "')")
                End If
            Else
                If LO_Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert TRACE_Location(DateAction,RecordAction,LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create) values ('" & DateAction & "','" & RecordAction & "','" & LO_ID & "',N'" & VL_ID & "',N'" & CN_ID & "',N'" & DT_ID & "',N'" & PV_ID & "','" & LO_BrID & "','" & LO_Rec_Status & "','" & LO_User_Create & "','" & LO_Date_Create & "')")
                Else
                    addIn("insert TRACE_Location(DateAction,RecordAction,LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create,LO_User_Modify,LO_Date_Modify) values ('" & DateAction & "','" & RecordAction & "','" & LO_ID & "',N'" & VL_ID & "',N'" & CN_ID & "',N'" & DT_ID & "',N'" & PV_ID & "','" & LO_BrID & "','" & LO_Rec_Status & "','" & LO_User_Create & "','" & LO_Date_Create & "','" & LO_User_Modify & "','" & LO_Date_Modify & "')")
                End If
            End If
        End If
        oDa.Dispose()
        oDt.Dispose()
        'addTrace_Location(DateAction, RecordAction, LO_ID, VL_ID, CN_ID, DT_ID, PV_ID, LO_BrID, LO_Rec_Status, LO_User_Create, LO_Date_Create, LO_User_Modify, LO_Date_Modify)
    End Sub
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
        Me.DataGridView1.Rows(iRow).Cells(0).Value = "Editing"
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.Text = "frmLocation" Then
            AddToGrid(Me.DataGridView1, 6, "select top 50 'Saved',LO_ID,VL_ID,CN_ID,DT_ID,PV_ID from BK_Location where LO_BrID='" & frmMain.lblCode.Text & "' order by LO_Date_Create desc")
        Else
            AddToGrid(Me.DataGridView1, 6, "select top 50 'Saved',LO_ID,VL_ID,CN_ID,DT_ID,PV_ID from BK_LocationOther where LO_BrID='" & frmMain.lblCode.Text & "' order by LO_Date_Create desc")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.Text = "frmLocation" Then
            AddToGrid(Me.DataGridView1, 6, "select top 100 'Saved',LO_ID,VL_ID,CN_ID,DT_ID,PV_ID from BK_Location where LO_BrID='" & frmMain.lblCode.Text & "' order by LO_Date_Create desc")
        Else
            AddToGrid(Me.DataGridView1, 6, "select top 100 'Saved',LO_ID,VL_ID,CN_ID,DT_ID,PV_ID from BK_LocationOther where LO_BrID='" & frmMain.lblCode.Text & "' order by LO_Date_Create desc")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Me.Text = "frmLocation" Then
            AddToGrid(Me.DataGridView1, 6, "select 'Saved',LO_ID,VL_ID,CN_ID,DT_ID,PV_ID from BK_Location where LO_BrID='" & frmMain.lblCode.Text & "' order by LO_Date_Create desc")
        Else
            AddToGrid(Me.DataGridView1, 6, "select 'Saved',LO_ID,VL_ID,CN_ID,DT_ID,PV_ID from BK_LocationOther where LO_BrID='" & frmMain.lblCode.Text & "' order by LO_Date_Create desc")
        End If
    End Sub
End Class