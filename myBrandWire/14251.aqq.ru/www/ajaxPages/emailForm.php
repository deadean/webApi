<form action="../model/mainController.php" method="post">
	
	<input type="hidden" name="check" value="name:Ваше имя?!.email:Укажите правильный e-mail!.text:Напишите сообщение!.captcha:Ошибка в проверочном коде!"/>
	<input type="hidden" name="subject" value="Сообщение с сайта"/>
	<input type="hidden" name="colors" value="ffffff.ffffee.880000.880000"/>
	
	<div class="commonTextFont" 
	style="
			-webkit-box-shadow: 18px 15px 56px 10px rgba(0,0,0,0.79);
			-moz-box-shadow: 18px 15px 56px 10px rgba(0,0,0,0.79);
			box-shadow: 18px 15px 56px 10px rgba(0,0,0,0.79);
			margin-left:50%;position:relative;
			left:-200px; width:400px;background:WhiteSmoke;
			 border:1px solid #880000;">
		
		<div style="padding:2px;background:DarkSlateBlue;color:#ffff66;border:1px text-align:center;">
			<strong>Напишите Нам!</strong>
		</div>
		
		<div style="padding:0 15px 10px 15px;" >
			<div class="blockLeft" style="margin-top:10px;">Ваше имя</div>
			<input class="commonInputTextBox" name="name" type="text" style="margin-left:-1px;width:100%;" maxlength="50" />
			<div class="blockLeft" style="margin-top:10px;">Обратный e-mail</div>
			<input class="commonInputTextBox" name="email" type="text" style="margin-left:-1px;width:100%;" maxlength="50" />
			<div class="blockLeft" style="margin-top:10px;">Сообщение</div>
			<textarea class="commonTextFont" name="text" style="margin-left:-1px;width:100%;height:100px; " rows="5" cols="20"></textarea>
			<div class="wrapper_hover blockRelative" style="margin-top:10px; text-align:center; left:110">
				<input class="btnSeeNews borderNone clickable" type="submit" value="Отправить" />
				<input type="hidden" name="action" value="sendEmailToUs" />
				
			</div>
		</div>
	</div>
</form>