<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_FormReimburseList" Title="方案报销单查询" Codebehind="FormReimburseList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width:200px;">
                    <div class="field_title">
                        报销单编号</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:200px;">
                    <div class="field_title">
                        申请单编号(必须填写完全)</div>
                    <asp:TextBox ID="txtApplyFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:200px;">
                    <div class="field_title">
                        申请人</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:200px;">
                    <div class="field_title">
                        支付方式</div>
                    <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                        DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="170px">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="width:400px;">
                    <div class="field_title">
                        提交日期</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand=" select 0 PaymentTypeID,' 全部' PaymentTypeName union SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] order by PaymentTypeName">
                </asp:SqlDataSource>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="field_title">
                        申请人所在部门</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="230px" />
                </td>
                <td colspan="2">
                    <div class="field_title">
                        客户</div>
                    <uc3:UCCustomer ID="UCCustomer" runat="server" Width="230px" />
                </td>
                <td >
                    <div class="field_title">
                        是否发货完成</div>
                    <asp:DropDownList ID="IsDeliveryCompleteDDL" runat="server" CssClass="InputCombo" Width="150px">
                        <asp:ListItem Text="全部" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="已经发货完成" Value="1"></asp:ListItem>
                        <asp:ListItem Text="未发货完成" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 100%;" colspan="6" valign="middle">
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
        方案报销单列表</div>
    <gc:GridView CssClass="GridView" ID="gvReimburseList" runat="server" DataSourceID="odsReimburseList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvReimburseList_RowDataBound" OnRowCommand="gvReimburseList_RowCommand">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormReimburseID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="单据编号">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="单据状态">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="客户名称">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="360px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormReimburse.PaymentTypeID" HeaderText="支付方式">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Amount" HeaderText="报销金额">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffName" HeaderText="申请人">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="提交日期">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="153px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsDeliveryComplete" HeaderText="是否发货完成">
                <ItemTemplate>
                    <asp:CheckBox ID="ckIsDeliveryComplete" runat="server" Checked='<%# Bind("IsDeliveryComplete") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Text="作废" runat="server" CausesValidation="false"
                        CommandName="scrap" CommandArgument='<%# Bind("FormID") %>' OnClientClick="return confirm('您将要作废该单据！')"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="border: 0;">
                    <td scope="col" style="width: 200px;" class="Empty1">
                        单据编号
                    </td>
                    <td scope="col" style="width: 100px;">
                        单据状态
                    </td>
                    <td scope="col" style="width: 460px;">
                        客户名称
                    </td>
                    <td scope="col" style="width: 100px;">
                        支付方式
                    </td>
                    <td scope="col" style="width: 150px;">
                        报销金额
                    </td>
                    <td scope="col" style="width: 100px;">
                        申请人
                    </td>
                    <td scope="col" style="width: 153px;">
                        提交日期
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
            <asp:BoundColumn HeaderText="客户名称" DataField="CustomerName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="支付方式" DataField="PaymentTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="报销金额" DataField="Amount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="申请人" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="提交日期" DataField="SubmitDate" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsReimburseList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormReimburseViewByRight" EnablePaging="True" SelectCountMethod="QueryFormReimburseViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
