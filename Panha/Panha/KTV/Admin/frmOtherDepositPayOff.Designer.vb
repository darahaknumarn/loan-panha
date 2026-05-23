<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOtherDepositPayOff
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOtherDepositPayOff))
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.No = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LD_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_Payoff = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EM_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CM_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CM_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CM_Phone = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CM_Address = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CU_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Amount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Int_Rate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LD_Date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel3.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.DataGridView1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 54)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1536, 456)
        Me.Panel3.TabIndex = 5
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.No, Me.LD_ID, Me.Date_Payoff, Me.EM_ID, Me.EM_Name, Me.CM_ID, Me.CM_Name, Me.CM_Phone, Me.CM_Address, Me.CU_ID, Me.Amount, Me.Int_Rate, Me.LD_Date})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1536, 456)
        Me.DataGridView1.TabIndex = 0
        '
        'No
        '
        Me.No.HeaderText = "...."
        Me.No.Name = "No"
        '
        'LD_ID
        '
        Me.LD_ID.HeaderText = "Loan ID"
        Me.LD_ID.Name = "LD_ID"
        '
        'Date_Payoff
        '
        Me.Date_Payoff.HeaderText = "Date_Payoff"
        Me.Date_Payoff.Name = "Date_Payoff"
        '
        'EM_ID
        '
        Me.EM_ID.HeaderText = "EmployeeID"
        Me.EM_ID.Name = "EM_ID"
        '
        'EM_Name
        '
        Me.EM_Name.HeaderText = "Em. Name"
        Me.EM_Name.Name = "EM_Name"
        '
        'CM_ID
        '
        Me.CM_ID.HeaderText = "Customer_ID"
        Me.CM_ID.Name = "CM_ID"
        '
        'CM_Name
        '
        Me.CM_Name.HeaderText = "Cus. Name"
        Me.CM_Name.Name = "CM_Name"
        '
        'CM_Phone
        '
        Me.CM_Phone.HeaderText = "Phone"
        Me.CM_Phone.Name = "CM_Phone"
        '
        'CM_Address
        '
        Me.CM_Address.HeaderText = "Address"
        Me.CM_Address.Name = "CM_Address"
        '
        'CU_ID
        '
        Me.CU_ID.HeaderText = "Currency"
        Me.CU_ID.Name = "CU_ID"
        '
        'Amount
        '
        Me.Amount.HeaderText = "Amount"
        Me.Amount.Name = "Amount"
        '
        'Int_Rate
        '
        Me.Int_Rate.HeaderText = "Int Rate(%)"
        Me.Int_Rate.Name = "Int_Rate"
        '
        'LD_Date
        '
        Me.LD_Date.HeaderText = "Date"
        Me.LD_Date.Name = "LD_Date"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Button4)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.Button2)
        Me.Panel2.Controls.Add(Me.Button5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 510)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1536, 49)
        Me.Panel2.TabIndex = 4
        '
        'Button4
        '
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button4.Location = New System.Drawing.Point(476, 6)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(156, 33)
        Me.Button4.TabIndex = 7
        Me.Button4.Text = "To Excel"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(320, 6)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(156, 33)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Show All"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(165, 6)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(156, 33)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Show Top 100"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button5.Location = New System.Drawing.Point(9, 6)
        Me.Button5.Margin = New System.Windows.Forms.Padding(4)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(156, 33)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "Show Top 50"
        Me.Button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button5.UseVisualStyleBackColor = True
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(95, 11)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(165, 30)
        Me.DateTimePicker1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(28, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.DateTimePicker1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1536, 54)
        Me.Panel1.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Image = Global.Panha.My.Resources.Resources.Alpha_Dista_Icon_15
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(284, 0)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(155, 54)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Show"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmOtherDepositPayOff
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1536, 559)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmOtherDepositPayOff"
        Me.Text = "frmOtherDepositPayOff"
        Me.Panel3.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LD_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_Payoff As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EM_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CM_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CM_Name As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CM_Phone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CM_Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CU_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Int_Rate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LD_Date As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
