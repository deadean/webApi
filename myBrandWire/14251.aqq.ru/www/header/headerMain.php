<link rel="stylesheet" type="text/css" href="../messages/style.css" />
<link rel="stylesheet" type="text/css" href="../css/animatedEmail.css" />
<link rel="stylesheet" type="text/css" href="../../messages/style.css" />
<link rel="stylesheet" type="text/css" href="../../css/animatedEmail.css" />

<link rel="alternate" href="http://mybrandw.s66.r53.com.ua/lib/php/rss1/rss.php" type="application/xml"
      title="MyBrandWire RSS">

<script src="../js/prefixfree.min.js"></script>
<script src="../../js/prefixfree.min.js"></script>

<!--<script type="text/javascript" src="../lib/jq/1.10.2/jquery-1.10.2.min.js"></script>-->
<!--<script type="text/javascript" src="../../lib/jq/1.10.2/jquery-1.10.2.min.js"></script>-->

<script type="text/javascript" src="../js/jsEmailForm.js"></script>
<script type="text/javascript" src="../../js/jsEmailForm.js"></script>

<div class=" header_mybrandwire_gmail_com clickable hrefWrapper " style="z-index: 1;">
	<label for="nav" class="entypo-menu commonTextFont clickable">mybrandwire@gmail.com</label>
</div>

<input type="checkbox" id="nav"  onclick="SetOpacityElementById('navID','1')"/>
<nav id="navID">
	<input type="checkbox" id="nav"/>
	<div class="btnClose">
		<label for="nav" class="entypo-menu clickable" style="width: 100;height: 50"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</br>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</br>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</br> </label>
	</div>
	<div>
		<?php
		if (is_file("../ajaxPages/emailForm.php")) {
			include_once "../ajaxPages/emailForm.php";
		}
		if (is_file("ajaxPages/emailForm.php")) {
			include_once "ajaxPages/emailForm.php";
		}
		if (is_file("../../ajaxPages/emailForm.php")) {
			include_once "../../ajaxPages/emailForm.php";
		}
		?>
	</div>
</nav>
<div class="header_global_container_ expandingBoxFix"  >
	<div class="header_layer_23-holder clearfix ">
		<img class=" header_layer_36_copy" src="images/layer_36_copy.jpg" alt="" width="1280" height="2" />
		<div class="header_wrapper1 clearfix ">
			<a href="<?php echo GetPageByPlace("Main" . $_SERVER['PHP_SELF']); ?>">
			<div class="header_logotype clickable ">
				<img class=" header_my" src="images/my.png" alt="my" width="43" height="30" title="my" />
				<img class=" header_brandwire" src="images/brandwire.png" alt="BrandWire" width="215" height="39" title="BrandWire" />
			</div> </a>


			<!--<div class="header_facebook " style="z-index: 1">
				<a target="_blank" href="https://www.facebook.com/groups/1411118389128924/"><img class=" header_facebook_2" src="images/facebook_2.jpg" alt="" width="13" height="24" /></a>
			</div>
			<div class="header_vkontacte" style="z-index: 1">
				<a target="_blank" href="https://vk.com/mybrandwire"><img class=" header_layer_16" src="images/layer_16.jpg" alt="" width="14" height="15" /></a>
			</div>-->

			<style>

                .search1{
                    margin-top: -25;
                }
				.search1 input {
					position: absolute;
					width: 30px; /** Same size as the go button */
					right: 0; /** Position the element over the go button */
					float: Left;
					margin-left: 130px;
                    marhin-top:25px;
					-webkit-transition: all 1s ease-in-out;
					-moz-transition: all 1s ease-in-out;
					-o-transition: all 1s ease-in-out;
					transition: all 1s ease-in-out;
					background: WhiteSmoke;
					height: 30px;
					font-style: italic;
					font-family: arial;
					font-size: 15px;
					outline: none;
					-webkit-appearance: none;
					border-radius: 10;
					-moz-opacity: 0;
					-khtml-opacity: 0;
					-webkit-opacity: 0;
					opacity: 0;
					z-index: 2;
				}

				.search1 input:hover, .search1 input:focus {
					width: 235px;
					-moz-opacity: 2;
					-khtml-opacity: 2;
					-webkit-opacity: 2;
					opacity: 2;
				}

			</style>
			<div class="search1 ">
				<script>
					function GetParams(placeValue) {
						params = {
							place : placeValue,
							value : document.getElementById('SearchForm').value
						};
						SetUnivaersalJSONAjax(params, 'idMainContent', '../ajaxPages/crud.php', 'find');
					}
				</script>
				<input type="text" name="searchParam" maxlength="64" id="SearchForm" placeholder="Поиск..."
				onkeyup="GetParams('<?php echo $_SERVER["PHP_SELF"]; ?>')"
				/>
				<div class="blockRelative blockRight clickable " style="margin-top: -25">
					<img class="" src="images/shape_5.png" style="" alt="" width="17" height="18" />
				</div>
			</div>

		</div>
	</div>
	<div class="header_main_menu">
		<div class="header_layer_1-holder menuWrapper  container  commonTextFont boldText">
			<div>
				<img class="" src="images/layer_2.jpg" alt="" width="2" height="42" />
			</div>
			<div class=" btnMainMenu clickable hoverBlock" onclick="document.getElementById('hMain').click()">
				<a id="hMain" href="<?php echo GetPageByPlace("Main" . $_SERVER['PHP_SELF']); ?>">Главная</a>
			</div>

			<div>
				<img class="" src="images/layer_2.jpg" alt="" width="2" height="42" />
			</div>
			<div class=" btnMainMenu clickable hoverBlock" onclick="document.getElementById('hAbout').click()">
				<a id="hAbout" href="<?php echo GetPageByPlace("About" . $_SERVER['PHP_SELF']); ?>">О Нас</a>
			</div>

			<div>
				<img class="" src="images/layer_2.jpg" alt="" width="2" height="42" />
			</div>
			<div class=" btnMainMenu clickable hoverBlock" onclick="document.getElementById('hServices').click()">
				<a id="hServices" href="<?php echo GetPageByPlace("Services" . $_SERVER['PHP_SELF']); ?>">Услуги</a>
			</div>

			<div>
				<img class="" src="images/layer_2.jpg" alt="" width="2" height="42" />
			</div>
			<div class=" btnMainMenu clickable hoverBlock" onclick="document.getElementById('hBlog').click()">
				<a id="hBlog" href="<?php echo GetPageByPlace("Blog" . $_SERVER['PHP_SELF']); ?>">Блог</a>
			</div>

			<div>
				<img class="" src="images/layer_2.jpg" alt="" width="2" height="42" />
			</div>
			<div class=" btnMainMenu clickable hoverBlock" onclick="document.getElementById('hMainNews').click()">
				<a id="hMainNews" href="<?php echo GetPageByPlace("MainNews" . $_SERVER['PHP_SELF']); ?>">Главные новости</a>
			</div>
			<div>
				<img class="" src="images/layer_2.jpg" alt="" width="2" height="42" />
			</div>

            <div class=" btnMainMenu clickable hoverBlock" onclick="document.getElementById('hHowThisWorks').click()">
                <a id="hHowThisWorks" href="<?php echo GetPageByPlace("HowThisWorks" . $_SERVER['PHP_SELF']); ?>">Как это работает?</a>
            </div>
            <div>
                <img class="" src="images/layer_2.jpg" alt="" width="2" height="42" />
            </div>

		</div>
	</div>
	<header class="header_top_line">
		<!--<img class=" header_layer_25" src="images/layer_25.png" alt="" width="1" height="38" />-->
		<img class=" header_layer_25_copy_3" src="images/layer_25.png" alt="" width="1" height="38" />
		<img class=" header_layer_25_copy" src="images/layer_25.png" alt="" width="1" height="38" />
		<img class=" header_layer_25_copy_2" src="images/layer_25.png" alt="" width="1" height="38" />
		<img class=" header_layer_32" src="images/layer_32.png" alt="" width="16" height="16" />
		<img class=" header_layer_30" src="images/layer_30.png" alt="" width="1280" height="3" />

		<div class="clearfix " style="display: none" id="blockRegister">
			<a  href="<?php echo GetPageByPlace("Register" . $_SERVER['PHP_SELF']); ?>" class=" header_register hrefWrapper">Регистрация</a>
			<img class=" header_layer_37" src="images/layer_37.png" alt="" width="16" height="13"  />
			<img class=" header_layer_54" src="images/layer_54.png" alt="" width="20" height="20" />
			<a href="<?php echo GetPageByPlace("Login" . $_SERVER['PHP_SELF']); ?>" class=" header_login hrefWrapper">Логин</a>
		</div>

		<div class="clearfix" id="blockLogin">
			<img class=" header_layer_54" src="images/layer_54.png" alt="" width="20" height="20">
			<p class="header_register" >
				Пользователь
			</p>
			<img class=" header_layer_37" src="images/layer_37.png" alt="" width="16" height="13" />
			<p class="header_login hrefWrapper" >
				<a href="<?php echo GetPageByPlace("Company" . $_SERVER['PHP_SELF']); ?>"> <?php echo $_SESSION['loginUser'] ?> </a>
			</p>
		</div>

		<!--<div class=" header_mybrandwire_gmail_com clickable hrefWrapper">mybrandwire@gmail.com</div>-->

		<p class=" header_mybrandwire">
			<a href="skype:mybrandwire?call" class="hrefWrapper">mybrandwire</a>
		</p>

		<!--<p class=" header_vybrat_yazyk">Выбрать язык:</p>-->
		<!--<img class=" header_layer_52" src="images/layer_52.png" alt="" width="22" height="18" />-->
		<!--<img class=" header_layer_53" src="images/layer_53.png" alt="" width="22" height="18" />-->
		<img class=" header_layer_31" src="images/layer_31.png" alt="" width="10" height="16" />
	</header>

	<!--<div class="header_rss" style="z-index: 1">
        <a target="_blank" href="../lib/php/rss1/rss.php">
		    <img class=" header_rss_2" src="images/rss.jpg" alt="" width="16" height="17" />
        </a>
	</div>

	<div class="header_twitter_v2" style="z-index: 1">
		<a  target="_blank" href="http://www.twitter.com"><img class="hoverIcon header_twitter" src="images/twitter.jpg" alt="" width="23" height="19" /></a>
	</div>-->

    <div class="header_text" style="float: left;position: relative;">
        <img class="" src="../images/headerText.png" alt="" style="float: left;position: relative;"/>
    </div>

</div>