Public Class frmProduct
    Sub shows()
        AddToGrid(DataGridView1, 4, "select assetID ,assetName ,oumName,totalAmount from tblAsset where BrID='" & frmMain.lblCode.Text & "' order by assetID")
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtPid.Text = "" Then
            resultError = frmMessageError.ShowBoxError("គ្មានទិន្នន័យសំរាប់លុប", "លុបទិន្នន័យ")
        Else
            result = MyMessageBox.ShowBox("បើសិនជាអ្នកលុបកូដទ្រព្យនេះ ទិន្នន័យទាំងអស់ដែលទាក់ទងនឹងវានិងបាត់ទាំងអស់", "លុបទិន្នន័យ")
            If result = "1" Then
                addIn("delete from tblAsset where assetID=" & Me.DataGridView1.SelectedRows(0).Cells(0).Value)
                resultError = frmMessageError.ShowBoxError("លុបបានជោគជ័យ", "លុបទិន្នន័យ")
                shows()
                Return
            End If
        End If
    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
            Me.Close()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.txtPid.Text = "" Or Me.txtPname.Text = "" Or txtAmount.Text = "" Then
            resultError = frmMessageError.ShowBoxError("ជ្រើសរើសទិន្នន័យជាមុនសិន", "ជ្រើសរើស")
            Return
        Else
            For Each Form In Me.MdiChildren
                Form.Close()
            Next
            frmAdd.MdiParent = frmMain
            frmAdd.WindowState = FormWindowState.Maximized
            frmAdd.Show()
            frmAdd.Text = "+"
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ToExcel(DataGridView1)
    End Sub
    Private Sub txtPid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPid.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPid.TextChanged
        Try
            txtPname.Text = getData("Select assetName from tblAsset where BrID='" & frmMain.lblCode.Text & "' and assetID= '" & txtPid.Text & "'")
            txtAmount.Text = getData("Select oumName from tblAsset where  BrID='" & frmMain.lblCode.Text & "' and assetID='" & txtPid.Text & "'")
            ComboBox1.Text = getData("select oumName from tblAsset where  BrID='" & frmMain.lblCode.Text & "' and assetID='" & txtPid.Text & "'")
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub frmProduct_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If frmMain.users = "sh" Then
            Button1.Enabled = False
            Button3.Enabled = False
            Button6.Enabled = False
        End If
        AddCombo(ComboBox1, "Select oumName from tblOum where BrID='" & frmMain.lblCode.Text & "'")
        shows()
        SetFontDatagrid(DataGridView1)
    End Sub
    Sub addProduct()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtPid.Text
                .Add("@d1", SqlDbType.NVarChar).Value = Me.txtPname.Text
                .Add("@d3", SqlDbType.NVarChar).Value = ComboBox1.Text
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
            End With
            sql = "insert into tblAsset (assetID ,assetName ,oumName,totalAmount,BrID  ) values(@d0,@d1,@d3,0,@d4)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Sub updateProduct()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtPname.Text
                .Add("@d1", SqlDbType.NVarChar).Value = ComboBox1.Text
            End With
            sql = "update tblAsset set assetName=@d0,oumName=@d1  where assetID=" & Me.DataGridView1.SelectedRows(0).Cells(0).Value
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                txtPid.Text = Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString
                Me.txtPname.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value
                Me.txtAmount.Text = Me.DataGridView1.SelectedRows(0).Cells(3).Value
                ComboBox1.Text = DataGridView1.SelectedRows(0).Cells(2).Value.ToString
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub Button3_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        frmMeasure.Owner = Me
        frmMeasure.Show()
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        ToExcel(DataGridView1)
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        frmDelegate.Show()
        frmDelegate.Text = "Asset"
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        shows()
    End Sub

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class