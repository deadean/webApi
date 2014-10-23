main.interface.interfaces.mailToClient = {
    template: '',
    
    newInfo: null,        
    client: null,
    
    subject: ko.observable(""),
    message: ko.observable(""),
    
    selectedDefaultId: ko.observable(""),
    
    defaultContent: [
        {
            name: "Button 1",
            subject: "Subject 1",
            message: "Default message 1"        
        },
        {
            name: "Button 2",
            subject: "Subject 2",
            message: "Default message 2"
        },
        {
            name: "Button 3",
            subject: "Subject 3",
            message: "Default message 3"
        }
    ],
            
    init: function(callback, params) {
        main.server.request("Clients", "getClient", {id: params.clientId}, function(client) {
            main.interface.interfaces.mailToClient.client = client;
            if (params.newId) {
                main.server.request('Bids', "bidInfo", {id: params.newId}, function(newInfo) {
                    main.interface.interfaces.mailToClient.newInfo = newInfo;
                    callback();
                });
            }
            else {
                callback();
            }
        });
    },
            
    setDefaultInfo: function(content) {
        main.interface.interfaces.mailToClient.subject(content.subject);
        main.interface.interfaces.mailToClient.message(content.message);
    },
            
    sendMessage: function() {
        if (this.subject() == "") {
            main.interface.messagePopup.show("Error", "Mail subject is empty");
            return;
        }
        
        if (this.message() == "") {
            main.interface.messagePopup.show("Error", "Mail message is empty");
            return;
        }
        
        var params = {
            client_id: this.client.id,
            subject: this.subject(),
            message: this.message()
        };
        
        main.server.request("Clients", "sendMessageToClient", params, function(result) {
            if (result) {
                main.interface.messagePopup.show('Success', "Mail was send to client");
            }
        });
    }
};


