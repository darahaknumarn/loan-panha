

Partial Public Class Sales
    
    Partial Class vAttendantDataTable

        Private Sub vAttendantDataTable_ColumnChanging(ByVal sender As System.Object, ByVal e As System.Data.DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.faxColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class


End Class
