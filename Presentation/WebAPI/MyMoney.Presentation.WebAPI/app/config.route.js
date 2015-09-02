(function () {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('routes', getRoutes());
    
    // Configure the routes and route resolvers
    app.config(['$routeProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, routes) {

        routes.forEach(function (r) {
            setRoute(r.url, r.config);
        });
        $routeProvider.otherwise({ redirectTo: '/' });

        function setRoute(url, definition) {
            definition.resolve = angular.extend(definition.resolve || {}, {
                prime: prime
            });
            $routeProvider.when(url, definition);
            return $routeProvider;;
        }
    }

    prime.$inject = ["datacontext"];

    function prime(dc) { return dc.prime(); }

    // Define the routes 
    function getRoutes() {
        return [
            {
                url: '/',
                config: {
                    templateUrl: 'app/dashboard/dashboard.html',
                    title: 'Dashboard',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Dashboard'
                    }
                }
            }, {
                url: '/admin',
                config: {
                    title: 'Admin Page',
                    templateUrl: 'app/admin/admin.html',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-lock"></i> Admin'
                    }
                }
            }, {
                url: '/account',
                config: {
                    title: 'Accounts List',
                    templateUrl: 'app/account/accountList.html',
                    settings: {
                        nav: 3,
                        content: '<i class="fa fa-money"></i> Accounts'
                    }
                }
            }, {
                url: '/account/search/:search',
                config: {
                    title: 'accounts-search',
                    templateUrl: 'app/account/accountList.html',
                    settings: {}
                }
            }, {
                url: '/account/:oid',
                config: {
                    templateUrl: 'app/account/accountDetails.html',
                    title: 'account',
                    settings: {}
                }
            }, {
                url: '/workinprogress',
                config: {
                    templateUrl: 'app/wip/wip.html',
                    title: 'workinprogress',
                    settings: {
                        content: '<i class="fa fa-asterisk"></i> Work In Progress'
                    }
                }
            }
        ];
    }
})();