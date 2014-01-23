<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Customer" Codebehind="Customer.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 180px;" class="field_title">
                    客户编号
                </td>
                <td style="width: 180px;" class="field_title">
                    客户名称
                </td>
                <td style="width: 180px;" class="field_title">
                    城市
                </td>
                <td style="width: 180px;" class="field_title">
                    激活
                </td>
                <td style="width: 380px;" class="field_title">
                    所属机构
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtCustNoBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtCustNameBySearch" Width="160px"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplCustCityBySearch" DataSourceID="sdsCityAll"
                        DataTextField="CityName" DataValueField="CityID" Width="160px" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="所有城市" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsCityAll" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select CityID,Province.ProvinceName+'-'+City.CityName as CityName from City,Province where City.ProvinceID=Province.ProvinceID order by Province.ProvinceName,City.CityName">
                    </asp:SqlDataSource>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplCustActiveBySearch" Width="160px">
                        <asp:ListItem Selected="True" Text="全部" Value="3"></asp:ListItem>
                        <asp:ListItem Text="激活" Value="1"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="vertical-align: top">
                    <uc3:OUSelect ID="UCOUBySearch" runat="server" CssClass="InputText" Width="260px" />
                </td>
            </tr>
        </table>
    </div>
    <div style="margin-top: 5px; width: 1200px; text-align: right;">
        <asp:Button runat="server" ID="lbtnSearch" Text="搜 索" CssClass="button_nor" OnClick="lbtnSearch_Click">
        </asp:Button></div>
    <br />
    <div class="title" style="width: 1260px;">
        客户信息</div>
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCustomer" CssClass="GridView" runat="server" DataSourceID="odsCustomer"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="CustomerID" AllowPaging="True"
                AllowSorting="True" PageSize="20" OnSelectedIndexChanged="gvCustomer_SelectedIndexChanged"
                EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="CustomerNo" HeaderText="客户编号">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCustomerNoByEdit" runat="server" Text='<%# Bind("CustomerNo") %>'
                                Width="60px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCustomerNoByEdit" runat="server" CausesValidation="False"
                                CommandName="Select" Text='<%# Eval("CustomerNo") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="88px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CustomerName" HeaderText="客户名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCustomerNameByEdit" runat="server" Text='<%# Bind("CustomerName") %>'
                                Width="240px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerNameByEdit" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="260px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="省份" InsertVisible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseCategoryByEdit" runat="server" Text='<%#GetProvinceNameByCityId (Eval("CityID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CityID" HeaderText="城市">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplCityByEdit" DataSourceID="sdsCity" DataTextField="CityName"
                                DataValueField="CityID" SelectedValue='<%# Bind("CityID") %>' Width="120px">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="dplSubCateByEdit" runat="server" Text='<%# GetCityNameById(Eval("CityID") )%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="AccountingCode" HeaderText="客户类型">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplCustomerTypeByEdit" DataSourceID="sdsCustomerType"
                                DataTextField="CustomerTypeName" DataValueField="CustomerTypeID" SelectedValue='<%# Bind("CustomerTypeID") %>'
                                Width="90px">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingCodeByEdit" runat="server" Text='<%#GetCustTypeNameById(Eval("CustomerTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="110px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="AccountingName" HeaderText="渠道类型">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplChannelTypeByEdit" DataSourceID="sdsChannelType"
                                DataTextField="ChannelTypeName" DataValueField="ChannelTypeID" SelectedValue='<%# Bind("ChannelTypeID") %>'
                                Width="90px">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingNameByEdit" runat="server" Text='<%# GetChanTypeNameById(Eval("ChannelTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="110px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsInContract" HeaderText="所属机构">
                        <EditItemTemplate>
                            <uc3:OUSelect ID="UCOU" runat="server" CssClass="InputText" Width="80px" OUId='<%# Bind("ApplyOrganizationUnitID") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblOUNameByEdit" Text='<%#GetOUNameById(Eval("ApplyOrganizationUnitID")) %>' />
                        </ItemTemplate>
                        <ItemStyle Width="190px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                     <asp:TemplateField SortExpression="IsInContract" HeaderText="所属预算机构">
                        <EditItemTemplate>
                            <uc3:OUSelect ID="UCOU1" runat="server" CssClass="InputText" Width="80px" OUId='<%# Bind("OrganizationUnitID") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblOUName1ByEdit" Text='<%#GetOUNameById(Eval("OrganizationUnitID")) %>' />
                        </ItemTemplate>
                        <ItemStyle Width="183px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="激活">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFCustCode" runat="server" ControlToValidate="txtCustomerNoByEdit"
                                Display="None" ErrorMessage="请您输入客户编号！" SetFocusOnError="True" ValidationGroup="custEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFCustName" runat="server" ControlToValidate="txtCustomerNameByEdit"
                                Display="None" ErrorMessage="请您输入客户名称！" SetFocusOnError="True" ValidationGroup="custEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsCustEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="custEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="custEdit" CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text="编辑"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table class="FormView" cellspacing="0" border="0">
                        <tr class="Header" style="height: 22px;">
                            <td style="width:88px;">
                                客户编号
                            </td>
                            <td style="width:260px;">
                                客户名称
                            </td>
                            <td style="width:80px;">
                                省份
                            </td>
                            <td style="width:140px;">
                                城市
                            </td>
                            <td style="width:110px;">
                                客户类型
                            </td>
                            <td style="width:110px;">
                                渠道类型
                            </td>
                            <td style="width:190px;">
                                所属机构
                            </td>
                            <td style="width:183px;">
                                所属预算机构
                            </td>
                            <td style="width:40px;">
                                激活
                            </td>
                            <td style="width:60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvCustomer" runat="server" DataKeyNames="CustomerID" DataSourceID="odsCustomer"
                DefaultMode="Insert" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 88px;">
                                <asp:TextBox ID="txtCustomerNoByAdd" runat="server" Text='<%# Bind("CustomerNo") %>'
                                    Width="60px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 260px;">
                                <asp:TextBox ID="txtCustomerNameByAdd" runat="server" Text='<%# Bind("CustomerName") %>'
                                    Width="240px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 80px;">
                                &nbsp;
                            </td>
                            <td align="center" style="height: 22px; width: 140px;">
                                <asp:DropDownList runat="server" ID="dplCityByAdd" DataSourceID="sdsCity" DataTextField="CityName"
                                    DataValueField="CityID" SelectedValue='<%# Bind("CityID") %>' Width="120px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 110px;">
                                <asp:DropDownList runat="server" ID="dplCustomerTypeByAdd" DataSourceID="sdsCustomerType"
                                    DataTextField="CustomerTypeName" DataValueField="CustomerTypeID" SelectedValue='<%# Bind("CustomerTypeID") %>'
                                    Width="90px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 110px;">
                                <asp:DropDownList runat="server" ID="dplChannelTypeByAdd" DataSourceID="sdsChannelType"
                                    DataTextField="ChannelTypeName" DataValueField="ChannelTypeID" SelectedValue='<%# Bind("ChannelTypeID") %>'
                                    Width="90px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 190px">
                                <uc3:OUSelect ID="ApplyUCOUByAdd" runat="server" OUId='<%# Bind("ApplyOrganizationUnitID") %>' CssClass="InputText" Width="80px" />
                            </td>
                            <td align="center" style="height: 22px; width: 183px">
                                <uc3:OUSelect ID="UCOUByAdd" runat="server" OUId='<%# Bind("OrganizationUnitID") %>' CssClass="InputText" Width="80px" />
                            </td>
                            <td align="center" style="height: 22px; width: 40px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:RequiredFieldValidator ID="RFCustCode" runat="server" ControlToValidate="txtCustomerNoByAdd"
                                    Display="None" ErrorMessage="请您输入客户编号！" SetFocusOnError="True" ValidationGroup="custAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFCustName" runat="server" ControlToValidate="txtCustomerNameByAdd"
                                    Display="None" ErrorMessage="请您输入客户名称！" SetFocusOnError="True" ValidationGroup="custAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsCustAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="custAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="custAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="CateInsertValidationSummary" ValidationGroup="CateIns"
                        ShowMessageBox="true" ShowSummary="false" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsCustomer" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="CustomerTotalCount"
                DeleteMethod="DeleteCustomer" InsertMethod="InsertCustomer" OnInserted="odsCust_Inserted" SelectMethod="GetCustomerPaged"
                UpdateMethod="UpdateCustomer" OnUpdated="odsCust_Updated" OnDeleting="odsCustomer_Deleting" OnInserting="odsCustomer_Inserting"
                OnUpdating="odsCustomer_Updating">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:SqlDataSource ID="sdsCity" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="select CityID,Province.ProvinceName+'-'+City.CityName as CityName from City,Province where City.ProvinceID=Province.ProvinceID and City.IsActive=1 order by Province.ProvinceName,City.CityName">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="SELECT [CustomerTypeID], [CustomerTypeName] FROM [CustomerType] where IsActive=1">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsChannelType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="SELECT [ChannelTypeID], [ChannelTypeName] FROM [ChannelType] where IsActive=1">
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
    <table border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top">
                <table width="600px" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="10px" class="search_bg3">
                            &nbsp;
                        </td>
                        <td width="99%" height="30" class="search_bg3">
                            金额限制
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <img src="../Images/bannner2.gif" width="600px" height="18" />
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="CustAmountLimitUpdatePanel" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <gc:GridView ID="CustAmountLimitGridView" CssClass="GridView" runat="server" DataSourceID="odsCustAmountLimit"
                            AutoGenerateColumns="False" DataKeyNames="CustomerAmountLimitID" AllowPaging="True"
                            AllowSorting="True" PageSize="20" EnableModelValidation="True" Visible="False">
                            <Columns>
                                <asp:TemplateField SortExpression="Year" HeaderText="年份">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtYearByEdit" runat="server" Text='<%# Bind("Year") %>' MaxLength="4"
                                            Width="160px" CssClass="InputText"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblYearByEdit" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="187px" />
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Amount" HeaderText="金额">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtAmountByEdit" runat="server" MaxLength="15" Text='<%# Bind("Amount") %>'
                                            Width="260px" CssClass="InputText"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmountByEdit" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="330px" HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:RequiredFieldValidator ID="RFYear" runat="server" ControlToValidate="txtYearByEdit"
                                            Display="None" ErrorMessage="请您输入年份！" SetFocusOnError="True" ValidationGroup="amountLimitEdit"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RF1" runat="server" ControlToValidate="txtYearByEdit"
                                            ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                            ErrorMessage="年份必须为数字" ValidationGroup="amountLimitEdit"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RFAmount" runat="server" ControlToValidate="txtAmountByEdit"
                                            Display="None" ErrorMessage="请您输入金额！" SetFocusOnError="True" ValidationGroup="amountLimitEdit"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtAmountByEdit"
                                            ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                            ErrorMessage="金额必须为数字" ValidationGroup="amountLimitEdit"></asp:RegularExpressionValidator>
                                        <asp:ValidationSummary ID="vsAmountLimitEdit" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="amountLimitEdit" />
                                        <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                            ValidationGroup="amountLimitEdit" CommandName="Update" Text="更新"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                            CommandName="Cancel" Text="取消"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                            CommandName="Edit" Text="编辑"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CommandName="Delete"
                                            Text="删除" OnClientClick="return confirm('确定删除此行数据吗？');"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="Header" />
                            <EmptyDataTemplate>
                                <table>
                                    <tr class="Header">
                                        <td style="width: 187px;">
                                            年份
                                        </td>
                                        <td style="width: 330px;">
                                            金额
                                        </td>
                                        <td style="width: 80px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <SelectedRowStyle CssClass="SelectedRow" />
                        </gc:GridView>
                        <asp:FormView ID="CustAmountLimitFormView" runat="server" DataKeyNames="ShopID" DataSourceID="odsCustAmountLimit"
                            DefaultMode="Insert" Visible="False" EnableModelValidation="True" CellPadding="0">
                            <InsertItemTemplate>
                                <table class="FormView">
                                    <tr>
                                        <td align="center" style="height: 22px; width: 187px;">
                                            <asp:TextBox ID="txtYearByAdd" runat="server" MaxLength="4" Text='<%# Bind("Year") %>'
                                                Width="160px" CssClass="InputDate"></asp:TextBox>
                                        </td>
                                        <td align="center" style="height: 22px; width: 330px;">
                                            <asp:TextBox ID="txtAmountByAdd" runat="server" MaxLength="15" Text='<%# Bind("Amount") %>'
                                                Width="260px" CssClass="InputNumber"></asp:TextBox>
                                        </td>
                                        <td align="center" style="width: 80px;" align="center">
                                            <asp:RequiredFieldValidator ID="RFYear" runat="server" ControlToValidate="txtYearByAdd"
                                                Display="None" ErrorMessage="请您输入年份！" SetFocusOnError="True" ValidationGroup="amountLimitAdd"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RF1" runat="server" ControlToValidate="txtYearByAdd"
                                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                                ErrorMessage="年份必须为数字" ValidationGroup="amountLimitAdd"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RFAmount" runat="server" ControlToValidate="txtAmountByAdd"
                                                Display="None" ErrorMessage="请您输入金额！" SetFocusOnError="True" ValidationGroup="amountLimitAdd"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtAmountByAdd"
                                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                                ErrorMessage="金额必须为数字" ValidationGroup="amountLimitAdd"></asp:RegularExpressionValidator>
                                            <asp:ValidationSummary ID="vsAmountLimitAdd" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="amountLimitAdd" />
                                            <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                Text="新增" ValidationGroup="amountLimitAdd"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <asp:ValidationSummary ID="CateInsertValidationSummary" ValidationGroup="CateIns"
                                    ShowMessageBox="true" ShowSummary="false" runat="server" />
                            </InsertItemTemplate>
                        </asp:FormView>
                        <asp:ObjectDataSource ID="odsCustAmountLimit" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                            SelectCountMethod="CustomerAmountLimitTotalCount" SelectMethod="GetCustAmountOperatePaged"
                            InsertMethod="InsertCustAmountLimit" UpdateMethod="UpdateCustAmountLimit" OnUpdating="odsCustAmountLimit_Updating"
                            OnUpdated="odsCustAmountLimit_Updated" OnInserting="odsCustAmountLimit_Inserting"
                            OnInserted="odsCustAmountLimit_Inserted" EnablePaging="true" SortParameterName="sortExpression">
                            <DeleteParameters>
                                <asp:Parameter Name="stuffUser" Type="object" />
                                <asp:Parameter Name="position" Type="object" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="stuffUser" Type="object" />
                                <asp:Parameter Name="position" Type="object" />
                                <asp:Parameter Name="Year" Type="Int32" />
                                <asp:Parameter Name="Amount" Type="Int32" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="stuffUser" Type="object" />
                                <asp:Parameter Name="position" Type="object" />
                                <asp:Parameter Name="Year" Type="Int32" />
                                <asp:Parameter Name="Amount" Type="Int32" />
                                <asp:Parameter Name="CustId" Type="Int32" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:Parameter Name="CustId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td width="25px">
            </td>
            <td valign="top">
                <table width="643px" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="10px" class="search_bg3">
                            &nbsp;
                        </td>
                        <td width="99%" height="30" class="search_bg3">
                            次数限制
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <img src="../Images/bannner2.gif" width="643px" height="18" />
                        </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="CustTimesLimitUpdatePanel" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <gc:GridView ID="CustTimesLimitGridView" CssClass="GridView" runat="server" DataSourceID="odsCustTimesLimit"
                            AutoGenerateColumns="False" DataKeyNames="CustomerTimesLimitID" AllowPaging="True"
                            AllowSorting="True" PageSize="20" EnableModelValidation="True" Visible="False">
                            <Columns>
                                <asp:TemplateField SortExpression="ExpenseItemID" HeaderText="费用项">
                                    <EditItemTemplate>
                                        <asp:DropDownList runat="server" ID="dplExpenseItemByEdit" DataSourceID="sdsExpenseItem"
                                            DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" SelectedValue='<%# Bind("ExpenseItemID") %>'
                                            Width="380px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpenseItemNameByEdit" runat="server" Text='<%# GetExpenseItemNameById(Eval("ExpenseItemID")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="410px"  HorizontalAlign="Center"/>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Times" HeaderText="次数">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTimesByEdit" runat="server" MaxLength="15" Text='<%# Bind("Times") %>'
                                            Width="100px" CssClass="InputText"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTimesByEdit" runat="server" Text='<%# Bind("Times") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" HorizontalAlign="center" />
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:RequiredFieldValidator ID="RFExpenseItem" runat="server" ControlToValidate="dplExpenseItemByEdit"
                                            Display="None" ErrorMessage="必须选择费用项！" SetFocusOnError="True" ValidationGroup="timesLimitEdit"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RFTimes" runat="server" ControlToValidate="txtTimesByEdit"
                                            Display="None" ErrorMessage="请您输入次数！" SetFocusOnError="True" ValidationGroup="timesLimitEdit"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtTimesByEdit"
                                            ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                            ErrorMessage="次数必须为数字" ValidationGroup="timesLimitEdit"></asp:RegularExpressionValidator>
                                        <asp:ValidationSummary ID="vsAmountLimitEdit" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="timesLimitEdit" />
                                        <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                            ValidationGroup="timesLimitEdit" CommandName="Update" Text="更新"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                            CommandName="Cancel" Text="取消"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                            CommandName="Edit" Text="编辑"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CommandName="Delete"
                                            Text="删除" OnClientClick="return confirm('确定删除此行数据吗？');"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="Header" />
                            <EmptyDataTemplate>
                                <table>
                                    <tr class="Header">
                                        <td style="width: 410px;">
                                            费用项
                                        </td>
                                        <td style="width: 150px;">
                                            次数
                                        </td>
                                        <td style="width: 80px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <SelectedRowStyle CssClass="SelectedRow" />
                        </gc:GridView>
                        <asp:FormView ID="fvCustTimesLimit" runat="server" DataKeyNames="CustomerTimesLimitID"
                            DataSourceID="odsCustTimesLimit" DefaultMode="Insert" Visible="False" EnableModelValidation="True"
                            CellPadding="0">
                            <InsertItemTemplate>
                                <table class="FormView">
                                    <tr>
                                        <td align="center" style="width: 410px;">
                                            <asp:DropDownList runat="server" ID="dplExpenseItemByAdd" DataSourceID="sdsExpenseItem"
                                                DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" SelectedValue='<%# Bind("ExpenseItemID") %>'
                                                Width="380px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center" style="width: 150px;">
                                            <asp:TextBox ID="txtTimesByAdd" runat="server" MaxLength="15" Text='<%# Bind("Times") %>'
                                                Width="100px" CssClass="InputNumber"></asp:TextBox>
                                        </td>
                                        <td style="width: 80px;" align="center">
                                            <asp:RequiredFieldValidator ID="RFExpenseItem" runat="server" ControlToValidate="dplExpenseItemByAdd"
                                                Display="None" ErrorMessage="必须选择费用项！" SetFocusOnError="True" ValidationGroup="timesLimitAdd"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RFTimes" runat="server" ControlToValidate="txtTimesByAdd"
                                                Display="None" ErrorMessage="请您输入次数！" SetFocusOnError="True" ValidationGroup="timesLimitAdd"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtTimesByAdd"
                                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                                ErrorMessage="次数必须为数字" ValidationGroup="timesLimitAdd"></asp:RegularExpressionValidator>
                                            <asp:ValidationSummary ID="vsTimesLimitAdd" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="timesLimitAdd" />
                                            <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                Text="新增" ValidationGroup="timesLimitAdd"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </InsertItemTemplate>
                        </asp:FormView>
                        <asp:ObjectDataSource ID="odsCustTimesLimit" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                            SelectCountMethod="CustomerTimesLimitTotalCount" SelectMethod="GetCustTimesOperatePaged"
                            InsertMethod="InsertCustTimesLimit" UpdateMethod="UpdateCustTimesLimit" DeleteMethod="DeleteCustTimesLimit"
                            OnInserting="odsCustTimesLimit_Inserting" OnUpdating="odsCustTimesLimit_Updating"
                            OnUpdated="odsCustTimesLimit_Updated" OnInserted="odsCustTimesLimit_Inserted"
                            OnDeleting="odsCustTimesLimit_Deleting" OnDeleted="odsCustTimesLimit_Deleted"
                            EnablePaging="true" SortParameterName="sortExpression">
                            <DeleteParameters>
                                <asp:Parameter Name="stuffUser" Type="object" />
                                <asp:Parameter Name="position" Type="object" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="stuffUser" Type="object" />
                                <asp:Parameter Name="position" Type="object" />
                                <asp:Parameter Name="ExpenseItemID" Type="Int32" />
                                <asp:Parameter Name="Times" Type="Int32" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="stuffUser" Type="object" />
                                <asp:Parameter Name="position" Type="object" />
                                <asp:Parameter Name="ExpenseItemID" Type="Int32" />
                                <asp:Parameter Name="Times" Type="Int32" />
                                <asp:Parameter Name="CustId" Type="Int32" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:Parameter Name="CustId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:SqlDataSource ID="sdsExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="SELECT ExpenseItemID,AccountingCode+'--'+AccountingName ExpenseItemName FROM ExpenseItem where IsInContract=1 and IsActive=1">
                        </asp:SqlDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
