(function () {
    "use strict";

    function accountsHub($rootScope, Hub, common, config) {
        var events = config.events;
        const serviceName = config.remoteServiceName + "/signalr";

        //declaring the hub connection
        const hub = new Hub("accountsHub", {

            //client side methods
            listeners: {
                accountWasDeleted: function (oid) {
                    common.$broadcast(events.accountWasDeleted, oid);
                }
            },

            //server side methods
            methods: [],

            //query params sent on initial connection
            queryParams: {
                'token': "exampletoken"
            },

            //handle connection error
            errorHandler: function (error) {
                console.error(error);
            },

            //specify a non default root
            rootPath: serviceName,

            stateChanged: function (state) {
                switch (state.newState) {
                case $.signalR.connectionState.connecting:
                    console.log("Connecting to Accounts Hub");
                    break;
                case $.signalR.connectionState.connected:
                    console.log("Connected to Accounts Hub");
                    break;
                case $.signalR.connectionState.reconnecting:
                    console.log("Re-connecting to Accounts Hub");
                    break;
                case $.signalR.connectionState.disconnected:
                    console.log("Disconnected from Accounts Hub");
                    break;
                }
            }
        });
        return {};
    }

    angular.module("app").factory("accountsHub", ["$rootScope", "Hub", "common", "config", accountsHub]);
})();