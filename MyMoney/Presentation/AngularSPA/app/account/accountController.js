(function () {
    "use strict";
    var controllerId = "accountController";
    function accountController($scope, $location, $route, $routeParams, config, common, datacontext, accountsHub) {
        var vm = this;
        var keyCodes = config.keyCodes;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(controllerId);
        var logError = getLogFn(controllerId, "error");
        var logSuccess = getLogFn(controllerId, "success");

        function getAccountCount() {
            return datacontext.account.getCount().then(function (data) {
                return vm.accountCount = data;
            });
        }

        function getAccountFilteredCount() {
            vm.accountFilteredCount = datacontext.account.getFilteredCount(vm.accountSearch);
        }

        function getAllAccounts(forceRefresh) {
            var accounts = datacontext.account.getAll(forceRefresh, vm.paging.currentPage, vm.paging.pageSize, vm.accountSearch).then(function (data) {
                vm.accounts = data;
                if (!vm.accountCount || forceRefresh) {
                    getAccountCount();
                }
                getAccountFilteredCount();
                return data;
            });
            return accounts;
        }

        function search($event) {
            if ($event.keyCode === keyCodes.esc) {
                vm.accountSearch = "";
            }
            getAllAccounts();
        }

        function goToAccount(account) {
            $location.path("/account/" + account.Oid);
        }

        function createAccount() {
            $location.path("/account/new");
        }

        function accountsFilter(account) {
            var textContains = common.textContains;
            var searchText = vm.accountSearch;
            var isMatch = searchText ? textContains(account.Name, searchText) : true;

            return isMatch;
        }
        
        function refresh() {
            getAllAccounts(true);
        }

        function pageChanged(page) {
            if (!page) {
                return;
            }
            vm.paging.currentPage = page;
            getAllAccounts();
        }

        function onAccountDeletedRemotely() {
            $scope.$on(config.events.accountWasDeleted,
                function (event, data) {
                    console.log("At accounts controller " + data);
                    refresh();
                });
        }

        function activate() {
            var promises = [getAllAccounts()];
            common.activateController(promises, controllerId).then(function() {
                log("Activated Accounts List View");
            }).then(onAccountDeletedRemotely);
        }
        
        vm.title = "Accounts";
        vm.accounts = [];
        vm.accountCount = 0;
        vm.filteredAccounts = [];
        vm.accountFilteredCount = 0;
        vm.accountsFilter = accountsFilter;
        vm.goToAccount = goToAccount;
        vm.accountSearch = $routeParams.search || "";
        vm.search = search;
        vm.createAccount = createAccount;
        vm.refresh = refresh;

        //Paging
        vm.paging = {
            currentPage: 1,
            maxPagesToShow: 5,
            pageSize: 10
        }

        vm.pageChanged = pageChanged;

        Object.defineProperty(vm.paging, "pageCount", {
            get: function () {
                return Math.floor(vm.accountFilteredCount / vm.paging.pageSize) + 1;
            }
        });
        //-

        activate();

    }

    angular.module("app").controller(controllerId, ["$scope", "$location", "$route", "$routeParams", "config", "common", "datacontext","accountsHub", accountController]);
})();