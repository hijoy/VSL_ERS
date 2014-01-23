<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="OtherForm_TravelReimburseApproval" Title="出差费用报销审批" CodeBehind="TravelReimburseApproval.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="DateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/APFlowNodes.ascx" TagName="APFlowNodes" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/YearAndMonthUserControl.ascx" TagName="YearAndMonthUserControl"
    TagPrefix="uc2" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function MinusTotal(lblName, obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvTravelReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName)) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvTravelReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText);
                    break;
                }
            }
            if (obj.value != "" && isNaN(obj.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            //计算数据
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) - parseFloat(obj.value);
                if (document.all("ctl00_ContentPlaceHolder1_gvTravelReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName)) {
                    document.all("ctl00_ContentPlaceHolder1_gvTravelReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText = commafy(lastTotal.toFixed(2));
                }
            }
        }

        function PlusTotal(lblName, obj) {
            var totalFee;
            for (j = 3; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvTravelReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName)) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvTravelReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText);
                    break;
                }
            }
            if (obj.value != "" && isNaN(obj.value)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById(obj.id).focus(); }, 0);
                return false;
            }
            //计算数据
            var lastTotal = 0;
            if (obj.value != "" && !isNaN(obj.value)) {
                lastTotal = parseFloat(totalFee) + parseFloat(obj.value);

                document.all("ctl00_ContentPlaceHolder1_gvTravelReimburseDetails_ctl" + GetTBitNum(j) + "_" + lblName).innerText = commafy(lastTotal.toFixed(2));
            }
        }

        function commafy(num) {
            num = num + "";
            var re = /(-?\d+)(\d{3})/
            while (re.test(num)) {
                num = num.replace(re, "$1,$2")
            }
            return num;
        }
        function uncommafy(str) {
            str = str + "";
            var re = /\,/g;
            str = str.replace(re, '')
            return str
        }
        function GetTBitNum(num) {
            if (num < 10) {
                num = "0" + String(num);
            }
            return num;
        }
    </script>
    <div class="title" style="width: 1258px">
        基本信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        单据编号</div>
                    <div>
                        <asp:TextBox ID="txtFormNo" runat="server" ReadOnly="true" Width="170px" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        申请日期</div>
                    <div>
                        <asp:TextBox ID="ApplyDateCtl" runat="server" ReadOnly="true" Width="170px" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        费用期间</div>
                    <div>
                        <asp:TextBox ID="txtPeriod" runat="server" ReadOnly="true" Width="170px" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        员工</div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        职位</div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        部门</div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        入职日期</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblTransportFee" runat="server" Text="交通费用预算" /></div>
                    <div>
                        <asp:TextBox ID="txtTransportFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblHotelFee" runat="server" Text="住宿费用预算" /></div>
                    <div>
                        <asp:TextBox ID="txtHotelFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblMealFee" runat="server" Text="餐费预算" /></div>
                    <div>
                        <asp:TextBox ID="txtMealFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblOtherFee" runat="server" Text="其他费用预算" /></div>
                    <div>
                        <asp:TextBox ID="txtOtherFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblTotal" runat="server" Text="费用预算总计" /></div>
                    <div>
                        <asp:TextBox ID="txtTotal" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div class="field_title">
                        出差申请</div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="hlTravelApply" runat="server"></asp:HyperLink></div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        历史单据</div>
                    <div style="margin-top: 5px">
                        <asp:HyperLink ID="lblRejectFormNo" runat="server"></asp:HyperLink></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        报销申请理由</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" ReadOnly="true" TextMode="multiline"
                            Height="60px" Columns="75"></asp:TextBox></div>
                </td>
                <td colspan="2" valign="top">
                    <div class="field_title">
                        附件
                    </div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" IsView="true" Width="400px" />
                </td>
                <td valign="top" align="center">
                    <asp:Image ID="imgOverBudget" runat="server" ImageUrl="~/Images/OverBudget.gif" Visible="false" />
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
                DataKeyNames="FormTravelApplyDetailID" OnRowDataBound="gvFormTravelApplyDetails_RowDataBound"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="开始日期">
                        <ItemTemplate>
                            <asp:Label ID="lblBeginDate" runat="server" Text='<%# Eval("BeginDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="开始日期">
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
    <br />
    <div id="BudgetTitleDIV" runat="server" class="title" style="width: 1258px">
        预算信息</div>
    <div id="BudgetInfoDIV" runat="server" style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        预算金额</div>
                    <div>
                        <asp:TextBox ID="txtTotalBudget" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        报销中金额</div>
                    <div>
                        <asp:TextBox ID="txtApprovingAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        已报销金额</div>
                    <div>
                        <asp:TextBox ID="txtApprovedAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        可用余额</div>
                    <div>
                        <asp:TextBox ID="txtRemainAmount" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        费用详细信息</div>
    <asp:UpdatePanel ID="upTravelReimburseDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvTravelReimburseDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormPersonalReimburseDetailID" OnRowDataBound="gvTravelReimburseDetails_RowDataBound"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="发生地点">
                        <ItemTemplate>
                            <asp:Label ID="lblPlace" runat="server" Text='<%# Eval("Place") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="发生日期">
                        <ItemTemplate>
                            <asp:Label ID="lblOccurDate" runat="server" Text='<%# Eval("OccurDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="160px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用项类型">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseManageType" runat="server" Text='<%# GetExpenseManageTypeNameById(Eval("ExpenseManageTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="报销金额">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实报金额">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRealAmount" MaxLength="15" runat="server" Width="130px" Text='<%# Eval("RealAmount") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="150px" />
                        <FooterStyle HorizontalAlign="Right" CssClass="RedTextAlignCenter" />
                        <FooterTemplate>
                            <asp:Label ID="totalRealAmountLbl" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="502px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 200px;" class="Empty1">
                                发生地点
                            </td>
                            <td style="width: 160px;">
                                发生日期
                            </td>
                            <td style="width: 250px;">
                                费用项类型
                            </td>
                            <td style="width: 150px;">
                                报销金额
                            </td>
                            <td style="width: 502px;">
                                备注
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc1:APFlowNodes ID="cwfAppCheck" runat="server" />
    <br />
    <asp:UpdatePanel ID="upButton" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" OnClick="SubmitBtn_Click" Text="审批" CssClass="button_nor" />&nbsp;
                <asp:Button ID="CancelBtn" runat="server" OnClick="CancelBtn_Click" Text="返回" CssClass="button_nor" />&nbsp;
                <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="编辑" CssClass="button_nor" />&nbsp;
                <asp:Button ID="ScrapBtn" runat="server" OnClick="ScrapBtn_Click" Text="作废" CssClass="button_nor" />
                <asp:Button ID="SaveBtn" runat="server" OnClick="SaveBtn_Click" Text="保存实报金额" CssClass="button_nor" />&nbsp;
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upCustomer" />
</asp:Content>
