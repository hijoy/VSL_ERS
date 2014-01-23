<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="OtherForm_MaterialApply" Title="������������" Codebehind="MaterialApply.aspx.cs" %>

<%@ Register Src="../UserControls/ShopSelectControl.ascx" TagName="UCShop" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/MaterialControl.ascx" TagName="UCMaterial" TagPrefix="uc2" %>
<%@ Register Src="../UserControls/ucUpdateProgress.ascx" TagName="ucUpdateProgress"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" src="../Script/js.js" type="text/javascript"></script>
    <script language="javascript" src="../Script/DateInput.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ParameterChanged() {
            var txtQuantity = document.all("ctl00$ContentPlaceHolder1$fvMaterialDetails$newQuantityCtl").value;
            var txtPrice = document.all("ctl00$ContentPlaceHolder1$fvMaterialDetails$newMaterialPriceCtl").value;
            if (txtQuantity != "" && isNaN(txtQuantity)) {
                alert("��¼������");
                window.setTimeout(function () { document.getElementById("ctl00$ContentPlaceHolder1$fvMaterialDetails$newQuantityCtl").focus(); }, 0);
                return false;
            }
            if (txtPrice != "" && isNaN(txtPrice)) {
                alert("��¼������");
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
    <div class="title" style="width: 1258px">
        ������Ϣ</div>
    <div style="width: 1268px; background-color: #F6F6F6">
        <table style="margin-left: 15px; margin-right: 15px; margin-bottom: 5px">
            <tr>
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
                <td style="width: 200px">
                    <div class="field_title">
                        �ŵ�<span class="requiredLable">*</span></div>
                    <div>
                        <uc1:UCShop ID="UCShop" runat="server" Width="130px" IsNoClear="true" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 200px">
                    <div class="field_title">
                        ��һ��������<span class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="FirstVolumeCtl" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        �ڶ���������<span class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="SecondVolumeCtl" MaxLength="15" runat="server" CssClass="InputText"
                            Width="170px"></asp:TextBox></div>
                </td>
                <td style="width: 200px">
                    <div class="field_title">
                        ������������<span class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="ThirdVolumeCtl" MaxLength="15" runat="server" CssClass="InputText"
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
                <td style="width: 200px">
                </td>
                <td style="width: 200px">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <div class="field_title">
                        ��������<span class="requiredLable">*</span></div>
                    <div>
                        <asp:TextBox ID="RemarkCtl" runat="server" CssClass="InputText" MaxLength="800" TextMode="multiline"
                            Height="60px" Columns="140"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="title" style="width: 1260px">
        ��ϸ��Ϣ</div>
    <asp:UpdatePanel ID="upMaterialDetails" runat="server">
        <ContentTemplate>
            <gc:GridView ID="gvMaterialDetails" runat="server" ShowFooter="True" CssClass="GridView"
                AutoGenerateColumns="False" DataKeyNames="FormMaterialDetailID" DataSourceID="odsMaterialDetails"
                OnRowDataBound="gvMaterialDetails_RowDataBound" CellPadding="0" CellSpacing="0">
                <Columns>
                    <asp:TemplateField HeaderText="��Ʒ����">
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialName" runat="server" Text='<%# Eval("MaterialName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="250px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��λ">
                        <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�������">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="400px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����">
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialPrice" runat="server" Text='<%# Eval("MaterialPrice","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="����">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblSum" Text="�ܼƣ�"></asp:Label>
                        </FooterTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="С��">
                        <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Amount","{0:N}") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label runat="server" ID="lblTotal"></asp:Label>
                        </FooterTemplate>
                        <FooterStyle CssClass="RedTextAlignCenter" HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="��ע">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="212px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="ɾ��"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="Header" />
                <FooterStyle Width="50px" Wrap="True" />
                <EmptyDataTemplate>
                    <table>
                        <tr class="Header">
                            <td style="width: 250px;" class="Empty1">
                                ��������
                            </td>
                            <td style="width: 50px;">
                                ��λ
                            </td>
                            <td style="width: 400px;">
                                �������
                            </td>
                            <td style="width: 100px;">
                                ����
                            </td>
                            <td style="width: 100px;">
                                ����
                            </td>
                            <td style="width: 100px;">
                                С��
                            </td>
                            <td style="width: 212px;">
                                ��ע
                            </td>
                            <td style="width: 50px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </gc:GridView>
            <asp:FormView ID="fvMaterialDetails" runat="server" DataKeyNames="FormMaterialDetailID"
                DataSourceID="odsMaterialDetails" DefaultMode="Insert" CellPadding="0">
                <InsertItemTemplate>
                    <table class="FormView">
                        <tr>
                            <td style="width: 250px;" align="center">
                                <uc2:UCMaterial ID="newUCMaterial" AutoPostBack="true" IsNoClear="true" runat="server"
                                    OnMaterialNameTextChanged="OnMaterialNameTextChanged" MaterialID='<%#Bind("MaterialID")%>'
                                    Width="160px" />
                            </td>
                            <td style="width: 50px;" align="center">
                                <asp:TextBox ID="newUOMCtl" runat="server" Width="30px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 400px;" align="center">
                                <asp:TextBox ID="newDescCtl" runat="server" Width="380px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:TextBox ID="newMaterialPriceCtl" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:TextBox ID="newQuantityCtl" runat="server" MaxLength="15" Text='<%# Bind("Quantity") %>'
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td style="width: 100px;" align="center">
                                <asp:TextBox ID="newTotalCtl" runat="server" Width="80px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width: 212px;" align="center">
                                <asp:TextBox ID="newRemarkCtl" runat="server" MaxLength="200" Text='<%# Bind("Remark") %>'
                                    Width="170px"></asp:TextBox>
                            </td>
                            <td style="width: 50px;" align="center">
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                    Text="���" ValidationGroup="NewDetailRow"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <asp:RequiredFieldValidator ID="RF1" runat="server" ControlToValidate="newQuantityCtl"
                                Display="None" ErrorMessage="��¼��������" ValidationGroup="NewDetailRow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="newQuantityCtl"
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
    <asp:ObjectDataSource ID="odsMaterialDetails" runat="server" DeleteMethod="DeleteFormMaterialDetailByID"
        SelectMethod="GetFormMaterialDetail" TypeName="BusinessObjects.MaterialApplyBLL"
        OnInserting="odsMaterialDetails_Inserting" OnObjectCreated="odsMaterialDetails_ObjectCreated"
        InsertMethod="AddFormMaterialDetail">
        <DeleteParameters>
            <asp:Parameter Name="FormMaterialDetailID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FormMaterialID" Type="Int32" />
            <asp:Parameter Name="MaterialID" Type="Int32" />
            <asp:Parameter Name="Quantity" Type="Decimal" />
            <asp:Parameter Name="Remark" Type="String" />
        </InsertParameters>
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
