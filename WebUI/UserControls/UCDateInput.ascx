<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_UCDateInput" Codebehind="UCDateInput.ascx.cs" %>
<style type="text/css">
    .defaultCss
    {
       width:100px
    }
</style>
    
<asp:TextBox ID="txtDate" runat="server" CssClass="defaultCss"></asp:TextBox>
<asp:ImageButton
    ID="ibtDate" runat="server" ImageUrl="~/images/Calendar.gif" BorderColor="Silver"
    BorderStyle="None" BorderWidth="0px" ImageAlign="AbsMiddle" Height="19px" Width="19px" TabIndex="-1" AccessKey="?" />
<!--<span style="color:Red">(∏Ò Ω:yyyy-mm-dd)</span>-->