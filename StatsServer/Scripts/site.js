var hub = $.connection.statsHub;

$.extend(hub.client, {
    sendStats: function (specs,cpuLoad, cpuFreq, cpuTemp, ram) {
        $("#s").text(specs);
        $("#cL").text(cpuLoad);
        $("#cF").text(cpuFreq);
        $("#r").text(ram);
    }
});

$.connection.hub.start();