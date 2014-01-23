<%@ Page Language="C#" AutoEventWireup="true" Inherits="BusinessProxySelect" CodeBehind="BusinessProxySelect.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>身份选择</title>
    <link href="css/newstyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 450px; margin-left: auto; margin-right: auto; margin-top: 150px;">
        <asp:SqlDataSource ID="BusinessProxyDS" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand="SELECT ProxyBusiness.UserID, ProxyBusiness.PositionId, ProxyBusiness.ProxyUserId, StuffUser.StuffName,Position.PositionName FROM   ProxyBusiness INNER JOIN Position ON ProxyBusiness.PositionId = Position.PositionId INNER JOIN StuffUser ON ProxyBusiness.UserID = StuffUser.StuffUserId  WHERE (ProxyBusiness.ProxyUserId = @ProxyUserId and ProxyBusiness.BeginDate<= getdate() and ProxyBusiness.EndDate >= getdate() )">
            <SelectParameters>
                <asp:Parameter Name="ProxyUserId" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <div class="title3">
            <asp:LinkButton ID="SelfOperateBtn" runat="server" OnClick="SelfOperateBtn_Click">自己业务操作</asp:LinkButton>
        </div>
        <gc:GridView ID="BusinessProxyGridView" CssClass="GridView" Width="460px" runat="server"
            AutoGenerateColumns="False" DataSourceID="BusinessProxyDS" OnSelectedIndexChanged="BusinessProxyGridView_SelectedIndexChanged">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select"
                            Text="代理填单"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="StuffUserIdCtl" runat="server" Text='<%# Bind("UserID") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PositionId" SortExpression="PositionId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="PositionIdCtl" runat="server" Text='<%# Bind("PositionId") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="ProxyStuffUserIdCtl" runat="server" Text='<%# Bind("ProxyUserId") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Width="100px" />
                </asp:TemplateField>
                <asp:BoundField DataField="StuffName" HeaderText="姓名" SortExpression="StuffName" />
                <asp:BoundField DataField="PositionName" HeaderText="职位" ItemStyle-Width="250px" SortExpression="PositionName" />
            </Columns>
            <HeaderStyle CssClass="Header" />
        </gc:GridView>
    </div>
    </form>
</body>
</html>
