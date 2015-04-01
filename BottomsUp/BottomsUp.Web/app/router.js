var module = angular.module('bottomsUp', ['ui.router', 'ngResource']);

//module.config(function ($routeProvider) {

//    $routeProvider.when("/", {
//        templateUrl: "/app/proposals/index.html",
//        controller: "proposalController",
//        controllerAs: 'vm',
//        resolve: {
//            initialData: ['proposalService', function (proposalService) {
//                return proposalService.query();
//            }]
//        }
//    });

//    $routeProvider.when("/proposals/:pid/requirements", {
//        templateUrl: "/app/proposals/details.html",
//        controller: "proposalDetailController",
//        controllerAs: 'vm',
//        resolve: {
//            initialData: ['proposalService', '$route', function (proposalService, $route) {
//                return proposalService.get({ pid: $route.current.params.pid, includeRequirements: true });
//            }]
//        }
//    });

//    $routeProvider.otherwise({ redirectTo: "/" });

//});

module.config(function ($stateProvider, $urlRouterProvider) {

    $stateProvider
    .state('home', {
        url: '/',
        templateUrl: '/app/proposals/index.html',
        controller: 'proposalController',
        controllerAs: 'vm',
        resolve: {
            initialData: ['proposalService', function (proposalService) {
                return proposalService.query();
            }]
        }
    })
    .state('requirements', {
        url: 'proposals/:pid/requirements',
        templateUrl: '/app/proposals/details.html',
        controller: 'proposalDetailController',
        controllerAs: 'vm',
        resolve: {
            initialData: ['proposalService', '$stateParams', function (proposalService, $stateParams) {
                return proposalService.get({ pid: $stateParams.pid, includeRequirements: true });
            }]
        }
    });

    $urlRouterProvider.otherwise('/notfound');
});

