<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SaleForm_ReimburseMoneyApproval" Title="U������������" CodeBehind="ReimburseMoneyApproval.aspx.cs" %>

<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
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
                <td style="width: 200px">
                    <div class="field_title">
                        ʵ��֧������</div>
                    <div>
                        <asp:TextBox ID="txtPaymentDate" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        <uc1:UCDateInput ID="ucPaymentDate" runat="server" Visible="false" />
                    </div>
                </td>
                <td>
                    <div class="field_title">
                        ��ʷ����</div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
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
    <div class="title" style="width: 1258px">
        ��Ʊ��Ϣ</div>
    <asp:UpdatePanel ID="upInvoice" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvInvoice" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormReimburseInvoiceID" DataSourceID="odsInvoice"
                OnRowDataBound="gvInvoice_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="��Ʊ����">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="220px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��Ʊ���">
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceAmount" runat="server" Text='<%# Eval("InvoiceAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ע">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�ظ�������">
                        <ItemTemplate>
                            <asp:Label ID="lblSystemInfo" runat="server" Text='<%# Eval("SystemInfo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="494px" HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsInvoice" runat="server" SelectMethod="GetFormReimburseInvoiceByFormReimburseID"
        TypeName="BusinessObjects.SalesReimburseBLL">
        <SelectParameters>
            <asp:Parameter Name="FormReimburseID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
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
                        <ItemStyle HorizontalAlign="Center" Width="120px" />
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
                        <ItemStyle HorizontalAlign="Center" Width="308px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="������">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="230px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ȷ�Ͻ��">
                        <ItemTemplate>
                            <asp:Label ID="lblAccruedAmount" runat="server" Text='<%# Eval("AccruedAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�ɱ������">
                        <ItemTemplate>
                            <asp:Label ID="lblRemainAmount" runat="server" Text='<%# Eval("RemainAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ԥ�����" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPrePaidAmount" runat="server" Text='<%# Eval("PrePaidAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="˰ǰ���">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="˰��">
                        <ItemTemplate>
                            <asp:Label ID="lblTaxAmount" runat="server" Text='<%# Eval("TaxAmount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <asp:ObjectDataSource ID="odsReimburseDetails" runat="server" SelectMethod="GetFormReimburseDetailByFormReimburseD"
        TypeName="BusinessObjects.SalesReimburseBLL">
        <SelectParameters>
            <asp:Parameter Name="FormReimburseID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="btnSavePaymentInfo" runat="server" CssClass="button_nor" Text="����֧����Ϣ"
                    OnClick="btnSavePaymentInfo_Click" Visible="false" />
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
    <uc3:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
