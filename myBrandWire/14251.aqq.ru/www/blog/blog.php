<?php session_save_path("../tmp");session_start(); ?>
<?php
	include_once '../lib/csFunctions.php';
	InitGlobServerRedirectConstants();
?>

<html>
 <head>
  <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
  <link rel="stylesheet" type="text/css" href="../company/style.css" />
  <link rel="stylesheet" type="text/css" href="../commonStyles.css" />
  <link rel="stylesheet" type="text/css" href="../header/style.css" />
  <link rel="stylesheet" type="text/css" href="style.css" />
     <link rel="shortcut icon" href="../images/favicon.ico">

     <script type="text/javascript" src="../services/servicesScript.js"></script>
 </head>
 
 <body onload="AnaliseAndSetElementsVisisbility('<?php echo $_SESSION["loginUser"] ?>', '<?php echo $_SESSION["idUser"] ?>')">
 	<div class="global_container_ expandingBoxFix" style="height: 100%;min-height: 876">
  	
  	<?php include_once '../header/headerMain.php'; ?>
  	
  	<div class=" emptyContent" style="top:5;height: 100%" id="idMainContent">
  		
  		<?php include_once 'blogMainContent.php'; ?>
  		
  	</div>
   
   
  </div>
 	
 </body>
 
</html>