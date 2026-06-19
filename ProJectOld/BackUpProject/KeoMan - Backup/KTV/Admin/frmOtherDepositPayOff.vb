Imports System.Data.SqlClient

Public Class frmOtherDepositPayOff

    Private Sub frmOtherDepositPayOff_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        newRow()
    End Sub
    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Try
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            Dim iCol = DataGridView1.CurrentCell.ColumnIndex
            Dim staffName As String = ""
            If iRow < DataGridView1.Rows.Count - 1 Then
                SendKeys.Send("{up}")
            End If
            If Me.DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("LD_ID").ColumnIndex Then
                If DataGridView1.CurrentCell.Value Is Nothing Then
                    Return
                Else
                    Dim LD_ID_Off As String = getData("select top 1 LD_ID from BK_OtherDepositPayoff where LD_ID=" & DataGridView1.CurrentCell.Value.ToString & " and LD_BrId=" & frmMain.lblCode.Text)
                    Dim LD_ID_Other As String = getData("select top 1 LD_ID from OtherDeposit where LD_ID=" & DataGridView1.CurrentCell.Value.ToString & " and LD_BrId=" & frmMain.lblCode.Text)
                    If LD_ID_Off = "" And LD_ID_Other = "" Then
                        DataGridView1.CurrentCell.Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("CM_ID").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("EM_ID").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("EM_Name").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("CM_ID").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("CM_Name").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("CM_Phone").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("CM_Address").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("CU_ID").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("Amount").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("Int_Rate").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("LD_Date").Value = ""
                        MessageBox.Show("Your enter loan id not exist, check again!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf LD_ID_Off = "" And LD_ID_Other <> "" Then
                        ShowOthersPayoff(Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value)
                        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("Date_Payoff").ColumnIndex, iRow)
                    Else
                        ShowPayoff(Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value)
                    End If
                End If
            ElseIf DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("Date_Payoff").ColumnIndex Then
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
                    If Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value = "" Then
                        Return
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value <> "" And Me.DataGridView1.Rows(iRow).Cells("CM_ID").Value <> "" And Me.DataGridView1.Rows(iRow).Cells("EM_ID").Value <> "" Then
                        If Me.DataGridView1.Rows(iRow).Cells(0).Value = "Saved" Then
                            If MessageBox.Show("Do you want to edit this record?", "Saving record", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                EditOtherPayoff()
                                MessageBox.Show("Record has been updated, thank you!", "Record updated", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        Else
                            If MessageBox.Show("Do you want to save this record?", "Saving record", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = Windows.Forms.DialogResult.Yes Then
                                saveOtherPayoff()
                                MessageBox.Show("Record has been saved, thank you!", "Record Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else

                            End If
                        End If
               
                    End If
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("Wrong format please check again!", "Not Date")
                    DataGridView1.Rows(iRow).Cells("LD_Date").Value = ""
                    Return
                End Try
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Need IT Now", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Try
            If Me.DataGridView1.Rows.Count = 0 Then
                Me.newRow()
            End If
            Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
            If e.KeyCode = Keys.F12 Then
                If Me.DataGridView1.Rows(iRow).Cells("No").Value = "Edit" Then
                    If Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value = "" Or Me.DataGridView1.Rows(iRow).Cells("Date_Payoff").Value = "" Then
                        MessageBox.Show("Not enough information to save this record, please check again.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    ElseIf Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value <> "" And Me.DataGridView1.Rows(iRow).Cells("Date_Payoff").Value <> "" And Me.DataGridView1.Rows(iRow).Cells("EM_ID").Value = "" And Me.DataGridView1.Rows(iRow).Cells("CM_ID").Value = "" Then
                        saveOtherPayoff()
                        ShowPayoff(Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value)
                        newRow()
                    End If
                Else
                    If Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value <> "" And Me.DataGridView1.Rows(iRow).Cells("Date_Payoff").Value <> "" Then
                        addIn("update BK_OtherDepositPayoff set Date_Payoff='" & Me.DataGridView1.Rows(iRow).Cells("Date_Payoff").Value & "' where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                        MessageBox.Show("Record has been updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ShowPayoff(Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value)
                    Else
                        MessageBox.Show("Not enough information for update this record, please check before save again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
            ElseIf e.KeyCode = Keys.Delete Then
                Dim a As Integer = Me.DataGridView1.Rows.Count()
                If a = 0 Then
                    MessageBox.Show("No data to delete, please check again.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Else
                    'Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
                    '--------------------------------------------------- Check Loan in repay or not
                    If MessageBox.Show("Are you sure to delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                        'AddTrace_Other("DELETE", Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value)
                        addIn("delete from BK_OtherDepositPayoff where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                        Me.DataGridView1.Rows.Remove(Me.DataGridView1.Rows(iRow))
                        MessageBox.Show("Record has been deleted!!!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            ElseIf (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
                newRow()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub EditOtherPayoff()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d3", SqlDbType.Date).Value = DataGridView1.Rows(iRow).Cells("Date_Payoff").Value
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d5", SqlDbType.DateTime).Value = DateTime.Now
            End With
            sql = "update BK_OtherDepositPayoff set Date_Payoff=@d3,Date_Modify=@d5,User_Modify=@d4 where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub ShowPayoff(ByVal id As String)
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select 'Saved',a.LD_ID,e.EM_ID,b.EM_Name,a.CM_ID,c.CM_KhName,c.CM_Phone,d.VL_ID +','+ CN_ID +','+ DT_ID +','+ PV_ID 'Address',CU_ID,LD_Dis_Amt,LD_IntRate,convert( varchar(12),e.LD_Dis_Date,101) LD_Dis_Date,convert( varchar(12),a.Date_Payoff,101) Date_Payoff " _
& "from BK_OtherDepositPayoff a " _
& " inner join OtherDeposit e on a.LD_BrId=e.LD_BrId and a.LD_ID=e.LD_ID" _
& " left join BK_Employee b on e.EM_ID=b.EM_ID and e.LD_BrId=b.EM_BrID" _
& " left join BK_CustomerOther c on e.CM_ID=c.CM_ID and e.LD_BrId=c.CM_BrId" _
& " left join BK_LocationOther d on c.LO_ID=d.LO_ID and c.CM_BrId=d.LO_BrID" _
& " where  a.LD_BrId like '" & frmMain.lblCode.Text & "' and a.LD_ID = '" & id & "'"

        On Error Resume Next
        oDt.Clear()
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        With Me.DataGridView1.Rows(iRow)
            .Cells(0).Value = oDt.Rows(0).Item(0).ToString
            .Cells("LD_ID").Value = oDt.Rows(0).Item(1).ToString
            .Cells("EM_ID").Value = oDt.Rows(0).Item(2).ToString
            .Cells("EM_Name").Value = oDt.Rows(0).Item(3).ToString
            .Cells("CM_ID").Value = oDt.Rows(0).Item(4).ToString
            .Cells("CM_Name").Value = oDt.Rows(0).Item(5).ToString
            .Cells("CM_Phone").Value = oDt.Rows(0).Item(6).ToString
            .Cells("CM_Address").Value = oDt.Rows(0).Item(7).ToString
            .Cells("CU_ID").Value = oDt.Rows(0).Item(8).ToString
            .Cells("Amount").Value = oDt.Rows(0).Item(9).ToString
            .Cells("Int_Rate").Value = oDt.Rows(0).Item(10).ToString
            .Cells("LD_Date").Value = oDt.Rows(0).Item(11).ToString
            .Cells("Date_Payoff").Value = oDt.Rows(0).Item(12).ToString
            Dim as1 As Double = .Cells("Amount").Value
            .Cells("Amount").Value = Format(as1, "###,###.##")
            oDa.Dispose()
            oDt.Dispose()
            Dim iRow1 As Integer = Me.DataGridView1.Rows.Count - 1
            .Cells("No").Style.BackColor = Color.Yellow
            .Cells("No").ReadOnly = True
            .Cells("LD_ID").Style.BackColor = Color.Yellow
            .Cells("LD_ID").ReadOnly = True
            .Cells("EM_Name").Style.BackColor = Color.Yellow
            .Cells("EM_Name").ReadOnly = True
            .Cells("CM_Name").Style.BackColor = Color.Yellow
            .Cells("CM_Name").ReadOnly = True
            .Cells("CM_Phone").Style.BackColor = Color.Yellow
            .Cells("CM_Phone").ReadOnly = True
            .Cells("CM_Address").Style.BackColor = Color.Yellow
            .Cells("CM_Address").ReadOnly = True
            Me.DataGridView1.CurrentCell = Me.DataGridView1(1, iRow1)
        End With
    End Sub
    Public Sub ShowOthersPayoff(ByVal id As String)
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select 'Edit',LD_ID,a.EM_ID,b.EM_Name,a.CM_ID,c.CM_KhName,c.CM_Phone,d.VL_ID +','+ CN_ID +','+ DT_ID +','+ PV_ID 'Address',CU_ID,LD_Dis_Amt,LD_IntRate,convert( varchar(12),a.LD_Dis_Date,101) LD_Dis_Date from OtherDeposit a " _
                         & "left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID " _
                         & "left join BK_CustomerOther c on a.CM_ID=c.CM_ID and a.LD_BrId=c.CM_BrId " _
                         & "left join BK_LocationOther d on c.LO_ID=d.LO_ID and c.CM_BrId=d.LO_BrID " _
                         & " where  LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID = '" & id & "'"

        On Error Resume Next
        oDt.Clear()
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        With Me.DataGridView1.Rows(iRow)
            .Cells(0).Value = oDt.Rows(0).Item(0).ToString
            .Cells("LD_ID").Value = oDt.Rows(0).Item(1).ToString
            .Cells("EM_ID").Value = oDt.Rows(0).Item(2).ToString
            .Cells("EM_Name").Value = oDt.Rows(0).Item(3).ToString
            .Cells("CM_ID").Value = oDt.Rows(0).Item(4).ToString
            .Cells("CM_Name").Value = oDt.Rows(0).Item(5).ToString
            .Cells("CM_Phone").Value = oDt.Rows(0).Item(6).ToString
            .Cells("CM_Address").Value = oDt.Rows(0).Item(7).ToString
            .Cells("CU_ID").Value = oDt.Rows(0).Item(8).ToString
            .Cells("Amount").Value = oDt.Rows(0).Item(9).ToString
            .Cells("Int_Rate").Value = oDt.Rows(0).Item(10).ToString
            .Cells("LD_Date").Value = oDt.Rows(0).Item(11).ToString
            Dim as1 As Double = .Cells("Amount").Value
            .Cells("Amount").Value = Format(as1, "###,###.##")
            oDa.Dispose()
            oDt.Dispose()
            Dim iRow1 As Integer = Me.DataGridView1.Rows.Count - 1
            .Cells("No").Style.BackColor = Color.Yellow
            .Cells("No").ReadOnly = True
            '.Cells("LD_ID").Style.BackColor = Color.Yellow
            '.Cells("LD_ID").ReadOnly = True
            .Cells("EM_Name").Style.BackColor = Color.Yellow
            .Cells("EM_Name").ReadOnly = True
            .Cells("CM_Name").Style.BackColor = Color.Yellow
            .Cells("CM_Name").ReadOnly = True
            .Cells("CM_Phone").Style.BackColor = Color.Yellow
            .Cells("CM_Phone").ReadOnly = True
            .Cells("CM_Address").Style.BackColor = Color.Yellow
            .Cells("CM_Address").ReadOnly = True
            Me.DataGridView1.CurrentCell = Me.DataGridView1(1, iRow1)
        End With
    End Sub
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells("No").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("No").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("EM_ID").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("EM_ID").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("EM_Name").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("EM_Name").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("CM_ID").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("CM_ID").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("CM_Name").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("CM_Name").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("CM_Phone").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("CM_Phone").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("CM_Address").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("CM_Address").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("CU_ID").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("CU_ID").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("Amount").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("Amount").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("Int_Rate").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("Int_Rate").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("LD_Date").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("LD_Date").ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
        DataGridView1.Rows(iRow).Cells("No").Value = "Editing"
    End Sub
    Private Sub saveOtherPayoff()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value
                .Add("@d1", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d2", SqlDbType.Int).Value = DataGridView1.Rows(iRow).Cells("CM_ID").Value
                .Add("@d3", SqlDbType.Date).Value = DataGridView1.Rows(iRow).Cells("Date_Payoff").Value
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d5", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d6", SqlDbType.Int).Value = 0
            End With
            sql = "insert into BK_OtherDepositPayoff(LD_ID,Date_Payoff,LD_BrId,CM_ID,Date_Create,User_Create,IsExport) values (@d0,@d3,@d1,@d2,@d5,@d4,@d6)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub

    Public Sub ShowOthers()
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select 'Saved',LD_ID,a.EM_ID,b.EM_Name,a.CM_ID,c.CM_KhName,c.CM_Phone,d.VL_ID +','+ CN_ID +','+ DT_ID +','+ PV_ID 'Address',CU_ID,LD_Dis_Amt,LD_IntRate,convert( varchar(12),a.LD_Dis_Date,101) LD_Dis_Date from OtherDeposit a " _
                         & "left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID " _
                         & "left join BK_Customer c on a.CM_ID1=c.ID and a.LD_BrId=c.CM_BrId " _
                         & "left join BK_Location d on c.LO_ID=d.LO_ID and c.CM_BrId=d.LO_BrID " _
                         & " where  LD_BrId='" & frmMain.lblCode.Text & "' and LD_Dis_Date = '" & Me.DateTimePicker1.Value.ToString & "'"

        On Error Resume Next
        oDt.Clear()
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        With Me.DataGridView1.Rows(iRow)
            .Cells(0).Value = oDt.Rows(0).Item(0).ToString
            .Cells("LD_ID").Value = oDt.Rows(0).Item(1).ToString
            .Cells("EM_ID").Value = oDt.Rows(0).Item(2).ToString
            .Cells("EM_Name").Value = oDt.Rows(0).Item(3).ToString
            .Cells("CM_ID").Value = oDt.Rows(0).Item(4).ToString
            .Cells("CM_Name").Value = oDt.Rows(0).Item(5).ToString
            .Cells("CM_Phone").Value = oDt.Rows(0).Item(6).ToString
            .Cells("CM_Address").Value = oDt.Rows(0).Item(7).ToString
            .Cells("CU_ID").Value = oDt.Rows(0).Item(8).ToString
            .Cells("Amount").Value = oDt.Rows(0).Item(9).ToString
            .Cells("Int_Rate").Value = oDt.Rows(0).Item(10).ToString
            .Cells("LD_Date").Value = oDt.Rows(0).Item(11).ToString
            Dim as1 As Double = .Cells("Amount").Value
            .Cells("Amount").Value = Format(as1, "###,###.##")
            oDa.Dispose()
            oDt.Dispose()
            Dim iRow1 As Integer = Me.DataGridView1.Rows.Count - 1
            .Cells("No").Style.BackColor = Color.Yellow
            .Cells("No").ReadOnly = True
            .Cells("LD_ID").Style.BackColor = Color.Yellow
            .Cells("LD_ID").ReadOnly = True
            .Cells("EM_Name").Style.BackColor = Color.Yellow
            .Cells("EM_Name").ReadOnly = True
            .Cells("CM_Name").Style.BackColor = Color.Yellow
            .Cells("CM_Name").ReadOnly = True
            .Cells("CM_Phone").Style.BackColor = Color.Yellow
            .Cells("CM_Phone").ReadOnly = True
            .Cells("CM_Address").Style.BackColor = Color.Yellow
            .Cells("CM_Address").ReadOnly = True
            Me.DataGridView1.CurrentCell = Me.DataGridView1(1, iRow1)
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
        AddToGridOtherPayOff(DataGridView1, 13, "select 'Saved',a.LD_ID,a.Date_Payoff,b.EM_ID,c.EM_Name,a.CM_ID,d.CM_KhName,d.CM_Phone" _
& ",e.VL_ID+','+e.CN_ID+','+e.DT_ID+','+e.VL_ID 'address',b.CU_ID,b.LD_Dis_Amt,b.LD_IntRate,b.LD_Dis_Date" _
& " from BK_OtherDepositPayoff a " _
& " left join OtherDeposit b on a.LD_ID=b.LD_ID and a.LD_BrId=b.LD_BrId " _
& " left join BK_Employee c on b.EM_ID=c.EM_ID and b.LD_BrId=c.EM_BrID " _
& " left join BK_CustomerOther d on b.CM_ID=d.CM_ID and b.LD_BrId=c.EM_BrID" _
& " left join BK_LocationOther e on d.LO_ID=e.LO_ID and d.CM_BrId=e.LO_BrID" _
& " where a.Date_Payoff ='" & date1 & "' and a.LD_BrId='" & frmMain.lblCode.Text & "'")
    End Sub

    Sub AddToGridOtherPayOff(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                    'dg.Rows(i).Cells("Amount").Value = Format(dr(9), "###,###.##")
                    'dg.Rows(i).Cells("coCharge_Amt").Value = Format(dr(15), "###,###.##")
                Next j
                i += 1
                'Dim iRow1 As Integer = frmOtherDeposit.DataGridView1.Rows.Count - 1
                'Dim iRow As Integer = frmOtherDeposit.DataGridView1.Rows.Count
                'With frmOtherDeposit.DataGridView1.Rows(iRow1)
                '    .Cells(0).Style.BackColor = Color.Yellow
                '    .Cells(0).Value = "Saved"
                '    .Cells("No").Style.BackColor = Color.Yellow
                '    .Cells("No").ReadOnly = True
                '    .Cells("LD_ID").Style.BackColor = Color.Yellow
                '    .Cells("LD_ID").ReadOnly = True
                '    .Cells("EM_Name").Style.BackColor = Color.Yellow
                '    .Cells("EM_Name").ReadOnly = True
                '    .Cells("CM_Name").Style.BackColor = Color.Yellow
                '    .Cells("CM_Name").ReadOnly = True
                '    .Cells("CM_Phone").Style.BackColor = Color.Yellow
                '    .Cells("CM_Phone").ReadOnly = True
                '    .Cells("CM_Address").Style.BackColor = Color.Yellow
                '    .Cells("CM_Address").ReadOnly = True
                '    frmOtherDeposit.DataGridView1.CurrentCell = frmOtherDeposit.DataGridView1(1, iRow1)
                'End With
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
End Class