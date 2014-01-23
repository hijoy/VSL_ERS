<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="OhterForm_TravelReimburseSelect" Culture="Auto" UICulture="Auto" CodeBehind="TravelReimburseSelect.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1258px">
        搜索条件</div>
    <div class="searchDiv">
        <table class="searchTable">
            <tr style="vertical-align: top; height: 40px">
                <td>
                    <div class="field_title">
                        单据编号</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px" MaxLength="20"></asp:TextBox>
                </td>
                <td style="padding-left: 50px">
                    <div class="field_title">
                        提交日期</div>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">至</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                </td>
                <td style="width: 200px;">
                    &nbsp;
                </td>
                <td align="right" valign="bottom">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="查询" OnClick="btnSearch_Click" />&nbsp;
                </td>
            </tr>
        </table>
        <br />
    </div>
    <br />
    <div class="title" style="width: 1258px">
        出差申请单列表</div>
    <asp:UpdatePanel ID="upForm" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView CssClass="GridView" ID="gvApplyList" runat="server" DataSourceID="odsApplyList"
                AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
                PageSize="10" OnRowDataBound="gvApplyList_RowDataBound">
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblFormApplyID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单据编号" SortExpression="FormNo">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnFormNo" runat="server" CausesValidation="False" CommandName="Select"
                                Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="150" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="交通费用" SortExpression="TransportFee">
                        <ItemTemplate>
                            <asp:Label ID="lblTransportFee" runat="server" Text='<%# Bind("TransportFee") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100" HorizontalAlign="right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="住宿费用" SortExpression="HotelFee">
                        <ItemTemplate>
                            <asp:Label ID="lblHotelFee" runat="server" Text='<%# Bind("HotelFee") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100" HorizontalAlign="right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="餐费" SortExpression="TotalFee">
                        <ItemTemplate>
                            <asp:Label ID="lblMealFee" runat="server" Text='<%# Bind("MealFee") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100" HorizontalAlign="right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="其他费用" SortExpression="OtherFee">
                        <ItemTemplate>
                            <asp:Label ID="lblOtherFee" runat="server" Text='<%# Bind("OtherFee") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100" HorizontalAlign="right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="总费用" SortExpression="TotalFee">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalFee" runat="server" Text='<%# Bind("TotalFee") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100" HorizontalAlign="right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="申请人" SortExpression="StuffName">
                        <ItemTemplate>
                            <asp:Label ID="lblStuffName" runat="server" Text='<%# Eval("StuffName")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="120" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="提交日期" SortExpression="SubmitDate">
                        <ItemTemplate>
                            <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="318" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnReimburse" runat="server" Text="报销申请"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="70" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 150px;" class="Empty1">
                                单据编号
                            </td>
                            <td style="width: 100px;">
                                交通费用
                            </td>
                            <td style="width: 100px;">
                                住宿费用
                            </td>
                            <td style="width: 100px;">
                                餐费
                            </td>
                            <td style="width: 100px;">
                                其他费用
                            </td>
                            <td style="width: 100px;">
                                总费用
                            </td>
                            <td style="width: 120px;">
                                申请人
                            </td>
                            <td style="width: 100px;">
                                提交日期
                            </td>
                            <td style="width: 318px;">
                                备注
                            </td>
                            <td style="width: 70px;">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10" class="Empty2 noneLabel">
                                无
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsApplyList" runat="server" SelectMethod="GetPagedFormTravelApplyView"
        TypeName="BusinessObjects.FormQueryBLL" EnablePaging="True" SelectCountMethod="QueryFormTravelApplyViewCount"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upForm" />
</asp:Content>
