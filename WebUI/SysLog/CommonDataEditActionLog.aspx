<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SysLog_CommonDataEditActionLog"
    Title="�������ݲ�����¼��ѯ" Codebehind="CommonDataEditActionLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="SearchUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="background-color: #EEEEEE; border-style: solid; border-width: 1px; width: 815px;">
                <table style="width: 815px;">
                    <tr>
                        <td style="width: 25%;">
                            ���ݱ�</td>
                        <td style="width: 25%;">
                            ��������</td>
                        <td style="width: 25%;">
                            Ա������</td>
                        <td style="width: 25%;">
                            Ա������</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DataTableNameDropDownList" runat="server"  Width="150px">
                                <asp:ListItem>ȫ��</asp:ListItem>
                                <asp:ListItem>����</asp:ListItem>
                                <asp:ListItem>����</asp:ListItem>
                                <asp:ListItem>����ԭ��</asp:ListItem>
                                <asp:ListItem>�ͷѱ�׼</asp:ListItem>
                                <asp:ListItem>���÷�ʳ�ޱ�׼</asp:ListItem>
                                <asp:ListItem>���ݱ�Ź���</asp:ListItem>
                                <asp:ListItem>�����ܾ�ԭ��</asp:ListItem>
                                <asp:ListItem>����ϵ��</asp:ListItem>
                                <asp:ListItem>����ϵ��</asp:ListItem>
                                <asp:ListItem>������Ѳ���</asp:ListItem>
                                <asp:ListItem>��ͬ���ò���</asp:ListItem>
                                <asp:ListItem>֧����ʽ����</asp:ListItem>                            
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="ActionTypeDropDownList" runat="server"  Width="150px">
                                <asp:ListItem>ȫ��</asp:ListItem>
                                <asp:ListItem>���</asp:ListItem>
                                <asp:ListItem>����</asp:ListItem>
                                <asp:ListItem>ɾ��</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="StuffIdCtl" runat="server"  Width="150px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="StuffNameCtl" runat="server"  Width="150px"></asp:TextBox></td>
                    </tr>                    
                    <tr>
                        <td>
                            ʱ�䷶Χ:</td>
                        <td style="width:150px">
                            �� <asp:TextBox ID="SrhAfterLogInTimeCtl" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td style="width:160px">
                            �� <asp:TextBox ID="SrhBeforeLogInTimeCtl" runat="server" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="SearchBtn" CssClass="button_nor" runat="server" Text="��ѯ" OnClick="SearchBtn_Click" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="GridViewUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="CommonDataEditActionLogGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="CommonDataEditActionLogId"
                DataSourceID="CommonDataEditActionLogDS" CssClass="GridView" Width="815px" AllowPaging="True" AllowSorting="True">
                <Columns>
                    <asp:BoundField ItemStyle-Width="80px" DataField="StuffId" HeaderText="Ա������" SortExpression="StuffId" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="StuffName" HeaderText="Ա������" SortExpression="StuffName" />
                    <asp:BoundField ItemStyle-Width="80px" DataField="DataTableName" HeaderText="���ݱ�" SortExpression="DataTableName" />
                    <asp:BoundField ItemStyle-Width="80px" DataField="ActionTime" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ActionTime" />
                    <asp:BoundField ItemStyle-Width="200px" DataField="NewValue" HeaderText="������" SortExpression="NewValue" />
                    <asp:BoundField ItemStyle-Width="195px" DataField="OldValue" HeaderText="������" SortExpression="OldValue" />
                    <asp:BoundField ItemStyle-Width="80px" DataField="ActionType" HeaderText="��������" SortExpression="ActionType" />
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table class="GridView" width="815px">
                        <tr>
                            <td>
                                Ա������</td>
                            <td>
                                Ա������</td>
                            <td>
                                ���ݱ�</td>
                            <td>
                                ����ʱ��</td>
                            <td>
                                ������</td>
                            <td>
                                ������</td>
                            <td>
                                ��������</td>
                        </tr>
                        <tr>
                            <td colspan="7" style="text-align:center;">��</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="CommonDataEditActionLogDS" runat="server" EnablePaging="True"
        SelectCountMethod="CommonDataEditActionLogTotalCount"
        SelectMethod="GetCommonDataEditActionLog" SortParameterName="sortExpression"
        TypeName="BusinessObjects.LogBLL">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />            
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
