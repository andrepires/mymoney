﻿(function () {
    "use strict";

    angular.module("app").service("config.breeze", ['$q', '$http', "config", configBreeze]);

    function configBreeze($q, $http, config) {
        function configureForWebApi() {
            var serviceName = config.remoteServiceName + "/api";
            // use $q for promises
            //use$q($q);

            // use the current module's $http for ajax calls
            var ajax = breeze.config.initializeAdapterInstance('ajax', 'angular');
            ajax.setHttp($http);

            // backingStore works for Angular (ES5 property 'ready')
            breeze.config.initializeAdapterInstance('modelLibrary', 'backingStore', true);

            // Tell breeze not to validate when we attach a newly created entity to any manager.
            // We could also set this per entityManager
            new breeze.ValidationOptions({ validateOnAttach: false }).setAsDefault();

            //breeze.NamingConvention.camelCase.setAsDefault();

            return {
                appTitle: "My Money",
                hasServerMetadata: false,
                serviceType: "webapi",
                serviceName: serviceName
            }
        }

        return configureForWebApi();
    }
})();
