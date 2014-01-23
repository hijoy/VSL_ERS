<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ProxyReimburse" Codebehind="ProxyReimburse.aspx.cs" %>

<%@ Register Src="~/UserControls/StaffControl.ascx" TagName="UCStaff" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style=" width:400px">
                    <span class="field_title">被代理人:</span>&nbsp;&nbsp;
                    <uc1:UCStaff ID="UCUser" runat="server" Width="150px" />                    
                </td>
                <td style=" width:400px">
                    <span class="field_title">代理人:</span>&nbsp;&nbsp;
                    <uc1:UCStaff ID="UCProxyUser" runat="server" Width="150px" />                    
                </td>
                <td style="width: 460px; vertical-align: middle;" align="center" >
                    <asp:Button runat="server" ID="lbtnSearch" Text="搜 索" CssClass="button_nor" OnClick="lbtnSearch_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1260px;">
        方案报销代理信息</div>
    <asp:UpdatePanel ID="upProxyReimburse" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvProxyReimburse" CssClass="GridView" runat="server" DataSourceID="odsProxyReimburse"
                AutoGenerateColumns="False" DataKeyNames="ID" AllowPaging="True" AllowSorting="True" PageSize="20" EnableModelValidation="True" >
                <Columns>
                    <asp:TemplateField SortExpression="UserID" HeaderText="被代理人">
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" runat="server" Text='<%# GetUserNameByID(Eval("UserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ProxyUserID" HeaderText="代理人">
                        <ItemTemplate>
                            <asp:Label ID="lblProxyUserName" runat="server" Text='<%# GetUserNameByID(Eval("ProxyUserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EndDate" HeaderText="交接日期" >
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate","{0:yyyy-MM-dd}")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false"
                                CommandName="Delete" Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="160px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 400px;" class="Empty1">
                                被代理人
                            </td>
                            <td align="center" style="width: 400px;">
                                代理人
                            </td>
                            <td align="center" style="width: 300px;">
                                交接日期
                            </td>
                            <td align="center" style="width: 160px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvProxyReimburse" runat="server" DataKeyNames="ID" DataSourceID="odsProxyReimburse"
                DefaultMode="Insert"  CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr class="Header">
                            <td align="center" style="height: 22px; width: 400px;">
                                <uc1:UCStaff ID="NewUCUser" runat="server" Width="150px" StaffID='<%# Bind("UserID") %>' />                    
                            </td>
                            <td align="center" style="height: 22px; width: 400px;">
                                <uc1:UCStaff ID="NewUCProxyUser" runat="server" Width="150px" StaffID='<%# Bind("ProxyUserID") %>' />  
                            </td>
                            <td align="center" style="height: 22px; width: 300px;">
                                <uc2:UCDateInput ID="NewUCEndDate" runat="server" />
                            </td>
                            <td align="center" style="width: 160px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="NewRow"></asp:LinkButton>
                            </td>
                        </tr>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewUCEndDate$txtDate"
                                    ErrorMessage="请选择截止日期！" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NewUCUser$txtStaffName"
                                    ErrorMessage="请选择被代理人！" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="newUCProxyUser$txtStaffName"
                                    ErrorMessage="请选择代理人！" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsProxyReimburseAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="NewRow" />
                        <tr>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsProxyReimburse" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="ProxyReimburseTotalCount"
                DeleteMethod="DeleteProxyReimburseByID" InsertMethod="InsertProxyReimburse" SelectMethod="GetProxyReimbursePaged" OnInserting="odsProxyReimburse_Inserting">
                <DeleteParameters>
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="ProxyUserID" Type="Int32" />
                    <asp:Parameter Name="EndDate" Type="Int32" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />

</asp:Content>
