<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="CostCenter"  Codebehind="CostCenter.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width:1260px;">成本中心维护</div>
    <asp:UpdatePanel ID="upCostCenter" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCostCenter" CssClass="GridView" runat="server" DataSourceID="odsCostCenter"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="CostCenterID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="CostCenterName" HeaderText="成本中心名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCostCenterName" runat="server" Text='<%# Bind("CostCenterName") %>'
                                Width="370px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCostCenterName" runat="server" Text='<%# Bind("CostCenterName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="396px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CostCenterCode" HeaderText="成本中心标记">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCostCenterCode" runat="server" Text='<%# Bind("CostCenterCode") %>'
                                Width="730px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCostCenterCode" runat="server" Text='<%# Bind("CostCenterCode") %>'></asp:Label>
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
                            <asp:RequiredFieldValidator ID="RFCostCenterName" runat="server" ControlToValidate="txtCostCenterName"
                                Display="None" ErrorMessage="请您输入成本中心名称！" SetFocusOnError="True" ValidationGroup="CostCenterEdit"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFCostCenterCode" runat="server" ControlToValidate="txtCostCenterCode"
                                Display="None" ErrorMessage="请您输入成本中心说明！" SetFocusOnError="True" ValidationGroup="CostCenterEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsCostCenterEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="CostCenterEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="CostCenterEdit" CommandName="Update" Text="更新"></asp:LinkButton>
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
                                成本中心名称
                            </td>
                            <td width="750px">
                                成本中心说明
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
            <asp:FormView ID="fvCostCenter" runat="server" DataKeyNames="CostCenterID" DataSourceID="odsCostCenter"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 396px;">
                                <asp:TextBox ID="txtCostCenterNameByAdd" runat="server" Text='<%# Bind("CostCenterName") %>'
                                    Width="370px" CssClass="InputText" ValidationGroup="CostCenterINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 750px;">
                                <asp:TextBox ID="txtCostCenterCodeByAdd" runat="server" Text='<%# Bind("CostCenterCode") %>'
                                    Width="730px" CssClass="InputText" ValidationGroup="CostCenterINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFCostCenterName" runat="server" ControlToValidate="txtCostCenterNameByAdd"
                                    Display="None" ErrorMessage="请您输入成本中心名称！" SetFocusOnError="True" ValidationGroup="CostCenterAdd"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RFCostCenterCode" runat="server" ControlToValidate="txtCostCenterCodeByAdd"
                                Display="None" ErrorMessage="请您输入成本中心说明！" SetFocusOnError="True" ValidationGroup="CostCenterAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsCostCenterAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="CostCenterAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="CostCenterAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="CostCenterInsertValidationSummary" ValidationGroup="CostCenterINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsCostCenter" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteCostCenter" InsertMethod="InsertCostCenter" SelectMethod="GetCostCenterPaged" SelectCountMethod="CostCenterTotalCount"
                SortParameterName="sortExpression" UpdateMethod="UpdateCostCenter" OnDeleting="odsCostCenter_Deleting"
                OnInserting="odsCostCenter_Inserting" OnUpdating="odsCostCenter_Updating" EnablePaging="true">
                <DeleteParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                    <asp:Parameter Name="CostCenterName" Type="String" />
                    <asp:Parameter Name="CostCenterCode" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CostCenterName" Type="String" />
                    <asp:Parameter Name="CostCenterCode" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
