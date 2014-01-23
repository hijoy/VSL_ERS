<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_MyTaskList" Codebehind="MyTaskList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top; height: 40px">
                <td style="width: 200px;">
                    <div class="field_title">
                        单据编号</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        申请人</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="160px"></asp:TextBox>
                </td>
                <td style="width: 280px;">
                    <div class="field_title">
                        申请人所在部门</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="160px" />
                </td>
                <td style="width: 200px;">
                    <div class="field_title">
                        提交日期</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                </nobr>
                </td>
                <td valign="bottom">
                </td>
            </tr>
            <tr>
                <td style="width: 100%; display: inline; float: right" colspan="4" valign="middle">
                    <table>
                        <tr>
                            <td>
                                单据类型：
                            </td>
                            <td>
                                <asp:CheckBoxList ID="ckbType" RepeatDirection="Horizontal" Width="550px" DataSourceID="odsFormType"
                                    DataTextField="FormTypeName" DataValueField="FormTypeID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="odsFormType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT FormTypeID,FormTypeName FROM FormType order by FormTypeID">
        </asp:SqlDataSource>
    </div>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <table width="1200px">
                <tr>
                    <td align="right" valign="middle" colspan="5">
                        <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="查询" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="title" style="width: 1260px;">
                等待我审批的单据</div>
            <gc:GridView CssClass="GridView" ID="gvMyAwaiting" runat="server" DataSourceID="odsMyAwaiting"
                OnPageIndexChanging="gvMyAwaiting_PageIndexChanging" AutoGenerateColumns="False"
                DataKeyNames="FormId" AllowPaging="True" AllowSorting="True" PageSize="20" OnRowDataBound="gvMyAwaiting_RowDataBound"
                CellPadding="0" CellSpacing="0">
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblFormId" runat="server" Text='<%# Bind("FormId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckCtl" runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FormTypeName" HeaderText="单据类型">
                        <ItemTemplate>
                            <asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormTypeName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FormNo" HeaderText="单据编号">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                                Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="StuffName" HeaderText="申请人">
                        <ItemTemplate>
                            <asp:Label ID="lblStuffuserId" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="SubmitDate" HeaderText="申请时间">
                        <ItemTemplate>
                            <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 315px;" class="Empty1">
                                单据类型
                            </td>
                            <td style="width: 315px;">
                                单据编号
                            </td>
                            <td style="width: 315px;">
                                申请人
                            </td>
                            <td style="width: 315px;">
                                申请时间
                            </td>
                        </tr>
                        <tr class="Empty2">
                            <td colspan="4" class="Empty2 noneLabel">
                                无
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsMyAwaiting" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormView" EnablePaging="True" SelectCountMethod="QueryFormViewCount"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                    Text="审批" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc4:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upCustomer" />
    <br />
</asp:Content>
