Public Class FrmCollateral
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "ថ្មី" Then
            BtnNew.Text = "រក្សាទុក"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
            txtCollateral_ID.Text = GetLastID("ID", "tblCollateral") + 1
        ElseIf BtnNew.Text = "រក្សាទុក" Then
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            If txtCollateral_Name.Text = "" Then
                resultError = frmMessageError.ShowBoxError(" មិនអាចរក្សាទុកទិន្នន័យបានទេព្រោះពត៌មានមិនគ្រប់គ្រាន់ ", "ទិន្នន័យមិនគ្រប់គ្រាន់")
                ' MessageBox.Show("Please fill Collateral Name", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If
            add()
            'addIn("insert tblcollateral(collateralname) values ('" & Me.txtCollateral_Name.Text & "')")
            Showcollateral()
        ElseIf BtnNew.Text = "រក្សាទុក" Then
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            If Me.txtCollateral_Name.Text = "" Then
                resultError = frmMessageError.ShowBoxError(" មិនអាចរក្សាទុកទិន្នន័យបានទេព្រោះពត៌មានមិនគ្រប់គ្រាន់ ", "ទិន្នន័យមិនគ្រប់គ្រាន់")
                '  MessageBox.Show("Please insert collateral name to update.")
            Else
                updateCo()
                'addIn("update tblcollateral set collateralname='" & Me.txtCollateral_Name.Text & "' where id=" & Me.txtCollateral_ID.Text)
                Showcollateral()
            End If

        Else
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        End If
    End Sub
    Sub updateCo()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtCollateral_Name.Text
            End With
            sql = "update tblcollateral set collateralname=@d0 where id=" & Me.DGridCollateral.SelectedRows(0).Cells(0).Value
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
    Sub Showcollateral()
        AddToGrid(DGridCollateral, 2, "select id, CollateralName  from tblcollateral where BrID='" & frmMain.lblCode.Text & "'")
    End Sub

    Private Sub BtnExit_Click(sender As System.Object, e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "បោះបង់" Then
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            Me.Close()
        End If
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        If BtnEdit.Text = "កែប្រែ" Then
            If txtCollateral_ID.Text = "" Then
                resultError = frmMessageError.ShowBoxError(" សូ​មជ្រើសរើសទ្រព្យដើម្បីកែប្រែ​ ", "មិនអាចកែប្រែបានទេ")
                ' MessageBox.Show("Please select any item to edit first.", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                BtnEdit.Enabled = False
                BtnNew.Text = "រក្សាទុក"
                BtnDelete.Enabled = False
                BtnExit.Text = "បោះបង់"
            End If
        End If
    End Sub
    Sub add()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = Me.txtCollateral_Name.Text
                .Add("@d1", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
            End With
            sql = "insert tblcollateral(collateralname,BrID) values (@d0,@d1)"
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


    
    Private Sub DGridCollateral_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.DGridCollateral.SelectedRows.Count > 0 Then
                Me.txtCollateral_ID.Text = Me.DGridCollateral.SelectedRows(0).Cells(0).Value.ToString
                Me.txtCollateral_Name.Text = Me.DGridCollateral.SelectedRows(0).Cells(1).Value

            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub

    Private Sub FrmCollateral_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        BtnNew.Enabled = False
        BtnDelete.Enabled = False
        BtnEdit.Enabled = False
        'BtnNew.Focus = False
        '--------------------------------------------set header font
        With DGridCollateral.ColumnHeadersDefaultCellStyle
            .Alignment = DataGridViewContentAlignment.MiddleCenter
            .BackColor = Color.DarkRed
            .ForeColor = Color.Gold
            .Font = New Font("Khmer OS Battambang", 10, FontStyle.Regular, GraphicsUnit.Point)
        End With

        '---------------------------------- Set font datagridview
        Dim cs As New DataGridViewCellStyle
        cs.ForeColor = Color.Black
        cs.Font = New Font("Khmer OS Battambang", 10, FontStyle.Regular)
        DGridCollateral.RowsDefaultCellStyle = cs
        '---------------------------------------------------------
        Showcollateral()
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        If Me.txtCollateral_ID.Text = "" Then
            result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យនេះមែនទេ", "លុបទិន្នន័យ")
            ' MessageBox.Show("Please select any item to delete", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf DGridCollateral.SelectedRows.Count > 0 Then

            If result = "1" Then
                Dim a As String = getData("select collateralid from tblResource where collateralid='" & Me.txtCollateral_ID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
                If a = "" Then
                    addIn("delete from tblCollateral where tblCollateral .id ='" & Me.txtCollateral_ID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
                    '    MessageBox.Show("Successfully delete the item", "Monyroth Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    '   MessageBox.Show("Sorry cannot delete this item because in using !", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If


                Showcollateral()
            End If
            'If MessageBox.Show("Are you sure you want to delete? ", "Monyroth Solution", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

            'End If
        End If
    End Sub

    Private Sub DGridCollateral_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Me.DGridCollateral.SelectedRows.Count > 0 Then
                Me.txtCollateral_ID.Text = Me.DGridCollateral.SelectedRows(0).Cells(0).Value.ToString
                Me.txtCollateral_Name.Text = Me.DGridCollateral.SelectedRows(0).Cells(1).Value
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
End Class