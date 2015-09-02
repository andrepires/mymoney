﻿(function () {
    "use strict";

    var serviceId = "helper";

    function helper($location, common) {
        function replaceLocationUrlGuidWithId(id) {
            // If the current Url is a Guid, then we replace 
            // it with the passed in id. Otherwise, we exit.
            var currentPath = $location.path();
            var slashPos = currentPath.lastIndexOf("/", currentPath.length - 2);
            var currentParameter = currentPath.substring(slashPos - 1);

            if (common.isNumber(currentParameter)) { return; }

            var newPath = currentPath.substring(0, slashPos + 1) + id;
            $location.path(newPath);
        }

        var service = {
            replaceLocationUrlGuidWithId: replaceLocationUrlGuidWithId
        };

        return service;
    }

    angular.module("app").factory(serviceId, ["$location", "common", helper]);
})();