Public Class frmMain
    Public strPath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
    Public users As String = getData("select User_Name from sys_User where PassWords='" & frmsignin.txtpass.Text & "'and User_Name='" & frmsignin.txtstaff.Text & "'")

    Private Sub lblLogoff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Application.Exit()
        'frmCustomerReport.Hide()
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
        'Me.lblshow.Text = Me.S1.Text
    End Sub
    Private Sub frmMain_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frmsignin.Close()
        Me.Close()
        Application.Exit()
    End Sub
    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.FromArgb(233, 51, 12)
        txtCompany.Multiline = False
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        Dim ID As String = getData("Select top 1 CompanyID from BK_Company ")
        Dim Name As String = getData("Select CompanyKhmerName from BK_Company where CompanyID='" & ID & "'")
        '-------------------------
        lblCode.Text = ID.ToString
        lblName.Text = Name.ToString
        lblserver.Text = "Server: " & g_cnn.DataSource.ToString & " , Database: " & g_cnn.Database.ToString
        frmsignin.Hide()
        'Get Privileges view for loged in user
        Dim DT As DataTable = ExecuteDatatable("SELECT MenuID FROM sys_UserPrivilege WHERE PrivilegeID = 1 AND User_Name = '" & users & "'", g_cnn)
        LoadMenuPrvileges(MenuStrip1, DT)
    End Sub

    Private Sub LoadMenuPrvileges(Menu As MenuStrip, DTPrivileges As DataTable)
        For Each child As ToolStripItem In Menu.Items
            For Each row As DataRow In DTPrivileges.Rows
                If row("MenuID") = child.Tag Then child.Enabled = True
            Next row
            IterateThrouthMenusItems(child, DTPrivileges)
        Next
    End Sub
    Private Sub IterateThrouthMenusItems(TSMI As ToolStripMenuItem, DTPrivileges As DataTable)
        For Each childs As ToolStripMenuItem In TSMI.DropDownItems
            For Each row As DataRow In DTPrivileges.Rows
                If row("MenuID") = childs.Tag Then childs.Enabled = True
            Next row
            If childs.HasDropDownItems Then
                IterateThrouthMenusItems(childs, DTPrivileges)
            End If
        Next
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

    Private Sub mnuadmin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frm_Restore.MdiParent = Me

        frm_Restore.Show()
        frm_Restore.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub mnubackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnubackup.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frm_Backup.MdiParent = Me
        frm_Backup.WindowState = FormWindowState.Maximized
        frm_Backup.Show()
    End Sub

    Private Sub mnurestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnurestore.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frm_Restore.MdiParent = Me
        frm_Restore.WindowState = FormWindowState.Maximized
        frm_Restore.Show()
    End Sub
    Private Sub mnulistemployees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next

    End Sub

    Private Sub mnuattendant2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmDashboard2.MdiParent = Me
        'frmDashboard2.WindowState = FormWindowState.Maximized
        'frmDashboard2.Show()
    End Sub
    Private Sub LogoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If Me.users = "sh" Or Me.users = "e01" Or Me.users = "acc" Then
            resultError = frmMessageError.ShowBoxError("អ្នកគ្មានសិទ្ធកែប្រែ​ Logo នោះទេ!!!​សូមអរគុណ។")
            Return
        ElseIf Me.users = "admin" Then
            frmSetup.Show()
        End If
        'For Each Form In Me.MdiChildren
        '    Form.Close()
        'Next
        'frmSetup.MdiParent = Me
        'frmSetup.WindowState = FormWindowState.Maximized

    End Sub
    Private Sub ResourceManagementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResourceManagementToolStripMenuItem.Click
        'Timer1.Stop()
        frmOwner.Text = "Deposit"
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmOwner.MdiParent = Me
        frmOwner.WindowState = FormWindowState.Maximized
        frmOwner.Text = "Deposit"
        frmOwner.Show()


    End Sub

    Private Sub FixAssetToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmFixAsset.MdiParent = Me
        'frmFixAsset.WindowState = FormWindowState.Maximized
        'frmFixAsset.Show()
    End Sub
    Private Sub បរពនធគរបគរងសងToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles បរពនធគរបគរងសងToolStripMenuItem.Click
        'Timer1.Stop()
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmOwner.MdiParent = Me
        frmOwner.WindowState = FormWindowState.Maximized
        frmOwner.Show()
        frmOwner.Text = "Withdrawal"
    End Sub
    Private Sub បរពនធគរបគរងសងToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles បរពនធគរបគរងសងToolStripMenuItem1.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmBank.MdiParent = Me
        frmBank.WindowState = FormWindowState.Maximized
        frmBank.Show()
    End Sub

    Private Sub txtCompany_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCompany.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCompany.Text = "" Then
                Return
            Else
                Dim ID As String = Me.txtCompany.Text
                Dim Name As String = getData("Select CompanyKhmerName from BK_Company where CompanyID='" & ID & "'")
                If Name = "" Then
                    Return
                Else
                    lblCode.Text = ID.ToString
                    lblName.Text = Name.ToString
                End If
                '-------------------------

            End If
        End If
    End Sub
    Private Sub ForITSolutionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmIT.MdiParent = Me
        frmIT.WindowState = FormWindowState.Maximized
        frmIT.Show()
    End Sub
    Private Sub អពបរពនធToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles អពបរពនធToolStripMenuItem.Click
        AboutmyPOS.StartPosition = FormStartPosition.CenterScreen
        AboutmyPOS.Show()
    End Sub
    Private Sub បញចញឥណទនToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles បញចញឥណទនToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmDisburshment.MdiParent = Me
        frmDisburshment.WindowState = FormWindowState.Maximized
        frmDisburshment.Show()
    End Sub
    Private Sub យកបរកនងបងផតចToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles យកបរកនងបងផតចToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmRepayment.MdiParent = Me
        frmRepayment.WindowState = FormWindowState.Maximized
        frmRepayment.Show()
    End Sub
    Private Sub អតថជនToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles អតថជនToolStripMenuItem.Click
        FrmCustomer.MdiParent = Me
        FrmCustomer.WindowState = FormWindowState.Maximized
        FrmCustomer.Show()
    End Sub

    Private Sub ទកនលងToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ទកនលងToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmLocation.MdiParent = Me
        frmLocation.WindowState = FormWindowState.Maximized
        frmLocation.Show()
    End Sub

    Private Sub ចណយToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ចណយToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmExpense.MdiParent = Me
        frmExpense.WindowState = FormWindowState.Maximized
        frmExpense.Show()
    End Sub

    Private Sub កដចណយToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles កដចណយToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmAsset.MdiParent = Me
        frmAsset.WindowState = FormWindowState.Maximized
        frmAsset.Show()
    End Sub

    Private Sub ថងឈបសរកToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ថងឈបសរកToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmHoliday.MdiParent = Me
        frmHoliday.WindowState = FormWindowState.Maximized
        frmHoliday.Show()
    End Sub

    Private Sub ដកបរករលទញដលលរToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ដកបរករលទញដលលរToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmExchange.Text = "KHRTOUSD"
        frmExchange.MdiParent = Me
        frmExchange.WindowState = FormWindowState.Maximized
        frmExchange.Show()
    End Sub

    Private Sub ដកបរកដលលរទញរលToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ដកបរកដលលរទញរលToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmExchange.Text = "USDTOKHR"
        frmExchange.MdiParent = Me
        frmExchange.WindowState = FormWindowState.Maximized
        frmExchange.Show()
    End Sub
    Private Sub យកលខអតថជនចសមកបរToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles យកលខអតថជនចសមកបរToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmChangeCustomer.MdiParent = Me
        frmChangeCustomer.WindowState = FormWindowState.Maximized
        frmChangeCustomer.Show()
    End Sub

    Private Sub បគគលកToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles បគគលកToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmEmployee.MdiParent = Me
        frmEmployee.WindowState = FormWindowState.Maximized
        frmEmployee.Show()
    End Sub

    Private Sub អនកបរបរសToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles អនកបរបរសToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmUsers.MdiParent = Me
        frmUsers.WindowState = FormWindowState.Maximized
        frmUsers.Show()
    End Sub
    Private Sub ករដកនងដកទនToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ករដកនងដកទនToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "ការដាក់ និងដកទុន"
        frmReport.MdiParent = Me
        'frmReport.WindowState = FormWindowState.Maximized
        frmReport.Show()
    End Sub

    Private Sub សតពបរកសងខបToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles សតពបរកសងខបToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "ស្តីពីប្រាក់សង្ខេប"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub
    Private Sub បញចញឥណទនToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles បញចញឥណទនToolStripMenuItem2.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "បញ្ចេញឥណទាន"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub ProfitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ProfitToolStripMenuItem1.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Profit"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub ឥណទនយតToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ឥណទនយតToolStripMenuItem1.Click

    End Sub

    Private Sub ចណយសរបToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ចណយសរបToolStripMenuItem1.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "ចំណាយសរុប"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub ចណយគតរលសToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ចណយគតរលសToolStripMenuItem1.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "ចំណាយគិតរំលស់"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub
    Private Sub សងខបមនតរឥណទនToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Loan Summary By CO"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub
    Private Sub បនយកបរកពអតថជនToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles បនយកបរកពអតថជនToolStripMenuItem1.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "បានយកប្រាក់ពីអតិថិជន"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub តរវយកបរកពអតថជនToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles តរវយកបរកពអតថជនToolStripMenuItem1.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "ត្រូវយកប្រាក់ពីអតិថិជន"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub ចណយរលសបរចខToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ចណយរលសបរចខToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "ចំណាយរំលស់ប្រចាំខែ"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub សងខបចណយមនគតរលសToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles សងខបចណយមនគតរលសToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "សង្ខេបចំណាយមិនគិតរំលស់"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub សមតលយនទរពយToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles សមតលយនទរពយToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "សមតុល្យនៃទ្រព្យ"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub អតថជនអសកមមកនងគរToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles អតថជនអសកមមកនងគរToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "អតិថិជនអសកម្មក្នុងគ្រា"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub ទទលបញញរToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ទទលបញញរToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmReport.Text = "អតិថិជនអសកម្មក្នុងគ្រា"
        frmOtherDeposit.MdiParent = Me
        frmOtherDeposit.WindowState = FormWindowState.Maximized
        frmOtherDeposit.Show()
    End Sub

    Private Sub StationaryToolStripMenuItem1_Click(sender As Object, e As EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmReport.Text = "អតិថិជនអសកម្មក្នុងគ្រា"
        frmStationary.MdiParent = Me
        frmStationary.WindowState = FormWindowState.Maximized
        frmStationary.Show()
    End Sub

    Private Sub StationaryTypeToolStripMenuItem_Click(sender As Object, e As EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmReport.Text = "អតិថិជនអសកម្មក្នុងគ្រា"
        frmSType.MdiParent = Me
        frmSType.WindowState = FormWindowState.Maximized
        frmSType.Show()
    End Sub

    Private Sub AddStationaryToolStripMenuItem_Click(sender As Object, e As EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmReport.Text = "អតិថិជនអសកម្មក្នុងគ្រា"
        frmStationaryIn.MdiParent = Me
        frmStationaryIn.WindowState = FormWindowState.Maximized
        frmStationaryIn.Show()
    End Sub

    Private Sub StationaryOutToolStripMenuItem_Click(sender As Object, e As EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmReport.Text = "អតិថិជនអសកម្មក្នុងគ្រា"
        frmStationaryOut.MdiParent = Me
        frmStationaryOut.WindowState = FormWindowState.Maximized
        frmStationaryOut.Show()
    End Sub

    Private Sub UserPrivilegesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UserPrivilegesToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmReport.Text = "អតិថិជនអសកម្មក្នុងគ្រា"
        frmPrivileges.MdiParent = Me
        frmPrivileges.WindowState = FormWindowState.Maximized
        frmPrivileges.Show()
    End Sub

    Private Sub ករដកនងដកលយពកងToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ករដកនងដកលយពកងToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Bank Transaction"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub
    Private Sub កតតបនToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles កតតបនToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmReport.Text = "Bank Transaction"
        frmChangeLocation.MdiParent = Me
        frmChangeLocation.WindowState = FormWindowState.Maximized
        frmChangeLocation.Show()
    End Sub

    Private Sub InterestDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InterestDetailToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Interest Detail"
        frmReport.MdiParent = Me
        'frmReport.WindowState = FormWindowState.Maximized
        frmReport.Show()
    End Sub

    Private Sub InterestSuToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InterestSuToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Interest Summary"
        frmReport.MdiParent = Me
        'frmReport.WindowState = FormWindowState.Maximized
        frmReport.Show()
    End Sub

    Private Sub CustomerHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CustomerHistoryToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmCustomerHistory.Text = "Interest Summary"
        frmCustomerHistory.MdiParent = Me
        frmCustomerHistory.WindowState = FormWindowState.Maximized
        frmCustomerHistory.Show()
    End Sub

    Private Sub បងបរកបញញរToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles បងបរកបញញរToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmCustomerHistory.Text = "Interest Summary"
        frmOtherDepositPayOff.MdiParent = Me
        frmOtherDepositPayOff.WindowState = FormWindowState.Maximized
        frmOtherDepositPayOff.Show()
    End Sub

    Private Sub CompanyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CompanyToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        'frmCustomerHistory.Text = "Interest Summary"
        frmCompany.MdiParent = Me
        frmCompany.WindowState = FormWindowState.Maximized
        frmCompany.Show()
    End Sub
    Private Sub LoanArrearsSummaryByCOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanArrearsSummaryByCOToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Loan Arrears Summary By CO"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub LoanArrearsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanArrearsToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Loan Arrears Detail"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub LoanOutstandingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanOutstandingToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Loan Outstanding Detail"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub LoanOutstandingByCOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanOutstandingByCOToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Loan Outstanding Summary by CO"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub LoanSummaryByCOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanSummaryByCOToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Loan Summary By CO"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub InactiveLoanIncomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InactiveLoanIncomeToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Inactive Loan Income"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub SummaryLoanToPayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryLoanToPayToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Summary loan to pay"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub SummaryLoanPaidToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryLoanPaidToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Summary loan paid"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub IncorrectRepayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncorrectRepayToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "Incorrect Repay"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmExport.Text = "Export"
        frmExport.MdiParent = Me
        frmExport.Show()
    End Sub

    Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmImport.Text = "Import"
        frmImport.MdiParent = Me
        frmImport.Show()
    End Sub

    Private Sub OtherIncomeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OtherIncomeToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmOtherIncome.Text = "Other Income"
        frmOtherIncome.MdiParent = Me
        frmOtherIncome.WindowState = FormWindowState.Maximized
        frmOtherIncome.Show()

    End Sub

    Private Sub LoanToRepayByBranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanToRepayByBranchToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "LoanToRepayByBranch"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub SummaryLoanPaidByBranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryLoanPaidByBranchToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "SummaryLoanPaidByBranch"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub LoanSummaryByBranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanSummaryByBranchToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "LoanSummaryByBranch"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub LoanOutstandingSummaryByBrandToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanOutstandingSummaryByBrandToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "LoanOutstandingSummaryByBrand"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub LoanArrearsSummaryByBranchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoanArrearsSummaryByBranchToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "LoanArrearsSummaryByBranch"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub ProfitByBrandToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProfitByBrandToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "ProfitByBrand"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub SpecialRepayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpecialRepayToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmReport.Text = "SpecialRepay"
        frmReport.MdiParent = Me
        frmReport.Show()
    End Sub

    Private Sub ProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmProduct.MdiParent = Me
        frmProduct.WindowState = FormWindowState.Maximized
        frmProduct.Show()
    End Sub

    Private Sub WriteoffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WriteoffToolStripMenuItem.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmWiteOff.Text = "Write Off"
        frmWiteOff.MdiParent = Me
        frmWiteOff.WindowState = FormWindowState.Maximized
        frmWiteOff.Show()
    End Sub
End Class