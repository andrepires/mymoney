//(function () {
//    "use strict";
//    const serviceId = "jsonResultsAdapter";
    
//    angular.module("app").value(serviceId, new breeze.JsonResultsAdapter({
//        name: "MyMoney",
//        extractResults: function(data) {
//            var results = data.results;
//            if (!results) throw new Error("Unable to resolve 'results' property.");
//            return results && (results.Oks || results.Errors);
//        }
//        //visitNode: function (node, parseContext, nodeContext) {
//        //    if (node.Oid && node.Oks) {
//        //        return { entityType: "Account" };
//        //    }
//        //}
//    }));
//})();