Imports System.IO
Imports System.Drawing.Graphics
Imports System.Data
Imports System.Data.SqlClient
Module ktvmode
    Public meetingName As String
    Public MeetingID As Integer
    Public Sub Security()
        Dim st As String
        Dim sec As Integer
        sec = 0
        st = "Select u.RID from tblrole R inner join tbluser U on R.rid =U.rid where U.EmployeeID='" & uid & "'"
        Try
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = st
            dr = com.ExecuteReader
            If dr.Read = True Then
                sec = Val(dr(0).ToString)
            End If
            con.Close()
            con.Dispose()
            Select Case sec
                Case 1
                    frmMain.WindowState = FormWindowState.Maximized
                    frmMain.Show()
                Case Else
                    frmMain.WindowState = FormWindowState.Maximized
                    frmMain.Show()
            End Select
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public ConnectionString As String = "DSN=ktv;uid=sa;pwd=123456"
    Public Function toImage(ByVal mybyte As Byte()) As Image
        Dim myImg As Image = Nothing
        Dim str As New MemoryStream(mybyte)
        myImg = System.Drawing.Image.FromStream(str)
        Return myImg
    End Function

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

    Public Function getData(ByVal st As String) As String
        getData = ""
        Try
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = st
            dr = com.ExecuteReader
            If dr.Read = True Then
                getData = dr(0).ToString
            End If
            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Public Function getDataUni(ByVal Tbl As String, ByVal targetField As String, ByVal fieldPara As String, ByVal Para As String) As String
        getDataUni = ""
        Try

            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Para
            End With
            com.CommandText = "Select " & targetField & " from " & Tbl & " where " & fieldPara & " =@d0 "
            dr = com.ExecuteReader
            If dr.Read = True Then
                getDataUni = dr(0).ToString
            End If
            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Sub AddToGrid(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String)
        Dim con As New SqlClient.SqlConnection
        Dim com As New SqlClient.SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As String = ""
        Try
            con.ConnectionString = connectionString1
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
                    If IsDate(dr(j)) = True Then
                        dg.Rows(i).Cells(j).Value = Format(dr(j), "dd-MMM-yyyy")
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
    Sub AddToGridwithPara(ByVal dg As DataGridView, ByVal n As Integer, ByVal st As String, ByVal Staffname As String)
        Dim con As New SqlClient.SqlConnection
        Dim com As SqlClient.SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim m As String = ""
        Try
            con.ConnectionString = connectionString1
            con.Open()
            com = New SqlClient.SqlCommand(st, con)
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
                    If IsDate(dr(j)) = True Then
                        dg.Rows(i).Cells(j).Value = Format(dr(j), "dd-MMM-yyyy")
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

            Dim con As New System.Data.SqlClient.SqlConnection
            Dim com As New System.Data.SqlClient.SqlCommand
            Dim dr As System.Data.SqlClient.SqlDataReader
            Dim i As Integer
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
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
            con.Close()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Sub ShowEmployeeSalary(ByVal lst As ListView, ByVal img As ImageList)

        Try
            Dim i As Integer
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = "exec getLoan_Payment"
            com.ExecuteNonQuery()
            com.Dispose()

            com = New SqlClient.SqlCommand("select E.EmpID, E.fullname,E.datejoint,P.AccumulatedSalary,P.AccumulatedAllowance,P.AccumulatedLoan,P.AccumulatedPaid ,AmountLeft as [Due Amount],E.Photo from tblEmployees E left outer join tblpayroll P on E.EmpID=P.empid")
            com.Connection = con
            lst.Items.Clear()
            img.Images.Clear()
            dr = com.ExecuteReader
            i = 0

            While dr.Read
                img.Images.Add(i, toImage(dr(8)))

                Dim li As ListViewItem = lst.Items.Add("", dr(1).ToString, i)
                li.Tag = dr(0).ToString
                lst.Items(i).SubItems.Add(Format(CDate(dr(2).ToString), "dd-MMM-yyyy"))
                lst.Items(i).SubItems.Add(FormatCurrency(IIf(IsDBNull(dr(3)) = True, 0, dr(3).ToString), 2))
                lst.Items(i).SubItems.Add(FormatCurrency(IIf(IsDBNull(dr(4)) = True, 0, dr(4).ToString), 2))
                lst.Items(i).SubItems.Add(FormatCurrency(IIf(IsDBNull(dr(5)) = True, 0, dr(5).ToString), 2))
                lst.Items(i).SubItems.Add(FormatCurrency(IIf(IsDBNull(dr(5)) = True, 0, dr(6).ToString), 2))
                lst.Items(i).SubItems.Add(FormatCurrency(IIf(IsDBNull(dr(5)) = True, 0, dr(7).ToString), 2))
                i += 1
            End While

            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Sub addIn(ByVal st As String)

        Try
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand

            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = st

            com.ExecuteScalar()
            com.Dispose()
            con.Close()
        Catch ex As Exception

            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Public Function getImage(ByVal st As String) As Byte()
        getImage = Nothing
        Try
            Dim con As New System.Data.SqlClient.SqlConnection
            Dim com As New System.Data.SqlClient.SqlCommand
            Dim dr As System.Data.SqlClient.SqlDataReader
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = st
            dr = com.ExecuteReader
            If dr.Read = True Then
                getImage = DirectCast(dr(0), Byte())
            End If
            con.Close()
            con.Dispose()
        Catch ex As Exception
        End Try

    End Function


End Module
