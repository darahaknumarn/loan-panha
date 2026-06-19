Imports System.IO
Imports System.Drawing.Graphics
Imports System.Data
Imports System.Data.SqlClient
'Imports System.Xml
Imports Microsoft.VisualBasic
Module ktvmode
    Public meetingName As String
    Public MeetingID As Integer
    Public dss As DataSet
    Public Sub Security()
        Dim st As String
        Dim sec As Integer
        sec = 0
        st = "Select u.RID from tblrole R inner join tbluser U on R.rid =U.rid where U.EmployeeID='" & uid & "'"
        Try
            'Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            com.CommandText = st
            dr = com.ExecuteReader
            If dr.Read = True Then
                sec = Val(dr(0).ToString)
            End If
            'con.Close()
            'con.Dispose()
            Select Case sec
                Case 1
                    frmMain.WindowState = FormWindowState.Maximized
                    frmMain.Show()
                Case Else
                    frmMain.WindowState = FormWindowState.Maximized
                    frmMain.Show()
                    'Dashboard.WindowState = FormWindowState.Maximized
                    'Dashboard.ShowDialog()
            End Select
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    'Public ConnectionString As String = "DSN=ktv;uid=sa;pwd=123456"
    Public Function ReadFile(ByVal Path As String) As Byte()
        Dim data As Byte() = Nothing

        Try
            Dim fInfo As New FileInfo(Path)
            Dim numBytes As Long = fInfo.Length
            Dim fStream As New FileStream(Path, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fStream)
            data = br.ReadBytes(CInt(numBytes))
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
        Return data
    End Function
    Public Function Check_date1(ByVal payDate As Date, ByVal amt_day As Integer)
        Dim Date_Check As Date
        Date_Check = payDate
        Dim j, n As Integer
        n = Val(getData("select count(StartDate) from BK_Holiday"))
        While j <= n
            Dim d As String = getData("select StartDate from BK_Holiday where StartDate='" & Date_Check & "'")
            'Date_Check = FormatDateTime(getData("select StartDate from BK_Holiday where StartDate='" & payDate & "'"), DateFormat.ShortDate)
            If d = "" Then
                Date_Check = Date_Check
                If Date_Check.DayOfWeek = DayOfWeek.Saturday Then
                    Date_Check = payDate.AddDays(2)
                    j = j + 1
                ElseIf Date_Check.DayOfWeek = DayOfWeek.Sunday Then
                    Date_Check = Date_Check.AddDays(1)
                    j = j + 1
                Else
                    j = n + 1
                End If
            Else
                Date_Check = Date_Check.AddDays(7)
                If Date_Check.DayOfWeek = DayOfWeek.Saturday Then
                    Date_Check = Date_Check.AddDays(1 + 1)
                ElseIf Date_Check.DayOfWeek = DayOfWeek.Sunday Then
                    Date_Check = Date_Check.AddDays(1)
                End If
                j = j + 1
            End If
        End While
        Return Date_Check
    End Function
    Public Function Check_date(ByVal payDate As Date, ByVal amt_day As Integer)
        Dim Date_Check As Date
        Date_Check = payDate
        Dim j, n As Integer
        n = Val(getData("select count(StartDate) from BK_Holiday"))
        While j <= n
            Dim d As String = getData("select StartDate from BK_Holiday where StartDate='" & Date_Check & "'")
            'Date_Check = FormatDateTime(getData("select StartDate from BK_Holiday where StartDate='" & payDate & "'"), DateFormat.ShortDate)
            If d = "" Then
                Date_Check = Date_Check
                If Date_Check.DayOfWeek = DayOfWeek.Saturday Then
                    Date_Check = payDate.AddDays(2)
                    j = j + 1
                ElseIf Date_Check.DayOfWeek = DayOfWeek.Sunday Then
                    Date_Check = Date_Check.AddDays(1)
                    j = j + 1
                Else
                    j = n + 1
                End If
            Else
                Date_Check = Date_Check.AddDays(1)
                If Date_Check.DayOfWeek = DayOfWeek.Saturday Then
                    Date_Check = Date_Check.AddDays(1 + 1)
                ElseIf Date_Check.DayOfWeek = DayOfWeek.Sunday Then
                    Date_Check = Date_Check.AddDays(1)
                End If
                j = j + 1
            End If
        End While

        Return Date_Check
    End Function
    Public Function getData(ByVal st As String) As String
          getData = ""
        Try
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            com.Connection = g_cnn
            com.CommandText = st
            dr = com.ExecuteReader
            If dr.Read = True Then
                getData = dr(0).ToString
            End If
            dr.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Public Function getDataUni(ByVal Tbl As String, ByVal targetField As String, ByVal fieldPara As String, ByVal Para As String) As String
        getDataUni = ""
        Try

            'Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Para
            End With
            com.CommandText = "Select " & targetField & " from " & Tbl & " where " & fieldPara & " =@d0 "
            dr = com.ExecuteReader
            If dr.Read = True Then
                getDataUni = dr(0).ToString
            End If
            'con.Close()
            'con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Sub AddToGridOther(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                    dg.Rows(i).Cells("Amount").Value = Format(dr(9), "###,###.##")
                    'dg.Rows(i).Cells("coCharge_Amt").Value = Format(dr(15), "###,###.##")
                Next j
                i += 1
                Dim iRow1 As Integer = frmOtherDeposit.DataGridView1.Rows.Count - 1
                Dim iRow As Integer = frmOtherDeposit.DataGridView1.Rows.Count
                With frmOtherDeposit.DataGridView1.Rows(iRow1)
                    .Cells(0).Style.BackColor = Color.Yellow
                    .Cells(0).Value = "Saved"
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
                    frmOtherDeposit.DataGridView1.CurrentCell = frmOtherDeposit.DataGridView1(1, iRow1)
                End With
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Sub AddToGridLoan(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                    dg.Rows(i).Cells("coLD_DisAmt").Value = Format(dr(8), "###,###.##")
                    dg.Rows(i).Cells("coCharge_Amt").Value = Format(dr(15), "###,###.##")
                Next j
                i += 1
                Dim iRow1 As Integer = frmDisburshment.DataGridView1.Rows.Count - 1
                Dim iRow As Integer = frmDisburshment.DataGridView1.Rows.Count
                With frmDisburshment.DataGridView1.Rows(iRow1)
                    .Cells(0).Style.BackColor = Color.Yellow
                    .Cells(0).Value = "Saved"
                    .Cells("coLD_ID").Style.BackColor = Color.Yellow
                    .Cells("coLD_ID").ReadOnly = True
                    .Cells("coEM_Name").Style.BackColor = Color.Yellow
                    .Cells("coEM_Name").ReadOnly = True
                    .Cells("coCM_ID").Style.BackColor = Color.Yellow
                    .Cells("coCM_ID").ReadOnly = True
                    .Cells("coCM_Name").Style.BackColor = Color.Yellow
                    .Cells("coCM_Name").ReadOnly = True
                    .Cells("coCM_Phone").Style.BackColor = Color.Yellow
                    .Cells("coCM_Phone").ReadOnly = True
                    .Cells("coAddress").Style.BackColor = Color.Yellow
                    .Cells("coAddress").ReadOnly = True
                    .Cells("coLD_DisAmt").Style.BackColor = Color.Yellow
                    .Cells("coLD_DisAmt").ReadOnly = True
                    .Cells("coCurrency").Style.BackColor = Color.Yellow
                    .Cells("coCurrency").ReadOnly = True
                    .Cells("coUnit").Style.BackColor = Color.Yellow
                    .Cells("coUnit").ReadOnly = True
                    .Cells("coTerm").Style.BackColor = Color.Yellow
                    .Cells("coTerm").ReadOnly = True
                    .Cells("coIntRate").Style.BackColor = Color.Yellow
                    .Cells("coIntRate").ReadOnly = True
                    .Cells("coType").Style.BackColor = Color.Yellow
                    .Cells("coType").ReadOnly = True
                    .Cells("coCharge_Amt").Style.BackColor = Color.Yellow
                    .Cells("coCharge_Amt").ReadOnly = True
                    .Cells("coDisDate").Style.BackColor = Color.Yellow
                    .Cells("coDisDate").ReadOnly = True
                    .Cells("coDisDatePay").Style.BackColor = Color.Yellow
                    .Cells("coDisDatePay").ReadOnly = True
                    .Cells("coDisDateEnd").Style.BackColor = Color.Yellow
                    .Cells("coDisDateEnd").ReadOnly = True
                    .Cells("coLD_Service").Style.BackColor = Color.Yellow
                    .Cells("coLD_Service").ReadOnly = True
                    .Cells("PName").Style.BackColor = Color.Yellow
                    .Cells("PName").ReadOnly = True
                    frmDisburshment.DataGridView1.CurrentCell = frmDisburshment.DataGridView1(1, iRow1)
                End With
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================

    Sub AddToGrid(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
        'Dim con As New SqlClient.SqlConnection
        Dim com As New SqlClient.SqlCommand
        Dim dr As SqlClient.SqlDataReader

        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As String = ""
        Try
            'dss.Clear()
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            com.CommandText = st
            dr = com.ExecuteReader
            dg.Rows.Clear()
            'Dim dt As New SqlDataAdapter(st, connectionString1)
            'dt.Fill(dss, "Table")
            'dg.DataSource = dss
            'dg.DataMember = "Table"
            'dss.DataSet = dt
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
                        dg.Rows(i).Cells(j).Value = FormatDateTime(dr(j), DateFormat.ShortDate)
                    Else
                        dg.Rows(i).Cells(j).Value = dr(j).ToString
                    End If
                Next j
                i += 1
            Loop
            'dss = DirectCast(dr.Read, DataTable)
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Sub AddToGridLDPaid(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                'If i Mod 2 = 0 Then
                '    dg.Rows(i).DefaultCellStyle.BackColor = Color.Azure
                'Else
                '    dg.Rows(i).DefaultCellStyle.BackColor = Color.White
                'End If

                For j = 0 To n - 1
                    'MessageBox.Show(dg.Rows(i).Cells(j).Value)
                    ' If IsNumeric(m) = True Then m = Format(m, "#.00").ToString
                    If IsDate(dr(j)) = True Then
                        'dg.Rows(i).Cells(j).Value
                        'dg.Rows(i).Cells(j).Value = Format(dr(j), "mm/dd/yyyy")
                        dg.Rows(i).Cells(j).Value = FormatDateTime(dr(j), DateFormat.ShortDate)
                        'ElseIf IsNumeric(dr(j)) = True And Val(dr(j).ToString) > 0 Then
                        '    dg.Rows(i).Cells(j).Value = Format(dr(j), "#.##")
                    Else
                        dg.Rows(i).Cells(j).Value = dr(j).ToString
                    End If

                Next j
                i += 1
                Dim a As String = dg.Rows(i - 1).Cells("co_AmtPaid1").Value
                If a = "" Then
                    a = 0
                End If
                If a - dg.Rows(i - 1).Cells(1).Value < 0 Then
                    dg.Rows(i - 1).DefaultCellStyle.BackColor = Color.Yellow
                End If
                frmRepayment.DataGridView2.Rows(i - 1).Cells("co_AmtPaid1").Value = Format(Val(frmRepayment.DataGridView2.Rows(i - 1).Cells("co_AmtPaid1").Value), "###,###.##")
                frmRepayment.DataGridView2.Rows(i - 1).Cells("co_Payoff").Value = Format(Val(frmRepayment.DataGridView2.Rows(i - 1).Cells("co_Payoff").Value), "###,###.##")
                frmRepayment.DataGridView2.Rows(i - 1).Cells("co_Prn").Value = Format(Val(frmRepayment.DataGridView2.Rows(i - 1).Cells("co_Prn").Value), "###,###.##")
                frmRepayment.DataGridView2.Rows(i - 1).Cells("co_Int").Value = Format(Val(frmRepayment.DataGridView2.Rows(i - 1).Cells("co_Int").Value), "###,###.##")
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Sub AddToGridLDPaid1(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
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
                'If i Mod 2 = 0 Then
                '    dg.Rows(i).DefaultCellStyle.BackColor = Color.Azure
                'Else
                '    dg.Rows(i).DefaultCellStyle.BackColor = Color.White
                'End If

                For j = 0 To n - 1
                    'MessageBox.Show(dg.Rows(i).Cells(j).Value)
                    ' If IsNumeric(m) = True Then m = Format(m, "#.00").ToString
                    If IsDate(dr(j)) = True Then
                        'dg.Rows(i).Cells(j).Value
                        'dg.Rows(i).Cells(j).Value = Format(dr(j), "mm/dd/yyyy")
                        dg.Rows(i).Cells(j).Value = FormatDateTime(dr(j), DateFormat.ShortDate)
                        'ElseIf IsNumeric(dr(j)) = True And Val(dr(j).ToString) > 0 Then
                        '    dg.Rows(i).Cells(j).Value = Format(dr(j), "#.##")
                    Else
                        dg.Rows(i).Cells(j).Value = dr(j).ToString
                    End If

                Next j
                i += 1
                Dim a As String = dg.Rows(i - 1).Cells("co_AmtPaid111").Value
                If a = "" Then
                    a = 0
                End If
                If a - dg.Rows(i - 1).Cells(1).Value < 0 Then
                    dg.Rows(i - 1).DefaultCellStyle.BackColor = Color.Yellow
                End If
                frmRepayment.DataGridView5.Rows(i - 1).Cells("co_AmtPaid111").Value = Format(Val(frmRepayment.DataGridView5.Rows(i - 1).Cells("co_AmtPaid111").Value), "###,###.##")
                frmRepayment.DataGridView5.Rows(i - 1).Cells("co_Payoff1").Value = Format(Val(frmRepayment.DataGridView5.Rows(i - 1).Cells("co_Payoff1").Value), "###,###.##")
                frmRepayment.DataGridView5.Rows(i - 1).Cells("co_Prn1").Value = Format(Val(frmRepayment.DataGridView5.Rows(i - 1).Cells("co_Prn1").Value), "###,###.##")
                frmRepayment.DataGridView5.Rows(i - 1).Cells("co_Int1").Value = Format(Val(frmRepayment.DataGridView5.Rows(i - 1).Cells("co_Int1").Value), "###,###.##")
            Loop
            dr.Close()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Sub AddToGridwithPara(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String, ByVal Staffname As String)
        ''Dim con As New SqlClient.SqlConnection
        Dim com As SqlClient.SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As String = ""
        Try
            'con.ConnectionString = connectionString1
            'con.Open()
            com = New SqlClient.SqlCommand(st, g_cnn)
            com.CommandTimeout = 30000
            If Staffname <> "" Then
                com.Parameters.AddWithValue("@A1", DirectCast(Staffname, Object))
            End If

            dr = com.ExecuteReader
            dg.Rows.Clear()
            i = 0
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
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub
    Public Sub AddToListView(ByVal st As String, ByVal ls As ListView, ByVal N As Integer)
        Try

            'Dim con As New System.Data.SqlClient.SqlConnection
            Dim com As New System.Data.SqlClient.SqlCommand
            Dim dr As System.Data.SqlClient.SqlDataReader
            Dim i As Integer
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            com.CommandText = st
            dr = com.ExecuteReader
            ls.Items.Clear()
            While dr.Read
                Dim li As ListViewItem = ls.Items.Add(dr(0))
                For i = 1 To N
                    li.SubItems.Add(IIf(IsDBNull(dr(i)) = True, "", dr(i)))
                Next i
            End While
            dr.Close()
            'con.Close()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function ReturnRound(ByVal Value As String)
        Dim value1 As Integer
        If Value = 0 Then
            value1 = 0
        Else
            Dim s1 As Double = Val(Value)
            Dim s As String = Math.Ceiling(s1)
            Dim s2 As String = s(s.Length - 2) & s(s.Length - 1)

            If Val(s2) * 10 = 0 Then
                value1 = Value
            Else
                value1 = (s - s2) + 100
            End If
            'If s2 >= Val(s(s.Length - 2)) * 10 Then
            '    value1 = (s - s2) + 100
            '    'value1 = (Val(s(s.Length - 3)) * 100) + 100
            'Else
            '    value1 = Value
            'End If
        End If
        Return value1
    End Function
    Public Sub addIn(ByVal st As String)

        Try
            'Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand

            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            com.CommandText = st
            'MessageBox.Show(g_cnn.ToString)
            com.ExecuteScalar()
            com.Dispose()
            'con.Close()
            'MessageBox.Show("Successfully complete the transaction", "NiTA HR Solution")
        Catch ex As Exception

            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Function getImage(ByVal st As String) As Byte()
        getImage = Nothing
        Try
            'Dim con As New System.Data.SqlClient.SqlConnection
            Dim com As New System.Data.SqlClient.SqlCommand
            Dim dr As System.Data.SqlClient.SqlDataReader
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            com.CommandText = st
            dr = com.ExecuteReader
            If dr.Read = True Then
                getImage = DirectCast(dr(0), Byte())
            End If
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            'MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Function
End Module
