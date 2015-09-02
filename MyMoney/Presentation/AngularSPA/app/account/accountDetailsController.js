(function () {
    "use strict";
    var controllerId = "accountDetailsController";

    function accountDetailsController($location, $route, $routeParams, $window, $scope, config, common, helper, datacontext, model) {
        var vm = this;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = common.logger.getLogFn(controllerId, "error");
        var logWarning = common.logger.getLogFn(controllerId, "warn");
        var entityName = model.entityNames.account;
        var wipEntityKey = undefined;

        function goToAccountsList() {
             $location.path("/account");
        }

        function storeWipEntity() {
            if (!vm.account) return;
            const name = (vm.account.Name || "[New account]") + " " + vm.account.Oid;
            const routeState = "account";
            wipEntityKey = datacontext.zStorageWip.storeWipEntity(
                vm.account, vm.account.Oid, entityName, name, routeState);
        }

        function autoStoreWip(immediate) {
            common.debouncedThrottle(controllerId, storeWipEntity, 1000, immediate);
        }

        function getRequestedAccount() {
            var oid = $routeParams.oid;

            if (oid === "new") {
                return vm.account = datacontext.account.create();
            }
            return datacontext.account.getEntityByIdOrFromWip(oid)
                .then(function (accountData) {
                    if (accountData) {
                        wipEntityKey = accountData.key;
                        vm.account = accountData.entity || accountData;
                    } else {
                        logWarning("Could not find account oid = " + oid);
                        goToAccountsList();
                    }

                }).catch(function (error) {
                    logError("Error while getting account oid = " + oid + ";" + error);
                    goToAccountsList();
                });
        }
        
        function onEveryChange() {
            $scope.$on(config.events.entitiesChanged,
                function (event, data) {
                    autoStoreWip();
                });
        }

        function onDestroy() {
            $scope.$on("$destroy", function () {
                autoStoreWip(true);
                datacontext.cancel();
            });
        }

        function onHasChanges() {
            $scope.$on(config.events.hasChangesChanged,
                function (event, data) { vm.hasChanges = data.hasChanges; });
        }

        function activate() {
            onDestroy();
            onHasChanges();
            var promises = [getRequestedAccount()];
            common.activateController(promises, controllerId)
                .then(function () {
                    log("Activated Account Detail View");
                }).then(onEveryChange);
        }

        function goBack() {
            $window.history.back();
        }

        function canSave() {
            return vm.hasChanges && !vm.isSaving;
        }

        function removeWipEntity() {
            datacontext.zStorageWip.removeWipEntity(wipEntityKey);
        }
        
        function save() {
            if (!canSave()) { return $q.when(null); }

            vm.isSaving = true;
            return datacontext.save().then(function (saveResult) {
                vm.isSaving = false;
                removeWipEntity();
                //TODO: Change by helper to avoid going to the server
                $location.path("/account/" + vm.account.Oid);
            }).catch(function (error) {
                logError("An error occurred when saving: " + error, true);
                vm.isSaving = false;
            });
        }


        function cancel() {
            datacontext.cancel();
            removeWipEntity();
            helper.replaceLocationUrlGuidWithId(vm.account.Oid);
            if (vm.account.entityAspect.entityState.isDetached()) {
                goToAccountsList();
            }
        }

        vm.title = "accountDetailsController";
        vm.account = {};
        vm.goBack = goBack;
        vm.hasChanges = false;
        vm.save = save;
        vm.cancel = cancel;

        vm.valuationDatePickerIsOpen = false;
        vm.openValuationDatePicker = function () {

            this.valuationDatePickerIsOpen = true;
        };

        vm.dateFormats = ["dd-MMMM-yyyy", "dd-MM-yyyy", "dd/MM/yyyy", "dd/MM/yy", "dd-MM-yy"];
        $scope.dateFormat = vm.dateFormats[2];

        $scope.dateOptions = {
            formatYear: "yyyy",
            startingDay:1
        };

        $scope.status = {opened: false};

        vm.dueDatePickerIsOpen = false;
        vm.paymentDatePickerIsOpen = false;

        Object.defineProperty(vm, "canSave", { get: canSave });

        activate();
    }

    angular.module("app").controller(controllerId, ["$location", "$route", "$routeParams", "$window", "$scope", "config", "common", "helper", "datacontext","model", accountDetailsController]);
})();
