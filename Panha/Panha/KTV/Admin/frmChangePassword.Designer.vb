<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangePassword
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
        Me.pcenter = New System.Windows.Forms.Panel()
        Me.btnexit = New System.Windows.Forms.Button()
        Me.btnapply = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtnewverify = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtnew = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtold = New System.Windows.Forms.TextBox()
        Me.pcenter.SuspendLayout()
        Me.SuspendLayout()
        '
        'pcenter
        '
        Me.pcenter.Controls.Add(Me.btnexit)
        Me.pcenter.Controls.Add(Me.btnapply)
        Me.pcenter.Controls.Add(Me.Label3)
        Me.pcenter.Controls.Add(Me.txtnewverify)
        Me.pcenter.Controls.Add(Me.Label2)
        Me.pcenter.Controls.Add(Me.txtnew)
        Me.pcenter.Controls.Add(Me.Label1)
        Me.pcenter.Controls.Add(Me.txtold)
        Me.pcenter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pcenter.Location = New System.Drawing.Point(0, 0)
        Me.pcenter.Name = "pcenter"
        Me.pcenter.Size = New System.Drawing.Size(626, 202)
        Me.pcenter.TabIndex = 0
        '
        'btnexit
        '
        Me.btnexit.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnexit.Image = Global.Panha.My.Resources.Resources.OOFL
        Me.btnexit.Location = New System.Drawing.Point(483, 165)
        Me.btnexit.Name = "btnexit"
        Me.btnexit.Size = New System.Drawing.Size(55, 34)
        Me.btnexit.TabIndex = 27
        Me.btnexit.UseVisualStyleBackColor = True
        '
        'btnapply
        '
        Me.btnapply.Font = New System.Drawing.Font("Khmer OS Battambang", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnapply.Location = New System.Drawing.Point(366, 165)
        Me.btnapply.Name = "btnapply"
        Me.btnapply.Size = New System.Drawing.Size(96, 34)
        Me.btnapply.TabIndex = 13
        Me.btnapply.Text = "ផ្លាស់ប្តូរ"
        Me.btnapply.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Khmer OS Battambang", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(164, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(171, 29)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "បញ្ជាក់លេខសំងាត់ថ្មីជាថ្មី"
        '
        'txtnewverify
        '
        Me.txtnewverify.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnewverify.Location = New System.Drawing.Point(338, 131)
        Me.txtnewverify.Name = "txtnewverify"
        Me.txtnewverify.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtnewverify.Size = New System.Drawing.Size(200, 26)
        Me.txtnewverify.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Khmer OS Battambang", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(164, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 29)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "លេខសំងាត់ថ្មី"
        '
        'txtnew
        '
        Me.txtnew.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnew.Location = New System.Drawing.Point(338, 99)
        Me.txtnew.Name = "txtnew"
        Me.txtnew.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtnew.Size = New System.Drawing.Size(200, 26)
        Me.txtnew.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Khmer OS Battambang", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(164, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 29)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "លេខសំងាត់ចាស់"
        '
        'txtold
        '
        Me.txtold.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtold.Location = New System.Drawing.Point(338, 67)
        Me.txtold.Name = "txtold"
        Me.txtold.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtold.Size = New System.Drawing.Size(200, 26)
        Me.txtold.TabIndex = 8
        '
        'frmChangePassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(626, 245)
        Me.Controls.Add(Me.pcenter)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MinimizeBox = False
        Me.Name = "frmChangePassword"
        Me.Text = "ប្តូរលេខសំងាត់"
        Me.pcenter.ResumeLayout(False)
        Me.pcenter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pcenter As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtnewverify As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtnew As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtold As System.Windows.Forms.TextBox
    Friend WithEvents btnapply As System.Windows.Forms.Button
    Friend WithEvents btnexit As System.Windows.Forms.Button
End Class
