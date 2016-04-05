var hub = $.connection.statsHub;
var stats = ko.observableArray();
//$.extend(hub.client, {
//    updateStats: function (id, specs, cpu, ram) {
//        if ($("#" + id).length) {
//            $("#" + id).html("<td>" + specs + "</td><td>" + cpu + "</td><td>" + ram + "</td>");
//        } else {
//            $("#tbl").append("<tr id='" + id + "'><td>" + specs + "</td><td>" + cpu + "</td><td>" + ram + "</td>");
//        }
            
//    },
//    removeStats: function(id) {
//        if ($("#" + id).length) {
//            $("#" + id).remove();
//        }
//    }
//});

var Stat = function (id, specs, cpu, ram) {
    var self = this;
    self.id = id;
    self.specs = ko.observable(specs);
    self.cpu = ko.observable(cpu);
    self.ram = ko.observable(ram);
};

$.extend(hub.client, {
    updateStats: function(id, specs, cpu, ram) {
        
        var i = ko.utils.arrayFirst(stats(), function (item) { return item.id === id });
        if (i == undefined) {
            stats.push(new Stat(id, specs, cpu, ram));
        } else {
            i.specs(specs);
            i.cpu(cpu);
            i.ram(ram);
        }

    },
    removeStats: function(id) {
        stats.remove(function(i) { return i.id === id; });
    }
});

ko.applyBindings(stats);
$.connection.hub.start();