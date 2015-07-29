(function(){
	"use strict";

    angular
        .module("securedApi")
        .controller("callBackController",
                     ["$scope", "$routeParams", "$location", "tokenManager", "notifier",
                     	callBackController]);

    function callBackController($scope, $routeParams, $location, tokenManager, notifier) {

		var hash = $routeParams.response;

		tokenManager.processTokenCallbackAsync(hash)
		.then(function() {
			notifier.notify('user-profile-available');
            $location.url("/");
        }, function (error) {
			if(error && error.message) {
				$scope.error = error.message;
			} else {
				$scope.error = "An error has occured.";
			}
		})
		.then(function () {
			$scope.$apply();
		});
    }

})();
