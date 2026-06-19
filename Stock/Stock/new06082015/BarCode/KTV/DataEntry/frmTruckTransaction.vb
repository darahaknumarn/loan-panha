Public Class frmTruckTransaction

    Private Sub frmTruckTransaction_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Me.btnmove.Image = KTV.My.Resources.collapse
        btnmove_Click(sender, e)
        ShowCategory(Me.lstCatExpense)
        ShowCategory(Me.lstCatIncome)
    End Sub

    Private Sub btnmove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnmove.Click
        If Me.btnmove.Tag = "R" Then
            Me.pExpense.Size = New System.Drawing.Size(0, Me.pExpense.Height)
            Me.pIncome.Size = New System.Drawing.Size(Me.Width - 40, Me.pIncome.Height)
            Me.btnmove.Tag = "L"
            Me.btnmove.Image = KTV.My.Resources.collapse
        Else
            Me.btnmove.Tag = "R"
            Me.btnmove.Image = KTV.My.Resources.expend
            Me.pIncome.Size = New System.Drawing.Size(0, Me.pIncome.Height)

        End If
    End Sub

    Private Sub B4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B4.Click
        Me.Close()
    End Sub

    Private Sub B1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B1.Click

        If Me.B1.Text.Equals("បញ្ជូលថ្មី") Then
            Me.B1.Text = "ថតទុក"
            Me.B2.Enabled = False
            Me.B3.Text = "ឈប់វិញ"
            Me.B4.Enabled = False

        ElseIf Me.B1.Text.Equals("ថតទុក") Then
            Me.B1.Text = "បញ្ជូលថ្មី"
            Me.B2.Text = "កែរប្រែ"
            Me.B3.Text = "លុបចោល"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            '------------------------Add tracking
            '-------------add income or expense base on the item exist ?
            If Me.gsales.Rows.Count > 1 Then
                'add income
            End If
            If Me.gexpense.Rows.Count > 1 Then
                'add expense
            End If
        ElseIf Me.B1.Text.Equals("ថតការកែរប្រែ") Then
            Me.B1.Text = "បញ្ជូលថ្មី"
            Me.B2.Text = "កែរប្រែ"
            Me.B3.Text = "លុបចោល"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            '-------------------------Update tracking
            If Me.gsales.Rows.Count > 1 Then
                'add income
            End If
            If Me.gexpense.Rows.Count > 1 Then
                'add expense
            End If

        End If
    End Sub

    Private Sub B2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B2.Click
        Me.B1.Text = "ថតការកែរប្រែ"
        Me.B2.Enabled = False
        Me.B3.Text = "ឈប់វិញ"
        Me.B4.Enabled = False
    End Sub

    Private Sub B3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B3.Click
        If Me.B3.Text.Equals("ឈប់វិញ") Then
            Me.B2.Enabled = True
            Me.B2.Text = "កែរប្រែ"
            Me.B4.Enabled = True
            Me.B1.Text = "បញ្ជូលថ្មី"
            Me.B3.Text = "លុបចោល"
        End If
    End Sub

    Private Sub Bprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bprint.Click
        '----------------------- print the report
    End Sub

    Sub ShowCategory(ByVal lst As ListView)
        Try
            Dim con As New System.Data.Odbc.OdbcConnection
            Dim com As New System.Data.Odbc.OdbcCommand
            Dim dr As System.Data.Odbc.OdbcDataReader
            con.ConnectionString = ConnectionString
            con.Open()
            com.Connection = con
            com.CommandText = "select CatID,CatName,Photo from tblcategory"
            lst.Items.Clear()
            dr = com.ExecuteReader
            While dr.Read
                Dim li As ListViewItem = lst.Items.Add(dr(1).ToString)
                li.Tag = dr(0).ToString
                'li.ToolTipText = dr(2).ToString
                Select Case Val(dr(0).ToString)
                    Case 1
                        li.StateImageIndex = 3
                    Case 2
                        li.StateImageIndex = 4
                    Case 3
                        li.StateImageIndex = 5
                    Case Else
                        li.StateImageIndex = 5
                End Select
            End While
            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub lstCatIncome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCatIncome.Click
        Try
            ShowCategory(Val(Me.lstCatIncome.SelectedItems(0).Tag), Me.lstproductsIncome)

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try

    End Sub
    Private Sub showCategory(ByVal Cat As Integer, ByVal lst As ListView)

        Try
            Dim con As New System.Data.Odbc.OdbcConnection
            Dim com As New System.Data.Odbc.OdbcCommand
            Dim dr As System.Data.Odbc.OdbcDataReader
            con.ConnectionString = ConnectionString
            con.Open()
            com.Connection = con
            com.CommandText = "Select PName,PID,barcode from tblproducts where CatID=" & Cat
            lst.Items.Clear()
            dr = com.ExecuteReader
            While dr.Read
                Dim li As ListViewItem = lst.Items.Add(dr(0).ToString)
                li.Tag = dr(1).ToString
                li.ToolTipText = dr(2).ToString
                Select Case Cat
                    Case 1
                        li.StateImageIndex = 3
                    Case 2
                        li.StateImageIndex = 4
                    Case 3
                        li.StateImageIndex = 5
                    Case Else
                        li.StateImageIndex = 5
                End Select
            End While
            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub lstCatExpense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCatExpense.Click
        Try
            ShowCategory(Val(Me.lstCatExpense.SelectedItems(0).Tag), lstproductExpense)

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try

    End Sub

    Private Sub lstproductsIncome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstproductsIncome.Click
        Try
            'Dim i As Integer
            'Dim PID As Integer
            'Dim price As Double
            'Dim Unit As Integer
            'Dim Punit As Integer
            'Dim j As Integer
            'i = Val(Microsoft.VisualBasic.Strings.Split(Me.txtTicket.Text, "-").ToList.Item(0))
            'PID = Val(Me.lstproducts.SelectedItems(0).Tag)
            'If isExist("Select * from tblSODetail where SOID=" & i & " and PID=" & PID) = False Then
            '    '-----------get the price first
            '    price = priceList(PID)
            '    Unit = CurrentUnit(PID)
            '    Me.gsales.Rows.Add()
            '    Me.gsales.Rows(gsales.NewRowIndex - 1).Cells(0).Value = PID
            '    Me.gsales.Rows(gsales.NewRowIndex - 1).Cells(1).Value = Me.lstproducts.SelectedItems(0).Text
            '    Me.gsales.Rows(gsales.NewRowIndex - 1).Cells(2).Value = price ' get the selling price from the price list---------20
            '    Me.gsales.Rows(gsales.NewRowIndex - 1).Cells(3).Value = IIf(Trim(Me.txtinput.Text).Equals("") = True, 1, Val(Me.txtinput.Text))
            '    Me.gsales.Rows(gsales.NewRowIndex - 1).Cells(4).Value = 0 'IIf(Trim(Me.txtinput.Text).Equals("") = True, 1, Val(Me.txtinput.Text))
            '    Me.gsales.Rows(gsales.NewRowIndex - 1).Cells(5).Value = price * Val(Me.txtinput.Text)
            '    Punit = Val(Me.txtinput.Text)
            '    'add to the sales detail
            '    'get the id

            '    If Me.txtTicket.Text <> "" Then
            '        addIn("insert into tblSODetail(SOID,PID,Unit,price,tax,[status]) values(" & i & "," & PID & "," & Punit & ", " & price & " ,0,'Active')")
            '        addIn("Update tblProductbyWarehouse set Unit=" & (Unit - Punit) & " where PID=" & PID)
            '    End If
            'Else
            '    If Me.txtTicket.Text <> "" Then
            '        '-------------find the duplicate
            '        For j = 0 To Me.gsales.Rows.Count - 2
            '            If Val(gsales.Rows(j).Cells(0).Value) = PID Then
            '                Exit For
            '            End If
            '        Next
            '        Unit = Val(Me.gsales.Rows(j).Cells(3).Value)
            '        addIn("Update tblSODetail set Unit=" & Unit + 1 & " Where SOID=" & i & " and PID=" & PID)
            '        Me.gsales.Rows(j).Cells(3).Value = Unit + 1
            '        Me.gsales.Rows(j).Cells(5).Value = (Unit + 1) * Val(Me.gsales.Rows(j).Cells(2).Value)
            '        addIn("Update tblProductbyWarehouse set Unit=(Unit- " & 1 & ") where PID=" & PID)
            '    End If
            'End If
            'Me.txttotal.Text = getTotal()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
    End Sub
End Class