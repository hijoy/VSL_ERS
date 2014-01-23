<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="EmailHistory" Codebehind="EmailHistory.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/CustomerControl.ascx" TagName="CustomerSelect" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div style="width: 1260px;" class="title">
        ��������
    </div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px;">
                    <div class="field_title">
                        �ռ�������</div>
                    <asp:TextBox ID="txtSendTo" MaxLength="20" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width: 330px;" colspan="2">
                    <div class="field_title">
                        ��������</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
                <td style="width: 200px;" class="field_title">
                    <div class="field_title">
                        ���ͽ��</div>
                    <nobr>
                    <asp:DropDownList ID="ddlResult" runat="server"  Width="170">
                    <asp:ListItem Selected="true" Text="ȫ��" Value=""></asp:ListItem>
                    <asp:ListItem Text="�ɹ�" Value="1"></asp:ListItem>
                    <asp:ListItem Text="ʧ��" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 200px;">
                    &nbsp;
                </td>
                <td valign="bottom">
                    <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="��ѯ" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div style="width: 1260px;" class="title">
        �ʼ���ʷ��Ϣ</div>
    <asp:UpdatePanel ID="upEmailHistory" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvEmailHistory" CssClass="GridView" runat="server" DataSourceID="odsEmailHistory"
                AutoGenerateColumns="False" DataKeyNames="EmailHistoryID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="SentTo" HeaderText="�ռ���">
                        <ItemTemplate>
                            <asp:Label ID="lblSendTo" runat="server" Text='<%# Bind("SentTo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="210px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�ʼ�����">
                        <ItemTemplate>
                            <asp:Label ID="lblEmailContentByEdit" runat="server" Text='<%# Bind("EmailContent") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="700px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="SendDate" HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblAddressByEdit" runat="server" Text='<%# Bind("SendDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ResultType" HeaderText="���ͽ��">
                        <ItemTemplate>
                            <asp:Label ID="lblContacterByEdit" runat="server" Text='<%#GetResultTypeName(Eval("ResultType")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="205px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 210px;" class="Empty1">
                                �ռ�������
                            </td>
                            <td style="width: 700px;">
                                �ʼ�����
                            </td>
                            <td style="width: 150px;">
                                ��������
                            </td>
                            <td style="width: 205px;">
                                ���ͽ��
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" class="Empty2 noneLabel">
                                ��
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:ObjectDataSource ID="odsEmailHistory" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                SortParameterName="sortExpression" EnablePaging="true" SelectCountMethod="QueryEmailHistoryCount"
                SelectMethod="GetEmailHistoryPaged">
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
