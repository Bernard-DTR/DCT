Option Strict On
Option Explicit On

Public Module DgvFormatter

  Public Sub ConfigurerDGV(dgv As DataGridView)

    With dgv
      .AllowUserToOrderColumns = False
      .AllowUserToResizeColumns = False
      .AllowUserToResizeRows = False
      .AllowDrop = False

      .RowsDefaultCellStyle.BackColor = Color.Beige
      .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192)

      .SelectionMode = DataGridViewSelectionMode.FullRowSelect
      .ScrollBars = ScrollBars.Vertical

      .DefaultCellStyle.WrapMode = DataGridViewTriState.True
      .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
      .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

      .Columns("Entrée").FillWeight = 15
      .Columns("Explication").FillWeight = 85

      .Columns("IDLexique").Visible = False
      '.Columns("Date_Entrée").Visible = False

      .Columns("Entrée").DefaultCellStyle.Font = New Font("Times New Roman", 15)
      .Columns("Entrée").DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft
      .Columns("Explication").DefaultCellStyle.Font = New Font("Times New Roman", 15)
    End With

  End Sub

End Module