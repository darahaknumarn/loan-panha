Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Text
Imports System.Windows.Forms
Public Class frmMessageError

    Shared newMessageBoxError As frmMessageError
    Public msgTimer As Timer
    Shared Button_id As String
    Private disposeFormTimer As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Shared Function ShowBoxError(ByVal txtMessage As String) As String
        newMessageBoxError = New frmMessageError()
        newMessageBoxError.lblMessage.Text = txtMessage
        newMessageBoxError.ShowDialog()
        Return Button_id
    End Function

    Public Shared Function ShowBoxError(ByVal txtMessage As String, ByVal txtTitle As String) As String
        newMessageBoxError = New frmMessageError()
        newMessageBoxError.lblTitle.Text = txtTitle
        newMessageBoxError.lblMessage.Text = txtMessage
        newMessageBoxError.ShowDialog()
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

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        'newMessageBox.msgTimer.[Stop]()
        'newMessageBox.msgTimer.Dispose()
        Button_id = "2"
        newMessageBoxError.Dispose()
    End Sub

    Private Sub timer_tick(ByVal sender As Object, ByVal e As EventArgs)
        disposeFormTimer -= 1

        If disposeFormTimer >= 0 Then
            newMessageBoxError.lblTimer.Text = disposeFormTimer.ToString()
        Else
            newMessageBoxError.msgTimer.[Stop]()
            newMessageBoxError.msgTimer.Dispose()
            newMessageBoxError.Dispose()
        End If
    End Sub
End Class