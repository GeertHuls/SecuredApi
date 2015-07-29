(function(){
	"use strict";

	angular
		.module("common.services")
		.factory("itemResource",
				["$resource",
				 "appSettings",
				 	itemResource]);
		
		function itemResource ($resource, appSettings) {

			var fetch = function (resource) {
				return $resource(appSettings.serverPath + "/api/" + resource);
			}

			return {
				fetch : fetch
			};
		}
})();