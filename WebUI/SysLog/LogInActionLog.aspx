<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SysLog_LogInActionLog" Title="用户登录日志" Codebehind="LogInActionLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="SearchUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="background-color: #EEEEEE; border-style: solid; border-width: 1px; width: 815px;">
                <table style="width: 815px;">
                    <tr>
                        <td style="width: 25%;">
                            员工工号</td>
                        <td style="width: 25%;">
                            员工姓名</td>
                        <td style="width: 25%;">
                            IP地址</td>
                        <td style="width: 25%;">
                            是否成功</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="SrhStuffIdCtl" runat="server" Width="100px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="SrhStuffNameCtl" runat="server" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="SrhClientIPCtl" runat="server" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:DropDownList ID="LogActoinList" runat="server" Width="150px">
                                <asp:ListItem>全部</asp:ListItem>
                                <asp:ListItem>成功</asp:ListItem>
                                <asp:ListItem>失败</asp:ListItem>
                            </asp:DropDownList>                        
                        </td>
                    </tr>

                    <tr>
                        <td>
                            时间范围:</td>
                        <td style="width:150px">
                            从 <asp:TextBox ID="SrhAfterLogInTimeCtl" runat="server"></asp:TextBox>
                        </td>
                        <td style="width:150px">
                            到 <asp:TextBox ID="SrhBeforeLogInTimeCtl" runat="server"></asp:TextBox></td>
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
            <gc:GridView ID="LogInActionLogGridView" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="LogInActionLogId" DataSourceID="LogInActionLogDS"
                CssClass="GridView" Width="815px">
                <Columns>
                    <asp:BoundField DataField="LogInActionLogId" HeaderText="LogInActionLogId" InsertVisible="False"
                        ReadOnly="True" Visible="false" SortExpression="LogInActionLogId" />
                    <asp:BoundField DataField="StuffId" HeaderText="工号" SortExpression="StuffId" />
                    <asp:BoundField DataField="StuffName" HeaderText="员工姓名" SortExpression="StuffName" />
                    <asp:BoundField DataField="UserName" HeaderText="登录帐号" SortExpression="UserName" />
                    <asp:CheckBoxField DataField="Success" HeaderText="登录成功" SortExpression="Success" />
                    <asp:BoundField DataField="LogInTime" HeaderText="登录时间" SortExpression="LogInTime" />
                    <asp:BoundField DataField="ClientIP" HeaderText="客户机IP" SortExpression="ClientIP" />
                </Columns>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource EnablePaging="true" ID="LogInActionLogDS" runat="server" SelectCountMethod="TotalCount"
        SelectMethod="GetLogInActionLog" TypeName="BusinessObjects.LogBLL" SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
