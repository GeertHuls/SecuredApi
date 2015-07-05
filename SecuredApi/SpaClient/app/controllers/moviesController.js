(function () {
    "use strict";

    angular
        .module("securedApi")
        .controller("moviesController",
                     ["itemResource",
                         moviesController]);

    function moviesController(itemResource) {
        var vm = this;

        itemResource.fetch('movies')
        	.query(function (data) {
	            vm.movies = data;
	        });
    }
}());
