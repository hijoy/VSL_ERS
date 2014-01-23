<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="AuthorizationManage_FlowParticipantManage" Codebehind="FlowParticipantManage.aspx.cs" %>

<%@ Register Src="../UserControls/StaffControl.ascx" TagName="StaffControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" style="width: 1260px;">
        流程管理</div>
    <asp:UpdatePanel ID="upCustomerType" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvFlowConfigure" CssClass="GridView" runat="server" DataSourceID="odsFlowConfigure"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="FlowID" AllowPaging="false"
                OnRowDataBound="gvFlowConfigure_RowDataBound" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="FlowName" HeaderText="流程名称">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblFlowName" runat="server" Text='<%# Bind("FlowName") %>' OnClick="lknSelect"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="302px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FlowID" HeaderText="流程参与者">
                        <ItemTemplate>
                            <asp:Label ID="lblFlowParticipant" runat="server" Text='<%# Bind("FlowID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="302px" HorizontalAlign="Center" Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FlowTemplateName" HeaderText="流程模板名称">
                        <ItemTemplate>
                            <asp:Label ID="lblFlowTemplateName" runat="server" Text='<%# Bind("FlowTemplateName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="302px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="BusinessUseCaseId" HeaderText="业务实例名称">
                        <ItemTemplate>
                            <asp:Label ID="lblBusinessUseCaseName" runat="server" Text='<%# Bind("BusinessUseCaseId") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="302px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 302px;" class="Empty1">
                                流程名称
                            </td>
                            <td style="width: 302px;">
                                流程参与者
                            </td>
                            <td style="width: 302px;">
                                流程模板名称
                            </td>
                            <td style="width: 302px;">
                                业务实例名称
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvFlowConfigure" runat="server" DataKeyNames="FlowID" DataSourceID="odsFlowConfigure"
                DefaultMode="Insert" EnableModelValidation="True" CellPadding="0" OnDataBound="fvFlowConfigure_DataBound">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 302px;">
                                <asp:TextBox ID="txtFlowName" runat="server" Text='<%# Bind("FlowName") %>' Width="200px"
                                    CssClass="InputText" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 302px;">
                                <asp:TextBox ID="txtFlowParticipant" runat="server" Width="200px" CssClass="InputText"
                                    MaxLength="20" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 302px;">
                                <asp:DropDownList runat="server" ID="ddlDefName" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="height: 22px; width: 302px;">
                                <asp:DropDownList runat="server" ID="ddlBusinessUseCase" DataSourceID="odsBusinessUseCase"
                                    DataTextField="BusinessUseCaseName" DataValueField="BusinessUseCaseId" Width="200px"
                                    SelectedValue='<%# Bind("BusinessUseCaseID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsBusinessUseCase" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [BusinessUseCaseId], [BusinessUseCaseName] FROM [BusinessUseCase] where BusinessUseCaseId< '100'">
                                </asp:SqlDataSource>
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CommandName="Insert" Text="新增"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsFlowConfigure" runat="server" TypeName="BusinessObjects.AuthorizationBLL"
                InsertMethod="InsertFlowParticipantConfigure" SelectMethod="GetFlowParticipantConfigure"
                OnInserting="odsFlowConfigure_Inserting" DeleteMethod="DeleteFlowParticipantConfigure"
                EnablePaging="false">
                <DeleteParameters>
                    <asp:Parameter Name="FlowID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="FlowTemplateName" Type="String" />
                    <asp:Parameter Name="FlowName" Type="String" />
                    <asp:Parameter Name="BusinessUseCaseId" Type="String" />
                </InsertParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upFlowParticipantDetail" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="StuffPanel" runat="server" style="display: none">
                <div class="title" style="width: 1260px">
                    流程参与者
                </div>
                <gc:GridView ID="StuffGridView"  CssClass="GridView" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="FlowParticipantConfigureDetailID" DataSourceID="FlowParticipantDetail"
                    CellPadding="0">
                    <HeaderStyle CssClass="Header" />
                    <Columns>
                        <asp:TemplateField SortExpression="StuffName" HeaderText="员工姓名">
                            <ItemTemplate>
                                <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StaffName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="605px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="UserName" HeaderText="用户名">
                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="602px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="删除"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table width="1269px">
                            <tr class="Header">
                                <td style="width: 605px" class="Empty1">
                                    员工姓名
                                </td>
                                <td style="width: 602px">
                                    用户名
                                </td>
                                <td style="width: 60px">
                                    &nbsp
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </gc:GridView>
                <asp:FormView ID="fvStuff" runat="server" DataKeyNames="FlowParticipantConfigureDetailID"
                    DataSourceID="FlowParticipantDetail" DefaultMode="Insert" EnableModelValidation="True"
                    CellPadding="0">
                    <InsertItemTemplate>
                        <table class="FormView">
                            <tr>
                                <td align="center" style="height: 22px; width: 605;">
                                    <uc1:StaffControl ID="StaffControl1" runat="server" Width="200px" AutoPostBack="true" OnStaffNameTextChanged="txtStaffName_TextChanged" />
                                </td>
                                <td align="center" style="height: 22px; width: 602px;">
                                    <asp:TextBox ID="txtFlowParticipant" runat="server" Text='' Width="200px" CssClass="InputText"
                                        MaxLength="20" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="center" style="width: 60px;">
                                    <asp:LinkButton ID="InsertLinkButton1" runat="server" CommandName="Insert" Text="新增"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </InsertItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="FlowParticipantDetail" runat="server" DeleteMethod="DeleteFlowParticipantConfigureDetailByID"
                    SelectMethod="GetFlowParticipantConfigureDetail" TypeName="BusinessObjects.AuthorizationBLL"
                    OnInserting="FlowParticipantDetail_Inserting" OnObjectCreated="FlowParticipantDetail_ObjectCreated"
                    InsertMethod="AddFlowParticipantConfigureDetail" >
                    <DeleteParameters>
                        <asp:Parameter Name="FlowParticipantConfigureDetailID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="FlowID" Type="Int32" />
                        <asp:Parameter Name="UserName" Type="Int32" />
                        <asp:Parameter Name="UserID" Type="Int32" />
                        <asp:Parameter Name="StaffName" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <div style="margin-top: 20px; width: 1200px; text-align: right">
                    <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                        Text="保存" />
                    <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                        Text="取消" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
