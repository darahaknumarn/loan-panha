<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDelegate
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
        Me.OFDFileBrowse = New System.Windows.Forms.OpenFileDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.mnupop = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnucheckErrors = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuimport = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnimport = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.status = New System.Windows.Forms.StatusStrip()
        Me.p1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.pListdelegate = New System.Windows.Forms.Panel()
        Me.dgsheet = New System.Windows.Forms.DataGridView()
        Me.cusID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.barcode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FUName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Gender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Title = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Position = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OCountry = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OTel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Email = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LTel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Photo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnupop.SuspendLayout()
        Me.status.SuspendLayout()
        Me.pListdelegate.SuspendLayout()
        CType(Me.dgsheet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OFDFileBrowse
        '
        Me.OFDFileBrowse.Filter = "Excel 2007-2010(*.xlsx)|*.xlsx|Excel 97-2003(*.xls)|*.xls"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(-2, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(118, 33)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "&Browse"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'mnupop
        '
        Me.mnupop.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem, Me.mnucheckErrors, Me.mnuimport})
        Me.mnupop.Name = "mnupop"
        Me.mnupop.Size = New System.Drawing.Size(141, 70)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.DeleteToolStripMenuItem.Text = "&Delete"
        '
        'mnucheckErrors
        '
        Me.mnucheckErrors.Name = "mnucheckErrors"
        Me.mnucheckErrors.Size = New System.Drawing.Size(140, 22)
        Me.mnucheckErrors.Text = "&Check Errors"
        '
        'mnuimport
        '
        Me.mnuimport.Name = "mnuimport"
        Me.mnuimport.Size = New System.Drawing.Size(140, 22)
        Me.mnuimport.Text = "&Import Data"
        '
        'btnimport
        '
        Me.btnimport.Location = New System.Drawing.Point(122, 12)
        Me.btnimport.Name = "btnimport"
        Me.btnimport.Size = New System.Drawing.Size(101, 33)
        Me.btnimport.TabIndex = 3
        Me.btnimport.Text = "&Import Now"
        Me.btnimport.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(914, 12)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(101, 33)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "&Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'status
        '
        Me.status.Dock = System.Windows.Forms.DockStyle.None
        Me.status.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.p1})
        Me.status.Location = New System.Drawing.Point(265, 23)
        Me.status.Name = "status"
        Me.status.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.status.Size = New System.Drawing.Size(16, 22)
        Me.status.TabIndex = 125
        Me.status.Text = "StatusStrip1"
        Me.status.Visible = False
        '
        'p1
        '
        Me.p1.Name = "p1"
        Me.p1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always
        Me.p1.Size = New System.Drawing.Size(100, 16)
        Me.p1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.p1.Visible = False
        '
        'pListdelegate
        '
        Me.pListdelegate.Controls.Add(Me.dgsheet)
        Me.pListdelegate.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pListdelegate.Location = New System.Drawing.Point(0, 87)
        Me.pListdelegate.Name = "pListdelegate"
        Me.pListdelegate.Size = New System.Drawing.Size(1115, 271)
        Me.pListdelegate.TabIndex = 126
        '
        'dgsheet
        '
        Me.dgsheet.AllowUserToAddRows = False
        Me.dgsheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgsheet.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.cusID, Me.barcode, Me.FName, Me.LName, Me.MName, Me.FUName, Me.DOB, Me.Gender, Me.Title, Me.Position, Me.MStatus, Me.OCountry, Me.OAddress, Me.LAddress, Me.MID, Me.OTel, Me.Email, Me.LTel, Me.Photo})
        Me.dgsheet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgsheet.Location = New System.Drawing.Point(0, 0)
        Me.dgsheet.Name = "dgsheet"
        Me.dgsheet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgsheet.Size = New System.Drawing.Size(1115, 271)
        Me.dgsheet.TabIndex = 5
        '
        'cusID
        '
        Me.cusID.HeaderText = "Attendant ID"
        Me.cusID.Name = "cusID"
        Me.cusID.Visible = False
        '
        'barcode
        '
        Me.barcode.HeaderText = "Bar code"
        Me.barcode.Name = "barcode"
        '
        'FName
        '
        Me.FName.HeaderText = "First Name"
        Me.FName.Name = "FName"
        '
        'LName
        '
        Me.LName.HeaderText = "Last Name"
        Me.LName.Name = "LName"
        '
        'MName
        '
        Me.MName.HeaderText = "Middle Name"
        Me.MName.Name = "MName"
        '
        'FUName
        '
        Me.FUName.HeaderText = "Full Name"
        Me.FUName.Name = "FUName"
        '
        'DOB
        '
        Me.DOB.HeaderText = "Birth Date"
        Me.DOB.Name = "DOB"
        '
        'Gender
        '
        Me.Gender.HeaderText = "Gender"
        Me.Gender.Name = "Gender"
        '
        'Title
        '
        Me.Title.HeaderText = "Title"
        Me.Title.Name = "Title"
        '
        'Position
        '
        Me.Position.HeaderText = "Postion"
        Me.Position.Name = "Position"
        '
        'MStatus
        '
        Me.MStatus.HeaderText = "Marital Status"
        Me.MStatus.Name = "MStatus"
        '
        'OCountry
        '
        Me.OCountry.HeaderText = "Origin Country"
        Me.OCountry.Name = "OCountry"
        '
        'OAddress
        '
        Me.OAddress.HeaderText = "Origin Address"
        Me.OAddress.Name = "OAddress"
        '
        'LAddress
        '
        Me.LAddress.HeaderText = "Local Address"
        Me.LAddress.Name = "LAddress"
        '
        'MID
        '
        Me.MID.HeaderText = "Meeting Code"
        Me.MID.Name = "MID"
        '
        'OTel
        '
        Me.OTel.HeaderText = "Origin Tel"
        Me.OTel.Name = "OTel"
        '
        'Email
        '
        Me.Email.HeaderText = "Email"
        Me.Email.Name = "Email"
        '
        'LTel
        '
        Me.LTel.HeaderText = "Local Tel"
        Me.LTel.Name = "LTel"
        '
        'Photo
        '
        Me.Photo.HeaderText = "Photo"
        Me.Photo.Name = "Photo"
        '
        'frmDelegate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1115, 358)
        Me.Controls.Add(Me.pListdelegate)
        Me.Controls.Add(Me.status)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnimport)
        Me.Name = "frmDelegate"
        Me.Text = "Delegate Information"
        Me.mnupop.ResumeLayout(False)
        Me.status.ResumeLayout(False)
        Me.status.PerformLayout()
        Me.pListdelegate.ResumeLayout(False)
        CType(Me.dgsheet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OFDFileBrowse As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents mnupop As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnucheckErrors As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuimport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnimport As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents status As System.Windows.Forms.StatusStrip
    Friend WithEvents p1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents pListdelegate As System.Windows.Forms.Panel
    Friend WithEvents dgsheet As System.Windows.Forms.DataGridView
    Friend WithEvents cusID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents barcode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FUName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Gender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Title As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Position As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MStatus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OCountry As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents OTel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Email As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LTel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Photo As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
