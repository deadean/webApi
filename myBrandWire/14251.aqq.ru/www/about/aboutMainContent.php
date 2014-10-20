<p class=" container showAbout textParagraph commonNewsContentHeader" style="font-size: 107%;height:30;color: #333;font-size: 23px;line-height: 1em;
font-family: 'PFDinDisplayProMedium', Arial, Helvetica, sans-serif;font-weight: 700;">
	О компании
</p>
<p class=" showAbout commonTextStyle commonTextFont107 commonTextColor commonLineHeight textLeft ">

	Проект MyBrandWire был создан в конце 2013 года командой молодых специалистов по копирайтингу и продвижению товаров в сети интернет.
	Детально изучая тему роста продаж, мы решили уйти от традиционного распространения контента и больше сконцентрироваться на развитии и продвижении культуры бренда.
</p>
<p class=" showAbout commonTextStyle commonTextFont107 commonTextColor commonLineHeight textLeft ">

	Мы считаем, что главной составляющей успешной маркетинговой системы  есть общение бренда со своей целевой аудиторией.
	Перед предпринимателем стоит непростая задача: создание особенного характера бренда и его правильное позиционирование.
	Не многие русскоязычные владельцы бизнесов понимают, что правильный маркетинг помогает не только привлекать новых клиентов, но так же, влияет на узнаваемость
	торговой марки, формируя поток так называемых «фанатов бренда» (постоянных покупателей).
</p>
<p class=" showAbout commonTextStyle commonTextColor commonLineHeight  textLeft commonTextFont107">

	Поэтому, прежде всего, мы решили сделать так, чтобы журналист, аналитик или обычный читатель могли оценить деятельность компании не по одной вырванной из
	контекста новости, а по «истории» сформировавшейся вокруг бренда при помощи различного рода публикаций.
	Для этого мы разработали специальный профайл компании, который отлично подходит для создания многогранной информационной оболочки.
	Только так, на наш взгляд, можно составить объективное мнение о бренде в сети.
</p>
<p class=" showAbout commonTextStyle commonTextColor commonLineHeight  textLeft commonTextFont107">

	Мы идем в ногу со временем. Поэтому прекрасно понимаем значимость социальных сетей и микроблогов в формировании общественного мнения.
	Мы готовы оказать вам широкую поддержку на публичных страницах самых популярных сайтов рунета: ВКонтакте, Facebook, Одноклассники, Twitter, LiveJournal и др.
	Укрепляйте позиции своего бренда, экономьте время и заработаете деньги вместе с нами!
</p>
<div class="horizontalSeparator localHorizontalSeparator"></div>

<div class=" showAbout textParagraph"
style="font-size: 107%;height:30;color: #333;font-size: 23px;line-height: 1em;font-family: 'PFDinDisplayProMedium', Arial, Helvetica, sans-serif;font-weight: 700;"

>
	<a class="clickable hrefWrapper" href="<?php echo GetPageByPlace("Blog" . $_SERVER['PHP_SELF']); ?>"> Последние статьи в Блоге</a>
</div>

<div class=" showAbout container">
	<?php foreach ($news as $key => $value) {
	?>
	<div class=" newsContainer " >
		<form class="clickable " id="<?php echo "form" . $value -> id; ?>" action="<? echo GetPageByPlace("BlogNewsForm" . $_SERVER['PHP_SELF']); ?>">
			<div class=" commonTextStyle commonTextColor commonLineHeight hrefWrapper" style="text-align: left"
			onclick="document.getElementById('<?php echo "form" . $value -> id; ?>').submit()"
			>
				<?php echo $value -> header; ?>
			</div>
			<div class=" commonTextStyle contentNewsLength commonTextColor commonLineHeight hrefWrapper" style="text-align: left"
			onclick="document.getElementById('<?php echo "form" . $value -> id; ?>').submit()"
			>
				<?php echo $value -> content; ?>
			</div>

			<div class=" wrapper_hover" style="margin-top: 15">
				<input type="submit" value="подробнее" class="buttonMore commonTextStyle borderNone clickable"/>
				<input type="hidden" name="id" value="<?php echo $value -> id; ?>" />
			</div>
		</form>

	</div>

	<?php } ?>
</div>

<div class="layer_0_copy_2-holder footerStyle " style="top: 0" >
	<?php
	include_once "../footer/footer.php";
	?>
</div>
