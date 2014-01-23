<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BaseData_Announcement" Codebehind="Announcement.aspx.cs" %>

<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" style="width: 1260px;">
        系统公告</div>
    <asp:GridView CssClass="GridView" ID="BulletinGridView" runat="server" DataSourceID="BulletinObjectDataSource"
        AutoGenerateColumns="False" DataKeyNames="BulletinID" AllowPaging="True" AllowSorting="True"
        ShowFooter="true" PageSize="10" OnRowDeleted="BulletinGridView_RowDeleted" CellPadding="0"
        CellSpacing="0">
        <Columns>
            <asp:TemplateField InsertVisible="False" SortExpression="SystemId" HeaderText="ID"
                Visible="False">
                <ItemTemplate>
                    <asp:Label ID="BulletinIDLabel" runat="server" Text='<%# Bind("BulletinID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="BulletinTitle" HeaderText="标题">
                <ItemStyle Width="830px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:LinkButton CommandName="Select" ID="TitleCtl" runat="server" Text='<%# Bind("BulletinTitle") %>'
                        OnClick="TitleCtl_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CreateTime" HeaderText="创建时间">
                <ItemTemplate>
                    <asp:Label ID="CreateTimeLabel" runat="server" Text='<%# Bind("CreateTime") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsActive" HeaderText="是否有时效">
                <ItemTemplate>
                    <asp:CheckBox Enabled="false" ID="IsActiveCtl" Checked='<%# Bind("IsActive") %>'
                        runat="server" />
                </ItemTemplate>
                <ItemStyle Width="60" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsHot" HeaderText="IsHot">
                <ItemTemplate>
                    <asp:CheckBox Enabled="false" ID="IsHotCtl" Checked='<%# Bind("IsHot") %>' runat="server" />
                </ItemTemplate>
                <ItemStyle Width="40" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="EditBtn" Visible="<%# HasManageRight %>" Text="编辑" CommandName="Select"
                        runat="server" OnClick="Edit_Click" />
                    <asp:LinkButton ID="DeleteBtn" Visible="<%# HasManageRight %>" Text="删除" OnClientClick="return confirm('确定删除此行数据吗？');"
                        CommandName="Delete" runat="server" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:LinkButton ID="NewBtn" Visible="<%# HasManageRight %>" Text="添加" runat="server"
                        OnClick="NewBtn_Click" />
                </FooterTemplate>
                <ItemStyle Width="80" HorizontalAlign="Center" />
                <FooterStyle Width="80" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="height: 22px;">
                    <th style="width: 60px;" class="Empty1">
                        ID
                    </th>
                    <th style="width: 850px;">
                        标题
                    </th>
                    <th style="width: 100px;">
                        创建者
                    </th>
                    <th style="width: 250px;">
                        创建时间
                    </th>
                    <th style="width: 50px;">
                        &nbsp;
                    </th>
                    <th style="width: 50px;">
                        &nbsp;
                    </th>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:LinkButton ID="NewBtn" Visible="<%# HasManageRight %>" Text="添加" runat="server"
                            OnClick="NewBtn_Click" />
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </asp:GridView>
    <div runat="server" id="Opdiv">
        <table style="width: 1270px; background-color: #F6F6F6">
            <tr>
                <td colspan="2" style="text-align: center;">
                    有时效<asp:CheckBox ID="EditIsActiveCheckBox" runat="server" />
                    IsHot<asp:CheckBox ID="EditIsHotCheckBox" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 50px;">
                    标题：
                </td>
                <td>
                    <asp:TextBox Width="1150px" ID="EditBulletinTitleTextBox" MaxLength="40" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 50px; vertical-align: top;">
                    内容：
                </td>
                <td>
                    <asp:TextBox Width="1150px" TextMode="multiLine" Rows="30" ID="EditBulletinContentTextBox"
                        runat="server">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top" style="text-align: center;">
                    <uc1:UCFlie ID="EditUCFileUpload" runat="server" Width="320px" />
                </td>
            </tr>
            <tr id="tr1" runat="server">
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnSave" runat="server" Visible="<%# HasManageRight %>" OnClick="btnSave_Click"
                        Text="保存" CssClass="button_nor" />
                    <asp:Button ID="btnCancel" runat="server" Visible="<%# HasManageRight %>" OnClick="btnCancel_Click"
                        Text="取消" CssClass="button_nor" />
                </td>
            </tr>
            <asp:Label ID="EditID" runat="server" Visible="false"></asp:Label>
        </table>
    </div>
    <asp:ObjectDataSource ID="BulletinObjectDataSource" runat="server" DeleteMethod="DeleteBulletin"
        SelectMethod="GetPage" TypeName="BusinessObjects.MasterDataBLL" SelectCountMethod="TotalCount"
        SortParameterName="sortExpression" EnablePaging="true">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="string" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="bulletinId" Type="int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
</asp:Content>
