<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_MaterialControl" Codebehind="MaterialControl.ascx.cs" %>
<nobr>
<asp:TextBox ID="txtMaterialName" runat="server" OnTextChanged="txtMaterialName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayMaterialName" Width="80px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;"  class="button_small" value=":::" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear%>" class="button_small" value="Çå¿Õ" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="MaterialNameCtl" runat="server" />
<asp:HiddenField ID="MaterialIDCtl" runat="server" />