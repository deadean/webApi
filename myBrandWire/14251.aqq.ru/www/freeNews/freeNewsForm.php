<?php
foreach ($news as $key => $value) {
	echo "<div class='blockRelative' style='top:10;border: 1px solid #ccc;margin-top:10;margin-bottom:10;border-radius:0;padding:10' >";?>

	<form method="post" enctype="multipart/form-data" id="<?php echo "formCompany".$value->id;?>"
		  action="../company/companyPage.php"
		>
		<input type="hidden" name="id" value="<?php echo $value->company->id;?>">
	</form>

<form action="<? echo "../news/mainNewsForm.php";?>" method="get" id="<?php echo "formSubmitInner".$value->id ?>">
	<div class=" blockRelative" style="width:570;height: 185;" >
		
		<div class="container ">
			<div class="" style="width: 570;height: 200;">
				<div class="blockFreeNews blockRelative" style="left:489;top:-10"><div>новости</div></div>
				
				<div class="blockRelative" style="width: 510;top:-10">
					<div class="textParagraph textLeft hoverTextUnderline clickable" style="min-height: 50;font-weight: normal;color: DarkSlateBlue "
						onclick="document.getElementById('<?php echo "formSubmitInner".$value->id ?>').submit()"
					>
						<?php echo $value->header; ?>		
					</div>
				</div>
					
				<div class="container" style="top:-20;">
  					<div class="dateImage"></div>
  					<div class="greyText leftSpace"><?php echo $value -> order -> datePublish; ?></div>
					<?php
						$imgSrcCompany = $value -> company -> logo;
					?>
  					<div class="leftSpace tagsImage"></div>
  					<div class="leftSpace greyText hoverTextUnderline clickable hrefWrapper "
						 onclick="
							 document.getElementById('s6').value
							 = document.getElementById('<?php echo "idCategoryNews".$value->id; ?>').value;
							 SetUnivaersalJSONAjax(
							 document.getElementById('<?php echo "idCategoryNews".$value->id; ?>').value
							 , 'idFreeNewsForm'
							 , '../ajaxPages/crud.php'
							 , 'sortFreeNewsByCategoryId');"
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
  				
  				<div style="min-height: 70;margin-top: 0">
	  				<div class="commonTextFont textLeft commonTextColor" >
		  				<?php
                            echo implode(' ', array_slice(explode(' ', $value -> content),0,16)).'...';
                        ?>
		  			</div>
	  			</div>
	  			
	  			<div class="wrapper_hover">
	  				<input type="submit" value="Подробнее" class="commonTextStyle companyTab borderNone clickable blockRelative" style="left:210;top:-10"/>
					<input type="hidden" name="id" value="<?php echo $value->id;?>" />
	  			</div>
  					
				<div class="" style="height: 5">
					
				</div>
			</div>
			
		</div>
	  	
	</div>
</form>

<?php
	echo "</div>"; }?>