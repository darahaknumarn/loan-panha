Public Class frmAdd
    Dim ID As String = frmMain.lblCode.Text
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmAdd_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        AddToGrid(frmProduct.DataGridView1, 4, "select assetID as 'កូដទ្រព្យ',assetName as 'ឈ្មោះទ្រព្យ',oumName as 'រង្វាស់',totalAmount as 'ចំនួនសរុប' from tblAsset ")
    End Sub
    Sub shows()
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 6
        DataGridView1.Columns(0).Name = "កូដ"
        DataGridView1.Columns(1).Name = "កូដទ្រព្យ"
        DataGridView1.Columns(2).Name = "ចំនួនសរុប"
        DataGridView1.Columns(3).Name = "តំលៃឯកតា"
        DataGridView1.Columns(4).Name = "កាលបរិច្ឆេត"
        DataGridView1.Columns(5).Name = "រូបបិយវត្ថុ"
        AddToGrid(DataGridView1, 6, "select ID,assetID ,amount ,unitprice ,dates,currency from tblAssetAdd where BrID='" & frmMain.lblCode.Text & "' order by ID desc")
    End Sub
    Private Sub frmAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        shows()
        ComboBox1.SelectedIndex = 0
        txtAmount.Focus()
        txtAssetID.Text = frmProduct.txtPid.Text
        txtAssetName.Text = frmProduct.txtPname.Text
    End Sub
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "ថ្មី" Then
            BtnNew.Text = "រក្សាទុកថ្មី"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
        ElseIf BtnNew.Text = "រក្សាទុកថ្មី" Then
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            If txtAssetID.Text = "" Or txtAssetName.Text = "" Or txtAmount.Text = "" Or txtUnitPrice.Text = "" Then
                resultError = frmMessageError.ShowBoxError("មិនមានទិន្នន័យគ្រប់គ្រាន់ សូមបំពេញពត៌មានជាមុនសិន។", "ទិន្នន័យគ្រប់គ្រាន់")
            Else
                If Val(txtAmount.Text) <= 0 Then
                    resultError = frmMessageError.ShowBoxError("មិនអាចរក្សាទុកបានទេព្រោះទិន្នន័យតូចជាងសូន្យ", "ពិនិត្យឡើងវិញ")
                    Return
                End If
                add()
                Dim a As Integer = getData("select totalAmount from tblAsset where assetID ='" & Me.txtAssetID.Text & "'")
                addIn("update tblAsset set totalAmount = totalAmount + " & Me.txtAmount.Text & " where assetID='" & txtAssetID.Text & "'")
                shows()
            End If
        Else
            If txtAssetName.Text = "" Or txtAssetName.Text = "" Or Me.txtAmount.Text = "" Or Me.txtUnitPrice.Text = "" Then
                resultError = frmMessageError.ShowBoxError("ពត៍មានមិនគ្រប់គ្រាន់", "ពិនិត្យឡើងវិញ")
                Return
            End If
            '-----------------------------------------------------------------------------------------
            If Val(txtAmount.Text) <= 0 Then
                resultError = frmMessageError.ShowBoxError("មិនអាចធ្វើការកែប្រែចំនួនសម្ភារៈតូចជាង រឺ ​ស្មើរសូន្យបានឡើយ។", "មិនអាចកែប្រែ")
            Else
                Dim addAmount As Integer = Val(getData("select amount  from tblAssetAdd where ID ='" & DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "'"))
                Dim minusamount As Integer = addAmount - Val(txtAmount.Text)
                addIn("update tblAsset set totalAmount = totalAmount - " & minusamount & " where assetID='" & txtAssetID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
                If ComboBox1.Text = "Riel" Then
                    addIn("update tblAssetAdd set assetID ='" & txtAssetID.Text & "',amount =amount-'" & minusamount & "',unitprice ='" & txtUnitPrice.Text & "',Riel ='" & Val(txtUnitPrice.Text) * Val(txtAmount.Text) & "',Dollar =0,dates ='" & DateTimePicker1.Value & "',currency ='" & ComboBox1.Text & "' where ID ='" & DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "' and BrID='" & frmMain.lblCode.Text & "'")
                Else
                    addIn("update tblAssetAdd set assetID ='" & txtAssetID.Text & "',amount =amount-'" & minusamount & "',unitprice ='" & txtUnitPrice.Text & "',Riel =0,Dollar ='" & Val(txtUnitPrice.Text) * Val(txtAmount.Text) & "',dates ='" & DateTimePicker1.Value & "',currency ='" & ComboBox1.Text & "' where ID ='" & DataGridView1.SelectedRows(0).Cells(0).Value.ToString & "' and BrID='" & frmMain.lblCode.Text & "'")
                End If
            End If
            resultError = frmMessageError.ShowBoxError("ទិន្នន័យបានកែប្រែជោគជ័យ", "កែទិន្នន័យ")
            shows()
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        End If
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If BtnEdit.Text = "កែប្រែ" Then
                If txtAssetID.Text = "" And txtAssetName.Text = "" Then
                    MessageBox.Show("Please select item to edit first, try again!", "Monyroth", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                Else
                    BtnEdit.Enabled = False
                    BtnNew.Text = "រក្សាទុក"
                    BtnDelete.Enabled = False
                    BtnExit.Text = "បោះបង់"
                End If
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Dim BrID As String = frmMain.lblCode.Text
        If txtAssetID.Text = "" And txtAssetName.Text = "" And Me.DataGridView1.SelectedRows.Count < 0 Then
            resultError = frmMessageError.ShowBoxError("គ្មានទិន្នន័យត្រូវលុប​ សូមជ្រើសរើសជាមុនសិន។", "គ្មានទិន្នន័យ")
            Return
        Else
            result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
            If result = "1" Then
                Dim addamount As Integer = getData("select amount from tblAssetAdd where ID=" & Me.DataGridView1.SelectedRows(0).Cells(0).Value)
                addIn("delete from tblAssetAdd where ID='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                addIn("delete from tblAssetDetail where assetAddID ='" & Me.DataGridView1.SelectedRows(0).Cells(0).Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                addIn("update tblAsset set totalAmount='" & addamount & "' - totalAmount where assetID='" & frmProduct.txtPid.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
                shows()
                resultError = frmMessageError.ShowBoxError("ទិន្នន័យបានលុបរួចរាល់។", "លុបទិន្នន័យ")
            Else
                Return
            End If
        End If
    End Sub
    Private Sub add()
        Dim ID As String = frmMain.lblCode.Text
        Dim ID1 As String = getData("select top 1 ID from tblAssetAdd order by ID desc")
        If ID1 = "" Or ID1 = "0" Then
            ID1 = "1"
        Else
            ID1 = Convert.ToDecimal(ID1) + 1
        End If
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d0", SqlDbType.NVarChar).Value = txtAssetID.Text
                .Add("@d1", SqlDbType.Float).Value = txtAmount.Text
                .Add("@d2", SqlDbType.Float).Value = txtUnitPrice.Text
                .Add("@d3", SqlDbType.DateTime).Value = DateTimePicker1.Value
                .Add("@d4", SqlDbType.NVarChar).Value = ComboBox1.Text
                .Add("@d6", SqlDbType.Float).Value = Val(Me.txtUnitPrice.Text) * Val(txtAmount.Text)
                .Add("@d7", SqlDbType.NVarChar).Value = ID
                .Add("@d8", SqlDbType.Int).Value = ID1
            End With
            If ComboBox1.Text = "Riel" Then
                sql = "insert into tblAssetAdd (ID,assetID ,amount ,unitprice,Riel,Dollar,dates ,currency ,BrID ) values(@d8,@d0,@d1,@d2,@d6,0,@d3,@d4,@d7)"
            Else
                sql = "insert into tblAssetAdd (ID,assetID ,amount ,unitprice,Riel,Dollar,dates ,currency ,BrID) values(@d8,@d0,@d1,@d2,0,@d6,@d3,@d4,@d7)"
            End If
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
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "បោះបង់" Then
            BtnNew.Text = "ថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            Me.Close()
            AddToGrid(frmProduct.DataGridView1, 4, "select assetID as 'កូដទ្រព្យ',assetName as 'ឈ្មោះទ្រព្យ',oumName as 'រង្វាស់',totalAmount as 'ចំនួនសរុប' from tblAsset where BrID='" & frmMain.lblCode.Text & "' order by assetID")
        End If
    End Sub
    Private Sub txtAssetID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAssetID.TextChanged
        txtAssetName.Text = getData("select assetName from tblAsset where assetID ='" & txtAssetID.Text & "'")
    End Sub
    Private Sub txtAmount_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAmount.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtAmount.Text = "" Then
                Return
            Else
                txtUnitPrice.Focus()
            End If
        End If
    End Sub
    Private Sub txtUnitPrice_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUnitPrice.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtAmount.Text = "" And txtAssetID.Text = "" And txtAssetName.Text = "" And txtUnitPrice.Text = "" Then
                Return
            Else
                result = MyMessageBox.ShowBox(" តើអ្នកចង់រក្សាទិន្នន័យនេះមែនទេ? ", "រក្សាទុកទិន្នន័យ")
                If result = "1" Then
                    If txtAmount.Text = "" Or txtAssetID.Text = "" Or txtAssetName.Text = "" Or txtUnitPrice.Text = "" Then
                        resultError = frmMessageError.ShowBoxError(" មិនអាចរក្សាទុកទិន្នន័យបានទេ ព្រោះពត៌មានមិនគ្រប់គ្រាន់ ។", "ទិន្នន័យមិនគ្រប់គ្រាន់")
                        Return
                    Else
                        If Val(txtAmount.Text) = 0 Or Val(txtAmount.Text) < 0 Then
                            resultError = frmMessageError.ShowBoxError("មិនអាចរក្សាទុកបានទេព្រោះទិន្នន័យតូចជាងសូន្យ", "ពិនិត្យឡើងវិញ")
                            Return
                        End If
                        add()
                        Dim a As Integer = getData("select totalAmount from tblAsset where assetID ='" & Me.txtAssetID.Text & "'")
                        If Me.Text = "+" Then
                            addIn("update tblAsset set totalAmount = totalAmount + " & Me.txtAmount.Text & " where assetID='" & txtAssetID.Text & "'")
                        Else
                            addIn("update tblAsset set totalAmount = totalAmount - " & Me.txtAmount.Text & " where assetID='" & txtAssetID.Text & "'")
                        End If
                        shows()
                        txtAmount.Text = ""
                        txtUnitPrice.Text = ""
                        txtAmount.Focus()
                    End If

                End If
            End If
        End If
    End Sub
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                txtAssetID.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value.ToString
                txtAssetName.Text = getData("select assetName from tblAsset where assetID='" & DataGridView1.SelectedRows(0).Cells(1).Value.ToString & "'")
                Me.txtAmount.Text = Me.DataGridView1.SelectedRows(0).Cells(2).Value
                Me.txtUnitPrice.Text = Me.DataGridView1.SelectedRows(0).Cells(3).Value
                Me.DateTimePicker1.Value = Me.DataGridView1.SelectedRows(0).Cells(4).Value
                Me.ComboBox1.Text = Me.DataGridView1.SelectedRows(0).Cells(5).Value.ToString
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        DataGridView1.Columns.Clear()
        DataGridView1.ColumnCount = 6
        DataGridView1.Columns(0).Name = "កូដ"
        DataGridView1.Columns(1).Name = "កូដទ្រព្យ"
        DataGridView1.Columns(2).Name = "ចំនួនសរុប"
        DataGridView1.Columns(3).Name = "តំលៃឯកតា"
        DataGridView1.Columns(4).Name = "កាលបរិច្ឆេត"
        DataGridView1.Columns(5).Name = "រូបបិយវត្ថុ"
        AddToGrid(DataGridView1, 6, "select ID,assetID ,amount ,unitprice ,dates,currency  from tblAssetAdd where assetID='" & TextBox1.Text & "' and BrID='" & frmMain.lblCode.Text & "' order by ID desc")
    End Sub
    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
End Class