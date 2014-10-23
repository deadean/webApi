var main = {
    logoPath: "images/logos/"
};

main.init = function() {
    this.server.init();
    
    this.interface.initTemplates();
    this.interface.messagePopup.init();
    this.interface.alertPopup.init();
    
   // this.interface.showInterface('mailToClient', {clientId: 1});
};