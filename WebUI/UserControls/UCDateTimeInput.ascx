<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_UCDateTimeInput" Codebehind="UCDateTimeInput.ascx.cs" %>
<script language="javascript" type="text/javascript" for="window" event="onfocus">

<script language="javascript" type="text/javascript">
<!--
function CheckDatePicker(obj,sExpensePeriod)
{
    
    strDate=obj.value;
    strDate=strDate.replace(/^(\s)*|(\s)*$/g,"");//ȥ���ַ������ߵĿո�
    
   //��֤���򣺳����ڸ�ʽ��������0���룬��2003-09-01
    var newPar=/^\d{4}\-\d{2}\-\d{2}$/
    if (strDate.length>0 && newPar.test(strDate)==false && sExpensePeriod==false)
    {
        obj.focus();
		alert("���ڸ�ʽ���ԣ����������룮");
		obj.value="";	
	    return false;
	}
	else
	{
	   return true;
	}
}

// -->
</script>

    
<nobr><asp:TextBox ID="txtDate" runat="server" Width="70px"></asp:TextBox>
<asp:ImageButton
    ID="ibtDate" runat="server" ImageUrl="~/images/Calendar.gif" BorderColor="Silver"
    BorderStyle="None" BorderWidth="0px" ImageAlign="AbsMiddle" Height="19px" Width="19px" TabIndex="-1" AccessKey="?" />
 <asp:DropDownList ID="ddlHour" runat="server" Width="40px" OnLoad="ddlHour_Load"></asp:DropDownList></nobr>