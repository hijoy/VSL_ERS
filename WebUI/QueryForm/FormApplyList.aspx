<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_FormApplyList" Title="�������뵥��ѯ" Codebehind="FormApplyList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="UCCustomer" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        ��������</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        ���ݱ��</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        ��������</div>
                    <asp:TextBox ID="txtFormApplyName" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        ������</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 300px;">
                    <div class="field_title">
                        ���������ڲ���</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="180px" />
                </td>
                <td colspan="2" style="width: 350px;">
                    <div class="field_title">
                        �ͻ�</div>
                    <uc3:UCCustomer ID="UCCustomer" runat="server" Width="220px" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        ����С��</div>
                    <asp:DropDownList ID="SubCategoryDDL" runat="server" DataSourceID="odsSubCategory"
                        DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID"
                        Width="380px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select 0 ExpenseSubCategoryID,' ȫ��' ExpenseSubCategoryName Union SELECT ExpenseSubCategoryID,ExpenseCategoryName+'-'+ExpenseSubCategoryName as ExpenseSubCategoryName FROM ExpenseSubCategory join ExpenseCategory on ExpenseSubCategory.ExpenseCategoryID = ExpenseCategory.ExpenseCategoryID order by ExpenseSubCategoryName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        ֧����ʽ</div>
                    <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                        DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="170px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 PaymentTypeID,' ȫ��' PaymentTypeName union SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] order by PaymentTypeName">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 300px;">
                    <div class="field_title">
                        �����ڼ�</div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td colspan="2" style="width: 350px;">
                    <div class="field_title">
                        �ύ����</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        ȷ��ִ������</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCConfirmBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="Label1" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCConfirmEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        Ԥ������ڼ�</div>
                    <nobr>
                        <uc4:YearAndMonthUserControl ID="UCAccruedPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        <asp:Label ID="Label2" runat="server">~~</asp:Label>
                        <uc4:YearAndMonthUserControl ID="UCAccruedPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </nobr>
                </td>
                <td style="width: 300px;">
                    <div class="field_title">
                        ��������</div>
                    <uc1:UCDateInput ID="ucDeliveryDate" runat="server" IsReadOnly="false" />
                </td>
                <td style="width: 170px;">
                    <div class="field_title">
                        �Ƿ�ȷ��ִ��</div>
                    <asp:DropDownList ID="IsCompleteDDL" runat="server" CssClass="InputCombo" Width="150px">
                        <asp:ListItem Text="ȫ��" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="�Ѿ�ȷ��ִ��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="δȷ��ִ��" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 170px;">
                    <div class="field_title">
                        �����Ƿ�ر�</div>
                    <asp:DropDownList ID="IsCloseDDL" runat="server" CssClass="InputCombo" Width="150px">
                        <asp:ListItem Text="ȫ��" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="�����ѹر�" Value="1"></asp:ListItem>
                        <asp:ListItem Text="����δ�ر�" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 40px">
                <td style="width: 400px;" colspan="2" valign="middle">
                    <span class="field_title">����״̬</span>
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="������" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="�������" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkRejected" runat="server" Text="�˻ش��޸�" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkScrap" runat="server" Text="����" Checked="false" />
                </td>
                <td style="width: 450px;" colspan="2" valign="middle">
                    <span class="field_title">�����Ƿ��Զ����</span>
                    <asp:DropDownList ID="IsAutoSplitDDL" runat="server" CssClass="InputCombo" Width="150px">
                        <asp:ListItem Text="ȫ��" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="���������" Value="1"></asp:ListItem>
                        <asp:ListItem Text="����δ���" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 400px;" colspan="2" valign="middle">
                    <span class="field_title">��������</span>
                    <asp:DropDownList ID="ddlPromotionScope" runat="server" DataSourceID="odsPromotionScope"
                        DataTextField="PromotionScopeName" DataValueField="PromotionScopeID" Width="170px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="odsPromotionScope" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand=" select 0 PromotionScopeID,' ȫ��' PromotionScopeName union SELECT PromotionScopeID,PromotionScopeName FROM PromotionScope order by PromotionScopeName">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" style="width:850px;">
                &nbsp;
            </td>
            <td style=" margin-top:10px; height:20px;  width:97px; color: #004f8b;font-size: 14px;border: 1px solid #bfd9e8; text-align:center; background-image:url(../images/42.gif);background-repeat: repeat-x;">
                    <asp:HyperLink ID="hlExport_Good" Target="_blank" runat="server" style="display:block;vertical-align:middle;line-height:20px;" Text="ʵ���ർ��"></asp:HyperLink>
            </td>
            <td style=" margin-top:10px; height:20px;  width:97px; color: #004f8b;font-size: 14px;border: 1px solid #bfd9e8; text-align:center; background-image:url(../images/42.gif);background-repeat: repeat-x;">
                    <asp:HyperLink ID="hlExport_Total" Target="_blank" runat="server" style="display:block;vertical-align:middle;line-height:20px;" Text="�г��ർ��"></asp:HyperLink>
            </td>
            <td align="right" valign="middle">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="��ѯ" OnClick="btnSearch_Click" />
            </td>
            
        </tr>
    </table>
    <br />
    <div class="title" style="width: 1260px;">
        �������뵥�б�</div>
    <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvApplyList_RowDataBound" OnRowCommand="gvApplyList_RowCommand">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="���ݱ��">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="��������">
                <ItemTemplate>
                    <asp:Label ID="lblFormApplyName" runat="server" Text='<%# FormApplyNameFormat(Eval("FormApplyName"))%>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="����״̬">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="CustomerName" HeaderText="�ͻ�����">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="180px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ShopID" HeaderText="�ŵ�����">
                <ItemTemplate>
                    <asp:Label ID="lblShopName" runat="server" Text='<%# GetShopNameByID(Eval("ShopID")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="140px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Amount" HeaderText="������">
                <ItemTemplate>
                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="80px" HorizontalAlign="right"  />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="Period" HeaderText="�����ڼ�">
                <ItemTemplate>
                    <asp:Label ID="lblPeriod" Text='<%# Bind("Period", "{0:yyyyMM}") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormApply.PaymentTypeID" HeaderText="֧����ʽ">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentType" runat="server" Text='<%# Bind("PaymentTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffName" HeaderText="������">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="50px" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="�ύ����">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="IsClose" HeaderText="�Ƿ�ر�">
                <ItemTemplate>
                    <asp:CheckBox ID="ckIsClose" runat="server" Checked='<%# Bind("IsClose") %>' Enabled="false" />
                </ItemTemplate>
                <ItemStyle Width="50px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AccruedPeriod" HeaderText="Ԥ���ڼ�">
                <ItemTemplate>
                    <asp:Label ID="lblAccruedPeriod" Text='<%# Bind("AccruedPeriod", "{0:yyyyMM}") %>'
                        runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="AccruedAmount" HeaderText="ȷ��ִ�н��">
                <ItemTemplate>
                    <asp:Label ID="lblAccruedAmount" runat="server" Text='<%# Bind("AccruedAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="�ѱ������">
                <ItemTemplate>
                    <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# Bind("PaymentAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��֧�����">
                <ItemTemplate>
                    <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Bind("PaidAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="right" />
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
                    <td scope="col" style="width: 100px;" class="Empty1">
                        ���ݱ��
                    </td>
                    <td style="width: 100px;">
                        ��������
                    </td>
                    <td scope="col" style="width: 60px;">
                        ����״̬
                    </td>
                    <td scope="col" style="width: 180px;">
                        �ͻ�����
                    </td>
                    <td scope="col" style="width: 120px;">
                        �ŵ�����
                    </td>
                    <td scope="col" style="width: 80px;">
                        ������
                    </td>
                    <td scope="col" style="width: 60px;">
                        �����ڼ�
                    </td>
                    <td scope="col" style="width: 60px;">
                        ֧����ʽ
                    </td>
                    <td scope="col" style="width: 50px;">
                        ������
                    </td>
                    <td scope="col" style="width: 70px;">
                        �ύ����
                    </td>
                    <td scope="col" style="width: 60px;">
                        �Ƿ�ر�
                    </td>
                    <td scope="col" style="width: 60px;">
                        Ԥ���ڼ�
                    </td>
                    <td scope="col" style="width: 80px;">
                        ȷ��ִ�н��
                    </td>
                    <td scope="col" style="width: 80px;">
                        �ѱ������
                    </td>
                    <td scope="col" style="width: 80px;">
                        ��֧�����
                    </td>
                </tr>
                <tr>
                    <td colspan="15" style="text-align: center;" class="Empty2 noneLabel">
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
            <asp:BoundColumn HeaderText="��������" DataField="FormApplyName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="����״̬" DataField="Status" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="�ͻ�����" DataField="CustomerName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="�ŵ�����" DataField="ShopName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="������" DataField="Amount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="�����ڼ�" DataField="Period" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="֧����ʽ" DataField="PaymentTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="������" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="�ύ����" DataField="SubmitDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="�����Ƿ�ر�" DataField="IsClose" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="ȷ��ִ��ʱ��" DataField="ConfirmCompleteDate" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormApplyViewByRight" EnablePaging="True" SelectCountMethod="QueryFormApplyViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
