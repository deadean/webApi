<?php session_save_path("tmp");session_start(); ?>
<?php

//unset($_SESSION);
//session_destroy();
//setcookie("USER","",time()-3600);
include_once 'lib/csFunctions.php';
include_once 'model/controller.php';
include_once 'lib/php/Interfaces/Cookies/ICookieService.php';
include_once 'lib/php/Interfaces/Cookies/CookieBase.php';
include_once 'lib/php/Implementations/Cookies/CCookiesService.php';
include_once 'lib/php/Implementations/Cookies/CCookie.php';

$cookieService = new \Cookies\CCookiesService();
//print_r($_COOKIE);
//echo $_COOKIE[$_COOKIE[\csConstants::$csUSER]];
if ($cookieService->IsUserCookiesExist()) {
	$cookieInfo = $cookieService->GetCookiesForUser();
	Controller::getInstance()->IsUserRegistered($_COOKIE[\csConstants::$csUSER], $_COOKIE[$_COOKIE[\csConstants::$csUSER]]);
	//print_r($_SESSION);
	session_write_close();
}

//print_r($_SESSION);

InitGlobServerRedirectConstants();
CheckForMessagies();
//error_reporting(0);
$user = unserialize($_SESSION["userObject"]);
?>
<html>
<head>
	<title>myBrandWire</title>
	<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
	<meta name="description" content="myBrandWire"/>
	<meta name="keywords" content="myBrandWire"/>
	<link rel="stylesheet" type="text/css" href="style.css"/>
	<link rel="stylesheet" type="text/css" href="commonStyles.css"/>
	<link rel="stylesheet" type="text/css" href="company/news/style.css"/>
	<link rel="stylesheet" type="text/css" href="header/style.css"/>
	<link rel="shortcut icon" href="images/favicon.ico">
	<script type="text/javascript" src="services/servicesScript.js"></script>
</head>

<body
	onload="AnaliseAndSetElementsVisisbility
		(
		'<?php echo $_SESSION["loginUser"] ?>',
		'<?php echo $_SESSION["idUser"] ?>'
		)"
	>
<?php
if ($_GET["action"] == "messageRegisterShow")
	include_once 'messages/sendEmailMessage.php';
?>
<div class="global_container_ expandingBoxFix">

	<?php
		include_once 'header/headerMain.php';
	?>

	<div id="idMainContent">

		<?php include_once 'ajaxPages/indexMainContent.php'; ?>

	</div>
</div>
</body>
<!--<link rel="stylesheet" type="text/css" href="css/css1200.css" />-->
</html>