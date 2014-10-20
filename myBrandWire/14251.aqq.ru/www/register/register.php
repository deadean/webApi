<?php session_save_path("../tmp");session_start(); ?>
<?php
include_once '../model/mainController.php';
$controller = new Controller();
$countries = $controller -> GetCountries();
?>
<html>
	<head>
		<title></title>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
        <link rel="shortcut icon" href="../images/favicon.ico">

        <meta name="description" content="" />
		<meta name="keywords" content="" />
		<meta name="generator" content="" />
		<link rel="stylesheet" type="text/css" href="style.css" />
		<link rel="stylesheet" type="text/css" href="styleRegister.css" />
		<link rel="stylesheet" type="text/css" href="../commonStyles.css" />
		<link href="../commonStyles.css" rel="stylesheet" type="text/css" />
		<link rel="stylesheet" href="../css/styleModalWindow.css" media="screen" type="text/css" />
		<script type="text/javascript" src="../services/servicesScript.js"></script>
		<script src="../js/globalJQ.js"></script>
		<script type='text/javascript' src='../lib/knockouts/knockout-3.1.0.js'></script>
		
	</head>

	<script>
		data = {
			errorRecoverPassLength : false,
			pass1 : "",
			errorNewUserEmailIsAlreadyRegistered : false,
			newUserEmail : ""
		};
	</script>

	<body>
		<div class="global_container_ clearfix expandingBoxFix">
			<header class="header clearfix">
				<div class="layer_5_copy_2-holder clearfix">
					<div class="c_wrapper1 clearfix">
						<img class="" src="images/my.png" alt="BrandWire" width="38" height="26" title="BrandWire" style="margin-left: -835;margin-top: 20;z-index: 1;position: relative" />
						<img class=" brandwire" src="images/brandwire1.png" alt="BrandWire" width="215" height="39" title="BrandWire" style="z-index: 2;position: relative" />
						<p class=" registraciya_novoi_uchetn">
							<strong>Регистрация новой учетной записи</strong>
						</p>
					</div>
					<div class="zakryt clickable hrefWrapper">
						<a href="../index.php">
						<p class=" ">
							закрыть
						</p>
						<div class="shape_1-holder" style="position: relative;left:90;top:-12">
							<img class="" src="images/layer_4.png" alt="" width="9" height="8" />
						</div> </a>
					</div>
				</div>

				<img class=" lay
				er_3" src="images/layer_3.png" alt="" width="13" height="7" />
			</header>
			<p class=" shag_1_personalnaya_infor">
				<strong>Шаг 1 — Персональная информация</strong>
			</p>
			<p class=" informaciya_budet_ispolzo">
				Информация будет использоваться для обратной связи
			</p>
			<form enctype="multipart/form-data" method="post" action="<?php echo $_SERVER['PHP_SELF']; ?>">
				<fieldset>

					<div class="layer_2-holder clearfix">
						<div class="group_1 clearfix">
							<p class=" imya">
								<strong>Имя</strong>
							</p>

							<div class="item">
								<input data-validate-length-range="6" data-validate-words="1" required="required"
								class="layer_1_copy_8-holder" type="text" name="txtNameUser" placeholder="Имя" id="txtNameUser" />
								<div class='tooltip help' style="top:0">
									<span>?</span>
									<div class='content'>
										<b></b>
										<p>
											Имя не должно быть пустым
										</p>
									</div>
								</div>
							</div>

						</div>
						<div class="group_1_copy clearfix" style="padding: 0;margin-top:15;width:546">
							<p class=" elektropochta" style="text-align: center;margin-top: 10">
								<strong>Фамилия</strong>
							</p>
							<div class="item">
								<input data-validate-length-range="6" data-validate-words="1" required="required"
								class="layer_1_copy_9-holder" style="margin-left:10" type="text" name="txtSurnameUser" placeholder="Фамилия" id="txtSurnameUser"/>
								<div class='tooltip help' style="top:0">
									<span>?</span>
									<div class='content'>
										<b></b>
										<p>
											Фамилия не должна быть пустой
										</p>
									</div>
								</div>
							</div>
						</div>
						<div class="group_1_copy clearfix" style="padding: 0;margin-top:15;width:946">
							<p class=" elektropochta" style="text-align: center;margin-top: 10">
								<strong>Электропочта</strong>
							</p>
							<div class="item" style="">
								<input data-validate-length-range="6" data-validate-words="1" required="required"
									   data-bind="value: newUserEmail"
								class="layer_1_copy_9-holder1 email" style="margin-left:10;width: 496" type="email" name="txtEmailUser"
								placeholder="Мы вышлем письмо с сылкой для активации" id="txtEmailUser"/>
								<div class='tooltip help' style="top:0">
									<span>?</span>
									<div class='content'>
										<b></b>
										<p>
											Email не должен быть пустым
										</p>
									</div>
								</div>
							</div>

							<div id="idTxtNewUserEmailErrorToolTip"
								 data-bind='visible:errorNewUserEmailIsAlreadyRegistered, valueUpdate: 'afterkeydown', event: { change: value_changed }'
								class="redText " style="float: left;margin-top:2;margin-left: auto;margin-right:0; width: 68%">
								Извините. Такой email уже зарегестрирован. Введите другой email адрес.
							</div>

						</div>

						<div class="group_1_copy clearfix" style="padding: 0;margin-top:15;width:946">
							<p class=" elektropochta" style="text-align: center;margin-top: 10">
								<strong>Телефон</strong>
							</p>
							<div class="item" style="">
								<input data-validate-length-range="6" data-validate-words="1" required="required"
								class="layer_1_copy_9-holder1" style="margin-left:10;width: 496" type="text" name="txtPhoneUser"
								placeholder="Для связи при возникновении вопросов" id="txtPhoneUser"/>
								<div class='tooltip help' style="top:0">
									<span>?</span>
									<div class='content'>
										<b></b>
										<p>
											Телефон не должен быть пустым
										</p>
									</div>
								</div>
							</div>
						</div>
					</div>

					<img class=" layer_3_copy" src="images/layer_3_copy.png" alt="" width="13" height="7" />
					<p class=" shag_2_informaciya_o_komp">
						<strong>Шаг 2 — Информация о компании</strong>
					</p>
					<p class=" profil_kompanii_budet_vid">
						Профиль компании будет виден всем пользователям
					</p>

					<div class="layer_2-holder_Company clearfix " >
						<div class="group_1 clearfix" style="padding: 0;margin-top:0;width:746">
							<p class=" imyaCompany ">
								<strong>Название компании</strong>
							</p>
							<div class="item">
								<input data-validate-length-range="6" data-validate-words="1" required="required"
								style="margin-left:0;width: 496"
								class="layer_1_copy_8-holder_Company" type="text" name="txtNameCompany" placeholder="Название компании" id="txtNameCompany"/>
								<div class='tooltip help' style="top:0">
									<span>?</span>
									<div class='content'>
										<b></b>
										<p>
											Название компании
										</p>
									</div>
								</div>
							</div>
						</div>
						<div class="group_1_copy clearfix ">
							<p class=" logotype ">
								<strong>Логотип компании</strong>
							</p>

							<div class="item">
								<!-- Поле MAX_FILE_SIZE должно быть указано до поля загрузки файла.
								value указывается в байтах
								value="300000" - можно загрузить максимум 29Кб
								value="1050000" - можно загрузить максимум 1Мб
								value="314600000" - можно загрузить максимум 300Мб-->
								<input type="hidden" name="MAX_FILE_SIZE" value="314600000" />
								<input class="logotypePath" type="text" name="txtLogoCompany" placeholder="Добавить логотип" id="txtLogoCompany"/>
								<div class="logoSelect" onclick="document.getElementById('userfile').click();" >
									<input type="file" id="userfile" name="userfile" value="" style="display: none" />
								</div>
								<script>
									var file_api = ( window.File && window.FileReader && window.FileList && window.Blob ) ? true : false;
									var inp = $('#userfile');
									inp.change(function(){
								        var file_name;
								        if( file_api && inp[ 0 ].files[ 0 ] )
								            file_name = inp[ 0 ].files[ 0 ].name;
								        else
								            file_name = inp.val();//.replace( "C:\\fakepath\\", '' );

								        if( ! file_name.length )
								            return;

								        $('#txtLogoCompany').val(file_name);
								    }).change();
								</script>
								<div class='tooltip help' style="top:0">
									<span>?</span>
									<div class='content'>
										<b></b>
										<p>
											Логотип не должен превышать размер в 300Мб.
                                            Рекомендуемые параметры 216 * 216px
										</p>
									</div>
								</div>
							</div>
						</div>
						<div class="group_1_copy_2_Company clearfix " style="padding: 0;margin-top:0;width:646">
							<p class=" txtAboutCompany " style="margin-top: 0">
								<strong>О компании</strong>
							</p>
							<div class="item">

                                <div class="container " style="">
                                    <div style="padding-left: 15">Введено </div>
                                    <div class="" style="width: 25" id="current">0</div>
                                    <div>из рекумендуемых 150</div>
                                </div>
								<div>
                                    <textarea required="required"
									style="margin-left:15;width: 496;height: 170"
									class="aboutCompany " rows="5" cols="45" name="txtAboutCompany" id="txtAboutCompany" ></textarea>
                                    <div class='tooltip help' style="top:0">
                                        <span>?</span>
                                        <div class='content'>
                                            <b></b>
                                            <p>
                                                Информация о компании должна быть не меньше 150 символов
                                            </p>
                                        </div>
                                    </div>
                                </div>
							</div>
							
						</div>
						<div class="group_1_Company clearfix " style="padding: 0;margin-top:10;width:846">
							<p class=" loginCompany  ">
								<strong>Логин </strong>
							</p>
							<div class="item">
								<input data-validate-length-range="6" data-validate-words="1" required="required"
								style="margin-left:0;width: 496"
								onchange="SetUnivaersalJSONAjax(document.getElementById('txtLoginCompany').value, 'idLoginError', '../ajaxPages/crud.php', 'isUserRegister');"
								class="layer_1_copy_8-holder_Company " type="text" name="txtLoginCompany" placeholder="Латинскими символами" id="txtLoginCompany"/>
								<div class='tooltip help' style="top:0">
									<span>?</span>
									<div class='content'>
										<b></b>
										<p>
											Ваш Логин латинскими символами
										</p>
									</div>
								</div>
							</div>
						</div>
						<div class="group_1_Company clearfix">
							<p class=" loginCompany ">
								<strong>Пароль</strong>
							</p>
							<div class="item">
								<input required="required"
									   data-bind="value: pass1"
								class="layer_1_copy_8-holder_Password" type="password" name="txtPasswordCompany" id="txtPasswordCompany"/>
								<div class='tooltip help' style="top:0">
									<span>?</span>
									<div class='content' >
										<b></b>
										<p>
											Пароль должен быть не меньше 6 символов
										</p>
									</div>
								</div>
							</div>
							<div id="idTxtPassErrorToolTip"
								 data-bind='visible:errorRecoverPassLength, valueUpdate: 'afterkeydown', event: { change: value_changed }'
								 class="redText " style="float: left;margin-top:3;margin-left: auto;margin-right:0; width: 72%">
								Пароль должен быть не меньше 6 символов
							</div>
						</div>
						<div class="group_1_Company clearfix ">
							<p class=" loginCompany ">
								<strong>Регион</strong>
							</p>
							<div>
								<div class="styled-select">
									<select id="regionSelect" onchange="SetAjaxRequest1('showCommunities',document.getElementById('regionSelect').value,'companyNewsContainer','listCommunities.php')">
										<?php
										foreach ($countries as $key => $value) {
											echo "<option value='$value->id'>" . $value -> name . "</option>";
										}
										?>
									</select>
								</div>

								<div id="companyNewsContainer" class="styled-select"></div>
							</div>
						</div>
					</div>

					<img class=" layer_3_copy" src="images/layer_3_copy.png" alt="" width="13" height="7" />
					<p class=" profil_kompanii_budet_vid">
						Вам осталось только подтвердить регистрацию
					</p>

					<div class="buttonSaveDiv clearfix">
						<input class="buttonSave" type="submit" value="Создать аккаунт"/>
						<input type="hidden" name="action" value="save"/>
					</div>

					<div id="idLoginError" style="display:none"></div>

				</fieldset>
			</form>

		</div>

		<script src="../lib/jq/jqueryLib.js" ></script>
		<script src="../js/multifield.js"></script>
		<script src="../js/validator.js"></script>
		<script>
			// initialize the validator function
			validator.message['date'] = 'not a real date';

			// validate a field on "blur" event, a 'select' on 'change' event & a '.reuired' classed multifield on 'keyup':
			$('form').on('blur', 'input[required], input.optional, select.required', validator.checkField).on('change', 'select.required', validator.checkField);

			$('.multi.required').on('keyup', 'input', function() {
				validator.checkField.apply($(this).siblings().last()[0]);
			}).on('blur', 'input', function() {
				validator.checkField.apply($(this).siblings().last()[0]);
			});

			// bind the validation to the form submit event
			//$('#send').click('submit');//.prop('disabled', true);

			$('form').submit(function(e) {
				e.preventDefault();
				var submit = true;
				// evaluate the form using generic validaing
				if (!validator.checkAll($(this))) {
					submit = false;
				}

                if($('#txtAboutCompany').val().length<150){
                    modules.modals.openModal($('.modal-trigger'),'modal-8companyAbout');
                    submit = false;
                }

				instanceViewModel.errorRecoverPassLength(instanceViewModel.pass1().length<=6);
				if(instanceViewModel.errorRecoverPassLength()==true
					|| instanceViewModel.errorNewUserEmailIsAlreadyRegistered()==true
				)
				{
					submit = false;
				}

				if (document.getElementById('idLoginError').innerHTML == "\nerror") {
					modules.modals.openModal($('.modal-trigger'),'modal-8');
					return false;
				}
				
				if (submit)
					this.submit();
				return false;
			});
		</script>
		<script type='text/javascript' src='ViewModels/CRegisterVm.js'></script>
		<script src="../js/indexCharacterCount.js"></script>
		<?php
			$isShow="0";
			$headerMessage = "Ошибка регистрации";
			$contentMessage1 = "Указанный Вами логин уже зарегестрирован";
			$contentMessage2 = "Попробуйте выбрать другой логин.";
			include '../lib/MessageBox.php';

            $isShow="0";
            $addedId="companyAbout";
            $headerMessage = "Ошибка регистрации";
            $contentMessage1 = "Вы ввели менее 150 символов в поле 'О компании'";
            $contentMessage2 = "";
            include '../lib/MessageBox.php';
		?>
	</body>
</html>