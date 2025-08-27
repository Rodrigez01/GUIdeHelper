Imports System.ComponentModel

Partial Class Form1
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer = Nothing
    Friend WithEvents dataGridViewWords As System.Windows.Forms.DataGridView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents txtTargetTitle As System.Windows.Forms.TextBox
    Friend WithEvents lblTarget As System.Windows.Forms.Label
    Friend WithEvents chkSendEnter As System.Windows.Forms.CheckBox
    Friend WithEvents nudTabs As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblTabs As System.Windows.Forms.Label
    Friend WithEvents nudDelay As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblDelay As System.Windows.Forms.Label

    <DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dataGridViewWords = New Windows.Forms.DataGridView()
        DataGridViewTextBoxColumn1 = New Windows.Forms.DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn2 = New Windows.Forms.DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn3 = New Windows.Forms.DataGridViewTextBoxColumn()
        btnAdd = New Windows.Forms.Button()
        btnDelete = New Windows.Forms.Button()
        btnEdit = New Windows.Forms.Button()
        txtFilter = New Windows.Forms.TextBox()
        lblFilter = New Windows.Forms.Label()
        txtTargetTitle = New Windows.Forms.TextBox()
        lblTarget = New Windows.Forms.Label()
        chkSendEnter = New Windows.Forms.CheckBox()
        nudTabs = New Windows.Forms.NumericUpDown()
        lblTabs = New Windows.Forms.Label()
        nudDelay = New Windows.Forms.NumericUpDown()
        lblDelay = New Windows.Forms.Label()
        CType(dataGridViewWords, ISupportInitialize).BeginInit()
        CType(nudTabs, ISupportInitialize).BeginInit()
        CType(nudDelay, ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dataGridViewWords
        ' 
        dataGridViewWords.AllowUserToAddRows = False
        dataGridViewWords.AllowUserToDeleteRows = False
        dataGridViewWords.AllowUserToResizeRows = False
        dataGridViewWords.ColumnHeadersHeightSizeMode = Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dataGridViewWords.Columns.AddRange(New Windows.Forms.DataGridViewColumn() {DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3})
        dataGridViewWords.Location = New System.Drawing.Point(12, 86)
        dataGridViewWords.Name = "dataGridViewWords"
        dataGridViewWords.ReadOnly = True
        dataGridViewWords.RowHeadersVisible = False
        dataGridViewWords.SelectionMode = Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        dataGridViewWords.Size = New System.Drawing.Size(740, 402)
        dataGridViewWords.TabIndex = 0
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewTextBoxColumn1.HeaderText = "Text"
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        DataGridViewTextBoxColumn1.ReadOnly = True
        ' 
        ' DataGridViewTextBoxColumn2
        ' 
        DataGridViewTextBoxColumn2.HeaderText = "Kategorie"
        DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        DataGridViewTextBoxColumn2.ReadOnly = True
        ' 
        ' DataGridViewTextBoxColumn3
        ' 
        DataGridViewTextBoxColumn3.HeaderText = "Hotkey"
        DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        DataGridViewTextBoxColumn3.ReadOnly = True
        ' 
        ' btnAdd
        ' 
        btnAdd.Location = New System.Drawing.Point(758, 86)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New System.Drawing.Size(100, 27)
        btnAdd.TabIndex = 1
        btnAdd.Text = "Hinzufügen"
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New System.Drawing.Point(758, 119)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New System.Drawing.Size(100, 27)
        btnDelete.TabIndex = 2
        btnDelete.Text = "Löschen"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnEdit
        ' 
        btnEdit.Location = New System.Drawing.Point(758, 152)
        btnEdit.Name = "btnEdit"
        btnEdit.Size = New System.Drawing.Size(100, 27)
        btnEdit.TabIndex = 3
        btnEdit.Text = "Bearbeiten"
        btnEdit.UseVisualStyleBackColor = True
        ' 
        ' txtFilter
        ' 
        txtFilter.Location = New System.Drawing.Point(95, 13)
        txtFilter.Name = "txtFilter"
        txtFilter.PlaceholderText = "z. B. 'Heilen'"
        txtFilter.Size = New System.Drawing.Size(250, 23)
        txtFilter.TabIndex = 5
        ' 
        ' lblFilter
        ' 
        lblFilter.AutoSize = True
        lblFilter.Location = New System.Drawing.Point(12, 16)
        lblFilter.Name = "lblFilter"
        lblFilter.Size = New System.Drawing.Size(70, 15)
        lblFilter.TabIndex = 4
        lblFilter.Text = "Suche/Filter"
        ' 
        ' txtTargetTitle
        ' 
        txtTargetTitle.Location = New System.Drawing.Point(511, 13)
        txtTargetTitle.Name = "txtTargetTitle"
        txtTargetTitle.PlaceholderText = "z. B. 'Mein Spiel'"
        txtTargetTitle.Size = New System.Drawing.Size(241, 23)
        txtTargetTitle.TabIndex = 7
        ' 
        ' lblTarget
        ' 
        lblTarget.AutoSize = True
        lblTarget.Location = New System.Drawing.Point(361, 16)
        lblTarget.Name = "lblTarget"
        lblTarget.Size = New System.Drawing.Size(170, 15)
        lblTarget.TabIndex = 6
        lblTarget.Text = "Fenstertitel des Zielprogramms"
        ' 
        ' chkSendEnter
        ' 
        chkSendEnter.AutoSize = True
        chkSendEnter.Location = New System.Drawing.Point(12, 52)
        chkSendEnter.Name = "chkSendEnter"
        chkSendEnter.Size = New System.Drawing.Size(156, 19)
        chkSendEnter.TabIndex = 8
        chkSendEnter.Text = "Nach Text ENTER senden"
        chkSendEnter.UseVisualStyleBackColor = True
        ' 
        ' nudTabs
        ' 
        nudTabs.Location = New System.Drawing.Point(293, 50)
        nudTabs.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        nudTabs.Name = "nudTabs"
        nudTabs.Size = New System.Drawing.Size(60, 23)
        nudTabs.TabIndex = 10
        ' 
        ' lblTabs
        ' 
        lblTabs.AutoSize = True
        lblTabs.Location = New System.Drawing.Point(208, 53)
        lblTabs.Name = "lblTabs"
        lblTabs.Size = New System.Drawing.Size(71, 15)
        lblTabs.TabIndex = 9
        lblTabs.Text = "TAB-Anzahl:"
        ' 
        ' nudDelay
        ' 
        nudDelay.Location = New System.Drawing.Point(554, 50)
        nudDelay.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        nudDelay.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        nudDelay.Name = "nudDelay"
        nudDelay.Size = New System.Drawing.Size(60, 23)
        nudDelay.TabIndex = 12
        nudDelay.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' lblDelay
        ' 
        lblDelay.AutoSize = True
        lblDelay.Location = New System.Drawing.Point(372, 53)
        lblDelay.Name = "lblDelay"
        lblDelay.Size = New System.Drawing.Size(167, 15)
        lblDelay.TabIndex = 11
        lblDelay.Text = "Delay zwischen Schritten (ms):"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New System.Drawing.SizeF(7.0F, 15.0F)
        AutoScaleMode = Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(872, 500)
        Controls.Add(nudDelay)
        Controls.Add(lblDelay)
        Controls.Add(nudTabs)
        Controls.Add(lblTabs)
        Controls.Add(chkSendEnter)
        Controls.Add(txtTargetTitle)
        Controls.Add(lblTarget)
        Controls.Add(txtFilter)
        Controls.Add(lblFilter)
        Controls.Add(btnEdit)
        Controls.Add(btnDelete)
        Controls.Add(btnAdd)
        Controls.Add(dataGridViewWords)
        Name = "Form1"
        Text = "AutoTyper VB – SendKeys (robust)"
        CType(dataGridViewWords, ISupportInitialize).EndInit()
        CType(nudTabs, ISupportInitialize).EndInit()
        CType(nudDelay, ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents DataGridViewTextBoxColumn1 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As Windows.Forms.DataGridViewTextBoxColumn
End Class
