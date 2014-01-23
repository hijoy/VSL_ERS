<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BaseData_ReimbursePeriod" Codebehind="ReimbursePeriod.aspx.cs" %>

<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        个人报销费用期间维护</div>
    <asp:UpdatePanel ID="upReimbursePeriod" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvReimbursePeriod" CssClass="GridView" runat="server" DataSourceID="odsReimbursePeriod"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="ReimbursePeriodID"
                AllowPaging="True" AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ApplyYear" HeaderText="报销期间">
                        <ItemTemplate>
                            <asp:Label ID="lblReimbursePeriod" runat="server" Text='<%# Eval("ReimbursePeriod","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="1208px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除" OnClientClick="return confirm('确定删除此行数据吗？');"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 1208px;" class="Empty1">
                                报销期间
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvReimbursePeriod" runat="server" DataKeyNames="ReimbursePeriodD"
                DataSourceID="odsReimbursePeriod" DefaultMode="Insert" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1208px;">
                                <uc4:YearAndMonthUserControl ID="ucNewPeriod" runat="server" SelectedDate='<%# Bind("ReimbursePeriod") %>'
                                    IsReadOnly="false" IsExpensePeriod="true" />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsReimbursePeriod" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteReimbursePeriodById" InsertMethod="InsertReimbursePeriod"
                SelectMethod="GetReimbursePeriodPaged" SelectCountMethod="ReimbursePeriodTotalCount" OnInserting="odsReimbursePeriod_Inserting"
                SortParameterName="sortExpression" UpdateMethod="UpdateReimbursePeriod" EnablePaging="true">
                <InsertParameters>
                    <asp:Parameter Name="ReimbursePeriod" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
