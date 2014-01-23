<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SaleForm_SalesGeneralApply" Title="方案申请" CodeBehind="SalesGeneralApply.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/SKUControl.ascx" TagName="SKUSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetPackageUnitPrice(PackageUnit_ID, obj) {
            var txtPackageUnit = document.getElementById(PackageUnit_ID);
            var totalFee;
            var j;
            for (j = 2; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvApplyDetails_ctl" + GetTBitNum(j) + "_lblAmount")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvApplyDetails_ctl" + GetTBitNum(j) + "_lblAmount").innerText);
                    break;
                }
            }
            if (totalFee && totalFee != "" && obj.value && obj.value != "" && !isNaN(obj.value) && !isNaN(totalFee)) {
                txtPackageUnit.value = commafy((totalFee / obj.value).toFixed(2));
            }
        }

        function commafy(num) {
            num = num + "";
            var re = /(-?\d+)(\d{3})/
            while (re.test(num)) {
                num = num.replace(re, "$1,$2")
            }
            return num;
        }
        function uncommafy(str) {
            str = str + "";
            var re = /\,/g;
            str = str.replace(re, '')
            return str
        }
        function GetTBitNum(num) {
            if (num < 10) {
                num = "0" + String(num);
            }
            return num;
        }
        
    </script>
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
                        <asp:TextBox ID="ContractNoCtl" runat="server" MaxLength="20" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        方案简述</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" TextMode="multiline" Height="60px" Columns="80"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2" valign="top">
                    <div class="field_title">
                        附件</div>
                    <div>
                        <uc2:UCFlie ID="UCFileUpload" runat="server" Width="380px" />
                    </div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        核销要求<label class="requiredLable">*</label></div>
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
            </tr>
            <tr>
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
                    <div class="field_title">
                        预计销售量<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="txtEstimatedSaleVolume" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        单箱费用</div>
                    <div>
                        <asp:TextBox ID="txtPackageUnitPrice" MaxLength="15" style="color:#ea0000;" runat="server" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <asp:RegularExpressionValidator ID="RF1" runat="server" ControlToValidate="FirstVolumeCtl"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="请输入数字" ValidationGroup="VolumeCtl"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="SecondVolumeCtl"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="请输入数字" ValidationGroup="VolumeCtl"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="ThirdVolumeCtl"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="请输入数字" ValidationGroup="VolumeCtl"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryVolumeCtl" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="VolumeCtl" />
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
    <div class="title" style="width: 1260px;">
        费用明细信息</div>
    <asp:HiddenField ID="ExpenseSubCategoryID" runat="server" Visible="False" />
    <asp:UpdatePanel ID="upApplyDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvApplyDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormApplySKUDetailID" DataSourceID="odsApplyDetails"
                OnRowDataBound="gvApplyDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="产品名称">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="500px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用项">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="申请金额">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 500px;" class="Empty1">
                                产品名称
                            </td>
                            <td style="width: 300px;">
                                费用项
                            </td>
                            <td style="width: 100px;">
                                申请金额
                            </td>
                            <td style="width: 300px;">
                                备注
                            </td>
                            <td style="width: 65px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvApplyDetails" runat="server" DataKeyNames="FormApplySKUDetailID"
                DataSourceID="odsApplyDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 500px;" align="center">
                                <uc1:SKUSelect ID="UCSKU" runat="server" IsNoClear="true" Width="410px" />
                            </td>
                            <td style="width: 300px;" align="center">
                                <asp:DropDownList ID="newExpenseItemDDL" runat="server" DataSourceID="odsNewExpenseItem"
                                    DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" Width="280px"
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
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newAmountCtl" MaxLength="15" runat="server" Text='<%# Bind("Amount") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 300px;" align="center">
                                <asp:TextBox ID="newRemarkCtl" MaxLength="20" runat="server" Text='<%# Bind("Remark") %>'
                                    Width="270px"></asp:TextBox>
                            </td>
                            <td style="width: 65px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="添加" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="newAmountCtl"
                                Display="None" ErrorMessage="请录入数量！" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="newExpenseItemDDL"
                                Display="None" ErrorMessage="请选择费用项！" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="newAmountCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="请输入数字" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
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
    <asp:ObjectDataSource ID="odsApplyDetails" runat="server" DeleteMethod="DeleteFormApplyDetailByID"
        SelectMethod="GetFormApplyDetailView" TypeName="BusinessObjects.SalesApplyBLL"
        OnInserting="odsApplyDetails_Inserting" OnObjectCreated="odsApplyDetails_ObjectCreated"
        InsertMethod="AddFormApplyDetailView">
        <DeleteParameters>
            <asp:Parameter Name="FormApplySKUDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormApplyID" Type="Int32" />
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="ExpenseItemID" Type="Int32" />
            <asp:Parameter Name="Amount" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:ImageButton ID="SubmitBtn" runat="server" ImageUrl="../images/btnSubmit.gif"
                    UseSubmitBehavior="False" OnClick="SubmitBtn_Click" Text="提交" />
                <asp:ImageButton ID="SaveBtn" runat="server" ImageUrl="../images/btnSave.gif" OnClick="SaveBtn_Click"
                    Text="保存" ValidationGroup="VolumeCtl" />
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
