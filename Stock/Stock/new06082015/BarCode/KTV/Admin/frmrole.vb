Public Class frmrole
    Dim RID As Integer
    Private Sub B4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B4.Click
        Me.Close()
    End Sub

    Private Sub B1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B1.Click
        If Me.B1.Text.Equals("បញ្ជូលថ្មី") Then
            Me.B1.Text = "ថតទុក"
            Me.B2.Enabled = False
            Me.B3.Text = "ឈប់វិញ"
            Me.B4.Enabled = False
        ElseIf Me.B1.Text.Equals("ថតទុក") Then
            Me.B1.Text = "បញ្ជូលថ្មី"
            Me.B2.Text = "កែរប្រែ"
            Me.B3.Text = "លុបចោល"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            AddRole(Me.txtrole.Text, "", 1)
            AddToGrid(dgrole, 4, "Select * from tblrole")
        ElseIf Me.B1.Text.Equals("ថតការកែរប្រែ") Then
            Me.B1.Text = "បញ្ជូលថ្មី"
            Me.B2.Text = "កែរប្រែ"
            Me.B3.Text = "លុបចោល"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            UpdateRole(RID, Me.txtrole.Text, "", 1)
            AddToGrid(dgrole, 4, "Select * from tblrole")
        End If
    End Sub
    Sub AddRole(ByVal Roles As String, ByVal Description As String, ByVal Status As Integer)
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection

            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = Roles
                .Add("@d2", SqlDbType.NVarChar).Value = Description
                .Add("@d3", SqlDbType.Int).Value = Status

            End With
            com.CommandText = "insert into tblRole(Roles,Description,Status) values(@d1,@d2,@d3)"
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try

    End Sub
    Sub UpdateRole(ByVal RID As Integer, ByVal Roles As String, ByVal Description As String, ByVal Status As Integer)
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection

            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = Roles
                .Add("@d2", SqlDbType.NVarChar).Value = Description
                .Add("@d3", SqlDbType.Int).Value = Status
                .Add("@d4", SqlDbType.Int).Value = RID
            End With
            com.CommandText = "Update tblRole set Roles=@d1,Description=@d2,Status=@d3 where RID=@d4"
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try

    End Sub
    Private Sub B2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B2.Click
        If RID > 0 Then
            Me.B1.Text = "ថតការកែរប្រែ"
            Me.B2.Enabled = False
            Me.B3.Text = "ឈប់វិញ"
            Me.B4.Enabled = False
        Else
            MessageBox.Show("Please select one of the role to edit", "NiTA Solution")
        End If
    End Sub

    Private Sub B3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B3.Click
        If Me.B3.Text.Equals("ឈប់វិញ") Then
            Me.B2.Enabled = True
            Me.B2.Text = "កែរប្រែ"
            Me.B4.Enabled = True
            Me.B1.Text = "បញ្ជូលថ្មី"
            Me.B3.Text = "លុបចោល"
        ElseIf Me.B3.Text = "លុបចោល" Then
            If RID > 0 Then
                If MessageBox.Show("Are you sure you want to delete this role?", "NiTA Solution", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                    addIn("Delete from tblRole where RID=" & RID)
                End If
            Else
                MessageBox.Show("Please select one Role to delete", "NiTA Solution")
            End If
        End If
    End Sub
    Private Sub dgrole_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgrole.Click
        If Me.dgrole.SelectedRows.Count > 0 Then
            RID = Me.dgrole.SelectedRows(0).Cells(0).Value
        End If
    End Sub

    Private Sub frmrole_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddToGrid(dgrole, 4, "Select * from tblrole")
    End Sub
End Class