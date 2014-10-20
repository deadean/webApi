<div class="container newsFormItem">
	<div class="leftHeader commonTextStyle">
		<span>Заголовок*</span></br>(Минимум вводимых слов 4)
	</div>
	<div class="rightContanter">
		<input type="text" data-validate-words="1" required="required" name="newsHeader" id="idNewsHeader" class="leftItem commonInputTextBox" value="<?php echo $editableNews->header;?>" />
	</div>
</div>

<div class="container newsFormItem" style="top:10">
	<div class="leftHeader commonTextStyle">
		<span>Категории</span><br/>
        (3 категории максимум)

	</div>
	<div class="rightContanter commonTextStyle">
		<select id="s6" multiple="multiple">
			<?php
				foreach ($categories as $key => $value) 
				{
					echo "<option value='$value->id'>".$value->name."</option>";	
				}
			?>
	    </select>
      	<div id="returnS6"></div>
	</div>
</div>

<div class="container newsFormItem" style="height:200;">
	<div class="leftHeader commonTextStyle">
		<span>Общее</span></br>
		(250 символов максимум)
	</div>
	<div class="rightContanter commonTextStyle">
		<textarea required="required" class="leftItem edit" rows="10" cols="45" id="idNewsCommon" name="newsCommon"><?php echo $editableNews->common;?></textarea>
	</div>
</div>

<div class="container newsFormItem" style="height:20;margin-bottom: 0">
    <div class="leftHeader commonTextStyle">
        <span></span>
    </div>
    <div class="rightContanter commonTextStyle commonTextFont107 boldText" style="color: darkred">
        <span>Пожалуйста, не копируйте "Общее" в Основной текст</span>
    </div>
</div>

<div class="container newsFormItem" style="height:200;margin-bottom: 50">
	<div class="leftHeader commonTextStyle">
		<span>Основной текст</span>
	</div>
	<div class="rightContanter commonTextStyle">
		<textarea class="leftItem editme" id="idContentTextArea" rows="10" cols="45" name="newsContent">
            <?php echo $editableNews->content;?>
        </textarea>
	</div>
</div>

<?php if($_POST["mode"]!="edit"){?>
<div class="container newsFormItem" style="margin-top:120;height: 100">
	<div class="leftHeader commonTextStyle">
		<span>Комментарии</span></br>
	</div>
	<div class="rightContanter commonTextStyle">
		<textarea class="leftItem " rows="5" cols="45"
                  placeholder="Здесь вы можете написать свои пожелания редактору" id="idNewsComments" name="newsComments"></textarea>
	</div>
</div>
<?php } ?>

<div class="container newsFormItem" style="height: 10;margin-top: <?php if($_POST['mode']=='edit') echo '120'; else echo '10';?>">

    <div class="leftHeader commonTextStyle">

    </div>
    <div class="rightContanter commonTextStyle">
        <div id="the-count" class="">
            <label id="current">0</label>
            <label id="maximum">/ 50</label>
        </div>
    </div>
</div>

<div class="container newsFormItem" style="height: 50;margin-top: 10">

	<div class="leftHeader commonTextStyle">
		<span>Теги</span>
	</div>
	<div class="rightContanter commonTextStyle">
		<input type="text" class="leftItem" id="idNewsTags" name="newsTags" maxlength="50" id="txtAboutCompany" value="<?php echo $editableNews->tags;?>"/>
	</div>
</div>

<!--<div class="container newsFormItem" style="top:10">-->
<!--	<div class="leftHeader commonTextStyle">-->
<!--		-->
<!--	</div>-->
<!--	<div class="rightContanter commonTextStyle">-->
<!--		<div class="addFile"></div>-->
<!--		<span>Прикрепить файл</span>-->
<!--	</div>-->
<!--</div>-->



<div class="container newsFormItem" style="top:10;margin-bottom: 80">
	<div class="leftHeader commonTextStyle">
		
	</div>
	<div class="rightContanter commonTextStyle wrapper_hover">
		<input type="submit" value="Просмотреть Новость" class="commonTextStyle btnSeeNews borderNone clickable"/>
		<input type="hidden" id="returnS7" name="RES" />
		<input type="hidden" id="mode" name="mode" value="<?php echo $mode; ?>" />
		<input type="hidden" name="id" id="id" value="<?php echo $editableNews->id; ?>" />
	</div>
    <script>
        function PreviewOrder(){
            selectedCompanyId='';
            for(i=0;i<document.getElementsByName('companyId').length;i++)
                if(document.getElementsByName('companyId')[i].checked)
                    selectedCompanyId=document.getElementsByName('companyId')[i].value;

            var persons = new Array();
            for(i=0;i<document.getElementsByName('contacts[]').length;i++)
                if(document.getElementsByName('contacts[]')[i].checked)
                    persons[i] = document.getElementsByName('contacts[]')[i].value;

            orderObject = {
                mode : $('#mode').val(),
                newsHeader : $('#idNewsHeader').val(),
                newsContent : $('#idContentTextArea').val(),
                newsCommon : $('#idNewsCommon').val(),
                newsTags : $('#idNewsTags').val(),
                newsComments : $('#idNewsComments').val(),
                checkboxAddCompanySocialNetworks : $('#checkboxAddCompanySocialNetworks').val(),
                checkboxSocialNetworks6 : $('#checkboxSocialNetworks6').val(),
                checkboxSocialNetworks7 : $('#checkboxSocialNetworks7').val(),
                contacts : persons,
                RES : $('#returnS7').val(),
                companyId : selectedCompanyId,
                checkboxPublish : $('#checkboxPublish').val(),
                id : $('#id').val()
            };
            SetUnivaersalJSONAjax2(orderObject, 'idNewsAdapteeContentForm', '../../ajaxPages/crud.php', 'PreviewOrder',null);
        }
    </script>
</div>

<script>
    $('form').submit(function(e) {
        e.preventDefault();
        var isCanSubmit = false;
		var words = $('#idNewsHeader').val().split(' ');
		if(words.length>=4){
			isCanSubmit=true;
		}
		else{
			$('#btnShowMes').click();
		}

		if($('#returnS7').val()==""){
			isCanSubmit=false;
			modules.modals.openModal($('.modal-trigger'),'modal-8ctrMessageCategoryIsEmpty');
		}

        if(isCanSubmit && totalprice==0){
            modules.modals.openModal($('.modal-trigger'),'modal-8ctrMessageFreeNews');
            return false;
        }

		if(isCanSubmit){
			this.submit();
		}

        return true;
    });
</script>

