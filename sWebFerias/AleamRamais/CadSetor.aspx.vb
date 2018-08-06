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
Public Class CadSetor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            CarregarSetores()
        End If

    End Sub
    Public Sub CarregarSetores()

        Dim strSQL As String


        strSQL = "SELECT distinct lotacao  FROM colaborador order by lotacao"


        Dim Objconn As New SqlDbConnect()

        Objconn.Conectar()
        Objconn.Parametros.Clear()
        Objconn.SetarSQL(strSQL)
        Objconn.Executar()

        If Objconn.Tabela.Rows.Count > 0 Then

            For Each DataRow In Objconn.Tabela.Rows

                cmbSetor.Items.Add(DataRow("lotacao").ToString())


            Next


        End If

        Objconn.Desconectar()


    End Sub

    Public Sub CarregarLotacao(Setor As String)

        Dim myConnectionCorrigeFA01 As MySqlConnection
        Dim myDataAdapter As MySqlDataAdapter
        Dim myDataSet As DataSet
        Dim strSQL As String



        myConnectionCorrigeFA01 = New MySqlConnection("server=172.16.0.32; user id=intranetadmin; password=Intranet@Al34m; database=scc_web; pooling=false;")
        'If sInicio = sFinal Then

        strSQL = "SELECT matricula, nome, tipo, diasferias  FROM colaborador where lotacao = '" & Setor & "' and TIPO ='COMISSIONADO' order by nome"

        myDataAdapter = New MySqlDataAdapter(strSQL, myConnectionCorrigeFA01)

        myDataSet = New DataSet()
        myDataAdapter.Fill(myDataSet, "lotacao")
        gdItens.DataSource = myDataSet

        gdItens.DataBind()
        myConnectionCorrigeFA01.Close()


    End Sub
    Protected Sub bntPesquisar_Click(sender As Object, e As ImageClickEventArgs) Handles bntPesquisar.Click

        Dim strSQL As String

        If cmbSetor.Text <> "" Then

            CarregarLotacao(cmbSetor.Text)

            Dim sAdmissao As String = ""
            Dim Ano As String = ""
            Dim Mes As String = ""
            Dim Dia As String = ""
            Dim Objconn As New SqlDbConnect()
            Objconn.Conectar()
            Objconn.Parametros.Clear()
            strSQL = "SELECT * FROM colaborador WHERE lotacao =  '" & Trim(cmbSetor.Text) & "'  and tipo='COMISSIONADO' LIMIT 1"
            Objconn.SetarSQL(strSQL)
            Objconn.Executar()

            If Objconn.Tabela.Rows.Count > 0 Then

                For Each DataRow In Objconn.Tabela.Rows

                    Dim sData As String
                    Dim sDataFinal As String

                    sData = DataRow("admissao").ToString()
                    Ano = Trim(Replace(Mid(sData, 1, 4), "-", ""))
                    Mes = Trim(Replace(Mid(sData, 6, 2), "-", ""))
                    Dia = Trim(Replace(Mid(sData, 9, 2), "-", ""))
                    sDataFinal = Dia + "/" + Mes + "/" + Ano

                    TxtMesFerias.Text = DataRow("mes_ferias").ToString()
                    TxtPeriodo1.Text = DataRow("periodo1").ToString()
                    TxtRetorno.Text = DataRow("retorno").ToString()
                    TxtDias.Text = DataRow("diasferias").ToString()

                Next


            Else

                TxtMesFerias.Text = ""
                TxtPeriodo1.Text = ""
                TxtRetorno.Text = ""
                TxtDias.Text = ""

            End If

            Objconn.Desconectar()

        End If


    End Sub
    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        Dim sNome As String
        Dim sMatricula As String
        Dim sSQL As String
        Dim Objconn As New SqlDbConnect()


        '18/12/2017 09:46h
        ' SALVAR OS DADOS EM CADA UM DOS ITENS CONTIDOS NO GRIDVIEW

        For ContadorLinhas As Integer = 0 To Me.gdItens.Rows.Count - 1

            'Selecionar os dados necessários
            sMatricula = Me.gdItens.Rows(ContadorLinhas).Cells(0).Text
            sNome = Me.gdItens.Rows(ContadorLinhas).Cells(1).Text

            Objconn.Conectar()
            Objconn.Parametros.Clear()
            sSQL = "UPDATE colaborador SET mes_ferias = '" + TxtMesFerias.Text.ToUpper() & "', " &
                             "periodo1  = '" & TxtPeriodo1.Text & "', " &
                             "retorno = '" & TxtRetorno.Text & "', " &
                             "diasferias = '" & TxtDias.Text & "' WHERE matricula = '" & Trim(sMatricula) & "' "

            Objconn.SetarSQL(ssQL)

            Objconn.Executar()

            If Objconn.Executar() = False Then

                lblAviso.Text = "Erro de alteração de dados."
                lblAviso.Visible = True

            Else

                lblAviso.Text = "Alteração efetuada com sucesso."
                lblAviso.Visible = True

            End If

            Objconn.Desconectar()


        Next



    End Sub

    Protected Sub gdItens_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdItens.RowCreated

        'Matricula
        e.Row.Cells(0).Width = New Unit(10, UnitType.Mm)
        e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(0).Wrap = False

        'Nome do servidor
        e.Row.Cells(1).Width = New Unit(30, UnitType.Mm)
        e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(1).Wrap = False

        'Comissionado
        e.Row.Cells(2).Width = New Unit(15, UnitType.Mm)
        e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(2).Wrap = False

        'Dias Férias
        e.Row.Cells(3).Width = New Unit(10, UnitType.Mm)
        e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
        e.Row.Cells(3).Wrap = False

    End Sub
End Class