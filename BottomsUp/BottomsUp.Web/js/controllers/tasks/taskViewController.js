var module = angular.module('bottomsUp');

module.controller('taskViewController',
    ['$scope', 'proposalService', 'taskService', '$routeParams', 'notificationFactory',
function ($scope, proposalService, taskService, $routeParams, notificationFactory) {

    $scope.tasks = $scope.selectedRequirement.tasks;

    var successCallback = function (e, cb) {
        notificationFactory.success();
        $scope.proposal = proposalService.get({
            pid: $routeParams.pid,
            includeRequirements: true
        })
    };

    var errorCallback = function (e) {
        notificationFactory.error(e.data.Message);
    };

    $scope.toggleAddMode = function () {
        $scope.addMode = !$scope.addMode;
    };

    $scope.toggleEditMode = function (task) {
        task.editMode = !task.editMode;
    };

    $scope.editTask = function (task) {
        console.log("Edit Task: " + task.id);
    }

    $scope.deleteTask = function (task) {
        console.log("Delete Task: " + task.id);
    }

    $scope.updateTask = function (task) {
        taskService.update({
            pid: $routeParams.pid,
            rid: $scope.selectedRequirement.id,
            tid: task.id
        }, task, successCallback, errorCallback);
    }

}]);