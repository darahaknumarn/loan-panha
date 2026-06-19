Imports System.Data
Imports System.Data.SqlClient
Imports Encrypted
Imports Microsoft.Office.Interop
Imports X = Microsoft.Office.Interop.Excel
'Imports Spire.Xls
Imports System.Drawing
'Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Outlook

'----- select * from BK_Loan
Module moremode
    Public result As String = ""
    Public resultError As String = ""
    Public p As String
    Public connectionString1 As String
    Public oCnn As SqlConnection
    Public obj As New ClsEncrypted
    Public oCnn3 As SqlConnection
    Public oCnn1 As OleDb.OleDbConnection
    Public oCnn2 As SqlConnection
    Public oDa As SqlDataAdapter
    Public rpt As String
    Public uid As String
    Public logIn As Date
    Public logOut As Date
    Friend Adatp As New SqlDataAdapter
    Friend DS As New DataSet("KTVSet")
    'Public nProgress As Integer = 2
    'reset password to ''
    Public Sub ResetPassword(ByVal EmployeeID As String)
        Try
            Dim strCmd As String
            Dim oCmd As SqlCommand
            strCmd = "Update tblUser set Password='' where EmployeeID='" & EmployeeID & "'"
            oCmd = New SqlCommand(strCmd, oCnn)
            oCmd.ExecuteNonQuery()
            strCmd = "Insert into tblResetPassword values('" & EmployeeID & "','" & Now.Date & "')"
            oCmd.CommandText = strCmd
            oCmd.ExecuteNonQuery()
            oCmd.Dispose()
            MessageBox.Show("Successfully reset the password", "POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As System.Exception
            MessageBox.Show("Error", "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    'sub to execute non query
    Sub ExecNonQuery(ByVal SQL As String)
        On Error GoTo errL
        Dim cmd As New SqlCommand(SQL, oCnn)
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        Exit Sub
errL:
        cmd.Dispose()
    End Sub
    Function isExist(ByVal SQL As String) As Boolean
        Try
            Dim b As Boolean
            b = False
            Dim conn As New SqlConnection(connectionString1)
            conn.Open()
            Dim cmd As New SqlCommand(SQL, conn)
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader
            If rd.Read Then
                b = True
            Else
                b = False
            End If
            rd.Close()
            cmd.Dispose()
            conn.Dispose()
            Return b

        Catch ex As System.Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try


    End Function
    Function isExistDg(ByVal dg As DataGridView, ByVal SearchIt As Integer) As Boolean
        Try
            Dim b As Boolean
            Dim i As Integer
            b = False
            For i = 0 To dg.RowCount - 2
                If Val(dg.Rows(i).Cells(0).Value) = SearchIt Then
                    b = True
                    Exit For
                End If
            Next
            Return b

        Catch ex As System.Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try


    End Function
    Function IsExisted(ByVal SQL As String)
        Dim b As Boolean
        Try
            Dim con As New SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            Dim cmd As New SqlCommand(SQL, con)
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader
            If rd.Read Then
                b = True
            Else
                b = False
            End If
            rd.Close()
            cmd.Dispose()
            Return b
        Catch ex As System.Exception
            MessageBox.Show(Err.Description, "NiTA Solution")
        End Try
        Return b
    End Function
    Sub ReadOnlyGrid(ByVal GV1 As DataGridView, ByVal GV2 As DataGridView, ByVal GV3 As DataGridView, Optional ByVal b As Boolean = True)
        GV1.ReadOnly = b
        GV2.ReadOnly = b
        GV3.ReadOnly = b
    End Sub
    'open connection using window authen and SQL authen
    'Public Function openConnection(Optional ByVal Server As String = ".", Optional ByVal uname As String = "sa", Optional ByVal pwd As String = "123456", Optional ByVal Mode As String = "2") As Boolean
    '    Dim strCon As String
    '    strCon = ""
    '    Try
    '        If Mode = "1" Then
    '            strCon = "Server=" & Server & ";database=ktv;Integrated Security=true"
    '        ElseIf Mode = "2" Then

    '            strCon = "Server=" & Server & ";database=ktv;user id=" & uname & ";pwd=" & pwd
    '            ' ConnectionString = strCon
    '        End If
    '        ''strCon = "Server=" & Server & ";database=HumanResource;Integrated Security=true"
    '        ''strCon = "Data Source=192.168.100.2;Initial Catalog=HumanResource;Integrated Security=SSPI;user id=sa;pwd=kl"
    '        ''strCon = "Server=" & Server & ";database=HumanResource;Integrated Security=true"
    '        oCnn3 = New SqlConnection(strCon)
    '        oCnn3.Open()
    '        ConnectionString1 = strCon
    '        Return True
    '    Catch ex As Exception
    '        MessageBox.Show("Error Connection to the server", "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return False
    '    End Try
    'End Function
    Public Function OpenCon(ByVal fn As String) As Boolean
        On Error GoTo errL
        oCnn1 = New OleDb.OleDbConnection("File Name=" & fn)
        oCnn1.Open()
        Return True
        Exit Function
errL:
        MsgBox(Err.Erl)
    End Function
    'open child to main form
    Public Sub openForm(ByVal frm As Form)
        ' On Error Resume Next
        'frm.MdiParent = FrmMain
        frm.ShowDialog()
    End Sub
    Function Amstr(ByVal S As String) As String
        S = Replace(S, "'", "''")
        Return "'" & S & "'"
    End Function
    'add datasource to combo and show
    Public Sub Combobox_Datasource(ByVal cboName As ComboBox, ByVal SourceName As String, ByVal DisplayField As String, ByVal ValueField As String, ByVal Cond As String)
        Dim strDatasource As String
        Dim oDt As New System.Data.DataTable
        Dim x As New System.Data.DataTable

        Try
            strDatasource = "select distinct " & DisplayField & "," & ValueField & " from " & SourceName
            If Cond <> "" Then
                strDatasource += " where " & Cond
            End If
            oDa = New SqlDataAdapter(strDatasource, oCnn)
            oDa.Fill(oDt)
            cboName.DataSource = oDt
            cboName.DisplayMember = oDt.Columns(0).ToString
            cboName.ValueMember = oDt.Columns(1).ToString
            cboName.SelectedIndex = -1

            oDa = Nothing

        Catch ex As System.Exception

        End Try

    End Sub
    Public Sub Combobox_source(ByVal cboName As ComboBox, ByVal sql As String)
        Dim strDatasource As String
        Dim oDt As New System.Data.DataTable
        Dim x As New System.Data.DataTable

        On Error Resume Next
        strDatasource = sql
        oDa = New SqlDataAdapter(strDatasource, oCnn)
        oDa.Fill(oDt)
        cboName.DataSource = oDt

        cboName.DisplayMember = oDt.Columns(0).ToString
        cboName.ValueMember = oDt.Columns(1).ToString
        cboName.SelectedIndex = -1

        oDa = Nothing
    End Sub
    'to know user log is ok or not
    Public Function OK(ByVal UName As String, ByVal pass As String) As Boolean
        OK = False
        Dim strCmd As String
        Dim oCmd As SqlCommand
        strCmd = "select * from tblUsers where SalaryID='" & UName & "' and Password=" & Amstr(obj.Encrypted(pass)) & ""
        oCmd = New SqlCommand(strCmd, oCnn)
        Dim oDr As SqlDataReader
        oDr = oCmd.ExecuteReader
        If oDr.Read() Then
            OK = True
            uid = UName
        End If
        oDr.Close()
        oCmd.Dispose()
    End Function
    Public Sub WorkPerYear(ByVal ID As String, ByVal Val As Integer)
        Dim strCmd1 As String
        Dim oCmd1 As SqlCommand
        strCmd1 = "select SalaryID,HiredDate from TblEmployee where SalaryID='" & ID & "'"
        oCmd1 = New SqlCommand(strCmd1, oCnn)
        Dim oDr1 As SqlDataReader
        oDr1 = oCmd1.ExecuteReader
        If oDr1.Read() Then
            Val = oDr1(1).ToString
            'DateDiff(Now(), oDr1("HiredDate"), "YYYY")
        End If
        oDr1.Close()
        oCmd1.Dispose()

    End Sub

    'to know the permission of user
    Public Function FullWhat(ByVal UName As String, ByVal Modules As Integer) As Integer
        On Error GoTo errL
        FullWhat = 0
        Dim strCmd As String
        Dim oCmd As SqlCommand
        strCmd = "select * from tblPermission where SalaryID='" & UName & "' and Module=" & Modules
        oCmd = New SqlCommand(strCmd, oCnn)
        Dim oDr As SqlDataReader
        oDr = oCmd.ExecuteReader
        If oDr.Read() Then
            If oDr("UFull").ToString.Equals("1") Then
                'full permission
                FullWhat = 2
            ElseIf oDr("URead").ToString.Equals("1") Then
                'readonly
                FullWhat = 1
            ElseIf oDr("UInvisible").ToString.Equals("1") Then
                'no permission
                FullWhat = 0
            End If

        End If
        oDr.Close()
        oCmd.Dispose()
        Exit Function
errL:
        MsgBox(Err.Description, , "NiTA POS Solution")
    End Function
    'Enable control button in form for read only permission
    Sub ReadPermission(ByVal tab As TabControl)
        On Error Resume Next
        Dim t As TabPage
        For Each t In tab.TabPages
            Dim c As Control
            For Each c In t.Controls
                If TypeOf c Is System.Windows.Forms.Button And c.Tag <> "e" Then
                    c.Enabled = False
                End If
                If TypeOf c Is System.Windows.Forms.GroupBox Then
                    Dim bt As Control
                    For Each bt In c.Controls
                        If TypeOf bt Is System.Windows.Forms.Button And c.Tag <> "e" Then
                            c.Enabled = False
                        End If
                    Next
                End If
            Next
        Next
    End Sub
    'get autonumber of one table
    Public Function AutoNumber(ByVal SourceName As String, ByVal FieldName As String) As Integer
        On Error Resume Next
        Dim strCmd As String
        Dim oCmd As SqlCommand
        Dim GetValue As Integer
        strCmd = "select max(right(" & FieldName & ",4))+1 as MaxValue from " & SourceName
        System.Console.WriteLine(strCmd)
        oCmd = New SqlCommand(strCmd, oCnn)
        If IsDBNull(oCmd.ExecuteScalar) Then
            GetValue = 1
        Else
            GetValue = oCmd.ExecuteScalar
        End If
        oCmd.Dispose()
        Return GetValue

    End Function
    Public Function AutoBarCode(ByVal SourceName As String, ByVal FieldName As String) As String
        Dim strCmd As String
        Dim oCmd As SqlCommand
        Dim con As New SqlConnection(connectionString1)
        con.Open()
        Dim dr As SqlClient.SqlDataReader
        Dim GetValue As String
        strCmd = "select MAX(CONVERT(bigint,barcode))+1 from tblDelegate"
        oCmd = New SqlCommand(strCmd, con)
        'Dim barcode As Integer
        dr = oCmd.ExecuteReader
        If dr.Read = False Or Val(dr(0).ToString) = 0 Then

            GetValue = "0000000001"
        Else
            GetValue = dr(0)
        End If
        'GetValue = dr(0).ToString
        oCmd.Dispose()
        con.Close()
        Return GetValue
    End Function
    'get data from db to show in datagridview using 2 parameter
    Public Sub ShowDataGrid(ByVal Ctrl As DataGridView, ByVal str As String)
        Dim oDt As New System.Data.DataTable
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(str, connectionString1)
        oDa.Fill(oDt)
        Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
    End Sub
    'Add Data to combobox
    Public Sub SetFontDatagrid(ByVal dg As DataGridView)
        With dg.ColumnHeadersDefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .BackColor = Color.DarkRed
            .ForeColor = Color.Gold
            .Font = New System.Drawing.Font("Khmer OS Battambang", 9, FontStyle.Regular, GraphicsUnit.Point)
        End With
        '---------------------------------- Set font datagridview
        Dim cs As New DataGridViewCellStyle
        cs.ForeColor = Color.Black
        cs.Font = New System.Drawing.Font("Khmer OS Battambang", 9, FontStyle.Regular)
        dg.RowsDefaultCellStyle = cs
        '---------------------------------------------------------
    End Sub

    Public Sub ToExcel(ByVal Dg As DataGridView)
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New X.Application
        Try
            Dim excelBook As X.Workbook = xlApp.Workbooks.Add
            '-----------------------------------------------------------
            'Excel.Workbook.open()
            'Excel.Workbooks.Open("")
            Dim excelWorksheet As X.Worksheet = CType(excelBook.Worksheets(1), X.Worksheet)
            xlApp.Visible = True
            'X.Workbooks.Open()
            rowsTotal = Dg.RowCount - 1
            colsTotal = Dg.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                For iC = 0 To colsTotal
                    .Cells(1, iC + 1).Value = Dg.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal
                    For j = 0 To colsTotal
                        .Cells(I + 2, j + 1).value = Dg.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Regular"
                .Rows("1:1").Font.Size = 10
                .Cells.Columns.AutoFit()
                .Cells.Select()
                .Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
                .Cells.Font.Name = "Khmer os battambang"
                .Cells.Font.Size = 10
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
    Public Sub excelexport(ByVal DataGridView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer, ByVal endcolumnBorder As String, ByVal StartMergeCell As String, ByVal EndMergeCell As String, ByVal startColumnSum As String, ByVal EndColumnSum As String)
        Dim rowsTotal, colsTotal As Short
        Dim I, j As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        ''-----------------------------------------------------------------------------
        Try
            '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(SampleLocation)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
            xlApp.Visible = True
            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                '.Cells.Select()
                '.Cells.Delete()
                'For iC = 0 To colsTotal
                '    .Cells(5, iC + 1).Value = DataGridView1.Columns(iC).HeaderText
                'Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                        'MessageBox.Show(I & " " & j)
                    Next (j)
                Next I
                .Rows("1:1").Font.FontStyle = "Bold"
                .Rows("1:1").Font.Size = 12
                '.Rows("1:1").font.size =
                .Cells.Columns.AutoFit()
                .Cells.Select()
                '-----------------------------------------
                Dim selection As Excel.Range
                selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & I + startRow)
                selection.Borders.Weight = Excel.XlBorderWeight.xlThin
                '------------------------------------------
                'MessageBox.Show(I)
                excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).MergeCells = True
                excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).Value = "Total:"
                '----excelWorksheet.Range(startColumnSum & j + 2 & ":" & EndColumnSum & j + 2).Value = "=SUM(" & startColumnSum & startRow & ":" & startColumnSum & j + 1 & ")"
                excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).Value = "=Sum(" & startColumnSum & startRow & ":" & startColumnSum & I + startRow - 1 & ")"
                'excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                'excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & I + startRow).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
                'excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & j + 2).Font.Bold = True
                .Cells(1, 1).Select()
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
    Public Sub excelexportCount(ByVal DataGridView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer, ByVal endcolumnBorder As String, ByVal StartMergeCell As String, ByVal EndMergeCell As String, ByVal startColumnSum As String, ByVal EndColumnSum As String)
        Dim rowsTotal, colsTotal As Short
        Dim I, j As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        ''-----------------------------------------------------------------------------
        Try
            '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(SampleLocation)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
            xlApp.Visible = True
            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                '.Cells.Select()
                '.Cells.Delete()
                'For iC = 0 To colsTotal
                '    .Cells(5, iC + 1).Value = DataGridView1.Columns(iC).HeaderText
                'Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                        'MessageBox.Show(I & " " & j)
                    Next (j)
                Next I
                '.Rows("1:1").Font.FontStyle = "Bold"
                '.Rows("2:2").Font.Size = 10
                '.Rows("1:1").font.size =
                '.Cells.Columns.AutoFit()
                .Cells.Select()
                '-----------------------------------------
                Dim selection As Excel.Range
                selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & j + I + 3)
                selection.Borders.Weight = Excel.XlBorderWeight.xlThin
                '------------------------------------------
                excelWorksheet.Range(StartMergeCell & j + I + 3 & ":" & EndMergeCell & j + I + 3).MergeCells = True
                excelWorksheet.Range(StartMergeCell & j + I + 3 & ":" & EndMergeCell & j + I + 3).Value = "Total:"
                '----excelWorksheet.Range(startColumnSum & j + 2 & ":" & EndColumnSum & j + 2).Value = "=SUM(" & startColumnSum & startRow & ":" & startColumnSum & j + 1 & ")"
                excelWorksheet.Range(startColumnSum & j + I + 3 & ":" & EndColumnSum & j + I + 3).Value = "=Counta(" & startColumnSum & startRow & ":" & startColumnSum & j + I + 2 & ")"
                excelWorksheet.Range(startColumnSum & j + I + 3 & ":" & EndColumnSum & j + I + 3).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                excelWorksheet.Range("a" & startRow - 1 & ":" & endcolumnBorder & j + 2).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
                excelWorksheet.Range("a" & startRow - 1 & ":" & endcolumnBorder & j + 2).Font.Bold = True
                .Cells(1, 1).Select()

            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
    Public Sub excelexportNormal(ByVal DataGridView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer, ByVal endcolumnBorder As String)
        Dim rowsTotal, colsTotal As Short
        Dim I, j As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        ''-----------------------------------------------------------------------------
        Try
            '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(SampleLocation)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
            xlApp.Visible = True
            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                '.Cells.Select()
                '.Cells.Delete()
                'For iC = 0 To colsTotal
                '    .Cells(5, iC + 1).Value = DataGridView1.Columns(iC).HeaderText
                'Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                        'MessageBox.Show(I & " " & j)
                    Next (j)
                Next I
                '.Rows("1:1").Font.FontStyle = "Bold"
                '.Rows("1:1").Font.Size = 12
                '.Rows("1:1").font.size =
                '.Cells.Columns.AutoFit()
                '.Cells.Select()
                '-----------------------------------------
                Dim selection As Excel.Range
                selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & I + startRow)
                selection.Borders.Weight = Excel.XlBorderWeight.xlThin
                '------------------------------------------
                ''MessageBox.Show(I)
                'excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).MergeCells = True
                'excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).Value = "Total:"
                ''----excelWorksheet.Range(startColumnSum & j + 2 & ":" & EndColumnSum & j + 2).Value = "=SUM(" & startColumnSum & startRow & ":" & startColumnSum & j + 1 & ")"
                'excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).Value = "=Sum(" & startColumnSum & startRow & ":" & startColumnSum & I + startRow - 1 & ")"
                ''excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                ''excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & I + startRow).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
                'excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & j + 2).Font.Bold = True
                '.Cells(1, 1).Select()
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
    Public Sub excelexportNormalNew(ByVal DataGridView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer, ByVal endcolumnBorder As String)
        Dim rowsTotal, colsTotal As Short
        Dim I, j As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        ''-----------------------------------------------------------------------------
        Try
            '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(SampleLocation)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
            xlApp.Visible = True
            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                '.Cells.Select()
                '.Cells.Delete()
                'For iC = 0 To colsTotal
                '    .Cells(5, iC + 1).Value = DataGridView1.Columns(iC).HeaderText
                'Next
                .Cells(4, 1).value = frmMain.lblName.Text
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                        'MessageBox.Show(I & " " & j)
                    Next (j)
                Next I
                '.Rows("1:1").Font.FontStyle = "Bold"
                '.Rows("1:1").Font.Size = 12
                '.Rows("1:1").font.size =
                '.Cells.Columns.AutoFit()
                '.Cells.Select()
                '-----------------------------------------
                Dim selection As Excel.Range
                selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & I + startRow)
                selection.Borders.Weight = Excel.XlBorderWeight.xlThin
                '------------------------------------------
                ''MessageBox.Show(I)
                'excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).MergeCells = True
                'excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).Value = "Total:"
                ''----excelWorksheet.Range(startColumnSum & j + 2 & ":" & EndColumnSum & j + 2).Value = "=SUM(" & startColumnSum & startRow & ":" & startColumnSum & j + 1 & ")"
                'excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).Value = "=Sum(" & startColumnSum & startRow & ":" & startColumnSum & I + startRow - 1 & ")"
                ''excelWorksheet.Range(startColumnSum & I + startRow & ":" & EndColumnSum & I + startRow).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                ''excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & I + startRow).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter
                'excelWorksheet.Range("a" & startRow & ":" & endcolumnBorder & j + 2).Font.Bold = True
                '.Cells(1, 1).Select()
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
    Public Sub AddCombo(ByVal cbo As ComboBox, ByVal SQL As String)
        Try
            Dim conn As New SqlClient.SqlConnection(connectionString1)
            conn.Open()
            Dim cmd1 As New SqlClient.SqlCommand(SQL, conn)
            Dim dr1 As SqlClient.SqlDataReader
            dr1 = cmd1.ExecuteReader
            If cbo.Items.Count > 0 Then
                cbo.Items.Clear()
            End If
            While dr1.Read
                cbo.Items.Add(dr1.Item(0))
            End While
            dr1.Close()
            cmd1.Dispose()
            conn.Close()
        Catch ex As System.Exception
            MessageBox.Show("Error ComboBox data", "NiTA POS Solution")
        End Try
    End Sub
    '------------------------------------------------------------------
    Public Sub AddCombobox(ByVal cbo As ComboBox, ByVal col As String, ByVal TableSource As String, ByVal cond As String)
        Dim sql As String
        On Error Resume Next
        sql = "select " & col & " from " & TableSource
        If cond <> "" Then
            sql += " where " & cond
        End If
        Dim cmd1 As New SqlCommand(sql, oCnn)
        Dim dr1 As SqlDataReader
        dr1 = cmd1.ExecuteReader
        If cbo.Items.Count > 0 Then
            cbo.Items.Clear()
        End If
        While dr1.Read
            cbo.Items.Add(dr1.Item(0))
        End While
        dr1.Close()
        cmd1.Dispose()
    End Sub
    'get data from db to show in datagridview using 2 parameter
    Public Sub ShowDataGrid1(ByVal Ctrl As DataGridView, ByVal str As String, ByVal oDt As System.Data.DataTable)
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(str, oCnn)
        oDa.Fill(oDt)
        Ctrl.DataSource = oDt
        oDt.Dispose()
        oDa.Dispose()
    End Sub
    'get type of OT public holiday or sunday or normal
    Public Sub CheckKeyIsNumericUP(ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal txt As System.Windows.Forms.TextBox)
        On Error Resume Next
        Dim k As Integer = Asc(e.KeyChar)
        If ((k >= System.Windows.Forms.Keys.D0 And k <= System.Windows.Forms.Keys.D9) Or k = 46) Or k = System.Windows.Forms.Keys.Back Then
            e.Handled = False
        Else
            MsgBox("Invalid Number", MsgBoxStyle.Critical)
            e.Handled = True

        End If

        If InStr(txt.Text, ".") <= 1 Or (k >= System.Windows.Forms.Keys.D0 And k <= System.Windows.Forms.Keys.D9) Or k = System.Windows.Forms.Keys.Back Then
        Else
            e.Handled = True
        End If

    End Sub
    Public Sub CheckKeyIsNumeric(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim k As Integer = Asc(e.KeyChar)
        If (k >= System.Windows.Forms.Keys.D0 And k <= System.Windows.Forms.Keys.D9) Or k = System.Windows.Forms.Keys.Back Then
            e.Handled = False
        Else
            ' MsgBox("Invalid Number", MsgBoxStyle.Critical)
            e.Handled = True
        End If
    End Sub
    'Voudda's addtional
    Public Sub AddtoText(ByVal text As System.Windows.Forms.TextBox, ByVal Value As String)
        text.Text = Value
    End Sub
    Public Sub AddtoCombo(ByVal text As ComboBox, ByVal Value As String)
        text.Text = Value
    End Sub
    Public Function getId(ByVal field As String, ByVal table As String, ByVal conditiion As String) As String
        Dim str As String = ""
        Dim id As String = ""
        Dim oCmd As SqlCommand
        Dim dr As SqlDataReader

        Try
            str = "select " & field & " from " & table & " " & conditiion
            oCnn = New SqlConnection(connectionString1)
            oCnn.Open()
            oCmd = New SqlCommand(str, oCnn)
            dr = oCmd.ExecuteReader
            If dr.Read Then
                id = dr(0).ToString
            End If
            dr.Close()
            oCnn.Close()
            oCmd.Dispose()

        Catch ex As System.Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try

        Return id
    End Function
    Public Function GetLastID(ByVal field As String, ByVal table As String) As String
        Dim Q As String = ""
        Dim GLID As String
        Dim Cmd As SqlCommand
        Dim conn As New SqlConnection
        Dim dr As SqlDataReader
        GLID = ""
        Try
            conn.ConnectionString = connectionString1
            conn.Open()
            Q = "select  top 1 " & field & "  from  " & table & " order by  " & field & " Desc"
            Cmd = New SqlCommand(Q, conn)
            dr = Cmd.ExecuteReader
            If dr.Read = True Then
                GLID = dr(0).ToString
            End If

            dr.Close()
            GetLastID = GLID

        Catch ex As System.Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
        Return GLID
    End Function
    Sub showInExcel(ByVal sql As String, ByVal startCol As Integer, ByVal startRo As Integer, ByVal N As Integer, ByVal ws As Microsoft.Office.Interop.Excel.Worksheets, ByVal pathname As String, ByVal pathdest As String, ByVal filename As String, ByVal Xapp As Microsoft.Office.Interop.Excel.Application)
        Dim con As New SqlClient.SqlConnection
        Dim com As New SqlClient.SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Dim i As Integer
        Dim j As Integer
        Try
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = sql
            Dim W As Microsoft.Office.Interop.Excel.Worksheet
            dr = com.ExecuteReader
            If System.IO.File.Exists(pathdest & filename) Then
                System.IO.File.Delete(pathdest & filename)
            End If
            System.IO.File.Copy(pathname & filename, pathdest & filename)

            W = Xapp.Workbooks.Open(pathdest & filename)
            Xapp.Visible = True

            Dim Rng As Microsoft.Office.Interop.Excel.Range
            ws = W.ActiveSheet
            j = 0
            Do While dr.Read = True
                Rng = ws.Range("A" & startRo + j + 1)
                Rng.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, False)
                ws.Cells(startRo + j, startCol - 1) = j + 1
                For i = 0 To N - 1
                    ws.Cells(startRo + j, startCol + i) = dr(i).ToString
                Next
                j += 1
            Loop
            dr.Close()
            con.Close()
            com.Dispose()
        Catch ex As System.Exception
            Xapp.Application.Quit()
            MessageBox.Show(Err.Description, "NiTA HR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function getLastRow() As String
        Dim Q As String = ""
        Dim GLID As String = ""
        Try
            Dim Cmd As SqlClient.SqlCommand
            Dim cnn As New SqlClient.SqlConnection(connectionString1)
            cnn.Open()
            Dim dr As SqlClient.SqlDataReader
            Q = "select top 1 EmployeeID from tbluser order by EmployeeID Desc"
            Cmd = New SqlClient.SqlCommand(Q, cnn)
            dr = Cmd.ExecuteReader
            If dr.Read Then
                GLID = dr(0)
            Else
                GLID = "00"
            End If
            dr.Close()
        Catch ex As System.Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
        Return GLID
    End Function
    Public Function getLastRows() As Integer
        Dim Q As String = ""
        Dim GLID As Integer
        Try
            Dim Cmd As SqlClient.SqlCommand
            Dim cnn As New SqlClient.SqlConnection(connectionString1)
            cnn.Open()
            Dim dr As SqlClient.SqlDataReader
            Q = "select top 1 EmpID from tblEmployees order by EmpID Desc"
            Cmd = New SqlClient.SqlCommand(Q, cnn)
            dr = Cmd.ExecuteReader
            If dr.Read Then
                GLID = Val(dr(0).ToString)
            Else
                GLID = 0
            End If
            dr.Close()
        Catch ex As System.Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
        Return GLID
    End Function
    Sub FillGridView(ByVal sql As String, ByVal dgv As DataGridView, ByVal tbl As String, ByVal ParamArray ctrl() As Object)
        Try
            dgv.DataSource = Nothing
            Dim con As SqlClient.SqlConnection
            Dim com As SqlClient.SqlDataAdapter
            Dim DtSet As System.Data.DataSet
            con = New SqlClient.SqlConnection(connectionString1)
            com = New SqlClient.SqlDataAdapter(sql, con)
            com.SelectCommand.CommandTimeout = 30000
            For i = 0 To ctrl.Count - 1
                If TypeOf ctrl(i) Is Control Then
                    com.SelectCommand.Parameters.AddWithValue("@A" & (i + 1), DirectCast(ctrl(i).text, Object))
                ElseIf TypeOf ctrl(i) Is String Or TypeOf ctrl(i) Is Decimal Or TypeOf ctrl(i) Is Double Or TypeOf ctrl(i) Is Integer Or TypeOf ctrl(i) Is Short Then
                    com.SelectCommand.Parameters.AddWithValue("@A" & (i + 1), ctrl(i))
                End If
            Next
            com.TableMappings.Add("Table", "TestTable")
            DtSet = New System.Data.DataSet
            com.Fill(DtSet)
            dgv.DataSource = DtSet.Tables(0)
            con.Close()
            com.SelectCommand.Parameters.Clear()
            frmMain.mystatus.Text = dgv.RowCount & " of " & QueryStr("Select count(*) from " & tbl & "") & " row(s)"
            If dgv.Tag = "hide" Then
                dgv.Columns(0).Visible = False
            End If
            dgv.ClearSelection()
        Catch ex As System.Exception
            MessageBox.Show(ex.Message, "Error DataAdapter", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Public Sub BuildWorksheet(ByVal SQL As String, ByVal Adapt As SqlDataAdapter, ByVal DS As DataSet, ByVal startCol As Integer, ByVal StartRow As Integer, ByVal sheetname As String, ByVal Table As String, ByVal sourcefile As String, ByVal filename As String, ByVal ParamArray ctrl() As Object)
        Try
            Dim con As New SqlConnection(connectionString1)
            Dim com As New SqlCommand(SQL, con)
            com.CommandTimeout = 30000
            For i = 0 To ctrl.Count - 1
                If TypeOf ctrl(i) Is Control Then
                    com.Parameters.AddWithValue("@A" & (i + 1), DirectCast(ctrl(i).text, Object))
                ElseIf TypeOf ctrl(i) Is String Or TypeOf ctrl(i) Is Decimal Or TypeOf ctrl(i) Is Double Or TypeOf ctrl(i) Is Integer Or TypeOf ctrl(i) Is Short Then
                    com.Parameters.AddWithValue("@A" & (i + 1), ctrl(i))
                End If
            Next
            Adapt = New SqlDataAdapter(com)
            DS = New DataSet("CT")
            Adapt.Fill(DS, Table)
            'ExportToExcel() ''''''''''''
            Dim dt As System.Data.DataTable
            Dim xl As New Microsoft.Office.Interop.Excel.Application
            If System.IO.File.Exists(filename) Then
                System.IO.File.Delete(filename)
            End If
            System.IO.File.Copy(sourcefile, filename)
            xl.Workbooks.Open(filename)
            xl.ActiveSheet.Name = sheetname
            xl.Visible = True
            'xl.Range("A1").Value = "Loading the DataSet...."
            Try
                xl.ScreenUpdating = False
                dt = DS.Tables(Table)
                Dim dc As DataColumn
                For Each dc In dt.Columns
                    xl.Range("A" & StartRow).Offset(0, startCol).Value = dc.ColumnName
                    startCol += 1
                Next
                Dim iRows As Int32
                For iRows = 0 To dt.Rows.Count - 1
                    xl.Range("A" & (StartRow + 1).ToString).Offset(iRows).Resize(1, startCol).Value = dt.Rows(iRows).ItemArray()
                Next
                xl.ActiveSheet.Range("A" & (StartRow + 1).ToString).AutoFilter()
                xl.ActiveSheet.Range("A" & (StartRow + 2).ToString).AutoFormat(Microsoft.Office.Interop.Excel.XlRangeAutoFormat.xlRangeAutoFormatSimple)

            Catch ex As System.Exception
            Finally
                xl.ScreenUpdating = True
                xl.ActiveWorkbook.PrintPreview()
            End Try
            xl = Nothing
            com.Parameters.Clear()
        Catch ex As System.Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "IT Solution")
        End Try
    End Sub
    Public Function QueryStr(ByVal sql As String, ByVal ParamArray ctrl() As Object) As String
        Dim value As String = ""
        Dim con As New SqlClient.SqlConnection
        Dim com As New SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Try
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = sql
            com.CommandTimeout = 30000
            For i = 0 To ctrl.Count - 1
                If TypeOf ctrl(i) Is String Or TypeOf ctrl(i) Is Decimal Or TypeOf ctrl(i) Is Double Or TypeOf ctrl(i) Is Integer Or TypeOf ctrl(i) Is Short Then
                    com.Parameters.AddWithValue("@A" & (i + 1), DirectCast(ctrl(i), Object))
                ElseIf TypeOf ctrl(i) Is Control Then
                    com.Parameters.AddWithValue("@A" & (i + 1), DirectCast(ctrl(i).text, Object))
                End If
            Next
            dr = com.ExecuteReader
            If dr.Read = True Then
                value = IIf(IsDBNull(dr(0)), "", dr(0))
            End If
            dr.Close()
            con.Close()
            com.Parameters.Clear()
        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try
        Return value
    End Function
    Public Function QueryVal(ByVal sql As String, ByVal ParamArray ctrl() As Object) As Double
        Dim value As Double = 0
        Dim con As New SqlClient.SqlConnection
        Dim com As New SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Try
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = sql
            com.CommandTimeout = 30000
            For i = 0 To ctrl.Count - 1
                If TypeOf ctrl(i) Is String Or TypeOf ctrl(i) Is Decimal Or TypeOf ctrl(i) Is Double Or TypeOf ctrl(i) Is Integer Or TypeOf ctrl(i) Is Short Then
                    com.Parameters.AddWithValue("@A" & (i + 1), DirectCast(ctrl(i), Object))
                ElseIf TypeOf ctrl(i) Is Control Then
                    com.Parameters.AddWithValue("@A" & (i + 1), DirectCast(ctrl(i).text, Object))
                End If
            Next
            dr = com.ExecuteReader
            If dr.Read = True Then
                value = IIf(IsDBNull(dr(0)), "", dr(0))
            End If
            dr.Close()
            con.Close()
            com.Parameters.Clear()
        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try
        Return value
    End Function
    Sub ExporttoExcel(ByVal DataGridView1 As DataGridView)
        '***********************************************************
        '***************Code from visiblevisual.com*****************
        '***********************************************************
        'verfying the datagridview having data or not
        If ((DataGridView1.Columns.Count = 0) Or (DataGridView1.Rows.Count = 0)) Then
            Exit Sub
        End If

        'Creating dataset to export
        Dim dset As New DataSet
        'add table to dataset
        dset.Tables.Add()
        'add column to that table
        For i As Integer = 0 To DataGridView1.ColumnCount - 1
            If DataGridView1.Columns(i).Visible = True Then
                dset.Tables(0).Columns.Add(DataGridView1.Columns(i).HeaderText)
            End If
        Next
        'Dim celltext As String
        Dim count As Integer = -1
        'add rows to the table
        Dim dr1 As DataRow
        For i As Integer = 0 To DataGridView1.RowCount - 1
            dr1 = dset.Tables(0).NewRow
            For j As Integer = 0 To DataGridView1.Columns.Count - 1
                If DataGridView1.Columns(j).Visible = True Then
                    count = count + 1

                    dr1(count) = DataGridView1.Rows(i).Cells(j).Value
                End If
            Next

            count = -1
            dset.Tables(0).Rows.Add(dr1)
        Next

        Dim excel As New Excel.Application
        Dim wBook As Excel.Workbook
        Dim wSheet As Excel.Worksheet

        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()


        Dim dt As System.Data.DataTable = dset.Tables(0)
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            excel.Cells(1, colIndex) = dc.ColumnName
        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)

            Next
        Next

        wSheet.Columns.AutoFit()



        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Excel Workbook|*.xls|Excel Workbook 2011|*.xlsx"
        saveFileDialog1.Title = "Save Excel File"
        saveFileDialog1.FileName = "Export " & Now.ToShortDateString & ".xls"
        saveFileDialog1.ShowDialog()

        saveFileDialog1.InitialDirectory = "C:/"
        If saveFileDialog1.FileName <> "" Then

            Dim fs As System.IO.FileStream = CType(saveFileDialog1.OpenFile(), System.IO.FileStream)
            fs.Close()
        End If


        Dim strFileName As String = saveFileDialog1.FileName
        Dim blnFileOpen As Boolean = False


        Try
            Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
            fileTemp.Close()
        Catch ex As System.Exception
            blnFileOpen = False
            Exit Sub
        End Try

        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If
        '------------------------------------
        wBook.SaveAs(strFileName)
        excel.Workbooks.Open(strFileName)
        excel.Visible = True
        Exit Sub
errorhandler:
        MsgBox(Err.Description)
    End Sub
End Module
