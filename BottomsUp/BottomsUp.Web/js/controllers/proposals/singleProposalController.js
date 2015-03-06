var module = angular.module('homeIndex');

var singleProposalController = function ($scope, dataService, $window, $routeParams) {

    $scope.name = null;

    dataService.getProposalsById($routeParams.id)
    .then(function (proposal) {
        $scope.proposal = proposal;
    },
    function () {
        //error
        $window.location = "#/";
    });
};

module.controller('singleProposalController', singleProposalController);