main.interface.interfaces.clients = {
    template: '',
            
    clients: null,       
    filter: ko.observable(""),
            
    init: function(callback) {
        main.server.request('Clients', 'getClients', {}, function(clients) {
            main.interface.interfaces.clients.clients = clients;
            
            callback();
        });
    },
            
    editClientClick: function(model) {
        main.interface.showInterface("editClient", {clientId: model.id});
    }
};


