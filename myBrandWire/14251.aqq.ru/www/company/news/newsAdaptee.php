<?php session_save_path("../../tmp");session_start();?>
<?php
    include_once '../../lib/csFunctions.php';
    include_once "../../model/controller.php";
    InitGlobServerRedirectConstants();
    $controller = new Controller();
    $categories = $controller->GetCategories();
    $user = unserialize($_SESSION["userObject"]);
    $companies = $user->companies;

    //print_r($_POST);

    $mode = "normal";
    $editableNews = "";
    $idsCategory = "";
    $namesCategory = "";
    if ($_POST["mode"] == "edit") {
        $mode = "edit";
        $editableNews = $controller->GetNewsById($_POST["id"]);
        if (isset($_POST["checkboxPublish"])) {
            $editableNews->order->payment = "1";
        }
        foreach ($editableNews->categories as $key => $value) {
            $idsCategory = $idsCategory . $value->id . ";";
            $namesCategory = $namesCategory . $value->name . ";";
        }
    }
    if ($_POST["mode"] == "change") {
        $mode = "change";
        $order = unserialize($_SESSION["order"]);
        $editableNews = $order->news;
        if(!is_null($editableNews->categories)){
            foreach ($editableNews->categories as $key => $value) {
                $idsCategory = $idsCategory . $value->id . ";";
                $namesCategory = $namesCategory . $value->name . ";";
            }
        }
    }
    //print_r($editableNews->categories);
    //$_SESSION["editableNews"]=serialize($editableNews);

    $onClickCheckboxSocialNetworksCommon =
    "selectedCompanyId='';for(i=0;i<document.getElementsByName('companyId').length;i++)
        if(document.getElementsByName('companyId')[i].checked)
            selectedCompanyId=document.getElementsByName('companyId')[i].value;";
    $onClickCheckboxSocialNetworks8 =$onClickCheckboxSocialNetworksCommon."SetUnivaersalJSONAjax2(selectedCompanyId, 'idBlockContacts', '../../ajaxPages/crud.php', 'showCompanyContacts',ShowMessageEnteredEnoughCompanyData);";
    $onClickCheckboxSocialNetworks7 =$onClickCheckboxSocialNetworksCommon."pars={selectedCompanyId:selectedCompanyId,action:par};SetUnivaersalJSONAjax2(pars, 'idBlockContacts2', '../../ajaxPages/crud.php', 'checkCompanyInfo',ShowMessageEnteredEnoughCompanyData2);";

    echo "<script> contactsInner = ".implode(",",$_POST["contacts"])."</script>";
?>
    <html>
    <head>
        <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
    <head>
        <link rel="shortcut icon" href="../../images/favicon.ico">

        <link rel="stylesheet" type="text/css" href="../../company/style.css"/>
        <link rel="stylesheet" type="text/css" href="../../commonStyles.css"/>
        <link rel="stylesheet" type="text/css" href="../../header/style.css"/>
        <link rel="stylesheet" type="text/css" href="style.css"/>
        <link rel="stylesheet" type="text/css" href="styleComboBox.css"/>
        <link rel="stylesheet" type="text/css" href="../../lib/jq/jquery-ui-1.8.13.custom.css">
        <link rel="stylesheet" type="text/css" href="../../lib/jq/ui.dropdownchecklist.themeroller.css">
        <link rel="stylesheet" href="../../css/styleModalWindow.css" media="screen" type="text/css"/>

        <script type="text/javascript" src="../../lib/jq/jquery-1.6.1.minOLD.js"></script>
        <script type="text/javascript" src="../../lib/jq/jquery-1.6.1.minOLD.js"></script>
        <script type="text/javascript" src="../../lib/jq/jquery-ui-1.8.13.custom.min.js"></script>
        <script type="text/javascript" src="src/ui.dropdownchecklist.js"></script>

        <script type="text/javascript" src="../../services/servicesScript.js"></script>
        <script src="../../lib/tinymce/js/tinymce/tinymce.min.js"></script>


        <script>
            tinymce.init({
                selector: 'textarea.editme',
                plugins: 'link image code',
                relative_urls: false,
                width: 700,
                height: 200,
                resize: false
            });
        </script>
        <script>
            function SetCompany1(id, ids) {
                var idsRes = ids.split("#");

                if (idsRes.length == 1) {
                    document.getElementById('cmp' + idsRes[0]).checked = true;
                    return;
                }

                for (var i = 0; i < idsRes.length; i++) {
                    if (idsRes[i] == id) {
                        if (document.getElementById('cmp' + idsRes[i]).checked == false) {
                            document.getElementById('cmp' + idsRes[i]).checked = true;
                            cmpId = '#cmp'.concat(idsRes[i]);
                            $(cmpId).attr('required', 'required');
                        }
                        continue;
                    }
                    cmpId = '#cmp'.concat(idsRes[i]);
                    $(cmpId).removeAttr('required');

                    document.getElementById('cmp' + idsRes[i]).checked = false;
                }
                ;
            }
            ;
        </script>
        <script type="text/javascript">
            $(document).ready(function () {

                $returnS6 = $('#returnS6');
                $returnS7 = $('#returnS7');
                $("#s6").dropdownchecklist({
                    onItemClick: function (checkbox, selector) {
                        //console.log(checkbox.checked);
                        //console.log(selector);
                        var thisIndex = checkbox.attr("id").split('-')[2].replace('i', '');
                        selector.options[thisIndex].selected = checkbox.attr("checked");

                        var values = "";
                        var newText = "";
                        count = 0;
                        for (i = 0; i < selector.options.length; i++) {
                            //newText += 'Option i = ' +i+ ' || value = ' +selector.options[i].value+ ' || checked = ' +selector.options[i].selected+ '<br/><br/>';
                            if (selector.options[i].selected && (selector.options[i].value != "") && count <= 3) {
                                if (values != "")
                                    values += ";";
                                values += selector.options[i].value;
                                count++;
                            }
                            else if (count > 3) {
                                console.log(count);
                                document.getElementById(checkbox.attr("id")).checked = false;
                                break;
                            }

                        }

                        //newText += 'Selector Value = '+values;
                        newText += values;
                        //$returnS6.html( newText );
                        //console.log(newText);
                        document.getElementById("returnS7").value = newText;
                    },
                    maxDropHeight: 200
                });

                $('select option').removeProp('selected');

            });
        </script>
        <script>
            function ShowMessageEnteredEnoughCompanyData(){
                if(document.getElementsByName('contacts[]').length==0){
                    modules.modals.openModal($('.modal-trigger'),'modal-8contactCheckbox');
                    $('#checkboxSocialNetworks8').attr('checked',false);
                }
                else{
                    for(i=0;i<document.getElementsByName('contacts[]').length;i++){
                        if(contactsInner.toString().indexOf(document.getElementsByName('contacts[]')[i].value)>-1)
                            document.getElementsByName('contacts[]')[i].checked=true;
                    }

                }
            }
            function ShowMessageEnteredEnoughCompanyData2(){
                isShowBycheckboxSocialNetworks7 = $('#idCompanyAddress').text()==1;
                isShowBycheckboxSocialNetworks6 = $('#idCompanyAbout').text()==1;
                isShowBycheckboxSocialNetworks5 = $('#idCompanySocNetworks').text()==1;
                if(isShowBycheckboxSocialNetworks5||isShowBycheckboxSocialNetworks6||isShowBycheckboxSocialNetworks7){
                    modules.modals.openModal($('.modal-trigger'),'modal-8contactCheckbox');
                }
                if($('#idCompanyAddress').text()!="NONE")
                    $('#checkboxSocialNetworks7').attr('checked',!isShowBycheckboxSocialNetworks7);
                if($('#idCompanyAbout').text()!="NONE")
                    $('#checkboxSocialNetworks6').attr('checked',!isShowBycheckboxSocialNetworks6);
                if($('#idCompanySocNetworks').text()!="NONE")
                    $('#checkboxAddCompanySocialNetworks').attr('checked',!isShowBycheckboxSocialNetworks5);
            }
        </script>

    </head>
    <body onload="
        AnaliseAndSetElementsVisisbilityNewsAdaptee(
        '<?php echo $_SESSION["loginUser"] ?>',
        '<?php echo $_SESSION["idUser"] ?>',
        '<?php echo $editableNews->order->payment; ?>',
        '<?php echo $idsCategory ?>',
        '<?php echo $namesCategory ?>');">

    <input type="hidden" id="idIsShowContactMessage" value="false"/>

    <div class="global_container_ expandingBoxFix ">
        <?php
            include_once "../../header/headerNewsAdaptee.php";
        ?>
    <div class="adapteeNewsToTheMainNews  " id="idNewsAdapteeContentForm">

    <form  action="<?php echo GetPageByPlace("ShowPreviewNewsForm" . $_SERVER['PHP_SELF']); ?>" method="post">
    <div class="" >
        <div class="container  publishByNameContainer ">
            <div class="lbPublishByName commonTextFont textRight ">
                Публиковать от имени :
            </div>
            <?php
            $defaultValue = $_POST["mode"] == "edit" || "change" ? "false" : "true";
            $str = "";
            foreach ($companies as $key => $value) {
                $str = $str . $value->id . "#";
            }
            $str = substr($str, 0, -1);

            foreach ($companies as $key => $value) {
                $condition = $_POST["mode"] == "edit"
                    ? $editableNews->company->id == $value->id : true;
                if ($condition)
                    echo "<div class='blockPublishByCompanyName commonTextStyle'>"
                        . "<input required='required' onclick='SetCompany1($value->id,\"$str\")' type='checkbox' name='companyId' value='$value->id' id='cmp$value->id' />$value->name" . "</div>";
            }

            echo "<script> $str </script>";
            if ($mode == "edit" || $mode == "change") {
                echo "<script> document.getElementById('cmp" . $editableNews->company->id . "').onclick(); </script>";
            }
            ?>
        </div>

        <?php include_once '../../lib/php/controls/ctrServicesPrice1.php';?>

    </div>
        <div class="adapteeNewsForm blockRelative" id="" style="top:10">
            <?php include 'newsForm.php' ?>
        </div>
        <script src="../../js/indexCharacterCount.js"></script>

        <div class="horizontalSeparator localHorizontalSeparator">

        </div>
    </div>

    <div class="layer_0_copy_2-holder clearfix footerStyle" style="top:0;">
        <?php
        include_once "../../footer/footer.php";
        ?>
    </div>

    <?php
        $isShow="0";
        $addedId = "contactCheckbox";
        $headerMessage = "Недостаточно данных.";
        $contentMessage1 = "Для начала, Вам необходимо заполнить необходимую информацию на странице Вашей компании";
        include('../../lib/MessageBox.php');

        $isShow="0";
        $addedId = "";
        $headerMessage = "Не все данные введены корректно";
        $contentMessage1 = "Заголовок новости должен состоять не менее, чем 4 слова";
        include('../../lib/MessageBox.php');

		$isShow="0";
		$addedId = "";
		$addedId = "ctrMessageCategoryIsEmpty";
		$headerMessage = "Не все данные введены корректно";
		$contentMessage1 = "Выберите категорию новости";
		include('../../lib/MessageBox.php');

        $isShow="0";
        $addedId = "ctrMessageFreeNews";
        $headerMessage = "Информация";
        $contentMessage1 =
            "При бесплатной публикации новость будет доступна только посетителям нашего сайта.
            Новость так же не будет иметь картинок и активных ссылок.
            Вы действительно хотите опубликовать новость бесплатно?";
        $actionBtnOk="totalprice = 1;$('form').submit();";
        $btnCancel = '
            <div style="top:20 ;left:180"
                 class="btn btn-primary btn-medium modal-close blockRelative commonTextFont"
		        onclick="modules.modals.closeModal($(\'.modal-trigger\'),\'modal-8ctrMessageFreeNews\');">
                    Отмена
            </div>';

            include('../../lib/MessageBox.php');
        ?>

    </body>
    </html>

<?php ?>