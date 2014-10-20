<?php session_save_path("../tmp");session_start(); ?>
<?php
    include_once '../lib/csFunctions.php';
    include_once '../model/controller.php';

    $idCompany = "";
    $portionSize = 3;
    foreach ($_GET as $key => $val) {
        $idCompany = $_GET[$key];
    }
    if(isset($_POST["portionSize"])){
        $portionSize = $_POST["portionSize"];
    }
    if(isset($_POST["idCompany"])){
        $idCompany = $_POST["idCompany"];
    }

    $controller = Controller::getInstance();
    if(is_null($currentCompany)){

        $currentCompany = $controller->GetCompanyById($idCompany);
    }

?>

<?php
    if(!is_null($currentCompany))
    {
        $i=0;

        foreach ($currentCompany->news as $item)
        {
            if($i==$portionSize)
                break;

            $i++;
			$firstMainNewsImage = "";
			preg_match("#<img src=[\"\'](.+?)[\"\'](.*)/>#si", $item -> content, $firstMainNewsImage);
			$firstMainNewsImage = $firstMainNewsImage[1];
			$firstMainNewsImage = empty($firstMainNewsImage) ? $currentCompany->logo : $firstMainNewsImage;
			$isPaymentImgBlock = "<a href=".GetPageByPlace('MainNewsForm'.$_SERVER['PHP_SELF']).'?id='.$item->id."><img class=' clearfix '
                id='imgNewsLogo' width='130' height='130' src='".$firstMainNewsImage."'/></a>";
            if($item->order->payment=="1"){
                $isPaymentImg = "<div class=''><img class=' blockRelative ' src='images/like.png' id='' width='30' height='30' style='top:-50;left:-41;border:none;' /></div>";
			}
?>

<?php
	echo "<div class='companyNewsItem '>"
            . "<div class='companyNewsContent '>"
                . "<div class='clearfix newsLogo ' style='width:500px;padding-left:0'>
                    ".$isPaymentImgBlock
                ."</div>" . $isPaymentImg
                . "<div style='width:600px;'>"
	                . "<div style='height: 50'>"
                        . "<p class=' kompaniya_sprint_prodleva'><strong>" . $item -> header . "</strong></p>"
                    . "</div>"
	                . "<div style='height: 30' class='commonTextFont textLeft commonTextColor .commonNewsContentFont'>"
                        .implode(' ', array_slice(explode(' ', $item -> content),0,16)).'...'
                    ."</div>"
                    . "<div class=' companyNewsOperations ' style='height: 30'>" . "<div class='container' style=''  >";

	if($item->order->payment=="1"){	
		echo "<div class='dateImage'></div>";
		//echo "<div class='greyText leftSpace'>"."dateSave"."</div>";
	}
	echo "<div class='leftSpace greyText '>";
	$textCategory = "";
	$idCategory = "";
	foreach ($item->categories as $key => $value1) {
		$textCategory = $value1 -> name;
		$idCategory = $value1->id;
		break;
	}
	echo $textCategory;
  	echo "</div>";
	echo "<div class='dateImage'></div>";
	echo "<div class=' leftSpace greenText boldText'>".$item -> order -> datePublish."</div>";
	
	
		echo "</div><div></form><form action='../company/news/newsAdaptee.php' method='post' name='form" . $item -> id . "'>
			<input type='hidden' value='" . $item -> id . "' name='id'  />
			<input type='hidden' value='edit' name='mode'  />
			<input type='hidden' value='checkboxPublish' name='checkboxPublish'  />
		" . $isPayment . "</div></div></form>" . "</div>" . "</div></div>";
	}

	}
?>

<script src='../js/jqueryModalWindow.js'></script>
<script src='../js/index.js'></script>