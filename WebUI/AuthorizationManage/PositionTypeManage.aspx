<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="AuthorizationManage_PositionTypeManage" Codebehind="PositionTypeManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" style="width: 1260px;">
        流程角色管理</div>
    <asp:UpdatePanel ID="upCustomerType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCustomerType" CssClass="GridView" runat="server" DataSourceID="odsPositionType"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="PositionTypeId" AllowPaging="false"
                EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="PositionTypeName" HeaderText="流程角色名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPositionTypeName" runat="server" Text='<%# Bind("PositionTypeName") %>'
                                Width="580px" CssClass="InputText" MaxLength="10"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPositionTypeName" runat="server" Text='<%# Bind("PositionTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="607px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PositionTypeCode" HeaderText="流程角色代码">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPositionTypeCode" runat="server" Text='<%# Bind("PositionTypeCode") %>'
                                Width="580px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPositionTypeCode" runat="server" Text='<%# Bind("PositionTypeCode") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="600px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="lknSave" runat="server" CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="linCancel" runat="server" CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lknEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="编辑"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 607px;" class="Empty1">
                                流程角色名称
                            </td>
                            <td style="width: 600px;">
                                流程角色代码
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvPositionType" runat="server" DataKeyNames="PositionTypeId" DataSourceID="odsPositionType"
                DefaultMode="Insert" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 607px;">
                                <asp:TextBox ID="txtPositionTypeName" runat="server" Text='<%# Bind("PositionTypeName") %>'
                                    Width="580px" CssClass="InputText" MaxLength="10"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 600px;">
                                <asp:TextBox ID="txtPositionTypeCode" runat="server" Text='<%# Bind("PositionTypeCode") %>'
                                    Width="580px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CommandName="Insert" Text="新增"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsPositionType" runat="server" TypeName="BusinessObjects.AuthorizationBLL"
                InsertMethod="InsertPositionType" SelectMethod="GetPositionTypes"  UpdateMethod="UpdatePositionType"
                EnablePaging="false" OnInserting="odsPositionType_Inserting" OnUpdating="odsPositionType_Updating">
                <UpdateParameters>
                    <asp:Parameter Name="PositionTypeName" Type="String" />
                    <asp:Parameter Name="PositionTypeCode" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="PositionTypeName" Type="String" />
                    <asp:Parameter Name="PositionTypeCode" Type="String" />
                </InsertParameters>
                
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
