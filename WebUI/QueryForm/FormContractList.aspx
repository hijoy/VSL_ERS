<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_FormContractList" Title="合同查询" Codebehind="FormContractList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/ShopSelectControl.ascx" TagName="UCShop" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top;">
                <td style="width:250px;">
                    <div class="field_title">
                        合同编号</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:250px;">
                    <div class="field_title">
                        签约对方单位</div>
                    <asp:TextBox ID="txtCompanyName" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:250px;">
                    <div class="field_title">
                        申请人</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td colspan="2" style="width:500px;">
                    <div class="field_title">
                        申请人所在部门</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="200px" />
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="width:250px;">
                    <div class="field_title">
                        合同类型</div>
                    <asp:DropDownList runat="server" ID="dplContractType" DataSourceID="sdsContractType"
                        DataTextField="ContractTypeName" DataValueField="ContractTypeID" Width="170px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="全部" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsContractType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="SELECT [ContractTypeID], [ContractTypeName] FROM [ContractType]">
                    </asp:SqlDataSource>
                </td>
                <td style="width:250px;">
                    <div class="field_title">是否盖章</div>
                    <asp:DropDownList ID="IsStampedDDL" runat="server" CssClass="InputCombo" Width="180px">
                        <asp:ListItem Text="全部" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:250px;">
                    <div class="field_title">是否正本已回收</div>
                    <asp:DropDownList ID="IsRecoveryDDL" runat="server" CssClass="InputCombo" Width="180px">
                        <asp:ListItem Text="全部" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="width:500px;">
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
        合同申请单列表</div>
    <gc:GridView CssClass="GridView" ID="gvContractList" runat="server" DataSourceID="odsContractList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvContractList_RowDataBound">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormContractID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="合同编号">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("ContractNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="单据状态">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffName" HeaderText="申请人">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="提交日期">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ContractName" HeaderText="合同名称">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("ContractName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ContractAmount" HeaderText="合同金额">
                <ItemTemplate>
                    <asp:Label ID="lblContractAmount" runat="server" Text='<%# Bind("ContractAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ContractTypeName" HeaderText="合同类型">
                <ItemTemplate>
                    <asp:Label ID="lblContractTypeName" runat="server" Text='<%# Bind("ContractTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="160px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FirstCompany" HeaderText="签约对方单位1">
                <ItemTemplate>
                    <asp:Label ID="lblFirstCompany" runat="server" Text='<%# Bind("FirstCompany") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="190px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SecondCompany" HeaderText="签约对方单位2">
                <ItemTemplate>
                    <asp:Label ID="lblSecondCompany" runat="server" Text='<%# Bind("SecondCompany") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="BeginDate" HeaderText="合同有效期">
                <ItemTemplate>
                    <asp:Label ID="lblBeginDate" runat="server" Text='<%# GetContractETD(Eval("BeginDate", "{0:yyyy/MM/dd}"),Eval("EndDate", "{0:yyyy/MM/dd}")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="140px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="border: 0;">
                    <td scope="col" style="width: 70px;" class="Empty1">
                        合同编号
                    </td>
                    <td scope="col" style="width: 70px;">
                        单据状态
                    </td>
                    <td scope="col" style="width: 100px;">
                        申请人
                    </td>
                    <td scope="col" style="width: 70px;">
                        提交日期
                    </td>
                    <td scope="col" style="width: 250px;">
                        合同名称
                    </td>
                    <td scope="col" style="width: 100px;">
                        合同金额
                    </td>
                    <td scope="col" style="width: 120px;">
                        合同类型
                    </td>
                    <td scope="col" style="width: 170px;">
                        签约对方单位1
                    </td>
                    <td scope="col" style="width: 170px;">
                        签约对方单位2
                    </td>
                    <td scope="col" style="width: 140px;">
                        合同有效期
                    </td>
                </tr>
                <tr>
                    <td colspan="10" class="Empty2 noneLabel">
                        无
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:DataGrid ID="ExportDataGrid" runat="server" Visible="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundColumn HeaderText="合同编号" DataField="ContractNo" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="单据状态" DataField="Status" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="申请人" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="提交日期" DataField="SubmitDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="合同名称" DataField="ContractName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="合同金额" DataField="ContractAmount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="合同类型" DataField="ContractTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="签约对方单位1" DataField="FirstCompany" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="签约对方单位2" DataField="SecondCompany" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="签约对方单位3" DataField="ThirdCompany" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="合同开始时间" DataField="BeginDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="合同结束时间" DataField="EndDate" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsContractList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormContractViewByRight" EnablePaging="True" SelectCountMethod="QueryFormContractViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
