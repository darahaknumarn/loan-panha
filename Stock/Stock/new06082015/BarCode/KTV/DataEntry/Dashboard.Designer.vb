<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dashboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dashboard))
        Me.lbltick = New System.Windows.Forms.Label()
        Me.lblname = New System.Windows.Forms.Label()
        Me.lblposition = New System.Windows.Forms.Label()
        Me.lblCountry = New System.Windows.Forms.Label()
        Me.lblmeetingName = New System.Windows.Forms.Label()
        Me.txtinput = New System.Windows.Forms.TextBox()
        Me.imgphoto = New System.Windows.Forms.PictureBox()
        Me.pic = New System.Windows.Forms.PictureBox()
        Me.labelfix1 = New System.Windows.Forms.Label()
        CType(Me.imgphoto, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lbltick
        '
        Me.lbltick.AutoSize = True
        Me.lbltick.Font = New System.Drawing.Font("Arial Black", 38.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltick.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lbltick.Location = New System.Drawing.Point(166, 593)
        Me.lbltick.Name = "lbltick"
        Me.lbltick.Size = New System.Drawing.Size(64, 72)
        Me.lbltick.TabIndex = 35
        Me.lbltick.Text = "2"
        Me.lbltick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblname
        '
        Me.lblname.BackColor = System.Drawing.Color.Transparent
        Me.lblname.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblname.ForeColor = System.Drawing.Color.Black
        Me.lblname.Location = New System.Drawing.Point(522, 237)
        Me.lblname.Name = "lblname"
        Me.lblname.Size = New System.Drawing.Size(821, 81)
        Me.lblname.TabIndex = 34
        Me.lblname.Text = "Johnson Sam"
        Me.lblname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblposition
        '
        Me.lblposition.BackColor = System.Drawing.Color.LimeGreen
        Me.lblposition.Font = New System.Drawing.Font("Microsoft Sans Serif", 48.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblposition.ForeColor = System.Drawing.Color.Black
        Me.lblposition.Location = New System.Drawing.Point(522, 499)
        Me.lblposition.Name = "lblposition"
        Me.lblposition.Size = New System.Drawing.Size(821, 91)
        Me.lblposition.TabIndex = 33
        Me.lblposition.Text = "Secretary of state"
        Me.lblposition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCountry
        '
        Me.lblCountry.BackColor = System.Drawing.Color.Transparent
        Me.lblCountry.Font = New System.Drawing.Font("Microsoft Sans Serif", 50.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountry.ForeColor = System.Drawing.Color.Black
        Me.lblCountry.Location = New System.Drawing.Point(528, 332)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(821, 80)
        Me.lblCountry.TabIndex = 32
        Me.lblCountry.Text = "United State of America"
        Me.lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblmeetingName
        '
        Me.lblmeetingName.BackColor = System.Drawing.Color.Transparent
        Me.lblmeetingName.Font = New System.Drawing.Font("Microsoft Sans Serif", 42.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmeetingName.ForeColor = System.Drawing.Color.Black
        Me.lblmeetingName.Location = New System.Drawing.Point(2, 21)
        Me.lblmeetingName.Name = "lblmeetingName"
        Me.lblmeetingName.Size = New System.Drawing.Size(1136, 78)
        Me.lblmeetingName.TabIndex = 31
        Me.lblmeetingName.Text = "ASEAN FOREIGN MINISTERS RETREAT"
        Me.lblmeetingName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtinput
        '
        Me.txtinput.Location = New System.Drawing.Point(2, 458)
        Me.txtinput.Name = "txtinput"
        Me.txtinput.PasswordChar = Global.Microsoft.VisualBasic.ChrW(36)
        Me.txtinput.Size = New System.Drawing.Size(10, 20)
        Me.txtinput.TabIndex = 30
        '
        'imgphoto
        '
        Me.imgphoto.Image = Global.CamITSo.My.Resources.Resources.lifestyle
        Me.imgphoto.Location = New System.Drawing.Point(27, 207)
        Me.imgphoto.Name = "imgphoto"
        Me.imgphoto.Size = New System.Drawing.Size(342, 383)
        Me.imgphoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgphoto.TabIndex = 37
        Me.imgphoto.TabStop = False
        '
        'pic
        '
        Me.pic.Image = CType(resources.GetObject("pic.Image"), System.Drawing.Image)
        Me.pic.Location = New System.Drawing.Point(1144, 0)
        Me.pic.Name = "pic"
        Me.pic.Size = New System.Drawing.Size(199, 189)
        Me.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pic.TabIndex = 38
        Me.pic.TabStop = False
        '
        'labelfix1
        '
        Me.labelfix1.AutoSize = True
        Me.labelfix1.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labelfix1.Location = New System.Drawing.Point(252, 113)
        Me.labelfix1.Name = "labelfix1"
        Me.labelfix1.Size = New System.Drawing.Size(636, 37)
        Me.labelfix1.TabIndex = 39
        Me.labelfix1.Text = "Siem Reap, Cambodia, 10-12 January 2012"
        '
        'Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.AntiqueWhite
        Me.ClientSize = New System.Drawing.Size(1361, 648)
        Me.Controls.Add(Me.labelfix1)
        Me.Controls.Add(Me.pic)
        Me.Controls.Add(Me.imgphoto)
        Me.Controls.Add(Me.lbltick)
        Me.Controls.Add(Me.lblname)
        Me.Controls.Add(Me.lblposition)
        Me.Controls.Add(Me.lblCountry)
        Me.Controls.Add(Me.lblmeetingName)
        Me.Controls.Add(Me.txtinput)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Dashboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Wall Board"
        Me.TopMost = True
        CType(Me.imgphoto, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents imgphoto As System.Windows.Forms.PictureBox
    Friend WithEvents lbltick As System.Windows.Forms.Label
    Friend WithEvents lblname As System.Windows.Forms.Label
    Friend WithEvents lblposition As System.Windows.Forms.Label
    Friend WithEvents lblCountry As System.Windows.Forms.Label
    Friend WithEvents lblmeetingName As System.Windows.Forms.Label
    Friend WithEvents txtinput As System.Windows.Forms.TextBox
    Friend WithEvents pic As System.Windows.Forms.PictureBox
    Friend WithEvents labelfix1 As System.Windows.Forms.Label
End Class
