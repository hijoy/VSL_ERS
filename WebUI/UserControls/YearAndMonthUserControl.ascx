<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_YearAndMonthUserControl" Codebehind="YearAndMonthUserControl.ascx.cs" %>
<script type="text/javascript" >
function CheckExpensePeriodDatePicker(obj)
{
    strDate=obj.value;
    strDate=strDate.replace(/^(\s)*|(\s)*$/g,"");//去掉字符串两边的空格
    
   //验证规则：长日期格式，不足用0补齐，如2003-09-01
    var newPar=/^\d{4}?(?:0[1-9]|1[0-2])$/
    if (strDate.length>0 && newPar.test(strDate)==false)
    {
        obj.focus();
		alert("日期格式不对，请重新输入．");
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

    
<asp:TextBox ID="txtDate" runat="server" Width="70px" ToolTip="格式:yyyymm" OnTextChanged="txtDate_TextChanged"></asp:TextBox>
<asp:ImageButton
    ID="ibtDate" runat="server" ImageUrl="~/images/Calendar.gif" BorderColor="Silver"
    BorderStyle="None" BorderWidth="0px" ImageAlign="AbsMiddle" Height="19px" Width="19px" TabIndex="-1" AccessKey="?" />
<!--<span style="color:Red">(格式:yyyymm)</span>-->