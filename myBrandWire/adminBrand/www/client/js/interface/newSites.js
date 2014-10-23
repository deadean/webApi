main.interface.interfaces.newSites = {
    template: '',
            
    filter: ko.observable(""),        
    selectedCategory: ko.observable(""),

    sites: [],
    newId: null,
    categories: [],
    
    statuses: ["notStarted", "verification", "published"],
    
    init: function(callback, params) {
        this.newId = params.id;
        this.sites = [];
        this.notUsedSites = [];
        main.server.request("Sites", "getNewSites", {new_id: params.id}, function(sites) {
            for (var i = 0; i < sites.length; i++) {
                var site = sites[i];
                site.link = ko.observable(decodeURIComponent(site.link));
                site.status = ko.observable(site.status);
                site.comment = ko.observable(site.comment);
                main.interface.interfaces.newSites.sites.push(site);
            }
            
            main.server.request("Sites", "getSitesCategories", {}, function(categories) {
               main.interface.interfaces.newSites.categories = categories; 
               callback();
            });
        });
    },
            
    addCategoryClick: function() {
        var newId = this.newId;
        if (this.selectedCategory != "") {
            main.server.request("Sites", "bindSitesCategoryToNew", {new_id: newId, category_id: this.selectedCategory}, function(result) {
               main.interface.showInterface('newSites', {id: newId}); 
            });
        }
    },
            
    saveSiteInfo: function(info) {
        console.log(info);
        main.server.request("Sites", "saveNewSiteInfo", {
            new_id: main.interface.current.newId,
            site_id: info.idSite,
            link: info.link() == null ? "" : info.link(),
            status: info.status,
            comment: info.comment() == null ? "" : info.comment()
        }, function(result) {
            main.interface.messagePopup.show('Сообщение', 'Данные успешно обновелены!');
        });
    }
};