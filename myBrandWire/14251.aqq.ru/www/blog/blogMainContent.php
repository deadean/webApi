<?php session_save_path("../tmp");session_start(); ?>
<?php
	include_once '../lib/csFunctions.php';
	InitGlobServerRedirectConstants();
	include_once "../model/controller.php";
	include_once "../model/mainController.php";
	$controller = new Controller();
	$news = $controller->GetBlogNews();
?>

<div class="textLeft commonTextFont textParagraph blockPadding " style="margin-top: 20;margin-bottom: 20">
	Блог
</div>

<div class="horizontalSeparator" style="margin-top: 0"></div>

<div class="textLeft commonTextFont blockRelative greyText" style="height: 50;top:15">
	Сортировать по : дате
</div>

<div class="horizontalSeparator" style="margin-top: 0"></div>

<div class="" style="width:600">
	<?php
	foreach ($news as $key => $value) {
		echo "<div class='blockRelative' style='top:10' >";
		include "blogNewsForm.php";
		echo "</div>";
		echo "<div class='horizontalSeparator1 blockRelative' style='top:10;' > </div>";
	}
	?>
</div>

<div class="layer_0_copy_2-holder clearfix footerStyle" style="top:5; margin-left: 100;position: relative;left:-240;width: 100%" >
	<?php
	include_once "../footer/footer.php";
	?>
</div>