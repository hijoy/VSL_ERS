<%@ Page Language="C#" AutoEventWireup="true" Inherits="PositionManagePage"
    MasterPageFile="~/MasterPage.master" Codebehind="PositionManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" style="width: 1260px;">
        查询条件
    </div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        登陆帐号</div>
                    <asp:TextBox ID="UserAccountTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        用户名</div>
                    <asp:TextBox ID="StuffNameTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        员工工号</div>
                    <asp:TextBox ID="EmployeeNoTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        电子邮件</div>
                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        电话</div>
                    <asp:TextBox ID="TelTextBox" runat="server" CssClass="InputText" Width="180px"></asp:TextBox>
                </td>
                <td style="width: 120px">
                    &nbsp;
                </td>
                <td style="width: 150px;" valign="bottom">
                    <asp:Button runat="server" ID="SearchButton" Text="搜 索" CssClass="button_nor" OnClick="SearchButton_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1260px;">
        员工信息
    </div>
    <gc:GridView ID="StuffUserGridView" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" DataKeyNames="StuffUserId" DataSourceID="StuffUserObjectDataSource"
        PageSize="20" ShowFooter="false" CssClass="GridView" OnSelectedIndexChanged="StuffUserGridView_SelectedIndexChanged"
        OnRowDataBound="StuffUserGridView_RowDataBound" CellPadding="0">
        <Columns>
            <asp:TemplateField HeaderText="员工姓名" SortExpression="StuffName">
                <ItemStyle Width="167px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="StuffNamelbl" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="职务">
                <ItemTemplate>
                    <asp:Label ID="PositionsLabel" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="300px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="登陆帐号" SortExpression="UserName">
                <ItemStyle Width="150px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="员工工号" SortExpression="StuffId">
                <ItemStyle Width="115px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StuffId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="电子邮件" SortExpression="Email">
                <ItemStyle Width="250px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label111" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="激活" SortExpression="IsActive">
                <ItemStyle Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="入职日期" SortExpression="AttendDate">
                <ItemStyle Width="100px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="Label112" runat="server" Text='<%# Bind("AttendDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemStyle Width="80px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:LinkButton Visible="<%# HasManageRight %>" ID="SetPositionLB" runat="server"
                        CausesValidation="False" CommandName="Select" Text="设置职务"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="StuffUserId" HeaderText="StuffUserId" InsertVisible="False"
                ReadOnly="True" SortExpression="StuffUserId" Visible="False" />
            <asp:BoundField DataField="UserPassword" HeaderText="UserPassword" SortExpression="UserPassword"
                Visible="False" />
            <asp:BoundField DataField="EMail" HeaderText="EMail" SortExpression="EMail" Visible="False" />
            <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone"
                Visible="False" />
        </Columns>
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td style="width: 167px;" class="Empty1">
                        用户名
                    </td>
                    <td style="width: 300px;">
                        职务
                    </td>
                    <td style="width: 150px;">
                        登陆帐号
                    </td>
                    <td style="width: 115px;">
                        员工工号
                    </td>
                    <td style="width: 250px;">
                        电子邮件
                    </td>
                    <td style="width: 100px;">
                        激活
                    </td>
                    <td style="width: 100px;">
                        入职日期
                    </td>
                    <td style="width: 80px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="7" class="Empty2 noneLabel">
                        无
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
        <HeaderStyle CssClass="Header" />
    </gc:GridView>
    <br />
    <div id="PositionSetPanel" runat="server">
        <div class="title" style="width: 1260px;">
            已设置职位
        </div>
        <gc:GridView ID="StuffUserPositionGridView" runat="server" AutoGenerateColumns="False"
            CssClass="GridView" DataKeyNames="PositionId" DataSourceID="StuffUserPositionDS"
            OnRowDataBound="StuffUserPositionGridView_RowDataBound" OnSelectedIndexChanged="StuffUserPositionGridView_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="PositionId" HeaderText="PositionId" InsertVisible="False"
                    ReadOnly="True" SortExpression="PositionId" Visible="False" />
                <asp:TemplateField HeaderText="隶属机构">
                    <ItemStyle Width="1100px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="ParentOUNamesCtl" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="职务名称" SortExpression="PositionName">
                    <ItemStyle Width="168px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton CommandName="Select" ID="LinkButton3" Text='<%# Bind("PositionName") %>'
                            runat="server"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <table>
                    <tr class="Header">
                        <td align="center" style="width: 1100px;" class="Empty1">
                            隶属机构
                        </td>
                        <td align="center" style="width: 168px;">
                            职务名称
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" class="Empty2 noneLabel">
                            无
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <HeaderStyle CssClass="Header" />
        </gc:GridView>
        <asp:ObjectDataSource ID="StuffUserPositionDS" runat="server" SelectMethod="GetPositionByStuffUser"
            TypeName="BusinessObjects.AuthorizationBLL">
            <SelectParameters>
                <asp:Parameter Name="stuffUserId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />
        <div class="title" style="width: 1260px;">
            设置职位
        </div>
        <div class="BorderedArea" style="width: 1265px; height: 400px; overflow: auto;">
            <asp:TreeView ID="OrganizationTreeView" runat="server" ExpandDepth="2" ShowLines="True">
                <SelectedNodeStyle BackColor="#000040" ForeColor="White" />
            </asp:TreeView>
        </div>
        <div style="text-align: center;">
            <asp:Button ID="SavePositionBtn" Enabled="<%# HasManageRight %>" Visible="<%# HasManageRight %>"
                runat="server" CssClass="button_nor" OnClick="SavePositionBtn_Click" Text="保存职位设置" /></div>
    </div>
    <asp:ObjectDataSource ID="StuffUserObjectDataSource" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="GetStuffUserPaged" TypeName="BusinessObjects.StuffUserBLL" SelectCountMethod="TotalCount"
        SortParameterName="sortExpression" EnablePaging="true" OnSelecting="StuffUserObjectDataSource_Selecting">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
