<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    Inherits="SaleForm_SalesGeneralApply" Title="��������" CodeBehind="SalesGeneralApply.aspx.cs" %>

<%@ Register Src="../UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/SKUControl.ascx" TagName="SKUSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MultiAttachmentFile.ascx" TagName="UCFlie" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetPackageUnitPrice(PackageUnit_ID, obj) {
            var txtPackageUnit = document.getElementById(PackageUnit_ID);
            var totalFee;
            var j;
            for (j = 2; j <= 100; j++) {
                if (document.all("ctl00_ContentPlaceHolder1_gvApplyDetails_ctl" + GetTBitNum(j) + "_lblAmount")) {
                    totalFee = uncommafy(document.all("ctl00_ContentPlaceHolder1_gvApplyDetails_ctl" + GetTBitNum(j) + "_lblAmount").innerText);
                    break;
                }
            }
            if (totalFee && totalFee != "" && obj.value && obj.value != "" && !isNaN(obj.value) && !isNaN(totalFee)) {
                txtPackageUnit.value = commafy((totalFee / obj.value).toFixed(2));
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
        ������Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        Ա��</div>
                    <div>
                        <asp:TextBox ID="StuffNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ְλ</div>
                    <div>
                        <asp:TextBox ID="PositionNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ����</div>
                    <div>
                        <asp:TextBox ID="DepartmentNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ְ����</div>
                    <div>
                        <asp:TextBox ID="AttendDateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        �����ڼ�</div>
                    <div>
                        <asp:TextBox ID="BeginPeriodCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblSignPeriod" runat="server">~~</asp:Label>&nbsp;
                        <asp:TextBox ID="EndPeriodCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        ����С��</div>
                    <div>
                        <asp:TextBox ID="ExpenseSubCategoryCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ͻ�����</div>
                    <div>
                        <asp:TextBox ID="CustomerNameCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td valign="top" style="width: 200px">
                    <div class="field_title">
                        ��������<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="txtFormApplyName" runat="server" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ŵ�</div>
                    <div>
                        <asp:DropDownList ID="ShopDDL" runat="server" DataSourceID="odsShop" DataTextField="ShopName"
                            DataValueField="ShopID" Width="180px">
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ֧����ʽ<label class="requiredLable">*</label></div>
                    <div>
                        <asp:DropDownList ID="PaymentTypeDDL" runat="server" DataSourceID="odsPaymentType"
                            DataTextField="PaymentTypeName" DataValueField="PaymentTypeID" Width="180px">
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��ͬ���(��ͬ�ڷ�������д)</div>
                    <div>
                        <asp:TextBox ID="ContractNoCtl" runat="server" MaxLength="20" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <div class="field_title">
                        ��������</div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" TextMode="multiline" Height="60px" Columns="80"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2" valign="top">
                    <div class="field_title">
                        ����</div>
                    <div>
                        <uc2:UCFlie ID="UCFileUpload" runat="server" Width="380px" />
                    </div>
                </td>
                <td valign="top">
                    <div class="field_title">
                        ����Ҫ��<label class="requiredLable">*</label></div>
                    <div>
                        <asp:CheckBoxList runat="server" ID="chkListReimburseRequirements" RepeatDirection="Horizontal"
                            RepeatColumns="8" RepeatLayout="Flow">
                            <asp:ListItem Text="ͼƬ" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Э����" Value="2"></asp:ListItem>
                            <asp:ListItem Text="�ͻ���" Value="4"></asp:ListItem>
                            <asp:ListItem Text="��ͬ" Value="8"></asp:ListItem>
                            <asp:ListItem Text="DM" Value="16"></asp:ListItem>
                            <asp:ListItem Text="����" Value="32"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1258px">
        ������Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td colspan="2" style="width: 400px">
                    <div class="field_title">
                        �����ڼ�<label class="requiredLable">*</label></div>
                    <div>
                        <uc1:UCDateInput ID="UCPromotionBegin" runat="server" IsReadOnly="false" />
                        <asp:Label ID="Label2" runat="server">~~</asp:Label>
                        <uc1:UCDateInput ID="UCPromotionEnd" runat="server" IsReadOnly="false" />
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ��������<label class="requiredLable">*</label></div>
                    <div>
                        <asp:DropDownList ID="PromotionScopeDDL" runat="server" DataSourceID="odsPromotionScope"
                            DataTextField="PromotionScopeName" DataValueField="PromotionScopeID" Width="170px">
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ������ʽ<label class="requiredLable">*</label></div>
                    <div>
                        <asp:DropDownList ID="PromotionTypeDDL" runat="server" DataSourceID="odsPromotionType"
                            DataTextField="PromotionTypeName" DataValueField="PromotionTypeID" Width="170px">
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �����</div>
                    <div>
                        <asp:TextBox ID="PromotionDescCtl" runat="server" MaxLength="400" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ������ʽ<label class="requiredLable">*</label></div>
                    <div>
                        <asp:DropDownList ID="ShelfTypeDDL" runat="server" DataSourceID="odsShelfType" DataTextField="ShelfTypeName"
                            DataValueField="ShelfTypeID" Width="170px">
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        ��һ��������<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="FirstVolumeCtl" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ڶ���������<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="SecondVolumeCtl" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ������������<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="ThirdVolumeCtl" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        Ԥ��������<label class="requiredLable">*</label></div>
                    <div>
                        <asp:TextBox ID="txtEstimatedSaleVolume" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �������</div>
                    <div>
                        <asp:TextBox ID="txtPackageUnitPrice" MaxLength="15" style="color:#ea0000;" runat="server" ReadOnly="true"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <asp:RegularExpressionValidator ID="RF1" runat="server" ControlToValidate="FirstVolumeCtl"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="����������" ValidationGroup="VolumeCtl"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF2" runat="server" ControlToValidate="SecondVolumeCtl"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="����������" ValidationGroup="VolumeCtl"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="ThirdVolumeCtl"
                        ValidationExpression="<%$ Resources:RegularExpressions, Money %>" Display="None"
                        ErrorMessage="����������" ValidationGroup="VolumeCtl"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummaryVolumeCtl" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="VolumeCtl" />
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="odsShop" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [ShopID], [ShopName] FROM [Shop] where IsActive = 1 and CustomerID = @CustomerID order by ShopName">
            <SelectParameters>
                <asp:Parameter Name="CustomerID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="odsPaymentType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [PaymentTypeID], [PaymentTypeName] FROM [PaymentType] where IsActive = 1 ">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="odsPromotionScope" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [PromotionScopeID], [PromotionScopeName] FROM [PromotionScope] where IsActive = 1 ">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="odsPromotionType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [PromotionTypeID], [PromotionTypeName] FROM [PromotionType] where IsActive = 1 ">
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="odsShelfType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
            SelectCommand=" SELECT [ShelfTypeID], [ShelfTypeName] FROM [ShelfType] where IsActive = 1 ">
        </asp:SqlDataSource>
    </div>
    <br />
    <div class="title" style="width: 1258px;">
        Ԥ����Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        �ͻ����Ԥ��</div>
                    <div>
                        <asp:TextBox ID="CustomerBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ͻ�ʣ��Ԥ��</div>
                    <div>
                        <asp:TextBox ID="CustomerBudgetRemainCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        <img src="../Images/DeptBudget.png" alt="�����꿪ʼ�µ��������顮�����ڼ䡯����ʼ��Ϊֹ���ۼ�Ԥ��YTD" /></div>
                    <div>
                        <asp:TextBox ID="OUBudgetCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ����������δִ�з���</div>
                    <div>
                        <asp:TextBox ID="OUApprovedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �����������������</div>
                    <div>
                        <asp:TextBox ID="OUApprovingAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ������ȷ��ִ�з���</div>
                    <div>
                        <asp:TextBox ID="OUCompletedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        �����ѱ�������</div>
                    <div>
                        <asp:TextBox ID="OUReimbursedAmountCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ���ſ���Ԥ��</div>
                    <div>
                        <asp:TextBox ID="OUBudgetRemainCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ����Ԥ��ʹ�ý���</div>
                    <div>
                        <asp:TextBox ID="OUBudgetRateCtl" runat="server" ReadOnly="true" Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 200px">
                </td>
                <td style="width: 200px">
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1260px;">
        ������ϸ��Ϣ</div>
    <asp:HiddenField ID="ExpenseSubCategoryID" runat="server" Visible="False" />
    <asp:UpdatePanel ID="upApplyDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvApplyDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormApplySKUDetailID" DataSourceID="odsApplyDetails"
                OnRowDataBound="gvApplyDetails_RowDataBound" CellPadding="0">
                <Columns>
                    <asp:TemplateField HeaderText="��Ʒ����">
                        <ItemTemplate>
                            <asp:Label ID="lblProductName" runat="server" Text='<%# GetProductNameByID(Eval("SKUID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="500px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="������">
                        <ItemTemplate>
                            <asp:Label ID="lblExpenseItem" runat="server" Text='<%# GetExpenseItemNameByID(Eval("ExpenseItemID")) %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="������">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ע">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="300px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="ɾ��"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 500px;" class="Empty1">
                                ��Ʒ����
                            </td>
                            <td style="width: 300px;">
                                ������
                            </td>
                            <td style="width: 100px;">
                                ������
                            </td>
                            <td style="width: 300px;">
                                ��ע
                            </td>
                            <td style="width: 65px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvApplyDetails" runat="server" DataKeyNames="FormApplySKUDetailID"
                DataSourceID="odsApplyDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 500px;" align="center">
                                <uc1:SKUSelect ID="UCSKU" runat="server" IsNoClear="true" Width="410px" />
                            </td>
                            <td style="width: 300px;" align="center">
                                <asp:DropDownList ID="newExpenseItemDDL" runat="server" DataSourceID="odsNewExpenseItem"
                                    DataTextField="ExpenseItemName" DataValueField="ExpenseItemID" Width="280px"
                                    SelectedValue='<%# Bind("ExpenseItemID") %>'>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="odsNewExpenseItem" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                                    SelectCommand="SELECT [ExpenseItemID], [AccountingCode]+'--'+[ExpenseItemName] ExpenseItemName FROM [ExpenseItem] where IsActive = 1 and ExpenseSubCategoryID = @ExpenseSubCategoryID order by ExpenseItemName">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ExpenseSubCategoryID" Name="ExpenseSubCategoryID"
                                            PropertyName="Value" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td style="width: 100px;" align="center" valign="top">
                                <asp:TextBox ID="newAmountCtl" MaxLength="15" runat="server" Text='<%# Bind("Amount") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 300px;" align="center">
                                <asp:TextBox ID="newRemarkCtl" MaxLength="20" runat="server" Text='<%# Bind("Remark") %>'
                                    Width="270px"></asp:TextBox>
                            </td>
                            <td style="width: 65px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="���" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="newAmountCtl"
                                Display="None" ErrorMessage="��¼��������" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RF2" runat="server" ControlToValidate="newExpenseItemDDL"
                                Display="None" ErrorMessage="��ѡ������" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RF3" runat="server" ControlToValidate="newAmountCtl"
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
    <div id="divSplitRate" runat="server" class="title" style="width: 1260px;" visible="false">
        ��̯������</div>
    <gc:GridView ID="gvSplitRate" runat="server" CssClass="GridView" AutoGenerateColumns="False"
        DataKeyNames="FormApplySplitRateID" CellPadding="0" Visible="false">
        <Columns>
            <asp:TemplateField HeaderText="�����ڼ�">
                <ItemTemplate>
                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("Period","{0:yyyy-MM}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��������̯������%��">
                <ItemTemplate>
                    <asp:TextBox ID="txtRate" runat="server" Text='<%# Bind("Rate") %>' Width="200"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="300" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��ע">
                <ItemTemplate>
                    <asp:TextBox ID="txtRemark" runat="server" Text='<%# Bind("Remark") %>' Width="600"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="667" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
    </gc:GridView>
    <asp:ObjectDataSource ID="odsApplyDetails" runat="server" DeleteMethod="DeleteFormApplyDetailByID"
        SelectMethod="GetFormApplyDetailView" TypeName="BusinessObjects.SalesApplyBLL"
        OnInserting="odsApplyDetails_Inserting" OnObjectCreated="odsApplyDetails_ObjectCreated"
        InsertMethod="AddFormApplyDetailView">
        <DeleteParameters>
            <asp:Parameter Name="FormApplySKUDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormApplyID" Type="Int32" />
            <asp:Parameter Name="SKUID" Type="Int32" />
            <asp:Parameter Name="ExpenseItemID" Type="Int32" />
            <asp:Parameter Name="Amount" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <asp:UpdatePanel ID="upCustomer" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div style="margin-top: 20px; width: 1200px; text-align: right">
                <asp:ImageButton ID="SubmitBtn" runat="server" ImageUrl="../images/btnSubmit.gif"
                    UseSubmitBehavior="False" OnClick="SubmitBtn_Click" Text="�ύ" />
                <asp:ImageButton ID="SaveBtn" runat="server" ImageUrl="../images/btnSave.gif" OnClick="SaveBtn_Click"
                    Text="����" ValidationGroup="VolumeCtl" />
                <asp:ImageButton ID="CancelBtn" runat="server" ImageUrl="../images/btnCancel.gif"
                    OnClick="CancelBtn_Click" Text="����" />
                <asp:ImageButton ID="DeleteBtn" runat="server" ImageUrl="../images/btnDelete.gif"
                    OnClick="DeleteBtn_Click" Text="ɾ��" />
            </div>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc3:ucUpdateProgress ID="upUP" runat="server" vAssociatedUpdatePanelID="upCustomer" />
</asp:Content>
