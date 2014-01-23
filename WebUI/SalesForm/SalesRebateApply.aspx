<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SaleForm_SalesRebateApply" Title="方案申请" Codebehind="SalesRebateApply.aspx.cs" %>

<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1258px">
        基本信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
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
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        费用期间</div>
                    <div>
                        <asp:TextBox ID="BeginPeriodCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>&nbsp;
                        <asp:TextBox ID="EndPeriodCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
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
                <%--<td style="width: 200px">
                    <div class="field_title">
                        客户类型</div>
                    <div>
                        <asp:TextBox ID="CustomerTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>--%>
                <td valign="top" style="width: 200px">
                    <div class="field_title">
                        方案名称<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="txtFormApplyName" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        门店</div>
                    <div>
                        <asp:DropDownList ID="ShopDDL" runat="server" DataSourceID="odsShop" DataTextField="ShopName"
                            DataValueField="ShopID" Width="180px">
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        支付方式 <label class="requiredLable">*</label></div>
                    <div>
                        <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                            DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="180px">
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        合同编号(合同内费用需填写)</div>
                    <div>
                        <asp:TextBox ID="ContractNoCtl" MaxLength="20" runat="server" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div class="field_title" valign="top">
                        备注</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" TextMode="multiline" Height="60px" 
                            Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        附件</div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="odsShop" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [ShopID], [ShopName] FROM [Shop] where IsActive = 1 and CustomerID = @CustomerID order by ShopName">
            <SelectParameters>
                <asp:Parameter Name="CustomerID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] where IsActive = 1 ">
        </asp:SqlDataSource>
    </div>
    <br />
    <div class="title" style="width: 1258px;">
        预算信息</div>
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
    <div class="title" style="width: 1260px;">
        费用明细信息</div>
    <asp:UpdatePanel ID="upApplyDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvApplyDetails" runat="server" ShowFooter="True" CssClass="GridView"
                OnRowDataBound="gvApplyDetails_RowDataBound" AutoGenerateColumns="False" DataKeyNames="FormApplySKUDetailID"
                DataSourceID="odsApplyDetails" CellPadding="0" CellSpacing="0">
                <Columns>
                    <asp:TemplateField HeaderText="产品名称">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="652px" />
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
    <asp:ObjectDataSource ID="odsApplyDetails" runat="server" SelectMethod="GetFormApplyDetailView"
        TypeName="BusinessObjects.SalesApplyBLL" OnObjectCreated="odsApplyDetails_ObjectCreated">
    </asp:ObjectDataSource>
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:ImageButton ID="SubmitBtn" runat="server" ImageUrl="../images/btnSubmit.gif"
                    OnClick="SubmitBtn_Click" Text="提交" />
                <asp:ImageButton ID="SaveBtn" runat="server" ImageUrl="../images/btnSave.gif" OnClick="SaveBtn_Click"
                    Text="保存" />
                <asp:ImageButton ID="CancelBtn" runat="server" ImageUrl="../images/btnCancel.gif"
                    OnClick="CancelBtn_Click" Text="返回" />
                <asp:ImageButton ID="DeleteBtn" runat="server" ImageUrl="../images/btnDelete.gif"
                    OnClick="DeleteBtn_Click" Text="删除" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upCustomer" />
</asp:Content>
