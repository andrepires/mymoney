(function () {
    'use strict';
    var controllerId = 'admin';
    angular.module('app').controller(controllerId, ["$scope","$http", "common", "config", admin]);

    function admin($scope, $http, common, config) {
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var vm = this;
        var serviceName = config.remoteServiceName + "/api";
        vm.title = 'Admin';
        vm.persistenceMode = "";
        activate();

        function activate() {
            var promises = [getPersistenceMode()];
            common.activateController(promises, controllerId)
                .then(function () { log('Activated Admin View'); });
        }

        function getPersistenceMode() {
            return $http.get(serviceName + "/Admin/PersistenceMode").then(function (response) {
                return vm.persistenceMode = response.data.replace(/['"]+/g, '');
            })
        }

        $scope.setPersistenceMode = function () {
            return $http.put(serviceName + "/Admin/PersistenceMode/", '"' + vm.persistenceMode + '"').then(function () {
                log("The persistence mode was changed to " + vm.persistenceMode);
            });
        }
    }
})();