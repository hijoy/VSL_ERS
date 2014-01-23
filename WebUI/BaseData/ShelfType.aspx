<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ShelfType" Codebehind="ShelfType.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        陈列形式维护
    </div>
    <asp:UpdatePanel ID="upShelfType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvShelfType" CssClass="GridView" runat="server" DataSourceID="odsShelfType"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="ShelfTypeID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ShelfTypeName" HeaderText="陈列形式名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtShelfTypeName" runat="server" Text='<%# Bind("ShelfTypeName") %>'
                                Width="1120px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="1147px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblShelfTypeName" runat="server" Text='<%# Bind("ShelfTypeName") %>'></asp:Label>
                        </ItemTemplate>
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
                            <asp:RequiredFieldValidator ID="RFchanTypeName" runat="server" ControlToValidate="txtShelfTypeName"
                                Display="None" ErrorMessage="请您输入陈列形式名称！" SetFocusOnError="True" ValidationGroup="chanTypeEdit"></asp:RequiredFieldValidator>
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
                                陈列形式名称
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
            <asp:FormView ID="fvShelfType" runat="server" DataKeyNames="ShelfTypeID" DataSourceID="odsShelfType"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1147px;">
                                <asp:TextBox ID="txtShelfTypeNameByAdd" runat="server" Text='<%# Bind("ShelfTypeName") %>'
                                    Width="1120px" CssClass="InputText" ValidationGroup="chanTypeINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFChanTypeName" runat="server" ControlToValidate="txtShelfTypeNameByAdd"
                                    Display="None" ErrorMessage="请您输入陈列形式名称！" SetFocusOnError="True" ValidationGroup="chanTypeAdd"></asp:RequiredFieldValidator>
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
            <asp:ObjectDataSource ID="odsShelfType" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteShelfType" InsertMethod="InsertShelfType" SelectMethod="GetShelfTypePaged"
                SelectCountMethod="ShelfTypeTotalCount" SortParameterName="sortExpression" UpdateMethod="UpdateShelfType"
                OnDeleting="odsShelfType_Deleting" OnInserting="odsShelfType_Inserting" OnUpdating="odsShelfType_Updating"
                EnablePaging="true">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                    <asp:Parameter Name="ShelfTypeName" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="ShelfTypeName" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
