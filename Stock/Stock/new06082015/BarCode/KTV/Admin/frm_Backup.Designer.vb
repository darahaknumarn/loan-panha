<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Backup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_Backup))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Backup_SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.txtBrowse = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBackup = New System.Windows.Forms.Button()
        Me.cbDatabseDatabase = New System.Windows.Forms.ComboBox()
        Me.txtdestination = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Khmer OS", 12.0!)
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(24, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(153, 40)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "ឈ្មោះទិន្នន័យ :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Khmer OS", 12.0!)
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(2, 126)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(174, 40)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "ទីតាំងទុកទិន្នន័យ:"
        '
        'txtBrowse
        '
        Me.txtBrowse.Font = New System.Drawing.Font("Khmer OS", 9.0!)
        Me.txtBrowse.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.txtBrowse.Location = New System.Drawing.Point(174, 167)
        Me.txtBrowse.Name = "txtBrowse"
        Me.txtBrowse.Size = New System.Drawing.Size(120, 34)
        Me.txtBrowse.TabIndex = 5
        Me.txtBrowse.Text = "ស្វែងរកទីតាំង"
        Me.txtBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtBrowse.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.SteelBlue
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Khmer OS Muol Light", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(20, 3, 3, 3)
        Me.Label4.Size = New System.Drawing.Size(837, 69)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "ទំរង់ថតទិន្នន័យទុក"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBackup
        '
        Me.txtBackup.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackup.ForeColor = System.Drawing.Color.Magenta
        Me.txtBackup.Location = New System.Drawing.Point(300, 167)
        Me.txtBackup.Name = "txtBackup"
        Me.txtBackup.Size = New System.Drawing.Size(143, 34)
        Me.txtBackup.TabIndex = 6
        Me.txtBackup.Text = "តំណើរការរក្សាទុក"
        Me.txtBackup.UseVisualStyleBackColor = True
        '
        'cbDatabseDatabase
        '
        Me.cbDatabseDatabase.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbDatabseDatabase.FormattingEnabled = True
        Me.cbDatabseDatabase.Items.AddRange(New Object() {"Barcode"})
        Me.cbDatabseDatabase.Location = New System.Drawing.Point(174, 85)
        Me.cbDatabseDatabase.Name = "cbDatabseDatabase"
        Me.cbDatabseDatabase.Size = New System.Drawing.Size(120, 35)
        Me.cbDatabseDatabase.TabIndex = 7
        '
        'txtdestination
        '
        Me.txtdestination.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdestination.Location = New System.Drawing.Point(174, 126)
        Me.txtdestination.Name = "txtdestination"
        Me.txtdestination.Size = New System.Drawing.Size(269, 35)
        Me.txtdestination.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Khmer OS", 12.0!)
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(12, 211)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 40)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "ចំណាំ:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Khmer OS", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label5.Location = New System.Drawing.Point(77, 214)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(720, 34)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "រាល់ការថតទិន្នន័យទុក សូមរក្សាទុកក្នុង ទីតាំងណាមួយដែលស្ថិតក្នុង Drive ""D"" ។ សូមអរគ" & _
    "ុណ...!"
        '
        'frm_Backup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(837, 408)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbDatabseDatabase)
        Me.Controls.Add(Me.txtBackup)
        Me.Controls.Add(Me.txtBrowse)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtdestination)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frm_Backup"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Backup_SaveFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents txtBrowse As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtBackup As System.Windows.Forms.Button
    Friend WithEvents cbDatabseDatabase As System.Windows.Forms.ComboBox
    Friend WithEvents txtdestination As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
