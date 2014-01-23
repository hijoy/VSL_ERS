<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="QueryForm_FormContractList" Title="��ͬ��ѯ" Codebehind="FormContractList.aspx.cs" %>

<%@ Register Src="~/UserControls/UCDateInput.ascx" TagName="UCDateInput" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/OUSelect.ascx" TagName="OUSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/ShopSelectControl.ascx" TagName="UCShop" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Script/js.js" type="text/javascript"></script>
    <script src="../Script/DateInput.js" type="text/javascript"></script>
    <div class="title" style="width: 1260px;">
        ��������</div>
    <div class="searchDiv" style="width: 1270px;">
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr style="vertical-align: top;">
                <td style="width:250px;">
                    <div class="field_title">
                        ��ͬ���</div>
                    <asp:TextBox ID="txtFormNo" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:250px;">
                    <div class="field_title">
                        ǩԼ�Է���λ</div>
                    <asp:TextBox ID="txtCompanyName" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td style="width:250px;">
                    <div class="field_title">
                        ������</div>
                    <asp:TextBox ID="txtStuffUser" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td colspan="2" style="width:500px;">
                    <div class="field_title">
                        ���������ڲ���</div>
                    <uc2:OUSelect ID="UCOU" runat="server" Width="200px" />
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="width:250px;">
                    <div class="field_title">
                        ��ͬ����</div>
                    <asp:DropDownList runat="server" ID="dplContractType" DataSourceID="sdsContractType"
                        DataTextField="ContractTypeName" DataValueField="ContractTypeID" Width="170px"
                        AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="ȫ��" Value=""></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sdsContractType" runat="server" ConnectionString="<%$ ConnectionStrings:ERSConnectionString %>"
                        SelectCommand="SELECT [ContractTypeID], [ContractTypeName] FROM [ContractType]">
                    </asp:SqlDataSource>
                </td>
                <td style="width:250px;">
                    <div class="field_title">�Ƿ����</div>
                    <asp:DropDownList ID="IsStampedDDL" runat="server" CssClass="InputCombo" Width="180px">
                        <asp:ListItem Text="ȫ��" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:250px;">
                    <div class="field_title">�Ƿ������ѻ���</div>
                    <asp:DropDownList ID="IsRecoveryDDL" runat="server" CssClass="InputCombo" Width="180px">
                        <asp:ListItem Text="ȫ��" Value="3" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="��" Value="1"></asp:ListItem>
                        <asp:ListItem Text="��" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="2" style="width:500px;">
                    <div class="field_title">
                        �ύ����</div>
                    <nobr>
                    <uc1:UCDateInput ID="UCDateInputBeginDate" runat="server" IsReadOnly="false" />
                    <asp:Label ID="lbSign" runat="server">~~</asp:Label>
                    <uc1:UCDateInput ID="UCDateInputEndDate" runat="server" IsReadOnly="false" />
                    </nobr>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="5" valign="middle">
                    <span class="field_title">����״̬</span>
                    <asp:CheckBox ID="chkAwaiting" runat="server" Text="������" Checked="false"></asp:CheckBox>&nbsp;&nbsp;
                    <asp:CheckBox ID="chkApproveCompleted" runat="server" Text="�������" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkRejected" runat="server" Text="�˻ش��޸�" Checked="false" />&nbsp;&nbsp;
                    <asp:CheckBox ID="chkScrap" runat="server" Text="����" Checked="false" />
                </td>
            </tr>

        </table>
    </div>
    <table width="1200px">
        <tr>
            <td align="right" valign="middle" colspan="6">
                <asp:Button ID="btnSearch" runat="server" CssClass="button_nor" Text="��ѯ" OnClick="btnSearch_Click" />&nbsp;
                <asp:Button ID="btnExport" runat="server" CssClass="button_nor" Text="����" OnClick="btnExport_Click" />&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div class="title" style="width: 1260px;">
        ��ͬ���뵥�б�</div>
    <gc:GridView CssClass="GridView" ID="gvContractList" runat="server" DataSourceID="odsContractList"
        AutoGenerateColumns="False" DataKeyNames="FormID" AllowPaging="True" AllowSorting="True"
        PageSize="20" OnRowDataBound="gvContractList_RowDataBound">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblFormContractID" runat="server" Text='<%# Bind("FormID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FormNo" HeaderText="��ͬ���">
                <ItemTemplate>
                    <asp:LinkButton ID="lblFormNo" runat="server" CausesValidation="False" CommandName="Select"
                        Text='<%# Bind("ContractNo") %>'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StatusID" HeaderText="����״̬">
                <ItemTemplate>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="StuffName" HeaderText="������">
                <ItemTemplate>
                    <asp:Label ID="lblStuffName" runat="server" Text='<%# Bind("StuffName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="60px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SubmitDate" HeaderText="�ύ����">
                <ItemTemplate>
                    <asp:Label ID="lblSubmitDate" runat="server" Text='<%# Bind("SubmitDate", "{0:yyyy-MM-dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="70px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ContractName" HeaderText="��ͬ����">
                <ItemTemplate>
                    <asp:Label ID="lblCustomerName" runat="server" Text='<%# Bind("ContractName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="250px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ContractAmount" HeaderText="��ͬ���">
                <ItemTemplate>
                    <asp:Label ID="lblContractAmount" runat="server" Text='<%# Bind("ContractAmount","{0:N}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="100px" HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="ContractTypeName" HeaderText="��ͬ����">
                <ItemTemplate>
                    <asp:Label ID="lblContractTypeName" runat="server" Text='<%# Bind("ContractTypeName") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="160px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="FirstCompany" HeaderText="ǩԼ�Է���λ1">
                <ItemTemplate>
                    <asp:Label ID="lblFirstCompany" runat="server" Text='<%# Bind("FirstCompany") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="190px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="SecondCompany" HeaderText="ǩԼ�Է���λ2">
                <ItemTemplate>
                    <asp:Label ID="lblSecondCompany" runat="server" Text='<%# Bind("SecondCompany") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="150px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField SortExpression="BeginDate" HeaderText="��ͬ��Ч��">
                <ItemTemplate>
                    <asp:Label ID="lblBeginDate" runat="server" Text='<%# GetContractETD(Eval("BeginDate", "{0:yyyy/MM/dd}"),Eval("EndDate", "{0:yyyy/MM/dd}")) %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="140px" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="Header" />
        <EmptyDataTemplate>
            <table>
                <tr class="Header" style="border: 0;">
                    <td scope="col" style="width: 70px;" class="Empty1">
                        ��ͬ���
                    </td>
                    <td scope="col" style="width: 70px;">
                        ����״̬
                    </td>
                    <td scope="col" style="width: 100px;">
                        ������
                    </td>
                    <td scope="col" style="width: 70px;">
                        �ύ����
                    </td>
                    <td scope="col" style="width: 250px;">
                        ��ͬ����
                    </td>
                    <td scope="col" style="width: 100px;">
                        ��ͬ���
                    </td>
                    <td scope="col" style="width: 120px;">
                        ��ͬ����
                    </td>
                    <td scope="col" style="width: 170px;">
                        ǩԼ�Է���λ1
                    </td>
                    <td scope="col" style="width: 170px;">
                        ǩԼ�Է���λ2
                    </td>
                    <td scope="col" style="width: 140px;">
                        ��ͬ��Ч��
                    </td>
                </tr>
                <tr>
                    <td colspan="10" class="Empty2 noneLabel">
                        ��
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <SelectedRowStyle CssClass="SelectedRow" />
    </gc:GridView>
    <asp:DataGrid ID="ExportDataGrid" runat="server" Visible="true" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundColumn HeaderText="��ͬ���" DataField="ContractNo" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="����״̬" DataField="Status" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="������" DataField="StuffName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="�ύ����" DataField="SubmitDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="��ͬ����" DataField="ContractName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="��ͬ���" DataField="ContractAmount" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="��ͬ����" DataField="ContractTypeName" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="ǩԼ�Է���λ1" DataField="FirstCompany" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="ǩԼ�Է���λ2" DataField="SecondCompany" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="ǩԼ�Է���λ3" DataField="ThirdCompany" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="��ͬ��ʼʱ��" DataField="BeginDate" HeaderStyle-Font-Bold="true" />
            <asp:BoundColumn HeaderText="��ͬ����ʱ��" DataField="EndDate" HeaderStyle-Font-Bold="true" />
        </Columns>
    </asp:DataGrid>
    <asp:ObjectDataSource ID="odsContractList" runat="server" TypeName="BusinessObjects.FormQueryBLL"
        SelectMethod="GetPagedFormContractViewByRight" EnablePaging="True" SelectCountMethod="QueryFormContractViewCountByRight"
        SortParameterName="sortExpression">
        <SelectParameters>
            <asp:Parameter Name="queryExpression" Type="String" />
            <asp:Parameter Name="UserID" Type="Int32" />
            <asp:Parameter Name="PositionID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
