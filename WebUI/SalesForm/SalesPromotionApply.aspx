<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SaleForm_SalesPromotionApply" Title="方案申请" CodeBehind="SalesPromotionApply.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/SKUControl.ascx" TagName="SKUSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
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
                        员工</div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        职位</div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        部门</div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        入职日期</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        费用期间</div>
                    <div>
                        <asp:TextBox ID="BeginPeriodCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>&nbsp;
                        <asp:TextBox ID="EndPeriodCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        费用小类</div>
                    <div>
                        <asp:TextBox ID="ExpenseSubCategoryCtl" runat="server" CssClass="InputTextReadOnly"
                            ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        客户名称</div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td valign="top" style="width: 200px">
                    <div class="field_title">
                        方案名称<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="txtFormApplyName" MaxLength="50" runat="server" Width="170px"></asp:TextBox></div>
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
                        支付方式<label class="requiredLable">*</label></div>
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
                <td colspan="3" valign="top">
                    <div class="field_title">
                        方案简述</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2" valign="top">
                    <div class="field_title">
                        附件</div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="380px" />
                </td>
                <td valign="top">
                    <div class="field_title">
                        核销要求</div>
                    <div>
                        <asp:CheckBoxList runat="server" ID="chkListReimburseRequirements" RepeatDirection="Horizontal"
                            RepeatColumns="8" RepeatLayout="Flow">
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
                        促销期间<label class="requiredLable">*</label></div>
                    <div>
                        <uc1:UCDateInput ID="UCPromotionBegin" runat="server" IsReadOnly="false" />
                        <asp:Label ID="Label2" runat="server">~~</asp:Label>
                        <uc1:UCDateInput ID="UCPromotionEnd" runat="server" IsReadOnly="false" />
                    </div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        供货期间<label class="requiredLable">*</label></div>
                    <div>
                        <uc1:UCDateInput ID="UCDeliveryBegin" runat="server" IsReadOnly="false" />
                        <asp:Label ID="Label3" runat="server">~~</asp:Label>
                        <uc1:UCDateInput ID="UCDeliveryEnd" runat="server" IsReadOnly="false" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        促销类型<label class="requiredLable">*</label></div>
                    <div>
                        <asp:DropDownList ID="PromotionScopeDDL" runat="server" DataSourceID="odsPromotionScope"
                            DataTextField="PromotionScopeName" DataValueField="PromotionScopeID" Width="170px">
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        促销形式<label class="requiredLable">*</label></div>
                    <div>
                        <asp:DropDownList ID="PromotionTypeDDL" runat="server" DataSourceID="odsPromotionType"
                            DataTextField="PromotionTypeName" DataValueField="PromotionTypeID" Width="170px">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        促销活动</div>
                    <div>
                        <asp:TextBox ID="PromotionDescCtl" runat="server" MaxLength="400" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        陈列形式<label class="requiredLable">*</label></div>
                    <div>
                        <asp:DropDownList ID="ShelfTypeDDL" runat="server" DataSourceID="odsShelfType" DataTextField="ShelfTypeName"
                            DataValueField="ShelfTypeID" Width="170px">
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        第一个月销量<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="FirstVolumeCtl" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        第二个月销量<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="SecondVolumeCtl" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        第三个月销量<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="ThirdVolumeCtl" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
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
        <asp:SqlDataSource ID="odsPromotionScope" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [PromotionScopeID], [PromotionScopeName] FROM [PromotionScope] where IsActive = 1 ">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="odsPromotionType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [PromotionTypeID], [PromotionTypeName] FROM [PromotionType] where IsActive = 1 ">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="odsShelfType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [ShelfTypeID], [ShelfTypeName] FROM [ShelfType] where IsActive = 1 ">
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
    <div class="title" style="width: 1258px;">
        费用明细信息</div>
    <div style="width: 1268px;">
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="ExpenseSubCategoryID" runat="server" Visible="False" />
                    <asp:UpdatePanel ID="upSKU" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                            <asp:ListView runat="server" ID="SKUListView" DataSourceID="odsSKU" DataKeyNames="FormApplySKUDetailID"
                                InsertItemPosition="LastItem" OnDataBound="SKUListView_OnDataBound">
                                <LayoutTemplate>
                                    <table class="Container8">
                                        <tr>
                                            <td>
                                                <label id="lbl1" style="font-weight: bold">
                                                    本次申请费用总计:</label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSum" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div runat="server" id="itemPlaceholder" />
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="light_window_top">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <col style="width: 400px" />
                                            <col style="width: 150px" />
                                            <col style="width: 150px" />
                                            <col style="width: 230px" align="center" />
                                            <col style="width: 160px" align="center" />
                                            <col style="width: 170px" align="center" />
                                            <tr>
                                                <td>
                                                    产品名称：<asp:Label ID="Label1" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                                                </td>
                                                <td>
                                                    正常供货价：<asp:Label ID="Label2" runat="server" Text='<%# Eval("SupplyPrice","{0:N}") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    促销供货价：<asp:Label ID="Label3" runat="server" Text='<%# Eval("PromotionPrice","{0:N}") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    买：<asp:Label ID="Label4" runat="server" Text='<%# Eval("BuyQuantity") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                                    送:<asp:Label ID="Label5" runat="server" Text='<%# Eval("GiveQuantity") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    预计销售量：<asp:Label ID="Label6" runat="server" Text='<%# Eval("EstimatedSaleVolume") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                                        Text="编辑产品"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                        Text="删除产品"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="light_window_bottom">
                                        <asp:HiddenField ID="SKUID" runat="server" Value='<%# Eval("FormApplySKUDetailID") %>'
                                            Visible="False" />
                                        <asp:ObjectDataSource ID="odsExpense" runat="server" TypeName="BusinessObjects.SalesApplyBLL"
                                            SelectMethod="GetFormApplyExpenseDetail" OnObjectCreated="odsExpense_ObjectCreated">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="SKUID" Name="FormApplySKUDetailID" PropertyName="Value"
                                                    Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <gc:GridView ID="gvExpense" runat="server" Width="1100px" ShowFooter="false" CssClass="GridView"
                                            AutoGenerateColumns="False" DataKeyNames="FormApplyExpenseDetailID" DataSourceID="odsExpense"
                                            OnRowDataBound="gvExpense_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="费用项">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="500px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="费用金额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="单箱费用">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPackageUnitPrice" runat="server" Text='<%# Bind("PackageUnitPrice","{0:N}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="删除" Visible="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="Header" />
                                            <EmptyDataTemplate>
                                                <table>
                                                    <tr class="Header">
                                                        <td style="width: 500px;" class="Empty1">
                                                            费用项
                                                        </td>
                                                        <td style="width: 200px;">
                                                            费用金额
                                                        </td>
                                                        <td style="width: 200px;">
                                                            单箱费用
                                                        </td>
                                                        <td style="width: 200px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </gc:GridView>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="light_window_top">
                                        <table cellspacing="0" cellpadding="0" border="0">
                                            <col style="width: 400px" />
                                            <col style="width: 140px" />
                                            <col style="width: 140px" />
                                            <col style="width: 220px" align="center" />
                                            <col style="width: 140px" align="center" />
                                            <col style="width: 230px" align="center" />
                                            <tr>
                                                <td>
                                                    产品名称:&nbsp;
                                                    <uc1:SKUSelect ID="UCSKU" runat="server" SKUID='<%# Bind("SKUID") %>' OnSKUNameTextChanged="SKUDDL_SelectedIndexChanged"
                                                        IsNoClear="true" Width="260px" />
                                                </td>
                                                <td>
                                                    正常供货价:&nbsp;<asp:TextBox ID="SupplyPriceCtl" runat="server" Text='<%# Bind("SupplyPrice") %>'
                                                        Width="50px" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    促销供货价:&nbsp;<asp:TextBox ID="PromotionPriceCtl" runat="server" Text='<%# Bind("PromotionPrice","{0:0.00}") %>'
                                                        Width="50px" MaxLength="15" ReadOnly="<%# IsPromotionReadOnly %>"></asp:TextBox>
                                                </td>
                                                <td>
                                                    买:&nbsp;<asp:TextBox ID="BuyQuantityCtl" runat="server" MaxLength="15" Text='<%# Bind("BuyQuantity") %>'
                                                        Width="40px" ReadOnly="<%# IsGiveReadOnly %>" CausesValidation="<%# IsGiveReadOnly %>"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                    送:&nbsp;<asp:TextBox ID="GiveQuantityCtl" MaxLength="15" runat="server" Text='<%# Bind("GiveQuantity") %>'
                                                        Width="40px" ReadOnly="<%# IsGiveReadOnly %>" CausesValidation="<%# IsGiveReadOnly %>"></asp:TextBox>
                                                </td>
                                                <td>
                                                    预计销售量:&nbsp;<asp:TextBox ID="EstimatedSaleVolumeCtl" runat="server" MaxLength="15"
                                                        Text='<%# Bind("EstimatedSaleVolume") %>' Width="50px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" ValidationGroup="EditSKUValidation"
                                                        CommandName="Update" Text="更新产品"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                        Text="退出编辑"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                                        Text="删除产品"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:RequiredFieldValidator ID="RFV12" runat="server" ControlToValidate="EstimatedSaleVolumeCtl"
                                            Display="None" ErrorMessage="请您输入预计销量！" ValidationGroup="EditSKUValidation"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REV13" runat="server" ControlToValidate="EstimatedSaleVolumeCtl"
                                            ValidationExpression="<%$ Resources:RegularExpressions, Number %>" Display="None"
                                            ErrorMessage="预计销售量格式不正确" ValidationGroup="EditSKUValidation"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator ID="RFV13" runat="server" ControlToValidate="PromotionPriceCtl"
                                            Display="None" ErrorMessage="请您输入促销供货价！" ValidationGroup="EditSKUValidation"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REV14" runat="server" ControlToValidate="PromotionPriceCtl"
                                            ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                            ErrorMessage="促销供货价格式不正确" ValidationGroup="EditSKUValidation"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="REV15" runat="server" ControlToValidate="BuyQuantityCtl"
                                            ValidationExpression="<%$ Resources:RegularExpressions, Number %>" Display="None"
                                            ErrorMessage="买的格式不正确" ValidationGroup="EditSKUValidation"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="REV16" runat="server" ControlToValidate="GiveQuantityCtl"
                                            ValidationExpression="<%$ Resources:RegularExpressions, Number %>" Display="None"
                                            ErrorMessage="送的格式不正确" ValidationGroup="EditSKUValidation"></asp:RegularExpressionValidator>
                                        <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="EditSKUValidation" />
                                    </div>
                                    <div class="light_window_bottom">
                                        <asp:HiddenField ID="SKUID" runat="server" Value='<%# Eval("FormApplySKUDetailID") %>'
                                            Visible="False" />
                                        <asp:ObjectDataSource ID="odsExpense" runat="server" TypeName="BusinessObjects.SalesApplyBLL"
                                            SelectMethod="GetFormApplyExpenseDetail" DeleteMethod="DeleteFormApplyExpenseDetailByID"
                                            InsertMethod="AddFormApplyExpenseDetail" OnInserting="odsExpense_ObjectInserting"
                                            OnObjectCreated="odsExpense_ObjectCreated">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="SKUID" Name="FormApplySKUDetailID" PropertyName="Value"
                                                    Type="Int32" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="FormApplyExpenseDetailID" Type="Int32" />
                                            </DeleteParameters>
                                            <InsertParameters>
                                                <asp:ControlParameter ControlID="SKUID" Name="FormApplySKUDetailID" PropertyName="Value"
                                                    Type="Int32" />
                                                <asp:Parameter Name="ExpenseItemID" Type="Int32" />
                                                <asp:Parameter Name="Amount" Type="Decimal" />
                                            </InsertParameters>
                                        </asp:ObjectDataSource>
                                        <gc:GridView ID="gvExpense" runat="server" Width="1100px" ShowFooter="false" CssClass="GridView"
                                            AutoGenerateColumns="False" DataKeyNames="FormApplyExpenseDetailID" DataSourceID="odsExpense">
                                            <Columns>
                                                <asp:TemplateField HeaderText="费用项">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpenseItemName" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="500px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="费用金额">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="单箱费用">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPackageUnitPrice" runat="server" Text='<%# Bind("PackageUnitPrice","{0:N}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                                            Text="删除"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="Header" />
                                            <EmptyDataTemplate>
                                                <table>
                                                    <tr class="Header">
                                                        <td style="width: 500px;" class="Empty1">
                                                            费用项
                                                        </td>
                                                        <td style="width: 200px;">
                                                            费用金额
                                                        </td>
                                                        <td style="width: 200px;">
                                                            单箱费用
                                                        </td>
                                                        <td style="width: 200px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </EmptyDataTemplate>
                                        </gc:GridView>
                                        <asp:FormView ID="fvExpense" runat="server" DataKeyNames="ID" DataSourceID="odsExpense"
                                            DefaultMode="Insert" Width="1100px">
                                            <InsertItemTemplate>
                                                <table class="add_box" width="1100px">
                                                    <tr style="height: 30px">
                                                        <td style="width: 500px">
                                                            <asp:DropDownList ID="newExpenseItemDDL" runat="server" DataSourceID="odsNewExpenseItem"
                                                                DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" Width="450px"
                                                                SelectedValue='<%# Bind("ExpenseItemID") %>'>
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="odsNewExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                                                SelectCommand="SELECT [ExpenseItemID], [AccountingCode]+'--'+[ExpenseItemName] ExpenseItemName FROM [ExpenseItem] where IsActive = 1 and ExpenseSubCategoryID = @ExpenseSubCategoryID order by ExpenseItemName">
                                                                <SelectParameters>
                                                                    <asp:ControlParameter ControlID="ExpenseSubCategoryID" Name="ExpenseSubCategoryID"
                                                                        PropertyName="Value" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:TextBox ID="newAmountCtl" runat="server" MaxLength="15" Text='<%# Bind("Amount") %>'
                                                                Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 200px">
                                                            <asp:TextBox ID="newPackageUnitPriceCtl" runat="server" MaxLength="15" ReadOnly="true"
                                                                Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 200px;" align="center">
                                                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" ValidationGroup="newExpenseValidation"
                                                                CommandName="Insert" Text="添加费用项"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <asp:RequiredFieldValidator ID="RFV22" runat="server" ControlToValidate="newAmountCtl"
                                                        Display="None" ErrorMessage="请您输入费用金额！" ValidationGroup="newExpenseValidation"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REV23" runat="server" ControlToValidate="newAmountCtl"
                                                        Display="None" ValidationExpression="<%$ Resources:RegularExpressions, Money %>"
                                                        ErrorMessage="费用金额格式不正确" ValidationGroup="newExpenseValidation"></asp:RegularExpressionValidator>
                                                    <asp:ValidationSummary ID="ExpenseValidationSummaryINS" runat="server" ShowMessageBox="True"
                                                        ShowSummary="False" ValidationGroup="newExpenseValidation" />
                                                </table>
                                                <br />
                                            </InsertItemTemplate>
                                        </asp:FormView>
                                    </div>
                                </EditItemTemplate>
                                <InsertItemTemplate>
                                    <div>
                                        <table class="simple_box" cellspacing="0" cellpadding="0" border="0" width="1260px">
                                            <col style="width: 400px" />
                                            <col style="width: 150px" />
                                            <col style="width: 150px" />
                                            <col style="width: 230px" align="center" />
                                            <col style="width: 160px" align="center" />
                                            <col style="width: 170px" align="center" />
                                            <tr style="height: 30px">
                                                <td>
                                                    产品名称:
                                                    <uc1:SKUSelect AutoPostBack="true" ID="UCSKU" OnSKUNameTextChanged="NewSKUDDL_SelectedIndexChanged"
                                                        Width="280" runat="server" IsNoClear="true" />
                                                </td>
                                                <td>
                                                    正常供货价:&nbsp;<asp:TextBox ID="NewSupplyPriceCtl" runat="server" Text='<%# Bind("SupplyPrice") %>'
                                                        Width="50px" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    促销供货价:&nbsp;<asp:TextBox ID="NewPromotionPriceCtl" runat="server" MaxLength="15"
                                                        Width="50px" ReadOnly="<%# IsPromotionReadOnly %>"></asp:TextBox>
                                                </td>
                                                <td>
                                                    买:&nbsp;<asp:TextBox ID="NewBuyQuantityCtl" runat="server" MaxLengt="15" Width="40px"
                                                        ReadOnly="<%# IsGiveReadOnly %>"></asp:TextBox>&nbsp;&nbsp;&nbsp; 送:&nbsp;<asp:TextBox
                                                            ID="NewGiveQuantityCtl" runat="server" MaxLength="15" Width="40px" ReadOnly="<%# IsGiveReadOnly %>"></asp:TextBox>
                                                </td>
                                                <td>
                                                    预计销售量:<asp:TextBox ID="NewEstimatedSaleVolumeCtl" runat="server" MaxLength="15" Text='<%# Bind("EstimatedSaleVolume") %>'
                                                        Width="50px"></asp:TextBox>
                                                    <td>
                                                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                            Text="添加产品信息" ValidationGroup="NewSKUValidation"></asp:LinkButton>
                                                    </td>
                                            </tr>
                                        </table>
                                        <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="NewEstimatedSaleVolumeCtl"
                                            Display="None" ErrorMessage="请您输入预计销量！" ValidationGroup="NewSKUValidation"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REV3" runat="server" ControlToValidate="NewEstimatedSaleVolumeCtl"
                                            ValidationExpression="<%$ Resources:RegularExpressions, Number %>" Display="None"
                                            ErrorMessage="预计销售量格式不正确" ValidationGroup="NewSKUValidation"></asp:RegularExpressionValidator>
                                        <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ShowMessageBox="True"
                                            ShowSummary="False" ValidationGroup="NewSKUValidation" />
                                    </div>
                                </InsertItemTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:ObjectDataSource ID="odsSKU" runat="server" TypeName="BusinessObjects.SalesApplyBLL"
                        SelectMethod="GetFormApplySKUDetail" InsertMethod="AddFormApplySKUDetail" UpdateMethod="UpdateFormApplySKUDetail"
                        DeleteMethod="DeleteFormApplySKUDetailByID" OnObjectCreated="odsSKU_ObjectCreated"
                        OnUpdating="odsSKU_Updating" OnInserting="odsSKU_Inserting" OnInserted="odsSKU_Inserted">
                        <DeleteParameters>
                            <asp:Parameter Name="FormApplySKUDetailID" Type="Int32" />
                        </DeleteParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="FormApplySKUDetailID" Type="Int32" />
                            <asp:Parameter Name="SKUID" Type="Int32" />
                            <asp:Parameter Name="SupplyPrice" Type="Decimal" />
                            <asp:Parameter Name="PromotionPrice" Type="Decimal" />
                            <asp:Parameter Name="BuyQuantity" Type="Int32" />
                            <asp:Parameter Name="GiveQuantity" Type="Int32" />
                            <asp:Parameter Name="EstimatedSaleVolume" Type="Int32" />
                        </UpdateParameters>
                        <InsertParameters>
                            <asp:Parameter Name="FormApplyID" Type="Int32" />
                            <asp:Parameter Name="SKUID" Type="Int32" />
                            <asp:Parameter Name="SupplyPrice" Type="Decimal" />
                            <asp:Parameter Name="PromotionPrice" Type="Decimal" />
                            <asp:Parameter Name="BuyQuantity" Type="Int32" />
                            <asp:Parameter Name="GiveQuantity" Type="Int32" />
                            <asp:Parameter Name="EstimatedSaleVolume" Type="Int32" />
                        </InsertParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    <div id="divSplitRate" runat="server" class="title" style="width: 1260px;" visible="false">
        分摊比例表</div>
    <gc:GridView ID="gvSplitRate" runat="server" CssClass="GridView"
        AutoGenerateColumns="False" DataKeyNames="FormApplySplitRateID" CellPadding="0"
        Visible="false">
        <Columns>
            <asp:TemplateField HeaderText="费用期间">
                <ItemTemplate>
                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Period","{0:yyyy-MM}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="方案金额分摊比例（%）">
                <ItemTemplate>
                    <asp:TextBox ID="txtRate" runat="server" Text='<%# Bind("Rate") %>' Width="200"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' Width="600"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="667" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
    </gc:GridView>
    <br />
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
