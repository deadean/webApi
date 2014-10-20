<?php session_save_path("../tmp");session_start(); ?>
<?php
include_once '../lib/csFunctions.php';
InitGlobServerRedirectConstants();
include_once "../model/controller.php";
$controller = new Controller();
$idNews = $_GET["id"];
$currentNews = $controller -> GetNewsById($idNews);
$currentNews->content = CValidate::ValidateHtmlString($currentNews->content);
$news = $controller -> GetPaymentNews();
$newsAction = "../mainNews/mainNews.php";
if ($currentNews -> order -> payment == "0")
	$newsAction = "../freeNews/freeNews.php";

$firstMainNewsImage = "";
preg_match("#<img src=[\"\'](.+?)[\"\'](.*)/>#si", $currentNews -> content, $firstMainNewsImage);
$firstMainNewsImage = $firstMainNewsImage[1];
?>

  		<div class="leftPart ">
  			<!--<div class="textLeft textParagraph" style="margin-bottom: 10;margin-top: 10">Ð“Ð»Ð°Ð²Ð½Ð°Ñ� Ð½Ð¾Ð²Ð¾Ñ�Ñ‚ÑŒ</div>-->
  			<div class="container">
  				<!--<div class="newsImage "><img width="200" height="200" src="<?php echo $firstMainNewsImage; ?>"/></div>-->
  				
  				<div class="newsContainer">
  					<div class="textParagraph commonNewsContentHeader textLeft" 
  					style="font-size: 107%;height:50;color: #333;font-size: 23px;line-height: 1em;
  					font-family: 'PFDinDisplayProMedium', Arial, Helvetica, sans-serif;font-weight: 700;">
  						<?php echo $currentNews -> header; ?>
  						<br/>
  						
  					</div>
  					<div class="horizontalSeparator"></div>
                    <form action="../company/companyPage.php" method="post" id="<?php echo 'form'.$currentNews->id;?>">
                        <div class="container" style="top:0"
                            onclick="document.getElementById('<?php echo 'form'.$currentNews->id;?>').submit();">
                            <div class="companyImage clickable" >
								<img id="imgCompanyLogo" width="65" height="65" src="<?php echo $currentNews->company->logo;?>">
							</div>
                            <div class="commonTextStyle clickable hrefWrapper" style="left:10;position: relative"
                                 >
                                    <a >
                                        <?php echo $currentNews->company->name ?>
                                    </a>
                                    <input type="hidden" name="id" value="<?php echo $currentNews->company->id; ?>"/>
                            </div>
                            <script>LoadImages(<?php echo $currentNews -> company -> logo; ?>);</script>
                        </div>
                    </form>
  					<div class="horizontalSeparator"></div>
  					<form action="<?php echo $newsAction; ?>" method="get" id="<?php echo "formSubmitFreeNews" . $currentNews -> id; ?>">
  					<div class="container" style="top:30">
  						<div class="dateImage"></div>
  						<div class="greyText leftSpace"><?php echo $currentNews -> order -> datePublish; ?></div>
  						<div class="leftSpace tagsImage"></div>
  						<div class="leftSpace greyText hoverTextUnderline clickable hrefWrapper"
  							onclick="document.getElementById('<?php echo "formSubmitFreeNews" . $currentNews -> id; ?>').submit();">
  						<?php
						$textCategory = "";
						$idCategory = "";
						foreach ($currentNews->categories as $key => $value1) {
							$textCategory = $value1 -> name;
							$idCategory = $value1 -> id;
							break;
						}
						echo $textCategory;
  						?></div>
  					</div>
  					<input type="hidden" name="param" value="<?php echo $idCategory; ?>" />
  				</form>
  				</div>
  			</div>
  			<div class="commonTextStyle textLeft commonNewsContentFont" style="min-height: 200;margin-top:20;text-indent: 15">
  				<?php echo $currentNews -> content; ?>
  			</div>
            <div class="commonTextStyle textLeft commonNewsContentFont container" style="height: 45px;line-height: 45px">
                <div>Мы в социальных сетях :</div>
                <?php
                    foreach ($currentNews->company->communications as $communicationCompany) {
						if(empty($communicationCompany->value))
							continue;

                        $imgSourcePathByCommunicationType = $communicationCompany->name=="Facebook"?"../images/facebook.png":"";
                        $imgSourcePathByCommunicationType = $communicationCompany->name=="Vkontakte"?"../company/images/layer_59vk.png":$imgSourcePathByCommunicationType;
                        $imgSourcePathByCommunicationType = $communicationCompany->name=="Googleplus"?"../company/images/layer_59.png":$imgSourcePathByCommunicationType;
                        $imgSourcePathByCommunicationType = $communicationCompany->name=="Odnoklassniki"?"../images/odnoklassniki.png":$imgSourcePathByCommunicationType;
                        $imgSourcePathByCommunicationType = $communicationCompany->name=="Twitter"?"../images/_twitter.png":$imgSourcePathByCommunicationType;
                        $imgSourcePathByCommunicationType = $communicationCompany->name=="Tumblr"?"../images/tumblr.png":$imgSourcePathByCommunicationType;
                        $imgSourcePathByCommunicationType = $communicationCompany->name=="LinkedIN"?"../images/linkedin.png":$imgSourcePathByCommunicationType;
                        $imgSourcePathByCommunicationType = $communicationCompany->name=="Pinterest"?"../images/pinterest.png":$imgSourcePathByCommunicationType;
                        $imgSourcePathByCommunicationType = $communicationCompany->name=="Instagramm"?"../images/instagramm.png":$imgSourcePathByCommunicationType;
                        if(empty($imgSourcePathByCommunicationType))
                            continue;

						if(!CValidate::isValidUrl($communicationCompany->value))
							continue;
                    ?>
                        <div >
                            <a href="<?php echo $communicationCompany->value;?>" target="_blank">
                                <img class="btnSocialNetworks" width="30" height="30" src="<?php echo $imgSourcePathByCommunicationType; ?>"/>
                            </a>
                        </div>
                    <?php }
                ?>
            </div>
            <div class="textLeft">
                <?php

                $currentCompany = $currentNews->company;
                $currentCompany->RefreshCompanyInfo();
                $currentCompany->RefrreshCommunications();
                $currentCompany->RefrreshPersons();

                $isAddCompanySocialNetworks = false;
                $ischeckboxSocialNetworks6 = $currentNews->order->isAutoAddAboutCompanyInfo;
                $ischeckboxSocialNetworks7 = $currentNews->order->isAutoAddCompanyAddress;
                $isAddCompanyContacts = $currentNews->order->isAutoAddCompanyContacts;
                $contacts = $currentNews->order->contactsIds;
                include_once "../company/companyAdditionalInfo.php";
                ?>
            </div>
  			<div class="commonTextStyle repostBlock  textLeft leftSpace blockPadding container">
  				<div>
  					<a href="https://twitter.com/share" class="twitter-share-button" data-text="" data-size="large">Tweet</a>
					<script>
						! function(d, s, id) {
							var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https';
							if (!d.getElementById(id)) {
								js = d.createElement(s);
								js.id = id;
								js.src = p + '://platform.twitter.com/widgets.js';
								fjs.parentNode.insertBefore(js, fjs);
							}
						}(document, 'script', 'twitter-wjs');
					</script>
				</div>
				<div class="fb-like" data-href="<?php echo $_SERVER["PHP_SELF"] . "?" . $_SERVER["QUERY_STRING"]; ?>" data-layout="button_count" data-action="like" data-show-faces="true" data-share="true"></div>
				<div>
					<script type="text/javascript">
						document.write(VK.Share.button({
							url : "http://"
						}, {
							type : "round",
							text : "Сохранить"
						}));
					</script>
				</div>
				<div>
					<g:plusone size="small"></g:plusone>
				</div>
  			</div>

  			<form action="../services/services.php">
	  			<div class="bannerBlock textParagraph blockPadding container">
	  				<div class="" style="width:480">Хотите разместить свою новость?</div>
	  				<div class="wrapper_hover"><input type="submit" class="blockPadding textParagraph showDetailsBlock commonTextStyle clickable borderNone" value="Узнать детали"/></div>
	  			</div>
  			</form>
  			<div class="container" style="margin-top:10">
  				<div class="textLeft textParagraph" style="margin-bottom: 10;margin-top: 10;width: 500">Последние главные новости</div>
  				<div class="textLeft textParagraph" style="margin-bottom: 10;margin-top: 10">Новости категории</div>	
  			</div>

  			<div class="horizontalSeparator" style="width:1000"></div>
  			<div class="container">
	  			<div class="" style="vertical-align: top">
		  			<?php
					$i = 0;
					foreach ($news as $item) {
						if ($i == 4)
							break;
						$i++;
						echo "<div class=' textLeft blockPadding mainNewsHref commonTextStyle container' style='width:500'>" . "<div class='' style='background:url(images/arrow.png) no-repeat; padding-right:20; width:0;height:15;'></div>" . "<div><a href='" . GetPageByPlace("MainNewsForm" . $_SERVER['PHP_SELF']) . "?id=" . $item -> id . "'>" . $item -> header . "</div></a>" . "</div>";
					}
					?>	
	  			</div>
	  			<div class="" style="vertical-align: top">
		  			<?php
					$newsByCategory = $controller -> GetPaymentNewsByCategoryId($idCategory);
					$i = 0;
					foreach ($newsByCategory as $item) {
						if ($i == 4)
							break;
						$i++;
						echo "<div class=' textLeft blockPadding mainNewsHref commonTextStyle container' style='width:500'>" . "<div class='' style='background:url(images/arrow.png) no-repeat; padding-right:20; width:0;height:15;'></div>" . "<div><a href='" . GetPageByPlace("MainNewsForm" . $_SERVER['PHP_SELF']) . "?id=" . $item -> id . "'>" . $item -> header . "</div></a>" . "</div>";
					}
					?>	
	  			</div>
  			</div>
  			
  			<div class="layer_0_copy_2-holder clearfix footerStyle" style="top:0;margin-left:-135" >
    			<?php
				include_once "../footer/footer.php";
 ?>
   			</div>
  		</div>
  		<div class="rightPart ">
  			
  		</div>
  		