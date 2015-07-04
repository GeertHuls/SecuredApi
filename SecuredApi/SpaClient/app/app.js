(function () {
    "use strict";

    var app = angular.module("securedApi", ["ngRoute"]);

    app.config(["$locationProvider", "$routeProvider",
        function ($locationProvider, $routeProvider) {

        $routeProvider.when("/", {
            templateUrl: "app/views/home.html"
        });

        $routeProvider.when("/movies", {
            templateUrl: "app/views/movies.html"
        });

        $routeProvider.when("/books", {
            templateUrl: "app/views/books.html"
        });

    }]);

}());