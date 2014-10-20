<?php session_save_path("../tmp");session_start(); ?>
<?php
//print_r($_SESSION);
include_once '../lib/csFunctions.php';
include_once '../model/mainController.php';

InitGlobServerRedirectConstants();
?>
<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
		<link rel="stylesheet" type="text/css" href="style.css" />
		<link rel="stylesheet" type="text/css" href="../commonStyles.css" />
		<link rel="stylesheet" type="text/css" href="../header/style.css" />
        <link rel="shortcut icon" href="../images/favicon.ico">


        <script type="text/JavaScript" src="servicesScript.js"></script>
		<script src="js/prefixfree.min.js"></script>
		<script src="js/modernizr.js"></script>


	</head>
	<body onload="AnaliseAndSetElementsVisisbility('<?php echo $_SESSION["loginUser"] ?>', '<?php echo $_SESSION["idUser"] ?>')">

		<div class="global_container_ expandingBoxFix " style="height: 100%;min-height: 600">

			<?php
			include_once '../header/headerMain.php';
			?>
			<div class="emptyContainer" id="idMainContent">
				<?php
					include_once 'servicesMainContent.php';
				?>
			</div>
		</div>
	</body>
</html>
<?php
$typeService = $_GET["type"];
if (is_null($typeService))
	return;
if ($typeService == "social")
	echo "<script>document.getElementById('menuSocial').click()</script>";
if ($typeService == "publish")
	echo "<script>document.getElementById('menuPublish').click()</script>";
if ($typeService == "writing")
	echo "<script>document.getElementById('menuWriting').click()</script>";
?>