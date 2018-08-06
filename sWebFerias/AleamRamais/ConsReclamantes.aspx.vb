'Aplicativo para a Comissão de Defesa do Consumidor
'Data de criação: 19/11/2015 10:41h
'Desenvolvedor: Leonardo Metelys - Gerência de Aplicativos - ALEAM
'Formulário de Consulta de Reclamantes

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

Public Class ConsReclamantes


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

    Protected Sub TxtPesquisar_Click(sender As Object, e As EventArgs) Handles TxtPesquisar.Click

        If TxtSolicitante.Text <> "" Then


            CarregarGrid(TxtSolicitante.Text)
            CarregarTotais()


        Else

            CarregarGridTodos()
            CarregarTotais()

        End If


    End Sub
    Private Sub CarregarTotais()



        Dim strSQL As String


        strSQL = "SELECT count(matricula) as Total FROM colaborador WHERE tipo = 'COMISSIONADO'"



        If TxtSolicitante.Text <> "" Then

            Dim sNomeReclamado As String = TxtSolicitante.Text
            sNomeReclamado = "%" + sNomeReclamado + "%"

            strSQL = strSQL + " AND nome like '" & sNomeReclamado.ToUpper() & "'"

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
        'If sInicio = sFinal Then

        strSQL = "SELECT *  FROM colaborador where nome like '" & NomeReclamado.ToUpper() & "' AND tipo ='COMISSIONADO' "

        myDataAdapter = New MySqlDataAdapter(strSQL, myConnectionCorrigeFA01)

        myDataSet = New DataSet()
        myDataAdapter.Fill(myDataSet, "comissionados")
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

        strSQL = "SELECT * FROM colaborador where tipo ='COMISSIONADO' order by NOME"

        myDataAdapter = New MySqlDataAdapter(strSQL, myConnectionCorrigeFA01)

        myDataSet = New DataSet()
        myDataAdapter.Fill(myDataSet, "comissionados")
        gditens.DataSource = myDataSet

        gditens.DataBind()
        myConnectionCorrigeFA01.Close()


    End Sub

    Protected Sub gdItens_DataBound(sender As Object, e As EventArgs) Handles gdItens.DataBound

        For ContadorLinhas As Integer = 0 To Me.gdItens.Rows.Count - 1

            If (Me.gdItens.Rows(ContadorLinhas).Cells(11).Text = "NÃO") Or (Me.gdItens.Rows(ContadorLinhas).Cells(11).Text = "N&#195;O") Then

                Me.gdItens.Rows(ContadorLinhas).BackColor = Drawing.Color.Red
                Me.gdItens.Rows(ContadorLinhas).ForeColor = Drawing.Color.White
                Me.gdItens.Rows(ContadorLinhas).Font.Bold = True


            End If


           


        Next

    End Sub

    Protected Sub gdItens_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdItens.RowCreated

        'Descrição do Tipo de Projeto
        'e.Row.Cells(2).Width = New Unit(30, UnitType.Mm)
        'e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
        'e.Row.Cells(2).Wrap = False

    End Sub
End Class