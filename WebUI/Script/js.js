   function showMDlg(strUrl)
    {
		    var sFeature = "dialogWidth:400px; dialogHeight:480px; edge:Raised; center:Yes; help:No; resizable:Yes; status:No;";
            var oRet;       
            oRet = window.showModalDialog((strUrl), window, sFeature);
    }

    function showDialog(strUrl)
    {
            var sFeature = "width=620,height=400,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,top=10,left=200"
		    window.open(strUrl,"_blank",sFeature);
    }
    function showCDialog(strUrl,width,height)
    {
            var sFeature = "width="+width+",height="+height+",directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,top=10,left=200"
		    window.open(strUrl,"_blank",sFeature);
    }

    function showMinDialog(strUrl)
    {
            var sFeature = "width=400,height=300,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,top=50,left=200"
		    window.open(strUrl,"_blank",sFeature);
    }

    function AppendUniqueParam(url,paramname,value)
    {
        var paramValue=null;
        var paramRule=new RegExp("(\\\?|&)"+paramname+"=([^&]+)(&|$)");
        var paramValues=url.match(paramRule);
        if(paramValues)
        {        
            url=url.replace(paramname+"="+paramValues[2],paramname+"="+value);         
        }	
        else
        {
            if(url.indexOf("?")>-1)
            {
                url=url+"&"+paramname+"="+value;
            }
            else
            {
                url=url+"?"+paramname+"="+value;
            }
        }
       
       return url;
    }

    //检查输入是小数
    function CheckInputIsDec(obj)
    {
        return ((event.keyCode>=48 && event.keyCode<=57) || ((event.keyCode==46) && (obj.value.indexOf('.') < 0)));
    }
    
    function CheckPasteIsDec(obj)
	{
		var strFloat= obj.value;

		strFloat=strFloat.replace(/^(\s)*|(\s)*$/g,"");//去掉字符串两边的空格        
		
		var newPar=/^(-|\+)?\d*.?\d+$///验证规则
		if(strFloat.length>0 && newPar.test(strFloat)==false)
		{
			obj.value="";
			return false;
		}
		else
		{
			obj.value=formatFloat(obj.value,2);
			if(obj.value>100000000)
			{
				obj.value="";
				alert("输入的值不能大于等于100,000,000,请重新输入!");			   
				return false;
			}
			else
			{
				return true;
			}
		}
	}
    
    function CheckInputIsDate(obj)
    {       
        // 0-9 48-57
        // -   45
        // /   47
        // .   46
         return ((event.keyCode>=48 && event.keyCode<=57) || (event.keyCode==45))
        //return (event.keyCode=="");
    }
    
     //只允许输入数字或者是字母
    function CheckInputIsDecorChr(obj)
    {       
         
           //strFloat=obj.value.replace(/[\W]/g,"");//规则
           strFloat=obj.value.replace(/[\uFF00-\uFFFF]/g,"").replace(/[\u4E00-\u9FA5]/g,"");//规则
           
           obj.value=strFloat;
             return true;
        //return ((event.keyCode>=48 && event.keyCode<=57) || (event.keyCode>=65 && event.keyCode<=90) || (event.keyCode>=97 && event.keyCode<=122) );
    }
    
    function CheckValueIsDec(obj)
    {
         strFloat=obj.value.replace(/^(\s)*|(\s)*$/g,"");//去掉字符串两边的空格        
		
		var newPar=/^(-|\+)?\d*.?\d+$///验证规则
		
		if(strFloat.length>0 && newPar.test(strFloat)==false)
		{
		    obj.value="";
		    window.alert("只能输入小数。");
		    return false;
		 }
		 else
		 {
		    return true;
		 }
    }

    //检查输入是整数
    function CheckInputIsInt(obj)
    {
        return (event.keyCode>=48 && event.keyCode<=57);
    }
    
    function CheckValueIsInt(obj)
    {
        strInteger=obj.value;//要验证的数值
        strInteger=strInteger.replace(/^(\s)*|(\s)*$/g,"");//去掉字符串两边的空格
        
		//验证规则：整数
		var newPar=/^(-|\+)?\d+$/
		if(strInteger.length>0 && newPar.test(strInteger)==false)
		{		
		   obj.value="";  
		   window.alert("只能输入整数。");		    	  
		   return false;
		 }
		 else
		 {
		    return true;
		 }
    }

    function ReloadOpener()
    {
        if(window.opener.location.href.lastIndexOf("#")==window.opener.location.href.length-1)
        {      
            window.opener.location.href=window.opener.location.href.substr(0,window.opener.location.href.length-1);
        }
        else
        {        
             window.opener.location.href=window.opener.location.href;
        }
    }

    function AddrBook(txtID)
    {
	    str = AddrBookVB('');

	    if (str != "")
	    {		
		    //Return First Email
		    if ( str.indexOf(';') >= 0 )
		    {
			    strA = str.substr(0, str.indexOf(';'));
    			
		    }
		    else
		    {
			    strA = str;
		    }
		    document.getElementById(txtID).value = strA;
		    return true;
	    }
	    else
	    {
		    return false;
	    }

    }
    
    function formatFloat(src, pos)
    {        
        src=String(src).replace(/^(\s)*|(\s)*$/g,"");
        
        if(src=="")
        {
            return "";
        }
        
        if(isFinite(src)==false)
        {
            alert("只能输入数字，请检查！");
            return "";
        }
        
        src=Number(src);
        
        if(Number(src)==0)
        {
            return "";
        }
        
        if(String(src).indexOf(".")==-1)
        {
           return String(src)+".00";
        } 
        else if(String(src).split(".")[1].length==1)
        {
            return String(src)+"0";
        }
        else
        {
            return String(Math.round(Number(src)*Math.pow(10, pos))/Math.pow(10, pos));
        }
     }

    //长度限制
    //使用示例(可以输入100个字符)
    //textBox.Attributes.Add("onKeyDown","javascript:textLimit(this,100);")
    //textBox.Attributes.Add("onKeyUp","javascript:textLimit(this,100);")    
    function textLimit(field, maxlimit)
    {
     if (field.value.length > maxlimit)
      field.value = field.value.substring(0, maxlimit);
    }

    //包括中文字符的长度
    String.prototype.cnlength = function() {
        var cArr = this.match(/[^\x00-\xff]/ig);
        return this.length + (cArr == null ? 0 : cArr.length);
    }

    function showPrintDialog(strUrl)
    {
            var sFeature = "directories=no,status=no,scrollbars=no,resizable=yes,menubar=no"
	        window.open(strUrl,"_blank",sFeature);
    }
    function textLimit(field, maxlimit)

    {

     if (field.value.length > maxlimit)

      field.value = field.value.substring(0, maxlimit);

    }


    function FixWidth(selectObj)
    {
        var newSelectObj = document.createElement("select");
        newSelectObj = selectObj.cloneNode(true);
        newSelectObj.selectedIndex = selectObj.selectedIndex;
        newSelectObj.id="newSelectObj";
      
        var e = selectObj;
        var absTop = e.offsetTop;
        var absLeft = e.offsetLeft;
        while(e = e.offsetParent)
        {
            absTop += e.offsetTop;
            absLeft += e.offsetLeft;
        }
        with (newSelectObj.style)
        {
            position = "absolute";
            top = absTop + "px";
            left = absLeft + "px";
            width = "auto";
        }
       
        var rollback = function(){ RollbackWidth(selectObj, newSelectObj); };
        if(window.addEventListener)
        {
            newSelectObj.addEventListener("blur", rollback, false);
            newSelectObj.addEventListener("change", rollback, false);
        }
        else
        {
            newSelectObj.attachEvent("onblur", rollback);
            newSelectObj.attachEvent("onchange", rollback);
        }
       
        selectObj.style.visibility = "hidden";
        document.body.appendChild(newSelectObj);
       
        var newDiv = document.createElement("div");
        with (newDiv.style)
        {
            position = "absolute";
            top = (absTop-10) + "px";
            left = (absLeft-10) + "px";
            width = newSelectObj.offsetWidth+20;
            height= newSelectObj.offsetHeight+20;;
            background = "transparent";
            //background = "green";
        }
        document.body.appendChild(newDiv);
        newSelectObj.focus();
        var enterSel="false";
        var enter = function(){enterSel=enterSelect();};
        newSelectObj.onmouseover = enter;
       
        var leavDiv="false";
        var leave = function(){leavDiv=leaveNewDiv(selectObj, newSelectObj,newDiv,enterSel);};
        newDiv.onmouseleave = leave;
    }

    function RollbackWidth(selectObj, newSelectObj)
    {
        selectObj.selectedIndex = newSelectObj.selectedIndex;
        selectObj.style.visibility = "visible";
        if(document.getElementById("newSelectObj") != null){
           document.body.removeChild(newSelectObj);
        }
    }

    function removeNewDiv(newDiv)
    {
        document.body.removeChild(newDiv);
    }

    function enterSelect(){
      return "true";
    }

    function leaveNewDiv(selectObj, newSelectObj,newDiv,enterSel){
        if(enterSel == "true" ){
            RollbackWidth(selectObj, newSelectObj);
            removeNewDiv(newDiv);
        }
    }
    
    function FixRightWidth(selectObj)
    {
        var newSelectObj = document.createElement("select");
        newSelectObj = selectObj.cloneNode(true);
        newSelectObj.selectedIndex = selectObj.selectedIndex;
        newSelectObj.id="newSelectObj";
      
        var e = selectObj;
        var absTop = e.offsetTop;
        var absLeft = e.offsetLeft;
        while(e = e.offsetParent)
        {
            absTop += e.offsetTop;
            absLeft += e.offsetLeft;
        }
        with (newSelectObj.style)
        {
            position = "absolute";
            top = absTop + "px";
            left = (absLeft-130) + "px";
            width = "190px";
        }
       
        var rollback = function(){ RollbackWidth(selectObj, newSelectObj); };
        if(window.addEventListener)
        {
            newSelectObj.addEventListener("blur", rollback, false);
            newSelectObj.addEventListener("change", rollback, false);
        }
        else
        {
            newSelectObj.attachEvent("onblur", rollback);
            newSelectObj.attachEvent("onchange", rollback);
        }
       
        selectObj.style.visibility = "hidden";
        document.body.appendChild(newSelectObj);
       
        var newDiv = document.createElement("div");
        with (newDiv.style)
        {
            position = "absolute";
            top = (absTop-10) + "px";
            left = (absLeft-10) + "px";
            width = newSelectObj.offsetWidth+20;
            height= newSelectObj.offsetHeight+20;;
            background = "transparent";
            //background = "green";
        }
        document.body.appendChild(newDiv);
        newSelectObj.focus();
        var enterSel="false";
        var enter = function(){enterSel=enterSelect();};
        newSelectObj.onmouseover = enter;
       
        var leavDiv="false";
        var leave = function(){leavDiv=leaveNewDiv(selectObj, newSelectObj,newDiv,enterSel);};
        newDiv.onmouseleave = leave;
    }    

    function FixRightWidth1(selectObj)
    {
        var newSelectObj = document.createElement("select");
        newSelectObj = selectObj.cloneNode(true);
        newSelectObj.selectedIndex = selectObj.selectedIndex;
        newSelectObj.id="newSelectObj";
      
        var e = selectObj;
        var absTop = e.offsetTop;
        var absLeft = e.offsetLeft;
        while(e = e.offsetParent)
        {
            absTop += e.offsetTop;
            absLeft += e.offsetLeft;
        }
        with (newSelectObj.style)
        {
            position = "absolute";
            top = absTop + "px";
            left = (absLeft-50) + "px";
            width = "200px";
        }
       
        var rollback = function(){ RollbackWidth(selectObj, newSelectObj); };
        if(window.addEventListener)
        {
            newSelectObj.addEventListener("blur", rollback, false);
            newSelectObj.addEventListener("change", rollback, false);
        }
        else
        {
            newSelectObj.attachEvent("onblur", rollback);
            newSelectObj.attachEvent("onchange", rollback);
        }
       
        selectObj.style.visibility = "hidden";
        document.body.appendChild(newSelectObj);
       
        var newDiv = document.createElement("div");
        with (newDiv.style)
        {
            position = "absolute";
            top = (absTop-10) + "px";
            left = (absLeft-10) + "px";
            width = newSelectObj.offsetWidth+20;
            height= newSelectObj.offsetHeight+20;;
            background = "transparent";
            //background = "green";
        }
        document.body.appendChild(newDiv);
        newSelectObj.focus();
        var enterSel="false";
        var enter = function(){enterSel=enterSelect();};
        newSelectObj.onmouseover = enter;
       
        var leavDiv="false";
        var leave = function(){leavDiv=leaveNewDiv(selectObj, newSelectObj,newDiv,enterSel);};
        newDiv.onmouseleave = leave;
    }    
    
    /*    
    *    ForDight(Dight,How):数值格式化函数，Dight要    
    *    格式化的  数字，How要保留的小数位数。    
    */     
    function  ForDight(Dight,How)     
    {     
       Dight  =  Math.round  (Dight*Math.pow(10,How))/Math.pow(10,How);     
       return  Dight;     
    }
   
  /*
  *   自动搜索代码块
  *
  */  
  
  
   //截取后台的数据 并转化为数组
function searchSuggest(txtSearch,search_suggest,sel1,hi,hiValue,hiText) { 
  var y=-1;  
  var inputField=txtSearch;
   if(inputField.value.replace(/(\s*$)/g,   "").length>0&&txtSearch.value.replace(/(^\s*)|(\s*$)/g,"")!="全部/All")
  {
   var a= hi.value;
   var  bool=false;
   var   newArr=new   Array() 
   arr = a.split(",");
   for(var i=0;i<arr.length;i++) //把字符串转化成为数组，如果数组中有与textbox中相近的词则转化到新数组中，并且显示。
    {
          var textFiled=arr[i].substring(arr[i].indexOf("-")+1,arr[i].length);
          var textValue=arr[i].substring(0,arr[i].indexOf("-")); 
          if (inputField.value.replace(/(\s*$)/g,   "")==textFiled.substring(0,inputField.value.length).replace(/(\s*$)/g,   ""))
          {
              bool=true;
               y++;
              newArr[y]=arr[i];    
          } 
    }
    showSearch(newArr,bool,search_suggest,sel1,hi);//调用显示select方法
  } 
  else
  {    if(event.keyCode!=40)
        {
        search_suggest.style.display="none";
        }
  } 
} 
//给隐藏表单域中的select赋值
function showSearch(arr,bool,search_suggest,sel1,hi)
{
   var sel=sel1; 
    clearAll(sel1);  //清空所有的options
   search_suggest.style.display="block"; 
  
   if(bool)//如果textbox中有相近的值 则查找并显示出
 {  
        for(var i=0;i<arr.length;i++)
        {  
            var textFiled=arr[i].substring(arr[i].indexOf("-")+1,arr[i].length);
            var textValue=arr[i].substring(0,arr[i].indexOf("-"));
            sel.options.add(new Option(textFiled,textValue));  
         } 
   } 
 
}


//下拉按钮显示全部
function  showAll(txtSearch,search_suggest,sel1,hi,hiValue,hiText,btn)
{ 
   if(search_suggest.style.display=="none")
        search_suggest.style.display="block";
   else
  { 
        search_suggest.style.display="none"; 
  }     
  var y=-1;  
   var  a= hi.value;
   var sel=sel1; 
   var inputField=txtSearch;
  var   arr=new   Array() ;
   arr = a.split(","); 
   clearAll(sel1);
  for(var i=0;i<arr.length;i++)
      {                
          var textFiled=arr[i].substring(arr[i].indexOf("-")+1,arr[i].length);
          var textValue=arr[i].substring(0,arr[i].indexOf("-"));
          
         if(i!=arr.length-1)
         {
          sel.options.add(new Option(textFiled,textValue));  
          } 
      }  
       for(var i=0;i<arr.length;i++)
      {                
          var textFiled=arr[i].substring(arr[i].indexOf("-")+1,arr[i].length);
          var textValue=arr[i].substring(0,arr[i].indexOf("-"));
          if(txtSearch.value==textFiled&&txtSearch.value!="") 
          {
                  sel.options[i].selected=true; 
                  sel.focus();
                  break;
          }
          else
          {
                 sel.options[0].selected=true; 
                 sel.focus(); 
          }
      }  
}
//清空options
function clearAll(sel1)
{
    var sel=sel1;  
    if(sel.options.length>0)
    {
         for(var i=sel.options.length-1;i>=0;i--) 
         sel.options.remove(i); 
    }    
} 

//按向下的箭头实现选择
function downChoose(sel1,txtSearch,hi,search_suggest)
{
    if(event.keyCode==40&&txtSearch.value.replace(/(^\s*)|(\s*$)/g,"")!="全部/All"&&sel1.length>0)
  { 
       var sel=sel1;
        if(!document.all)
       {
        sel.options[0].selected=true; 
        sel.focus();
       }
       else
       {
       sel.focus();
       }
   }
   
 }
//当修改文本框的时候检查文本值是否同选定值相同
function focusChick(txtSearch,search_suggest,sel1,hi,hiText1,hiValue)
{
   var inputField=txtSearch;
   var  hiText=hiText1;
   hiText.value="error"; 
   var hiValue=hiValue;
   var a= hi.value;
   var  bool=false;
   var   newArr=new   Array() 
   arr = a.split(",");
   for(var i=0;i<arr.length;i++) //把字符串转化成为数组，如果数组中有与textbox中相近的词则转化到新数组中，并且显示。
   {
          var textFiled=arr[i].substring(arr[i].indexOf("-")+1,arr[i].length);
          var textValue=arr[i].substring(0,arr[i].indexOf("-")); 
          if (inputField.value.replace(/(\s*$)/g,   "")==textFiled&&inputField.value!="")
          {
            hiText.value=inputField.value;
            hiValue.value=textValue;
          }
    } 
   if(sel1.length==0)
  {
    search_suggest.style.display="none";
  }  
} 

//按回车给textbox赋值
function choose(theValue,txtSearch,search_suggest,sel1,hiValue1,hiText)
{
  var inputField=txtSearch;
  var sel=sel1; 
  var hiValue=  hiValue1;
    if(event.keyCode==13) 
     { 
      inputField.value=sel.options[sel.selectedIndex].innerText ;
      hiValue.value=theValue;
      search_suggest.style.display="none"; 
      hiText.value= inputField.value ;
     }
}

//双击将选中text赋给文本框
function selClick(theValue,txtSearch,search_suggest,sel1,hi,hiValue1)
{
   var inputField=txtSearch;
   var a= hi.value;
   var  hiValue=hiValue1;
   var   arr=new   Array() 
           arr = a.split(",");
    for(var i=0;i<arr.length;i++) //把字符串转化成为数组，如果数组中有与textbox中相近的词则转化到新数组中，并且显示。
    {
          var textFiled=arr[i].substring(arr[i].indexOf("-")+1,arr[i].length);
          var textValue=arr[i].substring(0,arr[i].indexOf("-")); 
          if (theValue==textValue)
          {
             inputField.value=textFiled;
             hiValue.value=theValue;
          } 
    } 
   inputField.focus();
   search_suggest.style.display="none"; 
}
//当按向上或者向下箭头的时候给文本框赋值
function keyUp(txtSearch,sel1)
{
    if(event.keyCode!=13)
   { 
       var inputField=txtSearch;
       var sel=sel1;
       inputField.value= sel.options[sel.selectedIndex].innerText ;
    } 
  
}

//当鼠标点击别处的时候，层隐藏
 function selBlur(search_suggest,theValue,hiValue,hiText,txtSearch)
{
    hiValue.value=theValue;
    hiText.value= txtSearch.value;
    search_suggest.style.display="none"; 
}   