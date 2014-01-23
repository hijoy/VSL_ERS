<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_FormReimburseList" Title="������������ѯ" Codebehind="FormReimburseList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        ��������</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width:200px;">
                    <div class="field_title">
                        ���������</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:200px;">
                    <div class="field_title">
                        ���뵥���(������д��ȫ)</div>
                    <asp:TextBox ID="txtApplyFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:200px;">
                    <div class="field_title">
                        ������</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:200px;">
                    <div class="field_title">
                        ֧����ʽ</div>
                    <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                        DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="170px">
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="width:400px;">
                    <div class="field_title">
                        �ύ����</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                    SelectCommand=" select 0 PaymentTypeID,' ȫ��' PaymentTypeName union SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] order by PaymentTypeName">
                </asp:SqlDataSource>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="field_title">
                        ���������ڲ���</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="230px" />
                </td>
                <td colspan="2">
                    <div class="field_title">
                        �ͻ�</div>
                    <uc3:UCCustomer ID="UCCustomer" runat="server" Width="230px" />
                </td>
                <td >
                    <div class="field_title">
                        �Ƿ񷢻����</div>
                    <asp:DropDownList ID="IsDeliveryCompleteDDL" runat="server" CssClass="InputCombo" Width="150px">
                        <asp:ListItem Text="ȫ��" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="�Ѿ��������" Value="1"></asp:ListItem>
                        <asp:ListItem Text="δ�������" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 100%;" colspan="6" valign="middle">
                    <span class="field_title">����״̬</span>
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="������" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="�������" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkRejected" runat="server" Text="�˻ش��޸�" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkScrap" runat="server" Text="����" Checked="false" />
                </td>
            </tr>
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" valign="middle" colspan="6">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="��ѯ" OnClick="btnSearch_Click" />&nbsp;
                <asp:Button ID="btnExport" runat="server" CssClass="button_nor" Text="����" OnClick="btnExport_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="title" style="width: 1260px;">
        �����������б�</div>
    <gc:GridView CssClass="GridView" ID="gvReimburseList" runat="server" DataSourceID="odsReimburseList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvReimburseList_RowDataBound" OnRowCommand="gvReimburseList_RowCommand">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormReimburseID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="���ݱ��">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="200px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="����״̬">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="�ͻ�����">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="360px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormReimburse.PaymentTypeID" HeaderText="֧����ʽ">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Amount" HeaderText="�������">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffName" HeaderText="������">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="�ύ����">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="153px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsDeliveryComplete" HeaderText="�Ƿ񷢻����">
                <ItemTemplate>
                    <asp:CheckBox ID="ckIsDeliveryComplete" runat="server" Checked='<%# Bind("IsDeliveryComplete") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" Text="����" runat="server" CausesValidation="false"
                        CommandName="scrap" CommandArgument='<%# Bind("FormID") %>' OnClientClick="return confirm('����Ҫ���ϸõ��ݣ�')"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="border: 0;">
                    <td scope="col" style="width: 200px;" class="Empty1">
                        ���ݱ��
                    </td>
                    <td scope="col" style="width: 100px;">
                        ����״̬
                    </td>
                    <td scope="col" style="width: 460px;">
                        �ͻ�����
                    </td>
                    <td scope="col" style="width: 100px;">
                        ֧����ʽ
                    </td>
                    <td scope="col" style="width: 150px;">
                        �������
                    </td>
                    <td scope="col" style="width: 100px;">
                        ������
                    </td>
                    <td scope="col" style="width: 153px;">
                        �ύ����
                    </td>
                </tr>
                <tr>
                    <td colspan="7" style="text-align: center;" class="Empty2 noneLabel">
                        ��
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:DataGrid ID="ExportDataGrid" runat="server" Visible="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundColumn HeaderText="���ݱ��" DataField="FormNo" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="����״̬" DataField="Status" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="�ͻ�����" DataField="CustomerName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="֧����ʽ" DataField="PaymentTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="�������" DataField="Amount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="������" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="�ύ����" DataField="SubmitDate" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsReimburseList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormReimburseViewByRight" EnablePaging="True" SelectCountMethod="QueryFormReimburseViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
