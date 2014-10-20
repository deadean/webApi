<?php session_save_path("../tmp");session_start(); ?>
<?php
    include_once '../lib/csFunctions.php';
    include_once '../model/mainController.php';
    InitGlobServerRedirectConstants();
    //print_r($_COOKIE);
?>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
		<link href="login-box.css" rel="stylesheet" type="text/css" />
		<link href="../style.css" rel="stylesheet" type="text/css" />
		<link href="../commonStyles.css" rel="stylesheet" type="text/css" />
        <link rel="shortcut icon" href="../images/favicon.ico">
        <link rel="stylesheet" href="../css/styleModalWindow.css" media="screen" type="text/css" />

		<script type="text/javascript" src="../services/servicesScript.js"></script>
        <script type='text/javascript' src='../lib/knockouts/knockout-3.1.0.js'></script>

	</head>
	<body>
		<div class="clearfix">
			<div id="login-box">
				<H2>Авторизация</H2>
				Введите пожалуйста личные данные
				<br />
				<br />
				<form id="formLogin" method="post" action="<?php echo $_SERVER['PHP_SELF']; ?>">
					<div id="login-box-name" style="margin-top:20px;">
						Логин:
					</div>
					<div id="login-box-field" style="margin-top:20px;">
						<input id="login" name="login" class="form-login" title="Username" value="" size="30" maxlength="2048" />
					</div>
					<div id="login-box-name">
						Пароль:
					</div>
					<div id="login-box-field">
						<input id="password" name="password" type="password" class="form-login" title="Password" value="" size="30" maxlength="2048" />
					</div>
					<br />
					<div class="container " style="display: inline-block">
						<div style="width:180" class="container" >
							<div>
                                <input name="IsRememberMe" id="IsRememberMe" type="checkbox" data-bind="checked: IsRememberMe, click: SetRememberMe"/>
							</div>
							<div class="hrefWrapper" >Запомнить меня</div>
                        </div>
						<div style="width:230"
							 class="hrefWrapper clickable"
							 data-bind="click: OpenEmailRecoveryForm"
							>
							<div>Вспомнить пароль</div>
						</div>
						<div><a href="<?php echo GetPageByPlace("Register".$_SERVER['PHP_SELF']); ?>" class="hrefWrapper" >Регистрация</a></div>

					</div>
					<br />
					<br />
					<div class=" clearfix ">
						<input type="submit" class="loginBtn clearfix clickable" value="" />
						<a href="../index.php" class="clearfix cancelBtn" ></a>
					</div>
					<input type="hidden" name="action" value="login"/>
				</form>
			</div>
		</div>

		<script src='../js/jqueryModalWindow.js'></script>
		<script>
			data = <?php
                echo json_encode((array)$_COOKIE[csConstants::$csUSER]);
             ?>;
		</script>



		<?php if(isset($_GET["error"]))
		{
			$isShow="1";
			$headerMessage = "Ошибка авторизации"; 
			$contentMessage1 = "Проверьте, правильно ли введены логин и пароль?";
			$contentMessage2 = "Помните, для авторизации на сайте, необходимо вначале зарегестрироваться.";
			$addedContent = "<div style='top:20 ;left:160' class='btn btn-primary btn-medium modal-close blockRelative commonTextFont' 
								onclick=window.location='../register/register.php'>Регистрация</div>";
			include '../lib/MessageBox.php';
		}?>

		<?php
			$isShow="0";
			$isNotCloseByActionOk=true;
			$addedId = "ctrEmailRecoveryForm";
			$headerMessage = "Восстановление пароля";
			$contentMessage1 = "Введите email, указанный при регистрации :";
			$contentMessage2="";
			$actionBtnOk="instanceViewModel.TrySendEmail(data)";
			$addedContent =
				"
				<div style='margin-top: -25; margin-left: 150' class=''>
					<input class='commonInputTextBox' id='idRecoveryEmail' type='email' placeholder='Введите email' data-bind='value: userEmail'>
				</div>
				<div data-bind='visible:errorRecoverEmailMessageState' style='margin-top: 0; margin-left: 190' class='redText' align='left'>
					Введенный email не зарегестрирован на сайте
				</div>
				";
			include '../lib/MessageBox.php';
		?>

		<?php
			$isShow="0";
			$isNotCloseByActionOk=false;
			$addedId = "ctrEmailRecoveryHasSent";
			$headerMessage = "Восстановление пароля";
			$actionBtnOk="";
			$addedContent ="";
			$contentMessage1 = "На введенный email были отправлены инструкции для восстановления доступа";
			$contentMessage2="";
			include '../lib/MessageBox.php';
		?>

		<script type='text/javascript' src='ViewModels/LoginVm.js'></script>
	</body>
</html>
