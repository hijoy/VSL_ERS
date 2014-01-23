<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_StaffControl" Codebehind="StaffControl.ascx.cs" %>
<nobr>
<asp:TextBox ID="txtStaffName" runat="server"  OnTextChanged="txtStaffName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayStaffName" Width="100px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" value="Çå¿Õ" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="StaffNameCtl" runat="server" />
<asp:HiddenField ID="StaffIDCtl" runat="server" />