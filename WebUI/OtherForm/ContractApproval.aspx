<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="OtherForm_ContractApproval" Title="��ͬ����" CodeBehind="ContractApproval.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc3" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1258px">
        ��������Ϣ</div>
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
                <td valign="top" id="trHistory" runat="server">
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
        ǩԼ�Է���Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 400px">
                    <div class="field_title">
                        �Է���λ1</div>
                    <div>
                        <asp:TextBox ID="txtFirstCompany" runat="server" Width="340px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 400px">
                    <div class="field_title">
                        �Է���λ2</div>
                    <div>
                        <asp:TextBox ID="txtSecondCompany" runat="server" Width="340px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 400px">
                    <div class="field_title">
                        �Է���λ3</div>
                    <div>
                        <asp:TextBox ID="txtThirdCompany" runat="server" Width="340px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        ��ͬ��Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ͬ���</div>
                    <div>
                        <asp:TextBox ID="txtContractNo" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ͬ���ƣ���ʽ���ƣ�</div>
                    <div>
                        <asp:TextBox ID="txtContractName" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ͬ���</div>
                    <div>
                        <asp:TextBox ID="txtContractAmount" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ͬ����ҳ��</div>
                    <div>
                        <asp:TextBox ID="txtPageNumber" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ͬ����</div>
                    <div>
                        <asp:TextBox ID="txtContractType" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ֧����ʽ</div>
                    <div>
                        <asp:TextBox ID="txtPaymentType" runat="server" Width="170px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 400px" colspan="2">
                    <div class="field_title">
                        ��ͬ�ڼ�</div>
                    <div>
                        <asp:TextBox ID="txtBeginDate" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <asp:TextBox ID="txtEndDate" runat="server" Width="170px" ReadOnly="true"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �Ƿ��Ѹ���</div>
                    <div>
                        <asp:CheckBox ID="IsStampCtl" runat="server" Enabled="false" /></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �����Ƿ����</div>
                    <div>
                        <asp:CheckBox ID="IsRecoveryCtl" runat="server" Enabled="false" /></div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="field_title">
                        ��Ҫ����</div>
                    <div>
                        <asp:TextBox ID="txtMainContent" runat="server" CssClass="InputText" Width="1000px"
                            TextMode="multiline" Height="60px" ReadOnly="True"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="field_title">
                        ����仯</div>
                    <div>
                        <asp:TextBox ID="txtChangePart" runat="server" CssClass="InputText" Width="1000px"
                            TextMode="multiline" Height="60px" ReadOnly="True"></asp:TextBox>
                    </div>
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
                <asp:Button ID="StampBtn" runat="server" CssClass="button_nor" OnClick="StampBtn_Click"
                    Text="��ͬ�Ѹ���" />
                <asp:Button ID="RecoveryBtn" runat="server" CssClass="button_nor" OnClick="RecoveryBtn_Click"
                    Text="�������ջ�" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc4:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
