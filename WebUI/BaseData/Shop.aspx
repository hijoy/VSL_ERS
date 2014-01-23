<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Shop" Codebehind="Shop.aspx.cs" %>

<%@ Register Src="../UserControls/CustomerControl.ascx" TagName="CustomerSelect"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <div style="width: 1260px;" class="title">
        搜索条件
    </div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px;" class="field_title">
                    门店名称
                </td>
                <td style="width: 260px;" class="field_title">
                    客户
                </td>
                <td style="width: 200px;" class="field_title">
                    门店等级
                </td>
                <td style="width: 450px;">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtShopNameBySearch"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <uc3:CustomerSelect ID="UCCustomerBySearch" runat="server" CssClass="InputText" Width="120px" />
                </td>
                <td style="vertical-align: top">
                    <asp:DropDownList runat="server" ID="dplShopLevelBySearch" DataSourceID="sdsShopLevel"
                        DataTextField="ShopLevelName" DataValueField="ShopLevelID" AppendDataBoundItems="true"
                        Width="180px">
                        <asp:ListItem Selected="True" Text="全部" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button runat="server" ID="lbtnSearch" Text="搜 索" CssClass="button_nor" OnClick="lbtnSearch_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width: 1260px;" class="title">
        门店信息</div>
    <asp:UpdatePanel ID="upShop" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvShop" CssClass="GridView" runat="server" DataSourceID="odsShop"
                AutoGenerateColumns="False" DataKeyNames="ShopID" AllowPaging="True" AllowSorting="True"
                PageSize="20" OnRowDeleted="gvShop_RowDeleted" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ShopName" HeaderText="门店名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtShopNameByEdit" runat="server" Text='<%# Bind("ShopName") %>'
                                Width="160px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblShopNameByEdit" runat="server" Text='<%# Bind("ShopName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ShopNo" HeaderText="门店编号">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtShopNoByEdit" runat="server" Text='<%# Bind("ShopNo") %>' Width="80px"
                                CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblShopNoByEdit" runat="server" Text='<%# Bind("ShopNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CustomerID" HeaderText="客户" InsertVisible="False">
                        <EditItemTemplate>
                            <uc3:CustomerSelect ID="UCCustomerByEdit" runat="server" CssClass="InputText" Width="140px"
                                CustomerID='<%# Bind("CustomerID") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerNameByEdit" runat="server" Text='<%#GetCustomerNameById (Eval("CustomerID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="225px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ShopLevelID" HeaderText="门店等级">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplShopLevelByEdit" DataSourceID="sdsShopLevel"
                                DataTextField="ShopLevelName" DataValueField="ShopLevelID" SelectedValue='<%# Bind("ShopLevelID") %>'
                                Width="60px">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblShopLevelByEdit" runat="server" Text='<%# GetShopLevelNameById(Eval("ShopLevelID") )%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Address" HeaderText="地址">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAddressByEdit" runat="server" Text='<%# Bind("Address") %>' Width="230px"
                                CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAddressByEdit" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Contacter" HeaderText="联系人">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtContacterByEdit" runat="server" Text='<%# Bind("Contacter") %>'
                                Width="70px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblContacterByEdit" runat="server" Text='<%# Bind("Contacter") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Tel" HeaderText="电话">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTelByEdit" runat="server" Text='<%# Bind("Tel") %>' Width="90px"
                                CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTelByEdit" runat="server" Text='<%# Bind("Tel") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="110px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Email" HeaderText="邮箱">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmailByEdit" runat="server" Text='<%# Bind("Email") %>' Width="110px"
                                CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEmailByEdit" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
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
                        <ItemStyle Width="36px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFShopNameEdit" runat="server" ControlToValidate="txtShopNameByEdit"
                                Display="None" ErrorMessage="请您输入门店名称！" SetFocusOnError="True" ValidationGroup="shopEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFAddress" runat="server" ControlToValidate="txtAddressByEdit"
                                Display="None" ErrorMessage="请您输入地址！" SetFocusOnError="True" ValidationGroup="shopEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsShopEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="shopEdit" />
                            <asp:LinkButton ID="lbtnUpdateByEdit" Visible="<%# HasManageRight %>" runat="server"
                                CausesValidation="True" ValidationGroup="shopEdit" CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnEdit" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEdit" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text="编辑"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 180px;" class="Empty1">
                                门店名称
                            </td>
                            <td style="width: 100px;">
                                门店编号
                            </td>
                            <td style="width: 225px;">
                                客户
                            </td>
                            <td style="width: 80px;">
                                门店等级
                            </td>
                            <td style="width: 250px;">
                                地址
                            </td>
                            <td style="width: 90px;">
                                联系人
                            </td>
                            <td style="width: 110px;">
                                电话
                            </td>
                            <td style="width: 130px;">
                                邮箱
                            </td>
                            <td style="width: 36px;">
                                激活
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvShop" runat="server" DataKeyNames="ShopID" DataSourceID="odsShop"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 180px;">
                                <asp:TextBox ID="txtShopNameByAdd" runat="server" Text='<%# Bind("ShopName") %>'
                                    Width="160px" CssClass="InputText" MaxLength="40" ValidationGroup="ItemIns"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 100px;">
                                <asp:TextBox ID="txtShopNoByAdd" runat="server" Text='<%# Bind("ShopNo") %>' Width="80px"
                                    CssClass="InputText" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 225px;">
                                <uc3:CustomerSelect ID="UCCustomerByAdd" runat="server" CssClass="InputText" Width="140px"
                                    CustomerID='<%# Bind("CustomerID") %>' IsNoClear="true" />
                            </td>
                            <td align="center" style="height: 22px; width: 80px;">
                                <asp:DropDownList runat="server" ID="dplShopLevelByAdd" DataSourceID="sdsShopLevel"
                                    DataTextField="ShopLevelName" DataValueField="ShopLevelID" SelectedValue='<%# Bind("ShopLevelID") %>'
                                    Width="60px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 250px;">
                                <asp:TextBox ID="txtAddressByAdd" runat="server" Text='<%# Bind("Address") %>' Width="230px"
                                    CssClass="InputText" MaxLength="200"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 90px;">
                                <asp:TextBox ID="txtContacterByAdd" runat="server" Text='<%# Bind("Contacter") %>'
                                    Width="70px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 110px;">
                                <asp:TextBox ID="txtTelByAdd" runat="server" MaxLength="20" Text='<%# Bind("Tel") %>'
                                    Width="90px" CssClass="InputText"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 130px;">
                                <asp:TextBox ID="txtEmailByAdd" runat="server" MaxLength="20" Text='<%# Bind("Email") %>'
                                    Width="110px" CssClass="InputText"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 36px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFShopNameEdit" runat="server" ControlToValidate="txtShopNameByAdd"
                                    Display="None" ErrorMessage="请您输入门店名称！" SetFocusOnError="True" ValidationGroup="shopAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFAddress" runat="server" ControlToValidate="txtAddressByAdd"
                                    Display="None" ErrorMessage="请您输入地址！" SetFocusOnError="True" ValidationGroup="shopAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsShopAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="shopAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="shopAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsShop" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="ShopTotalCount"
                DeleteMethod="DeleteShop" InsertMethod="InsertShop" SelectMethod="GetShopPaged"
                UpdateMethod="UpdateShop" OnDeleted="odsShop_Deleted" OnInserted="odsShop_Inserted"
                OnUpdated="odsShop_Updated" OnDeleting="odsShop_Deleting" OnInserting="odsShop_Inserting"
                OnUpdating="odsShop_Updating">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ShopID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ShopID" Type="Int32" />
                    <asp:Parameter Name="ShopName" Type="String" Size="50" />
                    <asp:Parameter Name="CustomerID" Type="Int32" />
                    <asp:Parameter Name="ShopLevelID" Type="Int32" />
                    <asp:Parameter Name="Address" Type="String" Size="100" />
                    <asp:Parameter Name="Contacter" Type="String" Size="50" />
                    <asp:Parameter Name="Tel" Type="String" Size="50" />
                    <asp:Parameter Name="Email" Type="String" Size="50" />
                    <asp:Parameter Name="ShopNo" Type="String" Size="50" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ShopName" Type="String" />
                    <asp:Parameter Name="CustomerID" Type="Int32" />
                    <asp:Parameter Name="ShopLevelID" Type="Int32" />
                    <asp:Parameter Name="Address" Type="String" Size="100" />
                    <asp:Parameter Name="Contacter" Type="String" Size="50" />
                    <asp:Parameter Name="Tel" Type="String" Size="50" />
                    <asp:Parameter Name="ShopNo" Type="String" Size="50" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:SqlDataSource ID="sdsShopLevel" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="SELECT [ShopLevelID], [ShopLevelName] FROM [ShopLevel] where IsActive=1">
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdsCustomer" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="SELECT [CustomerID], [CustomerName] FROM [Customer] where IsActive=1">
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
