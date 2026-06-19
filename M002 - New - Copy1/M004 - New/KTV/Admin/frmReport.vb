Public Class frmReport
    Private Sub frmReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.F5 Then
            MessageBox.Show("Press F5")
        End If
    End Sub
    Private Sub frmReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ProgressBar1.Visible = False
        Label3.Visible = False
        AddCombo(cboEmployee, "select EM_ID from BK_Employee where EM_BrID='" & frmMain.lblCode.Text & "'")
        Dim com As Integer = Val(getData("select count(CompanyID) from BK_Company"))
        If com = 1 Then
            AddCombo(cboBranch, "select CompanyID from BK_Company")
            cboBranch.SelectedIndex = 0
        Else
            Me.cboBranch.Text = "All"
            AddCombo(cboBranch, "select CompanyID from BK_Company")
        End If
        lblEmployee.Text = "All"
        Me.cboEmployee.Text = "All"
        Me.cboWF.SelectedIndex = 0
        cboCurrency.Text = "All"
        Label1.Text = Me.Text
        If Me.Text = "Loan Arrears Detail" Or Me.Text = "Loan Arrears Summary By CO" Or Me.Text = "Loan Outstanding Detail" Or Me.Text = "Loan Outstanding Summary by CO" Then
            Me.DateTimePicker2.Visible = False
            lblEnd.Visible = False
        ElseIf Me.Text = "ត្រូវយកប្រាក់ពីអតិថិជន" Then
            trues()
            lblCurrency.Visible = False
            cboCurrency.Visible = False
        ElseIf Me.Text = "LoanToRepayByBranch"
            trues()
            lblCurrency.Visible = False
            cboCurrency.Visible = False
        ElseIf Me.Text = "បញ្ចេញឥណទាន" Or Me.Text = "Bank Transaction" Or Me.Text = "Inactive Loan Income" Then
            Me.DateTimePicker2.Visible = True
            lblEnd.Visible = True
            lblEmID.Visible = True
            cboEmployee.Visible = True
            lblEmployee.Visible = True
            lblCurrency.Visible = True
            cboCurrency.Visible = True
        ElseIf Me.Text = "ការដាក់ និងដកទុន" Or Me.Text = "Summary loan to pay" Or Me.Text = "Summary loan paid" Or Me.Text = "Incorrect Repay" Or Me.Text = "SummaryLoanPaidByBranch" Or Me.Text = "AssetPaidOff" Then
            trues()
            lblCurrency.Visible = False
            cboCurrency.Visible = False
            lblEmID.Visible = False
            lblEmployee.Visible = False
            cboEmployee.Visible = False
        ElseIf Me.Text = "ចំណាយសរុប" Or Me.Text = "ចំណាយគិតរំលស់" Or Me.Text = "ចំណាយរំលស់ប្រចាំខែ" Or Me.Text = "ចំណាយផ្សេងៗមិនគិតរំលស់" Or Me.Text = "សង្ខេបចំណាយមិនគិតរំលស់" Then
            trues()
            lblCurrency.Visible = False
            cboCurrency.Visible = False
            lblEmployee.Visible = False
            lblEmID.Visible = False
            cboEmployee.Visible = False
        ElseIf Me.Text = "ស្តីពីប្រាក់សង្ខេប" Then
            trues()
            lblEnd.Visible = True
            DateTimePicker2.Visible = True
            lblCurrency.Visible = False
            cboCurrency.Visible = False
            lblEmployee.Visible = False
            lblEmID.Visible = False
            cboEmployee.Visible = False
        ElseIf Me.Text = "ឥណទានសកម្ម" Or Me.Text = "សមតុល្យនៃទ្រព្យ" Then
            trues()
            Label2.Enabled = False
            DateTimePicker1.Enabled = False
            lblEmployee.Enabled = False
            lblEmID.Enabled = False
            cboEmployee.Enabled = False
        ElseIf Me.Text = "Profit" And Me.Text = "ProfitByBrand" Then
            lblEmID.Enabled = False
            lblEmployee.Enabled = False
            cboEmployee.Enabled = False
            lblCurrency.Enabled = False
            cboCurrency.Enabled = False
        ElseIf Me.Text = "LoanSummaryByBranch" Then
            'Label2.Enabled = False
            'DateTimePicker1.Enabled = False
            lblEmID.Enabled = False
            lblEmployee.Enabled = False
            cboEmployee.Enabled = False
            lblCurrency.Enabled = False
            cboCurrency.Enabled = False
        ElseIf Me.Text = "Interest Summary" Or Me.Text = "Interest Detail" Or Me.Text = "WriteOff" Then
            lblCurrency.Enabled = False
            cboCurrency.Enabled = False
            lblWF.Enabled = False
            cboWF.Enabled = False
        ElseIf Me.Text = "LoanOutstandingSummaryByBrand" Or Me.Text = "LoanArrearsSummaryByBranch" Then
            lblCurrency.Enabled = False
            cboCurrency.Enabled = False
            lblEmID.Enabled = False
            cboEmployee.Enabled = False
            Label2.Enabled = False
            DateTimePicker1.Enabled = False
        Else
            Me.DateTimePicker2.Visible = True
            lblCurrency.Visible = True
            lblEmID.Visible = True
            lblEnd.Visible = True
            cboCurrency.Visible = True
        End If
    End Sub
    Private Sub trues()
        Me.DateTimePicker2.Visible = True
        lblEnd.Visible = True
        lblEmID.Visible = True
        cboEmployee.Visible = True
        lblEmployee.Visible = True
        lblCurrency.Visible = True
        cboCurrency.Visible = True
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.ProgressBar1.Visible = True Then
            MessageBox.Show("In progressing, don't click anything!!!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        Else
            Timer1.Start()
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.ProgressBar1.Visible = True Then
            MessageBox.Show("In progressing, don't click anything!!!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        Else
            Me.Close()
        End If
    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        If Me.cboEmployee.Text = "All" Or Me.cboEmployee.Text = "all" Then
            lblEmployee.Text = "ទាំងអស់"
        Else
            Dim Em As String = getData("select EM_Name from BK_Employee where EM_ID='" & cboEmployee.Text & "' and EM_BrID='" & frmMain.lblCode.Text & "'")
            If Em = "" Then
                lblEmployee.Text = "គ្មាន"
            Else
                lblEmployee.Text = Em
            End If
        End If
    End Sub
    Private Sub cboEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmployee.SelectedIndexChanged
        If Me.cboEmployee.Text = "All" Or Me.cboEmployee.Text = "all" Then
            lblEmployee.Text = "ទាំងអស់"
        Else
            Dim Em As String = getData("select EM_Name from BK_Employee where EM_ID='" & cboEmployee.Text & "' and EM_BrID='" & frmMain.lblCode.Text & "'")
            If Em = "" Then
                lblEmployee.Text = "គ្មាន"
            Else
                lblEmployee.Text = Em
            End If
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ProgressBar1.Visible = True
        Label3.Visible = True
        ProgressBar1.Increment(10)
        If ProgressBar1.Value = 100 Then
            Timer1.Stop()
            Try
                For Each Form In Me.MdiChildren
                    Form.Close()
                Next
                frmResultReport.MdiParent = frmMain
                frmResultReport.WindowState = FormWindowState.Maximized
                frmResultReport.Show()
                Me.Hide()
            Catch ex As Exception
                MsgBox("For Server only, contact IT for more detail!", MsgBoxStyle.Information, "IT Solution")
                ProgressBar1.Visible = False
                Label1.Visible = False
            End Try
        End If
    End Sub

    Private Sub cboBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranch.SelectedIndexChanged
        Try
            If Me.cboBranch.Text = "All" Or Me.cboBranch.Text = "all" Then
                lblBranch.Text = "All"
            Else
                Dim Em As String = getData("select CompanyKhmerName from BK_Company where CompanyID='" & Me.cboBranch.Text & "'")
                If Em = "" Then
                    lblBranch.Text = "Nothing"
                Else
                    lblBranch.Text = Em
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub cboEmployee_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboEmployee.KeyPress
        Try
            If Me.cboEmployee.Text = "All" Or Me.cboEmployee.Text = "all" Then
                lblEmployee.Text = "ទាំងអស់"
            Else
                Dim Em As String = getData("select EM_Name from BK_Employee where EM_ID='" & cboEmployee.Text & "' and EM_BrID='" & frmMain.lblCode.Text & "'")
                If Em = "" Then
                    lblEmployee.Text = "គ្មាន"
                Else
                    lblEmployee.Text = Em
                End If
            End If
        Catch ex As Exception
            Return
        End Try
    End Sub
End Class