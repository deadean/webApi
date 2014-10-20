<form action="<? echo GetPageByPlace("BlogNewsForm".$_SERVER['PHP_SELF']);?>" method="get">
	<div class="">
		<div class="textParagraph textLeft" style="min-height: 50;font-weight: normal">
			<?php echo $value->header; ?>		
		</div>
		
		<div class="container blockPadding">
			<div class="dateImage"></div>
			<div class="greyText leftSpace">01.12.2013</div>
			<div class="leftSpace tagsImage"></div>
			<div class="leftSpace greyText">Теги</div>
	  	</div>
	  	
	  	<div style="min-height: 200; background: url(1.jpg) no-repeat;background-size: 100%">
	  		
	  	</div>
	  	
	  	<div class="commonTextFont textLeft" style="min-height: 50">
	  		<?php echo $value->content; ?>
	  	</div>
	  	
	  	<div class="wrapper_hover blockPadding" style="position: relative; left:-233;">
				<input type="submit" value="Подробнее" class="commonTextStyle companyTab borderNone clickable"/>
				<input type="hidden" name="id" value="<?php echo $value->id;?>" />
  		</div>
	  	
	  	
	</div>
</form>