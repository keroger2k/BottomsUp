var module = angular.module('bottomsUp', ['ngRoute', 'ngResource']);

module.config(function ($routeProvider) {

    $routeProvider.when("/", {
        templateUrl: "/js/views/proposals/index.html",
        controller: "proposalController",
        controllerAs: 'vm',
        resolve: {
            initialData: ['proposalService', function (proposalService) {
                return proposalService.query();
            }]
        }
    });

    $routeProvider.when("/proposals/:pid/requirements", {
        templateUrl: "/js/views/proposals/details.html",
        controller: "proposalDetailController",
        controllerAs: 'vm',
        resolve: {
            initialData: ['proposalService', '$route', function (proposalService, $route) {
                return proposalService.get({ pid: $route.current.params.pid, includeRequirements: true });
            }]
        }
    });

    $routeProvider.otherwise({ redirectTo: "/" });

});

