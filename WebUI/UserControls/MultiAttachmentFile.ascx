<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="UserControls_MultiAttachmentFile" Codebehind="MultiAttachmentFile.ascx.cs" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td style="display: inline;" valign="top">
            <asp:FileUpload ID="fileUpload" runat="server" BackColor="#e5effb" />
            <asp:Button ID="btAddFile" runat="server" Text="上传文件" OnClientClick="upload()" CssClass="button_upload"
                OnClick="btAddFile_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <gc:GridView ID="gvMultiAttachmentFile" runat="server" AutoGenerateColumns="False"
                CssClass="GridView" AllowPaging="false" ShowFooter="false" Width="100%" DataKeyNames="AttachmentFileName"
                OnRowDataBound="gvMultiAttachmentFile_RowDataBound" BorderWidth="0px" OnRowDeleting="gvMultiAttachmentFile_RowDeleting"
                ShowHeader="false">
                <Columns>
                    <asp:TemplateField HeaderText="RealName" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRealFileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RealAttachmentFileName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle Width="90%" BorderWidth="0" />
                        <ItemTemplate>
                            <a href='<%# DataBinder.Eval(Container.DataItem,"DownloadUrl")%>'>
                                <%# DataBinder.Eval(Container.DataItem, "RealAttachmentFileName")%>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemStyle HorizontalAlign="Center" BorderWidth="0" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnDelete" runat="server" CausesValidation="false" CommandName="Delete"
                                Height="20px" ImageUrl="~/Images/pic2.png" OnClientClick="return confirm('确定删除此行文件吗？');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </td>
    </tr>
    <asp:HiddenField ID="FUNameCtl" runat="server" />
    <asp:HiddenField ID="NewFUNameCtl" runat="server" />
    <asp:HiddenField ID="DeleteFUNameCtl" runat="server" />
    <asp:HiddenField ID="OldFUNameCtl" runat="server" />
    <asp:HiddenField ID="NewUploadFUNameCtl" runat="server" />
    <asp:HiddenField ID="RealFUNameCtl" runat="server" />
    <asp:HiddenField ID="NewRealFUNameCtl" runat="server" />
    <asp:HiddenField ID="DeleteRealFUNameCtl" runat="server" />
    <asp:HiddenField ID="OldRealFUNameCtl" runat="server" />
    <asp:HiddenField ID="NewUploadRealFUNameCtl" runat="server" />
</table>
<div id="divBg11" style="background: #FFFFFF; filter: alpha(opacity=10); opacity: 0.1;
    position: absolute; top: 0px; left: 0px; width: 100%; height: 3000px; display: none;">
</div>
<div id="divProcessing11" usercontrol_updateprogressname="usercontrol_UpdateProgressName"
    style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%; display: none;">
    <table style="height: 136px; width: 150px; z-index: 1002;" align="center" cellpadding="0"
        cellspacing="0" class="tdborder03">
        <tr>
            <td align="center" style="background-color: #ffffff;">
                <asp:Image ID="imgProcess" runat="server" ImageUrl="~/images/gears_an.gif" AlternateText="Processing" />
            </td>
        </tr>
        <tr>
            <td align="center" class="smallblue2" style="font-weight: bold; background-color: #ffffff;">
                文件上传中，请稍候....
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    function upload() {
        document.getElementById('divProcessing11').style.display = 'block';
        document.getElementById('divBg11').style.display = 'block';
    }
    function setDivPosition(divName) {
        //div
        var div = document.getElementById(divName);
        //窗口对象
        var doc = document;
        //如果是在iframe里面，则要取得父窗口的scroll和窗口高、宽。
        if (parent) {
            doc = parent.document;
        }
        div.style.top = (doc.documentElement.scrollTop + (doc.documentElement.clientHeight - parseInt(div.style.height)) / 2) - 120 + "px"; ;
        div.style.left = (doc.documentElement.scrollLeft + (doc.documentElement.clientWidth - parseInt(div.style.width)) / 2) - 300 * 2 + "px";
    }

    function setDivPosition_All() {
        var items = document.getElementsByTagName("Div");
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            var tname = item.getAttribute("usercontrol_UpdateProgressName");
            if (tname == "usercontrol_UpdateProgressName") {
                //alert(item.getAttribute("Id"));
                setDivPosition(item.getAttribute("Id"));
            }
        }
    }
    setDivPosition("divProcessing11");
</script>
<script language="javascript" type="text/javascript" for="window" event="onresize">
    setDivPosition_All();
</script>
<script language="javascript" type="text/javascript" for="window" event="onscroll">
    setDivPosition_All();
</script>
<script language="javascript" type="text/javascript" for="document" event="onmouseover">
    setDivPosition_All();
</script>
