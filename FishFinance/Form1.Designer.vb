<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Base_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Base_Form))
        Me.Balance = New System.Windows.Forms.Label()
        Me.Balance_L = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.A_Balance_L = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripDropDownButton4 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripDropDownButton9 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripDropDownButton3 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripDropDownButton5 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripDropDownButton7 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.InitalBalanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearSaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.PName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PRecoup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PDeadline = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PUID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.RName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RRecoup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RPaid = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RDeadline = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RUID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Balance
        '
        Me.Balance.AutoSize = True
        Me.Balance.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Balance.Location = New System.Drawing.Point(23, 62)
        Me.Balance.Name = "Balance"
        Me.Balance.Size = New System.Drawing.Size(97, 25)
        Me.Balance.TabIndex = 3
        Me.Balance.Text = "Balance"
        '
        'Balance_L
        '
        Me.Balance_L.AutoSize = True
        Me.Balance_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Balance_L.Location = New System.Drawing.Point(38, 100)
        Me.Balance_L.Name = "Balance_L"
        Me.Balance_L.Size = New System.Drawing.Size(57, 20)
        Me.Balance_L.TabIndex = 4
        Me.Balance_L.Text = "Label2"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 168)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 25)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Avaliable"
        '
        'A_Balance_L
        '
        Me.A_Balance_L.AutoSize = True
        Me.A_Balance_L.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.A_Balance_L.Location = New System.Drawing.Point(38, 205)
        Me.A_Balance_L.Name = "A_Balance_L"
        Me.A_Balance_L.Size = New System.Drawing.Size(57, 20)
        Me.A_Balance_L.TabIndex = 6
        Me.A_Balance_L.Text = "Label3"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton4, Me.ToolStripDropDownButton9, Me.ToolStripDropDownButton3, Me.ToolStripDropDownButton5, Me.ToolStripDropDownButton7, Me.ToolStripDropDownButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(885, 27)
        Me.ToolStrip1.TabIndex = 7
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripDropDownButton4
        '
        Me.ToolStripDropDownButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton4.Image = CType(resources.GetObject("ToolStripDropDownButton4.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton4.Name = "ToolStripDropDownButton4"
        Me.ToolStripDropDownButton4.ShowDropDownArrow = False
        Me.ToolStripDropDownButton4.Size = New System.Drawing.Size(124, 24)
        Me.ToolStripDropDownButton4.Text = "Add Expenditure"
        '
        'ToolStripDropDownButton9
        '
        Me.ToolStripDropDownButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton9.Image = CType(resources.GetObject("ToolStripDropDownButton9.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton9.Name = "ToolStripDropDownButton9"
        Me.ToolStripDropDownButton9.ShowDropDownArrow = False
        Me.ToolStripDropDownButton9.Size = New System.Drawing.Size(118, 24)
        Me.ToolStripDropDownButton9.Text = "Account History"
        '
        'ToolStripDropDownButton3
        '
        Me.ToolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton3.Image = CType(resources.GetObject("ToolStripDropDownButton3.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton3.Name = "ToolStripDropDownButton3"
        Me.ToolStripDropDownButton3.ShowDropDownArrow = False
        Me.ToolStripDropDownButton3.Size = New System.Drawing.Size(49, 24)
        Me.ToolStripDropDownButton3.Text = "Reset"
        '
        'ToolStripDropDownButton5
        '
        Me.ToolStripDropDownButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton5.Image = CType(resources.GetObject("ToolStripDropDownButton5.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton5.Name = "ToolStripDropDownButton5"
        Me.ToolStripDropDownButton5.ShowDropDownArrow = False
        Me.ToolStripDropDownButton5.Size = New System.Drawing.Size(77, 24)
        Me.ToolStripDropDownButton5.Text = "Add Data"
        Me.ToolStripDropDownButton5.ToolTipText = "Add Data"
        '
        'ToolStripDropDownButton7
        '
        Me.ToolStripDropDownButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton7.Image = CType(resources.GetObject("ToolStripDropDownButton7.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton7.Name = "ToolStripDropDownButton7"
        Me.ToolStripDropDownButton7.ShowDropDownArrow = False
        Me.ToolStripDropDownButton7.Size = New System.Drawing.Size(44, 24)
        Me.ToolStripDropDownButton7.Text = "Save"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InitalBalanceToolStripMenuItem, Me.ClearSaveToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(75, 24)
        Me.ToolStripDropDownButton1.Text = "Settings"
        '
        'InitalBalanceToolStripMenuItem
        '
        Me.InitalBalanceToolStripMenuItem.Name = "InitalBalanceToolStripMenuItem"
        Me.InitalBalanceToolStripMenuItem.Size = New System.Drawing.Size(180, 24)
        Me.InitalBalanceToolStripMenuItem.Text = "Inital Balance"
        '
        'ClearSaveToolStripMenuItem
        '
        Me.ClearSaveToolStripMenuItem.Name = "ClearSaveToolStripMenuItem"
        Me.ClearSaveToolStripMenuItem.Size = New System.Drawing.Size(180, 24)
        Me.ClearSaveToolStripMenuItem.Text = "Manual Mode"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(388, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(184, 20)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Pending Expenditures"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(392, 246)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(190, 20)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Pending Recoupments"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PName, Me.PRecoup, Me.PPrice, Me.Column1, Me.PDeadline, Me.PUID})
        Me.DataGridView1.Location = New System.Drawing.Point(265, 78)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(582, 150)
        Me.DataGridView1.TabIndex = 13
        '
        'PName
        '
        Me.PName.HeaderText = "Name"
        Me.PName.Name = "PName"
        Me.PName.ReadOnly = True
        '
        'PRecoup
        '
        Me.PRecoup.HeaderText = "Recoupment"
        Me.PRecoup.Name = "PRecoup"
        Me.PRecoup.ReadOnly = True
        '
        'PPrice
        '
        Me.PPrice.HeaderText = "Price"
        Me.PPrice.Name = "PPrice"
        Me.PPrice.ReadOnly = True
        '
        'Column1
        '
        Me.Column1.HeaderText = "Predicted Payback"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'PDeadline
        '
        Me.PDeadline.HeaderText = "Deadline"
        Me.PDeadline.Name = "PDeadline"
        Me.PDeadline.ReadOnly = True
        '
        'PUID
        '
        Me.PUID.HeaderText = "Expense ID"
        Me.PUID.Name = "PUID"
        Me.PUID.ReadOnly = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AllowUserToOrderColumns = True
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RName, Me.RRecoup, Me.RPaid, Me.RDeadline, Me.RUID})
        Me.DataGridView2.Location = New System.Drawing.Point(265, 267)
        Me.DataGridView2.MultiSelect = False
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView2.Size = New System.Drawing.Size(585, 150)
        Me.DataGridView2.TabIndex = 14
        '
        'RName
        '
        Me.RName.HeaderText = "Name"
        Me.RName.Name = "RName"
        Me.RName.ReadOnly = True
        '
        'RRecoup
        '
        Me.RRecoup.HeaderText = "Recoupment"
        Me.RRecoup.Name = "RRecoup"
        Me.RRecoup.ReadOnly = True
        '
        'RPaid
        '
        Me.RPaid.HeaderText = "Paid"
        Me.RPaid.Name = "RPaid"
        Me.RPaid.ReadOnly = True
        '
        'RDeadline
        '
        Me.RDeadline.HeaderText = "Deadline"
        Me.RDeadline.Name = "RDeadline"
        Me.RDeadline.ReadOnly = True
        '
        'RUID
        '
        Me.RUID.HeaderText = "UID"
        Me.RUID.Name = "RUID"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(798, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(633, 27)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(159, 25)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Last Updated:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(38, 304)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 20)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Label3"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 267)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(112, 25)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Predicted"
        '
        'Base_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(885, 429)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.A_Balance_L)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Balance_L)
        Me.Controls.Add(Me.Balance)
        Me.Name = "Base_Form"
        Me.Text = "Form1"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Balance As System.Windows.Forms.Label
    Friend WithEvents Balance_L As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents A_Balance_L As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents InitalBalanceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripDropDownButton4 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ToolStripDropDownButton3 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ClearSaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ToolStripDropDownButton5 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents RName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RRecoup As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RPaid As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RDeadline As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RUID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolStripDropDownButton7 As ToolStripDropDownButton
    Friend WithEvents ToolStripDropDownButton9 As ToolStripDropDownButton
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents PName As DataGridViewTextBoxColumn
    Friend WithEvents PRecoup As DataGridViewTextBoxColumn
    Friend WithEvents PPrice As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents PDeadline As DataGridViewTextBoxColumn
    Friend WithEvents PUID As DataGridViewTextBoxColumn
End Class
