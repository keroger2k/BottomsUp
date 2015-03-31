var module = angular.module('bottomsUp');

module.controller('taskViewController',
    ['$scope', 'proposalService', 'taskService', '$routeParams', 'notificationFactory',
function ($scope, proposalService, taskService, $routeParams, notificationFactory) {

        var successCallback = function (e, cb) {
        notificationFactory.success();
        $.each($scope.selectedRequirement.tasks, function (index, item) {
            if (e.id == $scope.selectedRequirement.tasks[index].id) {
                taskService.get({
                    pid: $routeParams.pid,
                    rid: $scope.selectedRequirement.id,
                    tid: e.id
                }, function (data) {
                    $scope.selectedRequirement.tasks[index] = data;
                });
            }
        });
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
        console.log(this.selectedTask);
        taskService.update({
            pid: $routeParams.pid,
            rid: $scope.selectedRequirement.id,
            tid: task.id
        }, task, successCallback, errorCallback);
    }

}]);