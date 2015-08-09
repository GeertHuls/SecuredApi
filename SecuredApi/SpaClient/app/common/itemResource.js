(function(){
	"use strict";

	angular
		.module("common.services")
		.factory("itemResource",
				["$resource",
				 "appSettings", "authorizer",
				 	itemResource]);
		
		function itemResource ($resource, appSettings, authorizer) {
			var fetch = function (resource) {
			    return $resource(appSettings.resourceServerUrl + "/api/" + resource, null, {
						'query': {
							isArray: true,
							headers: { 'Authorization': 'Bearer ' + authorizer.token }
						}
					});
			}

			return {
				fetch : fetch
			};
		}
})();