<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="BaseData_ApplyYear" Codebehind="ApplyYear.aspx.cs" %>

<%@ Register Src="~/UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        方案申请年份维护</div>
    <asp:UpdatePanel ID="upCustomerType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvCustomerType" CssClass="GridView" runat="server" DataSourceID="odsApplyYear"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="ApplyYearID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ApplyYear" HeaderText="申请年份">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtApplyYear" runat="server" Text='<%# Bind("ApplyYear") %>' Width="1180px"
                                CssClass="InputText" MaxLength="4"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblApplyYearName" runat="server" Text='<%# Bind("ApplyYear") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="1208px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFApplyYear" runat="server" ControlToValidate="txtApplyYear"
                                Display="None" ErrorMessage="请您输入年份！" SetFocusOnError="True" ValidationGroup="ApplyYearEdit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtApplyYear"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="年份必须为数字" ValidationGroup="ApplyYearEdit"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="vsApplyYearEdit" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="ApplyYearEdit" />
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True"
                                ValidationGroup="ApplyYearEdit" CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3"  runat="server" CausesValidation="false"
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1"  runat="server" CausesValidation="false"
                                CommandName="Edit" Text="编辑"></asp:LinkButton>
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
                                申请年份
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvApplyYear" runat="server" DataKeyNames="ApplyYearD" DataSourceID="odsApplyYear"
                DefaultMode="Insert"  EnableModelValidation="True" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1208px;">
                                <asp:TextBox ID="txtApplyYearByAdd" runat="server" Text='<%# Bind("ApplyYear") %>'
                                    Width="1180px" CssClass="InputText" ValidationGroup="chanTypeINS" MaxLength="4"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFChanTypeName" runat="server" ControlToValidate="txtApplyYearByAdd"
                                    Display="None" ErrorMessage="请您输入申请年份！" SetFocusOnError="True" ValidationGroup="ApplyYearAdd"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtApplyYearByAdd"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                    ErrorMessage="年份必须为数字" ValidationGroup="ApplyYearAdd"></asp:RegularExpressionValidator>
                                <asp:ValidationSummary ID="vsApplyYearAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="ApplyYearAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="ApplyYearAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="chanTypeInsertValidationSummary" ValidationGroup="chanTypeINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsApplyYear" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteApplyYearById" InsertMethod="InsertApplyYear" SelectMethod="GetApplyYearPaged"
                SelectCountMethod="ApplyYearTotalCount" SortParameterName="sortExpression" UpdateMethod="UpdateApplyYear"
                EnablePaging="true">
                <UpdateParameters>
                    <asp:Parameter Name="ApplyYear" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="ApplyYear" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
