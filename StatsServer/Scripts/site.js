var hub = $.connection.statsHub;

$.extend(hub.client, {
    sendStats: function (cpuLoad, cpuFreq, cpuTemp, ram) {
        $("#cL").text(cpuLoad);
        $("#cF").text(cpuFreq);
        $("#cT").text(cpuTemp);
        $("#r").text(ram);
    }
});

$.connection.hub.start();