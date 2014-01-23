<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_FormPersonalReimburseList"
    Title="个人费用报销查询" Codebehind="PersonalReimburseList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonth"
    TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <div class="field_title">
                        单据编号</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        申请人</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 400px;" colspan="2">
                    <div class="field_title">
                        申请人所在部门</div>
                    <uc3:OUSelect ID="UCOU" runat="server" Width="300px" />
                </td>
                <td style="width: 280px;">
                    <div class="field_title">
                        费用期间</div>
                    <nobr>
                        <uc2:YearAndMonth ID="UCPeriodBegin" runat="server" 
                        IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc2:YearAndMonth ID="UCPeriodEnd" runat="server" 
                        IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
            </tr>
            <tr>
                <td class="field_title" style="width: 280px;" colspan="2">
                    <div class="field_title">
                        提交日期</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                </nobr>
                </td>
                <td colspan="2">
                    <br />
                    <nobr>
                    <span class="field_title">
                        单据状态:</span>
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="待审批" Checked="false" ></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="审批完成" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkRejected" runat="server" Text="退回待修改" Checked="false" />&nbsp;&nbsp;                    
                    <asp:CheckBox ID="chkScrap" runat="server" Text="作废" Checked="false" />
                    </nobr>
                </td>
                <td colspan="3">
                    <br />
                    <nobr>
                    <span class="field_title">
                        单据类型:</span>
                    <asp:CheckBox ID="chkPersonalReimburse" runat="server" Text="个人费用报销" Checked="false" ></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkTravelReimburse" runat="server" Text="出差费用报销" Checked="false" />&nbsp;&nbsp;
                    </nobr>
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
        个人费用报销申请单列表</div>
    <gc:GridView CssClass="GridView" ID="gvPersonalReimburseList" runat="server" DataSourceID="odsPersonalReimburseList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvPersonalReimburseList_RowDataBound">
        <Columns>
            <asp:TemplateField SortExpression="FormNo" HeaderText="单据编号">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Period" HeaderText="费用期间">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" runat="server" Text='<%#Bind("Period","{0:yyyy/MM}") %>'></asp:Label>
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
            <asp:TemplateField SortExpression="OrganizationUnit.OrganizationUnitName" HeaderText="所属部门">
                <ItemTemplate>
                    <asp:Label ID="lblDepartmentName" runat="server" Text='<%# Bind("DepartmentName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="300px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="提交日期">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Amount" HeaderText="报销金额">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="RealAmount" HeaderText="实报金额">
                <ItemTemplate>
                    <asp:Label ID="lblRealAmount" runat="server" Text='<%# Bind("RealAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Right" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="border: 0;">
                    <td scope="col" style="width: 150px;" class="Empty1">
                        单据编号
                    </td>
                    <td scope="col" style="width: 150px;">
                        费用期间
                    </td>
                    <td scope="col" style="width: 100px;">
                        单据状态
                    </td>
                    <td scope="col" style="width: 150px;">
                        申请人
                    </td>
                    <td scope="col" style="width: 300px;">
                        所属部门
                    </td>
                    <td scope="col" style="width: 150px;">
                        提交日期
                    </td>
                    <td scope="col" style="width: 150px;">
                        报销金额
                    </td>
                    <td scope="col" style="width: 150px;">
                        实报金额
                    </td>
                </tr>
                <tr>
                    <td colspan="10" style="text-align: center;" class="Empty2 noneLabel">
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
            <asp:BoundColumn HeaderText="费用期间" DataField="Period" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="单据状态" DataField="Status" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="申请人" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="所属部门" DataField="DepartmentName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="提交日期" DataField="SubmitDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="报销金额" DataField="Amount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="实报金额" DataField="RealAmount" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsPersonalReimburseList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormPersonalReimburseViewByRight" EnablePaging="True" SelectCountMethod="QueryFormPersonalReimburseViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
