<?php
	foreach ($news as $key => $value)
 {?>

<form method="post" enctype="multipart/form-data" id="<?php echo "formCompany".$value->id;?>"
      action="../company/companyPage.php"
    >
    <input type="hidden" name="id" value="<?php echo $value->company->id;?>">
</form>

<div class='blockRelative'
     style='top:10;border: 1px solid #ccc;margin-top:10;margin-bottom:10;border-radius:10;padding:10' >
	
<form action="<? echo "../news/mainNewsForm.php"; ?>" method="get"
      id="<?php echo "formSubmitInner".$value->id ?>">

	<div class=" blockRelative" style="width:570;height: 300;" >
		
		<div class="container">
			<div class="" style="width: 50;height: 300;">
                <?php
                    $firstMainNewsImage = "";
                    preg_match("#<img src=[\"\'](.+?)[\"\'](.*)/>#si", $value -> content, $firstMainNewsImage);
                    $firstMainNewsImage = $firstMainNewsImage[1];
                    $firstMainNewsImage = $firstMainNewsImage==""? $value->company->logo:$firstMainNewsImage;
                ?>
				<div style=" width:112;height: 112; margin-top: -20" class="clickable"
						onclick="document.getElementById('<?php echo "formSubmitInner".$value->id ?>').submit()">
					<img src="<?php echo $firstMainNewsImage;?>" style="width: 112; height: 112;" 	
				</div>
			</div>
				<div style="width:100;height: 160"></div>
			</div>
			
			<div class="" style="width: 20;height: 300">
			</div>
			<div class="" style="width: 520;height: 300;">
				<div>
					<div class="textParagraph textLeft hoverTextUnderline clickable commonTextNewsHeader" style="margin-top:10; min-height: 50;font-weight: normal;color: DarkSlateBlue "
						onclick="document.getElementById('<?php echo "formSubmitInner".$value->id ?>').submit()">
						<?php echo $value -> header; ?>		
					</div>
				</div>
				<div class="horizontalSeparator" style="margin-top: 0;"> </div>	
				
				<div class="container" style="top:10;">
  					<div class="dateImage"></div>
  					<div class="greyText leftSpace">
                        <?php echo $value -> order -> datePublish; ?>
                    </div>
  					<div class="leftSpace tagsImage"></div>
  					<div class="leftSpace greyText hoverTextUnderline clickable hrefWrapper"
  						onclick="
  							document.getElementById('s6').value 
  							= document.getElementById('<?php echo "idCategoryNews".$value->id; ?>').value;
  							SetUnivaersalJSONAjax(document.getElementById('<?php echo "idCategoryNews".$value->id; ?>').value, 'idMainNewsForm', '../ajaxPages/crud.php', 'sortMainNewsByCategoryId');"
  					>
  						<?php
						$textCategory = "";
						$idCategory = "";
						foreach ($value->categories as $key => $value1) {
							$textCategory = $value1 -> name;
							$idCategory = $value1->id;
							break;
						}
						echo $textCategory;
  						?>
  					</div>  					
  					<input type="hidden" id="<?php echo 'idCategoryNews'.$value->id; ?>" value="<?php echo $idCategory; ?>"/>

                    <?php
                        $imgSrcCompany = $value -> company -> logo;
                    ?>

                    <div class="container">
                        <div style="width:20;background-color: DarkSlateGray" class="clickable"
                             onclick="document.getElementById('<?php echo 'formCompany'.$value->id;?>').submit();"
                            >
                            <img width="20" height="20" src="<?php echo $imgSrcCompany;?>"/>
                        </div>

                        <div class="greyText leftSpace clickable hoverTextUnderline"
                             onclick="document.getElementById('<?php echo 'formCompany'.$value->id;?>').submit();"
                            >
                            <?php echo $value -> company -> name; ?>
                        </div>
                    </div>
  				</div>
  				
  				<div class="horizontalSeparator" style="margin-top: 20;"> </div>
  				
  				<div style="min-height: 130;margin-top: 10;margin-bottom: 5">
	  				<div class="commonTextNewsContent textLeft commonTextColor commonNewsContentFont" id="text" style="height: 140;width: 410;" >
	  				    <?php
                            echo implode(' ', array_slice(explode(' ', $value -> content),0,16)).'...';
                        ?>
		  			</div>
	  			</div>
	  			
	  			<div class="wrapper_hover">
	  				<input type="submit" value="Подробнее" class="commonTextStyle companyTab borderNone clickable blockRelative" style=" left:-150"/>
					<input type="hidden" name="id" value="<?php echo $value -> id; ?>" />
	  			</div>
  					
				<div class="" style="height: 5">
					
				</div>				
			</div>
			
		</div>
	  	
	</div>
</form>
</div>
<?php }?>