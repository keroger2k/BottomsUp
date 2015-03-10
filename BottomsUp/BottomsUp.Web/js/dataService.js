var module = angular.module('homeIndex');

module.factory("dataService", function ($http, $resource) {
    return $resource('api/v1/proposals/:id',
         { id: '@@id' },
         { 'update': { method: 'PUT' } },
         { 'query': { method: 'GET', isArray: false } });
});