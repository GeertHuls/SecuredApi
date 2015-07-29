(function(){
	'use strict';
	
	angular
		.module('securedApi')
		.controller('loginController',
			['$scope', 'tokenManager', 'authorizer', 'notifier',
                loginController]);

	function loginController($scope, tokenManager, authorizer, notifier) {
		var vm = this;

		vm.login = function () {
		    tokenManager.redirectForToken();
		};

		vm.logout = function () {
			tokenManager.redirectForLogout();
		};

		vm.isAuthorized = authorizer.isAuthorized;

		vm.userName = authorizer.userName();

		notifier.subscribe($scope, 'user-profile-available', function () {
			vm.userName = authorizer.userName();
		})
	}
})();