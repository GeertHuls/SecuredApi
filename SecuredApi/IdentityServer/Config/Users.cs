using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer3.Core;
using IdentityServer3.Core.Services.InMemory;

namespace IdentityServer.Config
{
    public static class Users
    {
        public static List<InMemoryUser> Get()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Username = "Geert",
                    Password = "secret",
                    Subject = "1",

                    Claims = new[]
                    {
                        new Claim(Constants.ClaimTypes.GivenName, "Geert"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Huls"),
                        new Claim(Constants.ClaimTypes.Role, "Books"),
                        new Claim(Constants.ClaimTypes.Role, "Movies")
                    }
                }
            };
        }
    }
}