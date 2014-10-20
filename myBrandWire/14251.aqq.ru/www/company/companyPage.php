<?php session_save_path("../tmp");session_start();?>
<?php
    include_once '../lib/csFunctions.php';
    InitGlobServerRedirectConstants();

    include_once '../model/mainController.php';
    include_once '../model/controller.php';
    include_once '../model/model.php';

    //print_r($_GET);
    //print_r($_POST);

    $controller = Controller::getInstance();
    $currentCompany = $controller->GetCompanyById($_POST["id"]);

?>
<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
        <link rel="shortcut icon" href="../images/favicon.ico">


        <link rel="stylesheet" type="text/css" href="style.css" />
		<link rel="stylesheet" type="text/css" href="../commonStyles.css" />
		<link rel="stylesheet" type="text/css" href="../header/style.css" />
		<link rel="stylesheet" href="../css/normalize.css">
		<link rel="stylesheet" href="../css/styleModalWindow.css" media="screen" type="text/css" />
		<link rel="stylesheet" type="text/css" href="js/simptip-mini.css" media="screen,projection" />

		<script type="text/javascript" src="../services/servicesScript.js"></script>
		<script type="text/javascript" src="js/jqueryLib.js"></script>

	</head>

	<body onload="AnaliseAndSetElementsVisisbility('<?php echo $_SESSION["loginUser"] ?>', '<?php echo $_SESSION["idUser"] ?>')">

		<div class="global_container_ expandingBoxFix " style="min-height: 800;height:100%">

			<?php
			include_once '../header/headerMain.php';
			?>

            <div class="clearfix " id="container">

    		</div>

		<div id="companyTabId" class="" style="margin-top: -100">
			<?php
			include_once "companyTabPage.php";
			?>
		</div>

		</div>
		<script>
            LoadImages(<?php echo $currentCompany->logo; ?>);
		</script>
		
	</body>

</html>
