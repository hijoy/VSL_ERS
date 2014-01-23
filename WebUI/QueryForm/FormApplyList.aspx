<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_FormApplyList" Title="方案申请单查询" Codebehind="FormApplyList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        单据编号</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        方案名称</div>
                    <asp:TextBox ID="txtFormApplyName" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        申请人</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 300px;">
                    <div class="field_title">
                        申请人所在部门</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="180px" />
                </td>
                <td colspan="2" style="width: 350px;">
                    <div class="field_title">
                        客户</div>
                    <uc3:UCCustomer ID="UCCustomer" runat="server" Width="220px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        费用小类</div>
                    <asp:DropDownList ID="SubCategoryDDL" runat="server" DataSourceID="odsSubCategory"
                        DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID"
                        Width="380px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select 0 ExpenseSubCategoryID,' 全部' ExpenseSubCategoryName Union SELECT ExpenseSubCategoryID,ExpenseCategoryName+'-'+ExpenseSubCategoryName as ExpenseSubCategoryName FROM ExpenseSubCategory join ExpenseCategory on ExpenseSubCategory.ExpenseCategoryID = ExpenseCategory.ExpenseCategoryID order by ExpenseSubCategoryName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        支付方式</div>
                    <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                        DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="170px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 PaymentTypeID,' 全部' PaymentTypeName union SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] order by PaymentTypeName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 300px;">
                    <div class="field_title">
                        费用期间</div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td colspan="2" style="width: 350px;">
                    <div class="field_title">
                        提交日期</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        确认执行日期</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCConfirmBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="Label1" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCConfirmEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        预提费用期间</div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCAccruedPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="Label2" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCAccruedPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td style="width: 300px;">
                    <div class="field_title">
                        供货日期</div>
                    <uc1:UCDateInput ID="ucDeliveryDate" runat="server" IsReadOnly="false" />
                </td>
                <td style="width: 170px;">
                    <div class="field_title">
                        是否确认执行</div>
                    <asp:DropDownList ID="IsCompleteDDL" runat="server" CssClass="InputCombo" Width="150px">
                        <asp:ListItem Text="全部" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="已经确认执行" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未确认执行" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 170px;">
                    <div class="field_title">
                        方案是否关闭</div>
                    <asp:DropDownList ID="IsCloseDDL" runat="server" CssClass="InputCombo" Width="150px">
                        <asp:ListItem Text="全部" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="方案已关闭" Value="1"></asp:ListItem>
                        <asp:ListItem Text="方案未关闭" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 40px">
                <td style="width: 400px;" colspan="2" valign="middle">
                    <span class="field_title">单据状态</span>
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="待审批" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="审批完成" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkRejected" runat="server" Text="退回待修改" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkScrap" runat="server" Text="作废" Checked="false" />
                </td>
                <td style="width: 450px;" colspan="2" valign="middle">
                    <span class="field_title">方案是否被自动拆分</span>
                    <asp:DropDownList ID="IsAutoSplitDDL" runat="server" CssClass="InputCombo" Width="150px">
                        <asp:ListItem Text="全部" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="方案被拆分" Value="1"></asp:ListItem>
                        <asp:ListItem Text="方案未拆分" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 400px;" colspan="2" valign="middle">
                    <span class="field_title">促销类型</span>
                    <asp:DropDownList ID="ddlPromotionScope" runat="server" DataSourceID="odsPromotionScope"
                        DataTextField="PromotionScopeName" DataValueField="PromotionScopeID" Width="170px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsPromotionScope" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 PromotionScopeID,' 全部' PromotionScopeName union SELECT PromotionScopeID,PromotionScopeName FROM PromotionScope order by PromotionScopeName">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" style="width:850px;">
                &nbsp;
            </td>
            <td style=" margin-top:10px; height:20px;  width:97px; color: #004f8b;font-size: 14px;border: 1px solid #bfd9e8; text-align:center; background-image:url(../images/42.gif);background-repeat: repeat-x;">
                    <asp:HyperLink ID="hlExport_Good" Target="_blank" runat="server" style="display:block;vertical-align:middle;line-height:20px;" Text="实物类导出"></asp:HyperLink>
            </td>
            <td style=" margin-top:10px; height:20px;  width:97px; color: #004f8b;font-size: 14px;border: 1px solid #bfd9e8; text-align:center; background-image:url(../images/42.gif);background-repeat: repeat-x;">
                    <asp:HyperLink ID="hlExport_Total" Target="_blank" runat="server" style="display:block;vertical-align:middle;line-height:20px;" Text="市场类导出"></asp:HyperLink>
            </td>
            <td align="right" valign="middle">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="查询" OnClick="btnSearch_Click" />
            </td>
            
        </tr>
    </table>
    <br />
    <div class="title" style="width: 1260px;">
        方案申请单列表</div>
    <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvApplyList_RowDataBound" OnRowCommand="gvApplyList_RowCommand">
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
                <ItemStyle Width="100px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="方案名称">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyName" runat="server" Text='<%# FormApplyNameFormat(Eval("FormApplyName"))%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="单据状态">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="客户名称">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="180px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ShopID" HeaderText="门店名称">
                <ItemTemplate>
                    <asp:Label ID="lblShopName" runat="server" Text='<%# GetShopNameByID(Eval("ShopID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="140px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Amount" HeaderText="申请金额">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="right"  />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Period" HeaderText="费用期间">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" Text='<%# Bind("Period", "{0:yyyyMM}") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormApply.PaymentTypeID" HeaderText="支付方式">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffName" HeaderText="申请人">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="50px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="提交日期">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsClose" HeaderText="是否关闭">
                <ItemTemplate>
                    <asp:CheckBox ID="ckIsClose" runat="server" Checked='<%# Bind("IsClose") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AccruedPeriod" HeaderText="预提期间">
                <ItemTemplate>
                    <asp:Label ID="lblAccruedPeriod" Text='<%# Bind("AccruedPeriod", "{0:yyyyMM}") %>'
                        runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AccruedAmount" HeaderText="确认执行金额">
                <ItemTemplate>
                    <asp:Label ID="lblAccruedAmount" runat="server" Text='<%# Bind("AccruedAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已报销金额">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# Bind("PaymentAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已支付金额">
                <ItemTemplate>
                    <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Bind("PaidAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Text="作废" runat="server" CausesValidation="false"
                        CommandName="scrap" CommandArgument='<%# Bind("FormID") %>' OnClientClick="return confirm('您将要作废该单据！')"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="border: 0;">
                    <td scope="col" style="width: 100px;" class="Empty1">
                        单据编号
                    </td>
                    <td style="width: 100px;">
                        方案名称
                    </td>
                    <td scope="col" style="width: 60px;">
                        单据状态
                    </td>
                    <td scope="col" style="width: 180px;">
                        客户名称
                    </td>
                    <td scope="col" style="width: 120px;">
                        门店名称
                    </td>
                    <td scope="col" style="width: 80px;">
                        申请金额
                    </td>
                    <td scope="col" style="width: 60px;">
                        费用期间
                    </td>
                    <td scope="col" style="width: 60px;">
                        支付方式
                    </td>
                    <td scope="col" style="width: 50px;">
                        申请人
                    </td>
                    <td scope="col" style="width: 70px;">
                        提交日期
                    </td>
                    <td scope="col" style="width: 60px;">
                        是否关闭
                    </td>
                    <td scope="col" style="width: 60px;">
                        预提期间
                    </td>
                    <td scope="col" style="width: 80px;">
                        确认执行金额
                    </td>
                    <td scope="col" style="width: 80px;">
                        已报销金额
                    </td>
                    <td scope="col" style="width: 80px;">
                        已支付金额
                    </td>
                </tr>
                <tr>
                    <td colspan="15" style="text-align: center;" class="Empty2 noneLabel">
                        无
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:DataGrid ID="ExportDataGrid" runat="server" Visible="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundColumn HeaderText="单据编号" DataField="FormNo" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="方案名称" DataField="FormApplyName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="单据状态" DataField="Status" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="客户名称" DataField="CustomerName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="门店名称" DataField="ShopName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="申请金额" DataField="Amount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="费用期间" DataField="Period" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="支付方式" DataField="PaymentTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="申请人" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="提交日期" DataField="SubmitDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="方案是否关闭" DataField="IsClose" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="确认执行时间" DataField="ConfirmCompleteDate" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormApplyViewByRight" EnablePaging="True" SelectCountMethod="QueryFormApplyViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
