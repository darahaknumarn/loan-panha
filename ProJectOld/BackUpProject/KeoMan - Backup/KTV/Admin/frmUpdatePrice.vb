Public Class frmUpdatePrice

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim date1 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
        Dim result As Integer = DateTime.Compare(date2, date1)
        If TextBox1.Text = "" Then
            result = frmMessageError.ShowBoxError("បញ្ចូលតំលៃជាមុនសិន។", "គ្មានតំលៃ")
            Return
        Else
            If result > 0 Then
                If TextBox1.Text > 100 Then
                    addIn("update tblSang set unitPrice=0,PriceUSD=0 where Date between '" & date1 & "' and '" & date2 & "' and BrID='" & frmMain.lblCode.Text & "'")
                    addIn("update tblSang set unitPrice = '" & TextBox1.Text & "' where Date between '" & date1 & "' and '" & date2 & "' and BrID='" & frmMain.lblCode.Text & "'")
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យបានធ្វើការកែប្រែរួចរាល់។", "កែប្រែ")
                ElseIf TextBox1.Text = 0 Then
                    resultError = frmMessageError.ShowBoxError("តម្លៃសាំងមិនអាចស្មើរនឹងសូន្យបានទេ។", "កែប្រែ")
                    Return
                Else
                    addIn("update tblSang set unitPrice=0,PriceUSD=0 where Date between '" & date1 & "' and '" & date2 & "' and BrID='" & frmMain.lblCode.Text & "'")
                    addIn("update tblSang set PriceUSD = '" & TextBox1.Text & "' where Date between '" & date1 & "' and '" & date2 & "' and BrID='" & frmMain.lblCode.Text & "'")
                    resultError = frmMessageError.ShowBoxError("ទិន្នន័យបានធ្វើការកែប្រែរួចរាល់។", "កែប្រែ")
                End If
            Else
                result = frmMessageError.ShowBoxError("ជ្រើសរើសថ្ងៃខែខុស។", "ថ្ងៃខែខុស")
                Return
            End If
        End If
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class