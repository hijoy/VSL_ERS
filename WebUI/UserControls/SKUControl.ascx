<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_SKUControl" Codebehind="SKUControl.ascx.cs" %>
<nobr>
<asp:TextBox ID="txtSKUName" runat="server" OnTextChanged="txtSKUName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplaySKUName" Width="80px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" class="button_small" value=":::" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" class="button_small" value="Çå¿Õ" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="SKUNameCtl" runat="server" />
<asp:HiddenField ID="SKUIDCtl" runat="server" />