<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="RejectReason" Codebehind="RejectReason.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" style="width: 1260px;">
        审批拒绝原因</div>
    <asp:UpdatePanel ID="RejectReasonUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="RejectReasonGridView" CssClass="GridView" runat="server" DataSourceID="RejectReasonObjectDataSource"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="RejectReasonId"
                OnDataBound="RejectReasonGridView_DataBound" PageSize="20" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="序号" SortExpression="RejectReasonIndex">
                        <EditItemTemplate>
                            <asp:TextBox ID="RejectReasonIndexTextBox" runat="server" CssClass="InputText" Text='<%# Bind("RejectReasonIndex") %>'
                                Width="100px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RejectReasonIndexRequiredFieldValidator" runat="server"
                                ControlToValidate="RejectReasonIndexTextBox" Display="None" ErrorMessage="请您输入序号！"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryEDIT" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="EDIT" />
                        </EditItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="RejectReasonIndexLabel" runat="server" Text='<%# Bind("RejectReasonIndex") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="原因简称" SortExpression="RejectReasonTitle">
                        <EditItemTemplate>
                            <asp:TextBox ID="RejectReasonTitleTextBox" runat="server" CssClass="InputText" MaxLength="100"
                                Text='<%# Bind("RejectReasonTitle") %>' Width="330px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RejectReasonTitleRequiredFieldValidator" runat="server"
                                ControlToValidate="RejectReasonTitleTextBox" Display="None" ErrorMessage="请您输入原因简称！"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="350px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="RejectReasonTitleLabel" runat="server" Text='<%# Bind("RejectReasonTitle") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="审批拒绝原因" SortExpression="RejectReasonContent">
                        <EditItemTemplate>
                            <asp:TextBox ID="RejectReasonContentTextBox" runat="server" Text='<%# Bind("RejectReasonContent") %>'
                                CssClass="InputText" Width="670px" MaxLength="500"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RejectReasonContentRequiredFieldValidator" runat="server"
                                ControlToValidate="RejectReasonContentTextBox" Display="None" ErrorMessage="请您输入拒绝原因！"
                                SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <ItemStyle Width="695px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="RejectReasonContentLabel" runat="server" Text='<%# Bind("RejectReasonContent") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="激活">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
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
                    <table class="GridView">
                        <tr class="Header">
                            <td scope="col" style="width: 120px">
                                拒绝原因ID
                            </td>
                            <td scope="col" style="width: 350px">
                                原因简称
                            </td>
                            <td scope="col" style="width: 695px">
                                审批拒绝原因
                            </td>
                            <td scope="col" style="width: 40px">
                                激活
                            </td>
                            <td scope="col" style="width: 50px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="RejectReasonFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="RejectReasonAddUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="RejectReasonFormView" runat="server" DataKeyNames="RejectReasonId"
                DataSourceID="RejectReasonObjectDataSource" DefaultMode="Insert" Visible="<%# HasManageRight %>">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 120px">
                                <asp:TextBox ID="RejectReasonIndexTextBox" runat="server" Text='<%# Bind("RejectReasonIndex") %>'
                                    CssClass="InputText" Width="100px" ValidationGroup="INS"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 350px">
                                <asp:TextBox ID="RejectReasonTitleTextBox" runat="server" Text='<%# Bind("RejectReasonTitle") %>'
                                    CssClass="InputText" Width="330px" ValidationGroup="INS" MaxLength="100"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 695px">
                                <asp:TextBox ID="RejectReasonContentTextBox" runat="server" Text='<%# Bind("RejectReasonContent") %>'
                                    CssClass="InputText" Width="670px" ValidationGroup="INS" MaxLength="500"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 40px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="RejectReasonIndexRequiredFieldValidator" runat="server"
                        ControlToValidate="RejectReasonIndexTextBox" Display="None" ErrorMessage="请您输入序号！"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RejectReasonTitleRequiredFieldValidator" runat="server"
                        ControlToValidate="RejectReasonTitleTextBox" Display="None" ErrorMessage="请您输入原因简称！"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RejectReasonContentRequiredFieldValidator" runat="server"
                        ControlToValidate="RejectReasonContentTextBox" Display="None" ErrorMessage="请您输入拒绝原因！"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="RejectReasonObjectDataSource" runat="server" DeleteMethod="DeleteById"
        InsertMethod="InsertRejectReason" OldValuesParameterFormatString="{0}" SelectMethod="GetRejectReasonPaged"
        TypeName="BusinessObjects.MasterDataBLL" EnablePaging="true" UpdateMethod="UpdateRejectReason"
        SelectCountMethod="QueryTotalCount" SortParameterName="sortExpression">
        <DeleteParameters>
            <asp:Parameter Name="RejectReasonId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="RejectReasonId" Type="Int32" />
            <asp:Parameter Name="RejectReasonIndex" Type="Int32" />
            <asp:Parameter Name="RejectReasonTitle" Type="String" />
            <asp:Parameter Name="RejectReasonContent" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="RejectReasonIndex" Type="Int32" />
            <asp:Parameter Name="RejectReasonTitle" Type="String" />
            <asp:Parameter Name="RejectReasonContent" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
