<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.mystatus = New System.Windows.Forms.StatusStrip()
        Me.TopPanel = New System.Windows.Forms.Panel()
        Me.lblshow = New System.Windows.Forms.Label()
        Me.myimg = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCompany = New System.Windows.Forms.TextBox()
        Me.mnufile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnulogoff = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CompanyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuexit = New System.Windows.Forms.ToolStripMenuItem()
        Me.អពបរពនធToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModulesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResourceManagementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.បរពនធគរបគរងសងToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.បរពនធគរបគរងសងToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnutool = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuadmin = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnudatabasemanagement = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnubackup = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnurestore = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ForITSolutionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TopPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'mystatus
        '
        Me.mystatus.Location = New System.Drawing.Point(0, 711)
        Me.mystatus.Name = "mystatus"
        Me.mystatus.Size = New System.Drawing.Size(767, 22)
        Me.mystatus.TabIndex = 0
        Me.mystatus.Text = "Hi"
        '
        'TopPanel
        '
        Me.TopPanel.Controls.Add(Me.lblshow)
        Me.TopPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TopPanel.Location = New System.Drawing.Point(0, 61)
        Me.TopPanel.Name = "TopPanel"
        Me.TopPanel.Size = New System.Drawing.Size(767, 39)
        Me.TopPanel.TabIndex = 6
        Me.TopPanel.Visible = False
        '
        'lblshow
        '
        Me.lblshow.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblshow.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblshow.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblshow.Image = Global.CamITSo.My.Resources.Resources.calculator
        Me.lblshow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblshow.Location = New System.Drawing.Point(0, 0)
        Me.lblshow.Name = "lblshow"
        Me.lblshow.Size = New System.Drawing.Size(767, 39)
        Me.lblshow.TabIndex = 0
        Me.lblshow.Text = "កម្មវិធីគ្រប់គ្រងការលក់ដូរ"
        Me.lblshow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'myimg
        '
        Me.myimg.ImageStream = CType(resources.GetObject("myimg.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.myimg.TransparentColor = System.Drawing.Color.Transparent
        Me.myimg.Images.SetKeyName(0, "nextresult - Copy.png")
        Me.myimg.Images.SetKeyName(1, "nextresult.png")
        Me.myimg.Images.SetKeyName(2, "nextresult - Copy.png")
        Me.myimg.Images.SetKeyName(3, "nextresult.png")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblName)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lblCode)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtCompany)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 681)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(767, 30)
        Me.Panel1.TabIndex = 14
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblName.Location = New System.Drawing.Point(331, 3)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(0, 23)
        Me.lblName.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Green
        Me.Label3.Location = New System.Drawing.Point(240, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 23)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Company Name:"
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.ForeColor = System.Drawing.Color.Navy
        Me.lblCode.Location = New System.Drawing.Point(196, 3)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(0, 23)
        Me.lblCode.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Purple
        Me.Label1.Location = New System.Drawing.Point(106, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Company Code:"
        '
        'txtCompany
        '
        Me.txtCompany.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.Location = New System.Drawing.Point(3, 3)
        Me.txtCompany.Multiline = True
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(100, 24)
        Me.txtCompany.TabIndex = 0
        '
        'mnufile
        '
        Me.mnufile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnulogoff, Me.ToolStripMenuItem1, Me.CompanyToolStripMenuItem, Me.mnuexit, Me.អពបរពនធToolStripMenuItem})
        Me.mnufile.Font = New System.Drawing.Font("Khmer Moul", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnufile.Image = CType(resources.GetObject("mnufile.Image"), System.Drawing.Image)
        Me.mnufile.Name = "mnufile"
        Me.mnufile.Size = New System.Drawing.Size(133, 57)
        Me.mnufile.Text = "ឯកសារ"
        '
        'mnulogoff
        '
        Me.mnulogoff.Image = CType(resources.GetObject("mnulogoff.Image"), System.Drawing.Image)
        Me.mnulogoff.Name = "mnulogoff"
        Me.mnulogoff.Size = New System.Drawing.Size(267, 58)
        Me.mnulogoff.Text = "ប្តូរអ្នកប្រើប្រាស់"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = CType(resources.GetObject("ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(267, 58)
        Me.ToolStripMenuItem1.Text = "ប្តូរលេខសំងាត់"
        '
        'CompanyToolStripMenuItem
        '
        Me.CompanyToolStripMenuItem.Image = CType(resources.GetObject("CompanyToolStripMenuItem.Image"), System.Drawing.Image)
        Me.CompanyToolStripMenuItem.Name = "CompanyToolStripMenuItem"
        Me.CompanyToolStripMenuItem.Size = New System.Drawing.Size(267, 58)
        Me.CompanyToolStripMenuItem.Text = "ក្រុមហ៊ុន"
        '
        'mnuexit
        '
        Me.mnuexit.Image = CType(resources.GetObject("mnuexit.Image"), System.Drawing.Image)
        Me.mnuexit.Name = "mnuexit"
        Me.mnuexit.Size = New System.Drawing.Size(267, 58)
        Me.mnuexit.Text = "ចាកចេញ"
        '
        'អពបរពនធToolStripMenuItem
        '
        Me.អពបរពនធToolStripMenuItem.Image = CType(resources.GetObject("អពបរពនធToolStripMenuItem.Image"), System.Drawing.Image)
        Me.អពបរពនធToolStripMenuItem.Name = "អពបរពនធToolStripMenuItem"
        Me.អពបរពនធToolStripMenuItem.Size = New System.Drawing.Size(267, 58)
        Me.អពបរពនធToolStripMenuItem.Text = "អំពីប្រព័ន្ធ"
        '
        'ModulesToolStripMenuItem
        '
        Me.ModulesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ResourceManagementToolStripMenuItem, Me.បរពនធគរបគរងសងToolStripMenuItem, Me.បរពនធគរបគរងសងToolStripMenuItem1})
        Me.ModulesToolStripMenuItem.Font = New System.Drawing.Font("Khmer Muol", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ModulesToolStripMenuItem.Image = CType(resources.GetObject("ModulesToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ModulesToolStripMenuItem.Name = "ModulesToolStripMenuItem"
        Me.ModulesToolStripMenuItem.Size = New System.Drawing.Size(327, 57)
        Me.ModulesToolStripMenuItem.Text = "ប្រព័ន្ធ និង របាយការណ៍"
        '
        'ResourceManagementToolStripMenuItem
        '
        Me.ResourceManagementToolStripMenuItem.Image = CType(resources.GetObject("ResourceManagementToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ResourceManagementToolStripMenuItem.Name = "ResourceManagementToolStripMenuItem"
        Me.ResourceManagementToolStripMenuItem.Size = New System.Drawing.Size(406, 48)
        Me.ResourceManagementToolStripMenuItem.Text = "ប្រព័ន្ធគ្រប់គ្រងទ្រព្យតម្កល់"
        '
        'បរពនធគរបគរងសងToolStripMenuItem
        '
        Me.បរពនធគរបគរងសងToolStripMenuItem.Image = CType(resources.GetObject("បរពនធគរបគរងសងToolStripMenuItem.Image"), System.Drawing.Image)
        Me.បរពនធគរបគរងសងToolStripMenuItem.Name = "បរពនធគរបគរងសងToolStripMenuItem"
        Me.បរពនធគរបគរងសងToolStripMenuItem.Size = New System.Drawing.Size(406, 48)
        Me.បរពនធគរបគរងសងToolStripMenuItem.Text = "ប្រព័ន្ធគ្រប់គ្រងស្តុក"
        '
        'បរពនធគរបគរងសងToolStripMenuItem1
        '
        Me.បរពនធគរបគរងសងToolStripMenuItem1.Image = CType(resources.GetObject("បរពនធគរបគរងសងToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.បរពនធគរបគរងសងToolStripMenuItem1.Name = "បរពនធគរបគរងសងToolStripMenuItem1"
        Me.បរពនធគរបគរងសងToolStripMenuItem1.Size = New System.Drawing.Size(406, 48)
        Me.បរពនធគរបគរងសងToolStripMenuItem1.Text = "ប្រព័ន្ធគ្រប់គ្រងសាំង"
        '
        'mnutool
        '
        Me.mnutool.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuadmin, Me.mnudatabasemanagement, Me.LogoToolStripMenuItem, Me.ExportToolStripMenuItem, Me.ImportToolStripMenuItem, Me.ForITSolutionToolStripMenuItem})
        Me.mnutool.Font = New System.Drawing.Font("Khmer Muol", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mnutool.Image = CType(resources.GetObject("mnutool.Image"), System.Drawing.Image)
        Me.mnutool.Name = "mnutool"
        Me.mnutool.Size = New System.Drawing.Size(158, 57)
        Me.mnutool.Text = "ឧបករណ៍"
        '
        'mnuadmin
        '
        Me.mnuadmin.Image = CType(resources.GetObject("mnuadmin.Image"), System.Drawing.Image)
        Me.mnuadmin.Name = "mnuadmin"
        Me.mnuadmin.Size = New System.Drawing.Size(389, 48)
        Me.mnuadmin.Text = "Administration"
        '
        'mnudatabasemanagement
        '
        Me.mnudatabasemanagement.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnubackup, Me.mnurestore})
        Me.mnudatabasemanagement.Image = CType(resources.GetObject("mnudatabasemanagement.Image"), System.Drawing.Image)
        Me.mnudatabasemanagement.Name = "mnudatabasemanagement"
        Me.mnudatabasemanagement.Size = New System.Drawing.Size(389, 48)
        Me.mnudatabasemanagement.Text = " Database Management ​"
        '
        'mnubackup
        '
        Me.mnubackup.Image = CType(resources.GetObject("mnubackup.Image"), System.Drawing.Image)
        Me.mnubackup.Name = "mnubackup"
        Me.mnubackup.Size = New System.Drawing.Size(286, 48)
        Me.mnubackup.Text = "Backup Database"
        '
        'mnurestore
        '
        Me.mnurestore.Image = CType(resources.GetObject("mnurestore.Image"), System.Drawing.Image)
        Me.mnurestore.Name = "mnurestore"
        Me.mnurestore.Size = New System.Drawing.Size(286, 48)
        Me.mnurestore.Text = "Restore Database"
        '
        'LogoToolStripMenuItem
        '
        Me.LogoToolStripMenuItem.Image = CType(resources.GetObject("LogoToolStripMenuItem.Image"), System.Drawing.Image)
        Me.LogoToolStripMenuItem.Name = "LogoToolStripMenuItem"
        Me.LogoToolStripMenuItem.Size = New System.Drawing.Size(389, 48)
        Me.LogoToolStripMenuItem.Text = "Logo"
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.Image = Global.CamITSo.My.Resources.Resources.nextresult___Copy
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(389, 48)
        Me.ExportToolStripMenuItem.Text = "Export"
        '
        'ImportToolStripMenuItem
        '
        Me.ImportToolStripMenuItem.Image = Global.CamITSo.My.Resources.Resources.nextresult
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        Me.ImportToolStripMenuItem.Size = New System.Drawing.Size(389, 48)
        Me.ImportToolStripMenuItem.Text = "Import"
        '
        'ForITSolutionToolStripMenuItem
        '
        Me.ForITSolutionToolStripMenuItem.Image = CType(resources.GetObject("ForITSolutionToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ForITSolutionToolStripMenuItem.Name = "ForITSolutionToolStripMenuItem"
        Me.ForITSolutionToolStripMenuItem.Size = New System.Drawing.Size(389, 48)
        Me.ForITSolutionToolStripMenuItem.Text = "បញ្ចូលទិន្នន័យតាមសាខា"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.MenuStrip1.Font = New System.Drawing.Font("Khmer OS Battambang", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnufile, Me.ModulesToolStripMenuItem, Me.mnutool})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(767, 61)
        Me.MenuStrip1.TabIndex = 12
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(767, 733)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TopPanel)
        Me.Controls.Add(Me.mystatus)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Vivat General Management System"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TopPanel.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mystatus As System.Windows.Forms.StatusStrip
    Friend WithEvents TopPanel As System.Windows.Forms.Panel
    Friend WithEvents myimg As System.Windows.Forms.ImageList
    Friend WithEvents lblshow As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCompany As System.Windows.Forms.TextBox
    Friend WithEvents mnufile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnulogoff As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CompanyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuexit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents អពបរពនធToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModulesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResourceManagementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents បរពនធគរបគរងសងToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents បរពនធគរបគរងសងToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnutool As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuadmin As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnudatabasemanagement As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnubackup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnurestore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ForITSolutionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
