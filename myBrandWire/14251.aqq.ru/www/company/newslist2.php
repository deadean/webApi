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
$isPayment = "<div class='container' style='padding:10' id='paidNews'>
<div class='wrapper_hover2' onclick='form" . $item -> id . "[1].submit()'>
<input class='btnSeeNews2 commonTextStyle borderNone clickable' type='button' value='Сделать платной' style='width:140'/>
</div>
<div>
<div class='infoable btnToolTip simptip-position-bottom simptip-multiline simptip-smooth'
data-tooltip=
'Если Вы хотите публиковать новость в разделе &laquo;Главные новости&raquo и применить к ней другие платные услуги,
нажмите сюда. Помните, что если новость утратила свою актуальность или уже была опубликована в сети, результаты работ
могут стать менее эффективными'
>i</div>
<div style='height:17'></div>
</div>
</div>";
$isPaymentImg = "<div class=' ' style='padding:13'></div>";
$firstMainNewsImage = "";
$isPaymentImgBlock="";
	$firstMainNewsImage = "";
	preg_match("#<img src=[\"\'](.+?)[\"\'](.*)/>#si", $item -> content, $firstMainNewsImage);
	$firstMainNewsImage = $firstMainNewsImage[1];
	$firstMainNewsImage = $firstMainNewsImage==""? $currentCompany->logo : $firstMainNewsImage;
	$isPaymentImgBlock = "<img class=' clearfix '  id='imgNewsLogo' width='130' height='130' src='".$firstMainNewsImage."'/>";
if($item->order->payment=="1"){
$isPayment = "<div class='container' style='padding:10' id='paidNews'>
<div class='wrapper_hover2' >
<input class='btnSeeNews2 commonTextStyle borderNone clickable' type='submit' value='Публиковать в соц. сетях' style='width:170'/>
</div>
<div>
<div class='infoable btnToolTip simptip-position-bottom simptip-multiline simptip-smooth'
data-tooltip=
'
Экономьте свое время на публикацию в соц. сетях.
Распространяйте информацию, следите за комментариями, отвечайте на вопросы клиентов, анализируйте активность своей целевой аудитории
с единого центра. Чтобы заказать услугу, нажмите сюда
'>i</div>
<div style='height:17'></div>
</div>
</div>";
$isPaymentImg = "<div ><img class=' blockRelative ' src='images/like.png' id='' width='30' height='30' style='top:-50;left:-41;border:none;' /></div>";
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
	echo "<div class='companyNewsItem '>" . "<div class='companyNewsItemMenu wrapper_hover'>" . "<div class='companyNewsItemMenuItem commonTextStyle container' onclick='form" . $item -> id . "[0].submit()'>" . "<div style='width:100'>Редактировать</div>" . "<div class='simptip-position-bottom simptip-multiline simptip-smooth' data-tooltip=
'
Помните, что после поступления текста, редакция без промедления приступает к распространению материала.
При этом, если текст уже поступил в рассылку, редакция не сможет внести изменения в распространяемую версию
'style='position:absolute; margin-left:115;margin-top:-20;background:url(images/layer_65_copy_2.png) no-repeat;width:12;height:12;background-size:100%;' ></div>" . "</div>" . "<div class='companyNewsItemMenuItem commonTextStyle container'>" . "<div style='width:100' class='wrapper_hover'>

<input type='button' value='Удалить' onclick=" . $deleteBtnAction . "
class='borderNone btnSeeNews4 clickable btn-small modal-trigger' data-modal-id='modal-8' />

<input type='hidden' value='" . $item -> id . "' name='id'  />
<input type='hidden' value='edit' name='mode'  />
</div>" . "<div class='' style='position:absolute; margin-left:115;margin-top:-30; background:url(images/layer_64_copy_4.png) no-repeat;width:12;height:12;background-size:100%'></div>" . "</div>" . "</div></form>" . "<div class='companyNewsContent '>" . "<div class='clearfix newsLogo ' style='width:500px;padding-left:0'>
".$isPaymentImgBlock."</div>" . $isPaymentImg . "<div style='width:600px;'>" . "<div style='height: 50'>" . "<p class=' kompaniya_sprint_prodleva'><strong>" . $item -> header . "</strong></p>" . "</div>" . "<div style='height: 10'>" . "</div>" . "<div class=' companyNewsOperations ' style='height: 60'>" . "<div class='container' style=''  >";
	if ($item -> order -> isPublish == "0") {
		echo "<div class='wrapper_hover2'>
<input class='btnSeeNews2 commonTextStyle borderNone clickable' type='button' value='Публиковать на главной'
onclick=" . $publishBtnAction . "
style='width:160'/></div>" . "<div><div class='infoable btnToolTip simptip-position-bottom simptip-multiline simptip-smooth'
data-tooltip=
'
Чтобы опубликовать бесплатный текст на главной странице, нажмите сюда
'>
i</div><div  style='height:17'></div></div>";
	}
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