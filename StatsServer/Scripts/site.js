var hub = $.connection.statsHub;

$.extend(hub.client, {
    sendStats: function (cpuLoad, cpuFreq, cpuTemp, ram) {
        console.log(cpuLoad, cpuFreq, cpuTemp, ram);
    }
});

$.connection.hub.start();