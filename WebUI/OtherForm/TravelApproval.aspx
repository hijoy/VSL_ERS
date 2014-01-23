<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="OtherForm_TravelApproval" Culture="Auto" UICulture="Auto" CodeBehind="TravelApproval.aspx.cs" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonth"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/MultiAttachmentFile.ascx" TagName="ucFileUpload"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="title" style="width: 1258px">
        基本信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table class="searchTable">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblFormNo" Text="单据编号" runat="server" /></div>
                    <div>
                        <asp:TextBox ID="txtFormNo" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label2" runat="server" Text="申请人" /></div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label3" runat="server" Text="职位" /></div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label4" runat="server" Text="部门" /></div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px" align="left">
                    <div class="field_title">
                        <asp:Label ID="Label6" runat="server" Text="入职日期" /></div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblTransportFee" runat="server" Text="交通费用" /></div>
                    <div>
                        <asp:TextBox ID="txtTransportFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblHotelFee" runat="server" Text="住宿费用" /></div>
                    <div>
                        <asp:TextBox ID="txtHotelFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblMealFee" runat="server" Text="餐费" /></div>
                    <div>
                        <asp:TextBox ID="txtMealFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblOtherFee" runat="server" Text="其他费用" /></div>
                    <div>
                        <asp:TextBox ID="txtOtherFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblTotal" runat="server" Text="费用总计" /></div>
                    <div>
                        <asp:TextBox ID="txtTotal" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        历史单据</div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="Label5" runat="server" Text="备注" /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="77" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        附件
                    </div>
                    
                    <uc1:ucFileUpload ID="UCFileUpload" runat="server" Width="400px" IsView="true" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        出差申请详细</div>
    <asp:UpdatePanel ID="upFormTravelApplyDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvFormTravelApplyDetails" runat="server" CssClass="GridView" AutoGenerateColumns="False"
                DataKeyNames="FormTravelApplyDetailID" DataSourceID="odsFormTravelApplyDetails"
                OnRowDataBound="gvFormTravelApplyDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="出发日期">
                        <ItemTemplate>
                            <asp:Label ID="lblBeginDate" runat="server" Text='<%# Eval("BeginDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="返回日期">
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="出差天数">
                        <ItemTemplate>
                            <asp:Label ID="lblDays" runat="server" Text='<%# Eval("Days") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="出发地城市">
                        <ItemTemplate>
                            <asp:Label ID="lblDeparture" runat="server" Text='<%# Eval("Departure") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="目的地城市">
                        <ItemTemplate>
                            <asp:Label ID="lblDestination" runat="server" Text='<%# Eval("Destination") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="交通工具">
                        <ItemTemplate>
                            <asp:Label ID="lblVehicle" runat="server" Text='<%# Eval("Vehicle") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="出差原因">
                        <ItemTemplate>
                            <asp:Label ID="lblDetailRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="351" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 150px;" class="Empty1">
                                开始时间
                            </td>
                            <td style="width: 150px;">
                                结束时间
                            </td>
                            <td style="width: 60px;">
                                出差天数
                            </td>
                            <td style="width: 200px;">
                                出发地城市
                            </td>
                            <td style="width: 200px;">
                                目的地城市
                            </td>
                            <td style="width: 150px;">
                                交通工具
                            </td>
                            <td style="width: 351px;">
                                出差原因
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsFormTravelApplyDetails" runat="server" SelectMethod="GetFormTravelApplyDetailByFormTravelApplyID"
        TypeName="BusinessObjects.PersonalReimburseBLL" InsertMethod="AddFormTravelApplyDetail"
        OnObjectCreated="odsFormTravelApplyDetails_ObjectCreated">
        <SelectParameters>
            <asp:Parameter Name="FormTravelApplyID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="审批" CssClass="button_nor" />
                <asp:Button ID="CancelBtn" runat="server" Text="返回" CssClass="button_nor" OnClick="CancelBtn_Click" />
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="编辑" CssClass="button_nor" />
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="作废" CssClass="button_nor"
                    OnClientClick="return confirm('确定作废单据吗？');" />
            </div>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="SubmitBtn" />
        </Triggers>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upCustomer" />
</asp:Content>
