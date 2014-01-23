<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="OtherForm_BudgetAllocationApply"
    Title="Ԥ���������" Codebehind="BudgetAllocationApply.aspx.cs" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonth"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/BudgetSalesFeeViewControl.ascx" TagName="BudgetSales"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="DateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
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
                        Ա��</div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ְλ</div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ����</div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ְ����</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        &nbsp;</div>
                    <div>
                        &nbsp;</div>
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
                        <ItemStyle Width="245px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����ڼ�">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("Period", "{0:yyyy/MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����������">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItemName" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="260px" HorizontalAlign="Center" />
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
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="ɾ��"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="54px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 245px;" class="Empty1">
                                �ͻ�����
                            </td>
                            <td style="width: 100px;">
                                �����ڼ�
                            </td>
                            <td style="width: 260px;">
                                ����������
                            </td>
                            <td style="width: 120px;">
                                ��ʼԤ��
                            </td>
                            <td style="width: 120px;">
                                ����Ԥ��
                            </td>
                            <td style="width: 120px;">
                                Ԥ�����
                            </td>
                            <td style="width: 120px;">
                                ��Ԥ��
                            </td>
                            <td style="width: 120px;">
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
            <asp:FormView ID="fvBudgetAllocationOutDetails" runat="server" DataKeyNames="FormBudgetAllocationDetailID"
                DataSourceID="odsBudgetAllocationOutDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr class="Header">
                            <td align="center" style="width: 245px;">
                                <uc2:BudgetSales ID="UCBudgetSales" AutoPostBack="true" IsNoClear="true" runat="server"
                                    Width="150px" OnCustomerTextChanged="OnDataTextChanged" />
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtPeriod" runat="server" ReadOnly="true" Width="70px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 260px;">
                                <asp:TextBox ID="txtExpenseTypeName" ReadOnly="true" runat="server" Width="230px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtOriginalBudget" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtNormalBudget" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtAdjustBudget" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtTotalBudget" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtTransferBudget" runat="server" MaxLength="15" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 50px;">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="���" ValidationGroup="NewDetailOutRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="txtTransferBudget"
                                ErrorMessage="����д������" Display="None" ValidationGroup="NewDetailOutRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTransferBudget"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="����������" ValidationGroup="NewDetailOutRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailOutRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsBudgetAllocationOutDetails" runat="server" DeleteMethod="DeleteFormBudgetAllocationDetailByID"
                SelectMethod="GetFormBudgetAllocationDetailOut" TypeName="BusinessObjects.BudgetAllocationApplyBLL"
                InsertMethod="AddFormBudgetAllocationDetail" OnInserting="odsBudgetAllocationOutDetails_Inserting"
                OnObjectCreated="odsBudgetAllocationOutDetails_ObjectCreated" OnInserted="odsBudgetAllocationOutDetails_Inserted"
                OnDeleted="odsBudgetAllocationOutDetails_Deleted">
                <DeleteParameters>
                    <asp:Parameter Name="FormBudgetAllocationDetailID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="User" Type="Object" />
                    <asp:Parameter Name="FormBudgetAllocationID" Type="Int32" />
                    <asp:Parameter Name="BudgetSaleFeeViewId" Type="Int32" />
                    <asp:Parameter Name="AllocationType" Type="Int32" />
                    <asp:Parameter Name="TransferBudget" Type="Decimal" />
                </InsertParameters>
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
                        <ItemStyle Width="245px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����ڼ�">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod" runat="server" Text='<%# Eval("Period", "{0:yyyy/MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����������">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItemName" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="260px" HorizontalAlign="Center" />
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
                            <asp:Label runat="server" ID="lblTotal" CssClass=""></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="ɾ��"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="54px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 245px;" class="Empty1">
                                �ͻ�����
                            </td>
                            <td style="width: 100px;">
                                �����ڼ�
                            </td>
                            <td style="width: 260px;">
                                ����������
                            </td>
                            <td style="width: 120px;">
                                ��ʼԤ��
                            </td>
                            <td style="width: 120px;">
                                ����Ԥ��
                            </td>
                            <td style="width: 120px;">
                                Ԥ�����
                            </td>
                            <td style="width: 120px;">
                                ��Ԥ��
                            </td>
                            <td style="width: 120px;">
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
            <asp:FormView ID="fvBudgetAllocationInDetails" runat="server" DataKeyNames="FormBudgetAllocationDetailID"
                DataSourceID="odsBudgetAllocationInDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr class="Header">
                            <td align="center" style="width: 245px;">
                                <uc2:BudgetSales ID="UCBudgetSales" AutoPostBack="true" IsNoClear="true" runat="server"
                                    Width="150px" OnCustomerTextChanged="OnDataTextChanged1" />
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtPeriod" runat="server" ReadOnly="true" Width="70px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 260px;">
                                <asp:TextBox ID="txtExpenseTypeName" ReadOnly="true" runat="server" Width="230px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtOriginalBudget" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtNormalBudget" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtAdjustBudget" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtTotalBudget" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtTransferBudget" MaxLength="15" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 54px;">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="���" ValidationGroup="NewDetailInRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="txtTransferBudget"
                                ErrorMessage="����д������" Display="None" ValidationGroup="NewDetailInRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtTransferBudget"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="����������" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailInRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsBudgetAllocationInDetails" runat="server" DeleteMethod="DeleteFormBudgetAllocationDetailByID"
                SelectMethod="GetFormBudgetAllocationDetailIn" TypeName="BusinessObjects.BudgetAllocationApplyBLL"
                InsertMethod="AddFormBudgetAllocationDetail" OnInserting="odsBudgetAllocationInDetails_Inserting"
                OnObjectCreated="odsBudgetAllocationInDetails_ObjectCreated" OnInserted="odsBudgetAllocationInDetails_Inserted"
                OnDeleted="odsBudgetAllocationInDetails_Deleted">
                <DeleteParameters>
                    <asp:Parameter Name="FormBudgetAllocationDetailID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="User" Type="Object" />
                    <asp:Parameter Name="FormBudgetAllocationID" Type="Int32" />
                    <asp:Parameter Name="BudgetSaleFeeViewId" Type="Int32" />
                    <asp:Parameter Name="AllocationType" Type="Int32" />
                    <asp:Parameter Name="TransferBudget" Type="Decimal" />
                </InsertParameters>
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
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="140" ViewStateMode="Enabled"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="field_title">
                        ����</div>
                    <div>
                        <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                    Text="�ύ" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click"
                    Text="����" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="����" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text="ɾ��" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upCustomer" />
</asp:Content>
