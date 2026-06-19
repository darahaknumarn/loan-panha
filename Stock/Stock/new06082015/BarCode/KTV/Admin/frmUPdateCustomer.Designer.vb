<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUPdateCustomer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUPdateCustomer))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.startDate = New System.Windows.Forms.DateTimePicker()
        Me.endDate = New System.Windows.Forms.DateTimePicker()
        Me.dgLoan = New System.Windows.Forms.DataGridView()
        Me.LD_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CMID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LD_Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LD_Date_Create = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LD_Date_Modify = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LDBrID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblLastUpdate = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.Update = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.dgCustomer = New System.Windows.Forms.DataGridView()
        Me.CM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CM_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LO_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateCreate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateModify = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CM_BrId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgLocation = New System.Windows.Forms.DataGridView()
        Me.LOID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.VL_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CN_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DT_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PV_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LO_BrId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOCreate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LOModify = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer()
        Me.OFDFileBrowse = New System.Windows.Forms.OpenFileDialog()
        Me.mnuimport = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnucheckErrors = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnupop = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.p1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.Panel1.SuspendLayout()
        CType(Me.dgLoan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.dgCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStripContainer1.SuspendLayout()
        Me.mnupop.SuspendLayout()
        Me.status.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Khmer OS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(557, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 40)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "លេខម៉ាស៊ីន:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1693, 63)
        Me.Panel1.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Khmer Muol", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(536, 6)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(546, 55)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "ទំរង់ទាញយកព័ត៌មានអំពីអតិថិជន"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtIP
        '
        Me.txtIP.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIP.Location = New System.Drawing.Point(704, 18)
        Me.txtIP.Margin = New System.Windows.Forms.Padding(4)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.Size = New System.Drawing.Size(219, 34)
        Me.txtIP.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Khmer OS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(565, 62)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 40)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "ថ្ងៃចាប់ផ្តើម:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Khmer OS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(589, 98)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 40)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "ថ្ងៃបញ្ចប់:"
        '
        'startDate
        '
        Me.startDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.startDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.startDate.Location = New System.Drawing.Point(704, 62)
        Me.startDate.Margin = New System.Windows.Forms.Padding(4)
        Me.startDate.Name = "startDate"
        Me.startDate.Size = New System.Drawing.Size(219, 30)
        Me.startDate.TabIndex = 4
        '
        'endDate
        '
        Me.endDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.endDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.endDate.Location = New System.Drawing.Point(704, 101)
        Me.endDate.Margin = New System.Windows.Forms.Padding(4)
        Me.endDate.Name = "endDate"
        Me.endDate.Size = New System.Drawing.Size(219, 30)
        Me.endDate.TabIndex = 4
        '
        'dgLoan
        '
        Me.dgLoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgLoan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LD_ID, Me.CMID, Me.EM_ID, Me.LD_Status, Me.LD_Date_Create, Me.LD_Date_Modify, Me.LDBrID})
        Me.dgLoan.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgLoan.Location = New System.Drawing.Point(0, 389)
        Me.dgLoan.Margin = New System.Windows.Forms.Padding(4)
        Me.dgLoan.Name = "dgLoan"
        Me.dgLoan.ReadOnly = True
        Me.dgLoan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgLoan.Size = New System.Drawing.Size(1693, 213)
        Me.dgLoan.TabIndex = 0
        '
        'LD_ID
        '
        Me.LD_ID.HeaderText = "លេខកិច្ចសន្យា"
        Me.LD_ID.Name = "LD_ID"
        Me.LD_ID.ReadOnly = True
        '
        'CMID
        '
        Me.CMID.HeaderText = "កូដអតិថិជន"
        Me.CMID.Name = "CMID"
        Me.CMID.ReadOnly = True
        '
        'EM_ID
        '
        Me.EM_ID.HeaderText = "កូដបុគ្គលិក"
        Me.EM_ID.Name = "EM_ID"
        Me.EM_ID.ReadOnly = True
        '
        'LD_Status
        '
        Me.LD_Status.HeaderText = "ស្ថានភាព"
        Me.LD_Status.Name = "LD_Status"
        Me.LD_Status.ReadOnly = True
        '
        'LD_Date_Create
        '
        Me.LD_Date_Create.HeaderText = "ថ្ងៃបង្កើត"
        Me.LD_Date_Create.Name = "LD_Date_Create"
        Me.LD_Date_Create.ReadOnly = True
        '
        'LD_Date_Modify
        '
        Me.LD_Date_Modify.HeaderText = "ថ្ងៃកែប្រែ"
        Me.LD_Date_Modify.Name = "LD_Date_Modify"
        Me.LD_Date_Modify.ReadOnly = True
        '
        'LDBrID
        '
        Me.LDBrID.HeaderText = "កូដសាខា"
        Me.LDBrID.Name = "LDBrID"
        Me.LDBrID.ReadOnly = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel3.Controls.Add(Me.lblLastUpdate)
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Controls.Add(Me.endDate)
        Me.Panel3.Controls.Add(Me.btnUpdate)
        Me.Panel3.Controls.Add(Me.Update)
        Me.Panel3.Controls.Add(Me.txtIP)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.startDate)
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.btnExit)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 63)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1693, 160)
        Me.Panel3.TabIndex = 6
        '
        'lblLastUpdate
        '
        Me.lblLastUpdate.AutoSize = True
        Me.lblLastUpdate.Font = New System.Drawing.Font("Khmer OS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLastUpdate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblLastUpdate.Location = New System.Drawing.Point(1257, 22)
        Me.lblLastUpdate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLastUpdate.Name = "lblLastUpdate"
        Me.lblLastUpdate.Size = New System.Drawing.Size(0, 40)
        Me.lblLastUpdate.TabIndex = 36
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 148)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1693, 12)
        Me.Panel2.TabIndex = 35
        '
        'btnUpdate
        '
        Me.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnUpdate.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.ForeColor = System.Drawing.Color.Brown
        Me.btnUpdate.Image = Global.CamITSo.My.Resources.Resources.transform
        Me.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdate.Location = New System.Drawing.Point(1085, 65)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(145, 39)
        Me.btnUpdate.TabIndex = 0
        Me.btnUpdate.Text = "រក្សាទុក"
        Me.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'Update
        '
        Me.Update.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Update.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Update.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Update.Image = CType(resources.GetObject("Update.Image"), System.Drawing.Image)
        Me.Update.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Update.Location = New System.Drawing.Point(932, 65)
        Me.Update.Margin = New System.Windows.Forms.Padding(4)
        Me.Update.Name = "Update"
        Me.Update.Size = New System.Drawing.Size(145, 39)
        Me.Update.TabIndex = 0
        Me.Update.Text = "បង្ហាញ"
        Me.Update.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Update.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Purple
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(932, 18)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(145, 39)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "ភ្ជាប់"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.Red
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(1085, 18)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(145, 39)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "ចាកចេញ"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'dgCustomer
        '
        Me.dgCustomer.BackgroundColor = System.Drawing.Color.DarkGray
        Me.dgCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCustomer.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CM_ID, Me.CM_Name, Me.LO_ID, Me.DateCreate, Me.DateModify, Me.CM_BrId})
        Me.dgCustomer.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgCustomer.Location = New System.Drawing.Point(0, 223)
        Me.dgCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.dgCustomer.Name = "dgCustomer"
        Me.dgCustomer.ReadOnly = True
        Me.dgCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgCustomer.Size = New System.Drawing.Size(1693, 166)
        Me.dgCustomer.TabIndex = 7
        '
        'CM_ID
        '
        Me.CM_ID.HeaderText = "កូដអតិថិជន"
        Me.CM_ID.Name = "CM_ID"
        Me.CM_ID.ReadOnly = True
        '
        'CM_Name
        '
        Me.CM_Name.HeaderText = "ឈ្មោះអតិថិជន"
        Me.CM_Name.Name = "CM_Name"
        Me.CM_Name.ReadOnly = True
        '
        'LO_ID
        '
        Me.LO_ID.HeaderText = "កូដតំបន់"
        Me.LO_ID.Name = "LO_ID"
        Me.LO_ID.ReadOnly = True
        '
        'DateCreate
        '
        Me.DateCreate.HeaderText = "ថ្ងៃបង្កើត"
        Me.DateCreate.Name = "DateCreate"
        Me.DateCreate.ReadOnly = True
        '
        'DateModify
        '
        Me.DateModify.HeaderText = "ថ្ងៃកែប្រែ"
        Me.DateModify.Name = "DateModify"
        Me.DateModify.ReadOnly = True
        '
        'CM_BrId
        '
        Me.CM_BrId.HeaderText = "កូដសាខា"
        Me.CM_BrId.Name = "CM_BrId"
        Me.CM_BrId.ReadOnly = True
        '
        'dgLocation
        '
        Me.dgLocation.BackgroundColor = System.Drawing.SystemColors.ActiveBorder
        Me.dgLocation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgLocation.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LOID, Me.VL_ID, Me.CN_ID, Me.DT_ID, Me.PV_ID, Me.LO_BrId, Me.LOCreate, Me.LOModify})
        Me.dgLocation.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.dgLocation.Location = New System.Drawing.Point(0, 602)
        Me.dgLocation.Margin = New System.Windows.Forms.Padding(4)
        Me.dgLocation.Name = "dgLocation"
        Me.dgLocation.ReadOnly = True
        Me.dgLocation.Size = New System.Drawing.Size(1693, 186)
        Me.dgLocation.TabIndex = 8
        '
        'LOID
        '
        Me.LOID.HeaderText = "កូដតំបន់"
        Me.LOID.Name = "LOID"
        Me.LOID.ReadOnly = True
        '
        'VL_ID
        '
        Me.VL_ID.HeaderText = "ភូមិ"
        Me.VL_ID.Name = "VL_ID"
        Me.VL_ID.ReadOnly = True
        '
        'CN_ID
        '
        Me.CN_ID.HeaderText = "ឈុំ"
        Me.CN_ID.Name = "CN_ID"
        Me.CN_ID.ReadOnly = True
        '
        'DT_ID
        '
        Me.DT_ID.HeaderText = "ស្រុក"
        Me.DT_ID.Name = "DT_ID"
        Me.DT_ID.ReadOnly = True
        '
        'PV_ID
        '
        Me.PV_ID.HeaderText = "ខេត្ត"
        Me.PV_ID.Name = "PV_ID"
        Me.PV_ID.ReadOnly = True
        '
        'LO_BrId
        '
        Me.LO_BrId.HeaderText = "សាខា"
        Me.LO_BrId.Name = "LO_BrId"
        Me.LO_BrId.ReadOnly = True
        '
        'LOCreate
        '
        Me.LOCreate.HeaderText = "ថ្ងៃបង្កើត"
        Me.LOCreate.Name = "LOCreate"
        Me.LOCreate.ReadOnly = True
        '
        'LOModify
        '
        Me.LOModify.HeaderText = "ថ្ងៃកែប្រែ"
        Me.LOModify.Name = "LOModify"
        Me.LOModify.ReadOnly = True
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.AutoScroll = True
        Me.ToolStripContainer1.ContentPanel.Margin = New System.Windows.Forms.Padding(4)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(1693, 0)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 602)
        Me.ToolStripContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(1693, 0)
        Me.ToolStripContainer1.TabIndex = 9
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'OFDFileBrowse
        '
        Me.OFDFileBrowse.Filter = "Excel 2007-2010(*.xlsx)|*.xlsx|Excel 97-2003(*.xls)|*.xls"
        '
        'mnuimport
        '
        Me.mnuimport.Name = "mnuimport"
        Me.mnuimport.Size = New System.Drawing.Size(159, 24)
        Me.mnuimport.Text = "&Import Data"
        '
        'mnucheckErrors
        '
        Me.mnucheckErrors.Name = "mnucheckErrors"
        Me.mnucheckErrors.Size = New System.Drawing.Size(159, 24)
        Me.mnucheckErrors.Text = "&Check Errors"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(159, 24)
        Me.DeleteToolStripMenuItem.Text = "&Delete"
        '
        'mnupop
        '
        Me.mnupop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem, Me.mnucheckErrors, Me.mnuimport})
        Me.mnupop.Name = "mnupop"
        Me.mnupop.Size = New System.Drawing.Size(160, 76)
        '
        'status
        '
        Me.status.Dock = System.Windows.Forms.DockStyle.None
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.p1})
        Me.status.Location = New System.Drawing.Point(353, 15)
        Me.status.Name = "status"
        Me.status.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.status.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.status.Size = New System.Drawing.Size(202, 22)
        Me.status.TabIndex = 127
        Me.status.Text = "StatusStrip1"
        Me.status.Visible = False
        '
        'p1
        '
        Me.p1.Name = "p1"
        Me.p1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always
        Me.p1.Size = New System.Drawing.Size(133, 16)
        Me.p1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.p1.Visible = False
        '
        'frmUPdateCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(1693, 788)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Controls.Add(Me.dgLocation)
        Me.Controls.Add(Me.dgLoan)
        Me.Controls.Add(Me.dgCustomer)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmUPdateCustomer"
        Me.Text = "frmUPdateCustomer"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dgLoan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.dgCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.mnupop.ResumeLayout(False)
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents startDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents endDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Update As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents dgLoan As System.Windows.Forms.DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents dgCustomer As System.Windows.Forms.DataGridView
    Friend WithEvents dgLocation As System.Windows.Forms.DataGridView
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents OFDFileBrowse As System.Windows.Forms.OpenFileDialog
    Friend WithEvents mnuimport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnucheckErrors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnupop As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents status As System.Windows.Forms.StatusStrip
    Friend WithEvents p1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblLastUpdate As System.Windows.Forms.Label
    Friend WithEvents LD_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CMID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LD_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LD_Date_Create As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LD_Date_Modify As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LDBrID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CM_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LO_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DateCreate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DateModify As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CM_BrId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents VL_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CN_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DT_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PV_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LO_BrId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOCreate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LOModify As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
