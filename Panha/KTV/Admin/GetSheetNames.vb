Imports Microsoft.Office
Imports System.Data.OleDb
Public Class GetSheetNames
    Dim ExcelConnectionStr As String
    Private Sub btngetname_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngetname.Click
        Try
            If ComboBox1.Text <> "" Then
                sheetnames = ComboBox1.Text
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub GetSheetNames_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim ext As IO.FileInfo
            ext = My.Computer.FileSystem.GetFileInfo(p)
            If ext.Extension = ".xls" Then
                ExcelConnectionStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & p & ";" & "Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
            ElseIf ext.Extension = ".xlsx" Then
                ExcelConnectionStr = "Provider=Microsoft.ace.OLEDB.12.0;Data Source=" & p & ";" & "Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
            End If
            Dim ExcelConnection As New OleDbConnection(ExcelConnectionStr)
            Dim ExcelCommand = New OleDbCommand
            ExcelCommand.Connection = ExcelConnection
            Dim ExcelAdapter As New OleDbDataAdapter(ExcelCommand)
            ExcelConnection.Open()
            Dim ExcelSheets As DataTable = ExcelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
            ComboBox1.Items.Clear()
            For i As Integer = 0 To ExcelSheets.Rows.Count - 1
                If ExcelSheets.Rows(i).Item("TABLE_NAME") <> "empf_1" Then
                    ComboBox1.Items.Add(ExcelSheets.Rows(i).Item("TABLE_NAME"))
                End If
            Next
            ExcelConnection.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
