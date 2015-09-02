(function () {
    'use strict';

    var app = angular.module('app');

    var namespace = "MyMoney";

    //Entity Names
    var entityNames = {
        account: "Account",
        accountOperationResult: "AccountOperationResult"
    };

    //Repository Names
    var repositoryNames = ["account"];

    // Configure Toastr
    toastr.options.timeOut = 4000;
    toastr.options.positionClass = 'toast-bottom-right';

    var keyCodes = {
        backspace: 8,
        tab: 9,
        enter: 13,
        esc: 27,
        space: 32,
        pageup: 33,
        pagedown: 34,
        end: 35,
        home: 36,
        left: 37,
        up: 38,
        right: 39,
        down: 40,
        insert: 45,
        del: 46
    };

    //var remoteServiceName = 'http://localhost:3270';

    var remoteServiceName = 'https://microsoft-apiappb838c42666d343b29debe9b264b8ef72.azurewebsites.net';
    //Application events
    var events = {
        controllerActivateSuccess: 'controller.activateSuccess',
        spinnerToggle: 'spinner.toggle',
        entitiesChanged: "datacontext.entitiesChanged",
        hasChangesChanged: "datacontext.hasChangesChanged",
        accountWasDeleted: "accountsHub.accountWasDeleted",
        storage: {
            error: "store.error",
            storeChanged: "store.changed",
            wipChanged: "wip.changed"
        }
    };

    var config = {
        namespace: namespace,
        appErrorPrefix: '[My Money Error] ', //Configure the exceptionHandler decorator
        docTitle: 'My Money: ',
        events: events,
        remoteServiceName: remoteServiceName,
        version: '2.1.0',
        keyCodes: keyCodes,
        entityNames: entityNames,
        repositoryNames: repositoryNames
    };

    app.value('config', config);
    
    app.config(['$logProvider', function ($logProvider) {
        // turn debugging off/on (no info or warn)
        if ($logProvider.debugEnabled) {
            $logProvider.debugEnabled(true);
        }
    }]);
    
    //#region Configure the common services via commonConfig
    app.config(['commonConfigProvider', function (cfg) {
        cfg.config.controllerActivateSuccessEvent = config.events.controllerActivateSuccess;
        cfg.config.spinnerToggleEvent = config.events.spinnerToggle;
    }]);
    //#endregion
})();