<?php session_save_path("../tmp");session_start(); ?>
<?php
include_once '../lib/csFunctions.php';
include_once '../model/model.php';

$user = unserialize($_SESSION["userObject"]);
$idCompany = "";
$portionSize = 3;
//print_r($_POST);
foreach ($_GET as $key => $val) {
	$idCompany = $_GET[$key];
}
if(isset($_POST["portionSize"])){
	$portionSize = $_POST["portionSize"];
}
if(isset($_POST["idCompany"])){
	$idCompany = $_POST["idCompany"];
}

if (!$currentCompany)
	$currentCompany = $user -> companies[$idCompany];
$currentCompany -> RefreshNews();
?>

<?php
if(!is_null($currentCompany))
{
$i=0;

if(!$currentCompany->news){
?>

<div class="fontNewsListWithEmptyNews textLeft">
	<p>
		Здесь Вы можете публиковать информацию любого содержания,раскрывающую сущность Вашей деятельности.
	</p>
	<p>
		Будте креативны и не сковывайте себя в действиях, ведь Ваш бренд особенный!
		Покажите миру его лицо, для кого он предназначен и чем он живет!
	</p>
	<p>
		Только те материалы, которые будут публиковаться на главной странице, предварительно будут проверены модератором.
	</p>
	<p>
		Желаем Вам успехов!
	</p>
</div>

<?php
//return;
}

foreach ($currentCompany->news as $item)
{

	if($i==$portionSize)
		break;

	$i++;

	$isPaymentImg="";
	$firstMainNewsImage = "";
	$isPaymentImgBlock="";
	preg_match("#<img src=[\"\'](.+?)[\"\'](.*)/>#si", $item -> content, $firstMainNewsImage);
	$firstMainNewsImage = $firstMainNewsImage[1];
	$firstMainNewsImage = $firstMainNewsImage==""? $currentCompany->logo:$firstMainNewsImage;
	$isPaymentImgBlock = "<img class=' clearfix '  id='imgNewsLogo' width='130' height='130' src='".$firstMainNewsImage."'/>";

	if($item->order->payment=="1"){
		$isPaymentImg =
		"<div class=''>
			<img class=' blockRelative ' src='images/like.png' id='' width='30' height='30' style='top:-50;left:-41;border:none;' />
		</div>";
	}
echo "<script>
function deleteId(id)
{
modules.modals.closeModal($('.modal-trigger'),'modal-8');
SetUnivaersalCRUDAjax(id,'companyNewsContainer','../ajaxPages/crud.php','removeNews');
SetAjaxRequest('showCompanyNews','$currentCompany->id');
}
function publishNews(id)
{
//alert(ids);
SetUnivaersalCRUDAjax(id,'companyNewsContainer','../ajaxPages/crud.php','publishNews');
SetAjaxRequest('showCompanyNews','$currentCompany->id');
}
</script>";
$test = "deleteId(".$item->id.")";
$deleteBtnAction = "modules.modals.openModal($('.modal-trigger'),'modal-8".$item->id."');";
$publishBtnAction = "publishNews(".$item->order->id.")";;
?>

<div id="<?php echo "modal-8" . $item -> id; ?>" class="modal" data-modal-effect="flip-vertical">
	<div class="modal-content">
		<h2 style="margin-bottom: 10">Удаление новости</h2>
		<p style="text-align: left;line-height: 1.5">
			Вы действительно хотите удалить материал? Удаление материала предполагает его удаление со страницы профайла компании. Все материалы, когда-либо
			опубликованные на главной странице сайта остаются доступными для просмотра.
		</p>
		<div style="top:20 ;left:220" class="btn btn-primary btn-medium modal-close blockRelative" onclick="<?php echo $test; ?>">
			Согласен
		</div>
	</div>
</div>
<?php echo "<form action='../company/news/newsAdaptee.php' method='post' name='form" . $item -> id . "'>";
	echo "<div class='companyNewsItem '>" 
			. "<div class='companyNewsItemMenu wrapper_hover'>" 
				. "<div class='companyNewsItemMenuItem commonTextStyle container' onclick='form" . $item -> id . "[0].submit()'>" 
					. "<div style='width:100'>Редактировать</div>" . "<div class='simptip-position-bottom simptip-multiline simptip-smooth' data-tooltip=
'
Помните, что после поступления текста, редакция без промедления приступает к распространению материала.
При этом, если текст уже поступил в рассылку, редакция не сможет внести изменения в распространяемую версию
'style='position:absolute; margin-left:115;margin-top:-20;background:url(images/layer_65_copy_2.png) no-repeat;width:12;height:12;background-size:100%;' ></div>" . "</div>" . "<div class='companyNewsItemMenuItem commonTextStyle container'>" . "<div style='width:100' class='wrapper_hover'>

<input type='button' value='Удалить' onclick=" . $deleteBtnAction . "
class='borderNone btnSeeNews4 clickable btn-small modal-trigger' data-modal-id='modal-8' />

<input type='hidden' value='" . $item -> id . "' name='id'  />
<input type='hidden' value='edit' name='mode'  />
</div>" 
. "<div class='' style='position:absolute; margin-left:115;margin-top:-30; background:url(images/layer_64_copy_4.png) no-repeat;width:12;height:12;background-size:100%'></div>" 
. "</div>" . "</div></form>" 
. "<div class='companyNewsContent '>" 
    . "<div class='clearfix newsLogo' style='width:500px;padding-left:0'>"
        ."<a href='../news/mainNewsForm.php?id=".$item->id."' target='_blank'>"
            .$isPaymentImgBlock
        ."</a>"
    ."</div>"
    . $isPaymentImg
    . "<div style='width:600px;'>"
	    . "<div style='height: 50'>"
            . "<p class=' kompaniya_sprint_prodleva hoverTextUnderline'>"
                ."<a href='../news/mainNewsForm.php?id=".$item->id."' target='_blank'>"
                    ."<strong>" . $item -> header . "</strong>"
                ."</a>"
            ."</p>"
        . "</div>"
	    . "<div style='height: 30' class='commonTextFont textLeft commonTextColor .commonNewsContentFont'>";
            echo implode(' ', array_slice(explode(' ', $item -> content),0,16)).'...';
        echo "</div>"
        . "<div class=' companyNewsOperations ' style='height: 30;margin-top:10'>"
            . "<div class='container' style=''  >";

                if($item->order->payment=="1"){
                    echo "<div class='dateImage'></div>";
                    //echo "<div class='greyText leftSpace'>"."dateSave"."</div>";
                }
	            echo "<div class='leftSpace greyText'>";
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

<!--<script src='../js/jqueryModalWindow.js'></script>-->
<script src='../js/index.js'></script>