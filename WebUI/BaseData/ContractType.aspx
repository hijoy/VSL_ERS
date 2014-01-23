<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ContractType"  Codebehind="ContractType.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width:1260px;">合同类型维护</div>
    <asp:UpdatePanel ID="upContractType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvContractType" CssClass="GridView" runat="server" DataSourceID="odsContractType"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="ContractTypeID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ContractTypeName" HeaderText="合同类型名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtContractTypeName" runat="server" Text='<%# Bind("ContractTypeName") %>'
                                Width="370px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblContractTypeName" runat="server" Text='<%# Bind("ContractTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="396px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ContractTypeMark" HeaderText="合同类型标记">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtContractTypeMark" runat="server" Text='<%# Bind("ContractTypeMark") %>'
                                Width="730px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblContractTypeMark" runat="server" Text='<%# Bind("ContractTypeMark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="750px" HorizontalAlign="Center" />
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
                            <asp:RequiredFieldValidator ID="RFcontTypeName" runat="server" ControlToValidate="txtContractTypeName"
                                Display="None" ErrorMessage="请您输入合同类型名称！" SetFocusOnError="True" ValidationGroup="contTypeEdit"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFcontTypeMark" runat="server" ControlToValidate="txtContractTypeMark"
                                Display="None" ErrorMessage="请您输入合同类型说明！" SetFocusOnError="True" ValidationGroup="contTypeEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vscontTypeEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="contTypeEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="contTypeEdit" CommandName="Update" Text="更新"></asp:LinkButton>
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
                            <td width="396px" class="Empty1">
                                合同类型名称
                            </td>
                            <td width="750px">
                                合同类型说明
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
            <asp:FormView ID="fvContractType" runat="server" DataKeyNames="ContractTypeID" DataSourceID="odsContractType"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 396px;">
                                <asp:TextBox ID="txtContractTypeNameByAdd" runat="server" Text='<%# Bind("ContractTypeName") %>'
                                    Width="370px" CssClass="InputText" ValidationGroup="contTypeINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 750px;">
                                <asp:TextBox ID="txtContractTypeMarkByAdd" runat="server" Text='<%# Bind("ContractTypeMark") %>'
                                    Width="730px" CssClass="InputText" ValidationGroup="contTypeINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFcontTypeName" runat="server" ControlToValidate="txtContractTypeNameByAdd"
                                    Display="None" ErrorMessage="请您输入合同类型名称！" SetFocusOnError="True" ValidationGroup="contTypeAdd"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RFcontTypeMark" runat="server" ControlToValidate="txtContractTypeMarkByAdd"
                                Display="None" ErrorMessage="请您输入合同类型说明！" SetFocusOnError="True" ValidationGroup="contTypeAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vscontTypeAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="contTypeAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="contTypeAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="contTypeInsertValidationSummary" ValidationGroup="contTypeINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsContractType" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteContractType" InsertMethod="InsertContractType" SelectMethod="GetContractTypePaged" SelectCountMethod="ContractTypeTotalCount"
                SortParameterName="sortExpression" UpdateMethod="UpdateContractType" OnDeleting="odsContractType_Deleting"
                OnInserting="odsContractType_Inserting" OnUpdating="odsContractType_Updating" EnablePaging="true">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                    <asp:Parameter Name="ContractTypeName" Type="String" />
                    <asp:Parameter Name="ContractTypeMark" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="ContractTypeName" Type="String" />
                    <asp:Parameter Name="ContractTypeMark" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
