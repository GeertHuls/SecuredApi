﻿(function(){
	"use strict";

	angular
		.module("common.services")
		.factory("tokenManager",
				["$browser", "notifier", tokenManager]);
	
	function tokenManager ($browser, notifier) {
		
        var config = {
            client_id: "spaclient",
            authority: "https://secured.local:449/identityserver/core",
            redirect_uri: window.location.protocol + "//" + window.location.host + $browser.baseHref() + "#/cb/",
            post_logout_redirect_uri: window.location.protocol + "//" + window.location.host,
            response_type: "id_token token",
            scope: "openid profile email roles securedapi",
            silent_redirect_uri: "",
            silent_renew: false
        };
        
		var manager = new OidcTokenManager(config);

		manager.addOnTokenExpired(function () {
			notifier.notify('token-expired');
		});

	    return manager;
	}
})();
