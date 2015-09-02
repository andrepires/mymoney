(function () {
    "use strict";
    const serviceId = "repository.abstract";

    function abstractRepository(common, config, zStorage, zStorageWip) {
        var $q = common.$q;

        function getAllLocal(resource, ordering, predicate) {
            if (predicate) {
                return breeze.EntityQuery.from(resource)
                .where(predicate)
                .orderBy(ordering)
                .using(this.manager)
                .executeLocally();
            }
            return breeze.EntityQuery.from(resource)
                .orderBy(ordering)
                .using(this.manager)
                .executeLocally();
        }

        function getById(id, forceRemote) {
            var self = this;
            var entityName = self.entityName;
            var manager = self.manager;
            if (!forceRemote) {
                var entity = manager.getEntityByKey(entityName, id);
                if (entity) {
                    self.log("Retrieved [" + entityName + "] id: " + entity.Oid + " from cache.", entity, true);
                    if (entity.entityAspect.entityState.isDeleted()) {
                        entity = null;
                    }
                    return $q.when(entity);
                }
            }

            function querySucceeded(data) {
                entity = data.entity;
                if (!entity) {
                    self.log("Could not find [" + entityName + "] id: " + id, null, true);
                    return null;
                }
                self.log("Retrieved [" + entityName + "] id: " + entity.Oid + " from remote data source.", entity, true);
                zStorage.save();
                return entity;
            }

            return manager.fetchEntityByKey(entityName, id).then(querySucceeded).catch(self.Failed);

        }

        function getEntityByIdOrFromWip(val) {
            var self = this;
            var entityName = self.entityName;
            var wipEntityKey = undefined;
            var wipResult = null;

            //val can be either Guid or number
            if (common.isNumber(val)) {
                val = parseInt(val);
                wipEntityKey = zStorageWip.findWipKeyByEntityId(entityName, val);
                if (!wipEntityKey) {
                    return getById.bind(self)(val);
                }
            }

            if (common.isGuid(val)) {
                wipEntityKey = zStorageWip.findWipKeyByEntityId(entityName, val);
                if (!wipEntityKey) {
                    return getById.bind(self)(val);
                }
            }

            var importedEntity = zStorageWip.loadWipEntity(wipEntityKey);
            if (importedEntity) {
                importedEntity.entityAspect.validateEntity();
                wipResult = { entity: importedEntity, key: wipEntityKey };
            } else {
                self.log("Could not find [" + entityName + "] oid in WIP: " + wipEntityKey, null, true);
            }
            return $q.when(wipResult);

        }
        function getLocalEntityCount(resource) {
            var entities = breeze.EntityQuery.from(resource)
                .using(this.manager)
                .executeLocally();
            return entities.length;
        }

        function queryFailed(error) {
            var message = config.appErrorPrefix + "Error retrieving data. " + (error.message || "");
            this.logError(message, error);
            return $q.reject(new Error(message));
        }

        function ctor(entityManager, entityName, repositoryServiceId) {
            this.entityName = entityName;
            this.getById = getById.bind(this);
            this.getEntityByIdOrFromWip = getEntityByIdOrFromWip.bind(this);
            this.log = common.logger.getLogFn(repositoryServiceId);
            this.logError = common.logger.getLogFn(repositoryServiceId, "error");
            this.manager = entityManager;
            this.queryFailed = queryFailed.bind(this);
        }

        ctor.prototype = {
            constructor: ctor,
            $q: $q,
            getAllLocal: getAllLocal,
            getLocalEntityCount: getLocalEntityCount,
            zStorage: zStorage,
            zStorageWip: zStorageWip
        };

        return ctor;
    }

    angular.module("app").service(serviceId, ["common", "config", "zStorage", "zStorageWip", abstractRepository]);

   
})();