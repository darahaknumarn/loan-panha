Imports System.IO
Imports System.Text
Public Class Frm_Connection
    Dim PM As New Cls_qbPayroll
    Private Sub btnsave_Click(ByVal sender As System.Object,
    ByVal e As System.EventArgs) Handles btnsave.Click
        If Me.txtName.Text = "" Or txtdatabase.Text = "" Or txtpassword.Text = "" Or txtserver.Text = "" Or txtuserid.Text = "" Then
            MessageBox.Show("Not enough information, can't save this record!", "Can't save!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Try
            If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt") Then
                If MessageBox.Show("This name is already, do you want to replace?", "Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                    'File.Delete(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt")
                    Dim FS1 As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt", FileMode.Truncate)
                    Dim SW As New StreamWriter(FS1)
                    If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt") Then
                        SW.Write(txtserver.Text & "-" & txtdatabase.Text & "-" & txtuserid.Text & "-" & txtpassword.Text)
                        SW.Close()
                        MessageBox.Show("Record has been saved!")
                    End If
                    Return
                End If
                'Return
            Else
                'File.Create(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt")
                'FileClose(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt")
                Dim fs2 As IO.FileStream = IO.File.Create(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt")
                'Use fs to read or write the file
                fs2.Close()
                Dim FS1 As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt", FileMode.Truncate)
                Dim SW As New StreamWriter(FS1)
                If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt") Then
                    SW.Write(txtserver.Text & "-" & txtdatabase.Text & "-" & txtuserid.Text & "-" & txtpassword.Text)
                    MessageBox.Show("Record has been saved!")
                    SW.Close()
                    FS1.Close()
                End If
                'MessageBox.Show("No Exist")
                'Return
            End If
            'Dim FS As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & Me.txtName.Text & ".txt", FileMode.Truncate)
            'Dim SW As New StreamWriter(FS)
            'If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\System.txt") Then
            '    SW.Write(txtserver.Text & "-" & txtdatabase.Text & "-" & txtuserid.Text & "-" & txtpassword.Text)
            '    SW.Close()
            '    FS.Close()
            'End If
            'Dim a As Integer
            'a = MsgBox("System were reset, do you want to restart?", MsgBoxStyle.YesNo)
            'If a = vbYes Then
            '    'IsLogin = False
            '    Application.Restart()
            'Else
            '    Application.Exit()
            'End If
            reader()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    Private Function ValidateEntry() As Integer
        Dim blnResult As Boolean = True
        If txtserver.Text Is String.Empty Then
            blnResult = False
            Exit Function
        End If
        If txtdatabase.Text Is String.Empty Then
            blnResult = False
            Exit Function
        End If
        If txtuserid.Text Is String.Empty Then
            blnResult = False
            Exit Function
        End If
        If txtpassword.Text Is String.Empty Then
            blnResult = False
            Exit Function
        End If
        Return blnResult
    End Function

    Private Sub Frm_Connection_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        frmsignin.reader()
    End Sub
    Private Sub Frm_Connection_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        reader()
        'If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\System.txt") Then
        '    Dim FS As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\System.txt", FileMode.Open)
        '    Dim SR As New StreamReader(FS)
        '    Dim a(4) As String
        '    Dim s As String = SR.ReadLine
        '    If Not s Is Nothing Then
        '        a = Split(s, "-")
        '        txtserver.Text = a(0)
        '        txtdatabase.Text = a(1)
        '        txtuserid.Text = a(2)
        '        txtpassword.Text = a(3)
        '    End If
        '    SR.Close()
        '    FS.Close()
        'Else
        '    Dim FS As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\System.txt", FileMode.Create)
        '    FS.Close()
        'End If
    End Sub
    Private Sub reader()
        'Using objReader As New StreamReader(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\System.txt")
        '    Do While objReader.Peek() <> -1
        '        Dim line As String = objReader.ReadLine()
        '        Dim splitLine() As String = line.Split("|")
        '        Dim teamName As String = splitLine(0)
        '        Dim gamesWon As Integer = CInt(splitLine(1))
        '        Dim gamesLost As Integer = CInt(splitLine(2))
        '        Dim percentOfGamesWon As Double = gamesWon / (gamesWon + gamesLost) * 100
        '        Me.ComboBox1.Items.Add(teamName)
        '    Loop
        'End Using
        'For i As Integer = 0 To lstDir.Items.Count - 1
        '    For Each File As String In Directory.GetFiles(lstDir.Items(i))
        '        Fname = File.ToString.Substring(File.LastIndexOf("\") + 1)
        '        i = +1

        '        If File.Contains(".zip") Then
        '            res = ""
        '            For Each Str As Char In Fname
        '                If IsNumeric(Str) Then
        '                    res = res & Str
        '                End If
        '            Next

        '            For x As Integer = 0 To lstDir.Items.Count - 1
        '                For Each newFile As String In Directory.GetFiles(lstDir.Items(x))
        '                    If newFile.Contains(res) Then
        '                        dgContents.Rows.Add(Fname)
        '                    End If
        '                Next
        '            Next

        '        End If

        '    Next
        'Next
        Me.DataGridView1.Rows.Clear()
        Dim sf As String
        For Each sf In Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\")
            Dim s As String
            s = IO.Path.GetFileNameWithoutExtension(sf.Substring(sf.LastIndexOf("\") + 1, sf.Length - sf.LastIndexOf("\") - 1))
            DataGridView1.Rows.Add(s)
            'ComboBox1.SelectedIndex = 0
        Next

    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Try
            Dim cm As String = Me.DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(0).Value.ToString
            If cm = "" Then
                Return
            Else
                If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & cm & ".txt") Then
                    Dim FS As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & cm & ".txt", FileMode.Open)
                    Dim SR As New StreamReader(FS)
                    Dim a(4) As String
                    Dim s As String = SR.ReadLine
                    txtName.Text = Me.DataGridView1.Rows(DataGridView1.CurrentRow.Index).Cells(0).Value.ToString
                    If Not s Is Nothing Then
                        a = Split(s, "-")
                        txtserver.Text = a(0)
                        txtdatabase.Text = a(1)
                        txtuserid.Text = a(2)
                        txtpassword.Text = a(3)
                    Else
                        txtserver.Text = ""
                        txtdatabase.Text = ""
                        txtuserid.Text = ""
                        txtpassword.Text = ""
                    End If
                    SR.Close()
                    FS.Close()
                Else
                    Dim FS As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & cm & ".txt", FileMode.Create)
                    FS.Close()
                End If
                End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.txtName.Text = "" Or Me.txtserver.Text = "" Or Me.txtdatabase.Text = "" Or Me.txtuserid.Text = "" Or Me.txtpassword.Text = "" Then
            MessageBox.Show("Please select record before delete!")
            Return
        Else
            If MessageBox.Show("Are you sure want to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                File.Delete(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & txtName.Text & ".txt")
                MessageBox.Show("Record has been deleted!")
                txtName.Text = ""
                txtserver.Text = ""
                txtdatabase.Text = ""
                txtuserid.Text = ""
                txtpassword.Text = ""
                reader()
            End If
        End If
    End Sub
End Class
