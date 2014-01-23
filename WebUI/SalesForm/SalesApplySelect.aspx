<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SalesForm_SalesApplySelect" Title="��������" Codebehind="SalesApplySelect.aspx.cs" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/CustomerControl.ascx" TagName="CustomerSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <asp:UpdatePanel ID="upBudgetAllocationOutDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="title" style="width: 1260px">
                ������д������Ϣ���ٵ����һ�����з�������</div>
            <div class="searchDiv" style="width: 1270px;">
                <table class="searchTable" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="width: 350px;">
                            <div class="field_title">
                                �����ڼ�<label class="requiredLable">*</label></div>
                            <uc1:YearAndMonthUserControl ID="UCPeriodBegin" runat="server" IsReadOnly="false"
                                IsExpensePeriod="true" />
                            <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                            <uc1:YearAndMonthUserControl ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                        </td>
                        <td style="width: 250px;">
                            <div class="field_title">
                                �ͻ�<label class="requiredLable">*</label></div>
                            <div>
                                <asp:DropDownList ID="CustomerDDL" runat="server" DataSourceID="odsCustomer" DataTextField="CustomerName"
                                    DataValueField="CustomerID" Width="200px">
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="width: 350px;">
                            <div class="field_title">
                                ����С��<label class="requiredLable">*</label></div>
                            <div>
                                <asp:DropDownList ID="SubCategoryDDL" runat="server" DataSourceID="odsSubCategory"
                                    DataTextField="ExpenseSubCategoryName" DataValueField="ExpenseSubCategoryID"
                                    Width="300px" OnSelectedIndexChanged="SubCategoryDDL_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td style="width: 250px;">
                            <div id="PromotionPriceTypeTD" runat="server" visible="false">
                                <div class="field_title">
                                    ��Ʒ��������<label class="requiredLable">*</label></div>
                                <div>
                                    <asp:DropDownList runat="server" ID="PromotionPriceTypeDDL" Width="200px">
                                        <asp:ListItem Text="�����ۿ�����" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="�����" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <asp:ObjectDataSource ID="odsCustomer" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetCustomerByPositionID" TypeName="BusinessObjects.MasterDataBLL">
                        <SelectParameters>
                            <asp:Parameter Name="PositionID" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:SqlDataSource ID="odsSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="select 0 ExpenseSubCategoryID,' ��ѡ�����С��' ExpenseSubCategoryName Union SELECT ExpenseSubCategoryID,ExpenseCategoryName+'-'+ExpenseSubCategoryName as ExpenseSubCategoryName FROM ExpenseSubCategory join ExpenseCategory on ExpenseSubCategory.ExpenseCategoryID = ExpenseCategory.ExpenseCategoryID where ExpenseSubCategory.IsActive = 1 order by ExpenseSubCategoryName">
                    </asp:SqlDataSource>
                </table>
            </div>
            <div style="margin-top: 20px; width: 1250px; text-align: right">
                <asp:Button ID="NextButton" runat="server" CssClass="button_nor" Text="��һ��" OnClick="NextButton_Click"
                    Width="100px" />&nbsp;&nbsp;
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        var beginDate = document.getElementById("ctl00_ContentPlaceHolder1_UCPeriodBegin_txtDate");
        var endDate = document.getElementById("ctl00_ContentPlaceHolder1_UCPeriodEnd_txtDate");
        beginDate.onpropertychange = function () {
            endDate.value = beginDate.value;
        }
    </script>
</asp:Content>
