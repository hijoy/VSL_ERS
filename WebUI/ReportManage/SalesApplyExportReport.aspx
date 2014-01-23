<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ReportManage_SalesApplyExportReport" Codebehind="SalesApplyExportReport.aspx.cs" %>

<%@ Register Src="../UserControls/ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:ReportViewer ID="ReportViewer" runat="server" />
</asp:Content>
