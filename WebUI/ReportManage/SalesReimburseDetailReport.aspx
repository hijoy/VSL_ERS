<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ReportManage_SalesReimburseDetailReport" Codebehind="SalesReimburseDetailReport.aspx.cs" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="../UserControls/ReportViewer.ascx" TagName="ReportViewer" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td>
                    <div class="field_title">
                        报销单编号</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td>
                    <div class="field_title">
                        申请单编号(必须填写完全)</div>
                    <asp:TextBox ID="txtApplyFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td>
                    <div class="field_title">
                        报销单申请人</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td>
                    <div class="field_title">
                        支付方式</div>
                    <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                        DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="170px">
                    </asp:DropDownList>
                </td>
                <td colspan="2">
                    <div class="field_title">
                       报销单提交日期</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand=" select 0 PaymentTypeID,' 全部' PaymentTypeName union SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] order by PaymentTypeName">
                </asp:SqlDataSource>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="field_title">
                        报销单申请人所在部门</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="230px" />
                </td>
                <td colspan="2">
                    <div class="field_title">
                        客户</div>
                    <uc3:UCCustomer ID="UCCustomer" runat="server" Width="230px" />
                </td>
                <td>
                    <span class="field_title">报销单单据状态</span>
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="待审批" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="审批完成" Checked="false" />&nbsp;&nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" valign="middle" colspan="6">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="查询" OnClick="btnSearch_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <uc1:ReportViewer ID="ReportViewer" runat="server" />
</asp:Content>
