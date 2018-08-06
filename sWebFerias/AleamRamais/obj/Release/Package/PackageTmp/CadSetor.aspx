<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CadSetor.aspx.vb" Inherits="AleamRamais.CadSetor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <div class="blocoGrupoCampos">    
       <div class="blocoeditor">
                    <label>
                        Lotação</label>
                    <br />
                     <asp:DropDownList ID="cmbSetor" runat="server" Height="19px" Width="225px">
                      </asp:DropDownList>
        </div>

    
        <div class="blocoeditor">
                <asp:ImageButton ID="bntPesquisar" runat="server" 
                        ImageUrl="~/Images/pesq.jpg" 
                        Style="background-image: url(~/Styles/Imagens/pesqback.jpg);
                            background-repeat: no-repeat; margin-top:10px; background-position: right;" BackColor="#FF3300"
                            ForeColor="Black" BorderColor="#333300" Height="26px" Width="55px" />                  
          </div>
          <div class="blocoeditor">
              <label>
               Més Férias</label>
               <br />
              <asp:TextBox ID="TxtMesFerias" runat="server" Width="120px" TabIndex="4"></asp:TextBox>                    
          </div>
         <div class="blocoeditor">
                 <label>
                        Periodo 1</label>
                    <br />
                    <asp:TextBox ID="TxtPeriodo1" runat="server" Width="170px" TabIndex="5"></asp:TextBox>
          </div>
          <div class="blocoeditor">
                     <label>
                        Data Retorno Férias</label>
                    <br />
                    <asp:TextBox ID="TxtRetorno" runat="server" Width="150px" TabIndex="7"></asp:TextBox>                    
                </div>
           <div class="blocoeditor">
                     <label>
                        Dias</label>
                    <br />
                    <asp:TextBox ID="TxtDias" runat="server" Width="120px" TabIndex="7"></asp:TextBox>                    
                </div>
                <div class="blocoeditor">                      
                        <asp:Button ID="btnSalvar" ValidationGroup="btnSalvar" runat="server" Width="85px"
                            Text="Salvar" OnClick="btnSalvar_Click" Height="46px" />
                    </div>
   </div>

     <div class="blocoGrupoCampos" style="margin-left: 25px;">
                        <div class="blocoeditor">
                            <asp:Label ID="lblAviso" style="font-family: Arial Black;margin-left: 425px;font-size: 9pt;" runat="server" Text="Erro ou Invalido." ForeColor="Red"
                                Visible="False" Width="400px"></asp:Label>
                        </div>
                </div>

     <table>
    <td class="style527">

         <asp:GridView ID="gdItens" runat="server" 
        CaptionAlign="Top" DataKeyNames="matricula"
                       Height="47px" Style="margin-left: 15px; margin-top: 19px;
                        color: #000000; font-size: medium;" Width="1256px" 
        Caption="Comissionados por Lotação" SelectedIndex="1" PageSize="20" 
    GridLines="None" UseAccessibleHeader="False"
                        CellPadding="8" ForeColor="#333333" TabIndex="3">
                        <AlternatingRowStyle BackColor="White" />                        
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="true" ForeColor="White" />
                        <PagerStyle Height="30px" BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="false" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView> 
        <asp:Timer ID="sAtualiza" runat="server" Enabled="False" Interval="100">
        </asp:Timer>
    </td>
    </table>

       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
