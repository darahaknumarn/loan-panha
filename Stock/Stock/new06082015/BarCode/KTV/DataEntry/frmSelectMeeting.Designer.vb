<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectMeeting
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
        Me.B4 = New System.Windows.Forms.Button()
        Me.cbomeeting = New System.Windows.Forms.ComboBox()
        Me.bmeeting = New System.Windows.Forms.BindingSource(Me.components)
        Me.Sales1 = New CamITSo.Sales()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnchoose = New System.Windows.Forms.Button()
        Me.TblMeetingTableAdapter = New CamITSo.SalesTableAdapters.tblMeetingTableAdapter()
        CType(Me.bmeeting, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Sales1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'B4
        '
        Me.B4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.B4.Location = New System.Drawing.Point(531, 138)
        Me.B4.Name = "B4"
        Me.B4.Size = New System.Drawing.Size(96, 34)
        Me.B4.TabIndex = 4
        Me.B4.Text = "&Exit"
        Me.B4.UseVisualStyleBackColor = True
        '
        'cbomeeting
        '
        Me.cbomeeting.DataSource = Me.bmeeting
        Me.cbomeeting.DisplayMember = "MName"
        Me.cbomeeting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbomeeting.FormattingEnabled = True
        Me.cbomeeting.Location = New System.Drawing.Point(32, 72)
        Me.cbomeeting.Name = "cbomeeting"
        Me.cbomeeting.Size = New System.Drawing.Size(595, 28)
        Me.cbomeeting.TabIndex = 5
        Me.cbomeeting.ValueMember = "MID"
        '
        'bmeeting
        '
        Me.bmeeting.DataMember = "tblMeeting"
        Me.bmeeting.DataSource = Me.Sales1
        '
        'Sales1
        '
        Me.Sales1.DataSetName = "Sales"
        Me.Sales1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(28, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(272, 20)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "&Meeting Name"
        '
        'btnchoose
        '
        Me.btnchoose.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnchoose.Location = New System.Drawing.Point(272, 138)
        Me.btnchoose.Name = "btnchoose"
        Me.btnchoose.Size = New System.Drawing.Size(221, 34)
        Me.btnchoose.TabIndex = 19
        Me.btnchoose.Text = "&Select the meeting"
        Me.btnchoose.UseVisualStyleBackColor = True
        '
        'TblMeetingTableAdapter
        '
        Me.TblMeetingTableAdapter.ClearBeforeFill = True
        '
        'frmSelectMeeting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(654, 188)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnchoose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbomeeting)
        Me.Controls.Add(Me.B4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmSelectMeeting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ជ្រើសរើសការប្រជុំ Select meeting name to record"
        Me.TopMost = True
        CType(Me.bmeeting, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Sales1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents B4 As System.Windows.Forms.Button
    Friend WithEvents cbomeeting As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnchoose As System.Windows.Forms.Button
    Friend WithEvents bmeeting As System.Windows.Forms.BindingSource
    Friend WithEvents Sales1 As CamITSo.Sales
    Friend WithEvents TblMeetingTableAdapter As CamITSo.SalesTableAdapters.tblMeetingTableAdapter
End Class
