<div><h4 style="margin-bottom: 10">Выберите социальные контакты Вашей компании</h4></div>
<?php $btn = "
				modules.modals.closeModal($('.modal-trigger'),'modal-8');
				var ids='';
				for(i=0;i<document.getElementsByName('ss').length;i++)
				{ if(document.getElementsByName('ss')[i].checked==true) ids+=document.getElementsByName('ss')[i].id+'_';};
				paramObject = {
					idsParam : ids,
					idCompany : document.getElementById('idCompany').value
				};
				SetUnivaersalJSONAjax3('../ajaxPages/crud.php','addSocialNetworksToCompany',paramObject,function(data){
						var str = JSON.parse(data);
						SetCompany1(document.getElementById('idCompany').value, str.logo.toString());
					});
			" ?>


<div>
	<ul class="textLeft">
	<?php foreach ($communications as $key => $value) { ?>
		
	<li style="padding: 4">
		<input type="checkbox" name="ss" id="<?php echo $value->id?>" value="<?php echo $value->name?>" />
		<label style="font-size: 16" for="<?php echo $value->id?>"><?php echo $value->name?></label>
	</li>
		
	<?php } ?>
	
	</ul>
</div>

<div style="top:20 ;left:180" class="btn btn-primary btn-medium modal-close blockRelative commonTextFont"
onclick="<?php echo $btn; ?>">
	Ок
</div>

