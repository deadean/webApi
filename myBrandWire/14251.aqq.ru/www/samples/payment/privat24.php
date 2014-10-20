<form action="https://api.privatbank.ua/p24api/ishop" method="POST">
    <input type="text" name="amt" value="сумма"/>
    <input type="text" name="ccy" value="валюта (UAH / USD / EUR)" />
    <input type="hidden" name="merchant" value="ID мерчанта" />
    <input type="hidden" name="order" value="уникальный код операции" />
    <input type="hidden" name="details" value="назначение платежа" />
    <input type="hidden" name="ext_details"
           value="дополнительные данные (код товара, и т.п.) /можно оставить пустым/" />
    <input type="hidden" name="pay_way" value="privat24" />
    <input type="hidden" name="return_url" value="страница, принимающая клиента после оплаты" />
    <input type="hidden" name="server_url" value="страница, принимающая ответ API о результате платежа" />
    <input type="submit" value="Оплатить" />
</form>

<a href="http://www.interkassa.com/" title="INTERKASSA" target="_blank">
    <img src="http://www.interkassa.com/img/ik_88x31_01.gif" alt="INTERKASSA" />
</a>