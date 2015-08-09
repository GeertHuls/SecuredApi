﻿(function(){
	"use strict";

	angular
		.module("common.services")
		.factory("tokenManager",
				["$browser", "notifier", "appSettings", tokenManager]);
	
	function tokenManager ($browser, notifier, appSettings) {
		
		console.log(appSettings);
        var config = {
            client_id: "spaclient",
            authority: appSettings.identityServerUrl,
            redirect_uri: window.location.protocol + "//" + window.location.host + $browser.baseHref() + "#/cb/",
            post_logout_redirect_uri: window.location.protocol + "//" + window.location.host + $browser.baseHref(),
            response_type: "id_token token",
            scope: "openid profile email roles securedapi",
            silent_redirect_uri: window.location.protocol + "//" + window.location.host + $browser.baseHref() + "app/views/frame.html",
            silent_renew: true
        };

		var manager = new OidcTokenManager(config);

		manager.addOnTokenExpired(function () {
			notifier.notify('token-expired');
			manager.removeToken();
		});

	    return manager;
	}
})();
