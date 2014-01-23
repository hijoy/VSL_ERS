<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ExpenseItem" Codebehind="ExpenseItem.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        ������</div>
    <asp:UpdatePanel ID="upExpenseItem" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvExpenseItem" CssClass="GridView" runat="server" DataSourceID="odsExpenseItem"
                AutoGenerateColumns="False" DataKeyNames="ExpenseItemID" AllowPaging="True" AllowSorting="True"
                PageSize="20" OnRowDeleted="gvExpenseItem_RowDeleted" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ExpenseItemName" HeaderText="����������">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtExpenseItemNameByEdit" runat="server" Text='<%# Bind("ExpenseItemName") %>'
                                Width="280px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItemName" runat="server" Text='<%# Bind("ExpenseItemName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="���������" InsertVisible="False">
                        <EditItemTemplate>
                            <asp:Label ID="lblExpenseCategory" runat="server"></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseCategory" runat="server" Text='<%#GetExpenseCateNameBySubCateId (Eval("ExpenseSubCategoryId")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ExpenseSubCategoryId" HeaderText="������С��">
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplSubCateByEdit" DataSourceID="sdsSubCate"
                                DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID"
                                SelectedValue='<%# Bind("ExpenseSubCategoryId") %>' Width="200px">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseSubCategor" runat="server" Text='<%# GetExpenseSubCateNameById(Eval("ExpenseSubCategoryId") )%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="220px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="AccountingCode" HeaderText="��Ŀ���">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccountingCodeByEdit" runat="server" Text='<%# Bind("AccountingCode") %>'
                                Width="100px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingCode" runat="server" Text='<%# Bind("AccountingCode") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="AccountingName" HeaderText="��Ŀ����">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAccountingNameByEdit" runat="server" Text='<%# Bind("AccountingName") %>'
                                Width="280px" CssClass="InputText" MaxLength="40"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAccountingName" runat="server" Text='<%# Bind("AccountingName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsInContract" HeaderText="�Ƿ��ͬ��">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkInContractByEdit" Checked='<%# Bind("IsInContract") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkInContractByEdit" Enabled="false" Checked='<%# Bind("IsInContract") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="����">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFExpenseItemEdit" runat="server" ControlToValidate="txtExpenseItemNameByEdit"
                                Display="None" ErrorMessage="����������������ƣ�" SetFocusOnError="True" ValidationGroup="expenseItemEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFAccountingCode" runat="server" ControlToValidate="txtAccountingCodeByEdit"
                                Display="None" ErrorMessage="���������Ŀ��ţ�" SetFocusOnError="True" ValidationGroup="expenseItemEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFAccountingName" runat="server" ControlToValidate="txtAccountingNameByEdit"
                                Display="None" ErrorMessage="���������Ŀ���ƣ�" SetFocusOnError="True" ValidationGroup="expenseItemEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsExpenseItemEdit" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="expenseItemEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="expenseItemEdit" CommandName="Update" Text="����"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text="ȡ��"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text="�༭"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 300px;" class="Empty1">
                                ����������
                            </td>
                            <td style="width: 120px;">
                                ���ô���
                            </td>
                            <td style="width: 220px;">
                                ����С��
                            </td>
                            <td style="width: 120px;">
                                ��Ŀ���
                            </td>
                            <td style="width: 300px;">
                                ��Ŀ����
                            </td>
                            <td style="width: 90px;">
                                �Ƿ��ͬ��
                            </td>
                            <td style="width: 50px;">
                                ����
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="ExpenseItemFormView" runat="server" DataKeyNames="ExpenseItemID"
                DataSourceID="odsExpenseItem" DefaultMode="Insert" Visible="<%# HasManageRight %>"
                EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 300px;">
                                <asp:TextBox ID="txtExpenseItemNameByAdd" runat="server" Text='<%# Bind("ExpenseItemName") %>'
                                    Width="280px" CssClass="InputText" MaxLength="20" ValidationGroup="ItemIns"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:Label runat="server" ID="lblExpenseCategoryNameByAdd" Text=""></asp:Label>
                            </td>
                            <td align="center" style="width: 220px;">
                                <asp:DropDownList runat="server" ID="dplSubCateByAdd" DataSourceID="sdsSubCate" DataTextField="ExpenseSubCategoryName"
                                    DataValueField="ExpenseSubCategoryID" SelectedValue='<%# Bind("ExpenseSubCategoryId") %>'
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="width: 120px;">
                                <asp:TextBox ID="txtAccountingCodeByAdd" runat="server" Text='<%# Bind("AccountingCode") %>'
                                    Width="100px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 300px;">
                                <asp:TextBox ID="txtAccountingNameByAdd" runat="server" Text='<%# Bind("AccountingName") %>'
                                    Width="280px" CssClass="InputText" MaxLength="40"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 90px;">
                                <asp:CheckBox runat="server" ID="chkInContractByAdd" Checked='<%# Bind("IsInContract") %>' />
                            </td>
                            <td align="center" style="width: 50px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFExpenseItemEdit" runat="server" ControlToValidate="txtExpenseItemNameByAdd"
                                    Display="None" ErrorMessage="����������������ƣ�" SetFocusOnError="True" ValidationGroup="expenseItemAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFAccountingCode" runat="server" ControlToValidate="txtAccountingCodeByAdd"
                                    Display="None" ErrorMessage="���������Ŀ��ţ�" SetFocusOnError="True" ValidationGroup="expenseItemAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFAccountingName" runat="server" ControlToValidate="txtAccountingNameByAdd"
                                    Display="None" ErrorMessage="���������Ŀ���ƣ�" SetFocusOnError="True" ValidationGroup="expenseItemAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsExpenseItemEdit" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="expenseItemAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="����" ValidationGroup="expenseItemAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="CateInsertValidationSummary" ValidationGroup="CateIns"
                        ShowMessageBox="true" ShowSummary="false" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsExpenseItem" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="ExpenseItemTotalCount"
                DeleteMethod="DeleteExpenseItem" InsertMethod="InsertExpenseItem" SelectMethod="GetExpenseItemPaged"
                UpdateMethod="UpdateExpenseItem" OnDeleting="odsExpenseItem_Deleting" OnInserting="odsExpenseItem_Inserting"
                OnUpdating="odsExpenseItem_Updating" oninserted="odsExpenseItem_Inserted" 
                onupdated="odsExpenseItem_Updated">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ExpenseItemName" Type="String" Size="50" />
                    <asp:Parameter Name="ExpenseSubCategoryID" Type="Int32" />
                    <asp:Parameter Name="AccountingCode" Type="String" Size="50" />
                    <asp:Parameter Name="AccountingName" Type="String" Size="50" />
                    <asp:Parameter Name="IsInContract" Type="Boolean" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ExpenseItemName" Type="String" Size="50" />
                    <asp:Parameter Name="ExpenseSubCategoryID" Type="Int32" />
                    <asp:Parameter Name="AccountingCode" Type="String" Size="50" />
                    <asp:Parameter Name="AccountingName" Type="String" Size="50" />
                    <asp:Parameter Name="IsInContract" Type="Boolean" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:SqlDataSource ID="sdsSubCate" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                SelectCommand="SELECT [ExpenseSubCategoryID], [ExpenseSubCategoryName] FROM [ExpenseSubCategory] where IsActive=1">
            </asp:SqlDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
