Option Strict On
Option Explicit On
Imports System.Data.SqlClient
Public Class SqlMotRepository

  Private ReadOnly _cnnString As String =
      "Data Source=localhost;Initial Catalog=HPOmen;Integrated Security=True"

  Private ReadOnly _cnn As SqlConnection
  Private ReadOnly _adp As SqlDataAdapter

  Public Sub New()
    _cnn = New SqlConnection(_cnnString)

    _adp = New SqlDataAdapter("SELECT * FROM MotT2 ORDER BY Entrée", _cnn)
    _adp.MissingSchemaAction = MissingSchemaAction.AddWithKey

    Dim bld As New SqlCommandBuilder(_adp)
    _adp.InsertCommand = bld.GetInsertCommand()
    _adp.UpdateCommand = bld.GetUpdateCommand()
    _adp.DeleteCommand = bld.GetDeleteCommand()

    ' 🔒 Sécurise les états du DataTable
    _adp.AcceptChangesDuringUpdate = False

    ' 🔄 Recharge la ligne après UPDATE si SQL Server génère des valeurs
    _adp.UpdateCommand.UpdatedRowSource =
        UpdateRowSource.OutputParameters Or UpdateRowSource.FirstReturnedRecord
  End Sub

  Public Function tab_Charger() As DataTable
    Dim tab As New DataTable With {.Locale = Globalization.CultureInfo.InvariantCulture}
    _adp.Fill(tab)
    Return tab
  End Function

  Public Function Rechercher(texte As String) As DataTable
    Dim sql As String =
        "SELECT * FROM MotT2 
             WHERE Entrée LIKE @p OR Explication LIKE @p
             ORDER BY Entrée"

    Dim adp As New SqlDataAdapter(sql, _cnn)
    adp.SelectCommand.Parameters.AddWithValue("@p", "%" & texte & "%")

    Dim tab As New DataTable With {.Locale = Globalization.CultureInfo.InvariantCulture}
    adp.Fill(tab)
    Return tab
  End Function

  Public Function tab_Charger_10_IDLexique() As DataTable
    Dim sql As String = "SELECT Top 10 * FROM MotT2 ORDER BY IDLexique DESC"
    Dim adp As New SqlDataAdapter(sql, _cnn)
    Dim tab As New DataTable With {.Locale = Globalization.CultureInfo.InvariantCulture}
    adp.Fill(tab)
    Return tab
  End Function

  Public Function tab_Sauver(tab As DataTable) As Integer
    Return _adp.Update(tab)
  End Function
  Public Function tab_Charger_Doublons() As DataTable
    Dim sql As String =
        "SELECT * FROM MotT2
         WHERE Entrée IN (
             SELECT Entrée FROM MotT2 GROUP BY Entrée HAVING COUNT(*) > 1
         )
         ORDER BY Entrée"
    Dim adp As New SqlDataAdapter(sql, _cnn)
    Dim tab As New DataTable With {.Locale = Globalization.CultureInfo.InvariantCulture}
    adp.Fill(tab)
    Return tab
  End Function
End Class
