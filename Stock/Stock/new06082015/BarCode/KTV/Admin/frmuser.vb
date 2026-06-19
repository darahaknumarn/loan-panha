Imports System.Reflection
Imports Microsoft.Office.Interop
Public Class frmuser
    Dim EID As String
    Dim password As String = "123"
    Dim sql As String
    Dim ID As Integer
    Private Sub B4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B4.Click
        Me.Close()
    End Sub

    Private Sub B1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles B1.Click
        If Me.B1.Text.Equals("បញ្ជូលថ្មី") Then
            Me.B1.Text = "ថតទុក"
            Me.B2.Enabled = False
            Me.B3.Text = "ឈប់វិញ"
            Me.B4.Enabled = False
            EID = "E" & Microsoft.VisualBasic.Format(Val(Microsoft.VisualBasic.Right(getLastRow(), 2)) + 1, "00")
        ElseIf Me.B1.Text.Equals("ថតទុក") Then
            Me.B1.Text = "បញ្ជូលថ្មី"
            Me.B2.Text = "កែរប្រែ"
            Me.B3.Text = "លុបចោល"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            '------------------------Add User
            AddUser()
            ShowUser()
        ElseIf Me.B1.Text.Equals("ថតការកែរប្រែ") Then
            Me.B1.Text = "បញ្ជូលថ្មី"
            Me.B2.Text = "កែរប្រែ"
            Me.B3.Text = "លុបចោល"
            Me.B4.Enabled = True
            Me.B2.Enabled = True
            '-------------------------Update user
            UpdateUser(EID)
            ShowUser()
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
        ElseIf Me.B3.Text = "លុបចោល" Then
            If MessageBox.Show("Are you sure you want to delete this user account?" & Microsoft.VisualBasic.Chr(13) & "តើអ្នកពិតជាចង់លុបមែនទេ?", "NiTA Solution", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                addIn("Delete from tbluser where EmployeeID ='" & EID & "'")
                ShowUser()
            End If
        End If
    End Sub
    Sub UpdateUser(ByVal EID As String)
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            Dim imageData As Byte()
            Dim sFileName As String
            Dim rid As String
            sFileName = System.IO.Path.GetFileName(Me.imgphoto.ImageLocation)
            If sFileName = "" Then
                CamITSo.My.Resources.lifestyle.Save(Application.ExecutablePath & "909009009.jpg")
                sFileName = Application.ExecutablePath & "909009009.jpg"
                Me.imgphoto.ImageLocation = sFileName
            End If

            imageData = ReadFile(Me.imgphoto.ImageLocation)
            rid = Val(Me.cborole.SelectedValue)
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = EID
                .Add("@d2", SqlDbType.NVarChar).Value = Me.txtuser.Text
                .Add("@d3", SqlDbType.Int).Value = Val(IIf(rid = "", 1, rid))
                .AddWithValue("@d4", DirectCast(imageData, Object))
                .Add("@d5", SqlDbType.NVarChar).Value = password
                .Add("@d6", SqlDbType.NVarChar).Value = "1"

            End With
            com.CommandText = "Update tblUser set Employee=@d2,RID=@d3,Photo=@d4,password=@d5,Status=@d6 where EmployeeID=@d1"
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try

    End Sub

    Sub AddUser()
        Try
            Dim com As New SqlClient.SqlCommand
            Dim con As New SqlClient.SqlConnection
            Dim imageData As Byte()
            Dim sFileName As String
            Dim rid As String
            sFileName = System.IO.Path.GetFileName(Me.imgphoto.ImageLocation)
            If sFileName = "" Then
                CamITSo.My.Resources.lifestyle.Save(Application.ExecutablePath & "909009009.jpg")
                sFileName = Application.ExecutablePath & "909009009.jpg"
                Me.imgphoto.ImageLocation = sFileName
            End If
            rid = Val(Me.cborole.SelectedValue)
            imageData = ReadFile(Me.imgphoto.ImageLocation)
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            With com.Parameters
                .Add("@d1", SqlDbType.NVarChar).Value = EID
                .Add("@d2", SqlDbType.NVarChar).Value = Me.txtuser.Text
                .Add("@d3", SqlDbType.Int).Value = IIf(rid = "", 1, Val(rid))
                .AddWithValue("@d4", DirectCast(imageData, Object))
                .Add("@d5", SqlDbType.NVarChar).Value = password
                .Add("@d6", SqlDbType.NVarChar).Value = "1"

            End With
            com.CommandText = "insert into tblUser(EmployeeID,Employee,RID,Photo,password,Status) values(@d1,@d2,@d3,@d4,@d5,@d6)"
            com.ExecuteNonQuery()
            com.Parameters.Clear()
            com.Dispose()
            con.Close()
            con.Dispose()

        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try

    End Sub
    Private Sub frmuser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Dim ado As New SqlClient.SqlDataAdapter("Select * from tblrole where status=1", connectionString1)
            ado.Fill(Me.Sales1.tblRole)
            ShowUser()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "CamITSo Co.,LTD")
        End Try
        
    End Sub

    Private Sub ShowUser()

        Try
            Dim i As Integer
            Dim con As New SqlClient.SqlConnection
            Dim com As New SqlClient.SqlCommand
            Dim dr As SqlClient.SqlDataReader
            con.ConnectionString = connectionString1
            con.Open()
            com.Connection = con
            com.CommandText = "select U.Employee,R.roles,U.Photo,U.EmployeeID,U.UID from tblUser U inner join tblRole R on U.RID=R.rid where U.Status=1"
            Me.lstuser.Items.Clear()
            Me.myimg.Images.Clear()
            dr = com.ExecuteReader
            i = 0

            While dr.Read
                myimg.Images.Add(i, toImage(dr(2)))
                Dim li As ListViewItem = Me.lstuser.Items.Add("", dr(0).ToString, i)
                li.StateImageIndex = i
                li.SubItems.Add(dr(1).ToString)
                li.Tag = dr(3).ToString
                li.ToolTipText = dr(1).ToString

                i += 1
            End While

            con.Close()
            con.Dispose()
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub btnimg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimg.Click
        ofd.ShowDialog()
        ofd.Filter = "JPEG File (*.jpeg)|*.jpeg|JPG File (*.jpg)|*.jpg|All files (*.*)|*.*"
        Me.imgphoto.ImageLocation = ofd.FileName.ToString
    End Sub

    'Private Sub lstuser_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstuser.MouseClick
    '    Try
    '        If Me.lstuser.SelectedItems.Count > 0 Then
    '            ID = Val(Me.lstuser.SelectedItems(0).ToolTipText)
    '            EID = Me.lstuser.SelectedItems(0).Tag
    '            Me.imgphoto.Image = Me.IMG.Images(Me.lstuser.SelectedItems(0).StateImageIndex)
    '            Me.txtuser.Text = Me.lstuser.SelectedItems(0).Text
    '            Me.cborole.Text = Me.lstuser.SelectedItems(0).ToolTipText
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(Err.Description, "NiTA POS Solution")
    '    End Try

    'End Sub
    'Sub showUsers(ByVal EID As String)
    '    Try
    '        Dim con As New SqlClient.SqlConnection
    '        Dim dr As SqlClient.SqlDataReader
    '        Dim com As New SqlClient.SqlCommand
    '        con.ConnectionString = connectionString1
    '        con.Open()
    '        com.Connection = con
    '        com.CommandText = "select * from tblUser where EmployeeID='" & EID & "'"
    '        dr = com.ExecuteReader
    '        If dr.Read = True Then
    '            Me.txtAddress.Text=dr("
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(Err.Description, "NiTA Solution")
    '    End Try
    'End Sub


    Private Sub btnemail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnemail.Click
        'Dim oApp As Outlook.Application = New Outlook.Application()

        '' Get NameSpace and Logon.
        'Dim oNS As Outlook.NameSpace = oApp.GetNamespace("mapi")
        'oNS.Logon("Outlook", Missing.Value, False, True) ' TODO:

        '' Get the first contact from the Contacts folder.
        'Dim cContacts As Outlook.MAPIFolder = oNS.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderContacts)
        'Dim oItems As Outlook.Items = cContacts.Items

        'Dim oCt As Outlook.ContactItem

        Try

            'oCt = oItems.GetFirst()


            ' Display some common properties.
            'Console.WriteLine(oCt.FullName)
            'Console.WriteLine(oCt.Title)
            'Console.WriteLine(oCt.Birthday)
            'Console.WriteLine(oCt.CompanyName)
            'Console.WriteLine(oCt.Department)
            'Console.WriteLine(oCt.Body)
            'Console.WriteLine(oCt.FileAs)
            'Console.WriteLine(oCt.Email1Address)
            'Console.WriteLine(oCt.BusinessHomePage)
            'Console.WriteLine(oCt.MailingAddress)
            'Console.WriteLine(oCt.BusinessAddress)
            'Console.WriteLine(oCt.OfficeLocation)
            'Console.WriteLine(oCt.Subject)
            'Console.WriteLine(oCt.JobTitle)

        Catch

            Console.WriteLine("an error occurred")

        Finally

            ' Display
            'oCt.Display(True)

            ' Log off.
            'oNS.Logoff()

            '' Clean up.
            'oApp = Nothing
            'oNS = Nothing
            'oItems = Nothing
            'oCt = Nothing

        End Try




    End Sub

    Private Sub lstuser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstuser.Click
        Try
            If Me.lstuser.SelectedItems.Count > 0 Then
                ID = Val(Me.lstuser.SelectedItems(0).ToolTipText)
                EID = Me.lstuser.SelectedItems(0).Tag
                Me.imgphoto.Image = Me.IMG.Images(Me.lstuser.SelectedItems(0).StateImageIndex)
                Me.txtuser.Text = Me.lstuser.SelectedItems(0).Text
                Me.cborole.Text = Me.lstuser.SelectedItems(0).ToolTipText
            End If
        Catch ex As Exception
            MessageBox.Show(Err.Description, "NiTA POS Solution")
        End Try
    End Sub
End Class