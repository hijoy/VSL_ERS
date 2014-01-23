<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ErrorPage_NoRightErrorPage" Title="无访问权限系统提示" Codebehind="NoRightErrorPage.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center; margin-top:40px; color:Red; font-weight:bold;">对不起，您无权访问此系统或无权执行该操作，如需授权，请和管理员联系</div>
    <div style="height:500px;">
        <b>详细信息:</b><br />
        <asp:Label ID="lblError" runat="server" Width="800px" Height="500px"></asp:Label>
    </div>
</asp:Content>

