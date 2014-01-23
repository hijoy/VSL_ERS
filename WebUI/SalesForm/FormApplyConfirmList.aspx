<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_FormApplyConfirmList" Title="等待我确认执行的申请单" Codebehind="FormApplyConfirmList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0" width="1270px">
            <tr style="vertical-align: top; height: 40px">
                <td >
                    <div class="field_title"> 
                        单据编号</div>
                    <asp:TextBox ID="txtFormNo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td >
                    <div class="field_title">
                        客户</div>
                        <asp:DropDownList ID="CustomerDDL" runat="server" DataSourceID="odsCustomer"
                            DataTextField="CustomerName" DataValueField="CustomerID" Width="170px" AppendDataBoundItems="true">
                            <asp:ListItem Text="请选择" Selected="False" Value="0" ></asp:ListItem>
                        </asp:DropDownList>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        费用小类</div>
                    <asp:DropDownList ID="SubCategoryDDL" runat="server" DataSourceID="odsSubCategory" DataTextField="ExpenseSubCategoryName" 
                        DataValueField="ExpenseSubCategoryID" Width="370px" >
                    </asp:DropDownList>
                </td>
                <td >
                    <div class="field_title">
                        支付方式</div>
                    <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                        DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="170px">
                    </asp:DropDownList>
                </td>
                <td >
                    <div class="field_title">
                        申请人</div>
                    <asp:TextBox ID="txtStuffUser" MaxLength="50" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td></td>
                <asp:SqlDataSource ID="odsSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand="select 0 ExpenseSubCategoryID,' 全部' ExpenseSubCategoryName Union SELECT ExpenseSubCategoryID,ExpenseCategoryName+'-'+ExpenseSubCategoryName as ExpenseSubCategoryName FROM ExpenseSubCategory join ExpenseCategory on ExpenseSubCategory.ExpenseCategoryID = ExpenseCategory.ExpenseCategoryID order by ExpenseSubCategoryName">
                </asp:SqlDataSource>            
                <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand=" select 0 PaymentTypeID,' 全部' PaymentTypeName union SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] order by PaymentTypeName">
                </asp:SqlDataSource>
                <asp:ObjectDataSource ID="odsCustomer" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetCustomerByPositionID" TypeName="BusinessObjects.MasterDataBLL">
                    <SelectParameters>
                        <asp:Parameter Name="PositionID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </tr>
            <tr>
                <td  colspan="2">
                    <div class="field_title">费用期间</div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td colspan="2">
                    <div class="field_title">
                        提交日期</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </div>
    <table width="1250px">
        <tr>
            <td align="right" valign="middle" colspan="6">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="查询"
                    OnClick="btnSearch_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="title" style="width: 1260px;">方案申请单列表</div>
    <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList" AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True"
            AllowSorting="True" PageSize="20" OnRowDataBound="gvApplyList_RowDataBound" >
        <Columns> 
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="单据编号">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="170px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormApplyName" HeaderText="方案名称">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyName" runat="server" Text='<%# Bind("FormApplyName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="客户名称">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ShopID" HeaderText="门店名称">
                <ItemTemplate>
                    <asp:Label ID="lblShopName" runat="server" Text='<%# GetShopNameByID(Eval("ShopID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Amount" HeaderText="申请金额">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Period" HeaderText="费用期间">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" Text='<%# Bind("Period", "{0:yyyyMM}") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center"  />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ExpenseSubCategoryID" HeaderText="费用小类">
                <ItemTemplate>
                    <asp:Label ID="lblSubCategory" runat="server" Text='<%# Bind("ExpenseSubCategoryName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="PaymentTypeName" HeaderText="支付方式">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffName" HeaderText="申请人">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="提交日期">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td style="width: 50px;" class="Empty1">                        
                    </td>
                    <td style="width: 170px;">
                        单据编号
                    </td>
                    <td style="width: 250px;">
                        客户名称
                    </td>
                    <td style="width: 250px;">
                        门店名称
                    </td>
                    <td style="width: 100px;">
                        申请金额
                    </td>
                    <td style="width: 80px;">
                        费用期间
                    </td>
                    <td style="width: 200px;">
                        费用小类
                    </td>
                    <td style="width: 60px;">
                        支付方式
                    </td>
                    <td style="width: 60px;">
                        申请人
                    </td>
                    <td style="width: 100px;">
                        提交日期
                    </td>
                </tr>
                <tr>
                    <td colspan="10" class="Empty2 noneLabel">
                        无
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormApplyView" EnablePaging="True" SelectCountMethod="QueryFormApplyViewCount"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
