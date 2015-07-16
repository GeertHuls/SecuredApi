(function(){
	'use strict';
	
	angular
		.module('securedApi')
		.controller('loginController',
			[loginController]);

	function loginController () {
		var vm = this;

		vm.test = function () {
			console.log('test');
		}
	}
})();