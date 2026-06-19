<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReport
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblEnd = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lblEmID = New System.Windows.Forms.Label()
        Me.lblCurrency = New System.Windows.Forms.Label()
        Me.cboCurrency = New System.Windows.Forms.ComboBox()
        Me.lblEmployee = New System.Windows.Forms.Label()
        Me.cboEmployee = New System.Windows.Forms.ComboBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboBranch = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblBranch = New System.Windows.Forms.Label()
        Me.cboWF = New System.Windows.Forms.ComboBox()
        Me.lblWF = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Khmer OS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(23, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 28)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Start Date:"
        '
        'lblEnd
        '
        Me.lblEnd.AutoSize = True
        Me.lblEnd.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblEnd.Location = New System.Drawing.Point(23, 95)
        Me.lblEnd.Name = "lblEnd"
        Me.lblEnd.Size = New System.Drawing.Size(70, 28)
        Me.lblEnd.TabIndex = 2
        Me.lblEnd.Text = "End Date:"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(147, 53)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(116, 26)
        Me.DateTimePicker1.TabIndex = 3
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(147, 91)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(116, 26)
        Me.DateTimePicker2.TabIndex = 4
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Purple
        Me.Button1.Location = New System.Drawing.Point(128, 311)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 38)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Show"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Khmer OS", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Red
        Me.Button2.Location = New System.Drawing.Point(230, 311)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 38)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Exit"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'lblEmID
        '
        Me.lblEmID.AutoSize = True
        Me.lblEmID.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblEmID.Location = New System.Drawing.Point(23, 132)
        Me.lblEmID.Name = "lblEmID"
        Me.lblEmID.Size = New System.Drawing.Size(74, 28)
        Me.lblEmID.TabIndex = 7
        Me.lblEmID.Text = "Employee:"
        '
        'lblCurrency
        '
        Me.lblCurrency.AutoSize = True
        Me.lblCurrency.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCurrency.Location = New System.Drawing.Point(23, 175)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(68, 28)
        Me.lblCurrency.TabIndex = 8
        Me.lblCurrency.Text = "Currency:"
        '
        'cboCurrency
        '
        Me.cboCurrency.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCurrency.FormattingEnabled = True
        Me.cboCurrency.Items.AddRange(New Object() {"All", "រៀល", "ដុល្លារ"})
        Me.cboCurrency.Location = New System.Drawing.Point(147, 174)
        Me.cboCurrency.Name = "cboCurrency"
        Me.cboCurrency.Size = New System.Drawing.Size(85, 32)
        Me.cboCurrency.TabIndex = 10
        '
        'lblEmployee
        '
        Me.lblEmployee.AutoSize = True
        Me.lblEmployee.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployee.Location = New System.Drawing.Point(238, 132)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(0, 28)
        Me.lblEmployee.TabIndex = 11
        '
        'cboEmployee
        '
        Me.cboEmployee.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmployee.FormattingEnabled = True
        Me.cboEmployee.Items.AddRange(New Object() {"All", "រៀល", "ដុល្លារ"})
        Me.cboEmployee.Location = New System.Drawing.Point(147, 131)
        Me.cboEmployee.Name = "cboEmployee"
        Me.cboEmployee.Size = New System.Drawing.Size(85, 32)
        Me.cboEmployee.TabIndex = 12
        '
        'Timer1
        '
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(91, 362)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(235, 23)
        Me.ProgressBar1.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 12.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(160, 388)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 19)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Please wait..."
        '
        'cboBranch
        '
        Me.cboBranch.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBranch.FormattingEnabled = True
        Me.cboBranch.Items.AddRange(New Object() {"All", "រៀល", "ដុល្លារ"})
        Me.cboBranch.Location = New System.Drawing.Point(147, 219)
        Me.cboBranch.Name = "cboBranch"
        Me.cboBranch.Size = New System.Drawing.Size(85, 32)
        Me.cboBranch.TabIndex = 16
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(23, 219)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 28)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Branch:"
        '
        'lblBranch
        '
        Me.lblBranch.AutoSize = True
        Me.lblBranch.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBranch.Location = New System.Drawing.Point(238, 219)
        Me.lblBranch.Name = "lblBranch"
        Me.lblBranch.Size = New System.Drawing.Size(0, 28)
        Me.lblBranch.TabIndex = 17
        '
        'cboWF
        '
        Me.cboWF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWF.Font = New System.Drawing.Font("Khmer OS", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboWF.FormattingEnabled = True
        Me.cboWF.Items.AddRange(New Object() {"All", "WriteOff Only", "Without WriteOff"})
        Me.cboWF.Location = New System.Drawing.Point(147, 269)
        Me.cboWF.Name = "cboWF"
        Me.cboWF.Size = New System.Drawing.Size(116, 32)
        Me.cboWF.TabIndex = 19
        '
        'lblWF
        '
        Me.lblWF.AutoSize = True
        Me.lblWF.Font = New System.Drawing.Font("Khmer Kep", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblWF.Location = New System.Drawing.Point(23, 269)
        Me.lblWF.Name = "lblWF"
        Me.lblWF.Size = New System.Drawing.Size(106, 28)
        Me.lblWF.TabIndex = 18
        Me.lblWF.Text = "WriteOff Option:"
        '
        'frmReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(407, 416)
        Me.Controls.Add(Me.cboWF)
        Me.Controls.Add(Me.lblWF)
        Me.Controls.Add(Me.lblBranch)
        Me.Controls.Add(Me.cboBranch)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.cboEmployee)
        Me.Controls.Add(Me.lblEmployee)
        Me.Controls.Add(Me.cboCurrency)
        Me.Controls.Add(Me.lblCurrency)
        Me.Controls.Add(Me.lblEmID)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.lblEnd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmReport"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblEnd As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents lblEmID As System.Windows.Forms.Label
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents cboCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents lblEmployee As System.Windows.Forms.Label
    Friend WithEvents cboEmployee As System.Windows.Forms.ComboBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboBranch As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblBranch As System.Windows.Forms.Label
    Friend WithEvents cboWF As System.Windows.Forms.ComboBox
    Friend WithEvents lblWF As System.Windows.Forms.Label
End Class
