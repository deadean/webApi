main.interface.interfaces.bids = {
    template: '',
            
    filter: {
        payment: {
            free: ko.observable(true),
            paid: ko.observable(true)
        },
        status: {
            notPayed: ko.observable(false),
            notModerated: ko.observable(true),
            moderated: ko.observable(true)
        },
        moderator: {
            notBind: ko.observable(true),
            yours: ko.observable(true),
            notYours: ko.observable(false)
        }
    },
            
    bids: ko.observableArray(null),
            
    init: function(callback) {
        main.server.request("Bids", "getBids", {}, function(bids) {
           for (var i = 0; i < bids.length; i++) {
               var color = main.interface.interfaces.bids.getBidColor(bids[i]);
               if (color == "green") {
                   bids[i].commonStatus = "moderated";
                   bids[i].mouseOverColor = "#32CD32";
                   bids[i].mouseOutColor = "#00FA9A";
               }
               
               if (color == "orange") {
                   bids[i].commonStatus = "notModerated";
                   bids[i].mouseOverColor = "#EEB422";
                   bids[i].mouseOutColor = "#FFC125";
               }
               
               if (color == "gray") {
                   bids[i].commonStatus = "notPayed";
                   bids[i].mouseOverColor = "#bbbbbb";
                   bids[i].mouseOutColor = "#dddddd";
               }
           }
           main.interface.interfaces.bids.bids(bids);
           callback();
        });
    },
    
    clickBid: function(bid) {
        main.interface.showInterface("editBid", {id: bid.id});
    },
            
    getBidColor: function(bid) {
        var bg = "gray";
        if (bid.status != 'waiting_for_payment') {
            if (bid.status != 'moderated' && bid.status != 'published') {
                bg = "orange";
            }
            else {
                bg = "green";
            }
        }
        return bg;
    }
};



