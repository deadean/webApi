main.interface.interfaces.editClient = {
    template: '',
            
    client: null,
            
    init: function(callback, params) {
        main.server.request("Clients", "getClient", {id: params.clientId}, function(client) {
            main.interface.interfaces.editClient.client = client;
            callback();
        });
    },
    
    clickNew: function(model) {
        main.interface.showInterface("editBid", {id: model.id});
    },
            
    mailToClient: function() {
        main.interface.showInterface("mailToClient", {clientId: this.client.id});
    }
    
};


