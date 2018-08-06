'Aplicativo para Férias do RH
'Data de criação: 06/01/2016 12:48h
'Desenvolvedor: Leonardo Metelys - Gerência de Aplicativos - ALEAM
'Formulário de Consulta de Férias ao Mês dos Efetivos

Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Configuration
Imports System.Net.Mail
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.DirectoryServices.AccountManagement
Imports System.TimeSpan
Imports System.Globalization.CultureInfo
Imports AleamRamais.Classes

Public Class ConsMesFeriasEfetivos
    Inherits System.Web.UI.Page
    Dim sqlConn As MySqlConnection
    Dim sqlCmd As MySqlCommand
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Public sConta As Integer
    Public sSQL As String
    Public cnn As OdbcConnection
    Public myConnection As OleDbConnection
    Public myCommand As OleDbCommand
    Public dsr As OleDbDataReader
    Public sTotal As Double
    Public ra As Integer
    'Conexão com o MYSQL
    Const ConnStr As String = "Driver={MySQL ODBC 5.1 Driver};" + "Server=172.16.0.32;Database=scc_web;uid=intranetadmin;pwd=Intranet@Al34m;option=3"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            CarregarGridTodos()
            CarregarTotais()


        End If
    End Sub
    Private Sub CarregarTotais()



        Dim strSQL As String


        strSQL = "SELECT count(matricula) as Total FROM colaborador WHERE tipo = 'EFETIVO'"

        If TxtSolicitante.Text <> "" Then

            Dim sNomeReclamado As String = TxtSolicitante.Text
            sNomeReclamado = "%" + sNomeReclamado + "%"

            strSQL = strSQL + " AND mes_ferias like '" & sNomeReclamado.ToUpper() & "'"

        End If


        Dim Objconn As New SqlDbConnect()

        Objconn.Conectar()
        Objconn.Parametros.Clear()
        Objconn.SetarSQL(strSQL)
        Objconn.Executar()

        If Objconn.Tabela.Rows.Count > 0 Then

            For Each DataRow In Objconn.Tabela.Rows

                lblAviso.Text = "Total de Registros: " + DataRow("Total").ToString()
                lblAviso.Visible = True
            Next


        End If

        Objconn.Desconectar()



    End Sub
    Private Sub CarregarGrid(ByVal NomeReclamado As String)

        Dim myConnectionCorrigeFA01 As MySqlConnection
        Dim myDataAdapter As MySqlDataAdapter
        Dim myDataSet As DataSet
        Dim strSQL As String


        NomeReclamado = "%" + NomeReclamado + "%"

        myConnectionCorrigeFA01 = New MySqlConnection("server=172.16.0.32; user id=intranetadmin; password=Intranet@Al34m; database=scc_web; pooling=false;")

        strSQL = "SELECT matricula, nome, lotacao,1parcela13, periodo1,periodo2,periodo3,mes_ferias as MesFerias, " & _
             " CASE mes_ferias when 'JANEIRO' THEN 1 " & _
             " when 'FEVEREIRO' THEN 2 " & _
             " when 'MARÇO' THEN 3 " & _
             " when 'ABRIL' THEN 4 " & _
             " when 'MAIO' THEN 5 " & _
             " when 'JUNHO' THEN 6 " & _
             " when 'JULHO' THEN 7 " & _
             " when 'AGOSTO' THEN 8 " & _
             " when 'SETEMBRO' THEN 9 " & _
             " when 'OUTUBRO' THEN 10 " & _
             " when 'NOVEMBRO' THEN 11 " & _
             " when 'DEZEMBRO' THEN 12 " & _
             " when '0' THEN 13 END AS Mes " & _
             "FROM colaborador where tipo ='EFETIVO' and mes_ferias like '" & NomeReclamado.ToUpper() & "' order by lotacao"

        myDataAdapter = New MySqlDataAdapter(strSQL, myConnectionCorrigeFA01)

        myDataSet = New DataSet()
        myDataAdapter.Fill(myDataSet, "efetivos")
        gditens.DataSource = myDataSet

        gditens.DataBind()
        myConnectionCorrigeFA01.Close()


    End Sub

    Private Sub CarregarGridTodos()

        Dim myConnectionCorrigeFA01 As MySqlConnection
        Dim myDataAdapter As MySqlDataAdapter
        Dim myDataSet As DataSet
        Dim strSQL As String



        myConnectionCorrigeFA01 = New MySqlConnection("server=172.16.0.32; user id=intranetadmin; password=Intranet@Al34m; database=scc_web; pooling=false;")
        'If sInicio = sFinal Then

        strSQL = "SELECT matricula, nome, lotacao,1parcela13, periodo1,periodo2,periodo3,mes_ferias as MesFerias, " & _
            " CASE mes_ferias when 'JANEIRO' THEN 1 " & _
            " when 'FEVEREIRO' THEN 2 " & _
            " when 'MARÇO' THEN 3 " & _
            " when 'ABRIL' THEN 4 " & _
            " when 'MAIO' THEN 5 " & _
            " when 'JUNHO' THEN 6 " & _
            " when 'JULHO' THEN 7 " & _
            " when 'AGOSTO' THEN 8 " & _
            " when 'SETEMBRO' THEN 9 " & _
            " when 'OUTUBRO' THEN 10 " & _
            " when 'NOVEMBRO' THEN 11 " & _
            " when 'DEZEMBRO' THEN 12 " & _
            " when '0' THEN 13 END AS Mes " & _
            "FROM colaborador where tipo ='EFETIVO' order by Mes,lotacao"

        myDataAdapter = New MySqlDataAdapter(strSQL, myConnectionCorrigeFA01)

        myDataSet = New DataSet()
        myDataAdapter.Fill(myDataSet, "efetivos")
        gditens.DataSource = myDataSet

        gditens.DataBind()
        myConnectionCorrigeFA01.Close()


    End Sub
    Protected Sub TxtPesquisar_Click(sender As Object, e As EventArgs) Handles TxtPesquisar.Click
        If TxtSolicitante.Text <> "" Then


            CarregarGrid(TxtSolicitante.Text)
            CarregarTotais()


        Else

            CarregarGridTodos()
            CarregarTotais()

        End If
    End Sub
    Protected Sub gdItens_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdItens.RowCreated

        For ContadorLinhas As Integer = 0 To Me.gdItens.Rows.Count - 1

            If (Me.gdItens.Rows(ContadorLinhas).Cells(8).Text <> "") Then

                Me.gdItens.Rows(ContadorLinhas).Cells(8).Visible = False


            End If


        Next

    End Sub
End Class