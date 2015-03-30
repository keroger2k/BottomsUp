var module = angular.module('bottomsUp');

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

module.factory("taskService", function ($http, $resource) {
    return $resource('api/v1/proposals/:pid/requirements/:rid/tasks/:tid',
         { tid: '@tid', pid: '@pid', rid: '@rid' },
         { 'update': { method: 'PUT' } },
         { 'query': { method: 'GET', isArray: false } });
});

module.factory('notificationFactory', function () {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-left",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    return {
        success: function () {
            toastr.success("Success");
        },
        error: function (text) {
            toastr.error(text, "Error");
        }
    };
});