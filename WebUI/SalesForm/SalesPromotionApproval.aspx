<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SaleForm_SalesPromotionApproval" Title="方案申请审批" CodeBehind="SalesPromotionApproval.aspx.cs" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../Script/js.js"></script>
    <script type="text/javascript" src="../Script/DateInput.js"></script>
    <div class="title" style="width: 1258px">
        基本信息
        <asp:Label ID="PrintInfor" runat="server" Style="padding-left: 900px;"></asp:Label>&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="PrintBtn" runat="server" OnClick="PrintBtn_Click" OnClientClick="PrintClick()"
            Text="打印" />&nbsp;
    </div>
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
                <%--<td style="width: 200px">
                    <div class="field_title">
                        客户类型</div>
                    <div>
                        <asp:TextBox ID="CustomerTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>--%>
                <td valign="top" style="width: 200px">
                    <div class="field_title">
                        方案名称</div>
                    <div>
                        <asp:TextBox ID="txtFormApplyName" ReadOnly="true" runat="server" Width="170px"></asp:TextBox></div>
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
                <td colspan="" valign="top">
                    <div class="field_title">
                        附件</div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="380px" IsView="true" />
                </td>
                <td valign="top">
                    <div class="field_title">
                        核销要求</div>
                    <div>
                        <asp:CheckBoxList runat="server" ID="chkListReimburseRequirements" Enabled="false"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Text="图片" Value="1"></asp:ListItem>
                            <asp:ListItem Text="协议书" Value="2"></asp:ListItem>
                            <asp:ListItem Text="送货单" Value="4"></asp:ListItem>
                            <asp:ListItem Text="合同" Value="8"></asp:ListItem>
                            <asp:ListItem Text="DM" Value="16"></asp:ListItem>
                            <asp:ListItem Text="其他" Value="32"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        促销信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        促销期间</div>
                    <div>
                        <asp:TextBox ID="PromotionBeginCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server">~~</asp:Label>
                        <asp:TextBox ID="PromotionEndCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        供货期间</div>
                    <div>
                        <asp:TextBox ID="DeliveryBeginCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server">~~</asp:Label>
                        <asp:TextBox ID="DeliveryEndCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        促销类型</div>
                    <div>
                        <asp:TextBox ID="PromotionScopeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        促销形式</div>
                    <div>
                        <asp:TextBox ID="PromotionTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        促销活动</div>
                    <div>
                        <asp:TextBox ID="PromotionDescCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        陈列形式</div>
                    <div>
                        <asp:TextBox ID="ShelfTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        第一个月销量</div>
                    <div>
                        <asp:TextBox ID="FirstVolumeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        第二个月销量</div>
                    <div>
                        <asp:TextBox ID="SecondVolumeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        第三个月销量</div>
                    <div>
                        <asp:TextBox ID="ThirdVolumeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        前三月平均销量</div>
                    <div>
                        <asp:TextBox ID="AverageVolumeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px;">
        预算信息
        <asp:HyperLink Style="padding-left: 5px;" ID="btnViewBudget" runat="server" Text="（查看当前预算）" /></div>
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
    <div style="width: 1268px;">
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="ExpenseSubCategoryID" runat="server" Visible="False" />
                    <asp:UpdatePanel ID="upSKU" runat="server">
                        <ContentTemplate>
                            <asp:ListView runat="server" ID="SKUListView" DataSourceID="odsSKU" DataKeyNames="FormApplySKUDetailID"
                                InsertItemPosition="None">
                                <LayoutTemplate>
                                    <div runat="server" id="itemPlaceholder" />
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="light_window_top">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <col style="width: 580px" />
                                            <col style="width: 150px" />
                                            <col style="width: 150px" />
                                            <col style="width: 220px" align="center" />
                                            <col style="width: 160px" align="center" />
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;产品名称：<asp:Label ID="Label1" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                                                </td>
                                                <td>
                                                    正常供货价：<asp:Label ID="Label2" runat="server" Text='<%# Eval("SupplyPrice") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    促销供货价：<asp:Label ID="Label3" runat="server" Text='<%# Eval("PromotionPrice") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    买：<asp:Label ID="Label4" runat="server" Text='<%# Eval("BuyQuantity") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                                    送:<asp:Label ID="Label5" runat="server" Text='<%# Eval("GiveQuantity") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    预计销售量：<asp:Label ID="Label6" runat="server" Text='<%# Eval("EstimatedSaleVolume") %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="light_window_bottom">
                                        <asp:HiddenField ID="SKUID" runat="server" Value='<%# Eval("FormApplySKUDetailID") %>'
                                            Visible="False" />
                                        <asp:ObjectDataSource ID="odsExpense" runat="server" TypeName="BusinessObjects.SalesApplyBLL"
                                            SelectMethod="GetFormApplyExpenseDetailByFormApplySKUDetailID">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="SKUID" Name="FormApplySKUDetailID" PropertyName="Value"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <gc:GridView ID="gvExpense" runat="server" Width="1000px" ShowFooter="false" CssClass="GridView"
                                            AutoGenerateColumns="False" OnRowDataBound="gvExpense_RowDataBound" DataKeyNames="FormApplyExpenseDetailID"
                                            DataSourceID="odsExpense">
                                            <Columns>
                                                <asp:TemplateField HeaderText="费用项">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'
                                                            Width="450px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="450px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="费用金额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="单箱费用">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPackageUnitPrice" runat="server" Text='<%# Bind("PackageUnitPrice","{0:N}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="重复方案单号">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="Header" />
                                            <EmptyDataTemplate>
                                                <table class="GridView" style="width: 1000px">
                                                    <tr class="Header">
                                                        <td style="width: 600px;" align="center">
                                                            <b>费用项</b>
                                                        </td>
                                                        <td style="width: 200px;" align="center">
                                                            <b>费用金额</b>
                                                        </td>
                                                        <td style="width: 150px;" align="center">
                                                            <b>单箱费用</b>
                                                        </td>
                                                        <td style="width: 150px;" align="center">
                                                            <b>重复方案单号</b>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </gc:GridView>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:ObjectDataSource ID="odsSKU" runat="server" TypeName="BusinessObjects.SalesApplyBLL"
                        SelectMethod="GetFormApplySKUDetailByFormApplyID">
                        <SelectParameters>
                            <asp:Parameter Name="FormApplyID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    <div id="divSplitRate" runat="server" class="title" style="width: 1260px;" visible="false">
        分摊比例表</div>
    <gc:GridView ID="gvSplitRate" runat="server" CssClass="GridView" AutoGenerateColumns="False"
        DataKeyNames="FormApplySplitRateID" CellPadding="0" Visible="false">
        <Columns>
            <asp:TemplateField HeaderText="费用期间">
                <ItemTemplate>
                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Period","{0:yyyy-MM}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="方案金额分摊比例（%）">
                <ItemTemplate>
                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' Width="200"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>' Width="600"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="667" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
    </gc:GridView>
    <br />
    <uc3:APFlowNodes ID="cwfAppCheck" runat="server" />
    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <uc1:YearAndMonthUserControl ID="UCBeginPeriod" runat="server" IsReadOnly="false"
                    IsExpensePeriod="true" />
                &nbsp;<asp:Label ID="lblSignal" runat="server" Text="~~"></asp:Label>&nbsp;
                <uc1:YearAndMonthUserControl ID="UCEndPeriod" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                &nbsp;
                <asp:Button ID="CopyBtn" runat="server" OnClick="CopyBtn_Click" Text="方案书复制" CssClass="button_nor" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="审批" CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="返回" CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="编辑" CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="作废" CssClass="button_nor" />&nbsp;
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function PrintClick() {
            var strWebSiteUrl = '<%=System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"] %>';
            var url = strWebSiteUrl + '/ReportManage/SalesPromotionPrintReport.aspx?FormID=' + '<%=this.ViewState["ObjectId"] %>';
            window.open(url, "_blank", 'dialogHeight: 652px; dialogWidth: 600px; edge: Raised; center: Yes; help: No; resizable: Yes; status: No;');
        }
    </script>
    <uc4:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upButton" />
</asp:Content>
