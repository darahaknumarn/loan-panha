
Imports System.Data.SqlClient

Public Class frmPrivileges
    Private Sub frmPrivileges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Combobox_Datasource(cboUser, "sys_User", "User_Name", "User_Name", "", g_cnn)
        cboUser.SelectedIndex = 0
        BindPrivilegeGrid(cboUser.SelectedValue)
    End Sub

    Private Sub BindPrivilegeGrid(logInUser As String)
        dgvPrivilege.DataSource = Nothing
        Dim AllRows As DataTable
        ' Main Menu
        Dim MainMenuTable As DataTable
        Dim MainMenuString As String = "SELECT SM.MenuID,SM.MenuName,SM.ParentName, MAX(CASE WHEN SUP.PrivilegeID=1 THEN 1 ELSE 0 END) vpermisson, MAX(CASE WHEN SUP.PrivilegeID=2 THEN 1 ELSE 0 END) apermisson,   MAX(CASE WHEN SUP.PrivilegeID=3 THEN 1 ELSE 0 END) upermisson,MAX(CASE WHEN SUP.PrivilegeID=4 THEN 1 ELSE 0 END) dpermisson,MAX(CASE WHEN SUP.PrivilegeID=5 THEN 1 ELSE 0 END) uaepermisson FROM sys_Menu SM     LEFT JOIN sys_UserPrivilege SUP  ON SM.MenuID = SUP.MenuID AND User_Name='" & logInUser & "'  WHERE SM.ParentName=0 GROUP BY SM.MenuID,SM.MenuName,SM.ParentName Order By MAX(SM.OrderIndex) "
        MainMenuTable = ExecuteDatatable(MainMenuString, g_cnn)
        AllRows = MainMenuTable.Clone()
        For Each DTR As DataRow In MainMenuTable.Rows
            AllRows.ImportRow(DTR)

            ' 1St Sub Menu
            Dim SubMenuTable As DataTable
            Dim SubMenuString As String = "SELECT SM.MenuID,SM.MenuName,SM.ParentName, MAX(CASE WHEN SUP.PrivilegeID=1 THEN 1 ELSE 0 END) vpermisson,  MAX(CASE WHEN SUP.PrivilegeID=2 THEN 1 ELSE 0 END) apermisson,MAX(CASE WHEN SUP.PrivilegeID=3 THEN 1 ELSE 0 END) upermisson, MAX(CASE WHEN SUP.PrivilegeID=4 THEN 1 ELSE 0 END) dpermisson,MAX(CASE WHEN SUP.PrivilegeID=5 THEN 1 ELSE 0 END) uaepermisson FROM sys_Menu SM  LEFT JOIN sys_UserPrivilege SUP  ON SM.MenuID = SUP.MenuID AND User_Name='" & logInUser & "' WHERE SM.ParentName= " & DTR.Item("MenuID") & " GROUP BY SM.MenuID,SM.MenuName,SM.ParentName Order By MAX(SM.OrderIndex)"
            SubMenuTable = ExecuteDatatable(SubMenuString, g_cnn)
            For Each DTRS As DataRow In SubMenuTable.Rows
                AllRows.ImportRow(DTRS)

                ' 2nd Sub Menu
                Dim Sub2MenuTable As DataTable
                Dim Sub2MenuString As String = "SELECT SM.MenuID,SM.MenuName,SM.ParentName,MAX(CASE WHEN SUP.PrivilegeID=1 THEN 1 ELSE 0 END) vpermisson, MAX(CASE WHEN SUP.PrivilegeID=2 THEN 1 ELSE 0 END) apermisson,MAX(CASE WHEN SUP.PrivilegeID=3 THEN 1 ELSE 0 END) upermisson,MAX(CASE WHEN SUP.PrivilegeID=4 THEN 1 ELSE 0 END) dpermisson,MAX(CASE WHEN SUP.PrivilegeID=5 THEN 1 ELSE 0 END) uaepermisson FROM sys_Menu SM   LEFT JOIN sys_UserPrivilege SUP  ON SM.MenuID = SUP.MenuID AND User_Name='" & logInUser & "' WHERE SM.ParentName= " & DTRS.Item("MenuID") & " GROUP BY SM.MenuID,SM.MenuName,SM.ParentName Order By MAX(SM.OrderIndex)"
                Sub2MenuTable = ExecuteDatatable(Sub2MenuString, g_cnn)
                For Each DTRS2 As DataRow In Sub2MenuTable.Rows
                    AllRows.ImportRow(DTRS2)
                Next
            Next
        Next
        MakeColumns(dgvPrivilege)
        dgvPrivilege.DataSource = AllRows
    End Sub

    Private Sub MakeColumns(objDGV As DataGridView)
        'objDGV.Columns.Add(MakeTColumn("No", "Nº", 0, True))
        objDGV.Columns.Add(MakeTColumn("MenuID", "MenuID", 0, False))
        objDGV.Columns.Add(MakeTColumn("MenuName", "Menu Name", 300, True))
        objDGV.Columns.Add(MakeCheckColumn("vpermisson", "View", 150, True))
        objDGV.Columns.Add(MakeCheckColumn("apermisson", "Insert", 150, True))
        objDGV.Columns.Add(MakeCheckColumn("upermisson", "Update", 150, True))
        objDGV.Columns.Add(MakeCheckColumn("dpermisson", "Delete", 150, True))
        objDGV.Columns.Add(MakeCheckColumn("uaepermisson", "Update & Delete after Export", 150, True))
        objDGV.Columns.Add(MakeTColumn("ParentName", "ParentName", 0, False))
    End Sub

    Private Sub dgvPrivilege_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvPrivilege.CellFormatting
        'Dim drv As DataRowView
        For i As Integer = 0 To Me.dgvPrivilege.Rows.Count - 1
            If Me.dgvPrivilege.Rows(i).Cells("ParentName").Value = 0 Then
                Me.dgvPrivilege.Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            End If
        Next
    End Sub

    Private Sub cboUser_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cboUser.SelectionChangeCommitted
        If cboUser.SelectedIndex > -1 Then BindPrivilegeGrid(cboUser.SelectedValue.ToString)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboUser.SelectedIndex > -1 Then
            '--------Start Validate Permission ------                 
            If Not CheckPermission(Me.Tag, uid, 2) Then 'Check if menu(FormTag is menuID) and user(loggedin user) and privilege (2=New,3=update,4=delete,5=after export)
                MessageBox.Show("អ្នកមិនមានសិទ្ធិកែប្រែសិទ្ធិអ្នកប្រើប្រាស់ទេ, សូមទាក់ទង IT", "Privileges")
                Return
            End If
            '--------End Validate--------------------
            Dim sqlTransac As SqlTransaction
            sqlTransac = g_cnn.BeginTransaction()
            Try
                ClearOldPrivileges(cboUser.SelectedValue, g_cnn, sqlTransac)
                SaveNewPrivileges(cboUser.SelectedValue, g_cnn, sqlTransac)
                sqlTransac.Commit()
                MessageBox.Show("User Privileges was saved successfull.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                sqlTransac.Rollback()
                MessageBox.Show("Cannot Save User Privileges", ex.Message)
            End Try
        End If
    End Sub

    Private Sub SaveNewPrivileges(selectedValue As String, sqlCon As SqlConnection, sqlTran As SqlTransaction)
        For Each dtgr As DataGridViewRow In dgvPrivilege.Rows
            If Not IsDBNull(dtgr.Cells("vpermisson").Value) Then If dtgr.Cells("vpermisson").Value = 1 Then InsertUserPrivilege(dtgr.Cells("MenuID").Value, cboUser.SelectedValue, 1, sqlCon, sqlTran)
            If Not IsDBNull(dtgr.Cells("apermisson").Value) Then If dtgr.Cells("apermisson").Value = 1 Then InsertUserPrivilege(dtgr.Cells("MenuID").Value, cboUser.SelectedValue, 2, sqlCon, sqlTran)
            If Not IsDBNull(dtgr.Cells("upermisson").Value) Then If dtgr.Cells("upermisson").Value = 1 Then InsertUserPrivilege(dtgr.Cells("MenuID").Value, cboUser.SelectedValue, 3, sqlCon, sqlTran)
            If Not IsDBNull(dtgr.Cells("dpermisson").Value) Then If dtgr.Cells("dpermisson").Value = 1 Then InsertUserPrivilege(dtgr.Cells("MenuID").Value, cboUser.SelectedValue, 4, sqlCon, sqlTran)
            If Not IsDBNull(dtgr.Cells("uaepermisson").Value) Then If dtgr.Cells("uaepermisson").Value = 1 Then InsertUserPrivilege(dtgr.Cells("MenuID").Value, cboUser.SelectedValue, 5, sqlCon, sqlTran)
        Next
    End Sub

    Private Sub InsertUserPrivilege(P_MenuID As Integer, Acct As String, pvl As Integer, sqlCon As SqlConnection, sqlTran As SqlTransaction)
        Dim com As SqlCommand = sqlCon.CreateCommand()
        com.Transaction = sqlTran
        com.CommandText = "INSERT INTO sys_UserPrivilege VALUES(@MID,@Acct,@Pvl)"
        com.Parameters.Add("@MID", SqlDbType.Int).Value = P_MenuID
        com.Parameters.Add("@Acct", SqlDbType.NVarChar).Value = Acct
        com.Parameters.Add("@Pvl", SqlDbType.Int).Value = pvl
        com.ExecuteNonQuery()
    End Sub

    Private Sub ClearOldPrivileges(selectedValue As String, sqlCon As SqlConnection, sqlTran As SqlTransaction)
        Dim com As SqlCommand = sqlCon.CreateCommand()
        com.Transaction = sqlTran
        com.CommandText = "DELETE FROM sys_UserPrivilege WHERE User_Name = '" & selectedValue & "'"
        com.ExecuteNonQuery()
    End Sub
End Class