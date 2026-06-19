<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResourceManagement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResourceManagement))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnColleteral = New System.Windows.Forms.Button()
        Me.BtnStaff = New System.Windows.Forms.Button()
        Me.BtnCustomer = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DateReturn = New System.Windows.Forms.DateTimePicker()
        Me.DateBorrow = New System.Windows.Forms.DateTimePicker()
        Me.txtStaffid = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.radThen15 = New System.Windows.Forms.RadioButton()
        Me.CboSearch = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Returned = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.NotReturn = New System.Windows.Forms.RadioButton()
        Me.txtDay = New System.Windows.Forms.TextBox()
        Me.txtInsert = New System.Windows.Forms.TextBox()
        Me.All = New System.Windows.Forms.RadioButton()
        Me.txtCustomerID = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnReturn = New System.Windows.Forms.Button()
        Me.BtnReport = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.BtnNew = New System.Windows.Forms.Button()
        Me.txtCollateralName = New System.Windows.Forms.TextBox()
        Me.txtCollateralID = New System.Windows.Forms.TextBox()
        Me.txtStaffName = New System.Windows.Forms.TextBox()
        Me.txtCustomerAddress = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.DateReturn)
        Me.Panel1.Controls.Add(Me.DateBorrow)
        Me.Panel1.Controls.Add(Me.txtStaffid)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.txtCustomerID)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtCustomerName)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.BtnReturn)
        Me.Panel1.Controls.Add(Me.BtnReport)
        Me.Panel1.Controls.Add(Me.BtnExit)
        Me.Panel1.Controls.Add(Me.BtnDelete)
        Me.Panel1.Controls.Add(Me.BtnEdit)
        Me.Panel1.Controls.Add(Me.BtnNew)
        Me.Panel1.Controls.Add(Me.txtCollateralName)
        Me.Panel1.Controls.Add(Me.txtCollateralID)
        Me.Panel1.Controls.Add(Me.txtStaffName)
        Me.Panel1.Controls.Add(Me.txtCustomerAddress)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1805, 314)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.SeaShell
        Me.GroupBox2.Controls.Add(Me.BtnColleteral)
        Me.GroupBox2.Controls.Add(Me.BtnStaff)
        Me.GroupBox2.Controls.Add(Me.BtnCustomer)
        Me.GroupBox2.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(828, 74)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(191, 209)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ព័ត៌មាន"
        '
        'BtnColleteral
        '
        Me.BtnColleteral.AutoSize = True
        Me.BtnColleteral.BackColor = System.Drawing.Color.Transparent
        Me.BtnColleteral.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnColleteral.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnColleteral.Image = Global.CamITSo.My.Resources.Resources.archive_24
        Me.BtnColleteral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnColleteral.Location = New System.Drawing.Point(16, 31)
        Me.BtnColleteral.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnColleteral.Name = "BtnColleteral"
        Me.BtnColleteral.Size = New System.Drawing.Size(147, 52)
        Me.BtnColleteral.TabIndex = 4
        Me.BtnColleteral.Text = "ទ្រព្យតម្តល់"
        Me.BtnColleteral.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnColleteral.UseVisualStyleBackColor = False
        '
        'BtnStaff
        '
        Me.BtnStaff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnStaff.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnStaff.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStaff.ForeColor = System.Drawing.Color.Purple
        Me.BtnStaff.Image = CType(resources.GetObject("BtnStaff.Image"), System.Drawing.Image)
        Me.BtnStaff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnStaff.Location = New System.Drawing.Point(16, 88)
        Me.BtnStaff.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnStaff.Name = "BtnStaff"
        Me.BtnStaff.Size = New System.Drawing.Size(147, 52)
        Me.BtnStaff.TabIndex = 4
        Me.BtnStaff.Text = "បុគ្គលិក"
        Me.BtnStaff.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnStaff.UseVisualStyleBackColor = True
        '
        'BtnCustomer
        '
        Me.BtnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCustomer.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCustomer.ForeColor = System.Drawing.Color.Navy
        Me.BtnCustomer.Image = CType(resources.GetObject("BtnCustomer.Image"), System.Drawing.Image)
        Me.BtnCustomer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCustomer.Location = New System.Drawing.Point(16, 146)
        Me.BtnCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnCustomer.Name = "BtnCustomer"
        Me.BtnCustomer.Size = New System.Drawing.Size(147, 52)
        Me.BtnCustomer.TabIndex = 4
        Me.BtnCustomer.Text = "អតិថិជន"
        Me.BtnCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCustomer.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 302)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1805, 12)
        Me.Panel3.TabIndex = 12
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1805, 55)
        Me.Panel2.TabIndex = 11
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Khmer Muol", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(675, 0)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(421, 55)
        Me.Label13.TabIndex = 8
        Me.Label13.Text = "ប្រព័ន្ធគ្រប់គ្រងទ្រព្យតម្កល់"
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.Transparent
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Khmer Kep", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.Green
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(1443, 70)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(144, 43)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "បង្ហាញទៅ Excel"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button3.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.Transparent
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Khmer Kep", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Teal
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(1443, 126)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(144, 42)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "បង្ហាញឡើងវិញ"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.UseVisualStyleBackColor = False
        '
        'DateReturn
        '
        Me.DateReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateReturn.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateReturn.Location = New System.Drawing.Point(653, 247)
        Me.DateReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.DateReturn.Name = "DateReturn"
        Me.DateReturn.Size = New System.Drawing.Size(132, 29)
        Me.DateReturn.TabIndex = 7
        Me.DateReturn.Value = New Date(2012, 8, 1, 0, 0, 0, 0)
        '
        'DateBorrow
        '
        Me.DateBorrow.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateBorrow.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateBorrow.Location = New System.Drawing.Point(396, 247)
        Me.DateBorrow.Margin = New System.Windows.Forms.Padding(4)
        Me.DateBorrow.Name = "DateBorrow"
        Me.DateBorrow.Size = New System.Drawing.Size(145, 29)
        Me.DateBorrow.TabIndex = 7
        Me.DateBorrow.Value = New Date(2012, 8, 6, 13, 14, 38, 0)
        '
        'txtStaffid
        '
        Me.txtStaffid.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStaffid.Location = New System.Drawing.Point(149, 121)
        Me.txtStaffid.Margin = New System.Windows.Forms.Padding(4)
        Me.txtStaffid.Name = "txtStaffid"
        Me.txtStaffid.Size = New System.Drawing.Size(73, 32)
        Me.txtStaffid.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.LightGray
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.GroupBox1.Controls.Add(Me.radThen15)
        Me.GroupBox1.Controls.Add(Me.CboSearch)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Returned)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.NotReturn)
        Me.GroupBox1.Controls.Add(Me.txtDay)
        Me.GroupBox1.Controls.Add(Me.txtInsert)
        Me.GroupBox1.Controls.Add(Me.All)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(1048, 70)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(363, 212)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "ការស្វែងរក"
        '
        'radThen15
        '
        Me.radThen15.AutoSize = True
        Me.radThen15.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radThen15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.radThen15.Location = New System.Drawing.Point(133, 64)
        Me.radThen15.Margin = New System.Windows.Forms.Padding(4)
        Me.radThen15.Name = "radThen15"
        Me.radThen15.Size = New System.Drawing.Size(127, 32)
        Me.radThen15.TabIndex = 10
        Me.radThen15.Text = "លើសចំនួនថ្ងៃ :"
        Me.radThen15.UseVisualStyleBackColor = True
        '
        'CboSearch
        '
        Me.CboSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboSearch.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CboSearch.FormattingEnabled = True
        Me.CboSearch.Items.AddRange(New Object() {"កូដទ្រព្យ", "កូដអតិថិជន", "កូដបុគ្គលិក"})
        Me.CboSearch.Location = New System.Drawing.Point(139, 103)
        Me.CboSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.CboSearch.Name = "CboSearch"
        Me.CboSearch.Size = New System.Drawing.Size(172, 38)
        Me.CboSearch.TabIndex = 0
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(5, 158)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(122, 30)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "បញ្ចូលលេខកូដ :"
        '
        'Returned
        '
        Me.Returned.AutoSize = True
        Me.Returned.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Returned.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Returned.Location = New System.Drawing.Point(133, 32)
        Me.Returned.Margin = New System.Windows.Forms.Padding(4)
        Me.Returned.Name = "Returned"
        Me.Returned.Size = New System.Drawing.Size(87, 32)
        Me.Returned.TabIndex = 9
        Me.Returned.Text = "បានសង"
        Me.Returned.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(5, 107)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 30)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "ជ្រើសរើស :"
        '
        'NotReturn
        '
        Me.NotReturn.AutoSize = True
        Me.NotReturn.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NotReturn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.NotReturn.Location = New System.Drawing.Point(8, 64)
        Me.NotReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.NotReturn.Name = "NotReturn"
        Me.NotReturn.Size = New System.Drawing.Size(109, 32)
        Me.NotReturn.TabIndex = 8
        Me.NotReturn.Text = "មិនទាន់សង"
        Me.NotReturn.UseVisualStyleBackColor = True
        '
        'txtDay
        '
        Me.txtDay.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDay.Location = New System.Drawing.Point(265, 65)
        Me.txtDay.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDay.Name = "txtDay"
        Me.txtDay.Size = New System.Drawing.Size(44, 25)
        Me.txtDay.TabIndex = 0
        '
        'txtInsert
        '
        Me.txtInsert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtInsert.Location = New System.Drawing.Point(139, 154)
        Me.txtInsert.Margin = New System.Windows.Forms.Padding(4)
        Me.txtInsert.Name = "txtInsert"
        Me.txtInsert.ShortcutsEnabled = False
        Me.txtInsert.Size = New System.Drawing.Size(172, 38)
        Me.txtInsert.TabIndex = 0
        '
        'All
        '
        Me.All.AutoSize = True
        Me.All.Checked = True
        Me.All.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.All.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.All.Location = New System.Drawing.Point(8, 32)
        Me.All.Margin = New System.Windows.Forms.Padding(4)
        Me.All.Name = "All"
        Me.All.Size = New System.Drawing.Size(87, 32)
        Me.All.TabIndex = 7
        Me.All.TabStop = True
        Me.All.Text = "ទាំងអស់"
        Me.All.UseVisualStyleBackColor = True
        '
        'txtCustomerID
        '
        Me.txtCustomerID.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerID.Location = New System.Drawing.Point(448, 118)
        Me.txtCustomerID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerID.Name = "txtCustomerID"
        Me.txtCustomerID.Size = New System.Drawing.Size(71, 32)
        Me.txtCustomerID.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(21, 247)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 30)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "ឈ្មោះទ្រព្យ    :"
        '
        'txtCustomerName
        '
        Me.txtCustomerName.BackColor = System.Drawing.Color.White
        Me.txtCustomerName.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.Location = New System.Drawing.Point(468, 159)
        Me.txtCustomerName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.ReadOnly = True
        Me.txtCustomerName.Size = New System.Drawing.Size(143, 32)
        Me.txtCustomerName.TabIndex = 0
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Navy
        Me.Label10.Location = New System.Drawing.Point(582, 248)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(66, 30)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "ថ្ងៃសង :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(343, 247)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 30)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "ថ្ងៃខ្ចី :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Navy
        Me.Label8.Location = New System.Drawing.Point(342, 201)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(155, 30)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "អាសយដ្ខានអតិថិជន :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Navy
        Me.Label7.Location = New System.Drawing.Point(342, 161)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(118, 30)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "ឈ្មោះអតិថិជន :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(342, 121)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(98, 30)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "កូដអតិថិជន :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(21, 207)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 30)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "កូដទ្រព្យ        :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(21, 166)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 30)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "ឈ្មោះបុគ្គលិក :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(21, 124)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 30)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "កូដបុគ្គលិក     :"
        '
        'BtnReturn
        '
        Me.BtnReturn.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnReturn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnReturn.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReturn.ForeColor = System.Drawing.Color.Black
        Me.BtnReturn.Image = CType(resources.GetObject("BtnReturn.Image"), System.Drawing.Image)
        Me.BtnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnReturn.Location = New System.Drawing.Point(275, 66)
        Me.BtnReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnReturn.Name = "BtnReturn"
        Me.BtnReturn.Size = New System.Drawing.Size(116, 42)
        Me.BtnReturn.TabIndex = 4
        Me.BtnReturn.Text = "សង"
        Me.BtnReturn.UseVisualStyleBackColor = False
        '
        'BtnReport
        '
        Me.BtnReport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnReport.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReport.ForeColor = System.Drawing.Color.Teal
        Me.BtnReport.Image = CType(resources.GetObject("BtnReport.Image"), System.Drawing.Image)
        Me.BtnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnReport.Location = New System.Drawing.Point(1443, 182)
        Me.BtnReport.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnReport.Name = "BtnReport"
        Me.BtnReport.Size = New System.Drawing.Size(144, 42)
        Me.BtnReport.TabIndex = 4
        Me.BtnReport.Text = "របាយការណ៍"
        Me.BtnReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnReport.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.BackColor = System.Drawing.Color.Transparent
        Me.BtnExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnExit.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnExit.Image = CType(resources.GetObject("BtnExit.Image"), System.Drawing.Image)
        Me.BtnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnExit.Location = New System.Drawing.Point(1443, 240)
        Me.BtnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(144, 43)
        Me.BtnExit.TabIndex = 4
        Me.BtnExit.Text = "ចាកចេញ"
        Me.BtnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExit.UseVisualStyleBackColor = False
        '
        'BtnDelete
        '
        Me.BtnDelete.BackColor = System.Drawing.Color.Transparent
        Me.BtnDelete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDelete.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.ForeColor = System.Drawing.Color.Red
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDelete.Location = New System.Drawing.Point(527, 67)
        Me.BtnDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(116, 42)
        Me.BtnDelete.TabIndex = 4
        Me.BtnDelete.Text = "លុប"
        Me.BtnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnDelete.UseVisualStyleBackColor = False
        '
        'BtnEdit
        '
        Me.BtnEdit.BackColor = System.Drawing.Color.Transparent
        Me.BtnEdit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEdit.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.ForeColor = System.Drawing.Color.Black
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnEdit.Location = New System.Drawing.Point(403, 67)
        Me.BtnEdit.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(116, 42)
        Me.BtnEdit.TabIndex = 4
        Me.BtnEdit.Text = "កែប្រែ"
        Me.BtnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnEdit.UseVisualStyleBackColor = False
        '
        'BtnNew
        '
        Me.BtnNew.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.BtnNew.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnNew.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNew.ForeColor = System.Drawing.Color.Navy
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnNew.Location = New System.Drawing.Point(151, 66)
        Me.BtnNew.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(116, 42)
        Me.BtnNew.TabIndex = 4
        Me.BtnNew.Text = "ខ្ចី"
        Me.BtnNew.UseVisualStyleBackColor = False
        '
        'txtCollateralName
        '
        Me.txtCollateralName.BackColor = System.Drawing.Color.White
        Me.txtCollateralName.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCollateralName.Location = New System.Drawing.Point(149, 244)
        Me.txtCollateralName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCollateralName.Name = "txtCollateralName"
        Me.txtCollateralName.ReadOnly = True
        Me.txtCollateralName.Size = New System.Drawing.Size(158, 32)
        Me.txtCollateralName.TabIndex = 0
        '
        'txtCollateralID
        '
        Me.txtCollateralID.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCollateralID.Location = New System.Drawing.Point(149, 203)
        Me.txtCollateralID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCollateralID.Name = "txtCollateralID"
        Me.txtCollateralID.Size = New System.Drawing.Size(73, 32)
        Me.txtCollateralID.TabIndex = 0
        '
        'txtStaffName
        '
        Me.txtStaffName.BackColor = System.Drawing.Color.White
        Me.txtStaffName.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStaffName.Location = New System.Drawing.Point(149, 162)
        Me.txtStaffName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtStaffName.Name = "txtStaffName"
        Me.txtStaffName.ReadOnly = True
        Me.txtStaffName.Size = New System.Drawing.Size(158, 32)
        Me.txtStaffName.TabIndex = 0
        '
        'txtCustomerAddress
        '
        Me.txtCustomerAddress.BackColor = System.Drawing.Color.White
        Me.txtCustomerAddress.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerAddress.Location = New System.Drawing.Point(512, 199)
        Me.txtCustomerAddress.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCustomerAddress.Name = "txtCustomerAddress"
        Me.txtCustomerAddress.ReadOnly = True
        Me.txtCustomerAddress.Size = New System.Drawing.Size(273, 32)
        Me.txtCustomerAddress.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7, Me.Column8, Me.Column9, Me.Column10, Me.Column11, Me.Column12, Me.Column13})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 314)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1805, 555)
        Me.DataGridView1.TabIndex = 1
        '
        'Column1
        '
        Me.Column1.HeaderText = "លេខរៀង"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Column2
        '
        Me.Column2.HeaderText = "កូដបុគ្គលិក"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "ឈ្មោះបុគ្គលិក"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "កូដទ្រព្យ"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "ឈ្មោះទ្រព្យ"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        '
        'Column6
        '
        Me.Column6.HeaderText = "កូដអតិថិជន"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.HeaderText = "ឈ្មោះអតិថិជន"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.HeaderText = "អស័យដ្នាន"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.HeaderText = "ថ្ងៃខ្ចី"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column10
        '
        Me.Column10.HeaderText = "ថ្ងៃត្រូវសង"
        Me.Column10.Name = "Column10"
        Me.Column10.ReadOnly = True
        '
        'Column11
        '
        Me.Column11.HeaderText = "ថ្ងៃសងជាក់ស្តែង"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        '
        'Column12
        '
        Me.Column12.HeaderText = "ចំនួនថ្ងៃលើស"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        '
        'Column13
        '
        Me.Column13.HeaderText = "បានទទួល"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        '
        'frmResourceManagement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.CancelButton = Me.BtnExit
        Me.ClientSize = New System.Drawing.Size(1805, 869)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmResourceManagement"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtCustomerAddress As System.Windows.Forms.TextBox
    Friend WithEvents BtnReturn As System.Windows.Forms.Button
    Friend WithEvents BtnReport As System.Windows.Forms.Button
    Friend WithEvents BtnCustomer As System.Windows.Forms.Button
    Friend WithEvents BtnStaff As System.Windows.Forms.Button
    Friend WithEvents BtnColleteral As System.Windows.Forms.Button
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents BtnEdit As System.Windows.Forms.Button
    Friend WithEvents BtnNew As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCollateralName As System.Windows.Forms.TextBox
    Friend WithEvents txtCollateralID As System.Windows.Forms.TextBox
    Friend WithEvents txtStaffName As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtStaffid As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerID As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Friend WithEvents DateReturn As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateBorrow As System.Windows.Forms.DateTimePicker
    Friend WithEvents CboSearch As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtInsert As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Returned As System.Windows.Forms.RadioButton
    Friend WithEvents NotReturn As System.Windows.Forms.RadioButton
    Friend WithEvents All As System.Windows.Forms.RadioButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents radThen15 As System.Windows.Forms.RadioButton
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDay As System.Windows.Forms.TextBox
End Class
