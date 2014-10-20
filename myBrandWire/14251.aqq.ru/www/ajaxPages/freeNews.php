<?php
	if (is_file("lib/csFunctions.php")) {
		include_once 'lib/csFunctions.php';
		include_once "model/controller.php";
	}
	if (is_file("../lib/csFunctions.php")) {
		include_once '../lib/csFunctions.php';
		include_once "../model/controller.php";
	}
	
	InitGlobServerRedirectConstants();
	$controller = new Controller();
	$news = $controller->GetFreeNews();
    //print_r($news);
	$news = array_slice($news, 0,4);
?>

<?php foreach ($news as $key => $value) { ?>

	<div class="group_2 clearfix">
		<div class="c_wrapper17 clearfix">
	        <img class=" layer_10" src="images/layer_10.png" alt="" width="16" height="16" />
	        <form action="../freeNews/freeNews.php" method="get" id="<?php echo "formSubmitFreeNews".$value->id;?>">
	        <p class=" layer15_09_2013_finansy"><?php echo $value -> order -> datePublish; ?> &nbsp;| &nbsp;
	        	
	        	<span style="color: #383838;" class="hoverTextUnderline clickable" 
	        		onclick="document.getElementById('<?php echo "formSubmitFreeNews".$value->id;?>').submit();">
	        		<?php
						$textCategory = "";
						$idCategory = "";
						foreach ($value->categories as $key => $value1) {
							$textCategory = $value1 -> name;
							$idCategory = $value1->id;
							break;
						}
						echo $textCategory;
  						?></span></p>
  					<input type="hidden" name="param" value="<?php echo $idCategory;?>" />
  				</form>
	        <div class="group_10_copy_3 clearfix">
	         <img class=" shape_8_copy_7" src="images/shape_8_copy_7.jpg" alt="" width="2" height="15" />
	         <p class=" novosti_copy">новости</p>
	        </div>
	    </div>
	    <p class=" informaciya_o_snizhenii_v hoverTextUnderline">
	    	<a href="<?php echo GetPageByPlace('MainNewsForm'.$_SERVER['PHP_SELF']).'?id='.$value->id; ?>">
	    		<?php echo $value->header; ?>
	    	</a>
	    </p>
	</div>	
	
<?php } ?>	
