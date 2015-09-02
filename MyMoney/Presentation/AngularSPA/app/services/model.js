(function () {
    "use strict";
    const serviceId = "model";

    function model(config, modelMetadata) {

        var entityNames = config.entityNames;

        function registerAccount(metadataStore) {

            function AccountCtor() {
                //Any unbound property goes here
            }

            //Create Calculated or Formatted properties here:
            Object.defineProperty(AccountCtor.prototype, "isPastDue", {
                get: function () {
                    var dueDate = this.DueDate;
                    var now = new Date();
                    var value = moment(now).isAfter(dueDate);
                    if (value === true) {
                        return "This account is past due. You should have paid it...";
                    }
                    return value;
                }
            });

            Object.defineProperty(AccountCtor.prototype, "FormattedDueDate", {
                get: function () {
                    if (!this.DueDate) {
                        return this.DueDate = moment(new Date()).format("YYYY-MM-DD");
                    }
                    return this.DueDate;
                },
                set: function(value) {
                    this.DueDate = moment(value).format("YYYY-MM-DD");
                }
            });
            Object.defineProperty(AccountCtor.prototype, "FormattedPaymentDate", {
                get: function () {
                    if (!this.PaymentDate) {
                        return this.PaymentDate = moment(new Date()).format("YYYY-MM-DD");
                    }
                    return this.PaymentDate;
                },
                set: function (value) {
                    this.PaymentDate = moment(value).format("YYYY-MM-DD");
                }
            });

            metadataStore.registerEntityTypeCtor(entityNames.account, AccountCtor);
        }

        function configureMetadataStore(metadataStore) {
            registerAccount(metadataStore);
            modelMetadata.initializeMetadataStore(metadataStore);
        }

        //This is the place where we can register different account types
        function registerResourceNames(metadataStore) {
            var types = metadataStore.getEntityTypes();

            function set(resourceName, entityName) {
                metadataStore.setEntityTypeForResourceName(resourceName, entityName);
            }

            types.forEach(function (type) {
                if (type instanceof breeze.EntityType) {
                    set(type.shortName, type);
                }
            });
            var accountEntityName = entityNames.account;
            ["Accounts", "Account"].forEach(function (resource) {
                set(resource, accountEntityName);
            });
        }

        function extendMetadata(metadataStore) {
            //Apply validators here
            registerResourceNames(metadataStore);
        }

        var service = {
            entityNames: entityNames,
            configureMetadataStore: configureMetadataStore,
            extendMetadata: extendMetadata
        }

        return service;
    }

    angular.module("app").service(serviceId,["config", "model.metadata", model]);

})();