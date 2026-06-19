Imports Microsoft.Reporting.WinForms

Public Class frmReport

    Private Property tblDelegateTableAdapter As Object

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub frmReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'BarCodeDataSet.tblRecording' table. You can move, or remove it, as needed.
        'Me.tblRecordingTableAdapter.Fill(Me.BarCodeDataSet.tblRecording)
        'TODO: This line of code loads data into the 'BarCodeDataSet.tblDelegate' table. You can move, or remove it, as needed.
        'Me.tblDelegateTableAdapter.Fill(Me.BarCodeDataSet.tblDelegate)

        Me.ReportViewer1.RefreshReport()
        Me.ReportViewer2.RefreshReport()
        Me.ReportViewer3.RefreshReport()
        Me.ReportViewer4.RefreshReport()
        Me.ReportViewer5.RefreshReport()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
Again:
        Try
            Dim param(4) As ReportParameter
            Select Case Me.ComboBox1.SelectedIndex
                Case 0

                    Me.BarCodeDataSet.tblDelegate.Clear()
                    Me.ReportViewer1.BringToFront()
                    Dim dao2 As New SqlClient.SqlDataAdapter("select * from tbldelegate", connectionString1)
                    dao2.Fill(Me.BarCodeDataSet.tblDelegate)
                    Me.ReportViewer1.RefreshReport()
                    Me.ReportViewer1.Show()
                Case 1
                    Me.BarCodeDataSet.vWinner.Clear()
                    Me.ReportViewer2.BringToFront()
                    Dim dao1 As New SqlClient.SqlDataAdapter("Select * from vwinner", connectionString1)
                    dao1.Fill(Me.BarCodeDataSet.tblRecording)
                    Me.ReportViewer2.RefreshReport()
                    Me.ReportViewer2.Show()
                Case 2
                    Me.BarCodeDataSet.tblDelegate.Clear()
                    Me.ReportViewer3.BringToFront()
                    Dim sql As String = "select * from tblDelegate where barCode not in (select distinct barCode from tblRecording) "
                    Dim dao1 As New SqlClient.SqlDataAdapter(sql, connectionString1)
                    dao1.Fill(Me.BarCodeDataSet.tblDelegate)
                    Me.ReportViewer3.RefreshReport()
                    Me.ReportViewer3.Show()
                Case 3
                    Me.BarCodeDataSet.tblRecording.Clear()
                    Me.ReportViewer4.BringToFront()
                    Dim dao1 As New SqlClient.SqlDataAdapter("Select * from tblrecording", connectionString1)
                    dao1.Fill(Me.BarCodeDataSet.tblRecording)
                    Me.ReportViewer4.RefreshReport()
                    Me.ReportViewer4.Show()
                    '    MReportViewer3e.rpt3.BringToFront()
                    '    Dim dao1 As New SqlClient.SqlDataAdapter("select * from vAttendant where cast(AttendantTimeDate as date)='" & dtp_reportDate.Value & "'", connectionString1)
                    '    dao1.Fill(Me.BarCodeDataSet.vAttendant)
                    '    Me.rpt3.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                    '    param(2) = New ReportParameter("SelectDate", dtp_reportDate.Value)
                    '    param(3) = New ReportParameter("SelectMeeting", cboMeeting.Text)
                    '    rpt3.LocalReport.SetParameters(param(2))
                    '    rpt3.LocalReport.SetParameters(param(3))
                    '    Me.rpt3.RefreshReport()
                    '    Me.rpt3.Show()

                    'Case 3

                    'Me.rpt4.BringToFront()
                    'Dim sql As String = "select * from tblDelegate where barCode not in (select distinct barCode from tblRecording "
                    'where("")
                    '    sql += " cast(AttendantTimeDate as date)='" & dtp_reportDate.Value & "' and SelectedMeeting=" & cboMeeting.SelectedValue & " )"
                    '    Dim dao1 As New SqlClient.SqlDataAdapter(sql, connectionString1)
                    '    dao1.Fill(Me.BarCodeDataSet.tblDelegate)
                    '    param(0) = New ReportParameter("AbsentDate", dtp_reportDate.Value)
                    '    param(1) = New ReportParameter("MeetingName", cboMeeting.Text)
                    '    rpt4.LocalReport.SetParameters(param(0))
                    '    rpt4.LocalReport.SetParameters(param(1))
                    '    Me.rpt4.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
                    '    Me.rpt4.RefreshReport()
                    '    Me.rpt4.Show()
                    Try
                        '        Dim datemeeting As String = Command()
                        '        dtp_reportDate.Text = datemeeting
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try


            End Select

        Catch ex As Exception
            If Err.Number > 0 Then GoTo Again
            MessageBox.Show(Err.Description, "CamITSo Co., LTD", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Private Function BarCodeDataSet() As Object
        Throw New NotImplementedException
    End Function

End Class