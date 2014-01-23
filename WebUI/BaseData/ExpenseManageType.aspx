<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="ExpenseManageType" CodeBehind="ExpenseManageType.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        管理费用类别维护</div>
    <asp:UpdatePanel ID="upExpenseManageType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvExpenseManageType" CssClass="GridView" runat="server" DataSourceID="odsExpenseManageType"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="ExpenseManageTypeID"
                AllowPaging="True" AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ExpenseManageTypeName" HeaderText="管理费用类别名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtExpenseManageTypeName" runat="server" Text='<%# Bind("ExpenseManageTypeName") %>'
                                Width="620px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseManageTypeName" runat="server" Text='<%# Bind("ExpenseManageTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="645px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="AccountingCode" HeaderText="科目编号">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>'
                                Width="170px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="190px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="AccountingName" HeaderText="科目名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccountingName" runat="server" Text='<%# Bind("AccountingName") %>'
                                Width="170px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingName" runat="server" Text='<%# Bind("AccountingName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="190px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ExpenseMangeCategoryID" HeaderText="费用类别">
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlExpenseMangeCategory" runat="server" SelectedValue='<%# Bind("ExpenseManageCategoryID") %>'>
                                <asp:ListItem Text="个人费用报销" Value="1"></asp:ListItem>
                                <asp:ListItem Text="出差费用报销" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseManageCategory" runat="server" Text='<%# GetExpenseManageCategoryName(Eval("ExpenseManageCategoryID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120" HorizontalAlign="Center" />
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
                            <asp:RequiredFieldValidator ID="RFExpenseManageTypeName" runat="server" ControlToValidate="txtExpenseManageTypeName"
                                Display="None" ErrorMessage="请您输入管理费用类别名称！" SetFocusOnError="True" ValidationGroup="ExpenseManageTypeEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFAccountingCode" runat="server" ControlToValidate="txtAccountingCode"
                                Display="None" ErrorMessage="请您输入科目编号！" SetFocusOnError="True" ValidationGroup="ExpenseManageTypeEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFAccountingName" runat="server" ControlToValidate="txtAccountingName"
                                Display="None" ErrorMessage="请您输入科目名称！" SetFocusOnError="True" ValidationGroup="ExpenseManageTypeEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsExpenseManageTypeEdit" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="ExpenseManageTypeEdit" />
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" ValidationGroup="ExpenseManageTypeEdit"
                                CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CommandName="Cancel"
                                Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="编辑"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 645px" class="Empty1">
                                管理费用类别名称
                            </td>
                            <td style="width: 250px">
                                科目编号
                            </td>
                            <td style="width: 250px">
                                科目名称
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
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvExpenseManageType" runat="server" DataKeyNames="ExpenseManageTypeID"
                DataSourceID="odsExpenseManageType" DefaultMode="Insert" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 645px;">
                                <asp:TextBox ID="txtExpenseManageTypeNameByAdd" runat="server" Text='<%# Bind("ExpenseManageTypeName") %>'
                                    Width="620px" CssClass="InputText" ValidationGroup="ExpenseManageTypeINS" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 190px;">
                                <asp:TextBox ID="txtAccountingCodeByAdd" runat="server" Text='<%# Bind("AccountingCode") %>'
                                    Width="170px" CssClass="InputText" ValidationGroup="ExpenseManageTypeINS" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 190px;">
                                <asp:TextBox ID="txtAccountingNameByAdd" runat="server" Text='<%# Bind("AccountingName") %>'
                                    Width="170px" CssClass="InputText" ValidationGroup="ExpenseManageTypeINS" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 120px;">
                                <asp:DropDownList ID="ddlExpenseMangeCategory" runat="server" SelectedValue='<%# Bind("ExpenseManageCategoryID") %>'>
                                    <asp:ListItem Text="个人费用报销" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="出差费用报销" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFExpenseManageTypeName" runat="server" ControlToValidate="txtExpenseManageTypeNameByAdd"
                                    Display="None" ErrorMessage="请您输入管理费用类别名称！" SetFocusOnError="True" ValidationGroup="ExpenseManageTypeAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFAccountingCode" runat="server" ControlToValidate="txtAccountingCodeByAdd"
                                    Display="None" ErrorMessage="请您输入科目编号！" SetFocusOnError="True" ValidationGroup="ExpenseManageTypeAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFAccountingName" runat="server" ControlToValidate="txtAccountingNameByAdd"
                                    Display="None" ErrorMessage="请您输入科目名称！" SetFocusOnError="True" ValidationGroup="ExpenseManageTypeAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsExpenseManageTypeAdd" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="ExpenseManageTypeAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="ExpenseManageTypeAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ExpenseManageTypeInsertValidationSummary" ValidationGroup="ExpenseManageTypeINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsExpenseManageType" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteExpenseManageType" InsertMethod="InsertExpenseManageType"
                SelectMethod="GetExpenseManageTypePaged" SelectCountMethod="ExpenseManageTypeTotalCount"
                SortParameterName="sortExpression" UpdateMethod="UpdateExpenseManageType" OnDeleting="odsExpenseManageType_Deleting"
                OnInserting="odsExpenseManageType_Inserting" OnUpdating="odsExpenseManageType_Updating"
                EnablePaging="true">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                    <asp:Parameter Name="ExpenseManageTypeName" Type="String" />
                    <asp:Parameter Name="AccountingCode" Type="String" />
                    <asp:Parameter Name="AccountingName" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="ExpenseManageTypeName" Type="String" />
                    <asp:Parameter Name="AccountingCode" Type="String" />
                    <asp:Parameter Name="AccountingName" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
