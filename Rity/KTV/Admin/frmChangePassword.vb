Public Class frmChangePassword

    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Me.Close()
    End Sub

    Private Sub btnapply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnapply.Click
        If Me.txtold.Text <> "" And Me.txtnew.Text <> "" And Me.txtnewverify.Text <> "" Then
            If isExist("select * from tbluser where EmployeeID='" & uid & "'and password='" & Me.txtold.Text & "' and Status='1'") = True Then
                If Me.txtnew.Text <> Me.txtnewverify.Text Then
                    MessageBox.Show("Please make sure the new password and the verified password is the same", "NiTA POS Solution")
                Else
                    addIn("Update tbluser set password='" & Me.txtnew.Text & "' where employeeid='" & uid & "' and password='" & Me.txtold.Text & "'")
                    MessageBox.Show("Successfully changed the password")
                End If
            End If
        End If
    End Sub
End Class