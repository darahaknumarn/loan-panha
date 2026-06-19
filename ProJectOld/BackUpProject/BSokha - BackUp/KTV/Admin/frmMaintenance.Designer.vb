<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaintenance
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
        Me.flowaction = New System.Windows.Forms.FlowLayoutPanel()
        Me.buser = New System.Windows.Forms.Button()
        Me.brole = New System.Windows.Forms.Button()
        Me.flowaction.SuspendLayout()
        Me.SuspendLayout()
        '
        'flowaction
        '
        Me.flowaction.Controls.Add(Me.buser)
        Me.flowaction.Controls.Add(Me.brole)
        Me.flowaction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowaction.Location = New System.Drawing.Point(0, 0)
        Me.flowaction.Name = "flowaction"
        Me.flowaction.Size = New System.Drawing.Size(919, 467)
        Me.flowaction.TabIndex = 0
        '
        'buser
        '
        Me.buser.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.buser.Location = New System.Drawing.Point(3, 3)
        Me.buser.Name = "buser"
        Me.buser.Size = New System.Drawing.Size(173, 75)
        Me.buser.TabIndex = 8
        Me.buser.Text = "User Account"
        Me.buser.UseVisualStyleBackColor = True
        '
        'brole
        '
        Me.brole.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.brole.ForeColor = System.Drawing.Color.Red
        Me.brole.Location = New System.Drawing.Point(182, 3)
        Me.brole.Name = "brole"
        Me.brole.Size = New System.Drawing.Size(103, 75)
        Me.brole.TabIndex = 9
        Me.brole.Text = "Clear Database"
        Me.brole.UseVisualStyleBackColor = True
        '
        'frmMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(919, 467)
        Me.Controls.Add(Me.flowaction)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmMaintenance"
        Me.Text = "ការគ្រប់គ្រង Items controllers"
        Me.flowaction.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents flowaction As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents brole As System.Windows.Forms.Button
    Friend WithEvents buser As System.Windows.Forms.Button
End Class
