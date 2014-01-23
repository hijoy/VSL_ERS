<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="StuffUserManagePage" MasterPageFile="~/MasterPage.master" Codebehind="StuffUserManage.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        ��������</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        ��½�ʺ�</div>
                    <asp:TextBox ID="UserAccountTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        �û���</div>
                    <asp:TextBox ID="StuffNameTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        Ա������</div>
                    <asp:TextBox ID="EmployeeNoTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        �����ʼ�</div>
                    <asp:TextBox ID="EmailTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        �绰</div>
                    <asp:TextBox ID="TelTextBox" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 50px;">
                    &nbsp;
                </td>
                <td colspan="2" align="left" valign="bottom">
                    <input type="hidden" id="btnclicked" name="btnclicked" value="0" />
                    <asp:Button ID="SearchButton" runat="server" CssClass="button_nor" Text="��ѯ" OnClick="SearchButton_Click" />&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1260px;">
        �û���Ϣ
    </div>
    <asp:UpdatePanel ID="StuffUserUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="StuffUserGridView" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="StuffUserId" DataSourceID="StuffUserObjectDataSource"
                PageSize="20" ShowFooter="True" CssClass="GridView" OnSelectedIndexChanged="StuffUserGridView_SelectedIndexChanged"
                OnRowDataBound="StuffUserGridView_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="��½�ʺ�" SortExpression="UserName">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="162px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ա������" SortExpression="StuffName">
                        <ItemTemplate>
                            <asp:LinkButton ID="StuffNameLinkButton" runat="server" CommandName="Select" OnClick="StuffNameLinkButton_Click"
                                Text='<%# Bind("StuffName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ְ��">
                        <ItemTemplate>
                            <asp:Label ID="PositionsLabel" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="320px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ա������" SortExpression="StuffId">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("StuffId") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����ʼ�" SortExpression="Email">
                        <ItemTemplate>
                            <asp:Label ID="Label111" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ְ����" SortExpression="AttendDate">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("AttendDate","{0:yyyy-MM-dd}" ) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����" SortExpression="IsActive">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("IsActive") %>' Enabled="false" />
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                Text="Update"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton Visible="<%# HasManageRight %>" ID="EditLinkButton" runat="server"
                                CausesValidation="False" CommandName="Select" Text="�༭" OnClick="EditLinkButton_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton Visible="<%# HasManageRight %>" ID="AddLinkButton1" runat="server"
                                CausesValidation="True" CommandName="Select" Text="����" OnClick="AddLinkButton_Click"></asp:LinkButton>
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="StuffUserId" HeaderText="StuffUserId" InsertVisible="False"
                        ReadOnly="True" SortExpression="StuffUserId" Visible="False" />
                    <asp:BoundField DataField="UserPassword" HeaderText="UserPassword" SortExpression="UserPassword"
                        Visible="False" />
                    <asp:BoundField DataField="EMail" HeaderText="EMail" SortExpression="EMail" Visible="False" />
                    <asp:BoundField DataField="Telephone" HeaderText="Telephone" SortExpression="Telephone"
                        Visible="False" />
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 162px;" class="Empty1">
                                ��½�ʺ�
                            </td>
                            <td style="width: 150px;">
                                Ա������
                            </td>
                            <td style="width: 320px;">
                                ְ��
                            </td>
                            <td style="width: 100px;">
                                Ա������
                            </td>
                            <td style="width: 250px;">
                                �����ʼ�
                            </td>
                            <td style="width: 100px;">
                                ��ְ����
                            </td>
                            <td style="width: 100px;">
                                ����
                            </td>
                            <td style="width: 80px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" class="Empty2 noneLabel">
                                ��
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
                <HeaderStyle CssClass="Header" />
                <FooterStyle CssClass="Footer" />
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="SearchButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="StuffUserFormView" EventName="ItemInserted" />
            <asp:AsyncPostBackTrigger ControlID="StuffUserFormView" EventName="ItemUpdated" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="StuffUserAddUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:FormView ID="StuffUserFormView" runat="server" DataKeyNames="StuffUserId" DataSourceID="StuffUserAddObjectDataSource"
                Width="1270px" OnItemUpdating="StuffUserFormView_ItemUpdating" OnDataBound="StuffUserFormView_DataBound"
                OnItemInserting="StuffUserFormView_ItemInserting">
                <EditItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;ϵͳ��½�ʺ�
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>'
                                    ValidationGroup="EDIT" Width="200px" MaxLength="50" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;�û���
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="StuffNameTextBox" runat="server" Text='<%# Bind("StuffName") %>'
                                    ValidationGroup="EDIT" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;����
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="UserPasswordTextBox" runat="server" ValidationGroup="EDIT"
                                    Width="200px" Text="!!!!!!" TextMode="Password" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;Ա������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="StuffIdTextBox" runat="server" Text='<%# Bind("StuffId") %>'
                                    ValidationGroup="EDIT" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;ȷ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="UserPasswordsTextBox" runat="server" ValidationGroup="EDIT"
                                    Text="!!!!!!" Width="200px" TextMode="Password" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;�����ʼ�
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="EMailTextBox" runat="server" Text='<%# Bind("EMail") %>' Width="200px"
                                    MaxLength="100" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;�绰
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="TelephoneTextBox" runat="server" Text='<%# Bind("Telephone") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;��ְ����
                            </td>
                            <td align="left" style="padding-left: 6px;">
                                <uc1:UCDateInput ID="UCAttendDate" runat="server" SelectedDate='<%#Bind("AttendDate","{0:yyyy-MM-dd}")%>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                ��ͨ�ѽ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtTrafficFee" runat="server" Text='<%# Bind("TrafficFeeLimit") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                �绰�ѽ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtTelephoneFee" runat="server" Text='<%# Bind("TelephoneFeeLimit") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;Ӣ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="EnglishNameTextBox" runat="server" Text='<%# Bind("EnglishName") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                ����
                            </td>
                            <td align="left">
                                &nbsp;<asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsActive") %>' />
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;���п���
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtBankCard" runat="server" Text='<%# Bind("BankCard") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                                &nbsp;<asp:Button ID="UpdateButton" runat="server" CssClass="button_nor" Text="�޸�"
                                    CommandName="Update" OnClick="InsertButton_Click" ValidationGroup="EDIT" />
                                <asp:Button ID="Button2" runat="server" CssClass="button_nor" Text="����" OnClick="BackButton_Click"
                                    CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="UserNameRequiredFieldValidator" runat="server" ControlToValidate="UserNameTextBox"
                        Display="Dynamic" ErrorMessage="���������½����" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="StuffNameRequiredFieldValidator" runat="server" ControlToValidate="StuffNameTextBox"
                        Display="Dynamic" ErrorMessage="��������Ա��������" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="StuffIDRequiredFieldValidator" runat="server" ControlToValidate="StuffIdTextBox"
                        Display="Dynamic" ErrorMessage="��������Ա�����ţ�" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="UserPWDRequiredFieldValidator" runat="server" ControlToValidate="UserPasswordTextBox"
                        Display="Dynamic" ErrorMessage="�����������룡" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="ConfirmPWDFieldValidator" runat="server" ControlToValidate="UserPasswordsTextBox"
                        Display="Dynamic" ErrorMessage="��������ȷ�����룡" SetFocusOnError="True" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="UserPasswordCompareValidator" runat="server" ControlToCompare="UserPasswordTextBox"
                        ControlToValidate="UserPasswordsTextBox" Display="Dynamic" ErrorMessage="ǰ�����벻һ�£����������룡"
                        SetFocusOnError="True" ValidationGroup="EDIT"></asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="EMailRegularExpressionValidator" runat="server"
                        ControlToValidate="EMailTextBox" Display="Dynamic" ErrorMessage="EMail��ַ��ʽ�Ƿ������磺XXXXX@XXXX.XXX"
                        SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" ShowMessageBox="True"
                        ShowSummary="true" ValidationGroup="EDIT" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;ϵͳ��½�ʺ�
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right" width="22%">
                                &nbsp;�û���
                            </td>
                            <td align="left" width="28%">
                                &nbsp;<asp:TextBox ID="StuffNameTextBox" runat="server" Text='<%# Bind("StuffName") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;����
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="UserPasswordTextBox" runat="server" Text='<%# Bind("UserPassword") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;Ա������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="StuffIdTextBox" runat="server" Text='<%# Bind("StuffId") %>'
                                    ValidationGroup="INS" Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;ȷ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="UserPasswordsTextBox" runat="server" ValidationGroup="INS"
                                    Width="200px" MaxLength="50" TextMode="Password"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;�����ʼ�
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="EMailTextBox" runat="server" Text='<%# Bind("EMail") %>' Width="200px"
                                    MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;�绰
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="TelephoneTextBox" runat="server" Text='<%# Bind("Telephone") %>'
                                    Width="200px" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;��ְ����
                            </td>
                            <td align="left" style="padding-left: 6px;">
                                <uc1:UCDateInput ID="UCNewAttendDate" runat="server" IsReadOnly="false" SelectedDate='<%#Bind("AttendDate","{0:yyyy-MM-dd}")%>' />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                ��ͨ�ѽ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtTrafficFee" runat="server" Text='<%# Bind("TrafficFeeLimit") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                �绰�ѽ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtTelephoneFee" runat="server" Text='<%# Bind("TelephoneFeeLimit") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;Ӣ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="EnglishNameTextBox" runat="server" Text='<%# Bind("EnglishName") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;���п���
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtBankCard" runat="server" Text='<%# Bind("BankCard") %>'
                                    Width="200px" MaxLength="50" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                            </td>
                            <td align="left">
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;<asp:Button ID="InsertButton" runat="server" CssClass="button_nor" Text="����"
                                    CommandName="Insert" OnClick="InsertButton_Click" ValidationGroup="INS" />
                                <asp:Button ID="BackButton" runat="server" CssClass="button_nor" Text="����" OnClick="BackButton_Click"
                                    CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="UserNameRequiredFieldValidator" runat="server" ControlToValidate="UserNameTextBox"
                        Display="Dynamic" ErrorMessage="���������½����" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="StuffNameRequiredFieldValidator" runat="server" ControlToValidate="StuffNameTextBox"
                        Display="Dynamic" ErrorMessage="��������Ա��������" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>&nbsp;
                    <asp:RequiredFieldValidator ID="UserPasswordRequiredFieldValidator" runat="server"
                        ControlToValidate="UserPasswordTextBox" Display="Dynamic" ErrorMessage="�����������룡"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="StuffIdRequiredFieldValidator" runat="server" ControlToValidate="StuffIdTextBox"
                        Display="Dynamic" ErrorMessage="��������Ա�����ţ�" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>&nbsp;
                    <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EMailTextBox"
                        Display="Dynamic" ErrorMessage="��������Email��" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>&nbsp;
                    <asp:RequiredFieldValidator ID="UserPasswordsRequiredFieldValidator" runat="server"
                        ControlToValidate="UserPasswordsTextBox" Display="Dynamic" ErrorMessage="��������ȷ�����룡"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="UserPasswordCompareValidator" runat="server" ControlToCompare="UserPasswordTextBox"
                        ControlToValidate="UserPasswordsTextBox" Display="Dynamic" ErrorMessage="ǰ�����벻һ�£����������룡"
                        SetFocusOnError="True" ValidationGroup="INS"></asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="EMailRegularExpressionValidator" runat="server"
                        ControlToValidate="EMailTextBox" Display="Dynamic" ErrorMessage="EMail��ַ��ʽ�Ƿ������磺XXXXX@XXXX.XXX"
                        SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
                <ItemTemplate>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr style="height: 30px">
                            <td style="text-align: right; width: 22%;">
                                ϵͳ��½�ʺ�
                            </td>
                            <td style="text-align: left; width: 28%;">
                                &nbsp;<asp:TextBox ID="UserNameTextBox" runat="server" ReadOnly="True" Text='<%# Bind("UserName") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 22%;">
                                �û���
                            </td>
                            <td style="text-align: left; width: 28%;">
                                &nbsp;<asp:TextBox ID="StuffNameTextBox" runat="server" ReadOnly="True" Text='<%# Bind("StuffName") %>'
                                    Width="200px"></asp:TextBox>&nbsp;
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                �绰/Tel
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="TelephoneTextBox" runat="server" ReadOnly="True" Text='<%# Bind("Telephone") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;Ա������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="StuffIdTextBox" runat="server" ReadOnly="True" Text='<%# Bind("StuffId") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right;">
                                &nbsp;�����ʼ�
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="EMailTextBox" runat="server" ReadOnly="True" Text='<%# Bind("EMail") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right;">
                                &nbsp;��ְ����
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="AttendDateTextBox" runat="server" ReadOnly="True" Text='<%# Bind("AttendDate","{0:yyyy-MM-dd}") %>'
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                ��ͨ�ѽ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtTrafficFee" runat="server" Text='<%# Bind("TrafficFeeLimit") %>'
                                    Width="200px" MaxLength="50" ReadOnly="true" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                �绰�ѽ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtTelephoneFee" runat="server" Text='<%# Bind("TelephoneFeeLimit") %>'
                                    Width="200px" MaxLength="50" ReadOnly="true" ValidationGroup="EDIT"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="text-align: right">
                                &nbsp;Ӣ������
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="EnglishNameTextBox" runat="server" Text='<%# Bind("EnglishName") %>'
                                    Width="200px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right;">
                                ����
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="IsActiveCheckBox" runat="server" Checked='<%# Bind("IsActive") %>'
                                    Enabled="False" />
                            </td>
                        </tr>
                        <tr style="height: 30px">
                            <td align="left" style="text-align: right">
                                &nbsp;���п���
                            </td>
                            <td align="left">
                                &nbsp;<asp:TextBox ID="txtBankCard" runat="server" Text='<%# Bind("BankCard") %>'
                                    Width="200px" MaxLength="50" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="left" style="text-align: right">
                                &nbsp;
                            </td>
                            <td align="left">
                                &nbsp;<asp:Button ID="BackButton" runat="server" CssClass="button_nor" Text="����"
                                    OnClick="BackButton_Click" CommandName="Cancel" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </ContentTemplate>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="StuffUserGridView" EventName="SelectedIndexChanged" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="OrganizationUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="PositionSetPanel" runat="server">
                <div>
                    ������ְλ��</div>
                <div>
                    <gc:GridView ID="StuffUserPositionGridView" runat="server" AutoGenerateColumns="False"
                        CssClass="GridView" DataKeyNames="PositionId" DataSourceID="StuffUserPositionDS"
                        Width="815px" OnRowDataBound="StuffUserPositionGridView_RowDataBound" OnSelectedIndexChanged="StuffUserPositionGridView_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="PositionId" HeaderText="PositionId" InsertVisible="False"
                                ReadOnly="True" SortExpression="PositionId" Visible="False" />
                            <asp:TemplateField HeaderText="��������">
                                <ItemStyle Width="700px" />
                                <ItemTemplate>
                                    <asp:Label ID="ParentOUNamesCtl" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ְ������" SortExpression="PositionName">
                                <ItemStyle Width="115px" />
                                <ItemTemplate>
                                    <asp:LinkButton CommandName="Select" ID="LinkButton3" Text='<%# Bind("PositionName") %>'
                                        runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table class="GridView">
                                <tr class="Header">
                                    <td style="width: 700px;">
                                        ��������
                                    </td>
                                    <td style="width: 115px;">
                                        ְ������
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;" colspan="2">
                                        ��
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="Header" />
                    </gc:GridView>
                    <asp:ObjectDataSource ID="StuffUserPositionDS" runat="server" SelectMethod="GetPositionByStuffUser"
                        TypeName="BusinessObjects.AuthorizationBLL">
                        <SelectParameters>
                            <asp:Parameter Name="stuffUserId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div class="TitleBar">
                    �û�ְλ����</div>
                <asp:Label ID="PositionSetLabel" Text="��ѡ��ְλ" runat="server"></asp:Label>
                <div class="BorderedArea" style="width: 815px; height: 400px; overflow: auto;">
                    <asp:TreeView ID="OrganizationTreeView" runat="server" ExpandDepth="2" ShowLines="True">
                        <SelectedNodeStyle BackColor="#000040" ForeColor="White" />
                    </asp:TreeView>
                </div>
                <div style="text-align: center;">
                    <asp:Button ID="SavePositionBtn" Enabled="<%# HasManageRight %>" Visible="<%# HasManageRight %>"
                        runat="server" CssClass="button_nor" OnClick="SavePositionBtn_Click" Text="����ְλ����" /></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="StuffUserObjectDataSource" runat="server" TypeName="BusinessObjects.StuffUserBLL"
        OldValuesParameterFormatString="{0}" SelectMethod="GetStuffUserPaged" DeleteMethod="DeleteAlterFeeSubType"
        SelectCountMethod="TotalCount" SortParameterName="sortExpression" EnablePaging="true"
        OnSelecting="StuffUserObjectDataSource_Selecting" OnDeleted="StuffUserObjectDataSource_Deleted">
        <DeleteParameters>
            <asp:Parameter Name="StuffUserId" Type="Int32" />
        </DeleteParameters>
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="StuffUserAddObjectDataSource" runat="server" DeleteMethod="DeleteAlterFeeSubType"
        InsertMethod="InsertStuffUser" OldValuesParameterFormatString="{0}" SelectMethod="GetStuffUser"
        TypeName="BusinessObjects.StuffUserBLL" UpdateMethod="UpdateStuffUser" OnInserted="StuffUserAddObjectDataSource_Inserted"
        OnUpdated="StuffUserAddObjectDataSource_Updated">
        <DeleteParameters>
            <asp:Parameter Name="StuffUserId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="StuffUserId" Type="Int32" />
            <asp:Parameter Name="StuffName" Type="String" />
            <asp:Parameter Name="StuffId" Type="String" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="UserPassword" Type="String" />
            <asp:Parameter Name="IsActive" Type="Boolean" />
            <asp:Parameter Name="Telephone" Type="String" />
            <asp:Parameter Name="EMail" Type="String" />
            <asp:Parameter Name="EnglishName" Type="String" />
            <asp:Parameter Name="AttendDate" Type="DateTime" />
            <asp:Parameter Name="TrafficFeeLimit" Type="Decimal" />
            <asp:Parameter Name="TelephoneFeeLimit" Type="Decimal" />
        </UpdateParameters>
        <SelectParameters>
            <asp:Parameter Name="StuffUserId" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="StuffName" Type="String" />
            <asp:Parameter Name="StuffId" Type="String" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="UserPassword" Type="String" />
            <asp:Parameter Name="Telephone" Type="String" />
            <asp:Parameter Name="EMail" Type="String" />
            <asp:Parameter Name="EnglishName" Type="String" />
            <asp:Parameter Name="AttendDate" Type="DateTime" />
            <asp:Parameter Name="TrafficFeeLimit" Type="Decimal" />
            <asp:Parameter Name="TelephoneFeeLimit" Type="Decimal" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
