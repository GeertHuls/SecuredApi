using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityServer.Models;
using IdentityServer.UserStore;
using IdentityServer.UserStore.Model;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using Microsoft.Owin;

namespace IdentityServer.Controllers
{
    public class CompleteAdditionalInformationController : Controller
    {
        public async Task<ActionResult> Index()
        {
            await EnsurePartialSignedUserFound();

            return View(new CompleteAdditionalInformationModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(CompleteAdditionalInformationModel model)
        {
            var partialSignInUser = await EnsurePartialSignedUserFound();

            if (!ModelState.IsValid) return View();

            CreateAccount(model, partialSignInUser);

            var environment = GetOwinContext().Environment;
            return Redirect(await environment.GetPartialLoginResumeUrlAsync());
        }

        private void CreateAccount(CompleteAdditionalInformationModel model,
            ClaimsIdentity partialSignInUser)
        {
            var newUser = new User
            {
                Subject = Guid.NewGuid().ToString(),
                IsActive = true
            };

            newUser.UserLogins.Add(new UserLogin()
            {
                Subject = newUser.Subject,
                LoginProvider = "windows",
                ProviderKey = partialSignInUser
                    .Claims
                    .First(c => c.Type == "external_provider_user_id")
                    .Value
            });

            newUser.UserClaims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = newUser.Subject,
                ClaimType = Constants.ClaimTypes.Email,
                ClaimValue = partialSignInUser
                    .Claims
                    .First(c => c.Type == Constants.ClaimTypes.Email)
                    .Value
            });

            newUser.UserClaims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = newUser.Subject,
                ClaimType = Constants.ClaimTypes.GivenName,
                ClaimValue = model.FirstName
            });


            newUser.UserClaims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = newUser.Subject,
                ClaimType = Constants.ClaimTypes.FamilyName,
                ClaimValue = model.LastName
            });

            newUser.UserClaims.Add(new UserClaim()
            {
                Id = Guid.NewGuid().ToString(),
                Subject = newUser.Subject,
                ClaimType = "role",
                ClaimValue = model.Role
            });

            Save(newUser);
        }

        private async Task<ClaimsIdentity> EnsurePartialSignedUserFound()
        {
            var partialSignInUser = await GetOwinContext()
                .Environment.GetIdentityServerPartialLoginAsync();

            if (partialSignInUser == null)
            {
                throw new InvalidOperationException("Partial signed-in user not found.");
            }

            return partialSignInUser;
        }

        private IOwinContext GetOwinContext()
        {
            return Request.GetOwinContext();
        }

        private void Save(User newUser) =>
            UserRepositoryFactory.Create().SaveUser(newUser);
    }
}