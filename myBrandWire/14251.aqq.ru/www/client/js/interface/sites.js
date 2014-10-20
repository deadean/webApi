main.interface.interfaces.sites = {
    template: '',
            
    filter: ko.observable(""),                 
    showAddSiteForm: ko.observable(false),
            
    sites: [],
    categories: [],
            
    init: function(callback, params) {
        this.closeAddSiteForm();
        main.server.request("Sites", 'getSites', {}, function(sites) {
            main.interface.interfaces.sites.sites = sites;
            main.server.request("Sites", "getSitesCategories", {}, function(categories) {
                main.interface.interfaces.sites.categories = categories;
                callback();
            });
        });
    },
    
    addNewSite: function() {

    },
    
    openAddSiteForm: function() {
        $('#addSiteForm').ajaxForm(function() {
            main.interface.showInterface('sites');
        });
        this.showAddSiteForm(true);
    },
            
    closeAddSiteForm: function() {
        this.showAddSiteForm(false);
    },
            
    deleteSiteClick: function(model) {
        main.server.request("Sites", "removeSite", {id: model.idSite}, function(result) {
            main.interface.showInterface('sites');
        });
    } 
};


