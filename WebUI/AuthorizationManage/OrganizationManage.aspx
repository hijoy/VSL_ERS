<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="AuthorizationManage_OrganizationManage"
    Title="组织结构" Codebehind="OrganizationManage.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-left: 100px;">
        <asp:Button ID="ShowAllOU" runat="server" OnClick="ShowAllOU_Click" CssClass="button_big"
            Text="显示所有组织结构" /></div>
    <br />
    <div style="width: 1200px;">
        <table width="100%" border=0>
            <tr><td valign="top">
                <div style="width: 600px; border-top-width: thin; border-left-width: thin;
                    border-bottom-width: thin; border-right-width: thin; padding-left: 100px;">
                    <asp:TreeView ID="OrganizationTreeView" runat="server" OnSelectedNodeChanged="OrganizationTreeView_SelectedNodeChanged"
                        ShowCheckBoxes="All" ShowLines="True" ExpandDepth="1">
                        <HoverNodeStyle BackColor="Silver" />
                        <SelectedNodeStyle BackColor="Navy" ForeColor="White" />
                    </asp:TreeView>
                </div>            
            </td>
            <td>
                <div style="width: 500px;">
                    <asp:Panel ID="AddRootOUPanelArea" CssClass="BorderedArea" Visible="false" runat="server">
                        <div class="title3" style="width: 482px;">
                            添加根组织机构
                        </div>
                        <table style="background-color: #f6f6f6; width: 492px;" cellpadding="0">
                            <tr>
                                <td style="width: 100px;">
                                    <asp:Label ID="Label1" runat="server" Text="机构名称："></asp:Label>
                                </td>
                                <td style="width: 240px;">
                                    <asp:TextBox ID="RootUnitNameCtl" runat="server" CssClass="InputText" Width="240px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="机构代码："></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="RootUnitCodeCtl" runat="server" CssClass="InputText" Width="240px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    机构类型：
                                </td>
                                <td>
                                    <asp:DropDownList ID="RootUnitTypeCtl" runat="server" DataSourceID="OrganizationUnitTypeSqlDataSource"
                                        DataTextField="OrganizationUnitTypeName" DataValueField="OrganizationUnitTypeId"
                                        OnDataBound="RootUnitTypeCtl_DataBound" CssClass="InputCombo" Width="240px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    成本中心：
                                </td>
                                <td>
                                    <asp:DropDownList ID="RootCostCenterDDL" runat="server" DataSourceID="odsCostCenter"
                                        DataTextField="CostCenterCode" DataValueField="CostCenterID" CssClass="InputCombo"
                                        Width="240px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="AddRootOrganizationUnitBtn" runat="server" Text="增加" OnClick="AddRootOrganizationUnitBtn_Click"
                                        CssClass="button_nor" ValidationGroup="AddRootOU" />
                                </td>
                            </tr>
                        </table>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RootUnitNameCtl"
                            Display="None" ErrorMessage="机构名称是必填项" ValidationGroup="AddRootOU"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="AddRootOU" />
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="EditOUPanelArea" runat="server" CssClass="BorderedArea">
                        <div class="title3" style="width: 482px;">
                            编辑组织机构
                        </div>
                        <table style="background-color: #f6f6f6; width: 492px;">
                            <tr>
                                <td style="width: 100px;">
                                    <asp:Label ID="Label3" runat="server" Text="机构名称："></asp:Label>
                                </td>
                                <td style="width: 240px;">
                                    <asp:TextBox ID="UnitNameCtl" runat="server" CssClass="InputText" Width="240px" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="机构代码："></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="UnitCodeCtl" runat="server" CssClass="InputText" Width="240px" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    机构类型：
                                </td>
                                <td>
                                    <asp:DropDownList ID="UnitTypeCtl" runat="server" DataSourceID="OrganizationUnitTypeSqlDataSource"
                                        DataTextField="OrganizationUnitTypeName" DataValueField="OrganizationUnitTypeId"
                                        OnDataBound="UnitTypeCtl_DataBound" CssClass="InputCombo" Width="240px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    成本中心：
                                </td>
                                <td>
                                    <asp:DropDownList ID="CostCenterDDL" runat="server" DataSourceID="odsCostCenter"
                                        DataTextField="CostCenterCode" DataValueField="CostCenterID" CssClass="InputCombo"
                                        Width="240px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="UnitIsActiveCtl" Text="启用" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="ChangeParentUnitBtn" runat="server" Text="迁移到选定机构" OnClick="ChangeParentUnitBtn_Click"
                                        CausesValidation="False" CssClass="button_big" />
                                    <asp:Button ID="UpdataOrganizationUnitBtn" runat="server" Text="更新" OnClick="UpdataOrganizationUnitBtn_Click"
                                        CssClass="button_nor" ValidationGroup="EditOU" />
                                    <asp:Button ID="DeleteOrganizationUnitBtn" runat="server" Text="删除" OnClick="DeleteOrganizationUnitBtn_Click"
                                        CausesValidation="False" CssClass="button_nor" />
                                </td>
                            </tr>
                        </table>
                        <asp:SqlDataSource ID="OrganizationUnitTypeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="SELECT [OrganizationUnitTypeId], [OrganizationUnitTypeName] FROM [OrganizationUnitType]">
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="odsCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="select 0 [CostCenterID],' 空' CostCenterCode union SELECT [CostCenterID], [CostCenterCode]+'-'+[CostCenterName] as CostCenterCode FROM [CostCenter] where IsActive = 1 order by CostCenterCode">
                        </asp:SqlDataSource>
                        <cc1:ConfirmButtonExtender TargetControlID="DeleteOrganizationUnitBtn" ConfirmText="确实要删除该组织机构吗?"
                            ID="ConfirmButtonExtender1" runat="server">
                        </cc1:ConfirmButtonExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="UnitNameCtl"
                            Display="None" ErrorMessage="机构名称是必填项" ValidationGroup="EditOU"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="EditOU" />
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="NewOUPanelArea" runat="server" CssClass="BorderedArea">
                        <div class="title3" style="width: 482px;">
                            添加下级组织机构
                        </div>
                        <table style="background-color: #f6f6f6; width: 492px;">
                            <tr>
                                <td style="width: 100px;">
                                    <asp:Label ID="Label6" runat="server" Text="机构名称："></asp:Label>
                                </td>
                                <td style="width: 240px;">
                                    <asp:TextBox ID="NewUnitNameCtl" runat="server" CssClass="InputText" Width="240px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="机构代码："></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="NewUnitCodeCtl" runat="server" CssClass="InputText" Width="240px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    机构类型：
                                </td>
                                <td>
                                    <asp:DropDownList ID="NewUnitTypeCtl" runat="server" DataSourceID="OrganizationUnitTypeSqlDataSource"
                                        DataTextField="OrganizationUnitTypeName" DataValueField="OrganizationUnitTypeId"
                                        OnDataBound="NewUnitTypeCtl_DataBound" CssClass="InputCombo" Width="240px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    成本中心：
                                </td>
                                <td>
                                    <asp:DropDownList ID="NewCostCenterDDL" runat="server" DataSourceID="odsCostCenter"
                                        DataTextField="CostCenterCode" DataValueField="CostCenterID" CssClass="InputCombo"
                                        Width="240px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="AddOrganizationUnitBtn" runat="server" Text="增加下级机构" OnClick="AddOrganizationUnitBtn_Click"
                                        CssClass="button_big" ValidationGroup="NewOU" />
                                </td>
                            </tr>
                        </table>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NewUnitNameCtl"
                            Display="None" ErrorMessage="机构名称是必填项" ValidationGroup="NewOU"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="NewOU" />
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="NewPositionPanelArea" runat="server" CssClass="BorderedArea">
                        <div class="title3" style="width: 482px;">
                            添加职位
                        </div>
                        <table style="background-color: #f6f6f6; width: 492px;">
                            <tr>
                                <td style="width: 100px;">
                                    <asp:Label ID="Label5" runat="server" Text="职位名称："></asp:Label>
                                </td>
                                <td style="width: 240px;">
                                    <asp:TextBox ID="NewPositionNameCtl" runat="server" CssClass="InputText" Width="240px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    流程角色：
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="NewPositionTypeCtl" runat="server" DataSourceID="PositionTypeSqlDataSource"
                                        DataTextField="PositionTypeName" DataValueField="PositionTypeId" />
                                    <%--<asp:ListBox ID="NewPositionTypeCtl" runat="server" DataSourceID="PositionTypeSqlDataSource"
                                                DataTextField="PositionTypeName" DataValueField="PositionTypeId" AppendDataBoundItems="true"
                                                SelectionMode="Multiple" Width="240px">
                                                <asp:ListItem Value="" Text="请选择" />
                                                </asp:ListBox>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="AddPositionBtn" runat="server" Text="添加职位" OnClick="AddPositionBtn_Click"
                                        CssClass="button_nor" ValidationGroup="NewPosition" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <asp:SqlDataSource ID="PositionTypeSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="SELECT [PositionTypeId], [PositionTypeName] FROM [PositionType]">
                        </asp:SqlDataSource>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="NewPositionNameCtl"
                            Display="None" ErrorMessage="职位名称是必填项" ValidationGroup="NewPosition"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="NewPosition" />
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="EditPositionPanelArea" runat="server" CssClass="BorderedArea">
                        <div class="title3" style="width: 482px;">
                            编辑职位
                        </div>
                        <table style="background-color: #f6f6f6; width: 492px;">
                            <tr>
                                <td style="width: 100px;">
                                    <asp:Label ID="Label8" runat="server" Text="职位名称："></asp:Label>
                                </td>
                                <td style="width: 240px;">
                                    <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputText" Width="240px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    角色类型：
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="PositionTypeCtl" runat="server" DataSourceID="PositionTypeSqlDataSource"
                                        DataTextField="PositionTypeName" DataValueField="PositionTypeId" />
                                    <%--<asp:ListBox ID="PositionTypeCtl" runat="server" DataSourceID="PositionTypeSqlDataSource"
                                                DataTextField="PositionTypeName" DataValueField="PositionTypeId" OnDataBound="PositionTypeCtl_DataBound"
                                                SelectionMode="Multiple" Width="240px"></asp:ListBox>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="PositionIsActiveCtl" Text="启用" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="ChangeOrganizationUnitBtn" runat="server" Text="迁移到选定机构" OnClick="ChangeOrganizationUnitBtn_Click"
                                        CausesValidation="False" CssClass="button_big" />
                                    <asp:Button ID="UpdatePositionBtn" runat="server" Text="更新" OnClick="UpdatePositionBtn_Click"
                                        CssClass="button_nor" ValidationGroup="EditPosition" />
                                    <asp:Button ID="DeletePositionBtn" runat="server" Text="删除" OnClick="DeletePositionBtn_Click"
                                        CausesValidation="False" CssClass="button_nor" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PositionNameCtl"
                                        Display="None" ErrorMessage="职位名称是必填项" ValidationGroup="EditPosition"></asp:RequiredFieldValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="EditPosition" />
                                </td>
                            </tr>
                        </table>
                        <cc1:ConfirmButtonExtender TargetControlID="DeletePositionBtn" ConfirmText="确定要删除该职位吗？"
                            ID="ConfirmButtonExtender2" runat="server">
                        </cc1:ConfirmButtonExtender>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="StuffPanel" runat="server" CssClass="BorderedArea">
                        <div class="title3" style="width: 482px;">
                            岗位员工
                        </div>
                        <gc:GridView ID="StuffGridView" Width="492px" CssClass="GridView" runat="server"
                            AutoGenerateColumns="False" DataKeyNames="StuffUserId" DataSourceID="PositionStuffDS"
                            CellPadding="0">
                            <HeaderStyle CssClass="Header" />
                            <Columns>
                                <asp:BoundField DataField="StuffUserId" HeaderText="StuffUserId" InsertVisible="False"
                                    ReadOnly="True" SortExpression="StuffUserId" Visible="False" />
                                <asp:BoundField DataField="UserName" ItemStyle-HorizontalAlign="Center" HeaderText="用户名"
                                    SortExpression="UserName" />
                                <asp:BoundField DataField="StuffName" ItemStyle-HorizontalAlign="Center" HeaderText="员工姓名"
                                    SortExpression="StuffName" />
                                <asp:BoundField DataField="StuffId" ItemStyle-HorizontalAlign="Center" HeaderText="工号"
                                    SortExpression="StuffId" />
                                <asp:CheckBoxField DataField="IsActive" ItemStyle-HorizontalAlign="Center" HeaderText="是否在职"
                                    SortExpression="IsActive" />
                            </Columns>
                            <EmptyDataTemplate>
                                <table width="370px">
                                    <tr class="Header">
                                        <td class="Empty1">
                                            用户名
                                        </td>
                                        <td>
                                            员工姓名
                                        </td>
                                        <td>
                                            工号
                                        </td>
                                        <td>
                                            是否在职
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center;" class="Empty2 noneLabel">
                                            无在岗员工
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </gc:GridView>
                        <asp:ObjectDataSource ID="PositionStuffDS" runat="server" SelectMethod="GetDataByPositionId"
                            TypeName="BusinessObjects.AuthorizationDSTableAdapters.StuffUserTableAdapter">
                            <SelectParameters>
                                <asp:Parameter Name="PositionId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:Panel>
                    <br />
                </div>
            </td></tr>
        </table>
    </div>
</asp:Content>
