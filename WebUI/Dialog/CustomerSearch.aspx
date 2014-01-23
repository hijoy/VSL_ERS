<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DialogMasterPage.master"
    Inherits="Dialog_CustomerSearch" Codebehind="CustomerSearch.aspx.cs" %>

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
    <div class="title1" style="width: 842px;">
        ��������</div>
    <table style="background-color: #F6F6F6; vertical-align: top; width: 842px;">
        <tr>
            <td style="width: 200px;" class="field_title">
                �ͻ����
            </td>
            <td style="width: 200px;" class="field_title">
                �ͻ�����
            </td>
            <td style="width: 200px;" class="field_title">
                ����
            </td>
            <td style="width:100px;" class="field_title">&nbsp;</td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:TextBox runat="server" ID="txtCustNoBySearch"></asp:TextBox>
            </td>
            <td style="vertical-align: top">
                <asp:TextBox runat="server" ID="txtCustNameBySearch"></asp:TextBox>
            </td>
            <td style="vertical-align: top">
                <asp:TextBox runat="server" ID="txtCityNameBySearch"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td style="vertical-align: top">
                <asp:LinkButton runat="server" ID="lbtnSearch" Text="�� ��" CssClass="button_nor" OnClick="lbtnSearch_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="sdsCity" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
        SelectCommand="select CityID,Province.ProvinceName+'-'+City.CityName as CityName from City,Province where City.ProvinceID=Province.ProvinceID order by Province.ProvinceName,City.CityName">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsCustomerType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
        SelectCommand="SELECT [CustomerTypeID], [CustomerTypeName] FROM [CustomerType]">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sdsChannelType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
        SelectCommand="SELECT [ChannelTypeID], [ChannelTypeName] FROM [ChannelType]">
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1" style="width: 842px;">
        �ͻ�</div>
    <gc:GridView ID="gvCustomer" CssClass="GridView" runat="server" DataSourceID="odsCustomer"
        CellPadding="0" AutoGenerateColumns="False" DataKeyNames="CustomerID" AllowPaging="True"
        AllowSorting="True" PageSize="20" EnableModelValidation="True" OnRowDataBound="gvCustomer_RowDataBound1">
        <Columns>
            <asp:TemplateField SortExpression="CustomerNo" HeaderText="�ͻ����">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerNoByEdit" runat="server" Text='<%# Eval("CustomerNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="�ͻ�����">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerNameByEdit" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="180px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CityID" HeaderText="����">
                <ItemTemplate>
                    <asp:Label ID="dplSubCateByEdit" runat="server" Text='<%# GetCityNameById(Eval("CityID") )%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AccountingCode" HeaderText="�ͻ�����">
                <ItemTemplate>
                    <asp:Label ID="lblAccountingCodeByEdit" runat="server" Text='<%#GetCustTypeNameById(Eval("CustomerTypeID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AccountingName" HeaderText="��������">
                <ItemTemplate>
                    <asp:Label ID="lblAccountingNameByEdit" runat="server" Text='<%# GetChanTypeNameById(Eval("ChannelTypeID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsInContract" HeaderText="��������">
                <ItemTemplate>
                    <asp:Label runat="server" ID="chkInContractByEdit" Text='<%#GetOUNameById(Eval("OrganizationUnitID")) %>' />
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsActive" HeaderText="����">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="height: 22px;">
                    <td style="width: 60px;">
                        �ͻ����
                    </td>
                    <td style="width: 150px;">
                        �ͻ�����
                    </td>
                    <td style="width: 100px;">
                        ����
                    </td>
                    <td style="width: 100px;">
                        �ͻ�����
                    </td>
                    <td style="width: 100px;">
                        ��������
                    </td>
                    <td style="width: 200px;">
                        ��������
                    </td>
                    <td style="width: 60px;">
                        ����
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsCustomer" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="CustomerTotalCount"
        SelectMethod="GetCustomerPaged">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
