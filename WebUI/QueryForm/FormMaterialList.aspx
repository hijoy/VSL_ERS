<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_FormMaterialList" Title="广宣物资申请单查询" Codebehind="FormMaterialList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/ShopSelectControl.ascx" TagName="UCShop" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width:200px;">
                    <div class="field_title">
                        单据编号</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:200px;">
                    <div class="field_title">
                        申请人</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:300px;">
                    <div class="field_title">
                        申请人所在部门</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="180px" />
                </td>
                <td style="width:260px;">
                    <div class="field_title">
                        门店</div>
                    <uc3:UCShop ID="UCShop" runat="server" Width="140px" />
                </td>
                <td style="width:200px;">
                    <div class="field_title">
                        提交日期</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                </nobr>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="5" valign="middle">
                    <span class="field_title">单据状态</span>
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="待审批" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="审批完成" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkRejected" runat="server" Text="退回待修改" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkScrap" runat="server" Text="作废" Checked="false" />
                </td>
            </tr>
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" valign="middle" colspan="6">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="查询" OnClick="btnSearch_Click" />&nbsp;
                <asp:Button ID="btnExport" runat="server" CssClass="button_nor" Text="导出" OnClick="btnExport_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="title" style="width: 1260px;">
        广宣物资申请单列表</div>
    <gc:GridView CssClass="GridView" ID="gvMaterialList" runat="server" DataSourceID="odsMaterialList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvMaterialList_RowDataBound">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormMaterialID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="单据编号">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="单据状态">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffName" HeaderText="申请人">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="提交日期">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Amount" HeaderText="申请金额">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="客户名称">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ShopName" HeaderText="门店名称">
                <ItemTemplate>
                    <asp:Label ID="lblShopName" runat="server" Text='<%# Bind("ShopName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="313px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="border: 0;">
                    <td scope="col" style="width: 150px;" class="Empty1">
                        单据编号
                    </td>
                    <td scope="col" style="width: 100px;">
                        单据状态
                    </td>
                    <td scope="col" style="width: 150px;">
                        申请人
                    </td>
                    <td scope="col" style="width: 150px;">
                        提交日期
                    </td>
                    <td scope="col" style="width: 150px;">
                        申请金额
                    </td>
                    <td scope="col" style="width: 250px;">
                        客户名称
                    </td>
                    <td scope="col" style="width: 313px;">
                        门店名称
                    </td>
                </tr>
                <tr>
                    <td colspan="7" style="text-align: center;" class="Empty2 noneLabel">
                        无
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:DataGrid ID="ExportDataGrid" runat="server" Visible="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundColumn HeaderText="单据编号" DataField="FormNo" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="单据状态" DataField="Status" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="申请人" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="提交日期" DataField="SubmitDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="申请金额" DataField="Amount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="客户名称" DataField="CustomerName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="门店名称" DataField="ShopName" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsMaterialList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormMaterialViewByRight" EnablePaging="True" SelectCountMethod="QueryFormMaterialViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
