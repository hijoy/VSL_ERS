<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DialogMasterPage.master" Inherits="ReportManage_SalesPromotionPrintReport" Codebehind="SalesPromotionPrintReport.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../UserControls/ReportViewerWithoutPara.ascx" TagName="ReportViewer"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
    <asp:Panel ID="Panel1" Width="740px" runat="server" Height="600px" BackColor="#FFFFFF">
        <uc1:ReportViewer ID="ReportViewer1" runat="server"  Width="740" Height="540"/>
        <asp:ScriptManager ID="ScriptManager1" AllowCustomErrorsRedirect="true" runat="server">
        </asp:ScriptManager>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server" Visible="false">
</asp:Content>
