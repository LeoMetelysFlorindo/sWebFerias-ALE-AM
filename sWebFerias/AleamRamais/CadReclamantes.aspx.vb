'Aplicativo para as Férias de Funcionarios 
'Data de criação: 06/01/2016 10:42
'Desenvolvedor: Leonardo Metelys - Gerência de Aplicativos - ALEAM
'Formulário de Cadastro dos Comisisonados

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

Public Class CadReclamados
    Inherits System.Web.UI.Page

    Dim sMatricula As String
    Dim sTipoServidor As String
    Dim sNomeServidor As String
    Dim sLotacao As String
    Dim sMesFerias As String
    Dim sPeriodo1 As String
    Dim sRetornoFerias As String

    Dim strSQL As String = ""
    'Conexão com o MYSQL
    Const ConnStr As String = "Driver={MySQL ODBC 5.1 Driver};" + "Server=172.16.0.32;Database=scc_web;uid=intranetadmin;pwd=Intranet@Al34m;option=3"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Pegar os dados do usuário logado
        If Not Page.IsPostBack Then

            'Dim currentADUser As System.DirectoryServices.AccountManagement.UserPrincipal
            'currentADUser = System.DirectoryServices.AccountManagement.UserPrincipal.Current
            'Dim userEmail As String = currentADUser.EmailAddress
            'Dim sUsuario As String = currentADUser.Name

            ''Informando o nome do usuário logado na rede
            ''sUsuario = Request.QueryString("id")
            'LbUsuario.Text = "Usuário Login: " + sUsuario
            'LbUsuario.Visible = True

            txtCodigo.Focus()

        End If
    End Sub

    Protected Sub BtnMenu_Click(sender As Object, e As EventArgs) Handles BtnMenu.Click, BtnMenu.Click, BtnMenu.Click, BtnMenu.Click, BtnMenu.Click, BtnMenu.Click, BtnMenu.Click, BtnMenu.Click

        'Repassar ao relatório o parâmetro da mátricula do funcionário
        Response.Redirect("frmAbrirRelatorio.aspx?id=" & txtCodigo.Text)
    End Sub


    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        txtCodigo.Enabled = True

        txtCodigo.Text = ""
        TxtNome.Text = ""
        TxtLotacao.Text = ""
        TxtAdmissao.Text = ""
        TxtMesFerias.Text = ""
        TxtParcela13.Text = ""
        TxtPeriodo1.Text = ""
        TxtPeriodo2.Text = ""
        TxtPeriodo3.Text = ""
        TxtObs.Text = ""
        TxtAtivo.Text = ""
        TxtNascimento.Text = ""
        TxtEmail.Text = ""
        TxtRetorno.Text = ""
        TxtDias.Text = ""
        lblAviso.Visible = False
        txtCodigo.Focus()


    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click, BtnNovo.Click

        Dim sEfetivo As String = "COMISSIONADO"

        If txtCodigo.Text <> "" Then

            If TxtNome.Text = "" Then

                lblAviso.Text = "Nome Efetivo não informado!!"
                lblAviso.Visible = True

                Exit Sub

            End If

            If TxtLotacao.Text = "" Then

                lblAviso.Text = "Lotação Efetivo não informada!!"
                lblAviso.Visible = True

                Exit Sub

            End If

            If TxtAdmissao.Text = "" Then

                lblAviso.Text = "Admissão Efetivo não informada!!"
                lblAviso.Visible = True

                Exit Sub

            End If


            If TxtMesFerias.Text = "" Then

                lblAviso.Text = "Mês Férias não informado!!"
                lblAviso.Visible = True

                Exit Sub

            End If



            Dim Objconn As New SqlDbConnect()
            Dim ssQL As String = "SELECT matricula FROM colaborador WHERE matricula  =  '" & Trim(txtCodigo.Text) & "' and tipo='COMISSIONADO' "

            Objconn.Conectar()

            Objconn.Parametros.Clear()

            Objconn.SetarSQL(ssQL)

            Objconn.Executar()

            '
            If Objconn.Tabela.Rows.Count > 0 Then



                Objconn.Conectar()
                Objconn.Parametros.Clear()
                ssQL = "UPDATE colaborador SET nome = '" + TxtNome.Text.ToUpper() & "', " & _
                                 "lotacao = '" & TxtLotacao.Text.ToUpper() & "', " & _
                                 "admissao = '" & TxtAdmissao.Text & "', " & _
                                 "mes_ferias = '" & TxtMesFerias.Text.ToUpper() & "', 1parcela13  = '" & TxtParcela13.Text.ToUpper() & "', " & _
                                 "periodo1  = '" & TxtPeriodo1.Text & "', periodo2  = '" & TxtPeriodo2.Text & "', periodo3 = '" & TxtPeriodo3.Text & "', " & _
                                 "obs = '" & TxtObs.Text.ToUpper() & "', " & _
                                 "datanasc = '" & TxtNascimento.Text & "', " & _
                                 "email = '" & TxtEmail.Text & "', " & _
                                 "retorno = '" & TxtRetorno.Text & "', " & _
                                 "diasferias = '" & TxtDias.Text & "', " & _
                                 "ativo = '" & TxtAtivo.Text.ToUpper() & "' WHERE matricula = '" & Trim(txtCodigo.Text) & "' "

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

            Else


                Objconn.Conectar()
                Objconn.Parametros.Clear()

                ssQL = "INSERT INTO colaborador (matricula,nome,lotacao,admissao,mes_ferias,1parcela13,periodo1,periodo2,periodo3,obs,tipo,ativo,datanasc,email, retorno, diasferias) VALUES ('" & Trim(txtCodigo.Text) & "','" & _
                                 TxtNome.Text.ToUpper() & "','" & TxtLotacao.Text.ToUpper() & "','" & TxtAdmissao.Text & "','" & _
                                 Trim(TxtMesFerias.Text.ToUpper) & "','" & Trim(TxtParcela13.Text.ToUpper()) & "','" & TxtPeriodo1.Text & "','" & TxtPeriodo2.Text & "','" & TxtPeriodo3.Text & "','" & _
                                 TxtObs.Text.ToUpper() & "','" & sEfetivo & "','" & TxtAtivo.Text.ToUpper() & "','" & TxtNascimento.Text & "','" & TxtEmail.Text & "','" & TxtRetorno.Text & "','" & TxtDias.Text & "')"

                Objconn.SetarSQL(ssQL)

                If Objconn.Executar() = False Then

                    lblAviso.Text = "Erro de inserção de dados."
                    lblAviso.Visible = True
                Else

                    lblAviso.Text = "Inserção efetuada com sucesso."
                    lblAviso.Visible = True

                End If

                Objconn.Desconectar()

            End If



        End If

    End Sub

    Protected Sub bntPesquisar_Click(sender As Object, e As ImageClickEventArgs) Handles bntPesquisar.Click

        If txtCodigo.Text <> "" Then



            Dim sAdmissao As String = ""
            Dim Ano As String = ""
            Dim Mes As String = ""
            Dim Dia As String = ""
            Dim Objconn As New SqlDbConnect()
            Objconn.Conectar()
            Objconn.Parametros.Clear()
            strSQL = "SELECT * FROM colaborador WHERE matricula =  '" & Trim(txtCodigo.Text) & "'  and tipo='COMISSIONADO'"
            Objconn.SetarSQL(strSQL)
            Objconn.Executar()

            If Objconn.Tabela.Rows.Count > 0 Then

                For Each DataRow In Objconn.Tabela.Rows

                    TxtNome.Text = DataRow("nome").ToString()
                    TxtLotacao.Text = DataRow("lotacao").ToString()

                    Dim sData As String
                    Dim sDataFinal As String

                    '2017-12-01

                    sData = DataRow("admissao").ToString()
                    Ano = Trim(Replace(Mid(sData, 1, 4), "-", ""))
                    Mes = Trim(Replace(Mid(sData, 6, 2), "-", ""))
                    Dia = Trim(Replace(Mid(sData, 9, 2), "-", ""))
                    sDataFinal = Dia + "/" + Mes + "/" + Ano

                    TxtAdmissao.Text = sDataFinal



                    TxtMesFerias.Text = DataRow("mes_ferias").ToString()
                    TxtParcela13.Text = DataRow("1parcela13").ToString()
                    TxtPeriodo1.Text = DataRow("periodo1").ToString()
                    TxtPeriodo2.Text = DataRow("periodo2").ToString()
                    TxtPeriodo3.Text = DataRow("periodo3").ToString()
                    TxtObs.Text = DataRow("obs").ToString()
                    TxtAtivo.Text = DataRow("ativo").ToString()
                    TxtNascimento.Text = DataRow("datanasc").ToString()
                    TxtEmail.Text = DataRow("email").ToString()
                    TxtRetorno.Text = DataRow("retorno").ToString()
                    TxtDias.Text = DataRow("diasferias").ToString()

                Next

                txtCodigo.Enabled = False

            Else

                txtCodigo.Enabled = False

                TxtNome.Text = ""
                TxtLotacao.Text = ""
                TxtAdmissao.Text = ""
                TxtMesFerias.Text = ""
                TxtParcela13.Text = ""
                TxtPeriodo1.Text = ""
                TxtPeriodo2.Text = ""
                TxtPeriodo3.Text = ""
                TxtObs.Text = ""
                TxtAtivo.Text = ""
                TxtNascimento.Text = ""
                TxtEmail.Text = ""
                TxtRetorno.Text = ""
                TxtNome.Focus()

            End If

            Objconn.Desconectar()

        End If

    End Sub

    Protected Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged

        If txtCodigo.Text <> "" Then


            Dim sAdmissao As String = ""
            Dim Ano As String = ""
            Dim Mes As String = ""
            Dim Dia As String = ""
            Dim Objconn As New SqlDbConnect()
            Objconn.Conectar()
            Objconn.Parametros.Clear()
            strSQL = "SELECT * FROM colaborador WHERE matricula =  '" & Trim(txtCodigo.Text) & "' and tipo='COMISSIONADO' "
            Objconn.SetarSQL(strSQL)
            Objconn.Executar()

            If Objconn.Tabela.Rows.Count > 0 Then

                For Each DataRow In Objconn.Tabela.Rows

                    TxtNome.Text = DataRow("nome").ToString()

                    'If Not IsDBNull(DataRow("CNPJ").ToString()) Then
                    '    TxtCnpj.Text = DataRow("CNPJ").ToString()
                    'End If

                    'If Not IsDBNull(DataRow("CPF").ToString()) Then
                    '    TxtCnpj.Text = DataRow("CPF").ToString()
                    'End If

                    TxtLotacao.Text = DataRow("lotacao").ToString()
                    TxtAdmissao.Text = DataRow("admissao").ToString()
                    TxtMesFerias.Text = DataRow("mes_ferias").ToString()
                    TxtParcela13.Text = DataRow("1parcela13").ToString()
                    TxtPeriodo1.Text = DataRow("periodo1").ToString()
                    TxtPeriodo2.Text = DataRow("periodo2").ToString()
                    TxtPeriodo3.Text = DataRow("periodo3").ToString()
                    TxtObs.Text = DataRow("obs").ToString()
                    TxtAtivo.Text = DataRow("ativo").ToString()
                    TxtNascimento.Text = DataRow("datanasc").ToString()
                    TxtEmail.Text = DataRow("email").ToString()
                    TxtRetorno.Text = DataRow("retorno").ToString()
                    TxtDias.Text = DataRow("diasferias").ToString()

                Next

                txtCodigo.Enabled = False

            Else

                txtCodigo.Enabled = False
                TxtNome.Text = ""
                TxtLotacao.Text = ""
                TxtAdmissao.Text = ""
                TxtMesFerias.Text = ""
                TxtParcela13.Text = ""
                TxtPeriodo1.Text = ""
                TxtPeriodo2.Text = ""
                TxtPeriodo3.Text = ""
                TxtObs.Text = ""
                TxtAtivo.Text = ""
                TxtNascimento.Text = ""
                TxtEmail.Text = ""
                TxtRetorno.Text = ""

                TxtNome.Focus()

            End If

            Objconn.Desconectar()


        End If
       
    End Sub

    Protected Sub btnNovo_Click(sender As Object, e As EventArgs) Handles BtnNovo.Click, BtnNovo.Click, BtnNovo.Click

        txtCodigo.Text = ""
        txtCodigo.Enabled = True
        TxtNome.Text = ""
        TxtLotacao.Text = ""
        TxtAdmissao.Text = ""
        TxtMesFerias.Text = ""
        TxtParcela13.Text = ""
        TxtPeriodo1.Text = ""
        TxtPeriodo2.Text = ""
        TxtPeriodo3.Text = ""

        TxtObs.Text = ""
        TxtAtivo.Text = ""
        TxtNascimento.Text = ""
        TxtEmail.Text = ""
        TxtRetorno.Text = ""

        txtCodigo.Focus()


    End Sub

    Protected Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        If txtCodigo.Text <> "" Then

            ExportarPDF(txtCodigo.Text)

        End If

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


        CrDiskFileDestinationOptions.DiskFileName = "\\172.16.0.31\Ferias\" + Matricula + ".pdf"

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
End Class