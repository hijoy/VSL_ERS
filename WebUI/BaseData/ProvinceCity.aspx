<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="ProvinceCity" Codebehind="ProvinceCity.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        省份维护
    </div>
    <asp:UpdatePanel ID="upProvince" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvProvince" CssClass="GridView" runat="server" DataSourceID="odsProvince"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="ProvinceID" AllowPaging="True"
                AllowSorting="True" PageSize="20" OnSelectedIndexChanged="gvProvince_SelectedIndexChanged"
                OnRowDeleted="gvProvince_RowDeleted" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="ProvinceName" HeaderText="省份名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtProvinceName" runat="server" Text='<%# Bind("ProvinceName") %>'
                                Width="1120px" CssClass="InputText" MaxLength="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnProvinceName" runat="server" CausesValidation="False" CommandName="Select"
                                Text='<%# Bind("ProvinceName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="1147px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="激活">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFProvName" runat="server" ControlToValidate="txtProvinceName"
                                Display="None" ErrorMessage="请您输入省份名称！" SetFocusOnError="True" ValidationGroup="provEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsProvEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="provEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="provEdit" CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="false"
                                CommandName="Edit" Text="编辑"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header" style="height: 22px;">
                            <td align="center" style="width: 1147px;" class="Empty1">
                                省份名称
                            </td>
                            <td align="center" style="width: 60px;">
                                激活
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <SelectedRowStyle CssClass="SelectedRow" />
            </gc:GridView>
            <asp:FormView ID="fvProvince" runat="server" DataKeyNames="ProvinceID" DataSourceID="odsProvince"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="height: 22px; width: 1147px;">
                                <asp:TextBox ID="txtProvinceNameByAdd" runat="server" Text='<%# Bind("ProvinceName") %>'
                                    Width="1120px" CssClass="InputText" ValidationGroup="ProvINS" MaxLength="50"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFProvName" runat="server" ControlToValidate="txtProvinceNameByAdd"
                                    Display="None" ErrorMessage="请您输入省份名称！" SetFocusOnError="True" ValidationGroup="provAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsProvAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="provAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="provAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ProvInsertValidationSummary" ValidationGroup="ProvINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsProvince" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteProvince" InsertMethod="InsertProvince" SelectMethod="GetProvince"
                UpdateMethod="UpdateProvince" OnDeleting="odsProvince_Deleting" OnInserting="odsProvince_Inserting"
                OnUpdating="odsProvince_Updating" OnInserted="odsProvinc_Inserted" OnUpdated="odsProvinc_Updated">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ProvinceName" Type="String" Size="50" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="ProvinceName" Type="String" Size="50" />
                </InsertParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="title" style="width: 1260px;">
        城市维护
    </div>
    <asp:UpdatePanel ID="upCity" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView Visible="false" ID="gvCity" CssClass="GridView" runat="server" DataSourceID="odsCity"
                AutoGenerateColumns="False" CellPadding="0" AllowSorting="True" AllowPaging="True"
                PageSize="20" DataKeyNames="CityId">
                <HeaderStyle CssClass="Header" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td align="center" style="width: 1147px;" class="Empty1">
                                城市名称
                            </td>
                            <td align="center" style="width: 60px;">
                                激活
                            </td>
                            <td style="width: 60px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="城市名称" SortExpression="CityName">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCityNameByEdit" runat="server" Text='<%# Bind("CityName") %>'
                                CssClass="InputText" Width="1120px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblCityNameByEdit" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="1147px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="激活">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFCityName" runat="server" ControlToValidate="txtCityNameByEdit"
                                Display="None" ErrorMessage="请您输入城市名称！" SetFocusOnError="True" ValidationGroup="cityEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsCityEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="cityEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" ValidationGroup="cityEdit"
                                CausesValidation="True" CommandName="Update" Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Cancel" Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="False"
                                CommandName="Edit" Text="编辑"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
            </gc:GridView>
            <asp:FormView ID="fvCity" Visible="false" runat="server" DataKeyNames="CityId" DataSourceID="odsCity"
                DefaultMode="Insert" OnItemInserting="fvCity_ItemInserting" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 1147px;">
                                <asp:TextBox ID="txtCityNameByAdd" runat="server" Text='<%# Bind("CityName") %>'
                                    CssClass="InputText" ValidationGroup="CityInsert" Width="1120px"></asp:TextBox>
                            </td>
                            <td align="center" style="height: 22px; width: 60px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFCityName" runat="server" ControlToValidate="txtCityNameByAdd"
                                    Display="None" ErrorMessage="请您输入城市名称！" SetFocusOnError="True" ValidationGroup="cityAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsCityAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="cityAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="cityAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="CityInsertValidationSummary" ValidationGroup="CityInsert"
                        runat="server" ShowMessageBox="true" ShowSummary="false" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsCity" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                DeleteMethod="DeleteCity" InsertMethod="InsertCity" SelectMethod="GetCityPaged" OnInserted="odsCity_Inserted" OnUpdated="odsCity_Updated"
                EnablePaging="true" SelectCountMethod="TotalCount" UpdateMethod="UpdateCity"
                SortParameterName="sortExpression" OnDeleting="odsCity_Deleting" OnSelecting="odsCity_Selecting"
                OnInserting="odsCity_Inserting" OnUpdating="odsCity_Updating">
                <DeleteParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="CityName" Type="String" Size="50" />
                    <asp:Parameter Name="ProvId" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="stuffUser" Type="object" />
                    <asp:Parameter Name="position" Type="object" />
                    <asp:Parameter Name="CityName" Type="String" Size="50" />
                    <asp:Parameter Name="ProvId" Type="Int32" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="ProvId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
