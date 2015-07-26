(function(){
	'use strict';
	
	angular
		.module('securedApi')
		.controller('loginController',
			["tokenManager", "authorizer", "$location", "$scope",
                loginController]);

	function loginController(tokenManager, authorizer, $location, $scope) {
		var vm = this;

		vm.login = function () {
		    tokenManager.redirectForToken();
		};

		vm.logout = function () {
			tokenManager.redirectForLogout();
		};

		vm.isAuthorized = authorizer.isAuthorized;
	}
})();