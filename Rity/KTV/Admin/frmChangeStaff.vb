Public Class frmChangeStaff

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If txtStaffID.Text = "" Or txtName.Text = "" Or TextBox1.Text = "" Or TextBox2.Text = "" Then
        Else
            result = MyMessageBox.ShowBox("តើអ្នកចង់ផ្ទេរពត៌មានពីបុគ្គលិក '" & txtName.Text & "' ទៅបុគ្គលិក '" & TextBox2.Text & "' មែនទេ?", "ផ្ទេរពត៌មាន")
            If result = "1" Then
                addIn("update tblResource set staffid='" & TextBox1.Text & "' where staffid='" & txtStaffID.Text & "' and BrID='" & frmMain.lblCode.Text & "' and checking=0")
                resultError = frmMessageError.ShowBoxError("ការផ្ទេរពត៌មានបានជោគជ័យ។", "ផ្ទេរពត៌មាន")
            End If
        End If
    End Sub

    Private Sub txtStaffID_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtStaffID.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtName.Text = "" Then
                Return
            Else
                TextBox1.Focus()
            End If
        End If
    End Sub

    Private Sub txtStaffID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStaffID.TextChanged
        Try
            txtName.Text = getData("Select StaffName from tblStaff where BrID='" & frmMain.lblCode.Text & "' and StaffID= '" & txtStaffID.Text & "'")
            txtMoto.Text = getData("select MotoNo  from tblStaff where  BrID='" & frmMain.lblCode.Text & "' and StaffID ='" & txtStaffID.Text & "'")
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            TextBox2.Text = getData("Select StaffName from tblStaff where BrID='" & frmMain.lblCode.Text & "' and StaffID= '" & TextBox1.Text & "'")
            TextBox3.Text = getData("select MotoNo  from tblStaff where  BrID='" & frmMain.lblCode.Text & "' and StaffID ='" & TextBox1.Text & "'")
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
End Class