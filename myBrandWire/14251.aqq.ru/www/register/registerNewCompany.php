<?php
$countries = $controller -> GetCountries();
?>
<!--<script src="../js/globalJQ.js"></script>-->
<p class=" shag_2_informaciya_o_komp">
	<strong>Информация о компании</strong>
</p>
<p class=" profil_kompanii_budet_vid">
	Профиль компании будет виден всем пользователям
</p>

<style>
	.leftHeader {
		width: 200;
	}
	.rightHeader {
		width: 350;
	}
	.verticalDiff {
		margin-top: 10;
	}
</style>

<div class=" blockRelative" style="background:  url('images/layer_2_Company.jpg') no-repeat;top:10">
	<form method="post" action="../ajaxPages/crud.php" id="formSaveCompany">
	<div class="container verticalDiff">
		<div class="commonTextFont107 leftHeader" >
			<h6><strong>Название компании</strong></h6>
		</div>
		<div class="rightHeader">
			<input style="width:100%" class="commonInputTextBox" type="text" name="txtNameCompany" placeholder="Название компании" id="txtNameCompany"/>
		</div>
	</div>

	<div class="container verticalDiff">
		<div class="commonTextFont107 leftHeader" >
			<h6><strong>Логотип компании</strong></h6>
		</div>
		<div class="rightHeader container" style="text-align: left">
			<div>
				<input class="commonInputTextBox" type="text" name="txtLogoCompany" placeholder="Логотип компании" id="txtLogoCompany"/>
			</div>
			<div class="logoSelect" onclick="document.getElementById('userfile').click();" >
				<input type="file" id="userfile" name="userfile" value="" style="display: none" />
			</div>
		</div>
	</div>

	<div class="container verticalDiff">
		<div class="commonTextFont107 leftHeader" >
			<h6><strong>О компании</strong></h6>
		</div>
		<div class="rightHeader">
			<div id="the-count" class="" style="margin-left: 0;width:50">
    			<label id="current">0</label>
    			<label id="maximum">/ 150</label>
  			</div>
			<textarea class="aboutCompany " rows="10" cols="45" name="txtAboutCompany" id="txtAboutCompany" style="height: 170" maxlength="150" ></textarea>
		</div>
	</div>
	<div class="container verticalDiff">
		<div class="commonTextFont107 leftHeader" >
			<h6><strong>Регион</strong></h6>
		</div>
		<div class="rightHeader">
			<div class="container">
				<div class="styled-select" style="width: 150">
					<select id="regionSelect" onchange="SetAjaxRequest1('showCommunities',document.getElementById('regionSelect').value,'companyNewsContainer','../register/listCommunities.php')">
						<?php
						foreach ($countries as $key => $value) {
							echo "<option value='$value->id'>" . $value -> name . "</option>";
						}
						?>
					</select>
				</div>

				<div id="companyNewsContainer" class="styled-select" style="width: 150"></div>
			</div>

		</div>
	</div>
	
		<div style="top:20 ;left:220" class="btn btn-primary btn-medium modal-close blockRelative"
		onclick="
		modules.modals.closeModal($('.modal-trigger'),'modal-8Company');
		document.getElementById('formSaveCompany').submit();
		//SetAjaxRequest('showCompany','<?php echo $currentCompany -> id; ?>');
		">
			Сохранить
		</div>
		<input type="hidden" name="login" value="<?php echo $_POST['login'] ?>" />
		<input type="hidden" name="password" value="<?php echo $_POST['password'] ?>" />
		<input type="hidden" name="idUser" value="<?php echo $user->id ?>" />
		<input type="hidden" name="action" value="addNewCompanyToUser" />
	</form>
</div>
<script src="../js/indexCharacterCount.js"></script>

