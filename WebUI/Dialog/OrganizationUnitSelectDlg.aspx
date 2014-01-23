<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="Dialog_OrganizationUnitSelectDlg" MasterPageFile="~/DialogMasterPage.master" Codebehind="OrganizationUnitSelectDlg.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphTop" runat="Server">
<b>组织结构选择</b>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript" language="javascript">
        function init() {
            window.resizeTo(800,600);
        }
        
        function dlgCancel() {
            window.returnValue = null;
            window.close();
        }
        function dlgOk() {
            var OUCodeCtl = document.getElementById("ctl00_cphMain_OUCodeCtl");
            var OUNameCtl = document.getElementById("ctl00_cphMain_OUNameCtl");
            var OUIdCtl = document.getElementById("ctl00_cphMain_OUIdCtl");
            if (OUIdCtl.value == null || OUIdCtl.value.length == 0) {
                alert("请先选择组织机构");
                return;
            } else {
                var returnValue = new Object();
                returnValue.ouCode = OUCodeCtl.value;
                returnValue.ouName = OUNameCtl.value;
                returnValue.ouId = OUIdCtl.value;
                window.returnValue = returnValue;
                window.close();
            }
       }
    </script>
        <div style="overflow: auto; height: 560px;">
            <asp:TreeView ID="OUTree" runat="server"  CssClass="InputText" ExpandDepth="2" OnSelectedNodeChanged="OUTree_SelectedNodeChanged">
                <SelectedNodeStyle BackColor="#000040" ForeColor="White" />
            <NodeStyle Font-Names="Verdana" Font-Size="12px" ForeColor="Black" HorizontalPadding="3px"
                NodeSpacing="0px" VerticalPadding="0px" />
                
            </asp:TreeView>
            <input type="hidden" id="OUIdCtl" runat="server" />
            <input type="hidden" id="OUCodeCtl" runat="server" />
            <input type="hidden" id="OUNameCtl" runat="server" />
        </div>

        <div style="text-align: center;">
            <input id="OkBtn" class="button_nor" type="button" onclick="dlgOk();" value="确定" />
            <input id="CancelBtn" class="button_nor" type="button" onclick="dlgCancel();" value="取消" />
        </div>

</asp:Content>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <base target="_self" />
    <title>组织机构选择</title>
    <link href="../Css/style.css" rel="stylesheet" type="text/css" />



</head>
<body>
    
</body>
</html>--%>
