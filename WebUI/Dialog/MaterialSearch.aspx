<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DialogMasterPage.master"
    Inherits="Dialog_MaterialSearch" Codebehind="MaterialSearch.aspx.cs" %>

<%@ Implements Interface="System.Web.UI.IPostBackEventHandler" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
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
    <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="5" style="height: 15px">
            </td>
        </tr>
        <tr style="vertical-align: middle;">
            <td style="width: 20%;" class="field_title">
                物资名称
            </td>
            <td style="width: 40%;">
                <asp:TextBox ID="txtMaterialName" runat="server" CssClass="InputText" Width="200px"></asp:TextBox>
            </td>
            <td style="width: 40%; ">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="查询"
                    OnClick="btnSearch_Click" />&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle" colspan="3" style="height: 15px;">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div>
        <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="25" align="center" valign="top" colspan="5">
                    <gc:GridView CssClass="GridView" ID="gvMaterial" runat="server" DataSourceID="odsMaterial" AutoGenerateColumns="False" DataKeyNames="MaterialID" 
                        Width="100%" AllowPaging="True" AllowSorting="True" PageSize="20" OnRowDataBound="gvMaterial_RowDataBound">
                        <Columns>
                            <asp:TemplateField SortExpression="MaterialName" HeaderText="物资名称">
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialName" runat="server" Text='<%# Bind("MaterialName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="MaterialPrice" HeaderText="单价">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrice" runat="server" Text='<%# Bind("MaterialPrice") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="UOM" HeaderText="单位">
                                <ItemTemplate>
                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UOM")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="MinimumNumber" HeaderText="最小领用数量">
                                <ItemTemplate>
                                    <asp:Label ID="lblMinimumNumber" runat="server" Text='<%# Bind("MinimumNumber")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Description" HeaderText="规格描述">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="350px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle CssClass="Header" />
                        <EmptyDataTemplate>
                            <table>
                                <tr class="Header" style="border: 0;">
                                    <td style="width: 150px;" class="Empty1">
                                        物资名称
                                    </td>
                                    <td style="width: 100px;" >
                                        单价
                                    </td>
                                    <td style="width: 50px;">
                                        单位
                                    </td>
                                    <td style="width: 100px;">
                                        最小领用数量
                                    </td>
                                    <td style="width: 350px;">
                                        规格描述
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        无
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <SelectedRowStyle CssClass="SelectedRow" />
                    </gc:GridView>
                    <asp:ObjectDataSource ID="odsMaterial" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                        SelectMethod="GetActiveMaterialPaged" SelectCountMethod="ActiveMaterialTotalCount"
                        EnablePaging="True" SortParameterName="sortExpression">
                        <SelectParameters>
                            <asp:Parameter Name="queryExpression" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
