Public Class frmFirst

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Dim a As Integer = RectangleShape2.Width
        RectangleShape2.Width += 5
        If RectangleShape2.Width = 515 Then
            Timer1.Stop()
            frmsignin.Show()
            Me.Close()

        End If
        'MessageBox.Show(RectangleShape2.Width.ToString)
    End Sub
End Class