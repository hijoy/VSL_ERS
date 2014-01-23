<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SaleForm_ReimburseGoodsApply" Title="方案报销" CodeBehind="ReimburseGoodsApply.aspx.cs" %>

<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ParameterChanged() {
            var txtQuantity = document.all("ctl00$ContentPlaceHolder1$fvSKUDetails$newQuantityCtl").value;
            var txtPrice = document.all("ctl00$ContentPlaceHolder1$fvSKUDetails$newUnitPriceCtl").value;
            if (txtQuantity != "" && isNaN(txtQuantity)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvSKUDetails$newQuantityCtl").focus(); }, 0);
                return false;
            }
            if (txtPrice != "" && isNaN(txtPrice)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvSKUDetails$newUnitPriceCtl").focus(); }, 0);
                return false;
            }
            if (!isNaN(parseFloat(txtQuantity)) && !isNaN(parseFloat(txtPrice))) {
                var quantity = parseFloat(txtQuantity);
                var price = parseFloat(txtPrice);
                var amount = quantity * price;
                document.all("ctl00$ContentPlaceHolder1$fvSKUDetails$newTotalCtl").value = commafy(amount.toFixed(2));
            }
        }
        function MinusTotal(obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_totallbl")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_totallbl").innerText);
                    break;
                }
            }
            if (obj.value != "" && isNaN(obj.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            //计算数据
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) - parseFloat(obj.value);
                if (document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_totallbl")) {
                    document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_totallbl").innerText = commafy(lastTotal.toFixed(2));
                }
            }
        }

        function PlusTotal(obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_totallbl")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_totallbl").innerText);
                    break;
                }
            }
            if (obj.value != "" && isNaN(obj.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            //计算数据
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) + parseFloat(obj.value);

                document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_totallbl").innerText = commafy(lastTotal.toFixed(2));
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
                <td style="width: 200px">
                    <div class="field_title">
                        客户名称</div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        支付方式</div>
                    <div>
                        <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                            DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="170px"
                            Enabled="false">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        备注</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" Width="550px" MaxLength="800" TextMode="multiline"
                            Height="60px"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        附件</div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] where IsActive = 1 and PaymentTypeID = 4 ">
        </asp:SqlDataSource>
    </div>
    <br />
    <div class="title" style="width: 1258px;">
        报销明细信息</div>
    <asp:UpdatePanel ID="upReimburseDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvReimburseDetails" runat="server" ShowFooter="True" CssClass="GridView"
                OnRowDataBound="gvReimburseDetails_RowDataBound" AutoGenerateColumns="False"
                DataKeyNames="FormReimburseDetailID" DataSourceID="odsReimburseDetails" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="FormApplyExpenseDetailID" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblFormApplyExpenseDetailID" runat="server" Text='<%# Bind("FormApplyExpenseDetailID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="申请单编号">
                        <ItemTemplate>
                            <asp:HyperLink ID="lblApplyFormNo" runat="server" Text='<%# Eval("ApplyFormNo") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="119px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用期间">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("ApplyPeriod","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="支付方式">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyPaymentType" runat="server" Text='<%# GetPaymentTypeNameByID(Eval("ApplyPaymentTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="门店">
                        <ItemTemplate>
                            <asp:Label ID="lblShop" runat="server" Text='<%# GetShopNameByID(Eval("ShopID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="产品">
                        <ItemTemplate>
                            <asp:Label ID="lblSku" runat="server" Text='<%# GetSKUNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="300px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用项">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                        <FooterStyle HorizontalAlign="Center" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="sumlbl" runat="server" Text="合计"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="确认金额">
                        <ItemTemplate>
                            <asp:Label ID="lblAccruedAmount" runat="server" Text='<%# Eval("AccruedAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="applbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="可报销金额">
                        <ItemTemplate>
                            <asp:Label ID="lblRemainAmount" runat="server" Text='<%# Eval("RemainAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="Remainlbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="本次报销金额">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" Width="100px" Text='<%# Eval("Amount") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="totallbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsReimburseDetails" runat="server" SelectMethod="GetFormReimburseDetail"
        TypeName="BusinessObjects.SalesReimburseBLL" OnObjectCreated="odsReimburseDetails_ObjectCreated">
    </asp:ObjectDataSource>
    <br />
    <div class="title" style="width: 1258px">
        领用产品信息</div>
    <asp:UpdatePanel ID="upSKUDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvSKUDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormReimburseSKUDetailID" DataSourceID="odsSKUDetails"
                OnRowDataBound="gvSKUDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="产品">
                        <ItemTemplate>
                            <asp:Label ID="lblSKUName" runat="server" Text='<%# GetSKUInfoByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="501px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="箱数量">
                        <ItemTemplate>
                            <asp:Label ID="lblPackageQuantity" runat="server" Text='<%# Eval("PackageQuantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="供货价格">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="领用箱数">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <FooterStyle HorizontalAlign="Center" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="sumskulbl" runat="server" Text="合计"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="金额">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="totalskulbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="320px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 501px;" class="Empty1">
                                产品
                            </td>
                            <td style="width: 60px;">
                                箱数量
                            </td>
                            <td style="width: 100px;">
                                供货价格
                            </td>
                            <td style="width: 100px;">
                                领用箱数
                            </td>
                            <td style="width: 100px;">
                                金额
                            </td>
                            <td style="width: 320px;">
                                备注
                            </td>
                            <td style="width: 80px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvSKUDetails" runat="server" DataKeyNames="FormReimburseSKUDetailID"
                DataSourceID="odsSKUDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 501px;" align="center">
                                <asp:DropDownList ID="NewSKUDDL" runat="server" DataSourceID="odsNewSKU" DataTextField="SKUName"
                                    DataValueField="SKUID" Width="470px" SelectedValue='<%# Bind("SKUID") %>' OnSelectedIndexChanged="NewSKUDDL_OnSelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsNewSKU" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="Select 0 SKUID,' 请选择产品' SKUName Union SELECT SKUID, SKUNo+'-'+SKUName+'-'+Spec as SKUName FROM [SKU] where IsActive = 1 order by SKUName">
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 60px;" align="center">
                                <asp:TextBox ID="newPackageQuantityCtl" runat="server" Width="40px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:TextBox ID="newUnitPriceCtl" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:TextBox ID="newQuantityCtl" runat="server" MaxLength="15" Text='<%# Bind("Quantity") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:TextBox ID="newTotalCtl" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 320px;" align="center">
                                <asp:TextBox ID="newRemarkCtl" runat="server" MaxLength="200" Text='<%# Bind("Remark") %>'
                                    Width="300px"></asp:TextBox>
                            </td>
                            <td style="width: 80px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="添加" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="newQuantityCtl"
                                Display="None" ErrorMessage="请录入数量！" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="newQuantityCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Double %>" Display="None"
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
    <asp:ObjectDataSource ID="odsSKUDetails" runat="server" DeleteMethod="DeleteFormReimburseSKUDetailByID"
        SelectMethod="GetFormReimburseSKUDetail" TypeName="BusinessObjects.SalesReimburseBLL"
        OnInserting="odsSKUDetails_Inserting" OnObjectCreated="odsSKUDetails_ObjectCreated"
        InsertMethod="AddFormReimburseSKUDetail">
        <DeleteParameters>
            <asp:Parameter Name="FormReimburseSKUDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormReimburseID" Type="Int32" />
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="UnitPrice" Type="Decimal" />
            <asp:Parameter Name="Quantity" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:ImageButton ID="SubmitBtn" runat="server" ImageUrl="../images/btnSubmit.gif"
                    OnClick="SubmitBtn_Click" />
                <asp:ImageButton ID="SaveBtn" runat="server" ImageUrl="../images/btnSave.gif" OnClick="SaveBtn_Click"
                    Text="保存" />
                <asp:ImageButton ID="CancelBtn" runat="server" ImageUrl="../images/btnCancel.gif"
                    OnClick="CancelBtn_Click" />
                <asp:ImageButton ID="DeleteBtn" runat="server" ImageUrl="../images/btnDelete.gif"
                    OnClick="DeleteBtn_Click" Text="删除" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upCustomer" />
</asp:Content>
