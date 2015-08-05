(function () {
	"use strict";

	var common = angular
		.module("common.services",
						["ngResource"]);

	var model = document.getElementById("model").textContent.trim();
	model = JSON.parse(model);

	for (var key in model) {
		common.constant(key, model[key]);
	}
}());