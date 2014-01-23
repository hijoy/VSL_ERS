<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DialogMasterPage.master"
    Inherits="Dialog_StaffSearch" Codebehind="StaffSearch.aspx.cs" %>

<%@ Implements Interface="System.Web.UI.IPostBackEventHandler" %>
<%@ Register Src="../UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript">
        var _oldColor;
        function SetNewColor(source) {
            _oldColor = source.style.backgroundColor;
            source.style.backgroundColor = '#C0C0C0';
        }

        function SetOldColor(source) {
            source.style.backgroundColor = _oldColor;
        }
    </script>
    <div class="title1" style="width: 842px;">搜索条件</div>

    <table style="background-color: #F6F6F6; vertical-align: top; width: 842px;">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        登陆帐号</div>
                    <asp:TextBox ID="UserAccountTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        用户名</div>
                    <asp:TextBox ID="StuffNameTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        员工工号</div>
                    <asp:TextBox ID="EmployeeNoTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 50px;">
                    &nbsp;
                </td>
                <td colspan="2" align="left" valign="middle">
                    <input type="hidden" id="btnclicked" name="btnclicked" value="0" />
                    <asp:Button ID="SearchButton" runat="server" CssClass="button_nor" Text="查询" OnClick="SearchButton_Click" />&nbsp;&nbsp;
                </td>
            </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1" style="width: 842px;">员工</div>
    <gc:GridView ID="gvStaff" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="StuffUserId" DataSourceID="odsStuffUser"
        PageSize="20" ShowFooter="True" CssClass="GridView" OnRowDataBound="gvStaff_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="登陆帐号" SortExpression="UserName">
                <ItemTemplate>
                    <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="员工姓名" SortExpression="StuffName">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="职务">
                <ItemTemplate>
                    <asp:Label ID="PositionsLabel" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="300px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="员工工号" SortExpression="StuffId">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StuffId") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="入职日期" SortExpression="AttendDate">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("AttendDate","{0:yyyy-MM-dd}" ) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="激活" SortExpression="IsActive">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <th style="width: 150px;" class="Empty1">
                        登陆帐号
                    </th>
                    <th style="width: 100px;">
                        员工姓名
                    </th>
                    <th style="width: 300px;">
                        职务
                    </th>
                    <th style="width: 100px;">
                        员工工号
                    </th>
                    <th style="width: 100px;">
                        入职日期
                    </th>
                    <th style="width: 100px;">
                        激活
                    </th>
                </tr>
                <tr><td colspan="6" class="Empty2 noneLabel">无</td></tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
        <HeaderStyle CssClass="Header" />
        <FooterStyle CssClass="Footer" />
    </gc:GridView>

    <asp:ObjectDataSource ID="odsStuffUser" runat="server" TypeName="BusinessObjects.StuffUserBLL"
        OldValuesParameterFormatString="{0}" SelectMethod="GetStuffUserPaged"
        SelectCountMethod="TotalCount" SortParameterName="sortExpression" EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
