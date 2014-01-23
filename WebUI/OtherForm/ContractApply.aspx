<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="OtherForm_ContractApply" Title="合同申请" Codebehind="ContractApply.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ParameterChanged() {
            var txtQuantity = document.all("ctl00$ContentPlaceHolder1$fvMaterialDetails$newQuantityCtl").value;
            var txtPrice = document.all("ctl00$ContentPlaceHolder1$fvMaterialDetails$newMaterialPriceCtl").value;
            if (txtQuantity != "" && isNaN(txtQuantity)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvMaterialDetails$newQuantityCtl").focus(); }, 0);
                return false;
            }
            if (txtPrice != "" && isNaN(txtPrice)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvMaterialDetails$newMaterialPriceCtl").focus(); }, 0);
                return false;
            }
            if (!isNaN(parseFloat(txtQuantity)) && !isNaN(parseFloat(txtPrice))) {
                var quantity = parseFloat(txtQuantity);
                var price = parseFloat(txtPrice);
                var amount = quantity * price;
                document.all("ctl00$ContentPlaceHolder1$fvMaterialDetails$newTotalCtl").value = commafy(amount.toFixed(2));
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
    </script>
    <script language="javascript" type="text/javascript" for="window" event="onfocus">
<!--
return window_onfocus()
// -->
    </script>
    <div class="title" style="width: 1258px">
        申请人信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
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
                <td style="width: 200px">
                    <div class="field_title">
                        入职日期</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        &nbsp;</div>
                    <div>
                        &nbsp;
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        &nbsp;</div>
                    <div>
                        &nbsp;</div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        签约对方信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 400px">
                    <div class="field_title">
                        对方单位1<span class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="txtFirstCompany" runat="server" MaxLength="40" Width="340px"></asp:TextBox></div>
                </td>
                <td style="width: 400px">
                    <div class="field_title">
                        对方单位2</div>
                    <div>
                        <asp:TextBox ID="txtSecondCompany" runat="server" MaxLength="40" Width="340px"></asp:TextBox></div>
                </td>
                <td style="width: 400px">
                    <div class="field_title">
                        对方单位3</div>
                    <div>
                        <asp:TextBox ID="txtThirdCompany" runat="server" MaxLength="40" Width="340px"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        合同信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        合同名称(正式名称)<span class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="txtContractName" runat="server" MaxLength="20" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        合同金额<span class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="txtContractAmount" runat="server" MaxLength="15" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        合同正本页数<span class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="txtPageNumber" runat="server" MaxLength="10" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        合同类型<span class="requiredLable">*</span></div>
                    <div>
                        <asp:DropDownList runat="server" ID="dplContractType" DataSourceID="sdsContractType"
                            DataTextField="ContractTypeName" DataValueField="ContractTypeID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="sdsContractType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand="SELECT 0 as [ContractTypeID],'请选择' as  [ContractTypeName] union select [ContractTypeID], [ContractTypeName] FROM [ContractType] where IsActive=1">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        支付方式</div>
                    <div>
                        <asp:TextBox ID="txtPaymentType" runat="server" MaxLength="20" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        &nbsp</div>
                    <div>
                        &nbsp;</div>
                </td>
            </tr>
            <tr>
                <td style="width: 400px" colspan="6">
                    <div class="field_title">
                        合同期间<span class="requiredLable">*</span></div>
                    <div>
                        <uc1:UCDateInput ID="UCPeriodBegin" runat="server" IsReadOnly="false" IsExpensePeriod="true"
                            OnDateTextChanged="UCPeriodBegin_OnDateTextChanged" />
                        <asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>
                        <uc1:UCDateInput ID="UCPeriodEnd" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="field_title">
                        主要内容<span class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="txtMainContent" runat="server" CssClass="InputText" Width="1000px"
                            TextMode="multiline" Height="60px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="field_title">
                        条款变化</div>
                    <div>
                        <asp:TextBox ID="txtChangePart" runat="server" CssClass="InputText" Width="1000px"
                            TextMode="multiline" Height="60px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div class="field_title">
                        附件<span class="requiredLable">*</span></div>
                    <div>
                        <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:Button ID="SubmitBtn" runat="server" CssClass="button_nor" OnClick="SubmitBtn_Click"
                    Text="提交" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click"
                    Text="保存" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="返回" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text="删除" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server"/>
</asp:Content>
