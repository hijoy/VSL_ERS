<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="OtherForm_BudgetAllocationApproval"
    Title="Ԥ���������" Codebehind="BudgetAllocationApproval.aspx.cs" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonth"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/BudgetSalesFeeViewControl.ascx" TagName="BudgetSales"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="DateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register src="../UserControls/APFlowNodes.ascx" tagname="APFlowNodes" tagprefix="uc3" %>

<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function ParameterChanged() {
            var txtQuantity = document.all("ctl00$ContentPlaceHolder1$fvBudgetAllocationDetails$newQuantityCtl").value;
            var txtPrice = document.all("ctl00$ContentPlaceHolder1$fvBudgetAllocationDetails$newBudgetAllocationPriceCtl").value;
            if (txtQuantity != "" && isNaN(txtQuantity)) {
                alert("��¼������");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvBudgetAllocationDetails$newQuantityCtl").focus(); }, 0);
                return false;
            }
            if (txtPrice != "" && isNaN(txtPrice)) {
                alert("��¼������");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvBudgetAllocationDetails$newBudgetAllocationPriceCtl").focus(); }, 0);
                return false;
            }
            if (!isNaN(parseFloat(txtQuantity)) && !isNaN(parseFloat(txtPrice))) {
                var quantity = parseFloat(txtQuantity);
                var price = parseFloat(txtPrice);
                var amount = quantity * price;
                document.all("ctl00$ContentPlaceHolder1$fvBudgetAllocationDetails$newTotalCtl").value = commafy(amount.toFixed(2));
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
                        <asp:TextBox ID="txtFormNo" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
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
                <td valign="top">
                    <div class="field_title">
                        ��ʷ����</div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        Ԥ�������Ϣ</div>
    <asp:UpdatePanel ID="upBudgetAllocationOutDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvBudgetAllocationOutDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormBudgetAllocationDetailID" DataSourceID="odsBudgetAllocationOutDetails"
                OnRowDataBound="gvBudgetAllocationOutDetails_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="�ͻ�����">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="299px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����ڼ�">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("Period", "{0:yyyy/MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����������">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItemName" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="260px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ʼԤ��" SortExpression="OriginalBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblOriginalBudget" runat="server" Text='<%# Eval("OriginalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����Ԥ��" SortExpression="NormalBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblNormalBudget" runat="server" Text='<%# Eval("NormalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ԥ�����" SortExpression="AdjustBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��Ԥ��" SortExpression="TotalBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalBudget" runat="server" Text='<%# Eval("TotalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="�ܼƣ�"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ԥ�����" SortExpression="TransferBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblTransferBudget" runat="server" Text='<%# Eval("TransferBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table class="GridView">
                        <tr class="Header">
                            <td style="width: 245px;" class="field_title">
                                �ͻ�����
                            </td>
                            <td style="width: 100px;" class="field_title">
                                �����ڼ�
                            </td>
                            <td style="width: 260px;" class="field_title">
                                ����������
                            </td>
                            <td style="width: 120px;" class="field_title">
                                ��ʼԤ��
                            </td>
                            <td style="width: 120px;" class="field_title">
                                ����Ԥ��
                            </td>
                            <td style="width: 120px;" class="field_title">
                                Ԥ�����
                            </td>
                            <td style="width: 120px;" class="field_title">
                                ��Ԥ��
                            </td>
                            <td style="width: 120px;" class="field_title">
                                Ԥ�����
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
            <asp:ObjectDataSource ID="odsBudgetAllocationOutDetails" runat="server" SelectMethod="GetFormBudgetAllocationDetailOut"
                TypeName="BusinessObjects.BudgetAllocationApplyBLL">
                <SelectParameters>
                    <asp:Parameter Name="FormBudgetAllocationID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="title" style="width: 1258px">
        Ԥ�������Ϣ</div>
    <asp:UpdatePanel ID="upBudgetAllocationInDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvBudgetAllocationInDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormBudgetAllocationDetailID" DataSourceID="odsBudgetAllocationInDetails"
                OnRowDataBound="gvBudgetAllocationInDetails_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="�ͻ�����">
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="299px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����ڼ�">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("Period", "{0:yyyy/MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����������">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItemName" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="260px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ʼԤ��" SortExpression="OriginalBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblOriginalBudget" runat="server" Text='<%# Eval("OriginalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����Ԥ��" SortExpression="NormalBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblNormalBudget" runat="server" Text='<%# Eval("NormalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ԥ�����" SortExpression="AdjustBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��Ԥ��" SortExpression="TotalBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalBudget" runat="server" Text='<%# Eval("TotalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="�ܼƣ�"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ԥ�����" SortExpression="TransferBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblTransferBudget" runat="server" Text='<%# Eval("TransferBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="120px" />
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table class="GridView">
                        <tr class="Header">
                            <td style="width: 245px;" class="field_title">
                                �ͻ�����
                            </td>
                            <td style="width: 100px;" class="field_title">
                                �����ڼ�
                            </td>
                            <td style="width: 260px;" class="field_title">
                                ����������
                            </td>
                            <td style="width: 120px;" class="field_title">
                                ��ʼԤ��
                            </td>
                            <td style="width: 120px;" class="field_title">
                                ����Ԥ��
                            </td>
                            <td style="width: 120px;" class="field_title">
                                Ԥ�����
                            </td>
                            <td style="width: 120px;" class="field_title">
                                ��Ԥ��
                            </td>
                            <td style="width: 120px;" class="field_title">
                                Ԥ�����
                            </td>
                            <td style="width: 54px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
            <asp:ObjectDataSource ID="odsBudgetAllocationInDetails" runat="server" SelectMethod="GetFormBudgetAllocationDetailIn"
                TypeName="BusinessObjects.BudgetAllocationApplyBLL">
                <SelectParameters>
                    <asp:Parameter Name="FormBudgetAllocationID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="title" style="width: 1258px">
        ������Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td colspan="6">
                    <div class="field_title">
                        ��������</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" Width="800px" TextMode="multiline"
                            Height="60px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="field_title">
                        ����</div>
                    <div>
                        <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" IsView="true" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <uc3:APFlowNodes ID="cwfAppCheck" runat="server" />
     <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="����" CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="����" CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="�༭" CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="����" CssClass="button_nor" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc4:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
