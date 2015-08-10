(function(){
    'use strict';

    angular.module('securedApi')
        .controller('navigationController',
            ["$scope", '$location', '$route', 'authorizer', 'notifier',
                navigationController]);

    function navigationController($scope, $location, $route, authorizer, notifier) {
        var vm = this;

        vm.isActive = function (route) {
            var path = $location.path();
            return path === route;
        };

        vm.isAuthorized = authorizer.isAuthorized;

        notifier.subscribe($scope, 'token-expired', function () {
            $location.url('/');
            $route.reload();
        });
    }
})();