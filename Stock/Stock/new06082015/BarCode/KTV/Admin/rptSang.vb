Public Class rptSang

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        txtStaffID.ReadOnly = False
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        txtStaffID.ReadOnly = True
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim date1 As Date = FormatDateTime(DateTimePicker1.Value, DateFormat.ShortDate)
        Dim date2 As Date = FormatDateTime(DateTimePicker2.Value, DateFormat.ShortDate)
        Dim result As Integer = DateTime.Compare(date1, date2)
        Dim BrID As String = Me.ComboBox1.Text
        '-----------------------------.te--------------------------------------------------------------------------
        If BrID = "" Then
            MessageBox.Show("Please input correct brand ID before search.")
        Else
            If RadioButton1.Checked Then
                If CheckBox1.Checked Then
                    If result > 0 Then
                        MessageBox.Show("You've selected date not correct, please check again before reload record!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    Else
                        DataGridView1.Columns.Clear()
                        DataGridView1.ColumnCount = 7
                        DataGridView1.Columns(0).Name = "កូដបុគ្គលិក"
                        DataGridView1.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
                        DataGridView1.Columns(2).Name = "លេខម៉ូតូ"
                        DataGridView1.Columns(3).Name = "ចំនួនលីត្រ"
                        DataGridView1.Columns(4).Name = "តំលៃសរុប"
                        DataGridView1.Columns(5).Name = "ចំនួនគីឡូម៉ែត្រ"
                        AddToGrid(DataGridView1, 6, "select b.staffid,b.StaffName ,b.MotoNo,sum(amount)amount,sum(amount*unitPrice) as Total,sum(a.Km )Km from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & BrID & "' group by b.StaffID ,b.StaffName ,b.MotoNo order by StaffID ")
                    End If
                Else
                    DataGridView1.Columns.Clear()
                    DataGridView1.ColumnCount = 7
                    DataGridView1.Columns(0).Name = "កូដបុគ្គលិក"
                    DataGridView1.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
                    DataGridView1.Columns(2).Name = "លេខម៉ូតូ"
                    DataGridView1.Columns(3).Name = "ចំនួនលីត្រ"
                    DataGridView1.Columns(4).Name = "តំលៃសរុប"
                    DataGridView1.Columns(5).Name = "ចំនួនគីឡូម៉ែត្រ"
                    AddToGrid(DataGridView1, 6, "select b.staffid,b.StaffName ,b.MotoNo,sum(amount)amount,sum(amount*unitPrice) as Total,sum(a.Km )Km from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.BrID='" & BrID & "' group by b.StaffID ,b.StaffName ,b.MotoNo order by StaffID ")
                End If
            Else
                If CheckBox1.Checked Then
                    If result > 0 Then
                        MessageBox.Show("You've selected date not correct, please check again before reload record!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return
                    Else
                        DataGridView1.Columns.Clear()
                        DataGridView1.ColumnCount = 7
                        DataGridView1.Columns(0).Name = "កូដបុគ្គលិក"
                        DataGridView1.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
                        DataGridView1.Columns(2).Name = "លេខម៉ូតូ"
                        DataGridView1.Columns(3).Name = "ចំនួនលីត្រ"
                        DataGridView1.Columns(4).Name = "តំលៃសរុប"
                        DataGridView1.Columns(5).Name = "ចំនួនគីឡូម៉ែត្រ"
                        AddToGrid(DataGridView1, 6, "select b.staffid,b.StaffName ,b.MotoNo,sum(amount)amount,sum(amount*unitPrice) as Total,sum(a.Km )Km from tblSang a left join tblStaff b on a.StaffID =b.StaffID and a.BrID=b.BrID where b.staffID='" & txtStaffID.Text & "' and a.Date between '" & date1 & "' and '" & date2 & "' and a.BrID='" & BrID & "'group by b.StaffID ,b.StaffName ,b.MotoNo order by StaffID ")
                    End If
                Else
                    DataGridView1.Columns.Clear()
                    DataGridView1.ColumnCount = 7
                    DataGridView1.Columns(0).Name = "កូដបុគ្គលិក"
                    DataGridView1.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
                    DataGridView1.Columns(2).Name = "លេខម៉ូតូ"
                    DataGridView1.Columns(3).Name = "ចំនួនលីត្រ"
                    DataGridView1.Columns(4).Name = "តំលៃសរុប"
                    DataGridView1.Columns(5).Name = "ចំនួនគីឡូម៉ែត្រ"
                    AddToGrid(DataGridView1, 6, "select b.staffid,b.StaffName ,b.MotoNo,sum(amount)amount,sum(amount*unitPrice) as Total,sum(a.Km )Km from tblSang a left join tblStaff b on a .StaffID =b.StaffID and a.BrID=b.BrID where a.BrID = '" & BrID & "' and b.staffID='" & txtStaffID.Text & "'  group by b.StaffID ,b.StaffName ,b.MotoNo order by StaffID ")
                End If
            End If

        End If
        'If RadioButton1.Checked And CheckBox1.Checked Then
        '    If result > 0 And txtInsert.Text = "All" Then
        '        MessageBox.Show("You've selected date not correct, please check again before reload record!", "IT Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        Return
        '    End If
        'Else
        '    DataGridView1.Columns.Clear()
        '    DataGridView1.ColumnCount = 7
        '    DataGridView1.Columns(0).Name = "កូដបុគ្គលិក"
        '    DataGridView1.Columns(1).Name = "ឈ្មោះបុគ្គលិក"
        '    DataGridView1.Columns(2).Name = "លេខម៉ូតូ"
        '    DataGridView1.Columns(3).Name = "ចំនួនលីត្រ"
        '    DataGridView1.Columns(4).Name = "តំលៃសរុប"
        '    DataGridView1.Columns(5).Name = "ចំនួនគីឡូម៉ែត្រ"
        '    AddToGrid(DataGridView1, 6, "select b.staffid,b.StaffName ,b.MotoNo,sum(amount)amount,sum(amount*unitPrice) as Total,sum(a.Km )Km from tblSang a left join tblStaff b on a .StaffID =b.StaffID group by b.StaffID ,b.StaffName ,b.MotoNo order by StaffID ")
        '    '            use(barcode)
        '    'select b.staffid,b.StaffName ,b.MotoNo
        '    ',sum(amount)amount,sum(unitprice)unitprice,sum(amount*unitPrice) as Total,sum(a.Km )Km
        '    'from tblSang a left join tblStaff b on a .StaffID =b.StaffID
        '    'group by b.StaffID ,b.StaffName ,b.MotoNo  
        '    'order by StaffID 
        'End If
    End Sub

    Private Sub rptSang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'BarCodeDataSet2.tblcompany1' table. You can move, or remove it, as needed.
        'Me.Tblcompany1TableAdapter.Fill(Me.BarCodeDataSet2.tblcompany1)
        SetFontDatagrid(DataGridView1)
    End Sub

    Private Sub txtStaffID_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtStaffID.TextChanged
        lblName.Text = getData("Select staffName from tblstaff where staffID=" & txtStaffID.Text)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Label1.Text = getData("Select Name from tblcompany1 where ID ='" & ComboBox1.SelectedValue & "'")
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ToExcel(DataGridView1)
    End Sub

    Private Function Tblcompany1TableAdapter() As Object
        Throw New NotImplementedException
    End Function

End Class