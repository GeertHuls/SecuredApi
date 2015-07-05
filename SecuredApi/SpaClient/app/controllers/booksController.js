(function () {
    "use strict";

    angular
        .module("securedApi")
        .controller("booksController",
                     ["itemResource",
                         booksController]);

    function booksController(itemResource) {
        var vm = this;

        itemResource.fetch('books')
        	.query(function (data) {
        	    vm.books = data;
        	});
    }
}());
