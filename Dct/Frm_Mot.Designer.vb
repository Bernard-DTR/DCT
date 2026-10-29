<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Mot
  Inherits System.Windows.Forms.Form

  'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

  'Requise par le Concepteur Windows Form
  Private components As System.ComponentModel.IContainer

  'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
  'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
  'Ne la modifiez pas à l'aide de l'éditeur de code.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    TB = New TextBox()
    Mnu = New MenuStrip()
    Mnu01 = New ToolStripMenuItem()
    Mnu0199 = New ToolStripMenuItem()
    Mnu02 = New ToolStripMenuItem()
    Mnu0201 = New ToolStripMenuItem()
    Mnu0202 = New ToolStripMenuItem()
    Mnu0299 = New ToolStripMenuItem()
    Mnu0203 = New ToolStripMenuItem()
    Mnu0203TB = New ToolStripTextBox()
    Mnu0204CB = New ToolStripComboBox()
    Mnu0205 = New ToolStripMenuItem()
    Mnu0206 = New ToolStripMenuItem()
    Mnu03 = New ToolStripMenuItem()
    CB = New ToolStripComboBox()
    DGV = New DataGridView()
    Mnu.SuspendLayout()
    CType(DGV, ComponentModel.ISupportInitialize).BeginInit()
    SuspendLayout()
    ' 
    ' TB
    ' 
    TB.Location = New Point(22, 701)
    TB.Multiline = True
    TB.Name = "TB"
    TB.ReadOnly = True
    TB.ScrollBars = ScrollBars.Vertical
    TB.Size = New Size(2218, 120)
    TB.TabIndex = 0
    TB.TabStop = False
    ' 
    ' Mnu
    ' 
    Mnu.ImageScalingSize = New Size(24, 24)
    Mnu.Items.AddRange(New ToolStripItem() {Mnu01, Mnu02, Mnu03})
    Mnu.Location = New Point(0, 0)
    Mnu.Name = "Mnu"
    Mnu.Size = New Size(2252, 33)
    Mnu.TabIndex = 1
    Mnu.Text = "MenuStrip1"
    ' 
    ' Mnu01
    ' 
    Mnu01.DropDownItems.AddRange(New ToolStripItem() {Mnu0199})
    Mnu01.Name = "Mnu01"
    Mnu01.Size = New Size(78, 29)
    Mnu01.Text = "Fichier"
    ' 
    ' Mnu0199
    ' 
    Mnu0199.Name = "Mnu0199"
    Mnu0199.ShortcutKeys = Keys.Alt Or Keys.F4
    Mnu0199.Size = New Size(234, 34)
    Mnu0199.Text = "Fermer"
    ' 
    ' Mnu02
    ' 
    Mnu02.DropDownItems.AddRange(New ToolStripItem() {Mnu0201, Mnu0202, Mnu0299, Mnu0203, Mnu0204CB, Mnu0205, Mnu0206})
    Mnu02.Name = "Mnu02"
    Mnu02.Size = New Size(83, 29)
    Mnu02.Text = "Edition"
    ' 
    ' Mnu0201
    ' 
    Mnu0201.Name = "Mnu0201"
    Mnu0201.ShortcutKeys = Keys.Control Or Keys.S
    Mnu0201.Size = New Size(379, 34)
    Mnu0201.Text = "Mise à jour"
    ' 
    ' Mnu0202
    ' 
    Mnu0202.Name = "Mnu0202"
    Mnu0202.ShortcutKeys = Keys.F5
    Mnu0202.Size = New Size(379, 34)
    Mnu0202.Text = "Rafraîchir"
    ' 
    ' Mnu0299
    ' 
    Mnu0299.Name = "Mnu0299"
    Mnu0299.Size = New Size(379, 34)
    Mnu0299.Text = "Mise au point"
    ' 
    ' Mnu0203
    ' 
    Mnu0203.DropDownItems.AddRange(New ToolStripItem() {Mnu0203TB})
    Mnu0203.Name = "Mnu0203"
    Mnu0203.Size = New Size(379, 34)
    Mnu0203.Text = "Rechercher"
    ' 
    ' Mnu0203TB
    ' 
    Mnu0203TB.Name = "Mnu0203TB"
    Mnu0203TB.Size = New Size(100, 31)
    ' 
    ' Mnu0204CB
    ' 
    Mnu0204CB.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
    Mnu0204CB.Name = "Mnu0204CB"
    Mnu0204CB.Size = New Size(121, 33)
    Mnu0204CB.Text = "Position en"
    ' 
    ' Mnu0205
    ' 
    Mnu0205.Name = "Mnu0205"
    Mnu0205.Size = New Size(379, 34)
    Mnu0205.Text = "Affichage des 15 dernières saisies"
    ' 
    ' Mnu0206
    ' 
    Mnu0206.Name = "Mnu0206"
    Mnu0206.Size = New Size(379, 34)
    Mnu0206.Text = "Mots en double"
    ' 
    ' Mnu03
    ' 
    Mnu03.DropDownItems.AddRange(New ToolStripItem() {CB})
    Mnu03.Name = "Mnu03"
    Mnu03.Size = New Size(77, 29)
    Mnu03.Text = "Divers"
    ' 
    ' CB
    ' 
    CB.Name = "CB"
    CB.Size = New Size(121, 33)
    ' 
    ' DGV
    ' 
    DGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
    DGV.Location = New Point(22, 51)
    DGV.Name = "DGV"
    DGV.RowHeadersWidth = 62
    DGV.Size = New Size(2218, 626)
    DGV.TabIndex = 2
    ' 
    ' Frm_Mot
    ' 
    AutoScaleDimensions = New SizeF(10F, 25F)
    AutoScaleMode = AutoScaleMode.Font
    ClientSize = New Size(2252, 848)
    Controls.Add(DGV)
    Controls.Add(TB)
    Controls.Add(Mnu)
    MainMenuStrip = Mnu
    Name = "Frm_Mot"
    Text = "Frm_Mot"
    Mnu.ResumeLayout(False)
    Mnu.PerformLayout()
    CType(DGV, ComponentModel.ISupportInitialize).EndInit()
    ResumeLayout(False)
    PerformLayout()
  End Sub

  Friend WithEvents TB As TextBox
  Friend WithEvents Mnu As MenuStrip
  Friend WithEvents Mnu01 As ToolStripMenuItem
  Friend WithEvents Mnu0199 As ToolStripMenuItem
  Friend WithEvents Mnu02 As ToolStripMenuItem
  Friend WithEvents Mnu0201 As ToolStripMenuItem
  Friend WithEvents Mnu0299 As ToolStripMenuItem
  Friend WithEvents DGV As DataGridView
  Friend WithEvents Mnu0202 As ToolStripMenuItem
  Friend WithEvents Mnu0203 As ToolStripMenuItem
  Friend WithEvents Mnu0203TB As ToolStripTextBox
  Friend WithEvents Mnu0204CB As ToolStripComboBox
  Friend WithEvents Mnu0205 As ToolStripMenuItem
  Friend WithEvents Mnu0206 As ToolStripMenuItem
  Friend WithEvents Mnu03 As ToolStripMenuItem
  Friend WithEvents CB As ToolStripComboBox
End Class
