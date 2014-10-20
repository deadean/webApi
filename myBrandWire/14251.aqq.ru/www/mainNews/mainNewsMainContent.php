<?php

include_once "../model/controller.php";
include_once "../model/mainController.php";
$controller = new Controller();
$news = $controller -> GetPaymentNews();
//print_r($news);
$news = array_slice($news, 0,3);
$categories = $controller -> GetCategories();
$user = unserialize($_SESSION["userObject"]);

?>
<div class="textLeft commonTextFont textParagraph blockPadding " style="margin-top: 20;margin-bottom: 20">
	Главные новости
</div>

<div class="horizontalSeparator" style="margin-top: 0;width:600"></div>

<div class="container textLeft " style=" height: 50;top:5">
	<div class="commonTextFont blockRelative greyText clickable hrefWrapperDarkSlateBlue"
         onclick="
            document.getElementById('s6').value='0';
            SetUnivaersalJSONAjax('', 'idMainNewsForm', '../ajaxPages/crud.php', 'sortMainNewsByDate');"
        >
		Сортировать по : дате,
	</div>
	<div class="commonTextFont blockRelative " style="max-width:50;">
		<div style="margin-left: 5" class="greyText clickable hrefWrapperDarkSlateBlue"
		onclick="$('#msgChooseCategory').css('display','block'); modules.modals.openModal($('.modal-trigger'),'modal-8');"
		>
			категории
		</div>
	</div>
	<link rel="stylesheet" href="../css/styleModalWindow.css" media="screen" type="text/css" />

	<div style="width:255;"></div>

	<?php
	$addNewMainNews = "";

	if (!$user) {
		$addNewMainNews = "../login/login.php";
	} else {
		$addNewMainNews = "../company/news/newsAdaptee.php";
	}
	?>
	<div>
		<form action="<?php echo $addNewMainNews; ?>" id="formSubmit" class="wrapper_hover">
			<div class=" btnPublish commonTextFont" style="text-align: center; " onclick="document.getElementById('formSubmit').submit();">
				Опубликовать ПР
			</div>
		</form>
	</div>
</div>

<div class="horizontalSeparator" style="margin-top: 0;width:600"></div>

<div style="display: none" id="msgChooseCategory">
	<?php

	$isShow = "0";
	$headerMessage = "Выбор категории";
	$contentMessage1 = "Выберите категорию новостей для просмотра";
	$addedContent = "<div class='container newsFormItem' style='top:10'>
												<div class='leftHeader commonTextStyle'>
												<span>Категории</span>
												</div>
												<div class='rightContanter commonTextStyle styled-select' style='float:none'>
												<select id='s6' >";
	$addedContent = $addedContent . "<option value='0'>" . "" . "</option>";
	foreach ($categories as $key => $value) {
		$addedContent = $addedContent . "<option value='$value->id'>" . $value -> name . "</option>";
	}
	$actionBtnOk = "SetUnivaersalJSONAjax(document.getElementById('s6').value, 'idMainNewsForm', '../ajaxPages/crud.php', 'sortMainNewsByCategoryId');";
	$addedContent = $addedContent . '</select><div id="returnS6"></div></div></div><input type="hidden" id="returnS7" name="RES" />
								<div style="top:20 ;margin-left:180;display:inline-block"></div>';
	include_once '../lib/MessageBox.php';

	?>
</div>

<div class="" style="width:600" id="idMainNewsForm">
	<?php
	include "mainNewsForm.php";
	?>
</div>

<div class="layer_0_copy_2-holder clearfix footerStyle" style="top:5; margin-left: 100;position: relative;left:-240;width: 100%" >
	<?php
	include_once "../footer/footer.php";
	?>
</div>

