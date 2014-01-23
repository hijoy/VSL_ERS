<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SaleForm_ReimburseMoneyApply" Title="方案报销" CodeBehind="ReimburseMoneyApply.aspx.cs" %>

<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function MinusTotal(lblName, obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName)) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText);
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
                if (document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName)) {
                    document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText = commafy(lastTotal.toFixed(2));
                }
            }
        }

        function PlusTotal(lblName, obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName)) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText);
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

                document.all("ctl00_ContentPlaceHolder1_gvReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText = commafy(lastTotal.toFixed(2));
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
                        <asp:TextBox ID="RemarkCtl" runat="server" MaxLength="800" Width="550px" TextMode="multiline"
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
            SelectCommand=" SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] where IsActive = 1 And PaymentTypeID != 4">
        </asp:SqlDataSource>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        发票信息</div>
    <asp:UpdatePanel ID="upInvoice" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvInvoice" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormReimburseInvoiceID" DataSourceID="odsInvoice"
                OnRowDataBound="gvInvoice_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="发票号码">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="223px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发票金额">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceAmount" runat="server" Text='<%# Eval("InvoiceAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="重复方案号">
                        <ItemTemplate>
                            <asp:Label ID="lblSystemInfo" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 223px;" class="Empty1">
                                发票号码
                            </td>
                            <td style="width: 150px;">
                                发票金额
                            </td>
                            <td style="width: 400px;">
                                备注
                            </td>
                            <td style="width: 400px;">
                                系统消息
                            </td>
                            <td style="width: 90px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
            <asp:FormView ID="fvInvoice" runat="server" DataKeyNames="FormReimburseInvoiceID"
                DataSourceID="odsInvoice" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 223px;" align="center">
                                <asp:TextBox ID="txtInvoiceNo" runat="server" MaxLength="20" Width="190px" Text='<%# Bind("InvoiceNo") %>'></asp:TextBox>
                            </td>
                            <td style="width: 150px;" align="center">
                                <asp:TextBox ID="txtAmount" runat="server" MaxLength="15" Width="120px" Text='<%# Bind("InvoiceAmount") %>'></asp:TextBox>
                            </td>
                            <td style="width: 400px;" align="center">
                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="200" Width="370px" Text='<%# Bind("Remark") %>'></asp:TextBox>
                            </td>
                            <td style="width: 400px;" align="center">
                            </td>
                            <td style="width: 90px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="添加" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="txtInvoiceNo"
                                ErrorMessage="请录入发票号码！" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="txtAmount"
                                ErrorMessage="请填写报销金额！" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmount"
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
    <asp:ObjectDataSource ID="odsInvoice" runat="server" DeleteMethod="DeleteFormReimburseInvoiceByID"
        SelectMethod="GetFormReimburseInvoice" TypeName="BusinessObjects.SalesReimburseBLL"
        InsertMethod="AddFormReimburseInvoice" OnObjectCreated="odsInvoice_ObjectCreated">
        <DeleteParameters>
            <asp:Parameter Name="FormReimburseInvoiceID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormReimburseID" Type="Int32" />
            <asp:Parameter Name="InvoiceNo" Type="String" />
            <asp:Parameter Name="InvoiceAmount" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
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
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用期间">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("ApplyPeriod","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="支付方式">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyPaymentType" runat="server" Text='<%# GetPaymentTypeNameByID(Eval("ApplyPaymentTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="门店">
                        <ItemTemplate>
                            <asp:Label ID="lblShop" runat="server" Text='<%# GetShopNameByID(Eval("ShopID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="产品">
                        <ItemTemplate>
                            <asp:Label ID="lblSku" runat="server" Text='<%# GetSKUNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="318px" />
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
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="applbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="可报销金额">
                        <ItemTemplate>
                            <asp:Label ID="lblRemainAmount" runat="server" Text='<%# Eval("RemainAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="Remainlbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="预付金额" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPrePaidAmount" MaxLength="15" runat="server" Width="60px" Text='<%# Eval("PrePaidAmount") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="totallblPrePaid" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="税前金额">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmount" MaxLength="15" runat="server" Width="60px" Text='<%# Eval("Amount") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="totallblBeforeTax" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="税金">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTaxAmount" MaxLength="15" runat="server" Width="60px" Text='<%# Eval("TaxAmount") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="80px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="totallblTax" runat="server"></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upCustomer" />
    <br />
</asp:Content>
