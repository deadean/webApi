<?php
/**
 * Created by PhpStorm.
 * User: dean
 * Date: 07.06.14
 * Time: 17:59
 */

?>

<div>

</div>
<div class="commonTextFont107" style="margin: 0 auto;width: 100%;line-height: 2.9" align="left">

	<div class="container" data-bind="visible:isEdit">
		<div style="padding-left: 15">Введено </div>
		<div class="" style="width: 25;padding-left: 2; padding-right: 1" id="current" data-bind="text:aboutCount"></div>
		<div>из рекумендуемых 150</div>
	</div>

    <div id="idBtnEditCompanyAbout" class="btnEdit clickable " style="float: left;display:block"
		 data-bind="click:SetEdit"
         onclick="
         ChangeVisibleStatus('idBtnSaveCompanyAbout');
         ChangeVisibleStatus('idBtnEditCompanyAbout');
        $('.'.concat('name-help')).slideUp(500);
        $('.'.concat('name-helpMain')).slideDown(500);
        ChangeSlideDownAnimation('idInputCompanyAbout','name-help');
        ChangeSlideDownAnimation('idCompanyAbout','name-help')">

    </div>

    <div id="idBtnSaveCompanyAbout" class="btnSave clickable " style="float: left;display: none" data-bind="click:Save">
    </div>

	<input id="idInputCompanyAbout" data-bind="value: about, valueUpdate: 'input'" class="wordwrap"
		   style="display: none;width: 700;height: 40; overflow-x: scroll" />
    <div id="idCompanyAbout" data-bind="text: about" class="wordwrap" style="line-height: 1.5"  />
</div>

