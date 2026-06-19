Imports System.IO

Public Class frmsignin
    Private Sub btnlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlogin.Click
        Try
            Dim a(4) As String
            If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & ComboBox1.Text & ".txt") Then
                Dim FS As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\" & ComboBox1.Text & ".txt", FileMode.Open)
                Dim SR As New StreamReader(FS)
                Dim s As String = SR.ReadLine
                If Not s Is Nothing Then
                    a = Split(s, "-")
                End If
                SR.Close()
                FS.Close()
            End If
            '----------------------------------------------
            If ChkCnn(a(0), a(1), a(2), a(3)) = True Then
                Me.Show()
            Else
                Dim stat As Integer
                stat = MsgBox("Connection failure, You want to continue?", MsgBoxStyle.YesNo)
                If stat = vbYes Then
                    Frm_Connection.Show()
                    Return
                Else
                    End
                End If
            End If
            Me.btnlogin.Focus()
            Me.BackColor = Color.FromArgb(233, 51, 12)
            '---------------------------------------------------------------------------------------------
            If IsExisted("select * from sys_User where PassWords='" & Me.txtpass.Text & "'and User_Name='" & Me.txtstaff.Text & "'") = True Then
                uid = Me.txtstaff.Text
                Me.Hide()
                frmMain.WindowState = FormWindowState.Maximized
                frmMain.Show()
                'Security()
            Else
                resultError = frmMessageError.ShowBoxError("អ្នកប្រើប្រាស់ និង លេខសម្ងាត់មិនត្រឹមត្រូវទេ។", "ការបញ្ចូលខុស")
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
        Frm_Connection.Close()
        Application.Exit()
    End Sub

    Private Sub frmsignin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        reader()
        'Dim a(4) As String
        'If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\System.txt") Then
        '    Dim FS As New FileStream(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\System.txt", FileMode.Open)
        '    Dim SR As New StreamReader(FS)
        '    Dim s As String = SR.ReadLine
        '    If Not s Is Nothing Then
        '        a = Split(s, "-")
        '    End If
        '    SR.Close()
        '    FS.Close()
        'End If

        'If ChkCnn(a(0), a(1), a(2), a(3)) = True Then
        '    Me.Show()
        'Else
        '    Dim stat As Integer
        '    stat = MsgBox("Connection failure, You want to continue?", MsgBoxStyle.YesNo)
        '    If stat = vbYes Then
        '        Frm_Connection.Show()
        '    Else
        '        End
        '    End If
        'End If

        'Me.btnlogin.Focus()
        'Me.BackColor = Color.FromArgb(233, 51, 12)
    End Sub
    Public Sub reader()
        ComboBox1.Items.Clear()
        Dim sf As String
        For Each sf In Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory & "\Connections\")
            Dim s As String
            s = IO.Path.GetFileNameWithoutExtension(sf.Substring(sf.LastIndexOf("\") + 1, sf.Length - sf.LastIndexOf("\") - 1))
            ComboBox1.Items.Add(s)
            ComboBox1.SelectedIndex = 0
        Next
    End Sub
    Private Sub txtpass_Enter(sender As Object, e As EventArgs) Handles txtpass.Enter
        If txtpass.Text = "PASSWORD" Then
            txtpass.Text = ""
        End If
    End Sub

    Private Sub txtpass_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpass.KeyPress
        If e.KeyChar = Chr(13) Then
            btnlogin_Click(sender, e)
        End If
    End Sub

    Private Sub txtstaff_Enter(sender As Object, e As EventArgs) Handles txtstaff.Enter
        If txtstaff.Text = "USER NAME" Then
            txtstaff.Text = ""
        End If
    End Sub
    Private Sub txtstaff_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtstaff.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtstaff.Text = "" Then
                Return
            Else
                txtpass.Focus()
            End If
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub txtpass_Leave(sender As Object, e As EventArgs) Handles txtpass.Leave
        If Me.txtpass.Text = "" Then
            txtpass.Text = "PASSWORD"
        End If
    End Sub
    Private Sub txtpass_TextChanged(sender As Object, e As EventArgs) Handles txtpass.TextChanged
        If Me.txtpass.Text = "PASSWORD" Then
            txtpass.UseSystemPasswordChar = False
        Else
            txtpass.UseSystemPasswordChar = True
        End If
    End Sub
    Private Sub txtstaff_Leave(sender As Object, e As EventArgs) Handles txtstaff.Leave
        If txtstaff.Text = "" Then
            txtstaff.Text = "USER NAME"
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Application.Exit()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Frm_Connection.Show()
    End Sub
End Class