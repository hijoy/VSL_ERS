<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="Home" CodeBehind="Home.aspx.cs" %>

<%@ OutputCache Duration="1" NoStore="true" Location="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <table width="1270px" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 20px;">
                &nbsp;
            </td>
            <td width="604px" valign="top">
                <div class="title2" style="width: 590px;">
                    系统公告</div>
                <gc:GridView ID="BulletinGridView" CssClass="GridView" runat="server" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="BulletinId" DataSourceID="BulletinDS"
                    OnRowDataBound="BulletinGridView_RowDataBound" PageSize="10">
                    <Columns>
                        <asp:TemplateField HeaderText="标题" SortExpression="BulletinTitle">
                            <ItemTemplate>
                                <asp:HyperLink ID="TitleLink" runat="server" Text='<%# Bind("BulletinTitle") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="400px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="发布时间" SortExpression="CreateTime">
                            <ItemTemplate>
                                <asp:Label ID="CreateTimeLabel" runat="server" Text='<%# Eval("CreateTime","{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="IsHot" HeaderText="IsHot" SortExpression="IsHot">
                            <ItemStyle Width="47px" HorizontalAlign="Center" />
                        </asp:CheckBoxField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr>
                                <td style="width: 400px;" class="Empty1">
                                    标题
                                </td>
                                <td style="width: 150px;">
                                    发布时间
                                </td>
                                <td style="width: 47px;">
                                    IsHot
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;" class="Empty2 noneLabel">
                                    无
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </gc:GridView>
                <asp:ObjectDataSource ID="BulletinDS" runat="server" SelectMethod="GetPageInActive"
                    TypeName="BusinessObjects.MasterDataBLL" EnablePaging="True" SelectCountMethod="TotalCount"
                    SortParameterName="sortExpression">
                    <SelectParameters>
                        <asp:Parameter Name="queryExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="BulletinDetailDS" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetBulletinById" TypeName="BusinessObjects.MasterDataBLL">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="BulletinGridView" Name="BulletinId" PropertyName="SelectedValue"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
                <div class="title2" style="width: 590px;">
                    我的草稿</div>
                <gc:GridView ID="gvMyDraft" runat="server" CssClass="GridView" DataSourceID="odsMyDraft"
                    AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
                    PageSize="10" OnRowDataBound="gvMyDraft_RowDataBound" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:TemplateField SortExpression="FormTypeName" HeaderText="单据类型">
                            <ItemTemplate>
                                &nbsp;<asp:LinkButton ID="lblFormType" runat="server" CausesValidation="False" CommandName="Select"
                                    ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:Label ID="lblFormApplyName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="LastModified" HeaderText="录入时间">
                            <ItemTemplate>
                                <asp:Label ID="lblCreateDate" runat="server" Text='<%# Bind("LastModified", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="248px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr class="Header">
                                <td style="width: 150px;" class="Empty1">
                                    单据类型
                                </td>
                                <td style="width: 200px;">
                                    名称
                                </td>
                                <td style="width: 248px;">
                                    录入时间
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Empty2 noneLabel">
                                    无
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <SelectedRowStyle CssClass="SelectedRow" />
                </gc:GridView>
                <asp:ObjectDataSource ID="odsMyDraft" runat="server" TypeName="BusinessObjects.FormQueryBLL"
                    SelectMethod="GetPagedFormView" EnablePaging="True" SelectCountMethod="QueryFormViewCount"
                    SortParameterName="sortExpression">
                    <SelectParameters>
                        <asp:Parameter Name="queryExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
            <td width="603px" valign="top">
                <div class="title2" style="width: 590px;">
                    等待我审批的单据</div>
                <gc:GridView CssClass="GridView" ID="gvMyAwaiting" runat="server" DataSourceID="odsMyAwaiting"
                    AutoGenerateColumns="False" DataKeyNames="FormId" AllowPaging="True" AllowSorting="True"
                    PageSize="10" OnRowDataBound="gvMyAwaiting_RowDataBound" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblFormId" runat="server" Text='<%# Bind("FormId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormTypeName" HeaderText="单据类型">
                            <ItemTemplate>
                                <asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormNo" HeaderText="单据编号">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                                    Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:Label ID="lblFormApplyName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="StuffName" HeaderText="申请人">
                            <ItemTemplate>
                                <asp:Label ID="lblStuffuserId" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="SubmitDate" HeaderText="申请时间">
                            <ItemTemplate>
                                <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="116px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr class="Header">
                                <td style="width: 120px;" class="Empty1">
                                    单据类型
                                </td>
                                <td style="width: 120px;">
                                    单据编号
                                </td>
                                <td style="width: 120px;">
                                    名称
                                </td>
                                <td style="width: 120px;">
                                    申请人
                                </td>
                                <td style="width: 116px;">
                                    申请时间
                                </td>
                            </tr>
                            <tr class="Empty2">
                                <td colspan="5" class="Empty2 noneLabel">
                                    无
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <SelectedRowStyle CssClass="SelectedRow" />
                </gc:GridView>
                <asp:ObjectDataSource ID="odsMyAwaiting" runat="server" TypeName="BusinessObjects.FormQueryBLL"
                    SelectMethod="GetPagedFormViewForAwaiting" EnablePaging="True" SelectCountMethod="QueryFormViewCount"
                    SortParameterName="sortExpression">
                    <SelectParameters>
                        <asp:Parameter Name="queryExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
                <div class="title2" style="width: 590px;">
                    我提交的单据</div>
                <gc:GridView ID="gvMySubmitted" CssClass="GridView" runat="server" DataSourceID="odsMySubmitted"
                    AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
                    PageSize="10" OnRowDataBound="gvMySubmitted_RowDataBound" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:TemplateField SortExpression="FormTypeName" HeaderText="单据类型">
                            <ItemTemplate>
                                <asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormNo" HeaderText="单据编号">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                                    Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:Label ID="lblFormApplyName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="115px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="StatusID" HeaderText="单据状态">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="审核时间" SortExpression="ApprovedDate">
                            <ItemTemplate>
                                <asp:Label ID="lblApproveDate" runat="server" Text='<%# Bind("ApprovedDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="SubmitDate" HeaderText="申请时间">
                            <ItemTemplate>
                                <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="InTurnUserIds" HeaderText="当前审批人">
                            <ItemTemplate>
                                <asp:Label ID="lblCurrentUser" runat="server" Text='<%#GetCurrentUserByInturnID(Eval("InTurnUserIds")) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="98px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="Header" />
                    <EmptyDataTemplate>
                        <table>
                            <tr class="Header">
                                <td style="width: 120px;" class="Empty1">
                                    单据类型
                                </td>
                                <td style="width: 120px;">
                                    单据编号
                                </td>
                                <td style="width: 120px;">
                                    名称
                                </td>
                                <td style="width: 120px;">
                                    单据状态
                                </td>
                                <td style="width: 116px;">
                                    审核时间
                                </td>
                                <td style="width: 116px;">
                                    申请时间
                                </td>
                                <td style="width: 120px;">
                                    当前审批人
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" class="Empty2 noneLabel">
                                    无
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <SelectedRowStyle CssClass="SelectedRow" />
                </gc:GridView>
                <asp:ObjectDataSource ID="odsMySubmitted" runat="server" TypeName="BusinessObjects.FormQueryBLL"
                    SelectMethod="GetPagedFormView" EnablePaging="True" SelectCountMethod="QueryFormViewCount"
                    SortParameterName="sortExpression">
                    <SelectParameters>
                        <asp:Parameter Name="queryExpression" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>
