<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SaleForm_SalesPromotionExecution" Title="������������" CodeBehind="SalesPromotionExecution.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../Script/js.js"></script>
    <script type="text/javascript" src="../Script/DateInput.js"></script>
    <script language="javascript" type="text/javascript">

        function MinusTotal(obj) {
            var totalFee;
            if (document.all("ctl00_ContentPlaceHolder1_SKUListView_lblSum")) {
                totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_SKUListView_lblSum").innerText);
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
                if (document.all("ctl00_ContentPlaceHolder1_SKUListView_lblSum")) {
                    document.all("ctl00_ContentPlaceHolder1_SKUListView_lblSum").innerText = commafy(lastTotal.toFixed(2));
                }
            }
        }

        function PlusTotal(obj) {
            var totalFee;
            if (document.all("ctl00_ContentPlaceHolder1_SKUListView_lblSum")) {
                totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_SKUListView_lblSum").innerText);
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
                document.all("ctl00_ContentPlaceHolder1_SKUListView_lblSum").innerText = commafy(lastTotal.toFixed(2));
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
        ������Ϣ
        <asp:Label ID="PrintInfor" runat="server" Style="padding-left: 900px;"></asp:Label>&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="PrintBtn" runat="server" OnClick="PrintBtn_Click" OnClientClick="PrintClick()"
            Text="��ӡ" />&nbsp;
    </div>
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
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        �����ڼ�</div>
                    <div>
                        <asp:TextBox ID="BeginPeriodCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>&nbsp;
                        <asp:TextBox ID="EndPeriodCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ����С��</div>
                    <div>
                        <asp:TextBox ID="ExpenseSubCategoryCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ͻ�����</div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <%--<td style="width: 200px">
                    <div class="field_title">
                        �ͻ�����</div>
                    <div>
                        <asp:TextBox ID="CustomerTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>--%>
                <td valign="top" style="width: 200px">
                    <div class="field_title">
                        ��������</div>
                    <div>
                        <asp:TextBox ID="txtFormApplyName" ReadOnly="true" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ŵ�</div>
                    <div>
                        <asp:TextBox ID="ShopNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        ֧����ʽ</div>
                    <div>
                        <asp:TextBox ID="PaymentTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ͬ���</div>
                    <div>
                        <asp:TextBox ID="ContractNoCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td>
                    <div class="field_title">
                        ����������</div>
                    <div>
                        <asp:TextBox ID="AmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td>
                    <div class="field_title">
                        ȷ��ִ������</div>
                    <div>
                        <asp:TextBox ID="ConfirmCompleteDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td>
                    <div class="field_title">
                        Ԥ������ڼ�</div>
                    <div>
                        <asp:TextBox ID="AccruedPeriodCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td>
                    <div class="field_title">
                        ʵ�ʷ��ý��</div>
                    <div>
                        <asp:TextBox ID="AccruedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div class="field_title">
                        ��������</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" ReadOnly="true" TextMode="multiline" Height="60px"
                            Columns="75"></asp:TextBox>
                    </div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        ����</div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" IsView="true" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        ������Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        �����ڼ�</div>
                    <div>
                        <asp:TextBox ID="PromotionBeginCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server">~~</asp:Label>
                        <asp:TextBox ID="PromotionEndCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        �����ڼ�</div>
                    <div>
                        <asp:TextBox ID="DeliveryBeginCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        <asp:Label ID="Label7" runat="server">~~</asp:Label>
                        <asp:TextBox ID="DeliveryEndCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��������</div>
                    <div>
                        <asp:TextBox ID="PromotionScopeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ������ʽ</div>
                    <div>
                        <asp:TextBox ID="PromotionTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        �����</div>
                    <div>
                        <asp:TextBox ID="PromotionDescCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ������ʽ</div>
                    <div>
                        <asp:TextBox ID="ShelfTypeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��һ��������</div>
                    <div>
                        <asp:TextBox ID="FirstVolumeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ڶ���������</div>
                    <div>
                        <asp:TextBox ID="SecondVolumeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ������������</div>
                    <div>
                        <asp:TextBox ID="ThirdVolumeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ǰ����ƽ������</div>
                    <div>
                        <asp:TextBox ID="AverageVolumeCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px;">
        Ԥ����Ϣ<asp:HyperLink Style="padding-left: 5px;" ID="btnViewBudget" runat="server" Text="���鿴��ǰԤ�㣩" /></div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        �ͻ����Ԥ��</div>
                    <div>
                        <asp:TextBox ID="CustomerBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ͻ�ʣ��Ԥ��</div>
                    <div>
                        <asp:TextBox ID="CustomerBudgetRemainCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <img src="../Images/DeptBudget.png" alt="�����꿪ʼ�µ��������顮�����ڼ䡯����ʼ��Ϊֹ���ۼ�Ԥ��YTD" /></div>
                    <div>
                        <asp:TextBox ID="OUBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ����������δִ�з���</div>
                    <div>
                        <asp:TextBox ID="OUApprovedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �����������������</div>
                    <div>
                        <asp:TextBox ID="OUApprovingAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ������ȷ��ִ�з���</div>
                    <div>
                        <asp:TextBox ID="OUCompletedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        �����ѱ�������</div>
                    <div>
                        <asp:TextBox ID="OUReimbursedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ���ſ���Ԥ��</div>
                    <div>
                        <asp:TextBox ID="OUBudgetRemainCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ����Ԥ��ʹ�ý���</div>
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
        ������ϸ��Ϣ</div>
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
                                    <table class="Container8">
                                        <tr>
                                            <td>
                                                <label id="lbl1" style="font-weight: bold">
                                                    ʵ�ʷ����ܼ�:</label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSum" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
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
                                                    &nbsp;&nbsp;&nbsp;��Ʒ���ƣ�<asp:Label ID="Label1" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                                                </td>
                                                <td>
                                                    ���������ۣ�<asp:Label ID="Label2" runat="server" Text='<%# Eval("SupplyPrice") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    ���������ۣ�<asp:Label ID="Label3" runat="server" Text='<%# Eval("PromotionPrice") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    ��<asp:Label ID="Label4" runat="server" Text='<%# Eval("BuyQuantity") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                                    ��:<asp:Label ID="Label5" runat="server" Text='<%# Eval("GiveQuantity") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    Ԥ����������<asp:Label ID="Label6" runat="server" Text='<%# Eval("EstimatedSaleVolume") %>'></asp:Label>
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
                                        <gc:GridView ID="gvExpense" runat="server" Width="1000px" ShowFooter="false" CssClass="GridView"
                                            AutoGenerateColumns="False" DataKeyNames="FormApplyExpenseDetailID" DataSourceID="odsExpense"
                                            OnRowDataBound="gvExpense_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFormApplyExpenseDetailID" runat="server" Text='<%# Eval("FormApplyExpenseDetailID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="0px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="������">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="500px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="���ý��">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="�������">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPackageUnitPrice" runat="server" Text='<%# Bind("PackageUnitPrice","{0:N}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ʵ�ʷ���">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAccruedAmount" runat="server" Text='<%# Bind("AccruedAmount","{0:N}") %>'
                                                            Width="130px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="�ظ���������">
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="Header" />
                                            <EmptyDataTemplate>
                                                <table class="GridView" style="width: 1000px">
                                                    <tr class="Header">
                                                        <td style="width: 500px;" align="center">
                                                            <b>������</b>
                                                        </td>
                                                        <td style="width: 150px;" align="center">
                                                            <b>���ý��</b>
                                                        </td>
                                                        <td style="width: 150px;" align="center">
                                                            <b>�������</b>
                                                        </td>
                                                        <td style="width: 150px;" align="center">
                                                            <b>ʵ�ʷ���</b>
                                                        </td>
                                                        <td style="width: 150px;" align="center">
                                                            <b>�ظ���������</b>
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
                        SelectMethod="GetFormApplySKUDetail" OnObjectCreated="odsSKU_ObjectCreated">
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    <br />
     <div id="divSplitRate" runat="server" class="title" style="width: 1260px;" visible="false">
        ��̯������</div>
    <gc:GridView ID="gvSplitRate" runat="server" CssClass="GridView" AutoGenerateColumns="False"
        DataKeyNames="FormApplySplitRateID" CellPadding="0" Visible="false">
        <Columns>
            <asp:TemplateField HeaderText="�����ڼ�">
                <ItemTemplate>
                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Period","{0:yyyy-MM}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��������̯������%��">
                <ItemTemplate>
                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' Width="200"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��ע">
                <ItemTemplate>
                    <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>' Width="600"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="667" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
    </gc:GridView>
    <br />
    <uc3:APFlowNodes ID="cwfAppCheck" runat="server" IsView="true" />
    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <uc1:YearAndMonthUserControl ID="UCBeginPeriod" runat="server" IsReadOnly="false"
                    IsExpensePeriod="true" />
                &nbsp;<asp:Label ID="lblSignal" runat="server" Text="~~"></asp:Label>&nbsp;
                <uc1:YearAndMonthUserControl ID="UCEndPeriod" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                &nbsp;
                <asp:Button ID="CopyBtn" runat="server" OnClick="CopyBtn_Click" Text="�����鸴��" CssClass="button_nor" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="AccrudePeriodSignal" runat="server" Text="Ԥ������ڼ�:" Visible="false"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="PeriodDDL" runat="server" DataSourceID="odsPeriod" DataTextField="AccruedPeriod"
                    DataValueField="AccruedPeriodID" Width="170px" Visible="false">
                </asp:DropDownList>
                <asp:SqlDataSource ID="odsPeriod" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand=" select 0 AccruedPeriodID,' ��ѡ��' AccruedPeriod Union SELECT [AccruedPeriodID], convert(varchar(50),year(AccruedPeriod))+'-'+convert(varchar(50),month(AccruedPeriod)) AccruedPeriod FROM [AccruedPeriod] ">
                </asp:SqlDataSource>
                &nbsp;
                <asp:Button ID="ExecuteConfirmBtn" runat="server" OnClick="ExecuteConfirmBtn_Click"
                    Text="ȷ��ִ��" CssClass="button_nor" Visible="false" />&nbsp;
                <asp:Button ID="ExecuteCancelBtn" runat="server" OnClick="ExecuteCancelBtn_Click"
                    Text="ȡ��ִ��" CssClass="button_nor" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="����" CssClass="button_nor" />&nbsp;
                <asp:Button ID="CloseBtn" runat="server" CssClass="button_nor" OnClick="CloseBtn_Click"
                    Visible="false" Text="�����ر�" />
                <cc1:ConfirmButtonExtender runat="server" TargetControlID="CloseBtn" ConfirmText="�����رպ󽫲����ٽ��б�����ȷ���ر���"
                    ID="ConfirmButtonExtender1">
                </cc1:ConfirmButtonExtender>
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
