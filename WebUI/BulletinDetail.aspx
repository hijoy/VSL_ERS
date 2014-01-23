<%@ Page Language="C#" AutoEventWireup="true" Inherits="BulletinDetail" Codebehind="BulletinDetail.aspx.cs" %>
<%@ Register Src="UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统公告</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="tdborder06" style="background-color: #F6F6F6">
        <table width="820px" style="margin-left: 5px; margin-right: 5px; margin-top: 5px;margin-bottom: 5px">
            <tr align="center" style="width:820px">
                <td align="center">
                <asp:Label Font-Bold="true" Font-Size="14pt" ID="BulletinTitleLabel" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="CreateTimeLabel" runat="server" />
                    <br/><br/>
                </td>
            </tr>
            <tr >
                <td>
                    <asp:TextBox Width="820px" TextMode="multiLine" CssClass="InputTextReadOnly" ReadOnly="true" Rows="30" ID="BulletinContentCtl"
                        runat="server" Height="550px">
                    </asp:TextBox>                
                </td>
            </tr>
            <tr>
                <td valign="top" style="text-align: center;">
                    <uc1:UCFlie ID="ViewUCFileUpload" runat="server" Width="320px" IsView="true" />
                </td>
            </tr>                    
        </table>
        </div>
        
    </form>
</body>
</html>
