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
			},

			userName : function () {
				if (tokenManager && tokenManager.profile) {
					return tokenManager.profile.given_name
						? tokenManager.profile.given_name
						: null;
				}
			}
		};
	}
})();