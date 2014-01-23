<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_ucUpdateProgress" Codebehind="ucUpdateProgress.ascx.cs" %>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200">
    <ProgressTemplate>
        <div style="background: #FFFFFF; filter: alpha(opacity=10); opacity: 0.1; position: fixed;
            top: 0px; left: 0px; width: 100%; height: 3000px;">
        </div>
        <div id="divProcessing" usercontrol_updateprogressname="usercontrol_UpdateProgressName"
            runat="server" style="position: fixed; top: 0px; left: 0px; width: 100%; height: 100%;">
            <table style="height: 136px; width: 150px; z-index: 1002;" align="center" cellpadding="0"
                cellspacing="0" class="tdborder03">
                <tr>
                    <td align="center" style=" background-color:#ffffff;">
                        <asp:Image ID="imgProcess" runat="server" ImageUrl="~/images/gears_an.gif" AlternateText="Processing" />
                    </td>
                </tr>
                <tr>
                    <td align="center" class="smallblue2" style="font-weight: bold;background-color:#ffffff;">
                        处理中请稍候....
                    </td>
                </tr>
            </table>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
