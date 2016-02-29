(function() {
    "use strict";
    const serviceId = "repository.account";

    function repositoryAccount(model, repositoryAbstract) {


        function createRepository(manager) {
            var entityName = model.entityNames.account;
            var metadataStore = manager.metadataStore;
            var base = new repositoryAbstract(manager, entityName, serviceId);
            var accountType = metadataStore.getEntityType(entityName);
            var accountsQuery = breeze.EntityQuery.from(accountType.defaultResourceName);
            var orderBy = "Name, DueDate, Value";
            var count = undefined;

            function create() {
                return manager.createEntity(entityName);
            }

            function createAccountNamePredicate(filterValue) {
                return breeze.Predicate
                    .create("Name", "contains", filterValue);
            }

            function getAll(forceRefresh, page, size, nameFilter) {
                var take = size || 20;
                var skip = page ? (page - 1) * size : 0;
                var areAccountsLoaded = base.zStorage.areItemsLoaded("accounts");
                var accounts;

                function getLocallyByPage() {
                    var predicate = null;
                    if (nameFilter) {
                        predicate = createAccountNamePredicate(nameFilter);
                    }
                    var accounts = accountsQuery
                        .where(predicate)
                        .orderBy(orderBy)
                        .take(take).skip(skip)
                        .using(base.manager)
                        .executeLocally();
                    return accounts;
                }

                function querySucceed(data) {
                    accounts = data.results;
                    //Set zStorage loaded for Accounts to true
                    base.zStorage.areItemsLoaded("accounts", true);
                    base.log("Retrieved accounts from remote data source: " + accounts.length, true);
                    base.zStorage.save();
                    return getLocallyByPage();
                }

                if (forceRefresh) {
                    base.zStorage.clear();
                }

                if (areAccountsLoaded && !forceRefresh) {
                    return base.$q.when(getLocallyByPage());
                }
                
                accounts = accountsQuery
                    .orderBy(orderBy)
                    .toType(entityName)
                    .using(manager).execute()
                    .then(querySucceed).catch(base.queryFailed);

                return accounts;
            }

            function getCount() {
                var areItemsLoaded = base.zStorage.areItemsLoaded("accounts");
                if (areItemsLoaded) {
                    return base.$q.when(base.getLocalEntityCount(entityName));
                }
                if (count !== undefined) {
                    return base.$q.when(count);
                }
                return accountsQuery.take(0).inlineCount()
                    .using(base.manager).execute()
                    .then(function(data) {
                        return count = data.inlineCount;
                    });
            }

            function getFilteredCount(nameFilter) {
                var predicate = breeze.Predicate
                    .and(createAccountNamePredicate(nameFilter));
                var accounts = accountsQuery
                    .where(predicate)
                    .using(base.manager)
                    .executeLocally();
                return accounts.length;
            }

            var repository = {
                create: create,
                getAll: getAll,
                getCount: getCount,
                getFilteredCount: getFilteredCount,
                getAccountByOid: base.getById,
                getEntityByIdOrFromWip: base.getEntityByIdOrFromWip
            }

            return repository;
        }

        return {
            create: createRepository
        };
    }

    angular.module("app").service(serviceId, ["model", "repository.abstract", repositoryAccount]);

})();