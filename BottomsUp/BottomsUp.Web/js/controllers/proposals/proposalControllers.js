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
    ['$scope', '$routeParams', 'proposalService', 'notificationFactory',
function ($scope, $routeParams, proposalService, notificationFactory) {
    $scope.selectedRequirement = [];
    $scope.addMode = false;

    $scope.requirementSelected = function (requirement) {
        $scope.selectedRequirement = requirement;
    }

    $scope.proposal = proposalService.get({
        pid: $routeParams.pid,
        includeRequirements: true
    })

}]);