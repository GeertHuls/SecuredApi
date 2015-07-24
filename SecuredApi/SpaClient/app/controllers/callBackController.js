(function(){
	"use strict";

    angular
        .module("securedApi")
        .controller("callBackController",
                     ["$scope", "$routeParams", "$location", "tokenManager",
                     	callBackController]);

    function callBackController($scope, $routeParams, $location, tokenManager) {

		var hash = $routeParams.response;

		tokenManager.processTokenCallbackAsync(hash).then(function() {
            $location.url("/");
            $scope.$apply();
        }, function (error) {
        	if(error && error.message) {
        		$scope.error = error.message;
        	} else {
        		$scope.error = "An error has occured.";
        	}
        	$scope.$apply();
		});
    }

})();
