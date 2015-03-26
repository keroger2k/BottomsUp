var module = angular.module('bottomsUp', ['ngRoute', 'ngResource']);

module.config(function ($routeProvider) {

    $routeProvider.when("/", {
        controller: "proposalController",
        templateUrl: "/js/views/proposals/index.html"
    });

    $routeProvider.when("/proposals/:pid/requirements", {
        controller: "proposalDetailController",
        templateUrl: "/js/views/proposals/details.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });

});

