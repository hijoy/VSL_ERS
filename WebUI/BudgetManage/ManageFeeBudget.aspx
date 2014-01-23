<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ManageFeeBudget" Codebehind="ManageFeeBudget.aspx.cs" %>

<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="ucOUSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        ��������</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 400px;">
                    <div class="field_title">
                        Ԥ�㲿��</div>
                    <uc1:ucOUSelect runat="server" ID="ucSearchOU" Width="220px" />
                </td>
                <td style="width: 300px;">
                    <div class="field_title">
                        ����</div>
                    <nobr>
                            <uc2:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false"
                                IsExpensePeriod="true" />
                            <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                            <uc2:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                </td>
                <td style="width: 250px;">
                    <div class="field_title">
                        ������</div>
                    <asp:DropDownList ID="SearchExpenseTypeDDL" runat="server" DataSourceID="odsSearchExpenseType"
                        DataTextField="ExpenseManageTypeName" DataValueField="ExpenseManageTypeID" Width="150px">
                    </asp:DropDownList>
                </td>
                <td style="width: 100px;" align="center" valign="bottom">
                    <asp:Button ID="SearchBtn" CssClass="button_nor" runat="server" Text="��ѯ" OnClick="SearchBtn_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1260px;">
        �������Ԥ����Ϣ</div>
    <asp:UpdatePanel ID="UPBudget" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView CssClass="GridView" ID="GVBudget" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BudgetManageFeeID" CellPadding="0" DataSourceID="odsBudget" AllowPaging="True"
                AllowSorting="true" PageSize="20" OnSelectedIndexChanged="GVBudget_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="Ԥ�㲿��" SortExpression="OrganizationUnitID">
                        <EditItemTemplate>
                            <asp:Label ID="lblOrganizationUnit1" runat="server" Text='<%# GetOUNameByOuID(Eval("OrganizationUnitID")) %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbOrganizationUnit" runat="server" CommandName="Select" Text='<%# GetOUNameByOuID(Eval("OrganizationUnitID"))%>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����ڼ�" SortExpression="Period">
                        <EditItemTemplate>
                            <asp:Label ID="lblPeriod1" runat="server" Text='<%# Eval("Period", "{0:yyyy/MM}") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriod2" runat="server" Text='<%# Eval("Period", "{0:yyyy/MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="������" SortExpression="ExpenseManageTypeID">
                        <EditItemTemplate>
                            <asp:Label ID="expenseTypelbl1" runat="server" Text='<%# GetExpenseTypeNameByID(Eval("ExpenseManageTypeID")) %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="expenseTypelbl2" runat="server" Text='<%# GetExpenseTypeNameByID(Eval("ExpenseManageTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ʼԤ��" SortExpression="OriginalBudget">
                        <EditItemTemplate>
                            <asp:Label ID="lblOriginalBudget1" runat="server" Text='<%# Eval("OriginalBudget", "{0:N}") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOriginalBudget2" runat="server" Text='<%# Eval("OriginalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����Ԥ��" SortExpression="NormalBudget">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNormalBudget" runat="server" Text='<%# Bind("NormalBudget") %>'
                                Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="txtNormalBudget"
                                Display="None" ErrorMessage="������������Ԥ�㣡" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtNormalBudget"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="��������ȷ����ʽ" ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNormalBudget" runat="server" Text='<%# Eval("NormalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ԥ�����" SortExpression="AdjustBudget">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAdjustBudget" runat="server" Text='<%# Bind("AdjustBudget") %>'
                                Width="80px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="txtAdjustBudget"
                                ValidationExpression="<%$ Resources:RegularExpressions, MinusMoney %>" Display="None"
                                ErrorMessage="��������ȷ����ʽ" ValidationGroup="EDIT"></asp:RegularExpressionValidator>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��Ԥ��" SortExpression="TotalBudget">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalBudget" runat="server" Text='<%# Eval("TotalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�޸�ԭ��">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>'
                                Width="240px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RF4" runat="server" ControlToValidate="txtModifyReason"
                                Display="None" ErrorMessage="���������޸�ԭ��" ValidationGroup="EDIT"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryEDIT" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="EDIT" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="261px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                                Text="����" ValidationGroup="EDIT"></asp:LinkButton>
                            <asp:LinkButton ID="CancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="ȡ��"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="EditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="�༭"></asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="ɾ��" OnClientClick="return confirm('ȷ��ɾ������������');"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 250px" class="Empty1">
                                Ԥ�㲿��
                            </td>
                            <td style="width: 120px">
                                �����ڼ�
                            </td>
                            <td style="width: 150px">
                                ������
                            </td>
                            <td style="width: 100px">
                                ��ʼԤ��
                            </td>
                            <td style="width: 100px">
                                ����Ԥ��
                            </td>
                            <td style="width: 100px">
                                Ԥ�����
                            </td>
                            <td style="width: 100px">
                                ��Ԥ��
                            </td>
                            <td style="width: 280px">
                                �޸�ԭ��
                            </td>
                            <td style="width: 80px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BudgetAddFormView" EventName="ItemInserted" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="AddUpdatePanel" runat="server">
        <ContentTemplate>
            <asp:FormView DefaultMode="insert" ID="BudgetAddFormView" runat="server" DataKeyNames="BudgetManageFeeID"
                DataSourceID="odsBudget" Visible="<%# HasManageRight %>" CellPadding="0">
                <InsertItemTemplate>
                    <table style="height: 30px;" class="FormView">
                        <tr>
                            <td align="center" style="width: 250px;">
                                <uc1:ucOUSelect runat="server" ID="ucNewOuSelect" IsNoClear="true" OUId='<%# Bind("OrganizationUnitID") %>'
                                    Width="190px" />
                            </td>
                            <td align="center" style="width: 120px;">
                                <uc2:YearAndMonthUserControl ID="ucNewPeriod" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                            </td>
                            <td align="center" style="width: 150px;">
                                <asp:DropDownList ID="newExpenseTypeDDL" runat="server" SelectedValue='<%# Bind("ExpenseManageTypeID") %>'
                                    DataSourceID="odsExpenseType" DataTextField="ExpenseManageTypeName" DataValueField="ExpenseManageTypeID"
                                    Width="130px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtNewOriginalBudget" runat="server" Text='<%# Bind("OriginalBudget") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtNewNormalBudget" runat="server" Text='<%# Bind("NormalBudget") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtNewAdjustBudget" runat="server" Text='<%# Bind("AdjustBudget") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                            </td>
                            <td align="center" style="width: 260px;">
                                <asp:TextBox ID="txtNewModifyReason" runat="server" Text='<%# Bind("ModifyReason") %>'
                                    Width="240px"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 80px;">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="����" ValidationGroup="INS"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="NewRF1" runat="server" ControlToValidate="txtNewOriginalBudget"
                        Display="None" ErrorMessage="��¼���ʼԤ�㣡" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="NewRF2" runat="server" ControlToValidate="txtNewNormalBudget"
                        Display="None" ErrorMessage="��¼������Ԥ�㣡" SetFocusOnError="True" ValidationGroup="INS"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="txtNewOriginalBudget"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="��������ȷ����ʽ" ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF4" runat="server" ControlToValidate="txtNewNormalBudget"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="��������ȷ����ʽ" ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF5" runat="server" ControlToValidate="txtNewAdjustBudget"
                        ValidationExpression="<%$ Resources:RegularExpressions, MinusMoney %>" Display="None"
                        ErrorMessage="��������ȷ����ʽ" ValidationGroup="INS"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="INS" />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GVBudget" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsBudget" runat="server" DeleteMethod="DeleteBudgetManageFeeByID"
        InsertMethod="InsertBudgetManageFee" OldValuesParameterFormatString="{0}" SelectMethod="GetPagedBudgetManageFee"
        TypeName="BusinessObjects.BudgetBLL" UpdateMethod="UpdateBudgetManageFee" EnablePaging="true"
        SortParameterName="sortExpression" SelectCountMethod="QueryBudgetManageFeeTotalCount"
        OnInserting="odsBudget_Inserting" OnUpdating="odsBudget_Updating" OnUpdated="odsBudget_Updating">
        <DeleteParameters>
            <asp:Parameter Name="BudgetManageFeeID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="BudgetManageFeeID" Type="Int32" />
            <asp:Parameter Name="NormalBudget" Type="Decimal" />
            <asp:Parameter Name="AdjustBudget" Type="Decimal" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
            <asp:Parameter Name="ModifyReason" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="OrganizationUnitID" Type="Int32" />
            <asp:Parameter Name="Period" Type="DateTime" />
            <asp:Parameter Name="ExpenseManageTypeID" Type="Int32" />
            <asp:Parameter Name="OriginalBudget" Type="Decimal" />
            <asp:Parameter Name="NormalBudget" Type="Decimal" />
            <asp:Parameter Name="AdjustBudget" Type="Decimal" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
            <asp:Parameter Name="ModifyReason" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="odsSearchExpenseType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
        SelectCommand="select 0 ExpenseManageTypeID,'ȫ��' ExpenseManageTypeName union SELECT [ExpenseManageTypeID], [ExpenseManageTypeName] FROM [ExpenseManageType] ">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="odsExpenseType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
        SelectCommand=" SELECT [ExpenseManageTypeID], [ExpenseManageTypeName] FROM [ExpenseManageType] where IsActive = 1">
    </asp:SqlDataSource>
    <br />
    <div class="title" style="width: 1260px;">
        �����ʷ</div>
    <asp:UpdatePanel ID="UPHistory" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView CssClass="GridView" ID="GVHistory" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BudgetManageFeeHistoryID" DataSourceID="odsHistory" AllowPaging="false"
                AllowSorting="false">
                <Columns>
                    <asp:TemplateField HeaderText="����Ԥ��">
                        <ItemTemplate>
                            <asp:Label ID="lblNormalBudget" runat="server" Text='<%# Eval("NormalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ԥ�����">
                        <ItemTemplate>
                            <asp:Label ID="lblAdjustBudget" runat="server" Text='<%# Eval("AdjustBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��Ԥ��">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalBudget" runat="server" Text='<%# Eval("TotalBudget", "{0:N}") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�޸���">
                        <ItemTemplate>
                            <asp:Label ID="lblUser" runat="server" Text='<%# GetUserNameByID(Eval("UserID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�޸���ְ��">
                        <ItemTemplate>
                            <asp:Label ID="lblPosition" runat="server" Text='<%# GetPositionNameByID(Eval("PositionID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����">
                        <ItemTemplate>
                            <asp:Label ID="lblAction" runat="server" Text='<%# Eval("Action") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�޸�ʱ��">
                        <ItemTemplate>
                            <asp:Label ID="lblModifyDate" runat="server" Text='<%# Eval("ModifyDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�޸�ԭ��">
                        <ItemTemplate>
                            <asp:Label ID="lblModifyReason" runat="server" Text='<%# Eval("ModifyReason") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="560px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GVBudget" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsHistory" runat="server" OldValuesParameterFormatString="{0}"
        SelectMethod="GetBudgetManageFeeHistoryByParentID" TypeName="BusinessObjects.BudgetBLL">
        <SelectParameters>
            <asp:Parameter Name="BudgetManageFeeID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
