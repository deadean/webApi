function loadScript(url)
{
    var head = document.getElementsByTagName('head')[0];
    var script = document.createElement('script');
    script.type = 'text/javascript';
    script.src = url;
    //var elements = document.getElementsByTagName( 'script' );
    //for (var i = 0; i <elements.length; i++) {
   		//if(elements[i].src.contains(url))
   			//head.removeChild(elements[i]);
	//}
	
    head.appendChild(script);
    console.log(head);
}
function ChangeSlideDownAnimation(idElement,classElement){
	id = '#'.concat(idElement);
    InvokeChangeSlideDownAnimation(idElement,id,classElement);
}

function InvokeChangeSlideDownAnimation(idElement, id, classElement){
    if(document.getElementById(idElement).style.display=="none"){
        $(id.concat(classElement));
        $(id).slideDown(500);
    }
    else{
        $(id.concat(classElement));
        $(id).slideUp(500);
    }
}
function SetPressRealizeContentVisible(a){
	//alert("t");
	document.getElementById("ifrContent").src = "mainController.php?type="+a;
	SetCurrentOperation(a);
}
function BlockSetCheckBox(id){
	document.getElementById(id).checked = !document.getElementById(id).checked; 
}
function SetBlockEditorStatus(idEditor,element){
	
	if(element.checked == true){
		SetBlockElementById(idEditor);
	}
	else{
		SetNoneBlockElementById(idEditor);
	}
}
function PauseSlide(){
	$(function () {
	    $('#slideshow').cycle('pause',{
	        timeout: 1000,
	        fx: 'fade',
	        pager: '#pager',
	        pause: 0,
	        pauseOnPagerHover: 3
	    });
	});
}
function ResumeSlide(){
	$(function () {
	    $('#slideshow').cycle('resume',{
	        timeout: 1000,
	        fx: 'fade',
	        pager: '#pager',
	        pause: 0,
	        pauseOnPagerHover: 3
	    });
	});
}
function ChangeVisibleStatusBySlideShow(id){
	//alert("d");
	SetNoneBlockElementById('slide1');
	SetZIndex('slide1',4);
	SetOpacity('slide1',0);
	SetImgSrc('slide1Img','images/shape_6_copy.png');
	SetNoneBlockElementById('slide2');
	SetZIndex('slide2',4);
	SetOpacity('slide2',0);
	SetImgSrc('slide2Img','images/shape_6_copy.png');
	SetNoneBlockElementById('slide3');
	SetZIndex('slide3',4);
	SetOpacity('slide3',0);
	SetImgSrc('slide3Img','images/shape_6_copy.png');
	SetNoneBlockElementById('slide4');
	SetZIndex('slide4',4);
	SetOpacity('slide4',0);
	SetImgSrc('slide4Img','images/shape_6_copy.png');
	SetBlockElementById(id);
	SetZIndex(id,5);
	SetOpacity(id,1);
	SetImgSrc(id+"Img",'images/shape_6_copy_4.png');
}
function SetImgSrc(id,srcPath){
	document.getElementById(id).src=srcPath;
}
function SetZIndex(id,index){
	document.getElementById(id).style.zIndex=index;
}
function SetOpacity(id,index){
	document.getElementById(id).style.opacity=index;
}
function ChangeVisibleStatus(id){
	//alert(id);
	var status = document.getElementById(id).style.display;
	//alert(status);
	
	if(status=='block')
		status='none';
	else
		status='block';
	
	
	document.getElementById(id).style.display = status;
}
function SetOpacityElementById(id,value){
	document.getElementById(id).style.opacity = value;
	window.scrollTo(0,0);
}
function SetBlockElementById(id){
	document.getElementById(id).style.display = 'block';
}
function SetNoneBlockElementById(id){
	document.getElementById(id).style.display = 'none';
}
function ShowToolTip(a){
	var elems = document.getElementsByClassName('test');
	for (i in elems){
		var id = elems[i].getAttribute('id');
		if(id.indexOf("newsInfo")!=-1 && id.indexOf(a)!=-1){
			ChangeVisibleStatus(id);
		}
	}
}
function SetCurrentOperation(type){
	if(type==1)
		document.getElementById("hCenterIndexCurrentFunction").innerHTML = "Написание пресс-релиза";
	if(type==2)
		document.getElementById("hCenterIndexCurrentFunction").innerHTML = "Публикация пресс-релиза";
	if(type==3)
		document.getElementById("hCenterIndexCurrentFunction").innerHTML = "Распространение по базам СМИ";
	if(type==4)
		document.getElementById("hCenterIndexCurrentFunction").innerHTML = "Адаптация новости";
	if(type==5)
		document.getElementById("hCenterIndexCurrentFunction").innerHTML = "Test function";
}
function test(){
	alert("Hello");
}
function SetTextByElement(element, content){
	document.getElementById(element).innerHTML = content;
}

function LoadImages(companyId) {
    console.log("RELOAD IMAGE SRC FROM PATH :"+companyId);
	//alert(companyId);
	searchPic = new Image(100,100);
    
    //var path = "../uploads/id_";
    //path=path.concat(companyId);
    //path=path.concat("_.jpg");
    //alert(path);
    //searchPic.src = path;

    searchPic.src = companyId;
    
    document.getElementById("imgCompanyLogo").src=searchPic.src;
}

function AnaliseAndSetElementsVisisbilityNewsAdaptee(userLogin, userId,isPayment,idsCategory,namesCategory){
	//alert(userId);
	//alert(userLogin);
	//alert(isPayment);
	//alert(idsCategory);
	if(!userLogin){
		SetNoneBlockElementById("blockLogin");
		SetBlockElementById("blockRegister");
	}
	else{
		var els = document.getElementsByName("companyId");
		if(els.length==1){
			for (var i=0; i < els.length; i++) {
			  els[i].checked = true;
			  break;
			};
		}
        SetNoneBlockElementById("mce_24");
        SetNoneBlockElementById("mce_30");
        SetNoneBlockElementById("mce_32");
        SetNoneBlockElementById("mce_15");
        SetNoneBlockElementById("mce_16");
		if(isPayment==0){
			SetNoneBlockElementById("mce_31-body");
			SetNoneBlockElementById("mce_23-open");
			document.getElementById("checkboxPublish").click();
			document.getElementById("checkboxPublish").click();
		}
		else{
			document.getElementById("checkboxPublish").click();
			document.getElementById('checkboxPublish').checked='true';
		}
		
		var ids = idsCategory.split(";");
		var names = namesCategory.split(";");
		var namesPrint = "";
		for (var i=0; i < ids.length; i++) {
			namesPrint=namesPrint+names[i]+",";	
		}
		namesPrint = namesPrint.substring(0, namesPrint.length - 2);
		document.getElementsByClassName('ui-dropdownchecklist-text')[0].innerHTML = namesPrint ;
		document.getElementById('returnS7').value = idsCategory;
		
		for (var i=0; i < ids.length; i++) {
		  var el = 'ddcl-s6-i'.concat(ids[i]-1);
		  document.getElementById(el).checked = true;
		};
		
		LoadImages(userId);
		
		document.getElementById("returnS7").value = "";
		SetNoneBlockElementById("blockRegister");
		SetBlockElementById("blockLogin");
	}
}
function AnaliseAndSetElementsVisisbility(userLogin, userId){
	//alert(userId);
	//alert(userLogin);
	if(!userLogin){
		SetNoneBlockElementById("blockLogin");
		SetBlockElementById("blockRegister");
	}
	else{
		var els = document.getElementsByName("companyId");
		if(els.length==1){
			for (var i=0; i < els.length; i++) {
			  els[i].checked = true;
			  break;
			};
		}
		document.getElementById("returnS7").value = "";
		SetNoneBlockElementById("blockRegister");
		SetBlockElementById("blockLogin");
	}
}
function HideElements(){
	SetNoneBlockElementById("mce_7");
}
function SetCompany(idCompany){
	if (window.XMLHttpRequest)
	{
	  xmlhttp=new XMLHttpRequest();
	}
	else
  	{
  		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  	}
  	
	xmlhttp.onreadystatechange=function()
  	{
  		if (xmlhttp.readyState==4 && xmlhttp.status==200)
    	{
    		document.getElementById("companyTabId").innerHTML=xmlhttp.responseText;
    		loadScript("../js/indexAnimatedButton.js");
            loadScript("../lib/jq/1.10.2/jquery-1.10.2.min.js");
            loadScript("../lib/jq/1.10.2/jquery.form.min.js");
            loadScript("../lib/knockouts/knockout-3.1.0.js");
            loadScript("ViewModels/CompanyTabVm.js");
    		//LoadImages(idCompany);
    	}
  	}
	
	xmlhttp.open("POST","companyTab.php?idCompany="+idCompany,true);
	xmlhttp.send();
	//loadScript("../js/indexAnimatedButton.js");
}

function SetCompany1(idCompany, logo){
    if (window.XMLHttpRequest)
    {
        xmlhttp=new XMLHttpRequest();
    }
    else
    {
        xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
    }

    xmlhttp.onreadystatechange=function()
    {
        if (xmlhttp.readyState==4 && xmlhttp.status==200)
        {
            document.getElementById("companyTabId").innerHTML=xmlhttp.responseText;
            loadScript("../js/indexAnimatedButton.js");
            loadScript("../lib/jq/1.10.2/jquery-1.10.2.min.js");
            loadScript("../lib/jq/1.10.2/jquery.form.min.js");
            loadScript("../lib/knockouts/knockout-3.1.0.js");
            loadScript("ViewModels/CompanyTabVm.js");
            LoadImages(logo);
        }
    }

    xmlhttp.open("POST","companyTab.php?idCompany="+idCompany,true);
    xmlhttp.send();
    //loadScript("../js/indexAnimatedButton.js");
}
function SetCompanyNews(idCompany){
	if (window.XMLHttpRequest)
	{
	  xmlhttp=new XMLHttpRequest();
	}
	else
  	{
  		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  	}
  	xmlhttp.onreadystatechange=function()
  	{
  		if (xmlhttp.readyState==4 && xmlhttp.status==200)
    	{
    		document.getElementById("companyNewsContainer1").innerHTML=xmlhttp.responseText;
    	}
  	}
	xmlhttp.open("POST","newslist.php?idCompany="+idCompany,true);
	xmlhttp.send();
}
function SetNULL(){
	if (window.XMLHttpRequest)
	{
	  xmlhttp=new XMLHttpRequest();
	}
	else
  	{
  		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  	}
  	xmlhttp.onreadystatechange=function()
  	{
  		if (xmlhttp.readyState==4 && xmlhttp.status==200)
    	{
    		
    		document.getElementById("companyNewsContainer1").innerHTML=xmlhttp.responseText;
    	}
  	}
	xmlhttp.open("POST","test.php",true);
	xmlhttp.send();
}
function SetUnivaersalAjax(param,idElement,pathFile){
	//alert(param);
	//alert(pathFile);
	//alert(idElement);
	if (window.XMLHttpRequest)
	{
	  xmlhttp=new XMLHttpRequest();
	}
	else
  	{
  		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  	}
  	xmlhttp.onreadystatechange=function()
  	{
  		if (xmlhttp.readyState==4 && xmlhttp.status==200)
    	{
    		//alert(idElement);	
    		document.getElementById(idElement).innerHTML=xmlhttp.responseText;
    	}
  	}
	xmlhttp.open("POST",pathFile+"?param="+param,true);
	xmlhttp.send();
}
function SetUnivaersalCRUDAjax(param,idElement,pathFile,action){
	if (window.XMLHttpRequest)
	{
	  xmlhttp=new XMLHttpRequest();
	}
	else
  	{
  		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  	}
  	xmlhttp.onreadystatechange=function()
  	{
  		if (xmlhttp.readyState==4 && xmlhttp.status==200)
    	{
    		var destinationElement = document.getElementById(idElement);
    		if(destinationElement == null)
    			return;
    		destinationElement.innerHTML=xmlhttp.responseText;
    	}
  	}
  	paramString = GetParamStringByAction(param,action);
	xmlhttp.open("POST",pathFile+"?param="+paramString+"&action="+action,true);
	xmlhttp.send();
	//loadScript("../js/indexAnimatedButton.js");
}
function SetUnivaersalJSONAjax(param,idElement,pathFile,action){
	//alert(param);
	if (window.XMLHttpRequest)
	{
	  xmlhttp=new XMLHttpRequest();
	}
	else
  	{
  		xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
  	}
  	xmlhttp.onreadystatechange=function()
  	{
  		if (xmlhttp.readyState==4 && xmlhttp.status==200)
    	{
    		var destinationElement = document.getElementById(idElement);
    		if(destinationElement == null)
    			return;
    		destinationElement.innerHTML=xmlhttp.responseText;
    		
    	}
  	}
	xmlhttp.open("POST",pathFile+"?param="+param+"&action="+action,true);
	xmlhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded");
	xmlhttp.send(GetParamStringByAction(param,action));
}

function SetUnivaersalJSONAjax2(param,idElement,pathFile,action, onsuccess){
    //alert(param);
    if (window.XMLHttpRequest)
    {
        xmlhttp=new XMLHttpRequest();
    }
    else
    {
        xmlhttp=new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.onreadystatechange=function()
    {
        if (xmlhttp.readyState==4 && xmlhttp.status==200)
        {
            var destinationElement = document.getElementById(idElement);
            if(destinationElement == null)
                return;
            destinationElement.innerHTML=xmlhttp.responseText;
            onsuccess();

        }
    }
    xmlhttp.open("POST",pathFile+"?param="+param+"&action="+action,true);
    xmlhttp.setRequestHeader("Content-type","application/x-www-form-urlencoded");
    xmlhttp.send(GetParamStringByAction(param,action));
}

function SetUnivaersalJSONAjax3(controller, method, data, handler)
{
    req = $.ajax({
        //url: url1 + '/' + controller + '/' + method,
        url:controller+'?action='+method,
        data: data,
        datatype:"json",
        type:"post"
    });

    req.done(function(data) {
        console.log("Output SetUnivaersalJSONAjax3: "+req.responseText);
        var objData = $.parseJSON(req.responseText);
        handler(data);
    });
};

function GetParamStringByAction(param,action){
	//alert(action);
	if(action=="publishNews"){
		return param;
	}
	if(action=="updateCompanySocialContacts"){
		var res="";
		res = "idCompany=".concat(param.idCompany).concat("&");
		res = res.concat("name=").concat(param.name).concat("&");
		res = res.concat("type=").concat(param.type);
		return res;
	}
    if(action=="PreviewOrder"){
        var res="";
        res = "mode=".concat(param.mode).concat("&");
        res = res.concat("newsHeader=").concat(param.newsHeader).concat("&");
        res = res.concat("newsContent=").concat(param.newsContent).concat("&");
        res = res.concat("newsCommon=").concat(param.newsCommon).concat("&");
        res = res.concat("newsTags=").concat(param.newsTags).concat("&");
        res = res.concat("newsComments=").concat(param.newsComments).concat("&");
        res = res.concat("checkboxAddCompanySocialNetworks=").concat(param.checkboxAddCompanySocialNetworks).concat("&");
        res = res.concat("contacts=").concat(param.contacts).concat("&");
        res = res.concat("checkboxSocialNetworks6=").concat(param.checkboxSocialNetworks6).concat("&");
        res = res.concat("checkboxSocialNetworks7=").concat(param.checkboxSocialNetworks7).concat("&");
        res = res.concat("RES=").concat(param.RES).concat("&");
        res = res.concat("companyId=").concat(param.companyId).concat("&");
        res = res.concat("checkboxPublish=").concat(param.checkboxPublish).concat("&");
        res = res.concat("id=").concat(param.id);
        return res;
    }
	if(action=="find"){
		var res="";
		res = "place=".concat(param.place).concat("&");
		res = res.concat("value=").concat(param.value);
		return res;
	}
	if(action=="addSocialNetworksToCompany"){
		var res="";
		res = "ids=".concat(param.idsParam).concat("&");
		res = res.concat("idCompany=").concat(param.idCompany);
		return res;
	}
	
	if(action=="showMainNewsPortionSize"){
		var res="";
		res = "idCategory=".concat(param.idCategory).concat("&");
		res = res.concat("portionSize=").concat(param.portionSize);
		return res;
	}
	if(action=="showFreeNewsPortionSize"){
		var res="";
		res = "idCategory=".concat(param.idCategory).concat("&");
		res = res.concat("portionSize=").concat(param.portionSize);
		return res;
	}
	if(action=="removeNews"){
		return param;
	}
	if(action=="createContactByCompany"){
		var res="";
		res = "idCompany=".concat(param.idCompany).concat("&");
		res = res.concat("name=").concat(param.name).concat("&");
		res = res.concat("phone=").concat(param.phone).concat("&");
		res = res.concat("email=").concat(param.email).concat("&");
		res = res.concat("skype=").concat(param.skype).concat("&");
		res = res.concat("idContact=").concat(param.idContact).concat("&");
		res = res.concat("position=").concat(param.position);
		return res;
	}
	if(action=="createAddressByCompany"){
		var res="";
		res = "idCompany=".concat(param.idCompany).concat("&");
        res = res.concat("companyName=").concat(param.companyName).concat("&");
		res = res.concat("companyRegion=").concat(param.companyRegion).concat("&");
		res = res.concat("companyCommunity=").concat(param.companyCommunity).concat("&");
		res = res.concat("companyPhone=").concat(param.companyPhone).concat("&");
		res = res.concat("companyEmail=").concat(param.companyEmail).concat("&");
		res = res.concat("companySkype=").concat(param.companySkype);
		return res;
	}
	
	if(action=="deleteContactByCompany"){
		var res="";
		res = "idCompany=".concat(param.idCompany).concat("&");
		res = res.concat("idContact=").concat(param.idContact);
		return res;
	}
	
	if(action=="showCompanyNews"){
		var res="";
		res = "idCompany=".concat(param.idCompany).concat("&");
		res = res.concat("portionSize=").concat(param.portionSize);
		return res;
	}
    if(action=="checkCompanyInfo"){
        var res="";
        res = "selectedCompanyId=".concat(param.selectedCompanyId).concat("&");
        res = res.concat("type=").concat(param.action);
        return res;
    }
}
function SetAjaxRequest1(action,param,idElement,pathFile)
{
	if(action=='showCommunities'){
		SetUnivaersalAjax(param,idElement,pathFile);
	}
}
function SetAjaxRequest(action, param){
	//alert(action);
	//alert(param);
	if(action=='showCompany'){
		SetCompany(param);
		
	}
	
	
	if(action=='showCompanyNews'){
		SetCompanyNews(param);
	}
	if(action=='setNULL'){
		SetNULL();
	}
	
	//loadScript("../js/indexAnimatedButton.js");
}
