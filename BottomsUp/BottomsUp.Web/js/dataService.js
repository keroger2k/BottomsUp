var module = angular.module('homeIndex');

module.factory("dataService", function ($http, $q) {
    var _proposals = [];
    var _isInit = false;

    var _isReady = function () {
        return _isInit;
    };

    var _getProposals = function () {

        var deferred = $q.defer();

        $http.get("/api/v1/proposals")
        .then(function (result) {
            angular.copy(result.data, _proposals);
            _isInit = true;
            deferred.resolve();
        },
        function () {
            deferred.reject();
        })

        return deferred.promise;
    };

    var _addProposal = function (newProposal) {

        var deferred = $q.defer();

        $http.post("/api/v1/proposals", newProposal)
            .then(function (result) {
                var np = result.data;
                _proposals.splice(0, 0, np);
                deferred.resolve(np);
            },
            function () {
                deferred.reject();
            });
        return deferred.promise;
    };

    var _getProposalsById = function (id) {
        var deferred = $q.defer();

        if (_isReady()) {
            var prop = _findProposal(id);
            if (prop) {
                deferred.resolve(prop);
            } else {
                deferred.reject();
            }
        } else {
            _getProposals().then(
                function () {
                    var prop = _findProposal(id);
                    if (prop) {
                        deferred.resolve(prop);
                    } else {
                        deferred.reject();
                    }
                },
                function () {
                    deferred.reject();
                });
        }
        return deferred.promise;
    };

    var _findProposal = function (id) {

        var found = null;

        $.each(_proposals, function (i, item) {
            if (item.id == id) {
                found = item;
                return false;
            }
        });

        return found;
    };

    var _deleteProposalById = function (id) {
        var deferred = $q.defer();
        var prop = _findProposal(id);
        if (prop) {
            $.each(_proposals, function (i) {
                if (_proposals[i].id === id) {
                    _proposals.splice(i, 1);
                    return false;
                }
            });
            $http.delete('/api/v1/proposals/' + id)
                .then(function (data) {
                    deferred.resolve(data);
                },
                function () {
                    deferred.reject();
                });
        } else {
            deferred.reject();
        }
        return deferred.promise;
    };

    return {
        proposals: _proposals,
        getProposals: _getProposals,
        addProposal: _addProposal,
        isReady: _isReady,
        getProposalsById: _getProposalsById,
        deleteProposalById: _deleteProposalById
    };

});