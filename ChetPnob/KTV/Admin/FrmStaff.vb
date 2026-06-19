Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Public Class FrmStaff
    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        If BtnNew.Text = "New" Then
            Me.txtEmID.Text = ""
            Me.txtEnglishName.Text = ""
            Me.txtEmID.ReadOnly = False
            BtnNew.Text = "Save"
            BtnEdit.Enabled = False
            BtnDelete.Enabled = False
            BtnExit.Text = "Cancel"
        ElseIf BtnNew.Text = "Save" Then
            Me.txtEmID.ReadOnly = True
            BtnNew.Text = "New"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "Exit"
            'If txtEmID.Text = "" Or txtEnglishName.Text = "" Or ComboBox1.Text = "" Then
            '    resultError = frmMessageError.ShowBoxError("សូមបំពេញលេខកូដ ឈ្មោះ និង តួនាទីបុគ្គលិក។", "មិនអាចបន្តទៅមុខ")
            '    ' MessageBox.Show("Please fill the Staff ID and Staff Name", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Return
            'Else
            '    'addstaff()
            '    'ShowStaff()
            'End If
        ElseIf BtnNew.Text = "Update" Then
            'updatestaff()
            'ShowStaff()
            BtnNew.Text = "New"
            BtnExit.Text = "Exit"
            BtnEdit.Text = "Edit"
            BtnDelete.Enabled = True
            BtnEdit.Enabled = True
        Else
            BtnNew.Text = "New"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "Exit"
        End If
    End Sub
    Sub updatestaff()
        If txtEnglishName.Text = "" Then
            resultError = frmMessageError.ShowBoxError("សូមបំពេញឈ្មោះអោយបានត្រឹមត្រូវមុននិងធ្វើការរក្សាទុក", "ពិនិត្យឡើងវិញ")
            Return
        End If
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = Me.txtEnglishName.Text
                .Add("@d2", SqlDbType.NVarChar).Value = txtPOB.Text
                '.Add("@d3", SqlDbType.NVarChar).Value = ComboBox1.Text
            End With
            sql = "update tblStaff set StaffName=@d1,motoNo=@d2,Position=@d3,motoNo2=@d4,motoNo3=@d5 where StaffID='" & txtEmID.Text & "' and BrID='" & frmMain.lblCode.Text & "'"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Sub addstaff()
        Dim id1 As String = getData("select staffid from tblStaff where staffID='" & txtEmID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
        If id1 = "" Then
            Dim ID As String = getData("Select top 1 ID from tblstaff where BrID='" & frmMain.lblCode.Text & "' order by ID desc")
            If ID = "" Then
                ID = "1"
            Else
                ID = Convert.ToDecimal(ID) + 1
            End If
            Dim sql As String
            Try
                Dim com As New SqlClient.SqlCommand
                com.Connection = g_cnn
                With com.Parameters
                    .Add("@d0", SqlDbType.NVarChar).Value = Me.txtEmID.Text
                    .Add("@d1", SqlDbType.NVarChar).Value = Me.txtEnglishName.Text
                    .Add("@d2", SqlDbType.NVarChar).Value = txtPOB.Text
                    .Add("@d3", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                    '.Add("@d4", SqlDbType.NVarChar).Value = ComboBox1.Text.Trim
                    .Add("@d5", SqlDbType.Int).Value = ID
                End With
                sql = "insert into tblstaff(id,staffid,staffname,motoNo,BrID,Position,motoNo2,motoNo3) values (@d5,@d0,@d1,@d2,@d3,@d4,@d6,@d7)"
                com.CommandText = sql
                com.ExecuteNonQuery()
                com.Parameters.Clear()
                com.Dispose()
            Catch ex As Exception
                resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
            End Try
        Else
            resultError = frmMessageError.ShowBoxError("មិនអាចបញ្ចូលបានទេព្រោះបុគ្គលិកនេះមានរួចហើយ", "មើលឡើងវិញ")
            Return
        End If
    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        If BtnExit.Text = "Cancel" Then
            Me.txtEmID.ReadOnly = True
            BtnNew.Text = "New"
            BtnEdit.Enabled = True
            BtnDelete.Enabled = True
            BtnExit.Text = "Exit"
        Else
            Me.Close()
        End If
    End Sub
    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        If BtnEdit.Text = "កែប្រែ" Then
            If Me.txtEmID.Text = "" Or Me.txtEnglishName.Text = "" Then
                resultError = frmMessageError.ShowBoxError("សូមជ្រើសរើសលេខកូដនឹងឈ្មោះបុគ្គលិកមុននឹងធ្វើការកែប្រែ", "ពិនិត្យម្តងទៀត")
            Else
                BtnEdit.Enabled = False
                BtnNew.Text = "រក្សាទុក"
                BtnDelete.Enabled = False
                BtnExit.Text = "បោះបង់"
                Me.txtEmID.ReadOnly = True
                Me.txtPOB.ReadOnly = False
            End If
        End If
    End Sub
    Sub ShowStaff()
        ShowDataGrid(DataGridView1, "Select * from BK_Employee")
    End Sub
    Private Sub FrmStaff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ShowStaff()
    End Sub
    Private Sub DataGridView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.Click
        Try
            If Me.DataGridView1.SelectedRows.Count > 0 Then
                Me.txtEmID.Text = Me.DataGridView1.SelectedRows(0).Cells(1).Value.ToString
                Me.txtEnglishName.Text = Me.DataGridView1.SelectedRows(0).Cells(2).Value
                txtPOB.Text = Me.DataGridView1.SelectedRows(0).Cells(3).Value.ToString
            End If
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        'MessageBox.Show(frmMain.lblCode.Text)
        If txtEmID.Text = "" Or txtEnglishName.Text = "" Then
            resultError = frmMessageError.ShowBoxError("មិនអាចលុបបានទេត្រូវជ្រើសរើសបុគ្គលិកជាមុនសិន", "លុបទិន្នន័យ")
        Else
            Dim a As String = getData("select StaffID from tblAssetDetail where StaffID='" & Me.txtEmID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
            Dim b As String = getData("select StaffID from tblResource where StaffID='" & Me.txtEmID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
            Dim c As String = getData("select StaffID from tblSang where StaffID='" & Me.txtEmID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
            If a <> "" Or b <> "" Or c <> "" Then
                resultError = frmMessageError.ShowBoxError("បុគ្គលិកម្នាក់នេះមិនអាចលុបបានទេព្រោះនៅមានប្រតិបត្តិការណ៏ក្នុងប្រព័ន្ធ!!! សូមពិនិត្យមើលម្តងទៀត!!!")
            Else
                result = MyMessageBox.ShowBox("តើអ្នកចង់លុបបុគ្គលិកលេខ'" & Me.txtEmID.Text & "'មែនទេ?", "លុបទិន្នន័យ")
                If result = "1" Then
                    addIn("delete from tblStaff where StaffID='" & txtEmID.Text & "' and BrID='" & frmMain.lblCode.Text & "'")
                End If
            End If
            ShowStaff()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowStaff()
    End Sub
    Private Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmChangeStaff.MdiParent = frmMain
        frmChangeStaff.WindowState = FormWindowState.Maximized
        frmChangeStaff.Show()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For Each Form In Me.MdiChildren
            Form.Close()
        Next
        frmPosition.MdiParent = frmMain
        frmPosition.WindowState = FormWindowState.Maximized
        frmPosition.Show()
    End Sub
    Private Sub txtMoto_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPOB.KeyDown
        If e.KeyCode = Keys.Space Then
            e.SuppressKeyPress = True
        End If
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        frmLocation.Text = "LocationOtherEmployee"
        frmLocation.Show()
    End Sub


End Class