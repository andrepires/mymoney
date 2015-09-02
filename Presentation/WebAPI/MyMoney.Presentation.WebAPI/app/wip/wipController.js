(function () {
    "use strict";
    const controllerId = "wipController";

    function wipController($scope, $location, common, config, datacontext, bootstrapDialog) {
        var vm = this;

        function getWipSummary() {
             vm.wip = datacontext.zStorageWip.getWipSummary();
        }

        function cancelAllWip() {
            vm.isDeleting = true;

            function confirmDelete() {
                datacontext.zStorageWip.clearAllWip();
                vm.isDeleting = false;
            }

            function cancelDelete() { vm.isDeleting = false; }

            return bootstrapDialog.deleteDialog("Work in Progress")
                .then(confirmDelete, cancelDelete);
        }
        
        function gotoWip(wipData) {
            $location.path("/" + wipData.routeState + "/" + wipData.key);
        }

        function setSort(prop) {
            vm.predicate = prop;
            vm.reverse = !vm.reverse;
        }

        function activate() {
            common.activateController([getWipSummary()], controllerId);

            $scope.$on(config.events.storage.wipChanged, function (event, data) {
                vm.wip = data;
            });
        }

        vm.title = "Work in Progress";
        vm.wip = [];
        vm.cancelAllWip = cancelAllWip;
        vm.gotoWip = gotoWip;
        vm.predicate = "";
        vm.reverse = false;
        vm.setSort = setSort;
        
        activate();
    }

    angular.module("app").controller(controllerId, ["$scope", "$location", "common", "config", "datacontext", "bootstrap.dialog", wipController]);

})();