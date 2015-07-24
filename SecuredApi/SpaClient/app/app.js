(function () {
    "use strict";

    var app = angular.module("securedApi",
        ["common.services", "ngRoute"]);

    app.config(["$locationProvider", "$routeProvider",
        function ($locationProvider, $routeProvider) {

        $routeProvider.when("/", {
            templateUrl: "app/views/home.html"
        });

        $routeProvider.when("/movies", {
            templateUrl: "app/views/movies.html",
            controller: "moviesController as vm"
        });

        $routeProvider.when("/books", {
            templateUrl: "app/views/books.html",
            controller: "booksController as vm"
        });

        $routeProvider.when("/cb/:response", {
           templateUrl: "app/views/message.html",
           controller: "callBackController as vm"
        });

    }]);

}());