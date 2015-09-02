﻿(function () {
    "use strict";

    var app = angular.module("app");

    var serviceId = "routemediator";

    // We inject $route because of an
    // issue in Angular that requires $route 
    // to be loaded to handle the routing
    function routemediator($location, $rootScope, $route, config, logger) {
        var handlingRouteChangeError = false;

        function handleRoutingErrors() {
            // Route cancelation:
            // On routing error, go to the dashboard.
            // Provide an exit clause if it tries to do it twice.
            $rootScope.$on("$routeChangeError",
                function (event, current, previous, rejection) {
                    if (handlingRouteChangeError) { return; }
                    handlingRouteChangeError = true;
                    var destination = (current && (current.title || current.name || current.loadedTemplateUrl))
                        || "unknown target";
                    var msg = "Error routing to " + destination + ". " + (rejection.msg || "");
                    logger.logWarning(msg, current, serviceId, true);
                    $location.path("/");
                }
            );
        }

        function updateDocTitle() {
            $rootScope.$on("$routeChangeSuccess",
                function (event, current, previous) {
                    handlingRouteChangeError = false;
                    var title = config.docTitle + " " + (current.title || "");
                    $rootScope.title = title; // data bind to <title>
                }
            );
        }

        function setRoutingEventHandlers() {
            handleRoutingErrors();
            updateDocTitle();
        }

        var service = {
            setRoutingEventHandlers: setRoutingEventHandlers
        };
        return service;

    }

    app.factory(serviceId,
        ["$location", "$rootScope", "$route", "config", "logger", routemediator]);

})();