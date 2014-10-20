<div class="" style="height: auto;max-width: 300;margin-top: 10;margin-left: 20;margin-bottom: 0">

        <?php
            if(!is_null($companyContacts)){
                //$checkedState = count($companyContacts)==1?"checked":"";
                //checked="<?php echo $checkedState;
                foreach($companyContacts as $item){ ?>

                    <div class="container">
                        <div><input type="checkbox"  name="contacts[]" id="<?php echo $item->id;?>" value="<?php echo $item->id;?>"></div>
                        <div><span><?php echo $item->firstname;?></span></div>
                    </div>

        <?php } } ?>

</div>