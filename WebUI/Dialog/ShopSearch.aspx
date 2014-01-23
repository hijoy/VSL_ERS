<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DialogMasterPage.master"
    Inherits="Dialog_ShopSearch" Codebehind="ShopSearch.aspx.cs" %>

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
    <div class="title1" style="width: 842px">
        搜索条件</div>
    <table style="background-color: #F6F6F6; vertical-align: top; width: 835px;">
        <tr>
            <td style="width: 200px;" class="field_title">
                门店名称
            </td>
            <td style="width: 260px;" class="field_title">
                客户
            </td>
            <td style="width: 200px;" class="field_title">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:TextBox runat="server" ID="txtShopNameBySearch"></asp:TextBox>
            </td>
            <td style="vertical-align: top">
                <asp:TextBox runat="server" ID="txtCustomerNameBySearch"></asp:TextBox>
            </td>
            <td style="vertical-align: top">
            </td>
            <td style="width: 100px; vertical-align: middle;" align="center" rowspan="2">
                &nbsp;
            </td>
            <td style="width: 200px; vertical-align: middle;" align="center" rowspan="2">
                <asp:Button runat="server" ID="lbtnSearch" Text="搜 索" CssClass="button_nor" OnClick="lbtnSearch_Click">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1" style="width: 842px">
        门店信息</div>
    <gc:GridView ID="gvShop" CssClass="GridView" runat="server" DataSourceID="odsShop"
        AutoGenerateColumns="False" DataKeyNames="ShopID" AllowPaging="True" AllowSorting="True"
        PageSize="20" EnableModelValidation="True" OnRowDataBound="gvShop_RowDataBound">
        <Columns>
            <asp:TemplateField SortExpression="ShopName" HeaderText="门店名称">
                <ItemTemplate>
                    <asp:Label ID="lblShopNameByEdit" runat="server" Text='<%# Bind("ShopName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="客户名称">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerNameByEdit" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Email" HeaderText="客户类型">
                <ItemTemplate>
                    <asp:Label ID="lblCustTypeByEdit" runat="server" Text='<%# Bind("CustomerTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Email" HeaderText="城市">
                <ItemTemplate>
                    <asp:Label ID="lblCityNameByEdit" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Address" HeaderText="地址">
                <ItemTemplate>
                    <asp:Label ID="lblAddressByEdit" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="240px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="height: 22px;">
                    <td style="width: 200px;" class="Empty1">
                        门店名称
                    </td>
                    <td style="width: 200px;">
                        客户名称
                    </td>
                    <td style="width: 100px;">
                        客户类型
                    </td>
                    <td style="width: 100px;">
                        城市
                    </td>
                    <td style="width: 240px;">
                        地址
                    </td>
                </tr>
                <tr><td colspan="5" class="Empty2 noneLabel">无</td></tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsShop" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SortParameterName="sortExpression" EnablePaging="true" SelectMethod="GetShopViewPaged"
        SelectCountMethod="ShopViewTotalCount">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
