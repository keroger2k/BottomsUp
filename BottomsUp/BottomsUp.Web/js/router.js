
var module = angular.module('bottomsUp', ['ngRoute', 'ngResource']);

module.config(function ($routeProvider) {

    $routeProvider.when("/", {
        controller: "proposalController",
        templateUrl: "/js/views/proposals/index.html"
    });

    $routeProvider.when("/proposals/:pid", {
        controller: "proposalDetailController",
        templateUrl: "/js/views/proposals/details.html"
    });

    $routeProvider.otherwise({ redirectTo: "/" });

});

module.factory("dataService", function ($http, $resource) {
    return $resource('api/v1/proposals/:pid',
         { pid: '@id' },
         { 'update': { method: 'PUT' } },
         { 'query': { method: 'GET', isArray: false } });
});

module.controller('proposalController', ['$scope', 'dataService', function ($scope, dataService) {
    $scope.proposals = [];

    dataService.query(function (data) {
        $scope.proposals = data;
    });

}]);

module.controller('proposalDetailController', ['$scope', '$routeParams', 'dataService', function ($scope, $routeParams, dataService) {
    $scope.proposal = dataService.get({ pid: $routeParams.pid, includeRequirements: true });
}]);