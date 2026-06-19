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
        Dim ReportDataSource5 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource6 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource7 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Dim ReportDataSource8 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
        Me.tblDelegateBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        'Me.BarCodeDataSet = New CamITSo.BarCodeDataSet()
        Me.tblRecordingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        'Me.tblDelegateTableAdapter = New CamITSo.BarCodeDataSetTableAdapters.tblDelegateTableAdapter()
        Me.ReportViewer2 = New Microsoft.Reporting.WinForms.ReportViewer()
        'Me.tblRecordingTableAdapter = New CamITSo.BarCodeDataSetTableAdapters.tblRecordingTableAdapter()
        Me.ReportViewer3 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.ReportViewer4 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.ReportViewer5 = New Microsoft.Reporting.WinForms.ReportViewer()
        CType(Me.tblDelegateBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarCodeDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tblRecordingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tblDelegateBindingSource
        '
        Me.tblDelegateBindingSource.DataMember = "tblDelegate"
        'Me.tblDelegateBindingSource.DataSource = Me.BarCodeDataSet
        '
        'BarCodeDataSet
        '
        Me.BarCodeDataSet.DataSetName = "BarCodeDataSet"
        Me.BarCodeDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'tblRecordingBindingSource
        '
        Me.tblRecordingBindingSource.DataMember = "tblRecording"
        'Me.tblRecordingBindingSource.DataSource = Me.BarCodeDataSet
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(895, 73)
        Me.Panel1.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.Red
        Me.Button2.Location = New System.Drawing.Point(508, 26)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(116, 33)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Exit"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Navy
        Me.Button1.Location = New System.Drawing.Point(364, 26)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(138, 33)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Report"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 24)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select report:"
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"List of visitors", "List of winner", "List of absent", "List of attendant"})
        Me.ComboBox1.Location = New System.Drawing.Point(136, 23)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(189, 32)
        Me.ComboBox1.TabIndex = 0
        '
        'ReportViewer1
        '
        Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource5.Name = "DataSet1"
        ReportDataSource5.Value = Me.tblDelegateBindingSource
        Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource5)
        Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "CamITSo.rptVisitors.rdlc"
        Me.ReportViewer1.Location = New System.Drawing.Point(0, 73)
        Me.ReportViewer1.Name = "ReportViewer1"
        Me.ReportViewer1.Size = New System.Drawing.Size(895, 437)
        Me.ReportViewer1.TabIndex = 1
        '
        'tblDelegateTableAdapter
        '
        Me.tblDelegateTableAdapter.ClearBeforeFill = True
        '
        'ReportViewer2
        '
        Me.ReportViewer2.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource6.Name = "DataSet1"
        ReportDataSource6.Value = Me.tblRecordingBindingSource
        Me.ReportViewer2.LocalReport.DataSources.Add(ReportDataSource6)
        Me.ReportViewer2.LocalReport.ReportEmbeddedResource = "CamITSo.rptWinner.rdlc"
        Me.ReportViewer2.Location = New System.Drawing.Point(0, 73)
        Me.ReportViewer2.Name = "ReportViewer2"
        Me.ReportViewer2.Size = New System.Drawing.Size(895, 437)
        Me.ReportViewer2.TabIndex = 2
        '
        'tblRecordingTableAdapter
        '
        'Me.tblRecordingTableAdapter.ClearBeforeFill = True
        '
        'ReportViewer3
        '
        Me.ReportViewer3.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource7.Name = "DataSet1"
        ReportDataSource7.Value = Me.tblDelegateBindingSource
        Me.ReportViewer3.LocalReport.DataSources.Add(ReportDataSource7)
        Me.ReportViewer3.LocalReport.ReportEmbeddedResource = "CamITSo.rptAbsant.rdlc"
        Me.ReportViewer3.Location = New System.Drawing.Point(0, 73)
        Me.ReportViewer3.Name = "ReportViewer3"
        Me.ReportViewer3.Size = New System.Drawing.Size(895, 437)
        Me.ReportViewer3.TabIndex = 3
        '
        'ReportViewer4
        '
        Me.ReportViewer4.Dock = System.Windows.Forms.DockStyle.Fill
        ReportDataSource8.Name = "DataSet1"
        ReportDataSource8.Value = Me.tblRecordingBindingSource
        Me.ReportViewer4.LocalReport.DataSources.Add(ReportDataSource8)
        Me.ReportViewer4.LocalReport.ReportEmbeddedResource = "CamITSo.rptAttendant.rdlc"
        Me.ReportViewer4.Location = New System.Drawing.Point(0, 73)
        Me.ReportViewer4.Name = "ReportViewer4"
        Me.ReportViewer4.Size = New System.Drawing.Size(895, 437)
        Me.ReportViewer4.TabIndex = 4
        '
        'ReportViewer5
        '
        Me.ReportViewer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportViewer5.Location = New System.Drawing.Point(0, 73)
        Me.ReportViewer5.Name = "ReportViewer5"
        Me.ReportViewer5.Size = New System.Drawing.Size(895, 437)
        Me.ReportViewer5.TabIndex = 5
        '
        'frmReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(895, 510)
        Me.Controls.Add(Me.ReportViewer5)
        Me.Controls.Add(Me.ReportViewer4)
        Me.Controls.Add(Me.ReportViewer3)
        Me.Controls.Add(Me.ReportViewer2)
        Me.Controls.Add(Me.ReportViewer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmReport"
        Me.Text = "frmReport"
        CType(Me.tblDelegateBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        'CType(Me.BarCodeDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tblRecordingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents tblDelegateBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents BarCodeDataSet As CamITSo.BarCodeDataSet
    'Friend WithEvents tblDelegateTableAdapter As CamITSo.BarCodeDataSetTableAdapters.tblDelegateTableAdapter
    Friend WithEvents ReportViewer2 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents tblRecordingBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents tblRecordingTableAdapter As CamITSo.BarCodeDataSetTableAdapters.tblRecordingTableAdapter
    Friend WithEvents ReportViewer3 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents ReportViewer4 As Microsoft.Reporting.WinForms.ReportViewer
    Friend WithEvents ReportViewer5 As Microsoft.Reporting.WinForms.ReportViewer
End Class
