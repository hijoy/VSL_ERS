<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ErrorPage_NoRightErrorPage" Title="�޷���Ȩ��ϵͳ��ʾ" Codebehind="NoRightErrorPage.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center; margin-top:40px; color:Red; font-weight:bold;">�Բ�������Ȩ���ʴ�ϵͳ����Ȩִ�иò�����������Ȩ����͹���Ա��ϵ</div>
    <div style="height:500px;">
        <b>��ϸ��Ϣ:</b><br />
        <asp:Label ID="lblError" runat="server" Width="800px" Height="500px"></asp:Label>
    </div>
</asp:Content>
