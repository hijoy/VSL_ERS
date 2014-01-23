<%@ Page Language="C#" AutoEventWireup="true"
    MasterPageFile="~/DialogMasterPage.master" Inherits="Dialog_BudgetSalesFeeViewSearch" Codebehind="BudgetSalesFeeViewSearch.aspx.cs" %>

<%@ Implements Interface="System.Web.UI.IPostBackEventHandler" %>
<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var _oldColor;
        function SetNewColor(source) {
            _oldColor = source.style.backgroundColor;
            source.style.backgroundColor = '#C0C0C0';
        }

        function SetOldColor(source) {
            source.style.backgroundColor = _oldColor;
        }
    </script>
    <div class="title1" style="width: 842px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 842px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px;" class="field_Title">
                    客户名称
                </td>
                <td style="width: 200px;" class="field_Title">
                    费用项
                </td>
                <td style="width: 200px;" class="field_Title">
                    费用期间
                </td>
                <td style="width: 100px;" class="field_Title">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:DropDownList ID="CustomerDDL" runat="server" DataSourceID="odsCustomer" DataTextField="CustomerName"
                        DataValueField="CustomerID" Width="200px">
                    </asp:DropDownList>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="SearchExpenseItemDDL" DataSourceID="sdsExpenseItemAll"
                        DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" Width="180px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="vertical-align: top">
                    <uc2:YearAndMonthUserControl ID="ucNewPeriod" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="vertical-align: top">
                    <asp:LinkButton runat="server" ID="lbtnSearch" Text="搜 索" CssClass="button_nor" OnClick="lbtnSearch_Click"></asp:LinkButton>
                </td>
                <asp:ObjectDataSource ID="odsCustomer" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetCustomerByPositionID" TypeName="BusinessObjects.MasterDataBLL">
                    <SelectParameters>
                        <asp:Parameter Name="PositionID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </tr>
        </table>
        <asp:SqlDataSource ID="sdsExpenseItemAll" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT ExpenseItemID,AccountingCode+'--'+AccountingName ExpenseItemName FROM ExpenseItem">
        </asp:SqlDataSource>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1" style="width: 842px;">
        费用预算</div>
    <gc:GridView ID="gvBudget" Width="842px" CssClass="GridView" runat="server" DataSourceID="odsBudget"
        CellPadding="0" AutoGenerateColumns="False" DataKeyNames="BudgetSalesFeeID" AllowPaging="True"
        AllowSorting="True" PageSize="20" EnableModelValidation="True" OnRowDataBound="gvBudget_RowDataBound">
        <Columns>
            <asp:TemplateField SortExpression="CustomerNo" HeaderText="客户编号">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerNoByEdit" runat="server" Text='<%# Eval("CustomerNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="客户名称">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerNameByEdit" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ExpenseItemName" HeaderText="费用项">
                <ItemTemplate>
                    <asp:Label ID="lblExpenseItemNameByEdit" runat="server" Text='<%# GetExpenseItemNameById(Eval("ExpenseItemID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="220px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Period" HeaderText="费用期间">
                <ItemTemplate>
                    <asp:Label ID="lblPeriodByEdit" runat="server" Text='<%# Eval("Period","{0:yyyy-MM}")%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="TotalBudget" HeaderText="预算金额">
                <ItemTemplate>
                    <asp:Label ID="lblTotalBudgetByEdit" runat="server" Text='<%#Bind("TotalBudget") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="220px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="height: 22px;">
                    <td style="width: 100px;" class="Empty1">
                        客户编号
                    </td>
                    <td style="width: 200px;">
                        客户名称
                    </td>
                    <td style="width: 200px;">
                        费用项
                    </td>
                    <td style="width: 150px;">
                        费用期间
                    </td>
                    <td style="width: 220px;">
                        预算金额
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="Empty2 noneLabel">
                        无
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsBudget" runat="server" TypeName="BusinessObjects.BudgetAllocationApplyBLL"
        SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="BudgetSalesFeeTotalCount"
        SelectMethod="BudgetSalesFeePaged">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
