Public Class frmChangeLocation
    Dim OldEm As String = ""
    Private Sub frmChangeLocation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        ShowDataGrid(DataGridView1, "exec spGetLDByLocation '" & frmMain.lblCode.Text & "'")
    End Sub

    Private Sub DataGridView1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        OldEm = Me.DataGridView1.CurrentCell.Value
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Try
            If e.ColumnIndex = 0 Then
                Dim EM_ID As String = getData("Select EM_ID from BK_Employee where EM_ID='" & Me.DataGridView1.CurrentCell.Value.ToString & "' and EM_BrID='" & frmMain.lblCode.Text & "'")
                If EM_ID <> "" Then
                    addIn("Update dbo.BK_Loan Set dbo.BK_Loan.EM_ID='" & Me.DataGridView1.CurrentCell.Value.ToString & "',dbo.BK_Loan.LD_User_Modify='" & frmMain.users & "',dbo.BK_Loan.LD_Date_Modify=GETDATE() From dbo.BK_Loan a Inner Join dbo.BK_Customer b On a.CM_ID=b.CM_ID and a.LD_BrId=b.CM_BrId Inner Join dbo.BK_Location c On b.LO_ID = c.LO_ID and b.CM_BrId = c.LO_BrID Inner Join dbo.BK_Employee d On a.EM_ID = d.EM_ID and a.LD_BrId= d.EM_BrID Where a.LD_Status='Active' and a.LD_BrId ='" & frmMain.lblCode.Text & "' and c.LO_ID='" & Me.DataGridView1.CurrentRow.Cells(2).Value & "' and a.EM_ID='" & OldEm & "'")
                    MessageBox.Show("Update completed!!!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("This employee not exist, please check again!", "Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch exception1 As Exception
            MessageBox.Show("Can't update, please check again", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Try
            If e.KeyCode = Keys.F11 Then
                ToExcel(Me.DataGridView1)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "IT Solution")
        End Try
    End Sub
End Class