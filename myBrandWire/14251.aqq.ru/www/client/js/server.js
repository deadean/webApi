main.server = {
    //url: "http://http://mybrandw.s66.r53.com.ua/server",
    url: "http://14251.aqq.ru/server",
};

main.server.init = function() {
};

main.server.request = function(controller, method, data, handler) {
    $.ajax({
        url: main.server.url + '/' + controller + '/' + method,
        data: data,
        dataType: "json",
        success: function(data) {
            if (!main.server.isError(data)) {
                handler(data.info);
            }
        }
    });
};

main.server.isError = function(data) {
    if (data.status === 'ok')
        return false;
        
    main.interface.messagePopup.show('Ошибка', 'Во время запроса на сервер произошла следующая ошибка: ' + data.info.errorDescription);
    return true;
};

