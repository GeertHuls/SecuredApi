(function(){
	
	angular
		.module("common.services")
		.factory("movieResource",
				["$resource",
				 "appSettings",
				 	movieResource]);
		
		function movieResource ($resource, appSettings) {
			return $resource(appSettings.serverPath + "/api/movies");
		}
})();