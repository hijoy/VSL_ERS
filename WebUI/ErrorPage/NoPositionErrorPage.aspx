<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="NoPositionErrorPage" Codebehind="NoPositionErrorPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>维他奶协同办公系统</title>
    <link href="~/css/style.css" rel="stylesheet" type="text/css" runat="server" />
</head>
<body>
    <div style="text-align: center; margin-top: 60px; color: Red; font-weight: bold;">
        对不起，您无权访问此系统或无权执行该操作，如需授权，请和管理员联系</div>
        <div style="text-align: center; margin-top: 40px;">
        <b>详细信息：</b>
        <asp:Label ID="lblErrorDetail" runat="server" Text="该用户没有设置职务！"></asp:Label></div>
</body>
</html>
