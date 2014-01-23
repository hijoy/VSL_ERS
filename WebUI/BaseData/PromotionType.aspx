<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="PromotionType" Codebehind="PromotionType.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        ������ʽά��
    </div>
    <asp:UpdatePanel ID="upPromotionType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvPromotionType" CssClass="GridView" runat="server" DataSourceID="odsPromotionType"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="PromotionTypeID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="PromotionTypeName" HeaderText="������ʽ����">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPromotionTypeName" runat="server" Text='<%# Bind("PromotionTypeName") %>'
                                Width="1120px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPromotionTypeName" runat="server" Text='<%# Bind("PromotionTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="1147px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="����">
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
                            <asp:RequiredFieldValidator ID="RFchanTypeName" runat="server" ControlToValidate="txtPromotionTypeName"
                                Display="None" ErrorMessage="�������������ʽ���ƣ�" SetFocusOnError="True" ValidationGroup="chanTypeEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vschanTypeEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="chanTypeEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="chanTypeEdit" CommandName="Update" Text="����"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text="ȡ��"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text="�༭"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header" style="height: 22px;">
                            <td style="width: 1147px;" class="Empty1">
                                ������ʽ����
                            </td>
                            <td style="width: 60px;">
                                ����
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvPromotionType" runat="server" DataKeyNames="PromotionTypeID"
                DataSourceID="odsPromotionType" DefaultMode="Insert" Visible="<%# HasManageRight %>"
                EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 1147px;">
                                <asp:TextBox ID="txtPromotionTypeNameByAdd" runat="server" Text='<%# Bind("PromotionTypeName") %>'
                                    Width="1120px" CssClass="InputText" ValidationGroup="chanTypeINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFChanTypeName" runat="server" ControlToValidate="txtPromotionTypeNameByAdd"
                                    Display="None" ErrorMessage="�������������ʽ���ƣ�" SetFocusOnError="True" ValidationGroup="chanTypeAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vschanTypeAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="chanTypeAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="����" ValidationGroup="chanTypeAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="chanTypeInsertValidationSummary" ValidationGroup="chanTypeINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsPromotionType" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeletePromotionType" InsertMethod="InsertPromotionType" SelectMethod="GetPromotionTypePaged"
                SelectCountMethod="PromotionTypeTotalCount" SortParameterName="sortExpression"
                UpdateMethod="UpdatePromotionType" OnDeleting="odsPromotionType_Deleting" OnInserting="odsPromotionType_Inserting"
                OnUpdating="odsPromotionType_Updating" EnablePaging="true">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                    <asp:Parameter Name="PromotionTypeName" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="PromotionTypeName" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
