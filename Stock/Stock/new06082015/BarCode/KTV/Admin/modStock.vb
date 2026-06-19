Module modStock

    '------------cut stock when sales transaction
    Sub SubstractStock(ByVal PID As Integer, ByVal WID As Integer)
        addIn("Update tbl")
    End Sub
    '------------alert when stock less than threshold
    Function alertThreshold() As String


        Return ("Please do the replenishment for the")
    End Function
    '------------receipt goods
    Sub ReceiptGoods(ByVal POID As Integer, ByVal WID As Integer)
        '-----------------receipt goods base on the PO Number
        addIn("Update ")
    End Sub
    '------------update list price

    '-----------write off stock
    Sub WriteOffStock(ByVal PID As Integer, ByVal WID As Integer)
        addIn("Update")
    End Sub
    '-----------stock evaluation
    '-------------select price list
    Function priceList(ByVal pid As Integer) As Double
        Try
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            Dim price As Double
            price = 0
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = "Select PID,Costing,Selling from tblStockDiary where PID=" & pid
            dr = com.ExecuteReader
            If dr.Read = True Then
                price = Val(dr(2).ToString)
            End If
            con.Close()
            con.Dispose()
            Return price
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try

    End Function
    Function CurrentUnit(ByVal pid As Integer) As Integer
        Try
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            Dim Unit As Double
            Unit = 0
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = "Select PID,Unit from tblProductbyWarehouse where PID=" & pid
            dr = com.ExecuteReader
            If dr.Read = True Then
                Unit = Val(dr(1).ToString)
            End If
            con.Close()
            con.Dispose()
            Return Unit
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
    End Function
End Module
