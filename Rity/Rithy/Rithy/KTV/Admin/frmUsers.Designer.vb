<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUsers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUsers))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.chkLock = New System.Windows.Forms.CheckBox()
        Me.cboBranch = New System.Windows.Forms.ComboBox()
        Me.txtFullName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.BtnNew = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.dgvUsers = New System.Windows.Forms.DataGridView()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel5.SuspendLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnExit
        '
        Me.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnExit.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.ForeColor = System.Drawing.Color.Red
        Me.BtnExit.Image = CType(resources.GetObject("BtnExit.Image"), System.Drawing.Image)
        Me.BtnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnExit.Location = New System.Drawing.Point(454, 9)
        Me.BtnExit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(142, 51)
        Me.BtnExit.TabIndex = 5
        Me.BtnExit.Text = "ចាកចេញ"
        Me.BtnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'chkLock
        '
        Me.chkLock.AutoSize = True
        Me.chkLock.Enabled = False
        Me.chkLock.Location = New System.Drawing.Point(180, 178)
        Me.chkLock.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkLock.Name = "chkLock"
        Me.chkLock.Size = New System.Drawing.Size(69, 24)
        Me.chkLock.TabIndex = 10
        Me.chkLock.Text = "Lock"
        Me.chkLock.UseVisualStyleBackColor = True
        '
        'cboBranch
        '
        Me.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBranch.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBranch.FormattingEnabled = True
        Me.cboBranch.Location = New System.Drawing.Point(572, 129)
        Me.cboBranch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboBranch.Name = "cboBranch"
        Me.cboBranch.Size = New System.Drawing.Size(202, 44)
        Me.cboBranch.TabIndex = 9
        '
        'txtFullName
        '
        Me.txtFullName.Enabled = False
        Me.txtFullName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFullName.Location = New System.Drawing.Point(572, 85)
        Me.txtFullName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(202, 35)
        Me.txtFullName.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(410, 135)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(150, 35)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Branch:"
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(411, 85)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(150, 35)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Full Name:"
        '
        'txtPassword
        '
        Me.txtPassword.Enabled = False
        Me.txtPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(180, 129)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(202, 35)
        Me.txtPassword.TabIndex = 5
        '
        'BtnNew
        '
        Me.BtnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnNew.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNew.ForeColor = System.Drawing.Color.Navy
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnNew.Location = New System.Drawing.Point(18, 9)
        Me.BtnNew.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(134, 51)
        Me.BtnNew.TabIndex = 2
        Me.BtnNew.Text = "ថ្មី"
        Me.BtnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnNew.UseVisualStyleBackColor = True
        '
        'BtnDelete
        '
        Me.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDelete.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.ForeColor = System.Drawing.Color.Maroon
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDelete.Location = New System.Drawing.Point(309, 9)
        Me.BtnDelete.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(134, 51)
        Me.BtnDelete.TabIndex = 3
        Me.BtnDelete.Text = "លុប"
        Me.BtnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnEdit
        '
        Me.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEdit.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnEdit.ForeColor = System.Drawing.Color.Olive
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnEdit.Location = New System.Drawing.Point(164, 9)
        Me.BtnEdit.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(134, 51)
        Me.BtnEdit.TabIndex = 4
        Me.BtnEdit.Text = "កែប្រែ"
        Me.BtnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnEdit.UseVisualStyleBackColor = True
        '
        'txtUserName
        '
        Me.txtUserName.Enabled = False
        Me.txtUserName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.Location = New System.Drawing.Point(180, 85)
        Me.txtUserName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(202, 35)
        Me.txtUserName.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(18, 135)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(150, 35)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Password:"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 85)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(150, 35)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "User Name:"
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkLock)
        Me.Panel5.Controls.Add(Me.cboBranch)
        Me.Panel5.Controls.Add(Me.txtFullName)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.txtPassword)
        Me.Panel5.Controls.Add(Me.txtUserName)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.dgvUsers)
        Me.Panel5.Controls.Add(Me.Panel3)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1640, 525)
        Me.Panel5.TabIndex = 2
        '
        'dgvUsers
        '
        Me.dgvUsers.AllowUserToAddRows = False
        Me.dgvUsers.AllowUserToDeleteRows = False
        Me.dgvUsers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Khmer OS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvUsers.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvUsers.Location = New System.Drawing.Point(-4, 232)
        Me.dgvUsers.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgvUsers.Name = "dgvUsers"
        Me.dgvUsers.ReadOnly = True
        Me.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvUsers.Size = New System.Drawing.Size(1640, 306)
        Me.dgvUsers.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.BtnNew)
        Me.Panel3.Controls.Add(Me.BtnDelete)
        Me.Panel3.Controls.Add(Me.BtnEdit)
        Me.Panel3.Controls.Add(Me.BtnExit)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1640, 73)
        Me.Panel3.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1640, 525)
        Me.Panel2.TabIndex = 9
        '
        'frmUsers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1640, 525)
        Me.Controls.Add(Me.Panel2)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmUsers"
        Me.Text = "frmUsers"
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        CType(Me.dgvUsers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents chkLock As System.Windows.Forms.CheckBox
    Friend WithEvents cboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents txtFullName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents BtnNew As System.Windows.Forms.Button
    Friend WithEvents BtnDelete As System.Windows.Forms.Button
    Friend WithEvents BtnEdit As System.Windows.Forms.Button
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents dgvUsers As System.Windows.Forms.DataGridView
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
