<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCountry
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pInfo = New System.Windows.Forms.Panel()
        Me.goption = New System.Windows.Forms.GroupBox()
        Me.Search = New System.Windows.Forms.Button()
        Me.btnshowall = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCNameKH = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtCName = New System.Windows.Forms.TextBox()
        Me.B4 = New System.Windows.Forms.Button()
        Me.B1 = New System.Windows.Forms.Button()
        Me.B3 = New System.Windows.Forms.Button()
        Me.B2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCCode = New System.Windows.Forms.TextBox()
        Me.pdetail = New System.Windows.Forms.Panel()
        Me.dgdetail = New System.Windows.Forms.DataGridView()
        Me.CIDD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CountryCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CountryName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CountryNameKH = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pInfo.SuspendLayout()
        Me.goption.SuspendLayout()
        Me.pdetail.SuspendLayout()
        CType(Me.dgdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pInfo
        '
        Me.pInfo.Controls.Add(Me.goption)
        Me.pInfo.Controls.Add(Me.Label3)
        Me.pInfo.Controls.Add(Me.txtCNameKH)
        Me.pInfo.Controls.Add(Me.Label2)
        Me.pInfo.Controls.Add(Me.txtCName)
        Me.pInfo.Controls.Add(Me.B4)
        Me.pInfo.Controls.Add(Me.B1)
        Me.pInfo.Controls.Add(Me.B3)
        Me.pInfo.Controls.Add(Me.B2)
        Me.pInfo.Controls.Add(Me.Label1)
        Me.pInfo.Controls.Add(Me.txtCCode)
        Me.pInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pInfo.Location = New System.Drawing.Point(0, 0)
        Me.pInfo.Name = "pInfo"
        Me.pInfo.Size = New System.Drawing.Size(1261, 195)
        Me.pInfo.TabIndex = 1
        '
        'goption
        '
        Me.goption.Controls.Add(Me.Search)
        Me.goption.Controls.Add(Me.btnshowall)
        Me.goption.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.goption.Location = New System.Drawing.Point(568, 12)
        Me.goption.Name = "goption"
        Me.goption.Size = New System.Drawing.Size(671, 144)
        Me.goption.TabIndex = 30
        Me.goption.TabStop = False
        Me.goption.Text = "Show information options"
        '
        'Search
        '
        Me.Search.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Search.Location = New System.Drawing.Point(569, 46)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(96, 34)
        Me.Search.TabIndex = 28
        Me.Search.Text = "&Search"
        Me.Search.UseVisualStyleBackColor = True
        '
        'btnshowall
        '
        Me.btnshowall.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnshowall.Location = New System.Drawing.Point(569, 102)
        Me.btnshowall.Name = "btnshowall"
        Me.btnshowall.Size = New System.Drawing.Size(96, 34)
        Me.btnshowall.TabIndex = 29
        Me.btnshowall.Text = "&Show All"
        Me.btnshowall.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(41, 133)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(272, 20)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Country Name KH"
        '
        'txtCNameKH
        '
        Me.txtCNameKH.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCNameKH.Location = New System.Drawing.Point(319, 130)
        Me.txtCNameKH.Name = "txtCNameKH"
        Me.txtCNameKH.Size = New System.Drawing.Size(214, 26)
        Me.txtCNameKH.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(41, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(272, 20)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Country Name"
        '
        'txtCName
        '
        Me.txtCName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCName.Location = New System.Drawing.Point(319, 98)
        Me.txtCName.Name = "txtCName"
        Me.txtCName.Size = New System.Drawing.Size(214, 26)
        Me.txtCName.TabIndex = 5
        '
        'B4
        '
        Me.B4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B4.Location = New System.Drawing.Point(347, 12)
        Me.B4.Name = "B4"
        Me.B4.Size = New System.Drawing.Size(96, 34)
        Me.B4.TabIndex = 3
        Me.B4.Text = "E&xit"
        Me.B4.UseVisualStyleBackColor = True
        '
        'B1
        '
        Me.B1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B1.Location = New System.Drawing.Point(41, 12)
        Me.B1.Name = "B1"
        Me.B1.Size = New System.Drawing.Size(96, 34)
        Me.B1.TabIndex = 0
        Me.B1.Text = "&New"
        Me.B1.UseVisualStyleBackColor = True
        '
        'B3
        '
        Me.B3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B3.Location = New System.Drawing.Point(245, 12)
        Me.B3.Name = "B3"
        Me.B3.Size = New System.Drawing.Size(96, 34)
        Me.B3.TabIndex = 2
        Me.B3.Text = "&Delete"
        Me.B3.UseVisualStyleBackColor = True
        '
        'B2
        '
        Me.B2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B2.Location = New System.Drawing.Point(143, 12)
        Me.B2.Name = "B2"
        Me.B2.Size = New System.Drawing.Size(96, 34)
        Me.B2.TabIndex = 1
        Me.B2.Text = "&Edit"
        Me.B2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(41, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 20)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Country Code"
        '
        'txtCCode
        '
        Me.txtCCode.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCCode.Location = New System.Drawing.Point(319, 66)
        Me.txtCCode.Name = "txtCCode"
        Me.txtCCode.Size = New System.Drawing.Size(214, 26)
        Me.txtCCode.TabIndex = 4
        '
        'pdetail
        '
        Me.pdetail.Controls.Add(Me.dgdetail)
        Me.pdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pdetail.Location = New System.Drawing.Point(0, 195)
        Me.pdetail.Name = "pdetail"
        Me.pdetail.Size = New System.Drawing.Size(1261, 205)
        Me.pdetail.TabIndex = 2
        '
        'dgdetail
        '
        Me.dgdetail.AllowUserToAddRows = False
        Me.dgdetail.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgdetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgdetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgdetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CIDD, Me.CountryCode, Me.CountryName, Me.CountryNameKH})
        Me.dgdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgdetail.Location = New System.Drawing.Point(0, 0)
        Me.dgdetail.Name = "dgdetail"
        Me.dgdetail.ReadOnly = True
        Me.dgdetail.RowHeadersVisible = False
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdetail.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgdetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgdetail.Size = New System.Drawing.Size(1261, 205)
        Me.dgdetail.TabIndex = 0
        '
        'CIDD
        '
        Me.CIDD.HeaderText = "CIDD"
        Me.CIDD.Name = "CIDD"
        Me.CIDD.ReadOnly = True
        Me.CIDD.Visible = False
        '
        'CountryCode
        '
        Me.CountryCode.HeaderText = "Country Code"
        Me.CountryCode.Name = "CountryCode"
        Me.CountryCode.ReadOnly = True
        Me.CountryCode.Width = 150
        '
        'CountryName
        '
        Me.CountryName.HeaderText = "Country Name"
        Me.CountryName.Name = "CountryName"
        Me.CountryName.ReadOnly = True
        Me.CountryName.Width = 150
        '
        'CountryNameKH
        '
        Me.CountryNameKH.HeaderText = "Country Name KH"
        Me.CountryNameKH.Name = "CountryNameKH"
        Me.CountryNameKH.ReadOnly = True
        Me.CountryNameKH.Width = 200
        '
        'frmCountry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1261, 400)
        Me.Controls.Add(Me.pdetail)
        Me.Controls.Add(Me.pInfo)
        Me.Name = "frmCountry"
        Me.Text = "ប្រទេស Countries"
        Me.pInfo.ResumeLayout(False)
        Me.pInfo.PerformLayout()
        Me.goption.ResumeLayout(False)
        Me.pdetail.ResumeLayout(False)
        CType(Me.dgdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pInfo As System.Windows.Forms.Panel
    Friend WithEvents goption As System.Windows.Forms.GroupBox
    Friend WithEvents Search As System.Windows.Forms.Button
    Friend WithEvents btnshowall As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCNameKH As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCName As System.Windows.Forms.TextBox
    Friend WithEvents B4 As System.Windows.Forms.Button
    Friend WithEvents B1 As System.Windows.Forms.Button
    Friend WithEvents B3 As System.Windows.Forms.Button
    Friend WithEvents B2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCCode As System.Windows.Forms.TextBox
    Friend WithEvents pdetail As System.Windows.Forms.Panel
    Friend WithEvents dgdetail As System.Windows.Forms.DataGridView
    Friend WithEvents CID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CIDD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CountryCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CountryName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CountryNameKH As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
