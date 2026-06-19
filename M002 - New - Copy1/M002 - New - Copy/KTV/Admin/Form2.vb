Imports System.Data.SqlClient

Public Class frmSpecialRepay

    Private Sub frmSpecialRepay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim iRow = frmRepayment.DataGridView5.CurrentCell.RowIndex
        Dim iCol = frmRepayment.DataGridView5.CurrentCell.ColumnIndex
        Try
            Dim CM_ID1 As Integer = Val(getData("Select top 1 CM_ID1 from BK_Loan where LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID='" & frmRepayment.TextBox2.Text & "'"))
            Dim cm_Id As String = getData("Select top 1 CM_ID from BK_Loan where LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID='" & frmRepayment.TextBox2.Text & "'")
            Dim Name As String = getData("select top 1 CM_KhName from BK_Customer where ID='" & CM_ID1 & "' and CM_BrId='" & frmMain.lblCode.Text & "'")
            Dim Address As String = getData("select c.VL_ID+','+CN_ID+','+DT_ID+','+PV_ID addres from BK_Loan a inner join BK_Customer b on a.CM_ID=b.CM_ID and a.LD_BrId=b.CM_BrId inner join BK_Location c on b.LO_ID=c.LO_ID and b.CM_BrId=c.LO_BrID where LD_BrId='" & frmMain.lblCode.Text & "' and LD_ID='" & frmRepayment.TextBox2.Text & "' and b.ID='" & CM_ID1 & "'")
            'Label10.Text = cm_Id & ":" & " " & Name & ", អសយដ្ឋាន: " & Address
            txtLoanID.Text = frmRepayment.TextBox2.Text
            txtCMID.Text = cm_Id
            txtCMName.Text = Name
            txtAmtToPay.Text = frmRepayment.DataGridView5.Rows(iRow).Cells(1).Value
            txtDateToPay.Text = frmRepayment.DataGridView5.Rows(iRow).Cells(0).Value
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        '----------------------------------------------------
        If frmRepayment.DataGridView5.Rows(frmRepayment.DataGridView5.CurrentCell.RowIndex).Cells(7).Value <> "" Then
            MessageBox.Show("This schedule is already repay, please select other schedule!", "Special Repay", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim status As String = getData("select top 1 LD_Status from BK_Loan where LD_ID='" & txtLoanID.Text & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
        If status <> "Active" Then
            MessageBox.Show("This loan is already payoff, please check again.", "Special Repay", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        '---------------------------------------------------------------------------------------------
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "exec sp_repay '" & txtLoanID.Text & "','" & frmMain.lblCode.Text & "'"
        'On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        Dim prn As Double = oDt.Rows(0).Item(9).ToString
        Dim int As Double = oDt.Rows(0).Item(10).ToString
        Dim LD_Service As Double = oDt.Rows(0).Item(11).ToString
        oDa.Dispose()
        oDt.Dispose()
        '-----------------------------------------------------------------------------------------------
        If MessageBox.Show("Do you want to repay?", "Special Repay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim EM_ID As String = getData("select top 1 EM_ID from BK_Loan where LD_ID='" & txtLoanID.Text & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
            Dim CM_ID As String = getData("select top 1 CM_ID from BK_Loan where LD_ID='" & txtLoanID.Text & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
            Dim CM_ID1 As Integer = Val(getData("select top 1 CM_ID1 from BK_Loan where LD_ID='" & txtLoanID.Text & "' and LD_BrId='" & frmMain.lblCode.Text & "'"))
            frmRepayment.addSpecialRepay(txtLoanID.Text, CM_ID, frmMain.lblCode.Text, txtDateToPay.Text, EM_ID, "យកប្រាក់ពីអតិថិជន", txtAmtToPay.Text, Me.DatePaid.Value, txtAmtToPay.Text, 0, 1, frmMain.users, DateTime.Now(), 0, CM_ID1, prn, int, LD_Service, 1)
            MessageBox.Show("Record has been saved!", "Special Repay", MessageBoxButtons.OK, MessageBoxIcon.Information)
            AddToGridLDPaid1(frmRepayment.DataGridView5, 11, "exec spGetLoanRepayDetail '" & frmRepayment.TextBox2.Text.Trim & "','" & frmMain.lblCode.Text & "'")
            Me.Close()
        Else
        End If
    End Sub
End Class