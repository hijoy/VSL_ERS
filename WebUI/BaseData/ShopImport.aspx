<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="ShopImport" CodeBehind="ShopImport.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="ucOUSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        门店信息导入</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="field_title">
                        <asp:HyperLink ID="hlTemplateDownload" Text="导入模板下载" runat="server" NavigateUrl="~/TemplateExcel/ShopImportTemplate.xls"></asp:HyperLink></div>
                    <div>
                        <input type="hidden" id="btnclicked" name="btnclicked" value="0" />
                        <input id="fileUpLoad" runat="server" type="file" style="width: 370px" />
                    </div>
                </td>
                <td valign="bottom" colspan="3">
                    <asp:Button ID="btnImport" CssClass="button_nor" runat="server" Text="导入" OnClick="btnImport_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top;">
                <td colspan="2" style="width: 400px;">
                    <div class="field_title">
                        上传时间</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                </nobr>
                </td>
                <td align="right" valign="bottom" style="width: 800px;">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="查询" OnClick="btnSearch_Click" />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div class="title" style="width: 1260px;">
        导入记录</div>
    <asp:UpdatePanel ID="LogUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="LogGridView" CssClass="GridView" runat="server" DataSourceID="LogObjectDataSource"
                AutoGenerateColumns="False" DataKeyNames="LogID" AllowPaging="True" AllowSorting="True"
                PageSize="10" OnSelectedIndexChanged="LogGridView_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField InsertVisible="False" SortExpression="LogId" Visible="False">
                        <ItemStyle Width="0px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="CurrIdLabel" runat="server" Text='<%# Bind("LogID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FileName" HeaderText="文件名称">
                        <ItemStyle Width="350px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="FileNameLabel" runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ImportDate" HeaderText="导入时间">
                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="ImportDateLabel" runat="server" Text='<%# Eval("ImportDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ImportUserID" HeaderText="导入人">
                        <ItemStyle Width="160px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="ImportUserLabel" runat="server" Text='<%# GetStuffNameByID(Eval("ImportUserID")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="TotalCount" HeaderText="总记录数">
                        <ItemStyle Width="170px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="TotalCountLabel" runat="server" Text='<%# Bind("TotalCount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="SuccessCount" HeaderText="成功记录数">
                        <ItemStyle Width="170px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="SuccessCountLabel" runat="server" Text='<%# Bind("SuccessCount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FailCount" HeaderText="失败记录数">
                        <ItemStyle Width="173px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="FailCountLabel" runat="server" Text='<%# Bind("FailCount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="ViewDetailLinkButton" runat="server" CommandName="Select" Text="查看明细"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header" style="height: 22px;">
                            <td style="width: 350px;" class="Empty1">
                                文件名称
                            </td>
                            <td style="width: 180px;">
                                导入时间
                            </td>
                            <td scope="col" style="width: 160px;">
                                导入人
                            </td>
                            <td scope="col" style="width: 170px;">
                                总记录数
                            </td>
                            <td scope="col" style="width: 170px;">
                                成功记录数
                            </td>
                            <td scope="col" style="width: 173px;">
                                失败记录数
                            </td>
                            <td scope="col" style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" class="Empty2 noneLabel">
                                无
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:ObjectDataSource ID="LogObjectDataSource" runat="server" TypeName="BusinessObjects.ImportBLL"
                SelectMethod="GetPagedImportLog" SelectCountMethod="QueryImportLogCount" EnablePaging="true"
                SortParameterName="sortExpression">
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="title" style="width: 1260px;">
        导入记录详细信息</div>
    <asp:UpdatePanel ID="LogDetailUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView Visible="false" ID="LogDetailGridView" CssClass="GridView" runat="server"
                DataSourceID="LogDetailObjectDataSource" AutoGenerateColumns="False" CellPadding="0"
                AllowSorting="True" AllowPaging="True" PageSize="10" DataKeyNames="LogDetailID">
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 200px;" class="Empty1">
                                行数
                            </td>
                            <td scope="col" style="width: 1070px;">
                                错误描述
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="Empty2 noneLabel">
                                无
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="LogDetailId" InsertVisible="False" SortExpression="LogDetailId"
                        Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="LogDetailIdLabel" runat="server" Text='<%# Bind("LogDetailId") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="0px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="行数" SortExpression="Line">
                        <ItemTemplate>
                            <asp:Label ID="LineLabel" runat="server" Text='<%# Bind("Line") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="210px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="错误描述" SortExpression="Error">
                        <ItemTemplate>
                            <asp:Label ID="ErrorLabel" runat="server" Text='<%# Bind("Error") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="1060px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </gc:GridView>
            <asp:ObjectDataSource ID="LogDetailObjectDataSource" runat="server" TypeName="BusinessObjects.ImportBLL"
                SelectMethod="GetPagedImportLogDetail" SelectCountMethod="QueryImportLogDetailCount"
                EnablePaging="True" SortParameterName="sortExpression">
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
