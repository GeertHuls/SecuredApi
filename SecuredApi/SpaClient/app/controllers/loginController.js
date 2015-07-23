(function(){
	'use strict';
	
	angular
		.module('securedApi')
		.controller('loginController',
			["tokenManager",
                loginController]);

	function loginController(tokenManager) {
		var vm = this;

		vm.test = function () {
		    tokenManager.redirectForToken();
		}
	}
})();