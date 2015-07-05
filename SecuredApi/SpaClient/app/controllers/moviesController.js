(function () {
    "use strict";

    angular
        .module("securedApi")
        .controller("moviesController",
                     ["movieResource",
                         moviesController]);

    function moviesController(movieResource) {
        var vm = this;

        movieResource.query(function (data) {
            vm.movies = data;
        });
    }
}());
