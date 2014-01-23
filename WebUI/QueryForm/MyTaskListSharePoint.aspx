<%@ Page Language="C#" AutoEventWireup="true" Inherits="QueryForm_MyTaskListSharePoint" Codebehind="MyTaskListSharePoint.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>等待我审批的单据</title>
    <link id="Link1" href="~/css/newstyle.css" rel="stylesheet" type="text/css" runat="server" />
<%--    <link id="Link2" href="~/css/Extra.css" rel="stylesheet" type="text/css" runat="server" />
    <link id="Link3" href="~/css/ControlCss.css" rel="stylesheet" type="text/css" runat="server" />--%>
    
</head>
<body>
    <form id="form1" runat="server">


                <gc:GridView CssClass="GridView" ID="gvMyAwaiting" runat="server" DataSourceID="odsMyAwaiting"
                    AutoGenerateColumns="False" DataKeyNames="FormId" AllowPaging="True" AllowSorting="True"
                    PageSize="5" OnRowDataBound="gvMyAwaiting_RowDataBound" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblFormId" runat="server" Text='<%# Bind("FormId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormTypeName" HeaderText="单据类型">
                            <ItemTemplate>
                                <asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormNo" HeaderText="单据编号">
                            <ItemTemplate>
                             <a runat="server" id="FormNo" target="_blank"> <%# Eval("FormNo") %></a> 
                              
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="StuffName" HeaderText="申请人">
                            <ItemTemplate>
                                <asp:Label ID="lblStuffuserId" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="SubmitDate" HeaderText="申请时间">
                            <ItemTemplate>
                                <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="146px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr class="Header">
                                <td style="width: 150px;" class="Empty1">
                                    单据类型
                                </td>
                                <td style="width: 150px;">
                                    单据编号
                                </td>
                                <td style="width: 150px;">
                                    申请人
                                </td>
                                <td style="width: 146px;">
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
                <asp:ObjectDataSource ID="odsMyAwaiting" runat="server" TypeName="BusinessObjects.FormQueryBLL"
                    SelectMethod="GetPagedFormView" EnablePaging="True" SelectCountMethod="QueryFormViewCount"
                    SortParameterName="sortExpression">
                    <SelectParameters>
                        <asp:Parameter Name="queryExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
        </form>
</body>
</html>
