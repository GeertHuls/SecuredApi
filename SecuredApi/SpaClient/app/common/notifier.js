(function(){
	'use strict';

	angular
		.module("common.services")
			.factory("notifier",
					["$rootScope",
					 	notifier]);

	function notifier ($rootScope) {
		return {
			subscribe: function(scope, evt, callback) {
				var handler = $rootScope.$on(evt, callback);
				scope.$on('$destroy', handler);
			},

			notify: function(evt) {
				$rootScope.$emit(evt);
			}
    	};
	}
})();