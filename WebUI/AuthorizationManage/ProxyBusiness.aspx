<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="AuthorizationManage_ProxyBusiness" Culture="Auto" UICulture="Auto" Codebehind="ProxyBusiness.aspx.cs" %>

<%@ Register Src="~/UserControls/StaffControl.ascx" TagName="UCStaff" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1240px;">
        代理填单信息</div>
    <asp:UpdatePanel ID="upProxyBusiness" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvProxyBusiness" CssClass="GridView" runat="server" DataSourceID="odsProxyBusiness"
                AutoGenerateColumns="False" DataKeyNames="ProxyBusinessID" AllowPaging="false" EnableModelValidation="True" >
                <Columns>
                    <asp:TemplateField HeaderText="我的职务" >
                        <ItemTemplate>
                            <asp:Label ID="lblPositionName" runat="server" Text='<%# GetPositionNameByID(Eval("PositionID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="代理人">
                        <ItemTemplate>
                            <asp:Label ID="lblProxyUserName" runat="server" Text='<%# GetUserNameByID(Eval("ProxyUserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="BeginDate" HeaderText="开始日期">
                        <ItemTemplate>
                            <asp:Label ID="lblBeginDate" runat="server" Text='<%# Eval("BeginDate","{0:yyyy-MM-dd}")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EndDate" HeaderText="截止日期">
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate","{0:yyyy-MM-dd}")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false"
                                CommandName="Delete" Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="140px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 300px;" class="Empty1">
                                我的职务
                            </td>
                            <td align="center" style="width: 300px;">
                                代理人
                            </td>
                            <td align="center" style="width: 250px;">
                                开始日期
                            </td>
                            <td align="center" style="width: 250px;">
                                截止日期
                            </td>
                            <td align="center" style="width: 140px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvProxyBusiness" runat="server" DataKeyNames="ProxyBusinessID" DataSourceID="odsProxyBusiness"
                DefaultMode="Insert"  CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr class="Header">
                            <td align="center" style="height: 22px; width: 300px;">
                                              
                            </td>
                            <td align="center" style="height: 22px; width: 300px;">
                                <uc1:UCStaff ID="NewUCProxyUser" runat="server" Width="150px" StaffID='<%# Bind("ProxyUserID") %>' />  
                            </td>
                            <td align="center" style="height: 22px; width: 250px;">
                                <uc2:UCDateInput ID="NewUCBeginDate" runat="server" SelectedDate='<%# Bind("BeginDate") %>' />
                            </td>
                            <td align="center" style="height: 22px; width: 250px;">
                                <uc2:UCDateInput ID="NewUCEndDate" runat="server" SelectedDate='<%# Bind("EndDate") %>' />
                            </td>
                            <td align="center" style="width: 140px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="NewRow"></asp:LinkButton>
                            </td>
                        </tr>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="NewUCBeginDate$txtDate"
                                    meta:resourcekey="RequiredFieldValidator_BeginDate" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewUCEndDate$txtDate"
                                    meta:resourcekey="RequiredFieldValidator_EndDate" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="newUCProxyUser$txtStaffName"
                                    meta:resourcekey="RequiredFieldValidator_ProxyUserName" Display="None" ValidationGroup="NewRow"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsProxyBusinessAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="NewRow" />
                        <tr>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsProxyBusiness" runat="server" TypeName="BusinessObjects.AuthorizationBLL"
                EnablePaging="false" DeleteMethod="DeleteProxyBusinessByID" InsertMethod="AddProxyBusiness" SelectMethod="GetProxyBusinessByUserID" 
                OnInserting="odsProxyBusiness_Inserting" OnInserted="odsProxyBusiness_Inserted">
                <DeleteParameters>
                    <asp:Parameter Name="ProxyBusinessID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                    <asp:Parameter Name="PositionID" Type="Int32" />
                    <asp:Parameter Name="ProxyUserID" Type="Int32" />
                    <asp:Parameter Name="BeginDate" Type="Datetime" />
                    <asp:Parameter Name="EndDate" Type="Datetime" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="UserID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />

</asp:Content>
