<?php

	include_once '../lib/csFunctions.php';
	include_once '../model/controller.php';
	include_once '../lib/php/Interfaces/Recovery/CRecoveryBase.php';

	$recoveryInfo = new \php\Interfaces\Recovery\CRecoveryBase();
	$recoveryInfo->inputHashCode = $_GET['id'];
	$resultStruct = $recoveryInfo->IsInputHashCodeExistAndUseful();
	$idUser = $resultStruct['error']==\php\enErrors::$NONE ? $resultStruct['idUser'] : "";
?>

<html>

<script>
	data = <?php echo json_encode($_GET); ?>;

	data.errorRecoverPassEqual = false;
	data.errorRecoverPassLength = true;
	data.pass1="";
	data.pass2="";
	data.IsInputHashCodeExistAndUseful =  <?php echo $resultStruct['error']==\php\enErrors::$NONE ? 1 : 0; ?>;
</script>

<head>
	<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
	<link href="login-box.css" rel="stylesheet" type="text/css" />
	<link href="../style.css" rel="stylesheet" type="text/css" />
	<link href="../commonStyles.css" rel="stylesheet" type="text/css" />
	<link rel="shortcut icon" href="../images/favicon.ico">
	<link rel="stylesheet" href="../css/styleModalWindow.css" media="screen" type="text/css" />

	<script type="text/javascript" src="../services/servicesScript.js"></script>
	<script type='text/javascript' src='../lib/knockouts/knockout-3.1.0.js'></script>
	<script src="../lib/jq/1.10.2/jquery-1.10.2.min.js"></script>

</head>



<body>
	<?php
		if($resultStruct['error']==\php\enErrors::$NONE){
	?>

		<script>
			data.idUser = <?php echo $idUser; ?>;
		</script>
		<div data-bind="visible:IsInputHashCodeExistAndUseful">
			<?php include_once 'Views/ctrRecoveryEmailForm.php';?>
		</div>

		<script type='text/javascript' src='ViewModels/CRecoveryEmailFormVm.js'></script>
	<?php } ?>

	<?php
		$isShow="0";
		$isNotCloseByActionOk=false;
		$addedId = "ctrEmailRecoveryLinkIsOutOfDate";
		$headerMessage = "Восстановление пароля";
		$actionBtnOk="window.location='../login/login.php'";
		$addedContent ="";
		$contentMessage1 = "Срок действия ссылки для восстановления пароля истек.
		Повторите процедуру восстановления пароля";
		include '../lib/MessageBox.php';
	?>

	<script>
		if(!data.IsInputHashCodeExistAndUseful)
			modules.modals.openModal($('.modal-trigger'),'modal-8ctrEmailRecoveryLinkIsOutOfDate');
	</script>
</body>

</html>

