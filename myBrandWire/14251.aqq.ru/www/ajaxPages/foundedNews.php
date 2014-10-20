<?php session_save_path("../tmp");session_start(); ?>
<?php
include_once '../lib/csFunctions.php';
InitGlobServerRedirectConstants();
include_once "../model/controller.php";
include_once "../model/mainController.php";
$controller = new Controller();

?>
<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
		<link rel="stylesheet" type="text/css" href="../company/style.css" />
		<link rel="stylesheet" type="text/css" href="../commonStyles.css" />
		<link rel="stylesheet" type="text/css" href="../header/style.css" />
		<link rel="stylesheet" type="text/css" href="../company/news/style.css" />
		<link rel="stylesheet" type="text/css" href="style.css" />

		<script type="text/javascript" src="../services/servicesScript.js"></script>
		<script type="text/javascript" src="../lib/jq/jquery-1.6.1.min.js"></script>
		<script>
			var portionSizeValue = 3;
			function UpdateNewsList() {
				portionSizeValue += 1;
				params = {
					portionSize : portionSizeValue,
					idCategory : document.getElementById('s6').value
				}
				SetUnivaersalJSONAjax(params, 'idFreeNewsForm', '../ajaxPages/crud.php', 'showFreeNewsPortionSize');
			}


			$(document).ready(function() {
				$(window).scroll(function() {
					if ($(window).scrollTop() + $(window).height() - 150 > $(document).height() || ($(window).scrollTop() + $(window).height() + 150 > $(document).height())) {
						UpdateNewsList();
					}
				});
			});
		</script>
	</head>

	<body onload="AnaliseAndSetElementsVisisbility('<?php echo $_SESSION["loginUser"] ?>', '<?php echo $_SESSION["idUser"] ?>')">
		<div class="global_container_ expandingBoxFix" style="min-height: 800;height:100%">

			<div class=" emptyContent" style="top:5;height: 100%">

				<div class="textLeft commonTextFont textParagraph blockPadding " style="margin-top: 20;margin-bottom: 20">
					Найденные результаты
				</div>

				<div class="horizontalSeparator" style="margin-top: 0;width:600"></div>

				<div class="" style="width:600" id="idFreeNewsForm">
					<?php
						if (is_file("../freeNews/freeNewsForm.php")) {
							include_once "../freeNews/freeNewsForm.php";
						}
						if (is_file("freeNewsForm.php")) {
							include_once "freeNewsForm.php";
						}
					?>
				</div>

			</div>

		</div>
	</body>
</html>
