main.interface.interfaces.editBid = {
    template: '',
        
    info: null,        
            
    header: ko.observable(null),
    common: ko.observable(null),
    content: ko.observable(null),
    tags: ko.observable(null),
            
    init: function(callback, params) {
        main.server.request("Bids", "bidInfo", {id: params.id}, function(bidInfo) {
            main.interface.current.info = bidInfo;
            
            main.interface.current.header(bidInfo.header);
            main.interface.current.common(bidInfo.common);
            main.interface.current.content(bidInfo.content);
            main.interface.current.tags(bidInfo.tags);
            
            callback();
        });
    },
            
    onReady: function() {
        tinymce.init({
            selector : 'textarea:last',
            plugins : 'link image code',
            relative_urls : false,
            width : 500,
            height : 200,
            resize : false
        });
        jQuery('#datetimepicker').datetimepicker({
            format:'Y-m-d H:i:s',
            lang: 'ru'
        });
        jQuery('#datetimepicker').val(this.info.publish_time);
    },
    
    bindBidClick: function() {
        if (this.info.moderator) {
            main.interface.messagePopup.show('Ошибка', 'Другой модератор уже работает над данной заявкой!');
            return;
        }
        
        main.server.request("Bids", "bindBidToModerator", {id: this.info.id}, function(result) {
           if (result) {
               main.interface.showInterface("editBid", {id: main.interface.current.info.id});
           }
        });
    },
            
    spellCheckedClick: function() {
        main.server.request("Bids", "spellChecked", {id: this.info.id}, function(result) {
            main.interface.showInterface("editBid", {id: main.interface.current.info.id});
        });
    },
    
    removeTagsClick: function() {
        var model = this;
        main.server.request("Bids", "getBidInfoWithOutTags", {id: this.info.id}, function(info) {
            model.header(info.header);
            model.common(info.common);
            tinyMCE.activeEditor.setContent(info.content);
            model.tags(info.tags);
        });
    },
            
    saveBidInfoClick: function() {
        main.server.request("Bids", "saveBidInfo", {id: this.info.id, header: this.header(), common: this.common(), content: tinyMCE.activeEditor.getContent(), tags: this.tags(), publish_time: $('#datetimepicker').val()}, function(result) {
            main.interface.messagePopup.show('Системная новость', 'Изменения были успешно сохранены!');
            main.interface.showInterface("editBid", {id: main.interface.current.info.id});
        });
    },
            
    mailClientClick: function() {
        main.interface.showInterface("mailToClient", {clientId: this.info.idUser, newId: this.info.id});
    },
            
    approvalPassedClick: function() {
        main.server.request("Bids", "approvalPassed", {id: this.info.id}, function(result) {
            main.interface.messagePopup.show('Системная новость', 'Заявка прошла модерацию!');
        });
    },
    
    deliveryInfoClick: function() {
        main.interface.showInterface("newSites", {id: this.info.id});
    },
    
    makeReport: function() {
        window.location = "/server/Sites/makeReport?new_id=" + this.info.id;
    },
            
    deleteNew: function() {
        main.server.request("Bids", "deleteNew", {id: this.info.id}, function(result) {
            main.interface.showInterface("bids");
        });
    },

    setStatusPayed: function() {
        main.server.request("Bids", "setStatusPayed", {id: this.info.id}, function(result) {
            main.interface.showInterface("bids");
        });
    },

    setStatusEdit: function() {
        main.server.request("Bids", "setStatusEdit", {id: this.info.id}, function(result) {
            main.interface.showInterface("bids");
        });
    }
};


