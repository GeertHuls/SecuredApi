(function(){
	"use strict";

    angular
        .module("securedApi")
        .controller("callBackController",
                     [callBackController]);

    function callBackController() {
        var vm = this;

        console.log("hello from callback!");
    }

})();