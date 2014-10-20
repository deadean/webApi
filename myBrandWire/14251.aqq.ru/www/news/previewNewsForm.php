<?php session_save_path("../tmp");session_start(); ?>
<?php
include_once '../lib/csFunctions.php';
InitGlobServerRedirectConstants();
include_once "../model/controller.php";
include_once "../model/mainController.php";
//print_r($_POST);

$controller = new Controller();

$mode = $_POST["mode"];
$companyName = "";
$header = "";
$categories = "";
$content = "";

$_POST["newsContent"] = CValidate::ValidateHtmlString($_POST["newsContent"]);
//echo $_POST["newsContent"];
$order = $controller -> CreateNewOrder($_POST["newsHeader"], $_POST["newsCommon"], $_POST["newsContent"],
    $_POST["newsTags"], $_POST["RES"], $_POST["companyId"], $_POST["newsComments"], isset($_POST["checkboxPublish"]),
    isset($_POST["checkboxAddCompanySocialNetworks"]),$_POST["contacts"],isset($_POST["checkboxSocialNetworks6"]),
    isset($_POST["checkboxSocialNetworks7"])
);
//print_r($order);

if ($mode == "edit") {
	$editableNews = $controller -> GetNewsById($_POST["id"]);
	$editableNews -> header = $order -> news -> header;
	$editableNews -> common = $order -> news -> common;
	$editableNews -> content = $_POST["newsContent"];
	$editableNews -> tags = $order -> news -> tags;
	$editableNews -> categories = $order -> news -> categories;
	$editableNews -> comments = $order -> news -> comments;
	$editableNews -> order -> payment = $order -> payment;

	$companyName = $editableNews -> company -> name;
	$header = $editableNews -> header;
	$content = $editableNews -> content;
	$categories = $editableNews -> categories;

	$_SESSION["editableNews"] = serialize($editableNews);
	//print_r($editableNews);
} else {
	$mode = "publish";

	$user = unserialize($_SESSION["userObject"]);
	$companies = $user -> companies;
	$order -> company = $companies[$_POST["companyId"]];
    $order -> news -> company = $companies[$_POST["companyId"]];
	$_SESSION["order"] = serialize($order);
	$companyName = $order -> company -> name;
	$header = $order -> news -> header;
	$content = $order -> news -> content;
	$categories = $order -> news -> categories;
}

//print_r($_POST);
?>

<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
		<link rel="stylesheet" type="text/css" href="../company/style.css" />
		<link rel="stylesheet" type="text/css" href="../commonStyles.css" />
		<link rel="stylesheet" type="text/css" href="../header/style.css" />
		<link rel="stylesheet" type="text/css" href="style.css" />
        <link rel="stylesheet" href="../css/styleModalWindow.css" media="screen" type="text/css"/>
		<link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css"/>
		<script type="text/javascript" src="../services/servicesScript.js"></script>
		<script type="text/javascript" src="../lib/jq/1.10.2/jquery-1.10.2.min.js"></script>
		<script type="text/javascript" src="../../lib/jq/1.10.2/jquery-1.10.2.min.js"></script>

	</head>

    <script>
        $(document).ready(function() { CountOrdersCost(); });
    </script>

	<body onload="AnaliseAndSetElementsVisisbility('<?php echo $_SESSION["loginUser"] ?>', '<?php echo $_SESSION["idUser"] ?>');">
		<div class="global_container_ expandingBoxFix" style="min-height: 800;height:100%">

			<?php
			    include_once '../header/headerMain.php';
			?>
            <form id="formBack" action="../company/news/newsAdaptee.php" method="post">
<!--                Dont declare here id for elements-->
                <?php if (isset($_POST["checkboxPublish"])) { ?>
                    <input type="hidden" name="checkboxPublish" >
                <?php } ?>
                <?php if (isset($_POST["checkboxSpelling"])) { ?>
                    <input type="hidden" name="checkboxSpelling" >
                <?php } ?>
                <?php if (isset($_POST["checkboxAddCompanySocialNetworks"])) { ?>
                    <input type="hidden" name="checkboxAddCompanySocialNetworks" >
                <?php } ?>
                <?php if (isset($_POST["checkboxSocialNetworks6"])) { ?>
                    <input type="hidden" name="checkboxSocialNetworks6" >
                <?php } ?>
                <?php if (isset($_POST["checkboxSocialNetworks7"])) { ?>
                    <input type="hidden" name="checkboxSocialNetworks7" >
                <?php } ?>
                <?php if (isset($_POST["checkboxSocialNetworks8"])) { ?>
                    <input type="hidden" name="checkboxSocialNetworks8" value="checkboxSocialNetworks8" >
                <?php } ?>
                <?php if (isset($_POST["contacts"])) { ?>
                    <input type="hidden" name="contacts[]" value="<? echo implode('#',$_POST['contacts']); ?>" >
                <?php } ?>
                <input type="hidden" name="mode" id="mode" value="change">
            </form>

			<div class=" emptyContent " style="top:5;height: 100%">

				<form method="post" id="form" action="<?php echo $_SERVER["PHP_SELF"]; ?>">
					<div class="textLeft commonTextFont textParagraph blockPadding " style="margin-top: 20;margin-bottom: 20">
						Предпросмотр Публикации
					</div>

					<div class="horizontalSeparator" style="margin-bottom: 0"></div>

					<div class="container" >

						<div class="" style="width: 200;border-right-style: solid;border-right-color: #dc8372;border-right-width: 1;">
							<div class="commonTextStyle " style="height: 334;">
								<div style="position: relative;top:20;">
									Выбранные услуги :
								</div>
							</div>
							<div class="commonTextStyle greyBackground " style="height: 40;padding-top: 20;">
								Публиковать от имени :
							</div>
							<div class="commonTextStyle  " style="height: 40;padding-top: 20;">
								Заголовок :
							</div>
							<div class="commonTextStyle greyBackground " style="height: 40;padding-top: 20;">
								Категория :
							</div>
							<?php if($mode != "edit"){ ?>
							<div class="commonTextStyle greyBackground  " style="height: 40;padding-top: 20;">
								Время и дата публикации :
							</div>
							<?php } ?>
						</div>


						<div>
							<div class="" style="">
								<?php include_once '../lib/php/controls/ctrServicesPrice1.php'?>
							</div>
							<div class="commonTextFont greyBackground boldText textLeft " style="height: 40;padding-top: 20;padding-left:40; width:760;margin-top:8">
								<?php echo $companyName; ?>
							</div>
							<div class="commonTextFont boldText textLeft" style="height: 40;padding-top: 20;padding-left:40; width:760">
								<?php echo $header; ?>
							</div>
							<div class="commonTextFont greyBackground boldText textLeft" style="height: 40;padding-top: 20;padding-left:40; width:760">
								<?php
								$res = "";
								if (!is_null($categories)) {
									foreach ($categories as $key => $value) {
										$res .= $value -> name . ", ";
									}

									$res = substr($res, 0, strlen($res) - 2);
									echo $res;
								}
								?>
							</div>

							
							<?php if($mode != "edit"){ ?>
							<div class="commonTextFont greyBackground boldText textLeft " style="height: 40;padding-top: 20;padding-left:40; width:760;margin-top:0">
								<input name="datePublish" required="required"
                                       style="border: 1px solid grey; border-radius: 5px; margin-top: -3;height: 30;padding: 5"
								class="borderNone" type="datetime" id="datetimepicker3"/>
							</div>

							<script src="../js/globalJQ.js"></script>
							<script src="../js/jquery.datetimepicker.js"></script>
							<script>
								$('#image_button').click(function() {
									$('#datetimepicker3').datetimepicker('show');
								});

								$('#datetimepicker3').datetimepicker({
									//inline:true
                                    onChangeDateTime:function(dp,$input){
										console.log(Date.parse($input.val()));
										console.log($input.val());
                                        $('#idDatePublish').text($input.val());
                                    }
								});
							</script>
							
							<?php } ?>

						</div>

					</div>

                    <div class="commonTextFont107 boldText  " style="height: 40;padding-top: 20;">
                        Текст
                    </div>

                    <div class="commonTextNewsContent textLeft commonTextColor commonNewsContentFont" style="">
                        <?php echo $_POST["newsContent"]; ?>
                    </div>

                    <div class=" textLeft ">
                        <?php
                            $currentCompany = $mode == "edit" ? $editableNews->company : $order->company;
                            $currentCompany->RefrreshCommunications();
                            $currentCompany->RefrreshPersons();
                            $isAddCompanySocialNetworks = isset($_POST["checkboxAddCompanySocialNetworks"]);
                            $ischeckboxSocialNetworks6 = isset($_POST["checkboxSocialNetworks6"]);
                            $ischeckboxSocialNetworks7 = isset($_POST["checkboxSocialNetworks7"]);
                            $isAddCompanyContacts = $_POST["contacts"]!=NULL;
                            $contacts = $_POST["contacts"];
                            include_once "../company/companyAdditionalInfo.php";?>
                    </div>

                    <div class="container" style="margin-bottom: 5">
                        <div class="commonTextFont107 " style="">Сейчас :</div>
                        <div id="idCurrentDate" class="colorTextDarkslateblue commonTextFont107 boldText" style="padding-left: 10"></div>
                        <script>
                            var date = new Date();
                            $('#idCurrentDate').text("Дата : "
								+ date.toLocaleDateString().split('.')[2] + "."
								+ (date.toLocaleDateString().split('.')[1].length == 1
									? ("0" + date.toLocaleDateString().split('.')[1] + ".")
									: date.toLocaleDateString().split('.')[1] + ".")
								+ (date.toLocaleDateString().split('.')[0].length == 1
								? ("0" + date.toLocaleDateString().split('.')[0] + ".")
								: date.toLocaleDateString().split('.')[1] + ".")
								+" "+date.toLocaleTimeString().substr(0,5));
                        </script>
                    </div>

                    <div class="container" style="margin-bottom: 5;margin-top: 10">
                        <div class="commonTextFont107 boldText" style="">Ваш материал будет доступен :</div>
                        <div id="idDatePublish" class="greenText commonTextFont107 boldText" style="padding-left: 10"></div>
                    </div>

					<div class="horizontalSeparator" style="margin-top: 0"></div>

					<div class="container wrapper_hover" style="margin-top: 10">
                        <div class="clickable btnSeeNews commonTextStyle"
                             onclick="$('#formBack').submit()"
                            >
                            Назад
                        </div>
						<div class="wrapper_hover" style="position: relative;left:20"
                             onclick="
                             	if(document.getElementById('datetimepicker3') != null &&
                             		document.getElementById('datetimepicker3').value=='')
                             	{
                             		modules.modals.openModal($('.modal-trigger'),'modal-8ctrMessageNotDateSelected');
                             		return;
                             	}
                             	else
                             	{
									if(totalprice==0)
										modules.modals.openModal($('.modal-trigger'),'modal-8ctrMessageFreeNews');
									else
										$('#submitButton').click();
                                }
                             "
                            >
							<input type="" id="" value="Публиковать" class="commonTextStyle btnSeeNews borderNone clickable"/>

						</div>
						<input type="hidden" value="<?php echo $mode; ?>" name="action" />
                        <input type="submit" id="submitButton" style="display: none" />
						<input type="hidden" id="TotalDollarCost" name="TotalDollarCost"/>

					</div>

                    <div class="layer_0_copy_2-holder clearfix footerStyle" style="margin-left:-140;top:0" >
                        <?php
                            include_once "../footer/footer.php";
                        ?>
                    </div>

                    <?php
                        $isShow="0";
                        $addedId = "ctrMessageFreeNews";
                        $headerMessage = "Информация";
                        $contentMessage1 =
                            "Чтобы публиковать новость на главной странице сайта зайдите в раздел
                            \"Управление публикациями\" и нажмите \"Публиковать на главной\" ";

                        $actionBtnOk="$('#submitButton').click();";
                        include('../lib/MessageBox.php');

						$isShow="0";
						$addedId = "ctrMessageNotDateSelected";
						$headerMessage = "Информация";
						$contentMessage1="Не выбрана дата публикации новости";
						include('../lib/MessageBox.php');
                    ?>

				</form>

            </div>


		</div>
        <script>
            CountOrdersCost();
            $('#TotalDollarCost').val($('#idTotalDollarCost').text());
        </script>
	</body>

</html>