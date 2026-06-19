Imports Microsoft.Reporting.WinForms

Public Class frmRptResource

    Private Sub frmRptResource_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'MonyrothDS.vResource' table. You can move, or remove it, as needed.
        'Me.vResourceTableAdapter.Fill(Me.MonyrothDS.vResource)
        AddCombo(cmboId, "Select staffid from tblstaff")
        Me.ReportViewer1.RefreshReport()
        Me.ReportViewer2.RefreshReport()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
Again:
            Dim param(2) As ReportParameter
            Select Case Me.ComboBox1.SelectedIndex
                Case 0
                    Me.MonyrothDS.vResource.Clear()
                    Me.ReportViewer1.BringToFront()
                    If cmboId.Text = "All" Then
                        Dim dao2 As New SqlClient.SqlDataAdapter("select * from vresource where checking=1 and returndate between '" & FormatDateTime(Me.DateTimePicker1.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(Me.DateTimePicker2.Value, DateFormat.ShortDate) & "'", connectionString1)
                        param(0) = New ReportParameter("F", DateTimePicker1.Value)
                        param(1) = New ReportParameter("T", DateTimePicker2.Value)
                        ReportViewer1.LocalReport.SetParameters(param(0))
                        ReportViewer1.LocalReport.SetParameters(param(1))
                        dao2.Fill(Me.MonyrothDS.vResource)
                        Me.ReportViewer1.RefreshReport()
                        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                        Me.ReportViewer1.Show()
                    Else
                        Dim dao2 As New SqlClient.SqlDataAdapter("select * from vresource where checking=1 and staffid='" & Me.cmboId.Text & "' and returndate between '" & FormatDateTime(Me.DateTimePicker1.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(Me.DateTimePicker2.Value, DateFormat.ShortDate) & "'", connectionString1)
                        param(0) = New ReportParameter("F", DateTimePicker1.Value)
                        param(1) = New ReportParameter("T", DateTimePicker2.Value)
                        ReportViewer1.LocalReport.SetParameters(param(0))
                        ReportViewer1.LocalReport.SetParameters(param(1))
                        dao2.Fill(Me.MonyrothDS.vResource)
                        Me.ReportViewer1.RefreshReport()
                        Me.ReportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                        Me.ReportViewer1.Show()
                    End If
               
                Case 1
                    Me.MonyrothDS.vResource.Clear()
                    Me.ReportViewer2.BringToFront()
                    If cmboId.Text = "All" Then
                        Dim dao2 As New SqlClient.SqlDataAdapter("select * from vresource where checking=0 and borrowdate between '" & FormatDateTime(Me.DateTimePicker1.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(Me.DateTimePicker2.Value, DateFormat.ShortDate) & "'", connectionString1)
                        param(0) = New ReportParameter("F", DateTimePicker1.Value)
                        param(1) = New ReportParameter("T", DateTimePicker2.Value)
                        ReportViewer2.LocalReport.SetParameters(param(0))
                        ReportViewer2.LocalReport.SetParameters(param(1))
                        dao2.Fill(Me.MonyrothDS.vResource)
                        Me.ReportViewer2.RefreshReport()
                        Me.ReportViewer2.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                        Me.ReportViewer2.Show()
                    Else
                        Dim dao2 As New SqlClient.SqlDataAdapter("select * from vresource where checking=0 and staffid='" & Me.cmboId.Text & "' and borrowdate between '" & FormatDateTime(Me.DateTimePicker1.Value, DateFormat.ShortDate) & "' and '" & FormatDateTime(Me.DateTimePicker2.Value, DateFormat.ShortDate) & "'", connectionString1)
                        param(0) = New ReportParameter("F", DateTimePicker1.Value)
                        param(1) = New ReportParameter("T", DateTimePicker2.Value)
                        ReportViewer2.LocalReport.SetParameters(param(0))
                        ReportViewer2.LocalReport.SetParameters(param(1))
                        dao2.Fill(Me.MonyrothDS.vResource)
                        Me.ReportViewer2.RefreshReport()
                        Me.ReportViewer2.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                        Me.ReportViewer2.Show()
                    End If

            End Select
        Catch ex As Exception
            If Err.Number > 0 Then GoTo Again
            MessageBox.Show(Err.Description, "CamITSo Co., LTD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub cmboId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmboId.SelectedIndexChanged
        lblName.Text = getData("Select Staffname from tblstaff where staffid='" & Me.cmboId.Text & "'")
    End Sub
End Class