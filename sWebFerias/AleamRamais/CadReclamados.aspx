<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadReclamados.aspx.vb" Inherits="AleamRamais.CadReclamados1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
   
       
    <div class="blocoGrupoCampos" style="margin-left: 25px; width: 800px;">
        <fieldset class="bordaFieldset" style="width:780px;">
            <legend>Cadastro de Efetivos</legend>
                       
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
                    <asp:TextBox ID="TxtLotacao" runat="server" Width="150px" TabIndex="2"></asp:TextBox>
                </div>
                <br />
                
            </div>

            <div class="blocoGrupoCampos"  style="margin-left: 0px;  width: 800px;">
                <div class="blocoeditor">
                 <label>
                        Admissão</label>
                    <br />
                    <asp:TextBox ID="TxtAdmissao" runat="server" Width="150px" TabIndex="3"></asp:TextBox>
                </div>
                <div class="blocoeditor">
                     <label>
                        Més Férias</label>
                    <br />
                    <asp:TextBox ID="TxtMesFerias" runat="server" Width="120px" TabIndex="4"></asp:TextBox>                    
                </div>
                <div class="blocoeditor">
                     <label>
                        1ª Parcela 13º</label>
                    <br />
                    <asp:TextBox ID="TxtParcela13" runat="server" Width="100px" TabIndex="4"></asp:TextBox>                    
                </div>
                 <div class="blocoeditor">
                     <label>
                       E-mail</label>
                    <br />
                    <asp:TextBox ID="TxtEmail" runat="server" Width="187px" TabIndex="4"></asp:TextBox>                    
                </div>
               <br />
           </div>    
           <div class="blocoGrupoCampos"  style="margin-left: 0px; width: 800px;"> 
                 <div class="blocoeditor">
                 <label>
                        Periodo 1</label>
                    <br />
                    <asp:TextBox ID="TxtPeriodo1" runat="server" Width="150px" TabIndex="5"></asp:TextBox>
                </div>
                 <div class="blocoeditor">
                     <label>
                        Periodo 2</label>
                    <br />
                    <asp:TextBox ID="TxtPeriodo2" runat="server" Width="150px" TabIndex="6"></asp:TextBox>                    
                </div>
                <div class="blocoeditor">
                     <label>
                        Periodo 3</label>
                    <br />
                    <asp:TextBox ID="TxtPeriodo3" runat="server" Width="150px" TabIndex="7"></asp:TextBox>                    
                </div>

                <div class="blocoeditor">
                     <label>
                        Data Retorno Férias</label>
                    <br />
                    <asp:TextBox ID="TxtRetorno" runat="server" Width="150px" TabIndex="7"></asp:TextBox>                    
                </div>
              
                <br />
             </div>
            <div class="blocoGrupoCampos"  style="margin-left: 0px; width: 800px;"> 
                 <div class="blocoeditor">
                 <label>
                        Observações</label>
                    <br />
                    <asp:TextBox ID="TxtObs" runat="server" Width="250px" TabIndex="5"></asp:TextBox>
                </div>
                 <div class="blocoeditor">
                     <label>
                        Ativo</label>
                    <br />
                    <asp:TextBox ID="TxtAtivo" runat="server" Width="76px" TabIndex="6"></asp:TextBox>                    
                </div>
                <div class="blocoeditor">
                     <label>
                        Data Nascimento</label>
                    <br />
                    <asp:TextBox ID="TxtNascimento" runat="server" Width="92px" TabIndex="7"></asp:TextBox>                    
                </div>
                 <div class="blocoeditor">
                     <label>
                        Dias</label>
                    <br />
                    <asp:TextBox ID="TxtDias" runat="server" Width="120px" TabIndex="7"></asp:TextBox>                    
                </div>
              

             </div>
                <div class="blocoGrupoCampos"  style="margin-left: 25px;  width: 800px;">
                      <div class="blocoeditor">
                        <br />
                        <asp:Button ID="BtnNovo" ValidationGroup="btnNovo" runat="server" Width="100px"
                            Text="Novo" OnClick="btnNovo_Click" />
                    </div>
                    <div class="blocoeditor">
                        <br />
                        <asp:Button ID="btnSalvar" ValidationGroup="btnSalvar" runat="server" Width="100px"
                            Text="Salvar" OnClick="btnSalvar_Click" />
                    </div>
                    <div class="blocoeditor">
                        <br />
                        <asp:Button ID="btnCancelar" runat="server" Width="100px" Text="Limpar Form" OnClick="btnCancelar_Click" />
                    </div>

                    <div class="blocoeditor">
                        <br />
                        <asp:Button ID="BtnMenu" runat="server" Width="100px" Text="Imprimir" OnClick="btnMenu_Click" Visible="False" />
                    </div>
                    <div class="blocoeditor">
                        <br />
                        <asp:Button ID="btnPDF" runat="server" Width="100px" Text="Gerar PDF" OnClick="btnPDF_Click" />
                    </div>
                </div>
                <div class="blocoGrupoCampos"  style="margin-left: 25px;  width: 800px;" >
                <div class="blocoeditor">
                    <asp:Label ID="lblAviso" runat="server" Text="Erro ou Invalido." ForeColor="Red"
                        Visible="false"></asp:Label>
                </div>
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
      <br/>
       <br/>
      
       
   
    

</asp:Content>
