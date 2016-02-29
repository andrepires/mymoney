(function () {
    'use strict';

    var controllerId = 'sidebar';

    function sidebar($route, config, routes, datacontext) {
        var vm = this;

        vm.isCurrent = isCurrent;

        //WIP
        vm.wip = [];
        vm.routes = routes;
        vm.wipChangedEvent = config.events.storage.wipChanged;
        //-

        activate();

        function activate() {
            getNavRoutes();
            //WIP
            vm.wip = datacontext.zStorageWip.getWipSummary();
            //-
        }

        function getNavRoutes() {
            vm.navRoutes = routes.filter(function (r) {
                return r.config.settings && r.config.settings.nav;
            }).sort(function (r1, r2) {
                return r1.config.settings.nav - r2.config.settings.nav;
            });
        }

        function isCurrent(route) {
            if (!route.config.title || !$route.current || !$route.current.title) {
                return '';
            }
            var menuName = route.config.title;
            return $route.current.title.substr(0, menuName.length) === menuName ? 'current' : '';
        }
    };

    angular.module('app').controller(controllerId,
        ['$route', 'config', 'routes', "datacontext", sidebar]);

})();
