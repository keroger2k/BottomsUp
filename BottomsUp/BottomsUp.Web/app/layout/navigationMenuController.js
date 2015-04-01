var module = angular.module('bottomsUp');

module.controller('navigationMenuController',
    function (proposalService) {
        this.tab = 1;

        this.selectTab = function (setTab) {
            this.tab = setTab;
        };

        this.isSelected = function (checkTab) {
            return this.tab === checkTab;
        };

    });