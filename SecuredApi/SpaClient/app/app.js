(function () {
    "use strict";

    var app = angular.module("securedApi", ["ngRoute"]);

    //use $location? http://stackoverflow.com/questions/13832529/how-to-config-routeprovider-and-locationprovider-in-angularjs
    app.config(["$routeProvider", function ($routeProvider) {
        $routeProvider.when("/", {
            templateUrl: "/app/views/home.html"
        });
    }]);

}());