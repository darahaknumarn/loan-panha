<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRepayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRepayment))
        Me.co_Charge1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Payoff1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Balnance1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Service1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Int1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Prn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_AmtPaid11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_DateToPay1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridView5 = New System.Windows.Forms.DataGridView()
        Me.co_AmtPaid111 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Des1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_DatePaid1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.dgCMCode = New System.Windows.Forms.DataGridView()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.radLoanId = New System.Windows.Forms.RadioButton()
        Me.radCuName = New System.Windows.Forms.RadioButton()
        Me.radCuId = New System.Windows.Forms.RadioButton()
        Me.txtLoanid = New System.Windows.Forms.TextBox()
        Me.txtCmName = New System.Windows.Forms.TextBox()
        Me.txtCmId = New System.Windows.Forms.TextBox()
        Me.DataGridView3 = New System.Windows.Forms.DataGridView()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.DateTimePicker3 = New System.Windows.Forms.DateTimePicker()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.coDateTopayWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coAmtToPayWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coChargeWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coDatePaidWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coPaidWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coDesWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCM_AddressWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCM_NameWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCM_IDWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coEMName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coEMID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coLD_IDWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coNoWF = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WfRepay = New System.Windows.Forms.TabPage()
        Me.dgWF = New System.Windows.Forms.DataGridView()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.DataGridView4 = New System.Windows.Forms.DataGridView()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.coNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coLD_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCM_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCM_Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coDes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coAmtPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coDatePaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCharge = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coAmtToPay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coDateTopay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblEmployee = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.co_DateToPay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_AmtPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Prn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Int = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Service = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Balnance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Payoff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_AmtPaid1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Charge = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_Des = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.co_DatePaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.lblResultSum = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblResultCount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblAutoSum = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel16.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel15.SuspendLayout()
        CType(Me.dgCMCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel12.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel17.SuspendLayout()
        Me.WfRepay.SuspendLayout()
        CType(Me.dgWF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel11.SuspendLayout()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel10.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'co_Charge1
        '
        Me.co_Charge1.HeaderText = "ពិន័យ"
        Me.co_Charge1.Name = "co_Charge1"
        Me.co_Charge1.ReadOnly = True
        '
        'co_Payoff1
        '
        Me.co_Payoff1.HeaderText = "បង់ផ្តាច់"
        Me.co_Payoff1.Name = "co_Payoff1"
        Me.co_Payoff1.ReadOnly = True
        '
        'co_Balnance1
        '
        Me.co_Balnance1.HeaderText = "សម្យតុល្យ"
        Me.co_Balnance1.Name = "co_Balnance1"
        Me.co_Balnance1.ReadOnly = True
        '
        'co_Service1
        '
        Me.co_Service1.HeaderText = "ក.ប្រតិបត្តិការ"
        Me.co_Service1.Name = "co_Service1"
        Me.co_Service1.ReadOnly = True
        '
        'co_Int1
        '
        Me.co_Int1.HeaderText = "ការប្រាក់"
        Me.co_Int1.Name = "co_Int1"
        Me.co_Int1.ReadOnly = True
        '
        'co_Prn1
        '
        Me.co_Prn1.HeaderText = "ប្រាក់ដើម"
        Me.co_Prn1.Name = "co_Prn1"
        Me.co_Prn1.ReadOnly = True
        '
        'co_AmtPaid11
        '
        Me.co_AmtPaid11.HeaderText = "ទឹកប្រាក់ត្រូវបង់"
        Me.co_AmtPaid11.Name = "co_AmtPaid11"
        Me.co_AmtPaid11.ReadOnly = True
        '
        'co_DateToPay1
        '
        Me.co_DateToPay1.HeaderText = "ថ្ងៃត្រូវបង់"
        Me.co_DateToPay1.Name = "co_DateToPay1"
        Me.co_DateToPay1.ReadOnly = True
        '
        'DataGridView5
        '
        Me.DataGridView5.AllowUserToAddRows = False
        Me.DataGridView5.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView5.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.co_DateToPay1, Me.co_AmtPaid11, Me.co_Prn1, Me.co_Int1, Me.co_Service1, Me.co_Balnance1, Me.co_Payoff1, Me.co_AmtPaid111, Me.co_Charge1, Me.co_Des1, Me.co_DatePaid1})
        Me.DataGridView5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView5.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView5.Name = "DataGridView5"
        Me.DataGridView5.ReadOnly = True
        Me.DataGridView5.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridView5.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView5.Size = New System.Drawing.Size(1942, 522)
        Me.DataGridView5.TabIndex = 2
        '
        'co_AmtPaid111
        '
        Me.co_AmtPaid111.HeaderText = "ទឹកប្រាក់បានបង់"
        Me.co_AmtPaid111.Name = "co_AmtPaid111"
        Me.co_AmtPaid111.ReadOnly = True
        '
        'co_Des1
        '
        Me.co_Des1.HeaderText = "អធិប្បាយ"
        Me.co_Des1.Name = "co_Des1"
        Me.co_Des1.ReadOnly = True
        '
        'co_DatePaid1
        '
        Me.co_DatePaid1.HeaderText = "ថ្ងៃបានបង់"
        Me.co_DatePaid1.Name = "co_DatePaid1"
        Me.co_DatePaid1.ReadOnly = True
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.DataGridView5)
        Me.Panel16.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel16.Location = New System.Drawing.Point(3, 51)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(1942, 522)
        Me.Panel16.TabIndex = 2
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Panel16)
        Me.TabPage5.Controls.Add(Me.Panel14)
        Me.TabPage5.Location = New System.Drawing.Point(4, 49)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(1948, 576)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Tag = "79"
        Me.TabPage5.Text = "Special Repay"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel14.Controls.Add(Me.Panel15)
        Me.Panel14.Controls.Add(Me.Label9)
        Me.Panel14.Controls.Add(Me.Label10)
        Me.Panel14.Controls.Add(Me.TextBox2)
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel14.Location = New System.Drawing.Point(3, 3)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(1942, 48)
        Me.Panel14.TabIndex = 1
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.Label8)
        Me.Panel15.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel15.Location = New System.Drawing.Point(1650, 0)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(292, 48)
        Me.Panel15.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(27, 2)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(282, 40)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Esc: To exit, F11: To excel"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(4, 3)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(161, 40)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "លេខកិច្ចសន្យា:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(357, 5)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(54, 40)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "Null"
        '
        'TextBox2
        '
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(174, 5)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(168, 35)
        Me.TextBox2.TabIndex = 0
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(8, 17)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(178, 40)
        Me.DateTimePicker2.TabIndex = 6
        '
        'Button5
        '
        Me.Button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button5.Location = New System.Drawing.Point(340, 17)
        Me.Button5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(128, 45)
        Me.Button5.TabIndex = 5
        Me.Button5.Text = "Excel"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button4.Location = New System.Drawing.Point(204, 17)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(128, 45)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "បង្ហាញ"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Button4.UseVisualStyleBackColor = True
        '
        'dgCMCode
        '
        Me.dgCMCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgCMCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgCMCode.Location = New System.Drawing.Point(0, 85)
        Me.dgCMCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgCMCode.Name = "dgCMCode"
        Me.dgCMCode.ReadOnly = True
        Me.dgCMCode.Size = New System.Drawing.Size(1940, 481)
        Me.dgCMCode.TabIndex = 1
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.dgCMCode)
        Me.Panel12.Controls.Add(Me.Panel13)
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel12.Location = New System.Drawing.Point(4, 5)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(1940, 566)
        Me.Panel12.TabIndex = 0
        '
        'Panel13
        '
        Me.Panel13.Controls.Add(Me.DateTimePicker2)
        Me.Panel13.Controls.Add(Me.Button5)
        Me.Panel13.Controls.Add(Me.Button4)
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel13.Location = New System.Drawing.Point(0, 0)
        Me.Panel13.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(1940, 85)
        Me.Panel13.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Panel12)
        Me.TabPage4.Location = New System.Drawing.Point(4, 49)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage4.Size = New System.Drawing.Size(1948, 576)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "ស្វែងរកកូដអតិថិជនដែលមិនប្រើ"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'radLoanId
        '
        Me.radLoanId.AutoSize = True
        Me.radLoanId.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radLoanId.Location = New System.Drawing.Point(1050, 17)
        Me.radLoanId.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.radLoanId.Name = "radLoanId"
        Me.radLoanId.Size = New System.Drawing.Size(209, 31)
        Me.radLoanId.TabIndex = 8
        Me.radLoanId.TabStop = True
        Me.radLoanId.Text = "Search by loan id:"
        Me.radLoanId.UseVisualStyleBackColor = True
        '
        'radCuName
        '
        Me.radCuName.AutoSize = True
        Me.radCuName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radCuName.Location = New System.Drawing.Point(423, 17)
        Me.radCuName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.radCuName.Name = "radCuName"
        Me.radCuName.Size = New System.Drawing.Size(290, 31)
        Me.radCuName.TabIndex = 7
        Me.radCuName.TabStop = True
        Me.radCuName.Text = "Search by customer name:"
        Me.radCuName.UseVisualStyleBackColor = True
        '
        'radCuId
        '
        Me.radCuId.AutoSize = True
        Me.radCuId.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.radCuId.Location = New System.Drawing.Point(8, 17)
        Me.radCuId.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.radCuId.Name = "radCuId"
        Me.radCuId.Size = New System.Drawing.Size(256, 31)
        Me.radCuId.TabIndex = 6
        Me.radCuId.TabStop = True
        Me.radCuId.Text = "Search by customer id:"
        Me.radCuId.UseVisualStyleBackColor = True
        '
        'txtLoanid
        '
        Me.txtLoanid.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanid.Location = New System.Drawing.Point(1256, 15)
        Me.txtLoanid.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLoanid.Name = "txtLoanid"
        Me.txtLoanid.Size = New System.Drawing.Size(148, 35)
        Me.txtLoanid.TabIndex = 5
        '
        'txtCmName
        '
        Me.txtCmName.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCmName.Location = New System.Drawing.Point(711, 17)
        Me.txtCmName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCmName.Name = "txtCmName"
        Me.txtCmName.Size = New System.Drawing.Size(314, 35)
        Me.txtCmName.TabIndex = 3
        '
        'txtCmId
        '
        Me.txtCmId.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCmId.Location = New System.Drawing.Point(264, 15)
        Me.txtCmId.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCmId.Name = "txtCmId"
        Me.txtCmId.Size = New System.Drawing.Size(148, 35)
        Me.txtCmId.TabIndex = 0
        '
        'DataGridView3
        '
        Me.DataGridView3.AllowUserToAddRows = False
        Me.DataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView3.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView3.Name = "DataGridView3"
        Me.DataGridView3.ReadOnly = True
        Me.DataGridView3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView3.Size = New System.Drawing.Size(1940, 277)
        Me.DataGridView3.TabIndex = 0
        '
        'Button8
        '
        Me.Button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button8.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.Image = CType(resources.GetObject("Button8.Image"), System.Drawing.Image)
        Me.Button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button8.Location = New System.Drawing.Point(507, 5)
        Me.Button8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(128, 43)
        Me.Button8.TabIndex = 3
        Me.Button8.Text = "បង្ហាញ"
        Me.Button8.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button6.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button6.Location = New System.Drawing.Point(822, 5)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(142, 43)
        Me.Button6.TabIndex = 10
        Me.Button6.Text = "AutoSum"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button7.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.ForeColor = System.Drawing.Color.Red
        Me.Button7.Image = Global.morokot.My.Resources.Resources.INFO
        Me.Button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button7.Location = New System.Drawing.Point(644, 5)
        Me.Button7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(170, 43)
        Me.Button7.TabIndex = 11
        Me.Button7.Text = "Clear repayment"
        Me.Button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(298, 5)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(40, 41)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "All"
        '
        'ComboBox2
        '
        Me.ComboBox2.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(177, 6)
        Me.ComboBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(120, 34)
        Me.ComboBox2.TabIndex = 8
        '
        'DateTimePicker3
        '
        Me.DateTimePicker3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker3.Location = New System.Drawing.Point(6, 5)
        Me.DateTimePicker3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker3.Name = "DateTimePicker3"
        Me.DateTimePicker3.Size = New System.Drawing.Size(160, 35)
        Me.DateTimePicker3.TabIndex = 1
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel17.Controls.Add(Me.Button6)
        Me.Panel17.Controls.Add(Me.Button7)
        Me.Panel17.Controls.Add(Me.Label11)
        Me.Panel17.Controls.Add(Me.ComboBox2)
        Me.Panel17.Controls.Add(Me.Button8)
        Me.Panel17.Controls.Add(Me.DateTimePicker3)
        Me.Panel17.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel17.Location = New System.Drawing.Point(4, 5)
        Me.Panel17.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(1940, 52)
        Me.Panel17.TabIndex = 2
        '
        'coDateTopayWF
        '
        Me.coDateTopayWF.HeaderText = "ថ្ងៃត្រូវបង់"
        Me.coDateTopayWF.Name = "coDateTopayWF"
        '
        'coAmtToPayWF
        '
        Me.coAmtToPayWF.HeaderText = "ប្រាក់ត្រូវបង់"
        Me.coAmtToPayWF.Name = "coAmtToPayWF"
        '
        'coChargeWF
        '
        Me.coChargeWF.HeaderText = "ពិន័យ"
        Me.coChargeWF.Name = "coChargeWF"
        '
        'coDatePaidWF
        '
        Me.coDatePaidWF.HeaderText = "ថ្ងៃបានបង់"
        Me.coDatePaidWF.Name = "coDatePaidWF"
        '
        'coPaidWF
        '
        Me.coPaidWF.HeaderText = "ប្រាក់បានបង់"
        Me.coPaidWF.Name = "coPaidWF"
        '
        'coDesWF
        '
        Me.coDesWF.HeaderText = "អធិប្បាយ"
        Me.coDesWF.Name = "coDesWF"
        '
        'coCM_AddressWF
        '
        Me.coCM_AddressWF.HeaderText = "អស័យដ្ឋាន"
        Me.coCM_AddressWF.Name = "coCM_AddressWF"
        '
        'coCM_NameWF
        '
        Me.coCM_NameWF.HeaderText = "ឈ្មោះអតិថិជន"
        Me.coCM_NameWF.Name = "coCM_NameWF"
        '
        'coCM_IDWF
        '
        Me.coCM_IDWF.HeaderText = "កូដអតិថិជន"
        Me.coCM_IDWF.Name = "coCM_IDWF"
        '
        'coEMName
        '
        Me.coEMName.HeaderText = "ឈ្មោះបុគ្គលិក"
        Me.coEMName.Name = "coEMName"
        '
        'coEMID
        '
        Me.coEMID.HeaderText = "កូដបុគ្គលិក"
        Me.coEMID.Name = "coEMID"
        '
        'coLD_IDWF
        '
        Me.coLD_IDWF.HeaderText = "លេខកិច្ចសន្យា"
        Me.coLD_IDWF.Name = "coLD_IDWF"
        '
        'coNoWF
        '
        Me.coNoWF.HeaderText = "...."
        Me.coNoWF.Name = "coNoWF"
        '
        'WfRepay
        '
        Me.WfRepay.Controls.Add(Me.dgWF)
        Me.WfRepay.Controls.Add(Me.Panel17)
        Me.WfRepay.Location = New System.Drawing.Point(4, 49)
        Me.WfRepay.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.WfRepay.Name = "WfRepay"
        Me.WfRepay.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.WfRepay.Size = New System.Drawing.Size(1948, 576)
        Me.WfRepay.TabIndex = 5
        Me.WfRepay.Text = "WriteOff Repayment"
        Me.WfRepay.UseVisualStyleBackColor = True
        '
        'dgWF
        '
        Me.dgWF.AllowUserToAddRows = False
        Me.dgWF.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgWF.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgWF.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.coNoWF, Me.coLD_IDWF, Me.coEMID, Me.coEMName, Me.coCM_IDWF, Me.coCM_NameWF, Me.coCM_AddressWF, Me.coDesWF, Me.coPaidWF, Me.coDatePaidWF, Me.coChargeWF, Me.coAmtToPayWF, Me.coDateTopayWF})
        Me.dgWF.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgWF.Location = New System.Drawing.Point(4, 57)
        Me.dgWF.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgWF.Name = "dgWF"
        Me.dgWF.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.dgWF.Size = New System.Drawing.Size(1940, 514)
        Me.dgWF.TabIndex = 3
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.DataGridView4)
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel11.Location = New System.Drawing.Point(0, 346)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(1940, 220)
        Me.Panel11.TabIndex = 2
        '
        'DataGridView4
        '
        Me.DataGridView4.AllowUserToAddRows = False
        Me.DataGridView4.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView4.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView4.Name = "DataGridView4"
        Me.DataGridView4.ReadOnly = True
        Me.DataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView4.Size = New System.Drawing.Size(1940, 220)
        Me.DataGridView4.TabIndex = 0
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.DataGridView3)
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel10.Location = New System.Drawing.Point(0, 69)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(1940, 277)
        Me.Panel10.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.DataGridView1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(4, 57)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1940, 514)
        Me.Panel5.TabIndex = 2
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.coNo, Me.coLD_ID, Me.coCM_ID, Me.coCM_Name, Me.coCM_Address, Me.coDes, Me.coAmtPaid, Me.coDatePaid, Me.coCharge, Me.coAmtToPay, Me.coDateTopay})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridView1.Size = New System.Drawing.Size(1940, 514)
        Me.DataGridView1.TabIndex = 1
        '
        'coNo
        '
        Me.coNo.HeaderText = "...."
        Me.coNo.Name = "coNo"
        '
        'coLD_ID
        '
        Me.coLD_ID.HeaderText = "លេខកិច្ចសន្យា"
        Me.coLD_ID.Name = "coLD_ID"
        '
        'coCM_ID
        '
        Me.coCM_ID.HeaderText = "កូដអតិថិជន"
        Me.coCM_ID.Name = "coCM_ID"
        '
        'coCM_Name
        '
        Me.coCM_Name.HeaderText = "ឈ្មោះអតិថិជន"
        Me.coCM_Name.Name = "coCM_Name"
        '
        'coCM_Address
        '
        Me.coCM_Address.HeaderText = "អស័យដ្ឋាន"
        Me.coCM_Address.Name = "coCM_Address"
        '
        'coDes
        '
        Me.coDes.HeaderText = "អធិប្បាយ"
        Me.coDes.Name = "coDes"
        '
        'coAmtPaid
        '
        Me.coAmtPaid.HeaderText = "ប្រាក់បានបង់"
        Me.coAmtPaid.Name = "coAmtPaid"
        '
        'coDatePaid
        '
        Me.coDatePaid.HeaderText = "ថ្ងៃបានបង់"
        Me.coDatePaid.Name = "coDatePaid"
        '
        'coCharge
        '
        Me.coCharge.HeaderText = "ពិន័យ"
        Me.coCharge.Name = "coCharge"
        '
        'coAmtToPay
        '
        Me.coAmtToPay.HeaderText = "ប្រាក់ត្រូវបង់"
        Me.coAmtToPay.Name = "coAmtToPay"
        '
        'coDateTopay
        '
        Me.coDateTopay.HeaderText = "ថ្ងៃត្រូវបង់"
        Me.coDateTopay.Name = "coDateTopay"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel3.Controls.Add(Me.lblEmployee)
        Me.Panel3.Controls.Add(Me.ComboBox1)
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.Button2)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.DateTimePicker1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(4, 5)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1940, 52)
        Me.Panel3.TabIndex = 0
        '
        'lblEmployee
        '
        Me.lblEmployee.AutoSize = True
        Me.lblEmployee.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployee.Location = New System.Drawing.Point(298, 5)
        Me.lblEmployee.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(40, 41)
        Me.lblEmployee.TabIndex = 9
        Me.lblEmployee.Text = "All"
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(177, 6)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(120, 34)
        Me.ComboBox1.TabIndex = 8
        '
        'Button3
        '
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button3.Location = New System.Drawing.Point(822, 5)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(142, 43)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "AutoSum"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Red
        Me.Button2.Image = Global.morokot.My.Resources.Resources.INFO
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(644, 5)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(170, 43)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Clear repayment"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(507, 5)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(128, 43)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "បង្ហាញ"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(6, 5)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(160, 35)
        Me.DateTimePicker1.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TabControl1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1956, 629)
        Me.Panel2.TabIndex = 3
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.WfRepay)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1956, 629)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel5)
        Me.TabPage1.Controls.Add(Me.Panel3)
        Me.TabPage1.Font = New System.Drawing.Font("Khmer Kep", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage1.Location = New System.Drawing.Point(4, 49)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage1.Size = New System.Drawing.Size(1948, 576)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "យកប្រាក់ និងបង់ផ្តាច់"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridView2)
        Me.TabPage2.Controls.Add(Me.Panel4)
        Me.TabPage2.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabPage2.Location = New System.Drawing.Point(4, 49)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Size = New System.Drawing.Size(1948, 576)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "មើលតារាងនិមួយៗ"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.co_DateToPay, Me.co_AmtPaid, Me.co_Prn, Me.co_Int, Me.co_Service, Me.co_Balnance, Me.co_Payoff, Me.co_AmtPaid1, Me.co_Charge, Me.co_Des, Me.co_DatePaid})
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(4, 53)
        Me.DataGridView2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.DataGridView2.Size = New System.Drawing.Size(1940, 518)
        Me.DataGridView2.TabIndex = 1
        '
        'co_DateToPay
        '
        Me.co_DateToPay.HeaderText = "ថ្ងៃត្រូវបង់"
        Me.co_DateToPay.Name = "co_DateToPay"
        Me.co_DateToPay.ReadOnly = True
        '
        'co_AmtPaid
        '
        Me.co_AmtPaid.HeaderText = "ទឹកប្រាក់ត្រូវបង់"
        Me.co_AmtPaid.Name = "co_AmtPaid"
        Me.co_AmtPaid.ReadOnly = True
        '
        'co_Prn
        '
        Me.co_Prn.HeaderText = "ប្រាក់ដើម"
        Me.co_Prn.Name = "co_Prn"
        Me.co_Prn.ReadOnly = True
        '
        'co_Int
        '
        Me.co_Int.HeaderText = "ការប្រាក់"
        Me.co_Int.Name = "co_Int"
        Me.co_Int.ReadOnly = True
        '
        'co_Service
        '
        Me.co_Service.HeaderText = "ក.ប្រតិបត្តិការ"
        Me.co_Service.Name = "co_Service"
        Me.co_Service.ReadOnly = True
        '
        'co_Balnance
        '
        Me.co_Balnance.HeaderText = "សម្យតុល្យ"
        Me.co_Balnance.Name = "co_Balnance"
        Me.co_Balnance.ReadOnly = True
        '
        'co_Payoff
        '
        Me.co_Payoff.HeaderText = "បង់ផ្តាច់"
        Me.co_Payoff.Name = "co_Payoff"
        Me.co_Payoff.ReadOnly = True
        '
        'co_AmtPaid1
        '
        Me.co_AmtPaid1.HeaderText = "ទឹកប្រាក់បានបង់"
        Me.co_AmtPaid1.Name = "co_AmtPaid1"
        Me.co_AmtPaid1.ReadOnly = True
        '
        'co_Charge
        '
        Me.co_Charge.HeaderText = "ពិន័យ"
        Me.co_Charge.Name = "co_Charge"
        Me.co_Charge.ReadOnly = True
        '
        'co_Des
        '
        Me.co_Des.HeaderText = "អធិប្បាយ"
        Me.co_Des.Name = "co_Des"
        Me.co_Des.ReadOnly = True
        '
        'co_DatePaid
        '
        Me.co_DatePaid.HeaderText = "ថ្ងៃបានបង់"
        Me.co_DatePaid.Name = "co_DatePaid"
        Me.co_DatePaid.ReadOnly = True
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel4.Controls.Add(Me.Panel7)
        Me.Panel4.Controls.Add(Me.Label5)
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Controls.Add(Me.TextBox1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(4, 5)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1940, 48)
        Me.Panel4.TabIndex = 0
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.Label6)
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel7.Location = New System.Drawing.Point(1648, 0)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(292, 48)
        Me.Panel7.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(27, 2)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(254, 41)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Esc: To exit, F11: To excel"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 3)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(131, 41)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "លេខកិច្ចសន្យា:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(306, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 41)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Null"
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(132, 5)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(168, 35)
        Me.TextBox1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.Panel6)
        Me.TabPage3.Location = New System.Drawing.Point(4, 49)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage3.Size = New System.Drawing.Size(1948, 576)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "ស្វែងរកអតិថិជន"
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Panel11)
        Me.Panel6.Controls.Add(Me.Panel10)
        Me.Panel6.Controls.Add(Me.Panel9)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel6.Location = New System.Drawing.Point(4, 5)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(1940, 566)
        Me.Panel6.TabIndex = 0
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel9.Controls.Add(Me.radLoanId)
        Me.Panel9.Controls.Add(Me.radCuName)
        Me.Panel9.Controls.Add(Me.radCuId)
        Me.Panel9.Controls.Add(Me.txtLoanid)
        Me.Panel9.Controls.Add(Me.txtCmName)
        Me.Panel9.Controls.Add(Me.txtCmId)
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(1940, 69)
        Me.Panel9.TabIndex = 0
        '
        'lblResultSum
        '
        Me.lblResultSum.AutoSize = True
        Me.lblResultSum.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultSum.Location = New System.Drawing.Point(294, 11)
        Me.lblResultSum.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblResultSum.Name = "lblResultSum"
        Me.lblResultSum.Size = New System.Drawing.Size(41, 47)
        Me.lblResultSum.TabIndex = 3
        Me.lblResultSum.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(206, 8)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(105, 47)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Sum:"
        '
        'lblResultCount
        '
        Me.lblResultCount.AutoSize = True
        Me.lblResultCount.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultCount.Location = New System.Drawing.Point(111, 9)
        Me.lblResultCount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblResultCount.Name = "lblResultCount"
        Me.lblResultCount.Size = New System.Drawing.Size(41, 47)
        Me.lblResultCount.TabIndex = 1
        Me.lblResultCount.Text = "0"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(-6, 8)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(129, 47)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Count:"
        '
        'lblAutoSum
        '
        Me.lblAutoSum.AutoSize = True
        Me.lblAutoSum.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoSum.Location = New System.Drawing.Point(180, 11)
        Me.lblAutoSum.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAutoSum.Name = "lblAutoSum"
        Me.lblAutoSum.Size = New System.Drawing.Size(41, 47)
        Me.lblAutoSum.TabIndex = 6
        Me.lblAutoSum.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Times New Roman", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 8)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(185, 47)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "AutoSum:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(566, 17)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(977, 36)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "F12: Save, Delete: Delete repay, Esc: Exit, Ctrl+N: New row, F11: To excel"
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.lblAutoSum)
        Me.Panel8.Controls.Add(Me.Label7)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel8.Location = New System.Drawing.Point(1524, 0)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(432, 63)
        Me.Panel8.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.lblResultSum)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblResultCount)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 629)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1956, 63)
        Me.Panel1.TabIndex = 2
        '
        'frmRepayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1956, 692)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmRepayment"
        Me.Text = "frmRepayment"
        CType(Me.DataGridView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel16.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        CType(Me.dgCMCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel12.ResumeLayout(False)
        Me.Panel13.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.DataGridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.WfRepay.ResumeLayout(False)
        CType(Me.dgWF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel11.ResumeLayout(False)
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel10.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents co_Charge1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Payoff1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Balnance1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Service1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Int1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Prn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_AmtPaid11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_DateToPay1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridView5 As System.Windows.Forms.DataGridView
    Friend WithEvents co_AmtPaid111 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Des1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_DatePaid1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents dgCMCode As System.Windows.Forms.DataGridView
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents radLoanId As System.Windows.Forms.RadioButton
    Friend WithEvents radCuName As System.Windows.Forms.RadioButton
    Friend WithEvents radCuId As System.Windows.Forms.RadioButton
    Friend WithEvents txtLoanid As System.Windows.Forms.TextBox
    Friend WithEvents txtCmName As System.Windows.Forms.TextBox
    Friend WithEvents txtCmId As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents DateTimePicker3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents coDateTopayWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coAmtToPayWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coChargeWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coDatePaidWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coPaidWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coDesWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCM_AddressWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCM_NameWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCM_IDWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coEMName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coEMID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coLD_IDWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coNoWF As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WfRepay As System.Windows.Forms.TabPage
    Friend WithEvents dgWF As System.Windows.Forms.DataGridView
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView4 As System.Windows.Forms.DataGridView
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents coNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coLD_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCM_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCM_Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coDes As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coAmtPaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coDatePaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCharge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coAmtToPay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coDateTopay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblEmployee As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents co_DateToPay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_AmtPaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Prn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Int As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Service As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Balnance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Payoff As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_AmtPaid1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Charge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_Des As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents co_DatePaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents lblResultSum As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblResultCount As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblAutoSum As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
