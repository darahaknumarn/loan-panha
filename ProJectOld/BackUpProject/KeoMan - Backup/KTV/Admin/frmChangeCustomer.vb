Imports System.Data.SqlClient

Public Class frmChangeCustomer
    Public Sub addCustomer()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = txtCM_ID.Text
                .Add("@d1", SqlDbType.NVarChar).Value = txtCM_Name.Text
                .Add("@d2", SqlDbType.Int).Value = txtLO_ID.Text
                .Add("@d3", SqlDbType.NVarChar).Value = txtPhone.Text
                .Add("@d4", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d5", SqlDbType.Int).Value = 1
                .Add("@d6", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d7", SqlDbType.DateTime).Value = DateTime.Now
                Dim a As String = getData("select max(ID)ID from BK_Customer where CM_BrId='" & frmMain.lblCode.Text & "'")
                If a = "" Then
                    .Add("@d8", SqlDbType.Int).Value = 1
                Else
                    .Add("@d8", SqlDbType.Int).Value = Val(a) + 1
                End If
                .Add("@d9", SqlDbType.NVarChar).Value = "Active"
            End With
            sql = "insert into BK_Customer(CM_ID,CM_KhName,LO_ID,CM_Phone,CM_BrId,CM_Rec_Status,CM_User_Create,CM_Date_Create,LD_Cycle,ID,Status) values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,1,@d8,@d9)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Public Sub addChange()
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            Dim LO_ID As Integer = Val(getData("select LO_ID from BK_Customer where CM_ID='" & txtCM_ID.Text & "' and CM_BrId='" & frmMain.lblCode.Text & "' and Status='Active'"))
            Dim id_old As Integer = Val(getData("select ID from BK_Customer where CM_ID='" & txtCM_ID.Text & "' and CM_BrId='" & frmMain.lblCode.Text & "' and Status='Active' "))
            Dim a As String = getData("select max(ID)ID from BK_Customer where CM_BrId='" & frmMain.lblCode.Text & "'")
            Dim id_new As Integer = Val(a) + 1
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = txtCM_ID.Text
                .Add("@d1", SqlDbType.NVarChar).Value = lblName.Text
                .Add("@d2", SqlDbType.NVarChar).Value = lblPhone.Text
                .Add("@d3", SqlDbType.Int).Value = LO_ID
                .Add("@d4", SqlDbType.NVarChar).Value = txtCM_Name.Text
                .Add("@d5", SqlDbType.NVarChar).Value = txtPhone.Text
                .Add("@d6", SqlDbType.NVarChar).Value = txtLO_ID.Text
                .Add("@d7", SqlDbType.Date).Value = FormatDateTime(DateTime.Now, DateFormat.ShortDate)
                .Add("@d8", SqlDbType.NVarChar).Value = frmMain.users
                .Add("@d9", SqlDbType.DateTime).Value = DateTime.Now
                .Add("@d10", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                .Add("@d11", SqlDbType.Int).Value = id_old
                .Add("@d12", SqlDbType.Int).Value = id_new
            End With
            sql = "insert BK_ChangeCustomer(CM_ID,Name_Old,Phone_Old,LO_Old,Name_New,Phone_New,LO_New,Date,User_Create,Date_Create,BrId,ID_Old,ID_New) values(@d0,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As System.Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub txtCM_ID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCM_ID.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.txtCM_ID.Text = "" Then
                Return
            Else
                Dim stastus As String = getData("Select LD_Status from BK_Loan where CM_ID='" & Me.txtCM_ID.Text & "' and LD_BrId='" & frmMain.lblCode.Text & "'")
                If stastus = "Active" Then
                    MessageBox.Show("This customer is still active so can't update, please check again.", "Still Active", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.txtCM_ID.Text = ""
                    Me.txtCM_Name.Text = ""
                    Me.txtLO_ID.Text = ""
                    Me.txtPhone.Text = ""
                    Return
                Else
                    Dim oDt As New System.Data.DataTable
                    Dim Str As String = "select a.CM_KhName,a.CM_Phone,b.VL_ID+','+b.CN_ID+','+b.DT_ID+','+b.PV_ID addres from BK_Customer a inner join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID where a.Status='Active' and a.CM_BrId='" & frmMain.lblCode.Text & "' and CM_ID='" & Me.txtCM_ID.Text & "'"
                    Dim str1 As String = getData("select a.CM_KhName,a.CM_Phone,b.VL_ID+','+b.CN_ID+','+b.DT_ID+','+b.PV_ID addres from BK_Customer a inner join BK_Location b on a.LO_ID=b.LO_ID and a.CM_BrId=b.LO_BrID where a.Status='Active' and a.CM_BrId='" & frmMain.lblCode.Text & "' and CM_ID='" & Me.txtCM_ID.Text & "'")
                    If str1 = "" Then
                        MessageBox.Show("No this customer ID in system.", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.txtCM_ID.Text = ""
                        Me.txtCM_Name.Text = ""
                        Me.txtLO_ID.Text = ""
                        Me.txtPhone.Text = ""
                        Return
                    End If
                    oDt.Clear()
                    oDa = New SqlDataAdapter(Str, g_cnn)
                    oDa.Fill(oDt)
                    Me.lblName.Text = oDt.Rows(0).Item(0).ToString
                    Me.lblPhone.Text = oDt.Rows(0).Item(1).ToString
                    Me.lblAddress.Text = oDt.Rows(0).Item(2).ToString
                    oDa.Dispose()
                    oDt.Dispose()
                    txtCM_Name.Focus()
                End If
            End If
        End If

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.txtCM_ID.Text = "" Or lblAddress.Text = "" Or lblName.Text = "" Or txtCM_Name.Text = "" Or txtLO_ID.Text = "" Or lblAddress1.Text = "" Or txtPhone.Text = "" Or lblAddress1.Text = "" Then
            resultError = frmMessageError.ShowBoxError("ទិន្នន័យមិនគ្រប់គ្រាន់ដើម្បីរក្សាទុកទេ។", "ពិនិត្យឡើងវិញ")
            Return
        Else
            addChange()
            addIn("Update BK_Customer set Status='Inactive' where ID=(Select top 1 ID from BK_Customer where CM_ID='" & txtCM_ID.Text & "' and CM_BrId='" & frmMain.lblCode.Text & "' and Status='Active')")
            addCustomer()
            MessageBox.Show("Record has been updated!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.txtCM_ID.Text = ""
            Me.txtCM_Name.Text = ""
            Me.txtLO_ID.Text = ""
            Me.txtPhone.Text = ""
            showChange()
        End If
    End Sub
    Private Sub showChange()
        AddToGrid(DataGridView1, 8, "select CM_ID,Name_Old,Phone_Old,LO_Old,Name_New,Phone_New,LO_New,Date from BK_ChangeCustomer where BrId='" & frmMain.lblCode.Text & "' and Date='" & FormatDateTime(DateTime.Now, DateFormat.ShortDate) & "' ")
    End Sub
    Private Sub txtCM_Name_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCM_Name.KeyDown

        If e.KeyCode = Keys.Enter Then
            If txtCM_Name.Text = "" Then
                Return
            Else
                txtPhone.Focus()
            End If
        End If
 
    End Sub
    Private Sub txtPhone_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPhone.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Me.txtPhone.Text = "" Then
                Return
            Else
                txtLO_ID.Focus()
            End If
        End If

    End Sub
    Private Sub txtLO_ID_KeyDown(sender As Object, e As KeyEventArgs) Handles txtLO_ID.KeyDown
        If e.KeyCode = Keys.Enter Then

            If txtLO_ID.Text = "" Then
                Return
            Else
                Dim oDt As New System.Data.DataTable
                Dim Str As String = "select b.VL_ID+','+b.CN_ID+','+b.DT_ID+','+b.PV_ID addres from  BK_Location b  where b.LO_ID='" & txtLO_ID.Text & "' and LO_BrID='" & frmMain.lblCode.Text & "'"
                'On Error Resume Next
                If Str = "" Then
                    Return
                End If
                oDt.Clear()
                oDa = New SqlDataAdapter(Str, g_cnn)
                oDa.Fill(oDt)
                Me.lblAddress1.Text = oDt.Rows(0).Item(0).ToString
                oDa.Dispose()
                oDt.Dispose()
            End If
        End If
      
    End Sub
    Private Sub frmChangeCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        showChange()
    End Sub

    Private Sub txtCM_ID_TextChanged(sender As Object, e As EventArgs) Handles txtCM_ID.TextChanged
        lblName.Text = ""
        lblPhone.Text = ""
        lblAddress.Text = ""
    End Sub
End Class