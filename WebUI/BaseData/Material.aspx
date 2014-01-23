<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Material" Codebehind="Material.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        搜索条件</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 200px;" class="field_title">
                    物资名称
                </td>
                <td style="width: 200px;" class="field_title">
                    物资编号
                </td>
                <td style="width: 400px;" class="field_title">
                    物资价格
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtNameBySearch"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtMaterialNoBySearch"></asp:TextBox>
                </td>
                <td style="vertical-align: top">
                    <asp:TextBox runat="server" ID="txtPriceBegin" CssClass="InputNumber"></asp:TextBox>~~
                    <asp:TextBox runat="server" ID="txtPriceEnd" CssClass="InputNumber"></asp:TextBox>
                </td>
                <td style="width: 400px; vertical-align: middle;" align="center" rowspan="2">
                    &nbsp;
                </td>
                <td style="width: 200px; vertical-align: middle;" align="center" rowspan="2">
                    <asp:Button runat="server" ID="lbtnSearch" Text="搜 索" CssClass="button_nor" OnClick="lbtnSearch_Click">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1260px;">
        物资信息</div>
    <asp:UpdatePanel ID="upMaterial" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvMaterial" CssClass="GridView" runat="server" DataSourceID="odsMaterial"
                CellPadding="0" AutoGenerateColumns="False" DataKeyNames="MaterialID" AllowPaging="True"
                AllowSorting="True" PageSize="20" EnableModelValidation="True">
                <Columns>
                    <asp:TemplateField SortExpression="MaterialNo" HeaderText="物资编号">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMaterialNo" runat="server" Text='<%# Bind("MaterialNo") %>' Width="130px"
                                CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialNo" runat="server" Text='<%# Bind("MaterialNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="MaterialName" HeaderText="物资名称">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMaterialName" runat="server" Text='<%# Bind("MaterialName") %>'
                                Width="280px" CssClass="InputText" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialName" runat="server" Text='<%# Bind("MaterialName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="300px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="MaterialPrice" HeaderText="物资价格">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMaterialPrice" runat="server" Text='<%# Bind("MaterialPrice") %>'
                                Width="80px" CssClass="InputNumber" MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialPrice" runat="server" CssClass="NumberLabel" Text='<%# Bind("MaterialPrice") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="UOM" HeaderText="单位类型">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUOM" runat="server" Text='<%# Bind("UOM") %>' Width="80px" CssClass="InputText"
                                MaxLength="20"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="MinimumNumber" HeaderText="最小领用数量">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMinimumNumber" runat="server" Text='<%# Bind("MinimumNumber") %>'
                                Width="80px" CssClass="InputNumber" MaxLength="15"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMinimumNumber" runat="server" CssClass="NumberLabel" Text='<%# Bind("MinimumNumber") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Description" HeaderText="规格说明">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'
                                Width="380px" CssClass="InputText" MaxLength="200"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="400px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="IsActive" HeaderText="激活">
                        <EditItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit" Checked='<%# Bind("IsActive") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="chkActiveByEdit1" Enabled="false" Checked='<%# Bind("IsActive") %>' />
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RFMaterialName" runat="server" ControlToValidate="txtMaterialName"
                                Display="None" ErrorMessage="请您输入物资名称！" SetFocusOnError="True" ValidationGroup="MaterialEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFMaterialPrice" runat="server" ControlToValidate="txtMaterialPrice"
                                Display="None" ErrorMessage="请您输入物资价格！" SetFocusOnError="True" ValidationGroup="MaterialEdit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF1" runat="server" ControlToValidate="txtMaterialPrice"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="物资价格必须为数字" ValidationGroup="MaterialEdit"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFUOM" runat="server" ControlToValidate="txtUOM"
                                Display="None" ErrorMessage="请您输入物资单位类型！" SetFocusOnError="True" ValidationGroup="MaterialEdit"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RFMinimumNumber" runat="server" ControlToValidate="txtMinimumNumber"
                                Display="None" ErrorMessage="请您输入物资最小领用数量！" SetFocusOnError="True" ValidationGroup="MaterialEdit"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtMinimumNumber"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="最小领用数量必须为数字" ValidationGroup="MaterialEdit"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RFDescription" runat="server" ControlToValidate="txtDescription"
                                Display="None" ErrorMessage="请您输入物资描述！" SetFocusOnError="True" ValidationGroup="MaterialEdit"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="vsMaterialEdit" runat="server" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="MaterialEdit" />
                            <asp:LinkButton ID="LinkButton1" Visible="<%# HasManageRight %>" runat="server" CausesValidation="True"
                                ValidationGroup="MaterialEdit" CommandName="Update" Text="更新"></asp:LinkButton>
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
                        <tr class="Header">
                            <td style="width: 150px;" class="Empty1">
                                物资编号
                            </td>
                            <td style="width: 300px;">
                                物资名称
                            </td>
                            <td style="width: 100px;">
                                物资价格
                            </td>
                            <td style="width: 100px;">
                                单位类型
                            </td>
                            <td style="width: 100px;">
                                最小领用数量
                            </td>
                            <td style="width: 400px;">
                                描述
                            </td>
                            <td style="width: 50px;">
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
            <asp:FormView ID="fvMaterial" runat="server" DataKeyNames="MaterialID" DataSourceID="odsMaterial"
                DefaultMode="Insert" Visible="<%# HasManageRight %>" EnableModelValidation="True"
                CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td align="center" style="width: 150px;">
                                <asp:TextBox ID="txtMaterialNoByAdd" runat="server" Text='<%# Bind("MaterialNo") %>'
                                    Width="130px" CssClass="InputText" ValidationGroup="MaterialINS" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 300px;">
                                <asp:TextBox ID="txtMaterialNameByAdd" runat="server" Text='<%# Bind("MaterialName") %>'
                                    Width="280px" CssClass="InputText" ValidationGroup="MaterialINS" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtMaterialPriceByAdd" runat="server" Text='<%# Bind("MaterialPrice") %>'
                                    Width="80px" CssClass="InputNumber" ValidationGroup="MaterialINS" MaxLength="15"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtUOMByAdd" runat="server" Text='<%# Bind("UOM") %>' Width="80px"
                                    CssClass="InputText" ValidationGroup="MaterialINS" MaxLength="20"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 100px;">
                                <asp:TextBox ID="txtMinimumNumberByAdd" runat="server" Text='<%# Bind("MinimumNumber") %>'
                                    Width="80px" CssClass="InputNumber" ValidationGroup="MaterialINS" MaxLength="15"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 400px;">
                                <asp:TextBox ID="txtDescriptionByAdd" runat="server" Text='<%# Bind("Description") %>'
                                    Width="380px" CssClass="InputText" ValidationGroup="MaterialINS" MaxLength="200"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 50px;">
                                <asp:CheckBox runat="server" ID="chkActiveByAdd" Checked='<%# Bind("IsActive") %>' />
                            </td>
                            <td align="center" style="width: 60px;">
                                <asp:RequiredFieldValidator ID="RFMaterialName" runat="server" ControlToValidate="txtMaterialNameByAdd"
                                    Display="None" ErrorMessage="请您输入物资名称！" SetFocusOnError="True" ValidationGroup="MaterialAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFMaterialPrice" runat="server" ControlToValidate="txtMaterialPriceByAdd"
                                    Display="None" ErrorMessage="请您输入物资价格！" SetFocusOnError="True" ValidationGroup="MaterialAdd"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="txtMaterialPriceByAdd"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                    ErrorMessage="物资价格必须为数字" ValidationGroup="MaterialAdd"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RFUOM" runat="server" ControlToValidate="txtUOMByAdd"
                                    Display="None" ErrorMessage="请您输入物资单位类型！" SetFocusOnError="True" ValidationGroup="MaterialAdd"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RFMinimumNumber" runat="server" ControlToValidate="txtMinimumNumberByAdd"
                                    Display="None" ErrorMessage="请您输入物资最小领用数量！" SetFocusOnError="True" ValidationGroup="MaterialAdd"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMinimumNumberByAdd"
                                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                    ErrorMessage="最小领用数量必须为数字" ValidationGroup="MaterialAdd"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RFDescription" runat="server" ControlToValidate="txtDescriptionByAdd"
                                    Display="None" ErrorMessage="请您输入物资描述！" SetFocusOnError="True" ValidationGroup="MaterialAdd"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="vsMaterialAdd" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="MaterialAdd" />
                                <asp:LinkButton ID="InsertLinkButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="新增" ValidationGroup="MaterialAdd"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="MaterialInsertValidationSummary" ValidationGroup="MaterialINS"
                        ShowMessageBox="true" ShowSummary="false" EnableClientScript="true" runat="server" />
                </InsertItemTemplate>
            </asp:FormView>
            <asp:ObjectDataSource ID="odsMaterial" runat="server" TypeName="BusinessObjects.MasterDataBLL"
                InsertMethod="InsertMaterial" SelectMethod="GetMaterialPaged" SelectCountMethod="MaterialTotalCount"
                SortParameterName="sortExpression" UpdateMethod="UpdateMaterial" EnablePaging="true">
                <UpdateParameters>
                    <asp:Parameter Name="MaterialNo" Type="String" />
                    <asp:Parameter Name="MaterialName" Type="String" />
                    <asp:Parameter Name="MaterialPrice" Type="Decimal" />
                    <asp:Parameter Name="UOM" Type="String" />
                    <asp:Parameter Name="MinimumNumber" Type="Int32" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="MaterialNo" Type="String" />
                    <asp:Parameter Name="MaterialName" Type="String" />
                    <asp:Parameter Name="MaterialPrice" Type="Decimal" />
                    <asp:Parameter Name="UOM" Type="String" />
                    <asp:Parameter Name="MinimumNumber" Type="Int32" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="queryExpression" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
