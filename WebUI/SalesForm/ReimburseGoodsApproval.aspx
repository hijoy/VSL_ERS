<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SaleForm_ReimburseGoodsApproval" Title="������������" CodeBehind="ReimburseGoodsApproval.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ParameterChanged() {
            var txtQuantity = document.all("ctl00$ContentPlaceHolder1$fvSKUDetails$newQuantityCtl").value;
            var txtPrice = document.all("ctl00$ContentPlaceHolder1$fvSKUDetails$newUnitPriceCtl").value;
            if (txtQuantity != "" && isNaN(txtQuantity)) {
                alert("��¼������");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvSKUDetails$newQuantityCtl").focus(); }, 0);
                return false;
            }
            if (txtPrice != "" && isNaN(txtPrice)) {
                alert("��¼������");
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
                if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_totalRealskulbl")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_totalRealskulbl").innerText);
                    break;
                }
            }
            if (obj.value != "" && isNaN(obj.value)) {
                alert("��¼������");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            //��������
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) - parseFloat(obj.value);
                if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_totalRealskulbl")) {
                    document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_totalRealskulbl").innerText = commafy(lastTotal.toFixed(2));
                }
            }
        }

        function PlusTotal(obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_totalRealskulbl")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_totalRealskulbl").innerText);
                    break;
                }
            }
            if (obj.value != "" && isNaN(obj.value)) {
                alert("��¼������");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            //��������
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) + parseFloat(obj.value);
                document.all("ctl00_ContentPlaceHolder1_gvSKUDetails_ctl" + GetTBitNum(j) + "_totalRealskulbl").innerText = commafy(lastTotal.toFixed(2));
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
        ������Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        ���ݱ��</div>
                    <div>
                        <asp:TextBox ID="FormNoCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��������</div>
                    <div>
                        <asp:TextBox ID="ApplyDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        Ա��</div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ְλ</div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ����</div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ְ����</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        �ͻ�����</div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ͻ�����</div>
                    <div>
                        <asp:TextBox ID="CustomerTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ֧����ʽ</div>
                    <div>
                        <asp:TextBox ID="PaymentTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td>
                    <div class="field_title">
                        ��ʷ����</div>
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
                        ��ע</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" Width="550px" TextMode="multiline" ReadOnly="true"
                            Height="60px"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" IsView="true" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px;">
        ������ϸ��Ϣ</div>
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
                    <asp:TemplateField HeaderText="���뵥���">
                        <ItemTemplate>
                            <asp:HyperLink ID="lblApplyFormNo" runat="server" Text='<%# Eval("ApplyFormNo") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="119px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����ڼ�">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("ApplyPeriod","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="֧����ʽ">
                        <ItemTemplate>
                            <asp:Label ID="lblApplyPaymentType" runat="server" Text='<%# GetPaymentTypeNameByID(Eval("ApplyPaymentTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�ŵ�">
                        <ItemTemplate>
                            <asp:Label ID="lblShop" runat="server" Text='<%# GetShopNameByID(Eval("ShopID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��Ʒ">
                        <ItemTemplate>
                            <asp:Label ID="lblSku" runat="server" Text='<%# GetSKUNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="300px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="������">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ȷ�Ͻ��">
                        <ItemTemplate>
                            <asp:Label ID="lblAccruedAmount" runat="server" Text='<%# Eval("AccruedAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�ɱ������">
                        <ItemTemplate>
                            <asp:Label ID="lblRemainAmount" runat="server" Text='<%# Eval("RemainAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="���α������">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsReimburseDetails" runat="server" SelectMethod="GetFormReimburseDetailByFormReimburseD"
        TypeName="BusinessObjects.SalesReimburseBLL">
        <SelectParameters>
            <asp:Parameter Name="FormReimburseID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div class="title" style="width: 1258px">
        ���ò�Ʒ��Ϣ</div>
    <asp:UpdatePanel ID="upSKUDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvSKUDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormReimburseSKUDetailID" DataSourceID="odsSKUDetails"
                OnRowDataBound="gvSKUDetails_RowDataBound" CellPadding="0" OnSelectedIndexChanged="gvSKUDetails_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="��Ʒ">
                        <ItemTemplate>
                            <asp:Label ID="lblSKUName" runat="server" Text='<%# GetSKUInfoByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="501px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="������">
                        <ItemTemplate>
                            <asp:Label ID="lblPackageQuantity" runat="server" Text='<%# Eval("PackageQuantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����۸�">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="sumskulbl" runat="server" Text="�ϼ�"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="���">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="totalskulbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ע">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="300px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbViewDelivery" runat="server" CausesValidation="False" CommandName="Select"
                                Text="������Ϣ"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 501px;" class="Empty1">
                                ��Ʒ
                            </td>
                            <td style="width: 60px;">
                                ������
                            </td>
                            <td style="width: 100px;">
                                �����۸�
                            </td>
                            <td style="width: 100px;">
                                ��������
                            </td>
                            <td style="width: 100px;">
                                ���
                            </td>
                            <td style="width: 300px;">
                                ��ע
                            </td>
                            <td style="width: 100px;">
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsSKUDetails" runat="server" SelectMethod="GetFormReimburseSKUDetailByFormReimburseID"
        TypeName="BusinessObjects.SalesReimburseBLL">
        <SelectParameters>
            <asp:Parameter Name="FormReimburseID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div id="DeliveryDIV" runat="server" class="title" style="width: 1258px;">
        ������Ϣ</div>
    <asp:UpdatePanel ID="upDelivery" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvDelivery" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormReimburseDeliveryID" DataSourceID="odsDelivery"
                OnRowDataBound="gvDelivery_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryNo" runat="server" Text='<%# Eval("DeliveryNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                        <FooterTemplate>
                            <asp:Label ID="sumskulbl" runat="server" Text="�ϼ�"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryQuantity" runat="server" Text='<%# Eval("DeliveryQuantity") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="lblTotalDeliveryQuantity" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�������">
                        <ItemTemplate>
                            <asp:Label ID="lblDeliveryAmount" runat="server" Text='<%# Eval("DeliveryAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="150px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="lblTotalDeliveryAmount" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ע">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="500px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="ɾ��" Visible="<%# HasManageRight %>"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 200px;" class="Empty1">
                                ��������
                            </td>
                            <td style="width: 200px;">
                                ��������
                            </td>
                            <td style="width: 150px;">
                                ��������
                            </td>
                            <td style="width: 150px;">
                                �������
                            </td>
                            <td style="width: 500px;">
                                ��ע
                            </td>
                            <td style="width: 60px;">
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvDelievery" runat="server" DataKeyNames="FormReimburseDeliveryID"
                DataSourceID="odsDelivery" DefaultMode="Insert" CellPadding="0" Visible="<%# HasManageRight %>">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 200px;" align="center">
                                <asp:TextBox ID="newDeliveryNoCtl" runat="server" Text='<%# Bind("DeliveryNo") %>'
                                    Width="180px"></asp:TextBox>
                            </td>
                            <td style="width: 200px;" align="center">
                                <uc3:UCDateInput ID="newUCDelieveryDate" runat="server" SelectedDate='<%#Bind("DeliveryDate","{0:yyyy-MM-dd}")%>' />
                            </td>
                            <td style="width: 150px;" align="center">
                                <asp:TextBox ID="newDeliveryQuantityCtl" runat="server" Text='<%# Bind("DeliveryQuantity") %>'
                                    Width="130px"></asp:TextBox>
                            </td>
                            <td style="width: 150px;" align="center">
                                <asp:TextBox ID="TextBox1" runat="server" Width="130px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 500px;" align="center">
                                <asp:TextBox ID="newRemarkCtl" runat="server" Text='<%# Bind("Remark") %>' Width="470px"></asp:TextBox>
                            </td>
                            <td style="width: 60px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="���" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="newDeliveryNoCtl"
                                Display="None" ErrorMessage="��¼�뷢�����ţ�" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="newUCDelieveryDate$txtDate"
                                ErrorMessage="��ѡ�񷢻����ڣ�" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="newDeliveryQuantityCtl"
                                Display="None" ErrorMessage="��¼�뷢��������" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="newDeliveryQuantityCtl"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="����������" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsDelivery" runat="server" DeleteMethod="DeleteFormReimburseDeliveryByID"
        SelectMethod="GetFormReimburseDeliveryByFormReimburseSKUDetailID" TypeName="BusinessObjects.SalesReimburseBLL"
        OnInserting="odsDelivery_inserting" InsertMethod="AddFormReimburseDelivery">
        <SelectParameters>
            <asp:Parameter Name="FormReimburseSKUDetailID" Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="FormReimburseDeliveryID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormReimburseSKUDetailID" Type="Int32" />
            <asp:Parameter Name="DeliveryNo" Type="String" />
            <asp:Parameter Name="DeliveryQuantity" Type="Decimal" />
            <asp:Parameter Name="DeliveryDate" Type="Datetime" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <br />
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="btnDeliveryComplete" runat="server" CssClass="button_nor" OnClick="btnDeliveryComplete_Click"
                    Text="�������" />
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                    Text="����" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="����" />
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="�༭" CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" CssClass="button_nor" OnClick="ScrapBtn_Click"
                    Text="����" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc4:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
