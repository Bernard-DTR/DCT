Option Strict On
Option Explicit On
Imports System.Data.SqlClient
'Il est nécessaire d'installer ce composant via Projet/Gérer les packages NuGet
'WeCantSpell.Hunspell Dernière version stable 7.0.1  avec un framework .NET 8.0
'Restauration des packages pour C:\Développement\Dictionnaire\Dct\Dct\Dct.vbproj...
'  Get https://api.nuget.org/v3-flatcontainer/wecantspell.hunspell/index.json
'  OK https : //api.nuget.org/v3-flatcontainer/wecantspell.hunspell/index.json 81 ms
'  Get https://api.nuget.org/v3-flatcontainer/wecantspell.hunspell/7.0.1/wecantspell.hunspell.7.0.1.nupkg
'  OK https : //api.nuget.org/v3-flatcontainer/wecantspell.hunspell/7.0.1/wecantspell.hunspell.7.0.1.nupkg 16 ms
'WeCantSpell.Hunspell 7.0.1 installés à partir de https: //api.nuget.org/v3/index.json vers C:\Users\berna\.nuget\packages\wecantspell.hunspell\7.0.1 avec le code de hachage de contenu Ha0x66QWA+IPj2kJYoweA8MSnB1tymBW6kAIS6Kkj/U3SlMLCBPjDlUXzrrY1zJAxVItrGpFIXjkl3eEIYIE9A==.
'  CACHE https : //api.nuget.org/v3/vulnerabilities/index.json
'  CACHE https : //api.nuget.org/v3-vulnerabilities/2026.09.02.23.52.57/vulnerability.base.json
'  CACHE https : //api.nuget.org/v3-vulnerabilities/2026.09.02.23.52.57/2026.09.08.14.43.59/vulnerability.update.json
'Installation du package NuGet WeCantSpell.Hunspell 7.0.1.
'Écriture du fichier des composants sur le disque. Chemin : C : \Développement\Dictionnaire\Dct\Dct\obj\project.assets.json
'Installation réussie de « WeCantSpell.Hunspell 7.0.1 » sur Dct
'L'exécution des actions de nuget a pris 362 ms
'Temps écoulé :  00:00:01.3181408
'========== Fin ==========
Imports WeCantSpell.Hunspell
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Configuration

'--------------------------------------------------------------------------------------------------------
' Gestion d'un dictionnaire de mots avec les suggestions de correction orthographique
' Le code esr réalisé avec Copilot qui a généré la structure de base et les événements
' 09/09/2026 ajout d'une correction orthographique avec le package NuGet WeCantSpell.Hunspell
'            la correction orthographique est différente de celle effectuée dans Buhsnell avec le package NHunspell
'            car le framework est différent (.NET 8.0 pour WeCantSpell.Hunspell et .NET Framework 4.8 pour NHunspell)
'--------------------------------------------------------------------------------------------------------




'--------------------------------------------------------------------------------------------
'  x  Présentation par IDLexique descendant pour contrôler les derniers mots
'  x  Barre d'outils avec + et 26 lettres pour ajouter des mots plus facilement
'  x  Afficher les mots en double
'  x  Proposer une correction orthographique
'  x  Majuscule en début de mot pour entrée et de phrase pour explication
'     Il reste une erreur lors de l'enregistrement des sauvegardes
'     ? voir bds.EndEdit()
'     Révision le 19/10/2026
'--------------------------------------------------------------------------------------------

Public Class Frm_Mot
  Shared Sub New()
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)
  End Sub

  Private repo As SqlMotRepository
  Private tab As DataTable
  Private bds As BindingSource
  Private isLoading As Boolean = True
  Private dict As WordList

  Private Sub Frm_Mot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    With TB
      .Multiline = True
      .ReadOnly = True
      .WordWrap = True
      .ScrollBars = ScrollBars.Vertical
      .TabStop = False
    End With

    Dim dicPath As String = IO.Path.Combine("C:\Program Files\LibreOffice\share\extensions\dict-fr\", "fr.dic")
    dict = WordList.CreateFromFiles(dicPath) ' .aff inféré automatiquement

    Mnu0299.Visible = False ' Pour développement uniquement
    repo = New SqlMotRepository()

    ' --- Chargement initial ---
    tab = repo.tab_Charger()
    bds = New BindingSource With {.DataSource = tab}
    DGV.DataSource = bds

    ' --- Formatage ---
    DgvFormatter.ConfigurerDGV(DGV)

    TB.Text = $" Frm_Mot_Load {tab.Rows.Count} entrée(s) chargée(s). "

    For Each key As String In ConfigurationManager.AppSettings.AllKeys
      If key.StartsWith("Url") Then
        CB.Items.Add(ConfigurationManager.AppSettings(key))
      End If
    Next


    ' --- Positionnement sur la dernière entrée ---
    RestaurerDernierePosition()

    isLoading = False

  End Sub

  Private Sub RestaurerDernierePosition()
    Dim last As String = My.Settings.LastEntrée
    If String.IsNullOrWhiteSpace(last) Then Exit Sub

    Dim rows As DataRow() = tab.Select($"Entrée = '{last.Replace("'", "''")}'")
    Dim l As Integer = rows.Length
    If l = 0 Then
      MessageBox.Show($"La dernière entrée {last} n'a pas été trouvée.")
      Exit Sub
    End If

    Dim index As Integer = tab.Rows.IndexOf(rows(0))
    DGV.CurrentCell = DGV.Rows(index).Cells("Entrée")
    DGV.FirstDisplayedScrollingRowIndex = index
  End Sub

  Private Sub DGV_SelectionChanged(sender As Object, e As EventArgs) Handles DGV.SelectionChanged
    If isLoading OrElse DGV.CurrentCell Is Nothing Then Exit Sub
    Try
      My.Settings.LastEntrée = CStr(DGV.CurrentRow.Cells("Entrée").Value)
      My.Settings.Save()
    Catch ex As Exception
      TB.Text = $" DGV_SelectionChanged {ex.Message} "
    End Try
  End Sub

  Private Sub MnuSauver_Click(sender As Object, e As EventArgs) Handles Mnu0201.Click
    Try
      bds.EndEdit()
      Dim nb As Integer = repo.tab_Sauver(tab)
      TB.Text = $" MnuSauver_Click {nb} modification(s) enregistrée(s)."
      MnuRecharger_Click(sender, e)
      Mnu0205_Click(sender, e)
    Catch ex As Exception
      TB.BackColor = Color.Red
      TB.Text = $" MnuSauver_Click bds.EndEdit() {ex.Message} "
    End Try
  End Sub

  Private Sub MnuRecharger_Click(sender As Object, e As EventArgs) Handles Mnu0202.Click
    tab = repo.tab_Charger()
    bds.DataSource = tab
    TB.Text = $" MnuRecharger_Click {tab.Rows.Count} entrée(s) rechargée(s)."
  End Sub

  Private Sub MnuRechercher_KeyDown(sender As Object, e As KeyEventArgs) Handles Mnu0203TB.KeyDown
    If e.KeyCode <> Keys.Enter Then Exit Sub

    Dim texte As String = Mnu0203TB.Text.Trim()
    My.Settings.LastRecherche = texte

    tab = repo.Rechercher(texte)
    bds.DataSource = tab

    e.SuppressKeyPress = True
    CType(CType(sender, ToolStripTextBox).Owner, ToolStripDropDown).Hide()

    TB.Text = $"MnuRechercher_KeyDown {tab.Rows.Count} résultat(s) trouvé(s)."
  End Sub

  Private Sub DGV_DoubleClick(sender As Object, e As EventArgs) Handles DGV.DoubleClick
    If DGV.CurrentCell.ColumnIndex <> DGV.Columns("Entrée").Index Then Exit Sub
    Try
      Dim mot As String = CStr(DGV.CurrentCell.Value).Replace(" ", "+")
      Dim url As String = $"https://www.bing.com/search?q=Définition,+{mot}"
      TB.Text = url

      Process.Start(New ProcessStartInfo With {
        .FileName = url,
        .UseShellExecute = True
    })
    Catch ex As Exception
      TB.Text = $"DGV_DoubleClick {ex.Message} "
    End Try
  End Sub

  Private Sub Mnu0204CB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Mnu0204CB.SelectedIndexChanged
    Dim lettre As String = Mnu0204CB.Text
    tab = repo.tab_Charger()
    bds.DataSource = tab
    For i As Integer = 0 To DGV.Rows.Count - 1
      Dim val As String = CStr(DGV.Rows(i).Cells("Entrée").Value)
      If val.StartsWith(lettre, StringComparison.InvariantCultureIgnoreCase) Then
        DGV.CurrentCell = DGV.Rows(i).Cells("Entrée")
        Exit For
      End If
    Next
    TB.Text = $" Mnu0204CB_SelectedIndexChanged {Mnu0204CB.Text}."
  End Sub

  Private Sub Mnu0205_Click(sender As Object, e As EventArgs) Handles Mnu0205.Click
    tab = repo.tab_Charger_10_IDLexique()
    bds.DataSource = tab
    TB.Text = $" Mnu0205_Click {tab.Rows.Count} entrée(s) rechargée(s)."
  End Sub

  Private Sub Mnu0206_Click(sender As Object, e As EventArgs) Handles Mnu0206.Click
    tab = repo.tab_Charger_Doublons()
    bds.DataSource = tab
    TB.Text = $" Mnu0206_Click {tab.Rows.Count} entrée(s) rechargée(s)."
  End Sub

  Private Function NettoyerTexte(txt As String) As String
    ' Supprime tout sauf lettres et espaces
    Dim propre As String =
        Regex.Replace(txt, "[^A-Za-zÀ-ÖØ-öø-ÿ\s]", " ")

    ' Remplace les multiples espaces par un seul
    Return Regex.Replace(propre, "\s+", " ").Trim()
  End Function

  Private Sub DGV_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGV.CellEndEdit
    Try

      TB.Clear()
      TB.BackColor = DefaultBackColor

      Dim erreurs As New List(Of String)
      Dim erreurSuggestions As New List(Of String)

      Dim colonne As String = DGV.CurrentCell.OwningColumn.Name
      Dim erreurChamp As String = "Champ : " & colonne

      Dim valeurBrute As String = CStr(DGV.CurrentRow.Cells(colonne).Value)
      Dim valeurNettoyee As String = NettoyerTexte(valeurBrute)

      Dim regex As Regex = Nothing

      Select Case colonne
        Case "Entrée"
          regex = New Regex("^[A-Z].*$")
          If Not regex.IsMatch(valeurBrute) Then
            erreurs.Add("Entrée doit commencer par une majuscule.")
          End If

        Case "Explication"
          regex = New Regex("^[A-Z0-9].*\.$")
          If Not regex.IsMatch(valeurBrute) Then
            erreurs.Add("Explication doit commencer par une majuscule et se terminer par un point.")
          End If
      End Select

      ' Vérification orthographique
      Dim mots As String() = valeurNettoyee.Split({" "}, StringSplitOptions.RemoveEmptyEntries)

      For Each m As String In mots
        If Not dict.Check(m) Then
          erreurs.Add(m)

          Dim sug As IEnumerable(Of String) = dict.Suggest(m)
          If sug.Any() Then
            erreurSuggestions.Add($"Mot incorrect : {m}{vbCrLf}Suggestions : {String.Join(", ", sug)}")
          Else
            erreurSuggestions.Add($"Mot incorrect : {m}{vbCrLf}Aucune suggestion disponible.")
          End If
        End If
      Next

      ' Affichage
      If erreurs.Count = 0 Then
        TB.Text = "Aucune erreur détectée."
      Else
        TB.BackColor = Color.FromArgb(255, 192, 192)

        TB.AppendText("Enregistrement : " &
                      CStr(DGV.CurrentRow.Cells("Entrée").Value) & vbCrLf)

        TB.AppendText(erreurChamp & vbCrLf & vbCrLf)

        TB.AppendText("Erreurs : " & String.Join(", ", erreurs) & vbCrLf & vbCrLf)

        For Each bloc As String In erreurSuggestions
          TB.AppendText(bloc & vbCrLf)
        Next
      End If
    Catch ex As Exception
      TB.Text = $"DGV_CellEndEdit {ex.Message} "
    End Try
  End Sub


  Private Sub Mnu0199_Click(sender As Object, e As EventArgs) Handles Mnu0199.Click

    bds.EndEdit()

    If tab.GetChanges() IsNot Nothing Then

      Dim result As DialogResult = MessageBox.Show(
            "Des modifications n'ont pas été enregistrées." & vbCrLf &
            "Voulez-vous les enregistrer avant de quitter ?",
            "Modifications",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Warning)

      Select Case result
        Case DialogResult.Cancel
          ' Ne rien faire : on reste dans le formulaire
          Return
        Case DialogResult.Yes
          MnuSauver_Click(sender, e)
        Case DialogResult.No
          ' On quitte sans enregistrer
      End Select
    End If
    ' Si aucune modification ou si l'utilisateur a choisi
    ' Oui ou Non, on ferme le formulaire.
    Me.Close()
  End Sub

  Private Sub CB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB.SelectedIndexChanged
    Dim url As String = CB.SelectedItem.ToString()
    Dim psi As New ProcessStartInfo With {
        .FileName = url,
        .UseShellExecute = True
    }
    Using Process.Start(psi)
      TB.Text = $"CB_SelectedIndexChanged {url}"
    End Using
  End Sub
End Class