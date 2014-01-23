<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="OtherForm_TravelReimburseApply" Title="������ñ�������" CodeBehind="TravelReimburseApply.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="DateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ParameterChanged() {
            var txtQuantity = document.all("ctl00$ContentPlaceHolder1$fvTravelReimburseDetails$newQuantityCtl").value;
            var txtPrice = document.all("ctl00$ContentPlaceHolder1$fvTravelReimburseDetails$newTravelReimbursePriceCtl").value;
            if (txtQuantity != "" && isNaN(txtQuantity)) {
                alert("��¼������");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvTravelReimburseDetails$newQuantityCtl").focus(); }, 0);
                return false;
            }
            if (txtPrice != "" && isNaN(txtPrice)) {
                alert("��¼������");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvTravelReimburseDetails$newTravelReimbursePriceCtl").focus(); }, 0);
                return false;
            }
            if (!isNaN(parseFloat(txtQuantity)) && !isNaN(parseFloat(txtPrice))) {
                var quantity = parseFloat(txtQuantity);
                var price = parseFloat(txtPrice);
                var amount = quantity * price;
                document.all("ctl00$ContentPlaceHolder1$fvTravelReimburseDetails$newTotalCtl").value = commafy(amount.toFixed(2));
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
        ������Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        �����ڼ�<span class="requiredLable">*</span></div>
                    <div>
                        <asp:DropDownList ID="PeriodDDL" runat="server" DataSourceID="odsPeriod" DataTextField="ReimbursePeriod"
                            DataValueField="ReimbursePeriodID" Width="170px">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="odsPeriod" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                            SelectCommand=" select 0 ReimbursePeriodID,' ��ѡ��' ReimbursePeriod Union SELECT [ReimbursePeriodID], convert(varchar(50),year(ReimbursePeriod))+'-'+Right(100+month(ReimbursePeriod),2) ReimbursePeriod FROM [ReimbursePeriod] ">
                        </asp:SqlDataSource>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        Ա��</div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ְλ</div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ����</div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ְ����</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" CssClass="InputTextReadOnly" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblTransportFee" runat="server" Text="��ͨ����Ԥ��" /></div>
                    <div>
                        <asp:TextBox ID="txtTransportFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblHotelFee" runat="server" Text="ס�޷���Ԥ��" /></div>
                    <div>
                        <asp:TextBox ID="txtHotelFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblMealFee" runat="server" Text="�ͷ�Ԥ��" /></div>
                    <div>
                        <asp:TextBox ID="txtMealFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblOtherFee" runat="server" Text="��������Ԥ��" /></div>
                    <div>
                        <asp:TextBox ID="txtOtherFee" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <asp:Label ID="lblTotal" runat="server" Text="����Ԥ���ܼ�" /></div>
                    <div>
                        <asp:TextBox ID="txtTotal" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        ��������</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" TextMode="multiline"
                            Height="60px" Columns="75"></asp:TextBox></div>
                </td>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        ����
                    </div>
                    <uc2:UCFlie ID="UCFileUpload" runat="server" Width="400px" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        ����������ϸ</div>
    <asp:UpdatePanel ID="upFormTravelApplyDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvFormTravelApplyDetails" runat="server" CssClass="GridView" AutoGenerateColumns="False"
                DataKeyNames="FormTravelApplyDetailID" OnRowDataBound="gvFormTravelApplyDetails_RowDataBound"
                CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblBeginDate" runat="server" Text='<%# Eval("BeginDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblDays" runat="server" Text='<%# Eval("Days") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����س���">
                        <ItemTemplate>
                            <asp:Label ID="lblDeparture" runat="server" Text='<%# Eval("Departure") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ŀ�ĵس���">
                        <ItemTemplate>
                            <asp:Label ID="lblDestination" runat="server" Text='<%# Eval("Destination") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ͨ����">
                        <ItemTemplate>
                            <asp:Label ID="lblVehicle" runat="server" Text='<%# Eval("Vehicle") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����ԭ��">
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
                                ����ʱ��
                            </td>
                            <td style="width: 150px;">
                                ����ʱ��
                            </td>
                            <td style="width: 60px;">
                                ��������
                            </td>
                            <td style="width: 200px;">
                                �����س���
                            </td>
                            <td style="width: 200px;">
                                Ŀ�ĵس���
                            </td>
                            <td style="width: 150px;">
                                ��ͨ����
                            </td>
                            <td style="width: 351px;">
                                ����ԭ��
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <div class="title" style="width: 1258px">
        ������ϸ��Ϣ</div>
    <asp:UpdatePanel ID="upTravelReimburseDetails" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <gc:GridView ID="gvTravelReimburseDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormPersonalReimburseDetailID" DataSourceID="odsTravelReimburseDetails"
                OnRowDataBound="gvTravelReimburseDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="�����ص�">
                        <ItemTemplate>
                            <asp:Label ID="lblPlace" runat="server" Text='<%# Eval("Place") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPlace" runat="server" Text='<%# Bind("Place") %>' Width="180px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="200px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��������">
                        <ItemTemplate>
                            <asp:Label ID="lblOccurDate" runat="server" Text='<%# Eval("OccurDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <uc1:DateInput ID="UCOccurDate" runat="server" IsReadOnly="false" SelectedDate='<%# Bind("OccurDate") %>'
                                IsExpensePeriod="true" />
                        </EditItemTemplate>
                        <ItemStyle Width="160px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����������">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseManageType" runat="server" Text='<%# GetExpenseManageTypeNameById(Eval("ExpenseManageTypeID")) %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList runat="server" ID="dplExpenseManageType" DataTextField="ExpenseManageTypeName"
                                DataValueField="ExpenseManageTypeID" Width="230px" DataSourceID="odsExpenseManageType"
                                SelectedValue='<%# Bind("ExpenseManageTypeID") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="odsExpenseManageType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                SelectCommand="select ExpenseManageTypeID,ExpenseManageTypeName from dbo.ExpenseManageType where IsActive=1 and ExpenseManageCategoryID=2 order by ExpenseManageTypeName">
                            </asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemStyle Width="250px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�������">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="120px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ע">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRemark" runat="server" MaxLength="300" Text='<%# Bind("Remark") %>'
                                Width="420px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Width="442px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="�༭"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="ɾ��"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Update"
                                Text="����"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="ȡ��"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 200px;" class="Empty1">
                                �����ص�
                            </td>
                            <td style="width: 160px;">
                                ��������
                            </td>
                            <td style="width: 250px;">
                                ����������
                            </td>
                            <td style="width: 150px;">
                                �������
                            </td>
                            <td style="width: 442px;">
                                ��ע
                            </td>
                            <td style="width: 60px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="Header" />
            </gc:GridView>
            <asp:FormView ID="fvTravelReimburseDetails" runat="server" DataKeyNames="FormTravelReimburseDetailID"
                DataSourceID="odsTravelReimburseDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 200px;" align="center">
                                <asp:TextBox ID="txtPlace" Text='<%# Bind("Place") %>' runat="server" Width="180px"></asp:TextBox>
                            </td>
                            <td style="width: 160px;" align="center">
                                <uc1:DateInput ID="UCOccurDate" runat="server" IsReadOnly="false" IsExpensePeriod="true" />
                            </td>
                            <td style="width: 250px;" align="center">
                                <asp:DropDownList runat="server" ID="dplExpenseManageType" DataTextField="ExpenseManageTypeName"
                                    DataValueField="ExpenseManageTypeID" Width="230px" DataSourceID="odsExpenseManageType">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsExpenseManageType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="select ExpenseManageTypeID,ExpenseManageTypeName from dbo.ExpenseManageType where IsActive=1 and ExpenseManageCategoryID=2 order by ExpenseManageTypeName">
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 150px;" align="center">
                                <asp:TextBox ID="txtAmount" MaxLength="15" runat="server" Width="130px"></asp:TextBox>
                            </td>
                            <td style="width: 442px;" align="center">
                                <asp:TextBox ID="txtRemark" runat="server" MaxLength="300" Width="420px"></asp:TextBox>
                            </td>
                            <td style="width: 60px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="���" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="UCOccurDate$txtDate"
                                ErrorMessage="��ѡ�������ڣ�" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="txtAmount"
                                ErrorMessage="����д������" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF3" runat="server" ControlToValidate="dplExpenseManageType"
                                ErrorMessage="��ѡ��������ͣ�" Display="None" ValidationGroup="NewDetailRow"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAmount"
                                ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                                ErrorMessage="����������" ValidationGroup="NewDetailRow"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummaryINS" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="NewDetailRow" />
                        </tr>
                    </table>
                    <br />
                </InsertItemTemplate>
            </asp:FormView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="odsTravelReimburseDetails" runat="server" DeleteMethod="DeleteFormTravelReimburseDetailByID"
        SelectMethod="GetFormPersonalReimburseDetail" UpdateMethod="UpdateFormTravelReimburseDetail"
        TypeName="BusinessObjects.PersonalReimburseBLL" InsertMethod="AddFormTravelReimburseDetail"
        OnInserting="odsTravelReimburseDetails_Inserting" OnObjectCreated="odsTravelReimburseDetails_ObjectCreated"
        OnInserted="odsTravelReimburseDetails_Inserted" OnUpdating="odsTravelReimburseDetails_Updating"
        OnUpdated="odsTravelReimburseDetails_Updated">
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
                    Text="�ύ" />
                <asp:Button ID="SaveBtn" runat="server" CssClass="button_nor" OnClick="SaveBtn_Click"
                    Text="����" />
                <asp:Button ID="CancelBtn" runat="server" CssClass="button_nor" OnClick="CancelBtn_Click"
                    Text="����" />
                <asp:Button ID="DeleteBtn" runat="server" CssClass="button_nor" OnClick="DeleteBtn_Click"
                    Text="ɾ��" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upCustomer" />
</asp:Content>
