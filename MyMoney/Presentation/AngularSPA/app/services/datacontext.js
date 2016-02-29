(function () {
    'use strict';

    var serviceId = 'datacontext';
    function datacontext($injector, $rootScope, $http, config, common, model, emFactory, zStorage, zStorageWip) {
        var service;

        var $q = common.$q;
        var getLogFn = common.logger.getLogFn;
        var log = getLogFn(serviceId);
        var logError = getLogFn(serviceId, "error");
        var logSuccess = getLogFn(serviceId, "success");
        var repositoryNames = config.repositoryNames;
        var primePromise;
        var manager = emFactory.newManager();
        var events = config.events;
        var serviceName = config.remoteServiceName + "/api";

        function getRepository(repositoryName) {
            var fullRepositoryName = "repository." + repositoryName.toLowerCase();
            var factory = $injector.get(fullRepositoryName);
            return factory.create(manager);
        }

        function defineLazyLoadedRepositories() {
            repositoryNames.forEach(function (name) {
                Object.defineProperty(service, name, {
                    configurable: true, //will redefine this property once later
                    get: function () {
                        var repository = getRepository(name);
                        Object.defineProperty(service, name, {
                            value: repository,
                            configurable: false,
                            enumerable: true
                        });
                        return repository;
                    }
                });
            });
        }

        function setupEventForHasChangesChanged() {
            manager.hasChangesChanged.subscribe(function (eventArguments) {
                var data = { hasChanges: eventArguments.hasChanges };
                common.$broadcast(events.hasChangesChanged, data);
            });
        }

        function interceptPropertyChange(changeArguments) {
            var changedProperty = changeArguments.args.propertyName;
            //Do your interception here
        }

        function onAccountDeletedRemotely() {
            $rootScope.$on(config.events.accountWasDeleted,
                function (event, data) {
                    console.log("At datacontext " + data);
                    var entityToRemove = manager.getEntityByKey("Account", data);
                    manager.detachEntity(entityToRemove);
                    log("Account oid: '" + data + "' was deleted remotely!", true);
                });
        }

        function setupEventForEntitiesChanged() {
            manager.entityChanged.subscribe(function(changeArguments) {
                if (changeArguments.entityAction === breeze.EntityAction.PropertyChange) {
                    interceptPropertyChange(changeArguments);
                    common.$broadcast(events.entitiesChanged, changeArguments);
                }
            });
            onAccountDeletedRemotely();
        }

        function listenForStorageEvents() {
            $rootScope.$on(config.events.storage.storeChanged, function (event, data) {
                log("Updated local storage", data, true);
            });
            $rootScope.$on(config.events.storage.wipChanged, function (event, data) {
                log("Updated WIP", data, true);
            });
            $rootScope.$on(config.events.storage.error, function (event, data) {
                logError("Error occurred with local storage" + data.activity, data, true);
            });
        }

        function cancel() {
            if (manager.hasChanges()) {
                manager.rejectChanges();
                logSuccess("Canceled changes", null, true);
            }
        }

        function markDeleted(entity) {
            return Entity.EntityAspect.setDeleted();
        }

        function save() {


            function saveSucceed() {
                zStorage.save();
                manager.acceptChanges();
                logSuccess("Saved data", null, true);
            }

            function saveFailed(error) {
                var message = config.appErrorPrefix + "Save failed: " +
                    breeze.saveErrorMessageService.getErrorMessage(error);
                error.Message = message;
                logError(message, error);
                throw Error;
            }

            function saveChangesToWebApi() {
                //get all entity types
                var entityTypes = manager.metadataStore.getEntityTypes();
                var promise;
                var promises = [];

                entityTypes.forEach(function (entityType) {
                    var resourceName = entityType.defaultResourceName;

                    var allChanges = manager.getChanges(entityType);

                    if (allChanges.length == 0) {
                        return;
                    }

                    promise = $q.when(allChanges.forEach(function (change) {
                        var changeType = change.entityAspect.entityState.name;
                        var oid = change._backingStore.Oid;
                        var jsonData = createJsonDataFromChange(change);
                        switch (changeType) {
                            case "Added":
                                var url = serviceName + "/" + resourceName
                                return $http.post(url, jsonData);
                            case "Modified":
                                var url = serviceName + "/" + resourceName + "/" + oid
                                return $http.put(url, jsonData);
                            case "Deleted":
                                var url = serviceName + "/" + resourceName
                                return $http.delete(url);
                        }

                    }));

                    function createJsonDataFromChange(change) {
                        return JSON.stringify(change._backingStore);
                    }

                    promises.push(promise);

                });
                return $q.all(promises);
            }

            return saveChangesToWebApi().then(saveSucceed).catch(saveFailed);

            //return manager.saveChanges().then(saveSucceed).catch(saveFailed);
        }

        function prime() {
            //This function can only be called once
            if (primePromise) return primePromise;

            var storageIsEnabledAndHasData = zStorage.load(manager);

            function loadFromRemoteService() {
                //All data from remote server that must be cached go here
                var promise = $q.all([]);
                promise = promise.then(function () {
                    model.extendMetadata(manager.metadataStore);
                });
                return promise.then(function () {
                    zStorage.save();
                });
            }

            var promise = storageIsEnabledAndHasData ?
                $q.when(log("Loading entities and metadata from local storage")) :
                loadFromRemoteService();

            function success() {
                log("Primed data", service.account.cachedData);
            }

            primePromise = promise.then(success);
            return primePromise;
        }

        function initialize() {
            zStorage.init(manager);
            zStorageWip.init(manager);
            defineLazyLoadedRepositories();
            setupEventForHasChangesChanged();
            setupEventForEntitiesChanged();
            listenForStorageEvents();
        }

        service = {
            //Functions
            prime: prime,
            cancel: cancel,
            markDeleted: markDeleted,
            save: save,
            //Sub-Services
            zStorage: zStorage,
            zStorageWip: zStorageWip,
            //Repositories
            //Will be added on-demand by defineLazyLoadedRepositories method
        };

        initialize();

        return service;
    }

    angular.module('app').factory(serviceId, ["$injector", "$rootScope", "$http", "config", "common", "model", "entityManagerFactory", "zStorage", "zStorageWip", datacontext]);

})();