<?php session_save_path("../tmp");session_start(); ?>
<?php
include_once '../lib/csFunctions.php';
InitGlobServerRedirectConstants();

?>

<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8" />

		<link rel="stylesheet" type="text/css" href="../company/style.css" />
		<link rel="stylesheet" type="text/css" href="../commonStyles.css" />
		<link rel="stylesheet" type="text/css" href="../header/style.css" />
		<link rel="stylesheet" type="text/css" href="../company/news/style.css" />
		<link rel="stylesheet" type="text/css" href="style.css" />
        <link rel="shortcut icon" href="../images/favicon.ico">


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
				SetUnivaersalJSONAjax(params, 'idMainNewsForm', '../ajaxPages/crud.php', 'showMainNewsPortionSize');
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

			<?php
			include_once '../header/headerMain.php';
			?>

			<div class=" emptyContent" style="top:5;height: 100%">

				<div id="idMainContent">
				
				<?php include_once 'mainNewsMainContent.php'; ?>
				
			</div>

			</div>
		</div>
		
		<?php
		
		if (isset($_GET["param"])) {
			echo "<script>SetUnivaersalJSONAjax(" . $_GET["param"] . ", 'idMainNewsForm', '../ajaxPages/crud.php', 'sortMainNewsByCategoryId');</script>";
			echo "<script>document.getElementById('s6').value='".$_GET["param"]."'</script>";
		}
		
		?>

	</body>

</html>

