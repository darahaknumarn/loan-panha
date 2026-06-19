Public NotInheritable Class AboutmyPOS

    Private Sub AboutmyPOS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = "Vivath General Management System." 'My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = "Copyright to Vivath"
        'My.Application.Info.Copyright
        Me.LabelCompanyName.Text = "Company name: Vivath"
        'My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = "Prepared by Mr. Sary Monyroth" & vbCrLf & "" & vbCrLf & "Contact:" & vbCrLf & "" & vbCrLf & "    Tel: 096 307 6666, 098 494 994." & vbCrLf & "" & vbCrLf & "    Email: monyrothit@gmail.com"
        'My.Application.Info.Description
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub TextBoxDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxDescription.TextChanged

    End Sub
End Class
