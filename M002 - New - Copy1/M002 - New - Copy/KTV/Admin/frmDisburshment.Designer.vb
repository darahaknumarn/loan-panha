<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDisburshment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDisburshment))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblResultSum = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblResultCount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Column18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coLD_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coEM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coEM_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCM_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCM_Phone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coLD_DisAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCurrency = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coUnit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coTerm = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coIntRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCharge_Rate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coCharge_Amt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coInsurance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coInsuranceTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coLD_Service = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coDisDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coDisDatePay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coPayOff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.coDisDateEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblCustomerID = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblLoanID = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1.SuspendLayout
        Me.Panel6.SuspendLayout
        Me.Panel2.SuspendLayout
        Me.Panel4.SuspendLayout
        CType(Me.DataGridView1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel3.SuspendLayout
        Me.SuspendLayout
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.OrangeRed
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.lblResultSum)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lblResultCount)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.DateTimePicker1)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1924, 44)
        Me.Panel1.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel6.Location = New System.Drawing.Point(1218, 0)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(706, 44)
        Me.Panel6.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = true
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(9, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(696, 21)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "F12: To modify or save, Delete: To delete, Esc: To exit, Ctrl+N: New row, F10: To"& _ 
    " schedule"
        '
        'lblResultSum
        '
        Me.lblResultSum.AutoSize = true
        Me.lblResultSum.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblResultSum.ForeColor = System.Drawing.Color.White
        Me.lblResultSum.Location = New System.Drawing.Point(502, 14)
        Me.lblResultSum.Name = "lblResultSum"
        Me.lblResultSum.Size = New System.Drawing.Size(21, 23)
        Me.lblResultSum.TabIndex = 7
        Me.lblResultSum.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = true
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(452, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 23)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Sum:"
        '
        'lblResultCount
        '
        Me.lblResultCount.AutoSize = true
        Me.lblResultCount.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblResultCount.ForeColor = System.Drawing.Color.White
        Me.lblResultCount.Location = New System.Drawing.Point(392, 14)
        Me.lblResultCount.Name = "lblResultCount"
        Me.lblResultCount.Size = New System.Drawing.Size(21, 23)
        Me.lblResultCount.TabIndex = 5
        Me.lblResultCount.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = true
        Me.Label5.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(329, 13)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Count:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(117, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(108, 26)
        Me.DateTimePicker1.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button1.Font = New System.Drawing.Font("Khmer OS", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"),System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(231, 10)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 28)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "បង្ហាញ"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Button1.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label1.Location = New System.Drawing.Point(1, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "MM/DD/YYYY"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.Panel3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 44)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1924, 295)
        Me.Panel2.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.DataGridView1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1924, 251)
        Me.Panel4.TabIndex = 2
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = false
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column18, Me.coLD_ID, Me.coEM_ID, Me.coEM_Name, Me.coCM_ID, Me.coCM_Name, Me.coCM_Phone, Me.coAddress, Me.coLD_DisAmt, Me.coCurrency, Me.coUnit, Me.coTerm, Me.coIntRate, Me.coType, Me.coCharge_Rate, Me.coCharge_Amt, Me.coInsurance, Me.coInsuranceTotal, Me.coLD_Service, Me.coDisDate, Me.coDisDatePay, Me.coPayOff, Me.coRef, Me.coDisDateEnd})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridView1.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.RowHeadersVisible = false
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DataGridView1.Size = New System.Drawing.Size(1924, 251)
        Me.DataGridView1.TabIndex = 0
        '
        'Column18
        '
        Me.Column18.HeaderText = "...."
        Me.Column18.Name = "Column18"
        Me.Column18.Width = 44
        '
        'coLD_ID
        '
        Me.coLD_ID.HeaderText = "កិច្ចសន្យា"
        Me.coLD_ID.Name = "coLD_ID"
        Me.coLD_ID.Width = 75
        '
        'coEM_ID
        '
        Me.coEM_ID.HeaderText = "កូដបុគ្គលិក"
        Me.coEM_ID.Name = "coEM_ID"
        Me.coEM_ID.Width = 86
        '
        'coEM_Name
        '
        Me.coEM_Name.HeaderText = "ឈ្មោះបុគ្គលិក"
        Me.coEM_Name.Name = "coEM_Name"
        Me.coEM_Name.Width = 94
        '
        'coCM_ID
        '
        Me.coCM_ID.HeaderText = "កូដអតិថិជន"
        Me.coCM_ID.Name = "coCM_ID"
        Me.coCM_ID.Width = 92
        '
        'coCM_Name
        '
        Me.coCM_Name.HeaderText = "ឈ្មោះអតិថិជន"
        Me.coCM_Name.Name = "coCM_Name"
        '
        'coCM_Phone
        '
        Me.coCM_Phone.HeaderText = "លេខទូរស័ព្ទ"
        Me.coCM_Phone.Name = "coCM_Phone"
        Me.coCM_Phone.Width = 86
        '
        'coAddress
        '
        Me.coAddress.HeaderText = "អសយដ្ឋាន"
        Me.coAddress.Name = "coAddress"
        Me.coAddress.Width = 83
        '
        'coLD_DisAmt
        '
        Me.coLD_DisAmt.HeaderText = "ទឹកប្រាក់ខ្ចី"
        Me.coLD_DisAmt.Name = "coLD_DisAmt"
        Me.coLD_DisAmt.Width = 83
        '
        'coCurrency
        '
        Me.coCurrency.HeaderText = "រូបិយវុត្ថុ"
        Me.coCurrency.Name = "coCurrency"
        Me.coCurrency.Width = 69
        '
        'coUnit
        '
        Me.coUnit.HeaderText = "ឯកត្តា"
        Me.coUnit.Name = "coUnit"
        Me.coUnit.Width = 63
        '
        'coTerm
        '
        Me.coTerm.HeaderText = "កាលវិភាគ"
        Me.coTerm.Name = "coTerm"
        Me.coTerm.Width = 82
        '
        'coIntRate
        '
        Me.coIntRate.HeaderText = "ការប្រាក់"
        Me.coIntRate.Name = "coIntRate"
        Me.coIntRate.Width = 74
        '
        'coType
        '
        Me.coType.HeaderText = "ប្រភេទ"
        Me.coType.Name = "coType"
        Me.coType.Width = 65
        '
        'coCharge_Rate
        '
        Me.coCharge_Rate.HeaderText = "ថ្លៃសេវា(%)"
        Me.coCharge_Rate.Name = "coCharge_Rate"
        Me.coCharge_Rate.Width = 80
        '
        'coCharge_Amt
        '
        Me.coCharge_Amt.HeaderText = "ថ្លៃសេវាសរុប"
        Me.coCharge_Amt.Name = "coCharge_Amt"
        Me.coCharge_Amt.Width = 88
        '
        'coInsurance
        '
        Me.coInsurance.HeaderText = "ថ្លៃធានា(%)"
        Me.coInsurance.Name = "coInsurance"
        Me.coInsurance.Width = 85
        '
        'coInsuranceTotal
        '
        Me.coInsuranceTotal.HeaderText = "ថ្លៃធានាសរុប"
        Me.coInsuranceTotal.Name = "coInsuranceTotal"
        Me.coInsuranceTotal.Width = 93
        '
        'coLD_Service
        '
        Me.coLD_Service.HeaderText = "កម្រៃប្រតិបត្តិការ"
        Me.coLD_Service.Name = "coLD_Service"
        Me.coLD_Service.Width = 113
        '
        'coDisDate
        '
        Me.coDisDate.HeaderText = "ថ្ងៃខ្ចី"
        Me.coDisDate.Name = "coDisDate"
        Me.coDisDate.Width = 53
        '
        'coDisDatePay
        '
        Me.coDisDatePay.HeaderText = "ថ្ងៃចាប់ផ្តើម"
        Me.coDisDatePay.Name = "coDisDatePay"
        Me.coDisDatePay.Width = 87
        '
        'coPayOff
        '
        Me.coPayOff.HeaderText = "ហិរញ្ញប្បទាន"
        Me.coPayOff.Name = "coPayOff"
        Me.coPayOff.Width = 89
        '
        'coRef
        '
        Me.coRef.HeaderText = "Ref-Fee"
        Me.coRef.Name = "coRef"
        Me.coRef.Width = 70
        '
        'coDisDateEnd
        '
        Me.coDisDateEnd.HeaderText = "រហូតដល់"
        Me.coDisDateEnd.Name = "coDisDateEnd"
        Me.coDisDateEnd.Width = 73
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.OrangeRed
        Me.Panel3.Controls.Add(Me.lblCustomerID)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.lblLoanID)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 251)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1924, 44)
        Me.Panel3.TabIndex = 1
        '
        'lblCustomerID
        '
        Me.lblCustomerID.AutoSize = true
        Me.lblCustomerID.Font = New System.Drawing.Font("Khmer OS", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblCustomerID.Location = New System.Drawing.Point(580, 6)
        Me.lblCustomerID.Name = "lblCustomerID"
        Me.lblCustomerID.Size = New System.Drawing.Size(0, 32)
        Me.lblCustomerID.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = true
        Me.Label4.Font = New System.Drawing.Font("Khmer OS", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label4.Location = New System.Drawing.Point(385, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(187, 32)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "លេខអតិថិជនចុងក្រោយ:"
        '
        'lblLoanID
        '
        Me.lblLoanID.AutoSize = true
        Me.lblLoanID.Font = New System.Drawing.Font("Khmer OS", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblLoanID.Location = New System.Drawing.Point(190, 6)
        Me.lblLoanID.Name = "lblLoanID"
        Me.lblLoanID.Size = New System.Drawing.Size(0, 32)
        Me.lblLoanID.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Font = New System.Drawing.Font("Khmer OS", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(193, 32)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "លេខកិច្ចសន្យាចុងក្រោយ:"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "1493359111_Apply.ico")
        Me.ImageList1.Images.SetKeyName(1, "1493359123_Delete.ico")
        Me.ImageList1.Images.SetKeyName(2, "1493359147_Sync.ico")
        '
        'frmDisburshment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1924, 339)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmDisburshment"
        Me.Text = "frmDisburshment"
        Me.Panel1.ResumeLayout(false)
        Me.Panel1.PerformLayout
        Me.Panel6.ResumeLayout(false)
        Me.Panel6.PerformLayout
        Me.Panel2.ResumeLayout(false)
        Me.Panel4.ResumeLayout(false)
        CType(Me.DataGridView1,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel3.ResumeLayout(false)
        Me.Panel3.PerformLayout
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCustomerID As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblLoanID As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblResultSum As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblResultCount As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Column18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coLD_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coEM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coEM_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCM_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCM_Phone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coLD_DisAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCurrency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coUnit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coTerm As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coIntRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCharge_Rate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coCharge_Amt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coInsurance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coInsuranceTotal As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coLD_Service As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coDisDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coDisDatePay As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coPayOff As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coRef As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents coDisDateEnd As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
