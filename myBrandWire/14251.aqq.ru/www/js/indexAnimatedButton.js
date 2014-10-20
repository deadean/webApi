$('.mainFacebook').on("click", function(){
  $('.mainFacebook').toggleClass('activeLOGIC');
  $('.innerFacebook').toggleClass('activeLOGIC1');
  if(document.getElementById('idFacebook').getAttribute('class').indexOf('activeLOGIC1')==-1){
		document.getElementById('idFacebook').style.display = 'none';
		SetUnivaersalJSONAjax(
			{ 
				name : document.getElementById('idFacebookUser').value,
				idCompany : document.getElementById('idCompany').value,
				type : "facebook"
			}
			,"","../ajaxPages/crud.php","updateCompanySocialContacts");
  }
});

$('.innerFacebook').on("click",function(){
  $('.innerFacebook').toggleClass('activeLOGIC1');
  $('.mainFacebook').toggleClass('activeLOGIC');
});

$(".mainFacebook").bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function()
{ 
	if(document.getElementById('idFacebook').getAttribute('class').indexOf('activeLOGIC1')!=-1){
		document.getElementById('idFacebook').style.display = 'block';
	} 
});






$('.mainGooglePlus').click(function(){
  $('.mainGooglePlus').toggleClass('activeLOGIC');
  $('.innerGooglePlus').toggleClass('activeLOGIC1');
  if(document.getElementById('idGooglePlus').getAttribute('class').indexOf('activeLOGIC1')==-1){
		document.getElementById('idGooglePlus').style.display = 'none';
		SetUnivaersalJSONAjax(
			{ 
				name : document.getElementById('idGooglePlusUser').value,
				idCompany : document.getElementById('idCompany').value,
				type : "googleplus"
			}
			,"","../ajaxPages/crud.php","updateCompanySocialContacts");
  }
});

$('.innerGooglePlus').click(function(){
  $('.innerGooglePlus').toggleClass('activeLOGIC1');
  $('.mainGooglePlus').toggleClass('activeLOGIC');
});

$(".mainGooglePlus").bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function()
{ 
	if(document.getElementById('idGooglePlus').getAttribute('class').indexOf('activeLOGIC1')!=-1){
		document.getElementById('idGooglePlus').style.display = 'block';
	} 
});




$('.mainVk').click(function(){
  $('.mainVk').toggleClass('activeLOGIC');
  $('.innerVk').toggleClass('activeLOGIC1');
  if(document.getElementById('idVk').getAttribute('class').indexOf('activeLOGIC1')==-1){
		document.getElementById('idVk').style.display = 'none';
		SetUnivaersalJSONAjax(
			{ 
				name : document.getElementById('idVkUser').value,
				idCompany : document.getElementById('idCompany').value,
				type : "vkontakte"
			}
			,"","../ajaxPages/crud.php","updateCompanySocialContacts");
  }
});

$('.innerVk').click(function(){
  $('.innerVk').toggleClass('activeLOGIC1');
  $('.mainVk').toggleClass('activeLOGIC');
});

$(".mainVk").bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function()
{ 
	if(document.getElementById('idVk').getAttribute('class').indexOf('activeLOGIC1')!=-1){
		document.getElementById('idVk').style.display = 'block';
	} 
});


/*================================================================================================*/
$('.mainOd').click(function(){
    $('.mainOd').toggleClass('activeLOGIC');
    $('.innerOd').toggleClass('activeLOGIC1');
    if(document.getElementById('idOd').getAttribute('class').indexOf('activeLOGIC1')==-1){
        document.getElementById('idOd').style.display = 'none';
        SetUnivaersalJSONAjax(
            {
                name : document.getElementById('idOdUser').value,
                idCompany : document.getElementById('idCompany').value,
                type : "odnoklassniki"
            }
            ,"","../ajaxPages/crud.php","updateCompanySocialContacts");
    }
});

$('.innerOd').click(function(){
    $('.innerOd').toggleClass('activeLOGIC1');
    $('.mainOd').toggleClass('activeLOGIC');
});

$(".mainOd").bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function()
{
    if(document.getElementById('idOd').getAttribute('class').indexOf('activeLOGIC1')!=-1){
        document.getElementById('idOd').style.display = 'block';
    }
});

/*================================================================================================*/
$('.mainInsta').click(function(){
    $('.mainInsta').toggleClass('activeLOGIC');
    $('.innerInsta').toggleClass('activeLOGIC1');
    if(document.getElementById('idInsta').getAttribute('class').indexOf('activeLOGIC1')==-1){
        document.getElementById('idInsta').style.display = 'none';
        SetUnivaersalJSONAjax(
            {
                name : document.getElementById('idInstaUser').value,
                idCompany : document.getElementById('idCompany').value,
                type : "instagramm"
            }
            ,"","../ajaxPages/crud.php","updateCompanySocialContacts");
    }
});

$('.innerInsta').click(function(){
    $('.innerInsta').toggleClass('activeLOGIC1');
    $('.mainInsta').toggleClass('activeLOGIC');
});

$(".mainInsta").bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function()
{
    if(document.getElementById('idInsta').getAttribute('class').indexOf('activeLOGIC1')!=-1){
        document.getElementById('idInsta').style.display = 'block';
    }
});

/*================================================================================================*/
$('.mainTwitter').click(function(){
    $('.mainTwitter').toggleClass('activeLOGIC');
    $('.innerTwitter').toggleClass('activeLOGIC1');
    if(document.getElementById('idTwitter').getAttribute('class').indexOf('activeLOGIC1')==-1){
        document.getElementById('idTwitter').style.display = 'none';
        SetUnivaersalJSONAjax(
            {
                name : document.getElementById('idTwitterUser').value,
                idCompany : document.getElementById('idCompany').value,
                type : "twitter"
            }
            ,"","../ajaxPages/crud.php","updateCompanySocialContacts");
    }
});

$('.innerTwitter').click(function(){
    $('.innerTwitter').toggleClass('activeLOGIC1');
    $('.mainTwitter').toggleClass('activeLOGIC');
});

$(".mainTwitter").bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function()
{
    if(document.getElementById('idTwitter').getAttribute('class').indexOf('activeLOGIC1')!=-1){
        document.getElementById('idTwitter').style.display = 'block';
    }
});
/*================================================================================================*/
$('.mainPinterest').click(function(){
    $('.mainPinterest').toggleClass('activeLOGIC');
    $('.innerPinterest').toggleClass('activeLOGIC1');
    if(document.getElementById('idPinterest').getAttribute('class').indexOf('activeLOGIC1')==-1){
        document.getElementById('idPinterest').style.display = 'none';
        SetUnivaersalJSONAjax(
            {
                name : document.getElementById('idPinterestUser').value,
                idCompany : document.getElementById('idCompany').value,
                type : "Pinterest"
            }
            ,"","../ajaxPages/crud.php","updateCompanySocialContacts");
    }
});

$('.innerPinterest').click(function(){
    $('.innerPinterest').toggleClass('activeLOGIC1');
    $('.mainPinterest').toggleClass('activeLOGIC');
});

$(".mainPinterest").bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function()
{
    if(document.getElementById('idPinterest').getAttribute('class').indexOf('activeLOGIC1')!=-1){
        document.getElementById('idPinterest').style.display = 'block';
    }
});

/*================================================================================================*/
$('.mainTumblr').click(function(){
    $('.mainTumblr').toggleClass('activeLOGIC');
    $('.innerTumblr').toggleClass('activeLOGIC1');
    if(document.getElementById('idTumblr').getAttribute('class').indexOf('activeLOGIC1')==-1){
        document.getElementById('idTumblr').style.display = 'none';
        SetUnivaersalJSONAjax(
            {
                name : document.getElementById('idTumblrUser').value,
                idCompany : document.getElementById('idCompany').value,
                type : "Tumblr"
            }
            ,"","../ajaxPages/crud.php","updateCompanySocialContacts");
    }
});

$('.innerTumblr').click(function(){
    $('.innerTumblr').toggleClass('activeLOGIC1');
    $('.mainTumblr').toggleClass('activeLOGIC');
});

$(".mainTumblr").bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function()
{
    if(document.getElementById('idTumblr').getAttribute('class').indexOf('activeLOGIC1')!=-1){
        document.getElementById('idTumblr').style.display = 'block';
    }
});

/*================================================================================================*/
$('.mainLinkedin').click(function(){
    $('.mainLinkedin').toggleClass('activeLOGIC');
    $('.innerLinkedin').toggleClass('activeLOGIC1');
    if(document.getElementById('idLinkedin').getAttribute('class').indexOf('activeLOGIC1')==-1){
        document.getElementById('idLinkedin').style.display = 'none';
        SetUnivaersalJSONAjax(
            {
                name : document.getElementById('idLinkedinUser').value,
                idCompany : document.getElementById('idCompany').value,
                type : "Linkedin"
            }
            ,"","../ajaxPages/crud.php","updateCompanySocialContacts");
    }
});

$('.innerLinkedin').click(function(){
    $('.innerLinkedin').toggleClass('activeLOGIC1');
    $('.mainLinkedin').toggleClass('activeLOGIC');
});

$(".mainLinkedin").bind("transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd", function()
{
    if(document.getElementById('idLinkedin').getAttribute('class').indexOf('activeLOGIC1')!=-1){
        document.getElementById('idLinkedin').style.display = 'block';
    }
});


