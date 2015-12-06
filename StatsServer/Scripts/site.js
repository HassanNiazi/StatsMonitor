var hub = $.connection.statsHub;

$.extend(hub.client, {
    sendStats: function (specs,cpuLoad, cpuFreq, ram) {
        $("#s").text(specs);
        $("#cL").text(cpuLoad + '%');
        $("#cF").text(cpuFreq + 'MHz');
        $("#r").text(ram + '%');
    }
});

$.connection.hub.start();