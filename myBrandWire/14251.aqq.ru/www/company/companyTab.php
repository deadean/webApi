<?php session_save_path("../tmp");session_start(); ?>
<?php
    include_once '../lib/csFunctions.php';
    include_once '../model/controller.php';
    include_once '../model/model.php';

    $controller = Controller::getInstance();
    $user = unserialize($_SESSION["userObject"]);
    //print_r($_GET);
    //print_r($_POST);
    //print_r($user);

    $idCompany = "";

    foreach ($_GET as $key => $val) {
        $idCompany = $_GET["idCompany"];
    }

    if (!$currentCompany)
        $currentCompany = $user->companies[$idCompany];

    if ($currentCompany == NULL) {
        foreach ($user->companies as $company) {
            $currentCompany = $company;
            break;
        }
    }

    $currentCompany->RefrreshCommunications();
    $currentCompany->RefrreshPersons();
    $currentCompany->RefreshCompanyInfo();

	//print_r($currentCompany);

    $communicationsAll = $controller->GetCommunications();

    $facebook = "";
    $googlePlus = "";
    $vkontakte = "";
    $odnoklassniki = "";
    $instagramm = "";
    $twitter = "";
    $pinterest = "";
    $tumblr = "";
    $linkedin = "";

    $isFacebook = "0";
    $isGooglePlus = "0";
    $isOdnoklassniki = "0";
    $isInstagramm = "0";
    $isTwitter = "0";
    $isPinterest = "0";
    $isTumblr = "0";
    $isLinkedin = "0";

    $communications = array();

    //print_r($currentCompany->communications);
    foreach ($currentCompany->communications as $key => $value) {
        if (strcasecmp($value->name, "facebook") == 0) {
            $facebook = $value->value;
            $isFacebook = "1";
        }if (strcasecmp($value->name, "odnoklassniki") == 0) {
            $odnoklassniki = $value->value;
            $isOdnoklassniki = "1";
        }
        if (strcasecmp($value->name, "googleplus") == 0) {
            $googlePlus = $value->value;
            $isGooglePlus = "1";
        }
        if (strcasecmp($value->name, "vkontakte") == 0) {
            $vkontakte = $value->value;
            $isVkontakte = "1";
        }
        if (strcasecmp($value->name, "instagramm") == 0) {
            $instagramm = $value->value;
            $isInstagramm = "1";
        }
        if (strcasecmp($value->name, "twitter") == 0) {
            $twitter = $value->value;
            $isTwitter = "1";
        }
        if (strcasecmp($value->name, "pinterest") == 0) {
            $pinterest = $value->value;
            $isPinterest = "1";
        }
        if (strcasecmp($value->name, "tumblr") == 0) {
            $tumblr = $value->value;
            $isTumblr = "1";
        }
        if (strcasecmp($value->name, "linkedin") == 0) {
            $linkedin = $value->value;
            $isLinkedin = "1";
        }
    }

    //print_r($communicationsAll);

    foreach ($communicationsAll as $key => $value) {
        if (strcasecmp($value->name, "email") == 0 || strcasecmp($value->name, "skype") == 0
            || strcasecmp($value->name, "phone") == 0
        )
            continue;
        if (strcasecmp($value->name, "facebook") == 0 && $isFacebook)
            continue;
        if (strcasecmp($value->name, "googleplus") == 0 && $isGooglePlus)
            continue;
        if (strcasecmp($value->name, "vkontakte") == 0 && $isVkontakte)
            continue;
        if (strcasecmp($value->name, "odnoklassniki") == 0 && $isOdnoklassniki)
            continue;
        if (strcasecmp($value->name, "instagramm") == 0 && $isInstagramm)
            continue;
        if (strcasecmp($value->name, "twitter") == 0 && $isTwitter)
            continue;
        if (strcasecmp($value->name, "pinterest") == 0 && $isPinterest)
            continue;
        if (strcasecmp($value->name, "tumblr") == 0 && $isTumblr)
            continue;
        if (strcasecmp($value->name, "linkedin") == 0 && $isLinkedin)
            continue;
        $communications[$value->id] = $value;
    }

    //print_r($company);

?>
<script>
    var portionSizeValue = 3;
    function UpdateNewsList(idCompanyValue) {
        portionSizeValue += 1;
        params =
        {
            idCompany: idCompanyValue,
            portionSize: portionSizeValue
        }
        SetUnivaersalJSONAjax(params, 'companyNewsContainer1', document.getElementById("idNewsListType").value, 'showCompanyNews');
    }
    $(document).ready(function () {
        $(window).scroll(function () {
            //console.log($(window).scrollTop() + $(window).height()-150);
            //console.log($(document).height());
            if ($(window).scrollTop() + $(window).height() - 150 > $(document).height()
                || ($(window).scrollTop() + $(window).height() + 150 > $(document).height())) {
                UpdateNewsList(document.getElementById('idCompany').value);
            }
        });
    });
</script>

<script type="text/javascript">


</script>



<div class="content clearfix">

<div id="<?php echo "modal-8SocialMessage" ?>" class="modal" data-modal-effect="flip-vertical">
    <div class="modal-content">
        <?php
        include_once '../messages/SocialMessage.php';
        ?>
    </div>
</div>

<?php

$isShow="0";
$addedId="companyAbout";
$headerMessage = "Ошибка ввода данных";
$contentMessage1 = "Вы ввели менее 150 символов в поле 'О компании'";
$contentMessage2 = "";
include '../lib/MessageBox.php';

?>

<!--SetUnivaersalJSONAjax3('../ajaxPages/crud.php','updateCompanyImg','','');-->
<div class="about_company clearfix ">
    <img class=" layer_55 " id="imgCompanyLogo" alt="" width="216" height="216"/>
	<script>
		var options1 = {
			target:   '',   // target element(s) to be updated with server response
			beforeSubmit:  beforeSubmit1,  // pre-submit callback
			success:       afterSuccess1,  // post-submit callback
			resetForm: true        // reset the form after successful submit
		};

		function afterSuccess1(data)
		{
			//console.log(JSON.parse( data ));
			var str = JSON.parse( data );
			SetCompany1(document.getElementById('idCompany').value, str.toString());
		}

		//function to check file size before uploading.
		function beforeSubmit1(){
		}
	</script>
    <div style="height: 40">
        <form action="../ajaxPages/crud.php" method="post" enctype="multipart/form-data" id="MyUploadForm1">
            <input type="hidden" name="MAX_FILE_SIZE" value="314600000" />
            <input name="ImageFile" id="imageInput" type="file"
				   onchange="$('#MyUploadForm1').ajaxSubmit(options1);"
				   style="width: 32;display: none"/>
            <input type="hidden" name="idCompany" value="<?php echo $currentCompany->id; ?>"/>
            <input type="hidden" name="action" value="updateCompanyImg"/>
            <input type="hidden" name="previouslogo" value="<?php echo $currentCompany->logo;?>"/>
            <div class="btnEdit clickable "
                 onclick="$('#imageInput').click();" style="margin-left: -30"></div>
        </form>

    </div>


    <div class="c_wrapper13 clearfix ">
        <div align="left" style="margin-top: -60;margin-bottom:20;width: 100%;
        font-family: 'Trebuchet MS', Helvetica, sans-serif;font-size: 169.23%;color: #373737;" class=" textParagraph">
            <p class=""><strong id="nameCompany"><?php echo $currentCompany->name; ?></strong></p>
        </div>

        <script>
			data = <?php echo json_encode((array)$company); ?>;
			data.isEdit = false;
			data.aboutCount = 0;
		</script>
        <div><?php include_once 'Views/ctrCompanyAbout.php';?> </div>

        <div class="c_wrapper28 clearfix container">
            <div class=" commonTextFont107 greyText textLeft" style="padding-bottom: 20">
                <p class="">Следуйте за нами:</p>
            </div>

            <?php $btnActionOpenSocialMessage = count($communications) != 0 ? "modules.modals.openModal($('.modal-trigger'),'modal-8SocialMessage');" : ""; ?>
            <?php if (count($communications) != 0) { ?>
                <div class='btnAdd clickable modal-trigger' id="idShowModalWindowSocialMessageAdd"
                     onclick="<?php echo $btnActionOpenSocialMessage; ?>"></div>
            <?php } ?>

            <?php if ($isFacebook == "1") { ?>
                <div class="one mainFacebook layer_57 c_wrapper28-inner" onclick="">
                    <div
                        style="background: url('images/facebook.png') no-repeat; background-size: 100%;width:40;height:40;top:0;left:-2;top:-1"
                        class="blockRelative"></div>
                    <div class="blockRelative one1 innerFacebook" style="top:-38;left:60" id="idFacebook">
                        <input type="text" class="commonInputTextBox" id="idFacebookUser"
                               value="<?php echo $facebook; ?>" style="height: 30;width: 200"
                               placeholder="https://www.facebook.com/"/>
                    </div>
                </div>
            <?php } ?>

            <?php if ($isGooglePlus == "1") { ?>
                <div class="one layer_57 c_wrapper28-inner mainGooglePlus" onclick="" style="margin-left: 3">
                    <div
                        style="background: url('images/layer_59.png') no-repeat; background-size: 100%;width:40;height:40;top:0;left:-2;top:-1"
                        class="blockRelative"></div>
                    <div class="blockRelative one1 innerGooglePlus" style="top:-38;left:60" id="idGooglePlus">
                        <input type="text" class="commonInputTextBox" id="idGooglePlusUser"
                               value="<?php echo $googlePlus ?>" style="height: 30;width: 200"
                               placeholder="https://plus.google.com/"/>
                    </div>
                </div>
            <?php } ?>

            <?php if ($isVkontakte == "1") { ?>
                <div class="one layer_57 c_wrapper28-inner mainVk" onclick="" style="margin-left: 3">
                    <div
                        style="background: url('images/layer_59vk.png') no-repeat; background-size: 100%;width:40;height:40;top:0;left:-2;top:-1"
                        class="blockRelative"></div>
                    <div class="blockRelative one1 innerVk" style="top:-38;left:60" id="idVk">
                        <input type="text" class="commonInputTextBox" id="idVkUser" value="<?php echo $vkontakte ?>"
                               style="height: 30;width: 200" placeholder="http://vk.com/"/>
                    </div>
                </div>
            <?php } ?>

            <?php if ($isOdnoklassniki == "1") { ?>
                <div class="one layer_57 c_wrapper28-inner mainOd" onclick="" style="margin-left: 3">
                    <div
                        style="background: url('../images/odnoklassniki.png') no-repeat; background-size: 100%;width:42;height:42;top:-3;left:-3;"
                        class="blockRelative"></div>
                    <div class="blockRelative one1 innerOd" style="top:-38;left:60" id="idOd">
                        <input type="text" class="commonInputTextBox" id="idOdUser" value="<?php echo $odnoklassniki ?>"
                               style="height: 30;width: 200" placeholder="http://www.odnoklassniki.ru/"/>
                    </div>
                </div>
            <?php } ?>

            <?php if ($isInstagramm == "1") { ?>
                <div class="one layer_57 c_wrapper28-inner mainInsta" onclick="" style="margin-left: 3">
                    <div
                        style="background: url('../images/instagramm.png') no-repeat; background-size: 100%;width:43;height:43;top:-4;left:-4;"
                        class="blockRelative"></div>
                    <div class="blockRelative one1 innerInsta" style="top:-38;left:60" id="idInsta">
                        <input type="text" class="commonInputTextBox" id="idInstaUser" value="<?php echo $instagramm ?>"
                               style="height: 31;width: 200" placeholder="http://instagram.com/"/>
                    </div>
                </div>
            <?php } ?>

            <?php if ($isTwitter == "1") { ?>
                <div class="one layer_57 c_wrapper28-inner mainTwitter" onclick="" style="margin-left: 3">
                    <div
                        style="background: url('../images/_twitter.png') no-repeat; background-size: 100%;width:43;height:43;top:-5;left:-5;"
                        class="blockRelative"></div>
                    <div class="blockRelative one1 innerTwitter" style="top:-38;left:60" id="idTwitter">
                        <input type="text" class="commonInputTextBox" id="idTwitterUser" value="<?php echo $twitter ?>"
                               style="height: 30;width: 200" placeholder="https://twitter.com/"/>
                    </div>
                </div>
            <?php } ?>

            <?php if ($isPinterest == "1") { ?>
                <div class="one layer_57 c_wrapper28-inner mainPinterest" onclick="" style="margin-left: 3">
                    <div
                        style="background: url('../images/pinterest.png') no-repeat; background-size: 100%;width:42;height:42;top:-3;left:-3;"
                        class="blockRelative"></div>
                    <div class="blockRelative one1 innerPinterest" style="top:-38;left:60" id="idPinterest">
                        <input type="text" class="commonInputTextBox" id="idPinterestUser" value="<?php echo $pinterest ?>"
                               style="height: 30;width: 200" placeholder="https://www.pinterest.com/"/>
                    </div>
                </div>
            <?php } ?>

            <?php if ($isTumblr == "1") { ?>
                <div class="one layer_57 c_wrapper28-inner mainTumblr" onclick="" style="margin-left: 3">
                    <div
                        style="background: url('../images/tumblr.png') no-repeat; background-size: 100%;width:42;height:42;top:-3;left:-3;"
                        class="blockRelative"></div>
                    <div class="blockRelative one1 innerTumblr" style="top:-38;left:60" id="idTumblr">
                        <input type="text" class="commonInputTextBox" id="idTumblrUser" value="<?php echo $tumblr ?>"
                               style="height: 30;width: 200" placeholder="https://www.tumblr.com/"/>
                    </div>
                </div>
            <?php } ?>

            <?php if ($isLinkedin == "1") { ?>
                <div class="one layer_57 c_wrapper28-inner mainLinkedin" onclick="" style="margin-left: 3">
                    <div
                        style="background: url('../images/linkedin.png') no-repeat; background-size: 100%;width:43;height:43;top:-4;left:-4;"
                        class="blockRelative"></div>
                    <div class="blockRelative one1 innerLinkedin" style="top:-38;left:60" id="idLinkedin">
                        <input type="text" class="commonInputTextBox" id="idLinkedinUser" value="<?php echo $linkedin ?>"
                               style="height: 30;width: 200" placeholder="https://www.linkedin.com/"/>
                    </div>
                </div>
            <?php } ?>

            <input id="idCompany" type="hidden" value="<?php echo $currentCompany->id; ?>"/>

            <div id="idRes"></div>

            <script src='../services/servicesScript.js'></script>
            <script src="../js/indexAnimatedButton.js"></script>

        </div>
    </div>
</div>

<img class=" layer_60" src="images/layer_60.jpg" alt="" width="1000" height="1"/>

<div class="wrapper1 " style="margin-bottom: 20">

<div class="kontakty_kompanii clearfix ">


<div class="container ">
    <div><p class=" kontaktnye_lica"><strong>Контактные Лица</strong></p></div>
    <div style="width: 60"></div>
    <?php if (count($currentCompany->persons) < 3) { ?>
        <div class=" name btnAdd clickable" style=""
             onclick="ChangeSlideDownAnimation('idBlockContacts','name-help')"></div>
    <?php } ?>
</div>

<div class="blockRelative" style="height: 5;width: 250;top:3">
    <img class="" src="images/layer_61.jpg" alt="" width="310" height="1"/>
</div>
<div class="blockRelative">

    <div class="name-help " style="display:none;width:300;height:300;" id="idBlockContacts">
        <form action="../ajaxPages/crud.php" method="post" enctype="multipart/form-data" id="MyUploadForm">
            <div class="container blockPadding">
                <div class="commonTextFont" style="width:100">Имя</div>
                <div><input class="commonInputTextBox" type="text" placeholder="Имя" id="name" name="name"/></div>
            </div>
            <div class="container blockPadding">
                <div class="commonTextFont" style="width:100">Должность</div>
                <div><input class="commonInputTextBox" type="text" placeholder="Должность" id="position" name="position"/></div>
            </div>
            <div class="container blockPadding">
                <div class="commonTextFont" style="width:100">Телефон</div>
                <div><input class="commonInputTextBox" type="text" placeholder="Телефон" id="phone" name="phone"/></div>
            </div>
            <div class="container blockPadding">
                <div class="commonTextFont" style="width:100">Email</div>
                <div><input class="commonInputTextBox" type="text" placeholder="Email" id="email" name="email"/></div>
            </div>
            <div class="container blockPadding">
                <div class="commonTextFont" style="width:100">Skype</div>
                <div><input class="commonInputTextBox" type="text" placeholder="Skype" id="skype" name="skype"/></div>
            </div>
            <div class="container blockPadding" style="margin-top: 10">
                <div class="commonTextFont" style="width:100">Фотография</div>

                <style>
                    div.upload {
                        overflow: hidden;
                        cursor: hand;
                    }

                    div.upload input {
                        opacity: 0 !important;
                        cursor: hand;
                        overflow: hidden !important;
                    }
                </style>
                <div class="item" style="margin-top: 5">
                    <input type="hidden" name="MAX_FILE_SIZE" value="314600000" />
                    <div class="logoSelect upload clickable" style="margin-left: 1">
                        <input name="ImageFile" id="imageInput" type="file" style="width: 32;cursor: hand"/>
                    </div>
                </div>
            </div>
            <input type="hidden" name="idCompany" value="<?php echo $currentCompany->id; ?>"/>
            <input type="hidden" id="idContactId" name="idContactId" value="default"/>
            <input type="hidden" name="action" value="createContactByCompany"/>
            <script>
                function saveContact() {
                    idContactEdit = document.getElementById('idContactEdit').value;
                    contact = {
                        name: document.getElementById('name'.concat(idContactEdit)).value,
                        phone: document.getElementById('phone'.concat(idContactEdit)).value,
                        email: document.getElementById('email'.concat(idContactEdit)).value,
                        skype: document.getElementById('skype'.concat(idContactEdit)).value,
                        position: document.getElementById('position'.concat(idContactEdit)).value,
                        idCompany: document.getElementById('idCompany').value,
                        idContact: document.getElementById('idContactId'.concat(idContactEdit)).value
                    };
					SetUnivaersalJSONAjax3('../ajaxPages/crud.php','createContactByCompany',contact,function(data){
						var str = JSON.parse(data);
						SetCompany1(document.getElementById('idCompany').value, str.toString());
					});
                }
                function deleteContact(id) {
                    contact = {
                        idCompany: document.getElementById('idCompany').value,
                        idContact: id
                    };
					SetUnivaersalJSONAjax3('../ajaxPages/crud.php','deleteContactByCompany',contact,function(data){
						var str = JSON.parse(data);
						SetCompany1(document.getElementById('idCompany').value, str.toString());
					});

					//SetUnivaersalJSONAjax(contact, '', '../ajaxPages/crud.php', 'deleteContactByCompany');
                    //SetAjaxRequest("showCompany", document.getElementById('idCompany').value);
                }
            </script>
            <div class="container blockPadding">
                <div class="wrapper_hover" style="float: right">
                    <div class="btnAbout clickable" style="width:100" id="submit-btn"
                         onclick="
                         	$('#MyUploadForm').ajaxSubmit(options)
                         ">
						Сохранить</div>
                </div>
            </div>
        </form>

        <script>
            var options = {
                target:   '',   // target element(s) to be updated with server response
                beforeSubmit:  beforeSubmit,  // pre-submit callback
                success:       afterSuccess,  // post-submit callback
                resetForm: true        // reset the form after successful submit
            };

            function afterSuccess(data)
            {
				//console.log(data);
				//SetAjaxRequest("showCompany",document.getElementById('idCompany').value);
				var str = JSON.parse(data);
				SetCompany1(document.getElementById('idCompany').value, str.toString());
            }

            //function to check file size before uploading.
            function beforeSubmit(){
            }

            //function to format bites bit.ly/19yoIPO
            function bytesToSize(bytes) {
                var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
                if (bytes == 0) return '0 Bytes';
                var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
                return Math.round(bytes / Math.pow(1024, i), 2) + ' ' + sizes[i];
            }

        </script>

    </div>
    <div class="blockRelative" style="width:100">
        <?php
        foreach ($currentCompany->persons as $person) {
            $res = "<p class=' layer38_050_935_26_23_mas'>";
            foreach ($person->communications as $communication) {
                $res .= $communication->value . "<br style='margin-bottom: 0.0; line-height: 10.0px; display: block; content: ' '; />";
            }
            $res .= "</p>";
            $idContactEl = "idcontact" . $person->id;
            $idContactElMain = "idcontactMain" . $person->id;
            $btnEditAction = "
				document.getElementById('idContactEdit').value = $person->id;
				$('.'.concat('name-help')).slideUp(500);$('.'.concat('name-helpMain')).slideDown(500);ChangeSlideDownAnimation('$idContactEl','name-help');ChangeSlideDownAnimation('$idContactElMain','name-help')";
            $btnDeleteAction = "modules.modals.openModal($('.modal-trigger'),'modal-8contactDelete" . $person->id . "');";
            echo "<div class='container name-helpMain' style='margin-top:10;margin-bottom:10' id='$idContactElMain'>"

                . "<div><img class=' layer_1_copy_23' src='../uploads/companyContacts/".$person->id.".jpg' width='136' height='166' /></div>"
                . "<div class='' style='width:10'>&nbsp;&nbsp;&nbsp; </div>"
                . "<div class='' style=''>"
                . "<div class='container'>"
                . "<div style='width:90'>"
                . "<div class='textLeft commonTextFont107' style='margin-bottom:3'><strong>" . $person->firstname . "</strong></div>"
                . "<div class='textLeft'>" . $person->position . "</div>"
                . "</div>";
            ?>
            <div class='btnEdit clickable ' onclick="<?php echo $btnEditAction; ?>"></div>
            <div class='btnDelete clickable modal-trigger' onclick="<?php echo $btnDeleteAction; ?>"></div>
            <?php
            echo "</div>"
                . "<div><img class=' layer_61_copy' src='images/layer_61_copy.jpg' width='101' height='1' /></div>"
                . "<div>" . $res . "</div>"
                . "</div>"
                . "</div>";
            ?>
            <?php
            $isShow = "0";
            $headerMessage = "Удаление контактного лица компании";
            $contentMessage1 = "Вы уверены, что хотите удалить контактное лицо компании " . $currentCompany->name . " : " . $person->firstname;
            $actionBtnOk = "deleteContact('" . $person->id . "')";
            $btnCancel = "<div style='top:20 ;left:180' class='btn btn-primary btn-medium modal-close blockRelative commonTextFont'
								onclick=modules.modals.closeModal($('.modal-trigger'),'modal-8');>Отмена</div>";
            $addedId = "contactDelete" . $person->id;
            include '../lib/MessageBox.php';
            ?>
            <div class="name-help " style="display:none;width:300;height:250;"
                 id="<?php echo "idcontact" . $person->id; ?>">
                <div class="container blockPadding">
                    <div class="commonTextFont" style="width:100">Имя</div>
                    <div><input class="commonInputTextBox" type="text" placeholder="Имя"
                                id="<?php echo "name" . $person->id; ?>" value="<?php echo $person->firstname ?>"/>
                    </div>
                </div>
                <div class="container blockPadding">
                    <div class="commonTextFont" style="width:100">Должность</div>
                    <div><input class="commonInputTextBox" type="text" placeholder="Должность"
                                id="<?php echo "position" . $person->id; ?>" value="<?php echo $person->position ?>"/>
                    </div>
                </div>
                <div class="container blockPadding">
                    <div class="commonTextFont" style="width:100">Телефон</div>
                    <?php
                    $text = "";
                    foreach ($person->communications as $key => $value) {
                        if ($value->name == "Phone") {
                            $text = $value->value;
                            break;
                        }
                    }
                    ?>
                    <div><input class="commonInputTextBox" type="text" placeholder="Телефон"
                                id="<?php echo "phone" . $person->id; ?>" value="<?php echo $text; ?>"/></div>
                </div>
                <div class="container blockPadding">
                    <div class="commonTextFont" style="width:100">Email</div>
                    <?php
                    $text = "";
                    foreach ($person->communications as $key => $value) {
                        if ($value->name == "Email") {
                            $text = $value->value;
                            break;
                        }
                    }
                    ?>
                    <div><input class="commonInputTextBox" type="text" placeholder="Email"
                                id="<?php echo "email" . $person->id; ?>" value="<?php echo $text; ?>"/></div>
                </div>
                <div class="container blockPadding">
                    <div class="commonTextFont" style="width:100">Skype</div>
                    <?php
                    $text = "";
                    foreach ($person->communications as $key => $value) {
                        if ($value->name == "Skype") {
                            $text = $value->value;
                            break;
                        }
                    }
                    ?>
                    <div><input class="commonInputTextBox" type="text" placeholder="Skype"
                                id="<?php echo "skype" . $person->id; ?>" value="<?php echo $text; ?>"/></div>
                </div>
                <input type="hidden" name="idCompany" value="<?php echo $currentCompany->id; ?>"/>
                <input type="hidden" name="idContactId" id="<?php echo "idContactId" . $person->id; ?>"
                       value="<?php echo $person->id; ?>"/>

                <div class="container blockPadding">
                    <div class="wrapper_hover" style="float: right">
                        <div class="btnAbout clickable" style="width:100" onclick="<?php echo "saveContact" ?>()">
                            Сохранить
                        </div>
                    </div>
                </div>
            </div>
        <?php
        }
        ?>
        <input type="hidden" name="idContactEdit" id="idContactEdit" value=""/>
    </div>
</div>

<div class="container  ">
    <div><p class=" kontaktnye_lica"><strong>Адрес компании</strong></p></div>
    <div style="width: 60"></div>
    <?php //print_r($currentCompany);
    if ($currentCompany->region == NULL && $currentCompany->community == NULL) {
        ?>
        <div class=" name1 btnAdd clickable" style=""
             onclick="$('.'.concat('name-help')).slideUp(500);ChangeSlideDownAnimation('idBlockAddress','name-help')"></div>
    <?php } ?>
</div>

<div class="blockRelative" style="height: 5;width: 310;top:3">
    <img class="" src="images/layer_61.jpg" alt="" width="310" height="1"/>
</div>

<div class="name-helpAddress">
    <?php
        if ($currentCompany->region != NULL || $currentCompany->community != NULL) {
    ?>
        <div class=" container commonTextFont107 greyText" style='margin-top:10;width:300'>
            <div class=""><?php echo $currentCompany->region . ", "; ?></div>
            <div><?php echo $currentCompany->community; ?></div>
            <div class='btnEdit clickable ' style='float:right'
                 onclick="$('.'.concat('name-helpAddress')).slideUp(500);ChangeSlideDownAnimation('idBlockAddress','name-help')"></div>
        </div>
        <?php
        foreach ($currentCompany->communications as $key => $value) {
            if(!$value->IsNetworkTypes($value->name))
                continue;

            if ($value->Name != "Facebook" || $value->Name != "Vkontakte" || $value->Name != "Googleplus")
                echo "<div class='commonTextFont107 greyText textLeft' style='margin-top:5'>" . $value->value . "</div>";
        }
    }
    ?>
</div>

<div class="">
    <div class="name-help1 " style="display:none;width:300;height:100;" id="idBlockAddress">
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:150;">Полное название компании</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Полное название компании" id="companyName"
                        value="<?php echo $currentCompany->name; ?>"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:150 ">Регион</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Имя" id="companyRegion"
                        value="<?php echo $currentCompany->region; ?>"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style=" width:150">Город</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Имя" id="companyCommunity"
                        value="<?php echo $currentCompany->community; ?>"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style=" width:150">Email</div>
            <?php
            $text = "";
            foreach ($currentCompany->communications as $key => $value) {
                if ($value->name == "Email") {
                    $text = $value->value;
                    break;
                }
            }
            ?>
            <div><input class="commonInputTextBox" type="text" placeholder="Должность" id="companyEmail"
                        value="<?php echo $text; ?>"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style=" width:150">Телефон</div>
            <?php
            $text = "";
            foreach ($currentCompany->communications as $key => $value) {
                if ($value->name == "Phone") {
                    $text = $value->value;
                    break;
                }
            }
            ?>
            <div><input class="commonInputTextBox" type="text" placeholder="Телефон" id="companyPhone"
                        value="<?php echo $text; ?>"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:150">Сайт</div>
            <?php
            $text = "";
            foreach ($currentCompany->communications as $key => $value) {
                if ($value->name == "Skype") {
                    $text = $value->value;
                    break;
                }
            }
            ?>
            <div><input class="commonInputTextBox" type="text" placeholder="Телефон" id="companySkype"
                        value="<?php echo $text; ?>"/></div>
        </div>
        <script>
            function saveAddress() {
                info = {
                    companyName : document.getElementById('companyName').value,
                    companyRegion: document.getElementById('companyRegion').value,
                    companyCommunity: document.getElementById('companyCommunity').value,
                    companyEmail: document.getElementById('companyEmail').value,
                    companyPhone: document.getElementById('companyPhone').value,
                    companySkype: document.getElementById('companySkype').value,
                    idCompany: document.getElementById('idCompany').value
                };
				SetUnivaersalJSONAjax3('../ajaxPages/crud.php','createAddressByCompany',info,function(data){
					var str = JSON.parse(data);
					SetCompany1(document.getElementById('idCompany').value, str.toString());
				});
            }
        </script>
        <div class="container blockPadding">
            <div class="wrapper_hover" style="float: right">
                <div class="btnAbout clickable" style="width:100"
                     onclick="saveAddress();">Сохранить
                </div>
            </div>
        </div>
    </div>

</div>


</div>

<img class=" layer_60_copy " src="images/layer_60_copy.png" alt="" width="1000" height="1"/>

<div class="clearfix" id="container" style="left:0;">
    <?php
    $test = 'document.getElementById("idNewsListType").value="newslist.php";SetAjaxRequest("showCompanyNews","' . $currentCompany->id . '")';
    echo "<div class='clearfix companyTab hoverBlock' onclick='" . $test . "' >Тексты</div>";
    echo "<div class='space'></div>";

    $test = 'document.getElementById("idNewsListType").value="newslist2.php";SetUnivaersalAjax(' . $currentCompany->id . ',"companyNewsContainer1","newslist2.php")';
    echo "<div class='clearfix companyTab hoverBlock' onclick='" . $test . "' >Управление публикациями</div>";
    echo "<div class='space'></div>";
    echo "<div class='clearfix' style='width:100;'></div>";
    ?>
    <input type="hidden" id="idNewsListType" value="newslist.php"/>
</div>


<form method="post" action="<?php echo GetPageByPlace("ShowNewsAdapteeForm" . $_SERVER['PHP_SELF']); ?>">
    <div class="companyNewsPublishPR  wrapper_hover">
        <input type="submit" class="borderNone clickable publishPR" value="Опубликовать ПР"/>
    </div>
</form>


<div class="clearfix companyNewsContainer " id="companyNewsContainer1">
    <?php
        include_once "newslist.php";
    ?>
</div>
</div>

<div class="layer_0_copy_2-holder clearfix footerStyle" style="top:140;margin-left:-130">
    <?php
    include_once "../footer/footer.php";
    ?>
</div>

</div>
</div>

<script type='text/javascript' src='ViewModels/CompanyTabVm.js'></script>


