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

module.factory("tasksService", function ($http, $resource) {
    return $resource('api/v1/proposals/:pid/requirements/:rid/tasks/:tid',
         { tid: '@tid', pid: '@pid', rid: '@rid' },
         { 'update': { method: 'PUT' } },
         { 'query': { method: 'GET', isArray: false } });
});