(function(){
	'use strict';

	angular
		.module('common.services')
		.factory('authorizer',
				['tokenManager',
					authorizer]);

	function authorizer(tokenManager) {

	    var isAuthenticated = function () {
	        return !tokenManager.expired;
	    };

	    return {
	        isAuthenticated : isAuthenticated,

			isAuthorized : function (role) {

				if (!tokenManager ||
					tokenManager.expired ||
					!tokenManager.profile) {
					return false;
				}

				var profile = tokenManager.profile;
				return profile.role && profile.role.indexOf(role) >= 0;
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