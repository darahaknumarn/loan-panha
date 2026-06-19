Public Class frmPosition
    Dim Pcode As Integer
    Private Sub B4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub DataGridView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                txtNo.Text = Val(Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString)
                txtPosition.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub

    Sub addPosition()
        Dim BrID As String = frmMain.lblCode.Text
        Dim pid As String = getData("select top 1 PID from tblPosition order by PID desc")
        If pid = "" Or pid = "Null" Then
            pid = 1
        Else
            pid = Convert.ToDecimal(pid) + 1
        End If

        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = pid
                .Add("@d2", SqlDbType.NVarChar).Value = Me.txtPosition.Text.Trim
                .Add("@d3", SqlDbType.NVarChar).Value = BrID
            End With
            sql = "Insert into tblposition values(@d1,@d2,@d3)"
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
    Sub UpdatePosition()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = Me.txtPosition.Text
            End With
            sql = "Update tblposition set Position=@d1 where PID='" & txtNo.Text & "' and BrID='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try

    End Sub
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "បញ្ចូលថ្មី" Then
            BtnNew.Text = "រក្សាទុកថ្មី"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "បោះបង់"
            'txtPID.ReadOnly = False
        ElseIf BtnNew.Text = "រក្សាទុកថ្មី" Then
            BtnNew.Text = "បញ្ចូលថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            addPosition()
            showPosition()
        Else
            BtnNew.Text = "បញ្ចូលថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
            UpdatePosition()
            showPosition()
        End If
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Try
            If BtnEdit.Text = "កែប្រែ" Then
                If txtNo.Text = "" Or txtPosition.Text = "" Then
                    resultError = frmMessageError.ShowBoxError("សូមធ្វើការជ្រើសរើសទិន្នន័យជាមុនសិនមុននិងធ្វើកែប្រែ", "កែប្រែទិន្នន័យ")
                    'MessageBox.Show("Please select item to edit first, try again!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                Else
                    BtnNew.Text = "រក្សាទុក"
                    BtnDelete.Enabled = False
                    BtnEdit.Enabled = False
                    BtnExit.Text = "បោះបង់"
                End If
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Sub showPosition()
        AddToGrid(DataGridView1, 2, "Select PID,Position from tblPosition where BrID='" & frmMain.lblCode.Text & "'")
    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        '-------------------------------------
        'AddCombo(FrmStaff.ComboBox1, "select Position from tblPosition where BrID='" & frmMain.lblCode.Text & "'")
        'FrmStaff.ComboBox1.SelectedIndex = 0
        '-------------------------------------
        If BtnExit.Text = "បោះបង់" Then
            BtnNew.Text = "បញ្ចូលថ្មី"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "ចាកចេញ"
        Else
            Me.Close()
        End If
    End Sub

    Private Sub frmPosition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        showPosition()
    End Sub

    Private Sub DataGridView1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                txtNo.Text = Val(Me.DataGridView1.SelectedRows(0).Cells(0).Value.ToString)
                txtPosition.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            If txtNo.Text = "" Then
                resultError = frmMessageError.ShowBoxError("គ្មានអ្វីត្រូវលុប សូមជ្រើសរើសជាមុនសិន។", "ជ្រើសរើស")
            Else
                result = MyMessageBox.ShowBox("តើអ្នកចង់លុបទិន្នន័យមែនទេ?", "លុបទិន្នន័យ")
                If result = "1" Then
                    addIn("delete from tblPosition where PID='" & txtNo.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យត្រូវបានលុប។", "លុប")
                    showPosition()
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(Err.Description, "IT Solution")
        End Try

    End Sub
End Class