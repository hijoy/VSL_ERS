<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_AttachmentFile" Codebehind="AttachmentFile.ascx.cs" %>
<asp:FileUpload ID="fileUpload" runat="server"/><asp:HyperLink Runat="server" ID="lkDownload"></asp:HyperLink>
<asp:Button Runat="server" ID="btnDelete" Text="É¾³ý" OnClick="btnDelete_Click"></asp:Button>
<asp:Button Runat="server" ID="btnCancel" Text="È¡Ïû" OnClick="btnCancel_Click"></asp:Button>
<asp:HiddenField ID="FUNameCtl" runat="server" />
<asp:HiddenField ID="FUSizeCtl" runat="server" />
<asp:HiddenField ID="OldFUNameCtl" runat="server" />
<asp:HiddenField ID="OldFUSizeCtl" runat="server" />
