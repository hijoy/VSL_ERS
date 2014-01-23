<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_OUSelect" Codebehind="OUSelect.ascx.cs" %>
<asp:TextBox ID="OUNameCtl" runat="server" OnTextChanged="OUNameCtl_TextChanged"></asp:TextBox>
<asp:TextBox ID="DisplayCtl" ReadOnly="true" runat="server"></asp:TextBox>
<input type="button" value=":::" style="height:18px;display:<%= GetSelectVisible() %>" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%= IsNoClear%>;" class="button_small" value="Çå¿Õ" onclick="<%= GetResetScript() %>" />
<asp:HiddenField ID="OUIdCtl" runat="server" />
<asp:HiddenField ID="OUCodeCtl" runat="server" />


