<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SysLog_AuthorizationConfigureLog"
    Title="Ȩ��������־" Codebehind="AuthorizationConfigureLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="SearchUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="background-color: #EEEEEE; border-style: solid; border-width: 1px; width: 815px;">
                <table style="width: 815px;">
                    <tr>
                        <td style="width: 25%;">
                            ���ö���</td>
                        <td style="width: 25%;">
                            ��������</td>
                        <td style="width: 25%;">
                            Ա������</td>
                        <td style="width: 25%;">
                            Ա������</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ObjectDropDownList" runat="server" Width="150px">
                                <asp:ListItem>ȫ��</asp:ListItem>
                                <asp:ListItem>ϵͳ��ɫ����</asp:ListItem>
                                <asp:ListItem>ҵ�������Χ</asp:ListItem>
                                <asp:ListItem>ְ��Ȩ������</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="ActionTypeDropDownList" runat="server" Width="150px">
                                <asp:ListItem>ȫ��</asp:ListItem>
                                <asp:ListItem>���</asp:ListItem>
                                <asp:ListItem>����</asp:ListItem>
                                <asp:ListItem>ɾ��</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="StuffIdCtl" runat="server" Width="150px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="StuffNameCtl" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>                    

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
     
        <gc:GridView ID="RightLogGridView" CssClass="GridView" Width="815px" runat="server" DataSourceID="RightLogDS"
            AutoGenerateColumns="False" DataKeyNames="AuthorizationConfigureLogId" AllowPaging="True"
            AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="AuthorizationConfigureLogId" HeaderText="AuthorizationConfigureLogId"
                    InsertVisible="False" Visible="False" ReadOnly="True" SortExpression="AuthorizationConfigureLogId" />
                <asp:TemplateField HeaderText="���ö���" SortExpression="ConfigureTarget">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ConfigureTarget") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����">
                    <ItemStyle Width="165px" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("NewContent") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����">
                    <ItemStyle Width="150px" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("OldContent") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�޸�����" SortExpression="ConfigureType">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("ConfigureType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="�޸�ʱ��" SortExpression="ConfigureTime">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("ConfigureTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ա������" SortExpression="StuffId">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("StuffId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ա������" SortExpression="StuffName">
                    <ItemStyle Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="Header" />
        </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
        
    <asp:ObjectDataSource ID="RightLogDS" runat="server" EnablePaging="True" SelectCountMethod="AuthorizationConfigureLogTotalCount"
        SelectMethod="GetAuthorizationConfigureLog" SortParameterName="sortExpression"
        TypeName="BusinessObjects.LogBLL">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
