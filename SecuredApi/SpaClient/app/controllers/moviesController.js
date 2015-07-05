(function () {
    "use strict";

    angular
        .module("securedApi")
        .controller("moviesController",
                     ["movieResource",
                         moviesController]);

    function moviesController(movieResource) {
        movieResource.query(function (data) {
            console.log(data);
		});
    }
}());
