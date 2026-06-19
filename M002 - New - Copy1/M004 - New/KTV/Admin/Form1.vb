Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.Windows.Forms
Public Class MyMessageBox
    Shared newMessageBox As MyMessageBox
    Public msgTimer As Timer
    Shared Button_id As String
    Private disposeFormTimer As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Shared Function ShowBox(ByVal txtMessage As String) As String
        newMessageBox = New MyMessageBox()
        newMessageBox.lblMessage.Text = txtMessage
        newMessageBox.ShowDialog()
        Return Button_id
    End Function

    Public Shared Function ShowBox(ByVal txtMessage As String, ByVal txtTitle As String) As String
        newMessageBox = New MyMessageBox()
        newMessageBox.lblTitle.Text = txtTitle
        newMessageBox.lblMessage.Text = txtMessage
        newMessageBox.ShowDialog()
        Return Button_id
    End Function

    Private Sub MyMessageBox_Paint(ByVal sender As Object, ByVal e As PaintEventArgs)
        Dim mGraphics As Graphics = e.Graphics
        Dim pen1 As New Pen(Color.FromArgb(96, 155, 173), 1)

        Dim Area1 As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Dim LGB As New LinearGradientBrush(Area1, Color.FromArgb(96, 155, 173), Color.FromArgb(245, 251, 251), LinearGradientMode.Vertical)
        mGraphics.FillRectangle(LGB, Area1)
        mGraphics.DrawRectangle(pen1, Area1)
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
        'newMessageBox.msgTimer.[Stop]()
        'newMessageBox.msgTimer.Dispose()
        Button_id = "1"
        newMessageBox.Dispose()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        'newMessageBox.msgTimer.[Stop]()
        'newMessageBox.msgTimer.Dispose()
        Button_id = "2"
        newMessageBox.Dispose()
    End Sub

    Private Sub timer_tick(ByVal sender As Object, ByVal e As EventArgs)
        disposeFormTimer -= 1

        If disposeFormTimer >= 0 Then
            newMessageBox.lblTimer.Text = disposeFormTimer.ToString()
        Else
            newMessageBox.msgTimer.[Stop]()
            newMessageBox.msgTimer.Dispose()
            newMessageBox.Dispose()
        End If
    End Sub

    Private Sub MyMessageBox_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnOK.Select()
        '= True
    End Sub
End Class