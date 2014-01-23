﻿var date_start,date_end,g_object
var today = new Date();
var separator="-";
var inover=false;
var IsExpensePeriod=false;

function testing()
{
	window.alert("");
}
function CheckDatePicker(obj,sExpensePeriod)
{
    
    strDate=obj.value;
    strDate=strDate.replace(/^(\s)*|(\s)*$/g,"");//去掉字符串两边的空格
    
   //验证规则：长日期格式，不足用0补齐，如2003-09-01
    var newPar=/^\d{4}\-\d{2}\-\d{2}$/
    if (strDate.length>0 && newPar.test(strDate)==false && sExpensePeriod==false)
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

//mode :时间变换的类型0-年 1-月 2-直接选择月
function change_date(temp,mode)
{
	var t_month,t_year;
    if (mode){
        if(mode==1)
        t_month=parseInt(document.all.cele_date_month.value,10)+parseInt(temp,10);
        else
        t_month=parseInt(temp);
        if (t_month<document.all.cele_date_month.options(0).text) {
            document.all.cele_date_month.value=document.all.cele_date_month.options(document.all.cele_date_month.length-1).text;
            change_date(parseInt(document.all.cele_date_year.value,10)-1,0);
            }
        else{
            if (t_month>document.all.cele_date_month.options(document.all.cele_date_month.length-1).text){
                document.all.cele_date_month.value=document.all.cele_date_month.options(0).text;
                change_date(parseInt(document.all.cele_date_year.value,10)+1,0);
                }            
            else
                {document.all.cele_date_month.value=t_month;
                 set_cele_date(document.all.cele_date_year.value,document.all.cele_date_month.value);                
                }
        }
    }  
    else{
        t_year=parseInt(temp,10);
        
        if (t_year<document.all.cele_date_year.options(0).text) {
            document.all.cele_date_year.value=document.all.cele_date_year.options(0).text;
            set_cele_date(document.all.cele_date_year.value,1);                
            }
        else{
            if (parseInt(t_year,10)>parseInt(document.all.cele_date_year.options(document.all.cele_date_year.length-1).text,10)){
                document.all.cele_date_year.value=document.all.cele_date_year.options(document.all.cele_date_year.length-1).text;
                set_cele_date(document.all.cele_date_year.value,12);                
                }            
            else
                {document.all.cele_date_year.value=t_year;
                 set_cele_date(document.all.cele_date_year.value,document.all.cele_date_month.value);                
                }
        }
    }
    /*********2002-02-01 MODIFY BY WING **************/
    window.cele_date.focus();
    /****************MODIFY END***********************/
}

//初始化日历
function init(date_start,date_end)
{
     var temp_str;
     var tempmsg;
     var tempmsg1;
     var i=0;
     var j=0;
     date_start=new Date(1980,7,1);
     date_end=new Date(2004,8,1);
     document.writeln('<div name="cele_date" id="cele_date"  style="width:210px; height:210px; display:none; Z-INDEX: 3; LEFT: 69px; POSITION: absolute; TOP: 159px;" onClick="event.cancelBubble=true;" onBlur="hilayer()" onMouseout="lostlayerfocus()">-</div>');
     window.cele_date.innerHTML="";
     temp_str='<table border="0" class=rpt style="border:5px solid #dddddd" CellPadding="2" CellSpacing="1" ><tr><td align="center" colspan="7" onmouseover="overcolor(this)" style=padding:3px;>';
     temp_str+='<input type="Button" value="<<" onclick="change_date(-1,1)" onmouseover="getlayerfocus()" style="margin-left:0px;width:28px;"> ';

     temp_str+="";
     temp_str+='<select name="cele_date_year" id="cele_date_year" language="javascript" onchange="change_date(this.value,0)" onmouseover="getlayerfocus()" onblur="getlayerfocus()" style="width:60px;font-size: 9pt; border: 1px #666666 outset; background-color: #F4F8FB" id="Button" name="Button">';

     for (i=1900;i<=2050;i++)
     {
     	temp_str+='<OPTION value="'+i.toString()+'">'+i.toString()+'</OPTION>';
     }
     temp_str+='</select>';
     temp_str+="";
     temp_str+='<select name="cele_date_month" id="cele_date_month" language="javascript" onchange="change_date(this.value,2)" onmouseover="getlayerfocus()" onblur="getlayerfocus()" style="width:60px;font-size: 9pt; border: 1px #666666 outset; background-color: #F4F8FB\">';

     for (i=1;i<=12;i++)
     {
     	temp_str+='<OPTION value="'+i.toString()+'">'+i.toString()+'</OPTION>';
     }
     temp_str+="</select> ";
     temp_str+="";
     temp_str+='<input type="Button"  value=">>" onclick="change_date(1,1)" onmouseover="getlayerfocus()" style="margin-right:0px;width:28px;">';
     tempmsg=temp_str;
     temp_str+='</td></tr><tr class="rptHeader" height="18"><td onmouseover="overcolor(this)" class="DateTd">';
     temp_str+='<font color="red">Sun</font></td><td class="DateTd">';
     temp_str+='Mon</td><td class="DateTd">'; 
     temp_str+='Tue</td><td class="DateTd">'; 
     temp_str+='Wed</td><td class="DateTd">';
     temp_str+='Thu</td><td class="DateTd">';
     temp_str+='Fri</td><td class="DateTd">'; 
     temp_str+='<font color="#00CC00">Sat</font></td></tr>';
     for (i=1 ;i<=6 ;i++)
     {
     temp_str+="<tr>";
        for(j=1;j<=7;j++){
            temp_str+='<td  class="DateTd" name="c'+i+'_'+j+'" id="c'+i+'_'+j+'" style="Cursor:pointer;" style="color:#000000" language="javascript" onmouseover="overcolor(this)" onmouseout="outcolor(this)" onclick="td_click(this)">?</td>';
            }
     temp_str+="</tr>";    
     }
     temp_str+="</td></tr></table>";
     
     //Add by RichardZhang
     var myIframe = "<iframe src='javascript:false' style='position:absolute; visibility:inherit; top:0px; left:0px; width:190px; height:120px; z-index:-1; filter=progid:DXImageTransform.Microsoft.Alpha(style=0,opacity=0);'></iframe>";
     temp_str+=myIframe;
     
     window.cele_date.innerHTML=temp_str;
}


function set_cele_date(year,month)
{
   var i,j,p,k;
   var nd=new Date(year,month-1,1);
   event.cancelBubble=true;
   document.all.cele_date_year.value=year;//alert(document.all.cele_date_year.value);
   document.all.cele_date_month.value=month;   
   k=nd.getDay()-1;
   var temp;
   for (i=1;i<=6;i++)
      for(j=1;j<=7;j++)
      {
      eval('c'+i+'_'+j+'.innerHTML="&nbsp;"');
      eval('c'+i+'_'+j+'.bgColor="#F5F5F5"');
      eval('c'+i+'_'+j+'.style.cursor="pointer"');
      }
   while(month-1==nd.getMonth())
    { j=(nd.getDay() +1);
      p=parseInt((nd.getDate()+k) / 7)+1;
      eval('c'+p+'_'+j+'.innerHTML="'+nd.getDate()+'"');
      if ((nd.getDate()==today.getDate())&&(document.all.cele_date_month.value==today.getMonth()+1)&&(document.all.cele_date_year.value==today.getFullYear())){
      	 eval('c'+p+'_'+j+'.bgColor="#8FCA18"');
      }
      if (nd>date_end || nd<date_start)
      {
      eval('c'+p+'_'+j+'.bgColor="#FF9999"');
      eval('c'+p+'_'+j+'.style.cursor="text"');
      }
      nd=new Date(nd.valueOf() + 86400000);
    }
}

function show_cele_date(eP,d_start,d_end,t_object)
{
	window.cele_date.style.display="";
	//window.cele_date.style.zIndex=99;
	var s,cur_d;
	var eT = eP.offsetTop;  
	var eH = eP.offsetHeight+eT;  
	var dH = window.cele_date.style.pixelHeight;  
	var sT = document.body.scrollTop || document.documentElement.scrollTop || window.pageYOffset;
    if(!sT){sT=0;}
	var sL = document.body.scrollLeft || document.documentElement.scrollLeft || window.pageXOffset; 
    if(!sL){sL=0;}
	event.cancelBubble=true;
//window.alert(window.cele_date.style.posTop);	
	window.cele_date.style.posLeft = event.clientX-event.offsetX+sL+15;
	//Richard 2006-06-29  
	window.cele_date.style.posTop = event.clientY-event.offsetY+eH+sT-10;
	//window.cele_date.style.posTop = event.clientY-event.offsetY+sT-5;
	//Richard 2006-06-29 End
//window.alert(window.cele_date.style.posTop);
	if (window.cele_date.style.posLeft+window.cele_date.clientWidth>document.body.clientWidth) 
	    window.cele_date.style.posLeft+=eP.offsetWidth-window.cele_date.clientWidth-50;

	if (d_start!=""){
		if (d_start=="today"){
			date_start=new Date(today.getFullYear(),today.getMonth(),today.getDate());
		}else{
			s=d_start.split(separator);
			date_start=new Date(s[0],s[1]-1,s[2]);
		}
	}else{
		date_start=new Date(1750,1,1);
	}

	if (d_end!=""){
		s=d_end.split(separator);
		date_end=new Date(s[0],s[1]-1,s[2]);
	}else{
		date_end=new Date(3000,1,1);
	}

	g_object=t_object;
	

	cur_d=new Date();
	//set_cele_date(cur_d.getFullYear(),cur_d.getMonth()+1);
//window.alert(today.getFullYear()
	set_cele_date(today.getFullYear(),today.getMonth()+1);
	window.cele_date.style.display="block";
	window.cele_date.focus();
}

function td_click(t_object)
{
	var t_d;
	if (parseInt(t_object.innerHTML,10)>=1 && parseInt(t_object.innerHTML,10)<=31 ) 
	{	t_d=new Date(document.all.cele_date_year.value,document.all.cele_date_month.value-1,t_object.innerHTML);
		if (t_d<=date_end && t_d>=date_start)
		{
			var year = document.all.cele_date_year.value;
			var month = document.all.cele_date_month.value;
			var day = t_object.innerHTML;
			if (parseInt(month)<10) month = "0" + month;
			if (parseInt(day)<10) day = "0" + day;
            if (IsExpensePeriod==true)
            {
            g_object.value=year+""+month;
            }
            else
            {
            g_object.value=year+separator+month+separator+day;
            }
			window.cele_date.style.display="none";
			if (g_object.onchange != null)
			{
				g_object.onchange(); //add by RichardZhang 2005-10-12
			}
		}
	}
}

function h_cele_date()
{
    window.cele_date.style.display="none";
}

function overcolor(obj)
{
  if (obj.style.cursor=="hand") obj.style.color = "#ff0000";

  inover=true;
  window.cele_date.focus();
}

function outcolor(obj)
{
	obj.style.color = "#000000";
	inover=false;

}

function getNow(o){
    var Stamp=new Date();
    var year = Stamp.getFullYear();
    var month = Stamp.getMonth()+1;
    var day = Stamp.getDate();
    if(month<10){
	month="0"+month;
    }
    if(day<10){
	day="0"+day;
    }
    o.value=year+separator+month+separator+day;
}

function hilayer()
{
	if (inover==false)
	{
		var lay=document.all.cele_date;
		lay.style.display="none";
	}
}

function getlayerfocus()
{
	inover=true;
}

function lostlayerfocus()
{
	inover=false;
}

function DateSelection(sDateCtrl,ExpensePeriod)
{
	//testing();
	//return;
    IsExpensePeriod=ExpensePeriod;
    
    var strDateTime;
    if(sDateCtrl.tagName==undefined)
    {
        strDateTime = document.all(sDateCtrl).value;
    }
    else
    {
        strDateTime = sDateCtrl.value;
    }
    
    var strDate = strDateTime ;
    if ( strDateTime.indexOf(" ") > 0 )
    {
        strDate = strDateTime.substring(0,strDateTime.indexOf(" ")); 
    }
    
	var arrDate = strDate.split("-");
	if(arrDate.length == 3)
	{
		var tmp_year = eval(arrDate[0]);
		var tmp_month = eval(arrDate[1]);
		var tmp_date = eval(arrDate[2].substr(0,2));
		
		if(tmp_month>0 && tmp_month<13 && tmp_date>0 && tmp_date<31 && tmp_year>1000 && tmp_year<9999)
		{
			today.setFullYear(tmp_year);
			today.setMonth(tmp_month-1);
			today.setDate(tmp_date);
		}
	}
	
	if(sDateCtrl.tagName==undefined)
    {
	    show_cele_date(document.all(sDateCtrl), "", "", document.all(sDateCtrl));
	}
	else
	{
	    show_cele_date(sDateCtrl,"","",sDateCtrl);
	}
}

init();