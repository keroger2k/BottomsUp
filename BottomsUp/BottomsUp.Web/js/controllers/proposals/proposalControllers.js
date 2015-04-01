(function () {
    'use strict';

    angular.module('bottomsUp').controller('proposalController', proposalController);

    proposalController.$inject = ['initialData'];

    function proposalController(initialData) {

        var vm = this;

        vm.proposals = initialData;
    }

})();


(function () {
    'use strict';

    var module = angular.module('bottomsUp');

    module.factory('dataService', dataService);


    function dataService() {

        this.requirementTasks = [];

        function getRequirementTasks() {
            return this.requirementTasks;
        }

        return {
            requirementTasks: getRequirementTasks
        }
    }

})();


(function () {
    'use strict';

    angular.module('bottomsUp').controller('proposalDetailController', proposalDetailController);

    proposalDetailController.$inject = ['initialData', 'notificationFactory', 'taskService', '$routeParams', 'dataService'];

    function proposalDetailController(initialData, notificationFactory, taskService, $routeParams, dataService) {

        var vm = this;

        vm.proposal = initialData;
        vm.addMode = false;
        vm.dataService = dataService;
        vm.toggleAddMode = toggleAddMode;
        vm.toggleEditMode = toggleEditMode;
        vm.editTask = editTask;
        vm.deleteTask = deleteTask;
        vm.updateTask = updateTask;
        vm.requirementSelected = requirementSelected;
        vm.selectedRequiremnetId = 0;


        var successCallback = function (e, cb) {
            notificationFactory.success();
            taskService.query({
                pid: $routeParams.pid,
                rid: vm.selectedRequiremnetId,
            },
            function (data) {
                dataService.requirementTasks = data;
            });
        };

        function errorCallback(e) {
            notificationFactory.error(e.data.Message);
        }

        function toggleAddMode() {
            vm.addMode = !vm.addMode;
        }

        function toggleEditMode(task) {
            task.editMode = !task.editMode;
        }

        function editTask(task) {
            console.log("Edit Task: " + task.id);
        }

        function deleteTask(task) {
            console.log("Delete Task: " + task.id);
        }

        function requirementSelected(requirement) {
            vm.selectedRequiremnetId = requirement.id;
            dataService.requirementTasks = requirement.tasks;
        }

        function updateTask(task) {
            console.log(this.selectedTask);
            taskService.update({
                pid: $routeParams.pid,
                rid: vselectedRequiremnetId,
                tid: task.id
            }, task, successCallback, errorCallback);
        }
    }
})();

