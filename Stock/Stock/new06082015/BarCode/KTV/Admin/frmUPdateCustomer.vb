Imports System.Data.SqlClient
Public Class frmUPdateCustomer
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If txtIP.Text.Trim.Length > 0 Then
            SaveSetting("CamITSo", "IP", "String", txtIP.Text)
        Else
            resultError = frmMessageError.ShowBoxError("ទទេរ", "ទទេរ")
        End If
        FrmCustomer.showall()
        Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As String = "Data Source= " & txtIP.Text & ";Initial Catalog=Data;Persist Security Info=True;User ID=sa;Password=123456"
        If Me.txtIP.Text = "" Then
            resultError = frmMessageError.ShowBoxError("លេខម៉ាស៊ីនមិនត្រឹមត្រូវទេ សូមពិនិត្យម្តងទៀត។", "លេខម៉ាស៊ីន")
        Else
            Try
                Dim con As New SqlClient.SqlConnection
                Dim com As New SqlClient.SqlCommand
                Dim dr As SqlClient.SqlDataReader
                If connectionString1 = "" Then
                    Frm_Connection.Show()
                    Me.Close()
                    Exit Sub
                End If
                con.ConnectionString = a
                con.Open()
                com.Connection = con
                com.CommandText = "select * from BK_Company"
                dr = com.ExecuteReader()
                If dr.Read = True Then
                    result = frmMessageError.ShowBoxError("ការភ្ជាប់បានជោគជ័យ អ្នកអាចធ្វើការបង្ហាញទិនន្ន័យបាន។", "បានភ្ជាប់")
                End If
                con.Close()
                con.Dispose()
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
            End Try
        End If
    End Sub
    Private Sub Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Update.Click
        Dim date1 As Date = FormatDateTime(Me.startDate.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(Me.endDate.Value, DateFormat.ShortDate)
        date2 = date2.AddDays(1)
        Dim startdate As String = date1.ToString("MM/dd/yyyy")
        Dim enddate As String = date2.ToString("MM/dd/yyyy")

        Dim result As Integer = DateTime.Compare(date1, date2)
        'MessageBox.Show(startdate & " " & enddate)
        Try
            If result > 0 Then
                resultError = frmMessageError.ShowBoxError("ថ្ងៃចាប់ផ្តើមមិនអាចធំជាងថ្ងៃបញ្ចប់បានទេ។", "ខុសថ្ងៃខែ")
                'MessageBox.Show("You've selected end date is smaller then start date, please check again before reload record!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            Else
                AddToGrid1(dgCustomer, 6, "select CM_ID 'កូដអតិថិជន',CM_KhName 'ឈ្មោះអតិថិជន',LO_ID 'លេខតំបន់',CM_Date_Create  'ថ្ងៃបង្កើត',CM_Date_Modify  'ថ្ងៃកែប្រែ', CM_BrId 'កូដសាខា' from Data.dbo.BK_Customer WHERE CM_Date_Create between '" & startdate & "' and '" & enddate & "'  OR CM_Date_Modify Between '" & startdate & "' and '" & enddate & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
                AddToGrid1(dgLocation, 8, "select LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Date_Create,LO_Date_Modify from Data.dbo.BK_Location WHERE LO_Date_Create between '" & startdate & "' and '" & enddate & "'  OR LO_Date_Modify Between '" & startdate & "' and '" & enddate & "'")
                AddToGrid1(dgLoan, 7, "select LD_ID,CM_ID,EM_ID,LD_Status,LD_Date_Create,LD_Date_Modify,LD_BrId from BK_Loan WHERE LD_Date_Create between '" & startdate & "' and '" & enddate & "'  OR LD_Date_Modify Between '" & startdate & "' and '" & enddate & "'")
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
            'MessageBox.Show(ex.Message, "ា")
        End Try
    End Sub
    Sub AddToGrid1(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
        Dim a As String = "Data Source= " & txtIP.Text & ";Initial Catalog=Data;Persist Security Info=True;User ID=sa;Password=123456"
        Dim con As New SqlClient.SqlConnection
        Dim com As New SqlClient.SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As String = ""
        Try
            con.ConnectionString = a
            con.Open()
            com.Connection = con
            com.CommandText = st
            dr = com.ExecuteReader
            dg.Rows.Clear()

            Do While dr.Read = True
                dg.Rows.Add()
                If i Mod 2 = 0 Then
                    dg.Rows(i).DefaultCellStyle.BackColor = Color.Azure
                Else
                    dg.Rows(i).DefaultCellStyle.BackColor = Color.White
                End If

                For j = 0 To n - 1

                    ' If IsNumeric(m) = True Then m = Format(m, "#.00").ToString
                    If IsDate(dr(j)) = True Then
                        dg.Rows(i).Cells(j).Value = Format(dr(j), "dd-MMM-yyyy")
                        'ElseIf IsNumeric(dr(j)) = True And Val(dr(j).ToString) > 0 Then
                        '    dg.Rows(i).Cells(j).Value = Format(dr(j), "#.##")
                    Else
                        dg.Rows(i).Cells(j).Value = dr(j).ToString
                    End If

                Next j
                i += 1
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Sub insertloan()
        Try
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim sql As String = ""
            '''''''''''''''''''''''''''''''''''''''''''''''''''-----------------insert into  staffemergency''''''''''''''''''''''''''''''''''''''''''''''''''-----------
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            For i As Short = 0 To dgLoan.Rows.Count - 2
                If frmMain.lblCode.Text = "" Then
                    MessageBox.Show("Can not blank your brand code, please try again.")
                Else
                    com.Parameters.Clear()
                    sql = "Insert into BK_Loan1(LD_ID,CM_ID,EM_ID,LD_Status,LD_Date_Create,LD_Date_Modify,LD_BrId) values(@l0,@l1,@l2,@l3,@l4,@l5,@l6)"
                    com.CommandText = sql
                    With com.Parameters
                        .Add("@l0", SqlDbType.NVarChar).Value = dgLoan.Rows(i).Cells(0).Value
                        .Add("@l1", SqlDbType.NVarChar).Value = dgLoan.Rows(i).Cells(1).Value
                        .Add("@l2", SqlDbType.NVarChar).Value = dgLoan.Rows(i).Cells(2).Value
                        .Add("@l3", SqlDbType.NVarChar).Value = dgLoan.Rows(i).Cells(3).Value
                        .Add("@l4", SqlDbType.NVarChar).Value = dgLoan.Rows(i).Cells(4).Value
                        .Add("@l5", SqlDbType.NVarChar).Value = dgLoan.Rows(i).Cells(5).Value
                        .Add("@l6", SqlDbType.NVarChar).Value = dgLoan.Rows(i).Cells(6).Value
                    End With
                    com.ExecuteNonQuery()
                End If
            Next
            com.Dispose()
            con.Close()
            con.Dispose()
            'MessageBox.Show("Successfully import data in", "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Sub insertCustomer()
        Try
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim sql As String = ""
            '''''''''''''''''''''''''''''''''''''''''''''''''''-----------------insert into  staffemergency''''''''''''''''''''''''''''''''''''''''''''''''''-----------
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            For i As Short = 0 To dgCustomer.Rows.Count - 2
                If frmMain.lblCode.Text = "" Then
                    MessageBox.Show("Can not blank your brand code, please try again.")
                Else
                    com.Parameters.Clear()
                    sql = "Insert into BK_Customer1(CM_ID,CM_Name,LO_ID,CM_Date_Create,CM_Date_Modify,CM_BrId) values(@l0,@l1,@l2,@l3,@l4,@l5)"
                    com.CommandText = sql
                    With com.Parameters
                        .Add("@l0", SqlDbType.NVarChar).Value = dgCustomer.Rows(i).Cells(0).Value
                        .Add("@l1", SqlDbType.NVarChar).Value = dgCustomer.Rows(i).Cells(1).Value
                        .Add("@l2", SqlDbType.NVarChar).Value = dgCustomer.Rows(i).Cells(2).Value
                        .Add("@l3", SqlDbType.NVarChar).Value = dgCustomer.Rows(i).Cells(3).Value.ToString
                        .Add("@l4", SqlDbType.NVarChar).Value = dgCustomer.Rows(i).Cells(4).Value.ToString
                        .Add("@l5", SqlDbType.NVarChar).Value = dgCustomer.Rows(i).Cells(5).Value
                        '.Add("@l6", SqlDbType.NVarChar).Value = dgLoan.Rows(i).Cells(6).Value
                    End With
                    'MessageBox.Show(dgCustomer.Rows(i).Cells(0).Value)
                    com.ExecuteNonQuery()
                End If
            Next
            com.Dispose()
            con.Close()
            con.Dispose()
            'MessageBox.Show("Successfully import data in", "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")

        End Try
    End Sub
    Sub insertlocation()
        Try
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim sql As String = ""
            '''''''''''''''''''''''''''''''''''''''''''''''''''-----------------insert into  staffemergency''''''''''''''''''''''''''''''''''''''''''''''''''-----------
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            For i As Short = 0 To dgLocation.Rows.Count - 2
                If frmMain.lblCode.Text = "" Then
                    MessageBox.Show("Can not blank your brand code, please try again.")
                Else
                    com.Parameters.Clear()
                    sql = "Insert into BK_Location1(LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrId,LO_Date_Create,LO_Date_Modify) values(@l0,@l1,@l2,@l3,@l4,@l5,@l6,@l7)"
                    com.CommandText = sql
                    With com.Parameters
                        .Add("@l0", SqlDbType.NVarChar).Value = dgLocation.Rows(i).Cells(0).Value
                        .Add("@l1", SqlDbType.NVarChar).Value = dgLocation.Rows(i).Cells(1).Value
                        .Add("@l2", SqlDbType.NVarChar).Value = dgLocation.Rows(i).Cells(2).Value
                        .Add("@l3", SqlDbType.NVarChar).Value = dgLocation.Rows(i).Cells(3).Value
                        .Add("@l4", SqlDbType.NVarChar).Value = dgLocation.Rows(i).Cells(4).Value
                        .Add("@l5", SqlDbType.NVarChar).Value = dgLocation.Rows(i).Cells(5).Value
                        .Add("@l6", SqlDbType.NVarChar).Value = dgLocation.Rows(i).Cells(6).Value
                        .Add("@l7", SqlDbType.NVarChar).Value = dgLocation.Rows(i).Cells(7).Value
                    End With
                    com.ExecuteNonQuery()
                End If
            Next
            com.Dispose()
            con.Close()
            con.Dispose()
            'MessageBox.Show("Successfully import data in", "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub frmUPdateCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetFontDatagrid(dgCustomer)
        SetFontDatagrid(dgLoan)
        SetFontDatagrid(dgLocation)
        txtIP.Text = GetSetting("CamITSo", "IP", "String".Trim)
        lblLastUpdate.Text = "ថ្ងៃបញ្ចូលអតិថិជនចុងក្រោយ: " & GetSetting("CamITSo", "LastUpdated", "String".Trim)
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        '  ------------------------------ Update Loan 
        addIn("delete from BK_Loan1")
        insertloan()
        addIn(" delete from BK_Loan where LD_ID in(select LD_ID from BK_Loan1)")
        addIn("INSERT INTO BK_Loan SELECT * FROM BK_Loan1")
        '------------------------------ Update Customer
        addIn("delete from BK_Customer1")
        insertCustomer()
        addIn(" delete from BK_Customer where CM_ID in(select CM_ID from BK_Customer1)")
        addIn("INSERT INTO BK_Customer SELECT * FROM BK_Customer1")
        '------------------------------ Update Loacation
        addIn("delete from BK_Location1")
        insertlocation()
        addIn(" delete from BK_Location where LO_ID in(select LO_ID from BK_Location1)")
        addIn("INSERT INTO BK_Location SELECT * FROM BK_Location1")
        '------------------------------------------------------------------------------- Finished 
        SaveSetting("CamITSo", "LastUpdated", "String", FormatDateTime(endDate.Value, DateFormat.ShortDate))
        resultError = frmMessageError.ShowBoxError("ការបញ្ចូលទិន្នន័យបានជោគជ័យ។", "បញ្ចូលទិន្នន័យ")
        'MessageBox.Show("កា", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class