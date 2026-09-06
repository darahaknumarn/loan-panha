Imports System.Data.SqlClient

Public Class frmStationaryOut
    Private Sub frmStationaryOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DataGridView1.Columns("ID").Visible = False
        SetFontDatagrid(DataGridView1)
        newRow()
    End Sub
    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim iRow = Me.DataGridView1.CurrentCell.RowIndex
        Dim iCol = DataGridView1.CurrentCell.ColumnIndex
        'Dim iRow = DataGridView1.CurrentCell.RowIndex
        Dim staffName As String = ""
        If iCol = DataGridView1.Columns.Count - 1 Then
            If iRow < DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1(0, iRow + 1)
            End If
        Else
            If iRow < DataGridView1.Rows.Count - 1 Then
                SendKeys.Send("{up}")
            End If

            '-------------------------------------------------------- Cell EM ID
            If DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("EM_ID").ColumnIndex Then
                Dim a As String = DataGridView1.CurrentCell.Value
                If a = "" Then
                    Return
                Else
                    Dim EM_Name As String = getData("select top 1 EM_Name from BK_Employee where EM_ID='" & Me.DataGridView1.Rows(iRow).Cells("EM_ID").Value & "' and EM_BrID='" & frmMain.lblCode.Text & "'")
                    If EM_Name = "" Then
                        Me.DataGridView1.Rows(iRow).Cells("EM_ID").Value = ""
                        Me.DataGridView1.Rows(iRow).Cells("EM_Name").Value = ""
                        frmEmployee.Show()
                    Else
                        Me.DataGridView1.Rows(iRow).Cells("EM_Name").Value = EM_Name
                        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("SID").ColumnIndex, iRow)
                    End If
                End If
                'AutoSum()
                '-------------------------------------------------------- Cell DatePaid
            ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("SID").ColumnIndex Then
                Try
                    If Me.DataGridView1.CurrentCell.Value = "" Then
                        Return
                    Else
                        Dim SName As String = getData("select SName from BK_Stationary where SID='" & Me.DataGridView1.Rows(iRow).Cells("SID").Value & "' and BrID='" & frmMain.lblCode.Text & "'")
                        If SName = "" Then
                            Me.DataGridView1.Rows(iRow).Cells("SID").Value = ""
                            Me.DataGridView1.Rows(iRow).Cells("SName").Value = ""
                            frmStationary.Show()
                        Else
                            Me.DataGridView1.Rows(iRow).Cells("SName").Value = SName
                            DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("Date_Out").ColumnIndex, iRow)
                        End If
                    End If
                Catch ex As Exception
                    Return
                End Try
                '------------------------------------------------------------ Cell Description
            ElseIf DataGridView1.CurrentCell.ColumnIndex = Me.DataGridView1.Rows(iRow).Cells("Date_Out").ColumnIndex Then
               
            End If
        End If
    End Sub



    '--------------------------------- Method and function
    Private Sub newRow()
        DataGridView1.Rows.Add()
        Dim iRow As Integer = Me.DataGridView1.Rows.Count - 1
        DataGridView1.Rows(iRow).Cells(0).Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells(0).ReadOnly = True
        DataGridView1.Rows(iRow).Cells("EM_Name").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("EM_Name").ReadOnly = True
        DataGridView1.Rows(iRow).Cells("SName").Style.BackColor = Color.Yellow
        DataGridView1.Rows(iRow).Cells("SName").ReadOnly = True
        DataGridView1.CurrentCell = DataGridView1(Me.DataGridView1.Rows(iRow).Cells("EM_ID").ColumnIndex, iRow)
        'lblAutoSum.Text = 0
    End Sub
End Class