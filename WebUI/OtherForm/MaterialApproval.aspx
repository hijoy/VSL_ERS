<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="OtherForm_MaterialApproval" Title="广宣物资审批" Codebehind="MaterialApproval.aspx.cs" %>

<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1258px">
        基本信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        单据编号</div>
                    <div>
                        <asp:TextBox ID="FormNoCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        申请日期</div>
                    <div>
                        <asp:TextBox ID="ApplyDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        员工</div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        职位</div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        部门</div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        入职日期</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        客户名称</div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        门店名称</div>
                    <div>
                        <asp:TextBox ID="ShopNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        第一个月销量</div>
                    <div>
                        <asp:TextBox ID="FirstVolumeCtl" MaxLength="15" runat="server" ReadOnly="true" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        第二个月销量</div>
                    <div>
                        <asp:TextBox ID="SecondVolumeCtl" MaxLength="15" runat="server" ReadOnly="true" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        第三个月销量</div>
                    <div>
                        <asp:TextBox ID="ThirdVolumeCtl" MaxLength="15" runat="server" ReadOnly="true" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        前三个月平均销量</div>
                    <div>
                        <asp:TextBox ID="AverageVolumeCtl" MaxLength="15" runat="server" ReadOnly="true" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <div class="field_title">
                        申请理由</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" Width="800px" ReadOnly="true" TextMode="multiline"
                            Height="60px"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        明细信息</div>
    <asp:UpdatePanel ID="upMaterialDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvMaterialDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormMaterialDetailID" DataSourceID="odsMaterialDetails"
                OnRowDataBound="gvMaterialDetails_RowDataBound" CellPadding="0" CellSpacing="0">
                <Columns>
                    <asp:TemplateField HeaderText="物品名称">
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialName" runat="server" Text='<%# Eval("MaterialName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="250px"  />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位">
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px"  />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="规格描述">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="400px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单价">
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialPrice" runat="server" Text='<%# Eval("MaterialPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px"  />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="数量">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="小计">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="261px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 250px;" align="center" class="Empty1">
                                物资名称
                            </td>
                            <td style="width: 50px;" align="center">
                                单位
                            </td>
                            <td style="width: 400px;" align="center">
                                规格描述
                            </td>
                            <td style="width: 100px;" align="center">
                                单价
                            </td>
                            <td style="width: 100px;" align="center">
                                数量
                            </td>
                            <td style="width: 100px;" align="center">
                                小计
                            </td>
                            <td style="width: 200px;" align="center">
                                备注
                            </td>
                            <td style="width: 50px" align="center">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsMaterialDetails" runat="server" SelectMethod="GetFormMaterialDetailByFormMaterialID"
        TypeName="BusinessObjects.MaterialApplyBLL" EnablePaging="false">
        <SelectParameters>
            <asp:Parameter Name="FormMaterialID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="审批" CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="返回" CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="编辑" CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="作废" CssClass="button_nor" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc4:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upButton" />
    <br />
</asp:Content>
