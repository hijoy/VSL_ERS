<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="OtherForm_PersonalReimburseApply"
    Title="个人费用报销申请" Codebehind="PersonalReimburseApply.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="DateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ParameterChanged() {
            var txtQuantity = document.all("ctl00$ContentPlaceHolder1$fvPersonalReimburseDetails$newQuantityCtl").value;
            var txtPrice = document.all("ctl00$ContentPlaceHolder1$fvPersonalReimburseDetails$newPersonalReimbursePriceCtl").value;
            if (txtQuantity != "" && isNaN(txtQuantity)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvPersonalReimburseDetails$newQuantityCtl").focus(); }, 0);
                return false;
            }
            if (txtPrice != "" && isNaN(txtPrice)) {
                alert("请录入数字");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvPersonalReimburseDetails$newPersonalReimbursePriceCtl").focus(); }, 0);
                return false;
            }
            if (!isNaN(parseFloat(txtQuantity)) && !isNaN(parseFloat(txtPrice))) {
                var quantity = parseFloat(txtQuantity);
                var price = parseFloat(txtPrice);
                var amount = quantity * price;
                document.all("ctl00$ContentPlaceHolder1$fvPersonalReimburseDetails$newTotalCtl").value = commafy(amount.toFixed(2));
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
    <div class="title" style="width: 1258px">
        基本信息</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        费用期间<span class="requiredLable">*</span></div>
                    <div>
                        <asp:DropDownList ID="PeriodDDL" runat="server" DataSourceID="odsPeriod" 
                            DataTextField="ReimbursePeriod" DataValueField="ReimbursePeriodID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPeriod" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 ReimbursePeriodID,' 请选择' ReimbursePeriod Union SELECT [ReimbursePeriodID], convert(varchar(50),year(ReimbursePeriod))+'-'+Right(100+month(ReimbursePeriod),2) ReimbursePeriod FROM [ReimbursePeriod] ">
                        </asp:SqlDataSource>
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
                        &nbsp;</div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        申请理由</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="75"></asp:TextBox></div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        附件
                    </div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                </td>

            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        费用详细信息</div>
    <asp:UpdatePanel ID="upPersonalReimburseDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvPersonalReimburseDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormPersonalReimburseDetailID" DataSourceID="odsPersonalReimburseDetails"
                OnRowDataBound="gvPersonalReimburseDetails_RowDataBound" CellPadding="0" >
                <Columns>
                    <asp:TemplateField HeaderText="发生日期">
                        <ItemTemplate>
                            <asp:Label ID="lblOccurDate" runat="server" Text='<%# Eval("OccurDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uc1:DateInput ID="UCOccurDate" runat="server" IsReadOnly="false" SelectedDate='<%# Bind("OccurDate") %>'
                                IsExpensePeriod="true" />
                        </EditItemTemplate>
                        <ItemStyle Width="160px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="费用项类型">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseManageType" runat="server" Text='<%# GetExpenseManageTypeNameById(Eval("ExpenseManageTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplExpenseManageType" DataTextField="ExpenseManageTypeName"
                                DataValueField="ExpenseManageTypeID" Width="300px" DataSourceID="odsExpenseManageType"
                                SelectedValue='<%# Bind("ExpenseManageTypeID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsExpenseManageType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select ExpenseManageTypeID,ExpenseManageTypeName from dbo.ExpenseManageType where IsActive=1 and ExpenseManageCategoryID=1  order by ExpenseManageTypeName">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="350px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="报销金额">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="120px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" MaxLength="300" Text='<%# Bind("Remark") %>' Width="440px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="542px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="编辑"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Update"
                                Text="更新"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="取消"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 160px;" class="Empty1">
                                发生日期
                            </td>
                            <td style="width: 350px;">
                                费用项类型
                            </td>
                            <td style="width: 150px;">
                                报销金额
                            </td>
                            <td style="width: 542px;">
                                备注
                            </td>
                            <td style="width: 60px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
            <asp:FormView ID="fvPersonalReimburseDetails" runat="server" DataKeyNames="FormPersonalReimburseDetailID"
                DataSourceID="odsPersonalReimburseDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 160px;" align="center">
                                <uc1:DateInput ID="UCOccurDate" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                            </td>
                            <td style="width: 350px;" align="center">
                                <asp:DropDownList runat="server" ID="dplExpenseManageType" DataTextField="ExpenseManageTypeName"
                                    DataValueField="ExpenseManageTypeID" Width="300px" DataSourceID="odsExpenseManageType">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsExpenseManageType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select ExpenseManageTypeID,ExpenseManageTypeName from dbo.ExpenseManageType where IsActive=1 and ExpenseManageCategoryID=1 order by ExpenseManageTypeName">
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 150px;" align="center">
                                <asp:TextBox ID="txtAmount" MaxLength="15" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td style="width: 542px;" align="center">
                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="300" Width="440px"></asp:TextBox>
                            </td>
                            <td style="width: 60px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="添加" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="UCOccurDate$txtDate"
                                ErrorMessage="请选择发生日期！" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="txtAmount"
                                ErrorMessage="请填写报销金额！" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF3" runat="server" ControlToValidate="dplExpenseManageType"
                                ErrorMessage="请选择费用类型！" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmount"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="请输入数字" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsPersonalReimburseDetails" runat="server" DeleteMethod="DeleteFormPersonalReimburseDetailByID"
        SelectMethod="GetFormPersonalReimburseDetail" UpdateMethod="UpdateFormPersonalReimburseDetail" TypeName="BusinessObjects.PersonalReimburseBLL"
        InsertMethod="AddFormPersonalReimburseDetail" OnInserting="odsPersonalReimburseDetails_Inserting"
        OnObjectCreated="odsPersonalReimburseDetails_ObjectCreated" OnInserted="odsPersonalReimburseDetails_Inserted"
        OnUpdating="odsPersonalReimburseDetails_Updating" OnUpdated="odsPersonalReimburseDetails_Updated">
        <DeleteParameters>
            <asp:Parameter Name="FormPersonalReimburseDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="User" Type="Object" />
            <asp:Parameter Name="FormPersonalReimburseID" Type="Int32" />
            <asp:Parameter Name="OccurDate" Type="Datetime" />
            <asp:Parameter Name="ExpenseManageTypeID" Type="Int32" />
            <asp:Parameter Name="Amount" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="User" Type="Object" />
            <asp:Parameter Name="FormPersonalReimburseID" Type="Int32" />
            <asp:Parameter Name="OccurDate" Type="Datetime" />
            <asp:Parameter Name="ExpenseManageTypeID" Type="Int32" />
            <asp:Parameter Name="Amount" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
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
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upCustomer" />
</asp:Content>
