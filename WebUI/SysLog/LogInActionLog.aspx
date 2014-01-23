<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SysLog_LogInActionLog" Title="�û���¼��־" Codebehind="LogInActionLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="SearchUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="background-color: #EEEEEE; border-style: solid; border-width: 1px; width: 815px;">
                <table style="width: 815px;">
                    <tr>
                        <td style="width: 25%;">
                            Ա������</td>
                        <td style="width: 25%;">
                            Ա������</td>
                        <td style="width: 25%;">
                            IP��ַ</td>
                        <td style="width: 25%;">
                            �Ƿ�ɹ�</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="SrhStuffIdCtl" runat="server" Width="100px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="SrhStuffNameCtl" runat="server" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="SrhClientIPCtl" runat="server" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:DropDownList ID="LogActoinList" runat="server" Width="150px">
                                <asp:ListItem>ȫ��</asp:ListItem>
                                <asp:ListItem>�ɹ�</asp:ListItem>
                                <asp:ListItem>ʧ��</asp:ListItem>
                            </asp:DropDownList>                        
                        </td>
                    </tr>

                    <tr>
                        <td>
                            ʱ�䷶Χ:</td>
                        <td style="width:150px">
                            �� <asp:TextBox ID="SrhAfterLogInTimeCtl" runat="server"></asp:TextBox>
                        </td>
                        <td style="width:150px">
                            �� <asp:TextBox ID="SrhBeforeLogInTimeCtl" runat="server"></asp:TextBox></td>
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
            <gc:GridView ID="LogInActionLogGridView" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="LogInActionLogId" DataSourceID="LogInActionLogDS"
                CssClass="GridView" Width="815px">
                <Columns>
                    <asp:BoundField DataField="LogInActionLogId" HeaderText="LogInActionLogId" InsertVisible="False"
                        ReadOnly="True" Visible="false" SortExpression="LogInActionLogId" />
                    <asp:BoundField DataField="StuffId" HeaderText="����" SortExpression="StuffId" />
                    <asp:BoundField DataField="StuffName" HeaderText="Ա������" SortExpression="StuffName" />
                    <asp:BoundField DataField="UserName" HeaderText="��¼�ʺ�" SortExpression="UserName" />
                    <asp:CheckBoxField DataField="Success" HeaderText="��¼�ɹ�" SortExpression="Success" />
                    <asp:BoundField DataField="LogInTime" HeaderText="��¼ʱ��" SortExpression="LogInTime" />
                    <asp:BoundField DataField="ClientIP" HeaderText="�ͻ���IP" SortExpression="ClientIP" />
                </Columns>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource EnablePaging="true" ID="LogInActionLogDS" runat="server" SelectCountMethod="TotalCount"
        SelectMethod="GetLogInActionLog" TypeName="BusinessObjects.LogBLL" SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
