<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ReportManage_SalesReimburseRateForFinanceReport" Codebehind="SalesReimburseRateForFinanceReport.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../UserControls/ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td width="400px">
                    <div class="field_title">
                        部门</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="300px" />
                </td>
                <td width="200px">
                    <div class="field_title">
                        截止日期</div>
                    <uc3:YearAndMonthUserControl ID="UCDateInputEndDate" IsExpensePeriod="true" runat="server" IsReadOnly="false" />
                </td>
                <td style="width: 200px;">
                    &nbsp;
                </td>
                <td style="width: 200px;">
                    &nbsp;
                </td>
                <td valign="bottom" style="width: 200px;">
                    <asp:Button ID="btn_search" runat="server" CssClass="button_nor" Text="查询" OnClick="btn_search_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <uc1:ReportViewer ID="ReportViewer" runat="server" />
</asp:Content>
