<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmrole
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.B4 = New System.Windows.Forms.Button()
        Me.B1 = New System.Windows.Forms.Button()
        Me.B3 = New System.Windows.Forms.Button()
        Me.B2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pInfo = New System.Windows.Forms.Panel()
        Me.txtrole = New System.Windows.Forms.TextBox()
        Me.paction = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dgrole = New System.Windows.Forms.DataGridView()
        Me.RolesID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Roles = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Status = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pInfo.SuspendLayout()
        Me.paction.SuspendLayout()
        CType(Me.dgrole, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'B4
        '
        Me.B4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B4.Location = New System.Drawing.Point(337, 6)
        Me.B4.Name = "B4"
        Me.B4.Size = New System.Drawing.Size(96, 34)
        Me.B4.TabIndex = 15
        Me.B4.Text = "ចាកចេញ"
        Me.B4.UseVisualStyleBackColor = True
        '
        'B1
        '
        Me.B1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B1.Location = New System.Drawing.Point(31, 6)
        Me.B1.Name = "B1"
        Me.B1.Size = New System.Drawing.Size(96, 34)
        Me.B1.TabIndex = 12
        Me.B1.Text = "បញ្ជូលថ្មី"
        Me.B1.UseVisualStyleBackColor = True
        '
        'B3
        '
        Me.B3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B3.Location = New System.Drawing.Point(235, 6)
        Me.B3.Name = "B3"
        Me.B3.Size = New System.Drawing.Size(96, 34)
        Me.B3.TabIndex = 14
        Me.B3.Text = "លុបចោល"
        Me.B3.UseVisualStyleBackColor = True
        '
        'B2
        '
        Me.B2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B2.Location = New System.Drawing.Point(133, 6)
        Me.B2.Name = "B2"
        Me.B2.Size = New System.Drawing.Size(96, 34)
        Me.B2.TabIndex = 13
        Me.B2.Text = "កែរប្រែ"
        Me.B2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(37, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 20)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "តួនាទី (role)"
        '
        'pInfo
        '
        Me.pInfo.Controls.Add(Me.B4)
        Me.pInfo.Controls.Add(Me.B1)
        Me.pInfo.Controls.Add(Me.B3)
        Me.pInfo.Controls.Add(Me.B2)
        Me.pInfo.Controls.Add(Me.Label1)
        Me.pInfo.Controls.Add(Me.txtrole)
        Me.pInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pInfo.Location = New System.Drawing.Point(0, 40)
        Me.pInfo.Name = "pInfo"
        Me.pInfo.Size = New System.Drawing.Size(776, 106)
        Me.pInfo.TabIndex = 27
        '
        'txtrole
        '
        Me.txtrole.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtrole.Location = New System.Drawing.Point(180, 57)
        Me.txtrole.Name = "txtrole"
        Me.txtrole.Size = New System.Drawing.Size(253, 26)
        Me.txtrole.TabIndex = 6
        '
        'paction
        '
        Me.paction.Controls.Add(Me.Label7)
        Me.paction.Dock = System.Windows.Forms.DockStyle.Top
        Me.paction.Location = New System.Drawing.Point(0, 0)
        Me.paction.Name = "paction"
        Me.paction.Size = New System.Drawing.Size(776, 40)
        Me.paction.TabIndex = 25
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(0, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(141, 24)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "តារាងតួនាទី (role)"
        '
        'dgrole
        '
        Me.dgrole.AllowUserToAddRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrole.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgrole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgrole.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RolesID, Me.Roles, Me.Description, Me.Status})
        Me.dgrole.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgrole.Location = New System.Drawing.Point(0, 146)
        Me.dgrole.Name = "dgrole"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgrole.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgrole.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgrole.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgrole.Size = New System.Drawing.Size(776, 332)
        Me.dgrole.TabIndex = 28
        '
        'RolesID
        '
        Me.RolesID.HeaderText = "​លេខកូដតួនាទី Roles ID"
        Me.RolesID.Name = "RolesID"
        '
        'Roles
        '
        Me.Roles.HeaderText = "តួនាទី Role"
        Me.Roles.Name = "Roles"
        '
        'Description
        '
        Me.Description.HeaderText = "ផ្សេងៗ Description"
        Me.Description.Name = "Description"
        '
        'Status
        '
        Me.Status.HeaderText = "ស្ថានភាព Status"
        Me.Status.Name = "Status"
        '
        'frmrole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 478)
        Me.Controls.Add(Me.dgrole)
        Me.Controls.Add(Me.pInfo)
        Me.Controls.Add(Me.paction)
        Me.Name = "frmrole"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "តួនាទី (role)"
        Me.pInfo.ResumeLayout(False)
        Me.pInfo.PerformLayout()
        Me.paction.ResumeLayout(False)
        Me.paction.PerformLayout()
        CType(Me.dgrole, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents B4 As System.Windows.Forms.Button
    Friend WithEvents B1 As System.Windows.Forms.Button
    Friend WithEvents B3 As System.Windows.Forms.Button
    Friend WithEvents B2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pInfo As System.Windows.Forms.Panel
    Friend WithEvents txtrole As System.Windows.Forms.TextBox
    Friend WithEvents paction As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dgrole As System.Windows.Forms.DataGridView
    Friend WithEvents RolesID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Roles As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Status As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
