<%@ Page Language="C#" AutoEventWireup="true" Inherits="Bulletin" Codebehind="Bulletin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告栏</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #D9D9D9; margin-bottom: 0px;">
    <div style="width: 815px; background-color: White; margin-left: auto; margin-right: auto;">
        <form id="form1" runat="server">
            <gc:GridView ID="BulletinGridView" CssClass="GridView" Width="815px" runat="server" AllowPaging="True" AllowSorting="True"
                AutoGenerateColumns="False" DataKeyNames="BulletinId" DataSourceID="BulletinDS">
                <Columns>                    
                    <asp:TemplateField HeaderText="标题" SortExpression="BulletinTitle">
                        
                        <ItemStyle Width="600px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="TitleLink" CommandName="Select" runat="server" Text='<%# Bind("BulletinTitle") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发布时间">
                        <ItemTemplate>
                            <asp:Label ID="CreateTimeLabel" runat="server" Text='<%# Eval("CreateTime","{0:yyyy-MM-dd}") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    
                    <asp:CheckBoxField DataField="IsHot" HeaderText="IsHot" SortExpression="IsHot" >
                        <ItemStyle Width="50px" />
                    </asp:CheckBoxField>
                    <asp:BoundField DataField="BulletinId" Visible="False" HeaderText="BulletinId" InsertVisible="False"
                        ReadOnly="True" SortExpression="BulletinId" />
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table class="GridView" width="150px">
                        <tr class="Header">
                            <td style="width:500px;">标题</td>
                            <td style="width:150px;">发布时间</td>
                            <td style="width:50px;">IsHot</td>                            
                        </tr>
                        <tr>
                        <td colspan="4" style="text-align:center;">无</td></tr>
                    </table>
                </EmptyDataTemplate>
          
            </gc:GridView>
            <asp:ObjectDataSource ID="BulletinDS" runat="server" 
                SelectMethod="GetPageInActive" TypeName="BusinessObjects.BulletinBLL" EnablePaging="True" SelectCountMethod="TotalCount" SortParameterName="sortExpression">
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />                    
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:FormView Width="815px" ID="BulletinFormView" runat="server" DataKeyNames="BulletinId" DataSourceID="BulletinDetailDS">
                <ItemTemplate>
                        <div style="text-align: center; font-weight:bold; font-size:large">
                            <asp:Label ID="TitleLabel" runat="server" Text='<%# Eval("BulletinTitle") %>'></asp:Label>
                        </div>
                        <div style="text-align:center;">
                            <asp:Label ID="PublishDateLabel" runat="server" Text='<%# Eval("CreateTime") %>'></asp:Label>
                        </div>
                        <div>
                            <pre><asp:Literal ID="ContentCtl" runat="server" Text='<%# Eval("BulletinContent") %>'></asp:Literal></pre>
                            
                        </div>
                    
                </ItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="BulletinDetailDS" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetBulletinById" TypeName="BusinessObjects.BulletinBLL">
                <SelectParameters>
                    <asp:ControlParameter ControlID="BulletinGridView" Name="BulletinId" PropertyName="SelectedValue"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>           
        </form>
    </div>
</body>
</html>
