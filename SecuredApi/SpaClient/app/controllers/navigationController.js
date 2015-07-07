(function(){
    'use strict';

    angular.module('securedApi')
        .controller('navigationController',
            ['$location',
                navigationController]);

    function navigationController ($location) {
        var vm = this;

        vm.isActive = function (route) {
            var path = $location.path();
            return path === route;
        };
    }
})();