<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_PromotionPrintControl" Codebehind="PromotionPrintControl.ascx.cs" %>
<nobr>
<asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
<asp:TextBox ID="txtDisplayCustomerName" Width="80px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" value=":::" class="button_small" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" value="Çå¿Õ" class="button_small" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="CustomerNameCtl" runat="server" />
<asp:HiddenField ID="CustomerIDCtl" runat="server" />