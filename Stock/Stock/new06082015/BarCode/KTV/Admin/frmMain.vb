Public Class frmMain
    Public strPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
    Public users As String = getData("Select employeeid from tbluser where employeeid='" & frmsignin.txtstaff.Text & "' and password='" & frmsignin.txtpass.Text & "'")

    Private Sub lblLogoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Application.Exit()
        frmCustomerReport.Hide()
    End Sub
    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Application.Exit()
    End Sub
    Private Sub S1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        For Each Form In Me.MdiChildren
            Form.Close()
        Next

        frmChangePassword.MdiParent = Me
        frmChangePassword.Show()
    End Sub
    Private Sub S2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'For Each Form In Me.MdiChildren
        '    Form.Close()
        'Next
        'frmConfiguration.MdiParent = Me
        'frmConfiguration.Show()
        'Me.lblshow.Text = Me.S2.Text
    End Sub
    Private Sub frmMain_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmsignin.Close()
        Me.Close()
        Application.Exit()
    End Sub
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtCompany.Multiline = False
        'Label2.Left = Me.Width
        Timer1.Start()
        'MessageBox.Show(uid)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        Me.BackgroundImage = toImage(getImage("Select CompanyLogo from tblcompany"))
        Me.BackgroundImageLayout = ImageLayout.Zoom
        Dim ID As String = getData("Select top 1 ID from tblCompany1 ")
        Dim Name As String = getData("Select Name from tblCompany1 where ID='" & ID & "'")
        '-------------------------
        lblCode.Text = ID.ToString
        lblName.Text = Name.ToString
        frmsignin.Hide()
    End Sub
    Private Sub mnuexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuexit.Click
        Me.Close()
    End Sub
    Private Sub mnulogoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnulogoff.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        Me.Hide()
        frmsignin.Show()

    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmChangePassword.MdiParent = Me
        frmChangePassword.WindowState = FormWindowState.Maximized
        frmChangePassword.Show()
    End Sub
    Private Sub mnuReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.MdiParent = Me
        frmReport.WindowState = FormWindowState.Maximized
        frmReport.Show()

    End Sub
    'Private Sub mnuadmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuadmin.Click
    '    'For Each Form In Me.MdiChildren
    '    '    Form.Close()
    '    'Next
    '    'frmMaintenance.MdiParent = Me

    '    'frmMaintenance.Show()
    '    'frmMaintenance.WindowState = FormWindowState.Maximized
    'End Sub
    Private Sub mnubackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnubackup.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frm_Backup.MdiParent = Me
        frm_Backup.WindowState = FormWindowState.Maximized
        frm_Backup.Show()
    End Sub

    Private Sub mnurestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnurestore.Click
        'For Each Form In Me.MdiChildren
        '    Form.Close()
        'Next
        'frm_Restore.MdiParent = Me
        'frm_Restore.WindowState = FormWindowState.Maximized
        'frm_Restore.Show()
    End Sub
    Private Sub mnulistemployees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next

    End Sub

    Private Sub mnurecordAttendant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        Dashboard.MdiParent = Me
        Dashboard.WindowState = FormWindowState.Maximized
        Dashboard.Show()
    End Sub

    Private Sub mnumeetingreg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmMeeting.MdiParent = Me
        frmMeeting.WindowState = FormWindowState.Maximized
        frmMeeting.Show()
    End Sub


    Private Sub mnuattendant2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmDashboard2.MdiParent = Me
        'frmDashboard2.WindowState = FormWindowState.Maximized
        'frmDashboard2.Show()
    End Sub
    Private Sub LogoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoToolStripMenuItem.Click
        If Me.users = "sh" Or Me.users = "e01" Or Me.users = "acc" Then
            resultError = frmMessageError.ShowBoxError("អ្នកគ្មានសិទ្ធកែប្រែ​ Logo នោះទេ!!!​សូមអរគុណ។")
            Return
        ElseIf Me.users = "admin" Then
            frmSetup.Show()
        End If
    End Sub
    Private Sub ResourceManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResourceManagementToolStripMenuItem.Click
        Timer1.Stop()
        If Me.users = "acc" Then

            resultError = frmMessageError.ShowBoxError("អ្នកគ្មានសិទ្ធត្រួតពិនិត្យប្រព័ន្ធនេះទេ​!!!​សូមអរគុណ។")
            Return
        ElseIf Me.users = "E01" Then

            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmResourceManagement.MdiParent = Me
            frmResourceManagement.WindowState = FormWindowState.Maximized
            frmResourceManagement.Show()
        ElseIf Me.users = "sh" Then

            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmResourceManagement.MdiParent = Me
            frmResourceManagement.WindowState = FormWindowState.Maximized
            frmResourceManagement.Show()
        ElseIf Me.users = "admin" Then
            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmResourceManagement.MdiParent = Me
            frmResourceManagement.WindowState = FormWindowState.Maximized
            frmResourceManagement.Show()
        End If
    End Sub

    Private Sub FixAssetToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
    End Sub

    Private Sub បរពនធគរបគរងសងToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles បរពនធគរបគរងសងToolStripMenuItem.Click
        Timer1.Stop()
        If Me.users = "acc" Then

            resultError = frmMessageError.ShowBoxError("អ្នកគ្មានសិទ្ធត្រួតពិនិត្យប្រព័ន្ធនេះទេ​!!!​សូមអរគុណ។")
            Return
        ElseIf Me.users = "E01" Then

            'Me.Label2.Enabled = False

            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmStock.MdiParent = Me
            frmStock.WindowState = FormWindowState.Maximized
            frmStock.Show()
        ElseIf Me.users = "sh" Then

            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmStock.MdiParent = Me
            frmStock.WindowState = FormWindowState.Maximized
            frmStock.Show()
        ElseIf Me.users = "admin" Then
            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmStock.MdiParent = Me
            frmStock.WindowState = FormWindowState.Maximized
            frmStock.Show()
        End If
    End Sub
    Private Sub បរពនធគរបគរងសងToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles បរពនធគរបគរងសងToolStripMenuItem1.Click
        Timer1.Stop()
        If Me.users = "E01" Then
            resultError = frmMessageError.ShowBoxError("អ្នកគ្មានសិទ្ធត្រួតពិនិត្យប្រព័ន្ធនេះទេ​!!!​សូមអរគុណ")
            Return
        ElseIf Me.users = "acc" Then
            'Me.Label2.Enabled = False

            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmSang.MdiParent = Me
            frmSang.WindowState = FormWindowState.Maximized
            frmSang.Show()
        ElseIf Me.users = "sh" Then
            'Me.Label2.Enabled = False

            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmSang.MdiParent = Me
            frmSang.WindowState = FormWindowState.Maximized
            frmSang.Show()
        ElseIf Me.users = "admin" Then
            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmSang.MdiParent = Me
            frmSang.WindowState = FormWindowState.Maximized
            frmSang.Show()
        End If
    End Sub

    Private Sub txtCompany_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCompany.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCompany.Text = "" Then
                Return
            Else
                Dim ID As String = Me.txtCompany.Text
                Dim Name As String = getData("Select Name from tblCompany1 where ID='" & ID & "'")
                '-------------------------
                lblCode.Text = ID.ToString
                lblName.Text = Name.ToString
            End If
        End If
    End Sub

    Private Sub txtCompany_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCompany.TextChanged

    End Sub

    Private Sub ForITSolutionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ForITSolutionToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next

        frmIT.MdiParent = Me
        frmIT.WindowState = FormWindowState.Maximized
        frmIT.Show()

    End Sub

    Private Sub FasdToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next

        rptSang.MdiParent = Me
        rptSang.WindowState = FormWindowState.Maximized
        rptSang.Show()
    End Sub

    Private Sub របយករណរមToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Me.Label2.Enabled = False
        Timer1.Stop()
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frm1.MdiParent = Me
        frm1.WindowState = FormWindowState.Maximized
        frm1.Show()
    End Sub

    Private Sub អពបរពនធToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles អពបរពនធToolStripMenuItem.Click
        AboutmyPOS.StartPosition = FormStartPosition.CenterScreen
        AboutmyPOS.Show()
    End Sub
    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    If Label2.Location.X + Label2.Width < 0 Then
    '        Label2.Location = New Point(Me.Width, Label2.Location.Y)
    '    Else
    '        Label2.Location = New Point(Label2.Location.X - 3, Label2.Location.Y)
    '    End If
    'End Sub
End Class