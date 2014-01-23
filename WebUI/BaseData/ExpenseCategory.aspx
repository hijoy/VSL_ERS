<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ExpenseCategory" Codebehind="ExpenseCategory.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        费用大类</div>
    <asp:UpdatePanel ID="upCategory" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCategory" CssClass="GridView" runat="server" DataSourceID="odsCategory"
                AutoGenerateColumns="False" DataKeyNames="ExpenseCategoryID" AllowPaging="True"
                CellPadding="0" AllowSorting="True" PageSize="20" OnSelectedIndexChanged="gvCategory_SelectedIndexChanged"
                OnRowDeleted="gvCategory_RowDeleted" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ExpenseCategoryName" HeaderText="费用大类名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCategoryByEdit" runat="server" Text='<%# Bind("ExpenseCategoryName") %>'
                                Width="1110px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnCategoryName" runat="server" CausesValidation="False" CommandName="Select"
                                Text='<%# Bind("ExpenseCategoryName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="1147px" HorizontalAlign="Center" />
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
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFCategory" runat="server" ControlToValidate="txtCategoryByEdit"
                                Display="None" ErrorMessage="请您输入费用大类名称！" SetFocusOnError="True" ValidationGroup="cateEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsCateEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="cateEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="cateEdit" CommandName="Update" Text="更新"></asp:LinkButton>
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
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 1147px;" class="Empty1">
                                费用大类名称
                            </td>
                            <td align="center" style="width: 60px;">
                                激活
                            </td>
                            <td align="center" style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvCategory" runat="server" DataKeyNames="ExpenseCategoryID" DataSourceID="odsCategory"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" scope="col" style="height: 22px; width: 1147px;">
                                <asp:TextBox ID="txtCategoryNameByAdd" runat="server" Text='<%# Bind("ExpenseCategoryName") %>'
                                    Width="1110px" CssClass="InputText" ValidationGroup="CateIns" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFCategory" runat="server" ControlToValidate="txtCategoryNameByAdd"
                                    Display="None" ErrorMessage="请您输入费用大类名称！" SetFocusOnError="True" ValidationGroup="cateAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsCateAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="cateAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="cateAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="CateInsertValidationSummary" ValidationGroup="CateIns"
                        ShowMessageBox="true" ShowSummary="false" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsCategory" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteCategory" InsertMethod="InsertExpenseCategory" SelectMethod="GetExpenseCategory"
                UpdateMethod="UpdateCategory" OnDeleting="odsCategory_Deleting" OnInserting="odsCategory_Inserting"
                OnUpdating="odsCategory_Updating">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ExpenseCategoryName" Type="String" Size="50" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ExpenseCategoryName" Type="String" Size="50" />
                </InsertParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="title" style="width: 1260px;">
        费用小类</div>
    <asp:UpdatePanel ID="upSubCategory" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView Visible="false" ID="gvSubCategory" CssClass="GridView" runat="server"
                DataSourceID="odsSubCategory" AutoGenerateColumns="False" CellPadding="0" AllowSorting="True"
                AllowPaging="True" PageSize="20" DataKeyNames="ExpenseSubCategoryId">
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 946px;" class="Empty1">
                                费用小类名称
                            </td>
                            <td style="width: 200px;">
                                表单类型
                            </td>
                            <td style="width: 60px;">
                                激活
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="费用小类名称" SortExpression="ExpenseSubCategoryName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSubCategoryNameByEdit" runat="server" Text='<%# Bind("ExpenseSubCategoryName") %>'
                                CssClass="InputText" Width="910px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbtnSubCategoryName" runat="server" CausesValidation="False" Text='<%# Bind("ExpenseSubCategoryName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="946px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="表单类型" SortExpression="PageType">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplPageTypeByEdit" Width="180px" SelectedValue='<%# Bind("PageType") %>'>
                                <asp:ListItem Text="请选择表单类型" Value=""></asp:ListItem>
                                <asp:ListItem Text="价量相关" Value="0"></asp:ListItem>
                                <asp:ListItem Text="非价量相关" Value="1"></asp:ListItem>
                                <asp:ListItem Text="返利" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSubCategoryPageType" runat="server" Text='<%# getPageType(Eval("PageType"))%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="激活">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Width="40px" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Width="40px" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFSubCategoryEdit" runat="server" ControlToValidate="txtSubCategoryNameByEdit"
                                Display="None" ErrorMessage="请您输入费用小类名称！" SetFocusOnError="True" ValidationGroup="subCateEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsSubCateEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="subCateEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" ValidationGroup="subCateEdit"
                                CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Edit" Text="编辑"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </gc:GridView>
            <asp:FormView ID="fvSubCategory" Visible="false" runat="server" DataKeyNames="SubCategoryId"
                DataSourceID="odsSubCategory" DefaultMode="Insert" OnItemInserting="fvSubCategory_ItemInserting"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 946px;">
                                <asp:TextBox ID="txtSubCategoryNameByAdd" runat="server" Text='<%# Bind("ExpenseSubCategoryName") %>'
                                    CssClass="InputText" ValidationGroup="SubCategoryInsert" Width="910px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 200px;">
                                <asp:DropDownList runat="server" ID="dplPageTypeByAdd" Width="180px" SelectedValue='<%# Bind("PageType") %>'>
                                    <asp:ListItem Text="价量相关" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="非价量相关" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="返利" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Width="40px" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFSubCategoryAdd" runat="server" ControlToValidate="txtSubCategoryNameByAdd"
                                    Display="None" ErrorMessage="请您输入费用小类名称！" SetFocusOnError="True" ValidationGroup="subCateAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsSubCateAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="subCateAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="subCateAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="SubCategoryInsertValidationSummary" ValidationGroup="SubCategoryInsert"
                        runat="server" ShowMessageBox="true" ShowSummary="false" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsSubCategory" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteSubCategory" InsertMethod="InsertSubCategory" SelectMethod="GetSubCategoryPaged"
                EnablePaging="true" SelectCountMethod="ExpenseSubCategoryTotalCount" UpdateMethod="UpdateSubCategory"
                SortParameterName="sortExpression" OnDeleting="odsSubCategory_Deleting" OnSelecting="odsSubCategory_Selecting"
                OnInserting="odsSubCategory_Inserting" OnUpdating="odsSubCategory_Updating">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ExpenseSubCategoryName" Type="String" Size="50" />
                    <asp:Parameter Name="PageType" Type="Int32" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ExpenseSubCategoryName" Type="String" Size="50" />
                    <asp:Parameter Name="CateId" Type="Int32" />
                    <asp:Parameter Name="PageType" Type="Int32" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="CateId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
