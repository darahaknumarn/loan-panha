Public Class frmMaintenance

    

    

    

    

    Private Sub brole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles brole.Click
        If MessageBox.Show("Are you sure want to clear databas, note that if you select Yes all of data will be cleared?", "Monyroth Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
            addIn("delete from tbldelegate")
            addIn("delete from tblrecording")
            addIn("delete from tblwinner")
            MessageBox.Show("All of your data has been cleared.", "Monyroth Solution", MessageBoxButtons.OK)
        End If
        'For Each Form In Me.MdiChildren
        '    Form.Close()
        'Next
        'frmMain.lblshow.Text = Me.brole.Text
        'frmrole.MdiParent = frmMain
        'frmrole.Show()
    End Sub

    Private Sub buser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buser.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmMain.lblshow.Text = Me.buser.Text
        frmuser.MdiParent = frmMain
        frmuser.Show()
    End Sub
    Private Sub btncountry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmMain.lblshow.Text = Me.buser.Text
        frmCountry.MdiParent = frmMain
        frmCountry.Show()
    End Sub
    Private Sub btnPosition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmMain.lblshow.Text = Me.buser.Text
        frmPosition.MdiParent = frmMain
        frmPosition.Show()
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmMain.lblshow.Text = Me.buser.Text
        frmDelegate.MdiParent = frmMain
        frmDelegate.Show()
    End Sub
End Class