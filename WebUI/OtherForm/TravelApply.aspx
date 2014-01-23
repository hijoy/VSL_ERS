<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="OtherForm_TravelApply" CodeBehind="TravelApply.aspx.cs" %>

<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonth"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/MultiAttachmentFile.ascx" TagName="ucFileUpload"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="DateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SetTotal(transportFeeID, hotelFeeID, mealFeeID, otherFeeID, totalFeeID) {
            var totalFee = 0;
            txtTransportFee = document.getElementById(transportFeeID);
            txtHotelFee = document.getElementById(hotelFeeID);
            txtMealFee = document.getElementById(mealFeeID);
            txtOtherFee = document.getElementById(otherFeeID);
            txtTotalFee = document.getElementById(totalFeeID);
            if (txtTransportFee.value && txtTransportFee.value != "" && !isNaN(txtTransportFee.value)) {
                totalFee += parseFloat(txtTransportFee.value);
            }
            if (txtHotelFee.value && txtHotelFee.value != "" && !isNaN(txtHotelFee.value)) {
                totalFee += parseFloat(txtHotelFee.value);
            }
            if (txtMealFee.value && txtMealFee.value != "" && !isNaN(txtMealFee.value)) {
                totalFee += parseFloat(txtMealFee.value);
            }
            if (txtOtherFee.value && txtOtherFee.value != "" && !isNaN(txtOtherFee.value)) {
                totalFee += parseFloat(txtOtherFee.value);
            }
            txtTotalFee.value = commafy(totalFee.toFixed(2));
        }

        function commafy(num) {
            num = num + "";
            var re = /(-?\d+)(\d{3})/
            while (re.test(num)) {
                num = num.replace(re, "$1,$2")
            }
            return num;
        }
    </script>
    <div class="title" style="width: 1258px">
        基本信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
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
                        <asp:TextBox ID="txtTransportFee" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblHotelFee" runat="server" Text="住宿费用" /></div>
                    <div>
                        <asp:TextBox ID="txtHotelFee" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblMealFee" runat="server" Text="餐费" /></div>
                    <div>
                        <asp:TextBox ID="txtMealFee" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblOtherFee" runat="server" Text="其他费用" /></div>
                    <div>
                        <asp:TextBox ID="txtOtherFee" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="Label1" runat="server" Text="费用合计" /></div>
                    <div>
                        <asp:TextBox ID="txtTotalAmount" ReadOnly="true" runat="server" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3" valign="top">
                    <div class="field_title">
                        <asp:Label ID="lblRemark" runat="server" Text="备注" /></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="80"></asp:TextBox></div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        附件
                    </div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                </td>
            </tr>
            <tr>
                <asp:RegularExpressionValidator ID="REVTransportFee" runat="server" ControlToValidate="txtTransportFee"
                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                    ErrorMessage="交通费用请输入数字" ValidationGroup="add"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="REVHotelFee" runat="server" ControlToValidate="txtHotelFee"
                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                    ErrorMessage="住宿费用请输入数字" ValidationGroup="add"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="REVMealFee" runat="server" ControlToValidate="txtMealFee"
                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                    ErrorMessage="餐费请输入数字" ValidationGroup="add"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="REVOtherFee" runat="server" ControlToValidate="txtOtherFee"
                    ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                    ErrorMessage="其他费用请输入数字" ValidationGroup="add"></asp:RegularExpressionValidator>
                <asp:ValidationSummary ID="ValidationSummaryAdd" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="add" />
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        <asp:Label ID="lblTitle" runat="server" Text="出差申请详细" /></div>
    <asp:UpdatePanel ID="upFormTravelApplyDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvFormTravelApplyDetails" runat="server" CssClass="GridView" AutoGenerateColumns="False"
                DataKeyNames="FormTravelApplyDetailID" DataSourceID="odsFormTravelApplyDetails"
                OnRowDataBound="gvFormTravelApplyDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblBeginDateHeader" runat="server" Text="出发日期"></asp:Label><span class="requiredLable">*</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBeginDate" runat="server" Text='<%# Eval("BeginDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uc1:DateInput ID="UCBeginDate" runat="server" IsReadOnly="false" SelectedDate='<%# Bind("BeginDate") %>'
                                IsExpensePeriod="true" />
                        </EditItemTemplate>
                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblEndDateHeader" runat="server" Text="返回日期"></asp:Label><span class="requiredLable">*</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uc1:DateInput ID="UCEndDate" runat="server" IsReadOnly="false" SelectedDate='<%# Bind("EndDate") %>'
                                IsExpensePeriod="true" />
                        </EditItemTemplate>
                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="天数">
                        <ItemTemplate>
                            <asp:Label ID="lblDays" runat="server" Text='<%# Eval("Days") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblDepartureHeader" runat="server" Text="出发地城市"></asp:Label><span
                                class="requiredLable">*</span>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDeparture" runat="server" Text='<%# Eval("Departure") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDeparture" runat="server" Text='<%# Bind("Departure") %>' Width="160px"
                                MaxLength="30"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblDestinationHeader" runat="server" Text="目的地城市"></asp:Label><span
                                class="requiredLable">*</span></HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDestination" runat="server" Text='<%# Eval("Destination") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDestination" runat="server" Text='<%# Bind("Destination") %>'
                                Width="160px" MaxLength="30"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="180px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblVehicleHeader" runat="server" Text="交通工具"></asp:Label><span class="requiredLable">*</span></HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblVehicle" runat="server" Text='<%# Eval("Vehicle") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlVehicle" runat="server" SelectedValue='<%# Bind("Vehicle") %>'
                                Width="130px">
                                <asp:ListItem Selected="True" Value="" Text="请选择"></asp:ListItem>
                                <asp:ListItem Text="汽车"></asp:ListItem>
                                <asp:ListItem Text="火车"></asp:ListItem>
                                <asp:ListItem Text="飞机"></asp:ListItem>
                                <asp:ListItem Text="出租车"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemStyle Width="130px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="出差原因">
                        <ItemTemplate>
                            <asp:Label ID="lblDetailRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' Width="250px"
                                MaxLength="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="241" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbnEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                Text='编辑'></asp:LinkButton>
                            <asp:LinkButton ID="lblDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                OnClientClick="return window.confirm('确定要删除么')" Text='删除'></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="UCBeginDate$txtDate"
                                ErrorMessage="请选择出发日期！" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="UCEndDate$txtDate"
                                ErrorMessage="请选择返回日期！" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF3" runat="server" ControlToValidate="txtDeparture"
                                ErrorMessage="请填写出发地！" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF4" runat="server" ControlToValidate="txtDestination"
                                ErrorMessage="请填写目的地！" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF5" runat="server" ControlToValidate="ddlVehicle"
                                ErrorMessage="请填写交通工具！" Display="None" ValidationGroup="NewDetailRow" InitialValue=""></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="true" CommandName="Update"
                                ValidationGroup="NewDetailRow" Text='更新'></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text='取消'></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="70" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 180px;" class="Empty1">
                                出发时间<span class="requiredLable">*</span>
                            </td>
                            <td style="width: 180px;">
                                返回时间<span class="requiredLable">*</span>
                            </td>
                            <td style="width: 100px;">
                                天数
                            </td>
                            <td style="width: 180px;">
                                出发地城市<span class="requiredLable">*</span>
                            </td>
                            <td style="width: 180px;">
                                目的地城市<span class="requiredLable">*</span>
                            </td>
                            <td style="width: 130px;">
                                交通工具<span class="requiredLable">*</span>
                            </td>
                            <td style="width: 241px;">
                                出差原因
                            </td>
                            <td style="width: 70px;">
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
            <asp:FormView ID="fvFormTravelApplyDetails" runat="server" DataKeyNames="FormTravelApplyDetailID"
                DataSourceID="odsFormTravelApplyDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 180px;" align="center">
                                <uc1:DateInput ID="UCBeginDate" runat="server" IsReadOnly="false" IsExpensePeriod="true"
                                    SelectedDate='<%# Bind("BeginDate") %>' />
                            </td>
                            <td style="width: 180px;" align="center">
                                <uc1:DateInput ID="UCEndDate" runat="server" IsReadOnly="false" IsExpensePeriod="true"
                                    SelectedDate='<%# Bind("EndDate") %>' />
                            </td>
                            <td style="width: 100px;" align="center">
                            </td>
                            <td style="width: 180px;" align="center">
                                <asp:TextBox ID="txtDeparture" runat="server" MaxLength="30" Width="160px" Text='<%# Bind("Departure") %>'></asp:TextBox>
                            </td>
                            <td style="width: 180px;" align="center">
                                <asp:TextBox ID="txtDestination" runat="server" MaxLength="30" Width="160px" Text='<%# Bind("Destination") %>'></asp:TextBox>
                            </td>
                            <td style="width: 130px;" align="center">
                                <asp:DropDownList ID="ddlVehicle" runat="server" SelectedValue='<%# Bind("Vehicle") %>'
                                    Width="130px">
                                    <asp:ListItem Selected="True" Text="请选择" Value=""></asp:ListItem>
                                    <asp:ListItem Text="汽车"></asp:ListItem>
                                    <asp:ListItem Text="火车"></asp:ListItem>
                                    <asp:ListItem Text="飞机"></asp:ListItem>
                                    <asp:ListItem Text="出租车"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width: 241px;" align="center">
                                <asp:TextBox ID="txtRemark" runat="server" Width="220px" Text='<%# Bind("Remark")   %>'></asp:TextBox>
                            </td>
                            <td style="width: 70px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text='新增' ValidationGroup="DetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="UCBeginDate$txtDate"
                                ErrorMessage="请选择出发日期！" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="UCBeginDate$txtDate"
                                ErrorMessage="请选择返回日期！" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF3" runat="server" ControlToValidate="txtDeparture"
                                ErrorMessage="请填写出发地！" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF4" runat="server" ControlToValidate="txtDestination"
                                ErrorMessage="请填写目的地！" Display="None" ValidationGroup="DetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlVehicle"
                                ErrorMessage="请填写交通工具！" Display="None" ValidationGroup="DetailRow" InitialValue=""></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="DetailRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsFormTravelApplyDetails" runat="server" DeleteMethod="DeleteFormTravelApplyDetailByID"
        SelectMethod="GetFormTravelApplyDetailByFormTravelApplyID" UpdateMethod="UpdateFormTravelApplyDetail"
        TypeName="BusinessObjects.PersonalReimburseBLL" InsertMethod="AddFormTravelApplyDetail"
        OnObjectCreated="odsFormTravelApplyDetails_ObjectCreated">
        <SelectParameters>
            <asp:Parameter Name="FormTravelApplyID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" ValidationGroup="add"
                    OnClick="SubmitBtn_Click" Text="提交" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" ValidationGroup="add"
                    OnClick="SaveBtn_Click" Text="保存" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="返回" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    OnClientClick="return window.confirm('确定要删除么')" Text="删除" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vassociatedupdatepanelid="upCustomer" />
</asp:Content>
