<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="PageMasterData_BulletinDetail" Codebehind="BulletinDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="500" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                ����
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="InputText" Width="100%" ></asp:TextBox>
            </td>
        </tr>
        <tr id="trCreateTime" runat="server">
            <td>
                ����ʱ��
            </td>
            <td>
                <asp:Label ID="lblCreateTime" runat="server" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr id="trCreator" runat="server">
            <td>
                ������
            </td>
            <td>
                <asp:Label ID="lblCreator" runat="server" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                ����
            </td>
            <td>
                <asp:TextBox ID="txtContent" runat="server" CssClass="InputText" TextMode="MultiLine" Rows="40" Columns="80"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td>
                <asp:Button Text="ȷ��" runat="server" id="btnSubmit" OnClick="btnSubmit_Click" /><input type="reset" value="ȡ��" />
            </td>
        </tr>
    </table>
</asp:Content>
