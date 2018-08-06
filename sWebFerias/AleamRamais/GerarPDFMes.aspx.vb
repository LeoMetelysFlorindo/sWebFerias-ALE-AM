'Aplicativo para Férias do RH
'Data de criação: 13/01/2016 09:32h
'Desenvolvedor: Leonardo Metelys - Gerência de Aplicativos - ALEAM
'Formulário de Geração Automática de PDF´s por mês de Comissionados

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
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportAppServer.ReportDefModel
Imports CrystalDecisions.ReportSource

Public Class GerarPDFMes
    Inherits System.Web.UI.Page

    Public sContador As Integer

    Dim sMatricula As String
    Dim sTipoServidor As String
    Dim sNomeServidor As String
    Dim sLotacao As String
    Dim sMesFerias As String
    Dim sPeriodo1 As String
    Dim sRetornoFerias As String
    Dim sDiasFerias As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            TxtMes.Focus()

        End If
    End Sub

    Protected Sub TxtPesquisar_Click(sender As Object, e As EventArgs) Handles TxtPesquisar.Click

        If TxtMes.Text <> "" And TxtDRTInicio.Text = "" And TxtDRTFim.Text = "" Then


            lblAviso.Text = "Total de Registros: 0"
            lblAviso.Visible = True
            GerarDadosMes(TxtMes.Text)

        End If

        If TxtMes.Text <> "" And TxtDRTInicio.Text <> "" And TxtDRTFim.Text <> "" Then

            lblAviso.Text = "Total de Registros: 0"
            lblAviso.Visible = True
            GerarDRT(TxtMes.Text, TxtDRTInicio.Text, TxtDRTFim.Text)

        End If



    End Sub
    Private Sub GerarDRT(Mes As String, MatInicial As String, MatFinal As String)

        Dim strSQL As String

        Mes = "%" + Mes + "%"

        strSQL = "SELECT matricula, nome, lotacao,1parcela13, periodo1,periodo2,periodo3,mes_ferias as MesFerias, diasferias, " & _
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
             "FROM colaborador where tipo ='COMISSIONADO' and mes_ferias like '" & Mes.ToUpper() & "' order by Mes,lotacao LIMIT " & MatInicial & ", " & MatFinal & ""

        Dim Objconn As New SqlDbConnect()


        Objconn.Conectar()

        Objconn.Parametros.Clear()

        Objconn.SetarSQL(strSQL)

        Objconn.Executar()

        sContador = 0
        If Objconn.Tabela.Rows.Count > 0 Then

            For Each DataRow In Objconn.Tabela.Rows

                sMatricula = DataRow("matricula").ToString()

                ExportarPDF(sMatricula)

                sContador = sContador + 1

                System.Windows.Forms.Application.DoEvents()

            Next


        End If

        Objconn.Desconectar()

        lblAviso.Text = "Registros gerados: " + CStr(sContador)
        lblAviso.Visible = True

    End Sub


    Private Sub GerarDadosMes(Mes As String)


        Dim strSQL As String

        Mes = "%" + Mes + "%"

        strSQL = "SELECT matricula, nome, lotacao,1parcela13, periodo1,periodo2,periodo3,mes_ferias as MesFerias,diasferias,  " & _
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
             "FROM colaborador where tipo ='COMISSIONADO' and mes_ferias like '" & Mes.ToUpper() & "' order by Mes,lotacao"

        Dim Objconn As New SqlDbConnect()


        Objconn.Conectar()

        Objconn.Parametros.Clear()

        Objconn.SetarSQL(strSQL)

        Objconn.Executar()

        sContador = 0
        If Objconn.Tabela.Rows.Count > 0 Then

            For Each DataRow In Objconn.Tabela.Rows

                sMatricula = DataRow("matricula").ToString()

                ExportarPDF(sMatricula)

                sContador = sContador + 1

                System.Windows.Forms.Application.DoEvents()

            Next


        End If

        Objconn.Desconectar()

        lblAviso.Text = "Registros gerados: " + CStr(sContador)
        lblAviso.Visible = True


    End Sub

    Private Sub ExportarPDF(Matricula As String)

        Dim CrReport As New rptRelFerias() '// Report Name
        Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
        Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
        Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()

        sMatricula = Request.QueryString("id")

        sCarregarRelatorio(Matricula)

        'passa parametros de conexão com a base de dados
        CrReport.SetDatabaseLogon("scc_web", "Intranet@Al34m")

        Dim crParameterDiscreteValue As ParameterDiscreteValue
        Dim crParameterFieldDefinitions As ParameterFieldDefinitions
        Dim crParameterFieldLocation As ParameterFieldDefinition
        Dim crParameterValues As ParameterValues

        crParameterFieldDefinitions = CrReport.DataDefinition.ParameterFields

        crParameterFieldLocation = crParameterFieldDefinitions.Item("@TipoServidor")
        crParameterValues = crParameterFieldLocation.CurrentValues
        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        'obtem o valor do Tipo do Servidor
        crParameterDiscreteValue.Value = sTipoServidor
        crParameterValues.Add(crParameterDiscreteValue)
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


        '
        crParameterFieldLocation = crParameterFieldDefinitions.Item("@NomeServidor")
        crParameterValues = crParameterFieldLocation.CurrentValues
        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        'obtem o valor do nome do Reclamado
        crParameterDiscreteValue.Value = sNomeServidor
        crParameterValues.Add(crParameterDiscreteValue)
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


        crParameterFieldLocation = crParameterFieldDefinitions.Item("@Lotacao")
        crParameterValues = crParameterFieldLocation.CurrentValues
        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        'obtem o valor do Endereço do Reclamado
        crParameterDiscreteValue.Value = sLotacao
        crParameterValues.Add(crParameterDiscreteValue)
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


        crParameterFieldLocation = crParameterFieldDefinitions.Item("@diasferias")
        crParameterValues = crParameterFieldLocation.CurrentValues
        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        'obtem o valor da Hora da Audiêmcia
        crParameterDiscreteValue.Value = sDiasFerias
        crParameterValues.Add(crParameterDiscreteValue)
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

        crParameterFieldLocation = crParameterFieldDefinitions.Item("@mesferias")
        crParameterValues = crParameterFieldLocation.CurrentValues
        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        'obtem o valor do códifo do Reclamado
        crParameterDiscreteValue.Value = sMesFerias
        crParameterValues.Add(crParameterDiscreteValue)
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

        crParameterFieldLocation = crParameterFieldDefinitions.Item("@periodo1")
        crParameterValues = crParameterFieldLocation.CurrentValues
        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        'obtem o valor da Reclamação
        crParameterDiscreteValue.Value = sPeriodo1
        crParameterValues.Add(crParameterDiscreteValue)
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

        crParameterFieldLocation = crParameterFieldDefinitions.Item("@Retorno")
        crParameterValues = crParameterFieldLocation.CurrentValues
        crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
        'obtem o valor da Hora da Audiêmcia
        crParameterDiscreteValue.Value = sRetornoFerias
        crParameterValues.Add(crParameterDiscreteValue)
        crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


        CrDiskFileDestinationOptions.DiskFileName = "\\172.16.0.31\Ferias\" + TxtMes.Text + "_" + Matricula + ".pdf"

        CrFormatTypeOptions.FirstPageNumber = 1 '// Start Page in the Report
        CrFormatTypeOptions.LastPageNumber = 1 '// End Page in the Report
        CrFormatTypeOptions.UsePageRange = True

        CrExportOptions = CrReport.ExportOptions

        With CrExportOptions

            '// Set the destination to a disk file
            .ExportDestinationType = ExportDestinationType.DiskFile

            '// Set the format to PDF
            .ExportFormatType = ExportFormatType.PortableDocFormat

            '// Set the destination options to DiskFileDestinationOptions object
            .DestinationOptions = CrDiskFileDestinationOptions
            .FormatOptions = CrFormatTypeOptions

        End With

        Try
            '// Export the report
            CrReport.Export()
            lblAviso.Text = "PDF gerado com sucesso! Consulte a pasta no servidor!"
            lblAviso.Visible = True
        Catch err As Exception
            lblAviso.Text = err.ToString()
            lblAviso.Visible = True
        End Try

        'Fechar a conexão no fim
        CrReport.Close()

    End Sub

    Private Sub sCarregarRelatorio(sCodReclamacao As String)


        'Pegar dados da Reclamação
        Dim Objconn As New SqlDbConnect()
        Objconn.Conectar()
        Objconn.Parametros.Clear()

        'Gerar automaticamente o próximo código de registro
        Objconn.SetarSQL("SELECT * from colaborador where matricula = " & sCodReclamacao)
        Objconn.Executar()

        If Objconn.Tabela.Rows.Count > 0 Then

            For Each DataRow In Objconn.Tabela.Rows

                sTipoServidor = DataRow("tipo").ToString()
                sNomeServidor = DataRow("nome").ToString() + " - " + DataRow("matricula").ToString()
                sLotacao = DataRow("lotacao").ToString()
                sMesFerias = DataRow("mes_ferias").ToString()
                sPeriodo1 = DataRow("periodo1").ToString()
                sRetornoFerias = DataRow("retorno").ToString() + "."
                sDiasFerias = DataRow("diasferias").ToString()

            Next


        End If


        Objconn.Desconectar()



    End Sub

   
End Class