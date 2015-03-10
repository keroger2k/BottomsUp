
var module = angular.module('homeIndex');

var newProposalsController = function ($scope, $http, $window, dataService) {
    $scope.newProposal = {};

    $scope.save = function () {
        dataService.save($scope.person, successPostCallback, errorCallback);
    };
};

module.controller('newProposalsController', newProposalsController);
