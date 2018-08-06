'Aplicativo para as Férias de Funcionários
'Data de criação: 08/01/2016 10:27
'Desenvolvedor: Leonardo Metelys - Gerência de Aplicativos - ALEAM
'Formulário de Mala Direta para os Efetivos
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

Public Class EnviarEmailEfetivos
    Inherits System.Web.UI.Page
    Dim strSQL As String = ""
    'Conexão com o MYSQL
    Const ConnStr As String = "Driver={MySQL ODBC 5.1 Driver};" + "Server=172.16.0.32;Database=scc_web;uid=intranetadmin;pwd=Intranet@Al34m;option=3"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            TxtEmailOrigem.Text = "rodolpho.matsui@aleam.gov.br"
            txtCodigo.Focus()

        End If

    End Sub

    Protected Sub btnNovo_Click(sender As Object, e As EventArgs) Handles BtnNovo.Click
        'Enviar E-mail ao Efetivo Selecionado
        EnviarEmail(TxtEmailOrigem.Text, "AVISO DE FÉRIAS", TxtEmailDestino.Text, "", " Prezado(a). Esta mensagem está sendo enviada de modo automatica. Em anexo segue eu aviso de férias. ")


    End Sub
    Public Sub EnviarEmail(ByVal sRemetente As String, ByVal Assunto As String, ByVal sDestinatario As String, ByVal Cc As String, ByVal sBody As String)

        'Dim currentADUser As System.DirectoryServices.AccountManagement.UserPrincipal
        'currentADUser = System.DirectoryServices.AccountManagement.UserPrincipal.Current
        'Dim userEmail As String = currentADUser.EmailAddress
        Dim sUsuario As String = "Gerência de Lotação"


        Dim AttachmentFile As String = TxtArquivo.Text

        'Host da porta SMTP
        Dim SMTP As String

        SMTP = "webmail.aleam.gov.br"

        Dim assuntoMensagem As String
        Dim conteudoMensagem As String

        assuntoMensagem = Assunto
        conteudoMensagem = sBody


        'Cria objeto com dados do e-mail.
        Dim objEmail As New System.Net.Mail.MailMessage()

        'Define o Campo From e ReplyTo do e-mail.
        objEmail.From = New System.Net.Mail.MailAddress("<" & sRemetente & ">")
        'objEmail.ReplyTo = New System.Net.Mail.MailAddress("Nome <email@seudominio.com.br>")

        'Define os destinatários do e-mail.
        objEmail.To.Add("<" & sDestinatario & ">")

        'Define a prioridade do e-mail.
        objEmail.Priority = System.Net.Mail.MailPriority.Normal

        'Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
        objEmail.IsBodyHtml = True

        Dim attachment As System.Net.Mail.Attachment
        attachment = New System.Net.Mail.Attachment(AttachmentFile)
        objEmail.Attachments.Add(attachment)


        'Define o título do e-mail.
        objEmail.Subject = assuntoMensagem

        'Define o corpo do e-mail.
        objEmail.Body = "<b>" & conteudoMensagem & "</b>"

        'Para evitar problemas com caracteres "estranhos", configuramos o Charset para "ISO-8859-1"
        objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")
        objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1")



        'Cria objeto com os dados do SMTP
        Dim objSmtp As New System.Net.Mail.SmtpClient(SMTP, 587)

        'Alocamos o endereço do host para enviar os e-mails  
        objSmtp.Credentials = New System.Net.NetworkCredential(sRemetente, "3007722108")
        objSmtp.Host = SMTP
        objSmtp.Port = 25

        'Caso utilize conta de email do exchange da locaweb deve habilitar o SSL
        'objEmail.EnableSsl = true;

        'Enviamos o e-mail através do método .send()

        Try
            objSmtp.Send(objEmail)
            lblAviso.Text = ("E-mail enviado com sucesso !")
            lblAviso.Visible = True

        Catch ex As Exception
            lblAviso.Text = ("Ocorreram problemas no envio do e-mail. Erro = " & ex.Message)
            lblAviso.Visible = True

        End Try
        'excluímos o objeto de e-mail da memória
        objEmail.Dispose()
        'anexo.Dispose();


    End Sub
    Private Function FileExists(ByVal FileFullPath As String) _
     As Boolean
        If Trim(FileFullPath) = "" Then Return False

        Dim f As New IO.FileInfo(FileFullPath)
        Return f.Exists

    End Function
    Public Sub EnviarEmail()


        Dim currentADUser As System.DirectoryServices.AccountManagement.UserPrincipal
        currentADUser = System.DirectoryServices.AccountManagement.UserPrincipal.Current
        Dim userEmail As String = currentADUser.EmailAddress

        '------------ MANDANDO E-MAILS PELO GMAIL
        'Dim NumeroFigura As String = "C:\temp\winnt" + "imagem1.jpg"
        Const destino As String = "johan_orquiz@jabil.com"

        Const Body As String = " Esta mensagem está sendo enviada de modo automático. "

        Dim respostaEnvioLabel As String


        Dim remetenteEmail As String = "manoel.florindo@aleam.gov.br" '; //O e-mail do remetente

        Dim mail As MailMessage = New MailMessage()

        mail.To.Add(destino)
        mail.To.Add("nadyson_oliveira@jabil.com")
        mail.To.Add(userEmail)

        mail.From = New MailAddress(userEmail, "Leonardo Metelys ", System.Text.Encoding.UTF8)

        mail.Subject = "BRS – New Request # number"
        mail.SubjectEncoding = System.Text.Encoding.UTF8
        mail.Body = Body
        mail.BodyEncoding = System.Text.Encoding.UTF8
        mail.IsBodyHtml = True
        mail.Priority = MailPriority.High

        Dim client As SmtpClient = New SmtpClient() '//Adicionando as credenciais do seu e-mail e senha:

        client.Credentials = New System.Net.NetworkCredential(userEmail, "Jesurum3007")
        client.Port = 587
        client.Host = "smtp.gmail.com" '; //Definindo o provedor que irá disparar o e-mail
        client.EnableSsl = True '; //Gmail trabalha com Server Secured Layer

        Try
            client.Send(mail)
            respostaEnvioLabel = "Envio do E-mail com sucesso"
            'MsgBox(respostaEnvioLabel)
        Catch ex As Exception

            Response.Write("<script language='javascript'>window.alert('ERRO DE ENVIO DE E-MAIL!!!');</script>")

        End Try
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
            strSQL = "SELECT * FROM colaborador WHERE matricula =  '" & Trim(txtCodigo.Text) & "'"
            Objconn.SetarSQL(strSQL)
            Objconn.Executar()

            If Objconn.Tabela.Rows.Count > 0 Then

                For Each DataRow In Objconn.Tabela.Rows

                    TxtNome.Text = DataRow("nome").ToString()
                    TxtLotacao.Text = DataRow("lotacao").ToString()
                    TxtEmailDestino.Text = DataRow("email").ToString()
                    TxtArquivo.Text = "c:\Ferias\" + txtCodigo.Text + ".pdf"

                Next

                txtCodigo.Enabled = False

            Else

                lblAviso.Text = "Matrícula não encontrada."
                lblAviso.Visible = True
                txtCodigo.Enabled = True
                txtCodigo.Focus()

            End If

            Objconn.Desconectar()

        End If
    End Sub

    Protected Sub txtCodigo_TextChanged(sender As Object, e As EventArgs) Handles txtCodigo.TextChanged

        If txtCodigo.Text <> "" Then

            Dim Objconn As New SqlDbConnect()
            Objconn.Conectar()
            Objconn.Parametros.Clear()
            strSQL = "SELECT * FROM colaborador WHERE matricula =  '" & Trim(txtCodigo.Text) & "'"
            Objconn.SetarSQL(strSQL)
            Objconn.Executar()

            If Objconn.Tabela.Rows.Count > 0 Then

                For Each DataRow In Objconn.Tabela.Rows

                    TxtNome.Text = DataRow("nome").ToString()
                    TxtLotacao.Text = DataRow("lotacao").ToString()
                    TxtEmailDestino.Text = DataRow("email").ToString()
                    TxtArquivo.Text = "c:\PDF\" + txtCodigo.Text + ".pdf"

                Next

                txtCodigo.Enabled = False

            Else

                lblAviso.Text = "Matrícula não encontrada."
                lblAviso.Visible = True
                txtCodigo.Enabled = True
                txtCodigo.Focus()

            End If

            Objconn.Desconectar()

        End If

    End Sub
End Class