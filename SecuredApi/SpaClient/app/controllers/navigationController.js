(function(){
    'use strict';

    angular.module('securedApi')
        .controller('navigationController',
            ['$location', 'authorizer',
                navigationController]);

    function navigationController($location, authorizer) {
        var vm = this;

        vm.isActive = function (route) {
            var path = $location.path();
            return path === route;
        };

        vm.isAuthorized = authorizer.isAuthorized;
    }
})();