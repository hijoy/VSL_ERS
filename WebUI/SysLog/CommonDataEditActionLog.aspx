<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="SysLog_CommonDataEditActionLog"
    Title="基础数据操作记录查询" Codebehind="CommonDataEditActionLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="SearchUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="background-color: #EEEEEE; border-style: solid; border-width: 1px; width: 815px;">
                <table style="width: 815px;">
                    <tr>
                        <td style="width: 25%;">
                            数据表</td>
                        <td style="width: 25%;">
                            操作类型</td>
                        <td style="width: 25%;">
                            员工工号</td>
                        <td style="width: 25%;">
                            员工姓名</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DataTableNameDropDownList" runat="server"  Width="150px">
                                <asp:ListItem>全部</asp:ListItem>
                                <asp:ListItem>币种</asp:ListItem>
                                <asp:ListItem>汇率</asp:ListItem>
                                <asp:ListItem>交际原因</asp:ListItem>
                                <asp:ListItem>餐费标准</asp:ListItem>
                                <asp:ListItem>差旅费食宿标准</asp:ListItem>
                                <asp:ListItem>单据编号规则</asp:ListItem>
                                <asp:ListItem>审批拒绝原因</asp:ListItem>
                                <asp:ListItem>淡季系数</asp:ListItem>
                                <asp:ListItem>旺季系数</asp:ListItem>
                                <asp:ListItem>促销活动费参数</asp:ListItem>
                                <asp:ListItem>合同费用参数</asp:ListItem>
                                <asp:ListItem>支付方式参数</asp:ListItem>                            
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="ActionTypeDropDownList" runat="server"  Width="150px">
                                <asp:ListItem>全部</asp:ListItem>
                                <asp:ListItem>添加</asp:ListItem>
                                <asp:ListItem>更新</asp:ListItem>
                                <asp:ListItem>删除</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:TextBox ID="StuffIdCtl" runat="server"  Width="150px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="StuffNameCtl" runat="server"  Width="150px"></asp:TextBox></td>
                    </tr>                    
                    <tr>
                        <td>
                            时间范围:</td>
                        <td style="width:150px">
                            从 <asp:TextBox ID="SrhAfterLogInTimeCtl" runat="server" Width="120px"></asp:TextBox>
                        </td>
                        <td style="width:160px">
                            到 <asp:TextBox ID="SrhBeforeLogInTimeCtl" runat="server" Width="130px"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="SearchBtn" CssClass="button_nor" runat="server" Text="查询" OnClick="SearchBtn_Click" /></td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="GridViewUpdatePanel" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="CommonDataEditActionLogGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="CommonDataEditActionLogId"
                DataSourceID="CommonDataEditActionLogDS" CssClass="GridView" Width="815px" AllowPaging="True" AllowSorting="True">
                <Columns>
                    <asp:BoundField ItemStyle-Width="80px" DataField="StuffId" HeaderText="员工工号" SortExpression="StuffId" />
                    <asp:BoundField ItemStyle-Width="100px" DataField="StuffName" HeaderText="员工姓名" SortExpression="StuffName" />
                    <asp:BoundField ItemStyle-Width="80px" DataField="DataTableName" HeaderText="数据表" SortExpression="DataTableName" />
                    <asp:BoundField ItemStyle-Width="80px" DataField="ActionTime" HeaderText="操作时间" DataFormatString="{0:yyyy-MM-dd}" SortExpression="ActionTime" />
                    <asp:BoundField ItemStyle-Width="200px" DataField="NewValue" HeaderText="新数据" SortExpression="NewValue" />
                    <asp:BoundField ItemStyle-Width="195px" DataField="OldValue" HeaderText="旧数据" SortExpression="OldValue" />
                    <asp:BoundField ItemStyle-Width="80px" DataField="ActionType" HeaderText="操作类型" SortExpression="ActionType" />
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table class="GridView" width="815px">
                        <tr>
                            <td>
                                员工工号</td>
                            <td>
                                员工姓名</td>
                            <td>
                                数据表</td>
                            <td>
                                操作时间</td>
                            <td>
                                新数据</td>
                            <td>
                                旧数据</td>
                            <td>
                                操作类型</td>
                        </tr>
                        <tr>
                            <td colspan="7" style="text-align:center;">无</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="CommonDataEditActionLogDS" runat="server" EnablePaging="True"
        SelectCountMethod="CommonDataEditActionLogTotalCount"
        SelectMethod="GetCommonDataEditActionLog" SortParameterName="sortExpression"
        TypeName="BusinessObjects.LogBLL">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />            
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
