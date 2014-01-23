<%@ Page Language="C#" AutoEventWireup="true" Inherits="PositionSelect" Codebehind="PositionSelect.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>职务选择</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:850px; margin-left:auto; margin-right:auto; margin-top:100px;">
        <br />
        <div>职务选择：</div>
        <gc:GridView ID="PositionGridView" CssClass="GridView" Width="850px" runat="server" AutoGenerateColumns="False" 
            DataSourceID="StuffUserPositionDS" DataKeyNames="PositionId" OnSelectedIndexChanged="PositionGridView_SelectedIndexChanged"
            OnRowDataBound="StuffUserPositionGridView_RowDataBound">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                            Text="以此职务进入系统"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PositionId" SortExpression="PositionId" Visible="False">                    
                    <ItemTemplate>
                        <asp:Label ID="PositionIdCtl" runat="server" Text='<%# Bind("PositionId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="隶属机构">
                    <ItemStyle Width="600px" />
                    <ItemTemplate>
                        <asp:Label ID="ParentOUNamesCtl" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="职务名称" SortExpression="PositionName">
                    <ItemStyle Width="150px" />
                    <ItemTemplate>
                        <asp:Label ID="PositionNameCtl" Text='<%# Bind("PositionName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="Header" />
        </gc:GridView>
        <asp:ObjectDataSource ID="StuffUserPositionDS" runat="server" SelectMethod="GetPositionByStuffUser"
            TypeName="BusinessObjects.AuthorizationBLL">
            <SelectParameters>
                <asp:Parameter Name="StuffUserId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
