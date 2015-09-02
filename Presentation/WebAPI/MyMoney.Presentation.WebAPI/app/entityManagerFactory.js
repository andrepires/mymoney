(function () {
    "use strict";
    const serviceId = "entityManagerFactory";

    function entityManagerFactory(config, model) {
        var serviceName = config.remoteServiceName + "/api";

        var dataService = new breeze.DataService({
            serviceName: serviceName,
            hasServerMetadata: false,
            useJsonp: true
        });

        function createMetadataStore() {
            var store = new breeze.MetadataStore();
            model.configureMetadataStore(store);
            store.addDataService(dataService);
            return store;
        }

        var metadataStore = createMetadataStore();

        function newManager() {
            var manager = new breeze.EntityManager({
                serviceName: serviceName,
                metadataStore: metadataStore
            });
            return manager;
        }

        var provider = {
            metadataStore: metadataStore,
            newManager: newManager
        };

        return provider;

    }

    var app = angular.module("app");

    //app.value(
    //"jsonResultsAdapter", new breeze.JsonResultsAdapter({
    //    name: "MyMoney",
    //    extractResults: function (data) {
    //        var results = data.results;
    //        if (!results) throw new Error("Unable to resolve 'results' property.");
    //        return results && (results.Oks || results.Errors);
    //    },
    //    visitNode: function (node, parseContext, nodeContext) {
    //        if (node.Oid && node.Oks) {
    //            return { entityType: "Account" };
    //        }
    //    }
    //}));

    app.service(serviceId, ["config", "model", entityManagerFactory]);

})();