<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SaleForm_SalesRebateApproval" Codebehind="SalesRebateApproval.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1258px">
        基本信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        单据编号</div>
                    <div>
                        <asp:TextBox ID="FormNoCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        申请日期</div>
                    <div>
                        <asp:TextBox ID="ApplyDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        员工</div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        职位</div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        部门</div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        入职日期</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        费用期间</div>
                    <div>
                        <asp:TextBox ID="BeginPeriodCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>&nbsp;
                        <asp:TextBox ID="EndPeriodCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        费用小类</div>
                    <div>
                        <asp:TextBox ID="ExpenseSubCategoryCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        客户名称</div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        客户类型</div>
                    <div>
                        <asp:TextBox ID="CustomerTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        门店</div>
                    <div>
                        <asp:TextBox ID="ShopNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        支付方式</div>
                    <div>
                        <asp:TextBox ID="PaymentTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        合同编号</div>
                    <div>
                        <asp:TextBox ID="ContractNoCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td>
                    <div class="field_title">
                        本次申请金额</div>
                    <div>
                        <asp:TextBox ID="AmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        历史单据</div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div class="field_title">
                        方案简述</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" ReadOnly="true" Width="550px" TextMode="multiline"
                            Height="60px"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        附件</div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" IsView="true" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px;">
        预算信息<asp:HyperLink Style="padding-left: 5px;" ID="btnViewBudget" runat="server" Text="（查看当前预算）" /></div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        客户年度预算</div>
                    <div>
                        <asp:TextBox ID="CustomerBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        客户剩余预算</div>
                    <div>
                        <asp:TextBox ID="CustomerBudgetRemainCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <img src="../Images/DeptBudget.png" alt="本财年开始月到本方案书‘费用期间’的起始月为止的累计预算YTD" /></div>
                    <div>
                        <asp:TextBox ID="OUBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        部门已审批未执行费用</div>
                    <div>
                        <asp:TextBox ID="OUApprovedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        部门审批中申请费用</div>
                    <div>
                        <asp:TextBox ID="OUApprovingAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        部门已确认执行费用</div>
                    <div>
                        <asp:TextBox ID="OUCompletedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        部门已报销费用</div>
                    <div>
                        <asp:TextBox ID="OUReimbursedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        部门可用预算</div>
                    <div>
                        <asp:TextBox ID="OUBudgetRemainCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        部门预算使用进度</div>
                    <div>
                        <asp:TextBox ID="OUBudgetRateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 200px">
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px;">
        费用明细信息</div>
    <asp:UpdatePanel ID="upApplyDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvApplyDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormApplySKUDetailID" DataSourceID="odsApplyDetails"
                CellPadding="0" CellSpacing="0">
                <Columns>
                    <asp:TemplateField HeaderText="产品名称">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="649px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用项">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="350px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="销售额">
                        <ItemTemplate>
                            <asp:Label ID="lblSalesAmount" runat="server" Text='<%# Bind("SalesAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="145px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="申请金额">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsApplyDetails" runat="server" SelectMethod="GetFormApplyDetailViewByFormID"
        TypeName="BusinessObjects.SalesApplyBLL">
        <SelectParameters>
            <asp:Parameter Name="FormID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="审批" CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="返回" CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="编辑" CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="作废" CssClass="button_nor" />&nbsp;
                <asp:Button ID="CloseBtn" runat="server" CssClass="button_nor" OnClick="CloseBtn_Click"
                    Visible="false" Text="方案关闭" />
                <cc1:ConfirmButtonExtender runat="server" TargetControlID="CloseBtn" ConfirmText="方案关闭后将不能再进行报销，确定关闭吗？"
                    ID="ConfirmButtonExtender1">
                </cc1:ConfirmButtonExtender>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc4:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upButton" />
</asp:Content>
