Imports System.Data.SqlClient
Imports Microsoft.Office
Imports System.IO
''''''''''''''''''1. If control is datetimepicker, there two condition that has been input in control tag
''''''''''''''''''Example: Start Date,1/0)
''''''''''''''''''  - Start Date: is is the text that will alert as the message box
''''''''''''''''''  - 1/0: If 1 it will be format datetimepicker as 'dd/MMM/yyy' else if 0 'hh:ss'
''''''''''''''''''2. If control is combobox or Multicombobox, there three condition that has been input in control tag
''''''''''''''''''Example: Course,1,1/0
''''''''''''''''''  - Course: is the 'Data Mandatory Type Name' that is used to select ID by this name if the number at the last in the tag is 1
''''''''''''''''''  - 1:If it is 1 mean that this control must input value else if 0 this control can be blank
''''''''''''''''''  - 1/0: If it is 1 mean that this control must check ID else if 0 this control don't need to check ID
''''''''''''''''''3. If control is textbox, there two condition that has been input in control tag
''''''''''''''''''Example: First Name,1/0)
''''''''''''''''''  - Fist Name: is is the text that will alert as the message box
''''''''''''''''''  - 1/0: If 1 it will be format datetimepicker as 'dd/MMM/yyy' else if 0 'hh:ss'

Public Class Cls_qbPayroll
    Public Function Execution(ByVal sql As String, ByVal ParamArray Ctrl() As Object) As Boolean
        '    Execution = False
        '    For i = 0 To Ctrl.Length - 1
        '        If TypeOf Ctrl(i) Is Control Then
        '            If Ctrl(i).Tag <> "" Then
        '                If TypeOf Ctrl(i) Is DateTimePicker Then
        '                    Dim str() As String
        '                    str = Split(Ctrl(i).tag, ",")
        '                    If Ctrl(i).CustomFormat <> "dd/MMM/yyyy" And Ctrl(i).CustomFormat <> "hh:mm:ss tt" And Ctrl(i).CustomFormat <> "MMM/yyyy" Then
        '                        MsgBox(str(0) & " is mandatory!", MsgBoxStyle.Information)
        '                        Ctrl(i).Focus()
        '                        Execution = True
        '                        Exit Function
        '                    End If
        '                Else
        '                    If Ctrl(i).text = "" Then
        '                        Dim str() As String
        '                        str = Split(Ctrl(i).tag, ",")
        '                        If str(0) <> "" And str(1) = "1" Then
        '                            MsgBox(str(0) & " is mandatory!", MsgBoxStyle.Information)
        '                            Ctrl(i).Focus()
        '                            Execution = True
        '                            Exit Function
        '                        End If
        '                    End If
        '                End If
        '            End If
        '        End If
        '        If TypeOf Ctrl(i) Is ComboBox Or TypeOf Ctrl(i) Is MultipleCombobox.mcb Then
        '            If Ctrl(i).tag <> "" Then
        '                Dim str() As String
        '                str = Split(Ctrl(i).Tag, ",")
        '                If str(0) <> "" And str(1) = "1" And str(2) = "1" Then
        '                    If QueryStr("Select ID from [Data Mandatory] where [MyHR].[dbo].[GUnicode](Name)=[MyHR].[dbo].[GUnicode](@A1) and [DMT ID]=(select [DMT ID] from [Data Mandatory Type] where [MyHR].[dbo].[GUnicode](Name)=[MyHR].[dbo].[GUnicode](@A2))", Ctrl(i).Text, str(0)) = "" Then
        '                        Ctrl(i).Focus()
        '                        MsgBox(str(0) & " is invalid input!", MsgBoxStyle.Information)
        '                        Execution = True
        '                        Exit Function
        '                    End If
        '                End If
        '            End If
        '        End If
        '    Next

        '    If Execution = False Then
        '        Dim con As New SqlClient.SqlConnection
        '        Dim com As New SqlClient.SqlCommand
        '        Try
        '            con.ConnectionString = ConnectionString
        '            con.Open()
        '            com.Connection = con
        '            com.CommandType = CommandType.Text
        '            com.CommandText = sql
        '            com.CommandTimeout = 30000
        '            For i = 0 To Ctrl.Length - 1
        '                If TypeOf Ctrl(i) Is CheckBox Or TypeOf Ctrl(i) Is RadioButton Or TypeOf Ctrl(i) Is TreeNode Then
        '                    com.Parameters.AddWithValue("@A" & (i + 1), Ctrl(i).Checked)
        '                ElseIf TypeOf Ctrl(i) Is PictureBox Then
        '                    Dim ms As New IO.MemoryStream
        '                    Ctrl(i).Image.Save(ms, Imaging.ImageFormat.Jpeg)
        '                    Dim bytes() As Byte = ms.GetBuffer()
        '                    If bytes.Length <> 1742 Then
        '                        com.Parameters.AddWithValue("@A" & (i + 1), DirectCast(bytes, Object))
        '                    Else
        '                        com.Parameters.Add("@A" & (i + 1), SqlDbType.VarBinary).Value = DBNull.Value
        '                    End If
        '                ElseIf TypeOf Ctrl(i) Is DateTimePicker Then
        '                    If (Ctrl(i).Format = DateTimePickerFormat.Custom And Ctrl(i).CustomFormat = "dd/MMM/yyyy") Or (Ctrl(i).Format = DateTimePickerFormat.Custom And Ctrl(i).CustomFormat = "MMM/yyyy") Then
        '                        com.Parameters.AddWithValue("@A" & (i + 1), Ctrl(i).Value.Date)
        '                    ElseIf Ctrl(i).Format = DateTimePickerFormat.Custom And Ctrl(i).CustomFormat = "hh:mm:ss tt" Then
        '                        com.Parameters.AddWithValue("@A" & (i + 1), Ctrl(i).Value)
        '                    Else
        '                        com.Parameters.AddWithValue("@A" & (i + 1), DBNull.Value)
        '                    End If
        '                ElseIf TypeOf Ctrl(i) Is MultipleCombobox.mcb Or TypeOf Ctrl(i) Is ComboBox Then
        '                    If Ctrl(i).tag <> "" Then
        '                        Dim str() As String
        '                        str = Split(Ctrl(i).tag, ",")
        '                        If str(0) <> "" And str(2) = "1" Then
        '                            com.Parameters.AddWithValue("@A" & (i + 1), IIf(Ctrl(i).Text <> "", IDFromObj(Ctrl(i)), ""))
        '                        ElseIf str(0) <> "" And str(2) = "0" Then
        '                            com.Parameters.AddWithValue("@A" & (i + 1), IIf(Ctrl(i).Text <> "", Ctrl(i).text, ""))
        '                        End If
        '                    Else
        '                        com.Parameters.AddWithValue("@A" & (i + 1), IIf(Ctrl(i).Text <> "", Ctrl(i).text, ""))
        '                    End If
        '                ElseIf TypeOf Ctrl(i) Is TextBox Then
        '                    com.Parameters.AddWithValue("@A" & (i + 1), IIf(Ctrl(i).Text <> "", Ctrl(i).Text, ""))
        '                ElseIf TypeOf Ctrl(i) Is String Or TypeOf Ctrl(i) Is Decimal Or TypeOf Ctrl(i) Is Double Or TypeOf Ctrl(i) Is Integer Or TypeOf Ctrl(i) Is Short Then
        '                    com.Parameters.AddWithValue("@A" & (i + 1), IIf(CStr(Ctrl(i)) <> "", Ctrl(i), ""))
        '                End If
        '            Next
        '            com.ExecuteNonQuery()
        '            com.Parameters.Clear()
        '            con.Close()
        '            path = ""
        '        Catch ex As Exception
        '            com.Parameters.Clear()
        '            con.Close()
        '            MessageBox.Show(ex.Message, "Error ExecuteNonQuery", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        End Try
        '    End If
    End Function

    Public Sub DBToControl(ByVal sql As String, ByVal cond As Object, ByVal ParamArray Ctrl() As Object)
        Try
            'Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            ''con.ConnectionString = connectionString1
            ''con.Open()
            com.Connection = g_cnn
            com.CommandType = CommandType.Text
            com.CommandText = sql
            If TypeOf cond Is String Or TypeOf cond Is Decimal Or TypeOf cond Is Double Or TypeOf cond Is Integer Or TypeOf cond Is Short Then
                com.Parameters.AddWithValue("@A1", DirectCast(cond, Object))
            ElseIf TypeOf cond Is Control Then
                com.Parameters.AddWithValue("@A1", DirectCast(cond.Text, Object))
            End If
            com.CommandTimeout = 30000
            dr = com.ExecuteReader
            If dr.Read = True Then
                For i = 0 To Ctrl.Length - 1
                    If TypeOf Ctrl(i) Is CheckBox Or TypeOf Ctrl(i) Is RadioButton Or TypeOf Ctrl(i) Is TreeNode Then
                        If IsDBNull(dr(i)) = False Then
                            Ctrl(i).Checked = dr(i)
                        Else
                            Ctrl(i).Checked = False
                        End If
                    ElseIf TypeOf Ctrl(i) Is PictureBox Then
                        If IsDBNull(dr(i)) = False Then
                            Dim Photo() As Byte
                            Photo = DirectCast(dr(i), Byte())
                            Dim st As Stream
                            st = New MemoryStream(Photo, 0, UBound(Photo))
                            Ctrl(i).Image = Bitmap.FromStream(st)
                        Else
                            Ctrl(i).Image = My.Resources.alarmclock
                        End If
                    ElseIf TypeOf Ctrl(i) Is DateTimePicker Then
                        If IsDBNull(dr(i)) = False Then
                            If Ctrl(i).Tag <> "" Then
                                Dim str() As String
                                str = Split(Ctrl(i).tag, ",")
                                If str(1) = "0" Then
                                    Ctrl(i).CustomFormat = "hh:mm:ss tt"
                                Else
                                    Ctrl(i).CustomFormat = "dd/MMM/yyyy"
                                End If
                            Else
                                Ctrl(i).CustomFormat = "dd/MMM/yyyy"
                            End If
                            Ctrl(i).Text = dr(i)
                        Else
                            Ctrl(i).Format = DateTimePickerFormat.Custom
                            Ctrl(i).CustomFormat = " "
                        End If
                    Else
                        If IsDBNull(dr(i)) = False Then
                            Ctrl(i).Text = dr(i)
                        Else
                            Ctrl(i).Text = ""
                        End If
                    End If
                Next
            End If
            dr.Close()
            'con.Close()
            com.Parameters.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error ExecuteReader", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Public Function SearchCondition(ByVal FLD As String, ByVal ctrl As Object, Optional ByVal IsDate As Boolean = False) As String
        Dim cond As String = ""
        Try
            Dim SubCond As String = ""
            If ctrl.Text.Contains(" - ") Then
                Dim IDSplitArray() As String
                IDSplitArray = Split(ctrl.Text, " - ")
                For i = 0 To UBound(IDSplitArray)
                    SubCond = " and " & FLD & " between "
                    For j = 0 To UBound(IDSplitArray)
                        If IsDate = False Then
                            SubCond &= "'" & IDSplitArray(j) & "' and "
                        Else
                            SubCond &= "'" & CDate(IDSplitArray(j)) & "' and "
                        End If
                    Next
                    SubCond = Mid(SubCond, 1, SubCond.Length - 4)
                Next
                cond &= SubCond
            ElseIf ctrl.Text.Contains(",") Then
                Dim IDSplitArray() As String
                IDSplitArray = Split(ctrl.Text, ",")
                For i = 0 To UBound(IDSplitArray)
                    SubCond = " and " & FLD & " in("
                    For j = 0 To UBound(IDSplitArray)
                        If IsDate = False Then
                            SubCond &= "'" & IDSplitArray(j) & "',"
                        Else
                            SubCond &= "'" & CDate(IDSplitArray(j)) & "',"
                        End If
                    Next
                    SubCond = Mid(SubCond, 1, SubCond.Length - 1) & ")"
                Next
                cond &= SubCond
            Else
                If IsDate = False Then
                    cond &= " and " & FLD & " like '" & checkstring(ctrl.Text) & "'"
                Else
                    cond &= " and " & FLD & "= '" & CDate(ctrl.Text) & "'"
                End If
            End If
        Catch ex As Exception

        End Try
        Return cond
    End Function

    Public Function checkstring(ByVal str As String) As String
        Dim s As String = ""
        Try
            For i = 0 To str.Length - 1
                If Asc(Mid(str, i + 1, 1)) = 39 Then
                    s = s & "'+char(39)+'"
                ElseIf Asc(Mid(str, i + 1, 1)) = 37 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 42 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 35 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 38 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 63 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 64 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 36 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 60 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 62 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 123 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 125 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 91 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 93 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 40 Then
                    s = s & "%"
                ElseIf Asc(Mid(str, i + 1, 1)) = 41 Then
                    s = s & "%"
                Else
                    s = s & Mid(str, i + 1, 1)
                End If
            Next
        Catch ex As Exception
        End Try
        Return s
    End Function

    Public Function QueryStr(ByVal sql As String, ByVal ParamArray ctrl() As Object) As String
        Dim value As String = ""
        'Dim con As New SqlClient.SqlConnection
        Dim com As New SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Try
            ' ''con.ConnectionString = connectionString1
            ' ''con.Opn(e)
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
        Catch ex As Exception
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return value
    End Function

    Public Function GetExcelColumnName(ByVal columnNumber As Integer) As String
        Dim columnName As String = String.Empty
        Try
            Dim dividend As Integer = columnNumber
            Dim modulo As Integer
            While dividend > 0
                modulo = (dividend - 1) Mod 26
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName
                dividend = CInt((dividend - modulo) / 26)
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return columnName
    End Function

    Public Function ReadFilePath(ByVal Path As String) As Byte()
        Dim data As Byte() = Nothing
        Try
            Dim fInfo As New FileInfo(Path)
            Dim numBytes As Long = fInfo.Length
            Dim fStream As New FileStream(Path, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fStream)
            data = br.ReadBytes(CInt(numBytes))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return data
    End Function

    Public Function MarkGridView(ByVal DGV As DataGridView) As String
        Dim PatientIDLocal As String = ""
        Try
            Dim LocalArrayList As New ArrayList
            If DGV.RowCount > 0 Then
                For i As Integer = 0 To DGV.RowCount - 1
                    Dim b As Boolean
                    If LocalArrayList.Count > 0 Then
                        For j As Integer = 0 To LocalArrayList.Count - 1
                            If DGV.Item(0, i).Value = LocalArrayList(j).ToString.Replace("'", "") Then
                                b = True
                            End If
                        Next
                        If b = False Then
                            LocalArrayList.Add("'" & DGV.Item(0, i).Value & "'")
                        End If
                    Else
                        LocalArrayList.Add("'" & DGV.Item(0, i).Value & "'")
                    End If
                Next i
            End If
            If DGV.RowCount > 0 Then For Each ID As String In LocalArrayList : PatientIDLocal &= ID & "," : Next : PatientIDLocal = Mid(PatientIDLocal, 1, PatientIDLocal.Length - 1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return PatientIDLocal
    End Function

    Public Function MarkListView(ByVal Li As ListView) As String
        Dim PatientIDLocal As String = ""
        Try
            Dim LocalArrayList As New ArrayList
            If Li.Items.Count > 0 Then
                For i As Integer = 0 To Li.Items.Count - 1
                    Dim b As Boolean
                    If LocalArrayList.Count > 0 Then
                        For j As Integer = 0 To LocalArrayList.Count - 1
                            If Li.Items(0).Text = LocalArrayList(j).ToString.Replace("'", "") Then
                                b = True
                            End If
                        Next
                        If b = False Then
                            LocalArrayList.Add("'" & Li.Items(0).Text & "'")
                        End If
                    Else
                        LocalArrayList.Add("'" & Li.Items(0).Text & "'")
                    End If
                Next i
            End If
            If Li.Items.Count > 0 Then For Each ID As String In LocalArrayList : PatientIDLocal &= ID & "," : Next : PatientIDLocal = Mid(PatientIDLocal, 1, PatientIDLocal.Length - 1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return PatientIDLocal
    End Function

    Public Function MustExist(ByVal ctrl As Object, ByVal msg As String) As Boolean
        Dim b As Boolean = False
        If ctrl.Enabled = True Then
            If TypeOf ctrl Is DateTimePicker Then
                If ctrl.Format = DateTimePickerFormat.Custom And ctrl.CustomFormat = " " Then
                    MsgBox(msg, MsgBoxStyle.Information)
                    ctrl.Focus()
                    b = True
                End If
            Else
                If ctrl.Text = "" Then
                    MsgBox(msg, MsgBoxStyle.Information)
                    ctrl.Focus()
                    b = True
                End If
            End If
        End If
        Return b
    End Function

    Public Sub ClearAndEnableControl(ByVal ParamArray CtrlArray() As Object)
        Try
            'For i = 0 To CtrlArray.Length - 1
            '    If TypeOf CtrlArray(i) Is CheckBox Or TypeOf CtrlArray(i) Is RadioButton Then CtrlArray(i).checked = False : CtrlArray(i).Enabled = True
            '    If TypeOf CtrlArray(i) Is PictureBox Then CtrlArray(i).image = My.Resources.Picture_background : CtrlArray(i).Enabled = True
            '    If TypeOf CtrlArray(i) Is DateTimePicker Then CtrlArray(i).Format = DateTimePickerFormat.Custom : CtrlArray(i).CustomFormat = " " : CtrlArray(i).Enabled = True
            '    If TypeOf CtrlArray(i) Is ComboBox Or TypeOf CtrlArray(i) Is TextBox Then CtrlArray(i).Text = "" : CtrlArray(i).Enabled = True
            '    If TypeOf CtrlArray(i) Is MaskedTextBox Then CtrlArray(i).Mask = "00/LLL/0000" : CtrlArray(i).Enabled = True : CtrlArray(i).Text = ""
            '    If TypeOf CtrlArray(i) Is MultipleCombobox.mcb Then CtrlArray(i).Text = "" : CtrlArray(i).Enabled = True
            'Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ClearAndDisableControl(ByVal ParamArray CtrlArray() As Object)
        Try
            'For i = 0 To CtrlArray.Length - 1
            '    If TypeOf CtrlArray(i) Is CheckBox Or TypeOf CtrlArray(i) Is RadioButton Then CtrlArray(i).checked = False : CtrlArray(i).Enabled = False
            '    If TypeOf CtrlArray(i) Is PictureBox Then CtrlArray(i).image = My.Resources.Picture_background : CtrlArray(i).Enabled = False
            '    If TypeOf CtrlArray(i) Is DateTimePicker Then CtrlArray(i).Format = DateTimePickerFormat.Custom : CtrlArray(i).CustomFormat = " " : CtrlArray(i).Enabled = False
            '    If TypeOf CtrlArray(i) Is TextBox Or TypeOf CtrlArray(i) Is ComboBox Then CtrlArray(i).Text = "" : CtrlArray(i).Enabled = False
            '    If TypeOf CtrlArray(i) Is MaskedTextBox Then CtrlArray(i).Mask = "00/LLL/0000" : CtrlArray(i).Enabled = False : CtrlArray(i).Text = ""
            '    If TypeOf CtrlArray(i) Is MultipleCombobox.mcb Then CtrlArray(i).Text = "" : CtrlArray(i).Enabled = False
            'Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub EnableControl(ByVal ParamArray CtrlArray() As Object)
        Try
            For i = 0 To CtrlArray.Length - 1
                CtrlArray(i).Enabled = True
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub DisableControl(ByVal ParamArray CtrlArray() As Object)
        Try
            For i = 0 To CtrlArray.Length - 1
                CtrlArray(i).Enabled = False
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FillCombo(ByVal sql As String, ByVal cb As Object)
        ''Dim con As New SqlClient.SqlConnection
        Dim com As New SqlCommand
        Dim dr As SqlClient.SqlDataReader
        Try
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            com.CommandText = sql
            com.CommandTimeout = 30000
            dr = com.ExecuteReader
            cb.Items.Clear()
            Do While dr.Read = True
                cb.Items.Add(dr(0))
            Loop
            dr.Close()
            ''con.Close()
        Catch ex As Exception
            'con.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub OpenFormImgList_Index(ByVal frm As Form, ByVal indx As Integer, ByVal BannerText As String)
        Try
            'Frm_Main.btnBanner.ImageIndex = indx
            'Frm_Main.btnBanner.Text = BannerText
            'frm.MdiParent = Frm_Main
            'frm.Dock = DockStyle.Fill
            'frm.FormBorderStyle = FormBorderStyle.None
            'frm.BringToFront()
            'frm.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub OpenForm(ByVal frm As Form, ByVal tv As TreeView)
        Try
            'Frm_Main.btnBanner.ImageIndex = tv.SelectedNode.SelectedImageIndex
            'Frm_Main.btnBanner.Text = tv.SelectedNode.Text
            'frm.MdiParent = Frm_Main
            'frm.BringToFront()
            'frm.Show()
        Catch ex As Exception
        End Try
    End Sub

    Public Function IDFromObj(ByVal Obj As Object) As String
        Dim str() As String
        str = Split(Obj.Tag, ",")
        Return QueryStr("Select ID from [Data Mandatory] where [MyHR].[dbo].[GUnicode](Name)=[MyHR].[dbo].[GUnicode](@A1) and [DMT ID]=(select [DMT ID] from [Data Mandatory Type] where [MyHR].[dbo].[GUnicode](Name)=[MyHR].[dbo].[GUnicode](@A2))", Obj.Text, str(0))
    End Function

    Public Function IDFromStr(ByVal obj As Object, ByVal tag As String) As String
        Return QueryStr("Select ID from [Data Mandatory] where [MyHR].[dbo].[GUnicode](Name)=[MyHR].[dbo].[GUnicode](@A1) and [DMT ID]=(select [DMT ID] from [Data Mandatory Type] where [MyHR].[dbo].[GUnicode](Name)=[MyHR].[dbo].[GUnicode](@A2))", obj, tag)
    End Function

End Class
