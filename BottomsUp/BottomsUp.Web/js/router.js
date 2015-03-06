
var module = angular.module('homeIndex');

module.config(function ($routeProvider) {

    $routeProvider.when("/", {
        controller: "proposalsController",
        templateUrl: "/templates/proposals/proposalsView.html"
    });

    $routeProvider.when("/newproposal", {
        controller: "newProposalsController",
        templateUrl: "/templates/proposals/newProposalView.html"
    });

    $routeProvider.when("/proposal/:id", {
        controller: "singleProposalController",
        templateUrl: "/templates/proposals/singleProposalView.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });

});