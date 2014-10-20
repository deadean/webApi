<div id="<?php echo "modal-8".$addedId;?>" class="modal" data-modal-effect="flip-vertical">
	<div class="modal-content">
		<h2 style="margin-bottom: 10"><?php echo $headerMessage ?></h2>
		<p style="text-align: left;line-height: 1.5;" class="commonTextFont">
			<?php echo $contentMessage1 ?>
		</p>
		<p style="text-align: left;line-height: 1.5;" class="commonTextFont">
			<?php echo $contentMessage2 ?>
		</p>
		<?php echo $addedContent; ?>
		<?php
			$btn = isset($isNotCloseByActionOk) ? $actionBtnOk : "modules.modals.closeModal($('.modal-trigger'),'modal-8');".$actionBtnOk;
		?>
		<div style="top:20 ;left:180" class="btn btn-primary btn-medium modal-close blockRelative commonTextFont"
		onclick="<?php echo $btn;?>">
			Ок
		</div>
		<?php echo $btnCancel; ?>
	</div>
</div>
<div id="<?php echo "btnShowMes".$addedId;?>" class='modal-trigger'
     onclick="modules.modals.openModal($('.modal-trigger'),'modal-8<?php echo $addedId;?>');"></div>


<!--<script src='../js/jqueryModalWindow.js'></script>-->
<script src='../js/index.js'></script>

<script src='../../js/index.js'></script>

<?php
	if($isShow=="1") 
		echo "<script>document.getElementById('btnShowMes".$addedId."').click();</script>";
?>
