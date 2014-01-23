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
                    ϵͳ����</div>
                <gc:GridView ID="BulletinGridView" CssClass="GridView" runat="server" AllowPaging="True"
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="BulletinId" DataSourceID="BulletinDS"
                    OnRowDataBound="BulletinGridView_RowDataBound" PageSize="10">
                    <Columns>
                        <asp:TemplateField HeaderText="����" SortExpression="BulletinTitle">
                            <ItemTemplate>
                                <asp:HyperLink ID="TitleLink" runat="server" Text='<%# Bind("BulletinTitle") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="400px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����ʱ��" SortExpression="CreateTime">
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
                                    ����
                                </td>
                                <td style="width: 150px;">
                                    ����ʱ��
                                </td>
                                <td style="width: 47px;">
                                    IsHot
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;" class="Empty2 noneLabel">
                                    ��
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
                    �ҵĲݸ�</div>
                <gc:GridView ID="gvMyDraft" runat="server" CssClass="GridView" DataSourceID="odsMyDraft"
                    AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
                    PageSize="10" OnRowDataBound="gvMyDraft_RowDataBound" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:TemplateField SortExpression="FormTypeName" HeaderText="��������">
                            <ItemTemplate>
                                &nbsp;<asp:LinkButton ID="lblFormType" runat="server" CausesValidation="False" CommandName="Select"
                                    ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblFormApplyName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="200px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="LastModified" HeaderText="¼��ʱ��">
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
                                    ��������
                                </td>
                                <td style="width: 200px;">
                                    ����
                                </td>
                                <td style="width: 248px;">
                                    ¼��ʱ��
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="Empty2 noneLabel">
                                    ��
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
                    �ȴ��������ĵ���</div>
                <gc:GridView CssClass="GridView" ID="gvMyAwaiting" runat="server" DataSourceID="odsMyAwaiting"
                    AutoGenerateColumns="False" DataKeyNames="FormId" AllowPaging="True" AllowSorting="True"
                    PageSize="10" OnRowDataBound="gvMyAwaiting_RowDataBound" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblFormId" runat="server" Text='<%# Bind("FormId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormTypeName" HeaderText="��������">
                            <ItemTemplate>
                                <asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormNo" HeaderText="���ݱ��">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                                    Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblFormApplyName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="StuffName" HeaderText="������">
                            <ItemTemplate>
                                <asp:Label ID="lblStuffuserId" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="SubmitDate" HeaderText="����ʱ��">
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
                                    ��������
                                </td>
                                <td style="width: 120px;">
                                    ���ݱ��
                                </td>
                                <td style="width: 120px;">
                                    ����
                                </td>
                                <td style="width: 120px;">
                                    ������
                                </td>
                                <td style="width: 116px;">
                                    ����ʱ��
                                </td>
                            </tr>
                            <tr class="Empty2">
                                <td colspan="5" class="Empty2 noneLabel">
                                    ��
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
                    ���ύ�ĵ���</div>
                <gc:GridView ID="gvMySubmitted" CssClass="GridView" runat="server" DataSourceID="odsMySubmitted"
                    AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
                    PageSize="10" OnRowDataBound="gvMySubmitted_RowDataBound" CellPadding="0" CellSpacing="0">
                    <Columns>
                        <asp:TemplateField SortExpression="FormTypeName" HeaderText="��������">
                            <ItemTemplate>
                                <asp:Label ID="lblFormType" runat="server" Text='<%# Bind("FormTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="FormNo" HeaderText="���ݱ��">
                            <ItemTemplate>
                                <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                                    Text='<%# Bind("FormNo") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="����">
                            <ItemTemplate>
                                <asp:Label ID="lblFormApplyName" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="115px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="StatusID" HeaderText="����״̬">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="���ʱ��" SortExpression="ApprovedDate">
                            <ItemTemplate>
                                <asp:Label ID="lblApproveDate" runat="server" Text='<%# Bind("ApprovedDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="SubmitDate" HeaderText="����ʱ��">
                            <ItemTemplate>
                                <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd HH:mm}") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="InTurnUserIds" HeaderText="��ǰ������">
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
                                    ��������
                                </td>
                                <td style="width: 120px;">
                                    ���ݱ��
                                </td>
                                <td style="width: 120px;">
                                    ����
                                </td>
                                <td style="width: 120px;">
                                    ����״̬
                                </td>
                                <td style="width: 116px;">
                                    ���ʱ��
                                </td>
                                <td style="width: 116px;">
                                    ����ʱ��
                                </td>
                                <td style="width: 120px;">
                                    ��ǰ������
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" class="Empty2 noneLabel">
                                    ��
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
