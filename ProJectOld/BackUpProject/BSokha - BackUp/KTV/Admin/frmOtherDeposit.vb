Imports System.Data.SqlClient
Public Class frmOtherDeposit
    Private Sub frmOtherDeposit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblLoanID.Text = getData("select top 1 LD_ID from OtherDeposit where LD_BrId ='" & frmMain.lblCode.Text & "' order by cast(LD_ID as Int) desc ")
        lblcustomer.Text = getData("select top 1 CM_ID from BK_CustomerOther where CM_BrId='" & frmMain.lblCode.Text & "' order by cast(CM_ID as Int) desc")
        SetFontDatagrid(DataGridView1)
        Me.DataGridView1.Columns("No").Visible = False
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
                    Dim LD_ID As String = getData("select top 1 LD_ID from OtherDeposit where LD_ID=" & DataGridView1.CurrentCell.Value.ToString & " and LD_BrId=" & frmMain.lblCode.Text)
                    If LD_ID = "" Then
                        DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("EM_ID").ColumnIndex, iRow)
                    Else
                        showOther()
                        'newRow()
                    End If
                End If
            ElseIf DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("EM_ID").ColumnIndex Then
                Try
                    If DataGridView1.CurrentCell.Value Is Nothing Then
                        Return
                    Else
                        Dim EM_ID As String = getData("select top 1 EM_ID from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "' and EM_ID='" & DataGridView1.CurrentCell.Value.ToString & "'")
                        If EM_ID = "" Then
                            Me.DataGridView1.Rows(iRow).Cells("EM_Name").Value = ""
                            Me.DataGridView1.Rows(iRow).Cells("EM_ID").Value = ""
                            frmEmployee.Text = "FromOther"
                            frmEmployee.MdiParent = frmMain
                            frmEmployee.WindowState = FormWindowState.Maximized
                            frmEmployee.Show()
                        Else
                            DataGridView1.Rows(iRow).Cells("EM_Name").Value = getData("select top 1 EM_Name from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "' and EM_ID='" & DataGridView1.CurrentCell.Value.ToString & "'")
                            DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("CM_ID").ColumnIndex, iRow)
                        End If
                    End If
                Catch ex As Exception
                    Return
                End Try
            ElseIf DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("CM_ID").ColumnIndex Then
                Try

                    If DataGridView1.CurrentCell.Value.ToString = "" Then
                        Return
                    Else
                        Dim CM_Name As String = getData("select top 1 CM_KhName from BK_CustomerOther where CM_BrId='" & frmMain.lblCode.Text & "' and CM_ID='" & DataGridView1.CurrentCell.Value.ToString & "' and Status='Active'")
                        Dim CM_Phone As String = getData("select top 1 CM_Phone from BK_CustomerOther where CM_BrId='" & frmMain.lblCode.Text & "' and CM_ID='" & DataGridView1.CurrentCell.Value.ToString & "' and Status='Active'")
                        Dim CM_Address As String = getData("select top 1 b.VL_ID+','+b.CN_ID+','+b.DT_ID+','+b.PV_ID from BK_CustomerOther a left join BK_LocationOther b on a.CM_BrId=b.LO_BrID and a.LO_ID=b.LO_ID where CM_BrId='" & frmMain.lblCode.Text & "' and CM_ID='" & DataGridView1.CurrentCell.Value.ToString & "' and Status='Active'")
                        If CM_Name = "" Then
                            Me.DataGridView1.CurrentCell.Value = ""
                            FrmCustomer.Text = "CustomerOther"
                            FrmCustomer.Show()
                        Else
                            DataGridView1.Rows(iRow).Cells("CM_Name").Value = CM_Name.ToString
                            DataGridView1.Rows(iRow).Cells("CM_Phone").Value = CM_Phone.ToString
                            DataGridView1.Rows(iRow).Cells("CM_Address").Value = CM_Address.ToString
                            DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("CU_ID").ColumnIndex, iRow)
                        End If
                    End If
                Catch ex As Exception
                    Return
                End Try
            ElseIf DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("CU_ID").ColumnIndex Then
                Try
                    Dim Curr As String = getData("select top 1 CU_Name from BK_Currency where CU_ID=" & DataGridView1.CurrentCell.Value)
                    If Curr = "" Then
                        resultError = frmMessageError.ShowBoxError("កូដ 1 សំរាប់រៀល និង កូដ 2 សំរាប់ដុល្លារ។", "ការបញ្ចូលទិន្ន័យខុស")
                        Me.DataGridView1.Rows(iRow).Cells("CU_ID").Value = ""
                    ElseIf Me.DataGridView1.CurrentCell.Value.ToString = "រៀល" Or Me.DataGridView1.CurrentCell.Value.ToString = "ដុល្លារ" Then
                        Return
                    Else
                        DataGridView1.CurrentCell.Value = Curr.ToString
                        DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("Amount").ColumnIndex, iRow)
                    End If
                Catch ex As Exception
                    Return
                End Try
            ElseIf DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("Amount").ColumnIndex Then
                Try
                    If DataGridView1.Rows(iRow).Cells("Amount").Value = "" Then
                        Return
                    ElseIf (DataGridView1.Rows(iRow).Cells("Amount").Value) / 1 = DataGridView1.Rows(iRow).Cells("Amount").Value Then
                        Dim as1 As Double = Me.DataGridView1.Rows(iRow).Cells("Amount").Value
                        Me.DataGridView1.Rows(iRow).Cells("Amount").Value = Format(as1, "###,###.##")
                        DataGridView1.CurrentCell = DataGridView1(DataGridView1.Rows(iRow).Cells("Int_Rate").ColumnIndex, iRow)
                    Else
                        MessageBox.Show("This amount not correct, check and try again.", "Error Amount", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.DataGridView1.Rows(iRow).Cells("Amount").Value = ""
                    End If
                Catch ex As Exception
                    MessageBox.Show("This amount not correct, check and try again.", "Error Amount", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.DataGridView1.Rows(iRow).Cells("Amount").Value = ""
                    Return
                End Try
            ElseIf DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("Int_Rate").ColumnIndex Then
                Try
                    If DataGridView1.Rows(iRow).Cells("Int_Rate").Value = "" Then
                        'Return
                    ElseIf (DataGridView1.Rows(iRow).Cells("Int_Rate").Value) / 1 = DataGridView1.Rows(iRow).Cells("Int_Rate").Value Then
                        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("LD_Date").ColumnIndex, iRow)
                    Else
                        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលអត្រាការប្រាក់មិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                        DataGridView1.Rows(iRow).Cells("Int_Rate").Value = ""
                    End If
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលអត្រាការប្រាក់មិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
                    DataGridView1.Rows(iRow).Cells("Int_Rate").Value = ""
                    Return
                End Try
            ElseIf DataGridView1.CurrentCell.ColumnIndex = DataGridView1.Rows(iRow).Cells("LD_Date").ColumnIndex Then
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
                Catch ex As Exception
                    resultError = frmMessageError.ShowBoxError("ការបញ្ចូលថ្ងៃខ្ចីមិនត្រឹមត្រូវទេ សូមពិនិត្យឡើងវិញ។", "ការបញ្ចូលទិន្ន័យខុស")
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
                If Me.DataGridView1.Rows(iRow).Cells("No").Value = "Editing" Then
                    If Me.CheckNull = 1 Then
                        MessageBox.Show("Not enough information to save this record, please check again.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    Else
                        saveOther()
                        showOther()
                        newRow()
                    End If
                Else
                    MessageBox.Show("Update")
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
                        AddTrace_Other("DELETE", Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value)
                        addIn("delete from OtherDeposit where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
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
    '--------------------------------- method and function
    Private Sub saveOther()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = Me.DataGridView1.Rows(iRow).Cells("LD_ID").Value
                .Add("@d1", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d2", SqlDbType.Int).Value = DataGridView1.Rows(iRow).Cells("CM_ID").Value
                .Add("@d3", SqlDbType.Date).Value = DataGridView1.Rows(iRow).Cells("LD_Date").Value
                .Add("@d4", SqlDbType.Float).Value = DataGridView1.Rows(iRow).Cells("Amount").Value
                .Add("@d5", SqlDbType.NVarChar).Value = DataGridView1.Rows(iRow).Cells("CU_ID").Value
                .Add("@d6", SqlDbType.Float).Value = DataGridView1.Rows(iRow).Cells("Int_Rate").Value
                .Add("@d7", SqlDbType.NVarChar).Value = DataGridView1.Rows(iRow).Cells("EM_ID").Value
                .Add("@d8", SqlDbType.NVarChar).Value = "Active"
                .Add("@d9", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d10", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d11", SqlDbType.Int).Value = 0
                '.Add("@d12", SqlDbType.Date).Value = Date.MaxValue
                .Add("@d13", SqlDbType.NVarChar).Value = getData("select top 1 ID from BK_Customer where CM_ID='" & Me.DataGridView1.Rows(iRow).Cells("CM_ID").Value & "' and CM_BrId='" & frmMain.lblCode.Text & "' and Status='Active'")
            End With
            sql = "insert into OtherDeposit (LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_Dis_Amt,CU_ID,LD_IntRate,EM_ID,LD_Status,LD_User_Create,LD_Date_Create,IsExport,CM_ID1) values (@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d13)"
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
    Private Function CheckNull()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim a As Integer
        With Me.DataGridView1.Rows(iRow)
            If .Cells("No").Value Is Nothing Or .Cells("LD_ID").Value Is Nothing Or .Cells("EM_ID").Value Is Nothing Or .Cells("CM_ID").Value Is Nothing _
                Or .Cells("CU_ID").Value Is Nothing Or .Cells("Amount").Value Is Nothing Or .Cells("Int_Rate").Value Is Nothing Or .Cells("LD_Date").Value Is Nothing Then
                a = 1
            Else
                a = 2
            End If
        End With
        Return a
    End Function
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells("No").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("No").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("EM_Name").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("EM_Name").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("CM_Name").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("CM_Name").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("CM_Phone").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("CM_Phone").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("CM_Address").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("CM_Address").ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(1, iRow)
        DataGridView1.Rows(iRow).Cells("No").Value = "Editing"
    End Sub
    Public Sub ShowOthers()
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select 'Saved',LD_ID,a.EM_ID,b.EM_Name,a.CM_ID,c.CM_KhName,c.CM_Phone,d.VL_ID +','+ CN_ID +','+ DT_ID +','+ PV_ID 'Address',CU_ID,LD_Dis_Amt,LD_IntRate,convert( varchar(12),a.LD_Dis_Date,101) LD_Dis_Date from OtherDeposit a " _
                         & "left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID " _
                         & "left join BK_CustomerOther c on a.CM_ID1=c.ID and a.LD_BrId=c.CM_BrId " _
                         & "left join BK_LocationOther d on c.LO_ID=d.LO_ID and c.CM_BrId=d.LO_BrID " _
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
    Public Sub showOther()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select top 1 'Saved',LD_ID,a.EM_ID,b.EM_Name,a.CM_ID,c.CM_KhName,c.CM_Phone,d.VL_ID +','+ CN_ID +','+ DT_ID +','+ PV_ID 'Address',CU_ID,LD_Dis_Amt,LD_IntRate,convert( varchar(12),a.LD_Dis_Date,101) LD_Dis_Date from OtherDeposit a " _
                          & "left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID " _
                          & "left join BK_CustomerOther c on a.CM_ID1=c.ID and a.LD_BrId=c.CM_BrId " _
                          & "left join BK_LocationOther d on c.LO_ID=d.LO_ID and c.CM_BrId=d.LO_BrID " _
                          & " where LD_ID='" & Me.DataGridView1.CurrentCell.Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'"
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
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
    Private Function NoRecordChange()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select top 1 CM_ID,EM_ID,CU_ID,LD_Dis_Amt,LD_Dis_Date, where LD_ID='" & Me.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & "' and LD_BrId='" & frmMain.lblCode.Text & "'"
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim CM_ID As Integer = oDt.Rows(0).Item(0).ToString
        Dim EM_ID As Integer = oDt.Rows(0).Item(1).ToString
        Dim LD_ChargeRate As Double = oDt.Rows(0).Item(2).ToString
        Dim LD_ChargeAmt As Double = oDt.Rows(0).Item(3).ToString
        oDa.Dispose()
        oDt.Dispose()
        With DataGridView1.Rows(iRow)
            If CM_ID = .Cells("coCM_ID").Value And EM_ID = .Cells("coEM_ID").Value And LD_ChargeRate = .Cells("coCharge_Rate").Value And LD_ChargeAmt = .Cells("coCharge_Amt").Value Then
                Return 1
            Else
                Return 2
            End If
        End With
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddToGridOther(DataGridView1, 12, "select'Saved',LD_ID,a.EM_ID,b.EM_Name,a.CM_ID,c.CM_KhName,c.CM_Phone,d.VL_ID +','+ CN_ID +','+ DT_ID +','+ PV_ID 'Address',CU_ID,LD_Dis_Amt,LD_IntRate,convert( varchar(12),a.LD_Dis_Date,101) LD_Dis_Date from OtherDeposit a " _
                            & "left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID " _
                            & "left join BK_CustomerOther c on a.CM_ID1=c.ID and a.LD_BrId=c.CM_BrId " _
                            & "left join BK_LocationOther d on c.LO_ID=d.LO_ID and c.CM_BrId=d.LO_BrID " _
                            & " where a.LD_Dis_Date='" & Me.DateTimePicker1.Value & "' and LD_BrId='" & frmMain.lblCode.Text & "' order by LD_Dis_Date desc")
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ToExcel(DataGridView1)
    End Sub
    Public Sub AddTrace_Other(ByVal RecordAction As String, ByVal LD_ID As Integer)
        Dim CU_ID, LD_Status, LD_User_Create, LD_User_Modify, LD_User_Delete, LD_BrId, EM_ID As String
        Dim CM_ID, IsExport, IsWriteoff, CM_ID1 As Integer
        Dim LD_Dis_Date As Date
        Dim LD_Dis_Amt, LD_IntRate As Double
        'Dim LD_Rec_Status, LD_Service As Boolean
        Dim DateAction, LD_Date_Create, LD_Date_Modify, LD_Date_Delete As DateTime
        Try
            Dim oDt As New System.Data.DataTable
            Dim Str As String = "select top 1 * from OtherDeposit where LD_ID='" & LD_ID & "' and LD_BrId='" & frmMain.lblCode.Text & "'"
            oDt.Clear()
            oDa = New SqlDataAdapter(Str, g_cnn)
            oDa.Fill(oDt)
            DateAction = DateTime.Now
            '--- RecordAction
            LD_ID = oDt.Rows(0).Item(0).ToString
            LD_BrId = oDt.Rows(0).Item(1).ToString
            CM_ID = oDt.Rows(0).Item(2).ToString
            LD_Dis_Date = oDt.Rows(0).Item(3).ToString
            LD_Dis_Amt = oDt.Rows(0).Item(4).ToString
            CU_ID = oDt.Rows(0).Item(5).ToString
            LD_IntRate = oDt.Rows(0).Item(6).ToString
            EM_ID = oDt.Rows(0).Item(7).ToString
            LD_Status = oDt.Rows(0).Item(8).ToString
            LD_User_Create = oDt.Rows(0).Item(9).ToString
            LD_Date_Create = oDt.Rows(0).Item(10).ToString
            LD_User_Modify = oDt.Rows(0).Item(11).ToString
            If Format(oDt.Rows(0).Item(12).ToString, "") = "" Then
                LD_Date_Modify = DateTime.MaxValue.ToString
            Else
                LD_Date_Modify = oDt.Rows(0).Item(12).ToString
            End If
            'LD_Date_Modify = oDt.Rows(0).Item(20).ToString
            LD_User_Delete = frmMain.users
            LD_Date_Delete = DateTime.Now
            IsExport = oDt.Rows(0).Item(15).ToString
            IsWriteoff = oDt.Rows(0).Item(16).ToString
            CM_ID1 = oDt.Rows(0).Item(17).ToString
            If RecordAction = "DELETE" Then
                If LD_Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert into Trace_OtherDeposit1 (DateAction,RecordAction,LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_Dis_Amt,CU_ID,LD_IntRate,EM_ID,LD_Status,LD_User_Create,LD_Date_Create,LD_User_Delete,LD_Date_Delete,IsExport,IsWriteoff,CM_ID1) Values('" & DateAction & "','" & RecordAction & "','" & LD_ID & "','" & LD_BrId & "','" & CM_ID & "','" & LD_Dis_Date & "',N'" & LD_Dis_Amt & "',N'" & CU_ID & "','" & LD_IntRate & "','" & EM_ID & "','" & LD_Status & "','" & LD_User_Create & "','" & LD_Date_Create & "','" & LD_User_Delete & "','" & LD_Date_Delete & "','" & IsExport & "','" & IsWriteoff & "','" & CM_ID1 & "')")
                Else
                    addIn("insert into Trace_OtherDeposit1 (DateAction,RecordAction,LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_Dis_Amt,CU_ID,LD_IntRate,EM_ID,LD_Status,LD_User_Create,LD_Date_Create,LD_User_Modify,LD_Date_Modify,LD_User_Delete,LD_Date_Delete,IsExport,IsWriteoff,CM_ID1) Values('" & DateAction & "','" & RecordAction & "','" & LD_ID & "','" & LD_BrId & "','" & CM_ID & "','" & LD_Dis_Date & "',N'" & LD_Dis_Amt & "',N'" & CU_ID & "','" & LD_IntRate & "','" & EM_ID & "','" & LD_Status & "','" & LD_User_Create & "','" & LD_Date_Create & "','" & LD_User_Modify & "','" & LD_Date_Modify & "','" & LD_User_Delete & "','" & LD_Date_Delete & "','" & IsExport & "','" & IsWriteoff & "','" & CM_ID1 & "')")
                End If
            Else
                If LD_Date_Modify = DateTime.MaxValue.ToString Then
                    addIn("insert into Trace_OtherDeposit1 (DateAction,RecordAction,LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_Dis_Amt,CU_ID,LD_IntRate,EM_ID,LD_Status,LD_User_Create,LD_Date_Create,IsExport,IsWriteoff,CM_ID1) Values('" & DateAction & "','" & RecordAction & "','" & LD_ID & "','" & LD_BrId & "','" & CM_ID & "','" & LD_Dis_Date & "',N'" & LD_Dis_Amt & "',N'" & CU_ID & "','" & LD_IntRate & "','" & EM_ID & "','" & LD_Status & "','" & LD_User_Create & "','" & LD_Date_Create & "','" & IsExport & "','" & IsWriteoff & "','" & CM_ID1 & "')")
                Else
                    addIn("insert into Trace_OtherDeposit1 (DateAction,RecordAction,LD_ID,LD_BrId,CM_ID,LD_Dis_Date,LD_Dis_Amt,CU_ID,LD_IntRate,EM_ID,LD_Status,LD_User_Create,LD_Date_Create,LD_User_Modify,LD_Date_Modify,IsExport,IsWriteoff,CM_ID1) Values('" & DateAction & "','" & RecordAction & "','" & LD_ID & "','" & LD_BrId & "','" & CM_ID & "','" & LD_Dis_Date & "',N'" & LD_Dis_Amt & "',N'" & CU_ID & "','" & LD_IntRate & "','" & EM_ID & "','" & LD_Status & "','" & LD_User_Create & "','" & LD_Date_Create & "','" & LD_User_Modify & "','" & LD_Date_Modify & "','" & IsExport & "','" & IsWriteoff & "','" & CM_ID1 & "')")
                End If
            End If
            oDa.Dispose()
            oDt.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        FrmCustomer.Text = "CustomerOther"
        FrmCustomer.Show()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        frmLocation.Text = "LocationOther"
        frmLocation.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        showOther("top 50")
    End Sub
    Private Sub showOther(ByVal Num As String)
        AddToGridOther(DataGridView1, 12, "select " & Num & " 'Saved',LD_ID,a.EM_ID,b.EM_Name,a.CM_ID,c.CM_KhName,c.CM_Phone,d.VL_ID +','+ CN_ID +','+ DT_ID +','+ PV_ID 'Address',CU_ID,LD_Dis_Amt,LD_IntRate,convert( varchar(12),a.LD_Dis_Date,101) LD_Dis_Date from OtherDeposit a " _
                     & "left join BK_Employee b on a.EM_ID=b.EM_ID and a.LD_BrId=b.EM_BrID " _
                     & "left join BK_CustomerOther c on a.CM_ID1=c.ID and a.LD_BrId=c.CM_BrId " _
                     & "left join BK_LocationOther d on c.LO_ID=d.LO_ID and c.CM_BrId=d.LO_BrID " _
                     & " where  LD_BrId='" & frmMain.lblCode.Text & "' order by LD_Dis_Date desc")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        showOther("top 100")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        showOther("")
    End Sub
End Class