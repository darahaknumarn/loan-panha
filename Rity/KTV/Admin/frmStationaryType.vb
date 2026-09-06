Imports System.Data.SqlClient

Public Class frmSType

    Private Sub frmSType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetFontDatagrid(DataGridView1)
        Me.DataGridView1.Columns("ID").Visible = False
        newRow()
        AddToGrid(Me.DataGridView1, 3, "Select ID,TypeID,TypeName from BK_StationaryType where BrID='" & frmMain.lblCode.Text & "'")
        If Me.DataGridView1.Rows.Count = 0 Then
            newRow()
        End If
    End Sub
    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim iRow = DataGridView1.CurrentCell.RowIndex
        Try
            If DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("TypeID").ColumnIndex Then
                Dim a As String = DataGridView1.CurrentCell.Value
                If a = "" Then
                    Return
                Else
                    Dim EM_Name As String = getData("select TypeName from BK_StationaryType where TypeID='" & Me.DataGridView1.Rows(iRow).Cells("TypeID").Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                    If EM_Name = "" Then
                        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("TypeName").ColumnIndex, iRow)
                    Else
                        ShowType()
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        If (e.KeyCode And Not Keys.Modifiers) = Keys.N AndAlso e.Modifiers = Keys.Control Then
            newRow()
        ElseIf e.KeyCode = Keys.F12 Then
            If Me.DataGridView1.Rows(iRow).Cells("ID").Value = "" Then
                If Me.DataGridView1.Rows(iRow).Cells("TypeID").Value = "" Or Me.DataGridView1.Rows(iRow).Cells("TypeName").Value = "" Then
                    MessageBox.Show("Not enough information to save, please check again.", "Try again", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                Else
                    addType()
                    ShowType()
                    newRow()
                End If
            Else

            End If
        End If
    End Sub

    '----------------------- Method and function
    Private Sub addType()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim sql As String
        Try
            Dim com As New SqlClient.SqlCommand
            'Dim con As New SqlClient.SqlConnection
            'con.ConnectionString = connectionString1
            'con.Open()
            com.Connection = g_cnn
            With com.Parameters
                .Add("@d0", SqlDbType.Int).Value = Me.DataGridView1.Rows(iRow).Cells("TypeID").Value
                .Add("@d1", SqlDbType.NVarChar).Value = Me.DataGridView1.Rows(iRow).Cells("TypeName").Value.ToString.Trim
            End With
            sql = "insert BK_StationaryType(TypeID,TypeName,BrID,User_Create,Date_Create) values (@d0,@d1,'" & frmMain.lblCode.Text & "','" & frmMain.users & "','" & DateTime.Now & "')"
            com.CommandText = sql
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            'con.Close()
            'con.Dispose()
        Catch ex As Exception
            resultError = frmMessageError.ShowBoxError(ex.Message, "ទាក់ទងមកផ្នែក IT")
        End Try
    End Sub
    Private Sub ShowType()
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim oDt As New System.Data.DataTable
        Dim Str As String = "select top 1 ID,TypeID,TypeName from BK_StationaryType where TypeID='" & Me.DataGridView1.Rows(iRow).Cells("TypeID").Value & "' and BrID='" & frmMain.lblCode.Text & "'"
        On Error Resume Next
        oDt.Clear()
        oDa = New SqlDataAdapter(Str, g_cnn)
        oDa.Fill(oDt)
        DataGridView1.Rows(iRow).Cells(0).Value = oDt.Rows(0).Item(0).ToString
        DataGridView1.Rows(iRow).Cells("TypeID").Value = oDt.Rows(0).Item(1).ToString
        DataGridView1.Rows(iRow).Cells("TypeName").Value = oDt.Rows(0).Item(2).ToString
        oDa.Dispose()
        oDt.Dispose()
        'Dim iRow As Integer = Me.DataGridView1.Rows.Count
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("TypeID").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("TypeID").ReadOnly = True
    End Sub
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("TypeID").ColumnIndex, iRow)
    End Sub


End Class