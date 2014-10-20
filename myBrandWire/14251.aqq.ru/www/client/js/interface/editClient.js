main.interface.interfaces.editClient = {
    template: '',
    
    isEdit: ko.observable(false),
    client: null,
            
    init: function(callback, params) {
        main.server.request("Clients", "getClient", {id: params.clientId}, function(client) {
            main.interface.interfaces.editClient.client = client;
            for (var i = 0; i < client.news.length; i++) {
                client.news[i].color = main.interface.interfaces.bids.getBidColor(client.news[i]);
            }
            callback();
        });
    },
            
    onReady: function() {
        main.interface.interfaces.editClient.isEdit(false);
    },
    
    clickNew: function(model) {
        main.interface.showInterface("editBid", {id: model.id});
    },
            
    mailToClient: function() {
        main.interface.showInterface("mailToClient", {clientId: this.client.id});
    },
            
    startEdit: function() {
        main.interface.interfaces.editClient.isEdit(true);
    },
            
    saveChanges: function() {
        main.server.request("Clients", "saveClientInfo", {
                client_id: this.client.id, 
                name: $('#client_name').val(),
                surname: $('#client_surname').val(),
                email: $('#client_email').val(),
                phone: $('#client_phone').val()
            },
            function (result) {
                main.interface.showInterface("editClient", {clientId: main.interface.interfaces.editClient.client.id});
            }
        );
    },
            
    removeClient: function() {
        main.server.request("Clients", "removeClient", {client_id: this.client.id}, function(result) {
           if (result) {
               main.interface.showInterface('clients');
           } 
        });
    }      
};


