Imports System.Data
Imports System.Data.SqlClient
Imports Encrypted
Imports Microsoft.Office.Interop
Imports X = Microsoft.Office.Interop.Excel
Imports System.Drawing
Module moremode
    Public DB As String = ""
    Public g_cnn As SqlConnection
    Public result As String = ""
    Public resultError As String = ""
    Public p As String
    Public oDt1 As System.Data.DataSet
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
    Public ds1 As New DataSet
    Public server As String = ""
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
            MessageBox.Show("Error", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function CheckPermission(MenuId As Integer, acct As String, privilegelvl As Integer) As Boolean
        Try
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            com.Connection = g_cnn
            com.CommandText = "SELECT * FROM sys_UserPrivilege WHERE User_Name='" & acct & "' AND PrivilegeID = " & privilegelvl & " AND MenuID = " & MenuId
            dr = com.ExecuteReader
            If dr.Read = True Then
                dr.Close()
                Return True
            Else
                dr.Close()
                Return False
            End If
        Catch ex As System.Exception
            MessageBox.Show(Err.Description, "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
    Public Sub ExportDatagridViewToExcel1(dv As DataGridView, filePath As String, name As String)
        If filePath = "" Then
            Throw New Exception("File Path cannot be empty!")
        ElseIf System.IO.Path.GetExtension(filePath).ToLower().Contains("xls") = False Then
            '            MsgBox(System.IO.Path.GetExtension(filePath).ToLower())
            Throw New Exception("File extension should be *.xls")
        End If
        Dim content As String = ""
        Dim colName As New List(Of String)
        For Each col As DataGridViewColumn In dv.Columns
            'If col.Displayed = False Then
            '    Continue For
            'End If

            content &= "<th>" & col.HeaderText & "</th>"
            colName.Add(col.Name)
        Next
        content = "<tr>" & content & "</tr>"

        Dim cnt As Integer = dv.Rows.Count - 1
        If dv.AllowUserToAddRows = True Then
            cnt -= 1
        End If

        Dim cel As Integer = colName.Count - 1
        For i As Integer = 0 To cnt
            content &= "<tr>"
            For j As Integer = 0 To cel
                content &= "<td>" & dv.Rows(i).Cells(colName(j)).Value.ToString() & "</td>"
            Next
            content &= "</tr>"
        Next
        content = "<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office' " _
& "xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'> <head><meta http-equiv=Content-Type content='text/html; charset=utf-8'><meta name=ProgId content=Excel.Sheet><meta name=Generator content='Microsoft Excel 10'>" _
& " <style type='text/css'>table{border:1px solid #e5e3e3;border-collapse:collapse;font-family:'khmer os', sans-serif; font-size:9pt; mso-displayed-decimal-separator:'\.';mso-displayed-thousand-separator:'\,'}table td, table th{border: 1px solid #e5e3e3; height:36px}table th{background:#F5F5F5}table thead td{vertical-align:middle; text-align:center}</style> " _
& "</head><body><h1>" & name & "</h1><table>" & content & "</table></body></html>"
        '& "</head><body><h1>" & frmResultReport.lblStartDate.Text & "</h1><table>" & content & "</table></body></html>"
        Try
            System.IO.File.WriteAllText(filePath, content)
            MessageBox.Show("Excel is exported at D:\" & name & ".xls", "Completed!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If MessageBox.Show("Do you to open file " & name & "?", "IT Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Process.Start("D:\" & name & ".xls")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            'Throw New Exception("Unable to save because of permission denied or file is being used by another application.")
        End Try
    End Sub
    Public Sub ExportDatagridViewToExcel(dv As DataGridView, filePath As String)
        If filePath = "" Then
            Throw New Exception("File Path cannot be empty!")
        ElseIf System.IO.Path.GetExtension(filePath).ToLower().Contains("xls") = False Then
            '            MsgBox(System.IO.Path.GetExtension(filePath).ToLower())
            Throw New Exception("File extension should be *.xls")
        End If
        Dim content As String = ""
        Dim colName As New List(Of String)
        For Each col As DataGridViewColumn In dv.Columns
            'If col.Displayed = False Then
            '    Continue For
            'End If

            content &= "<th>" & col.HeaderText & "</th>"
            colName.Add(col.Name)
        Next
        content = "<tr>" & content & "</tr>"

        Dim cnt As Integer = dv.Rows.Count - 1
        If dv.AllowUserToAddRows = True Then
            cnt -= 1
        End If

        Dim cel As Integer = colName.Count - 1
        For i As Integer = 0 To cnt
            content &= "<tr>"
            For j As Integer = 0 To cel
                content &= "<td>" & dv.Rows(i).Cells(colName(j)).Value.ToString() & "</td>"
            Next
            content &= "</tr>"
        Next
        content = "<html xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office' " _
& "xmlns:x='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'> <head><meta http-equiv=Content-Type content='text/html; charset=utf-8'><meta name=ProgId content=Excel.Sheet><meta name=Generator content='Microsoft Excel 10'>" _
& " <style type='text/css'>table{border:1px solid #e5e3e3;border-collapse:collapse;font-family:'khmer os', sans-serif; font-size:9pt; mso-displayed-decimal-separator:'\.';mso-displayed-thousand-separator:'\,'}table td, table th{border: 1px solid #e5e3e3; height:36px}table th{background:#F5F5F5}table thead td{vertical-align:middle; text-align:center}</style> " _
& "</head><body><h1>" & frmResultReport.lblNameReport.Text & "</h1><br><h3> Date: " & frmResultReport.lblStartDate.Text & " - " & frmResultReport.lblEndDate.Text & "</br></h3><table>" & content & "</table></body></html>"
        '& "</head><body><h1>" & frmResultReport.lblStartDate.Text & "</h1><table>" & content & "</table></body></html>"
        Try
            System.IO.File.WriteAllText(filePath, content)
            MessageBox.Show("Excel is exported at D:\" & frmResultReport.lblNameReport.Text & ".xls", "Completed!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            If MessageBox.Show("Do you to open file " & frmResultReport.lblNameReport.Text & "?", "IT Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Process.Start("D:\" & frmResultReport.lblNameReport.Text & ".xls")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            'Throw New Exception("Unable to save because of permission denied or file is being used by another application.")
        End Try
    End Sub

    Public Function MakeTColumn(pstrName As String, pstrHeader As String, pintWidth As Integer, pVisible As Boolean) As DataGridViewColumn
        Dim col = New DataGridViewTextBoxColumn()
        col.DataPropertyName = pstrName
        col.Name = pstrName
        col.HeaderText = pstrHeader
        If (pintWidth > 0) Then col.Width = pintWidth
        col.Visible = pVisible
        Return col
    End Function
    Public Sub Combobox_Datasource(ByVal cboName As ComboBox, ByVal SourceName As String, ByVal DisplayField As String, ByVal ValueField As String, ByVal Cond As String, ByVal sqlCon As SqlConnection)
        Dim strDatasource As String
        Dim oDt As New System.Data.DataTable
        Dim x As New System.Data.DataTable
        Try
            strDatasource = "select distinct " & DisplayField & "," & ValueField & " from " & SourceName
            If Cond <> "" Then
                strDatasource += " where " & Cond
            End If
            oDa = New SqlDataAdapter(strDatasource, sqlCon)
            oDa.Fill(oDt)
            cboName.DataSource = oDt
            cboName.DisplayMember = oDt.Columns(0).ToString
            cboName.ValueMember = oDt.Columns(1).ToString
            cboName.SelectedIndex = -1
            oDa = Nothing
        Catch ex As System.Exception
        End Try
    End Sub
    Public Function ChkCnn(ByVal ServerName As String, ByVal Database As String, ByVal User As String, ByVal Pwd As String) As Boolean
        DB = Database
        Dim str As String
        str = "SERVER=" & ServerName & ";Database=" & Database & ";User ID=" & User & ";Pwd=" & Pwd
        Try
            g_cnn = New SqlConnection(str)
            g_cnn.Open()
            server = ServerName.ToString
            Return True
        Catch ex As System.Exception
            MsgBox("Cannot connect to server " & ex.Message)
            Return False
        End Try
    End Function
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
            Dim cmd As New SqlCommand(SQL, g_cnn)
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
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
    End Function
    Public Sub toExcelProfit()
        Dim iRow = frmResultReport.DataGridView1.CurrentCell.RowIndex
        ''Dim cnn As SqlConnection
        Dim connectionString As String = Nothing
        Dim sql As String = Nothing
        Dim data As String = Nothing
        Dim i As Integer = 0
        Dim j As Integer = 0
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Microsoft.Office.Interop.Outlook.Application
        Dim misValue As Object = System.Reflection.Missing.Value
        xlApp = New Excel.Application()
        '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
        Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(frmMain.strPath & "\Profit.xls", False, True)
        Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
        xlApp.Visible = True
        'cnn = New SqlConnection(connectionString1)
        'cnn.Open()
        sql = ""
        'Dim count As Integer = getData("select COUNT(LD_ID) from BK_LoanSchedule where LD_ID='" & frmResultReport.DataGridView1.Rows(iRow).Cells(1).Value & "' and SH_BrId='" & frmMain.lblCode.Text & "'")
        Dim dscmd As New SqlDataAdapter(sql, g_cnn)
        Dim ds As New DataSet()
        dscmd.Fill(ds)
        With excelWorksheet
            For i = 0 To ds.Tables(0).Rows.Count - 1
                For j = 0 To ds.Tables(0).Columns.Count - 1
                    data = ds.Tables(0).Rows(i).ItemArray(j).ToString()
                    .Cells(i + 7, j + 2) = data
                    .Cells(i + 7, 1) = i + 1
                Next
                If .Cells(i + 7, 3).value = 1 Then
                    .Cells(i + 7, 3) = "ច័ន្ទ"
                ElseIf .Cells(i + 7, 3).value = 2 Then
                    .Cells(i + 7, 3) = "អង្គារ"
                ElseIf .Cells(i + 7, 3).value = 3 Then
                    .Cells(i + 7, 3) = "ពុធ"
                ElseIf .Cells(i + 7, 3).value = 4 Then
                    .Cells(i + 7, 3) = "ព្រហស្បតិ៍"
                ElseIf .Cells(i + 7, 3).value = 5 Then
                    .Cells(i + 7, 3) = "សុក្រ"
                End If
            Next
        End With
    End Sub
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
            'Dim con As New SqlConnection
            'con.ConnectionString = g_cnn
            'con.Open()
            Dim cmd As New SqlCommand(SQL, g_cnn)
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
        'Dim con As New SqlConnection(g_cnn)
        'con.Open()
        Dim dr As SqlClient.SqlDataReader
        Dim GetValue As String
        strCmd = "select MAX(CONVERT(bigint,barcode))+1 from tblDelegate"
        oCmd = New SqlCommand(strCmd, g_cnn)
        'Dim barcode As Integer
        dr = oCmd.ExecuteReader
        If dr.Read = False Or Val(dr(0).ToString) = 0 Then

            GetValue = "0000000001"
        Else
            GetValue = dr(0)
        End If
        'GetValue = dr(0).ToString
        oCmd.Dispose()
        'con.Close()
        Return GetValue
    End Function
    Public Sub UpdateCustomer(ByVal CM_KhName As String, ByVal LO_ID As Integer, ByVal CM_Phone As String, ByVal CM_User_Modify As String, ByVal CM_Date_Modify As DateTime)
        Dim iRow = FrmCustomer.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = CM_KhName
                .Add("@d1", SqlDbType.Int).Value = LO_ID
                .Add("@d2", SqlDbType.NVarChar).Value = CM_Phone
                .Add("@d3", SqlDbType.NVarChar).Value = CM_User_Modify
                .Add("@d4", SqlDbType.DateTime).Value = CM_Date_Modify
            End With
            If FrmCustomer.Text = "CustomerOther" Then
                sql = "update BK_CustomerOther set CM_KhName=@d0,LO_ID=@d1,CM_Phone=@d2,CM_User_Modify=@d3,CM_Date_Modify=@d4 Where Status='Active' and CM_ID='" & FrmCustomer.DataGridView1.Rows(iRow).Cells(1).Value & "' and CM_BrId='" & frmMain.lblCode.Text & "'"
            Else
                sql = "update BK_Customer set CM_KhName=@d0,LO_ID=@d1,CM_Phone=@d2,CM_User_Modify=@d3,CM_Date_Modify=@d4 Where Status='Active' and CM_ID='" & FrmCustomer.DataGridView1.Rows(iRow).Cells(1).Value & "' and CM_BrId='" & frmMain.lblCode.Text & "'"
            End If
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As SystemException
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub addCustomer(ByVal CM_ID As Integer, ByVal CM_KhName As String, ByVal LO_ID As Integer, ByVal CM_Phone As String, ByVal CM_BrId As String, ByVal CM_Rec_Status As Integer, ByVal CM_User_Create As String, ByVal CM_Date_Create As DateTime)
        Dim iRow = FrmCustomer.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = CM_ID
                .Add("@d1", SqlDbType.NVarChar).Value = CM_KhName
                .Add("@d2", SqlDbType.Int).Value = LO_ID
                .Add("@d3", SqlDbType.NVarChar).Value = CM_Phone
                .Add("@d4", SqlDbType.NVarChar).Value = CM_BrId
                .Add("@d5", SqlDbType.Int).Value = CM_Rec_Status
                .Add("@d6", SqlDbType.NVarChar).Value = CM_User_Create
                .Add("@d7", SqlDbType.DateTime).Value = CM_Date_Create
                Dim a As String = ""
                If FrmCustomer.Text = "CustomerOther" Then
                    a = getData("select max(ID)ID from BK_CustomerOther where CM_BrId='" & frmMain.lblCode.Text & "'")
                Else
                    a = getData("select max(ID)ID from BK_Customer where CM_BrId='" & frmMain.lblCode.Text & "'")
                End If

                If a = "" Then
                    .Add("@d8", SqlDbType.Int).Value = 1
                Else
                    .Add("@d8", SqlDbType.Int).Value = Val(a) + 1
                End If
                .Add("@d9", SqlDbType.NVarChar).Value = "Active"
                .Add("@d10", SqlDbType.DateTime).Value = DateTime.MaxValue.Date
            End With
            If FrmCustomer.Text = "CustomerOther" Then
                sql = "insert into BK_CustomerOther(CM_ID,CM_KhName,LO_ID,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,LD_Cycle,ID,Status,Date_Change) values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,0,@d8,@d9,@d10)"
            Else
                sql = "insert into BK_Customer(CM_ID,CM_KhName,LO_ID,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,LD_Cycle,ID,Status,Date_Change) values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,0,@d8,@d9,@d10)"
            End If
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Function getLastCM_ID()
        Dim CM_ID As String = ""
        If FrmCustomer.Text = "CustomerOther" Then
            CM_ID = getData("select MAX(cast(isnull( CM_ID,0) as int)) from BK_CustomerOther where CM_BrId='" & frmMain.lblCode.Text & "'")
            If CM_ID = "" Then
                CM_ID = "0"
            End If
        Else
            CM_ID = getData("select MAX(cast(isnull( CM_ID,0) as int)) from BK_Customer where CM_BrId='" & frmMain.lblCode.Text & "'")
            If CM_ID = "" Then
                CM_ID = "0"
            End If
        End If
        Return CM_ID
    End Function
    Public Function getLastLO_ID()
        Dim LO_ID As String = ""
        If frmLocation.Text = "LocationOther" Or FrmCustomer.Text = "CustomerOther" Then
            LO_ID = getData("select MAX(cast( isnull(LO_ID,0) as int)) from BK_LocationOther where LO_BrID='" & frmMain.lblCode.Text & "'")
        Else
            LO_ID = getData("select MAX(cast( isnull(LO_ID,0) as int)) from BK_Location where LO_BrID='" & frmMain.lblCode.Text & "'")
        End If
        If LO_ID = "" Then
            LO_ID = "0"
        End If
        Return LO_ID
    End Function
    Public Sub addLocation(ByVal LO_ID As Integer, ByVal VL_ID As String, ByVal CN_ID As String, ByVal DT_ID As String, ByVal PV_ID As String, ByVal LO_BrID As String, ByVal LO_Rec_Status As Integer, ByVal LO_User_Create As String, ByVal LO_Date_Create As DateTime)
        Dim iRow = frmLocation.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = LO_ID
                .Add("@d1", SqlDbType.NVarChar).Value = VL_ID
                .Add("@d2", SqlDbType.NVarChar).Value = CN_ID
                .Add("@d3", SqlDbType.NVarChar).Value = DT_ID
                .Add("@d4", SqlDbType.NVarChar).Value = PV_ID
                .Add("@d5", SqlDbType.NVarChar).Value = LO_BrID
                .Add("@d6", SqlDbType.Int).Value = LO_Rec_Status
                .Add("@d7", SqlDbType.NVarChar).Value = LO_User_Create
                .Add("@d8", SqlDbType.DateTime).Value = LO_Date_Create
            End With
            If frmLocation.Text = "LocationOther" Then
                sql = "insert BK_LocationOther(LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create)  values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)"
            Else
                sql = "insert BK_Location(LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create)  values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8)"
            End If
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub addTrace_Location(ByVal DateAction As DateTime, ByVal RecordAction As String, ByVal LO_ID As Integer, _
                                 ByVal VL_ID As String, ByVal CN_ID As String, ByVal DT_ID As String, ByVal PV_ID As String, ByVal LO_BrID As String, _
                                 ByVal LO_Rec_Status As Integer, ByVal LO_User_Create As String, ByVal LO_Date_Create As DateTime, ByVal LO_User_Modify As String, _
                                 ByVal LO_Date_Modify As DateTime)
        Dim iRow = frmLocation.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            ''Dim con As New SqlClient.SqlConnection
            ''con.ConnectionString = connectionString1
            ''con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.DateTime).Value = DateAction
                .Add("@d1", SqlDbType.NVarChar).Value = RecordAction
                .Add("@d2", SqlDbType.Int).Value = LO_ID
                .Add("@d3", SqlDbType.NVarChar).Value = VL_ID
                .Add("@d4", SqlDbType.NVarChar).Value = CN_ID
                .Add("@d5", SqlDbType.NVarChar).Value = DT_ID
                .Add("@d6", SqlDbType.NVarChar).Value = PV_ID
                .Add("@d7", SqlDbType.NVarChar).Value = LO_BrID
                .Add("@d8", SqlDbType.Int).Value = LO_Rec_Status
                .Add("@d9", SqlDbType.NVarChar).Value = LO_User_Create
                .Add("@d10", SqlDbType.DateTime).Value = LO_Date_Create
                .Add("@d11", SqlDbType.NVarChar).Value = LO_User_Modify
                .Add("@d12", SqlDbType.DateTime).Value = LO_Date_Modify
                'If LO_User_Modify = "" Then
                '    LO_User_Modify = frmMain.users
                '    LO_Date_Modify = DateTime.Now
                'End If
                sql = "insert TRACE_Location (DateAction,RecordAction,LO_ID,VL_ID,CN_ID,DT_ID,PV_ID,LO_BrID,LO_Rec_Status,LO_User_Create,LO_Date_Create,LO_User_Modify,LO_Date_Modify)  values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)"
                com.CommandText = sql
                com.ExecuteNonQuery()
                com.Parameters.Clear()
                com.Dispose()
                'con.Close()
                'con.Dispose()
            End With
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub UpdateLocation(ByVal VL_ID As String, ByVal CN_ID As String, ByVal DT_ID As String, ByVal PV_ID As String)
        Dim iRow = frmLocation.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = VL_ID
                .Add("@d1", SqlDbType.NVarChar).Value = CN_ID
                .Add("@d2", SqlDbType.NVarChar).Value = DT_ID
                .Add("@d3", SqlDbType.NVarChar).Value = PV_ID
            End With
            If frmLocation.Text = "LocationOther" Then
                sql = "update BK_LocationOther set VL_ID=@d0,CN_ID=@d1,DT_ID=@d2,PV_ID=@d3,LO_User_Modify='" & frmMain.users & "',LO_Date_Modify='" & DateTime.Now() & "' where LO_ID='" & frmLocation.DataGridView1.Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'"
            Else
                sql = "update BK_Location set VL_ID=@d0,CN_ID=@d1,DT_ID=@d2,PV_ID=@d3,LO_User_Modify='" & frmMain.users & "',LO_Date_Modify='" & DateTime.Now() & "' where LO_ID='" & frmLocation.DataGridView1.Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'"
            End If
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As SystemException
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub showLocation()
        Dim iRow = frmLocation.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = ""
        If frmLocation.Text = "LocationOther" Then
            Str = "select 'Saved',LO_ID,VL_ID,CN_ID,DT_ID,PV_ID from BK_LocationOther where LO_ID='" & frmLocation.DataGridView1.Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'"
        Else
            Str = "select 'Saved',LO_ID,VL_ID,CN_ID,DT_ID,PV_ID from BK_Location where LO_ID='" & frmLocation.DataGridView1.Rows(iRow).Cells(1).Value & "' and LO_BrID='" & frmMain.lblCode.Text & "'"
        End If
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        frmLocation.DataGridView1.Rows(iRow).Cells(0).Value = oDt.Rows(0).Item(0).ToString
        frmLocation.DataGridView1.Rows(iRow).Cells(1).Value = oDt.Rows(0).Item(1).ToString
        frmLocation.DataGridView1.Rows(iRow).Cells(2).Value = oDt.Rows(0).Item(2).ToString
        frmLocation.DataGridView1.Rows(iRow).Cells(3).Value = oDt.Rows(0).Item(3).ToString
        frmLocation.DataGridView1.Rows(iRow).Cells(4).Value = oDt.Rows(0).Item(4).ToString
        frmLocation.DataGridView1.Rows(iRow).Cells(5).Value = oDt.Rows(0).Item(5).ToString
        'Ctrl.DataSource = oDt
        frmLocation.DataGridView1.Rows(iRow).Cells(1).Style.BackColor = Color.Yellow
        frmLocation.DataGridView1.Rows(iRow).Cells(1).ReadOnly = True
        oDa.Dispose()
        oDt.Dispose()
    End Sub
    Public Sub ShowDataGrid(ByVal Ctrl As DataGridView, ByVal str As String)
        'Ctrl.Rows.Clear()
        Dim oDt As New System.Data.DataTable
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(str, g_cnn)
        oDa.Fill(oDt)
        Ctrl.DataSource = oDt
        oDa.Dispose()
        oDt.Dispose()
    End Sub
    Public Sub SetFontDatagrid1(ByVal dg As DataGridView)
        With dg.ColumnHeadersDefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .BackColor = Color.DarkRed
            .ForeColor = Color.Gold
            .Font = New System.Drawing.Font("Khmer OS", 10, FontStyle.Regular, GraphicsUnit.Point)
        End With
        '---------------------------------- Set font datagridview
        Dim cs As New DataGridViewCellStyle
        cs.ForeColor = Color.Black
        cs.Font = New System.Drawing.Font("Khmer OS", 10, FontStyle.Regular)
        dg.RowsDefaultCellStyle = cs
        '---------------------------------------------------------
    End Sub
    Public Sub SetFontDatagrid(ByVal dg As DataGridView)
        With dg.ColumnHeadersDefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .BackColor = Color.DarkRed
            .ForeColor = Color.Gold
            .Font = New System.Drawing.Font("Khmer OS", 10, FontStyle.Regular, GraphicsUnit.Point)
        End With
        '---------------------------------- Set font datagridview
        Dim cs As New DataGridViewCellStyle
        cs.ForeColor = Color.Black
        cs.Font = New System.Drawing.Font("Khmer OS", 10, FontStyle.Regular)
        dg.RowsDefaultCellStyle = cs
        '---------------------------------------------------------
    End Sub
    Public Sub SetFontDatagrid3(ByVal dg As DataGridView)
        With dg.ColumnHeadersDefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .BackColor = Color.DarkRed
            .ForeColor = Color.Gold
            .Font = New System.Drawing.Font("Khmer OS", 8, FontStyle.Regular, GraphicsUnit.Point)
        End With
        '---------------------------------- Set font datagridview
        Dim cs As New DataGridViewCellStyle
        cs.ForeColor = Color.Black
        cs.Font = New System.Drawing.Font("Khmer OS", 8, FontStyle.Regular)
        dg.RowsDefaultCellStyle = cs
        '---------------------------------------------------------
    End Sub
    Public Sub showLoan()
        Dim iRow = frmDisburshment.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select 'Saved',a.LD_ID,a.EM_ID,d.EM_Name,a.CM_ID,b.CM_KhName,b.CM_Phone,c.VL_ID+','+c.CN_ID+','+c.DT_ID+','+c.PV_ID [Address],a.LD_Dis_Amt,Case When a.CU_ID=1 then N'រៀល' else N'ដុល្លារ' end Currency,a.LD_Unit,a.LD_Term,a.LD_IntRate,a.LD_Type,a.LD_ChargeRate,a.LD_ChargeAmt,convert( varchar(12),a.LD_Dis_Date,101) LD_Dis_Date,convert( varchar(12),a.LD_First_Date,101) LD_First_Date,convert( varchar(12),a.LD_Mat_Date,101) LD_Mat_Date,case when LD_Service=0 then N'មិនមាន' else N'មាន' end LD_Service,isnull(LD_InRate,0) LD_Insurance,isnull(LD_InAmt,0)LD_InsuranceTotal,PayOff,Ref,a.PID,g.Kh_Name from BK_Loan a inner join BK_Customer b on a.CM_ID1=b.ID and a.CM_ID=b.CM_ID and a.LD_BrId=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrID inner join BK_Employee d on a.EM_ID=d.EM_ID and a.LD_BrId=d.EM_BrID left join BK_Product g on a.PID=g.PID where LD_ID=" & frmDisburshment.DataGridView1.Rows(iRow).Cells("coLD_ID").Value & " and LD_BrId=" & frmMain.lblCode.Text
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        frmDisburshment.DataGridView1.Rows(iRow).Cells(0).Value = oDt.Rows(0).Item(0).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coLD_ID").Value = oDt.Rows(0).Item(1).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coEM_ID").Value = oDt.Rows(0).Item(2).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coEM_Name").Value = oDt.Rows(0).Item(3).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coCM_ID").Value = oDt.Rows(0).Item(4).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coCM_Name").Value = oDt.Rows(0).Item(5).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coCM_Phone").Value = oDt.Rows(0).Item(6).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coAddress").Value = oDt.Rows(0).Item(7).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value = oDt.Rows(0).Item(8).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coCurrency").Value = oDt.Rows(0).Item(9).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coUnit").Value = oDt.Rows(0).Item(10).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coTerm").Value = oDt.Rows(0).Item(11).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coIntRate").Value = oDt.Rows(0).Item(12).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coType").Value = oDt.Rows(0).Item(13).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coCharge_Rate").Value = oDt.Rows(0).Item(14).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value = oDt.Rows(0).Item(15).ToString
        'If frmDisburshment.DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value = "" Then
        '    frmDisburshment.DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value = "Hi"
        'Else
        '    frmDisburshment.DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value = "No"
        'End If
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coDisDate").Value = oDt.Rows(0).Item(16).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coDisDatePay").Value = oDt.Rows(0).Item(17).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coDisDateEnd").Value = oDt.Rows(0).Item(18).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coLD_Service").Value = oDt.Rows(0).Item(19).ToString
        Dim as1 As Double = frmDisburshment.DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value
        If as1 = 0 Then
        Else
            frmDisburshment.DataGridView1.Rows(iRow).Cells("coLD_DisAmt").Value = Format(as1, "###,###.##")
        End If

        Dim as11 As Double = frmDisburshment.DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value
        If as11 = 0 Then
        Else
            frmDisburshment.DataGridView1.Rows(iRow).Cells("coCharge_Amt").Value = Format(as11, "###,###.##")
        End If
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coInsurance").Value = oDt.Rows(0).Item(20).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coInsuranceTotal").Value = oDt.Rows(0).Item(21).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coPayOff").Value = oDt.Rows(0).Item(22).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("coRef").Value = oDt.Rows(0).Item(23).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("PID").Value = oDt.Rows(0).Item(24).ToString
        frmDisburshment.DataGridView1.Rows(iRow).Cells("PName").Value = oDt.Rows(0).Item(25).ToString
        oDa.Dispose()
        oDt.Dispose()
        Dim iRow1 As Integer = frmDisburshment.DataGridView1.Rows.Count - 1
        With frmDisburshment.DataGridView1.Rows(iRow)
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
            .Cells("coChage_Amt").Style.BackColor = Color.Yellow
            .Cells("coChage_Amt").ReadOnly = True
            .Cells("coDisDate").Style.BackColor = Color.Yellow
            .Cells("coDisDate").ReadOnly = True
            .Cells("coDisDatePay").Style.BackColor = Color.Yellow
            .Cells("coDisDatePay").ReadOnly =
            .Cells("coDisDateEnd").Style.BackColor = Color.Yellow
            .Cells("coDisDateEnd").ReadOnly = True
            .Cells("coLD_Service").Style.BackColor = Color.Yellow
            .Cells("coLD_Service").ReadOnly = True
            frmDisburshment.DataGridView1.CurrentCell = frmDisburshment.DataGridView1(1, iRow1)
        End With
    End Sub

    Public Sub DgToExcel(ByVal DataGridView1 As DataGridView)
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

        'Try
        Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
        fileTemp.Close()
        'Catch ex As Exception
        '    blnFileOpen = False
        '    Exit Sub
        'End Try

        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If

        wBook.SaveAs(strFileName)
        excel.Workbooks.Open(strFileName)
        excel.Visible = True
        Exit Sub
errorhandler:
        MsgBox(Err.Description)
    End Sub
    Public Function MakeCheckColumn(pstrName As String, pstrHeader As String, pintWidth As Integer, pVisible As Boolean) As DataGridViewCheckBoxColumn
        Dim col = New DataGridViewCheckBoxColumn()
        col.DataPropertyName = pstrName
        col.Name = pstrName
        col.HeaderText = pstrHeader
        If (pintWidth > 0) Then col.Width = pintWidth
        col.Visible = pVisible
        Return col
    End Function
    Public Function ExecuteDatatable(str As String, sqlCon As SqlConnection) As DataTable
        Dim oDt As New DataTable
        Dim oDa As SqlDataAdapter

        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(str, sqlCon)
        oDa.Fill(oDt)
        Return oDt
    End Function
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
            'Dim conn As New SqlClient.SqlConnection(connectionString1)
            'conn.Open()
            Dim cmd1 As New SqlClient.SqlCommand(SQL, g_cnn)
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
            'conn.Close()
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
            e.Handled = True
        End If
    End Sub
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
            'oCnn = New SqlConnection(connectionString1)
            'oCnn.Open()
            oCmd = New SqlCommand(str, g_cnn)
            dr = oCmd.ExecuteReader
            If dr.Read Then
                id = dr(0).ToString
            End If
            dr.Close()
            'oCnn.Close()
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
        'Dim conn As New SqlConnection
        Dim dr As SqlDataReader
        GLID = ""
        Try
            'conn.ConnectionString = connectionString1
            'conn.Open()
            Q = "select  top 1 " & field & "  from  " & table & " order by  " & field & " Desc"
            Cmd = New SqlCommand(Q, g_cnn)
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
        'Dim con As New SqlClient.SqlConnection
        Dim com As New SqlClient.SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Dim i As Integer
        Dim j As Integer
        Try
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
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
            'con.Close()
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
            'Dim cnn As New SqlClient.SqlConnection(connectionString1)
            'cnn.Open()
            Dim dr As SqlClient.SqlDataReader
            Q = "select top 1 EmployeeID from tbluser order by EmployeeID Desc"
            Cmd = New SqlClient.SqlCommand(Q, g_cnn)
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
            'Dim cnn As New SqlClient.SqlConnection(connectionString1)
            'cnn.Open()
            Dim dr As SqlClient.SqlDataReader
            Q = "select top 1 EmpID from tblEmployees order by EmpID Desc"
            Cmd = New SqlClient.SqlCommand(Q, g_cnn)
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
            'Dim con As SqlClient.SqlConnection
            Dim com As SqlClient.SqlDataAdapter
            Dim DtSet As System.Data.DataSet
            'con = New SqlClient.SqlConnection(connectionString1)
            com = New SqlClient.SqlDataAdapter(sql, g_cnn)
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
            'con.Close()
            com.SelectCommand.Parameters.Clear()
            'frmMain.mystatus.Text = dgv.RowCount & " of " & QueryStr("Select count(*) from " & tbl & "") & " row(s)"
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
            'Dim con As New SqlConnection(connectionString1)
            Dim com As New SqlCommand(SQL, g_cnn)
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
        'Dim con As New SqlClient.SqlConnection
        Dim com As New SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Try
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
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
            'con.Close()
            com.Parameters.Clear()
        Catch ex As System.Exception
            MsgBox(ex.Message)
        End Try
        Return value
    End Function
    Public Function QueryVal(ByVal sql As String, ByVal ParamArray ctrl() As Object) As Double
        Dim value As Double = 0
        'Dim con As New SqlClient.SqlConnection
        Dim com As New SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Try
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
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
            'con.Close()
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
    Public Sub excelSchedule(ByVal DataGrView1 As DataGridView, ByVal SampleLocation As String, ByVal startRow As Integer)
        'MessageBox.Show(SampleLocation)
        Dim rowsTotal, colsTotal As Short
        Dim I, j As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        ''-----------------------------------------------------------------------------
        Try
            '-------------------------------- "D:\LoanSystem\Program\Report\DepositSchedule.xls"
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Open(SampleLocation, False, True)
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets("Sheet1"), Excel.Worksheet)
            xlApp.Visible = True
            'rowsTotal = DataGridView1.RowCount - 1
            'colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                '.Cells.Select()
                '.Cells.Delete()
                'For iC = 0 To colsTotal
                '    .Cells(5, iC + 1).Value = DataGridView1.Columns(iC).HeaderText
                'Nexta

                .Cells(3, 1).value = frmMain.lblName.Text

                .Cells(4, 1).value = "កាលបរិច្ឆេទចាប់ពី "

                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        '.Cells(I + startRow, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                    Next (j)
                Next I
                '.Rows("1:1").Font.FontStyle = "Bold"
                '.Rows("1:1").Font.Size = 12
                ''.Rows("1:1").font.size =
                '.Cells.Columns.AutoFit()
                '.Cells.Select()
                '-----------------------------------------
                'Dim selection As Excel.Range
                'selection = excelWorksheet.Range("A" & startRow - 1 & ":" & endcolumnBorder & I + startRow)
                'selection.Borders.Weight = Excel.XlBorderWeight.xlThin
                '------------------------------------------
                'excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).MergeCells = True
                'excelWorksheet.Range(StartMergeCell & I + startRow & ":" & EndMergeCell & I + startRow).Value = "សរុប"
                'excelWorksheet.Range(startColumnSum & I + startRow & ":" & "I" & I + startRow).Value = "=Sum(" & startColumnSum & startRow & ":" & startColumnSum & I + startRow - 1 & ")"
                .Cells(1, 1).Select()
            End With
        Catch ex As System.Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub

End Module
