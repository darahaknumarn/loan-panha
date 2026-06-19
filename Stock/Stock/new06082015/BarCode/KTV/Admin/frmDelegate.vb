Public Class frmDelegate

    Private Sub mnucheckErrors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnucheckErrors.Click
        Try
            Dim i As Integer
            Dim n As Integer
            'check if it is already the right date time
            n = 0
            For i = 0 To Me.dgsheet.Rows.Count - 1
                If Me.dgsheet.Rows(i).Cells(0).Value <> "" Then
                    n += 1
                End If
            Next
            For i = 0 To n - 1


            Next i

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA Solution")
        End Try

    End Sub

    Private Sub mnuimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuimport.Click
        If MessageBox.Show("Are you sure you want to import data now? We only import the right data and the highlighted colored won't be imported", "NiTA HR Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            If Me.dgsheet.CurrentRow.DefaultCellStyle.BackColor = Color.White Then
                importme()
            Else
                MessageBox.Show("You can't import as the timesheet is error", "NiTA Solution")
            End If

        End If
    End Sub

    Sub importme()
        Try

            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim sql As String = ""
            Me.p1.Visible = True
            Me.p1.Style = ProgressBarStyle.Marquee
            '''''''''''''''''''''''''''''''''''''''''''''''''''-----------------check exist name''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''-----
            If dgsheet.Columns.Count > 47 Then
                MsgBox("your database field is over!" & vbCrLf & "Please make sure that columns is only 47", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                Me.p1.Style = ProgressBarStyle.Blocks

                Exit Sub
            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''-----------------insert into  staffemergency''''''''''''''''''''''''''''''''''''''''''''''''''-----------
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            Me.p1.Style = ProgressBarStyle.Blocks
            Me.p1.Maximum = Val(dgsheet.Rows.Count)
            For i As Short = 0 To dgsheet.Rows.Count - 1
                If Me.dgsheet.Rows(i).DefaultCellStyle.BackColor <> Color.Yellow And Me.dgsheet.Rows(i).DefaultCellStyle.BackColor <> Color.Red Then
                    If frmMain.lblCode.Text = "" Then
                        MessageBox.Show("Can not blank your brand code, please try again.")
                    Else
                        com.Parameters.Clear()
                        sql = "Insert into tblcustom(customid,customname,customadd,BrID) values(@c2,@c3,@c4,@c5)"
                        com.CommandText = sql
                        With com.Parameters
                            .Add("@c2", SqlDbType.Int).Value = dgsheet.Rows(i).Cells(0).Value
                            .Add("@c3", SqlDbType.NVarChar).Value = dgsheet.Rows(i).Cells(1).Value
                            .Add("@c4", SqlDbType.NVarChar).Value = dgsheet.Rows(i).Cells(2).Value
                            .Add("@c5", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                        End With
                        com.ExecuteNonQuery()
                    End If
          
                End If
                Me.p1.Value = i
            Next

            com.Dispose()
            con.Close()
            con.Dispose()
            MessageBox.Show("Successfully import data in", "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Sub importme1()
        Try

            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim sql As String = ""
            Me.p1.Visible = True
            Me.p1.Style = ProgressBarStyle.Marquee
            '''''''''''''''''''''''''''''''''''''''''''''''''''-----------------check exist name''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''-----
            If dgsheet.Columns.Count > 47 Then
                MsgBox("your database field is over!" & vbCrLf & "Please make sure that columns is only 47", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
                Me.p1.Style = ProgressBarStyle.Blocks

                Exit Sub
            End If

            '''''''''''''''''''''''''''''''''''''''''''''''''''-----------------insert into  staffemergency''''''''''''''''''''''''''''''''''''''''''''''''''-----------
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            Me.p1.Style = ProgressBarStyle.Blocks
            Me.p1.Maximum = Val(dgsheet.Rows.Count)
            For i As Short = 0 To dgsheet.Rows.Count - 1
                If Me.dgsheet.Rows(i).DefaultCellStyle.BackColor <> Color.Yellow And Me.dgsheet.Rows(i).DefaultCellStyle.BackColor <> Color.Red Then
                    com.Parameters.Clear()
                    sql = "Insert into tblAsset(assetID,assetName,oumName,totalAmount,BrID) values(@c2,@c3,@c4,@c5,@c6)"
                    com.CommandText = sql
                    With com.Parameters
                        .Add("@c2", SqlDbType.NVarChar).Value = dgsheet.Rows(i).Cells(0).Value
                        .Add("@c3", SqlDbType.NVarChar).Value = dgsheet.Rows(i).Cells(1).Value
                        .Add("@c4", SqlDbType.NVarChar).Value = dgsheet.Rows(i).Cells(2).Value
                        .Add("@c5", SqlDbType.Float).Value = 0
                        .Add("@c6", SqlDbType.NVarChar).Value = frmMain.lblCode.Text
                    End With
                    com.ExecuteNonQuery()
                End If
                Me.p1.Value = i
            Next

            com.Dispose()
            con.Close()
            con.Dispose()
            MessageBox.Show("Successfully import data in", "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim i As Integer = 0
            Dim n As Integer = 0
            Me.dgsheet.Columns.Clear()
            If OFDFileBrowse.ShowDialog() = Windows.Forms.DialogResult.OK Then
                p = OFDFileBrowse.FileName
                GetSheetNames.ShowDialog()
            Else
                Exit Sub
            End If
            Dim ext As IO.FileInfo
            ext = My.Computer.FileSystem.GetFileInfo(p)
            If ext.Extension = ".xlsx" Then
                Dim MyConnection As System.Data.OleDb.OleDbConnection
                Dim DtSet As System.Data.DataSet
                Dim MyCommand As System.Data.OleDb.OleDbDataAdapter

                MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.ACE.OLEDB.12.0; Data Source='" & OFDFileBrowse.FileName & "';Extended Properties=Excel 8.0;")
                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & sheetnames & "]", MyConnection)
                MyCommand.TableMappings.Add("Table", "TestTable")
                DtSet = New System.Data.DataSet
                MyCommand.Fill(DtSet)
                Me.dgsheet.DataSource = DtSet.Tables(0)
                MyConnection.Close()

            ElseIf ext.Extension = ".xls" Then
                Dim MyConnection As System.Data.OleDb.OleDbConnection
                Dim DtSet As System.Data.DataSet
                Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
                MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" & OFDFileBrowse.FileName & "';Extended Properties=Excel 8.0;")
                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select * from [" & sheetnames & "]", MyConnection)
                MyCommand.TableMappings.Add("Table", "TestTable")
                DtSet = New System.Data.DataSet
                MyCommand.Fill(DtSet)
                Me.dgsheet.DataSource = DtSet.Tables(0)
                MyConnection.Close()

            End If
            For i = 0 To Me.dgsheet.Rows.Count - 2
                Me.dgsheet.Columns(1).DefaultCellStyle.Font = New Font("Limon S1", 18)
                If Me.dgsheet.Rows(i).Cells(0).Value.ToString.Equals(Nothing) And Me.dgsheet.Rows(i).Visible = True Then
                    Me.dgsheet.Rows.RemoveAt(i)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show("Please select file to import", "NiTA HR Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub frmDelegate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'p1.Visible = True
        'p1.ToolTipText.ToString()
        'Me.pListdelegate.SetBounds(0, 100, Me.Width, Me.Height - 100)
    End Sub

    Private Sub btncheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mnucheckErrors_Click(sender, e)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        'If frmMeeting.SelectedIndex = -1 Then
        '    MessageBox.Show("Plse")
        '    Exit Sub
        'End If
        If Me.Text = "Asset" Then
            If MessageBox.Show("Are you sure you want to import data now? We only import the right data and the highlighted colored won't be imported", "Monyroth Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'If Me.dgsheet.CurrentRow.DefaultCellStyle.BackColor = Color.White Then
                addIn("delete from tblAsset where BrID='" & frmMain.lblCode.Text & "'")
                importme1()
                'Else
                '    MessageBox.Show("You can't import as the timesheet is error", "NiTA Solution")
                'End If

            End If
        Else
            If MessageBox.Show("Are you sure you want to import data now? We only import the right data and the highlighted colored won't be imported", "Monyroth Solution", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                'If Me.dgsheet.CurrentRow.DefaultCellStyle.BackColor = Color.White Then
                importme()
                'Else
                '    MessageBox.Show("You can't import as the timesheet is error", "NiTA Solution")
                'End If

            End If
        End If
        
    End Sub

    
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class