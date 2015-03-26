var module = angular.module('bottomsUp');

module.controller('proposalController',
    ['$scope', 'proposalService',
        function ($scope, proposalService) {
            $scope.proposals = [];

            proposalService.query(function (data) {
                $scope.proposals = data;
            });

        }]);

module.controller('proposalDetailController',
    ['$scope', '$routeParams', 'proposalService',
    function ($scope, $routeParams, proposalService) {
        $scope.selectedRequirement = [];

        $scope.proposal = proposalService.get({
            pid: $routeParams.pid,
            includeRequirements: true
        })

        $scope.requirementSelected = function (requirement) {
            $scope.selectedRequirement = requirement;
        }

    }]);