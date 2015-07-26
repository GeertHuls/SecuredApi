(function(){
	'use strict';

	angular
		.module('common.services')
		.factory('authorizer',
				['tokenManager',
					authorizer]);

	function authorizer (tokenManager) {
		return {
			isAuthorized : function () {
				return !tokenManager.expired;
			}
		};
	}
})();