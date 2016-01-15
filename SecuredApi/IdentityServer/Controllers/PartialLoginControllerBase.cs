using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityServer3.Core.Extensions;
using Microsoft.Owin;

namespace IdentityServer.Controllers
{
    public class PartialLoginControllerBase : Controller
    {
        protected async Task<ClaimsIdentity> EnsurePartialSignedUserFound()
        {
            var partialSignInUser = await GetOwinContext()
                .Environment.GetIdentityServerPartialLoginAsync();

            if (partialSignInUser == null)
            {
                throw new InvalidOperationException("Partial signed-in user not found.");
            }

            return partialSignInUser;
        }

        protected IOwinContext GetOwinContext()
        {
            return Request.GetOwinContext();
        }
    }
}