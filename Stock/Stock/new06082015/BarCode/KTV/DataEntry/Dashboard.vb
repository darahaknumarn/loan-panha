Public Class Dashboard
    Dim checkIt As Boolean = False
    Public meetingID As Integer

    Private Sub Dashboard_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MessageBox.Show("តើអ្នកពិតជាចង់បិទកម្មវិធីនេះមែនទេ?", "CamITSo", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub
    Private Sub Dashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        frmSelectMeeting.ShowDialog()
    End Sub
    Private Sub txtinput_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtinput.TextChanged
        Try
            If Len(Me.txtinput.Text) >= 10 Then
                ShowInfo(Me.txtinput.Text)

                Me.txtinput.SelectionStart = 0
                Me.txtinput.SelectionLength = 10
                ' Me.txtinput.SelectedText = True
            End If
        Catch ex As Exception
            ' MessageBox.Show(Err.Description)
        End Try
    End Sub
    Private Sub ShowInfo(ByVal input As String)
        Try
            Dim con As New System.Data.SqlClient.SqlConnection
            Dim com As New System.Data.SqlClient.SqlCommand
            Dim dr As System.Data.SqlClient.SqlDataReader
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = "Select idname,delegate_position,country,photo,event_attend from tbldelegate where barcode='" & input & "'"
            dr = com.ExecuteReader
            If dr.Read = True Then
                Me.imgphoto.ImageLocation = "D:\data\customers\MFA\Media\" & dr(3).ToString
                Me.lblCountry.Text = UCase(dr(2).ToString)
                Me.lblmeetingName.Text = UCase(meetingName) 'UCase(dr(4).ToString)
                Me.lblname.Text = UCase(dr(0).ToString)
                Me.lblposition.Text = dr(1).ToString
                RecordToSystem(Me.txtinput.Text)
                RecordingTimes(Me.txtinput.Text)
            Else
                MessageBox.Show("Can't find the information, please check again", "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.lblmeetingName.Text = "Can't find this delegate info"
                Me.lblCountry.Text = "Can't find this delegate info"
                Me.lblname.Text = "Can't find this delegate info"
                Me.lblposition.Text = "Can't find this delegate info"
            End If
            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Sub RecordToSystem(ByVal barCode As String)
        addIn("Insert into tblRecording(AttendantTimeDate,Meeting,BarCode,SelectedMeeting) values('" & Now & "','" & Me.lblmeetingName.Text & "','" & barCode & "','" & meetingName & "')")
    End Sub
    Sub RecordingTimes(ByVal Input As String)
        Try
            Dim con As New System.Data.SqlClient.SqlConnection
            Dim com As New System.Data.SqlClient.SqlCommand
            Dim dr As System.Data.SqlClient.SqlDataReader
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = "Select count(barcode) from tblRecording where barcode='" & Input & "'"
            dr = com.ExecuteReader
            If dr.Read = True Then
                Me.lbltick.Text = dr(0).ToString
            End If
            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lblposition_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblposition.Click

    End Sub

    Private Sub lblname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblname.Click

    End Sub
End Class