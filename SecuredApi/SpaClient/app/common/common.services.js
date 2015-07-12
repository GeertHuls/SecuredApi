(function () {
	"use strict";

	angular
		.module("common.services",
						["ngResource"])
		.constant("appSettings",
		{
			serverPath: "https://secured.local:449/resourceserver"
		});
}());