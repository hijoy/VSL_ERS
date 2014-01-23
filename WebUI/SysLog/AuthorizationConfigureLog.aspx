<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SysLog_AuthorizationConfigureLog"
    Title="权限设置日志" Codebehind="AuthorizationConfigureLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="SearchUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="background-color: #EEEEEE; border-style: solid; border-width: 1px; width: 815px;">
                <table style="width: 815px;">
                    <tr>
                        <td style="width: 25%;">
                            设置对象</td>
                        <td style="width: 25%;">
                            操作类型</td>
                        <td style="width: 25%;">
                            员工工号</td>
                        <td style="width: 25%;">
                            员工姓名</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ObjectDropDownList" runat="server" Width="150px">
                                <asp:ListItem>全部</asp:ListItem>
                                <asp:ListItem>系统角色设置</asp:ListItem>
                                <asp:ListItem>业务操作范围</asp:ListItem>
                                <asp:ListItem>职务权限设置</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="ActionTypeDropDownList" runat="server" Width="150px">
                                <asp:ListItem>全部</asp:ListItem>
                                <asp:ListItem>添加</asp:ListItem>
                                <asp:ListItem>更新</asp:ListItem>
                                <asp:ListItem>删除</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="StuffIdCtl" runat="server" Width="150px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="StuffNameCtl" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>                    

                        <td>
                            时间范围:</td>
                        <td style="width:150px">
                            从 <asp:TextBox ID="SrhAfterLogInTimeCtl" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td style="width:160px">
                            到 <asp:TextBox ID="SrhBeforeLogInTimeCtl" runat="server" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="SearchBtn" CssClass="button_nor" runat="server" Text="查询" OnClick="SearchBtn_Click" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
     <br />
    <asp:UpdatePanel ID="GridViewUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
     
        <gc:GridView ID="RightLogGridView" CssClass="GridView" Width="815px" runat="server" DataSourceID="RightLogDS"
            AutoGenerateColumns="False" DataKeyNames="AuthorizationConfigureLogId" AllowPaging="True"
            AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="AuthorizationConfigureLogId" HeaderText="AuthorizationConfigureLogId"
                    InsertVisible="False" Visible="False" ReadOnly="True" SortExpression="AuthorizationConfigureLogId" />
                <asp:TemplateField HeaderText="设置对象" SortExpression="ConfigureTarget">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ConfigureTarget") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="新增">
                    <ItemStyle Width="165px" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("NewContent") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="撤销">
                    <ItemStyle Width="150px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("OldContent") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="修改类型" SortExpression="ConfigureType">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("ConfigureType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="修改时间" SortExpression="ConfigureTime">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ConfigureTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="员工工号" SortExpression="StuffId">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("StuffId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="员工姓名" SortExpression="StuffName">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="Header" />
        </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
        
    <asp:ObjectDataSource ID="RightLogDS" runat="server" EnablePaging="True" SelectCountMethod="AuthorizationConfigureLogTotalCount"
        SelectMethod="GetAuthorizationConfigureLog" SortParameterName="sortExpression"
        TypeName="BusinessObjects.LogBLL">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
