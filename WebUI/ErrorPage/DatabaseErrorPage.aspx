<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ErrorPage_DatabaseErrorPage" Title="数据库访问故障提示" Codebehind="DatabaseErrorPage.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div style="text-align:center; margin-top:40px; color:Red; font-weight:bold;">
        对不起，数据库访问异常，请联系管理员
   </div>
    <div style="height:500px;">
        <b>详细信息:</b><br />
        <asp:Label ID="lblError" runat="server" Width="800px" Height="500px"></asp:Label>
    </div>
   
</asp:Content>

