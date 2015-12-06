var hub = $.connection.statsHub;

$.extend(hub.client, {
    updateStats: function (id, specs, cpuLoad, cpuFreq, ram) {
        if ($("#" + id).length) {
            $("#" + id).html("<td>" + specs + "</td><td>" + cpuLoad + "</td><td>" + cpuFreq + "</td><td>" + ram + "</td>")
        } else {
            $("#tbl").append("<tr id='" + id + "'><td>" + specs + "</td><td>" + cpuLoad + "</td><td>" + cpuFreq + "</td><td>" + ram + "</td>");
        }
            
    },
    removeStats: function(id) {
        if ($("#" + id).length) {
            $("#" + id).remove();
        }
    }
});

$.connection.hub.start();