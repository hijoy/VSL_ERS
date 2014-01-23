<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="QueryForm_FormApplyBatchPrint" Title="方案书批量打印" CodeBehind="FormApplyBatchPrint.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Print() {
            var expenseFormIds = "";
            var firstNum;
            for (j = 2; j <= 30; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvApplyList_ctl" + GetTBitNum(j) + "_CheckCtl")) {
                    if (document.all("ctl00_ContentPlaceHolder1_gvApplyList_ctl" + GetTBitNum(j) + "_CheckCtl").checked == true) {
                        firstNum = j;
                        break;
                    }
                }
            }

            for (i = firstNum; i <= 30; i++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvApplyList_ctl" + GetTBitNum(i) + "_CheckCtl")) {
                    if (document.all("ctl00_ContentPlaceHolder1_gvApplyList_ctl" + GetTBitNum(i) + "_CheckCtl").checked == true) {
                        if (i == firstNum) {
                            expenseFormIds = expenseFormIds + document.all("ctl00_ContentPlaceHolder1_gvApplyList_ctl" + GetTBitNum(i) + "_lblFormApplyID1").innerText;
                        } else {
                            expenseFormIds = expenseFormIds + "," + document.all("ctl00_ContentPlaceHolder1_gvApplyList_ctl" + GetTBitNum(i) + "_lblFormApplyID1").innerText;
                        }
                    }
                }
            }

            if (expenseFormIds != "") {
                window.open('/ReportManage/SalesApplyListBatch.aspx?ShowDialog=1&FormIDS='+expenseFormIds,'', 'height=600,width=800,toolbar=no,menubar=no,scrollbars=Yes, resizable=no,location=no, status=no');
                //showPrintDialog(url);
            }
        }

        function GetTBitNum(num) {
            if (num < 10) {
                num = "0" + String(num);
            }
            return num;
        }

    </script>
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
                <td colspan="2" style="width: 500px;">
                    <div class="field_title">
                        客户</div>
                    <uc3:UCCustomer ID="UCCustomer" runat="server" Width="220px" />
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
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" style="width: 1100px;">
                &nbsp;
            </td>
            <td>
                <input type="button" class="button_nor" id="hlPrint" value="打印" runat="server" style="display: block;
                    vertical-align: middle; line-height: 20px;" onclick="Print()" />
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
        PageSize="20" OnRowDataBound="gvApplyList_RowDataBound">
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyID1" runat="server" Text='<%# Eval("FormID") %>' Style="display: none;"></asp:Label>
                    <asp:CheckBox ID="CheckCtl" runat="server"></asp:CheckBox>
                </ItemTemplate>
                <ItemStyle Width="30px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="单据编号">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="方案名称">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyName" runat="server" Text='<%# FormApplyNameFormat(Eval("FormApplyName"))%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="单据状态">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
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
                <ItemStyle Width="80px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Period" HeaderText="费用期间">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" Text='<%# Bind("Period", "{0:yyyyMM}") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormApply.ExpenseSubCategoryID" HeaderText="费用小类">
                <ItemTemplate>
                    <asp:Label ID="lblSubCategory" runat="server" Text='<%# Bind("ExpenseSubCategoryName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="140px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormApply.PaymentTypeID" HeaderText="支付方式">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="提交日期">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsClose" HeaderText="是否关闭">
                <ItemTemplate>
                    <asp:CheckBox ID="ckIsClose" runat="server" Checked='<%# Bind("IsClose") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AccruedPeriod" HeaderText="预提期间">
                <ItemTemplate>
                    <asp:Label ID="lblAccruedPeriod" Text='<%# Bind("AccruedPeriod", "{0:yyyyMM}") %>'
                        runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsComplete" HeaderText="确认执行">
                <ItemTemplate>
                    <asp:CheckBox ID="ckIsIsComplete" runat="server" Checked='<%# Bind("IsComplete") %>'
                        Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
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
                    <td scope="col" style="width: 220px;">
                        客户名称
                    </td>
                    <td scope="col" style="width: 260px;">
                        门店名称
                    </td>
                    <td scope="col" style="width: 100px;">
                        申请金额
                    </td>
                    <td scope="col" style="width: 80px;">
                        费用期间
                    </td>
                    <td scope="col" style="width: 120px;">
                        费用小类
                    </td>
                    <td scope="col" style="width: 60px;">
                        支付方式
                    </td>
                    <td scope="col" style="width: 100px;">
                        提交日期
                    </td>
                    <td scope="col" style="width: 80px;">
                        是否关闭
                    </td>
                    <td scope="col" style="width: 80px;">
                        预提期间
                    </td>
                    <td scope="col" style="width: 80px;">
                        确认执行
                    </td>
                </tr>
                <tr>
                    <td colspan="13" style="text-align: center;" class="Empty2 noneLabel">
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
