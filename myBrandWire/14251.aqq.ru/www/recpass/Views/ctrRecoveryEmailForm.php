<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
	<link href="login-box.css" rel="stylesheet" type="text/css"/>
	<link href="../style.css" rel="stylesheet" type="text/css"/>
	<link href="../commonStyles.css" rel="stylesheet" type="text/css"/>
	<link rel="shortcut icon" href="../images/favicon.ico">
	<link rel="stylesheet" href="../css/styleModalWindow.css" media="screen" type="text/css"/>

	<script type="text/javascript" src="../services/servicesScript.js"></script>
	<script type='text/javascript' src='../lib/knockouts/knockout-3.1.0.js'></script>

</head>
<body>
<div class="clearfix">
	<div id="login-box">
		<div style="margin-top: 10px"><H3>Восстановление пароля</H3></div>
		<div style="margin-top: 10px">Введите пожалуйста новый пароль</div>

		<div class="container" style="margin-top: 20px">
			<div id="" style="width: 150px">
				Новый пароль:
			</div>
			<div id="login-box-field" style="padding-left: 10px">
				<input id="idPass1" name="login" type="password" class="form-login" title="Username"
					   data-bind="value: pass1"
					   size="30" maxlength="2048"/>
			</div>
		</div>

		<div class="redText" style="margin-left:0;margin-top:5px;margin-bottom:5px;float: right"
			 data-bind='visible:errorRecoverPassEqual, valueUpdate: 'afterkeydown', event: { change: value_changed }'>
			Пароли не совпадают
		</div>

		<div class="redText" style="margin-left:0;margin-top:5px;margin-bottom:5px;float: right"
			 data-bind='visible:errorRecoverPassLength, valueUpdate: 'afterkeydown', event: { change: value_changed }'>
			Пароль должен быть минимум 6 символов
		</div>

		<div style="clear: both"></div>

		<div class="container" style="margin-top: 10px">
			<div id="" style="width: 150px">
				Повторите пароль:
			</div>
			<div id="login-box-field" style="padding-left: 10px">
				<input id="idPass2" name="password" type="password" class="form-login" title="Password"
					   data-bind="value: pass2"
				   size="30" maxlength="2048"/>
			</div>
		</div>

		<div style="position: relative; margin-top: 30px;margin-left: auto;margin-right: 0; float: right" class="">
			<div class="loginBtn clearfix clickable" data-bind="click : onClickLogin"></div>
		</div>
	</div>
</div>

<script src='../js/jqueryModalWindow.js'></script>
</body>
</html>
