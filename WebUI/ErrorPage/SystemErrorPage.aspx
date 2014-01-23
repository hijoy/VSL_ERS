<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ErrorPage_SystemErrorPage" Title="系统运行故障提示" Codebehind="SystemErrorPage.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center; margin-top:40px; color:Red; font-weight:bold;">对不起，系统运行异常</div>
    <div style="height:500px;">
        <b>详细信息:</b><br />
        <asp:Label ID="lblError" runat="server" Width="800px" Height="500px"></asp:Label>
    </div>
</asp:Content>

