<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DialogMasterPage.master" Inherits="ReportManage_SalesBudgetByPosition" Codebehind="SalesBudgetByPosition.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../UserControls/ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
    <asp:Panel Width="995px" runat="server" Height="600px" BackColor="#FFFFFF">
        <uc1:ReportViewer ID="ReportViewer" runat="server" />
    </asp:Panel>
    <asp:ScriptManager ID="ScriptManager1" AllowCustomErrorsRedirect="true" runat="server">
    </asp:ScriptManager>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server" Visible="false">
</asp:Content>
