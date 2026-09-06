Imports X = Microsoft.Office.Interop.Excel
Public Class frmAssetType
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "ថ្មី" Then
            BtnNew.Text = "រក្សាទុក"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
        ElseIf BtnNew.Text = "រក្សាទុក" Then
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            addAssetType()
            addProduct()
            showasset()
        ElseIf BtnNew.Text = "រក្សាទុក" Then
            updateAsset()
            updateProduct()
            showasset()
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        End If
    End Sub
    Sub addAssetType()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtID.Text
                .Add("@d1", SqlDbType.NVarChar).Value = Me.txtStucture.Text
            End With
            sql = "insert into tblP(PID,PName) values (@d0,@d1)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "iT Solution")
        End Try
    End Sub
    Sub addProduct()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtID.Text
                .Add("@d1", SqlDbType.NVarChar).Value = Me.txtStucture.Text
            End With
            sql = "insert into tblProduct(PID,PName,amount) values (@d0,@d1,0)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Sub updateAsset()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtStucture.Text
            End With
            sql = "update tblP set PName=@d0  where PID=" & Me.DataGridView1.SelectedRows(0).Cells(0).Value
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Sub updateProduct()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtStucture.Text
            End With
            sql = "update tblProduct set PName=@d0  where PID=" & Me.DataGridView1.SelectedRows(0).Cells(0).Value
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Sub showasset()
        ShowDataGrid(DataGridView1, "select * from tblP")
    End Sub
    Private Sub frmAssetType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        showasset()
    End Sub
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                Me.txtID.Text = Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString
                Me.txtStucture.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        If BtnEdit.Text = "កែប្រែ" Then
            If Me.txtID.Text = "" Then
                resultError = frmMessageError.ShowBoxError("សូមជ្រើសរើស", "b")
                'MessageBox.Show("Please select any item to edit information.", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                BtnEdit.Enabled = False
                BtnNew.Text = "រក្សាទុក"
                BtnDelete.Enabled = False
                BtnExit.Text = "បោះបង់"
                Me.txtID.ReadOnly = True
            End If
        End If
    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "បោះបង់" Then
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            Me.Close()
        End If
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Me.txtID.Text = "" Then
            MessageBox.Show("Please select any item to delete", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf DataGridView1.SelectedRows.Count > 0 Then
            If MessageBox.Show("Are you sure you want to delete? ", "Monyroth Solution", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                addIn("delete from tblP where PID =" & Me.txtID.Text)
                MessageBox.Show("Successfully deleted the item", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                showasset()
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New X.Application
        Try
            Dim excelBook As X.Workbook = xlApp.Workbooks.Add
            Dim excelWorksheet As X.Worksheet = CType(excelBook.Worksheets(1), X.Worksheet)
            xlApp.Visible = True
            rowsTotal = DataGridView1.RowCount - 1
            colsTotal = DataGridView1.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                For iC = 0 To colsTotal
                    .Cells(1, iC + 1).Value = DataGridView1.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal
                    For j = 0 To colsTotal
                        .Cells(I + 2, j + 1).value = DataGridView1.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Regular"
                .Rows("1:1").Font.Size = 10
                .Cells.Columns.AutoFit()
                .Cells.Select()
                .Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
                .Cells.Font.Name = "Khmer os battambang"
                .Cells.Font.Size = 10
            End With
        Catch ex As Exception
            MsgBox("Export Excel Error " & ex.Message)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub

End Class