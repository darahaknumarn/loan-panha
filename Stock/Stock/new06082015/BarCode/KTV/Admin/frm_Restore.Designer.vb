<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_Restore
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
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RestoreDatabaseOFD = New System.Windows.Forms.OpenFileDialog()
        Me.cbDatabseDatabase = New System.Windows.Forms.ComboBox()
        Me.btnRestore = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtdestination = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.SteelBlue
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.Font = New System.Drawing.Font("Khmer OS", 20.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(20, 3, 3, 3)
        Me.Label4.Size = New System.Drawing.Size(658, 76)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "បញ្ចូលទិន្នន័យតាមសាខា"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'RestoreDatabaseOFD
        '
        Me.RestoreDatabaseOFD.FileName = "OpenFileDialog1"
        '
        'cbDatabseDatabase
        '
        Me.cbDatabseDatabase.FormattingEnabled = True
        Me.cbDatabseDatabase.Items.AddRange(New Object() {"BarCode"})
        Me.cbDatabseDatabase.Location = New System.Drawing.Point(193, 92)
        Me.cbDatabseDatabase.Name = "cbDatabseDatabase"
        Me.cbDatabseDatabase.Size = New System.Drawing.Size(106, 27)
        Me.cbDatabseDatabase.TabIndex = 13
        '
        'btnRestore
        '
        Me.btnRestore.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRestore.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnRestore.Location = New System.Drawing.Point(363, 164)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(103, 41)
        Me.btnRestore.TabIndex = 12
        Me.btnRestore.Text = "រក្សាទុក"
        Me.btnRestore.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Font = New System.Drawing.Font("Khmer OS", 9.0!)
        Me.btnBrowse.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.btnBrowse.Location = New System.Drawing.Point(190, 164)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(167, 41)
        Me.btnBrowse.TabIndex = 11
        Me.btnBrowse.Text = "ស្វែងរកទីតាំងរក្សាទុក"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Khmer OS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.Location = New System.Drawing.Point(12, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(180, 40)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "ទីតាំងទុកទិន្នន័យ :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Khmer OS", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.Location = New System.Drawing.Point(26, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(169, 44)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "ឈ្មោះទិន្នន័យ :"
        '
        'txtdestination
        '
        Me.txtdestination.Location = New System.Drawing.Point(192, 130)
        Me.txtdestination.Name = "txtdestination"
        Me.txtdestination.Size = New System.Drawing.Size(272, 27)
        Me.txtdestination.TabIndex = 10
        '
        'frm_Restore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(658, 304)
        Me.Controls.Add(Me.cbDatabseDatabase)
        Me.Controls.Add(Me.btnRestore)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtdestination)
        Me.Controls.Add(Me.Label4)
        Me.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frm_Restore"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Restore"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents RestoreDatabaseOFD As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cbDatabseDatabase As System.Windows.Forms.ComboBox
    Friend WithEvents btnRestore As System.Windows.Forms.Button
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtdestination As System.Windows.Forms.TextBox
End Class
