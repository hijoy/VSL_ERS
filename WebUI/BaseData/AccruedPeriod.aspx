<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BaseData_AccruedPeriod" Codebehind="AccruedPeriod.aspx.cs" %>

<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        预提费用期间维护</div>
    <asp:UpdatePanel ID="upAccruedPeriod" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvAccruedPeriod" CssClass="GridView" runat="server" DataSourceID="odsAccruedPeriod"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="AccruedPeriodID" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField HeaderText="费用期间">
                        <ItemTemplate>
                            <asp:Label ID="lblAccruedPeriod" runat="server" Text='<%# Eval("AccruedPeriod","{0:yyyy-MM}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="1208px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除" ></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 1208px;" class="Empty1">
                                费用期间
                            </td>
                            <td style="width: 60px;" >
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvAccruedPeriod" runat="server" DataKeyNames="AccruedPeriodD"
                DataSourceID="odsAccruedPeriod" DefaultMode="Insert" EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1208px;">
                                <uc4:YearAndMonthUserControl ID="ucNewPeriod" runat="server" SelectedDate='<%# Bind("AccruedPeriod") %>'
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
            <asp:ObjectDataSource ID="odsAccruedPeriod" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteAccruedPeriodById" InsertMethod="InsertAccruedPeriod"
                SelectMethod="GetAccruedPeriod" OnInserting="odsAccruedPeriod_Inserting">
                <InsertParameters>
                    <asp:Parameter Name="AccruedPeriod" Type="String" />
                </InsertParameters>
                <DeleteParameters>
                    <asp:Parameter Name="AccruedPeriodID" Type="Int32" />
                </DeleteParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
