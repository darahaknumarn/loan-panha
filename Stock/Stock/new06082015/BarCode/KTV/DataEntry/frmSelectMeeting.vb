Public Class frmSelectMeeting

    Private Sub B4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B4.Click
        If Me.cbomeeting.Text <> "" Then
            Dashboard.meetingID = Me.cbomeeting.SelectedValue
            Me.Close()
        Else
            MessageBox.Show("សូមជ្រើសរើសការប្រជុំសំរាប់កត់ត្រាការចូលរួម Please select a meeting name to record the attendant", "NiTA Solution", MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub frmSelectMeeting_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        meetingName = Me.cbomeeting.Text
    End Sub

    Private Sub frmSelectMeeting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Sales1.tblMeeting.DataSet.Clear()
            Dim da As New SqlClient.SqlDataAdapter("Select * from tblmeeting", connectionString1)
            da.Fill(Me.Sales1.tblMeeting)
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA Solution")
        End Try
    End Sub

    Private Sub btnchoose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchoose.Click
        If Me.cbomeeting.Text <> "" Then
            meetingName = Me.cbomeeting.Text
            MeetingID = cbomeeting.SelectedValue
            Me.Close()
        Else
            MessageBox.Show("សូមជ្រើសរើសការប្រជុំសំរាប់កត់ត្រាការចូលរួម Please select a meeting name to record the attendant", "NiTA Solution", MessageBoxButtons.OK)
        End If
    End Sub

End Class