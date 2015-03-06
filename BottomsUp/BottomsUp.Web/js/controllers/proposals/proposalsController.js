var module = angular.module('homeIndex');

var proposalsController = function ($scope, $http, dataService, $window) {
    $scope.data = dataService;
    $scope.isBusy = false;


    if (dataService.isReady() == false) {
        $scope.isBusy = true;
        dataService.getProposals()
            .then(function () {
                //success
            },
            function () {
                alert("failed to get proposals");
            }
            )
            .then(function () {
                $scope.isBusy = false;
            });
    }

    $scope.deleteProposal = function (id) {
        var deleteUser = $window.confirm('Are you absolutely sure you want to delete?');
        if (deleteUser) {
            dataService.deleteProposalById(id).then(function (data) {
                //alert('success');
                //console.log(data);
            },
            function () {
                alert('failure');
            });
        }
    };

    $scope.toggleEditMode = function (proposal) {
        proposal.editMode = !proposal.editMode;
    };
};

module.controller('proposalsController', proposalsController);
