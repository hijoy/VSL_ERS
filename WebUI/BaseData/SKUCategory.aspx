<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SKUCategory" Codebehind="SKUCategory.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        产品类型维护
    </div>
    <asp:UpdatePanel ID="upSKUCategory" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvSKUCategory" CssClass="GridView" runat="server" DataSourceID="odsSKUCategory"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="SKUCategoryID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="SKUCategoryName" HeaderText="产品类型名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSKUCategoryName" runat="server" Text='<%# Bind("SKUCategoryName") %>'
                                Width="1120px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSKUCategoryName" runat="server" Text='<%# Bind("SKUCategoryName") %>'></asp:Label>
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
                            <asp:RequiredFieldValidator ID="RFchanTypeName" runat="server" ControlToValidate="txtSKUCategoryName"
                                Display="None" ErrorMessage="请您输入产品类型名称！" SetFocusOnError="True" ValidationGroup="chanTypeEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vschanTypeEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="chanTypeEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="chanTypeEdit" CommandName="Update" Text="更新"></asp:LinkButton>
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
                        <tr class="Header" style="height: 22px;">
                            <td style="width: 1147px;" class="Empty1">
                                产品类型名称
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
            <asp:FormView ID="fvSKUCategory" runat="server" DataKeyNames="SKUCategoryID" DataSourceID="odsSKUCategory"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 1147px;">
                                <asp:TextBox ID="txtSKUCategoryNameByAdd" runat="server" Text='<%# Bind("SKUCategoryName") %>'
                                    Width="1120px" CssClass="InputText" ValidationGroup="chanTypeINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFChanTypeName" runat="server" ControlToValidate="txtSKUCategoryNameByAdd"
                                    Display="None" ErrorMessage="请您输入产品类型名称！" SetFocusOnError="True" ValidationGroup="chanTypeAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vschanTypeAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="chanTypeAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="chanTypeAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="chanTypeInsertValidationSummary" ValidationGroup="chanTypeINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsSKUCategory" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteSKUCategory" InsertMethod="InsertSKUCategory" SelectMethod="GetSKUCategoryPaged"
                SelectCountMethod="SKUCategoryTotalCount" SortParameterName="sortExpression"
                UpdateMethod="UpdateSKUCategory" OnDeleting="odsSKUCategory_Deleting" OnInserting="odsSKUCategory_Inserting"
                OnUpdating="odsSKUCategory_Updating" EnablePaging="true">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                    <asp:Parameter Name="SKUCategoryName" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="SKUCategoryName" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
