(function () {
    'use strict';

    var app = angular.module('app');

    app.directive('ccImgPerson', ['config', function (config) {
        //Usage:
        //<img data-cc-img-person="{{s.speaker.imageSource}}"/>
        var basePath = config.imageSettings.imageBasePath;
        var unknownImage = config.imageSettings.unknownPersonImageSource;
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            attrs.$observe('ccImgPerson', function (value) {
                value = basePath + (value || unknownImage);
                attrs.$set('src', value);
            });
        }
    }]);

    app.directive('ccSidebar', function () {
        // Opens and clsoes the sidebar menu.
        // Usage:
        //  <div data-cc-sidebar>
        // Creates:
        //  <div data-cc-sidebar class="sidebar">
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            var $sidebarInner = element.find('.sidebar-inner');
            var $dropdownElement = element.find('.sidebar-dropdown a');
            element.addClass('sidebar');
            $dropdownElement.click(dropdown);

            function dropdown(e) {
                var dropClass = 'dropy';
                e.preventDefault();
                if (!$dropdownElement.hasClass(dropClass)) {
                    hideAllSidebars();
                    $sidebarInner.slideDown(350);
                    $dropdownElement.addClass(dropClass);
                } else if ($dropdownElement.hasClass(dropClass)) {
                    $dropdownElement.removeClass(dropClass);
                    $sidebarInner.slideUp(350);
                }

                function hideAllSidebars() {
                    $sidebarInner.slideUp(350);
                    $('.sidebar-dropdown a').removeClass(dropClass);
                }
            }
        }
    });

    app.directive('ccWidgetClose', function () {
        // Usage:
        // <a data-cc-widget-close></a>
        // Creates:
        // <a data-cc-widget-close="" href="#" class="wclose">
        //     <i class="fa fa-remove"></i>
        // </a>
        var directive = {
            link: link,
            template: '<i class="fa fa-remove"></i>',
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            attrs.$set('href', '#');
            attrs.$set('wclose');
            element.click(close);

            function close(e) {
                e.preventDefault();
                element.parent().parent().parent().hide(100);
            }
        }
    });

    app.directive('ccWidgetMinimize', function () {
        // Usage:
        // <a data-cc-widget-minimize></a>
        // Creates:
        // <a data-cc-widget-minimize="" href="#"><i class="fa fa-chevron-up"></i></a>
        var directive = {
            link: link,
            template: '<i class="fa fa-chevron-up"></i>',
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            //$('body').on('click', '.widget .wminimize', minimize);
            attrs.$set('href', '#');
            attrs.$set('wminimize');
            element.click(minimize);

            function minimize(e) {
                e.preventDefault();
                var $wcontent = element.parent().parent().next('.widget-content');
                var iElement = element.children('i');
                if ($wcontent.is(':visible')) {
                    iElement.removeClass('fa fa-chevron-up');
                    iElement.addClass('fa fa-chevron-down');
                } else {
                    iElement.removeClass('fa fa-chevron-down');
                    iElement.addClass('fa fa-chevron-up');
                }
                $wcontent.toggle(500);
            }
        }
    });

    app.directive('ccScrollToTop', ['$window',
        // Usage:
        // <span data-cc-scroll-to-top></span>
        // Creates:
        // <span data-cc-scroll-to-top="" class="totop">
        //      <a href="#"><i class="fa fa-chevron-up"></i></a>
        // </span>
        function ($window) {
            var directive = {
                link: link,
                template: '<a href="#"><i class="fa fa-chevron-up"></i></a>',
                restrict: 'A'
            };
            return directive;

            function link(scope, element, attrs) {
                var $win = $($window);
                element.addClass('totop');
                $win.scroll(toggleIcon);

                element.find('a').click(function (e) {
                    e.preventDefault();
                    // Learning Point: $anchorScroll works, but no animation
                    //$anchorScroll();
                    $('body').animate({ scrollTop: 0 }, 500);
                });

                function toggleIcon() {
                    $win.scrollTop() > 300 ? element.slideDown() : element.slideUp();
                }
            }
        }
    ]);

    app.directive('ccSpinner', ['$window', function ($window) {
        // Description:
        //  Creates a new Spinner and sets its options
        // Usage:
        //  <div data-cc-spinner="vm.spinnerOptions"></div>
        var directive = {
            link: link,
            restrict: 'A'
        };
        return directive;

        function link(scope, element, attrs) {
            scope.spinner = null;
            scope.$watch(attrs.ccSpinner, function (options) {
                if (scope.spinner) {
                    scope.spinner.stop();
                }
                scope.spinner = new $window.Spinner(options);
                scope.spinner.spin(element[0]);
            }, true);
        }
    }]);

    app.directive('ccWidgetHeader', function () {
        //Usage:
        //<div data-cc-widget-header title="vm.map.title"></div>
        var directive = {
            link: link,
            scope: {
                'title': '@',
                'subtitle': '@',
                'rightText': '@',
                'allowCollapse': '@'
            },
            templateUrl: '/app/layout/widgetheader.html',
            restrict: 'A',
        };
        return directive;

        function link(scope, element, attrs) {
            attrs.$set('class', 'widget-head');
        }
    });

    app.directive("mmWip", ["$route", "wipRouteName", function ($route, wipRouteName) {

        function link(scope, element, attributes) {

            function wipIsCurrent() {
                if (!$route.current || !$route.current.title) {
                    return false;
                }
                return $route.current.title.substr(0, wipRouteName.length) === wipRouteName;
            }

            scope.$watch(wipIsCurrent, function (value) {
                value ?
                    element.addClass("current") :
                    element.removeClass("current");
            });
        }

        function getTemplate() {
            return '<a href="#{{wipRoute.url}}" >'
                + '<i class="fa fa-asterisk" data-ng-class="getWipClass()"></i>'
                + 'Work in Progress ({{wip.length}})</a>';
        }
        
        function wipController($scope) {

            function wipExists() {
                return !!$scope.wip.length;
            }

            function getWipClass() {
                return $scope.wipExists() ?
                    ["fa", "fa-asterisk", "fa-asterisk-alert"] :
                    ["fa", "fa-asterisk"];
            }

            function activate() {
                const eventName = $scope.changedEvent;
                $scope.$on(eventName, function (event, data) {
                    $scope.wip = data.wip;
                });
                $scope.wipRoute = $scope.routes.filter(function (route) {
                    return route.config.title === wipRouteName;
                })[0];
            }

            $scope.wipExists = wipExists;
            $scope.getWipClass = getWipClass;
            $scope.wipRoute = undefined;

            activate();
        }
        
        var directive = {
            controller: ["$scope", wipController],
            link: link,
            template: getTemplate(),
            scope: {
                wip: "=",
                changedEvent: "@",
                routes: "="
            },
            restrict: "A"
        };

        return directive;
    }]);

    app.directive('moDateInput', function ($window) {
        return {
            require: '^ngModel',
            restrict: 'A',
            link: function (scope, elm, attrs, ctrl) {
                var moment = $window.moment;
                scope.dateFormat = attrs.moDateInput;
                attrs.$observe('moDateInput', function (newValue) {
                    if (scope.dateFormat == newValue || !ctrl.$modelValue) return;
                    scope.dateFormat = newValue;
                    ctrl.$modelValue = new Date(ctrl.$setViewValue);
                });

                ctrl.$formatters.unshift(function (modelValue) {
                    scope = scope;
                    if (!scope.dateFormat || !modelValue) return "";
                    var retVal = moment(modelValue).format(scope.dateFormat);
                    return retVal;
                });

                ctrl.$parsers.unshift(function (viewValue) {
                    scope = scope;
                    var date = moment(viewValue, scope.dateFormat);
                    return (date && date.isValid() && date.year() > 1950) ? date.toDate() : "";
                });
            }
        };
    });

    app.directive('mmDate', ['$timeout', '$filter', function ($timeout, $filter)
    {
        return {
            require: 'ngModel',

            link: function ($scope, $element, $attrs, $ctrl) {
                var dateFormat = $attrs.mmDate;
                $ctrl.$parsers.push(function (viewValue)
                {
                    //convert string input into moment data model
                    var pDate = Date.parse(viewValue);
                    if (isNaN(pDate) === false) {
                        return new Date(pDate);
                    }
                    return undefined;

                });
                $ctrl.$formatters.push(function (modelValue)
                {
                    var pDate = Date.parse(modelValue);
                    if (isNaN(pDate) === false) {
                        return $filter('date')(new Date(pDate), dateFormat);
                    }
                    return undefined;
                });
                $element.on('blur', function ()
                {
                    var pDate = Date.parse($ctrl.$modelValue);
                    if (isNaN(pDate) === true) {
                        $ctrl.$setViewValue(null);
                        $ctrl.$render();
                    } else {
                        if ($element.val() !== $filter('date')(new Date(pDate), dateFormat)) {
                            $ctrl.$setViewValue($filter('date')(new Date(pDate), dateFormat));
                            $ctrl.$render();
                        }
                    }

                });
                //$timeout(function ()
                //{
                //    $element.kendoDatePicker({

                //        format: dateFormat
                //    });

                //});
            }
        };
    }])

})();