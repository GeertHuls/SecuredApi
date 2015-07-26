(function(){
    'use strict';

    angular.module('securedApi')
        .controller('navigationController',
            ['$location', 'tokenManager',
                navigationController]);

    function navigationController ($location, tokenManager) {
        var vm = this;

        vm.isActive = function (route) {
            var path = $location.path();
            return path === route;
        };

        vm.isAuthorized = function () {
            return !tokenManager.expired;
        };
    }
})();