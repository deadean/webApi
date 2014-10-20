<?php ?>
<script>
    function CountOrdersCost() {
        a = 10;
        b = 12;
        totalprice = parseInt($('#DollarPriceService1').text());
        totalprice += parseInt($('#checkboxPublish')[0].checked ? $('#DollarPriceService2').text() : 0);
        totalprice += parseInt($('#checkboxSpelling')[0].checked ? $('#DollarPriceService3').text() : 0);
        totalprice += parseInt($('#checkboxAddCompanySocialNetworks')[0].checked ? $('#DollarPriceService5').text() : 0);
        //totalprice += parseInt($('#checkboxSocialNetworks')[0].checked ? $('#DollarPriceService5').text() : 0);
        totalprice += parseInt($('#checkboxSocialNetworks6')[0].checked ? $('#DollarPriceService6').text() : 0);
        totalprice += parseInt($('#checkboxSocialNetworks7')[0].checked ? $('#DollarPriceService7').text() : 0);
        totalprice += parseInt($('#checkboxSocialNetworks8')[0].checked ? $('#DollarPriceService8').text() : 0);
        $('#idTotalDollarCost').text(totalprice);
        $('#idTotalEuroCost').text(totalprice * b);
        $('#idTotalHrnCost').text(totalprice * a);
        $('#HrnPriceService1').text(parseInt($('#DollarPriceService1').text()) * a);
        $('#HrnPriceService2').text(parseInt($('#DollarPriceService2').text()) * a);
        $('#HrnPriceService3').text(parseInt($('#DollarPriceService3').text()) * a);
        $('#HrnPriceService4').text(parseInt($('#DollarPriceService4').text()) * a);
        $('#HrnPriceService5').text(parseInt($('#DollarPriceService5').text()) * a);
        $('#HrnPriceService6').text(parseInt($('#DollarPriceService6').text()) * a);
        $('#HrnPriceService7').text(parseInt($('#DollarPriceService7').text()) * a);
        $('#HrnPriceService8').text(parseInt($('#DollarPriceService8').text()) * a);

        $('#EuroPriceService1').text(parseInt($('#DollarPriceService1').text()) * b);
        $('#EuroPriceService2').text(parseInt($('#DollarPriceService2').text()) * b);
        $('#EuroPriceService3').text(parseInt($('#DollarPriceService3').text()) * b);
        $('#EuroPriceService4').text(parseInt($('#DollarPriceService4').text()) * b);
        $('#EuroPriceService5').text(parseInt($('#DollarPriceService5').text()) * b);
        $('#EuroPriceService6').text(parseInt($('#DollarPriceService6').text()) * b);
        $('#EuroPriceService7').text(parseInt($('#DollarPriceService7').text()) * b);
        $('#EuroPriceService8').text(parseInt($('#DollarPriceService8').text()) * b);
    }
</script>

<div class=" container " style="position: relative;top:10">
    <div class=" adapteeNewsToTheMainNewsContainerServicesStyle">
        <div class="textParagraph clearfix mainServices" style="font-size:97%;">
            Базовые услуги
        </div>
        <div class="commonTextStyle clearfix mainServices" style="font-size:97%;">
            <input class="commonCheckBoxStyle" type="checkbox" name="checkboxShowProfile"
                   id="checkboxShowProfile" value="checkboxShowProfile" checked="true" onchange="CountOrdersCost();"
                   onclick="BlockSetCheckBox('checkboxShowProfile')"/>
            Показывать свой профайл блоггерам и журналистам.............
        </div>
        <div class="commonTextStyle clearfix mainServices" style="font-size:97%;">
            <input class="commonCheckBoxStyle" id="checkboxPublish" type="checkbox" name="checkboxPublish"
                   value="checkboxPublish"
                   onclick="
                       SetBlockEditorStatus('mce_31-body',this);
                       SetBlockEditorStatus('mce_34-body',this)
                       SetBlockEditorStatus('mce_23-open',this)
                   <?php
                   if (isset($_POST["checkboxPublish"]) || $_POST["mode"] == "edit") {
                       echo "BlockSetCheckBox('checkboxPublish')";
                   }
                   ?>
                       "
                   onchange="CountOrdersCost();"
                />
            Распространить по базе СМИ...............................................
        </div>
        <div class="commonTextStyle clearfix mainServices" style="font-size:97%;">
            <input class="commonCheckBoxStyle" type="checkbox" name="checkboxSpelling" value="checkboxSpelling"
                   id="checkboxSpelling" onchange="CountOrdersCost();"
                   onclick="<?php if ($_POST['mode'] == 'edit') echo "BlockSetCheckBox('checkboxSpelling')"; ?>"/>
            Проверить орфографию........................................................
        </div>

        <!--<div class="commonTextStyle clearfix mainServices" style="font-size:97%;">
                <input class="commonCheckBoxStyle" type="checkbox" name="checkboxAddCompanySocialNetworks"
                       value="checkboxAddCompanySocialNetworks" id="checkboxAddCompanySocialNetworks"
                       onchange="CountOrdersCost();"
                       onclick="<?php if ($_POST['mode'] == 'edit') echo "BlockSetCheckBox('checkboxAddCompanySocialNetworks')"; ?>"
                    />
                Прикрепить соц. стети........................................................
            </div>-->


    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator ">
        <div></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerPrices">
        <div
            class="adapteeNewsToTheMainNewsContainerPricesItemCurrency adapteeNewsToTheMainNewsContainerPricesItemCurrencyGrn "></div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="HrnPriceService1">
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="HrnPriceService2">
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="HrnPriceService3">
        </div>
        <!--<div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="HrnPriceService4">
        </div>-->


    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator ">
        <div></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerPrices">
        <div
            class="adapteeNewsToTheMainNewsContainerPricesItemCurrency adapteeNewsToTheMainNewsContainerPricesItemCurrencyDollar "></div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="DollarPriceService1">
            0
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="DollarPriceService2">
            110
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="DollarPriceService3">
            120
        </div>
        <!--<div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="DollarPriceService4">
            130
        </div>-->


    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator ">
        <div></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerPrices">
        <div
            class="adapteeNewsToTheMainNewsContainerPricesItemCurrency adapteeNewsToTheMainNewsContainerPricesItemCurrencyEuro"></div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="EuroPriceService1">
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="EuroPriceService2">
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="EuroPriceService3">
        </div>
        <!--<div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="EuroPriceService4">
        </div>-->

    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator ">
        <div></div>
    </div>

</div>

<div class=" container " style="position: relative;top:10">
    <div class=" adapteeNewsToTheMainNewsContainerServicesStyle" style="vertical-align: top;padding-top: 10">
        <div class="textParagraph clearfix mainServices" style="font-size:97%;">
            Прикрепить
        </div>
        <div class="commonTextStyle clearfix mainServices" style="font-size:97%;">
            <input class="commonCheckBoxStyle" type="checkbox" name="checkboxAddCompanySocialNetworks"
                   value="checkboxAddCompanySocialNetworks" id="checkboxAddCompanySocialNetworks" onchange="CountOrdersCost();"
                   onclick="
                       <?php
                   if ($_POST['mode'] == 'edit') echo "BlockSetCheckBox('checkboxAddCompanySocialNetworks')";
                   else echo "if(this.checked){par=this.name;".$onClickCheckboxSocialNetworks7."}";
                   ?>"

                />
            Соц Сети .............................................................................

        </div>
        <div class="commonTextStyle clearfix mainServices" style="font-size:97%;">
            <input class="commonCheckBoxStyle" type="checkbox" name="checkboxSocialNetworks6"
                   value="checkboxSocialNetworks6" id="checkboxSocialNetworks6" onchange="CountOrdersCost();"
                   onclick="
                       <?php
                   if ($_POST['mode'] == 'edit') echo "BlockSetCheckBox('checkboxSocialNetworks6')";
                   else echo "if(this.checked){par=this.name;".$onClickCheckboxSocialNetworks7."}";
                   ?>"
                />
            О компании..........................................................................

        </div>
        <div class="commonTextStyle clearfix mainServices" style="font-size:97%;">
            <input class="commonCheckBoxStyle" type="checkbox" name="checkboxSocialNetworks7"
                   value="checkboxSocialNetworks7" id="checkboxSocialNetworks7" onchange="CountOrdersCost();"
                   onclick="
                       <?php
                   if ($_POST['mode'] == 'edit') echo "BlockSetCheckBox('checkboxSocialNetworks7')";
                   else echo "if(this.checked){par=this.name;".$onClickCheckboxSocialNetworks7."}";
                   ?>"
                />
            Адрес компании...................................................................

        </div>


        <div class="commonTextStyle clearfix mainServices" style="font-size:97%;">

            <input type="hidden" name="action" value="test"/>

            <input class="commonCheckBoxStyle" type="checkbox" name="checkboxSocialNetworks8"
                   value="checkboxSocialNetworks8" id="checkboxSocialNetworks8" onchange="CountOrdersCost();"
                   onclick="<?php
                   if ($_POST['mode'] == 'edit') {
                       echo "BlockSetCheckBox('checkboxSocialNetworks8')";
                   } else {
                       echo "ChangeSlideDownAnimation('idBlockContacts','name-help'); if(this.checked){".$onClickCheckboxSocialNetworks8."}";
                   }?>"
                />
            Контакты..........................................................................
        </div>
        <div class="commonTextStyle clearfix mainservices name-help" style="display: none;height: 30"
             id="idBlockContacts">
        </div>
        <div class="" style="display: none" id="idBlockContacts2"></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator ">
        <div></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerPrices" style="vertical-align: top">
        <div class="adapteeNewsToTheMainNewsContainerPricesItemCurrency adapteeNewsToTheMainNewsContainerPricesItemCurrencyGrn "></div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="HrnPriceService5">
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="HrnPriceService6">
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="HrnPriceService7">
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="HrnPriceService8">
        </div>

    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator " style="vertical-align: top">
        <div></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerPrices" style="vertical-align: top">
        <div
            class="adapteeNewsToTheMainNewsContainerPricesItemCurrency adapteeNewsToTheMainNewsContainerPricesItemCurrencyDollar "></div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="DollarPriceService5">
            0
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="DollarPriceService6">
            0
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="DollarPriceService7">
            0
        </div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="DollarPriceService8">
            0
        </div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator " style="vertical-align: top">
        <div></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerPrices" style="vertical-align: top">
        <div
            class="adapteeNewsToTheMainNewsContainerPricesItemCurrency adapteeNewsToTheMainNewsContainerPricesItemCurrencyEuro"></div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="EuroPriceService5"></div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="EuroPriceService6"></div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="EuroPriceService7"></div>
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" id="EuroPriceService8"></div>

    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator ">
        <div></div>
    </div>
</div>

<div class=" container " style="position: relative;top:10">
    <div class=" adapteeNewsToTheMainNewsContainerServicesStyle">
        <div class="commonTextStyle clearfix totalCost greyText" style="font-size:97%;margin-left: 165">
            Обшая сумма заказа............................................................
        </div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator " style="height: 20">
        <div></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerPrices">
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" style="margin-top: 30"
             id="idTotalHrnCost"></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator " style="height: 20">
        <div></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerPrices">
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" style="margin-top: 30"
             id="idTotalDollarCost"></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator " style="height: 20">
        <div></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerPrices">
        <div class="adapteeNewsToTheMainNewsContainerPricesItem commonTextStyle" style="margin-top: 30"
             id="idTotalEuroCost"></div>
    </div>
    <div class="adapteeNewsToTheMainNewsContainerSeparator " style="height: 20">
        <div></div>
    </div>
</div>