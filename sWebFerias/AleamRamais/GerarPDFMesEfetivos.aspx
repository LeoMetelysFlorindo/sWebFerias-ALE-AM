<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GerarPDFMesEfetivos.aspx.vb" Inherits="AleamRamais.GerarPDFMesEfetivos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div>
    
   
    <table>
    <td class="style526">

        <asp:Panel ID="Panel15" runat="server" BorderStyle="None" Width="961px">
            <asp:Label ID="Label2" runat="server" Text="Mês das Férias (Efetivos):" 
                BackColor="White" 
                style="font-size: medium; font-weight: 700; font-family: 'Trebuchet MS'; margin-left: 11px;"></asp:Label>
            <asp:TextBox ID="TxtMes" runat="server" Height="23px" Width="188px"></asp:TextBox>
             <asp:Label ID="Lb11" runat="server" Text="Reg. Inicial:" 
                BackColor="White" 
                style="font-size: medium; font-weight: 700; font-family: 'Trebuchet MS'; margin-left: 11px;"></asp:Label>
            <asp:TextBox ID="TxtDRTInicio" runat="server" Height="23px" Width="102px"></asp:TextBox>
             <asp:Label ID="Label12" runat="server" Text="Reg. Final:" 
                BackColor="White" 
                style="font-size: medium; font-weight: 700; font-family: 'Trebuchet MS'; margin-left: 11px;"></asp:Label>
            <asp:TextBox ID="TxtDRTFim" runat="server" Height="23px" Width="81px"></asp:TextBox>
             <asp:Button ID="TxtPesquisar" runat="server" BackColor="Gray" 
                BorderColor="Black" BorderStyle="Groove" Height="29px" 
                style="font-size: medium; font-weight: 700; font-family: 'Trebuchet MS'; margin-left: 18px; margin-top: 6px" 
                Text="Gerar" Width="91px" />
        </asp:Panel>

    </td>
    </table>
          <div class="blocoGrupoCampos" style="margin-left: 25px;">
                        <div class="blocoeditor">
                            <asp:Label ID="lblAviso" style="font-family: Arial Black;margin-left: 425px;font-size: 9pt;" runat="server" Text="Erro ou Invalido." ForeColor="Red"
                                Visible="False" Width="400px"></asp:Label>
                        </div>
                </div>
    <table>
    <td class="style527">
         <asp:Timer ID="sAtualiza" runat="server" Enabled="False" Interval="10000">
        </asp:Timer>
        
      
    </td>
    </table>
    
    </div>
</asp:Content>
