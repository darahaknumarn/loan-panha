Public Class frmMeasure

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "New" Then
            BtnNew.Text = "Save"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "Cancel"
        ElseIf BtnNew.Text = "Save" Then
            BtnNew.Text = "New"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "Exit"
            addOum()
        Else
            If DataGridView1.SelectedRows.Count > 0 Then
                updateOum()
            End If
            BtnNew.Text = "New"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "Exit"
            ShowDataGrid(DataGridView1, "Select oumID Code,oumName Name from tblOum")
        End If
    End Sub
    Private Sub showOum()
        ShowDataGrid(DataGridView1, "Select oumID as 'កូដ',oumName as 'ឈ្មោះ' from tblOum")
    End Sub
    Private Sub updateOum()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = TextBox2.Text
            End With
            sql = "update tblOum set oumName=@d0 where oumID=" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
            ShowDataGrid(DataGridView1, "Select oumID as 'កូដ',oumName as 'ឈ្មោះ' from tblOum")
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub addOum()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = TextBox2.Text
                .Add("@d1", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
            End With
            sql = "insert into tblOum (oumName,BrID ) values(@d0,@d1)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
            ShowDataGrid(DataGridView1, "Select oumID as 'កូដ',oumName as 'ឈ្មោះ' from tblOum")
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        If BtnEdit.Text = "Edit" Then
            BtnEdit.Enabled = False
            BtnNew.Text = "Update"
            BtnDelete.Enabled = False
            BtnExit.Text = "Cancel"
        End If
    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "Cancel" Then
            BtnNew.Text = "New"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "Exit"
        Else
            Me.Close()
        End If
    End Sub
    Private Sub frmMeasure_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        AddCombo(frmProduct.ComboBox1, "Select oumName from tblOum")
    End Sub
    Private Sub frmMeasure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        TextBox2.Focus()
        ShowDataGrid(DataGridView1, "Select oumID Code,oumName Name from tblOum")
    End Sub
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                TextBox2.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value.ToString
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox2.Text = "" Then
                Return
            Else
                If MessageBox.Show("Are you sure want to save this item?", "Monyroth Solution", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                    If TextBox2.Text = "" Then
                        MessageBox.Show("Cannot save blank item!!!", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    Else
                        addOum()
                        ShowDataGrid(DataGridView1, "Select oumID Code,oumName Name from tblOum")
                    End If
                End If
                TextBox2.Focus()
            End If
        End If
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                If MessageBox.Show("Are you sure want to delete this item???", "Monyroth Solution", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                    addIn("delete from tblOum where oumID =" & Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString)
                    MessageBox.Show("Delete item successful.", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("Please select item to delete first, try again.", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            ShowDataGrid(DataGridView1, "Select oumID Code,oumName Name from tblOum")
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
End Class