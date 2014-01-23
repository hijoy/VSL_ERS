<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CustomerType" Codebehind="CustomerType.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width:1260px;">
        客户类型维护</div>
    <asp:UpdatePanel ID="upCustomerType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCustomerType" CssClass="GridView" runat="server" DataSourceID="odsCustomerType"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="CustomerTypeID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="CustomerTypeName" HeaderText="客户类型名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCustomerTypeName" runat="server" Text='<%# Bind("CustomerTypeName") %>'
                                Width="1120px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerTypeName" runat="server" Text='<%# Bind("CustomerTypeName") %>'></asp:Label>
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
                            <asp:RequiredFieldValidator ID="RFchanTypeName" runat="server" ControlToValidate="txtCustomerTypeName"
                                Display="None" ErrorMessage="请您输入客户类型名称！" SetFocusOnError="True" ValidationGroup="chanTypeEdit"></asp:RequiredFieldValidator>
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
                                客户类型名称
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
            <asp:FormView ID="fvCustomerType" runat="server" DataKeyNames="CustomerTypeID" DataSourceID="odsCustomerType"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1147px;">
                                <asp:TextBox ID="txtCustomerTypeNameByAdd" runat="server" Text='<%# Bind("CustomerTypeName") %>'
                                    Width="1120px" CssClass="InputText" ValidationGroup="chanTypeINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFChanTypeName" runat="server" ControlToValidate="txtCustomerTypeNameByAdd"
                                    Display="None" ErrorMessage="请您输入客户类型名称！" SetFocusOnError="True" ValidationGroup="chanTypeAdd"></asp:RequiredFieldValidator>
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
            <asp:ObjectDataSource ID="odsCustomerType" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteCustomerType" InsertMethod="InsertCustomerType" SelectMethod="GetCustomerTypePaged"
                SelectCountMethod="CustomerTypeTotalCount" SortParameterName="sortExpression"
                UpdateMethod="UpdateCustomerType" OnDeleting="odsCustomerType_Deleting" OnInserting="odsCustomerType_Inserting"
                OnUpdating="odsCustomerType_Updating" EnablePaging="true">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                    <asp:Parameter Name="CustomerTypeName" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CustomerTypeName" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
