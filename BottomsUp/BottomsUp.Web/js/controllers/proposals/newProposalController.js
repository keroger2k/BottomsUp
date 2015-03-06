
var module = angular.module('homeIndex');

var newProposalsController = function ($scope, $http, $window, dataService) {
    $scope.newProposal = {};

    $scope.save = function () {
        dataService.addProposal($scope.newProposal)
        .then(function () {
            $window.location = "#/";
        },
        function () {
            alert("could not save new proposal");
        });
    }
};

module.controller('newProposalsController', newProposalsController);
