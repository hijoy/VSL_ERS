<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/DialogMasterPage.master"
    Inherits="Dialog_SKUSearch" Codebehind="SKUSearch.aspx.cs" %>

<%@ Implements Interface="System.Web.UI.IPostBackEventHandler" %>
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
        function EnterTextBox(e) {
            var msie = (document.all) ? true : false;
            var keycode;
            if (!msie) keycode = window.event ? e.keyCode : e.which;
            else keycode = e.keyCode;
            if (keycode == 13 && document.getElementById('<%=this.lbtnSearch.ClientID%>').value != "") {
                if (msie) {
                    e.keyCode = 9;
                    e.returnValue = false;
                }
                document.getElementById('<%=this.lbtnSearch.ClientID%>').click();
            }
        } 
    </script>
    <div class="title1" style="width: 842px">
        搜索条件</div>
    <table style="background-color: #F6F6F6; vertical-align: top; width: 825px;">
        <tr>
            <td style="width: 200px;" class="field_title">
                产品编号
            </td>
            <td style="width: 200px;" class="field_title">
                产品名称
            </td>
            <td style="width: 200px;" class="field_title"> 
                产品种类
            </td>
            <td style="width: 200px;" class="field_title">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:TextBox runat="server" ID="txtSKUNo"></asp:TextBox>
            </td>
            <td style="vertical-align: top">
                <asp:TextBox runat="server" ID="txtSKUName"></asp:TextBox>
            </td>
            <td style="vertical-align: top">
                <asp:DropDownList runat="server" ID="dplSKUCategory" Width="150px" DataSourceID="sdsSKUCateAll"
                    AppendDataBoundItems="true" DataTextField="SKUCategoryName" DataValueField="SKUCategoryID">
                    <asp:ListItem Selected="True" Text="所有种类" Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="sdsSKUCateAll" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand="select SKUCategoryID,SKUCategoryName from SKUCategory"></asp:SqlDataSource>
            </td>
            <td style="vertical-align: top">
                &nbsp;
            </td>
            <td style="vertical-align: top">
                &nbsp;
            </td>
            <td style="width: 150px; vertical-align: middle;" align="center" rowspan="2">
                <asp:LinkButton runat="server" ID="lbtnSearch" Text="搜 索" CssClass="button_nor"
                    OnClick="lbtnSearch_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="title1" style="width: 842px">
        产品信息</div>
    <gc:GridView ID="gvSKU" CssClass="GridView" runat="server" DataSourceID="odsSKU"
        AutoGenerateColumns="False" DataKeyNames="SKUID" AllowPaging="True" AllowSorting="True"
        PageSize="20" EnableModelValidation="True" OnRowDataBound="gvSKU_RowDataBound1">
        <Columns>
            <asp:TemplateField SortExpression="SKUNo" HeaderText="产品编号">
                <ItemTemplate>
                    <asp:Label ID="lbtnSKUNoByEdit" runat="server" Text='<%# Eval("SKUNo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="110px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SKUName" HeaderText="产品名称">
                <ItemTemplate>
                    <asp:Label ID="lblSKUNameByEdit" runat="server" Text='<%# Bind("SKUName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="330px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SKUCategoryId" HeaderText="产品类别">
                <ItemTemplate>
                    <asp:Label ID="ExpenseSubCategorLabel" runat="server" Text='<%#GetSKUCateNameById(Eval("SKUCategoryID"))%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="PackageQuantity" HeaderText="装箱数">
                <ItemTemplate>
                    <asp:Label ID="lblPackageQuantity" Width="40px" CssClass="NumberLabel" runat="server"
                        Text='<%# Bind("PackageQuantity") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="PackagePercent" HeaderText="标箱比例">
                <ItemTemplate>
                    <asp:Label ID="lblPackagePercent" Width="40px" CssClass="NumberLabel" runat="server"
                        Text='<%# Bind("PackagePercent") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Spec" HeaderText="规格">
                <ItemTemplate>
                    <asp:Label ID="lblSpec" runat="server" Text='<%# Bind("Spec") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SKUCostCenter" HeaderText="成本中心">
                <ItemTemplate>
                    <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("SKUCostCenter") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header">
                    <td style="width: 110px;" class="Empty1">
                        产品编号
                    </td>
                    <td style="width: 330px;">
                        产品名称
                    </td>
                    <td style="width: 150px;">
                        产品类别
                    </td>
                    <td style="width: 120px;">
                        装箱数
                    </td>
                    <td style="width: 120px;">
                        标箱比例
                    </td>
                    <td style="width: 150px;">
                        口味
                    </td>
                    <td style="width: 150px;">
                        成本中心
                    </td>
                    <td style="width: 60px;">
                        &nbsp;
                    </td>
                </tr>
                <tr><td colspan="7" class="Empty2 noneLabel">无</td></tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:SqlDataSource ID="sdsSKUCateEnabled" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
        SelectCommand="select SKUCategoryID,SKUCategoryName from SKUCategory where IsActive=1">
    </asp:SqlDataSource>
    <asp:ObjectDataSource ID="odsSKU" runat="server" TypeName="BusinessObjects.MasterDataBLL"
        SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="SKUTotalCount"
        SelectMethod="GetSKUPaged">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
