<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="OtherForm_PersonalReimburseApproval"
    Title="���˷��ñ�������" Codebehind="PersonalReimburseApproval.aspx.cs" %>

<%@ Register src="../UserControls/APFlowNodes.ascx" tagname="APFlowNodes" tagprefix="uc1" %>
<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../Script/js.js"></script>
    <script type="text/javascript" src="../Script/DateInput.js"></script>
    <script language="javascript" type="text/javascript">
        function MinusTotal(lblName, obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvPersonalReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName)) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvPersonalReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText);
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
                if (document.all("ctl00_ContentPlaceHolder1_gvPersonalReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName)) {
                    document.all("ctl00_ContentPlaceHolder1_gvPersonalReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText = commafy(lastTotal.toFixed(2));
                }
            }
        }

        function PlusTotal(lblName, obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvPersonalReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName)) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvPersonalReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText);
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

                document.all("ctl00_ContentPlaceHolder1_gvPersonalReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText = commafy(lastTotal.toFixed(2));
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
                        <asp:TextBox ID="txtFormNo" runat="server" ReadOnly="true" Width="170px"/>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��������</div>
                    <div>
                        <asp:TextBox ID="ApplyDateCtl" runat="server" ReadOnly="true" Width="170px"/>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �����ڼ�</div>
                    <div>
                        <asp:TextBox ID="txtPeriod" runat="server" ReadOnly="true" Width="170px"/>
                    </div>
                </td>
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
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ְ����</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        ��ʷ����</div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        ��������</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText"  TextMode="multiline"
                            Height="60px" ReadOnly="true"  Columns="75"></asp:TextBox></div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        ����
                    </div>
                    <uc3:UCFlie ID="UCFileUpload" runat="server" Width="400px" IsView="true"/>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="BudgetTitleDIV" runat="server" class="title" style="width: 1258px">Ԥ����Ϣ</div>
    <div id="BudgetInfoDIV" runat="server" style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px;margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        Ԥ����</div>
                    <div>
                        <asp:TextBox ID="txtTotalBudget" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �����н��</div>
                    <div>
                        <asp:TextBox ID="txtApprovingAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ѱ������</div>
                    <div>
                        <asp:TextBox ID="txtApprovedAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �������</div>
                    <div>
                        <asp:TextBox ID="txtRemainAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        ������ϸ��Ϣ</div>
    <asp:UpdatePanel ID="upPersonalReimburseDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvPersonalReimburseDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormPersonalReimburseDetailID" DataSourceID="odsPersonalReimburseDetails" OnRowDataBound="gvPersonalReimburseDetails_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblOccurDate" runat="server" Text='<%# Eval("OccurDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����������">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseManageType" runat="server" Text='<%# GetExpenseManageTypeNameById(Eval("ExpenseManageTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="350px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�������">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Right"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ʵ�����">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRealAmount" MaxLength="15" runat="server" Width="130px" Text='<%# Eval("RealAmount") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="totalRealAmountLbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ע">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="508px" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table class="GridView">
                        <tr class="Header">
                            <td style="width: 100px;">
                                <b>��������</b>
                            </td>
                            <td style="width: 350px;">
                                <b>����������</b>
                            </td>
                            <td style="width: 150px;">
                                <b>�������</b>
                            </td>
                            <td style="width: 150px;">
                                <b>ʵ�����</b>
                            </td>
                            <td style="width: 508px;">
                                <b>��ע</b>
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsPersonalReimburseDetails" runat="server" SelectMethod="GetFormPersonalReimburseDetail"
        TypeName="BusinessObjects.PersonalReimburseBLL" OnObjectCreated="odsPersonalReimburseDetails_ObjectCreated">
    </asp:ObjectDataSource>
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate> 
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <uc2:YearAndMonthUserControl ID="UCPeriod" runat="server" IsReadOnly="false" IsExpensePeriod="true" />&nbsp;
                <asp:Button ID="CopyBtn" runat="server" OnClick="CopyBtn_Click" Text="����" CssClass="button_nor" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="����" CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="����" CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="�༭" CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="����" CssClass="button_nor" />
                <asp:Button ID="SaveBtn" runat="server" OnClick="SaveBtn_Click" Text="����ʵ�����" CssClass="button_nor" />&nbsp;
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <uc4:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
