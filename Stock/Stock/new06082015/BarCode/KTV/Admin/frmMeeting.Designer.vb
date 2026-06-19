<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMeeting
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pInfo = New System.Windows.Forms.Panel()
        Me.dStop = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dstart = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMeetingVenue = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtMeetingName = New System.Windows.Forms.TextBox()
        Me.B4 = New System.Windows.Forms.Button()
        Me.B1 = New System.Windows.Forms.Button()
        Me.B3 = New System.Windows.Forms.Button()
        Me.B2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgdetail = New System.Windows.Forms.DataGridView()
        Me.CIDD = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MeetingStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MeetingEnd = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MeetingName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.meetingVenue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pInfo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgdetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pInfo
        '
        Me.pInfo.Controls.Add(Me.dStop)
        Me.pInfo.Controls.Add(Me.Label5)
        Me.pInfo.Controls.Add(Me.dstart)
        Me.pInfo.Controls.Add(Me.Label4)
        Me.pInfo.Controls.Add(Me.Label3)
        Me.pInfo.Controls.Add(Me.txtMeetingVenue)
        Me.pInfo.Controls.Add(Me.Label2)
        Me.pInfo.Controls.Add(Me.txtMeetingName)
        Me.pInfo.Controls.Add(Me.B4)
        Me.pInfo.Controls.Add(Me.B1)
        Me.pInfo.Controls.Add(Me.B3)
        Me.pInfo.Controls.Add(Me.B2)
        Me.pInfo.Controls.Add(Me.Label1)
        Me.pInfo.Controls.Add(Me.txtDesc)
        Me.pInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pInfo.Location = New System.Drawing.Point(0, 0)
        Me.pInfo.Name = "pInfo"
        Me.pInfo.Size = New System.Drawing.Size(1261, 377)
        Me.pInfo.TabIndex = 3
        '
        'dStop
        '
        Me.dStop.CustomFormat = "dd-MMM-yyyy hh:mm:ss tt"
        Me.dStop.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dStop.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dStop.Location = New System.Drawing.Point(319, 104)
        Me.dStop.Name = "dStop"
        Me.dStop.Size = New System.Drawing.Size(363, 23)
        Me.dStop.TabIndex = 34
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(41, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(272, 20)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Meeting date end"
        '
        'dstart
        '
        Me.dstart.CustomFormat = "dd-MMM-yyyy hh:mm:ss tt"
        Me.dstart.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dstart.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dstart.Location = New System.Drawing.Point(319, 69)
        Me.dstart.Name = "dstart"
        Me.dstart.Size = New System.Drawing.Size(363, 23)
        Me.dstart.TabIndex = 32
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(41, 203)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(272, 20)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Description"
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(41, 170)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(272, 20)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Meeting Venue"
        '
        'txtMeetingVenue
        '
        Me.txtMeetingVenue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMeetingVenue.Location = New System.Drawing.Point(319, 167)
        Me.txtMeetingVenue.Name = "txtMeetingVenue"
        Me.txtMeetingVenue.Size = New System.Drawing.Size(363, 26)
        Me.txtMeetingVenue.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(41, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(272, 20)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Meeting Name"
        '
        'txtMeetingName
        '
        Me.txtMeetingName.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMeetingName.Location = New System.Drawing.Point(319, 135)
        Me.txtMeetingName.Name = "txtMeetingName"
        Me.txtMeetingName.Size = New System.Drawing.Size(363, 26)
        Me.txtMeetingName.TabIndex = 5
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
        Me.Label1.Text = "Meeting date start"
        '
        'txtDesc
        '
        Me.txtDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.Location = New System.Drawing.Point(319, 200)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.Size = New System.Drawing.Size(363, 120)
        Me.txtDesc.TabIndex = 4
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dgdetail)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 377)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1261, 71)
        Me.Panel1.TabIndex = 4
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
        Me.dgdetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CIDD, Me.MeetingStart, Me.MeetingEnd, Me.MeetingName, Me.meetingVenue, Me.Description})
        Me.dgdetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgdetail.Location = New System.Drawing.Point(0, 0)
        Me.dgdetail.Name = "dgdetail"
        Me.dgdetail.ReadOnly = True
        Me.dgdetail.RowHeadersVisible = False
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgdetail.RowsDefaultCellStyle = DataGridViewCellStyle4
        Me.dgdetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgdetail.Size = New System.Drawing.Size(1261, 71)
        Me.dgdetail.TabIndex = 4
        '
        'CIDD
        '
        Me.CIDD.HeaderText = "MID"
        Me.CIDD.Name = "CIDD"
        Me.CIDD.ReadOnly = True
        Me.CIDD.Visible = False
        '
        'MeetingStart
        '
        DataGridViewCellStyle2.Format = "G"
        DataGridViewCellStyle2.NullValue = Nothing
        Me.MeetingStart.DefaultCellStyle = DataGridViewCellStyle2
        Me.MeetingStart.HeaderText = "Meeting date start"
        Me.MeetingStart.Name = "MeetingStart"
        Me.MeetingStart.ReadOnly = True
        Me.MeetingStart.Width = 200
        '
        'MeetingEnd
        '
        DataGridViewCellStyle3.Format = "G"
        DataGridViewCellStyle3.NullValue = Nothing
        Me.MeetingEnd.DefaultCellStyle = DataGridViewCellStyle3
        Me.MeetingEnd.HeaderText = "Meeting date end"
        Me.MeetingEnd.Name = "MeetingEnd"
        Me.MeetingEnd.ReadOnly = True
        Me.MeetingEnd.Width = 200
        '
        'MeetingName
        '
        Me.MeetingName.HeaderText = "Meeting Name"
        Me.MeetingName.Name = "MeetingName"
        Me.MeetingName.ReadOnly = True
        Me.MeetingName.Width = 170
        '
        'meetingVenue
        '
        Me.meetingVenue.HeaderText = "Meeting Venue"
        Me.meetingVenue.Name = "meetingVenue"
        Me.meetingVenue.ReadOnly = True
        Me.meetingVenue.Width = 130
        '
        'Description
        '
        Me.Description.HeaderText = "Description"
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        Me.Description.Width = 300
        '
        'frmMeeting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1261, 448)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pInfo)
        Me.Name = "frmMeeting"
        Me.Text = "កត់ត្រាទិន្ន័យប្រជុំ Record meeting info"
        Me.pInfo.ResumeLayout(False)
        Me.pInfo.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgdetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pInfo As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtMeetingVenue As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtMeetingName As System.Windows.Forms.TextBox
    Friend WithEvents B4 As System.Windows.Forms.Button
    Friend WithEvents B1 As System.Windows.Forms.Button
    Friend WithEvents B3 As System.Windows.Forms.Button
    Friend WithEvents B2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDesc As System.Windows.Forms.TextBox
    Friend WithEvents dStop As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dstart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgdetail As System.Windows.Forms.DataGridView
    Friend WithEvents CIDD As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MeetingStart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MeetingEnd As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MeetingName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents meetingVenue As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
