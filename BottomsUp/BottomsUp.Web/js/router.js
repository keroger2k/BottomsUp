
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

module.factory("proposalService", function ($http, $resource) {
    return $resource('api/v1/proposals/:pid',
         { pid: '@id' },
         { 'update': { method: 'PUT' } },
         { 'query': { method: 'GET', isArray: false } });
});


module.factory("requirementService", function ($http, $resource) {
    return $resource('api/v1/proposals/:pid/requirements/:rid',
         { pid: '@pid', rid: '@rid' },
         { 'update': { method: 'PUT' } },
         { 'query': { method: 'GET', isArray: false } });
});

module.controller('proposalController',
    ['$scope', 'proposalService',
        function ($scope, proposalService) {
            $scope.proposals = [];

            proposalService.query(function (data) {
                $scope.proposals = data;
            });

        }]);

module.controller('proposalDetailController',
    ['$scope', '$routeParams', 'requirementService',
    function ($scope, $routeParams, requirementService) {
        $scope.requirements = [];

        requirementService.query({ rid: $routeParams.rid, pid: $routeParams.pid, includeTasks: true }, function (data) {
            $scope.requirements = data;
        });

        $scope.requirementSelected = function (requirement) {
            console.log(requirement);
        }

    }]);