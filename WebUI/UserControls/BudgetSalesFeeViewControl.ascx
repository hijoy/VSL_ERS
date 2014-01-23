<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_BudgetSalesFeeViewControl" Codebehind="BudgetSalesFeeViewControl.ascx.cs" %>
<nobr>
<asp:TextBox ID="txtCustomerName" runat="server" OnTextChanged="txtCustomerName_TextChanged"></asp:TextBox>
<asp:TextBox ID="txtDisplayCustomerName" Width="80px" runat="server"></asp:TextBox>
<input style="<%=IsVisible %>" type="button" style="height:18px;" class="button_small" value=":::" onclick="<%= GetShowDlgScript() %>"/>
<input type="button" style="height:18px;display:<%=IsNoClear %>;" class="button_small" value="清空" onclick="<%= GetResetScript() %>" /></nobr>
<asp:HiddenField ID="CustomerNameCtl" runat="server" />
<asp:HiddenField ID="BudgetSalesFeeIDIDCtl" runat="server" />