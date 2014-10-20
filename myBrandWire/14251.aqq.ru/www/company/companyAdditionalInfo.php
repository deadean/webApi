<div class="" style="margin-top: 10;margin-bottom: 10">

    <?php if($isAddCompanySocialNetworks){?>
    <div class="textParagraph" style="margin-bottom: 5">Мы в социальных сетях :</div>

    <?php foreach($currentCompany->communications as $item){
            //print_r($item);
            if($item->IsNetworkTypes($item->name))
                continue;

            $networkName = $item->name=="Facebook"?"Facebook":$item->name;
            $networkName = $item->name=="Vkontakte"?"Вконтакте":$item->name;
            $networkName = $item->name=="Googleplus"?"Google+":$item->name;
            $networkName = $item->name=="Odnoklassniki"?"Odnoklassniki":$item->name;
            $networkName = $item->name=="Twitter"?"Twitter":$item->name;
            $networkName = $item->name=="Tumblr"?"Tumblr":$item->name;
            $networkName = $item->name=="Instagramm"?"Instagramm":$item->name;
            $networkName = $item->name=="Pinterest"?"Pinterest":$item->name;
            $networkName = $item->name=="Linkedin"?"Linkedin":$item->name;
    ?>

            <div class="commonNewsContentFont blockPadding">
                <?php echo $networkName;?>
                :
                <a target="_blank" href="<?php echo $item->value;?>"><span style="text-decoration: underline;"><?php echo $item->value; ?></span></a>
            </div>

    <?php }} ?>

    <?php if($ischeckboxSocialNetworks6){?>
    <div class="textParagraph" style="margin-bottom: 5;margin-top: 10">О компании</div>
    <div class="commonNewsContentFont blockPadding">
        <?php echo $currentCompany->about; ?>
    </div>
    <?php }?>

    <?php if($isAddCompanyContacts){?>
    <div class="textParagraph" style="margin-bottom: 5;margin-top: 10">Адрес компании</div>
    <div class="commonNewsContentFont blockPadding">
        <div class=""><?php echo $currentCompany->region . ", "; ?></div>
        <div><?php echo $currentCompany->community; ?></div>

        <?php
            foreach ($currentCompany->communications as $key => $value) {
                if(!$value->IsNetworkTypes($value->name))
                    continue;

                if($value->name=="Skype"){
                    echo "<a target='_blank' href='".$value->value."'><div class='commonTextFont107 textLeft textunderline' style='margin-top:5'>" . $value->value . "</div></a>";
                }
                else{
                    echo "<div class='commonTextFont107 textLeft' style='margin-top:5'>" . $value->value . "</div>";
                }
            }
        ?>

    </div>
    <?php }?>

    <?php if($isAddCompanyContacts){?>
    <div class="textParagraph" style="margin-bottom: 5;margin-top: 10">Контактная информация</div>
    <div class="commonNewsContentFont" style="margin-left: 5">
        <?php foreach ($currentCompany->persons as $person) { if(in_array($person->id,$contacts)){?>
            <div class="" style="margin-top: 10;"><?php echo $person->firstname;?></div>
            <div class=""><?php echo $person->position;?></div>
            <?php
                foreach($person->communications as $communication){
                    $communicationName = $communication->name=="Skype"?"Skype":$communication->name;
                    $communicationName = $communication->name=="Phone"?"Тел.":$communication->name;
            ?>
                <div class=""><?php echo $communicationName." : ".$communication->value;?></div>
            <?php } ?>
        <?php }}?>

    </div>
    <?php }?>

</div>