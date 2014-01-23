<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SalesFeeBudget" Codebehind="SalesFeeBudget.aspx.cs" %>

<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="ucCustomerSelect"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 300px;" class="field_title">
                    客户
                </td>
                <td style="width: 200px;" class="field_title">
                    费用项
                </td>
                <td style="width: 200px;" class="field_title">
                    客户类型
                </td>
                <td style="width: 200px;" class="field_title">
                    渠道类型
                </td>
                <td style="width: 200px;" class="field_title">
                    城市
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <uc1:ucCustomerSelect ID="ucSearchCustomer" runat="server" CssClass="InputText" Width="140px" />
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="SearchExpenseItemDDL" DataSourceID="sdsExpenseItemAll"
                        DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" Width="180px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplCustomerTypeBySearch" DataSourceID="sdsCustomerType"
                        DataTextField="CustomerTypeName" DataValueField="CustomerTypeID" Width="180px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplChannelTypeBySearch" DataSourceID="sdsChannelType"
                        DataTextField="ChannelTypeName" DataValueField="ChannelTypeID" Width="180px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplCityBySearch" DataSourceID="sdsCity" DataTextField="CityName"
                        DataValueField="CityID" Width="180px" AppendDataBoundItems="true">
                        <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="vertical-align: top">
                </td>
            </tr>
            <tr>
                <td style="width: 200px;" class="field_title">
                    预算年月
                </td>
                <td style="width: 200px; vertical-align: bottom">
                    &nbsp;
                </td>
                <td style="width: 200px; vertical-align: bottom">
                    &nbsp;
                </td>
                <td style="width: 200px; vertical-align: bottom">
                    &nbsp;
                </td>
                <td style="width: 200px; vertical-align: bottom;" align="center" rowspan="2">
                    <asp:Button ID="SearchBtn" CssClass="button_nor" runat="server" Text="查询" OnClick="SearchBtn_Click" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <nobr>
                            <uc2:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false"
                                IsExpensePeriod="true" />
                            <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                            <uc2:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                </td>
                <td style="vertical-align: top">
                    &nbsp;
                </td>
                <td style="vertical-align: top">
                    &nbsp;
                </td>
                <td style="vertical-align: top">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="sdsExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand="SELECT ExpenseItemID,AccountingCode+'--'+AccountingName ExpenseItemName FROM ExpenseItem">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsCity" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand="select CityID,Province.ProvinceName+'-'+City.CityName as CityName from City,Province where City.ProvinceID=Province.ProvinceID order by Province.ProvinceName,City.CityName">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand="SELECT [CustomerTypeID], [CustomerTypeName] FROM [CustomerType]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsChannelType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand="SELECT [ChannelTypeID], [ChannelTypeName] FROM [ChannelType]">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="sdsExpenseItemAll" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT ExpenseItemID,AccountingCode+'--'+AccountingName ExpenseItemName FROM ExpenseItem">
        </asp:SqlDataSource>
    </div>
    <br />
    <div class="title" style="width: 1260px;">
        销售费用预算信息</div>
    <asp:UpdatePanel ID="UPBudget" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView CssClass="GridView" ID="GVBudget" runat="server" AutoGenerateColumns="False"
                CellPadding="0" DataKeyNames="BudgetSalesFeeID" DataSourceID="odsBudget" AllowPaging="True"
                AllowSorting="true" PageSize="20" OnSelectedIndexChanged="GVBudget_SelectedIndexChanged" >
                <Columns>
                    <asp:TemplateField HeaderText="预算客户" SortExpression="CustomerID">
                        <EditItemTemplate>
                            <asp:Label ID="lblCustomer1" runat="server" Text='<%# GetCustomerNameByID(Eval("CustomerID")) %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbCustomer" runat="server" CommandName="Select" Text='<%# GetCustomerNameByID(Eval("CustomerID"))%>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="230px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用期间" SortExpression="Period">
                        <EditItemTemplate>
                            <asp:Label ID="lblPeriod1" runat="server" Text='<%# Eval("Period", "{0:yyyy/MM}") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod2" runat="server" Text='<%# Eval("Period", "{0:yyyy/MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用项" SortExpression="ExpenseItemID">
                        <EditItemTemplate>
                            <asp:Label ID="ExpenseItemlbl1" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="ExpenseItemlbl2" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="220px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="初始预算" SortExpression="OriginalBudget">
                        <EditItemTemplate>
                            <asp:Label ID="lblOriginalBudget1" runat="server" Text='<%# Eval("OriginalBudget", "{0:N}") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOriginalBudget2" runat="server" Text='<%# Eval("OriginalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="正常预算" SortExpression="NormalBudget">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNormalBudget" runat="server" Text='<%# Bind("NormalBudget") %>'
                                Width="60px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="txtNormalBudget"
                                Display="None" ErrorMessage="请您输入正常预算！" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtNormalBudget"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="请输入正确金额格式" ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNormalBudget" runat="server" Text='<%# Eval("NormalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预算调拨" SortExpression="TransferBudget">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTransferBudget" runat="server" Text='<%# Bind("TransferBudget") %>'
                                Width="60px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RF5" runat="server" ControlToValidate="txtTransferBudget"
                                ValidationExpression="<%$ Resources:RegularExpressions, MinusMoney %>" Display="None"
                                ErrorMessage="请输入正确金额格式" ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTransferBudget" runat="server" Text='<%# Eval("TransferBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预算调整" SortExpression="AdjustBudget">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAdjustBudget" runat="server" Text='<%# Bind("AdjustBudget") %>'
                                Width="60px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="txtAdjustBudget"
                                ValidationExpression="<%$ Resources:RegularExpressions, MinusMoney %>" Display="None"
                                ErrorMessage="请输入正确金额格式" ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总预算" SortExpression="TotalBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalBudget" runat="server" Text='<%# Eval("TotalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="修改原因">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>'
                                Width="240px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RF4" runat="server" ControlToValidate="txtModifyReason"
                                Display="None" ErrorMessage="请您输入修改原因！" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryEDIT" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="EDIT" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="260px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                                Text="更新" ValidationGroup="EDIT"></asp:LinkButton>
                            <asp:LinkButton ID="CancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="EditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="编辑"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除" OnClientClick="return confirm('确定删除此行数据吗？');"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 230px;" class="Empty1">
                                预算客户
                            </td>
                            <td style="width: 90px;">
                                费用期间
                            </td>
                            <td style="width: 220px;">
                                费用项
                            </td>
                            <td style="width: 80px;">
                                初始预算
                            </td>
                            <td style="width: 80px;">
                                正常预算
                            </td>
                            <td style="width: 80px;">
                                预算调拨
                            </td>
                            <td style="width: 80px;">
                                预算调整
                            </td>
                            <td style="width: 80px;">
                                总预算
                            </td>
                            <td style="width: 260px;">
                                修改原因
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BudgetAddFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="AddUpdatePanel" runat="server">
        <ContentTemplate>
            <asp:FormView DefaultMode="insert" ID="BudgetAddFormView" runat="server" DataKeyNames="BudgetSalesFeeID"
                DataSourceID="odsBudget" Visible="<%# HasManageRight %>" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 230px;">
                                <uc1:ucCustomerSelect runat="server" ID="ucNewCustomerSelect" IsNoClear="true" CustomerID='<%# Bind("CustomerID") %>'
                                    Width="180px" />
                            </td>
                            <td align="center" style="width: 90px;">
                                <uc2:YearAndMonthUserControl ID="ucNewPeriod" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                            </td>
                            <td align="center" style="width: 220px;">
                                <asp:DropDownList ID="newExpenseItemDDL" runat="server" SelectedValue='<%# Bind("ExpenseItemID") %>'
                                    DataSourceID="odsExpenseItemEnabled" DataTextField="ExpenseItemName" DataValueField="ExpenseItemID"
                                    Width="215px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:TextBox ID="txtNewOriginalBudget" runat="server" Text='<%# Bind("OriginalBudget") %>'
                                    Width="60px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:TextBox ID="txtNewNormalBudget" runat="server" Text='<%# Bind("NormalBudget") %>'
                                    Width="60px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:TextBox ID="txtNewTransferBudget" runat="server" Text='<%# Bind("TransferBudget") %>'
                                    Width="60px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:TextBox ID="txtNewAdjustBudget" runat="server" Text='<%# Bind("AdjustBudget") %>'
                                    Width="60px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 80px;">
                            </td>
                            <td align="center" style="width: 260px;">
                                <asp:TextBox ID="txtNewModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>'
                                    Width="240px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="NewRF1" runat="server" ControlToValidate="txtNewOriginalBudget"
                        Display="None" ErrorMessage="请录入初始预算！" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="NewRF2" runat="server" ControlToValidate="txtNewNormalBudget"
                        Display="None" ErrorMessage="请录入正常预算！" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RF4" runat="server" ControlToValidate="txtNewOriginalBudget"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="请输入正确金额格式" ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="txtNewNormalBudget"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="请输入正确金额格式" ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF5" runat="server" ControlToValidate="txtNewAdjustBudget"
                        ValidationExpression="<%$ Resources:RegularExpressions, MinusMoney %>" Display="None"
                        ErrorMessage="请输入正确金额格式" ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF6" runat="server" ControlToValidate="txtNewTransferBudget"
                        ValidationExpression="<%$ Resources:RegularExpressions, MinusMoney %>" Display="None"
                        ErrorMessage="请输入正确金额格式" ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GVBudget" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsBudget" runat="server" SelectMethod="GetPagedBudgetSalesFee"
        SelectCountMethod="QueryBudgetSalesFeeTotalCount" InsertMethod="InsertBudgetSalesFee"
        UpdateMethod="UpdateBudgetSalesFee" DeleteMethod="DeleteBudgetSalesFeeByID" OldValuesParameterFormatString="{0}"
        EnablePaging="true" TypeName="BusinessObjects.BudgetBLL" SortParameterName="sortExpression"
        OnInserting="odsBudget_Inserting" OnInserted="odsBudget_Inserted" OnUpdating="odsBudget_Updating"
        OnUpdated="odsBudget_Updated" OnDeleted="odsBudget_Deleted">
        <UpdateParameters>
            <asp:Parameter Name="NormalBudget" Type="Decimal" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="AdjustBudget" Type="Decimal" />
            <asp:Parameter Name="TransferBudget" Type="Decimal" />
            <asp:Parameter Name="PositionID" Type="Int32" />
            <asp:Parameter Name="ModifyReason" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="CustomerID" Type="Int32" />
            <asp:Parameter Name="Period" Type="DateTime" />
            <asp:Parameter Name="ExpenseItemID" Type="Int32" />
            <asp:Parameter Name="OriginalBudget" Type="Decimal" />
            <asp:Parameter Name="NormalBudget" Type="Decimal" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="AdjustBudget" Type="Decimal" />
            <asp:Parameter Name="TransferBudget" Type="Decimal" />
            <asp:Parameter Name="PositionID" Type="Int32" />
            <asp:Parameter Name="ModifyReason" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="odsExpenseItemEnabled" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
        SelectCommand=" SELECT ExpenseItemID,AccountingCode+'--'+AccountingName ExpenseItemName FROM ExpenseItem where IsActive = 1">
    </asp:SqlDataSource>
    <br />
    <div class="title" style="width: 1260px;">
        变更历史
    </div>
    <asp:UpdatePanel ID="UPHistory" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView CssClass="GridView" ID="GVHistory" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BudgetSalesFeeHistoryID" DataSourceID="odsHistory" AllowPaging="false"
                AllowSorting="false">
                <Columns>
                    <asp:TemplateField HeaderText="正常预算">
                        <ItemTemplate>
                            <asp:Label ID="lblNormalBudget" runat="server" Text='<%# Eval("NormalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预算调拨">
                        <ItemTemplate>
                            <asp:Label ID="lblTransferBudget" runat="server" Text='<%# Eval("TransferBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预算调整">
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总预算">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalBudget" runat="server" Text='<%# Eval("TotalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="修改人">
                        <ItemTemplate>
                            <asp:Label ID="lblUser" runat="server" Text='<%# GetUserNameByID(Eval("UserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="修改人职务">
                        <ItemTemplate>
                            <asp:Label ID="lblPosition" runat="server" Text='<%# GetPositionNameByID(Eval("PositionID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="动作">
                        <ItemTemplate>
                            <asp:Label ID="lblAction" runat="server" Text='<%# Eval("Action") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="修改时间">
                        <ItemTemplate>
                            <asp:Label ID="lblModifyDate" runat="server" Text='<%# Eval("ModifyDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="修改原因">
                        <ItemTemplate>
                            <asp:Label ID="lblModifyReason" runat="server" Text='<%# Eval("ModifyReason") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="460px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GVBudget" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsHistory" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="GetBudgetSalesFeeHistoryByParentID" TypeName="BusinessObjects.BudgetBLL">
        <SelectParameters>
            <asp:Parameter Name="BudgetSalesFeeID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
