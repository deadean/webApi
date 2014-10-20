<?php session_save_path("../tmp");session_start(); ?>
<?php
	include_once '../lib/csFunctions.php';
	InitGlobServerRedirectConstants();
	include_once "../model/controller.php";
	include_once "../model/mainController.php";
	$controller = new Controller();
?>

<html>
 <head>
  <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
  <link rel="stylesheet" type="text/css" href="../company/style.css" />
  <link rel="stylesheet" type="text/css" href="../commonStyles.css" />
  <link rel="stylesheet" type="text/css" href="../header/style.css" />
  <link rel="stylesheet" type="text/css" href="style.css" />
  <script type="text/javascript" src="../services/servicesScript.js"></script>
 </head>
 
 <body onload="AnaliseAndSetElementsVisisbility('<?php echo $_SESSION["loginUser"] ?>', '<?php echo $_SESSION["idUser"] ?>')">
 	<div class="global_container_ expandingBoxFix" style="min-height: 800;height:100%">
  	
  	<?php include_once '../header/headerMain.php'; ?>
  	
  	<div class=" emptyContent" style="top:5;height: 100%">
  		
  		<form method="post" action="<?php echo $_SERVER["PHP_SELF"]; ?>">	
  		<div class="textLeft commonTextFont textParagraph blockPadding " style="margin-top: 20;margin-bottom: 20">
  			Написание
  		</div>
  		
  		<div class="container" >
  			
  			<div class="" style="width: 200;">
  				<div class="commonTextFont boldText textLeft leftSpace" style="height: 55;">Задачи и условия :</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace " style="height: 40">Дата события :</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace " style="height: 40">Регион :</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace " style="height: 55">Событие / информационный повод :</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace" style="height: 55;">Значимость факта для аудитории :</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace " style="height: 55;">Подробности обязательные для упоминания :</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace " style="height: 55;">Цитата :</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace " style="height: 120;">Контактное лицо для связи при возникновении вопросов :</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace " style="height: 40;">Ссылки на изображения / видео :</div>
  			</div>
  			
  			<div class="">
  				<div class="commonTextFont" style="height: 55; width:760">
  					<textarea class="blockLeft" rows="3" cols="75" name="spellingTasks"></textarea>
  				</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont textLeft container " style="height: 40">
  					<div><input type="date" name="dateStart" /></div>
  					<div class="blockPadding">по</div>
  					<div><input type="date" name="dateEnd" /></div>
  				</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont textLeft " style="height: 40;padding-left: 1">
  					<input type="text" name="region" class="commonInputTextBox" style="width: 320" />
  				</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont" style="height: 55; width:760">
  					<textarea class="blockLeft" rows="3" cols="75" name="event"></textarea>
  				</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont" style="height: 55; width:760">
  					<textarea class="blockLeft" rows="3" cols="75" name="fact"></textarea>
  				</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont" style="height: 55; width:760">
  					<textarea class="blockLeft" rows="3" cols="75" name="commonComments"></textarea>
  				</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont" style="height: 55; width:760">
  					<textarea class="blockLeft" rows="3" cols="75" name="text"></textarea>
  				</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace " style="height: 120;padding: 2">
  					<div><input type="text" class="commonInputTextBox " name="contactStatus" style="width: 320;margin-bottom: 10" placeholder="Должность"/></div>
  					<div><input type="text" class="commonInputTextBox " name="contactPhone" style="width: 320;margin-bottom: 10" placeholder="Телефон"/></div>
  					<div><input type="text" class="commonInputTextBox " name="contactEmail" style="width: 320" placeholder="Email"/></div>
  				</div>
  				<div style="height: 10;"></div>
  				<div class="commonTextFont boldText textLeft leftSpace " style="height: 40;padding: 2">
  					<input type="text" class="commonInputTextBox " name="links" style="width: 320;margin-bottom: 10"/>
  				</div>
  			</div>
  		</div>
  		
  		<div class="horizontalSeparator" style="margin-top: 0"> </div>
  		
  		<div class="container wrapper_hover" style="margin-top: 10;padding: 2">
  			<div class="wrapper_hover" style="position: relative;left:200">
				<input type="submit" value="Отправить" class="commonTextStyle btnSeeNews borderNone clickable"/>
				<input type="hidden" name="action" value="savePreNews" />
			</div>
			
  		</div>
  		
  		</form>
  		
  		<div class="layer_0_copy_2-holder clearfix footerStyle" style="top:5; margin-left: 100;position: relative;left:-240;width: 100%" >
    		<?php include_once "../footer/footer.php"; ?>
   		</div>
   		
  	</div>


   
   
  </div>
 	
 </body>
 
</html>