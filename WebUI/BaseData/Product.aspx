<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Product" Codebehind="Product.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px;" class="field_title">
                    产品编号
                </td>
                <td style="width: 200px;" class="field_title">
                    产品名称
                </td>
                <td style="width: 200px;" class="field_title">
                    产品种类
                </td>
                <td style="width: 200px;" class="field_title">
                    激活
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtSKUNo"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtSKUName"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplSKUCategory" Width="150px" DataSourceID="sdsSKUCateAll"
                        AppendDataBoundItems="true" DataTextField="SKUCategoryName" DataValueField="SKUCategoryID">
                        <asp:ListItem Selected="True" Text="所有种类" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsSKUCateAll" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select SKUCategoryID,SKUCategoryName from SKUCategory"></asp:SqlDataSource>
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplSKUActive" Width="150px">
                        <asp:ListItem Selected="True" Text="全部" Value="3"></asp:ListItem>
                        <asp:ListItem Text="激活" Value="1"></asp:ListItem>
                        <asp:ListItem Text="禁用" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="vertical-align: top; width: 400px;">
                    &nbsp;
                </td>
                <td style="width: 150px; vertical-align: middle;" align="center" rowspan="2">
                    <asp:Button runat="server" ID="lbtnSearch" Text="搜 索" CssClass="button_nor" OnClick="lbtnSearch_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1260px;">
        产品信息</div>
    <asp:UpdatePanel ID="upSKU" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvSKU" CssClass="GridView" runat="server" DataSourceID="odsSKU"
                AutoGenerateColumns="False" DataKeyNames="SKUID" AllowPaging="True" AllowSorting="True"
                PageSize="20" OnRowDeleted="gvSKU_RowDeleted" EnableModelValidation="True" OnSelectedIndexChanged="gvSKU_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField SortExpression="SKUNo" HeaderText="产品编号">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSKUNoByEdit" runat="server" Text='<%# Bind("SKUNo") %>' Width="80px"
                                MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSKUNo" runat="server" CausesValidation="False" CommandName="Select"
                                Text='<%# Bind("SKUNo") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="SKUName" HeaderText="产品名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSKUNameByEdit" runat="server" Text='<%# Bind("SKUName") %>' Width="180px"
                                MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSKUName" runat="server" Text='<%# Bind("SKUName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Spec" HeaderText="包装规格">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSpecByEdit" runat="server" Text='<%# Bind("Spec") %>' Width="180px"
                                MaxLength="500"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSpec" runat="server" Text='<%# Bind("Spec") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="SKUCategoryId" HeaderText="产品类别">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplSKUCateByEdit" Width="130px" SelectedValue='<%# Bind("SKUCategoryID") %>'
                                DataSourceID="sdsSKUCateEnabled" DataTextField="SKUCategoryName" DataValueField="SKUCategoryID">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="ExpenseSubCategorLabel" runat="server" Text='<%#GetSKUCateNameById(Eval("SKUCategoryID"))%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PackageQuantity" HeaderText="装箱数">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPackageQuantityByEdit" runat="server" Text='<%# Bind("PackageQuantity") %>'
                                Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPackageQuantity" runat="server" Text='<%# Bind("PackageQuantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PackagePercent" HeaderText="标箱比例">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPackagePercentByEdit" runat="server" Text='<%# Bind("PackagePercent") %>'
                                Width="60px" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPackagePercent" runat="server" Text='<%# Bind("PackagePercent") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Taste" HeaderText="口味">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTasteByEdit" runat="server" Text='<%# Bind("Taste") %>' Width="80px"
                                MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTaste" runat="server" Text='<%# Bind("Taste") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="SKUCostCenter" HeaderText="成本中心">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCostCenterByEdit" runat="server" Text='<%# Bind("SKUCostCenter") %>'
                                Width="110px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("SKUCostCenter") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="130px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="激活">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CostPrice" HeaderText="成本单价">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCostPriceByEdit" runat="server" Text='<%# Bind("CostPrice") %>'
                                Width="80px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCostPrice" runat="server" Text='<%# Bind("CostPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFSKUNo" runat="server" ControlToValidate="txtSKUNoByEdit"
                                Display="None" ErrorMessage="请您输入产品编号！" SetFocusOnError="True" ValidationGroup="skuEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFSKUName" runat="server" ControlToValidate="txtSKUNameByEdit"
                                Display="None" ErrorMessage="请您输入产品名称！" SetFocusOnError="True" ValidationGroup="skuEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFTel" runat="server" ControlToValidate="txtPackageQuantityByEdit"
                                Display="None" ErrorMessage="请您输入装箱数！" SetFocusOnError="True" ValidationGroup="skuEdit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF1" runat="server" ControlToValidate="txtPackageQuantityByEdit"
                                ValidationExpression="<%$ Resources:RegularExpressions, Number %>" Display="None"
                                ErrorMessage="请输入数字" ValidationGroup="skuEdit"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFEmail" runat="server" ControlToValidate="txtPackagePercentByEdit"
                                Display="None" ErrorMessage="请您输入装箱比！" SetFocusOnError="True" ValidationGroup="skuEdit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtPackagePercentByEdit"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="请输入数字" ValidationGroup="skuEdit"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFSpec" runat="server" ControlToValidate="txtSpecByEdit"
                                Display="None" ErrorMessage="请您输入包装规格！" ValidationGroup="skuEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFCostCenter" runat="server" ControlToValidate="txtCostPriceByEdit"
                                Display="None" ErrorMessage="请您输入成本价格！" SetFocusOnError="True" AccessKeyusOnError="True"
                                ValidationGroup="skuEdit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="txtCostPriceByEdit"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="请输入数字" ValidationGroup="skuEdit"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPackagePercentByEdit"
                                Display="None" ErrorMessage="请您输入装箱比！" SetFocusOnError="True" ValidationGroup="skuEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsSKUEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="skuEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="skuEdit" CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text="编辑"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 100px;" class="Empty1">
                                产品编号
                            </td>
                            <td align="center" style="width: 200px;">
                                产品名称
                            </td>
                            <td align="center" style="width: 200px;">
                                包装规格
                            </td>
                            <td align="center" style="width: 150px;">
                                产品类别
                            </td>
                            <td align="center" style="width: 80px;">
                                装箱数
                            </td>
                            <td align="center" style="width: 80px;">
                                标箱比例
                            </td>
                            <td align="center" style="width: 100px;">
                                口味
                            </td>
                            <td align="center" style="width: 130px;">
                                成本中心
                            </td>
                            <td align="center" style="width: 60px;">
                                激活
                            </td>
                            <td align="center" style="width: 100px;">
                                成本单价
                            </td>
                            <td align="center" style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvSKU" runat="server" DataKeyNames="SKUID" DataSourceID="odsSKU"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr class="Header">
                            <td align="center" style="height: 22px; width: 100px;">
                                <asp:TextBox ID="txtSKUNoByAdd" MaxLength="20" runat="server" Text='<%# Bind("SKUNo") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 200px;">
                                <asp:TextBox ID="txtSKUNameByAdd" runat="server" MaxLength="40" Text='<%# Bind("SKUName") %>'
                                    Width="180px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 200px;">
                                <asp:TextBox ID="txtSpecByAdd" runat="server" Text='<%# Bind("Spec") %>' Width="180px"
                                    MaxLength="200"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 150px;">
                                <asp:DropDownList runat="server" ID="dplSKUCateByAdd" Width="130px" DataTextField="SKUCategoryName"
                                    DataValueField="SKUCategoryID" SelectedValue='<%# Bind("SKUCategoryID") %>' DataSourceID="sdsSKUCateEnabled">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 80px;">
                                <asp:TextBox ID="txtPackageQuantityByAdd" runat="server" MaxLength="15" Text='<%# Bind("PackageQuantity") %>'
                                    Width="60px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 80px;">
                                <asp:TextBox ID="txtPackagePercentByAdd" runat="server" MaxLength="15" Text='<%# Bind("PackagePercent") %>'
                                    Width="60px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 100px;">
                                <asp:TextBox ID="txtTasteByAdd" runat="server" MaxLength="20" Text='<%# Bind("Taste") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 130px;">
                                <asp:TextBox ID="txtCostCenterByAdd" runat="server" MaxLength="20" Text='<%# Bind("SKUCostCenter") %>'
                                    Width="110px" CssClass="InputText"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="height: 22px; width: 100px;">
                                <asp:TextBox ID="txtCostPriceByAdd" runat="server" MaxLength="15" Text='<%# Bind("CostPrice") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFSKUNo" runat="server" ControlToValidate="txtSKUNoByAdd"
                                    Display="None" ErrorMessage="请您输入产品编号！" SetFocusOnError="True" ValidationGroup="skuAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFSKUName" runat="server" ControlToValidate="txtSKUNameByAdd"
                                    Display="None" ErrorMessage="请您输入产品名称！" SetFocusOnError="True" ValidationGroup="skuAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFPachageQuantity" runat="server" ControlToValidate="txtPackageQuantityByAdd"
                                    Display="None" ErrorMessage="请您输入装箱数！" SetFocusOnError="True" ValidationGroup="skuAdd"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RF1" runat="server" ControlToValidate="txtPackageQuantityByAdd"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Number %>" Display="None"
                                    ErrorMessage="装箱数只能为整数！" ValidationGroup="skuAdd"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RFEmail" runat="server" ControlToValidate="txtPackagePercentByAdd"
                                    Display="None" ErrorMessage="请您输入装箱比！" SetFocusOnError="True" ValidationGroup="skuAdd"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtPackagePercentByAdd"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                    ErrorMessage="请输入数字" ValidationGroup="skuAdd"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RFSpec" runat="server" ControlToValidate="txtSpecByAdd"
                                    Display="None" ErrorMessage="请您输入包装规格！" SetFocusOnError="True" ValidationGroup="skuAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCostCenterByAdd"
                                    Display="None" ErrorMessage="请您输入成本中心！" SetFocusOnError="True" ValidationGroup="skuAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCostPriceByAdd"
                                    Display="None" ErrorMessage="请您输入成本单价！" SetFocusOnError="True" ValidationGroup="skuAdd"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="txtCostPriceByAdd"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                    ErrorMessage="请输入数字" ValidationGroup="skuAdd"></asp:RegularExpressionValidator>
                                <asp:ValidationSummary ID="vsSKUAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="skuAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="skuAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:SqlDataSource ID="sdsSKUCateEnabled" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="select SKUCategoryID,SKUCategoryName from SKUCategory where IsActive=1">
            </asp:SqlDataSource>
            <asp:ObjectDataSource ID="odsSKU" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="SKUTotalCount"
                DeleteMethod="DeleteSKU" InsertMethod="InsertSKU" SelectMethod="GetSKUPaged"
                UpdateMethod="UpdateSKU" OnDeleting="odsSKU_Deleting" OnInserting="odsSKU_Inserting"
                OnUpdating="odsSKU_Updating">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="SKUNo" Type="String" />
                    <asp:Parameter Name="SKUName" Type="String" />
                    <asp:Parameter Name="SKUCategoryID" Type="Int32" />
                    <asp:Parameter Name="PackageQuantity" Type="Int32" />
                    <asp:Parameter Name="PackagePercent" Type="Decimal" />
                    <asp:Parameter Name="Spec" Type="String" />
                    <asp:Parameter Name="SKUCostCenter" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="CostPrice" Type="Decimal" />
                    <asp:Parameter Name="Taste" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="SKUNo" Type="String" />
                    <asp:Parameter Name="SKUName" Type="String" />
                    <asp:Parameter Name="SKUCategoryID" Type="Int32" />
                    <asp:Parameter Name="PackageQuantity" Type="Int32" />
                    <asp:Parameter Name="PackagePercent" Type="Decimal" />
                    <asp:Parameter Name="Spec" Type="String" />
                    <asp:Parameter Name="SKUCostCenter" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="CostPrice" Type="Decimal" />
                    <asp:Parameter Name="Taste" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="title" style="width: 1260px;">
        产品价格信息</div>
    <asp:UpdatePanel ID="upSKUPrice" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView Visible="false" ID="gvSKUPrice" CssClass="GridView" runat="server" DataSourceID="odsSKUPrice"
                AutoGenerateColumns="False" CellPadding="0" AllowSorting="True" AllowPaging="True"
                PageSize="20" DataKeyNames="SKUPriceID">
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table class="GridView" border="0" cellpadding="0" cellspacing="0">
                        <tr class="Header">
                            <td scope="col" style="width: 548px;">
                                客户类型
                            </td>
                            <td align="center" scope="col" style="height: 22px; width: 660px;">
                                价格
                            </td>
                            <td scope="col" style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="客户类型" SortExpression="CustomrTypeID">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplCustTypeByEdit" Width="520px" DataTextField="CustomerTypeName"
                                DataValueField="CustomerTypeID" DataSourceID="sdsCustTypeEnabled" SelectedValue='<%# Bind("CustomerTypeID") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCustTypeByEdit" runat="server" Text='<%#GetCustTypeNameById(Eval("CustomerTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="548px" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Price" HeaderText="价格">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSKUPriceByEdit" runat="server" Text='<%# Bind("Price") %>' CssClass="InputNumber"
                                Width="640px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSKUPriceByEdit" runat="server" Text='<%# Eval("Price") %>' CssClass="NumberLabel"
                                Width="640px"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="660px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFSKUPrice" runat="server" ControlToValidate="txtSKUPriceByEdit"
                                Display="None" ErrorMessage="请您输入价格！" SetFocusOnError="True" ValidationGroup="SKUPriceEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsSKUPriceEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="SKUPriceEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" ValidationGroup="SKUPriceEdit"
                                CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Edit" Text="编辑"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" Visible="<%# HasManageRight %>" runat="server" CommandName="Delete"
                                Text="删除" OnClientClick="return confirm('确定删除此行数据吗？');"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="60px" />
                    </asp:TemplateField>
                </Columns>
            </gc:GridView>
            <asp:FormView ID="fvSKUPrice" Visible="false" runat="server" DataKeyNames="SKUPriceId"
                DataSourceID="odsSKUPrice" DefaultMode="Insert" OnItemInserting="fvSKUPrice_ItemInserting">
                <InsertItemTemplate>
                    <table class="GridView" style="border-top-width: 0px; margin-top: 0px;">
                        <tr>
                            <td scope="col" style="height: 22px; width: 548px; border-left-width: 0px;">
                                <asp:DropDownList runat="server" ID="dplCustTypeByAdd" Width="520px" DataTextField="CustomerTypeName"
                                    SelectedValue='<%# Bind("CustomerTypeID") %>' DataValueField="CustomerTypeID"
                                    DataSourceID="sdsCustTypeEnabled">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="height: 22px; width: 660px;">
                                <asp:TextBox ID="txtSKUPriceByAdd" runat="server" Text='<%# Bind("Price") %>' CssClass="InputText"
                                    ValidationGroup="SKUPriceInsert" Width="640px"></asp:TextBox>
                            </td>
                            <td scope="col" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFSKUPrice" runat="server" ControlToValidate="txtSKUPriceByAdd"
                                    Display="None" ErrorMessage="请您输入价格！" SetFocusOnError="True" ValidationGroup="SKUPriceAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsSKUPriceAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="SKUPriceAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="SKUPriceAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="SKUPriceInsertValidationSummary" ValidationGroup="SKUPriceInsert"
                        runat="server" ShowMessageBox="true" ShowSummary="false" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:SqlDataSource ID="sdsCustTypeEnabled" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="select CustomerTypeID,CustomerTypeName from CustomerType"></asp:SqlDataSource>
            <asp:ObjectDataSource ID="odsSKUPrice" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteSKUPrice" InsertMethod="InsertSKUPrice" SelectMethod="GetSKUPricePaged"
                EnablePaging="true" SelectCountMethod="SKUPriceTotalCount" UpdateMethod="UpdateSKUPrice"
                SortParameterName="sortExpression" OnDeleting="odsSKUPrice_Deleting" OnSelecting="odsSKUPrice_Selecting"
                OnInserting="odsSKUPrice_Inserting" OnUpdating="odsSKUPrice_Updating" OnInserted="odsSKUPrice_Inserted"
                OnUpdated="odsSKUPrice_Updated" OnDeleted="odsSKUPrice_Deleted">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="positionID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="positionID" Type="Int32" />
                    <asp:Parameter Name="CustomerTypeID" Type="Int32" />
                    <asp:Parameter Name="Price" Type="Decimal" />
                    <asp:Parameter Name="SKUId" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CustomerTypeID" Type="Int32" />
                    <asp:Parameter Name="Price" Type="Decimal" />
                    <asp:Parameter Name="SKUId" Type="Int32" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="SKUId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
