(function () {
    'use strict';

    var app = angular.module('app', [
        // Angular modules 
        'ngAnimate',        // animations
        'ngRoute',          // routing
        'ngSanitize',       // sanitizes html bindings (ex: sidebar.js)
        "ng-bootstrap-datepicker",
        // Custom modules 
        'common',           // common functions, logger, spinner
        'common.bootstrap', // bootstrap dialog wrapper functions

        // 3rd Party Modules
        'ui.bootstrap',      // ui-bootstrap (ex: carousel, pagination, dialog)
        'ngzWip',            // zStorage and zStorageWip
        'SignalR',
        //Security

    ]);

    // Handle routing errors and success events
    app.run(['routemediator', "config.breeze", "accountsHub", function (routemediator) {
        routemediator.setRoutingEventHandlers();
    }]);
})();