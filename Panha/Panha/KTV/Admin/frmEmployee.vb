Public Class frmEmployee

    Private Sub ToolStripLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub frmEmployee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'BarCodeDataSet2.tblPosition' table. You can move, or remove it, as needed.
        Me.TblPositionTableAdapter.Fill(Me.BarCodeDataSet2.tblPosition)
        'TODO: This line of code loads data into the 'BarCodeDataSet1.tblStaff' table. You can move, or remove it, as needed.
        Me.TblStaffTableAdapter.Fill(Me.BarCodeDataSet1.tblStaff)

    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        ofd.ShowDialog()
        ofd.Filter = "JPEG File (*.jpeg)|*.jpeg|JPG File (*.jpg)|*.jpg|All files (*.*)|*.*"
        Me.PictureBox1.ImageLocation = ofd.FileName.ToString
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()

    End Sub


    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        frmPosition.Show()
    End Sub

    Private Sub ផ្សេង()
        Throw New NotImplementedException
    End Sub

End Class