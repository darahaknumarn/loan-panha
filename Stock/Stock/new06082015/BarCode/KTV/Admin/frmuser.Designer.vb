<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmuser
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pInfo = New System.Windows.Forms.Panel()
        Me.btnemail = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Bprint = New System.Windows.Forms.Button()
        Me.cbopo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtTel = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtemail = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtname1 = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.btnimg = New System.Windows.Forms.Button()
        Me.imgphoto = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cborole = New System.Windows.Forms.ComboBox()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Sales1 = New CamITSo.Sales()
        Me.B4 = New System.Windows.Forms.Button()
        Me.B1 = New System.Windows.Forms.Button()
        Me.B3 = New System.Windows.Forms.Button()
        Me.B2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtuser = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lstuser = New System.Windows.Forms.ListView()
        Me.Photo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Role = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ofd = New System.Windows.Forms.OpenFileDialog()
        Me.TblUserTableAdapter = New CamITSo.SalesTableAdapters.tblUserTableAdapter()
        Me.TblRoleTableAdapter = New CamITSo.SalesTableAdapters.tblRoleTableAdapter()
        Me.myimg = New System.Windows.Forms.ImageList(Me.components)
        Me.pInfo.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.imgphoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Sales1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(38, 126)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 18)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "រូបថត"
        '
        'pInfo
        '
        Me.pInfo.Controls.Add(Me.btnemail)
        Me.pInfo.Controls.Add(Me.Panel3)
        Me.pInfo.Controls.Add(Me.TabControl1)
        Me.pInfo.Controls.Add(Me.btnimg)
        Me.pInfo.Controls.Add(Me.imgphoto)
        Me.pInfo.Controls.Add(Me.Label6)
        Me.pInfo.Controls.Add(Me.Label5)
        Me.pInfo.Controls.Add(Me.cborole)
        Me.pInfo.Controls.Add(Me.B4)
        Me.pInfo.Controls.Add(Me.B1)
        Me.pInfo.Controls.Add(Me.B3)
        Me.pInfo.Controls.Add(Me.B2)
        Me.pInfo.Controls.Add(Me.Label1)
        Me.pInfo.Controls.Add(Me.txtuser)
        Me.pInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pInfo.Location = New System.Drawing.Point(0, 0)
        Me.pInfo.Name = "pInfo"
        Me.pInfo.Size = New System.Drawing.Size(934, 316)
        Me.pInfo.TabIndex = 0
        '
        'btnemail
        '
        Me.btnemail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnemail.Location = New System.Drawing.Point(307, 266)
        Me.btnemail.Name = "btnemail"
        Me.btnemail.Size = New System.Drawing.Size(96, 34)
        Me.btnemail.TabIndex = 7
        Me.btnemail.Text = "&Email"
        Me.btnemail.UseVisualStyleBackColor = True
        Me.btnemail.Visible = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Bprint)
        Me.Panel3.Controls.Add(Me.cbopo)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Location = New System.Drawing.Point(445, 6)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(436, 45)
        Me.Panel3.TabIndex = 50
        Me.Panel3.Visible = False
        '
        'Bprint
        '
        Me.Bprint.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Bprint.Image = Global.CamITSo.My.Resources.Resources.printer
        Me.Bprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Bprint.Location = New System.Drawing.Point(312, 0)
        Me.Bprint.Name = "Bprint"
        Me.Bprint.Size = New System.Drawing.Size(104, 45)
        Me.Bprint.TabIndex = 51
        Me.Bprint.Text = "បោះពុម្ភ"
        Me.Bprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Bprint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Bprint.UseVisualStyleBackColor = True
        '
        'cbopo
        '
        Me.cbopo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbopo.FormattingEnabled = True
        Me.cbopo.Location = New System.Drawing.Point(120, 3)
        Me.cbopo.Name = "cbopo"
        Me.cbopo.Size = New System.Drawing.Size(186, 28)
        Me.cbopo.TabIndex = 46
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 20)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "របាយការណ៍"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.ItemSize = New System.Drawing.Size(80, 40)
        Me.TabControl1.Location = New System.Drawing.Point(439, 57)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(492, 253)
        Me.TabControl1.TabIndex = 8
        Me.TabControl1.Visible = False
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label11)
        Me.TabPage1.Controls.Add(Me.txtFax)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.txtTel)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.txtemail)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.txtname1)
        Me.TabPage1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 44)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(484, 205)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "ទំនាក់ទំនង"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(19, 138)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(41, 20)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "ទូរសារ"
        '
        'txtFax
        '
        Me.txtFax.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.Location = New System.Drawing.Point(98, 132)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(145, 26)
        Me.txtFax.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(19, 106)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(43, 20)
        Me.Label10.TabIndex = 23
        Me.Label10.Text = "ទូរសព្ទ័"
        '
        'txtTel
        '
        Me.txtTel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTel.Location = New System.Drawing.Point(98, 100)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.Size = New System.Drawing.Size(145, 26)
        Me.txtTel.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(19, 71)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 20)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Email"
        '
        'txtemail
        '
        Me.txtemail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtemail.Location = New System.Drawing.Point(98, 68)
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Size = New System.Drawing.Size(145, 26)
        Me.txtemail.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(19, 31)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 20)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "ឈ្មោះ"
        '
        'txtname1
        '
        Me.txtname1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtname1.Location = New System.Drawing.Point(98, 25)
        Me.txtname1.Name = "txtname1"
        Me.txtname1.Size = New System.Drawing.Size(145, 26)
        Me.txtname1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label13)
        Me.TabPage2.Controls.Add(Me.txtCity)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.txtAddress)
        Me.TabPage2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 44)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(484, 205)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "អាស័យដ្ឋាន"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(29, 69)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(65, 20)
        Me.Label13.TabIndex = 29
        Me.Label13.Text = "ទីក្រុង​ខេត្ត"
        '
        'txtCity
        '
        Me.txtCity.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.Location = New System.Drawing.Point(108, 63)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(330, 26)
        Me.txtCity.TabIndex = 28
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(29, 27)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(69, 20)
        Me.Label12.TabIndex = 27
        Me.Label12.Text = "អាស័យដ្ឋាន"
        '
        'txtAddress
        '
        Me.txtAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.Location = New System.Drawing.Point(108, 21)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(330, 26)
        Me.txtAddress.TabIndex = 26
        '
        'btnimg
        '
        Me.btnimg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnimg.Image = Global.CamITSo.My.Resources.Resources.folder_view
        Me.btnimg.Location = New System.Drawing.Point(307, 132)
        Me.btnimg.Name = "btnimg"
        Me.btnimg.Size = New System.Drawing.Size(78, 67)
        Me.btnimg.TabIndex = 6
        Me.btnimg.UseVisualStyleBackColor = True
        '
        'imgphoto
        '
        Me.imgphoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgphoto.Image = Global.CamITSo.My.Resources.Resources.lifestyle
        Me.imgphoto.Location = New System.Drawing.Point(173, 132)
        Me.imgphoto.Name = "imgphoto"
        Me.imgphoto.Size = New System.Drawing.Size(128, 168)
        Me.imgphoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgphoto.TabIndex = 26
        Me.imgphoto.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(37, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 20)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "តួនាទី (role)"
        '
        'cborole
        '
        Me.cborole.DataSource = Me.BindingSource1
        Me.cborole.DisplayMember = "Roles"
        Me.cborole.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cborole.FormattingEnabled = True
        Me.cborole.Location = New System.Drawing.Point(173, 89)
        Me.cborole.Name = "cborole"
        Me.cborole.Size = New System.Drawing.Size(260, 28)
        Me.cborole.TabIndex = 5
        Me.cborole.ValueMember = "RID"
        '
        'BindingSource1
        '
        Me.BindingSource1.DataMember = "tblRole"
        Me.BindingSource1.DataSource = Me.Sales1
        '
        'Sales1
        '
        Me.Sales1.DataSetName = "Sales"
        Me.Sales1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'B4
        '
        Me.B4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B4.Location = New System.Drawing.Point(337, 6)
        Me.B4.Name = "B4"
        Me.B4.Size = New System.Drawing.Size(96, 34)
        Me.B4.TabIndex = 3
        Me.B4.Text = "ចាកចេញ"
        Me.B4.UseVisualStyleBackColor = True
        '
        'B1
        '
        Me.B1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B1.Location = New System.Drawing.Point(31, 6)
        Me.B1.Name = "B1"
        Me.B1.Size = New System.Drawing.Size(96, 34)
        Me.B1.TabIndex = 0
        Me.B1.Text = "បញ្ជូលថ្មី"
        Me.B1.UseVisualStyleBackColor = True
        '
        'B3
        '
        Me.B3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B3.Location = New System.Drawing.Point(235, 6)
        Me.B3.Name = "B3"
        Me.B3.Size = New System.Drawing.Size(96, 34)
        Me.B3.TabIndex = 2
        Me.B3.Text = "លុបចោល"
        Me.B3.UseVisualStyleBackColor = True
        '
        'B2
        '
        Me.B2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B2.Location = New System.Drawing.Point(133, 6)
        Me.B2.Name = "B2"
        Me.B2.Size = New System.Drawing.Size(96, 34)
        Me.B2.TabIndex = 1
        Me.B2.Text = "កែរប្រែ"
        Me.B2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(37, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 20)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "អ្នកប្រើប្រាស់"
        '
        'txtuser
        '
        Me.txtuser.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtuser.Location = New System.Drawing.Point(173, 57)
        Me.txtuser.Name = "txtuser"
        Me.txtuser.Size = New System.Drawing.Size(260, 26)
        Me.txtuser.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lstuser)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 316)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(934, 144)
        Me.Panel1.TabIndex = 25
        '
        'lstuser
        '
        Me.lstuser.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lstuser.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Photo, Me.Role})
        Me.lstuser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstuser.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstuser.FullRowSelect = True
        Me.lstuser.HideSelection = False
        Me.lstuser.Location = New System.Drawing.Point(0, 0)
        Me.lstuser.MultiSelect = False
        Me.lstuser.Name = "lstuser"
        Me.lstuser.ShowItemToolTips = True
        Me.lstuser.Size = New System.Drawing.Size(934, 144)
        Me.lstuser.TabIndex = 1
        Me.lstuser.TileSize = New System.Drawing.Size(1, 1)
        Me.lstuser.UseCompatibleStateImageBehavior = False
        Me.lstuser.View = System.Windows.Forms.View.Details
        '
        'Photo
        '
        Me.Photo.Text = "Photo"
        Me.Photo.Width = 263
        '
        'Role
        '
        Me.Role.Text = "Role"
        Me.Role.Width = 140
        '
        'ofd
        '
        Me.ofd.Title = "បើករករូបថត"
        '
        'TblUserTableAdapter
        '
        Me.TblUserTableAdapter.ClearBeforeFill = True
        '
        'TblRoleTableAdapter
        '
        Me.TblRoleTableAdapter.ClearBeforeFill = True
        '
        'myimg
        '
        Me.myimg.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.myimg.ImageSize = New System.Drawing.Size(64, 75)
        Me.myimg.TransparentColor = System.Drawing.Color.Transparent
        '
        'frmuser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(934, 460)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pInfo)
        Me.Name = "frmuser"
        Me.Text = "អ្នកប្រើប្រាស់"
        Me.pInfo.ResumeLayout(False)
        Me.pInfo.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.imgphoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Sales1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pInfo As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cborole As System.Windows.Forms.ComboBox
    Friend WithEvents B4 As System.Windows.Forms.Button
    Friend WithEvents B1 As System.Windows.Forms.Button
    Friend WithEvents B3 As System.Windows.Forms.Button
    Friend WithEvents B2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtuser As System.Windows.Forms.TextBox
    Friend WithEvents btnimg As System.Windows.Forms.Button
    Friend WithEvents imgphoto As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lstuser As System.Windows.Forms.ListView
    Friend WithEvents IMG As System.Windows.Forms.ImageList
    Friend WithEvents ofd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtFax As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtemail As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtname1 As System.Windows.Forms.TextBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Bprint As System.Windows.Forms.Button
    Friend WithEvents cbopo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnemail As System.Windows.Forms.Button
    Friend WithEvents Photo As System.Windows.Forms.ColumnHeader
    Friend WithEvents Role As System.Windows.Forms.ColumnHeader
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents Sales1 As CamITSo.Sales
    Friend WithEvents TblUserTableAdapter As CamITSo.SalesTableAdapters.tblUserTableAdapter
    Friend WithEvents TblRoleTableAdapter As CamITSo.SalesTableAdapters.tblRoleTableAdapter
    Friend WithEvents myimg As System.Windows.Forms.ImageList
End Class
