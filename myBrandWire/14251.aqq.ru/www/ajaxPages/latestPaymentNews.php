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
	$news = $controller->GetPaymentNews();
	$newsColumn1 = array();
	$newsColumn2 = array();
	
	$fl = TRUE;
	foreach ($news as $item)
	{
		if($fl){
			$newsColumn1[$item->id]=$item;
			$fl=FALSE;
		}
		else{
			$newsColumn2[$item->id]=$item;
			$fl=TRUE;
		}
	}

?>

<div class="c_wrapper26 clearfix ">
	<?php
		foreach ($newsColumn1 as $item)
		{
			$firstMainNewsImage = "";
			preg_match("#<img src=[\"\'](.+?)[\"\'](.*)/>#si", $item -> content, $firstMainNewsImage);
			$firstMainNewsImage = $firstMainNewsImage[1];
			$currentCoompany = "";
			foreach ($item->company as $key => $value) {
				$currentCoompany = $value;
				break;
			}

			$firstMainNewsImage = $firstMainNewsImage==""? $item->company->logo : $firstMainNewsImage;
			//$firstMainNewsImage = is_file($firstMainNewsImage) ? $firstMainNewsImage : "images/layer_13.jpg";
	?>
			<div class='group_5 clearfix'>
				<a href="<?php echo GetPageByPlace('MainNewsForm'.$_SERVER['PHP_SELF']).'?id='.$item->id; ?>"><img class=' layer_13' src='<?php echo $firstMainNewsImage;?>' width='112' height='112' /></a>
				<h2 class='kompaniya_sprint_prodleva commonTextNewsHeader hoverTextUnderline'>
					<a href="<?php echo GetPageByPlace('MainNewsForm'.$_SERVER['PHP_SELF']).'?id='.$item->id; ?>">
					<?php echo $item->header; ?>
				</a></h2>
			</div>
		<?php } ?>
</div>

<div class="c_wrapper27 clearfix ">
	<?php
		foreach ($newsColumn2 as $item)
		{
			$firstMainNewsImage = "";
			preg_match("#<img src=[\"\'](.+?)[\"\'](.*)/>#si", $item -> content, $firstMainNewsImage);
			$firstMainNewsImage = $firstMainNewsImage[1];
			$currentCoompany = "";
			foreach ($item->company as $key => $value) {
				$currentCoompany = $value;
				break;
			}
			$firstMainNewsImage = $firstMainNewsImage==""? $item->company->logo:$firstMainNewsImage;
			//$firstMainNewsImage = is_file($firstMainNewsImage) ? $firstMainNewsImage : "images/layer_13.jpg";
			
			echo 
			"<div class='group_5 clearfix'>"
				."<a href='".GetPageByPlace("MainNewsForm".$_SERVER['PHP_SELF'])."?id=".$item->id."'><img class=' layer_13' src='".$firstMainNewsImage."' width='112' height='112' /></a>"
				."<h2 class='kompaniya_sprint_prodleva hoverTextUnderline'><a href='".GetPageByPlace("MainNewsForm".$_SERVER['PHP_SELF'])."?id=".$item->id."'>"
					.$item->header
				."</a></h2>"
			."</div>"
			;
		}
	?>
</div>
