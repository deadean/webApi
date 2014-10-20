<?php session_save_path("../tmp");session_start();?>
<?php
    include_once '../lib/csFunctions.php';
    InitGlobServerRedirectConstants();


    include_once '../model/mainController.php';
    include_once '../model/controller.php';
    include_once '../model/model.php';

    //print_r($_GET);
    //print_r($_POST);

	//print_r($_SESSION);

    $user = unserialize($_SESSION["userObject"]);
    $controller = Controller::getInstance();
    $controller -> IsUserRegistered($user -> login, $user -> password);
    $user = unserialize($_SESSION["userObject"]);

    //print_r($user);
    $currentCompany = "";
    foreach ($user->companies as $company) {
        $currentCompany = $company;
        break;
    }

    //print_r($currentCompany);

	//$obj = new DateTime('now');
	//echo $obj->format('H:m:s');

?>

<html>

<head>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
	<meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate" />
	<meta http-equiv="Pragma" content="no-cache" />
	<meta http-equiv="Expires" content="0" />

    <link rel="shortcut icon" href="../images/favicon.ico">


    <link rel="stylesheet" type="text/css" href="style.css" />
    <link rel="stylesheet" type="text/css" href="../commonStyles.css" />
    <link rel="stylesheet" type="text/css" href="../header/style.css" />
    <link rel="stylesheet" href="../css/normalize.css">
    <link rel="stylesheet" href="../css/styleModalWindow.css" media="screen" type="text/css" />
    <link rel="stylesheet" type="text/css" href="js/simptip-mini.css" media="screen,projection" />

    <script type="text/javascript" src="../services/servicesScript.js"></script>
    <script type="text/javascript" src="js/jqueryLib.js"></script>

    <script type="text/javascript" src="../lib/jq/1.10.2/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../lib/jq/1.10.2/jquery.form.min.js"></script>
    <script type='text/javascript' src='../lib/knockouts/knockout-3.1.0.js'></script>


</head>

<body onload="AnaliseAndSetElementsVisisbility('<?php echo $_SESSION["loginUser"] ?>', '<?php echo $_SESSION["idUser"] ?>')">

<div class="global_container_ expandingBoxFix " style="min-height: 800;height:100%">

    <?php
        include_once '../header/headerMain.php';
    ?>

    <div id="<?php echo "modal-8Company" ?>" class="modal" data-modal-effect="flip-vertical">
        <div class="modal-content">
            <?php
            include_once '../register/registerNewCompany.php';
            ?>
        </div>
    </div>



    <!--
    <script src='../js/jqueryModalWindow.js'></script>
    <script src='../js/index.js'></script>-->

    <div class="clearfix" id="container">
		<script>
			function LoadCompany(id){
				company = {
					idCompany: id
				};
				SetUnivaersalJSONAjax3('../ajaxPages/crud.php','getCompanyInfo',company,function(data){
					var company = JSON.parse(data);
					SetCompany1(id, company.logo.toString());
				});
			}

		</script>
        <?php

        $currentCompany = "";
        foreach ($user->companies as $company) {
            if (!$currentCompany) {
                $currentCompany = $company;
            }?>

            <div class="wrapper_hover container">
				<div class='clearfix companyTab' id='$company->id'
					 onclick="LoadCompany(<?php echo $company->id;?>)"
					 style='' ><?php echo $company->name;?></div>
			</div>
            <div class='space'></div>
        <?php }
        ?>

<!--        <div class='wrapper_hover container btnAdd clickable modal-trigger' id="idShowModalWindowCompanyAdd" onclick="modules.modals.openModal($('.modal-trigger'),'modal-8Company');"></div>-->

    </div>



    <div id="companyTabId" class="">
        <?php
        include_once "companyTab.php";
        ?>
    </div>



</div>
<script>
	//console.log('<?php echo $currentCompany->logo; ?>');
    LoadImages('<?php echo $currentCompany->logo; ?>');
</script>

</body>

</html>

<?php
if (isset($_GET["type"])) {
    if ($_GET["type"] == "autoAdd") {
        echo "<script>document.getElementById('idShowModalWindowCompanyAdd').click();</script>";
    }
}
?>