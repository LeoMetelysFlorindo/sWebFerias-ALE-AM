Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net.Mail
Imports System.Data.Odbc
Imports System.Data.OleDb
Imports System.TimeSpan
Imports System.Globalization.CultureInfo
Imports MySql.Data.MySqlClient
Imports System.Threading.Thread
Imports System.Globalization
Imports System.IO
Imports AleamRamais.Classes
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportAppServer.ReportDefModel
Imports CrystalDecisions.ReportSource

Public Class frmAbrirRelatorio

    Inherits System.Web.UI.Page

    Dim sMatricula As String
    Dim sTipoServidor As String
    Dim sNomeServidor As String
    Dim sLotacao As String
    Dim sMesFerias As String
    Dim sPeriodo1 As String
    Dim sRetornoFerias As String
    Public Report As CrystalDecisions.CrystalReports.Engine.ReportDocument = New CrystalDecisions.CrystalReports.Engine.ReportDocument()
    Dim sSQQL As String
    'Conexão com o MYSQL
    Const ConnStr As String = "Driver={MySQL ODBC 5.1 Driver};" + "Server=172.16.0.32;Database=scc_web;uid=intranetadmin;pwd=Intranet@Al34m;option=3"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        'Pegar os dados do usuário logado
        If Not Page.IsPostBack Then

           

            'Pegando o Código da Reclamação
            sMatricula = Request.QueryString("id")

            sCarregarRelatorio(sMatricula)

            Dim myRelatorio As New rptRelFerias()

            'passa parametros de conexão com a base de dados
            myRelatorio.SetDatabaseLogon("scc_web", "Intranet@Al34m")

            Dim crParameterDiscreteValue As ParameterDiscreteValue
            Dim crParameterFieldDefinitions As ParameterFieldDefinitions
            Dim crParameterFieldLocation As ParameterFieldDefinition
            Dim crParameterValues As ParameterValues

            crParameterFieldDefinitions = myRelatorio.DataDefinition.ParameterFields

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


            Me.crView.ReportSource = myRelatorio
            Me.crView.Visible = True

            'ExportarPDF(sMatricula)

            'myRelatorio.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, True, "rptRelFerias")

           

        End If


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
                sRetornoFerias = DataRow("retorno").ToString()

            Next


        End If


        Objconn.Desconectar()



    End Sub

    'Private Sub ExportarPDF(Matricula As String)

    '    Dim CrReport As New rptRelFerias() '// Report Name
    '    Dim CrExportOptions As CrystalDecisions.Shared.ExportOptions
    '    Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
    '    Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()

    '    sMatricula = Request.QueryString("id")

    '    sCarregarRelatorio(sMatricula)

    '    'passa parametros de conexão com a base de dados
    '    CrReport.SetDatabaseLogon("scc_web", "Intranet@Al34m")

    '    Dim crParameterDiscreteValue As ParameterDiscreteValue
    '    Dim crParameterFieldDefinitions As ParameterFieldDefinitions
    '    Dim crParameterFieldLocation As ParameterFieldDefinition
    '    Dim crParameterValues As ParameterValues

    '    crParameterFieldDefinitions = CrReport.DataDefinition.ParameterFields

    '    crParameterFieldLocation = crParameterFieldDefinitions.Item("@TipoServidor")
    '    crParameterValues = crParameterFieldLocation.CurrentValues
    '    crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    '    'obtem o valor do Tipo do Servidor
    '    crParameterDiscreteValue.Value = sTipoServidor
    '    crParameterValues.Add(crParameterDiscreteValue)
    '    crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


    '    '
    '    crParameterFieldLocation = crParameterFieldDefinitions.Item("@NomeServidor")
    '    crParameterValues = crParameterFieldLocation.CurrentValues
    '    crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    '    'obtem o valor do nome do Reclamado
    '    crParameterDiscreteValue.Value = sNomeServidor
    '    crParameterValues.Add(crParameterDiscreteValue)
    '    crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


    '    crParameterFieldLocation = crParameterFieldDefinitions.Item("@Lotacao")
    '    crParameterValues = crParameterFieldLocation.CurrentValues
    '    crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    '    'obtem o valor do Endereço do Reclamado
    '    crParameterDiscreteValue.Value = sLotacao
    '    crParameterValues.Add(crParameterDiscreteValue)
    '    crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

    '    crParameterFieldLocation = crParameterFieldDefinitions.Item("@mesferias")
    '    crParameterValues = crParameterFieldLocation.CurrentValues
    '    crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    '    'obtem o valor do códifo do Reclamado
    '    crParameterDiscreteValue.Value = sMesFerias
    '    crParameterValues.Add(crParameterDiscreteValue)
    '    crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

    '    crParameterFieldLocation = crParameterFieldDefinitions.Item("@periodo1")
    '    crParameterValues = crParameterFieldLocation.CurrentValues
    '    crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    '    'obtem o valor da Reclamação
    '    crParameterDiscreteValue.Value = sPeriodo1
    '    crParameterValues.Add(crParameterDiscreteValue)
    '    crParameterFieldLocation.ApplyCurrentValues(crParameterValues)

    '    crParameterFieldLocation = crParameterFieldDefinitions.Item("@Retorno")
    '    crParameterValues = crParameterFieldLocation.CurrentValues
    '    crParameterDiscreteValue = New CrystalDecisions.Shared.ParameterDiscreteValue
    '    'obtem o valor da Hora da Audiêmcia
    '    crParameterDiscreteValue.Value = sRetornoFerias
    '    crParameterValues.Add(crParameterDiscreteValue)
    '    crParameterFieldLocation.ApplyCurrentValues(crParameterValues)


    '    CrDiskFileDestinationOptions.DiskFileName = "\\172.16.0.31\Ferias\" + sMatricula + ".pdf"

    '    CrFormatTypeOptions.FirstPageNumber = 1 '// Start Page in the Report
    '    CrFormatTypeOptions.LastPageNumber = 1 '// End Page in the Report
    '    CrFormatTypeOptions.UsePageRange = True

    '    CrExportOptions = CrReport.ExportOptions

    '    With CrExportOptions

    '        '// Set the destination to a disk file
    '        .ExportDestinationType = ExportDestinationType.DiskFile

    '        '// Set the format to PDF
    '        .ExportFormatType = ExportFormatType.PortableDocFormat

    '        '// Set the destination options to DiskFileDestinationOptions object
    '        .DestinationOptions = CrDiskFileDestinationOptions
    '        .FormatOptions = CrFormatTypeOptions

    '    End With

    '    Try
    '        '// Export the report
    '        CrReport.Export()
    '        lblAviso.Text = "PDF gerado com sucesso! Consulte a pasta no servidor!"
    '        lblAviso.Visible = True
    '    Catch err As Exception
    '        lblAviso.Text = err.ToString()
    '        lblAviso.Visible = True
    '    End Try

    'End Sub

    'Protected Sub Img1_Click(sender As Object, e As ImageClickEventArgs) Handles Img1.Click

    '    ExportarPDF(sMatricula)

    'End Sub
End Class