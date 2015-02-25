/// <reference path="C:\Users\Kyle\Documents\GitHub\BottomsUp\BottomsUp\BottomsUp.Web\Views/Home/Index.cshtml" />
/* Probably need to move this error handling */

$(document).ajaxError(function (event, xhr) {
    alert(xhr.status + ":" + xhr.statusText);
});

// For the time now
Date.prototype.timeNow = function () {
    return ((this.getHours() < 10) ? "0" : "") + this.getHours() + ":" + ((this.getMinutes() < 10) ? "0" : "") + this.getMinutes() + ":" + ((this.getSeconds() < 10) ? "0" : "") + this.getSeconds();
}

Date.prototype.today = function () {
    return this.getFullYear() + "-" + (((this.getMonth() + 1) < 10) ? "0" : "") + (this.getMonth() + 1) + "-" + ((this.getDate() < 10) ? "0" : "") + this.getDate();
}



/* 
 * Bottoms Up Data Object 
 *
 * Revealing Module Pattern
 *
 */

var BottomsUp = BottomsUp || {};

BottomsUp.Data = (function () {

    var getProposals = function () {
        return $.getJSON('/api/Proposals');
    };
    var getProposal = function (id) {
        return $.getJSON('/api/Proposals/' + id);
    };
    var getProposalRequirements = function (id) {
        return $.getJSON('/api/Proposals/' + id + '/requirements');
    };
    var updateProposal = function (proposal) {
        return $.ajax('/api/Proposals/' + proposal.Id, {
            type: "PUT",
            data: proposal
        });

    };
    var addProposal = function (proposal) {
        return $.ajax('/api/Proposals', {
            type: "POST",
            data: proposal
        });
    };
    var deleteProposal = function (id) {
        return $.ajax('/api/Proposals/' + id, {
            type: "DELETE"
        });
    };
    var getRequirement = function (id) {
        return $.getJSON('/api/requirements/' + id);
    };

    return {
        getProposal: getProposal,
        getProposals: getProposals,
        getProposalRequirements: getProposalRequirements,
        updateProposal: updateProposal,
        addProposal: addProposal,
        deleteProposal: deleteProposal,
        getRequirement: getRequirement
    };

}());