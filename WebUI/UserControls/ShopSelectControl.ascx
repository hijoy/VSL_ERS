<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_ShopControl" Codebehind="ShopSelectControl.ascx.cs" %>
<nobr>
<asp:TextBox ID="txtShopName" runat="server" OnTextChanged="txtShopName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayShopName" Width="80px" runat="server"></asp:TextBox>
<input type="button" style="height:18px;display:<%=IsVisible %>" class="button_small" value=":::" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>" class="button_small" value="Çå¿Õ" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="ShopNameCtl" runat="server" />
<asp:HiddenField ID="ShopIDCtl" runat="server" />