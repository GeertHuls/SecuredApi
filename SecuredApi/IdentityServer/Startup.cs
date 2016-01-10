﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Facebook;
using IdentityServer;
using IdentityServer.Config;
using IdentityServer.Helpers;
using IdentityServer.UserStore;
using IdentityServer3.Core;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/core", idsrvApp =>
            {
                var identityServerServiceFactory = new IdentityServerServiceFactory()
                    .UseInMemoryClients(Clients.Get())
                    .UseInMemoryScopes(Scopes.Get());

                var customUserService = new CustomUserService();
                identityServerServiceFactory.UserService = new Registration<IUserService>(resolver => customUserService);

                identityServerServiceFactory.CorsPolicyService =
                    new Registration<ICorsPolicyService>(
                        new DefaultCorsPolicyService
                        {
                            AllowAll = true
                        });

                var identityServerOptions = new IdentityServerOptions
                {
                    SiteName = "SecuredApi IdentityServer",
                    IssuerUri = "https://securedapiidsrv/embedded",
                    SigningCertificate = LoadCertificate(),

                    AuthenticationOptions = new AuthenticationOptions
                    {
                        IdentityProviders = ConfigureIdentityProviders,
                        EnablePostSignOutAutoRedirect = true,
                        PostSignOutAutoRedirectDelay = 2,
                        LoginPageLinks = new List<LoginPageLink>()
                        {
                            new LoginPageLink()
                            {
                                Type= "createaccount",
                                Text = "Create a new account",
                                Href = "~/createuseraccount"
                            }
                        },
                    },

                    Factory = identityServerServiceFactory
                };

                idsrvApp.UseIdentityServer(identityServerOptions);
            });
        }

        private void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            ConfigureFacebookProvider(app, signInAsType);
            ConfigureAzureAdProvider(app, signInAsType);
        }

        private static void ConfigureFacebookProvider(IAppBuilder app, string signInAsType)
        {
            var options = new FacebookAuthenticationOptions
            {
                AuthenticationType = "Facebook",
                Caption = "Facebook",
                SignInAsAuthenticationType = signInAsType,
                AppId = "XXXXXXXXXXXXXXXX",
                AppSecret = "XXXXXXXXXXXXXXXX",
                Provider = new FacebookAuthenticationProvider
                {
                    OnAuthenticated = async context =>
                    {
                        var facebookClient = new FacebookClient(context.AccessToken);
                        var facebookClaims = await facebookClient
                            .GetTaskAsync<JsonObject>("/me?fields=first_name,last_name,email");
                       
                        if (facebookClaims != null)
                        {
                            object firstName;
                            if (facebookClaims.TryGetValue("first_name", out firstName))
                            {
                                context.Identity.AddClaim(new Claim(
                                    Constants.ClaimTypes.GivenName,
                                    firstName.ToString()));
                            }

                            object lastName;
                            if (facebookClaims.TryGetValue("last_name", out lastName))
                            {
                                context.Identity.AddClaim(new Claim(
                                    Constants.ClaimTypes.FamilyName,
                                    lastName.ToString()));
                            }

                            object email;
                            if (facebookClaims.TryGetValue("email", out email))
                            {
                                context.Identity.AddClaim(new Claim(
                                    Constants.ClaimTypes.Email,
                                    email.ToString()));
                            }
                        }
                    }
                }
            };

            options.Scope.Add("email");

            app.UseFacebookAuthentication(options);
        }

        private static void ConfigureAzureAdProvider(IAppBuilder app, string signInAsType)
        {
            var aad = new OpenIdConnectAuthenticationOptions
            {
                AuthenticationType = "aad",
                Caption = "Azure AD",
                SignInAsAuthenticationType = signInAsType,
                Authority = "https://login.windows.net/xxxxxxxxxxxxxxx",
                ClientId = "cdb67f7b-0a52-450b-a555-fe01ee12abc2",
                RedirectUri = "https://secured.local:449/identityserver/core/aadcb",
                PostLogoutRedirectUri = "https://secured.local:449/spaclient",
                Scope = "openid profile email roles"
            };

            app.UseOpenIdConnectAuthentication(aad);
        }
        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\Certificates\secured.local.pfx",
                AppDomain.CurrentDomain.BaseDirectory), "");
        }
    }
}
