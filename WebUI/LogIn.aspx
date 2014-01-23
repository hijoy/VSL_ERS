<%@ Page Language="C#" AutoEventWireup="true" Inherits="LogIn" Codebehind="LogIn.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>维他奶协同办公系统</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .input
        {
            border-right: #4f4f4f 1px solid;
            padding-right: 1px;
            border-top: #4f4f4f 1px solid;
            padding-left: 1px;
            font-size: 9pt;
            padding-bottom: 1px;
            border-left: #4f4f4f 1px solid;
            padding-top: 1px;
            border-bottom: #4f4f4f 1px solid;
            height: 18px;
            background-color: #e7e7e7;
        }
        .textarea
        {
            border-top-width: 1px;
            padding-right: 1px;
            padding-left: 1px;
            border-left-width: 1px;
            font-size: 9pt;
            border-left-color: #cccccc;
            border-bottom-width: 1px;
            border-bottom-color: #cccccc;
            padding-bottom: 1px;
            border-top-color: #cccccc;
            padding-top: 1px;
            font-family: 宋体;
            background-color: #efefef;
            border-right-width: 1px;
            border-right-color: #cccccc;
        }
        .select
        {
            border-right: #717171 1px solid;
            padding-right: 1px;
            border-top: #717171 1px solid;
            padding-left: 1px;
            font-size: 9pt;
            padding-bottom: 1px;
            border-left: #717171 1px solid;
            padding-top: 1px;
            border-bottom: #717171 1px solid;
            font-family: "Verdana" , "Arial" , "Helvetica" , "sans-serif";
            background-color: #fffbf0;
        }
        .12
        {
            font-size: 12px;
            color: #000000;
            font-family: "Verdana" , "Arial" , "Helvetica" , "sans-serif";
        }
        .LoginBulletin
        {
            font-size: 12px;
            color: black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }
        .LoginBulletin A
        {
            color: #860001;
            line-height: 150%;
            text-decoration: none;
        }
        .LoginBulletin A:hover
        {
            color: #ff0033;
            text-decoration: underline;
        }
        .LogInBtn
        {
            border-top-width: 0px;
            border-left-width: 0px;
            border-bottom-width: 0px;
            border-right-width: 0px;
            background-image: url(Images/LogIn/b_login.gif);
            width: 40px;
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
    <div style="height: 130px">
        &nbsp;
    </div>
    <table id="Table1" height="100%" cellspacing="1" cellpadding="1" width="100%" border="0">
        <tbody>
            <tr>
                <td valign="center" align="middle" colspan="2">
                    <div>
                        <table cellspacing="0" cellpadding="0" width="483" border="0">
                            <tbody>
                                <tr>
                                    <td style="width: 483px; height: 121px; text-align:center;">
                                        <img height="121" src="images/LogIn/login_top2.jpg" width="478px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 483px; height: 101px;">
                                        <table cellspacing="6px" cellpadding="0px" width="480px" style="height: 100%" background="Images/login_di.jpg"
                                            border="0px">
                                            <tr>
                                                <td align="right" width="38%">
                                                    <strong><span style="font-size: 10pt">系统登录帐号/User ID：</span></strong>
                                                </td>
                                                <td style="width: 235px">
                                                    <asp:TextBox ID="UserIdCtl" runat="server" MaxLength="50" Width="120px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 28px" align="right">
                                                    <strong><span style="font-size: 10pt">密码/Password：</span></strong>
                                                </td>
                                                <td style="width: 235px; height: 28px">
                                                    <asp:TextBox ID="PasswordCtl" runat="server" MaxLength="50" TextMode="Password" Width="120px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="12" align="right">
                                                </td>
                                                <td>
                                                    <b>
                                                        <asp:Label ID="MessageCtl" ForeColor="red" Text="注意区分大小写" runat="server"></asp:Label></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="12" align="right">
                                                </td>
                                                <td>
                                                    <b>
                                                        <asp:Label ID="ErrorCtl" ForeColor="red"  runat="server"></asp:Label></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="12" align="right">
                                                    <strong><font color="#ffffff">&nbsp;</font></strong>
                                                </td>
                                                <td style="width: 235px">
                                                    &nbsp;
                                                    <asp:ImageButton ID="LogInBtn" ImageUrl="~/Images/b_login.gif" OnClick="LogInBtn_Click"
                                                        runat="server" />
                                                    <asp:CustomValidator ID="LogInValidator" runat="server" Display="None" OnServerValidate="LogInValidator_ServerValidate"></asp:CustomValidator>
                                                </td>
                                                <td style="width: 172px">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="12" align="right">
                                                    <strong><font color="#ffffff">&nbsp;</font></strong>
                                                </td>
                                                <td style="width: 235px">
                                                    &nbsp;
                                                </td>
                                                <td style="width: 172px">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 483px; height: 18px">
                                        <img height="18" src="images/login_bot.jpg" width="481">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <br>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
