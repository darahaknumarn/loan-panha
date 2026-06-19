Public Class frmMeeting
    Dim myMID As Integer
    Private Sub B4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B4.Click
        Me.Close()
    End Sub

    Private Sub B1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B1.Click
        If Me.B1.Text.Equals("&New") Then
            Me.B1.Text = "&Save"
            Me.B2.Enabled = False
            Me.B3.Text = "&Cancel"
            Me.B4.Enabled = False
            Me.dstart.Value = Now.Date
            Me.dstart.Value = Now.Date
            Me.txtMeetingName.Text = ""
            Me.txtMeetingVenue.Text = ""
            Me.txtDesc.Text = ""
        ElseIf Me.B1.Text.Equals("&Save") Then
            If Me.txtMeetingName.Text <> "" And Me.txtMeetingVenue.Text <> "" Then
                addMeeting()
            Else
                Exit Sub
            End If
            Me.B1.Text = "&New"
            Me.B2.Text = "&Edit"
            Me.B3.Text = "&Delete"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            '------------------------add customer
            AddToGrid(Me.dgdetail, 6, "select * from tblMeeting")
        ElseIf Me.B1.Text.Equals("&Update") Then
            If Me.txtMeetingName.Text <> "" And Me.txtMeetingVenue.Text <> "" And myMID > 0 Then
                UpdateMeeting(myMID)
            Else
                Exit Sub
            End If
            Me.B1.Text = "&New"
            Me.B2.Text = "&Edit"
            Me.B3.Text = "&Delete"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            AddToGrid(Me.dgdetail, 6, "select * from tblMeeting")
        End If
    End Sub

    Private Sub B2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B2.Click
        If myMID > 0 Then
            Me.B1.Text = "&Update"
            Me.B2.Enabled = False
            Me.B3.Text = "&Cancel"
            Me.B4.Enabled = False
        Else
            MessageBox.Show("Please select any item to edit", "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    Private Sub B3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B3.Click
        If Me.B3.Text.Equals("&Cancel") Then
            Me.B2.Enabled = True
            Me.B2.Text = "&Edit"
            Me.B4.Enabled = True
            Me.B1.Text = "&New"
            Me.B3.Text = "&Delete"
        ElseIf Me.B3.Text = "&Delete" Then
            If myMID > 0 Then
                If MessageBox.Show("Are you sure you want to delete? ", "NiTA Solution", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    addIn("Delete from tblMeeting where MID='" & myMID & "'")
                    MessageBox.Show("Successfully delete the item", "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    AddToGrid(Me.dgdetail, 6, "select * from tblMeeting")
                End If
            Else
                MessageBox.Show("Please select any item to delete", "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Sub addMeeting()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.DateTime).Value = Me.dstart.Value
                .Add("@d2", SqlDbType.DateTime).Value = Me.dStop.Value
                .Add("@d3", SqlDbType.NVarChar).Value = Me.txtMeetingName.Text
                .Add("@d4", SqlDbType.NVarChar).Value = Me.txtMeetingVenue.Text
                .Add("@d5", SqlDbType.NVarChar).Value = Me.txtDesc.Text
            End With
            sql = "Insert into tblMeeting(MStart,MStop,MName,MVenue,MDescription) values(@d1,@d2,@d3,@d4,@d5)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
    End Sub
    Sub UpdateMeeting(ByVal mMid As Integer)
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.Int).Value = mMid
                .Add("@d2", SqlDbType.DateTime).Value = Me.dstart.Value
                .Add("@d3", SqlDbType.DateTime).Value = Me.dStop.Value
                .Add("@d4", SqlDbType.NVarChar).Value = Me.txtMeetingName.Text
                .Add("@d5", SqlDbType.NVarChar).Value = Me.txtMeetingVenue.Text
                .Add("@d6", SqlDbType.NVarChar).Value = Me.txtDesc.Text

            End With
            sql = "Update tblMeeting set MStart=@d2,MStop=@d3,MName=@d4,MVenue=@d5,MDescription=@d6 where MID=@d1"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try

    End Sub

    Private Sub frmMeeting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddToGrid(Me.dgdetail, 6, "select * from tblMeeting")
    End Sub

    Private Sub dgdetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgdetail.Click
        Try
            If Me.dgdetail.SelectedRows.Count > 0 Then
                myMID = Val(Me.dgdetail.SelectedRows(0).Cells(0).Value.ToString)
                Me.dstart.Value = If(IsDBNull(Me.dgdetail.SelectedRows(0).Cells(1).Value), Now.Date, Me.dgdetail.SelectedRows(0).Cells(1).Value)
                Me.dStop.Value = If(IsDBNull(Me.dgdetail.SelectedRows(0).Cells(2).Value), Now.Date, Me.dgdetail.SelectedRows(0).Cells(2).Value)
                Me.txtMeetingName.Text = Me.dgdetail.SelectedRows(0).Cells(3).Value
                Me.txtMeetingVenue.Text = Me.dgdetail.SelectedRows(0).Cells(4).Value
                Me.txtDesc.Text = Me.dgdetail.SelectedRows(0).Cells(5).Value
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
    End Sub

    Private Sub pInfo_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pInfo.Paint

    End Sub
End Class