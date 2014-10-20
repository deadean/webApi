<?php session_save_path("../tmp");session_start(); ?>
<?php
	include_once '../lib/csFunctions.php';
	InitGlobServerRedirectConstants();
	include_once "../model/controller.php";
	$controller = new Controller();
	$news = $controller->GetBlogNews();
	$idNews =  $_GET["id"];
	$currentNews = $news[0];
	
	 
	foreach ($news as $item){
		if($item->id==$idNews){
			$currentNews = $item;
			break;
		}
	}
	
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
  	
  	
  	
  	<div class=" emptyContent container" style="top:5;height: 100%">
  		<div class="leftPart ">
  			<div class="textLeft textParagraph" style="margin-bottom: 10;margin-top: 10">Главная новость</div>
  			<div class="container">
  				<div class="newsImage "></div>
  				<div class="newsContainer">
  					<div class="textParagraph" style="font-size: 107%;height:50">
  						<?php echo $currentNews->header; ?>
  						<br/>
  						
  					</div>
  					<div class="horizontalSeparator"></div>
  					<div class="container" style="top:0">
  						<div class="companyImage"></div>
  						<div class="commonTextStyle" style="left:10;position: relative"><?php echo $currentNews->company->name ?></div>
  					</div>
  					<div class="horizontalSeparator"></div>
  					<div class="container" style="top:30">
  						<div class="dateImage"></div>
  						<div class="greyText leftSpace">01.12.2013</div>
  						<div class="leftSpace tagsImage"></div>
  						<div class="leftSpace greyText">Теги</div>
  					</div>
  				</div>
  			</div>
  			<div class="commonTextStyle " style="min-height: 200;margin-top:20;">
  				Текст новости</br>
  				
  			</div>
  			<div class="commonTextStyle repostBlock  textLeft leftSpace blockPadding ">
  				<div class="clickable " style="background: url(images/twitterRepost.png) no-repeat; height: 30;width: 100; "></div>
  			</div>
  			<form action="../services/services.php">
	  			<div class="bannerBlock textParagraph blockPadding textLeft container">
	  				<div class="" style="width:480">Хотите разместить свой пресс-релиз на этом сайте</div>
	  				<div class="wrapper_hover"><input type="submit" class="blockPadding textParagraph showDetailsBlock commonTextStyle clickable borderNone" value="Узнать детали"/></div>
	  			</div>
  			</form>
  			<div class="textLeft textParagraph" style="margin-bottom: 10;margin-top: 10">Последние главные новости</div>
  			<div class="horizontalSeparator" style="width:680"></div>
  			<div class="">
	  			<?php
	  				$i=0;
					foreach ($news as $item)
					{
						if($i==4)
							break;
						$i++;
						echo "<div class=' textLeft blockPadding mainNewsHref commonTextStyle container'>"
							."<div class='' style='background:url(images/arrow.png) no-repeat; padding-right:20; width:0;height:15;'></div>"
							."<div><a href='".GetPageByPlace("MainNewsForm".$_SERVER['PHP_SELF'])."'>"
								.$item->header
							."</div></a>"
						."</div>"
						;
					}
				?>	
  			</div>
  		</div>
  		<div class="rightPart ">
  			
  		</div>
  		
  		
  	</div>




   <div class="layer_0_copy_2-holder clearfix footerStyle" style="top:0;" >
    <?php include_once "../footer/footer.php"; ?>
   </div>
   
  </div>
 	
 </body>
 
</html>