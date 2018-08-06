<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EnviarEmailEfetivos.aspx.vb" Inherits="AleamRamais.EnviarEmailEfetivos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div class="blocoGrupoCampos" style="margin-left: 25px; width: 800px;">
        <fieldset class="bordaFieldset" style="width:880px;">
            <legend>Mala Direta - Enviar E-mails </legend>
                       
            <div class="blocoGrupoCampos">
                <div class="blocoeditor">
                    <label>
                        Matrícula</label>
                    <br />
                    <asp:TextBox ID="txtCodigo" runat="server" Width="100px">
                    </asp:TextBox>
                     
                                       
                </div>
                <div class="blocoeditor">
                <asp:ImageButton ID="bntPesquisar" runat="server" 
                        ImageUrl="~/Images/pesq.jpg" 
                        Style="background-image: url(~/Styles/Imagens/pesqback.jpg);
                            background-repeat: no-repeat; margin-top:10px; background-position: right;" BackColor="#FF3300"
                            ForeColor="Black" BorderColor="#333300" Height="26px" Width="55px" />                  
                </div>
                <div class="blocoeditor">
                    <label style=" margin-left:0px;">
                        Nome Efetivo</label>
                    <br />
                    <asp:TextBox ID="TxtNome" runat="server" Width="300px" TabIndex="1"></asp:TextBox>
                    
                </div>
                <div class="blocoeditor">
                 <label>
                      Lotação</label>
                    <br />
                    <asp:TextBox ID="TxtLotacao" runat="server" Width="250px" TabIndex="2"></asp:TextBox>
                </div>
                <br />
                
             </div>
             <div class="blocoGrupoCampos">
                <div class="blocoeditor">
                    <label>
                        E-mail Remetente</label>
                    <br />
                    <asp:TextBox ID="TxtEmailOrigem" runat="server" Width="272px" TabIndex="3"></asp:TextBox>
                     
                                       
                </div>
                 <div class="blocoeditor">
                    <label>
                        E-mail Destino</label>
                    <br />
                    <asp:TextBox ID="TxtEmailDestino" runat="server" Width="272px" TabIndex="4"></asp:TextBox>
                     
                                       
                </div>

                </div>
           
             
                 

                </div>
                <br/>
                <div class="blocoGrupoCampos"  style="margin-left: 25px;  width: 800px;">
                      <div class="blocoeditor">
                        <br />
                    </div>
                                     
                </div>
             
                <div class="blocoGrupoCampos"  style="margin-left: 25px;  width: 800px;" >
                <div class="blocoeditor">
                    <asp:Label ID="lblAviso" runat="server" Text="Erro ou Invalido." ForeColor="Red"
                        Visible="false"></asp:Label>
                </div>
                     <br />
                     <br />
                     <br />
                     <div class="blocoeditor">
                    <label>
                        Arquivo em Anexo</label>
                    <br />
                    <asp:TextBox ID="TxtArquivo" runat="server" Width="208px" TabIndex="5"></asp:TextBox>
                        <asp:Button ID="BtnNovo" ValidationGroup="btnNovo" runat="server" Width="100px"
                            Text="Enviar E-mail" OnClick="btnNovo_Click" />
                    </div>
                                       
                </div>
                <div>
                 </div>

           
        </fieldset>
        </div>
       <br/>
       <br/>
      <br/>
       <br/>
     <br/>
       <br/>
     <br/>
       <br/>
</asp:Content>
