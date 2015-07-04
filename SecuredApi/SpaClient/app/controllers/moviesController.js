(function () {
    "use strict";

    angular
        .module("securedApi")
        .controller("moviesController",
                     [moviesController]);

    function moviesController() {
        console.log("hello from movies controller");
    }
}());
