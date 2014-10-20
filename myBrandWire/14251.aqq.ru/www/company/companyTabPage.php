<?php
include_once '../lib/csFunctions.php';
include_once '../model/controller.php';
include_once '../model/model.php';

$controller = Controller::getInstance();
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
		$isFacebook = CValidate::isValidUrl($facebook) ? "1" : "0";
    }
    if (strcasecmp($value->name, "odnoklassniki") == 0) {
        $odnoklassniki = $value->value;
        $isOdnoklassniki = "1";
		$isOdnoklassniki = CValidate::isValidUrl($odnoklassniki) ? "1" : "0";
    }
    if (strcasecmp($value->name, "googleplus") == 0) {
        $googlePlus = $value->value;
        $isGooglePlus = "1";
		$isGooglePlus = CValidate::isValidUrl($googlePlus) ? "1" : "0";
    }
    if (strcasecmp($value->name, "vkontakte") == 0) {
        $vkontakte = $value->value;
        $isVkontakte = "1";
		$isVkontakte = CValidate::isValidUrl($vkontakte) ? "1" : "0";
    }
    if (strcasecmp($value->name, "instagramm") == 0) {
        $instagramm = $value->value;
        $isInstagramm = "1";
		$isInstagramm = CValidate::isValidUrl($instagramm) ? "1" : "0";
    }
    if (strcasecmp($value->name, "twitter") == 0) {
        $twitter = $value->value;
        $isTwitter = "1";
		$isTwitter = CValidate::isValidUrl($twitter) ? "1" : "0";
    }
    if (strcasecmp($value->name, "pinterest") == 0) {
        $pinterest = $value->value;
        $isPinterest = "1";
		$isPinterest = CValidate::isValidUrl($pinterest) ? "1" : "0";
    }
    if (strcasecmp($value->name, "tumblr") == 0) {
        $tumblr = $value->value;
        $isTumblr = "1";
		$isTumblr = CValidate::isValidUrl($tumblr) ? "1" : "0";
    }
    if (strcasecmp($value->name, "linkedin") == 0) {
        $linkedin = $value->value;
        $isLinkedin = "1";
		$isLinkedin = CValidate::isValidUrl($linkedin) ? "1" : "0";
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

foreach ($communicationsAll as $key => $value) {
    if (strcasecmp($value->name, "email") == 0 || strcasecmp($value->name, "skype") == 0
        || strcasecmp($value->name, "phone") == 0 || strcasecmp($value->name, "twitter") == 0
    )
        continue;
    if (strcasecmp($value->name, "facebook") == 0 && $isFacebook)
        continue;
    if (strcasecmp($value->name, "googleplus") == 0 && $isGooglePlus)
        continue;
    if (strcasecmp($value->name, "vkontakte") == 0 && $isVkontakte)
        continue;
    $communications[$value->id] = $value;
}

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

<div class="content clearfix" style="margin-top: -50">

<div class="about_company clearfix ">
    <img class=" layer_55 " id="imgCompanyLogo" alt="" width="216" height="216" src="<?php echo $currentCompany->logo;?>"/>

    <div class="c_wrapper13 clearfix">
        <p class=" jay_roberts"><strong id="nameCompany"><?php echo $currentCompany->name; ?></strong></p>

        <p class=" lorem_ipsum_dolor_sit_ame" id="aboutCompany"><?php echo $currentCompany->about; ?></p>

        <div class="c_wrapper28 clearfix container">
            <div class=" commonTextFont107 greyText textLeft" style="padding-bottom: 20">
                <p class="">Следуйте за нами:</p>
            </div>

            <?php if ($isFacebook == "1") { ?>
                <a href="<?php echo $facebook; ?>" target="_blank">
                    <div class="layer_57 c_wrapper28-inner clickable " onclick="">
                        <img src="images/facebook.png" class="btnSocialNetworks"/>
                    </div>
                </a>
            <?php } ?>

            <?php if ($isGooglePlus == "1") { ?>
                <a href="<?php echo $googlePlus; ?>" target="_blank">
                    <div class="layer_57 c_wrapper28-inner clickable " onclick="">
                        <img src="images/layer_59.png" class="btnSocialNetworks"/>
                    </div>
                </a>
            <?php } ?>

            <?php if ($isVkontakte == "1") { ?>
                <a href="<?php echo $vkontakte; ?>" target="_blank">
                    <div class="layer_57 c_wrapper28-inner clickable " onclick="">
                        <img src="images/layer_59vk.png" class="btnSocialNetworks"/>
                    </div>
                </a>
            <?php } ?>

            <?php if ($isOdnoklassniki == "1") { ?>
                <a href="<?php echo $odnoklassniki; ?>" target="_blank">
                    <div class="layer_57 c_wrapper28-inner clickable " onclick="">
                        <img src="../images/odnoklassniki.png" class="btnSocialNetworks"/>
                    </div>
                </a>
            <?php } ?>

            <?php if ($isInstagramm == "1") { ?>
                <a href="<?php echo $instagramm; ?>" target="_blank">
                    <div class="layer_57 c_wrapper28-inner clickable " onclick="">
                        <img src="../images/instagramm.png" class="btnSocialNetworks"/>
                    </div>
                </a>
            <?php } ?>

            <?php if ($isTwitter == "1") { ?>
                <a href="<?php echo $twitter; ?>" target="_blank">
                    <div class="layer_57 c_wrapper28-inner clickable " onclick="">
                        <img src="../images/_twitter.png" class="btnSocialNetworks"/>
                    </div>
                </a>
            <?php } ?>

            <?php if ($isPinterest == "1") { ?>
                <a href="<?php echo $pinterest; ?>" target="_blank">
                    <div class="layer_57 c_wrapper28-inner clickable " onclick="">
                        <img src="../images/pinterest.png" class="btnSocialNetworks"/>
                    </div>
                </a>
            <?php } ?>

            <?php if ($isTumblr == "1") { ?>
                <a href="<?php echo $pinterest; ?>" target="_blank">
                    <div class="layer_57 c_wrapper28-inner clickable " onclick="">
                        <img src="../images/tumblr.png" class="btnSocialNetworks"/>
                    </div>
                </a>
            <?php } ?>

            <?php if ($isLinkedin == "1") { ?>
                <a href="<?php echo $pinterest; ?>" target="_blank">
                    <div class="layer_57 c_wrapper28-inner clickable " onclick="">
                        <img src="../images/linkedin.png" class="btnSocialNetworks"/>
                    </div>
                </a>
            <?php } ?>


            <input id="idCompany" type="hidden" value="<?php echo $currentCompany->id; ?>"/>

            <div id="idRes"></div>

            <script src='../js/globalJQ.js'></script>
            <script src='../services/servicesScript.js'></script>

        </div>
    </div>
</div>

<img class=" layer_60" src="images/layer_60.jpg" alt="" width="1000" height="1"/>

<div class="wrapper1 " style="margin-bottom: 20">

<div class="kontakty_kompanii clearfix ">


<div class="container ">
    <div><p class=" kontaktnye_lica"><strong>Контактные Лица</strong></p></div>
    <div style="width: 60"></div>
</div>

<div class="blockRelative" style="height: 5;width: 250;top:3">
    <img class="" src="images/layer_61.jpg" alt="" width="310" height="1"/>
</div>

<div class="blockRelative">

    <div class="name-help " style="display:none;width:300;height:250;" id="idBlockContacts">
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:100">Имя</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Имя" id="name"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:100">Должность</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Должность" id="position"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:100">Телефон</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Телефон" id="phone"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:100">Email</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Email" id="email"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:100">Skype</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Skype" id="skype"/></div>
        </div>
        <input type="hidden" name="idCompany" value="<?php echo $currentCompany->id; ?>"/>
        <input type="hidden" id="idContactId" name="idContactId" value="default"/>
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
            echo "<div class='container name-helpMain' style='margin-top:10;margin-bottom:10' id='$idContactElMain'>"

                . "<div><img class=' layer_1_copy_23' src='images/layer_1_copy_23.jpg' width='136' height='166' /></div>"
                . "<div class='' style='width:10'>&nbsp;&nbsp;&nbsp; </div>"
                . "<div class='' style=''>"
                . "<div class='container'>"
                . "<div style='width:90'>"
                . "<div class='textLeft commonTextFont107' style='margin-bottom:3'><strong>" . $person->firstname . "</strong></div>"
                . "<div class='textLeft'>" . $person->position . "</div>"
                . "</div>";
            ?>
            <?php
            echo "</div>"
                . "<div><img class=' layer_61_copy' src='images/layer_61_copy.jpg' width='101' height='1' /></div>"
                . "<div>" . $res . "</div>"
                . "</div>"
                . "</div>";
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
        </div>
        <?php
        foreach ($currentCompany->communications as $key => $value) {
            if ($value->Name != "Facebook" || $value->Name != "Vkontakte" || $value->Name != "Googleplus")
                echo "<div class='commonTextFont107 greyText textLeft' style='margin-top:5'>" . $value->value . "</div>";
        }
    }
    ?>
</div>

<div class="">

    <div class="name-help1 " style="display:none;width:300;height:100;" id="idBlockAddress">

        <div class="container blockPadding">
            <div class="commonTextFont" style="width:100">Регион1</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Имя" id="companyRegion"
                        value="<?php echo $currentCompany->region; ?>"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:100">Город</div>
            <div><input class="commonInputTextBox" type="text" placeholder="Имя" id="companyCommunity"
                        value="<?php echo $currentCompany->community; ?>"/></div>
        </div>
        <div class="container blockPadding">
            <div class="commonTextFont" style="width:100">Email</div>
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
            <div class="commonTextFont" style="width:100">Телефон</div>
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
            <div class="commonTextFont" style="width:100">Skype</div>
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
                    companyRegion: document.getElementById('companyRegion').value,
                    companyCommunity: document.getElementById('companyCommunity').value,
                    companyEmail: document.getElementById('companyEmail').value,
                    companyPhone: document.getElementById('companyPhone').value,
                    companySkype: document.getElementById('companySkype').value,
                    idCompany: document.getElementById('idCompany').value
                };
                SetUnivaersalJSONAjax(info, '', '../ajaxPages/crud.php', 'createAddressByCompany');
                SetAjaxRequest("showCompany", document.getElementById('idCompany').value);
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
    echo "<div class='clearfix companyTab hoverBlock'>Тексты</div>";
    echo "<div class='clearfix' style='width:300;'></div>";
    ?>
    <input type="hidden" id="idNewsListType" value="newslistPage.php"/>
</div>


<div class="clearfix companyNewsContainer " id="companyNewsContainer1">
    <?php
    include_once "newslistPage.php";
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