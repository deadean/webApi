main.interface = {
    current: null,
    interfaces: {},
            
    messagePopup: {
        title: ko.observable(''),
        info: ko.observable(''),
        buttons: ko.observableArray([]),
        
        init: function() {},
        show: function() {},
        hide: function() {}
    },
            
    alertPopup: {
        styles: {
            default: {
                icon: "",
                bgcolor: "#ffffff"
            },
            success: {
                icon: "images/save.png",
                bgcolor: "#ffffff"
            },
            error: {
                icon: "images/error.png",
                bgcolor: "#ff0000"
            }
        },
                
        style: ko.observable('default'),
        message: ko.observable(''),
        
        init: function() {},
        show: function(message, style, time) {},
        hide: function() {}
    },

    initTemplates: function() {},
    showInterface: function() {}
};

main.interface.initTemplates = function() {
    for (var name in this.interfaces) {
        $.ajax({
            url: "/client/templates/interfaces/" + name + ".html",
            async: false,
            cache: false,
            success: function(html) {
                main.interface.interfaces[name].template = html;
            }
        });
    }
};

main.interface.showInterface = function(interfaceName, params) {
    console.log("Init interface '"+interfaceName+"'");
    this.current = this.interfaces[interfaceName];
    this.current.init(function() {
        $('#interface').html(main.interface.current.template);
        ko.renderTemplate('interface', main.interface.current, {}, document.getElementById('interface'));
        if (main.interface.current.onReady != null) {
            main.interface.current.onReady();
        }
    }, params);
};

main.interface.messagePopup.init = function() {
    ko.renderTemplate('messagePopup', main.interface.messagePopup, {}, document.getElementById('messagePopup'));
};

main.interface.messagePopup.show = function(title, info, buttons, showCloseButton, interface) {
    if (buttons == null)
        buttons = [];
    
    if (showCloseButton == null)
        showCloseButton = true;
    
    if (showCloseButton == true) {
        buttons.push({name: 'Закрыть', click: function() {
            main.interface.messagePopup.close();
            if (interface != null)
                main.interface.initInterface(interface);
        }});
    }
    
    this.title(title);
    this.info(info);
    this.buttons(buttons);
    $('#messagePopup').show();
};

main.interface.messagePopup.close = function() {
    this.title('');
    this.info('');
    this.buttons([]);
    $('#messagePopup').hide();
};

main.interface.alertPopup.init = function() {
    ko.renderTemplate('alertPopup', main.interface.alertPopup, {}, document.getElementById('alertPopup'));
};

main.interface.alertPopup.show = function(message, style, time) {
    if (!time) {
        time = 3000;
    }
    this.message(message);
    this.style(style);
    $('#alertPopup').show();
    setTimeout(this.hide, time);
};

main.interface.alertPopup.hide = function() {
    $('#alertPopup').hide();
};